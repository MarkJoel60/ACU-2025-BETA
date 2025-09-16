// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.Graphs;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// A stock Item (which has a value and is stored in a warehouse) or a
/// non-stock item (which is not kept in a warehouse and immediately available for purchase).
/// Whether the item is a stock item or a non-stock item is determined by the value of the <see cref="P:PX.Objects.IN.InventoryItem.StkItem" /> field.
/// The records of this type are created and edited on the Stock Items (IN202500) form
/// (which corresponds to the <see cref="T:PX.Objects.IN.InventoryItemMaint" /> graph) and
/// the Non-Stock Items (IN202000) form (which corresponds to the <see cref="T:PX.Objects.IN.NonStockItemMaint" /> graph).
/// </summary>
[PXPrimaryGraph(new System.Type[] {typeof (NonStockItemMaint), typeof (InventoryItemMaint), typeof (TemplateInventoryItemMaint)}, new System.Type[] {typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.isTemplate, Equal<False>>>>>.And<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<False>>>), typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.isTemplate, Equal<False>>>>>.And<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>), typeof (Where<BqlOperand<InventoryItem.isTemplate, IBqlBool>.IsEqual<True>>)})]
[PXCacheName]
public class InventoryItem : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted
{
  protected bool? _NonStockReceiptAsService;
  protected bool? _ExportToExternal;

  /// <summary>
  /// Indicates whether the record is selected for mass processing.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  [PXFormula(typeof (False))]
  public virtual bool? Selected { get; set; }

  /// <summary>
  /// Database identity.
  /// The unique identifier of the Inventory Item.
  /// </summary>
  [PXDBIdentity]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// Key field.
  /// The user-friendly unique identifier of the Inventory Item.
  /// The structure of the identifier is determined by the <i>INVENTORY</i> <see cref="T:PX.Objects.CS.Dimension">Segmented Key</see>.
  /// </summary>
  [PXDefault]
  [InventoryRaw(IsKey = true, DisplayName = "Inventory ID")]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string InventoryCD { get; set; }

  [PXUIField(DisplayName = "Is Converted")]
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsConverted { get; set; }

  [PXBool]
  public virtual bool? IsConversionMode { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that this item is a Stock Item.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Stock Item")]
  public virtual bool? StkItem { get; set; }

  /// <summary>The description of the Inventory Item.</summary>
  [DBMatrixLocalizableDescription(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INItemClass">Item Class</see>, to which the Inventory Item belongs.
  /// Item Classes provide default settings for items, which belong to them, and are used to group items.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INItemClass.ItemClassID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), CacheGlobal = true)]
  [PXDefault(typeof (Search2<INItemClass.itemClassID, InnerJoin<INSetup, On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<InventoryItem.stkItem>, Equal<False>>>>, And<BqlOperand<INSetup.dfltNonStkItemClassID, IBqlInt>.IsEqual<INItemClass.itemClassID>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<InventoryItem.stkItem>, Equal<True>>>>>.And<BqlOperand<INSetup.dfltStkItemClassID, IBqlInt>.IsEqual<INItemClass.itemClassID>>>>>>))]
  [PXUIRequired(typeof (INItemClass.stkItem))]
  public virtual int? ItemClassID { get; set; }

  /// <summary>
  /// The field is used to populate standard settings of <see cref="T:PX.Objects.IN.InventoryItem">Inventory Item</see> from
  /// <see cref="T:PX.Objects.IN.INItemClass">Item Class</see> when it's created On-The-Fly and not yet persisted.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INItemClass.ItemClassID" /> field.
  /// </value>
  [PXInt]
  [PXDefault(typeof (Search2<INItemClass.itemClassID, InnerJoin<INSetup, On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<InventoryItem.stkItem>, Equal<False>>>>, And<BqlOperand<INSetup.dfltNonStkItemClassID, IBqlInt>.IsEqual<INItemClass.itemClassID>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<InventoryItem.stkItem>, Equal<True>>>>>.And<BqlOperand<INSetup.dfltStkItemClassID, IBqlInt>.IsEqual<INItemClass.itemClassID>>>>>>))]
  [PXDBCalced(typeof (InventoryItem.itemClassID), typeof (int), Persistent = true)]
  public virtual int? ParentItemClassID { get; set; }

  /// <summary>The status of the Inventory Item.</summary>
  /// <value>
  /// Possible values are:
  /// <c>"AC"</c> - Active (can be used in inventory operations, such as issues and receipts),
  /// <c>"NS"</c> - No Sales (cannot be sold),
  /// <c>"NP"</c> - No Purchases (cannot be purchased),
  /// <c>"NR"</c> - No Request (cannot be used on requisition requests),
  /// <c>"IN"</c> - Inactive,
  /// <c>"DE"</c> - Marked for Deletion.
  /// Defaults to Active (<c>"AC"</c>).
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("AC")]
  [PXUIField]
  [InventoryItemStatus.List]
  public virtual string ItemStatus { get; set; }

  /// <summary>The type of the Inventory Item.</summary>
  /// <value>
  /// Possible values are:
  /// <c>"F"</c> - Finished Good (Stock Items only),
  /// <c>"M"</c> - Component Part (Stock Items only),
  /// <c>"A"</c> - Subassembly (Stock Items only),
  /// <c>"N"</c> - Non-Stock Item (a general type of Non-Stock Item),
  /// <c>"L"</c> - Labor (Non-Stock Items only),
  /// <c>"S"</c> - Service (Non-Stock Items only),
  /// <c>"C"</c> - Charge (Non-Stock Items only),
  /// <c>"E"</c> - Expense (Non-Stock Items only).
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.ItemType">Type</see> associated with the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>
  /// of the item if it's specified, or to Finished Good (<c>"F"</c>) otherwise.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("F", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.itemType), CacheGlobal = true)]
  [PXUIField]
  [INItemTypes.List]
  public virtual string ItemType { get; set; }

  /// <summary>
  /// The method used for inventory valuation of the item (Stock Items only).
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"T"</c> - Standard,
  /// <c>"A"</c> - Average,
  /// <c>"F"</c> - FIFO,
  /// <c>"S"</c> - Specific.
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.ValMethod">Valuation Method</see> associated with the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>
  /// of the item if it's specified, or to Standard (<c>"T"</c>) otherwise.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.valMethod), CacheGlobal = true)]
  [PXUIField(DisplayName = "Valuation Method")]
  [INValMethod.List]
  public virtual string ValMethod { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory" /> associated with the item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.TaxCategoryID">Tax Category</see> associated with the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>.
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.taxCategoryID), CacheGlobal = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.TX.TaxCategory.active, IBqlBool>.IsEqual<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>
  /// The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive)
  /// should be entered in the detail lines of a document.
  /// This field is displayed only if the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c> (Tax Settings): The tax amount for the document is calculated according to the settings of the applicable tax or taxes.
  /// <c>"G"</c> (Gross): The amount in the document detail line includes a tax or taxes.
  /// <c>"N"</c> (Net): The amount in the document detail line does not include taxes.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<InventoryItem.itemClassID, IBqlInt>.IsNotNull>, Selector<InventoryItem.itemClassID, INItemClass.taxCalcMode>>, TaxCalculationMode.taxSetting>))]
  [TaxCalculationMode.List]
  [PXDefault("T")]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that this item is a Weight Item.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIVisible(typeof (InventoryItem.stkItem))]
  [PXUIEnabled(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.stkItem, Equal<True>>>>>.And<BqlOperand<Selector<InventoryItem.lotSerClassID, INLotSerClass.lotSerTrack>, IBqlString>.IsNotEqual<INLotSerTrack.serialNumbered>>))]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<Selector<InventoryItem.lotSerClassID, INLotSerClass.lotSerTrack>, IBqlString>.IsEqual<INLotSerTrack.serialNumbered>>.Else<InventoryItem.weightItem>))]
  [PXUIField(DisplayName = "Weight Item")]
  public virtual bool? WeightItem { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> used as the base unit for the Inventory Item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.BaseUnit">Base Unit</see> associated with the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>.
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit" /> field.
  /// </value>
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.baseUnit), CacheGlobal = true)]
  [INSyncUoms(new System.Type[] {typeof (InventoryItem.salesUnit), typeof (InventoryItem.purchaseUnit)})]
  [INUnit]
  public virtual string BaseUnit { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> used as the sales unit for the Inventory Item.
  /// This field can be changed only if the <see cref="P:PX.Objects.CS.FeaturesSet.MultipleUnitMeasure">Multiple Units of Measure feature</see> is enabled.
  /// Otherwise, the sales unit is assumed to be the same as the <see cref="P:PX.Objects.IN.InventoryItem.BaseUnit">Base Unit</see>.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.SalesUnit">Sales Unit</see> associated with the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>.
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit" /> field.
  /// </value>
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.salesUnit), CacheGlobal = true)]
  [INUnit(typeof (InventoryItem.inventoryID))]
  public virtual string SalesUnit { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> used as the purchase unit for the Inventory Item.
  /// This field can be changed only if the <see cref="P:PX.Objects.CS.FeaturesSet.MultipleUnitMeasure">Multiple Units of Measure feature</see> is enabled.
  /// Otherwise, the purchase unit is assumed to be the same as the <see cref="P:PX.Objects.IN.InventoryItem.BaseUnit">Base Unit</see>.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.PurchaseUnit">Purchase Unit</see> associated with the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>.
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit" /> field.
  /// </value>
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.purchaseUnit), CacheGlobal = true)]
  [INUnit(typeof (InventoryItem.inventoryID))]
  public virtual string PurchaseUnit { get; set; }

  /// <summary>
  /// When set to <c>false</c>, indicates that the system will prevent enter of non-integer values into the quantity field for choosed <see cref="P:PX.Objects.IN.InventoryItem.BaseUnit">Base Unit</see> value.
  /// <value>
  /// Defaults to <c>true</c></value>
  /// </summary>
  [PXDBBool]
  [INSyncUoms(new System.Type[] {typeof (InventoryItem.decimalSalesUnit), typeof (InventoryItem.decimalPurchaseUnit)})]
  [PXDefault(true, typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.decimalBaseUnit), CacheGlobal = true)]
  [PXUIField]
  public virtual bool? DecimalBaseUnit { get; set; }

  /// <summary>
  /// When set to <c>false</c>, indicates that the system will prevent enter of non-integer values into the quantity field for choosed <see cref="P:PX.Objects.IN.InventoryItem.SalesUnit">Sales Unit</see> value.
  /// <value>
  /// Defaults to <c>true</c></value>
  /// </summary>
  [PXDBBool]
  [PXDefault(true, typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.decimalSalesUnit), CacheGlobal = true)]
  [PXUIField]
  public virtual bool? DecimalSalesUnit { get; set; }

  /// <summary>
  /// When set to <c>false</c>, indicates that the system will prevent enter of non-integer values into the quantity field for choosed <see cref="P:PX.Objects.IN.InventoryItem.PurchaseUnit">Purchase Unit</see> value.
  /// <value>
  /// Defaults to <c>true</c></value>
  /// </summary>
  [PXDBBool]
  [PXDefault(true, typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.decimalPurchaseUnit), CacheGlobal = true)]
  [PXUIField]
  public virtual bool? DecimalPurchaseUnit { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must calculate commission on the sale of this item.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Commisionable { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.IN.INPostClass">Posting Class</see> associated with the item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.PostClassID">Posting Class</see> selected for the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">item class</see>.
  /// Corresponds to the <see cref="P:PX.Objects.IN.INPostClass.PostClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Posting Class")]
  public virtual string PostClassID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">Suabaccount</see> defined by the <see cref="T:PX.Objects.CS.ReasonCode">Reason Code</see>, associated with this item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.ReasonCodeSubID">Reason Code Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.reasonCodeSubID>))]
  [SubAccount]
  [PXForeignReference(typeof (InventoryItem.FK.ReasonCodeSubaccount))]
  public virtual int? ReasonCodeSubID { get; set; }

  /// <summary>
  /// The income <see cref="T:PX.Objects.GL.Account" /> used to record sales of the item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.SalesAcctID">Sales Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.salesAcctID>))]
  [PXForeignReference(typeof (InventoryItem.FK.SalesAccount))]
  public virtual int? SalesAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to record sales of the item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.SalesSubID">Sales Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (InventoryItem.salesAcctID))]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.salesSubID>))]
  [PXForeignReference(typeof (InventoryItem.FK.SalesSubaccount))]
  public virtual int? SalesSubID { get; set; }

  /// <summary>
  /// The asset <see cref="T:PX.Objects.GL.Account" /> used to keep the inventory balance, resulting from the transactions with this item.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.InvtAcctID">Inventory Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.invtAcctID>))]
  [PXForeignReference(typeof (InventoryItem.FK.InventoryAccount))]
  public virtual int? InvtAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to keep the inventory balance, resulting from the transactions with this item.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.InvtSubID">Inventory Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (InventoryItem.invtAcctID))]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.invtSubID>))]
  [PXForeignReference(typeof (InventoryItem.FK.InventorySubaccount))]
  public virtual int? InvtSubID { get; set; }

  /// <summary>
  /// The expense <see cref="T:PX.Objects.GL.Account" /> used to record the cost of goods sold for this item when a sales order for it is released.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.COGSAcctID">COGS Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.cOGSAcctID>))]
  [PXForeignReference(typeof (InventoryItem.FK.COGSAccount))]
  public virtual int? COGSAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to record the cost of goods sold for this item when a sales order for it is released.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.COGSSubID">COGS Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (InventoryItem.cOGSAcctID))]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.cOGSSubID>))]
  [PXForeignReference(typeof (InventoryItem.FK.COGSSubaccount))]
  public virtual int? COGSSubID { get; set; }

  /// <summary>
  /// The asset <see cref="T:PX.Objects.GL.Account" /> used to keep the Expense Accrual Account, resulting from the transactions with this item.
  /// Applicable only for Non-Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.InvtAcctID">Inventory Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXInt]
  public virtual int? ExpenseAccrualAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to keep the Expense Accrual Account, resulting from the transactions with this item.
  /// Applicable only for Non-Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.InvtSubID">Inventory Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXInt]
  public virtual int? ExpenseAccrualSubID { get; set; }

  /// <summary>
  /// The expense <see cref="T:PX.Objects.GL.Account" /> used to record the cost of goods sold for this item when a sales order for it is released.
  /// Applicable only for Non-Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.COGSAcctID">COGS Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXInt]
  public virtual int? ExpenseAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to record the cost of goods sold for this item when a sales order for it is released.
  /// Applicable only for Non-Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.COGSSubID">COGS Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXInt]
  public virtual int? ExpenseSubID { get; set; }

  /// <summary>
  /// The expense <see cref="T:PX.Objects.GL.Account" /> used to record the differences in inventory value of this item estimated
  /// by using the pending standard cost and the currently effective standard cost for the quantities on hand.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) under Standard <see cref="P:PX.Objects.IN.InventoryItem.ValMethod">Valuation Method</see>.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.StdCstRevAcctID">Standard Cost Revaluation Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXFormula(typeof (BqlOperand<Selector<InventoryItem.postClassID, INPostClass.stdCstRevAcctID>, IBqlInt>.When<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>.Else<Null>))]
  [PXForeignReference(typeof (InventoryItem.FK.StandardCostRevaluationAccount))]
  public virtual int? StdCstRevAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to record the differences in inventory value of this item estimated
  /// by using the pending standard cost and the currently effective standard cost for the quantities on hand.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) under Standard <see cref="P:PX.Objects.IN.InventoryItem.ValMethod">Valuation Method</see>.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.StdCstRevSubID">Standard Cost Revaluation Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (InventoryItem.stdCstRevAcctID))]
  [PXFormula(typeof (BqlOperand<Selector<InventoryItem.postClassID, INPostClass.stdCstRevSubID>, IBqlInt>.When<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>.Else<Null>))]
  [PXForeignReference(typeof (InventoryItem.FK.StandardCostRevaluationSubaccount))]
  public virtual int? StdCstRevSubID { get; set; }

  /// <summary>
  /// The expense <see cref="T:PX.Objects.GL.Account" /> used to record the differences between the currently effective standard cost
  /// and the cost on the inventory receipt of the item.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) under Standard <see cref="P:PX.Objects.IN.InventoryItem.ValMethod">Valuation Method</see>.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.StdCstVarAcctID">Standard Cost Variance Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXFormula(typeof (BqlOperand<Selector<InventoryItem.postClassID, INPostClass.stdCstVarAcctID>, IBqlInt>.When<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>.Else<Null>))]
  [PXForeignReference(typeof (InventoryItem.FK.StandardCostVarianceAccount))]
  public virtual int? StdCstVarAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to record the differences between the currently effective standard cost
  /// and the cost on the inventory receipt of the item.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) under Standard <see cref="P:PX.Objects.IN.InventoryItem.ValMethod">Valuation Method</see>.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.StdCstVarSubID">Standard Cost Variance Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (InventoryItem.stdCstVarAcctID))]
  [PXFormula(typeof (BqlOperand<Selector<InventoryItem.postClassID, INPostClass.stdCstVarSubID>, IBqlInt>.When<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>.Else<Null>))]
  [PXForeignReference(typeof (InventoryItem.FK.StandardCostVarianceSubaccount))]
  public virtual int? StdCstVarSubID { get; set; }

  /// <summary>
  /// The expense <see cref="T:PX.Objects.GL.Account" /> used to record the differences between the extended price on the purchase receipt
  /// and the extended price on the Accounts Payable bill for this item.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) under any <see cref="P:PX.Objects.IN.InventoryItem.ValMethod">Valuation Method</see> except Standard.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.PPVAcctID">Purchase Price Variance Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.pPVAcctID>))]
  [PXForeignReference(typeof (InventoryItem.FK.PPVAccount))]
  public virtual int? PPVAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to record the differences between the extended price on the purchase receipt
  /// and the extended price on the Accounts Payable bill for this item.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) under any <see cref="P:PX.Objects.IN.InventoryItem.ValMethod">Valuation Method</see> except Standard.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.PPVSubID">Purchase Price Variance Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (InventoryItem.pPVAcctID))]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.pPVSubID>))]
  [PXForeignReference(typeof (InventoryItem.FK.PPVSubaccount))]
  public virtual int? PPVSubID { get; set; }

  /// <summary>
  /// The liability <see cref="T:PX.Objects.GL.Account" /> used to accrue amounts on purchase orders related to this item.
  /// Applicable for all Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) and for Non-Stock Items, for which a receipt is required.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.POAccrualAcctID">PO Accrual Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.pOAccrualAcctID>))]
  [PXForeignReference(typeof (InventoryItem.FK.POAccrualAccount))]
  public virtual int? POAccrualAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to accrue amounts on purchase orders related to this item.
  /// Applicable for all Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) and for Non-Stock Items, for which a receipt is required.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.POAccrualSubID">PO Accrual Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (InventoryItem.pOAccrualAcctID))]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.pOAccrualSubID>))]
  [PXForeignReference(typeof (InventoryItem.FK.POAccrualSubaccount))]
  public virtual int? POAccrualSubID { get; set; }

  /// <summary>
  /// The expense <see cref="T:PX.Objects.GL.Account" /> used to record differences between the landed cost amounts specified on purchase receipts
  /// and the amounts on inventory receipts.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.LCVarianceAcctID">Landed Cost Variance Account</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXFormula(typeof (BqlOperand<Selector<InventoryItem.postClassID, INPostClass.lCVarianceAcctID>, IBqlInt>.When<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>.Else<Null>))]
  [PXForeignReference(typeof (InventoryItem.FK.LandedCostVarianceAccount))]
  public virtual int? LCVarianceAcctID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">Subaccount</see> used to record differences between the landed cost amounts specified on purchase receipts
  /// and the amounts on inventory receipts.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INPostClass.LCVarianceSubID">Landed Cost Variance Sub.</see> set on the <see cref="P:PX.Objects.IN.InventoryItem.PostClassID">Posting Class</see> of the item.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (InventoryItem.lCVarianceAcctID))]
  [PXFormula(typeof (BqlOperand<Selector<InventoryItem.postClassID, INPostClass.lCVarianceSubID>, IBqlInt>.When<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>.Else<Null>))]
  [PXForeignReference(typeof (InventoryItem.FK.LandedCostVarianceSubaccount))]
  public virtual int? LCVarianceSubID { get; set; }

  [Account]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.deferralAcctID>))]
  [PXForeignReference(typeof (InventoryItem.FK.DeferralAccount))]
  public int? DeferralAcctID { get; set; }

  [SubAccount(typeof (InventoryItem.deferralAcctID))]
  [PXFormula(typeof (Selector<InventoryItem.postClassID, INPostClass.deferralSubID>))]
  [PXForeignReference(typeof (InventoryItem.FK.DeferralSubaccount))]
  public int? DeferralSubID { get; set; }

  /// <summary>Reserved for internal use.</summary>
  [PXDBInt]
  public virtual int? LastSiteID { get; set; }

  /// <summary>
  /// The standard cost assigned to the item before the current standard cost was set.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost", Enabled = false)]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.LastStdCost instead.")]
  public virtual Decimal? LastStdCost { get; set; }

  /// <summary>
  /// The standard cost to be assigned to the item when the costs are updated.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Cost")]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.PendingStdCost instead.")]
  public virtual Decimal? PendingStdCost { get; set; }

  /// <summary>
  /// The date when the <see cref="P:PX.Objects.IN.InventoryItem.PendingStdCost">Pending Cost</see> becomes effective.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Pending Cost Date")]
  [PXFormula(typeof (BqlOperand<Current<AccessInfo.businessDate>, IBqlDateTime>.When<BqlOperand<InventoryItem.pendingStdCost, IBqlDecimal>.IsNotEqual<decimal0>>.Else<InventoryItem.pendingStdCostDate>))]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.PendingStdCostDate instead.")]
  public virtual DateTime? PendingStdCostDate { get; set; }

  /// <summary>The current standard cost of the item.</summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Cost", Enabled = false)]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.StdCost instead.")]
  public virtual Decimal? StdCost { get; set; }

  /// <summary>
  /// The date when the <see cref="P:PX.Objects.IN.InventoryItem.StdCost">Current Cost</see> became effective.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Enabled = false)]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.StdCostDate instead.")]
  public virtual DateTime? StdCostDate { get; set; }

  /// <summary>
  /// The price used as the default price, if there are no other prices defined for this item in any price list in the Accounts Receivable module.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.BasePrice instead.")]
  public virtual Decimal? BasePrice { get; set; }

  /// <summary>
  /// The weight of the <see cref="P:PX.Objects.IN.InventoryItem.BaseUnit">Base Unit</see> of the item.
  /// </summary>
  /// <value>
  /// Given in the <see cref="P:PX.Objects.CS.CommonSetup.WeightUOM">default Weight Unit of the Inventory module</see>.
  /// </value>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseWeight { get; set; }

  /// <summary>The volume of the item.</summary>
  /// <value>
  /// Given in the <see cref="P:PX.Objects.CS.CommonSetup.VolumeUOM">default Volume Unit of the Inventory module</see>.
  /// </value>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Volume")]
  public virtual Decimal? BaseVolume { get; set; }

  /// <summary>
  /// The weight of the <see cref="P:PX.Objects.IN.InventoryItem.BaseUnit">Base Unit</see> of the item.
  /// </summary>
  /// <value>
  /// Given in the <see cref="P:PX.Objects.IN.InventoryItem.WeightUOM">Weight Unit of this item</see>.
  /// </value>
  [PXDBQuantity(6, typeof (InventoryItem.weightUOM), typeof (InventoryItem.baseWeight), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Weight")]
  public virtual Decimal? BaseItemWeight { get; set; }

  /// <summary>
  /// The volume of the <see cref="P:PX.Objects.IN.InventoryItem.BaseUnit">Base Unit</see> of the item.
  /// </summary>
  /// <value>
  /// Given in the <see cref="P:PX.Objects.IN.InventoryItem.VolumeUOM">Volume Unit of this item</see>.
  /// </value>
  [PXDBQuantity(6, typeof (InventoryItem.volumeUOM), typeof (InventoryItem.baseVolume), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Volume")]
  public virtual Decimal? BaseItemVolume { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> used for the <see cref="P:PX.Objects.IN.InventoryItem.BaseItemWeight">Weight</see> of the item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit" /> field.
  /// </value>
  [INUnit(null, typeof (CommonSetup.weightUOM), DisplayName = "Weight UOM")]
  public virtual string WeightUOM { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> used for the <see cref="P:PX.Objects.IN.InventoryItem.BaseItemVolume">Volume</see> of the item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit" /> field.
  /// </value>
  [INUnit(null, typeof (CommonSetup.volumeUOM), DisplayName = "Volume UOM")]
  public virtual string VolumeUOM { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the item must be packaged separately from other items.
  /// This field is automatically set to <c>true</c> if <i>By Quantity</i> is selected as the <see cref="P:PX.Objects.IN.InventoryItem.PackageOption">PackageOption</see>.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? PackSeparately { get; set; }

  /// <summary>
  /// The option that governs the system in the process of determining the optimal set of boxes for the item on each sales order.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"N"</c> - Manual,
  /// <c>"W"</c> - By Weight (the system will take into account the <see cref="P:PX.Objects.IN.INItemBoxEx.MaxWeight">Max. Weight</see> allowed for each box specififed for the item),
  /// <c>"Q"</c> - By Quantity (the system will take into account the <see cref="!:INItemBoxEx.MaxQty">Max. Quantity</see> allowed for each box specififed for the item.
  /// With this option items of this kind are always packages separately from other items),
  /// <c>"V"</c> - By Weight and Volume (the system will take into account the <see cref="P:PX.Objects.IN.INItemBoxEx.MaxWeight">Max. Weight</see> and
  /// <see cref="P:PX.Objects.IN.INItemBoxEx.MaxVolume">Max. Volume</see> allowed for each box specififed for the item).
  /// Defaults to Manual (<c>"M"</c>).
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Packaging Option")]
  [INPackageOption.List]
  public virtual string PackageOption { get; set; }

  /// <summary>
  /// Preferred (default) <see cref="T:PX.Objects.AP.Vendor">Vendor</see> for purchases of this item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [VendorNonEmployeeActiveOrHoldPayments(DisplayName = "Preferred Vendor", Required = false, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName))]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.PreferredVendorID instead.")]
  public virtual int? PreferredVendorID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Location" /> of the <see cref="P:PX.Objects.IN.InventoryItem.PreferredVendorID">Preferred (default) Vendor</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [LocationID(typeof (Where<BqlOperand<PX.Objects.CR.Location.bAccountID, IBqlInt>.IsEqual<BqlField<InventoryItem.preferredVendorID, IBqlInt>.FromCurrent>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Preferred Location")]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.PreferredVendorLocationID instead.")]
  public virtual int? PreferredVendorLocationID { get; set; }

  /// <summary>
  /// The default <see cref="T:PX.Objects.IN.INSubItem">Subitem</see> for this item, which is used when there are no subitems
  /// or when specifying subitems is not important.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.SubItem">Inventory Subitems</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INSubItem.SubItemID" /> field.
  /// </value>
  [SubItem(typeof (InventoryItem.inventoryID), DisplayName = "Default Subitem")]
  public virtual int? DefaultSubItemID { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must set the <see cref="P:PX.Objects.IN.InventoryItem.DefaultSubItemID">Default Subitem</see>
  /// for the lines involving this item by default on data entry forms.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.SubItem">Inventory Subitems</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use On Entry")]
  public virtual bool? DefaultSubItemOnEntry { get; set; }

  /// <summary>
  /// The default <see cref="T:PX.Objects.IN.INSite">Warehouse</see> used to store the items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) and when the <see cref="P:PX.Objects.CS.FeaturesSet.Warehouse">Warehouses</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INSite.SiteID" /> field.
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClassCurySettings.DfltSiteID">Default Warehouse</see> specified for the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Class of the item</see>.
  /// </value>
  [Site(DisplayName = "Default Warehouse", DescriptionField = typeof (INSite.descr))]
  [PXForeignReference(typeof (InventoryItem.FK.DefaultSite))]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.DfltSiteID instead.")]
  public virtual int? DfltSiteID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INLocation">Location of warehouse</see> used by default to issue items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) when the <see cref="P:PX.Objects.CS.FeaturesSet.WarehouseLocation">Warehouse Locations</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INLocation.LocationID" /> field.
  /// </value>
  [Location(typeof (InventoryItem.dfltSiteID), DisplayName = "Default Issue From", KeepEntry = false, ResetEntry = false, DescriptionField = typeof (INLocation.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.DfltShipLocationID instead.")]
  public virtual int? DfltShipLocationID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INLocation">Location of warehouse</see> used by default to receive items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) when the <see cref="P:PX.Objects.CS.FeaturesSet.WarehouseLocation">Warehouse Locations</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INLocation.LocationID" /> field.
  /// </value>
  [Location(typeof (InventoryItem.dfltSiteID), DisplayName = "Default Receipt To", KeepEntry = false, ResetEntry = false, DescriptionField = typeof (INLocation.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.DfltReceiptLocationID instead.")]
  public virtual int? DfltReceiptLocationID { get; set; }

  /// <summary>The default cost code of the item.</summary>
  [PXDBInt]
  [PXForeignReference(typeof (Field<InventoryItem.defaultCostCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [PXUIField]
  [PXUIEnabled(typeof (Where<FeatureInstalled<FeaturesSet.costCodes>>))]
  [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.costCodes>>))]
  [CostCodeDimensionSelector(null, null, null, null, true)]
  public virtual int? DefaultCostCodeID { get; set; }

  /// <summary>The workgroup that is responsible for the item.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID" /> field.
  /// </value>
  [PXDBInt]
  [PXWorkgroupSelector]
  [PXUIField(DisplayName = "Product Workgroup")]
  public virtual int? ProductWorkgroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Contact">product manager</see> responsible for this item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [Owner(typeof (InventoryItem.productWorkgroupID), DisplayName = "Product Manager")]
  public virtual int? ProductManagerID { get; set; }

  /// <summary>
  /// The workgroup that is responsible for the pricing of this item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID" /> field.
  /// </value>
  [PXDBInt]
  [PXWorkgroupSelector]
  [PXUIField(DisplayName = "Price Workgroup")]
  public virtual int? PriceWorkgroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Contact">manager</see> responsible for the pricing of this item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [Owner(typeof (InventoryItem.priceWorkgroupID), DisplayName = "Price Manager")]
  public virtual int? PriceManagerID { get; set; }

  /// <summary>
  /// An unbound field that, when equal to <c>true</c>, indicates that negative quantities are allowed for this item.
  /// </summary>
  /// <value>
  /// The value of this field is taken from the <see cref="P:PX.Objects.IN.INItemClass.NegQty" /> field of the <see cref="!:ItemClass">Class</see>, to which the item belongs.
  /// </value>
  [PXBool]
  [PXFormula(typeof (Selector<InventoryItem.itemClassID, INItemClass.negQty>))]
  public virtual bool? NegQty { get; set; }

  [PXString]
  [PXDBCalced(typeof (InventoryItem.lotSerClassID), typeof (string), Persistent = true)]
  public virtual string OrigLotSerClassID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INLotSerClass">lot/serial class</see>, to which the item is assigned.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.LotSerialTracking">Lot/Serial Tracking</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INLotSerClass.LotSerClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (INLotSerClass.lotSerClassID), DescriptionField = typeof (INLotSerClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Lot/Serial Class")]
  public virtual string LotSerClassID { get; set; }

  /// <summary>
  /// The deferral code used to perform deferrals on sale of the item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.DeferredCode">Deferral Code</see> selected for the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>.
  /// Corresponds to the <see cref="P:PX.Objects.DR.DRDeferredCode.DeferredCodeID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Deferral Code")]
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID>))]
  [PXRestrictor(typeof (Where<BqlOperand<DRDeferredCode.active, IBqlBool>.IsEqual<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new System.Type[] {typeof (DRDeferredCode.deferredCodeID)})]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string DeferredCode { get; set; }

  [PXDBDecimal(0, MinValue = 0.0, MaxValue = 10000.0)]
  [PXUIField(DisplayName = "Default Term")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DefaultTerm { get; set; }

  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Default Term UOM")]
  [DRTerms.UOMList]
  [PXDefault("Y")]
  public virtual string DefaultTermUOM { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INPriceClass">Item Price Class</see> associated with the item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.PriceClassID">Price Class</see> selected for the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>.
  /// Corresponds to the <see cref="P:PX.Objects.IN.INPriceClass.PriceClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (INPriceClass.priceClassID), DescriptionField = typeof (INPriceClass.description))]
  [PXUIField]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string PriceClassID { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system should split the revenue from sale of the item among its components.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Split into Components")]
  public virtual bool? IsSplitted { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system should use the component subaccounts in the component-associated deferrals.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Component Subaccounts", FieldClass = "SUBACCOUNT")]
  public virtual bool? UseParentSubID { get; set; }

  /// <summary>
  /// The total percentage of the item price as split among components.
  /// </summary>
  /// <value>
  /// The value is calculated automatically from the <see cref="P:PX.Objects.IN.INComponent.Percentage">percentages</see>
  /// of the <see cref="T:PX.Objects.IN.INComponent">components</see> associated with the item.
  /// Set to <c>100</c> if the item is not a package.
  /// </value>
  [PXDecimal]
  [PXUIField(DisplayName = "Total Percentage", Enabled = false)]
  public virtual Decimal? TotalPercentage { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the item is a kit.
  /// Kits are stock or non-stock items that consist of other items and are sold as a whole.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? KitItem { get; set; }

  /// <summary>
  /// The minimum markup percentage for the item.
  /// See the <see cref="P:PX.Objects.IN.InventoryItem.MarkupPct" /> field.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.MinGrossProfitPct">Min. Margup %</see> defined for the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>.
  /// </value>
  [PXDefault(TypeCode.Decimal, "0.0", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.minGrossProfitPct), CacheGlobal = true)]
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 1000.0)]
  [PXUIField]
  public virtual Decimal? MinGrossProfitPct { get; set; }

  /// <summary>
  /// Reserved for internal use.
  /// Indicates whether the item (assumed Non-Stock) requires receipt.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Receipt")]
  public virtual bool? NonStockReceipt { get; set; }

  /// <summary>
  /// Indicates whether the item (assumed Non-Stock) should be receipted as service.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>))]
  [PXUIField(DisplayName = "Process Item via Receipt")]
  public virtual bool? NonStockReceiptAsService
  {
    get => this._NonStockReceiptAsService;
    set => this._NonStockReceiptAsService = value;
  }

  /// <summary>
  /// Reserved for internal use.
  /// Indicates whether the item (assumed Non-Stock) requires shipment.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Shipment")]
  public virtual bool? NonStockShip { get; set; }

  /// <summary>
  /// When set to Sales, indicates that cost will be processed using expense accrual account.
  /// </summary>
  [PXDBString]
  [InventoryItem.postToExpenseAccount.List]
  [PXUIField(DisplayName = "Post Cost to Expenses On")]
  [PXUIEnabled(typeof (Where<BqlOperand<InventoryItem.kitItem, IBqlBool>.IsNotEqual<True>>))]
  [PXDefault("P", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.postToExpenseAccount), CacheGlobal = true)]
  public virtual string PostToExpenseAccount { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Cost Based On")]
  [PXUIEnabled(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.postToExpenseAccount, Equal<InventoryItem.postToExpenseAccount.sales>>>>>.And<BqlOperand<InventoryItem.kitItem, IBqlBool>.IsNotEqual<True>>>))]
  [PXDefault("S")]
  [CostBasisOption.List]
  public virtual string CostBasis { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 1000.0)]
  [PXUIEnabled(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.postToExpenseAccount, Equal<InventoryItem.postToExpenseAccount.sales>>>>>.And<BqlOperand<InventoryItem.costBasis, IBqlString>.IsEqual<CostBasisOption.percentOfSalesPrice>>>))]
  [PXUIField]
  public virtual Decimal? PercentOfSalesPrice { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<InventoryItem.itemType, IBqlString>.IsIn<INItemTypes.laborItem, INItemTypes.serviceItem, INItemTypes.chargeItem, INItemTypes.expenseItem>>, CompletePOLineTypes.amount>, CompletePOLineTypes.quantity>))]
  [PXUIField(DisplayName = "Close PO Line")]
  [CompletePOLineTypes.List]
  public virtual string CompletePOLine { get; set; }

  /// <exclude />
  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  [PXDefault("A", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.preferredItemClasses))]
  [PXUIField(DisplayName = "Preferred Item Classes", FieldClass = "RelatedItemAssistant")]
  [PreferredItemClassesList.List]
  public virtual string PreferredItemClasses { get; set; }

  /// <exclude />
  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  [PXDefault("A", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.priceOfSuggestedItems))]
  [PXUIField(DisplayName = "Price Of Suggested Items", FieldClass = "RelatedItemAssistant")]
  [PriceOfSuggestedItemsList.List]
  public virtual string PriceOfSuggestedItems { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INABCCode">ABC code</see>, to which the item is assigned for the purpose of physical inventories.
  /// The field is relevant only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INABCCode.ABCCodeID" /> field.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "ABC Code")]
  [PXSelector(typeof (INABCCode.aBCCodeID), DescriptionField = typeof (INABCCode.descr))]
  public virtual string ABCCodeID { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must not change the <see cref="P:PX.Objects.IN.InventoryItem.ABCCodeID">ABC Code</see>
  /// assigned to the item when ABC code assignments are updated.
  /// The field is relevant only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Fixed ABC Code")]
  public virtual bool? ABCCodeIsFixed { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INMovementClass">Movement Class</see>, to which the item is assigned for the purpose of physical inventories.
  /// The field is relevant only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INMovementClass.MovementClassID" /> field.
  /// </value>
  [PXDBString(1)]
  [PXUIField(DisplayName = "Movement Class")]
  [PXSelector(typeof (INMovementClass.movementClassID), DescriptionField = typeof (INMovementClass.descr))]
  public virtual string MovementClassID { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must not change the <see cref="P:PX.Objects.IN.InventoryItem.MovementClassID">Movement Class</see>
  /// assigned to the item when movement class assignments are updated.
  /// The field is relevant only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Fixed Movement Class")]
  public virtual bool? MovementClassIsFixed { get; set; }

  /// <summary>
  /// The percentage that is added to the item cost to get the selling price for it.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.MarkupPct">Markup %</see> specified for the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Item Class</see>.
  /// </value>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 1000.0)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.markupPct), CacheGlobal = true)]
  [PXUIField(DisplayName = "Markup %")]
  public virtual Decimal? MarkupPct { get; set; }

  /// <summary>
  /// The manufacturer's suggested retail price of the item.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "MSRP")]
  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.RecPrice instead.")]
  public virtual Decimal? RecPrice { get; set; }

  /// <summary>The URL of the image associated with the item.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Image")]
  [PXAttachedFile]
  public virtual string ImageUrl { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>))]
  [PXUIField]
  [CommodityCodeTypes.List]
  public virtual string CommodityCodeType { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Commodity Code")]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string HSTariffCode { get; set; }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "100.0", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.undershipThreshold), CacheGlobal = true)]
  [PXUIField(DisplayName = "Undership Threshold (%)")]
  public virtual Decimal? UndershipThreshold { get; set; }

  [PXDBDecimal(2, MinValue = 100.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.overshipThreshold), CacheGlobal = true)]
  [PXUIField(DisplayName = "Overship Threshold (%)")]
  public virtual Decimal? OvershipThreshold { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Country Of Origin")]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>))]
  [Country]
  public virtual string CountryOfOrigin { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXSearchable(32 /*0x20*/, "{0}: {1}", new System.Type[] {typeof (InventoryItem.itemType), typeof (InventoryItem.inventoryCD)}, new System.Type[] {typeof (InventoryItem.descr)}, NumberFields = new System.Type[] {typeof (InventoryItem.inventoryCD)}, Line1Format = "{0}{1}{2}", Line1Fields = new System.Type[] {typeof (INItemClass.itemClassCD), typeof (INItemClass.descr), typeof (InventoryItem.baseUnit)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (InventoryItem.descr)}, WhereConstraint = typeof (Where<BqlOperand<Current<InventoryItem.itemStatus>, IBqlString>.IsNotEqual<InventoryItemStatus.unknown>>))]
  [PXNote(PopupTextEnabled = true)]
  public virtual Guid? NoteID { get; set; }

  /// <summary>The default inventory source for projects.</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [InventorySourceType.List(false)]
  [PXUIField(DisplayName = "Default Inventory Source for Projects", FieldClass = "MaterialManagement")]
  public virtual string DefaultInventorySourceForProjects { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// The group mask showing which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the item belongs to.
  /// </summary>
  [PXDBGroupMask]
  public virtual byte[] GroupMask { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INPICycle">Physical Inventory Cycle</see> assigned to the item.
  /// The cycle defines how often the physical inventory counts will be performed for the item.
  /// The field is relevant only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />).
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INPICycle.CycleID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "PI Cycle")]
  [PXSelector(typeof (INPICycle.cycleID), DescriptionField = typeof (INPICycle.descr))]
  public virtual string CycleID { get; set; }

  /// <summary>
  /// Reserved for internal use.
  /// Provides the values of attributes associated with the item.
  /// For more information see the <see cref="T:PX.Objects.CS.CSAnswers" /> class.
  /// </summary>
  [CRAttributesField(typeof (InventoryItem.parentItemClassID))]
  public virtual string[] Attributes { get; set; }

  /// <summary>
  /// Reserved for internal use.
  /// <see cref="!:IAttributeSupport" /> implementation. The record class ID for attributes retrieval.
  /// </summary>
  public virtual int? ClassID => this.ItemClassID;

  /// <summary>
  /// An unbound field used in the User Interface to include the item into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  [PXUnboundDefault(false)]
  public virtual bool? Included { get; set; }

  /// <summary>Rich text description of the item.</summary>
  [PXDBLocalizableString(IsUnicode = true)]
  [PXUIField(DisplayName = "Content")]
  public virtual string Body { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the item is a template for other matrix items.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsTemplate { get; set; }

  /// <summary>
  /// References to parent Inventory Item, its database identifier, if this item was created from template.
  /// </summary>
  [PXUIField(DisplayName = "Template Item", FieldClass = "MatrixItem", Enabled = false)]
  [TemplateInventory]
  [PXForeignReference(typeof (Field<InventoryItem.templateItemID>.IsRelatedTo<InventoryItem.inventoryID>))]
  public virtual int? TemplateItemID { get; set; }

  /// <summary>
  /// References to Attribute which will be put as Row Attribute in Inventory Matrix by default.
  /// </summary>
  [PXDefault]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Row Attribute ID", FieldClass = "MatrixItem")]
  [MatrixAttributeSelector(typeof (Search2<CSAnswers.attributeID, InnerJoin<CSAttributeGroup, On<BqlOperand<CSAttributeGroup.attributeID, IBqlString>.IsEqual<CSAnswers.attributeID>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.refNoteID, Equal<BqlField<InventoryItem.noteID, IBqlGuid>.FromCurrent>>>>, And<BqlOperand<CSAnswers.isActive, IBqlBool>.IsEqual<True>>>, And<BqlOperand<CSAttributeGroup.entityClassID, IBqlString>.IsEqual<Use<BqlField<InventoryItem.parentItemClassID, IBqlInt>.FromCurrent>.AsString>>>, And<BqlOperand<CSAttributeGroup.entityType, IBqlString>.IsEqual<Constants.DACName<InventoryItem>>>>>.And<BqlOperand<CSAttributeGroup.attributeCategory, IBqlString>.IsEqual<CSAttributeGroup.attributeCategory.variant>>>>), typeof (InventoryItem.defaultColumnMatrixAttributeID), true, new System.Type[] {typeof (CSAttributeGroup.attributeID)}, DescriptionField = typeof (CSAttributeGroup.description), DirtyRead = true)]
  [PXRestrictor(typeof (Where<CSAttributeGroup.isActive, Equal<True>>), "The {0} attribute is inactive. Specify an active attribute.", new System.Type[] {typeof (CSAttributeGroup.attributeID)})]
  public virtual string DefaultRowMatrixAttributeID { get; set; }

  /// <summary>
  /// References to Attribute which will be put as Column Attribute in Inventory Matrix by default.
  /// </summary>
  [PXDefault]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Column Attribute ID", FieldClass = "MatrixItem")]
  [MatrixAttributeSelector(typeof (Search2<CSAnswers.attributeID, InnerJoin<CSAttributeGroup, On<BqlOperand<CSAttributeGroup.attributeID, IBqlString>.IsEqual<CSAnswers.attributeID>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.refNoteID, Equal<BqlField<InventoryItem.noteID, IBqlGuid>.FromCurrent>>>>, And<BqlOperand<CSAnswers.isActive, IBqlBool>.IsEqual<True>>>, And<BqlOperand<CSAttributeGroup.entityClassID, IBqlString>.IsEqual<Use<BqlField<InventoryItem.parentItemClassID, IBqlInt>.FromCurrent>.AsString>>>, And<BqlOperand<CSAttributeGroup.entityType, IBqlString>.IsEqual<Constants.DACName<InventoryItem>>>>>.And<BqlOperand<CSAttributeGroup.attributeCategory, IBqlString>.IsEqual<CSAttributeGroup.attributeCategory.variant>>>>), typeof (InventoryItem.defaultRowMatrixAttributeID), true, new System.Type[] {typeof (CSAttributeGroup.attributeID)}, DescriptionField = typeof (CSAttributeGroup.description), DirtyRead = true)]
  [PXRestrictor(typeof (Where<CSAttributeGroup.isActive, Equal<True>>), "The {0} attribute is inactive. Specify an active attribute.", new System.Type[] {typeof (CSAttributeGroup.attributeID)})]
  public virtual string DefaultColumnMatrixAttributeID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? GenerationRuleCntr { get; set; }

  /// <summary>
  /// The flag is true if there is Inventory Item which has TemplateItemId equals current InventoryID value.
  /// </summary>
  [PXBool]
  public virtual bool? HasChild { get; set; }

  /// <summary>
  /// References to parent Group <see cref="!:INAttributeDescriptionGroup.GroupID" />
  /// </summary>
  [PXDBInt]
  public virtual int? AttributeDescriptionGroupID { get; set; }

  /// <summary>Value of column matrix attribute of template item.</summary>
  [PXAttributeValue]
  public virtual string ColumnAttributeValue { get; set; }

  /// <summary>Value of row matrix attribute of template item.</summary>
  [PXAttributeValue]
  public virtual string RowAttributeValue { get; set; }

  /// <summary>
  /// Contains Inventory ID Example <see cref="T:PX.Objects.IN.Matrix.DAC.Projections.IDGenerationRule" />
  /// </summary>
  [PXString]
  public virtual string SampleID { get; set; }

  /// <summary>
  /// Contains Inventory Description Example <see cref="T:PX.Objects.IN.Matrix.DAC.Projections.DescriptionGenerationRule" />
  /// </summary>
  [PXString]
  public virtual string SampleDescription { get; set; }

  /// <summary>
  /// If true, only items selected in the Matrix Items list will be updated with the template changes on Update Matrix Items action.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Update Only Selected Items with Template Changes")]
  [PXDefault(false)]
  public virtual bool? UpdateOnlySelected { get; set; }

  [PXInt]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual int? DiscAcctID
  {
    get => new int?();
    set
    {
    }
  }

  [PXInt]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual int? DiscSubID
  {
    get => new int?();
    set
    {
    }
  }

  [PXDBString(1, IsUnicode = false)]
  [PXUIField(DisplayName = "Visibility", Visible = false)]
  [PXDefault("X")]
  public virtual string Visibility { get; set; }

  [PXDBString(1, IsUnicode = false)]
  [PXUIField(DisplayName = "Availability", Visible = false)]
  [PXDefault("X")]
  public virtual string Availability { get; set; }

  [PXDBString(1, IsUnicode = false)]
  [PXUIField(DisplayName = "When Qty Unavailable", Visible = false)]
  [PXDefault("X")]
  public virtual string NotAvailMode { get; set; }

  /// <summary>
  /// Value to adjust availability when exporting availability to external eStores.
  /// </summary>
  /// <example>If ERP has 200 items and user wants to display 190 in eStore, then <see cref="P:PX.Objects.IN.InventoryItem.AvailabilityAdjustment" /> value should be set to -10.</example>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Availability Adjustment")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? AvailabilityAdjustment { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true, typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.exportToExternal), CacheGlobal = true)]
  public virtual bool? ExportToExternal
  {
    get => this._ExportToExternal;
    set => this._ExportToExternal = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Special-Order Item", FieldClass = "SpecialOrders")]
  [PXDefault(false)]
  public virtual bool? IsSpecialOrderItem { get; set; }

  /// <summary>Replenishment source</summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [PXDefault("P", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.replenishmentSource), CacheGlobal = true)]
  [INReplenishmentSource.List]
  public string ReplenishmentSource { get; set; }

  /// <summary>
  /// Planning method - Decide which planning method applicable for the stock item.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Planning Method", FieldClass = "InvPlanning")]
  [PXDefault("N", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.planningMethod), CacheGlobal = true)]
  [INPlanningMethod.List]
  public string PlanningMethod { get; set; }

  public class PK : PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>
  {
    public static InventoryItem Find(PXGraph graph, int? inventoryID, PKFindOptions options = 0)
    {
      return (InventoryItem) PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.FindBy(graph, (object) inventoryID, options);
    }

    public static InventoryItem FindDirty(PXGraph graph, int? inventoryID)
    {
      return PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
      {
        (object) inventoryID
      }));
    }
  }

  public class UK : PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryCD>
  {
    public static InventoryItem Find(PXGraph graph, string inventoryCD, PKFindOptions options = 0)
    {
      return (InventoryItem) PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryCD>.FindBy(graph, (object) inventoryCD, options);
    }
  }

  public static class FK
  {
    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.taxCategoryID>
    {
    }

    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.salesAcctID>
    {
    }

    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.salesSubID>
    {
    }

    public class InventoryAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.invtAcctID>
    {
    }

    public class InventorySubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.invtSubID>
    {
    }

    public class COGSAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.cOGSAcctID>
    {
    }

    public class COGSSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.cOGSSubID>
    {
    }

    public class StandardCostRevaluationAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.stdCstRevAcctID>
    {
    }

    public class StandardCostRevaluationSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.stdCstRevSubID>
    {
    }

    public class PPVAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.pPVAcctID>
    {
    }

    public class PPVSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.pPVSubID>
    {
    }

    public class DeferralAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.deferralAcctID>
    {
    }

    public class DeferralSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.deferralSubID>
    {
    }

    public class LastSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.lastSiteID>
    {
    }

    public class StandardCostVarianceAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.stdCstVarAcctID>
    {
    }

    public class StandardCostVarianceSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.stdCstVarSubID>
    {
    }

    public class POAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.pOAccrualAcctID>
    {
    }

    public class POAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.pOAccrualSubID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.itemClassID>
    {
    }

    public class PostClass : 
      PrimaryKeyOf<INPostClass>.By<INPostClass.postClassID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.postClassID>
    {
    }

    [Obsolete("This foreign key is obsolete and is going to be removed in 2022R1. Use InventoryItemCurySettings.DefaultSite instead.")]
    public class DefaultSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.dfltSiteID>
    {
    }

    public class DefaultSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.defaultSubItemID>
    {
    }

    public class PriceClass : 
      PrimaryKeyOf<INPriceClass>.By<INPriceClass.priceClassID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.priceClassID>
    {
    }

    public class ReasonCodeSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.reasonCodeSubID>
    {
    }

    public class PICycle : 
      PrimaryKeyOf<INPICycle>.By<INPICycle.cycleID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.cycleID>
    {
    }

    public class LotSerialClass : 
      PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.lotSerClassID>
    {
    }

    public class LandedCostVarianceAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.lCVarianceAcctID>
    {
    }

    public class LandedCostVarianceSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.lCVarianceSubID>
    {
    }

    public class ABCCode : 
      PrimaryKeyOf<INABCCode>.By<INABCCode.aBCCodeID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.aBCCodeID>
    {
    }

    public class ProductWorkgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.productWorkgroupID>
    {
    }

    public class ProductManager : 
      PrimaryKeyOf<PX.Objects.CR.Standalone.EPEmployee>.By<PX.Objects.CR.Standalone.EPEmployee.bAccountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.productManagerID>
    {
    }

    public class PriceWorkgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.priceWorkgroupID>
    {
    }

    public class PriceManager : 
      PrimaryKeyOf<PX.Objects.CR.Standalone.EPEmployee>.By<PX.Objects.CR.Standalone.EPEmployee.bAccountID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.priceManagerID>
    {
    }

    public class DeferredCode : 
      PrimaryKeyOf<DRDeferredCode>.By<DRDeferredCode.deferredCodeID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.deferredCode>
    {
    }

    public class MovementClass : 
      PrimaryKeyOf<INMovementClass>.By<INMovementClass.movementClassID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.movementClassID>
    {
    }

    public class CountryOfOrigin : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<InventoryItem>.By<InventoryItem.countryOfOrigin>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.inventoryID>
  {
  }

  public abstract class inventoryCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.inventoryCD>
  {
  }

  public abstract class isConverted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.isConverted>
  {
  }

  public abstract class isConversionMode : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.isConversionMode>
  {
  }

  public abstract class stkItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.stkItem>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.descr>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.itemClassID>
  {
  }

  public abstract class parentItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.parentItemClassID>
  {
  }

  public abstract class itemStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.itemStatus>
  {
  }

  public abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.itemType>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.valMethod>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.taxCategoryID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.taxCalcMode>
  {
  }

  public abstract class weightItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.weightItem>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.baseUnit>
  {
    public class PreventEditIfExists<TSelect> : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<InventoryItem.baseUnit>>.On<InventoryItemMaintBase>.IfExists<TSelect>
      where TSelect : BqlCommand, new()
    {
      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        firstPreventingEntity = (object) PXResult.UnwrapMain(firstPreventingEntity);
        PXCache cach = ((PXGraph) ((PXGraphExtension<InventoryItemMaintBase>) this).Base).Caches[firstPreventingEntity.GetType()];
        return PXMessages.Localize("Base UOM cannot be changed for the item in use.") + Environment.NewLine + cach.GetRowDescription(firstPreventingEntity);
      }
    }
  }

  public abstract class salesUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.salesUnit>
  {
  }

  public abstract class purchaseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.purchaseUnit>
  {
  }

  public abstract class decimalBaseUnit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.decimalBaseUnit>
  {
  }

  public abstract class decimalSalesUnit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.decimalSalesUnit>
  {
  }

  public abstract class decimalPurchaseUnit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.decimalPurchaseUnit>
  {
  }

  public abstract class commisionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.commisionable>
  {
  }

  public abstract class postClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.postClassID>
  {
  }

  public abstract class reasonCodeSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.reasonCodeSubID>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.salesSubID>
  {
  }

  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.invtAcctID>
  {
    /// <exclude />
    public class WarnIfNonZeroCostLayerExists : 
      WarnIfNonZeroCostLayerExists<InventoryItem.invtAcctID, InventoryItemMaint>
    {
      public static bool IsActive() => true;
    }
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.invtSubID>
  {
  }

  public abstract class cOGSAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.cOGSAcctID>
  {
  }

  public abstract class cOGSSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.cOGSSubID>
  {
  }

  public abstract class expenseAccrualAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.expenseAccrualAcctID>
  {
  }

  public abstract class expenseAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.expenseAccrualSubID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.expenseSubID>
  {
  }

  public abstract class stdCstRevAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.stdCstRevAcctID>
  {
  }

  public abstract class stdCstRevSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.stdCstRevSubID>
  {
  }

  public abstract class stdCstVarAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.stdCstVarAcctID>
  {
  }

  public abstract class stdCstVarSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.stdCstVarSubID>
  {
  }

  public abstract class pPVAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.pPVAcctID>
  {
  }

  public abstract class pPVSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.pPVSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.pOAccrualSubID>
  {
  }

  public abstract class lCVarianceAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.lCVarianceAcctID>
  {
  }

  public abstract class lCVarianceSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.lCVarianceSubID>
  {
  }

  public abstract class deferralAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.deferralAcctID>
  {
  }

  public abstract class deferralSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.deferralSubID>
  {
  }

  public abstract class lastSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.lastSiteID>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.LastStdCost instead.")]
  public abstract class lastStdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryItem.lastStdCost>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.PendingStdCost instead.")]
  public abstract class pendingStdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.pendingStdCost>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.PendingStdCostDate instead.")]
  public abstract class pendingStdCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItem.pendingStdCostDate>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.StdCost instead.")]
  public abstract class stdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryItem.stdCost>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.StdCostDate instead.")]
  public abstract class stdCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItem.stdCostDate>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.BasePrice instead.")]
  public abstract class basePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryItem.basePrice>
  {
  }

  public abstract class baseWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryItem.baseWeight>
  {
  }

  public abstract class baseVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryItem.baseVolume>
  {
  }

  public abstract class baseItemWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.baseItemWeight>
  {
  }

  public abstract class baseItemVolume : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.baseItemVolume>
  {
  }

  public abstract class weightUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.weightUOM>
  {
  }

  public abstract class volumeUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.volumeUOM>
  {
  }

  public abstract class packSeparately : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.packSeparately>
  {
  }

  public abstract class packageOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.packageOption>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.PreferredVendorID instead.")]
  public abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.preferredVendorID>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.PreferredVendorLocationID instead.")]
  public abstract class preferredVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.preferredVendorLocationID>
  {
  }

  public abstract class defaultSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.defaultSubItemID>
  {
  }

  public abstract class defaultSubItemOnEntry : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.defaultSubItemOnEntry>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.DfltSiteID instead.")]
  public abstract class dfltSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.dfltSiteID>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.DfltShipLocationID instead.")]
  public abstract class dfltShipLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.dfltShipLocationID>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.DfltReceiptLocationID instead.")]
  public abstract class dfltReceiptLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.dfltReceiptLocationID>
  {
  }

  public abstract class defaultCostCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.defaultCostCodeID>
  {
  }

  public abstract class productWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.productWorkgroupID>
  {
  }

  public abstract class productManagerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.productManagerID>
  {
  }

  public abstract class priceWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.priceWorkgroupID>
  {
  }

  public abstract class priceManagerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.priceManagerID>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.negQty>
  {
  }

  public abstract class origLotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.origLotSerClassID>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.lotSerClassID>
  {
  }

  public abstract class deferredCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.deferredCode>
  {
  }

  public abstract class defaultTerm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryItem.defaultTerm>
  {
  }

  public abstract class defaultTermUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.defaultTermUOM>
  {
  }

  public abstract class priceClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.priceClassID>
  {
  }

  public abstract class isSplitted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.isSplitted>
  {
  }

  public abstract class useParentSubID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.useParentSubID>
  {
  }

  public abstract class totalPercentage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.totalPercentage>
  {
  }

  public abstract class kitItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.kitItem>
  {
  }

  public abstract class minGrossProfitPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.minGrossProfitPct>
  {
  }

  public abstract class nonStockReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.nonStockReceipt>
  {
  }

  public abstract class nonStockReceiptAsService : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.nonStockReceiptAsService>
  {
  }

  public abstract class nonStockShip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.nonStockShip>
  {
  }

  public abstract class postToExpenseAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.postToExpenseAccount>
  {
    public const string Purchases = "P";
    public const string Sales = "S";

    public class purchases : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      InventoryItem.postToExpenseAccount.purchases>
    {
      public purchases()
        : base("P")
      {
      }
    }

    public class sales : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      InventoryItem.postToExpenseAccount.sales>
    {
      public sales()
        : base("S")
      {
      }
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("P", "Purchases"),
          PXStringListAttribute.Pair("S", "Sales")
        })
      {
      }
    }

    [PXLocalizable]
    public static class DisplayName
    {
      public const string Purchases = "Purchases";
      public const string Sales = "Sales";
    }
  }

  public abstract class costBasis : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.costBasis>
  {
  }

  public abstract class percentOfSalesPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.percentOfSalesPrice>
  {
  }

  public abstract class completePOLine : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.completePOLine>
  {
  }

  public abstract class preferredItemClasses : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.preferredItemClasses>
  {
  }

  public abstract class priceOfSuggestedItems : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.priceOfSuggestedItems>
  {
  }

  public abstract class aBCCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.aBCCodeID>
  {
  }

  public abstract class aBCCodeIsFixed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.aBCCodeIsFixed>
  {
  }

  public abstract class movementClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.movementClassID>
  {
  }

  public abstract class movementClassIsFixed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.movementClassIsFixed>
  {
  }

  public abstract class markupPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryItem.markupPct>
  {
  }

  [Obsolete("This field is obsolete and will be removed in the later Acumatica versions. See AC-204053. Use InventoryItemCurySettings.RecPrice instead.")]
  public abstract class recPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryItem.recPrice>
  {
  }

  public abstract class imageUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.imageUrl>
  {
  }

  public abstract class commodityCodeType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.commodityCodeType>
  {
  }

  public abstract class hSTariffCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.hSTariffCode>
  {
  }

  public abstract class undershipThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.undershipThreshold>
  {
  }

  public abstract class overshipThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.overshipThreshold>
  {
  }

  public abstract class countryOfOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.countryOfOrigin>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  InventoryItem.noteID>
  {
  }

  public abstract class defaultInventorySourceForProjects : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.defaultInventorySourceForProjects>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  InventoryItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventoryItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItem.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  InventoryItem.Tstamp>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  InventoryItem.groupMask>
  {
  }

  public abstract class cycleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.cycleID>
  {
  }

  public abstract class attributes : 
    BqlType<IBqlAttributes, string[]>.Field<InventoryItem.attributes>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.included>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.body>
  {
  }

  public abstract class isTemplate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItem.isTemplate>
  {
  }

  public abstract class templateItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.templateItemID>
  {
  }

  public abstract class defaultRowMatrixAttributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.defaultRowMatrixAttributeID>
  {
  }

  public abstract class defaultColumnMatrixAttributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.defaultColumnMatrixAttributeID>
  {
  }

  public abstract class generationRuleCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.generationRuleCntr>
  {
  }

  public abstract class hasChild : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.hasChild>
  {
  }

  public abstract class attributeDescriptionGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItem.attributeDescriptionGroupID>
  {
  }

  public abstract class columnAttributeValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.columnAttributeValue>
  {
  }

  public abstract class rowAttributeValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.rowAttributeValue>
  {
  }

  public abstract class sampleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.sampleID>
  {
  }

  public abstract class sampleDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.sampleDescription>
  {
  }

  public abstract class updateOnlySelected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.updateOnlySelected>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class discAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.discAcctID>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class discSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItem.discSubID>
  {
  }

  public abstract class visibility : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.visibility>
  {
  }

  public abstract class availability : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.availability>
  {
  }

  public abstract class notAvailMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItem.notAvailMode>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.AvailabilityAdjustment" />
  public abstract class availabilityAdjustment : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItem.availabilityAdjustment>
  {
  }

  public abstract class exportToExternal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.exportToExternal>
  {
  }

  public abstract class isSpecialOrderItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItem.isSpecialOrderItem>
  {
  }

  public abstract class replenishmentSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.replenishmentSource>
  {
  }

  public abstract class planningMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItem.planningMethod>
  {
  }
}
