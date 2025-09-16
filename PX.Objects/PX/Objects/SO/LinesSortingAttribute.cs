// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.LinesSortingAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public abstract class LinesSortingAttribute : PXEventSubscriberAttribute, IPXRowDeletedSubscriber
{
  protected Type ParentType { get; }

  protected LinesSortingAttribute(Type parentType) => this.ParentType = parentType;

  protected abstract bool AllowSorting(object parent);

  public virtual void RowDeleted(PXCache linesCache, PXRowDeletedEventArgs e)
  {
    if (this.AllowSorting(PXParentAttribute.SelectParent(linesCache, e.Row, this.ParentType)))
      return;
    linesCache.SetValue(e.Row, this.FieldName, (object) null);
  }

  public static bool AllowSorting<TParent>(PXCache linesCache, TParent parent) where TParent : IBqlTable
  {
    if ((object) parent == null)
      return false;
    foreach (LinesSortingAttribute sortingAttribute in linesCache.GetAttributesOfType<LinesSortingAttribute>((object) null, (string) null))
    {
      if (sortingAttribute.ParentType == typeof (TParent))
        return sortingAttribute.AllowSorting((object) parent);
    }
    return false;
  }
}
