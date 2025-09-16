// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCostStatusTransitLineSummary
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
[PXProjection(typeof (Select4<INCostStatus, Where<INCostStatus.costSiteID, Equal<SiteAnyAttribute.transitSiteID>>, Aggregate<GroupBy<INCostStatus.inventoryID, GroupBy<INCostStatus.costSiteID, Sum<INCostStatus.qtyOnHand, Sum<INCostStatus.totalCost>>>>>>))]
[Serializable]
public class INCostStatusTransitLineSummary : INCostStatus
{
  [Site(true, BqlField = typeof (INCostStatus.siteID))]
  [PXDefault]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  public new abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    INCostStatusTransitLineSummary.inventoryID>
  {
  }

  public new abstract class costSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCostStatusTransitLineSummary.costSiteID>
  {
  }

  public new abstract class siteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCostStatusTransitLineSummary.siteID>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCostStatusTransitLineSummary.qtyOnHand>
  {
  }

  public new abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCostStatusTransitLineSummary.totalCost>
  {
  }
}
