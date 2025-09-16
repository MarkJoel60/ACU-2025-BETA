// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransferLocationStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select5<INLocationStatusInTransit, InnerJoin<INTransitLine, On<INTransitLine.costSiteID, Equal<INLocationStatusInTransit.locationID>>>, Where<INLocationStatusInTransit.qtyOnHand, Greater<Zero>>, Aggregate<GroupBy<INTransitLine.transferNbr, GroupBy<INLocationStatusInTransit.inventoryID, GroupBy<INLocationStatusInTransit.subItemID, Sum<INLocationStatusInTransit.qtyOnHand, Sum<INLocationStatusInTransit.qtyInTransit, Sum<INLocationStatusInTransit.qtyInTransitToSO>>>>>>>>), Persistent = false)]
public class INTransferLocationStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected 
  #nullable disable
  string _TransferNbr;
  protected Decimal? _QtyOnHand;
  protected int? _ToSiteID;

  [PXDBInt(IsKey = true, BqlField = typeof (INLocationStatusInTransit.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (INLocationStatusInTransit.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (INTransitLine.transferNbr), IsKey = true)]
  public virtual string TransferNbr
  {
    get => this._TransferNbr;
    set => this._TransferNbr = value;
  }

  [PXDBQuantity(BqlField = typeof (INLocationStatusInTransit.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [ToSite(DisplayName = "To Warehouse ID", DescriptionField = typeof (INSite.descr), BqlField = typeof (INTransitLine.toSiteID))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  public class PK : 
    PrimaryKeyOf<INTransferLocationStatus>.By<INTransferLocationStatus.inventoryID, INTransferLocationStatus.subItemID, INTransferLocationStatus.transferNbr>
  {
    public static INTransferLocationStatus Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      string transferNbr,
      PKFindOptions options = 0)
    {
      return (INTransferLocationStatus) PrimaryKeyOf<INTransferLocationStatus>.By<INTransferLocationStatus.inventoryID, INTransferLocationStatus.subItemID, INTransferLocationStatus.transferNbr>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) transferNbr, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INTransferLocationStatus>.By<INTransferLocationStatus.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INTransferLocationStatus>.By<INTransferLocationStatus.subItemID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTransferLocationStatus>.By<INTransferLocationStatus.toSiteID>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransferLocationStatus.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransferLocationStatus.subItemID>
  {
  }

  public abstract class transferNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransferLocationStatus.transferNbr>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransferLocationStatus.qtyOnHand>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransferLocationStatus.toSiteID>
  {
  }
}
