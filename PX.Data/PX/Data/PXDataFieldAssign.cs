// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldAssign
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>The parameters of assignment of a field value.</summary>
public class PXDataFieldAssign : PXDataFieldParam
{
  private PXDataFieldAssign.AssignBehavior _Behavior;
  public static PXDataFieldAssign OperationSwitchAllowed = new PXDataFieldAssign("CompanyID", (object) null);

  internal bool IsChanged { get; set; } = true;

  internal object OldValue { get; set; }

  internal string NewValue { get; set; }

  public PXDataFieldAssign(string fieldName, object value)
    : this(new Column(fieldName), value)
  {
  }

  public PXDataFieldAssign(Column column, object value)
    : base(column, column.GetDBType(), value)
  {
    this.NewValue = this.ValueToString(value);
  }

  public PXDataFieldAssign(string fieldName, PXDbType valueType, object value)
    : this(new Column(fieldName), valueType, value)
  {
  }

  public PXDataFieldAssign(Column column, PXDbType valueType, object value)
    : base(column, valueType, value)
  {
    this.NewValue = this.ValueToString(value);
  }

  public PXDataFieldAssign(string fieldName, PXDbType valueType, int? valueLength, object value)
    : this(new Column(fieldName), valueType, valueLength, value)
  {
  }

  public PXDataFieldAssign(Column column, PXDbType valueType, int? valueLength, object value)
    : base(column, valueType, valueLength, value)
  {
    this.NewValue = this.ValueToString(value);
  }

  internal PXDataFieldAssign(
    string fieldName,
    PXDbType valueType,
    int? valueLength,
    object value,
    string auditValue)
    : this(new Column(fieldName), valueType, valueLength, value, auditValue)
  {
  }

  internal PXDataFieldAssign(
    Column column,
    PXDbType valueType,
    int? valueLength,
    object value,
    string auditValue)
    : base(column, valueType, valueLength, value)
  {
    this.NewValue = auditValue;
  }

  internal PXDataFieldAssign copyAndRename(Column newColumn)
  {
    if (this.Storage != StorageBehavior.Table)
      return this;
    return new PXDataFieldAssign(newColumn, this.ValueType, this.ValueLength, this.Value, this.NewValue)
    {
      IsChanged = this.IsChanged,
      OldValue = this.OldValue,
      NewValue = this.NewValue,
      _Behavior = this._Behavior
    };
  }

  public PXDataFieldAssign.AssignBehavior Behavior
  {
    get => this._Behavior;
    set => this._Behavior = value;
  }

  protected string ValueToString(object val)
  {
    if (val == null)
      return (string) null;
    switch (System.Type.GetTypeCode(val.GetType()))
    {
      case TypeCode.Object:
        if (val is Guid guid)
          return guid.ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture);
        return !(val.GetType() == typeof (byte[])) ? (string) null : Convert.ToBase64String((byte[]) val);
      case TypeCode.Boolean:
        return ((bool) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case TypeCode.Int16:
        return ((short) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case TypeCode.Int32:
        return ((int) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case TypeCode.Int64:
        return ((long) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case TypeCode.Single:
        return ((float) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case TypeCode.Double:
        return ((double) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case TypeCode.Decimal:
        return ((Decimal) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case TypeCode.DateTime:
        return ((System.DateTime) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case TypeCode.String:
        return (string) val;
      default:
        return (string) null;
    }
  }

  public enum AssignBehavior
  {
    /// <summary>
    /// Assigns the specified value to the data field on every update. This is the default behavior.
    /// For this policy, set the new value to the field in your code.
    /// </summary>
    Replace,
    /// <summary>
    /// Adds the specified delta (with the appropriate sign) to the data field on every update.
    /// For this policy, set the delta to the field in your code.
    /// </summary>
    Summarize,
    /// <summary>
    /// On every update, assigns to the data field the maximum of the new value and the value from the database.
    /// For this policy, it is not necessary to set the new value to the field in your code.
    /// </summary>
    Maximize,
    /// <summary>
    /// On every update, assigns to the data field the minimum of the new value and the value from the database.
    /// For this policy, it is not necessary to set the new value to the field in your code.
    /// </summary>
    Minimize,
    /// <summary>
    /// Assigns the specified value to the data field if the old value is <see langword="null" />.
    /// For this policy, set the new value to the field in your code.
    /// </summary>
    Initialize,
    /// <summary>
    /// Assigns the <see langword="null" /> value to the data field.
    /// For this policy, it is not necessary to set the new value to the field in your code.
    /// </summary>
    Nullout,
  }
}
