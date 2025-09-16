// Decompiled with JetBrains decompiler
// Type: PX.SM.Neighbour
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class Neighbour : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _LeftEntityType;
  protected string _RightEntityType;
  protected byte[] _CoverageMask;
  protected byte[] _InverseMask;
  protected byte[] _WinCoverageMask;
  protected byte[] _WinInverseMask;

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string LeftEntityType
  {
    get => this._LeftEntityType;
    set => this._LeftEntityType = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string RightEntityType
  {
    get => this._RightEntityType;
    set => this._RightEntityType = value;
  }

  [PXDBField]
  [PXDefault]
  public virtual byte[] CoverageMask
  {
    get => this._CoverageMask;
    set => this._CoverageMask = value;
  }

  [PXDBField]
  [PXDefault]
  public virtual byte[] InverseMask
  {
    get => this._InverseMask;
    set => this._InverseMask = value;
  }

  [PXDBField]
  [PXDefault]
  public virtual byte[] WinCoverageMask
  {
    get => this._WinCoverageMask;
    set => this._WinCoverageMask = value;
  }

  [PXDBField]
  [PXDefault]
  public virtual byte[] WinInverseMask
  {
    get => this._WinInverseMask;
    set => this._WinInverseMask = value;
  }

  public abstract class leftEntityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Neighbour.leftEntityType>
  {
  }

  public abstract class rightEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Neighbour.rightEntityType>
  {
  }

  public abstract class coverageMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Neighbour.coverageMask>
  {
  }

  public abstract class inverseMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Neighbour.inverseMask>
  {
  }

  public abstract class winCoverageMask : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    Neighbour.winCoverageMask>
  {
  }

  public abstract class winInverseMask : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    Neighbour.winInverseMask>
  {
  }
}
