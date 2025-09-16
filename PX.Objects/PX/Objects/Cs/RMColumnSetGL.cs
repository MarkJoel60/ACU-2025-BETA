// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMColumnSetGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXPrimaryGraph(typeof (RMColumnSetMaint))]
[Serializable]
public class RMColumnSetGL : PXCacheExtension<
#nullable disable
RMColumnSet>
{
  protected string _Type;

  [PXDBString(2, IsFixed = true)]
  [RMType.List]
  [PXDefault("GL")]
  [PXUIField]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMColumnSetGL.type>
  {
  }
}
