// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PICountFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class PICountFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _SubItem;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected int? _StartLineNbr;
  protected int? _EndLineNbr;

  [StockItem(DisplayName = "Inventory ID")]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (INSiteStatusFilter.inventoryID), DisplayName = "Subitem")]
  public virtual string SubItem
  {
    get => this._SubItem;
    set => this._SubItem = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubItemCDWildcard
  {
    get
    {
      return this._SubItem != null ? SubCDUtils.CreateSubCDWildcard(this._SubItem, "INSUBITEM") : (string) null;
    }
  }

  [Location(typeof (INPIHeader.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PX.Objects.IN.LotSerialNbr]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBString(100, IsUnicode = true)]
  public virtual string LotSerialNbrWildcard
  {
    get
    {
      string wildcardAnything = PXDatabase.Provider.SqlDialect.WildcardAnything;
      return this._LotSerialNbr == null ? (string) null : wildcardAnything + this._LotSerialNbr + wildcardAnything;
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Start Line Nbr.")]
  public virtual int? StartLineNbr
  {
    get => this._StartLineNbr;
    set => this._StartLineNbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "End Line Nbr.")]
  public virtual int? EndLineNbr
  {
    get => this._EndLineNbr;
    set => this._EndLineNbr = value;
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PICountFilter.inventoryID>
  {
  }

  public abstract class subItem : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PICountFilter.subItem>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PICountFilter.subItemCDWildcard>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PICountFilter.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PICountFilter.lotSerialNbr>
  {
  }

  public abstract class lotSerialNbrWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PICountFilter.lotSerialNbrWildcard>
  {
  }

  public abstract class startLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PICountFilter.startLineNbr>
  {
  }

  public abstract class endLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PICountFilter.endLineNbr>
  {
  }
}
