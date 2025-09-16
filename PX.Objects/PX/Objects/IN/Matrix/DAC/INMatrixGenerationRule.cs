// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.INMatrixGenerationRule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC;

[PXCacheName("Matrix Generation Rule")]
public class INMatrixGenerationRule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Template Inventory Item identifier or Item Class identifier (depends on <see cref="P:PX.Objects.IN.Matrix.DAC.INMatrixGenerationRule.ParentType" />).
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  public virtual int? ParentID { get; set; }

  /// <summary>
  /// Type of ParentID value: C - Item Class, T - Template Inventory Item.
  /// </summary>
  [PXDBString(1, IsUnicode = false, IsFixed = true, IsKey = true)]
  [INMatrixGenerationRule.parentType.List]
  [PXDefault("T")]
  public virtual 
  #nullable disable
  string ParentType { get; set; }

  [PXDBString(1, IsKey = true, IsFixed = true, IsUnicode = false)]
  [INMatrixGenerationRule.type.List]
  public virtual string Type { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (InventoryItem.generationRuleCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Segment Type")]
  [INMatrixGenerationRule.segmentType.List]
  public virtual string SegmentType { get; set; }

  /// <summary>
  /// References to Attribute which will be put as part of result string.
  /// </summary>
  [DefaultConditional(typeof (INMatrixGenerationRule.segmentType), new object[] {"AC", "AV"})]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Attribute ID", Required = true)]
  [PXSelector(typeof (Search2<CSAnswers.attributeID, InnerJoin<CSAttributeGroup, On<BqlOperand<CSAttributeGroup.attributeID, IBqlString>.IsEqual<CSAnswers.attributeID>>>, Where<CSAnswers.refNoteID, Equal<Current<InventoryItem.noteID>>, And<CSAnswers.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>>>>>>), new System.Type[] {typeof (CSAttributeGroup.attributeID), typeof (CSAttributeGroup.description)}, DirtyRead = true)]
  [PXRestrictor(typeof (Where<CSAttributeGroup.isActive, Equal<True>>), "The {0} attribute is inactive. Specify an active attribute.", new System.Type[] {typeof (CSAttributeGroup.attributeID)})]
  public virtual string AttributeID { get; set; }

  /// <summary>User text which will be put as part of result string.</summary>
  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Constant")]
  public virtual string Constant { get; set; }

  [DefaultConditional(typeof (INMatrixGenerationRule.segmentType), new object[] {"AN"})]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Numbering ID", Required = true)]
  [PXSelector(typeof (PX.Objects.CS.Numbering.numberingID))]
  public virtual string NumberingID { get; set; }

  /// <summary>Number of characters for this part in result string.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Number of Characters", Required = true)]
  [PXDefault]
  public virtual int? NumberOfCharacters { get; set; }

  /// <summary>Use a Space as Separator after this part.</summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Use Space as Separator")]
  [PXDefault(false)]
  public virtual bool? UseSpaceAsSeparator { get; set; }

  /// <summary>Separator after this part.</summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Separator")]
  [PXDefault("-")]
  public virtual string Separator { get; set; }

  [PXUIField(DisplayName = "Line Order", Visible = false, Enabled = false)]
  [PXDBInt]
  public virtual int? SortOrder { get; set; }

  /// <summary>
  /// If true and length of the result string is less than number of characters (<see cref="P:PX.Objects.IN.Matrix.DAC.INMatrixGenerationRule.NumberOfCharacters" />), the system will add spaces.
  /// </summary>
  [PXUIField(DisplayName = "Add Spaces")]
  [PXDefault]
  [PXDBBool]
  public virtual bool? AddSpaces { get; set; }

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
    PrimaryKeyOf<INMatrixGenerationRule>.By<INMatrixGenerationRule.parentType, INMatrixGenerationRule.parentID, INMatrixGenerationRule.type, INMatrixGenerationRule.lineNbr>
  {
    public static INMatrixGenerationRule Find(
      PXGraph graph,
      string parentType,
      int? parentID,
      string type,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (INMatrixGenerationRule) PrimaryKeyOf<INMatrixGenerationRule>.By<INMatrixGenerationRule.parentType, INMatrixGenerationRule.parentID, INMatrixGenerationRule.type, INMatrixGenerationRule.lineNbr>.FindBy(graph, (object) parentType, (object) parentID, (object) type, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class TemplateInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INMatrixGenerationRule>.By<INMatrixGenerationRule.parentID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INMatrixGenerationRule>.By<INMatrixGenerationRule.parentID>
    {
    }

    public class Attribute : 
      PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>.ForeignKeyOf<INMatrixGenerationRule>.By<INMatrixGenerationRule.attributeID>
    {
    }

    public class Numbering : 
      PrimaryKeyOf<PX.Objects.CS.Numbering>.By<PX.Objects.CS.Numbering.numberingID>.ForeignKeyOf<INMatrixGenerationRule>.By<INMatrixGenerationRule.numberingID>
    {
    }
  }

  public abstract class parentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INMatrixGenerationRule.parentID>
  {
  }

  public abstract class parentType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INMatrixGenerationRule.parentType>
  {
    public const string TemplateItem = "T";
    public const string ItemClass = "C";

    [PXLocalizable]
    public class DisplayNames
    {
      public const string TemplateItem = "Template Inventory Item";
      public const string ItemClass = "Item Class";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("T", "Template Inventory Item"),
          PXStringListAttribute.Pair("C", "Item Class")
        })
      {
      }
    }

    public class templateItem : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INMatrixGenerationRule.parentType.templateItem>
    {
      public templateItem()
        : base("T")
      {
      }
    }

    public class itemClass : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INMatrixGenerationRule.parentType.itemClass>
    {
      public itemClass()
        : base("C")
      {
      }
    }
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INMatrixGenerationRule.type>
  {
    public const string ID = "I";
    public const string Description = "D";

    [PXLocalizable]
    public class DisplayNames
    {
      public const string ID = "ID";
      public const string Description = "Description";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("I", "ID"),
          PXStringListAttribute.Pair("D", "Description")
        })
      {
      }
    }

    public class id : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INMatrixGenerationRule.type.id>
    {
      public id()
        : base("I")
      {
      }
    }

    public class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INMatrixGenerationRule.type.description>
    {
      public description()
        : base("D")
      {
      }
    }
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INMatrixGenerationRule.lineNbr>
  {
  }

  public abstract class segmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMatrixGenerationRule.segmentType>
  {
    public const string TemplateID = "TI";
    public const string TemplateDescription = "TD";
    public const string AttributeCaption = "AC";
    public const string AttributeValue = "AV";
    public const string Constant = "CO";
    public const string AutoNumber = "AN";
    public const string Space = "SP";

    [PXLocalizable]
    public class DisplayNames
    {
      public const string TemplateID = "Template ID";
      public const string TemplateDescription = "Template Description";
      public const string AttributeCaption = "Attribute Caption";
      public const string AttributeValue = "Attribute Value";
      public const string Constant = "Constant";
      public const string AutoNumber = "Auto Number";
      public const string Space = "Space";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[7]
        {
          PXStringListAttribute.Pair("TI", "Template ID"),
          PXStringListAttribute.Pair("TD", "Template Description"),
          PXStringListAttribute.Pair("AC", "Attribute Caption"),
          PXStringListAttribute.Pair("AV", "Attribute Value"),
          PXStringListAttribute.Pair("CO", "Constant"),
          PXStringListAttribute.Pair("SP", "Space"),
          PXStringListAttribute.Pair("AN", "Auto Number")
        })
      {
      }
    }
  }

  public abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMatrixGenerationRule.attributeID>
  {
  }

  public abstract class constant : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INMatrixGenerationRule.constant>
  {
  }

  public abstract class numberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMatrixGenerationRule.numberingID>
  {
  }

  public abstract class numberOfCharacters : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INMatrixGenerationRule.numberOfCharacters>
  {
  }

  public abstract class useSpaceAsSeparator : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INMatrixGenerationRule.useSpaceAsSeparator>
  {
  }

  public abstract class separator : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMatrixGenerationRule.separator>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INMatrixGenerationRule.sortOrder>
  {
  }

  public abstract class addSpaces : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INMatrixGenerationRule.addSpaces>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INMatrixGenerationRule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMatrixGenerationRule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INMatrixGenerationRule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INMatrixGenerationRule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMatrixGenerationRule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INMatrixGenerationRule.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INMatrixGenerationRule.Tstamp>
  {
  }
}
