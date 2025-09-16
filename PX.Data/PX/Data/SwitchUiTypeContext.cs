// Decompiled with JetBrains decompiler
// Type: PX.Data.SwitchUiTypeContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

internal class SwitchUiTypeContext : IDisposable
{
  private readonly string _oldValue;

  public SwitchUiTypeContext(string uiType)
  {
    this._oldValue = PXContext.GetSlot<string>("ForceUI");
    PXContext.SetSlot<string>("ForceUI", uiType);
  }

  public void Dispose()
  {
    if (this._oldValue == null)
      PXContext.ClearSlot("ForceUI");
    else
      PXContext.SetSlot<string>("ForceUI", this._oldValue);
  }
}
