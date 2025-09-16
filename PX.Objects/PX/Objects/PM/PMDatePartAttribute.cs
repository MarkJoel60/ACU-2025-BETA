// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMDatePartAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>Abstract attribute for date part.</summary>
public abstract class PMDatePartAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertingSubscriber,
  IPXRowUpdatedSubscriber
{
  private string _dateFieldName;

  public PMDatePartAttribute(Type dateFieldType)
  {
    string str = !(dateFieldType == (Type) null) ? dateFieldType.Name : throw new ArgumentNullException(nameof (dateFieldType));
    this._dateFieldName = char.ToUpper(str[0]).ToString() + str.Substring(1);
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.UpdateDatePartValue(sender, e.Row);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.UpdateDatePartValue(sender, e.Row);
  }

  protected virtual void UpdateDatePartValue(PXCache sender, object row)
  {
    PXCache cach = sender.Graph.Caches[this._BqlTable];
    object dt = cach.GetValue(row, this._dateFieldName);
    if (dt == null)
      return;
    object datePartValue = this.GetDatePartValue((DateTime) dt);
    if (datePartValue == null)
      return;
    cach.SetValue(row, this._FieldName, datePartValue);
  }

  protected abstract object GetDatePartValue(DateTime dt);
}
