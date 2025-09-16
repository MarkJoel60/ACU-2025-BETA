// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <exclude />
[Serializable]
public class CAEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [CashAccount]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CAEnqFilter.cashAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? EndDate { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Last Start Date", Visible = false)]
  public virtual DateTime? LastStartDate { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "End Date", Visible = false)]
  public virtual DateTime? LastEndDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Summary")]
  public virtual bool? ShowSummary { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Include Unreleased")]
  public virtual bool? IncludeUnreleased { get; set; }

  [PXDecimal(typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CAEnqFilter.curyID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beginning Balance", Enabled = false)]
  public virtual Decimal? BegBal { get; set; }

  [PXDecimal(typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CAEnqFilter.curyID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Acct. Credit Total", Enabled = false)]
  public virtual Decimal? CreditTotal { get; set; }

  [PXDecimal(typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CAEnqFilter.curyID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Acct. Debit Total", Enabled = false)]
  public virtual Decimal? DebitTotal { get; set; }

  [PXDecimal(typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CAEnqFilter.curyID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Balance", Enabled = false)]
  public virtual Decimal? EndBal { get; set; }

  [PXDecimal(typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CAEnqFilter.curyID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beginning Balance", Enabled = false)]
  public virtual Decimal? BegClearedBal { get; set; }

  [PXDecimal(typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CAEnqFilter.curyID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Acct. Credit Total", Enabled = false)]
  public virtual Decimal? CreditClearedTotal { get; set; }

  [PXDecimal(typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CAEnqFilter.curyID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Acct. Debit Total", Enabled = false)]
  public virtual Decimal? DebitClearedTotal { get; set; }

  [PXDecimal(typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CAEnqFilter.curyID>>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Balance", Enabled = false)]
  public virtual Decimal? EndClearedBal { get; set; }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAEnqFilter.cashAccountID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAEnqFilter.curyID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAEnqFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAEnqFilter.endDate>
  {
  }

  public abstract class lastStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAEnqFilter.lastStartDate>
  {
  }

  public abstract class lastEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CAEnqFilter.lastEndDate>
  {
  }

  public abstract class showSummary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAEnqFilter.showSummary>
  {
  }

  public abstract class includeUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CAEnqFilter.includeUnreleased>
  {
  }

  public abstract class begBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAEnqFilter.begBal>
  {
  }

  public abstract class creditTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAEnqFilter.creditTotal>
  {
  }

  public abstract class debitTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAEnqFilter.debitTotal>
  {
  }

  public abstract class endBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAEnqFilter.endBal>
  {
  }

  public abstract class begClearedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAEnqFilter.begClearedBal>
  {
  }

  public abstract class creditClearedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAEnqFilter.creditClearedTotal>
  {
  }

  public abstract class debitClearedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAEnqFilter.debitClearedTotal>
  {
  }

  public abstract class endClearedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAEnqFilter.endClearedBal>
  {
  }
}
