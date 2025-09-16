// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolRead
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
[Serializable]
public class GLConsolRead : GLConsolData
{
  protected int? _AccountID;
  protected int? _SubID;

  [PXInt]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXInt]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  public abstract class accountID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  GLConsolRead.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolRead.subID>
  {
  }
}
