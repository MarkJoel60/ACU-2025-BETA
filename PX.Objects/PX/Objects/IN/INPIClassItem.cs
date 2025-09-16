// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIClassItem
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

[PXCacheName("Physical Inventory Type by Item")]
[Serializable]
public class INPIClassItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PIClassID;
  protected int? _InventoryID;

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INPIClass.pIClassID))]
  [PXParent(typeof (INPIClassItem.FK.PIClass))]
  public virtual string PIClassID
  {
    get => this._PIClassID;
    set => this._PIClassID = value;
  }

  [PXParent(typeof (INPIClassItem.FK.InventoryItem))]
  [StockItem(DisplayName = "Inventory ID", IsKey = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<INPIClassItem>.By<INPIClassItem.pIClassID, INPIClassItem.inventoryID>
  {
    public static INPIClassItem Find(
      PXGraph graph,
      string pIClassID,
      int? inventoryID,
      PKFindOptions options = 0)
    {
      return (INPIClassItem) PrimaryKeyOf<INPIClassItem>.By<INPIClassItem.pIClassID, INPIClassItem.inventoryID>.FindBy(graph, (object) pIClassID, (object) inventoryID, options);
    }
  }

  public static class FK
  {
    public class PIClass : 
      PrimaryKeyOf<INPIClass>.By<INPIClass.pIClassID>.ForeignKeyOf<INPIClassItem>.By<INPIClassItem.pIClassID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INPIClassItem>.By<INPIClassItem.inventoryID>
    {
    }
  }

  public abstract class pIClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClassItem.pIClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIClassItem.inventoryID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPIClassItem.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIClassItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIClassItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIClassItem.createdDateTime>
  {
  }
}
