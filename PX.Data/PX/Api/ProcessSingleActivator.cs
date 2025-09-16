// Decompiled with JetBrains decompiler
// Type: PX.Api.ProcessSingleActivator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;
using System.Web;

#nullable disable
namespace PX.Api;

internal class ProcessSingleActivator
{
  public PXAction<SYMappingActive> PrepareProcess;
  public PXAction<SYMappingActive> Prepare;
  public PXAction<SYMappingActive> Process;
  public PXAction<SYMappingActive> Rollback;
  public PXAction<SYMappingActive> ShowUploadPanel;
  public PXAction<SYMappingActive> GetLatestFile;
  private SYProcess graph;
  private PXView mappingsSingle;
  private PXView preparedData;
  private string[] knownTargets;
  private string[] knownParams;
  protected IPXSYProvider provider;
  protected Guid? providerID;

  public ProcessSingleActivator(
    SYProcess graph,
    PXAction<SYMappingActive> prepareProcess,
    PXAction<SYMappingActive> prepare,
    PXAction<SYMappingActive> rollback,
    PXAction<SYMappingActive> process,
    PXAction<SYMappingActive> showUploadPanel,
    PXAction<SYMappingActive> getLatestFile)
  {
    this.PrepareProcess = prepareProcess;
    this.Prepare = prepare;
    this.Rollback = rollback;
    this.Process = process;
    this.ShowUploadPanel = showUploadPanel;
    this.GetLatestFile = getLatestFile;
    this.graph = graph;
    this.mappingsSingle = graph.Views["MappingsSingle"];
    this.preparedData = graph.Views["PreparedData"];
    this.knownTargets = new string[6]
    {
      "ds",
      "tab",
      "DetailsForm",
      "PreparedData",
      "FormReplacementProperties",
      "GridSubstitutionInfo"
    };
    this.knownParams = new string[19]
    {
      "Cancel",
      "Save",
      "Refresh",
      "FetchRow",
      "RepaintTab",
      "LayoutSave",
      "ExportExcel",
      nameof (prepare),
      "import",
      nameof (rollback),
      "clearErrors",
      "switchActivation",
      "switchProcessing",
      "viewReplacement",
      "replaceOneValue",
      "replaceAllValues",
      "addSubstitutions",
      "saveSubstitutions",
      "closeSubstitutions"
    };
  }

  public void EnableFields()
  {
    PXUIFieldAttribute.SetEnabled<SYMappingActive.name>(this.mappingsSingle.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SYMappingActive.screenID>(this.mappingsSingle.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<SYMapping.providerID>(this.mappingsSingle.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<SYMapping.syncType>(this.mappingsSingle.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<SYMapping.status>(this.mappingsSingle.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<SYMapping.nbrRecords>(this.mappingsSingle.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<SYMapping.preparedOn>(this.mappingsSingle.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<SYMapping.completedOn>(this.mappingsSingle.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<SYMapping.discardResult>(this.mappingsSingle.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SYMapping.processInParallel>(this.mappingsSingle.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SYMapping.breakOnError>(this.mappingsSingle.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SYMapping.breakOnTarget>(this.mappingsSingle.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SYMapping.skipHeaders>(this.mappingsSingle.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SYMappingActive.batchSize>(this.mappingsSingle.Cache, (object) null, true);
  }

  public void EnableButtons(string status)
  {
    bool isEnabled1 = true;
    bool isEnabled2 = status != "N";
    this.PrepareProcess.SetEnabled(isEnabled1);
    this.Prepare.SetEnabled(isEnabled1);
    this.Rollback.SetEnabled(isEnabled2);
    this.Process.SetEnabled(isEnabled2);
  }

  public bool GenerateDynamicColumns
  {
    get
    {
      if (PXLongOperation.Exists(this.graph.UID))
        return true;
      if (HttpContext.Current == null || HttpContext.Current.Request == null)
        return false;
      if (PXSiteMap.Provider.FindSiteMapNodeByGraphTypeUnsecure(CustomizedTypeManager.GetTypeNotCustomized((PXGraph) this.graph).FullName)?.SelectedUI == "T" || PXSiteMap.Provider.FindSiteMapNodeByScreenID(PXContext.GetScreenID())?.SelectedUI == "T")
        return true;
      string str1 = HttpContext.Current.Request.Form["__CALLBACKID"];
      string str2 = HttpContext.Current.Request.Form["__CALLBACKPARAM"];
      return !string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str1) && str1.OrdinalEndsWith(this.knownTargets) && str2.OrdinalStartsWith(this.knownParams);
    }
  }

  public void SYMapping_RowSelected(PXCache cache, SYMapping row)
  {
    if (row == null)
      return;
    SYImportOperation current = (SYImportOperation) this.graph.Operation.Cache.Current;
    Guid? mappingId1 = current.MappingID;
    Guid? mappingId2 = row.MappingID;
    if ((mappingId1.HasValue == mappingId2.HasValue ? (mappingId1.HasValue ? (mappingId1.GetValueOrDefault() != mappingId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      current.MappingID = row.MappingID;
      this.graph.ClearGenerateDynamicColumns();
      this.graph.GenerateDynamicColumns(this.graph.GetDisplayNames());
    }
    this.EnableButtons(row.Status);
    try
    {
      this.GetProvider();
    }
    catch (PXArgumentException ex)
    {
      if (ex.Message.EndsWith("providerID"))
        cache.RaiseExceptionHandling<SYMapping.providerID>((object) row, (object) row.ProviderID, (Exception) new PXSetPropertyException("The provider does not exist in the system."));
      else
        throw;
    }
    catch (PXException ex)
    {
      cache.RaiseExceptionHandling<SYMapping.providerID>((object) row, (object) row.ProviderID, (Exception) new PXSetPropertyException(ex.Message));
    }
    bool isEnabled = !string.IsNullOrEmpty(this.GetAttachedFileName(this.provider));
    this.ShowUploadPanel.SetEnabled(isEnabled);
    this.GetLatestFile.SetEnabled(isEnabled);
    PXCache cache1 = cache;
    bool? processInParallel;
    int num1;
    if (current == null)
    {
      num1 = 1;
    }
    else
    {
      processInParallel = current.ProcessInParallel;
      bool flag = true;
      num1 = !(processInParallel.GetValueOrDefault() == flag & processInParallel.HasValue) ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<SYMapping.breakOnError>(cache1, (object) null, num1 != 0);
    PXCache cache2 = cache;
    int num2;
    if (current == null)
    {
      num2 = 1;
    }
    else
    {
      processInParallel = current.ProcessInParallel;
      bool flag = true;
      num2 = !(processInParallel.GetValueOrDefault() == flag & processInParallel.HasValue) ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<SYMapping.breakOnTarget>(cache2, (object) null, num2 != 0);
  }

  public void SwitchActivation()
  {
    bool flag = this.preparedData.Cache.Current != null && ((SYData) this.preparedData.Cache.Current).IsActive.GetValueOrDefault();
    foreach (SYData syData in this.preparedData.SelectMulti())
    {
      syData.IsActive = new bool?(!flag);
      this.preparedData.Cache.Update((object) syData);
    }
  }

  public void SwitchActivationUntilError()
  {
    foreach (SYData syData in this.preparedData.SelectMulti())
    {
      bool? isActive = syData.IsActive;
      bool flag = true;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      {
        if (syData.ErrorMessage != null)
          break;
        syData.IsActive = new bool?(false);
        this.preparedData.Cache.Update((object) syData);
      }
    }
  }

  public void SwitchProcessing()
  {
    bool flag = this.preparedData.Cache.Current != null && ((SYData) this.preparedData.Cache.Current).IsProcessed.GetValueOrDefault();
    foreach (SYData syData in this.preparedData.SelectMulti())
    {
      syData.IsProcessed = new bool?(!flag);
      this.preparedData.Cache.Update((object) syData);
    }
  }

  public void PerformRollback()
  {
    SYMappingActive current = (SYMappingActive) this.mappingsSingle.Cache.Current;
    System.DateTime? statusDate = (System.DateTime?) this.graph.History.Current?.StatusDate;
    bool flag = !statusDate.HasValue || SyMappingUtils.IsLastHistoryRecord(this.graph, current.MappingID, statusDate.Value);
    string message = flag ? "On the Prepared Data and History tabs, all records will be deleted. You will not be able to undo these changes. Would you like to proceed with deleting the records?" : "On the Prepared Data tab, all records will be deleted. On the History tab, all records created later than the currently selected one will be deleted. You will not be able to undo these changes. Would you like to proceed with deleting the records?";
    if (this.mappingsSingle.Ask((object) current, "Warning", message, MessageButtons.YesNo, MessageIcon.Warning) != WebDialogResult.Yes)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    SyMappingUtils.RollbackData(this.graph, current, flag ? (SyMappingUtils.RollbackHistoryHandler) ((_1, _2) => new System.DateTime?()) : ProcessSingleActivator.\u003C\u003EO.\u003C0\u003E__RollbackHistory ?? (ProcessSingleActivator.\u003C\u003EO.\u003C0\u003E__RollbackHistory = new SyMappingUtils.RollbackHistoryHandler(ProcessSingleActivator.RollbackHistory)));
    this.EnableButtons(current.Status);
  }

  public void SaveNewFileVersion(FileInfo file)
  {
    IPXSYProvider provider = this.GetProvider();
    if (provider == null)
      return;
    string attachedFileName = this.GetAttachedFileName(provider);
    if (string.IsNullOrEmpty(attachedFileName))
      return;
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    FileInfo file1 = instance.GetFile(attachedFileName);
    if (file1 == null)
      throw new PXException("The file '{0}' for the provider '{1}' has not been found.", new object[2]
      {
        (object) attachedFileName,
        (object) provider.ProviderName
      });
    file1.BinData = file.BinData;
    file1.Comment = file.Comment;
    file1.OriginalName = file.OriginalName;
    instance.SaveFile(file1, FileExistsAction.CreateVersion);
  }

  public void getLatestFile()
  {
    IPXSYProvider provider = this.GetProvider();
    if (provider == null)
      return;
    string attachedFileName = this.GetAttachedFileName(provider);
    if (string.IsNullOrEmpty(attachedFileName))
      return;
    FileInfo file = PXGraph.CreateInstance<UploadFileMaintenance>().GetFile(attachedFileName);
    if (file == null)
      throw new PXException("The file '{0}' for the provider '{1}' has not been found.", new object[2]
      {
        (object) attachedFileName,
        (object) provider.ProviderName
      });
    throw new PXRedirectToFileException(file.UID, true);
  }

  public void ClearErrors()
  {
    foreach (SYData syData in this.preparedData.SelectMulti())
    {
      if (!Str.IsNullOrEmpty(syData.FieldErrors) || !Str.IsNullOrEmpty(syData.ErrorMessage))
      {
        syData.FieldExceptions = (string) null;
        syData.FieldErrors = (string) null;
        syData.ErrorMessage = (string) null;
        this.preparedData.Cache.Update((object) syData);
      }
    }
  }

  private IPXSYProvider GetProvider()
  {
    if (!(this.mappingsSingle.Cache.Current is SYMapping current) || !current.ProviderID.HasValue)
    {
      this.provider = (IPXSYProvider) null;
      this.providerID = new Guid?();
    }
    else if (this.provider == null || !this.providerID.HasValue || this.providerID.Value != current.ProviderID.Value)
    {
      this.provider = SYProviderMaint.GetProvider(current.ProviderID.Value);
      this.providerID = current.ProviderID;
    }
    return this.provider;
  }

  private string GetAttachedFileName(IPXSYProvider prov)
  {
    if (prov == null)
      return (string) null;
    foreach (PXSYParameter parameter in prov.GetParameters())
    {
      if (parameter.Name == "FileName")
        return parameter.Value;
    }
    return (string) null;
  }

  private static System.DateTime? RollbackHistory(SYProcess graph, SYMapping mapping)
  {
    System.DateTime? statusDate = (System.DateTime?) graph.History.Current?.StatusDate;
    System.DateTime dateLimit1 = statusDate ?? new System.DateTime(1753, 1, 2);
    SYHistory lastHistoryRecord1 = SyMappingUtils.GetLastHistoryRecord(graph, mapping.MappingID, dateLimit1);
    if (lastHistoryRecord1 != null)
    {
      mapping.Status = lastHistoryRecord1.Status;
      mapping.NbrRecords = lastHistoryRecord1.NbrRecords;
      mapping.ExportTimeStamp = lastHistoryRecord1.ExportTimeStamp;
      mapping.ExportTimeStampUtc = lastHistoryRecord1.ExportTimeStampUtc;
      if (lastHistoryRecord1.Status == "I")
      {
        mapping.ImportTimeStamp = lastHistoryRecord1.ImportTimeStamp;
        mapping.CompletedOn = lastHistoryRecord1.StatusDate;
      }
      else if (lastHistoryRecord1.Status == "P")
      {
        mapping.PreparedOn = lastHistoryRecord1.StatusDate;
        return new System.DateTime?(dateLimit1);
      }
      SYProcess graph1 = graph;
      Guid? mappingId = mapping.MappingID;
      statusDate = lastHistoryRecord1.StatusDate;
      System.DateTime dateLimit2 = statusDate.Value;
      SYHistory lastHistoryRecord2 = SyMappingUtils.GetLastHistoryRecord(graph1, mappingId, dateLimit2, "P");
      if (lastHistoryRecord2 != null)
        mapping.PreparedOn = lastHistoryRecord2.StatusDate;
    }
    return new System.DateTime?(dateLimit1);
  }
}
