// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterTranPostGLGrouped
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select4<ARRegisterTranPostGL, Aggregate<GroupBy<ARRegisterTranPostGL.docType, GroupBy<ARRegisterTranPostGL.refNbr, GroupBy<ARRegisterTranPostGL.customerID, GroupBy<ARRegisterTranPostGL.finPeriodID, GroupBy<ARRegisterTranPostGL.tranPeriodID, GroupBy<ARRegisterTranPostGL.status, GroupBy<ARRegisterTranPostGL.hold, GroupBy<ARRegisterTranPostGL.voided, GroupBy<ARRegisterTranPostGL.canceled, GroupBy<ARRegisterTranPostGL.curyID, GroupBy<ARRegisterTranPostGL.isMigratedRecord, GroupBy<ARRegisterTranPostGL.curyOrigDocAmt, GroupBy<ARRegisterTranPostGL.curyDocBal, GroupBy<ARRegisterTranPostGL.docBal, GroupBy<ARRegisterTranPostGL.rGOLAmt, Sum<ARRegisterTranPostGL.calcCuryBalance, Sum<ARRegisterTranPostGL.calcCuryBalanceGL, Sum<ARRegisterTranPostGL.calcBalance, Sum<ARRegisterTranPostGL.calcBalanceGL, Sum<ARRegisterTranPostGL.calcCuryDebitARAmt, Sum<ARRegisterTranPostGL.calcDebitARAmt, Sum<ARRegisterTranPostGL.calcCuryCreditARAmt, Sum<ARRegisterTranPostGL.calcCreditARAmt, Sum<ARRegisterTranPostGL.calcRGOL, Sum<ARRegisterTranPostGL.calcCuryRetainageReleased, Sum<ARRegisterTranPostGL.calcRetainageReleased, Sum<ARRegisterTranPostGL.calcCuryRetainageUnreleased, Sum<ARRegisterTranPostGL.calcRetainageUnreleased, Sum<ARRegisterTranPostGL.calcRGOL, Max<ARRegisterTranPostGL.maxFinPeriodID, Max<ARRegisterTranPostGL.maxTranPeriodID, Max<ARRegisterTranPostGL.maxDocDate, Sum<ARRegisterTranPostGL.calcCuryRetainageUnpaidTotal, Sum<ARRegisterTranPostGL.calcRetainageUnpaidTotal>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
public class ARRegisterTranPostGLGrouped : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual string RefNbr { get; set; }

  [Customer(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual int? CustomerID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true, BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual string TranPeriodID { get; set; }

  [PXDBString(1, IsFixed = true, BqlTable = typeof (ARRegisterTranPostGL))]
  [ARDocStatus.List]
  public virtual string Status { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual bool? Hold { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual bool? Voided { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual bool? Canceled { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual bool? PendingPayment { get; set; }

  [PXDBString(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual string CuryID { get; set; }

  [PXDBBool(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CuryDocBal { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? DocBal { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? RGOLAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcCuryBalance { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcCuryBalanceGL { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcBalance { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcBalanceGL { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcCuryDebitARAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcDebitARAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcCuryCreditARAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcCreditARAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcRGOL { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual string MaxFinPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual string MaxTranPeriodID { get; set; }

  [PXDBDate(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual DateTime? MaxDocDate { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcCuryRetainageReleased { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcRetainageReleased { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcCuryRetainageUnreleased { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcRetainageUnreleased { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcCuryRetainageUnpaidTotal { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARRegisterTranPostGL))]
  public virtual Decimal? CalcRetainageUnpaidTotal { get; set; }

  public class PK : 
    PrimaryKeyOf<ARRegisterTranPostGLGrouped>.By<ARRegisterTranPostGLGrouped.docType, ARRegisterTranPostGLGrouped.refNbr>
  {
    public static ARRegisterTranPostGLGrouped Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARRegisterTranPostGLGrouped) PrimaryKeyOf<ARRegisterTranPostGLGrouped>.By<ARRegisterTranPostGLGrouped.docType, ARRegisterTranPostGLGrouped.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.docType>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.refNbr>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.customerID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.finPeriodID>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.tranPeriodID>
  {
  }

  public abstract class status : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterTranPostGLGrouped.hold>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterTranPostGLGrouped.voided>
  {
  }

  public abstract class canceled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.canceled>
  {
  }

  public abstract class pendingPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.pendingPayment>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARRegisterTranPostGLGrouped.curyID>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.isMigratedRecord>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.curyOrigDocAmt>
  {
  }

  public abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.curyDocBal>
  {
  }

  public abstract class docBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.docBal>
  {
  }

  public abstract class rGOLAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.rGOLAmt>
  {
  }

  public abstract class calcCuryBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcCuryBalance>
  {
  }

  public abstract class calcCuryBalanceGL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcCuryBalanceGL>
  {
  }

  public abstract class calcBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcBalance>
  {
  }

  public abstract class calcBalanceGL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcBalanceGL>
  {
  }

  public abstract class calcCuryDebitARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcCuryDebitARAmt>
  {
  }

  public abstract class calcDebitARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcDebitARAmt>
  {
  }

  public abstract class calcCuryCreditARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcCuryCreditARAmt>
  {
  }

  public abstract class calcCreditARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcCreditARAmt>
  {
  }

  public abstract class calcRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcRGOL>
  {
  }

  public abstract class maxFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.maxFinPeriodID>
  {
  }

  public abstract class maxTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.maxTranPeriodID>
  {
  }

  public abstract class maxDocDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.maxDocDate>
  {
  }

  public abstract class calcCuryRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcCuryRetainageReleased>
  {
  }

  public abstract class calcRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcRetainageReleased>
  {
  }

  public abstract class calcCuryRetainageUnreleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcCuryRetainageUnreleased>
  {
  }

  public abstract class calcRetainageUnreleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcRetainageUnreleased>
  {
  }

  public abstract class calcCuryRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcCuryRetainageUnpaidTotal>
  {
  }

  public abstract class calcRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterTranPostGLGrouped.calcRetainageUnpaidTotal>
  {
  }
}
