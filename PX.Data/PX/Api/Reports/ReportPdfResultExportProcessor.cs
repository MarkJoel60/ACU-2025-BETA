// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportPdfResultExportProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Data;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.Reports;

internal class ReportPdfResultExportProcessor : CommandProcessor<PX.Api.Models.Field>
{
  protected readonly Content ExportSchema;
  private readonly Report _report;
  private readonly string _screenId;
  private readonly IReportRunner _reportRunner;
  private readonly IReportRenderer _reportRenderer;
  private static readonly string ReportResultsName = "ReportResults";
  private static readonly string PdfContentName = "PdfContent";
  private static readonly string HTMLContentName = "HtmlContent";
  private static readonly string PreProcessName = "PreProcess";

  public ReportPdfResultExportProcessor(
    Content exportSchema,
    Report report,
    string screenId,
    IReportRunner reportRunner,
    IReportRenderer reportRenderer)
  {
    this.ExportSchema = exportSchema;
    this._report = report;
    this._screenId = screenId;
    this._reportRunner = reportRunner;
    this._reportRenderer = reportRenderer;
  }

  public override bool CanExecute(Command cmd)
  {
    if (!base.CanExecute(cmd) || !string.Equals(cmd.ObjectName, ReportPdfResultExportProcessor.ReportResultsName))
      return false;
    return string.Equals(cmd.FieldName, ReportPdfResultExportProcessor.PdfContentName) || string.Equals(cmd.FieldName, ReportPdfResultExportProcessor.HTMLContentName) || string.Equals(cmd.FieldName, ReportPdfResultExportProcessor.PreProcessName);
  }

  public override void Execute(PX.Api.Models.Field actionCmd)
  {
    Container container = ((IEnumerable<Container>) this.ExportSchema.Containers).First<Container>((Func<Container, bool>) (schemaContainer => schemaContainer.Name == ReportPdfResultExportProcessor.ReportResultsName));
    string key1 = "SubmitReportKeyPreProcessIsActive$" + this._screenId;
    if (PXSharedUserSession.CurrentUser.ContainsKey(key1))
    {
      object instanceId = (object) Guid.NewGuid();
      string key2 = "SubmitReportKeyPreProcessInstance$" + this._screenId;
      if (!PXSharedUserSession.CurrentUser.ContainsKey(key2))
        PXSharedUserSession.CurrentUser.Add(key2, instanceId);
      else
        instanceId = PXSharedUserSession.CurrentUser[key2];
      Exception error;
      switch (this._reportRunner.GetStatus(instanceId, out error))
      {
        case PXLongRunStatus.NotExists:
          this._reportRunner.Start(instanceId, this._report, this._screenId);
          break;
        case PXLongRunStatus.Completed:
          ReportNode result = this._reportRunner.GetResult(instanceId);
          if (result != null)
          {
            this.Render(result, container);
            ((ItemNode) result).Dispose();
          }
          this._reportRunner.Clear(instanceId);
          PXSharedUserSession.CurrentUser.Remove(key2);
          PXSharedUserSession.CurrentUser.Remove(key1);
          break;
        case PXLongRunStatus.Aborted:
          this._reportRunner.Clear(instanceId);
          PXSharedUserSession.CurrentUser.Remove(key2);
          PXSharedUserSession.CurrentUser.Remove(key1);
          throw error;
      }
    }
    else
    {
      ReportNode reportNode = (ReportNode) null;
      try
      {
        Guid instanceId = this._reportRunner.Start(this._report, this._screenId);
        Exception error;
        PXLongRunStatus status = this._reportRunner.GetStatus((object) instanceId, out error);
        while (status == PXLongRunStatus.InProcess)
          status = this._reportRunner.GetStatus((object) instanceId, out error);
        if (status == PXLongRunStatus.Completed)
          reportNode = this._reportRunner.PopResult((object) instanceId);
        if (status == PXLongRunStatus.Aborted)
        {
          this._reportRunner.Clear((object) instanceId);
          throw error;
        }
        this.Render(reportNode, container);
      }
      finally
      {
        ((ItemNode) reportNode)?.Dispose();
      }
    }
  }

  private void Render(ReportNode reportNode, Container container)
  {
    PX.Api.Models.Field field1 = ((IEnumerable<PX.Api.Models.Field>) container.Fields).FirstOrDefault<PX.Api.Models.Field>((Func<PX.Api.Models.Field, bool>) (field => field.FieldName == ReportPdfResultExportProcessor.PdfContentName));
    if (field1 != null)
      field1.Value = Convert.ToBase64String(this._reportRenderer.RenderReportToByteArray(reportNode, "PDF"));
    PX.Api.Models.Field field2 = ((IEnumerable<PX.Api.Models.Field>) container.Fields).FirstOrDefault<PX.Api.Models.Field>((Func<PX.Api.Models.Field, bool>) (field => field.FieldName == ReportPdfResultExportProcessor.HTMLContentName));
    if (field2 == null)
      return;
    field2.Value = Convert.ToBase64String(this._reportRenderer.RenderReportToByteArray(reportNode, "HTML"));
  }
}
