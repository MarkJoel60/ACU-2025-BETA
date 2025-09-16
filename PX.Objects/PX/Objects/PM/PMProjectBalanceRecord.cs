// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectBalanceRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <exclude />
[PXCacheName("Project Balance")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMProjectBalanceRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int EmptyInventoryID = 0;
  protected int? _RecordID;
  protected 
  #nullable disable
  string _AccountGroup;
  protected int? _SortOrder;
  protected string _Description;
  protected Decimal? _Performance;

  [PXInt(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Account Group")]
  public virtual string AccountGroup
  {
    get => this._AccountGroup;
    set => this._AccountGroup = value;
  }

  [PXInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.amount))]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public virtual Decimal? CuryAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Original Budgeted Amount in Base Currency")]
  public virtual Decimal? Amount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.draftCOAmount))]
  [PXUIField(DisplayName = "Potential CO Amount")]
  public virtual Decimal? CuryDraftCOAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Potential CO Amount in Base Currency")]
  public virtual Decimal? DraftCOAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.budgetedCOAmount))]
  [PXUIField(DisplayName = "Budgeted CO Amount")]
  public virtual Decimal? CuryBudgetedCOAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Budgeted CO Amount in Base Currency")]
  public virtual Decimal? BudgetedCOAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.revisedAmount))]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public virtual Decimal? RevisedAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.baseActualAmount))]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Hist. Actual Amount in Base Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual Decimal? ActualAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? BaseActualAmount { get; set; }

  /// <summary>
  /// The inclusive tax amount in project currency calculated from data <see cref="!:ARTran.CuryTaxAmt" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.inclTaxAmount))]
  [PXUIField(DisplayName = "Inclusive Tax Amount", Visible = false, Enabled = false)]
  public virtual Decimal? CuryInclTaxAmount { get; set; }

  /// <summary>
  /// The inclusive tax amount in the base currency calculated from data <see cref="!:ARTran.TaxAmt" />.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Inclusive Tax Amount in Base Currency", Visible = false, Enabled = false)]
  public virtual Decimal? InclTaxAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.committedAmount))]
  [PXUIField(DisplayName = "Revised Committed Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Revised Committed Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.committedOpenAmount))]
  [PXUIField(DisplayName = "Committed Open Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedOpenAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Committed Open Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedOpenAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.committedInvoicedAmount))]
  [PXUIField(DisplayName = "Committed Invoiced Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedInvoicedAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Committed Invoiced Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedInvoicedAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.originalCommittedAmount))]
  [PXUIField(DisplayName = "Original Committed Amount")]
  public virtual Decimal? CuryOriginalCommittedAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Original Committed Amount in Base Currency")]
  public virtual Decimal? OriginalCommittedAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.committedCOAmount))]
  [PXUIField(DisplayName = "Committed CO Amount")]
  public virtual Decimal? CuryCommittedCOAmount { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Committed CO Amount in Base Currency")]
  public virtual Decimal? CommittedCOAmount { get; set; }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.actualPlusOpenCommittedAmount))]
  [PXUIField(DisplayName = "Actual + Open Committed Amount", Enabled = false)]
  public virtual Decimal? CuryActualPlusOpenCommittedAmount
  {
    get
    {
      if (!this.CuryActualAmount.HasValue && !this.CuryInclTaxAmount.HasValue && !this.CuryCommittedOpenAmount.HasValue)
        return new Decimal?();
      Decimal valueOrDefault1 = this.CuryActualAmount.GetValueOrDefault();
      Decimal? nullable = this.CuryInclTaxAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryCommittedOpenAmount;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num + valueOrDefault3);
    }
  }

  [PXBaseCury]
  [PXUIField(DisplayName = "Actual + Open Committed Amount in Base Currency", Enabled = false)]
  public virtual Decimal? ActualPlusOpenCommittedAmount
  {
    get
    {
      return this.ActualAmount.HasValue || this.CommittedOpenAmount.HasValue ? new Decimal?(this.ActualAmount.GetValueOrDefault() + this.CommittedOpenAmount.GetValueOrDefault()) : new Decimal?();
    }
  }

  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProjectBalanceRecord.varianceAmount))]
  [PXUIField(DisplayName = "Variance Amount", Enabled = false)]
  public virtual Decimal? CuryVarianceAmount
  {
    get
    {
      return this.CuryRevisedAmount.HasValue || this.CuryActualPlusOpenCommittedAmount.HasValue ? new Decimal?(this.CuryRevisedAmount.GetValueOrDefault() - this.CuryActualPlusOpenCommittedAmount.GetValueOrDefault()) : new Decimal?();
    }
  }

  [PXBaseCury]
  [PXUIField(DisplayName = "Variance Amount", Enabled = false)]
  public virtual Decimal? VarianceAmount
  {
    get
    {
      return this.RevisedAmount.HasValue || this.ActualPlusOpenCommittedAmount.HasValue ? new Decimal?(this.RevisedAmount.GetValueOrDefault() - this.ActualPlusOpenCommittedAmount.GetValueOrDefault()) : new Decimal?();
    }
  }

  [PXDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Performance (%)", Enabled = false)]
  public virtual Decimal? Performance
  {
    get
    {
      if (!this.CuryRevisedAmount.HasValue)
        return new Decimal?();
      Decimal? nullable = this.CuryRevisedAmount;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        return new Decimal?(0.0M);
      nullable = this.CuryActualAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryInclTaxAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num2 = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryRevisedAmount;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num2 / valueOrDefault3 * 100M);
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectBalanceRecord.recordID>
  {
  }

  public abstract class accountGroup : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectBalanceRecord.accountGroup>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectBalanceRecord.sortOrder>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectBalanceRecord.description>
  {
  }

  public abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyAmount>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProjectBalanceRecord.amount>
  {
  }

  public abstract class curyDraftCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyDraftCOAmount>
  {
  }

  public abstract class draftCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.draftCOAmount>
  {
  }

  public abstract class curyBudgetedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyBudgetedCOAmount>
  {
  }

  public abstract class budgetedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.budgetedCOAmount>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyRevisedAmount>
  {
  }

  public abstract class revisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.revisedAmount>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyActualAmount>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.actualAmount>
  {
  }

  public abstract class baseActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.baseActualAmount>
  {
  }

  public abstract class curyInclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyInclTaxAmount>
  {
  }

  public abstract class inclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.inclTaxAmount>
  {
  }

  public abstract class curyCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyCommittedAmount>
  {
  }

  public abstract class committedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.committedAmount>
  {
  }

  public abstract class curyCommittedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyCommittedOpenAmount>
  {
  }

  public abstract class committedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.committedOpenAmount>
  {
  }

  public abstract class curyCommittedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyCommittedInvoicedAmount>
  {
  }

  public abstract class committedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.committedInvoicedAmount>
  {
  }

  public abstract class curyOriginalCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyOriginalCommittedAmount>
  {
  }

  public abstract class originalCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.originalCommittedAmount>
  {
  }

  public abstract class curyCommittedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyCommittedCOAmount>
  {
  }

  public abstract class committedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.committedCOAmount>
  {
  }

  public abstract class curyActualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyActualPlusOpenCommittedAmount>
  {
  }

  public abstract class actualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.actualPlusOpenCommittedAmount>
  {
  }

  public abstract class curyVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.curyVarianceAmount>
  {
  }

  public abstract class varianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.varianceAmount>
  {
  }

  public abstract class performance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBalanceRecord.performance>
  {
  }
}
