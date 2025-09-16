// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LinkLineFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.PO;

public class LinkLineFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SiteID;
  protected 
  #nullable disable
  string _SelectedMode;
  protected int? _InventoryID;
  protected string _UOM;

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXSelector(typeof (Search5<POOrderRS.orderNbr, LeftJoin<LinkLineReceipt, On<POOrderRS.orderNbr, Equal<LinkLineReceipt.orderNbr>, And<POOrder.orderType, Equal<LinkLineReceipt.orderType>, And<Current<LinkLineFilter.selectedMode>, Equal<LinkLineFilter.selectedMode.receipt>>>>, LeftJoin<LinkLineOrder, On<POOrderRS.orderNbr, Equal<LinkLineOrder.orderNbr>, And<POOrder.orderType, Equal<LinkLineOrder.orderType>, And<Current<LinkLineFilter.selectedMode>, Equal<LinkLineFilter.selectedMode.order>>>>>>, Where2<Where<LinkLineReceipt.orderNbr, IsNotNull, Or<LinkLineOrder.orderType, IsNotNull>>, And<Where<POOrder.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<POOrder.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>, And2<Not<FeatureInstalled<FeaturesSet.vendorRelations>>, Or2<FeatureInstalled<FeaturesSet.vendorRelations>, And<POOrder.vendorID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorID>>, And<POOrder.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorLocationID>>, And<POOrder.payToVendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>>>>>>>>>>, Aggregate<GroupBy<POOrderRS.orderNbr, GroupBy<POOrder.orderType>>>>))]
  public virtual string POOrderNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Warehouse", FieldClass = "INSITE")]
  [PXSelector(typeof (Search5<INSite.siteID, LeftJoin<LinkLineReceipt, On<INSite.siteID, Equal<LinkLineReceipt.receiptSiteID>, And<Current<LinkLineFilter.selectedMode>, Equal<LinkLineFilter.selectedMode.receipt>>>, LeftJoin<LinkLineOrder, On<INSite.siteID, Equal<LinkLineOrder.orderSiteID>, And<Current<LinkLineFilter.selectedMode>, Equal<LinkLineFilter.selectedMode.order>>>>>, Where<LinkLineReceipt.receiptSiteID, IsNotNull, Or<LinkLineOrder.orderSiteID, IsNotNull>>, Aggregate<GroupBy<INSite.siteID>>>), SubstituteKey = typeof (INSite.siteCD), DescriptionField = typeof (INSite.descr))]
  [PXDefault]
  [PXFormula(typeof (Default<LinkLineFilter.selectedMode>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(1)]
  [PXFormula(typeof (Switch<Case<Where<Selector<LinkLineFilter.inventoryID, PX.Objects.IN.InventoryItem.stkItem>, NotEqual<True>, And<Selector<LinkLineFilter.inventoryID, PX.Objects.IN.InventoryItem.nonStockReceipt>, NotEqual<True>>>, LinkLineFilter.selectedMode.order>, LinkLineFilter.selectedMode.receipt>))]
  [PXUIField(DisplayName = "Selected Mode")]
  [PXStringList(new string[] {"O", "R", "L"}, new string[] {"Purchase Order", "Purchase Receipt", "Landed Cost"})]
  public virtual string SelectedMode
  {
    get => this._SelectedMode;
    set => this._SelectedMode = value;
  }

  [Inventory(Enabled = false)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [INUnit(typeof (LinkLineFilter.inventoryID), Enabled = false)]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  public abstract class pOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineFilter.pOOrderNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineFilter.siteID>
  {
  }

  public abstract class selectedMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineFilter.selectedMode>
  {
    public const string Order = "O";
    public const string Receipt = "R";
    public const string LandedCost = "L";

    public class order : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LinkLineFilter.selectedMode.order>
    {
      public order()
        : base("O")
      {
      }
    }

    public class receipt : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LinkLineFilter.selectedMode.receipt>
    {
      public receipt()
        : base("R")
      {
      }
    }

    public class landedCost : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      LinkLineFilter.selectedMode.landedCost>
    {
      public landedCost()
        : base("L")
      {
      }
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineFilter.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineFilter.uOM>
  {
  }
}
