// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.PercentTotalDBDecimalAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

public class PercentTotalDBDecimalAttribute : PercentDBDecimalAttribute
{
  protected Type _MapErrorTo;
  protected PXPersistingCheck _PersistingCheck;

  public virtual PXPersistingCheck PersistingCheck
  {
    get => this._PersistingCheck;
    set => this._PersistingCheck = value;
  }

  public virtual Type MapErrorTo
  {
    get => this._MapErrorTo;
    set => this._MapErrorTo = value;
  }

  public PercentTotalDBDecimalAttribute()
  {
    this.MinValue = 0.0;
    this.MaxValue = 99999.0;
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object obj1;
    if (this._PersistingCheck == 2 || (e.Operation & 3) != 2 && (e.Operation & 3) != 1 || (obj1 = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal)) == null || !((Decimal) obj1 != 1M))
      return;
    object obj2 = (object) ((Decimal) obj1 * 100M);
    if (this._MapErrorTo == (Type) null)
    {
      if (sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, obj2, (Exception) new PXSetPropertyException("Value must equal 100%.", new object[1]
      {
        (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]"
      })))
        throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, (object) null, "Value must equal 100%.", new object[1]
        {
          (object) ((PXEventSubscriberAttribute) this)._FieldName
        });
    }
    else
    {
      string name = this._MapErrorTo.Name;
      string str = char.ToUpper(name[0]).ToString() + name.Substring(1);
      object valueExt = sender.GetValueExt(e.Row, str);
      if (valueExt is PXFieldState)
        valueExt = ((PXFieldState) valueExt).Value;
      if (sender.RaiseExceptionHandling(str, e.Row, valueExt, (Exception) new PXSetPropertyException("Value must equal 100%.", new object[2]
      {
        (object) str,
        (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]"
      })))
        throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, (object) null, "Value must equal 100%.", new object[1]
        {
          (object) ((PXEventSubscriberAttribute) this)._FieldName
        });
    }
  }
}
