// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemStatsA
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Obsolete("This class has been deprecated and will be removed in the later Acumatica versions.")]
[PXHidden]
[Serializable]
public class INItemStatsA : INItemStats
{
  public new abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INItemStatsA.inventoryID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemStatsA.siteID>
  {
  }

  public new abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStatsA.qtyOnHand>
  {
  }

  public new abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStatsA.totalCost>
  {
  }

  public new abstract class minCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStatsA.minCost>
  {
  }

  public new abstract class maxCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStatsA.maxCost>
  {
  }

  public new abstract class lastCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemStatsA.lastCost>
  {
  }

  public new abstract class lastCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemStatsA.lastCostDate>
  {
  }

  public new abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemStatsA.valMethod>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemStatsA.tstamp>
  {
  }
}
