// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBCalcedAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Defines the SQL expression that calculates a field
/// from the fields of the same DAC whose values are taken from the
/// database.</summary>
/// <remarks>
/// <para>You place this attribute on the field that is not bound to
/// any particular database column, but to multiple database columns.
/// Fields marked with this attribute are considered bound to the database.</para>
/// <para>The attribute will translate the provided BQL query into the SQL
/// code and insert it into the SELECT statement that retrieves data
/// records of this DAC. In the BQL query, you can reference any bound
/// field of the same DAC, including those marked with <see cref="T:PX.Data.PXDBScalarAttribute">PXDBScalar</see>. You can also use BQL
/// constants, arithmetic operations, equivalents of SQL function (such as
/// <tt>SUBSTRING</tt> and <tt>REPLACE</tt>), and the <tt>Switch</tt>
/// expression.</para>
/// <para>If, in contrast, you need to calculate the field on the server
/// side at runtime, use the <see cref="T:PX.Data.PXFormulaAttribute">PXFormula</see> attribute.</para>
/// <para>Note that you should also mark the field with an attribute
/// that usually indicates an unbound
/// field of a particular data type. Otherwise, the field may be
/// displayed incorrectly in the user interface.</para>
/// </remarks>
/// <example>
/// <code title="Example" lang="CS">
/// // The attribute below defines the expression to calculate the field of decimal type.
/// [PXDBCalced(typeof(Sub&lt;POLine.curyExtCost, POLine.curyOpenAmt&gt;),
///             typeof(decimal))]
/// public virtual decimal? CuryClosedAmt { get; set; }</code>
/// <code title="Example2" lang="CS">
/// // See the following example with the Switch expression.
/// [PXDBCalced(
///     typeof(Switch&lt;Case&lt;Where&lt;INUnit.unitMultDiv, Equal&lt;MultDiv.divide&gt;&gt;,
///         Mult&lt;INSiteStatus.qtyOnHand, INUnit.unitRate&gt;&gt;,
///         Div&lt;INSiteStatus.qtyOnHand, INUnit.unitRate&gt;&gt;),
///     typeof(decimal))]
/// public virtual decimal? QtyOnHandExt { get; set; }</code>
/// <code title="Example3" lang="CS">
/// // See the following example with the more complex BQL expression.
/// [Serializable]
/// public class Product : PXBqlTable, PX.Data.IBqlTable
/// {
///     ...
///     [PXDecimal(2)]
///     [PXDBCalced(typeof(
///         Minus&lt;Sub&lt;Sub&lt;IsNull&lt;Product.availQty, decimal_0&gt;,
///                       IsNull&lt;Product.bookedQty, decimal_0&gt;&gt;,
///               Product.minAvailQty&gt;&gt;),
///         typeof(decimal))]
///     public virtual decimal? Discrepancy { get; set; }
///     ...
/// }</code>
/// <code title="Example4" lang="CS">
/// // This example also shows the enclosing declaration of the Product DAC.
/// // You can retrieve the records from the Product table by executing the following code in some graph.
/// PXSelect&lt;Product&gt;.Select(this);</code>
/// <code title="Example5" lang="SQL">
/// // This BQL statement will be translated into the following SQL query.
/// SELECT [other fields],
///         -((ISNULL(Product.AvailQty, .0) - ISNULL(Product.BookedQty, .0))
///           - Product.MinAvailQty) as Product.Discrepancy
/// FROM Product</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class PXDBCalcedAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldSelectingSubscriber
{
  protected System.Type _OperandType;
  protected IBqlCreator _Operand;
  protected System.Type _Type;
  protected int _DatabaseOrdinal = -1;
  protected bool _Persistent;
  protected bool _BypassGroupby;
  public int CastToPrecision;
  public int CastToScale;

  /// <summary>Gets or sets the value that indicates whether the field the
  /// attribute is attached to is updated after a database commit
  /// operation.</summary>
  public virtual bool Persistent
  {
    get => this._Persistent;
    set => this._Persistent = value;
  }

  public PXDBCalcedAttribute(System.Type operand, System.Type type)
  {
    this._OperandType = operand;
    this._Type = type;
    foreach (System.Type c in BqlCommand.Decompose(operand))
    {
      if (typeof (IBqlSearch).IsAssignableFrom(c))
        this._BypassGroupby = true;
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!this._Persistent)
      return;
    sender.Graph.RowPersisted.AddHandler(sender.GetItemType(), new PXRowPersisted(this.RowPersisted));
  }

  /// <exclude />
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select || sender.BypassCalced && sender.BqlSelect == null)
      return;
    e.DataValue = e.Value;
    SQLExpression exp = (SQLExpression) null;
    List<System.Type> tables = new List<System.Type>();
    bool flag = false;
    if (e.Table != (System.Type) null)
    {
      tables.Add(e.Table);
    }
    else
    {
      tables.Add(this._BqlTable);
      System.Type type;
      if (sender.GetExtensionTables() != null && (type = sender.GetItemType()) != this._BqlTable)
      {
        for (; type != typeof (object) && !type.IsDefined(typeof (PXTableAttribute), false); type = type.BaseType)
        {
          if (type == this._BqlTable)
          {
            flag = true;
            break;
          }
        }
      }
    }
    if (!typeof (IBqlCreator).IsAssignableFrom(this._OperandType))
    {
      exp = string.Compare(this._OperandType.Name, this._FieldName, StringComparison.OrdinalIgnoreCase) == 0 ? (SQLExpression) new Column(this._OperandType.Name, (Table) new SimpleTable(tables[0])) : BqlCommand.GetSingleExpression(this._OperandType, sender.Graph, tables, (BqlCommand.Selection) null, BqlCommand.FieldPlace.Select);
    }
    else
    {
      if (this._Operand == null)
        this._Operand = Activator.CreateInstance(this._OperandType) as IBqlCreator;
      if (this._Operand == null)
        throw new PXArgumentException("Operand", "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
      Dictionary<PXCache, BqlCommand> dictionary = new Dictionary<PXCache, BqlCommand>();
      dictionary[sender] = sender.BqlSelect;
      if (flag)
      {
        System.Type type = sender.GetItemType();
        while ((type = type.BaseType) != typeof (object) && typeof (IBqlTable).IsAssignableFrom(type))
        {
          PXCache cach = sender.Graph.Caches[type];
          if (!dictionary.ContainsKey(cach))
            dictionary[cach] = cach.BqlSelect;
        }
      }
      if (sender.GetItemType().IsDefined(typeof (PXProjectionAttribute), false) && sender.BqlSelect != null)
      {
        foreach (System.Type table in sender.BqlSelect.GetTables())
        {
          PXCache cach = sender.Graph.Caches[table];
          if (cach.GetExtensionTables() != null && !dictionary.ContainsKey(cach))
            dictionary[cach] = cach.BqlSelect;
        }
      }
      try
      {
        foreach (PXCache key in dictionary.Keys)
          key.BqlSelect = (BqlCommand) null;
        this._Operand.AppendExpression(ref exp, sender.Graph, new BqlCommandInfo(false)
        {
          Tables = tables
        }, (BqlCommand.Selection) null);
      }
      finally
      {
        foreach (KeyValuePair<PXCache, BqlCommand> keyValuePair in dictionary)
          keyValuePair.Key.BqlSelect = keyValuePair.Value;
      }
    }
    e.BqlTable = this._BqlTable;
    if ((e.Operation & PXDBOperation.Place) == PXDBOperation.NestedSelectInReport)
      e.Expr = (SQLExpression) null;
    else if (((e.Operation & PXDBOperation.Option) != PXDBOperation.GroupBy ? 1 : (!(this._Type != typeof (bool)) || !(this._Type != typeof (Guid)) ? 0 : (!this._BypassGroupby ? 1 : 0))) != 0)
    {
      if (exp == null)
      {
        e.Expr = (SQLExpression) null;
      }
      else
      {
        if (this._Type == typeof (Decimal) && this.CastToPrecision > 0)
        {
          e.Expr = exp.AddCastTo(this._Type, this.CastToPrecision, this.CastToScale);
        }
        else
        {
          SQLExpression sqlExpression = !(exp is Column) ? exp.AddCastToIfNeeded(this._Type, 0, 0) : SQLExpression.Empty().SetLeft(exp);
          e.Expr = sqlExpression;
        }
        e.Expr?.Embrace();
      }
    }
    else
      e.Expr = SQLExpression.Null();
  }

  /// <exclude />
  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, e.Record.GetValue(e.Position, this._Type));
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered || e.ReturnState is PXFieldState returnState && !(returnState.DataType == typeof (object)) && !(returnState.DataType == this._Type))
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, this._Type, new bool?(false), new bool?(true), fieldName: this._FieldName);
  }

  /// <summary>Calculates the field value of the data record using the
  /// formula from the attribute instance that marks the
  /// specified field. </summary>
  /// <param name="Field">The field to calculate.</param>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXDBCalced</tt> type.</param>
  /// <param name="row">The data record.</param>
  public static void Calculate<Field>(PXCache sender, object row) where Field : IBqlField
  {
    foreach (PXDBCalcedAttribute pxdbCalcedAttribute in sender.GetAttributesReadonly<Field>(row).OfType<PXDBCalcedAttribute>())
      pxdbCalcedAttribute.Calculate(sender, row);
  }

  /// <exclude />
  public virtual void Calculate(PXCache sender, object row)
  {
    bool? result = new bool?();
    object obj = (object) null;
    if (typeof (IBqlField).IsAssignableFrom(this._OperandType))
    {
      if (sender.GetItemType() == BqlCommand.GetItemType(this._OperandType) && BqlCommand.GetItemType(this._OperandType).IsAssignableFrom(sender.GetItemType()))
        obj = sender.GetValue(row, this._OperandType.Name);
    }
    else
    {
      if (this._Operand == null)
        this._Operand = Activator.CreateInstance(this._OperandType) as IBqlCreator;
      if (this._Operand == null)
        throw new PXArgumentException("Operand", "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
      this._Operand.Verify(sender, row, (List<object>) null, ref result, ref obj);
    }
    sender.SetValue(row, this._FieldName, obj);
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    this.Calculate(sender, e.Row);
  }
}
