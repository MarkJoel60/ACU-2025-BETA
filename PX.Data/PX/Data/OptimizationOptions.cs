// Decompiled with JetBrains decompiler
// Type: PX.Data.OptimizationOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

internal class OptimizationOptions
{
  private bool _enableNewUIGraphReusing = true;

  public bool EnableNewUIGraphReusing
  {
    get
    {
      return this._enableNewUIGraphReusing && (!WebConfig.IsClusterEnabled || WebConfig.IsMultiSiteMode) && !WebConfig.SerializeSessionItems;
    }
    set => this._enableNewUIGraphReusing = value;
  }
}
