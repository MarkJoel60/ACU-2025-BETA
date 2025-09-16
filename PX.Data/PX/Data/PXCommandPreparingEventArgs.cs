// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCommandPreparingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>
/// Provides data for the <tt>CommandPreparing</tt> event.
/// </summary>
/// <seealso cref="T:PX.Data.PXCommandPreparing" />
/// <param name="row">The data record.</param>
/// <param name="value">The field value.</param>
/// <param name="operation">The type of the database operation.</param>
/// <param name="table">The DAC type of the data record.</param>
/// <param name="dialect">The SQL dialect.</param>
/// <summary>
/// Provides data for the <tt>CommandPreparing</tt> event.
/// </summary>
/// <seealso cref="T:PX.Data.PXCommandPreparing" />
/// <param name="row">The data record.</param>
/// <param name="value">The field value.</param>
/// <param name="operation">The type of the database operation.</param>
/// <param name="table">The DAC type of the data record.</param>
/// <param name="dialect">The SQL dialect.</param>
public sealed class PXCommandPreparingEventArgs(
  object row,
  object value,
  PXDBOperation operation,
  System.Type table,
  ISqlDialect dialect = null) : CancelEventArgs
{
  private bool _isExcludedFromUpdate;
  private bool _isExcludedFromDelete;
  /// <summary>The SQL dialect of the command.</summary>
  public readonly ISqlDialect SqlDialect = dialect;
  /// <summary>The SQL tree expression of the command.</summary>
  public SQLExpression Expr;

  /// <summary>Initializes and returns an object that contains a DAC field description
  /// that was used during the current operation.</summary>
  public PXCommandPreparingEventArgs.FieldDescription GetFieldDescription()
  {
    return new PXCommandPreparingEventArgs.FieldDescription(this.BqlTable, this.Expr, this.DataType, this.DataLength, this.DataValue, this.IsRestriction, this._isExcludedFromUpdate, this.IsForcedSubQuery, this._isExcludedFromDelete);
  }

  public void FillFromFieldDescription(
    PXCommandPreparingEventArgs.FieldDescription fDescr)
  {
    this.BqlTable = fDescr != null ? fDescr.BqlTable : throw new ArgumentNullException(nameof (fDescr));
    this.DataLength = fDescr.DataLength;
    this.DataType = fDescr.DataType;
    this.DataValue = fDescr.DataValue;
    this.Expr = fDescr.Expr;
    this.IsRestriction = fDescr.IsRestriction;
    this._isExcludedFromUpdate = fDescr.IsExcludedFromUpdate;
    this._isExcludedFromDelete = fDescr.IsExcludedFromDelete;
    this.IsForcedSubQuery = fDescr.IsForcedSubQuery;
    this.Expr = fDescr.Expr;
  }

  public void ExcludeFromInsertUpdate() => this._isExcludedFromUpdate = true;

  internal void ExcludeFromDelete() => this._isExcludedFromDelete = true;

  internal void ExcludeFromInsertUpdateDeleteStatements()
  {
    this.ExcludeFromInsertUpdate();
    this.ExcludeFromDelete();
  }

  public bool IsSelect() => (this.Operation & PXDBOperation.Delete) == PXDBOperation.Select;

  /// <summary>Returns the current DAC object.</summary>
  public object Row { get; } = row;

  /// <summary>Returns the current DAC field value or sets the value for the DAC field.</summary>
  public object Value { get; set; } = value;

  /// <summary>Returns the type of the current database operation.</summary>
  public PXDBOperation Operation { get; } = operation;

  /// <summary>Returns the type of the DAC objects placed in the cache.</summary>
  public System.Type Table { get; } = table;

  /// <summary>Returns or sets the DAC type that is being used during the current operation.</summary>
  public System.Type BqlTable { get; set; }

  public PXDbType DataType { get; set; } = PXDbType.Unspecified;

  /// <summary>Returns or sets the number of characters in the DAC field being
  /// used during the current operation.</summary>
  public int? DataLength { get; set; }

  /// <summary>Returns or sets the DAC field value being used during the
  /// current operation.</summary>
  public object DataValue { get; set; }

  /// <summary>Returns or sets the value indicating that the DAC field
  /// being used during the <tt>UPDATE</tt> or <tt>DELETE</tt> operation is placed in the <tt>WHERE</tt> clause.</summary>
  public bool IsRestriction { get; set; }

  /// <summary>
  /// Returns or sets the value indicating that the DAC field is a subquery and must be used as is.
  /// </summary>
  internal bool IsForcedSubQuery { get; set; }

  /// <summary>
  /// The nested class that provides information about the field
  /// required for the SQL statement generation.
  /// </summary>
  [Serializable]
  public sealed class FieldDescription : 
    ICloneable,
    IEquatable<PXCommandPreparingEventArgs.FieldDescription>
  {
    /// <summary>The type of the DAC objects placed in the cache.</summary>
    public readonly System.Type BqlTable;
    public PXDbType DataType;
    /// <summary>The storage size of the DAC field.</summary>
    public int? DataLength;
    /// <summary>The value stored in the DAC field.</summary>
    public object DataValue;
    /// <summary>
    /// The value indicating that the DAC field being used during the
    /// <tt>UPDATE</tt> or <tt>DELETE</tt> operation is placed in the <tt>WHERE</tt> clause.
    /// </summary>
    public readonly bool IsRestriction;
    /// <summary>
    /// The value indicating that the DAC field is a subquery and must be used as is.
    /// </summary>
    public readonly bool IsForcedSubQuery;
    /// <summary>The SQL tree expression of the field.</summary>
    public SQLExpression Expr;

    internal bool IsExcludedFromUpdate { get; private set; }

    internal bool IsExcludedFromDelete { get; private set; }

    internal FieldDescription(
      System.Type bqlTable,
      SQLExpression expr,
      PXDbType dataType,
      int? dataLength,
      object dataValue,
      bool isRestriction,
      bool isExcludedFromUpdate = false,
      bool isForcedSubQuery = false,
      bool isExcludedFromDelete = false)
    {
      this.BqlTable = bqlTable;
      this.DataType = dataType;
      this.DataLength = dataLength;
      this.DataValue = dataValue;
      this.IsRestriction = isRestriction;
      this.IsForcedSubQuery = isForcedSubQuery;
      this.IsExcludedFromUpdate = isExcludedFromUpdate;
      this.IsExcludedFromDelete = isExcludedFromDelete;
      this.Expr = expr;
    }

    public object Clone()
    {
      return (object) new PXCommandPreparingEventArgs.FieldDescription(this.BqlTable, this.Expr, this.DataType, this.DataLength, this.DataValue, this.IsRestriction, this.IsExcludedFromUpdate, this.IsForcedSubQuery, this.IsExcludedFromDelete);
    }

    public bool Equals(PXCommandPreparingEventArgs.FieldDescription other)
    {
      if (other == null)
        return false;
      if (this == other)
        return true;
      if (this.BqlTable == other.BqlTable && this.Expr == other.Expr && this.DataType == other.DataType)
      {
        int? dataLength1 = this.DataLength;
        int? dataLength2 = other.DataLength;
        if (dataLength1.GetValueOrDefault() == dataLength2.GetValueOrDefault() & dataLength1.HasValue == dataLength2.HasValue && (this.DataValue == other.DataValue || this.DataValue != null && this.DataValue.Equals(other.DataValue)) && this.IsRestriction == other.IsRestriction && this.IsExcludedFromUpdate == other.IsExcludedFromUpdate && this.IsExcludedFromDelete == other.IsExcludedFromDelete)
          return this.IsForcedSubQuery == other.IsForcedSubQuery;
      }
      return false;
    }
  }
}
