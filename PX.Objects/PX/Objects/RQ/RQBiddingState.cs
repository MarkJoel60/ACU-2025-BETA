// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQBiddingState
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

[Serializable]
public class RQBiddingState : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _SingleMode = new bool?(false);
  protected 
  #nullable disable
  string _ReqNbr;
  protected int? _LineNbr;

  [PXBool]
  [PXDefault(false)]
  public virtual bool? SingleMode
  {
    get => this._SingleMode;
    set => this._SingleMode = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXInt]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  public abstract class singleMode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQBiddingState.singleMode>
  {
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBiddingState.reqNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBiddingState.lineNbr>
  {
  }
}
