// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashFlowForecast2
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

[PXCacheName("Cash Flow Forecast Record")]
[PXHidden]
[Serializable]
public class CashFlowForecast2 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _CuryAmountDay = new Decimal?(0M);
  protected Decimal? _AmountDay = new Decimal?(0M);

  [PXDBLongIdentity(IsKey = true)]
  public virtual long? RecordID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? TranDate { get; set; }

  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where2<Match<Current<AccessInfo.userName>>, And<CashAccount.clearingAccount, Equal<boolFalse>>>>))]
  public virtual int? CashAccountID { get; set; }

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

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public virtual int? RecordType { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXSelector(typeof (BAccountR.bAccountID), SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Business Account", Enabled = false)]
  public virtual int? BAccountID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<CashForecastTran.tranID>), DescriptionField = typeof (CashForecastTran.tranDesc))]
  [PXUIField]
  public virtual int? EntryID { get; set; }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "CA")]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (CashFlowForecast.curyInfoID), typeof (CashFlowForecast2.amountDay))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? CuryAmountDay
  {
    get => this._CuryAmountDay;
    set => this._CuryAmountDay = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false, Visible = false)]
  public virtual Decimal? AmountDay
  {
    get => this._AmountDay;
    set => this._AmountDay = value;
  }

  public CashFlowForecast2 Copy(DateTime startDate, PX.Objects.AP.APInvoice src)
  {
    this.BAccountID = src.VendorID;
    this.CashAccountID = src.PayAccountID;
    DateTime? dueDate = src.DueDate;
    DateTime dateTime = startDate;
    this.TranDate = (dueDate.HasValue ? (dueDate.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) != 0 ? src.DueDate : new DateTime?(startDate);
    this.RecordType = new int?(-1);
    return this;
  }

  public CashFlowForecast2 Copy(DateTime startDate, PX.Objects.AP.APPayment src)
  {
    this.BAccountID = src.VendorID;
    this.CashAccountID = src.CashAccountID;
    DateTime? adjDate = src.AdjDate;
    DateTime dateTime = startDate;
    this.TranDate = (adjDate.HasValue ? (adjDate.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) != 0 ? src.AdjDate : new DateTime?(startDate);
    this.RecordType = new int?(-1);
    this.CuryID = src.CuryID;
    this.AcctCuryID = src.CuryID;
    return this;
  }

  public CashFlowForecast2 Copy(DateTime startDate, PX.Objects.AR.ARInvoice src)
  {
    this.BAccountID = src.CustomerID;
    this.CashAccountID = src.CashAccountID;
    DateTime? dueDate = src.DueDate;
    DateTime dateTime = startDate;
    this.TranDate = (dueDate.HasValue ? (dueDate.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) != 0 ? src.DueDate : new DateTime?(startDate);
    this.RecordType = new int?(1);
    return this;
  }

  public CashFlowForecast2 Copy(DateTime startDate, PX.Objects.AR.ARPayment src)
  {
    this.BAccountID = src.CustomerID;
    this.CashAccountID = src.CashAccountID;
    DateTime? adjDate = src.AdjDate;
    DateTime dateTime = startDate;
    this.TranDate = (adjDate.HasValue ? (adjDate.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) != 0 ? src.AdjDate : new DateTime?(startDate);
    this.CuryID = src.CuryID;
    this.AcctCuryID = src.CuryID;
    this.RecordType = new int?(1);
    return this;
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CashFlowForecast2.recordID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CashFlowForecast2.tranDate>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashFlowForecast2.cashAccountID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashFlowForecast2.curyID>
  {
  }

  public abstract class acctCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashFlowForecast2.acctCuryID>
  {
  }

  public abstract class recordType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashFlowForecast2.recordType>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashFlowForecast2.bAccountID>
  {
  }

  public abstract class entryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashFlowForecast2.entryID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CashFlowForecast2.curyInfoID>
  {
  }

  public abstract class curyAmountDay : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashFlowForecast2.curyAmountDay>
  {
  }

  public abstract class amountDay : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashFlowForecast2.amountDay>
  {
  }
}
