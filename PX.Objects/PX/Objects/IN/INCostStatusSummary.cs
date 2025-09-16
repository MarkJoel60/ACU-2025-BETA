// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCostStatusSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[Obsolete("This class has been deprecated and will be removed in the later Acumatica versions.")]
[PXCacheName("IN Cost Status Summary")]
[PXProjection(typeof (Select4<INCostStatus, Where<boolTrue, Equal<boolTrue>>, Aggregate<GroupBy<INCostStatus.accountID, GroupBy<INCostStatus.subID, GroupBy<INCostStatus.inventoryID, GroupBy<INCostStatus.costSubItemID, GroupBy<INCostStatus.costSiteID, GroupBy<INCostStatus.lotSerialNbr, Sum<INCostStatus.qtyOnHand, Sum<INCostStatus.totalCost>>>>>>>>>>))]
[Serializable]
public class INCostStatusSummary : INCostStatus
{
  [Site(true, BqlField = typeof (INCostStatus.siteID))]
  [PXDefault]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  public new abstract class accountID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INCostStatusSummary.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatusSummary.subID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCostStatusSummary.inventoryID>
  {
  }

  public new abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCostStatusSummary.costSubItemID>
  {
  }

  public new abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatusSummary.costSiteID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatusSummary.siteID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCostStatusSummary.lotSerialNbr>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCostStatusSummary.qtyOnHand>
  {
  }

  public new abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCostStatusSummary.totalCost>
  {
  }
}
