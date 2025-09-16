// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementProtoHelpers.CABankTranDocRef
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA.BankStatementHelpers;
using System;

#nullable enable
namespace PX.Objects.CA.BankStatementProtoHelpers;

[PXHidden]
[Serializable]
public class CABankTranDocRef : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IBankMatchRelevance
{
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBInt]
  public virtual int? TranID { get; set; }

  /// <summary>The transaction date.</summary>
  [PXDBDate]
  public virtual DateTime? TranDate { get; set; }

  [PXDBLong(IsKey = true)]
  public virtual long? CATranID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXStringList(new string[] {"AP", "AR"}, new string[] {"AP", "AR"})]
  public virtual 
  #nullable disable
  string DocModule { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = "")]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual string DocRefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  public virtual int? ReferenceID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Cash Account ID", Visible = false)]
  [PXDefault]
  public virtual int? CashAccountID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Match Relevance", Enabled = false)]
  public virtual Decimal? MatchRelevance { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTranAmt { get; set; }

  /// <summary>
  /// The cash discount amount in the currency of the document.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? CuryDiscAmt { get; set; }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the credit terms of source document
  /// </summary>
  [PXDBDate]
  public virtual DateTime? DiscDate { get; set; }

  public void Copy(CATran aSrc)
  {
    this.CashAccountID = aSrc.CashAccountID;
    this.CATranID = aSrc.TranID;
    this.CuryTranAmt = aSrc.CuryTranAmt;
    this.ReferenceID = aSrc.ReferenceID;
  }

  public void Copy(CABankTran aSrc)
  {
    this.TranID = aSrc.TranID;
    this.TranDate = aSrc.TranDate;
    this.CashAccountID = aSrc.CashAccountID;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranDocRef.selected>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDocRef.tranID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABankTranDocRef.tranDate>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTranDocRef.cATranID>
  {
  }

  public abstract class docModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranDocRef.docModule>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranDocRef.docType>
  {
  }

  public abstract class docRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranDocRef.docRefNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDocRef.referenceID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDocRef.cashAccountID>
  {
  }

  public abstract class matchRelevance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranDocRef.matchRelevance>
  {
  }

  public abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranDocRef.curyTranAmt>
  {
  }

  /// <summary>
  /// The cash discount amount in the currency of the document.
  /// </summary>
  public abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranDocRef.curyDiscAmt>
  {
  }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the credit terms of source document
  /// </summary>
  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABankTranDocRef.discDate>
  {
  }
}
