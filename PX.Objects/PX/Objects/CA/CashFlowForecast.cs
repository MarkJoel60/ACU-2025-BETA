// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashFlowForecast
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// This DAC does not have a corresponded table in the database - it's calculated on the fly.
/// </summary>
[PXCacheName("Cash Flow Forecast Record")]
[Serializable]
public class CashFlowForecast : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _AmountDay0 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay1 = new Decimal?(0M);
  protected Decimal? _AmountDay1 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay2 = new Decimal?(0M);
  protected Decimal? _AmountDay2 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay3 = new Decimal?(0M);
  protected Decimal? _AmountDay3 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay4 = new Decimal?(0M);
  protected Decimal? _AmountDay4 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay5 = new Decimal?(0M);
  protected Decimal? _AmountDay5 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay6 = new Decimal?(0M);
  protected Decimal? _AmountDay6 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay7 = new Decimal?(0M);
  protected Decimal? _AmountDay7 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay8 = new Decimal?(0M);
  protected Decimal? _AmountDay8 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay9 = new Decimal?(0M);
  protected Decimal? _AmountDay9 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay10 = new Decimal?(0M);
  protected Decimal? _AmountDay10 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay11 = new Decimal?(0M);
  protected Decimal? _AmountDay11 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay12 = new Decimal?(0M);
  protected Decimal? _AmountDay12 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay13 = new Decimal?(0M);
  protected Decimal? _AmountDay13 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay14 = new Decimal?(0M);
  protected Decimal? _AmountDay14 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay15 = new Decimal?(0M);
  protected Decimal? _AmountDay15 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay16 = new Decimal?(0M);
  protected Decimal? _AmountDay16 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay17 = new Decimal?(0M);
  protected Decimal? _AmountDay17 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay18 = new Decimal?(0M);
  protected Decimal? _AmountDay18 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay19 = new Decimal?(0M);
  protected Decimal? _AmountDay19 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay20 = new Decimal?(0M);
  protected Decimal? _AmountDay20 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay21 = new Decimal?(0M);
  protected Decimal? _AmountDay21 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay22 = new Decimal?(0M);
  protected Decimal? _AmountDay22 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay23 = new Decimal?(0M);
  protected Decimal? _AmountDay23 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay24 = new Decimal?(0M);
  protected Decimal? _AmountDay24 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay25 = new Decimal?(0M);
  protected Decimal? _AmountDay25 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay26 = new Decimal?(0M);
  protected Decimal? _AmountDay26 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay27 = new Decimal?(0M);
  protected Decimal? _AmountDay27 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay28 = new Decimal?(0M);
  protected Decimal? _AmountDay28 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay29 = new Decimal?(0M);
  protected Decimal? _AmountDay29 = new Decimal?(0M);
  protected Decimal? _CuryAmountDay30 = new Decimal?(0M);
  protected Decimal? _AmountDay30 = new Decimal?(0M);

  [PXDBLongIdentity(IsKey = true)]
  public virtual long? RecordID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? StartingDate { get; set; }

  [PXDBInt]
  [PXDefault]
  [CashFlowForecastRecordType.List]
  [PXUIField]
  public virtual int? RecordType { get; set; }

  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where2<Match<Current<AccessInfo.userName>>, And<CashAccount.clearingAccount, Equal<boolFalse>>>>))]
  public virtual int? CashAccountID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXSelector(typeof (BAccountR.bAccountID), SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Customer/Vendor", Enabled = false)]
  public virtual int? BAccountID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<CashForecastTran.tranID>), DescriptionField = typeof (CashForecastTran.tranDesc))]
  [PXUIField]
  public virtual int? EntryID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string AcctCuryID { get; set; }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "CA")]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay0))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 0", Enabled = false)]
  public virtual Decimal? CuryAmountDay0 { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Day 0 Amount", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay0
  {
    get => this._AmountDay0;
    set => this._AmountDay0 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay1))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 1", Enabled = false)]
  public virtual Decimal? CuryAmountDay1
  {
    get => this._CuryAmountDay1;
    set => this._CuryAmountDay1 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 1", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay1
  {
    get => this._AmountDay1;
    set => this._AmountDay1 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay2))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 2", Enabled = false)]
  public virtual Decimal? CuryAmountDay2
  {
    get => this._CuryAmountDay2;
    set => this._CuryAmountDay2 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 2", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay2
  {
    get => this._AmountDay2;
    set => this._AmountDay2 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay3))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 3", Enabled = false)]
  public virtual Decimal? CuryAmountDay3
  {
    get => this._CuryAmountDay3;
    set => this._CuryAmountDay3 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 3", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay3
  {
    get => this._AmountDay3;
    set => this._AmountDay3 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay4))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 4", Enabled = false)]
  public virtual Decimal? CuryAmountDay4
  {
    get => this._CuryAmountDay4;
    set => this._CuryAmountDay4 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 4", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay4
  {
    get => this._AmountDay4;
    set => this._AmountDay4 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay5))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 5", Enabled = false)]
  public virtual Decimal? CuryAmountDay5
  {
    get => this._CuryAmountDay5;
    set => this._CuryAmountDay5 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 5", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay5
  {
    get => this._AmountDay5;
    set => this._AmountDay5 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay6))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 6", Enabled = false)]
  public virtual Decimal? CuryAmountDay6
  {
    get => this._CuryAmountDay6;
    set => this._CuryAmountDay6 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 6", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay6
  {
    get => this._AmountDay6;
    set => this._AmountDay6 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay7))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 7", Enabled = false)]
  public virtual Decimal? CuryAmountDay7
  {
    get => this._CuryAmountDay7;
    set => this._CuryAmountDay7 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 7", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay7
  {
    get => this._AmountDay7;
    set => this._AmountDay7 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay8))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 8", Enabled = false)]
  public virtual Decimal? CuryAmountDay8
  {
    get => this._CuryAmountDay8;
    set => this._CuryAmountDay8 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 9", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay8
  {
    get => this._AmountDay8;
    set => this._AmountDay8 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay9))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 9", Enabled = false)]
  public virtual Decimal? CuryAmountDay9
  {
    get => this._CuryAmountDay9;
    set => this._CuryAmountDay9 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 9", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay9
  {
    get => this._AmountDay9;
    set => this._AmountDay9 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay10))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 10", Enabled = false)]
  public virtual Decimal? CuryAmountDay10
  {
    get => this._CuryAmountDay10;
    set => this._CuryAmountDay10 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Day 0 Amount", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay10
  {
    get => this._AmountDay10;
    set => this._AmountDay10 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay11))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 11", Enabled = false)]
  public virtual Decimal? CuryAmountDay11
  {
    get => this._CuryAmountDay11;
    set => this._CuryAmountDay11 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 11", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay11
  {
    get => this._AmountDay11;
    set => this._AmountDay11 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay12))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 12", Enabled = false)]
  public virtual Decimal? CuryAmountDay12
  {
    get => this._CuryAmountDay12;
    set => this._CuryAmountDay12 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 12", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay12
  {
    get => this._AmountDay12;
    set => this._AmountDay12 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay13))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 13", Enabled = false)]
  public virtual Decimal? CuryAmountDay13
  {
    get => this._CuryAmountDay13;
    set => this._CuryAmountDay13 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 13", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay13
  {
    get => this._AmountDay13;
    set => this._AmountDay13 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay14))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 14", Enabled = false)]
  public virtual Decimal? CuryAmountDay14
  {
    get => this._CuryAmountDay14;
    set => this._CuryAmountDay14 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 14", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay14
  {
    get => this._AmountDay14;
    set => this._AmountDay14 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay15))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 15", Enabled = false)]
  public virtual Decimal? CuryAmountDay15
  {
    get => this._CuryAmountDay15;
    set => this._CuryAmountDay15 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 15", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay15
  {
    get => this._AmountDay15;
    set => this._AmountDay15 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay16))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 16", Enabled = false)]
  public virtual Decimal? CuryAmountDay16
  {
    get => this._CuryAmountDay16;
    set => this._CuryAmountDay16 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 16", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay16
  {
    get => this._AmountDay16;
    set => this._AmountDay16 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay17))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 17", Enabled = false)]
  public virtual Decimal? CuryAmountDay17
  {
    get => this._CuryAmountDay17;
    set => this._CuryAmountDay17 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 17", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay17
  {
    get => this._AmountDay17;
    set => this._AmountDay17 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay18))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 18", Enabled = false)]
  public virtual Decimal? CuryAmountDay18
  {
    get => this._CuryAmountDay18;
    set => this._CuryAmountDay18 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 18", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay18
  {
    get => this._AmountDay18;
    set => this._AmountDay18 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay19))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 19", Enabled = false)]
  public virtual Decimal? CuryAmountDay19
  {
    get => this._CuryAmountDay19;
    set => this._CuryAmountDay19 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 19", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay19
  {
    get => this._AmountDay19;
    set => this._AmountDay19 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay20))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 20", Enabled = false)]
  public virtual Decimal? CuryAmountDay20
  {
    get => this._CuryAmountDay20;
    set => this._CuryAmountDay20 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 20", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay20
  {
    get => this._AmountDay20;
    set => this._AmountDay20 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay21))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 21", Enabled = false)]
  public virtual Decimal? CuryAmountDay21
  {
    get => this._CuryAmountDay21;
    set => this._CuryAmountDay21 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 21", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay21
  {
    get => this._AmountDay21;
    set => this._AmountDay21 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay22))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 22", Enabled = false)]
  public virtual Decimal? CuryAmountDay22
  {
    get => this._CuryAmountDay22;
    set => this._CuryAmountDay22 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 22", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay22
  {
    get => this._AmountDay22;
    set => this._AmountDay22 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay23))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 23", Enabled = false)]
  public virtual Decimal? CuryAmountDay23
  {
    get => this._CuryAmountDay23;
    set => this._CuryAmountDay23 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 23", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay23
  {
    get => this._AmountDay23;
    set => this._AmountDay23 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay24))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 24", Enabled = false)]
  public virtual Decimal? CuryAmountDay24
  {
    get => this._CuryAmountDay24;
    set => this._CuryAmountDay24 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 24", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay24
  {
    get => this._AmountDay24;
    set => this._AmountDay24 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay25))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 25", Enabled = false)]
  public virtual Decimal? CuryAmountDay25
  {
    get => this._CuryAmountDay25;
    set => this._CuryAmountDay25 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 25", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay25
  {
    get => this._AmountDay25;
    set => this._AmountDay25 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay26))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 26", Enabled = false)]
  public virtual Decimal? CuryAmountDay26
  {
    get => this._CuryAmountDay26;
    set => this._CuryAmountDay26 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 26", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay26
  {
    get => this._AmountDay26;
    set => this._AmountDay26 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay27))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 27", Enabled = false)]
  public virtual Decimal? CuryAmountDay27
  {
    get => this._CuryAmountDay27;
    set => this._CuryAmountDay27 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 27", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay27
  {
    get => this._AmountDay27;
    set => this._AmountDay27 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay28))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 28", Enabled = false)]
  public virtual Decimal? CuryAmountDay28
  {
    get => this._CuryAmountDay28;
    set => this._CuryAmountDay28 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 29", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay28
  {
    get => this._AmountDay28;
    set => this._AmountDay28 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay29))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 29", Enabled = false)]
  public virtual Decimal? CuryAmountDay29
  {
    get => this._CuryAmountDay29;
    set => this._CuryAmountDay29 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 29", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay29
  {
    get => this._AmountDay29;
    set => this._AmountDay29 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountDay30))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 30", Enabled = false)]
  public virtual Decimal? CuryAmountDay30
  {
    get => this._CuryAmountDay30;
    set => this._CuryAmountDay30 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Day 30", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay30
  {
    get => this._AmountDay30;
    set => this._AmountDay30 = value;
  }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast.amountSummary))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Summary", Enabled = false)]
  public virtual Decimal? CuryAmountSummary
  {
    get
    {
      if (!this.RecordType.HasValue || this.RecordType.Value == 0)
        return this._CuryAmountDay30;
      Decimal valueOrDefault1 = this.CuryAmountDay0.GetValueOrDefault();
      Decimal? nullable = this.CuryAmountDay1;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryAmountDay2;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      Decimal num2 = num1 + valueOrDefault3;
      nullable = this.CuryAmountDay3;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      Decimal num3 = num2 + valueOrDefault4;
      nullable = this.CuryAmountDay4;
      Decimal valueOrDefault5 = nullable.GetValueOrDefault();
      Decimal num4 = num3 + valueOrDefault5;
      nullable = this.CuryAmountDay5;
      Decimal valueOrDefault6 = nullable.GetValueOrDefault();
      Decimal num5 = num4 + valueOrDefault6;
      nullable = this.CuryAmountDay6;
      Decimal valueOrDefault7 = nullable.GetValueOrDefault();
      Decimal num6 = num5 + valueOrDefault7;
      nullable = this.CuryAmountDay7;
      Decimal valueOrDefault8 = nullable.GetValueOrDefault();
      Decimal num7 = num6 + valueOrDefault8;
      nullable = this.CuryAmountDay8;
      Decimal valueOrDefault9 = nullable.GetValueOrDefault();
      Decimal num8 = num7 + valueOrDefault9;
      nullable = this.CuryAmountDay9;
      Decimal valueOrDefault10 = nullable.GetValueOrDefault();
      Decimal num9 = num8 + valueOrDefault10;
      nullable = this.CuryAmountDay10;
      Decimal valueOrDefault11 = nullable.GetValueOrDefault();
      Decimal num10 = num9 + valueOrDefault11;
      nullable = this.CuryAmountDay11;
      Decimal valueOrDefault12 = nullable.GetValueOrDefault();
      Decimal num11 = num10 + valueOrDefault12;
      nullable = this.CuryAmountDay12;
      Decimal valueOrDefault13 = nullable.GetValueOrDefault();
      Decimal num12 = num11 + valueOrDefault13;
      nullable = this.CuryAmountDay13;
      Decimal valueOrDefault14 = nullable.GetValueOrDefault();
      Decimal num13 = num12 + valueOrDefault14;
      nullable = this.CuryAmountDay14;
      Decimal valueOrDefault15 = nullable.GetValueOrDefault();
      Decimal num14 = num13 + valueOrDefault15;
      nullable = this.CuryAmountDay15;
      Decimal valueOrDefault16 = nullable.GetValueOrDefault();
      Decimal num15 = num14 + valueOrDefault16;
      nullable = this.CuryAmountDay16;
      Decimal valueOrDefault17 = nullable.GetValueOrDefault();
      Decimal num16 = num15 + valueOrDefault17;
      nullable = this.CuryAmountDay17;
      Decimal valueOrDefault18 = nullable.GetValueOrDefault();
      Decimal num17 = num16 + valueOrDefault18;
      nullable = this.CuryAmountDay18;
      Decimal valueOrDefault19 = nullable.GetValueOrDefault();
      Decimal num18 = num17 + valueOrDefault19;
      nullable = this.CuryAmountDay19;
      Decimal valueOrDefault20 = nullable.GetValueOrDefault();
      Decimal num19 = num18 + valueOrDefault20;
      nullable = this.CuryAmountDay20;
      Decimal valueOrDefault21 = nullable.GetValueOrDefault();
      Decimal num20 = num19 + valueOrDefault21;
      nullable = this.CuryAmountDay21;
      Decimal valueOrDefault22 = nullable.GetValueOrDefault();
      Decimal num21 = num20 + valueOrDefault22;
      nullable = this.CuryAmountDay22;
      Decimal valueOrDefault23 = nullable.GetValueOrDefault();
      Decimal num22 = num21 + valueOrDefault23;
      nullable = this.CuryAmountDay23;
      Decimal valueOrDefault24 = nullable.GetValueOrDefault();
      Decimal num23 = num22 + valueOrDefault24;
      nullable = this.CuryAmountDay24;
      Decimal valueOrDefault25 = nullable.GetValueOrDefault();
      Decimal num24 = num23 + valueOrDefault25;
      nullable = this.CuryAmountDay25;
      Decimal valueOrDefault26 = nullable.GetValueOrDefault();
      Decimal num25 = num24 + valueOrDefault26;
      nullable = this.CuryAmountDay26;
      Decimal valueOrDefault27 = nullable.GetValueOrDefault();
      Decimal num26 = num25 + valueOrDefault27;
      nullable = this.CuryAmountDay27;
      Decimal valueOrDefault28 = nullable.GetValueOrDefault();
      Decimal num27 = num26 + valueOrDefault28;
      nullable = this.CuryAmountDay28;
      Decimal valueOrDefault29 = nullable.GetValueOrDefault();
      Decimal num28 = num27 + valueOrDefault29;
      nullable = this.CuryAmountDay29;
      Decimal valueOrDefault30 = nullable.GetValueOrDefault();
      return new Decimal?(num28 + valueOrDefault30);
    }
  }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Summary", Enabled = false, Visible = false)]
  public virtual Decimal? AmountSummary
  {
    get
    {
      if (!this.RecordType.HasValue || this.RecordType.Value == 0)
        return this._AmountDay30;
      Decimal valueOrDefault1 = this.AmountDay0.GetValueOrDefault();
      Decimal? nullable = this.AmountDay1;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 + valueOrDefault2;
      nullable = this.AmountDay2;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      Decimal num2 = num1 + valueOrDefault3;
      nullable = this.AmountDay3;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      Decimal num3 = num2 + valueOrDefault4;
      nullable = this.AmountDay4;
      Decimal valueOrDefault5 = nullable.GetValueOrDefault();
      Decimal num4 = num3 + valueOrDefault5;
      nullable = this.AmountDay5;
      Decimal valueOrDefault6 = nullable.GetValueOrDefault();
      Decimal num5 = num4 + valueOrDefault6;
      nullable = this.AmountDay6;
      Decimal valueOrDefault7 = nullable.GetValueOrDefault();
      Decimal num6 = num5 + valueOrDefault7;
      nullable = this.AmountDay7;
      Decimal valueOrDefault8 = nullable.GetValueOrDefault();
      Decimal num7 = num6 + valueOrDefault8;
      nullable = this.AmountDay8;
      Decimal valueOrDefault9 = nullable.GetValueOrDefault();
      Decimal num8 = num7 + valueOrDefault9;
      nullable = this.AmountDay9;
      Decimal valueOrDefault10 = nullable.GetValueOrDefault();
      Decimal num9 = num8 + valueOrDefault10;
      nullable = this.AmountDay10;
      Decimal valueOrDefault11 = nullable.GetValueOrDefault();
      Decimal num10 = num9 + valueOrDefault11;
      nullable = this.AmountDay11;
      Decimal valueOrDefault12 = nullable.GetValueOrDefault();
      Decimal num11 = num10 + valueOrDefault12;
      nullable = this.AmountDay12;
      Decimal valueOrDefault13 = nullable.GetValueOrDefault();
      Decimal num12 = num11 + valueOrDefault13;
      nullable = this.AmountDay13;
      Decimal valueOrDefault14 = nullable.GetValueOrDefault();
      Decimal num13 = num12 + valueOrDefault14;
      nullable = this.AmountDay14;
      Decimal valueOrDefault15 = nullable.GetValueOrDefault();
      Decimal num14 = num13 + valueOrDefault15;
      nullable = this.AmountDay15;
      Decimal valueOrDefault16 = nullable.GetValueOrDefault();
      Decimal num15 = num14 + valueOrDefault16;
      nullable = this.AmountDay16;
      Decimal valueOrDefault17 = nullable.GetValueOrDefault();
      Decimal num16 = num15 + valueOrDefault17;
      nullable = this.AmountDay17;
      Decimal valueOrDefault18 = nullable.GetValueOrDefault();
      Decimal num17 = num16 + valueOrDefault18;
      nullable = this.AmountDay18;
      Decimal valueOrDefault19 = nullable.GetValueOrDefault();
      Decimal num18 = num17 + valueOrDefault19;
      nullable = this.AmountDay19;
      Decimal valueOrDefault20 = nullable.GetValueOrDefault();
      Decimal num19 = num18 + valueOrDefault20;
      nullable = this.AmountDay20;
      Decimal valueOrDefault21 = nullable.GetValueOrDefault();
      Decimal num20 = num19 + valueOrDefault21;
      nullable = this.AmountDay21;
      Decimal valueOrDefault22 = nullable.GetValueOrDefault();
      Decimal num21 = num20 + valueOrDefault22;
      nullable = this.AmountDay22;
      Decimal valueOrDefault23 = nullable.GetValueOrDefault();
      Decimal num22 = num21 + valueOrDefault23;
      nullable = this.AmountDay23;
      Decimal valueOrDefault24 = nullable.GetValueOrDefault();
      Decimal num23 = num22 + valueOrDefault24;
      nullable = this.AmountDay24;
      Decimal valueOrDefault25 = nullable.GetValueOrDefault();
      Decimal num24 = num23 + valueOrDefault25;
      nullable = this.AmountDay25;
      Decimal valueOrDefault26 = nullable.GetValueOrDefault();
      Decimal num25 = num24 + valueOrDefault26;
      nullable = this.AmountDay26;
      Decimal valueOrDefault27 = nullable.GetValueOrDefault();
      Decimal num26 = num25 + valueOrDefault27;
      nullable = this.AmountDay27;
      Decimal valueOrDefault28 = nullable.GetValueOrDefault();
      Decimal num27 = num26 + valueOrDefault28;
      nullable = this.AmountDay28;
      Decimal valueOrDefault29 = nullable.GetValueOrDefault();
      Decimal num28 = num27 + valueOrDefault29;
      nullable = this.AmountDay29;
      Decimal valueOrDefault30 = nullable.GetValueOrDefault();
      return new Decimal?(num28 + valueOrDefault30);
    }
    set
    {
    }
  }

  public virtual bool IsZero()
  {
    return ((!(this.CuryAmountDay0.GetValueOrDefault() == 0M) || !(this.CuryAmountDay1.GetValueOrDefault() == 0M) || !(this.CuryAmountDay2.GetValueOrDefault() == 0M) || !(this.CuryAmountDay3.GetValueOrDefault() == 0M) || !(this.CuryAmountDay4.GetValueOrDefault() == 0M) || !(this.CuryAmountDay5.GetValueOrDefault() == 0M) || !(this.CuryAmountDay6.GetValueOrDefault() == 0M) || !(this.CuryAmountDay7.GetValueOrDefault() == 0M) || !(this.CuryAmountDay8.GetValueOrDefault() == 0M) || !(this.CuryAmountDay9.GetValueOrDefault() == 0M) || !(this.CuryAmountDay10.GetValueOrDefault() == 0M) || !(this.CuryAmountDay11.GetValueOrDefault() == 0M) || !(this.CuryAmountDay12.GetValueOrDefault() == 0M) || !(this.CuryAmountDay13.GetValueOrDefault() == 0M) || !(this.CuryAmountDay14.GetValueOrDefault() == 0M) || !(this.CuryAmountDay15.GetValueOrDefault() == 0M) || !(this.CuryAmountDay16.GetValueOrDefault() == 0M) || !(this.CuryAmountDay17.GetValueOrDefault() == 0M) || !(this.CuryAmountDay18.GetValueOrDefault() == 0M) || !(this.CuryAmountDay19.GetValueOrDefault() == 0M) || !(this.CuryAmountDay20.GetValueOrDefault() == 0M) || !(this.CuryAmountDay21.GetValueOrDefault() == 0M) || !(this.CuryAmountDay22.GetValueOrDefault() == 0M) || !(this.CuryAmountDay23.GetValueOrDefault() == 0M) || !(this.CuryAmountDay24.GetValueOrDefault() == 0M) || !(this.CuryAmountDay25.GetValueOrDefault() == 0M) || !(this.CuryAmountDay26.GetValueOrDefault() == 0M) || !(this.CuryAmountDay27.GetValueOrDefault() == 0M) || !(this.CuryAmountDay28.GetValueOrDefault() == 0M) ? 0 : (this.CuryAmountDay29.GetValueOrDefault() == 0M ? 1 : 0)) & (!(this.AmountDay0.GetValueOrDefault() == 0M) || !(this.AmountDay1.GetValueOrDefault() == 0M) || !(this.AmountDay2.GetValueOrDefault() == 0M) || !(this.AmountDay3.GetValueOrDefault() == 0M) || !(this.AmountDay4.GetValueOrDefault() == 0M) || !(this.AmountDay5.GetValueOrDefault() == 0M) || !(this.AmountDay6.GetValueOrDefault() == 0M) || !(this.AmountDay7.GetValueOrDefault() == 0M) || !(this.AmountDay8.GetValueOrDefault() == 0M) || !(this.AmountDay9.GetValueOrDefault() == 0M) || !(this.AmountDay10.GetValueOrDefault() == 0M) || !(this.AmountDay11.GetValueOrDefault() == 0M) || !(this.AmountDay12.GetValueOrDefault() == 0M) || !(this.AmountDay13.GetValueOrDefault() == 0M) || !(this.AmountDay14.GetValueOrDefault() == 0M) || !(this.AmountDay15.GetValueOrDefault() == 0M) || !(this.AmountDay16.GetValueOrDefault() == 0M) || !(this.AmountDay17.GetValueOrDefault() == 0M) || !(this.AmountDay18.GetValueOrDefault() == 0M) || !(this.AmountDay19.GetValueOrDefault() == 0M) || !(this.AmountDay20.GetValueOrDefault() == 0M) || !(this.AmountDay21.GetValueOrDefault() == 0M) || !(this.AmountDay22.GetValueOrDefault() == 0M) || !(this.AmountDay23.GetValueOrDefault() == 0M) || !(this.AmountDay24.GetValueOrDefault() == 0M) || !(this.AmountDay25.GetValueOrDefault() == 0M) || !(this.AmountDay26.GetValueOrDefault() == 0M) || !(this.AmountDay27.GetValueOrDefault() == 0M) || !(this.AmountDay28.GetValueOrDefault() == 0M) ? (false ? 1 : 0) : (this.AmountDay29.GetValueOrDefault() == 0M ? 1 : 0))) != 0;
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CashFlowForecast.recordID>
  {
  }

  public abstract class startingDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CashFlowForecast.startingDate>
  {
  }

  public abstract class recordType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashFlowForecast.recordType>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashFlowForecast.cashAccountID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashFlowForecast.bAccountID>
  {
  }

  public abstract class entryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashFlowForecast.entryID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashFlowForecast.curyID>
  {
  }

  public abstract class acctCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashFlowForecast.acctCuryID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CashFlowForecast.curyInfoID>
  {
  }

  public abstract class curyAmountDay0 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay0>
  {
  }

  public abstract class amountDay0 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay0>
  {
  }

  public abstract class curyAmountDay1 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay1>
  {
  }

  public abstract class amountDay1 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay1>
  {
  }

  public abstract class curyAmountDay2 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay2>
  {
  }

  public abstract class amountDay2 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay2>
  {
  }

  public abstract class curyAmountDay3 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay3>
  {
  }

  public abstract class amountDay3 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay3>
  {
  }

  public abstract class curyAmountDay4 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay4>
  {
  }

  public abstract class amountDay4 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay4>
  {
  }

  public abstract class curyAmountDay5 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay5>
  {
  }

  public abstract class amountDay5 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay5>
  {
  }

  public abstract class curyAmountDay6 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay6>
  {
  }

  public abstract class amountDay6 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay6>
  {
  }

  public abstract class curyAmountDay7 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay7>
  {
  }

  public abstract class amountDay7 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay7>
  {
  }

  public abstract class curyAmountDay8 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay8>
  {
  }

  public abstract class amountDay8 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay8>
  {
  }

  public abstract class curyAmountDay9 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay9>
  {
  }

  public abstract class amountDay9 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast.amountDay9>
  {
  }

  public abstract class curyAmountDay10 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay10>
  {
  }

  public abstract class amountDay10 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay10>
  {
  }

  public abstract class curyAmountDay11 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay11>
  {
  }

  public abstract class amountDay11 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay11>
  {
  }

  public abstract class curyAmountDay12 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay12>
  {
  }

  public abstract class amountDay12 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay12>
  {
  }

  public abstract class curyAmountDay13 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay13>
  {
  }

  public abstract class amountDay13 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay13>
  {
  }

  public abstract class curyAmountDay14 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay14>
  {
  }

  public abstract class amountDay14 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay14>
  {
  }

  public abstract class curyAmountDay15 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay15>
  {
  }

  public abstract class amountDay15 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay15>
  {
  }

  public abstract class curyAmountDay16 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay16>
  {
  }

  public abstract class amountDay16 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay16>
  {
  }

  public abstract class curyAmountDay17 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay17>
  {
  }

  public abstract class amountDay17 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay17>
  {
  }

  public abstract class curyAmountDay18 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay18>
  {
  }

  public abstract class amountDay18 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay18>
  {
  }

  public abstract class curyAmountDay19 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay19>
  {
  }

  public abstract class amountDay19 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay19>
  {
  }

  public abstract class curyAmountDay20 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay20>
  {
  }

  public abstract class amountDay20 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay20>
  {
  }

  public abstract class curyAmountDay21 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay21>
  {
  }

  public abstract class amountDay21 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay21>
  {
  }

  public abstract class curyAmountDay22 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay22>
  {
  }

  public abstract class amountDay22 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay22>
  {
  }

  public abstract class curyAmountDay23 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay23>
  {
  }

  public abstract class amountDay23 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay23>
  {
  }

  public abstract class curyAmountDay24 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay24>
  {
  }

  public abstract class amountDay24 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay24>
  {
  }

  public abstract class curyAmountDay25 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay25>
  {
  }

  public abstract class amountDay25 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay25>
  {
  }

  public abstract class curyAmountDay26 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay26>
  {
  }

  public abstract class amountDay26 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay26>
  {
  }

  public abstract class curyAmountDay27 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay27>
  {
  }

  public abstract class amountDay27 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay27>
  {
  }

  public abstract class curyAmountDay28 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay28>
  {
  }

  public abstract class amountDay28 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay28>
  {
  }

  public abstract class curyAmountDay29 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay29>
  {
  }

  public abstract class amountDay29 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay29>
  {
  }

  public abstract class curyAmountDay30 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountDay30>
  {
  }

  public abstract class amountDay30 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountDay30>
  {
  }

  public abstract class curyAmountSummary : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.curyAmountSummary>
  {
  }

  public abstract class amountSummary : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast.amountSummary>
  {
  }
}
