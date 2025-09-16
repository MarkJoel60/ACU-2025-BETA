// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POSiteStatusFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[Serializable]
public class POSiteStatusFilter : INSiteStatusFilter
{
  protected int? _VendorID;

  [PXUIField(DisplayName = "Warehouse")]
  [Site]
  [InterBranchRestrictor(typeof (Where2<SameOrganizationBranch<INSite.branchID, Current<POOrder.branchID>>, Or<Current<POOrder.orderType>, Equal<POOrderType.standardBlanket>>>))]
  [PXDefault(typeof (PX.Objects.IN.INRegister.siteID))]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Only Vendor's Items")]
  public override bool? OnlyAvailable
  {
    get => this._OnlyAvailable;
    set => this._OnlyAvailable = value;
  }

  [PXDBInt]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  public new abstract class siteID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  POSiteStatusFilter.siteID>
  {
  }

  public new abstract class onlyAvailable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POSiteStatusFilter.onlyAvailable>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POSiteStatusFilter.vendorID>
  {
  }
}
