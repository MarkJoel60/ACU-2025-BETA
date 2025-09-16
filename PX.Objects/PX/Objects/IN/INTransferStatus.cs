// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransferStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select5<INTransferLocationStatus, InnerJoin<INCostStatus, On<INCostStatus.receiptNbr, Equal<INTransferLocationStatus.transferNbr>, And<INCostStatus.inventoryID, Equal<INTransferLocationStatus.inventoryID>>>, InnerJoin<INCostSubItemXRef, On<INCostSubItemXRef.costSubItemID, Equal<INCostStatus.costSubItemID>, And<INCostSubItemXRef.subItemID, Equal<INTransferLocationStatus.subItemID>>>, CrossJoin<CommonSetup>>>, Aggregate<GroupBy<INCostStatus.receiptNbr, GroupBy<INCostStatus.inventoryID, GroupBy<INCostSubItemXRef.subItemID, Sum<INCostStatus.qtyOnHand, Sum<INCostStatus.totalCost>>>>>>>), Persistent = false)]
[PXPrimaryGraph(new Type[] {typeof (INTransferEntry)}, new Type[] {typeof (Select<INRegister, Where<INRegister.docType, Equal<INDocType.transfer>, And<INRegister.refNbr, Equal<Current<INTransferStatus.transferNbr>>>>>)})]
public class INTransferStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected 
  #nullable disable
  string _TransferNbr;
  protected Decimal? _QtyOnHand;
  protected Decimal? _TotalCost;
  protected short? _DecPlPrcCst;
  protected Decimal? _UnitCost;
  protected int? _ToSiteID;

  [PXDefault]
  [StockItem(IsKey = true, BqlField = typeof (INCostStatus.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true, BqlField = typeof (INCostSubItemXRef.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (INCostStatus.receiptNbr), IsKey = true)]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<INDocType.transfer>>>))]
  public virtual string TransferNbr
  {
    get => this._TransferNbr;
    set => this._TransferNbr = value;
  }

  [PXDBQuantity(BqlField = typeof (INCostStatus.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (INCostStatus.totalCost))]
  public virtual Decimal? TotalCost
  {
    get => this._TotalCost;
    set => this._TotalCost = value;
  }

  [PXDBShort(BqlField = typeof (CommonSetup.decPlPrcCst))]
  public virtual short? DecPlPrcCst
  {
    get => this._DecPlPrcCst;
    set => this._DecPlPrcCst = value;
  }

  [PXPriceCost]
  public virtual Decimal? UnitCost
  {
    [PXDependsOnFields(new Type[] {typeof (INTransferStatus.qtyOnHand), typeof (INTransferStatus.totalCost), typeof (INTransferStatus.decPlPrcCst)})] get
    {
      if (!this.QtyOnHand.HasValue || !this.TotalCost.HasValue)
        return new Decimal?();
      Decimal? nullable = this.QtyOnHand;
      Decimal num1 = 0M;
      Decimal num2;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        num2 = 0M;
      }
      else
      {
        nullable = this.TotalCost;
        Decimal num3 = nullable.Value;
        nullable = this.QtyOnHand;
        Decimal num4 = nullable.Value;
        num2 = Math.Round(num3 / num4, (int) this.DecPlPrcCst.Value, MidpointRounding.AwayFromZero);
      }
      return new Decimal?(num2);
    }
    set
    {
    }
  }

  [ToSite(DisplayName = "To Warehouse ID", DescriptionField = typeof (INSite.descr), BqlField = typeof (INTransferLocationStatus.toSiteID))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  public class PK : 
    PrimaryKeyOf<INTransferStatus>.By<INTransferStatus.inventoryID, INTransferStatus.subItemID, INTransferStatus.transferNbr>
  {
    public static INTransferStatus Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      string transferNbr,
      PKFindOptions options = 0)
    {
      return (INTransferStatus) PrimaryKeyOf<INTransferStatus>.By<INTransferStatus.inventoryID, INTransferStatus.subItemID, INTransferStatus.transferNbr>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) transferNbr, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INTransferStatus>.By<INTransferStatus.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INTransferStatus>.By<INTransferStatus.subItemID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTransferStatus>.By<INTransferStatus.toSiteID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransferStatus.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransferStatus.subItemID>
  {
  }

  public abstract class transferNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTransferStatus.transferNbr>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTransferStatus.qtyOnHand>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTransferStatus.totalCost>
  {
  }

  public abstract class decPlPrcCst : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INTransferStatus.decPlPrcCst>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTransferStatus.unitCost>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransferStatus.toSiteID>
  {
  }
}
