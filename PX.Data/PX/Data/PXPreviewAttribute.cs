// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPreviewAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class PXPreviewAttribute : PXViewExtensionAttribute
{
  private const string _ACTION_POSTFIX = "$RefreshPreview";
  private const string _VIEW_POSTFIX = "$Preview";
  private readonly System.Type _primaryViewType;
  private System.Type _previewType;
  private PXGraph _graph;
  private string _viewName;
  private System.Type _cacheType;
  private PXSelectDelegate _dataHandler;
  private BqlCommand _bqlSelect;

  public PXPreviewAttribute(System.Type primaryViewType)
    : this(primaryViewType, (System.Type) null)
  {
  }

  public PXPreviewAttribute(System.Type primaryViewType, System.Type previewType)
  {
    if (primaryViewType == (System.Type) null)
      throw new ArgumentNullException(nameof (primaryViewType));
    if (previewType != (System.Type) null && !typeof (IBqlTable).IsAssignableFrom(previewType))
      throw new ArgumentException($"'{previewType}' must impement PX.Data.IBqlTable interface.", nameof (previewType));
    this._primaryViewType = primaryViewType;
    this._previewType = previewType;
  }

  public override void ViewCreated(PXGraph graph, string viewName)
  {
    this._graph = graph;
    this._viewName = viewName;
    this._cacheType = this.Graph.Views[this.ViewName].GetItemType();
    if (this.PreviewType == (System.Type) null)
      this._previewType = this.CacheType;
    this.AddView();
    this.AddAction();
  }

  private void AddAction()
  {
    PXNamedAction.AddAction(this.Graph, this._primaryViewType, this.ViewName + "$RefreshPreview", (string) null, new PXButtonDelegate(this.RefreshPreview)).SetVisible(false);
  }

  private void AddView()
  {
    this.Graph.Views.Add(this.ViewName + "$Preview", new PXView(this.Graph, false, this.BqlSelect, (Delegate) this.SelectHandler));
  }

  protected virtual BqlCommand BqlSelect
  {
    get
    {
      if (this._bqlSelect == null)
        this._bqlSelect = (BqlCommand) Activator.CreateInstance(BqlCommand.Compose(typeof (Select<>), this.PreviewType));
      return this._bqlSelect;
    }
  }

  protected virtual PXSelectDelegate SelectHandler
  {
    get
    {
      return (PXSelectDelegate) (() => (IEnumerable) new object[1]
      {
        this.Graph.Caches[this.PreviewType].Current
      });
    }
  }

  protected System.Type PreviewType => this._previewType;

  protected PXGraph Graph => this._graph;

  protected string ViewName => this._viewName;

  protected System.Type CacheType => this._cacheType;

  protected virtual IEnumerable GetPreview()
  {
    if (this._dataHandler == null)
    {
      PXSelectDelegate pxSelectDelegate = (PXSelectDelegate) (() =>
      {
        PXCache cach = this.Graph.Caches[this.PreviewType];
        return (IEnumerable) new object[1]
        {
          cach.Current ?? cach.CreateInstance()
        };
      });
      string name = this.PreviewType?.ToString() + "_GetPreview";
      MethodInfo customHandler = this.Graph.GetType().GetMethod(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new System.Type[1]
      {
        this.CacheType
      }, (ParameterModifier[]) null);
      if (customHandler != (MethodInfo) null && typeof (IEnumerable).IsAssignableFrom(customHandler.ReturnType))
        pxSelectDelegate = (PXSelectDelegate) (() =>
        {
          object current = this.Graph.Caches[this.CacheType].Current;
          return (IEnumerable) customHandler.Invoke((object) this.Graph, new object[1]
          {
            current
          });
        });
      this._dataHandler = pxSelectDelegate;
    }
    return (IEnumerable) this._dataHandler().Cast<object>().ToList<object>();
  }

  [PXButton]
  [PXUIField(Visible = false, MapEnableRights = PXCacheRights.Select)]
  private IEnumerable RefreshPreview(PXAdapter adapter)
  {
    this.PerformRefresh();
    return adapter.Get();
  }

  protected virtual void PerformRefresh()
  {
    PXCache cach = this.Graph.Caches[this.PreviewType];
    foreach (object obj in this.GetPreview())
      cach.Current = obj;
  }
}
