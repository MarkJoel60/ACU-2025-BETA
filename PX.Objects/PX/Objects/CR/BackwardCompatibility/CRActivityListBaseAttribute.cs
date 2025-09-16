// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRActivityListBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public abstract class CRActivityListBaseAttribute : PXViewExtensionAttribute
{
  private readonly BqlCommand _command;
  private PXView _view;
  protected string _hostViewName;
  private PXSelectBase _select;

  protected CRActivityListBaseAttribute()
  {
  }

  protected CRActivityListBaseAttribute(System.Type select)
  {
    if (select == (System.Type) null)
      throw new ArgumentNullException(nameof (select));
    this._command = typeof (IBqlSelect).IsAssignableFrom(select) ? BqlCommand.CreateInstance(new System.Type[1]
    {
      select
    }) : throw new PXArgumentException("@select", PXMessages.LocalizeFormatNoPrefixNLA("The {0} select expression is incorrect.", new object[1]
    {
      (object) select.Name
    }));
  }

  public virtual void ViewCreated(PXGraph graph, string viewName)
  {
    this.Initialize(graph, viewName);
    this.AttachHandlers(graph);
  }

  private void Initialize(PXGraph graph, string viewName)
  {
    this._hostViewName = viewName;
    this._select = this.GetSelectView(graph);
    if (this._command == null)
      return;
    this._view = new PXView(graph, true, this._command);
  }

  protected PXSelectBase GraphSelector => this._select;

  protected abstract void AttachHandlers(PXGraph graph);

  protected object SelectRecord()
  {
    object obj = this._view != null ? this._view.SelectSingle(Array.Empty<object>()) : throw new InvalidOperationException("The Select command is not specified.");
    if (obj == null)
      return (object) null;
    return !(obj is PXResult pxResult) ? obj : pxResult[0];
  }

  protected virtual PXSelectBase GetSelectView(PXGraph graph)
  {
    object selectView = graph.GetType().GetField(this._hostViewName).GetValue((object) graph);
    System.Type type = selectView.GetType();
    for (System.Type c = type; c != typeof (object); c = c.BaseType)
    {
      if (c.IsGenericType)
        c = c.GetGenericTypeDefinition();
      if (typeof (CRActivityList<>).IsAssignableFrom(c))
        return (PXSelectBase) selectView;
    }
    throw new PXArgumentException((string) null, PXMessages.LocalizeFormatNoPrefixNLA("Attribute '{0}' can be used on only the {1} view or its children.", new object[2]
    {
      (object) ((object) this).GetType().Name,
      (object) type.Name
    }));
  }
}
