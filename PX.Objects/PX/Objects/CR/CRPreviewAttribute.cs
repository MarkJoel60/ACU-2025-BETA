// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRPreviewAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CR;

public class CRPreviewAttribute : PXPreviewAttribute
{
  private readonly PXSelectDelegate _select;
  private CRPreviewAttribute.GeneratePreview _handler;
  private object _current;

  public CRPreviewAttribute(System.Type primaryViewType, System.Type previewType)
    : base(primaryViewType, previewType)
  {
    // ISSUE: method pointer
    this._select = new PXSelectDelegate((object) this, __methodptr(\u003C\u002Ector\u003Eb__4_0));
  }

  protected virtual PXSelectDelegate SelectHandler => this._select;

  protected virtual IEnumerable GetPreview()
  {
    CRPreviewAttribute previewAttribute = this;
    if (previewAttribute._handler != null)
    {
      object current = previewAttribute.Graph.Caches[previewAttribute.CacheType].Current;
      yield return previewAttribute._handler(current);
    }
  }

  protected virtual void PerformRefresh()
  {
    foreach (object obj in base.GetPreview())
      this._current = obj;
  }

  public virtual void Attach(
    PXGraph graph,
    string viewName,
    CRPreviewAttribute.GeneratePreview getPreviewHandler)
  {
    if (this._handler != null)
      throw new InvalidOperationException("Attributes are already attached.");
    this._handler = getPreviewHandler ?? (CRPreviewAttribute.GeneratePreview) (o => o);
    ((PXViewExtensionAttribute) this).ViewCreated(graph, viewName);
  }

  public delegate object GeneratePreview(object row);
}
