// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterTranPostGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select2<ARRegister, InnerJoin<ARTranPostGL, On<ARTranPostGL.customerID, Equal<ARRegister.customerID>, And<ARTranPostGL.docType, Equal<ARRegister.docType>, And<ARTranPostGL.refNbr, Equal<ARRegister.refNbr>>>>>>), Persistent = false)]
[PXHidden]
public class ARRegisterTranPostGL : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlTable = typeof (ARRegister))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARRegister))]
  public virtual string RefNbr { get; set; }

  [Customer(BqlTable = typeof (ARRegister))]
  public virtual int? CustomerID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (ARRegister))]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true, BqlTable = typeof (ARRegister))]
  public virtual string TranPeriodID { get; set; }

  [PXDBString(1, IsFixed = true, BqlTable = typeof (ARRegister))]
  [ARDocStatus.List]
  public virtual string Status { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegister))]
  public virtual bool? Hold { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegister))]
  public virtual bool? Voided { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegister))]
  public virtual bool? Canceled { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegister))]
  public virtual bool? PendingPayment { get; set; }

  [PXDBString(BqlTable = typeof (ARRegister))]
  public virtual string CuryID { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegister))]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXDBShort(BqlTable = typeof (ARTranPostGL))]
  public virtual short? BalanceSign { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  [ARTranPost.type.List]
  public virtual string Type { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegister))]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegister))]
  public virtual Decimal? CuryDocBal { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegister))]
  public virtual Decimal? DocBal { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARRegister))]
  public virtual Decimal? RGOLAmt { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARTranPostGL.curyBalanceAmt, IBqlDecimal>.Multiply<ARTranPostGL.balanceSign>), typeof (Decimal))]
  public virtual Decimal? CalcCuryBalance { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARTranPostGL.curyDebitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.curyCreditARAmt>), typeof (Decimal))]
  public virtual Decimal? CalcCuryBalanceGL { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARTranPostGL.balanceAmt, IBqlDecimal>.Multiply<ARTranPostGL.balanceSign>), typeof (Decimal))]
  public virtual Decimal? CalcBalance { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARTranPostGL.debitARAmt, IBqlDecimal>.Subtract<ARTranPostGL.creditARAmt>), typeof (Decimal))]
  public virtual Decimal? CalcBalanceGL { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (IIf<Where<BqlOperand<ARTranPostGL.accountID, IBqlInt>.IsEqual<ARRegister.aRAccountID>>, BqlOperand<ARTranPostGL.curyBalanceAmt, IBqlDecimal>.Multiply<ARTranPostGL.balanceSign>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CalcCuryDebitARAmt { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (IIf<Where<BqlOperand<ARTranPostGL.accountID, IBqlInt>.IsEqual<ARRegister.aRAccountID>>, BqlOperand<ARTranPostGL.balanceAmt, IBqlDecimal>.Multiply<ARTranPostGL.balanceSign>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CalcDebitARAmt { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (IIf<Where<BqlOperand<ARTranPostGL.accountID, IBqlInt>.IsNotEqual<ARRegister.aRAccountID>>, BqlOperand<ARTranPostGL.curyBalanceAmt, IBqlDecimal>.Multiply<ARTranPostGL.balanceSign>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CalcCuryCreditARAmt { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (IIf<Where<BqlOperand<ARTranPostGL.accountID, IBqlInt>.IsNotEqual<ARRegister.aRAccountID>>, BqlOperand<ARTranPostGL.balanceAmt, IBqlDecimal>.Multiply<ARTranPostGL.balanceSign>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CalcCreditARAmt { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (IIf<Where<ARTranPostGL.type, Equal<ARTranPost.type.application>>, ARTranPostGL.rGOLAmt, Zero>), typeof (Decimal))]
  public virtual Decimal? CalcRGOL { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsDBField = false)]
  [PXDBCalced(typeof (IIf<Where<ARTranPostGL.type, Equal<ARTranPost.type.rgol>>, Null, ARTranPostGL.finPeriodID>), typeof (string))]
  public virtual string MaxFinPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsDBField = false)]
  [PXDBCalced(typeof (IIf<Where<ARTranPostGL.type, Equal<ARTranPost.type.rgol>>, Null, ARTranPostGL.tranPeriodID>), typeof (string))]
  public virtual string MaxTranPeriodID { get; set; }

  [PXDBDate(BqlField = typeof (ARTranPostGL.docDate))]
  public virtual DateTime? MaxDocDate { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (ARTranPostGL.curyRetainageReleasedAmt), typeof (Decimal))]
  public virtual Decimal? CalcCuryRetainageReleased { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (ARTranPostGL.retainageReleasedAmt), typeof (Decimal))]
  public virtual Decimal? CalcRetainageReleased { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (ARTranPostGL.curyRetainageUnreleasedAmt), typeof (Decimal))]
  public virtual Decimal? CalcCuryRetainageUnreleased { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (ARTranPostGL.retainageUnreleasedAmt), typeof (Decimal))]
  public virtual Decimal? CalcRetainageUnreleased { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlFunction<Add<ARTranPostGL.curyRetainageReleasedAmt, ARTranPostGL.curyRetainageUnreleasedAmt>, IBqlDecimal>.Subtract<ARTranPostGL.curyRetainagePaidTotal>), typeof (Decimal))]
  public virtual Decimal? CalcCuryRetainageUnpaidTotal { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlFunction<Add<ARTranPostGL.retainageReleasedAmt, ARTranPostGL.retainageUnreleasedAmt>, IBqlDecimal>.Subtract<ARTranPostGL.retainagePaidTotal>), typeof (Decimal))]
  public virtual Decimal? CalcRetainageUnpaidTotal { get; set; }

  public class PK : 
    PrimaryKeyOf<ARRegisterTranPostGL>.By<ARRegisterTranPostGL.docType, ARRegisterTranPostGL.refNbr>
  {
    public static ARRegisterTranPostGL Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARRegisterTranPostGL) PrimaryKeyOf<ARRegisterTranPostGL>.By<ARRegisterTranPostGL.docType, ARRegisterTranPostGL.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterTranPostGL.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterTranPostGL.refNbr>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterTranPostGL.customerID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGL.finPeriodID>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGL.tranPeriodID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterTranPostGL.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterTranPostGL.hold>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterTranPostGL.voided>
  {
  }

  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterTranPostGL.canceled>
  {
  }

  public abstract class pendingPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterTranPostGL.pendingPayment>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARRegisterTranPostGL.curyID>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterTranPostGL.isMigratedRecord>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ARRegisterTranPostGL.balanceSign>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterTranPostGL.type>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.curyOrigDocAmt>
  {
  }

  public abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterTranPostGL.docBal>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterTranPostGL.rGOLAmt>
  {
  }

  public abstract class calcCuryBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcCuryBalance>
  {
  }

  public abstract class calcCuryBalanceGL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcCuryBalanceGL>
  {
  }

  public abstract class calcBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcBalance>
  {
  }

  public abstract class calcBalanceGL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcBalanceGL>
  {
  }

  public abstract class calcCuryDebitARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcCuryDebitARAmt>
  {
  }

  public abstract class calcDebitARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcDebitARAmt>
  {
  }

  public abstract class calcCuryCreditARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcCuryCreditARAmt>
  {
  }

  public abstract class calcCreditARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcCreditARAmt>
  {
  }

  public abstract class calcRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterTranPostGL.calcRGOL>
  {
  }

  public abstract class maxFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGL.maxFinPeriodID>
  {
  }

  public abstract class maxTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGL.maxTranPeriodID>
  {
  }

  public abstract class maxDocDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGL.maxDocDate>
  {
  }

  public abstract class calcCuryRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcCuryRetainageReleased>
  {
  }

  public abstract class calcRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcRetainageReleased>
  {
  }

  public abstract class calcCuryRetainageUnreleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcCuryRetainageUnreleased>
  {
  }

  public abstract class calcRetainageUnreleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcRetainageUnreleased>
  {
  }

  public abstract class calcCuryRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcCuryRetainageUnpaidTotal>
  {
  }

  public abstract class calcRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGL.calcRetainageUnpaidTotal>
  {
  }
}
