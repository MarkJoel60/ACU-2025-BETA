// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIInventoryFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INPIInventoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _StartInventoryID;
  protected int? _EndInventoryID;

  [StockItem(DisplayName = "Start Inventory ID")]
  public virtual int? StartInventoryID
  {
    get => this._StartInventoryID;
    set => this._StartInventoryID = value;
  }

  [StockItem(DisplayName = "End Inventory ID")]
  public virtual int? EndInventoryID
  {
    get => this._EndInventoryID;
    set => this._EndInventoryID = value;
  }

  public abstract class startInventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    INPIInventoryFilter.startInventoryID>
  {
  }

  public abstract class endInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INPIInventoryFilter.endInventoryID>
  {
  }
}
