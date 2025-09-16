// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQInventoryItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXProjection(typeof (Select<PX.Objects.IN.InventoryItem>), Persistent = false)]
[PXPrimaryGraph(new Type[] {typeof (InventoryItemMaint)}, new Type[] {typeof (Select<RQInventoryItem, Where<RQInventoryItem.inventoryID, Equal<Current<RQInventoryItem.inventoryID>>>>)})]
[Serializable]
public class RQInventoryItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _InventoryCD;
  protected string _Descr;
  protected int? _ItemClassID;
  protected string _ItemStatus;
  protected string _ItemType;

  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  [PXUIField]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault]
  [InventoryRaw(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<boolTrue>>), BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD), IsKey = true)]
  public virtual string InventoryCD
  {
    get => this._InventoryCD;
    set => this._InventoryCD = value;
  }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.descr), IsProjection = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.itemClassID))]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  public virtual int? ItemClassID
  {
    get => this._ItemClassID;
    set => this._ItemClassID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.IN.InventoryItem.itemStatus))]
  [PXDefault("AC")]
  [PXUIField]
  [InventoryItemStatus.List]
  public virtual string ItemStatus
  {
    get => this._ItemStatus;
    set => this._ItemStatus = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.IN.InventoryItem.itemType))]
  [PXUIField]
  [INItemTypes.List]
  public virtual string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  public class PK : PrimaryKeyOf<RQInventoryItem>.By<RQInventoryItem.inventoryID>
  {
    public static RQInventoryItem Find(PXGraph graph, int? inventoryID, PKFindOptions options = 0)
    {
      return (RQInventoryItem) PrimaryKeyOf<RQInventoryItem>.By<RQInventoryItem.inventoryID>.FindBy(graph, (object) inventoryID, options);
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQInventoryItem.inventoryID>
  {
  }

  public abstract class inventoryCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQInventoryItem.inventoryCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQInventoryItem.descr>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQInventoryItem.itemClassID>
  {
  }

  public abstract class itemStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQInventoryItem.itemStatus>
  {
  }

  public abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQInventoryItem.itemType>
  {
  }
}
