// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranByCashAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// Contains totals of unprocessed Bank Transactions grouped by Cash Account
/// </summary>
[PXProjection(typeof (Select4<CABankTranSplitDebitCredit, Aggregate<GroupBy<CABankTranSplitDebitCredit.cashAccountID, Sum<CABankTranSplitDebitCredit.curyDebitAmount, Sum<CABankTranSplitDebitCredit.curyCreditAmount, Sum<CABankTranSplitDebitCredit.debitNumber, Sum<CABankTranSplitDebitCredit.creditNumber, Sum<CABankTranSplitDebitCredit.unprocessedNumber, Sum<CABankTranSplitDebitCredit.matchedNumber>>>>>>>>>))]
[PXCacheName("Bank Transactions by Cash Account")]
public class CABankTranByCashAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (CABankTranSplitDebitCredit.cashAccountID))]
  public virtual int? CashAccountID { get; set; }

  [PXDBDecimal(BqlField = typeof (CABankTranSplitDebitCredit.curyDebitAmount))]
  [PXUIField(DisplayName = "Unprocessed Receipts")]
  public virtual Decimal? CuryDebitAmount { get; set; }

  [PXDBDecimal(BqlField = typeof (CABankTranSplitDebitCredit.curyCreditAmount))]
  [PXUIField(DisplayName = "Unprocessed Disb.")]
  public virtual Decimal? CuryCreditAmount { get; set; }

  [PXDBInt(BqlField = typeof (CABankTranSplitDebitCredit.debitNumber))]
  [PXUIField(DisplayName = "Receipt Count")]
  public virtual int? DebitNumber { get; set; }

  [PXDBInt(BqlField = typeof (CABankTranSplitDebitCredit.creditNumber))]
  [PXUIField(DisplayName = "Disbursement Count")]
  public virtual int? CreditNumber { get; set; }

  [PXDBInt(BqlField = typeof (CABankTranSplitDebitCredit.unprocessedNumber))]
  [PXUIField(DisplayName = "Unmatched Count")]
  public virtual int? UnprocessedNumber { get; set; }

  [PXDBInt(BqlField = typeof (CABankTranSplitDebitCredit.matchedNumber))]
  [PXUIField(DisplayName = "Matched Count")]
  public virtual int? MatchedNumber { get; set; }

  public class PK : PrimaryKeyOf<
  #nullable disable
  CABankTranByCashAccount>.By<CABankTranByCashAccount.cashAccountID>
  {
    public static CABankTranByCashAccount Find(
      PXGraph graph,
      int? cashAccountID,
      PKFindOptions options = 0)
    {
      return (CABankTranByCashAccount) PrimaryKeyOf<CABankTranByCashAccount>.By<CABankTranByCashAccount.cashAccountID>.FindBy(graph, (object) cashAccountID, options);
    }
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTranByCashAccount.cashAccountID>
  {
  }

  public abstract class curyDebitAmount : IBqlField, IBqlOperand
  {
  }

  public abstract class curyCreditAmount : IBqlField, IBqlOperand
  {
  }

  public abstract class debitNumber : IBqlField, IBqlOperand
  {
  }

  public abstract class creditNumber : IBqlField, IBqlOperand
  {
  }

  public abstract class unprocessedNumber : IBqlField, IBqlOperand
  {
  }

  public abstract class matchedNumber : IBqlField, IBqlOperand
  {
  }
}
