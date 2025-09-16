// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranApplications
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

[PXProjection(typeof (Select2<ARRegister, InnerJoin<ARTran, On<ARTran.tranType, Equal<ARRegister.docType>, And<ARTran.refNbr, Equal<ARRegister.refNbr>>>, InnerJoin<ARTranPostGL, On<ARTranPostGL.docType, Equal<ARTran.tranType>, And<ARTranPostGL.refNbr, Equal<ARTran.refNbr>, And<ARTranPostGL.lineNbr, Equal<ARTran.lineNbr>>>>>>, Where<ARRegister.paymentsByLinesAllowed, Equal<True>, And<ARRegister.released, Equal<True>>>>), Persistent = false)]
[PXHidden]
public class ARTranApplications : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlField = typeof (ARTran.tranType))]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(IsKey = true, BqlField = typeof (ARTran.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (ARTran.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.curyOrigTranAmt))]
  public virtual Decimal? CuryOrigTranAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.origTranAmt))]
  public virtual Decimal? OrigTranAmt { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARTranPostGL.curyBalanceAmt, IBqlDecimal>.Multiply<BqlOperand<ARTranPostGL.balanceSign, IBqlShort>.Multiply<decimal_1>>), typeof (Decimal))]
  public virtual Decimal? CuryAppBalanceSigned { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARTranPostGL.balanceAmt, IBqlDecimal>.Multiply<BqlOperand<ARTranPostGL.balanceSign, IBqlShort>.Multiply<decimal_1>>), typeof (Decimal))]
  public virtual Decimal? AppBalanceSigned { get; set; }

  public class PK : 
    PrimaryKeyOf<ARTranApplications>.By<ARTranApplications.tranType, ARTranApplications.refNbr, ARTranApplications.lineNbr>
  {
    public static ARTranApplications Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ARTranApplications) PrimaryKeyOf<ARTranApplications>.By<ARTranApplications.tranType, ARTranApplications.refNbr, ARTranApplications.lineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranApplications.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranApplications.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranApplications.lineNbr>
  {
  }

  public abstract class curyOrigTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranApplications.curyOrigTranAmt>
  {
  }

  public abstract class origTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranApplications.origTranAmt>
  {
  }

  public abstract class curyAppBalanceSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranApplications.curyAppBalanceSigned>
  {
  }

  public abstract class appBalanceSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranApplications.appBalanceSigned>
  {
  }
}
