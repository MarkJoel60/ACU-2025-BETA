// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.APTranSigned
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select<PX.Objects.AP.APTran>), Persistent = false)]
[PXHidden]
[Serializable]
public class APTranSigned : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AP.APTran.tranType))]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AP.APTran.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.AP.APTran.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.AP.APTran.pOOrderType))]
  public virtual string POOrderType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APTran.pONbr))]
  public virtual string PONbr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AP.APTran.pOLineNbr))]
  public virtual int? POLineNbr { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AP.APTran.released))]
  public virtual bool? Released { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AP.APTran.drCr, Equal<DrCr.debit>>, PX.Objects.AP.APTran.baseQty>, Minus<PX.Objects.AP.APTran.baseQty>>), typeof (Decimal))]
  [PXQuantity]
  public virtual Decimal? SignedBaseQty { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AP.APTran.drCr, Equal<DrCr.debit>>, PX.Objects.AP.APTran.curyTranAmt>, Minus<PX.Objects.AP.APTran.curyTranAmt>>), typeof (Decimal))]
  [PXCury(typeof (POOrderAPDoc.curyID))]
  public virtual Decimal? SignedCuryTranAmt { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AP.APTran.drCr, Equal<DrCr.debit>>, PX.Objects.AP.APTran.curyRetainageAmt>, Minus<PX.Objects.AP.APTran.curyRetainageAmt>>), typeof (Decimal))]
  [PXCury(typeof (POOrderAPDoc.curyID))]
  public virtual Decimal? SignedCuryRetainageAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.AP.APTran.pOPPVAmt))]
  public virtual Decimal? POPPVAmt { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranSigned.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranSigned.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranSigned.lineNbr>
  {
  }

  public abstract class pOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranSigned.pOOrderType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranSigned.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranSigned.pOLineNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTranSigned.released>
  {
  }

  public abstract class signedBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranSigned.signedBaseQty>
  {
  }

  public abstract class signedCuryTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranSigned.signedCuryTranAmt>
  {
  }

  public abstract class signedCuryRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranSigned.signedCuryRetainageAmt>
  {
  }

  public abstract class pOPPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranSigned.pOPPVAmt>
  {
  }
}
