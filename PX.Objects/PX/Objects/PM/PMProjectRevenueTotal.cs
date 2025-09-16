// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectRevenueTotal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Aggregate Total Sum of Revenue(Income) budget lines of a Project.
/// </summary>
[PXCacheName("Contract Total")]
[PXProjection(typeof (Select4<PMBudget, Where<PMBudget.type, Equal<AccountType.income>>, Aggregate<GroupBy<PMBudget.projectID, Sum<PMBudget.curyRevisedAmount, Sum<PMBudget.curyAmount, Sum<PMBudget.curyInvoicedAmount, Sum<PMBudget.curyActualAmount, Sum<PMBudget.curyInclTaxAmount, Sum<PMBudget.curyTotalRetainedAmount, Sum<PMBudget.curyAmountToInvoice, Sum<PMBudget.curyChangeOrderAmount>>>>>>>>>>>))]
public class PMProjectRevenueTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Project</summary>
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <summary>Contract (Revenue) Total</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Contract Total", Enabled = false)]
  public virtual Decimal? CuryAmount { get; set; }

  /// <summary>Revised Contract Total</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyRevisedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Contract Total", Enabled = false)]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  /// <summary>Total Draft Invoiced Amount for Project</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyInvoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoice Amount", Enabled = false)]
  public virtual Decimal? CuryInvoicedAmount { get; set; }

  /// <summary>Total Actual Amount for Project</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyActualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <summary>
  /// The inclusive tax amount in project currency calculated from the data of <see cref="P:PX.Objects.AR.ARTran.CuryTaxAmt" />
  /// and from the data of <see cref="P:PX.Objects.AR.ARTax.CuryRetainedTaxAmt" />.
  /// </summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyInclTaxAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Amount", Visible = false, Enabled = false)]
  public virtual Decimal? CuryInclTaxAmount { get; set; }

  /// <summary>Total Amount to Invoice for Project</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyAmountToInvoice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount to Invoice")]
  public virtual Decimal? CuryAmountToInvoice { get; set; }

  /// <summary>Total Retained Amount (including Draft) for Project</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyTotalRetainedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Retained Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryTotalRetainedAmount { get; set; }

  /// <summary>Contract Completed % (without Change Orders)</summary>
  [PXDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed (%)", Enabled = false)]
  public virtual Decimal? ContractCompletedPct
  {
    [PXDependsOnFields(new Type[] {typeof (PMProjectRevenueTotal.curyAmount), typeof (PMProjectRevenueTotal.curyInvoicedAmount), typeof (PMBudget.curyActualAmount), typeof (PMBudget.curyInclTaxAmount)})] get
    {
      if (this.CuryAmount.HasValue)
      {
        Decimal? curyAmount = this.CuryAmount;
        Decimal num1 = 0M;
        if (!(curyAmount.GetValueOrDefault() == num1 & curyAmount.HasValue))
        {
          Decimal valueOrDefault1 = this.CuryInvoicedAmount.GetValueOrDefault();
          Decimal? contractCompletedPct = this.CuryActualAmount;
          Decimal valueOrDefault2 = contractCompletedPct.GetValueOrDefault();
          Decimal num2 = valueOrDefault1 + valueOrDefault2;
          contractCompletedPct = this.CuryInclTaxAmount;
          Decimal valueOrDefault3 = contractCompletedPct.GetValueOrDefault();
          Decimal num3 = num2 + valueOrDefault3;
          curyAmount = this.CuryAmount;
          if (curyAmount.HasValue)
            return new Decimal?(num3 / curyAmount.GetValueOrDefault() * 100M);
          contractCompletedPct = new Decimal?();
          return contractCompletedPct;
        }
      }
      return new Decimal?(0M);
    }
  }

  /// <summary>Contract Completed % (with Change Orders)</summary>
  [PXDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed (%)", Enabled = false)]
  public virtual Decimal? ContractCompletedWithCOPct
  {
    [PXDependsOnFields(new Type[] {typeof (PMProjectRevenueTotal.curyRevisedAmount), typeof (PMProjectRevenueTotal.curyInvoicedAmount), typeof (PMBudget.curyActualAmount), typeof (PMBudget.curyInclTaxAmount)})] get
    {
      if (this.CuryRevisedAmount.HasValue)
      {
        Decimal? curyRevisedAmount = this.CuryRevisedAmount;
        Decimal num1 = 0M;
        if (!(curyRevisedAmount.GetValueOrDefault() == num1 & curyRevisedAmount.HasValue))
        {
          Decimal valueOrDefault1 = this.CuryInvoicedAmount.GetValueOrDefault();
          Decimal? completedWithCoPct = this.CuryActualAmount;
          Decimal valueOrDefault2 = completedWithCoPct.GetValueOrDefault();
          Decimal num2 = valueOrDefault1 + valueOrDefault2;
          completedWithCoPct = this.CuryInclTaxAmount;
          Decimal valueOrDefault3 = completedWithCoPct.GetValueOrDefault();
          Decimal num3 = num2 + valueOrDefault3;
          curyRevisedAmount = this.CuryRevisedAmount;
          if (curyRevisedAmount.HasValue)
            return new Decimal?(num3 / curyRevisedAmount.GetValueOrDefault() * 100M);
          completedWithCoPct = new Decimal?();
          return completedWithCoPct;
        }
      }
      return new Decimal?(0M);
    }
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryChangeOrderAmount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyChangeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  /// <exclude />
  public abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMProjectRevenueTotal.projectID>
  {
  }

  /// <exclude />
  public abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.curyAmount>
  {
  }

  /// <exclude />
  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.curyRevisedAmount>
  {
  }

  /// <exclude />
  public abstract class curyInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.curyInvoicedAmount>
  {
  }

  /// <exclude />
  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.curyActualAmount>
  {
  }

  public abstract class curyInclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.curyInclTaxAmount>
  {
  }

  /// <exclude />
  public abstract class curyAmountToInvoice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.curyAmountToInvoice>
  {
  }

  /// <exclude />
  public abstract class curyTotalRetainedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.curyTotalRetainedAmount>
  {
  }

  /// <exclude />
  public abstract class contractCompletedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.contractCompletedPct>
  {
  }

  /// <exclude />
  public abstract class contractCompletedWithCOPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.contractCompletedWithCOPct>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryChangeOrderAmount" />
  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectRevenueTotal.curyChangeOrderAmount>
  {
  }
}
