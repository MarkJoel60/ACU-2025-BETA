// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranSplitDebitCredit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// Contains unprocessed Bank Transactions with the Amount split into Debit and Credit depending on DrCr value
/// </summary>
[PXProjection(typeof (Select<CABankTran, Where<CABankTran.processed, Equal<boolFalse>, And<CABankTran.tranType, Equal<CABankTranType.statement>>>>))]
[PXHidden]
public class CABankTranSplitDebitCredit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The cash account specified on the bank statement for which you want to upload bank transactions.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (CABankTran.cashAccountID))]
  public virtual int? CashAccountID { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (IIf<Where<CABankTran.drCr, Equal<CADrCr.cADebit>>, CABankTran.curyTranAmt, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryDebitAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (IIf<Where<CABankTran.drCr, Equal<CADrCr.cACredit>>, Minus<CABankTran.curyTranAmt>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryCreditAmount { get; set; }

  [PXInt]
  [PXDBCalced(typeof (IIf<Where<CABankTran.drCr, Equal<CADrCr.cADebit>>, int1, int0>), typeof (int))]
  public virtual int? DebitNumber { get; set; }

  [PXInt]
  [PXDBCalced(typeof (IIf<Where<CABankTran.drCr, Equal<CADrCr.cACredit>>, int1, int0>), typeof (int))]
  public virtual int? CreditNumber { get; set; }

  [PXInt]
  [PXDBCalced(typeof (IIf<Where<CABankTran.documentMatched, NotEqual<True>, And<CABankTran.processed, NotEqual<True>>>, int1, int0>), typeof (int))]
  public virtual int? UnprocessedNumber { get; set; }

  [PXInt]
  [PXDBCalced(typeof (IIf<Where<CABankTran.documentMatched, Equal<True>>, int1, int0>), typeof (int))]
  public virtual int? MatchedNumber { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankTranSplitDebitCredit>.By<CABankTranSplitDebitCredit.cashAccountID>
  {
    public static CABankTranSplitDebitCredit Find(
      PXGraph graph,
      int? cashAccountID,
      PKFindOptions options = 0)
    {
      return (CABankTranSplitDebitCredit) PrimaryKeyOf<CABankTranSplitDebitCredit>.By<CABankTranSplitDebitCredit.cashAccountID>.FindBy(graph, (object) cashAccountID, options);
    }
  }

  public abstract class cashAccountID : IBqlField, IBqlOperand
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
