// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.Projections.DescriptionGenerationRule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC.Projections;

[PXCacheName("Description Generation Rule")]
[PXBreakInheritance]
[PXProjection(typeof (Select<INMatrixGenerationRule, Where<INMatrixGenerationRule.type, Equal<INMatrixGenerationRule.type.description>>>), Persistent = true)]
public class DescriptionGenerationRule : INMatrixGenerationRule
{
  /// <summary>Template Inventory Item identifier.</summary>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  [PXParent(typeof (DescriptionGenerationRule.FK.TemplateInventoryItem))]
  public override int? ParentID
  {
    get => base.ParentID;
    set => base.ParentID = value;
  }

  [PXDBString(1, IsKey = true, IsFixed = true, IsUnicode = false)]
  [INMatrixGenerationRule.type.List]
  [PXDefault("D")]
  public override 
  #nullable disable
  string Type { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (InventoryItem.generationRuleCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public override int? LineNbr
  {
    get => base.LineNbr;
    set => base.LineNbr = value;
  }

  public new class PK : 
    PrimaryKeyOf<DescriptionGenerationRule>.By<DescriptionGenerationRule.parentType, DescriptionGenerationRule.parentID, DescriptionGenerationRule.type, DescriptionGenerationRule.lineNbr>
  {
    public static DescriptionGenerationRule Find(
      PXGraph graph,
      string parentType,
      int? parentID,
      string type,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (DescriptionGenerationRule) PrimaryKeyOf<DescriptionGenerationRule>.By<DescriptionGenerationRule.parentType, DescriptionGenerationRule.parentID, DescriptionGenerationRule.type, DescriptionGenerationRule.lineNbr>.FindBy(graph, (object) parentType, (object) parentID, (object) type, (object) lineNbr, options);
    }
  }

  public new static class FK
  {
    public class TemplateInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<DescriptionGenerationRule>.By<DescriptionGenerationRule.parentID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<DescriptionGenerationRule>.By<DescriptionGenerationRule.parentID>
    {
    }
  }

  public new abstract class parentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DescriptionGenerationRule.parentID>
  {
  }

  public new abstract class parentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DescriptionGenerationRule.parentType>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DescriptionGenerationRule.type>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DescriptionGenerationRule.lineNbr>
  {
  }

  public new abstract class sortOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DescriptionGenerationRule.sortOrder>
  {
  }

  public new abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DescriptionGenerationRule.attributeID>
  {
  }

  public new abstract class addSpaces : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DescriptionGenerationRule.addSpaces>
  {
  }
}
