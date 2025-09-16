// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.PrintEmailLienWaiverBaseService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Graphs;
using PX.Objects.CN.Compliance.CL.Models;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services;

internal class PrintEmailLienWaiverBaseService : IPrintEmailLienWaiverBaseService
{
  protected PrintEmailLienWaiversProcess PrintEmailLienWaiversProcess;
  protected ILienWaiverReportCreator LienWaiverReportCreator;
  protected IEnumerable<ComplianceDocument> ComplianceDocuments;
  protected LienWaiverReportGenerationModel LienWaiverReportGenerationModel;

  public PrintEmailLienWaiverBaseService(PXGraph graph)
  {
    this.PrintEmailLienWaiversProcess = (PrintEmailLienWaiversProcess) graph;
    this.LienWaiverReportCreator = ((PXGraph) this.PrintEmailLienWaiversProcess).GetService<ILienWaiverReportCreator>();
  }

  public bool IsLienWaiverValid(ComplianceDocument complianceDocument)
  {
    return complianceDocument.DocumentTypeValue.HasValue && EnumerableExtensions.IsNotIn<int?>(complianceDocument.ProjectID, new int?(), new int?(0)) && complianceDocument.VendorID.HasValue;
  }

  public virtual async Task Process(
    List<ComplianceDocument> complianceDocuments,
    CancellationToken cancellationToken)
  {
    PrintEmailLienWaiverBaseService waiverBaseService = this;
    ((PXGraph) waiverBaseService.PrintEmailLienWaiversProcess).Persist();
    waiverBaseService.ComplianceDocuments = complianceDocuments.Where<ComplianceDocument>(new Func<ComplianceDocument, bool>(waiverBaseService.IsLienWaiverValid));
    foreach (ComplianceDocument complianceDocument in waiverBaseService.ComplianceDocuments)
    {
      foreach (NotificationSourceModel notificationSourceModel in waiverBaseService.CreateNotificationSourceModels(complianceDocument))
        await waiverBaseService.ProcessLienWaiver(notificationSourceModel, complianceDocument, cancellationToken);
    }
  }

  protected virtual Task ProcessLienWaiver(
    NotificationSourceModel notificationSourceModel,
    ComplianceDocument complianceDocument,
    CancellationToken cancellationTokens)
  {
    PXProcessing.SetCurrentItem((object) complianceDocument);
    this.LienWaiverReportGenerationModel = this.LienWaiverReportCreator.CreateReport(notificationSourceModel.NotificationSource.ReportID, complianceDocument, notificationSourceModel.IsJointCheck);
    return Task.CompletedTask;
  }

  protected void UpdateLienWaiverProcessedStatus(ComplianceDocument complianceDocument)
  {
    complianceDocument.IsProcessed = new bool?(true);
    ((PXSelectBase) this.PrintEmailLienWaiversProcess.LienWaivers).Cache.Update((object) complianceDocument);
    ((PXSelectBase) this.PrintEmailLienWaiversProcess.LienWaivers).Cache.Persist((PXDBOperation) 1);
  }

  private string GetLienWaiverType(int? lienWaiverTypeId)
  {
    return ((PXGraph) this.PrintEmailLienWaiversProcess).Select<ComplianceAttribute>().Single<ComplianceAttribute>((Expression<Func<ComplianceAttribute, bool>>) (ca => ca.AttributeId == lienWaiverTypeId)).Value;
  }

  private static bool IsNotificationSourceValid(NotificationSource notificationSource)
  {
    return notificationSource != null && notificationSource.ReportID != null && notificationSource.Active.GetValueOrDefault();
  }

  private IEnumerable<NotificationSourceModel> CreateNotificationSourceModels(
    ComplianceDocument complianceDocument)
  {
    List<NotificationSourceModel> notificationSourceModels = new List<NotificationSourceModel>();
    this.AddNotificationSourceModelIfRequired((ICollection<NotificationSourceModel>) notificationSourceModels, complianceDocument.DocumentTypeValue, complianceDocument.VendorID, false);
    if (complianceDocument.JointVendorInternalId.HasValue)
      this.AddNotificationSourceModelIfRequired((ICollection<NotificationSourceModel>) notificationSourceModels, complianceDocument.DocumentTypeValue, complianceDocument.JointVendorInternalId, true);
    return (IEnumerable<NotificationSourceModel>) notificationSourceModels;
  }

  private void AddNotificationSourceModelIfRequired(
    ICollection<NotificationSourceModel> notificationSourceModels,
    int? lienWaiverType,
    int? vendorId,
    bool isJointCheck)
  {
    NotificationSourceModel notificationSourceModel = this.CreateNotificationSourceModel(lienWaiverType, vendorId, isJointCheck);
    if (!PrintEmailLienWaiverBaseService.IsNotificationSourceValid(notificationSourceModel.NotificationSource))
      return;
    notificationSourceModels.Add(notificationSourceModel);
  }

  private NotificationSourceModel CreateNotificationSourceModel(
    int? lienWaiverTypeId,
    int? vendorId,
    bool isJointCheck)
  {
    NotificationSource notificationSource = this.GetNotificationSource(lienWaiverTypeId, vendorId);
    return new NotificationSourceModel()
    {
      NotificationSource = notificationSource,
      VendorId = vendorId,
      IsJointCheck = isJointCheck
    };
  }

  private NotificationSource GetNotificationSource(int? lienWaiverTypeId, int? vendorId)
  {
    Guid? noteId = ((PXGraph) this.PrintEmailLienWaiversProcess).Select<PX.Objects.CR.BAccount>().Single<PX.Objects.CR.BAccount>((Expression<Func<PX.Objects.CR.BAccount, bool>>) (v => v.BAccountID == vendorId)).NoteID;
    string lienWaiverType = this.GetLienWaiverType(lienWaiverTypeId);
    return this.GetNotificationSource(lienWaiverType, noteId) ?? this.GetNotificationSourceForVendorClass(lienWaiverType, vendorId);
  }

  private NotificationSource GetNotificationSource(string lienWaiverType, Guid? vendorNoteId)
  {
    return PXResultset<NotificationSource>.op_Implicit(PXSelectBase<NotificationSource, PXViewOf<NotificationSource>.BasedOn<SelectFromBase<NotificationSource, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<NotificationSetup>.On<BqlOperand<NotificationSource.setupID, IBqlGuid>.IsEqual<NotificationSetup.setupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.notificationCD, Equal<P.AsString>>>>>.And<BqlOperand<NotificationSource.refNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>>.Config>.Select((PXGraph) this.PrintEmailLienWaiversProcess, new object[2]
    {
      (object) lienWaiverType,
      (object) vendorNoteId
    }));
  }

  private NotificationSource GetNotificationSourceForVendorClass(
    string lienWaiverType,
    int? vendorId)
  {
    return PXResultset<NotificationSource>.op_Implicit(PXSelectBase<NotificationSource, PXViewOf<NotificationSource>.BasedOn<SelectFromBase<NotificationSource, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<NotificationSetup>.On<BqlOperand<NotificationSource.setupID, IBqlGuid>.IsEqual<NotificationSetup.setupID>>>, FbqlJoins.Inner<Vendor>.On<BqlOperand<NotificationSource.classID, IBqlString>.IsEqual<Vendor.vendorClassID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.notificationCD, Equal<P.AsString>>>>>.And<BqlOperand<Vendor.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.PrintEmailLienWaiversProcess, new object[2]
    {
      (object) lienWaiverType,
      (object) vendorId
    }));
  }
}
