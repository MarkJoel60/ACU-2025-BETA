// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemCategory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
[Serializable]
public class INItemCategory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CategoryID;
  protected int? _InventoryID;
  protected bool? _CategorySelected;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (INCategory.categoryID))]
  [PXParent(typeof (INItemCategory.FK.Category))]
  [PXUIField(DisplayName = "Category")]
  public virtual int? CategoryID
  {
    get => this._CategoryID;
    set => this._CategoryID = value;
  }

  [PXParent(typeof (INItemCategory.FK.InventoryItem))]
  [AnyInventory(IsKey = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? CategorySelected
  {
    get => this._CategorySelected;
    set => this._CategorySelected = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<INItemCategory>.By<INItemCategory.inventoryID, INItemCategory.categoryID>
  {
    public static INItemCategory Find(
      PXGraph graph,
      int? inventoryID,
      int? categoryID,
      PKFindOptions options = 0)
    {
      return (INItemCategory) PrimaryKeyOf<INItemCategory>.By<INItemCategory.inventoryID, INItemCategory.categoryID>.FindBy(graph, (object) inventoryID, (object) categoryID, options);
    }
  }

  public static class FK
  {
    public class Category : 
      PrimaryKeyOf<INCategory>.By<INCategory.categoryID>.ForeignKeyOf<INItemCategory>.By<INItemCategory.categoryID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemCategory>.By<INItemCategory.inventoryID>
    {
    }
  }

  public abstract class categoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCategory.categoryID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCategory.inventoryID>
  {
  }

  public abstract class categorySelected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemCategory.categorySelected>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemCategory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemCategory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemCategory.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemCategory.Tstamp>
  {
  }
}
