// Decompiled with JetBrains decompiler
// Type: PX.Api.SYImportMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Data.Maintenance.SM;
using PX.Data.Maintenance.SM.SiteMapHelpers;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api;

public class SYImportMaint : 
  SYMappingMaint<SYMapping.mappingType.typeImport>,
  IGraphWithInitialization,
  IEntityItemsService
{
  public const string ImportByScenarioScreenUrl = "~/Pages/SM/SM206036.aspx";
  public const string ImportByScenarioScreenId = "SM206036";
  private const string _saveAction = "Save";
  private const string SetRestrictionSlotName = "SYImportMaint_RestrictedScreenIDs";
  public PXAction<SYMapping> ConvertSimpleMappingToManual;

  protected override string ScreenId => "SM206025";

  public PXSelectBase<CacheEntityItem> EntityItemsSelect => (PXSelectBase<CacheEntityItem>) null;

  [PXInternalUseOnly]
  public virtual Dictionary<string, HashSet<string>> GetAllowedItems()
  {
    return (Dictionary<string, HashSet<string>>) null;
  }

  private ConcurrentDictionary<PX.SM.SiteMap, bool> _restrictedScreenIDs
  {
    get
    {
      ConcurrentDictionary<PX.SM.SiteMap, bool> slot = PXContext.GetSlot<ConcurrentDictionary<PX.SM.SiteMap, bool>>("SYImportMaint_RestrictedScreenIDs");
      if (slot != null)
        return slot;
      return PXContext.SetSlot<ConcurrentDictionary<PX.SM.SiteMap, bool>>("SYImportMaint_RestrictedScreenIDs", PXDatabase.Provider.GetSlot<ConcurrentDictionary<PX.SM.SiteMap, bool>>("SYImportMaint_RestrictedScreenIDs", (PrefetchDelegate<ConcurrentDictionary<PX.SM.SiteMap, bool>>) (() => new ConcurrentDictionary<PX.SM.SiteMap, bool>()), typeof (PX.SM.SiteMap)));
    }
  }

  public SYImportMaint()
  {
    PXSiteMapNodeSelectorAttribute.SetRestriction<SYMapping.screenID>(this.Mappings.Cache, (object) null, (Func<PX.SM.SiteMap, bool>) (siteMap =>
    {
      bool flag;
      if (this._restrictedScreenIDs.TryGetValue(siteMap, out flag))
        return flag;
      if (PXSiteMap.IsPivot(siteMap.Url) || PXSiteMap.IsDashboard(siteMap.Url) || PXSiteMap.IsGenericInquiry(siteMap.Url) || SiteMapRestrictionsTypes.IsReport(siteMap) || SiteMapRestrictionsTypes.IsWiki(siteMap) || string.IsNullOrEmpty(siteMap.Graphtype))
      {
        this._restrictedScreenIDs.TryAdd(siteMap, true);
        return true;
      }
      this._restrictedScreenIDs.TryAdd(siteMap, false);
      return false;
    }));
  }

  public void Initialize()
  {
    this.ScreenToSiteMapAddHelper = (PXFieldScreenToSiteMapAddHelper<SYMapping>) new PXScenarioScreenToSiteMapAddHelper((PXGraph) this, this.ScreenInfoCacheControl, "IB", "~/Pages/SM/SM206036.aspx");
  }

  [PXButton]
  [PXUIField(DisplayName = "Convert To Manual Scenario")]
  protected IEnumerable convertSimpleMappingToManual(PXAdapter adapter)
  {
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => SYImportProcessSingle.PerformConvertionFromSimpleToManualMapping(this.Mappings.Current)));
    return adapter.Get();
  }

  protected virtual void _(Events.RowUpdated<SYMapping> e)
  {
    SYMapping row = e.Row;
    if (row == null || !row.ProcessInParallel.GetValueOrDefault())
      return;
    bool? nullable = row.BreakOnError;
    bool flag1 = true;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
    {
      nullable = row.BreakOnTarget;
      bool flag2 = true;
      if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
        return;
    }
    e.Cache.SetValueExt<SYMapping.breakOnError>((object) row, (object) false);
    e.Cache.SetValueExt<SYMapping.breakOnTarget>((object) row, (object) false);
  }

  protected virtual void _(Events.RowSelected<SYMapping> e)
  {
    bool valueOrDefault = ((bool?) e.Row?.ProcessInParallel).GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<SYMapping.breakOnError>(e.Cache, (object) e.Row, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<SYMapping.breakOnTarget>(e.Cache, (object) e.Row, !valueOrDefault);
    SYMapping row = e.Row;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      bool? isSimpleMapping = row.IsSimpleMapping;
      bool flag = true;
      num = !(isSimpleMapping.GetValueOrDefault() == flag & isSimpleMapping.HasValue) ? 1 : 0;
    }
    bool isEnabled = num != 0;
    this.FieldMappings.AllowUpdate = isEnabled;
    this.FieldMappings.AllowInsert = isEnabled;
    this.FieldMappings.AllowDelete = isEnabled;
    this.RowDown.SetEnabled(isEnabled);
    this.RowUp.SetEnabled(isEnabled);
    this.RowInsert.SetEnabled(isEnabled);
    this.InsertFrom.SetEnabled(isEnabled);
    this.ConvertSimpleMappingToManual.SetVisible(!isEnabled);
    if (isEnabled)
      return;
    PXUIFieldAttribute.SetWarning<SYMapping.name>(e.Cache, (object) e.Row, "To modify the mapping on this form, you must convert the import scenario to a manual scenario by clicking Convert to Manual Scenario on the form toolbar.");
  }

  protected void _(Events.RowSelected<SYMappingField> e)
  {
    if (e.Row?.FieldName == null)
      return;
    SyCommand command = SyImportContext.ParseCommand(e.Row);
    bool flag1 = command.CommandType == SyCommandType.Action;
    int num1;
    if (flag1)
    {
      bool? isVisible = e.Row.IsVisible;
      bool flag2 = true;
      num1 = isVisible.GetValueOrDefault() == flag2 & isVisible.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool isEnabled1 = num1 != 0;
    PXUIFieldAttribute.SetEnabled<SYMappingField.executeActionBehavior>(e.Cache, (object) e.Row, isEnabled1);
    PXUIFieldAttribute.SetEnabled<SYMappingField.needCommit>(e.Cache, (object) e.Row, !flag1 || command.Field != "Save");
    if (flag1)
    {
      SYMappingField row = e.Row;
      if (row.ExecuteActionBehavior == null)
      {
        string str;
        row.ExecuteActionBehavior = str = "E";
      }
    }
    int num2 = this.IsRowKey(e.Row) ? 1 : 0;
    if (num2 != 0)
      e.Row.NeedSearch = new bool?(true);
    int num3;
    if (num2 == 0 && !e.Row.ParentLineNbr.HasValue)
    {
      bool? isVisible = e.Row.IsVisible;
      bool flag3 = false;
      num3 = !(isVisible.GetValueOrDefault() == flag3 & isVisible.HasValue) ? 1 : 0;
    }
    else
      num3 = 0;
    bool isEnabled2 = num3 != 0;
    PXUIFieldAttribute.SetEnabled<SYMappingField.needSearch>(e.Cache, (object) e.Row, isEnabled2);
  }

  protected override void SYMappingField_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    SYMappingField row = e.Row as SYMappingField;
    row.NeedSearch = new bool?(this.IsRowKey(row));
    base.SYMappingField_RowInserted(sender, e);
    this.SetExecuteActionBehavior(row, sender);
  }

  protected override void SYMappingField_FieldName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    base.SYMappingField_FieldName_FieldUpdated(sender, e);
    this.SetExecuteActionBehavior(e.Row as SYMappingField, sender);
  }

  protected virtual void _(
    Events.FieldUpdated<SYMappingField, SYMappingField.executeActionBehavior> e)
  {
    SyCommand command = SyImportContext.ParseCommand(e.Row);
    if (!(command.CommandType == SyCommandType.Action & command.Field == "Save"))
      return;
    SYMappingField row = e.Row;
    ExecutionBehavior? executionBehavior1 = command.ExecutionBehavior;
    ExecutionBehavior executionBehavior2 = ExecutionBehavior.ForEachRecord;
    bool? nullable = new bool?(executionBehavior1.GetValueOrDefault() == executionBehavior2 & executionBehavior1.HasValue);
    row.NeedCommit = nullable;
  }

  protected virtual void SetExecuteActionBehavior(SYMappingField field, PXCache cache)
  {
    if (field == null)
      return;
    if (field.FieldName == null)
    {
      field.ExecuteActionBehavior = (string) null;
    }
    else
    {
      SyCommand command = SyImportContext.ParseCommand(field);
      if (command.CommandType == SyCommandType.Action)
      {
        cache.SetValueExt<SYMappingField.executeActionBehavior>((object) field, command.Field == "Save" ? (object) "L" : (object) "E");
      }
      else
      {
        if (field.ExecuteActionBehavior == null)
          return;
        field.ExecuteActionBehavior = (string) null;
      }
    }
  }

  protected virtual void _(
    Events.FieldSelecting<SYMappingField.fullFieldNameHidden> e)
  {
    if (!(e.Row is SYMappingField row) || string.IsNullOrEmpty(row.ObjectName) && string.IsNullOrEmpty(row.FieldName))
      return;
    string str = ScreenUtils.NormalizeViewName(row.ObjectName);
    e.ReturnValue = (object) $"{str}.{row.FieldName}";
  }

  protected virtual void _(Events.FieldUpdated<SYMappingField.needSearch> e)
  {
    if (!(e.Row is SYMappingField row) || !(e.NewValue is bool newValue))
      return;
    if (newValue)
    {
      bool? needCommit = row.NeedCommit;
      bool flag = true;
      if (!(needCommit.GetValueOrDefault() == flag & needCommit.HasValue))
        e.Cache.SetValueExt<SYMappingField.needCommit>((object) row, (object) true);
      else
        this.RecalcHidden(row);
    }
    else
      this.RemoveSearchSystemCommand(row);
  }

  private void RemoveSearchSystemCommand(SYMappingField row)
  {
    IEnumerable<SYMappingField> firstTableItems = PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>>.Config>.Select((PXGraph) this, (object) row.LineNbr).FirstTableItems;
    MappingFieldNamer.CreateParameterValue(row.FieldName);
    SYMappingField syMappingField = firstTableItems.FirstOrDefault<SYMappingField>((Func<SYMappingField, bool>) (c => SyImportContext.ParseCommand(c).CommandType == SyCommandType.Search));
    if (syMappingField == null)
      return;
    this.FieldMappings.Delete(syMappingField);
  }
}
