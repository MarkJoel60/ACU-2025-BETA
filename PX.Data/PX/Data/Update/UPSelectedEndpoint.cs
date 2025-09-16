// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UPSelectedEndpoint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Update;

[Serializable]
public class UPSelectedEndpoint : UPMeasureEndpoint
{
  protected bool? _Selected = new bool?(false);

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  public abstract class selected : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  UPSelectedEndpoint.selected>
  {
  }
}
