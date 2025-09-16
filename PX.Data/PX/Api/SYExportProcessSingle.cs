// Decompiled with JetBrains decompiler
// Type: PX.Api.SYExportProcessSingle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.Threading;

#nullable disable
namespace PX.Api;

public class SYExportProcessSingle : SYExportProcess
{
  public PXSave<SYMappingActive> Save;
  public PXCancel<SYMappingActive> Cancel;
  public PXFirst<SYMappingActive> First;
  public PXPrevious<SYMappingActive> Prev;
  public PXNext<SYMappingActive> Next;
  public PXLast<SYMappingActive> Last;
  public PXAction<SYMappingActive> Prepare;
  public PXAction<SYMappingActive> Export;
  public PXAction<SYMappingActive> PrepareExport;
  public PXAction<SYMappingActive> Rollback;
  public PXAction<SYMappingActive> SwitchActivation;
  public PXAction<SYMappingActive> SwitchActivationUntilError;
  public PXAction<SYMappingActive> SwitchProcessing;
  public PXAction<SYMappingActive> ShowUploadPanel;
  public PXAction<SYMappingActive> GetLatestFile;
  public PXAction<SYMappingActive> ClearErrors;
  public PXFilter<SYUploadPanel> UploadPanel;
  public PXSelect<SYMappingActive, Where<SYMappingActive.mappingType, Equal<SYMapping.mappingType.typeExport>, And<SYMapping.isActive, Equal<PX.Data.True>>>> MappingsSingle;
  public PXSelect<SYMappingActive, Where<SYMappingActive.name, Equal<Current<SYMappingActive.name>>>> MappingsSingleDetails;
  private PXScreenToSiteMapViewHelper screenToSiteMapViewHelper;
  private ProcessSingleActivator activator;

  protected override bool HideSave => false;

  protected override bool RestrictPreparedDataUpdate => false;

  public SYExportProcessSingle()
  {
    if (this.activator != null)
      this.activator.EnableFields();
    this.screenToSiteMapViewHelper = new PXScreenToSiteMapViewHelper("EB", this.Caches[typeof (SYMappingActive)], new PXAction[4]
    {
      (PXAction) this.First,
      (PXAction) this.Prev,
      (PXAction) this.Next,
      (PXAction) this.Last
    }, new System.Type[2]
    {
      typeof (SYMappingActive.name),
      typeof (SYMapping.sitemapTitle)
    });
  }

  protected IEnumerable mappingsSingle()
  {
    return (IEnumerable) PXSYMappingSelector.GetMappings<SYMappingActive>(new PXView((PXGraph) this, false, this.MappingsSingle.View.BqlSelect));
  }

  protected override bool GenerateDynamicInConstructor
  {
    get
    {
      if (this.activator == null)
        this.activator = new ProcessSingleActivator((SYProcess) this, this.PrepareExport, this.Prepare, this.Rollback, this.Export, this.ShowUploadPanel, this.GetLatestFile);
      return this.activator.GenerateDynamicColumns;
    }
  }

  [PXButton(Tooltip = "Prepare & Export", Category = "Processing")]
  [PXUIField(DisplayName = "Prepare & Export", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  public IEnumerable prepareExport(PXAdapter adapter)
  {
    this.PerformOperation("C");
    return adapter.Get();
  }

  [PXButton(Tooltip = "Prepare", Category = "Processing", IsLockedOnToolbar = true, DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Prepare", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  public IEnumerable prepare(PXAdapter adapter)
  {
    this.PerformOperation("P");
    return adapter.Get();
  }

  [PXButton(Tooltip = "On the Prepared Data tab, delete all records, and on the History tab, delete all records up to the currently selected one on the History tab.", Category = "Processing")]
  [PXUIField(DisplayName = "Clear Data", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  public IEnumerable rollback(PXAdapter adapter)
  {
    this.activator.PerformRollback();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Export", Category = "Processing", IsLockedOnToolbar = true, DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Export", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  public IEnumerable export(PXAdapter adapter)
  {
    this.PerformOperation("I");
    return adapter.Get();
  }

  [PXButton(Tooltip = "Change the activation status for all rows.")]
  [PXUIField(DisplayName = "Toggle Activation")]
  public IEnumerable switchActivation(PXAdapter adapter)
  {
    this.activator.SwitchActivation();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Clear the activation status for all rows before the row with an error.")]
  [PXUIField(DisplayName = "Clear Activation Until Error")]
  public IEnumerable switchActivationUntilError(PXAdapter adapter)
  {
    this.activator.SwitchActivationUntilError();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Change processing status for all rows.")]
  [PXUIField(DisplayName = "Toggle Processing")]
  public IEnumerable switchProcessing(PXAdapter adapter)
  {
    this.activator.SwitchProcessing();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Upload a new version of the file attached to the provider.", Category = "Data")]
  [PXUIField(DisplayName = "Upload File Version")]
  protected void showUploadPanel()
  {
    if (this.UploadPanel.AskExt() != WebDialogResult.OK)
      return;
    FileInfo file = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo["sessionKey_fileUploadProcess"];
    if (file == null)
      return;
    this.SaveNewFileVersion(file);
  }

  public void SaveNewFileVersion(FileInfo file) => this.activator.SaveNewFileVersion(file);

  [PXButton(Tooltip = "Get the latest version of the file that is used by the provider for this mapping.", Category = "Data")]
  [PXUIField(DisplayName = "Get Latest Version", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected void getLatestFile() => this.activator.getLatestFile();

  [PXButton(Tooltip = "Delete error messages for all rows in the prepared data.")]
  [PXUIField(DisplayName = "Clear Errors", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected void clearErrors() => this.activator.ClearErrors();

  internal override PXSYTable QueryPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    CancellationToken token)
  {
    throw new NotImplementedException();
  }

  protected override void SYMappingActive_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    base.SYMappingActive_RowSelected(cache, e);
    this.activator.SYMapping_RowSelected(cache, e.Row as SYMapping);
    this.Save.SetEnabled(!PXLongOperation.Exists(this.UID));
  }

  protected virtual void SYMappingActive_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
  }

  protected virtual void SYMappingActive_SitemapTitle_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
  }

  public override void Persist()
  {
    this.MappingsSingle.Cache.SetStatus((object) this.MappingsSingle.Current, PXEntryStatus.Notchanged);
    base.Persist();
  }

  private void PerformOperation(string opType)
  {
    if (this.PreparedData.Cache.IsDirty && this.Operation.Ask("Unsaved Prepared Data", "Prepared data was not saved. Do you want to save it?", MessageButtons.YesNo) == WebDialogResult.Yes)
      this.Save.Press();
    SYExportProcess graph = PXGraph.CreateInstance<SYExportProcess>();
    SYMappingActive row = this.MappingsSingle.Current;
    SYImportOperation operation = this.Operation.Current;
    operation.BreakOnError = new bool?(true);
    operation.BreakOnTarget = new bool?(true);
    operation.Operation = opType;
    this.LongOperationManager.StartOperation(this.UID, (System.Action<CancellationToken>) (cancellationToken =>
    {
      SYProcess.ProcessMapping((SYProcess) graph, row, operation, cancellationToken);
      this.activator.EnableButtons(row.Status);
    }));
  }
}
