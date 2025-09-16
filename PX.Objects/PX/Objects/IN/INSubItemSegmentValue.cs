// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSubItemSegmentValue
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

[PXCacheName("IN Subitem Segment Value")]
[Serializable]
public class INSubItemSegmentValue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected short? _SegmentID;
  protected 
  #nullable disable
  string _Value;

  [StockItem(IsKey = true, DirtyRead = true, DisplayName = "Inventory ID", Visible = false)]
  [PXParent(typeof (INSubItemSegmentValue.FK.InventoryItem))]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXUIField]
  public virtual short? SegmentID
  {
    get => this._SegmentID;
    set => this._SegmentID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public class PK : 
    PrimaryKeyOf<INSubItemSegmentValue>.By<INSubItemSegmentValue.inventoryID, INSubItemSegmentValue.segmentID>
  {
    public static INSubItemSegmentValue Find(
      PXGraph graph,
      int? inventoryID,
      long? segmentID,
      PKFindOptions options = 0)
    {
      return (INSubItemSegmentValue) PrimaryKeyOf<INSubItemSegmentValue>.By<INSubItemSegmentValue.inventoryID, INSubItemSegmentValue.segmentID>.FindBy(graph, (object) inventoryID, (object) segmentID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INSubItemSegmentValue>.By<INSubItemSegmentValue.inventoryID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSubItemSegmentValue.inventoryID>
  {
  }

  public abstract class segmentID : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INSubItemSegmentValue.segmentID>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSubItemSegmentValue.value>
  {
  }
}
