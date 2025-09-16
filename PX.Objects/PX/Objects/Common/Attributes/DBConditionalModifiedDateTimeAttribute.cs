// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DBConditionalModifiedDateTimeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class DBConditionalModifiedDateTimeAttribute : 
  PXDBLastModifiedDateTimeAttribute,
  IPXCommandPreparingSubscriber,
  IPXRowPersistedSubscriber,
  IPXRowUpdatingSubscriber,
  IPXRowInsertingSubscriber
{
  protected Type _valueField;
  protected object _expectedValue;

  public bool KeepValue { get; set; }

  public bool InvertLogic { get; set; }

  /// <summary>
  /// Initializes a new instance of the DBConditionalModifiedDateTimeAttribute attribute.
  /// If the new value is equal to the expected value, then this field will have the value "GetDate()" in the sql query.
  /// </summary>
  /// <param name="valueField">The reference to a field in same DAC. Cannot be null.</param>
  /// <param name="expectedValue">Expected value for "valueField".</param>
  public DBConditionalModifiedDateTimeAttribute(Type valueField, object expectedValue)
  {
    this._valueField = valueField ?? throw new PXArgumentException(nameof (valueField));
    this._expectedValue = expectedValue;
  }

  void IPXCommandPreparingSubscriber.CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    ((PXDBFieldAttribute) this).CommandPreparing(sender, e);
    if (e.Row == null || !((e.Operation & 3) == 2 | (e.Operation & 3) == 1))
      return;
    if (this.InvertLogic ^ object.Equals(sender.GetValue(e.Row, this._valueField.Name), this._expectedValue))
    {
      if (e.Value == null)
      {
        e.DataType = (PXDbType) 200;
        e.DataValue = ((PXDBDateAttribute) this).UseTimeZone ? (object) e.SqlDialect.GetUtcDate : (object) e.SqlDialect.GetDate;
      }
      else
        e.ExcludeFromInsertUpdate();
    }
    else
    {
      if (this.KeepValue)
        return;
      e.DataValue = (object) null;
    }
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!((e.Operation & 3) == 2 | (e.Operation & 3) == 1) || e.TranStatus != 1)
      return;
    if (this.InvertLogic ^ object.Equals(sender.GetValue(e.Row, this._valueField.Name), this._expectedValue))
    {
      if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) != null)
        return;
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) DateTime.Now);
    }
    else
    {
      if (this.KeepValue)
        return;
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
    }
  }

  void IPXRowUpdatingSubscriber.RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
  }

  void IPXRowInsertingSubscriber.RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
  }
}
