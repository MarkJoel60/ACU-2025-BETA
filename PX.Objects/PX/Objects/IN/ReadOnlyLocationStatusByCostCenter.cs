// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ReadOnlyLocationStatusByCostCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
public class ReadOnlyLocationStatusByCostCenter : INLocationStatusByCostCenter
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? InventoryID
  {
    get => base.InventoryID;
    set => base.InventoryID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? SubItemID
  {
    get => base.SubItemID;
    set => base.SubItemID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? SiteID
  {
    get => base.SiteID;
    set => base.SiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? LocationID
  {
    get => base.LocationID;
    set => base.LocationID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CostCenterID
  {
    get => base.CostCenterID;
    set => base.CostCenterID = value;
  }

  public new abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyLocationStatusByCostCenter.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyLocationStatusByCostCenter.subItemID>
  {
  }

  public new abstract class siteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyLocationStatusByCostCenter.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyLocationStatusByCostCenter.locationID>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyLocationStatusByCostCenter.costCenterID>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReadOnlyLocationStatusByCostCenter.qtyOnHand>
  {
  }
}
