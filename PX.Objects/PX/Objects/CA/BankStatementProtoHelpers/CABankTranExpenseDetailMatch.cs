// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementProtoHelpers.CABankTranExpenseDetailMatch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.CA.BankStatementProtoHelpers;

[PXHidden]
public class CABankTranExpenseDetailMatch : 
  CABankTranDocumentMatch,
  IBqlTable,
  IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField(DisplayName = "Matched")]
  public virtual bool? IsMatched { get; set; }

  /// <summary>The user-friendly unique identifier of the receipt.</summary>
  [PXString(15, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXUIField]
  [EPExpenceReceiptSelector]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Doc. Date")]
  public virtual DateTime? DocDate { get; set; }

  [PXUIField(DisplayName = "Paid With")]
  [PXString(8)]
  [LabelList(typeof (EPExpenseClaimDetails.paidWith.Labels))]
  public string PaidWith { get; set; }

  [PXString(20, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Card Number")]
  public virtual string CardNumber { get; set; }

  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  [PXCurrency(typeof (CABankTranExpenseDetailMatch.curyInfoID), typeof (CABankTranExpenseDetailMatch.docAmt))]
  [PXUIField(DisplayName = "Amount in Claim Curr.")]
  public virtual Decimal? CuryDocAmt { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? DocAmt { get; set; }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Claim Currency")]
  public virtual string ClaimCuryID { get; set; }

  [PXCurrency(typeof (CABankTranExpenseDetailMatch.curyInfoID), typeof (CABankTranExpenseDetailMatch.docAmtDiff))]
  [PXUIField(DisplayName = "Amount Difference")]
  public virtual Decimal? CuryDocAmtDiff { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? DocAmtDiff { get; set; }

  [PXInt]
  [PXDefault]
  [PXSelector(typeof (BAccountR.bAccountID), SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Employee")]
  public virtual int? ReferenceID { get; set; }

  [PXUIField(DisplayName = "Employee Name")]
  [PXString(60, IsUnicode = true)]
  public virtual string ReferenceName { get; set; }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  public override string GetDocumentKey() => $"EPECD{this.RefNbr}";

  public override void BuildDocRef(CABankTranDocRef docRef)
  {
    docRef.DocModule = "EP";
    docRef.DocType = "ECD";
    docRef.DocRefNbr = this.RefNbr;
    docRef.ReferenceID = this.ReferenceID;
  }

  public abstract class isMatched : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.isMatched>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.refNbr>
  {
  }

  public abstract class extRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.extRefNbr>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.docDate>
  {
  }

  public abstract class paidWith : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranExpenseDetailMatch.paidWith>
  {
  }

  public abstract class cardNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.cardNumber>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.curyInfoID>
  {
  }

  public abstract class curyDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.curyDocAmt>
  {
  }

  public abstract class docAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.docAmt>
  {
  }

  public abstract class claimCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.claimCuryID>
  {
  }

  public abstract class curyDocAmtDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.curyDocAmtDiff>
  {
  }

  public abstract class docAmtDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.docAmtDiff>
  {
  }

  public abstract class referenceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.referenceID>
  {
  }

  public abstract class referenceName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.referenceName>
  {
  }

  public abstract class tranDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranExpenseDetailMatch.tranDesc>
  {
  }
}
