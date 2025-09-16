// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistorySumForPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select4<ARHistory, Aggregate<GroupBy<ARHistory.finPeriodID, Sum<ARHistory.tranYtdBalance, Sum<ARHistory.tranPtdSales, Sum<ARHistory.tranPtdDrAdjustments, Sum<ARHistory.tranPtdCrAdjustments>>>>>>>))]
[PXCacheName("AR History Sum For Period")]
public class ARHistorySumForPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlTable = typeof (ARHistory))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdBalance { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCrAdjustments { get; set; }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistorySumForPeriod.finPeriodID>
  {
  }

  public abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistorySumForPeriod.tranYtdBalance>
  {
  }

  public abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistorySumForPeriod.tranPtdSales>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistorySumForPeriod.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistorySumForPeriod.tranPtdCrAdjustments>
  {
  }
}
