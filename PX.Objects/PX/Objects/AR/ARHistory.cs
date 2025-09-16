// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An accounts receivable history record, which accumulates a number
/// of important year-to-date and period-to-date amounts (such as sales, debit and credit
/// adjustments, and gains and losses) in base currency. The history is accumulated separately
/// across the following dimensions: branch, GL account, GL subaccount, financial period,
/// and customer. History records are created and updated during the document release
/// process (see <see cref="T:PX.Objects.AR.ARDocumentRelease" /> graph). Various helper projections
/// over this DAC are used in a number of AR inquiry forms and reports, such as Customer
/// Summary (AR401000).
/// </summary>
[PXCacheName("AR History")]
[Serializable]
public class ARHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.GL.Branch" /> to which the history belongs.
  /// This field is a key field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? AccountID { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.GL.Sub" /> to which the history belongs.
  /// This field is a key field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SubID { get; set; }

  /// <summary>
  /// A reference to the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod" /> to which the history belongs.
  /// This field is a key field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod.FinPeriodID" /> field.
  /// </value>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AR.Customer" /> to which the history belongs.
  /// This field is a key field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Customer.CustomerID" /> field.
  /// </value>
  [Customer(IsKey = true, DisplayName = "Customer ID")]
  [PXDefault]
  public virtual int? CustomerID { get; set; }

  /// <summary>
  /// A Boolean field that indicates (if set to <c>true</c>) that the documents
  /// that are related to this record have been deleted (archived).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DetDeleted { get; set; }

  /// <summary>
  /// The period-to-date amount of debit adjustments for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignDrMemos" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdDrAdjustments { get; set; }

  /// <summary>
  /// The period-to-date amount of credit adjustments for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignCrMemos" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCrAdjustments { get; set; }

  /// <summary>
  /// The period-to-date amount of sales for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignSales" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdSales { get; set; }

  /// <summary>
  /// The period-to-date amount of payments for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignPayments" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdPayments { get; set; }

  /// <summary>
  /// The period-to-date amount of discounts for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignDiscTaken" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdDiscounts { get; set; }

  /// <summary>
  /// The year-to-date balance of documents for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignPtd" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdBalance { get; set; }

  /// <summary>
  /// The beginning balance of documents for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinBegBalance { get; set; }

  /// <summary>
  /// The period-to-date cost of goods that are sold during the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The value is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCOGS { get; set; }

  /// <summary>
  /// The period-to-date amount of realized gains or losses for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignRGOL" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdRGOL { get; set; }

  /// <summary>
  /// The period-to-date amount of financial charges for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignFinCharges" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdFinCharges { get; set; }

  /// <summary>
  /// The period-to-date amount of deposits for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignDeposits" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdDeposits { get; set; }

  /// <summary>
  /// The year-to-date amount of deposits for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignDeposits" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdDeposits { get; set; }

  /// <summary>
  /// The period-to-date amount of item discounts for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignPtdItemDiscounts" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdItemDiscounts { get; set; }

  /// <summary>
  /// The period-to-date amount of item discounts for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdRevalued { get; set; }

  /// <summary>
  /// The period-to-date amount of debit adjustments for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignDrMemos" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDrAdjustments { get; set; }

  /// <summary>
  /// The period-to-date amount of credit adjustments for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignCrMemos" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCrAdjustments { get; set; }

  /// <summary>
  /// The period-to-date amount of sales for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignSales" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdSales { get; set; }

  /// <summary>
  /// The period-to-date amount of payments for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignPayments" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdPayments { get; set; }

  /// <summary>
  /// The period-to-date amount of discounts for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignDiscTaken" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDiscounts { get; set; }

  /// <summary>
  /// The year-to-date balance of documents for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignPtd" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdBalance { get; set; }

  /// <summary>
  /// The beginning balance of documents for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranBegBalance { get; set; }

  /// <summary>
  /// The period-to-date cost of goods that are sold during the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The value is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCOGS { get; set; }

  /// <summary>
  /// The period-to-date amount of realized gains or losses for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignRGOL" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdRGOL { get; set; }

  /// <summary>
  /// The period-to-date amount of financial charges for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignFinCharges" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdFinCharges { get; set; }

  /// <summary>
  /// The period-to-date amount of deposits for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignDeposits" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDeposits { get; set; }

  /// <summary>
  /// The year-to-date amount of deposits for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignDeposits" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdDeposits { get; set; }

  /// <summary>
  /// The period-to-date amount of item discounts for the <see cref="P:PX.Objects.AR.ARHistory.FinPeriodID" /> period
  /// (which is related to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field).
  /// The amount is specified in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// The sign of the amount is taken from <see cref="F:PX.Objects.AR.ARReleaseProcess.ARHistBucket.SignPtdItemDiscounts" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdItemDiscounts { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the fields with 'Fin' prefix should be used.
  /// When <c>false</c>, indicates that the fields with 'Tran' prefix should be used.
  /// </summary>
  [PXBool]
  public virtual bool? FinFlag { get; set; } = new bool?(true);

  /// <summary>The field is not used.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? NumberInvoicePaid { get; set; }

  /// <summary>The field is not used.</summary>
  [PXDBShort]
  [PXDefault(0)]
  public virtual short? PaidInvoiceDays { get; set; }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdCrAdjustments" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdCrAdjustments" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdCrAdjustments
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdCrAdjustments), typeof (ARHistory.tranPtdCrAdjustments)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdCrAdjustments : this.FinPtdCrAdjustments;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdCrAdjustments = value;
      else
        this.TranPtdCrAdjustments = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdDrAdjustments" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdDrAdjustments" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdDrAdjustments
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdDrAdjustments), typeof (ARHistory.tranPtdDrAdjustments)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdDrAdjustments : this.FinPtdDrAdjustments;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdDrAdjustments = value;
      else
        this.TranPtdDrAdjustments = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdSales
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdSales), typeof (ARHistory.tranPtdSales)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdSales : this.FinPtdSales;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdSales = value;
      else
        this.TranPtdSales = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdPayments" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdPayments" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdPayments
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdPayments), typeof (ARHistory.tranPtdPayments)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdPayments : this.FinPtdPayments;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdPayments = value;
      else
        this.TranPtdPayments = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdDiscounts" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdDiscounts" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdDiscounts
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdDiscounts), typeof (ARHistory.tranPtdDiscounts)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdDiscounts : this.FinPtdDiscounts;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdDiscounts = value;
      else
        this.TranPtdDiscounts = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinYtdBalance" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranYtdBalance" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? YtdBalance
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finYtdBalance), typeof (ARHistory.tranYtdBalance)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranYtdBalance : this.FinYtdBalance;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinYtdBalance = value;
      else
        this.TranYtdBalance = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinBegBalance" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranBegBalance" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? BegBalance
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finBegBalance), typeof (ARHistory.tranBegBalance)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranBegBalance : this.FinBegBalance;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinBegBalance = value;
      else
        this.TranBegBalance = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdCOGS" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdCOGS" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdCOGS
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdCOGS), typeof (ARHistory.tranPtdCOGS)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdCOGS : this.FinPtdCOGS;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdCOGS = value;
      else
        this.TranPtdCOGS = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdRGOL" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdRGOL" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdRGOL
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdRGOL), typeof (ARHistory.tranPtdRGOL)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdRGOL : this.FinPtdRGOL;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdRGOL = value;
      else
        this.TranPtdRGOL = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdFinCharges" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdFinCharges" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdFinCharges
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdFinCharges), typeof (ARHistory.tranPtdFinCharges)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdFinCharges : this.FinPtdFinCharges;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdFinCharges = value;
      else
        this.TranPtdFinCharges = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdDeposits" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdDeposits" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdDeposits
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdDeposits), typeof (ARHistory.tranPtdDeposits)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdDeposits : this.FinPtdDeposits;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdDeposits = value;
      else
        this.TranPtdDeposits = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinYtdDeposits" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranYtdDeposits" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? YtdDeposits
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finYtdDeposits), typeof (ARHistory.tranYtdDeposits)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranYtdDeposits : this.FinYtdDeposits;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinYtdDeposits = value;
      else
        this.TranYtdDeposits = value;
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>true</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.FinPtdItemDiscounts" /> field.
  /// If <see cref="P:PX.Objects.AR.ARHistory.FinFlag" /> is <c>false</c>, represents the <see cref="P:PX.Objects.AR.ARHistory.TranPtdItemDiscounts" /> field.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? PtdItemDiscounts
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdItemDiscounts), typeof (ARHistory.tranPtdItemDiscounts)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdItemDiscounts : this.FinPtdItemDiscounts;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdItemDiscounts = value;
      else
        this.TranPtdItemDiscounts = value;
    }
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdRetainageWithheld { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? PtdRetainageWithheld
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdRetainageWithheld), typeof (ARHistory.tranPtdRetainageWithheld)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdRetainageWithheld : this.FinPtdRetainageWithheld;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdRetainageWithheld = value;
      else
        this.TranPtdRetainageWithheld = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdRetainageWithheld
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finYtdRetainageWithheld), typeof (ARHistory.tranYtdRetainageWithheld)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranYtdRetainageWithheld : this.FinYtdRetainageWithheld;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinYtdRetainageWithheld = value;
      else
        this.TranYtdRetainageWithheld = value;
    }
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdRetainageReleased { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? PtdRetainageReleased
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finPtdRetainageReleased), typeof (ARHistory.tranPtdRetainageReleased)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranPtdRetainageReleased : this.FinPtdRetainageReleased;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinPtdRetainageReleased = value;
      else
        this.TranPtdRetainageReleased = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdRetainageReleased
  {
    [PXDependsOnFields(new Type[] {typeof (ARHistory.finFlag), typeof (ARHistory.finYtdRetainageReleased), typeof (ARHistory.tranYtdRetainageReleased)})] get
    {
      return !this.FinFlag.GetValueOrDefault() ? this.TranYtdRetainageReleased : this.FinYtdRetainageReleased;
    }
    set
    {
      if (this.FinFlag.GetValueOrDefault())
        this.FinYtdRetainageReleased = value;
      else
        this.TranYtdRetainageReleased = value;
    }
  }

  public class PK : 
    PrimaryKeyOf<ARHistory>.By<ARHistory.branchID, ARHistory.accountID, ARHistory.subID, ARHistory.customerID, ARHistory.finPeriodID>
  {
    public static ARHistory Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (ARHistory) PrimaryKeyOf<ARHistory>.By<ARHistory.branchID, ARHistory.accountID, ARHistory.subID, ARHistory.customerID, ARHistory.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARHistory>.By<ARHistory.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARHistory>.By<ARHistory.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<ARHistory>.By<ARHistory.subID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARHistory>.By<ARHistory.customerID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARHistory.finPeriodID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory.customerID>
  {
  }

  public abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARHistory.detDeleted>
  {
  }

  public abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdDrAdjustments>
  {
  }

  public abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdCrAdjustments>
  {
  }

  public abstract class finPtdSales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory.finPtdSales>
  {
  }

  public abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdPayments>
  {
  }

  public abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdDiscounts>
  {
  }

  public abstract class finYtdBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory.finYtdBalance>
  {
  }

  public abstract class finBegBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory.finBegBalance>
  {
  }

  public abstract class finPtdCOGS : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory.finPtdCOGS>
  {
  }

  public abstract class finPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory.finPtdRGOL>
  {
  }

  public abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdFinCharges>
  {
  }

  public abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdDeposits>
  {
  }

  public abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finYtdDeposits>
  {
  }

  public abstract class finPtdItemDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdItemDiscounts>
  {
  }

  public abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdRevalued>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdCrAdjustments>
  {
  }

  public abstract class tranPtdSales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory.tranPtdSales>
  {
  }

  public abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdPayments>
  {
  }

  public abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdDiscounts>
  {
  }

  public abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranYtdBalance>
  {
  }

  public abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranBegBalance>
  {
  }

  public abstract class tranPtdCOGS : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory.tranPtdCOGS>
  {
  }

  public abstract class tranPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory.tranPtdRGOL>
  {
  }

  public abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdFinCharges>
  {
  }

  public abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdDeposits>
  {
  }

  public abstract class tranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranYtdDeposits>
  {
  }

  public abstract class tranPtdItemDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdItemDiscounts>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARHistory.Tstamp>
  {
  }

  public abstract class finFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARHistory.finFlag>
  {
  }

  public abstract class numberInvoicePaid : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory.numberInvoicePaid>
  {
  }

  public abstract class paidInvoiceDays : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARHistory.paidInvoiceDays>
  {
  }

  public abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdRetainageWithheld>
  {
  }

  public abstract class finYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finYtdRetainageWithheld>
  {
  }

  public abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdRetainageWithheld>
  {
  }

  public abstract class tranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranYtdRetainageWithheld>
  {
  }

  public abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finPtdRetainageReleased>
  {
  }

  public abstract class finYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.finYtdRetainageReleased>
  {
  }

  public abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranPtdRetainageReleased>
  {
  }

  public abstract class tranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory.tranYtdRetainageReleased>
  {
  }
}
