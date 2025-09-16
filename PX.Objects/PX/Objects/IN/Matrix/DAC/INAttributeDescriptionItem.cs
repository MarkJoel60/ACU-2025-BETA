// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.INAttributeDescriptionItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC;

[PXCacheName("Attribute Description Item")]
public class INAttributeDescriptionItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>References to Inventory Item which is template.</summary>
  [PXUIField(DisplayName = "Template Item")]
  [TemplateInventory(IsKey = true)]
  [PXParent(typeof (INAttributeDescriptionItem.FK.TemplateInventoryItem))]
  public virtual int? TemplateID { get; set; }

  /// <summary>Identifier group of attributes</summary>
  [PXDBInt(IsKey = true)]
  public virtual int? GroupID { get; set; }

  /// <summary>Identifier of attribute</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = "")]
  public virtual 
  #nullable disable
  string AttributeID { get; set; }

  /// <summary>Value of attribute</summary>
  [PXAttributeValue]
  public virtual string ValueID { get; set; }

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
    PrimaryKeyOf<INAttributeDescriptionItem>.By<INAttributeDescriptionItem.templateID, INAttributeDescriptionItem.groupID, INAttributeDescriptionItem.attributeID, INAttributeDescriptionItem.valueID>
  {
    public static INAttributeDescriptionItem Find(
      PXGraph graph,
      int? templateID,
      int? groupID,
      string attributeID,
      string valueID,
      PKFindOptions options = 0)
    {
      return (INAttributeDescriptionItem) PrimaryKeyOf<INAttributeDescriptionItem>.By<INAttributeDescriptionItem.templateID, INAttributeDescriptionItem.groupID, INAttributeDescriptionItem.attributeID, INAttributeDescriptionItem.valueID>.FindBy(graph, (object) templateID, (object) groupID, (object) attributeID, (object) valueID, options);
    }
  }

  public static class FK
  {
    public class TemplateInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INAttributeDescriptionItem>.By<INAttributeDescriptionItem.templateID>
    {
    }

    public class Group : 
      PrimaryKeyOf<INAttributeDescriptionGroup>.By<INAttributeDescriptionGroup.templateID, INAttributeDescriptionGroup.groupID>.ForeignKeyOf<INAttributeDescriptionItem>.By<INAttributeDescriptionItem.templateID, INAttributeDescriptionItem.groupID>
    {
    }

    public class Attribute : 
      PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>.ForeignKeyOf<INAttributeDescriptionItem>.By<INAttributeDescriptionItem.attributeID>
    {
    }
  }

  public abstract class templateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INAttributeDescriptionItem.templateID>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INAttributeDescriptionItem.groupID>
  {
  }

  public abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAttributeDescriptionItem.attributeID>
  {
  }

  public abstract class valueID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAttributeDescriptionItem.valueID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INAttributeDescriptionItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAttributeDescriptionItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INAttributeDescriptionItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INAttributeDescriptionItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAttributeDescriptionItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INAttributeDescriptionItem.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INAttributeDescriptionItem.Tstamp>
  {
  }
}
