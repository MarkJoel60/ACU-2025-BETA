// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Position
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.EP;

[DebuggerDisplay("MapID={MapID} NodeID={NodeID} ItemID={ItemID}")]
[PXHidden]
[Serializable]
public class Position : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _MapID;
  protected int? _NodeID;
  protected int? _ItemID;

  [PXDBInt]
  public virtual int? MapID
  {
    get => this._MapID;
    set => this._MapID = value;
  }

  [PXDBInt]
  public virtual int? NodeID
  {
    get => this._NodeID;
    set => this._NodeID = value;
  }

  [PXDBInt]
  public virtual int? ItemID
  {
    get => this._ItemID;
    set => this._ItemID = value;
  }

  [PXDBInt]
  public virtual int? RouteItemID { get; set; }

  [PXDBInt]
  public virtual int? RouteParentID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? UseCurrentTreeItem { get; set; }

  public abstract class mapID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  Position.mapID>
  {
  }

  public abstract class nodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Position.nodeID>
  {
  }

  public abstract class itemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Position.itemID>
  {
  }
}
