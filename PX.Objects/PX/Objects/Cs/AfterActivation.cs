// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AfterActivation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class AfterActivation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Refresh;

  [PXDBBool]
  public virtual bool? Refresh
  {
    get => this._Refresh;
    set => this._Refresh = value;
  }

  public abstract class refresh : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  AfterActivation.refresh>
  {
  }
}
