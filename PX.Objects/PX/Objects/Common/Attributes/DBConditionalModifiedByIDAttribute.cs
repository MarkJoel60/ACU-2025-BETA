// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DBConditionalModifiedByIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class DBConditionalModifiedByIDAttribute : PXDBGuidAttribute, IPXRowPersistedSubscriber
{
  protected Type _valueField;
  protected object _expectedValue;

  /// <summary>
  /// Initializes a new instance of the DBConditionalModifiedByAttribute attribute.
  /// If the new value is equal to the expected value, then this field will have the identifier value of current user.
  /// </summary>
  /// <param name="valueField">The reference to a field in same DAC. Cannot be null.</param>
  /// <param name="expectedValue">Expected value for "valueField".</param>
  public DBConditionalModifiedByIDAttribute(Type valueField, object expectedValue)
    : base(false)
  {
    this._valueField = valueField ?? throw new PXArgumentException(nameof (valueField));
    this._expectedValue = expectedValue;
  }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    ((PXDBFieldAttribute) this).CommandPreparing(sender, e);
    if (e.Row == null || !((e.Operation & 3) == 2 | (e.Operation & 3) == 1))
      return;
    if (object.Equals(sender.GetValue(e.Row, this._valueField.Name), this._expectedValue))
    {
      if (e.Value == null)
      {
        e.DataType = (PXDbType) 14;
        e.DataValue = (object) PXAccess.GetTrueUserID();
      }
      else
        e.ExcludeFromInsertUpdate();
    }
    else
      e.DataValue = (object) null;
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!((e.Operation & 3) == 2 | (e.Operation & 3) == 1) || e.TranStatus != 1)
      return;
    if (object.Equals(sender.GetValue(e.Row, this._valueField.Name), this._expectedValue))
    {
      if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) != null)
        return;
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) PXAccess.GetTrueUserID());
    }
    else
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
  }
}
