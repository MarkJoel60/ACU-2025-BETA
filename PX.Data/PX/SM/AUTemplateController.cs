// Decompiled with JetBrains decompiler
// Type: PX.SM.AUTemplateController
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

/// <exclude />
public class AUTemplateController : PXGraph<AUTemplateController, AUTemplate>
{
  public PXSelect<AUTemplate> Filter;
  public PXSelect<AUTemplateData, Where<Current<AUTemplate.templateID>, Equal<AUTemplateData.templateId>>> Items;
  public PXAction<AUTemplate> actionSaveToClipboard;
  private PXGraph _graph;

  public AUTemplateController()
  {
    this.Filter.Cache.AllowInsert = false;
    this.Items.Cache.AllowInsert = false;
    this.CopyPaste.SetVisible(false);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Save To Clipboard")]
  protected void ActionSaveToClipboard()
  {
    PXCopyPasteData<PXGraph> currentUserClipboard = PXCopyPasteData<PXGraph>.CurrentUserClipboard;
    PXCopyPasteData<PXGraph>.SaveClipboard(currentUserClipboard);
    currentUserClipboard.ScreenId = this.Filter.Current.ScreenID;
    currentUserClipboard.ImportValues((IEnumerable<KeyValuePair<string, string>>) ((IEnumerable<AUTemplateData>) PXSelectBase<AUTemplateData, PXSelect<AUTemplateData, Where<Current<AUTemplate.templateID>, Equal<AUTemplateData.templateId>>>.Config>.Select((PXGraph) this).FirstTableItems.ToArray<AUTemplateData>()).Where<AUTemplateData>((Func<AUTemplateData, bool>) (d =>
    {
      bool? active = d.Active;
      bool flag = true;
      return active.GetValueOrDefault() == flag & active.HasValue;
    })).Select<AUTemplateData, KeyValuePair<string, string>>((Func<AUTemplateData, KeyValuePair<string, string>>) (d => new KeyValuePair<string, string>(d.FieldId, d.Value))).ToList<KeyValuePair<string, string>>());
  }

  private PXGraph EnsureGraph()
  {
    if (this._graph == null)
    {
      string graph = this.Filter.Current.Graph;
      if (!Str.IsNullOrEmpty(graph))
        this._graph = PXGraph.CreateInstance(PXBuildManager.GetType(graph, false));
    }
    return this._graph;
  }

  protected void _(
    Events.FieldSelecting<AUTemplateData, AUTemplateData.rowType> e)
  {
    if (e.Row == null)
      return;
    string viewName = PXCopyPasteData<PXGraph>.GetViewName(e.Row.View);
    if (Str.IsNullOrEmpty(viewName))
      return;
    PXGraph pxGraph = this.EnsureGraph();
    if (pxGraph == null)
      return;
    try
    {
      e.ReturnValue = (object) pxGraph.Views[viewName].GetItemType().FullName;
    }
    catch (PXViewDoesNotExistException ex)
    {
      PXUIFieldAttribute.SetError<AUTemplateData.rowType>(e.Cache, (object) e.Row, ex.Message);
    }
  }

  public override void Persist()
  {
    base.Persist();
    PXPageCacheUtils.InvalidateCachedPages();
  }
}
