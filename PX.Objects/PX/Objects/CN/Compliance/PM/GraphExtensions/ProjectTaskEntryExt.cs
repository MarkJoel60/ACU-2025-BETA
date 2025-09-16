// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.GraphExtensions.ProjectTaskEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.PM.GraphExtensions;

public class ProjectTaskEntryExt : 
  PXGraphExtension<ComplianceViewEntityExtension<ProjectTaskEntry, PMTask>, ProjectTaskEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocument, Where<ComplianceDocument.costTaskID, Equal<Current<PMTask.taskID>>, Or<ComplianceDocument.revenueTaskID, Equal<Current<PMTask.taskID>>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  public PXSetup<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> LienWaiverSetup;
  private ComplianceDocumentService service;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.ValidateComplianceSetup();
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<ProjectTaskEntry>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForProjectTask(((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  private void ValidateComplianceSetup()
  {
    if (((PXSelectBase<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>) this.LienWaiverSetup).Current == null)
      throw new PXSetupNotEnteredException<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>();
  }

  public IEnumerable complianceDocuments()
  {
    List<ComplianceDocument> list = this.GetComplianceDocuments().ToList<ComplianceDocument>();
    this.service.ValidateComplianceDocuments((PXCache) null, (IEnumerable<ComplianceDocument>) list, ((PXSelectBase) this.ComplianceDocuments).Cache);
    return (IEnumerable) list;
  }

  protected virtual void PmTask_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs args,
    PXRowSelected baseHandler)
  {
    if (!(args.Row is PMTask))
      return;
    baseHandler.Invoke(cache, args);
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    if (PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) ((PXGraphExtension<ProjectTaskEntry>) this).Base.Project).Select(Array.Empty<object>()))?.Status == "L")
      ((PXSelectBase) this.ComplianceDocuments).Cache.SetAllEditPermissions(false);
    else
      ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<ProjectTaskEntry>) this).Base.Task).Cache.Inserted);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PMTask> args)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<PMTask>>) args).Cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMTask> args)
  {
    PMTask row = args.Row;
    if (row == null)
      return;
    foreach (ComplianceDocument complianceDocument1 in this.GetComplianceDocuments())
    {
      int? nullable1 = complianceDocument1.CostTaskID;
      int? nullable2 = row.TaskID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        ComplianceDocument complianceDocument2 = complianceDocument1;
        nullable2 = new int?();
        int? nullable3 = nullable2;
        complianceDocument2.CostTaskID = nullable3;
      }
      nullable2 = complianceDocument1.RevenueTaskID;
      nullable1 = row.TaskID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        ComplianceDocument complianceDocument3 = complianceDocument1;
        nullable1 = new int?();
        int? nullable4 = nullable1;
        complianceDocument3.RevenueTaskID = nullable4;
      }
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument1);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    this.service.UpdateExpirationIndicator(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceDocument> args)
  {
    PMTask current = ((PXSelectBase<PMTask>) ((PXGraphExtension<ProjectTaskEntry>) this).Base.Task).Current;
    if (current == null)
      return;
    ComplianceDocument row = args.Row;
    if (row == null)
      return;
    this.FillProjectTaskInfo(row, current);
  }

  private void FillProjectTaskInfo(ComplianceDocument complianceDocument, PMTask task)
  {
    string type = task.Type;
    if (type != null)
    {
      if (type == "Cost" || type == "CostRev")
        complianceDocument.CostTaskID = task.TaskID;
      if (type == "Rev" || type == "CostRev")
        complianceDocument.RevenueTaskID = task.TaskID;
    }
    complianceDocument.ProjectID = task.ProjectID;
    complianceDocument.CustomerID = task.CustomerID;
    complianceDocument.CustomerName = this.GetCustomerName(complianceDocument.CustomerID);
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<PMTask>) ((PXGraphExtension<ProjectTaskEntry>) this).Base.Task).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>, Where2<Where<ComplianceDocument.costTaskID, Equal<Required<PMTask.taskID>>, Or<ComplianceDocument.revenueTaskID, Equal<Required<PMTask.taskID>>>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>((PXGraph) ((PXGraphExtension<ProjectTaskEntry>) this).Base)).Select(new object[2]
      {
        (object) ((PXSelectBase<PMTask>) ((PXGraphExtension<ProjectTaskEntry>) this).Base.Task).Current.TaskID,
        (object) ((PXSelectBase<PMTask>) ((PXGraphExtension<ProjectTaskEntry>) this).Base.Task).Current.TaskID
      }).FirstTableItems.ToList<ComplianceDocument>();
  }

  private string GetCustomerName(int? customerId)
  {
    if (!customerId.HasValue)
      return (string) null;
    return PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ProjectTaskEntry>) this).Base, new object[1]
    {
      (object) customerId
    }))?.AcctName;
  }
}
