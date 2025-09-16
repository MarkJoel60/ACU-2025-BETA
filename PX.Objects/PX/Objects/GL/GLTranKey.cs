// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

public class GLTranKey : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IEquatable<
#nullable disable
GLTranKey>
{
  protected string _Module;
  protected string _BatchNbr;
  protected int? _LineNbr;

  public GLTranKey()
  {
  }

  public GLTranKey(GLTran tran)
  {
    this.Module = tran?.Module;
    this.BatchNbr = tran?.BatchNbr;
    this.LineNbr = (int?) tran?.LineNbr;
  }

  public GLTranKey(string module, string batchNbr, int? lineNbr)
  {
    this.Module = module;
    this.BatchNbr = batchNbr;
    this.LineNbr = lineNbr;
  }

  [PXString(2, IsKey = true)]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXString(15, IsKey = true)]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXInt(IsKey = true)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  public bool Equals(GLTranKey key)
  {
    if (!(this.Module == key.Module) || !(this.BatchNbr == key.BatchNbr))
      return false;
    int? lineNbr1 = this.LineNbr;
    int? lineNbr2 = key.LineNbr;
    return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
  }

  public bool Equals(GLTran tran) => this.Equals(new GLTranKey(tran));

  public virtual int GetHashCode()
  {
    return (this.Module ?? "").GetHashCode() ^ (this.BatchNbr ?? "").GetHashCode() ^ this.LineNbr.GetValueOrDefault().GetHashCode();
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranKey.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranKey.batchNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranKey.lineNbr>
  {
  }
}
