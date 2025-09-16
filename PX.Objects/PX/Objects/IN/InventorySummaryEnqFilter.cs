// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventorySummaryEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;

#nullable enable
namespace PX.Objects.IN;

public class InventorySummaryEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _SubItemCD;
  protected int? _SiteID;
  protected int? _LocationID;
  protected bool? _ExpandByLotSerialNbr;

  [AnyInventory(typeof (Search<InventoryItem.inventoryID, Where<InventoryItem.stkItem, NotEqual<boolFalse>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr), Required = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (InventorySummaryEnqFilter.inventoryID), DisplayName = "Subitem")]
  public virtual string SubItemCD
  {
    get => this._SubItemCD;
    set => this._SubItemCD = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubItemCDWildcard
  {
    get => SubCDUtils.CreateSubCDWildcard(this._SubItemCD, "INSUBITEM");
  }

  [Site(DescriptionField = typeof (INSite.descr), DisplayName = "Warehouse")]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (InventorySummaryEnqFilter.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBBool]
  [PXDefault]
  [PXUIField]
  public virtual bool? ExpandByLotSerialNbr
  {
    get => this._ExpandByLotSerialNbr;
    set => this._ExpandByLotSerialNbr = value;
  }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ExpandByCostLayerType { get; set; }

  [OrganizationTree(null, true, FieldClass = "MultipleBaseCurrencies", Required = true)]
  public int? OrgBAccountID { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventorySummaryEnqFilter.inventoryID>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventorySummaryEnqFilter.subItemCD>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventorySummaryEnqFilter.subItemCDWildcard>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventorySummaryEnqFilter.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventorySummaryEnqFilter.locationID>
  {
  }

  public abstract class expandByLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventorySummaryEnqFilter.expandByLotSerialNbr>
  {
  }

  /// <exclude />
  public abstract class expandByCostLayerType : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventorySummaryEnqFilter.expandByCostLayerType>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}
