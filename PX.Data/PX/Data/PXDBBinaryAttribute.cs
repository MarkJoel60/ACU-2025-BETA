// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBBinaryAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>byte[]</tt> type to the binary
/// database column of either fixed or variable length.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example>
/// <code>
/// [PXDBBinary]
/// [PXUIField(Visible = false)]
/// public virtual byte[] NewValue { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBBinaryAttribute : PXDBFieldAttribute
{
  protected int _Length = -1;
  protected bool _IsFixed;

  /// <summary>Gets or sets an indication that the binay value has a fixed
  /// length. This property should be set to <tt>true</tt> if the database
  /// column has a fixed length type (<tt>binary</tt>) and to <tt>false</tt>
  /// if the database column has a variable length type
  /// (<tt>varbinary</tt>). The default value is <tt>false</tt>.</summary>
  public bool IsFixed
  {
    get => this._IsFixed;
    set => this._IsFixed = value;
  }

  /// <summary>Gets the maximum length of the binary value.</summary>
  /// <remarks>The default value is -1 (the length is not limited). A different
  /// value can be set in the constructor.</remarks>
  public int Length => this._Length;

  /// <summary>Initializes a new unparameterized instance of the attribute.</summary>
  public PXDBBinaryAttribute()
  {
  }

  /// <summary>Initializes a new instance with the given maximum
  /// length.</summary>
  /// <param name="length">The maximum length of the field value.</param>
  /// <example>
  /// The code below shows the value definition of a DAC field.
  /// <code>
  /// [PXDBBinary(500)]
  /// public byte[] CommonValues { get; set; }
  /// </code>
  /// </example>
  public PXDBBinaryAttribute(int length) => this._Length = length;

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    if (this._DatabaseFieldName != null && (e.Operation & PXDBOperation.Option) == PXDBOperation.GroupBy)
      e.Expr = SQLExpression.Null();
    e.DataType = this._IsFixed ? PXDbType.Binary : PXDbType.VarBinary;
    e.DataValue = (object) (this.SerializeValue(e.Value) ?? new byte[0]);
    if (this._Length <= -1)
      return;
    e.DataLength = new int?(this._Length);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      byte[] numArray;
      if (this._Length > -1)
      {
        numArray = new byte[this._Length];
        e.Record.GetBytes(e.Position, 0L, numArray, 0, this._Length);
      }
      else
        numArray = e.Record.GetBytes(e.Position);
      if (numArray == null)
        numArray = new byte[0];
      object obj = this.DeserializeValue(numArray);
      sender.SetValue(e.Row, this._FieldOrdinal, obj);
    }
    ++e.Position;
  }

  protected virtual object DeserializeValue(byte[] bytes) => (object) bytes;

  protected virtual byte[] SerializeValue(object value) => (byte[]) value;
}
