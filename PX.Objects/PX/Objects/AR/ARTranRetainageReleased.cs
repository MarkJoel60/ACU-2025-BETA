// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranRetainageReleased
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select2<ARRegister, InnerJoin<ARTran, On<ARTran.tranType, Equal<ARRegister.docType>, And<ARTran.refNbr, Equal<ARRegister.refNbr>>>>, Where<ARRegister.paymentsByLinesAllowed, Equal<True>, And<ARRegister.released, Equal<True>, And<Where<ARRegister.isRetainageDocument, Equal<True>, Or<ARRegister.isRetainageReversing, Equal<True>>>>>>>), Persistent = false)]
[PXHidden]
public class ARTranRetainageReleased : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  [PXDBCalced(typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.isRetainageDocument, NotEqual<True>>>>>.And<BqlOperand<ARRegister.isRetainageReversing, IBqlBool>.IsEqual<True>>, ARRegister.origDocType, ARTran.origDocType>), typeof (string))]
  public virtual 
  #nullable disable
  string OrigDocType { get; set; }

  [PXString(IsKey = true)]
  [PXDBCalced(typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.isRetainageDocument, NotEqual<True>>>>>.And<BqlOperand<ARRegister.isRetainageReversing, IBqlBool>.IsEqual<True>>, ARRegister.origRefNbr, ARTran.origRefNbr>), typeof (string))]
  public virtual string OrigRefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (ARTran.origLineNbr))]
  public virtual int? OrigLineNbr { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, decimal1>, decimal_1>), typeof (Decimal))]
  public virtual Decimal? BalanceSign { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Mult<IIf<BqlOperand<ARRegister.isRetainageDocument, IBqlBool>.IsEqual<True>, ARTran.curyOrigTranAmt, Minus<BqlOperand<ARTran.curyRetainageAmt, IBqlDecimal>.Add<ARTran.curyRetainedTaxAmt>>>, ARTranRetainageReleased.balanceSign>), typeof (Decimal))]
  public virtual Decimal? CuryRetainageReleased { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Mult<IIf<BqlOperand<ARRegister.isRetainageDocument, IBqlBool>.IsEqual<True>, ARTran.origTranAmt, Minus<BqlOperand<ARTran.retainageAmt, IBqlDecimal>.Add<ARTran.retainedTaxAmt>>>, ARTranRetainageReleased.balanceSign>), typeof (Decimal))]
  public virtual Decimal? RetainageReleased { get; set; }

  public class PK : 
    PrimaryKeyOf<ARTranRetainageReleased>.By<ARTranRetainageReleased.origDocType, ARTranRetainageReleased.origRefNbr, ARTranRetainageReleased.origLineNbr>
  {
    public static ARTranRetainageReleased Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ARTranRetainageReleased) PrimaryKeyOf<ARTranRetainageReleased>.By<ARTranRetainageReleased.origDocType, ARTranRetainageReleased.origRefNbr, ARTranRetainageReleased.origLineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranRetainageReleased.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranRetainageReleased.origRefNbr>
  {
  }

  public abstract class origLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARTranRetainageReleased.origLineNbr>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranRetainageReleased.balanceSign>
  {
  }

  public abstract class curyRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranRetainageReleased.curyRetainageReleased>
  {
  }

  public abstract class retainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranRetainageReleased.retainageReleased>
  {
  }
}
