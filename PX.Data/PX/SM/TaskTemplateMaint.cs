// Decompiled with JetBrains decompiler
// Type: PX.SM.TaskTemplateMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.Description;
using PX.Data.Maintenance.SM;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class TaskTemplateMaint : 
  PXGraph<TaskTemplateMaint>,
  IEntityItemsService,
  IFormulaEditorInternalFields
{
  private bool isGiScreen;
  private PXSiteMapNode screenNode;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PX.SM.TaskTemplate.body)})]
  public PXSelect<PX.SM.TaskTemplate> TaskTemplates;
  public PXSelectReadonly<PX.SM.TaskTemplate> TaskTemplatesRO;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PX.SM.TaskTemplate.body)})]
  public PXSelect<PX.SM.TaskTemplate, Where<PX.SM.TaskTemplate.taskTemplateID, Equal<Current<PX.SM.TaskTemplate.taskTemplateID>>>> CurrentTaskTemplate;
  [PXHidden]
  public PXSelect<PX.SM.TaskTemplate> TaskTemplate;
  public PXSelect<TaskTemplateSetting, Where<TaskTemplateSetting.taskTemplateID, Equal<Current<PX.SM.TaskTemplate.taskTemplateID>>>> TaskTemplateSettings;
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> EntityItems;
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> PreviousEntityItems;
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> ScreenOwnerItems;
  public PXSave<PX.SM.TaskTemplate> Save;
  public PXCancel<PX.SM.TaskTemplate> Cancel;
  public PXInsert<PX.SM.TaskTemplate> Insert;
  public PXDelete<PX.SM.TaskTemplate> Delete;
  public PXFirst<PX.SM.TaskTemplate> First;
  public PXPrevious<PX.SM.TaskTemplate> Prev;
  public PXNext<PX.SM.TaskTemplate> Next;
  public PXLast<PX.SM.TaskTemplate> Last;

  [InjectDependency]
  private IWorkflowService _workflowService { get; set; }

  [InjectDependency]
  protected IWorkflowFieldsService _workflowFieldsService { get; set; }

  [InjectDependency]
  protected internal IScreenInfoProvider _screenInfoProvider { get; set; }

  /// <summary>Indicates if the screen selected in a current task template is a Generic Inquiry</summary>
  [PXInternalUseOnly]
  public bool CurrentScreenIsGI => this.isGiScreen;

  /// <summary>Identity of the screen selected in a current task template</summary>
  [PXInternalUseOnly]
  public string CurrentScreenID => this.TaskTemplates.Current?.ScreenID;

  /// <summary>Site map node of the screen selected in a current task template</summary>
  [PXInternalUseOnly]
  public PXSiteMapNode CurrentSiteMapNode => this.screenNode;

  public TaskTemplateMaint()
  {
    PXUIFieldAttribute.SetVisible<PX.SM.TaskTemplate.localeName>(this.TaskTemplates.Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
    PXSiteMapNodeSelectorAttribute.SetRestriction<PX.SM.TaskTemplate.screenID>(this.TaskTemplates.Cache, (object) null, (Func<SiteMap, bool>) (s => PXSiteMap.IsDashboard(s.Url)));
  }

  [PXInternalUseOnly]
  public virtual Dictionary<string, HashSet<string>> GetAllowedItems()
  {
    return (Dictionary<string, HashSet<string>>) null;
  }

  protected IEnumerable entityItems(string parent)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return SMNotificationMaint.GetEntityItemsImpl(parent, this.CurrentScreenID, (PXGraph) this, TaskTemplateMaint.\u003C\u003EO.\u003C0\u003E__EveryEntityItemsFilter ?? (TaskTemplateMaint.\u003C\u003EO.\u003C0\u003E__EveryEntityItemsFilter = new Func<string, string, CacheEntityItem, CacheEntityItem>(SMNotificationMaint.EveryEntityItemsFilter)), this._workflowService);
  }

  protected virtual void TaskTemplate_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row is PX.SM.TaskTemplate row)
    {
      this.isGiScreen = SMNotificationMaint.IsGIScreen(row.ScreenID, out this.screenNode);
      PXUIFieldAttribute.SetEnabled<PX.SM.TaskTemplate.taskTemplateID>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<PX.SM.TaskTemplate.refNoteID>(cache, (object) row, this.isGiScreen);
      PXUIFieldAttribute.SetVisible<PX.SM.TaskTemplate.refNoteID>(cache, (object) row, this.isGiScreen);
      PXUIFieldAttribute.SetVisible<PX.SM.TaskTemplate.attachActivity>(cache, (object) row, !this.isGiScreen);
      PXUIFieldAttribute.SetEnabled<PX.SM.TaskTemplate.attachActivity>(cache, (object) row, !this.isGiScreen);
      if (this.isGiScreen || this.screenNode == null)
        return;
      string entryScreenId = PXList.Provider.GetEntryScreenID(this.screenNode.ScreenID);
      if (entryScreenId == null)
        return;
      this.screenNode = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(entryScreenId);
    }
    else
    {
      this.isGiScreen = false;
      this.screenNode = (PXSiteMapNode) null;
    }
  }

  protected virtual void TaskTemplateSetting_FromSchema_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TaskTemplateSetting row))
      return;
    row.Value = (string) null;
    this.UpdateValueFieldState(cache, row);
  }

  public PXSelectBase<CacheEntityItem> EntityItemsSelect
  {
    get => (PXSelectBase<CacheEntityItem>) this.EntityItems;
  }

  /// <summary>Provides the Value field with the state of appropriate Task's field or with the list of fields from the selected screen.</summary>
  public void UpdateValueFieldState(PXCache cache, TaskTemplateSetting row)
  {
    string currentScreenId = this.CurrentScreenID;
    if (currentScreenId == null)
      return;
    string[] values = (string[]) null;
    string[] labels = (string[]) null;
    foreach (PXFieldValuesListAttribute valuesListAttribute in cache.GetAttributesOfType<PXFieldValuesListAttribute>((object) row, "value"))
    {
      bool? fromSchema = row.FromSchema;
      bool flag = true;
      if (fromSchema.GetValueOrDefault() == flag & fromSchema.HasValue)
      {
        valuesListAttribute.IsActive = true;
      }
      else
      {
        valuesListAttribute.IsActive = false;
        if (values == null && !SMNotificationMaint.GetScreenFields(this._screenInfoProvider, currentScreenId, out values, out labels))
          break;
        valuesListAttribute.SetList(cache, values, labels);
      }
    }
  }

  public string[] GetInternalFields()
  {
    string currentScreenId = this.CurrentScreenID;
    if (string.IsNullOrEmpty(currentScreenId))
      return Array.Empty<string>();
    PXSiteMap.ScreenInfo info = ScreenUtils.ScreenInfo.TryGet(currentScreenId);
    if (info == null)
      return Array.Empty<string>();
    IEnumerable<string> second = this._workflowFieldsService.GetFormFields(currentScreenId).SelectMany<KeyValuePair<string, AUWorkflowFormField[]>, string>((Func<KeyValuePair<string, AUWorkflowFormField[]>, IEnumerable<string>>) (form => ((IEnumerable<AUWorkflowFormField>) form.Value).Select<AUWorkflowFormField, string>((Func<AUWorkflowFormField, string>) (field => GetFieldPath(form.Key, field.FieldName)))));
    return info.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (c => c.Key != "FilterPreview")).Select(c => new
    {
      container = c,
      viewName = ScreenUtils.NormalizeViewName(c.Key)
    }).SelectMany(t => (IEnumerable<PX.Data.Description.FieldInfo>) info.Containers[t.container.Key].Fields, (t, field) => GetFieldPath(t.viewName, field.FieldName)).Union<string>(second).Distinct<string>().ToArray<string>();

    static string GetFieldPath(string viewName, string fieldName) => $"[{viewName}.{fieldName}]";
  }
}
