// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>A header of a bank statement by an account.</summary>
[PXCacheName("Bank Statement")]
[Serializable]
public class CABankTranHeader : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where<Match<Current<AccessInfo.userName>>>>))]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<CABankTranHeader.refNbr, Where<CABankTranHeader.cashAccountID, Equal<Optional<CABankTranHeader.cashAccountID>>, And<CABankTranHeader.tranType, Equal<Optional<CABankTranHeader.tranType>>>>, OrderBy<Desc<CABankTranHeader.refNbr>>>), new Type[] {typeof (CABankTranHeader.refNbr), typeof (CABankTranHeader.cashAccountID), typeof (CABankTranHeader.curyID), typeof (CABankTranHeader.docDate), typeof (CABankTranHeader.endBalanceDate), typeof (CABankTranHeader.curyEndBalance)})]
  [AutoNumber(typeof (CABankTranHeader.tranType), typeof (CABankTranHeader.docDate), new string[] {"S", "I"}, new Type[] {typeof (CASetup.cAStatementNumberingID)})]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (CABankTranType.statement))]
  [CABankTranType.List]
  [PXUIField]
  public virtual string TranType { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Statement Date")]
  public virtual DateTime? DocDate { get; set; }

  [PXDBString(5, InputMask = ">LLLLL", IsUnicode = true)]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CABankTranHeader.cashAccountID>>>>))]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  public virtual string CuryID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (Search<CABankTranHeader.endBalanceDate, Where<CABankTranHeader.cashAccountID, Equal<Current<CABankTranHeader.cashAccountID>>, And<CABankTranHeader.tranType, Equal<Current<CABankTranHeader.tranType>>, And<CABankTranHeader.endBalanceDate, LessEqual<Current<CABankTranHeader.docDate>>, And<Where<Current<CABankTranHeader.refNbr>, IsNull, Or<CABankTranHeader.refNbr, NotEqual<Current<CABankTranHeader.refNbr>>>>>>>>, OrderBy<Desc<CABankTranHeader.startBalanceDate>>>))]
  [PXUIField(DisplayName = "Start Balance Date")]
  public virtual DateTime? StartBalanceDate { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "End Balance Date")]
  public virtual DateTime? EndBalanceDate { get; set; }

  [PXDBCury(typeof (CABankTranHeader.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<CABankTranHeader.curyEndBalance, Where<CABankTranHeader.cashAccountID, Equal<Current<CABankTranHeader.cashAccountID>>, And<CABankTranHeader.tranType, Equal<Current<CABankTranHeader.tranType>>, And<CABankTranHeader.endBalanceDate, LessEqual<Current<CABankTranHeader.docDate>>, And<Where<Current<CABankTranHeader.refNbr>, IsNull, Or<CABankTranHeader.refNbr, NotEqual<Current<CABankTranHeader.refNbr>>>>>>>>, OrderBy<Desc<CABankTranHeader.startBalanceDate>>>))]
  [PXUIField(DisplayName = "Beginning Balance")]
  public virtual Decimal? CuryBegBalance { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (CABankTranHeader.curyID))]
  [PXUIField(DisplayName = "Ending Balance")]
  public virtual Decimal? CuryEndBalance { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (CABankTranHeader.curyID))]
  [PXUIField(DisplayName = "Total Receipts", Enabled = false)]
  public virtual Decimal? CuryDebitsTotal { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (CABankTranHeader.curyID))]
  [PXUIField(DisplayName = "Total Disbursements", Enabled = false)]
  public virtual Decimal? CuryCreditsTotal { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Calculated Balance", Enabled = false)]
  [PXCury(typeof (CABankTranHeader.curyID))]
  public virtual Decimal? CuryDetailsEndBalance
  {
    [PXDependsOnFields(new Type[] {typeof (CABankTranHeader.curyBegBalance), typeof (CABankTranHeader.curyDebitsTotal), typeof (CABankTranHeader.curyCreditsTotal)})] get
    {
      Decimal? curyBegBalance = this.CuryBegBalance;
      Decimal? curyDebitsTotal = this.CuryDebitsTotal;
      Decimal? detailsEndBalance = this.CuryCreditsTotal;
      Decimal? nullable = curyDebitsTotal.HasValue & detailsEndBalance.HasValue ? new Decimal?(curyDebitsTotal.GetValueOrDefault() - detailsEndBalance.GetValueOrDefault()) : new Decimal?();
      if (curyBegBalance.HasValue & nullable.HasValue)
        return new Decimal?(curyBegBalance.GetValueOrDefault() + nullable.GetValueOrDefault());
      detailsEndBalance = new Decimal?();
      return detailsEndBalance;
    }
    set
    {
    }
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Bank Statements Format")]
  public virtual string BankStatementFormat { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Format Verision Nbr")]
  public virtual string FormatVerisionNbr { get; set; }

  [PXDBDate]
  public virtual DateTime? TranMaxDate { get; set; }

  /// <summary>
  /// Specifies (if not set to <c>true</c>) that the bank transactions statement is locked by the external system.
  /// The field is used only if the <see cref="P:PX.Objects.CS.FeaturesSet.AmazonIntegration" /> feature is activated.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Manual Matching Allowed", Enabled = false, Visible = false)]
  public bool? ManualMatchingAllowed { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankTranHeader>.By<CABankTranHeader.cashAccountID, CABankTranHeader.refNbr, CABankTranHeader.tranType>
  {
    public static CABankTranHeader Find(
      PXGraph graph,
      int? cashAccountID,
      string refNbr,
      string tranType,
      PKFindOptions options = 0)
    {
      return (CABankTranHeader) PrimaryKeyOf<CABankTranHeader>.By<CABankTranHeader.cashAccountID, CABankTranHeader.refNbr, CABankTranHeader.tranType>.FindBy(graph, (object) cashAccountID, (object) refNbr, (object) tranType, options);
    }
  }

  public static class FK
  {
    public class CashAcccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CABankTranHeader>.By<CABankTranHeader.cashAccountID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CABankTranHeader>.By<CABankTranHeader.curyID>
    {
    }
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranHeader.cashAccountID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranHeader.refNbr>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranHeader.tranType>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABankTranHeader.docDate>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranHeader.curyID>
  {
  }

  public abstract class startBalanceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranHeader.startBalanceDate>
  {
  }

  public abstract class endBalanceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranHeader.endBalanceDate>
  {
  }

  public abstract class curyBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranHeader.curyBegBalance>
  {
  }

  public abstract class curyEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranHeader.curyEndBalance>
  {
  }

  public abstract class curyDebitsTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranHeader.curyDebitsTotal>
  {
  }

  public abstract class curyCreditsTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranHeader.curyCreditsTotal>
  {
  }

  public abstract class curyDetailsEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranHeader.curyDetailsEndBalance>
  {
  }

  public abstract class bankStatementFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranHeader.bankStatementFormat>
  {
  }

  public abstract class formatVerisionNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranHeader.formatVerisionNbr>
  {
  }

  public abstract class tranMaxDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranHeader.tranMaxDate>
  {
  }

  public abstract class manualMatchingAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTranHeader.manualMatchingAllowed>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTranHeader.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTranHeader.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranHeader.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranHeader.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankTranHeader.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranHeader.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranHeader.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankTranHeader.Tstamp>
  {
  }
}
