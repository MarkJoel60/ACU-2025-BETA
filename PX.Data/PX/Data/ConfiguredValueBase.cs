// Decompiled with JetBrains decompiler
// Type: PX.Data.ConfiguredValueBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.Common;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public abstract class ConfiguredValueBase
{
  protected abstract void SetConfiguredValue(string value);

  protected abstract string ConfigurationKey { get; }

  internal void Configure(IConfiguration configuration)
  {
    string str = configuration[this.ConfigurationKey];
    if (string.IsNullOrWhiteSpace(str))
      return;
    this.SetConfiguredValue(str);
  }
}
