// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DBIntConditionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class DBIntConditionAttribute : PXDBIntAttribute
{
  protected Type _valueField;
  protected string _databaseField;
  protected object _expectedValue;

  /// <summary>
  /// Initializes a new instance of the DBIntConditionAttribute attribute.
  /// If the new value is equal to the expected value, then this field will save to database.
  /// </summary>
  /// <param name="valueField">The reference to a field in same DAC. Cannot be null.</param>
  /// <param name="expectedValue">Expected value for "valueField".</param>
  /// <param name="databaseField">A value of property will save to this field.</param>
  public DBIntConditionAttribute(Type valueField, object expectedValue, Type databaseField)
  {
    this._valueField = valueField ?? throw new PXArgumentException(nameof (valueField));
    this._expectedValue = expectedValue;
    if (databaseField == (Type) null)
      throw new PXArgumentException(nameof (databaseField));
    this._databaseField = char.ToUpper(databaseField.Name[0]).ToString() + databaseField.Name.Substring(1);
  }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    ((PXDBFieldAttribute) this).CommandPreparing(sender, e);
    e.BqlTable = ((PXEventSubscriberAttribute) this)._BqlTable;
    Type type = e.Table == (Type) null ? ((PXEventSubscriberAttribute) this)._BqlTable : e.Table;
    e.Expr = (SQLExpression) new Column(this._databaseField, (Table) new SimpleTable(type, (string) null), e.DataType);
    PXDBOperation pxdbOperation = PXDBOperationExt.Command(e.Operation);
    if (pxdbOperation != 1 && pxdbOperation != 2 || object.Equals(sender.GetValue(e.Row, this._valueField.Name), this._expectedValue))
      return;
    e.ExcludeFromInsertUpdate();
  }
}
