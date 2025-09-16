// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementProtoHelpers.CABankTranInvoiceMatch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA.BankStatementProtoHelpers;

/// <exclude />
[Serializable]
public class CABankTranInvoiceMatch : CABankTranDocumentMatch, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField(DisplayName = "Matched")]
  public virtual bool? IsMatched { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Best Match")]
  public virtual bool? IsBestMatch { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  [BatchModule.List]
  [PXUIField(DisplayName = "Module")]
  public virtual 
  #nullable disable
  string OrigModule { get; set; }

  [PXDBString(3, IsFixed = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Type")]
  [CAAPARTranType.ListByModuleRestricted(typeof (CABankTranInvoiceMatch.origModule))]
  public virtual string OrigTranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string OrigRefNbr { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [CashAccount]
  public virtual int? CashAccountID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Doc. Date")]
  public virtual DateTime? TranDate { get; set; }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the credit terms of source document
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Discount Date")]
  public virtual DateTime? DiscDate { get; set; }

  [PXDefault]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField(DisplayName = "Disb. / Receipt")]
  public virtual string DrCr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public virtual int? ReferenceID { get; set; }

  [PXUIField]
  [PXString(60, IsUnicode = true)]
  public virtual string ReferenceCD { get; set; }

  [PXUIField]
  [PXString(60, IsUnicode = true)]
  public virtual string ReferenceName { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string TranDesc { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (CABankTranInvoiceMatch.cashAccountID), typeof (Selector<CABankTranInvoiceMatch.cashAccountID, PX.Objects.CA.CashAccount.branchID>), null, null, null, true, false, null, typeof (CABankTranInvoiceMatch.tranPeriodID), null, true, true)]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string FinPeriodID { get; set; }

  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released { get; set; }

  [PXDBCurrency(typeof (CATran.curyInfoID), typeof (CATran.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran. Amount")]
  public virtual Decimal? TranAmt { get; set; }

  /// <summary>
  /// The cash discount amount of the document.
  /// Given in the <see cref="P:PX.Objects.CA.BankStatementProtoHelpers.CABankTranInvoiceMatch.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBDecimal]
  [PXUIField]
  public virtual Decimal? CuryDiscAmt { get; set; }

  /// <summary>
  /// The cash discount amount of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Doc. Date")]
  public virtual DateTime? DueDate { get; set; }

  public void Copy(PX.Objects.AR.ARInvoice aSrc)
  {
    this.CashAccountID = new int?();
    this.TranDate = aSrc.DocDate;
    this.TranDesc = aSrc.DocDesc;
    this.ReferenceID = aSrc.CustomerID;
    this.DrCr = aSrc.DrCr == "C" ? "D" : "C";
    this.FinPeriodID = aSrc.FinPeriodID;
    this.ExtRefNbr = aSrc.InvoiceNbr;
    this.CuryInfoID = aSrc.CuryInfoID;
    this.CuryID = aSrc.CuryID;
    this.CuryTranAmt = aSrc.CuryDocBal;
    this.TranAmt = aSrc.DocBal;
    this.CuryDiscAmt = aSrc.CuryDiscBal;
    this.DiscAmt = aSrc.DiscBal;
    this.DiscDate = aSrc.DiscDate;
    this.DueDate = aSrc.DueDate;
    this.Released = aSrc.Released;
    this.OrigTranType = aSrc.DocType;
    this.OrigRefNbr = aSrc.RefNbr;
    this.OrigModule = "AR";
    this.BranchID = aSrc.BranchID;
  }

  public void Copy(PX.Objects.AP.APInvoice aSrc)
  {
    this.CashAccountID = new int?();
    this.TranDate = aSrc.DocDate;
    this.TranDesc = aSrc.DocDesc;
    this.ReferenceID = aSrc.VendorID;
    this.DrCr = aSrc.DrCr == "C" ? "D" : "C";
    this.FinPeriodID = aSrc.FinPeriodID;
    this.ExtRefNbr = aSrc.InvoiceNbr;
    this.CuryInfoID = aSrc.CuryInfoID;
    this.CuryID = aSrc.CuryID;
    this.CuryTranAmt = aSrc.CuryDocBal;
    this.TranAmt = aSrc.DocBal;
    this.CuryDiscAmt = aSrc.CuryDiscBal;
    this.DiscAmt = aSrc.DiscBal;
    this.DiscDate = aSrc.DiscDate;
    this.DueDate = aSrc.DueDate;
    this.Released = aSrc.Released;
    this.OrigTranType = aSrc.DocType;
    this.OrigRefNbr = aSrc.RefNbr;
    this.OrigModule = "AP";
    this.BranchID = aSrc.BranchID;
  }

  public void Copy(PX.Objects.CA.Light.ARInvoice aSrc)
  {
    this.CashAccountID = new int?();
    this.TranDate = aSrc.DocDate;
    this.TranDesc = aSrc.DocDesc;
    this.ReferenceID = aSrc.CustomerID;
    this.DrCr = aSrc.DrCr == "C" ? "D" : "C";
    this.FinPeriodID = aSrc.FinPeriodID;
    this.ExtRefNbr = aSrc.InvoiceNbr;
    this.CuryInfoID = aSrc.CuryInfoID;
    this.CuryID = aSrc.CuryID;
    this.CuryTranAmt = aSrc.CuryDocBal;
    this.TranAmt = aSrc.DocBal;
    this.CuryDiscAmt = aSrc.CuryDiscBal;
    this.DiscAmt = aSrc.DiscBal;
    this.DiscDate = aSrc.DiscDate;
    this.DueDate = aSrc.DueDate;
    this.Released = aSrc.Released;
    this.OrigTranType = aSrc.DocType;
    this.OrigRefNbr = aSrc.RefNbr;
    this.OrigModule = "AR";
    this.BranchID = aSrc.BranchID;
  }

  public void Copy(PX.Objects.CA.Light.APInvoice aSrc)
  {
    this.CashAccountID = new int?();
    this.TranDate = aSrc.DocDate;
    this.TranDesc = aSrc.DocDesc;
    this.ReferenceID = aSrc.VendorID;
    this.DrCr = aSrc.DrCr == "C" ? "D" : "C";
    this.FinPeriodID = aSrc.FinPeriodID;
    this.ExtRefNbr = aSrc.InvoiceNbr;
    this.CuryInfoID = aSrc.CuryInfoID;
    this.CuryID = aSrc.CuryID;
    this.CuryTranAmt = aSrc.CuryDocBal;
    this.TranAmt = aSrc.DocBal;
    this.CuryDiscAmt = aSrc.CuryDiscBal;
    this.DiscAmt = aSrc.DiscBal;
    this.DiscDate = aSrc.DiscDate;
    this.DueDate = aSrc.DueDate;
    this.Released = aSrc.Released;
    this.OrigTranType = aSrc.DocType;
    this.OrigRefNbr = aSrc.RefNbr;
    this.OrigModule = "AP";
    this.BranchID = aSrc.BranchID;
  }

  public void Copy(CATran aSrc)
  {
  }

  public override string GetDocumentKey() => this.OrigModule + this.OrigTranType + this.OrigRefNbr;

  public override void BuildDocRef(CABankTranDocRef docRef)
  {
    docRef.DocModule = this.OrigModule;
    docRef.DocType = this.OrigTranType;
    docRef.DocRefNbr = this.OrigRefNbr;
    docRef.ReferenceID = this.ReferenceID;
    docRef.CuryTranAmt = this.CuryTranAmt;
    docRef.CuryDiscAmt = this.CuryDiscAmt;
    docRef.DiscDate = this.DiscDate;
  }

  public abstract class isMatched : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranInvoiceMatch.isMatched>
  {
  }

  public abstract class isBestMatch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTranInvoiceMatch.isBestMatch>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranInvoiceMatch.branchID>
  {
  }

  public abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranInvoiceMatch.origModule>
  {
  }

  public abstract class origTranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranInvoiceMatch.origTranType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranInvoiceMatch.origRefNbr>
  {
  }

  public abstract class extRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranInvoiceMatch.extRefNbr>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTranInvoiceMatch.cashAccountID>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranInvoiceMatch.tranDate>
  {
  }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the credit terms of source document
  /// </summary>
  public abstract class discDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranInvoiceMatch.discDate>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranInvoiceMatch.drCr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranInvoiceMatch.referenceID>
  {
  }

  public abstract class referenceCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranInvoiceMatch.referenceCD>
  {
  }

  public abstract class referenceName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranInvoiceMatch.referenceName>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranInvoiceMatch.tranDesc>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranInvoiceMatch.tranPeriodID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranInvoiceMatch.finPeriodID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTranInvoiceMatch.curyInfoID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranInvoiceMatch.curyID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranInvoiceMatch.released>
  {
  }

  public abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranInvoiceMatch.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranInvoiceMatch.tranAmt>
  {
  }

  /// <summary>
  /// The cash discount amount of the document.
  /// Given in the <see cref="T:PX.Objects.CA.BankStatementProtoHelpers.CABankTranInvoiceMatch.curyID">currency of the document</see>.
  /// </summary>
  public abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranInvoiceMatch.curyDiscAmt>
  {
  }

  /// <summary>
  /// The cash discount amount of the document.
  /// Given in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency of the branch</see>.
  /// </summary>
  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranInvoiceMatch.discAmt>
  {
  }

  public abstract class dueDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranInvoiceMatch.dueDate>
  {
  }
}
