// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistorySumCreditSales
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select<ARHistorySumForPeriod>))]
[PXCacheName("AR History Sum Credit Sales")]
public class ARHistorySumCreditSales : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlTable = typeof (ARHistorySumForPeriod))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistorySumForPeriod))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdBalance { get; set; }

  [PXDecimal(4, BqlTable = typeof (ARHistorySumForPeriod))]
  [PXDBCalced(typeof (Add<ARHistorySumForPeriod.tranPtdSales, Sub<ARHistorySumForPeriod.tranPtdDrAdjustments, ARHistorySumForPeriod.tranPtdCrAdjustments>>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CreditSales { get; set; }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistorySumCreditSales.finPeriodID>
  {
  }

  public abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistorySumCreditSales.tranYtdBalance>
  {
  }

  public abstract class creditSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistorySumCreditSales.creditSales>
  {
  }
}
