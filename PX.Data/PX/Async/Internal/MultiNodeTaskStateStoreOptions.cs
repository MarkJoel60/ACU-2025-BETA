// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.MultiNodeTaskStateStoreOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using System;

#nullable enable
namespace PX.Async.Internal;

internal sealed class MultiNodeTaskStateStoreOptions
{
  internal TimeSpan Timeout { get; set; }

  internal void ReadFrom(IConfiguration configuration)
  {
    string s = ((IConfiguration) PX.Hosting.SessionState.Extensions.GetSessionStateStoreProviderSettings(configuration).Configuration)["longOperationsTimeout"];
    int result;
    if (s == null || !int.TryParse(s, out result) || result <= 0)
      return;
    this.Timeout = TimeSpan.FromMinutes((double) result);
  }
}
