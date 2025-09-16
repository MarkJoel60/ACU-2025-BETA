// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.DAC.Projections.INCrossSellSuggestionProcessResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems.DAC.Projections;

/// <exclude />
[PXProjection(typeof (SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<INRelatedInventory.inventoryID, IBqlInt>.IsEqual<PX.Objects.IN.InventoryItem.inventoryID>>>, FbqlJoins.Left<INItemClass>.On<BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.IsEqual<INItemClass.itemClassID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventory.createdByPossibleRelatedItem, Equal<True>>>>, And<BqlOperand<INRelatedInventory.relation, IBqlString>.IsEqual<InventoryRelation.crossSell>>>>.And<BqlOperand<INRelatedInventory.acceptedMLSuggestion, IBqlBool>.IsEqual<False>>>))]
[PXHidden]
public class INCrossSellSuggestionProcessResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [AnyInventory(IsKey = true, BqlField = typeof (INRelatedInventory.inventoryID), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr), DisplayName = "Original Item ID", Enabled = false)]
  public virtual int? OriginalInventoryID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Original Item Description", Enabled = false)]
  [PXFormula(typeof (Selector<INCrossSellSuggestionProcessResult.originalInventoryID, PX.Objects.IN.InventoryItem.descr>))]
  public virtual 
  #nullable disable
  string OriginalDescr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INRelatedInventory.lineID))]
  public virtual int? LineID { get; set; }

  [AnyInventory(BqlField = typeof (INRelatedInventory.relatedInventoryID), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr), DisplayName = "Cross-Sell Item ID", Enabled = false)]
  public virtual int? CrossSellInventoryID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Cross-Sell Item Description", Enabled = false)]
  [PXFormula(typeof (Selector<INCrossSellSuggestionProcessResult.crossSellInventoryID, PX.Objects.IN.InventoryItem.descr>))]
  public virtual string CrossSellDescr { get; set; }

  [PXDBInt(BqlField = typeof (INRelatedInventory.rank))]
  [PXUIField(DisplayName = "Rank", Enabled = false)]
  public virtual int? Rank { get; set; }

  [PXDBString(4, IsFixed = true, BqlField = typeof (INRelatedInventory.tag))]
  [PXUIField(DisplayName = "Tag", Enabled = false)]
  [InventoryRelationTag.List]
  public virtual string Tag { get; set; }

  [INUnit(typeof (INCrossSellSuggestionProcessResult.crossSellInventoryID), BqlField = typeof (INRelatedInventory.uom))]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (INCrossSellSuggestionProcessResult.uom), typeof (INCrossSellSuggestionProcessResult.baseQty), MinValue = 0.0, HandleEmptyKey = true, BqlField = typeof (INRelatedInventory.qty))]
  [PXUIField(DisplayName = "Quantity")]
  [PXDefault]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, BqlField = typeof (INRelatedInventory.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBDate(BqlField = typeof (INRelatedInventory.effectiveDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Effective Date")]
  public virtual DateTime? EffectiveDate { get; set; }

  [PXDBDate(BqlField = typeof (INRelatedInventory.expirationDate))]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual DateTime? ExpirationDate { get; set; }

  [PXDBDecimal(16 /*0x10*/, MinValue = 0.0, MaxValue = 1.0, BqlField = typeof (INRelatedInventory.mLScore))]
  [PXUIField(DisplayName = "Relevance Score", Enabled = false)]
  public virtual Decimal? MLScore { get; set; }

  [PXDBInt(BqlField = typeof (INItemClass.itemClassID))]
  [PXUIField(DisplayName = "Original Item Class ID", Visible = false, Enabled = false)]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual int? OriginalItemClassID { get; set; }

  [PXDBString(BqlField = typeof (INItemClass.descr))]
  [PXUIField(DisplayName = "Original Item Class Description", Visible = false, Enabled = false)]
  public virtual string OriginalItemClassDescr { get; set; }

  [PXDBString(5, IsFixed = true, BqlField = typeof (INRelatedInventory.relation))]
  public virtual string Relation { get; set; }

  [PXDBBool(BqlField = typeof (INRelatedInventory.required))]
  [PXUIField(DisplayName = "Required", Visible = false, Enabled = false)]
  public virtual bool? Required { get; set; }

  [PXDBBool(BqlField = typeof (INRelatedInventory.isActive))]
  [PXUIField(DisplayName = "Active", Visible = false, Enabled = false)]
  public virtual bool? IsActive { get; set; }

  public abstract class selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.selected>
  {
  }

  public abstract class originalInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.originalInventoryID>
  {
  }

  public abstract class originalDescr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.originalDescr>
  {
  }

  public abstract class lineID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.lineID>
  {
  }

  public abstract class crossSellInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.crossSellInventoryID>
  {
  }

  public abstract class crossSellDescr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.crossSellDescr>
  {
  }

  public abstract class rank : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCrossSellSuggestionProcessResult.rank>
  {
  }

  public abstract class tag : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.tag>
  {
  }

  public abstract class uom : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.uom>
  {
  }

  public abstract class qty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.qty>
  {
  }

  public abstract class baseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.baseQty>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.effectiveDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.expirationDate>
  {
  }

  public abstract class mLScore : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.mLScore>
  {
  }

  public abstract class originalItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.originalItemClassID>
  {
  }

  public abstract class originalItemClassDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.originalItemClassDescr>
  {
  }

  public abstract class relation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.relation>
  {
  }

  public abstract class required : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.required>
  {
  }

  public abstract class isActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INCrossSellSuggestionProcessResult.isActive>
  {
  }
}
