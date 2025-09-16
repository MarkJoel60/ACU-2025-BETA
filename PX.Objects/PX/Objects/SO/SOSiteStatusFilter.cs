// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSiteStatusFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
[PXCacheName("Inventory Lookup Filter")]
public class SOSiteStatusFilter : INSiteStatusFilter
{
  [PXUIField(DisplayName = "Warehouse")]
  [Site]
  [PXDefault]
  public override int? SiteID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Selection Mode")]
  [SOAddItemMode.List]
  public virtual int? Mode { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Sold Since")]
  public virtual DateTime? HistoryDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Default<SOSiteStatusFilter.mode>))]
  [PXUIField(DisplayName = "Show Drop-Ship Sales")]
  public virtual bool? DropShipSales { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  public virtual 
  #nullable disable
  string Behavior { get; set; }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField(DisplayName = "Ship-To Location")]
  public virtual int? CustomerLocationID { get; set; }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOSiteStatusFilter.siteID>
  {
  }

  public new abstract class inventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSiteStatusFilter.inventory>
  {
  }

  public abstract class mode : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOSiteStatusFilter.mode>
  {
  }

  public abstract class historyDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSiteStatusFilter.historyDate>
  {
  }

  public abstract class dropShipSales : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSiteStatusFilter.dropShipSales>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOSiteStatusFilter.behavior>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOSiteStatusFilter.customerLocationID>
  {
  }
}
