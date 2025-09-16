// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterRetainageReleased
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select<ARRegister, Where<ARRegister.isRetainageDocument, Equal<True>, And<ARRegister.released, Equal<True>>>>), Persistent = false)]
[PXHidden]
public class ARRegisterRetainageReleased : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlField = typeof (ARRegister.origDocType))]
  public virtual 
  #nullable disable
  string OrigDocType { get; set; }

  [PXDBString(IsKey = true, BqlField = typeof (ARRegister.origRefNbr))]
  public virtual string OrigRefNbr { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, decimal1>, decimal_1>), typeof (Decimal))]
  public virtual Decimal? BalanceSign { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARRegister.curyOrigDocAmt, IBqlDecimal>.Multiply<ARRegisterRetainageReleased.balanceSign>), typeof (Decimal))]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARRegister.origDocAmt, IBqlDecimal>.Multiply<ARRegisterRetainageReleased.balanceSign>), typeof (Decimal))]
  public virtual Decimal? OrigDocAmt { get; set; }

  public class PK : 
    PrimaryKeyOf<ARRegisterRetainageReleased>.By<ARRegisterRetainageReleased.origDocType, ARRegisterRetainageReleased.origRefNbr>
  {
    public static ARRegisterRetainageReleased Find(
      PXGraph graph,
      string origDocType,
      string origRefNbr,
      PKFindOptions options = 0)
    {
      return (ARRegisterRetainageReleased) PrimaryKeyOf<ARRegisterRetainageReleased>.By<ARRegisterRetainageReleased.origDocType, ARRegisterRetainageReleased.origRefNbr>.FindBy(graph, (object) origDocType, (object) origRefNbr, options);
    }
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterRetainageReleased.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterRetainageReleased.origRefNbr>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterRetainageReleased.balanceSign>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterRetainageReleased.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterRetainageReleased.origDocAmt>
  {
  }
}
