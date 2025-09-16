// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFormulaEditorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

[PXUIField(DisplayName = "Formula")]
[PXString(IsUnicode = true)]
[PXDBString(IsUnicode = true)]
public class PXFormulaEditorAttribute : PXEntityAttribute, IPXFieldSelectingSubscriber
{
  private string _viewName;

  public override void CacheAttached(PXCache cache)
  {
    base.CacheAttached(cache);
    this.CreateView(cache);
    PXFormulaEditorAttribute.MakeFormulaCacheVirtual(cache);
  }

  private void CreateView(PXCache cache)
  {
    this._viewName = $"_FormulaEditorView#{cache.GetItemType().FullName}_{this._FieldName}";
    if (cache.Graph.Views.ContainsKey(this._viewName))
      return;
    PXSelectReadonly<FormulaOption> pxSelectReadonly = new PXSelectReadonly<FormulaOption>(cache.Graph, (Delegate) new PXSelectDelegate(this.SelectFormulaOptions));
    cache.Graph.Views[this._viewName] = pxSelectReadonly.View;
  }

  private static void MakeFormulaCacheVirtual(PXCache cache)
  {
    cache.Graph.Caches<FormulaOption>().DisableReadItem = true;
  }

  private IEnumerable SelectFormulaOptions()
  {
    PXGraph currentGraph = PXView.CurrentGraph;
    HashSet<FormulaOption> options = new HashSet<FormulaOption>(currentGraph.Caches<FormulaOption>().GetComparer<FormulaOption>());
    foreach (PXFormulaEditor.OptionsProviderAttribute providerAttribute in currentGraph.Caches[this._BqlTable].GetAttributesOfType<PXFormulaEditor.OptionsProviderAttribute>((object) null, this._FieldName))
      providerAttribute.ChangeOptionsSet(currentGraph, (ISet<FormulaOption>) options);
    return (IEnumerable) options;
  }

  protected override bool ChildrenAttributesComeFirstFor<ISubscriber>()
  {
    return base.ChildrenAttributesComeFirstFor<ISubscriber>() || typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber);
  }

  void IPXFieldSelectingSubscriber.FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    this.FieldSelecting(cache, e);
  }

  protected virtual void FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFormulaEditorState.CreateInstance(e.ReturnState, this._viewName);
  }
}
