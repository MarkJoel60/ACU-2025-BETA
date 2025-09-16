// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.SOOrderUnbilledFreightTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.Attributes;

/// <summary>
/// Extends <see cref="T:PX.Objects.SO.SOOrderTaxAttribute" /> and calculates <see cref="P:PX.Objects.SO.SOOrder.CuryUnbilledOrderTotal" /> and <see cref="P:PX.Objects.SO.SOOrder.CuryUnbilledTaxTotal" /> for the Parent (Header) SOOrder.
/// This Attribute overrides some of functionality of <see cref="T:PX.Objects.SO.SOTaxAttribute" />.
/// This Attribute is applied to the <see cref="P:PX.Objects.SO.SOOrder.FreightTaxCategoryID" /> field instead of SO Line.
/// </summary>
/// <example>
/// [SOOrderUnbilledFreightTax(typeof(SOOrder), typeof(SOTax), typeof(SOTaxTran), typeof(taxCalcMode), TaxCalc = TaxCalc.ManualLineCalc)]
/// </example>
public class SOOrderUnbilledFreightTaxAttribute : SOOrderTaxAttribute
{
  protected override short SortOrder => 2;

  public SOOrderUnbilledFreightTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : this(ParentType, TaxType, TaxSumType, (Type) null)
  {
  }

  public SOOrderUnbilledFreightTaxAttribute(
    Type ParentType,
    Type TaxType,
    Type TaxSumType,
    Type TaxCalculationMode)
    : base(ParentType, TaxType, TaxSumType, TaxCalculationMode)
  {
    this._CuryTaxableAmt = typeof (SOTax.curyUnbilledTaxableAmt).Name;
    this._CuryExemptedAmt = typeof (SOTax.curyUnbilledTaxableAmt).Name;
    this._CuryTaxAmt = typeof (SOTax.curyUnbilledTaxAmt).Name;
    this.CuryLineTotal = typeof (SOOrder.curyUnbilledLineTotal);
    this.CuryTaxTotal = typeof (SOOrder.curyUnbilledTaxTotal);
    this.CuryTranAmt = typeof (SOOrder.curyUnbilledFreightTot);
    this._Attributes.Clear();
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(sender.Graph is SOInvoiceEntry))
      return;
    this.TaxCalc = TaxCalc.Calc;
  }

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    this._CalcDocTotals(sender, row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal, CuryTaxDiscountTotal);
    Decimal num1 = (Decimal) (this.ParentGetValue<SOOrder.curyUnbilledLineTotal>(sender.Graph) ?? (object) 0M);
    Decimal num2 = (Decimal) (this.ParentGetValue<SOOrder.curyUnbilledMiscTot>(sender.Graph) ?? (object) 0M);
    Decimal num3 = (Decimal) (this.ParentGetValue<SOOrder.curyUnbilledFreightTot>(sender.Graph) ?? (object) 0M);
    Decimal num4 = (Decimal) (this.ParentGetValue<SOOrder.curyUnbilledDiscTotal>(sender.Graph) ?? (object) 0M);
    Decimal num5 = num2;
    Decimal objA = num1 + num5 + num3 + CuryTaxTotal - CuryInclTaxTotal - num4;
    if (object.Equals((object) objA, (object) (Decimal) (this.ParentGetValue<SOOrder.curyUnbilledOrderTotal>(sender.Graph) ?? (object) 0M)))
      return;
    this.ParentSetValue<SOOrder.curyUnbilledOrderTotal>(sender.Graph, (object) objA);
    this.ParentSetValue<SOOrder.openDoc>(sender.Graph, (object) (objA != 0M));
  }

  protected override void ReDefaultTaxes(PXCache cache, List<object> details)
  {
  }

  protected override void ReDefaultTaxes(
    PXCache cache,
    object clearDet,
    object defaultDet,
    bool defaultExisting = true)
  {
  }
}
