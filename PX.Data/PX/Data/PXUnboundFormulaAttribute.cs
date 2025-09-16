// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUnboundFormulaAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Calculates the value from the child data record fields and
/// computes the aggregation of such values over all child data
/// records.</summary>
/// <remarks>
///   <para>Unlike the <see cref="T:PX.Data.PXFormulaAttribute">PXFormula</see> attribute, this attribute does not
/// assign the computed value to the field the attribute is attached to.
/// The value is only used for aggregated calculation of the parent data
/// record field. Hence, you can place this attribute on declaration of
/// any child DAC field.</para>
///   <para>The <see cref="T:PX.Data.PXParentAttribute">PXParent</see> attribute must be added to some field of
/// the child DAC.</para>
/// </remarks>
/// <example>
///   <code title="Example">[PXUnboundFormula(
///     typeof(APAdjust.adjgBalSign.Multiply&lt;APAdjust.curyAdjgAmt&gt;),
///     typeof(SumCalc&lt;APPayment.curyApplAmt&gt;))]
/// public virtual decimal? CuryAdjgAmt { get; set; }</code>
///   <code title="Example2">//Multiple UnboundFormula attributes can be placed on the same DAC field definition,
/// //as shown in the example below.
/// [PXUnboundFormula(
///     typeof(Switch&lt;
///         Case&lt;WhereExempt&lt;APTaxTran.taxID&gt;, APTaxTran.curyTaxableAmt&gt;,
///         decimal0&gt;),
///     typeof(SumCalc&lt;APInvoice.curyVatExemptTotal&gt;))]
/// [PXUnboundFormula(
///     typeof(Switch&lt;
///         Case&lt;WhereTaxable&lt;APTaxTran.taxID&gt;, APTaxTran.curyTaxableAmt&gt;,
///         decimal0&gt;),
///     typeof(SumCalc&lt;APInvoice.curyVatTaxableTotal&gt;))]
/// public override Decimal? CuryTaxableAmt { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
/// <summary>
/// Initializes a new instance that calculates the value of the field
/// the attribute is atached to and sets an aggregate function to calculate
/// the value of a field in the parent data record. The aggregation
/// function is applied to the values calculated by the first parameter
/// for all child data records.
/// </summary>
/// <param name="formulaType">The formula to calculate the field value from
/// other fields of the same data record; an expression built from BQL functions
/// such as <tt>Add</tt>, <tt>Sub</tt>, <tt>Mult</tt>, <tt>Div</tt>, <tt>Switch</tt> and other functions.
/// If null, the aggregation function takes into account the field value
/// inputted by the user.</param>
/// <param name="aggregateType">The aggregation formula to calculate the parent
/// data record field from the child data records fields.</param>
public class PXUnboundFormulaAttribute(System.Type formulaType, System.Type aggregateType) : 
  PXFormulaAttribute(formulaType, aggregateType)
{
  /// <summary>Gets the name of the field the attribute is attached
  /// to.</summary>
  public override string FormulaFieldName => (string) null;

  protected override void InitDependencies(PXCache sender)
  {
  }

  protected override void InitAggregate(System.Type aggregateType)
  {
    if (aggregateType != (System.Type) null)
    {
      this._ParentFieldType = aggregateType.GetGenericArguments()[0];
      if (!typeof (IBqlField).IsAssignableFrom(this._ParentFieldType))
        throw new PXArgumentException("_ParentFieldType", "The parent field '{0}' cannot be obtained.", new object[1]
        {
          (object) this._ParentFieldType.Name
        });
      this._Aggregate = typeof (IBqlUnboundAggregateCalculator).IsAssignableFrom(aggregateType) ? (object) (IBqlUnboundAggregateCalculator) Activator.CreateInstance(aggregateType) : throw new PXArgumentException("_Aggregate", "The aggregate type {0} cannot be found.", new object[1]
      {
        (object) aggregateType.Name
      });
    }
    else
    {
      this._ParentFieldType = (System.Type) null;
      this._Aggregate = (object) null;
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this._Formula is ISwitch formula))
      return;
    formula.OuterField = (System.Type) null;
  }

  protected override object CalcAggregate(PXCache cache, object row, object oldrow, int digit)
  {
    return ((IBqlUnboundAggregateCalculator) this._Aggregate).Calculate(cache, row, oldrow, this._Formula, digit);
  }

  protected override object CalcAggregate(PXCache cache, object row, object[] records, int digit)
  {
    return ((IBqlUnboundAggregateCalculator) this._Aggregate).Calculate(cache, row, this._Formula, records, digit);
  }

  /// <exclude />
  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (this._Formula == null)
      return;
    this.UpdateParent(sender, e.Row, e.OldRow, new Func<PXCache, object, object>(((PXFormulaAttribute) this).EnsureParent));
    this.EnsureChildren(sender, e.OldRow);
  }

  /// <exclude />
  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (this._Formula == null)
      return;
    object parent = this.EnsureParent(sender, e.Row, e.PendingRow);
    this.UpdateParent(sender, e.Row, (object) null, (Func<PXCache, object, object>) ((cache, row) => parent));
  }

  /// <exclude />
  public override void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this._Formula == null)
      return;
    this.UpdateParent(sender, (object) null, e.Row);
    this.EnsureChildren(sender, e.Row);
  }
}
