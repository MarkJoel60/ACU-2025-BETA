// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUpdateStdCostRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// Represents an inventory item, that needs to update standard cost.
/// </summary>
[PXProjection(typeof (SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INItemSite>.On<KeysRelation<Field<INItemSite.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemSite>, InventoryItem, INItemSite>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.valMethod, Equal<INValMethod.standard>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.active, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.siteStatus, Equal<INItemStatus.active>>>>>.And<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>>>>>, FbqlJoins.Left<InventoryItemCurySettings>.On<KeysRelation<Field<InventoryItemCurySettings.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, InventoryItemCurySettings>, InventoryItem, InventoryItemCurySettings>.And<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<False>>>>, FbqlJoins.Left<INSite>.On<INItemSite.FK.Site>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.itemStatus, NotIn3<INItemStatus.inactive, INItemStatus.toDelete>>>>>.And<BqlOperand<InventoryItem.isTemplate, IBqlBool>.IsEqual<False>>>))]
[Serializable]
public class INUpdateStdCostRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _InventoryID;
  protected int? _SiteID;
  protected int? _InvtAcctID;
  protected int? _InvtSubID;
  protected Decimal? _PendingStdCost;
  protected DateTime? _PendingStdCostDate;
  protected Decimal? _StdCost;
  protected bool? _StdCostOverride;

  /// <summary>
  /// Specifies (if set to <see langword="true" />) than user selected record.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <inheritdoc cref="T:PX.Objects.IN.InventoryItem.inventoryID" />
  [Inventory(IsKey = true, DirtyRead = true, DisplayName = "Inventory ID", BqlField = typeof (InventoryItem.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.Descr" />
  [PXUIField(DisplayName = "Description")]
  [PXDBString(BqlField = typeof (InventoryItem.descr))]
  public virtual 
  #nullable disable
  string Descr { get; set; }

  /// <inheritdoc cref="T:PX.Objects.IN.INItemSite.siteID" />
  [Site(BqlField = typeof (INItemSite.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  /// <summary>Record identifier.</summary>
  [PXInt(IsKey = true)]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INItemSite.siteID>, int1>), typeof (int))]
  public virtual int? RecordID { get; set; }

  /// <inheritdoc cref="T:PX.Objects.IN.InventoryItem.invtAcctID" />
  [Account]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INItemSite.invtAcctID>, InventoryItem.invtAcctID>), typeof (int))]
  public virtual int? InvtAcctID
  {
    get => this._InvtAcctID;
    set => this._InvtAcctID = value;
  }

  /// <inheritdoc cref="T:PX.Objects.IN.InventoryItem.invtSubID" />
  [SubAccount(typeof (INUpdateStdCostRecord.invtAcctID))]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INItemSite.invtSubID>, InventoryItem.invtSubID>), typeof (int))]
  public virtual int? InvtSubID
  {
    get => this._InvtSubID;
    set => this._InvtSubID = value;
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INItemSite.pendingStdCost" />
  [PXPriceCost]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INItemSite.pendingStdCost>, IsNull<InventoryItemCurySettings.pendingStdCost, decimal0>>), typeof (Decimal))]
  [PXUIField(DisplayName = "Pending Cost")]
  public virtual Decimal? PendingStdCost
  {
    get => this._PendingStdCost;
    set => this._PendingStdCost = value;
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INItemSite.pendingStdCostDate" />
  [PXDate]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INItemSite.pendingStdCostDate>, InventoryItemCurySettings.pendingStdCostDate>), typeof (DateTime))]
  [PXUIField(DisplayName = "Pending Cost Date")]
  public virtual DateTime? PendingStdCostDate
  {
    get => this._PendingStdCostDate;
    set => this._PendingStdCostDate = value;
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INItemSite.pendingStdCostReset" />
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INItemSite.pendingStdCostReset>, boolFalse>), typeof (bool))]
  public virtual bool? PendingStdCostReset { get; set; }

  /// <inheritdoc cref="T:PX.Objects.IN.INItemSite.stdCost" />
  [PXPriceCost]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INItemSite.stdCost>, IsNull<InventoryItemCurySettings.stdCost, decimal0>>), typeof (Decimal))]
  [PXUIField(DisplayName = "Current Cost", Enabled = false)]
  public virtual Decimal? StdCost
  {
    get => this._StdCost;
    set => this._StdCost = value;
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INItemSite.stdCostOverride" />
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INItemSite.stdCostOverride>, boolFalse>), typeof (bool))]
  [PXUIField(DisplayName = "Std. Cost Override")]
  public virtual bool? StdCostOverride
  {
    get => this._StdCostOverride;
    set => this._StdCostOverride = value;
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INSite.baseCuryID" />
  [PXString(5, IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.stkItem, Equal<boolTrue>>, INSite.baseCuryID>, InventoryItemCurySettings.curyID>), typeof (string))]
  [PXUIField(DisplayName = "Currency", Enabled = false, FieldClass = "MultipleBaseCurrencies")]
  public virtual string CuryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.IsTemplate" />
  [PXDBBool(BqlField = typeof (InventoryItem.isTemplate))]
  public virtual bool? IsTemplate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.StkItem" />
  [PXDBBool(BqlField = typeof (InventoryItem.stkItem))]
  public virtual bool? StkItem { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.GroupMask" />
  [PXDBBinary(BqlField = typeof (InventoryItem.groupMask))]
  public virtual byte[] GroupMask { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.Selected" />
  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INUpdateStdCostRecord.selected>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.InventoryID" />
  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INUpdateStdCostRecord.inventoryID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INUpdateStdCostRecord.descr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.SiteID" />
  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INUpdateStdCostRecord.siteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.RecordID" />
  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INUpdateStdCostRecord.recordID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.InvtAcctID" />
  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INUpdateStdCostRecord.invtAcctID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.InvtSubID" />
  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INUpdateStdCostRecord.invtSubID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.PendingStdCost" />
  public abstract class pendingStdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INUpdateStdCostRecord.pendingStdCost>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.PendingStdCostDate" />
  public abstract class pendingStdCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INUpdateStdCostRecord.pendingStdCostDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.PendingStdCostReset" />
  public abstract class pendingStdCostReset : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INUpdateStdCostRecord.pendingStdCostReset>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.StdCost" />
  public abstract class stdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INUpdateStdCostRecord.stdCost>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.StdCostOverride" />
  public abstract class stdCostOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INUpdateStdCostRecord.stdCostOverride>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.CuryID" />
  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INUpdateStdCostRecord.curyID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.IsTemplate" />
  public abstract class isTemplate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INUpdateStdCostRecord.isTemplate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.StkItem" />
  public abstract class stkItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INUpdateStdCostRecord.stkItem>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INUpdateStdCostRecord.GroupMask" />
  public abstract class groupMask : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INUpdateStdCostRecord.groupMask>
  {
  }
}
