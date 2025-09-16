// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyInfoDBDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CM;

public class CurrencyInfoDBDefaultAttribute(Type sourceType) : PXDBDefaultAttribute(sourceType)
{
  protected virtual void EnsureIsRestriction(PXCache sender)
  {
    if (this._IsRestriction.Value.HasValue)
      return;
    this._IsRestriction.Value = new bool?(true);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this._SourceType != (Type) null && e.Row != null)
    {
      PXCache cach = sender.Graph.Caches[this._SourceType];
      if (cach.Current != null)
      {
        e.NewValue = cach.GetValue(cach.Current, this._SourceField ?? ((PXEventSubscriberAttribute) this)._FieldName);
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
      object obj = PXParentAttribute.SelectParent(sender, e.Row, this._SourceType);
      if (obj != null)
      {
        e.NewValue = cach.GetValue(obj, this._SourceField ?? ((PXEventSubscriberAttribute) this)._FieldName);
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    base.FieldDefaulting(sender, e);
  }

  public virtual void SourceRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this._SourceType == typeof (CurrencyInfo))
    {
      CurrencyInfo row = (CurrencyInfo) e.Row;
      long? nullable = CurrencyCollection.MatchBaseCuryInfoId(row);
      if (e.Operation == 2 && nullable.HasValue)
      {
        CurrencyInfo copy = (CurrencyInfo) sender.CreateCopy((object) row);
        this.StorePersisted(sender, (object) copy);
        copy.CuryInfoID = nullable;
        return;
      }
    }
    base.SourceRowPersisting(sender, e);
  }
}
