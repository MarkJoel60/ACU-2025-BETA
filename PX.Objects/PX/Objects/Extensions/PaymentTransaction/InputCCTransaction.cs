// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.InputCCTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Extensions.PaymentTransaction;

[PXHidden]
public class InputCCTransaction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsUnicode = true)]
  [TranTypeList]
  [PXUIField(DisplayName = "Transaction type", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXDefault]
  public virtual string PCTranNumber { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXDefault]
  public virtual string PCTranApiNumber { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXDefault]
  public virtual string CommerceTranNumber { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXDefault]
  public virtual string OrigPCTranNumber { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Proc. Center Auth. Nbr.")]
  public virtual string AuthNumber { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Transaction Date")]
  public virtual System.DateTime? TranDate { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string ExtProfileId { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? NeedValidation { get; set; }

  [PXDecimal(4)]
  [PXDefault]
  public virtual Decimal? Amount { get; set; }

  [PXDate]
  public virtual System.DateTime? ExpirationDate { get; set; }

  /// <summary>Type of a card associated with the document.</summary>
  [PXString]
  [PXUIField(DisplayName = "Card Type")]
  public virtual string CardType { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InputCCTransaction.tranType>
  {
  }

  public abstract class pCTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InputCCTransaction.pCTranNumber>
  {
  }

  public abstract class pCTranApiNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InputCCTransaction.pCTranApiNumber>
  {
  }

  public abstract class commerceTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InputCCTransaction.commerceTranNumber>
  {
  }

  public abstract class origPCTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InputCCTransaction.origPCTranNumber>
  {
  }

  public abstract class authNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InputCCTransaction.authNumber>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InputCCTransaction.tranDate>
  {
  }

  public abstract class extProfileId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InputCCTransaction.extProfileId>
  {
  }

  public abstract class needValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InputCCTransaction.needValidation>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InputCCTransaction.amount>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    InputCCTransaction.expirationDate>
  {
  }

  public abstract class cardType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InputCCTransaction.cardType>
  {
  }
}
