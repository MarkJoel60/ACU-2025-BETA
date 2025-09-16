// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.Unbound.EntryHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC.Unbound;

[PXCacheName("Entity Header")]
public class EntryHeader : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Template Item")]
  [TemplateInventory(DirtyRead = true)]
  public virtual int? TemplateItemID { get; set; }

  [PXUIField(DisplayName = "Description", Enabled = false)]
  [PXString]
  public virtual 
  #nullable disable
  string Description { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Column Attribute ID")]
  [PXDefault(typeof (Search<InventoryItem.defaultColumnMatrixAttributeID, Where<InventoryItem.inventoryID, Equal<Current<EntryHeader.templateItemID>>>>))]
  [PXFormula(typeof (Default<EntryHeader.templateItemID>))]
  [MatrixAttributeSelector(typeof (Search2<CSAttributeGroup.attributeID, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<Current<EntryHeader.templateItemID>>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<InventoryItem.itemClassID>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>, And<NotExists<Select<CSAnswers, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.isActive, Equal<False>>>>, And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<CSAttributeGroup.attributeID>>>>.And<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<InventoryItem.noteID>>>>>>>>>>>), typeof (EntryHeader.rowAttributeID), true, new Type[] {typeof (CSAttributeGroup.attributeID)}, DescriptionField = typeof (CSAttributeGroup.description))]
  public virtual string ColAttributeID { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Row Attribute ID")]
  [PXDefault(typeof (Search<InventoryItem.defaultRowMatrixAttributeID, Where<InventoryItem.inventoryID, Equal<Current<EntryHeader.templateItemID>>>>))]
  [PXFormula(typeof (Default<EntryHeader.templateItemID>))]
  [MatrixAttributeSelector(typeof (Search2<CSAttributeGroup.attributeID, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<Current<EntryHeader.templateItemID>>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<InventoryItem.itemClassID>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>, And<NotExists<Select<CSAnswers, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.isActive, Equal<False>>>>, And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<CSAttributeGroup.attributeID>>>>.And<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<InventoryItem.noteID>>>>>>>>>>>), typeof (EntryHeader.colAttributeID), true, new Type[] {typeof (CSAttributeGroup.attributeID)}, DescriptionField = typeof (CSAttributeGroup.description))]
  public virtual string RowAttributeID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Display Availability Details")]
  public virtual bool? ShowAvailable { get; set; }

  [Site(DescriptionField = typeof (INSite.descr))]
  public virtual int? SiteID { get; set; }

  [Location(typeof (EntryHeader.siteID), KeepEntry = false, DescriptionField = typeof (INLocation.descr))]
  public virtual int? LocationID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("AA")]
  [PX.Objects.IN.Matrix.Attributes.PlanType.List]
  [PXUIField(DisplayName = "Plan Type")]
  public virtual string DisplayPlanType { get; set; }

  [PXString(1, IsFixed = true)]
  [PXDefault("L")]
  public virtual string SmartPanelType { get; set; }

  public abstract class templateItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EntryHeader.templateItemID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EntryHeader.description>
  {
  }

  public abstract class colAttributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EntryHeader.colAttributeID>
  {
  }

  public abstract class rowAttributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EntryHeader.rowAttributeID>
  {
  }

  public abstract class showAvailable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EntryHeader.showAvailable>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EntryHeader.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EntryHeader.locationID>
  {
  }

  public abstract class displayPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EntryHeader.displayPlanType>
  {
  }

  public abstract class smartPanelType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EntryHeader.smartPanelType>
  {
    public const string Entry = "E";
    public const string Lookup = "L";
  }
}
