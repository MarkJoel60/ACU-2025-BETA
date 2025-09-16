// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.INRelatedInventory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

/// <summary>
/// Represents Related Items for stock- and non-stock- items.
/// The records of this type are created and edited through the Realetd Items tab in the Stock Items (IN202500) form
/// (which corresponds to the <see cref="T:PX.Objects.IN.InventoryItemMaint" /> graph) and
/// the Non-Stock Items (IN202000) form (which corresponds to the <see cref="T:PX.Objects.IN.NonStockItemMaint" /> graph) screens.
/// The cache of this type in the graphs is active if one or both of the following features are enabled: <see cref="T:PX.Objects.CS.FeaturesSet.relatedItems" /> and <see cref="T:PX.Objects.CS.FeaturesSet.commerceIntegration" />.
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (NonStockItemMaint), typeof (InventoryItemMaint)}, new Type[] {typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>), typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>)})]
[PXCacheName]
[Serializable]
public class INRelatedInventory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  [PXParent(typeof (INRelatedInventory.FK.InventoryItem))]
  [PXUIEnabled(typeof (Where<BqlOperand<INRelatedInventory.createdByPossibleRelatedItem, IBqlBool>.IsEqual<False>>))]
  public virtual int? InventoryID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? LineID { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Relation")]
  [PXDefault("CSELL")]
  [InventoryRelation.List]
  [PXUIEnabled(typeof (Where<BqlOperand<INRelatedInventory.createdByPossibleRelatedItem, IBqlBool>.IsEqual<False>>))]
  public virtual 
  #nullable disable
  string Relation { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Rank")]
  [PXDefault]
  public virtual int? Rank { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Tag")]
  [PXDefault("RLTD")]
  [InventoryRelationTag.List]
  public virtual string Tag { get; set; }

  [Inventory(ValidateValue = false, SupportNewValues = true)]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsNotEqual<BqlField<INRelatedInventory.inventoryID, IBqlInt>.FromCurrent>>), "The inventory item cannot be selected as its own related item. Select another item.", new Type[] {})]
  [PXDefault]
  [PXParent(typeof (INRelatedInventory.FK.RelatedInventoryItem))]
  [PXUIEnabled(typeof (Where<BqlOperand<INRelatedInventory.createdByPossibleRelatedItem, IBqlBool>.IsEqual<False>>))]
  public virtual int? RelatedInventoryID { get; set; }

  [PXString]
  [PXUIField]
  [PXFormula(typeof (Selector<INRelatedInventory.relatedInventoryID, PX.Objects.IN.InventoryItem.descr>))]
  public virtual string Desc { get; set; }

  [INUnit(typeof (INRelatedInventory.relatedInventoryID))]
  [PXFormula(typeof (Selector<INRelatedInventory.relatedInventoryID, PX.Objects.IN.InventoryItem.baseUnit>))]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (INRelatedInventory.uom), typeof (INRelatedInventory.baseQty), MinValue = 0.0, HandleEmptyKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? EffectiveDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual DateTime? ExpirationDate { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Customer Approval Not Needed")]
  [PXUIEnabled(typeof (Where<BqlOperand<INRelatedInventory.relation, IBqlString>.IsEqual<InventoryRelation.substitute>>))]
  [PXDefault(false)]
  public virtual bool? Interchangeable { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Required")]
  [PXUIEnabled(typeof (Where<BqlOperand<INRelatedInventory.relation, IBqlString>.IsNotEqual<InventoryRelation.upSell>>))]
  [PXDefault(false)]
  public virtual bool? Required { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CreatedByPossibleRelatedItem { get; set; }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Accepted ML Suggestion", FieldClass = "RelatedItemAssistant")]
  [PXUIEnabled(typeof (Where<BqlOperand<INRelatedInventory.createdByPossibleRelatedItem, IBqlBool>.IsEqual<True>>))]
  public virtual bool? AcceptedMLSuggestion { get; set; }

  /// <exclude />
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 1.0)]
  [PXUIField(DisplayName = "Relevance Score", Enabled = false, FieldClass = "RelatedItemAssistant")]
  public virtual Decimal? MLScore { get; set; }

  [PXNote(PopupTextEnabled = true)]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<INRelatedInventory>.By<INRelatedInventory.inventoryID, INRelatedInventory.lineID>
  {
    public static INRelatedInventory Find(
      PXGraph graph,
      int? inventoryID,
      int? lineID,
      PKFindOptions options = 0)
    {
      return (INRelatedInventory) PrimaryKeyOf<INRelatedInventory>.By<INRelatedInventory.inventoryID, INRelatedInventory.lineID>.FindBy(graph, (object) inventoryID, (object) lineID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INRelatedInventory>.By<INRelatedInventory.inventoryID>
    {
    }

    public class RelatedInventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INRelatedInventory>.By<INRelatedInventory.relatedInventoryID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRelatedInventory.inventoryID>
  {
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRelatedInventory.lineID>
  {
  }

  public abstract class relation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRelatedInventory.relation>
  {
  }

  public abstract class rank : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRelatedInventory.rank>
  {
  }

  public abstract class tag : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRelatedInventory.tag>
  {
  }

  public abstract class relatedInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INRelatedInventory.relatedInventoryID>
  {
    public class InventoryBaseUnitRule : 
      PX.Objects.IN.InventoryItem.baseUnit.PreventEditIfExists<Select<INRelatedInventory, Where<INRelatedInventory.relatedInventoryID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>>>>
    {
    }
  }

  public abstract class desc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRelatedInventory.relatedInventoryID>
  {
  }

  public abstract class uom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRelatedInventory.uom>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRelatedInventory.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRelatedInventory.baseQty>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRelatedInventory.effectiveDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRelatedInventory.expirationDate>
  {
  }

  public abstract class interchangeable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRelatedInventory.interchangeable>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRelatedInventory.required>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRelatedInventory.isActive>
  {
  }

  public abstract class createdByPossibleRelatedItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRelatedInventory.createdByPossibleRelatedItem>
  {
  }

  public abstract class acceptedMLSuggestion : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRelatedInventory.acceptedMLSuggestion>
  {
  }

  public abstract class mLScore : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INRelatedInventory.mLScore>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INRelatedInventory.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INRelatedInventory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRelatedInventory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRelatedInventory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INRelatedInventory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRelatedInventory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRelatedInventory.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INRelatedInventory.Tstamp>
  {
  }
}
