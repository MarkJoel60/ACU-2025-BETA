// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestLineFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.RQ;

public class RQRequestLineFilter : RQRequestSelection
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected bool? _AllowUpdate = new bool?(true);

  [Inventory(Filterable = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (RQRequestLineFilter.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? AllowUpdate
  {
    get => this._AllowUpdate;
    set => this._AllowUpdate = value;
  }

  public abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineFilter.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineFilter.subItemID>
  {
  }

  public abstract class allowUpdate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestLineFilter.allowUpdate>
  {
  }

  public new abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestLineFilter.filterSet>
  {
  }
}
