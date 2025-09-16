// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.LienWaiverReportCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Graphs;
using PX.Objects.CN.Compliance.CL.Models;
using PX.Objects.CR;
using PX.Reports;
using PX.Reports.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services;

public class LienWaiverReportCreator : ILienWaiverReportCreator
{
  private readonly PrintEmailLienWaiversProcess printEmailLienWaiversProcess;
  private readonly UploadFileMaintenance uploadFileMaintenance;
  private readonly IReportLoaderService reportLoader;
  private readonly IReportRenderer reportRenderer;
  private bool isLinkToLienWaiverNeeded;
  private bool isJointCheck;

  public LienWaiverReportCreator(
    PXGraph graph,
    IReportLoaderService reportLoaderService,
    IReportRenderer reportRendererService)
  {
    this.reportLoader = reportLoaderService;
    this.reportRenderer = reportRendererService;
    this.printEmailLienWaiversProcess = (PrintEmailLienWaiversProcess) graph;
    this.uploadFileMaintenance = PXGraph.CreateInstance<UploadFileMaintenance>();
  }

  public LienWaiverReportGenerationModel CreateReport(
    string reportId,
    ComplianceDocument complianceDocument,
    bool isCheckForJointVendor,
    string format = "PDF",
    bool shouldLinkToLienWaiver = true)
  {
    this.isLinkToLienWaiverNeeded = shouldLinkToLienWaiver;
    this.isJointCheck = isCheckForJointVendor;
    LienWaiverReportGenerationModel reportGenerationModel = this.GetLienWaiverReportGenerationModel(reportId, complianceDocument);
    this.reportLoader.InitDefaultReportParameters(reportGenerationModel.Report, (IDictionary<string, string>) reportGenerationModel.Parameters);
    using (StreamManager streamManager = new StreamManager())
    {
      this.reportRenderer.Render(format, reportGenerationModel.Report, (Hashtable) null, streamManager);
      reportGenerationModel.ReportFileInfo = this.SaveReportFile(streamManager, complianceDocument, format);
    }
    return reportGenerationModel;
  }

  private LienWaiverReportGenerationModel GetLienWaiverReportGenerationModel(
    string reportId,
    ComplianceDocument complianceDocument)
  {
    return new LienWaiverReportGenerationModel()
    {
      Report = this.reportLoader.LoadReport(reportId, (IPXResultset) null),
      Parameters = this.GetReportParameters(complianceDocument)
    };
  }

  private Dictionary<string, string> GetReportParameters(ComplianceDocument complianceDocument)
  {
    return new Dictionary<string, string>()
    {
      ["ComplianceDocumentId"] = complianceDocument.ComplianceDocumentID.ToString(),
      ["IsJointCheck"] = this.isJointCheck.ToString()
    };
  }

  private FileInfo SaveReportFile(
    StreamManager streamManager,
    ComplianceDocument complianceDocument,
    string format)
  {
    string contractCd = ((PXGraph) this.printEmailLienWaiversProcess).Select<PX.Objects.CT.Contract>().SingleOrDefault<PX.Objects.CT.Contract>((Expression<Func<PX.Objects.CT.Contract, bool>>) (ct => ct.ContractID == complianceDocument.ProjectID))?.ContractCD;
    string vendorCd = this.GetVendorCd(complianceDocument);
    this.DeleteFileIfNeeded(complianceDocument.NoteID, contractCd, vendorCd);
    FileInfo fileInfo = new FileInfo(this.GetNameForLienWaiverReport(complianceDocument, format, contractCd, vendorCd), (string) null, streamManager.MainStream.GetBytes());
    this.uploadFileMaintenance.SaveFile(fileInfo, (FileExistsAction) 1);
    this.LinkToLienWaiverIfNeeded(fileInfo.UID, complianceDocument.NoteID);
    return fileInfo;
  }

  private void LinkToLienWaiverIfNeeded(Guid? fileNoteId, Guid? lienWaiverNoteId)
  {
    if (!this.isLinkToLienWaiverNeeded)
      return;
    ((PXGraph) this.printEmailLienWaiversProcess).Caches[typeof (NoteDoc)].Insert((object) LienWaiverReportCreator.CreateNoteDoc(fileNoteId, lienWaiverNoteId));
    ((PXGraph) this.printEmailLienWaiversProcess).Caches[typeof (NoteDoc)].Persist((PXDBOperation) 2);
  }

  private void DeleteFileIfNeeded(
    Guid? complianceDocumentNoteId,
    string projectCd,
    string vendorCd)
  {
    PXResultset<UploadFile> waiverReportFileQuery = this.GetLienWaiverReportFileQuery(complianceDocumentNoteId, projectCd, vendorCd);
    UploadFile uploadFile = GraphHelper.RowCast<UploadFile>((IEnumerable) waiverReportFileQuery).SingleOrDefault<UploadFile>();
    if (uploadFile == null)
      return;
    NoteDoc noteDoc = GraphHelper.RowCast<NoteDoc>((IEnumerable) waiverReportFileQuery).Single<NoteDoc>();
    ((PXGraph) this.printEmailLienWaiversProcess).Caches[typeof (UploadFile)].PersistDeleted((object) uploadFile);
    ((PXGraph) this.printEmailLienWaiversProcess).Caches[typeof (NoteDoc)].PersistDeleted((object) noteDoc);
  }

  private string GetNameForLienWaiverReport(
    ComplianceDocument complianceDocument,
    string format,
    string projectCd,
    string vendorCd)
  {
    DateTime? businessDate = ((PXGraph) this.printEmailLienWaiversProcess).Accessinfo.BusinessDate;
    string reportFileExtension = LienWaiverReportCreator.GetReportFileExtension(format);
    return $"{complianceDocument.NoteID}\\LW-{projectCd}-{vendorCd}-{businessDate:MM-dd-yyyy}{reportFileExtension}";
  }

  private string GetVendorCd(ComplianceDocument complianceDocument)
  {
    int? vendorId = this.isJointCheck ? complianceDocument.JointVendorInternalId : complianceDocument.VendorID;
    return ((PXGraph) this.printEmailLienWaiversProcess).Select<BAccount>().SingleOrDefault<BAccount>((Expression<Func<BAccount, bool>>) (ven => ven.BAccountID == vendorId))?.AcctCD;
  }

  private static string GetReportFileExtension(string format)
  {
    return !(format == "Excel") ? ".pdf" : ".xlsx";
  }

  private PXResultset<UploadFile> GetLienWaiverReportFileQuery(
    Guid? complianceDocumentNoteId,
    string projectCd,
    string vendorCd)
  {
    string str = $"{complianceDocumentNoteId}\\LW-{projectCd}-{vendorCd}";
    return PXSelectBase<UploadFile, PXViewOf<UploadFile>.BasedOn<SelectFromBase<UploadFile, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<NoteDoc>.On<BqlOperand<UploadFile.fileID, IBqlGuid>.IsEqual<NoteDoc.fileID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NoteDoc.noteID, Equal<P.AsGuid>>>>>.And<BqlOperand<UploadFile.name, IBqlString>.Contains<P.AsString>>>>.Config>.Select((PXGraph) this.printEmailLienWaiversProcess, new object[2]
    {
      (object) complianceDocumentNoteId,
      (object) str
    });
  }

  private static NoteDoc CreateNoteDoc(Guid? fileId, Guid? complianceDocumentNoneId)
  {
    return new NoteDoc()
    {
      FileID = fileId,
      NoteID = complianceDocumentNoneId
    };
  }
}
