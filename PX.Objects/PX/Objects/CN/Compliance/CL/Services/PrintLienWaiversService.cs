// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.PrintLienWaiversService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Models;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services;

internal class PrintLienWaiversService(PXGraph graph) : 
  PrintEmailLienWaiverBaseService(graph),
  IPrintLienWaiversService,
  IPrintEmailLienWaiverBaseService
{
  private PXReportRequiredException reportRequiredException;

  public override async Task Process(
    List<ComplianceDocument> complianceDocuments,
    CancellationToken cancellationToken)
  {
    await base.Process(complianceDocuments, cancellationToken);
    if (this.reportRequiredException != null)
    {
      ((PXBaseRedirectException) this.reportRequiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw this.reportRequiredException;
    }
  }

  protected override async Task ProcessLienWaiver(
    NotificationSourceModel notificationSourceModel,
    ComplianceDocument complianceDocument,
    CancellationToken cancellationToken)
  {
    PrintLienWaiversService lienWaiversService = this;
    // ISSUE: reference to a compiler-generated method
    await lienWaiversService.\u003C\u003En__1(notificationSourceModel, complianceDocument, cancellationToken);
    await lienWaiversService.ConfigurePrintActionParameters(notificationSourceModel.NotificationSource.ReportID, notificationSourceModel.NotificationSource.NBranchID, cancellationToken);
    lienWaiversService.UpdateLienWaiverProcessedStatus(complianceDocument);
    PXProcessing.SetProcessed();
  }

  private async Task ConfigurePrintActionParameters(
    string reportId,
    int? branchId,
    CancellationToken cancellationToken)
  {
    PrintLienWaiversService lienWaiversService = this;
    lienWaiversService.reportRequiredException = PXReportRequiredException.CombineReport(lienWaiversService.reportRequiredException, reportId, lienWaiversService.LienWaiverReportGenerationModel.Parameters, false, (CurrentLocalization) null);
    int num = await SMPrintJobMaint.CreatePrintJobGroups(SMPrintJobMaint.AssignPrintJobToPrinter(new Dictionary<PrintSettings, PXReportRequiredException>(), lienWaiversService.GetReportParametersForDeviceHub(), (IPrintable) ((PXSelectBase<ProcessLienWaiversFilter>) lienWaiversService.PrintEmailLienWaiversProcess.Filter).Current, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) lienWaiversService.PrintEmailLienWaiversProcess).SearchPrinter), "Vendor", reportId, reportId, branchId, (CurrentLocalization) null), cancellationToken) ? 1 : 0;
  }

  private Dictionary<string, string> GetReportParametersForDeviceHub()
  {
    return new Dictionary<string, string>()
    {
      ["ComplianceDocument.ComplianceDocumentID"] = this.LienWaiverReportGenerationModel.Parameters["ComplianceDocumentId"],
      ["IsJointCheck"] = this.LienWaiverReportGenerationModel.Parameters["IsJointCheck"]
    };
  }
}
