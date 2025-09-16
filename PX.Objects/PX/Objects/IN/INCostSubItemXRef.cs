// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCostSubItemXRef
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

[Serializable]
public class INCostSubItemXRef : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SubItemID;
  protected int? _CostSubItemID;
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [SubItem(IsKey = true)]
  public virtual int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INCostSubItemXRef>.By<INCostSubItemXRef.subItemID, INCostSubItemXRef.costSubItemID>
  {
    public static INCostSubItemXRef Find(
      PXGraph graph,
      int? subItemID,
      int? costSubItemID,
      PKFindOptions options = 0)
    {
      return (INCostSubItemXRef) PrimaryKeyOf<INCostSubItemXRef>.By<INCostSubItemXRef.subItemID, INCostSubItemXRef.costSubItemID>.FindBy(graph, (object) subItemID, (object) costSubItemID, options);
    }
  }

  public static class FK
  {
    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INCostSubItemXRef>.By<INCostSubItemXRef.subItemID>
    {
    }

    public class CostSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INCostSubItemXRef>.By<INCostSubItemXRef.costSubItemID>
    {
    }
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostSubItemXRef.subItemID>
  {
  }

  public abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostSubItemXRef.costSubItemID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INCostSubItemXRef.Tstamp>
  {
  }
}
