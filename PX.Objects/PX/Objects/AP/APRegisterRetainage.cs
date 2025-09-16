// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegisterRetainage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select5<APRegisterSigned, InnerJoin<APRegisterOrigRetainage, On<APRegisterSigned.origDocType, Equal<APRegisterOrigRetainage.docType>, And<APRegisterSigned.origRefNbr, Equal<APRegisterOrigRetainage.refNbr>>>>, Where<APRegisterSigned.released, Equal<True>, And<APRegisterSigned.isRetainageDocument, Equal<True>, And<APRegisterSigned.paymentsByLinesAllowed, Equal<False>, And<APRegisterOrigRetainage.released, Equal<True>, And<APRegisterOrigRetainage.retainageApply, Equal<True>, And<APRegisterOrigRetainage.openDoc, Equal<True>>>>>>>, Aggregate<Sum<APRegisterSigned.docBalSigned, Sum<APRegisterSigned.origDocAmtSigned, GroupBy<APRegisterSigned.origDocType, GroupBy<APRegisterSigned.origRefNbr>>>>>>))]
[PXCacheName("APRegister Retainage")]
[Serializable]
public class APRegisterRetainage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APRegisterSigned.origDocType))]
  public virtual 
  #nullable disable
  string OrigDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "", BqlField = typeof (APRegisterSigned.origRefNbr))]
  public virtual string OrigRefNbr { get; set; }

  [PXBaseCury]
  [PXDependsOnFields(new System.Type[] {typeof (APRegisterRetainage.origDocType), typeof (APRegisterRetainage.docBal)})]
  public virtual Decimal? DocBalSigned
  {
    get
    {
      if (!("ADR" == this.OrigDocType))
        return this.DocBal;
      Decimal? docBal = this.DocBal;
      return !docBal.HasValue ? new Decimal?() : new Decimal?(-docBal.GetValueOrDefault());
    }
  }

  [PXBaseCury]
  [PXDBCalced(typeof (Mult<APRegisterSigned.docBal, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>), typeof (Decimal))]
  public virtual Decimal? DocBal { get; set; }

  [PXBaseCury]
  [PXDependsOnFields(new System.Type[] {typeof (APRegisterRetainage.origDocType), typeof (APRegisterRetainage.origDocAmt)})]
  public virtual Decimal? OrigDocAmtSigned
  {
    get
    {
      if (!("ADR" == this.OrigDocType))
        return this.OrigDocAmt;
      Decimal? origDocAmt = this.OrigDocAmt;
      return !origDocAmt.HasValue ? new Decimal?() : new Decimal?(-origDocAmt.GetValueOrDefault());
    }
  }

  [PXBaseCury]
  [PXDBCalced(typeof (Mult<APRegisterSigned.origDocAmt, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck>>, decimal1, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.voidQuickCheck, APDocType.prepayment>>, decimal_1>>>), typeof (Decimal))]
  public virtual Decimal? OrigDocAmt { get; set; }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterRetainage.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterRetainage.origRefNbr>
  {
  }

  public abstract class docBalSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterRetainage.docBalSigned>
  {
  }

  public abstract class docBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterRetainage.docBalSigned>
  {
  }

  public abstract class origDocAmtSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterRetainage.origDocAmtSigned>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterRetainage.origDocAmt>
  {
  }
}
