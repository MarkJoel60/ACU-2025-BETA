// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.InvoiceTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

[PXHidden]
public class InvoiceTran : PXMappedCacheExtension
{
  /// <summary>The identifier of the branch associated with the document.</summary>
  public virtual int? BranchID { get; set; }

  public virtual DateTime? TranDate { get; set; }

  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  public virtual string TranPeriodID { get; set; }

  public virtual int? LineNbr { get; set; }

  public virtual string TaxCategoryID { get; set; }

  public virtual int? InventoryID { get; set; }

  public virtual string TranDesc { get; set; }

  public virtual bool? ManualPrice { get; set; }

  public virtual Decimal? CuryUnitCost { get; set; }

  public virtual Decimal? Qty { get; set; }

  public virtual string UOM { get; set; }

  public virtual bool? NonBillable { get; set; }

  public virtual DateTime? Date { get; set; }

  public virtual int? ProjectID { get; set; }

  public virtual int? TaskID { get; set; }

  public virtual int? CostCodeID { get; set; }

  public virtual int? AccountID { get; set; }

  public virtual int? SubID { get; set; }

  public virtual Decimal? CuryLineAmt { get; set; }

  public virtual Decimal? CuryTaxAmt { get; set; }

  public virtual Decimal? CuryTaxableAmt { get; set; }

  public virtual Decimal? CuryTranAmt { get; set; }

  public virtual Decimal? TaxableAmt { get; set; }

  public virtual Decimal? TranAmt { get; set; }

  /// <exclude />
  public abstract class branchID : IBqlField, IBqlOperand
  {
  }

  public abstract class tranDate : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class finPeriodID : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceTran.lineNbr>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceTran.taxCategoryID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceTran.inventoryID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceTran.tranDesc>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceTran.manualPrice>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceTran.curyUnitCost>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceTran.qty>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceTran.uOM>
  {
  }

  public abstract class nonBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceTran.nonBillable>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  InvoiceTran.date>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceTran.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceTran.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceTran.costCodeID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceTran.subID>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceTran.curyLineAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceTran.curyTaxAmt>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InvoiceTran.curyTaxableAmt>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceTran.curyTranAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceTran.taxableAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceTran.tranAmt>
  {
  }
}
