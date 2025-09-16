// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.DAC.INRelatedInventoryUserFeedback
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems.DAC;

/// <summary>
/// Represents Related Items Feedback for stock- and non-stock- items.
/// The records of this type are created when a user approves/deletes ML suggestion.
/// The records of this type should be exported to an external system.
/// </summary>
[PXCacheName]
public class INRelatedInventoryUserFeedback : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (INRelatedInventoryUserFeedback.FK.InventoryItem))]
  public virtual int? InventoryID { get; set; }

  /// <exclude />
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (INRelatedInventoryUserFeedback.FK.InventoryItem))]
  public virtual int? RelatedInventoryID { get; set; }

  /// <exclude />
  [PXDBBool]
  public virtual bool? IsCrossSell { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

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
    PrimaryKeyOf<INRelatedInventoryUserFeedback>.By<INRelatedInventoryUserFeedback.inventoryID, INRelatedInventoryUserFeedback.relatedInventoryID>
  {
    public static INRelatedInventoryUserFeedback Find(
      PXGraph graph,
      int? inventoryID,
      int? relatedInventoryID,
      PKFindOptions options = 0)
    {
      return (INRelatedInventoryUserFeedback) PrimaryKeyOf<INRelatedInventoryUserFeedback>.By<INRelatedInventoryUserFeedback.inventoryID, INRelatedInventoryUserFeedback.relatedInventoryID>.FindBy(graph, (object) inventoryID, (object) relatedInventoryID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INRelatedInventoryUserFeedback>.By<INRelatedInventoryUserFeedback.inventoryID>
    {
    }

    public class RelatedInventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INRelatedInventoryUserFeedback>.By<INRelatedInventoryUserFeedback.relatedInventoryID>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.inventoryID>
  {
  }

  public abstract class relatedInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.relatedInventoryID>
  {
  }

  public abstract class isCrossSell : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.isCrossSell>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INRelatedInventoryUserFeedback.Tstamp>
  {
  }
}
