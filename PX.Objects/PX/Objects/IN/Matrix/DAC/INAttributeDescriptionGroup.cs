// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.INAttributeDescriptionGroup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC;

[PXCacheName("Attribute Description Group")]
public class INAttributeDescriptionGroup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>References to Inventory Item which is template.</summary>
  [PXUIField(DisplayName = "Template Item")]
  [TemplateInventory(IsKey = true)]
  [PXParent(typeof (INAttributeDescriptionGroup.FK.TemplateInventoryItem))]
  public virtual int? TemplateID { get; set; }

  /// <summary>Identifier group of attributes</summary>
  [PXDBInt(IsKey = true)]
  public virtual int? GroupID { get; set; }

  /// <summary>Description of the group of attributes</summary>
  [PXDBString(4000, IsUnicode = true)]
  public virtual 
  #nullable disable
  string Description { get; set; }

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
    PrimaryKeyOf<INAttributeDescriptionGroup>.By<INAttributeDescriptionGroup.templateID, INAttributeDescriptionGroup.groupID>
  {
    public static INAttributeDescriptionGroup Find(
      PXGraph graph,
      int? templateID,
      int? groupID,
      PKFindOptions options = 0)
    {
      return (INAttributeDescriptionGroup) PrimaryKeyOf<INAttributeDescriptionGroup>.By<INAttributeDescriptionGroup.templateID, INAttributeDescriptionGroup.groupID>.FindBy(graph, (object) templateID, (object) groupID, options);
    }
  }

  public static class FK
  {
    public class TemplateInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INAttributeDescriptionGroup>.By<INAttributeDescriptionGroup.templateID>
    {
    }
  }

  public abstract class templateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INAttributeDescriptionGroup.templateID>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INAttributeDescriptionGroup.groupID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAttributeDescriptionGroup.description>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INAttributeDescriptionGroup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAttributeDescriptionGroup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INAttributeDescriptionGroup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INAttributeDescriptionGroup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAttributeDescriptionGroup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INAttributeDescriptionGroup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INAttributeDescriptionGroup.Tstamp>
  {
  }
}
