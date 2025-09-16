// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranRetainage
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

[PXProjection(typeof (Select5<APTranSigned, InnerJoin<APRegister, On<APTranSigned.refNbr, Equal<APRegister.refNbr>, And<APTranSigned.tranType, Equal<APRegister.docType>>>, InnerJoin<APRegisterOrigRetainage, On<APRegister.origDocType, Equal<APRegisterOrigRetainage.docType>, And<APRegister.origRefNbr, Equal<APRegisterOrigRetainage.refNbr>>>>>, Where<APRegister.released, Equal<True>, And<APRegister.isRetainageDocument, Equal<True>, And2<Where<APRegister.paymentsByLinesAllowed, Equal<True>, Or<APRegister.docType, Equal<APDocType.debitAdj>>>, And<APRegisterOrigRetainage.released, Equal<True>, And<APRegisterOrigRetainage.retainageApply, Equal<True>, And<APRegisterOrigRetainage.openDoc, Equal<True>>>>>>>, Aggregate<Sum<APTranSigned.tranBalSigned, Sum<APTranSigned.origTranAmtSigned, GroupBy<APRegister.origDocType, GroupBy<APRegister.origRefNbr, GroupBy<APTranSigned.origLineNbr>>>>>>>))]
[PXCacheName("AP Tran Retainage")]
[Serializable]
public class APTranRetainage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APRegister.origDocType))]
  public virtual 
  #nullable disable
  string OrigDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "", BqlField = typeof (APRegister.origRefNbr))]
  public virtual string OrigRefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (APTranSigned.origLineNbr))]
  public virtual int? OrigLineNbr { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Mult<APTranSigned.tranBal, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>), typeof (Decimal))]
  public virtual Decimal? TranBalSigned { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Mult<APTranSigned.origTranAmt, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck>>, decimal1, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.voidQuickCheck, APDocType.prepayment>>, decimal_1>>>), typeof (Decimal))]
  public virtual Decimal? OrigTranAmtSigned { get; set; }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranRetainage.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranRetainage.origRefNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranRetainage.origLineNbr>
  {
  }

  public abstract class tranBalSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranRetainage.tranBalSigned>
  {
  }

  public abstract class origTranAmtSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranRetainage.origTranAmtSigned>
  {
  }
}
