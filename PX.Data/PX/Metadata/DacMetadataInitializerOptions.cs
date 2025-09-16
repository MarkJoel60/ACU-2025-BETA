// Decompiled with JetBrains decompiler
// Type: PX.Metadata.DacMetadataInitializerOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Metadata;

internal class DacMetadataInitializerOptions
{
  public bool CollectOnStartup { get; set; } = true;

  public TimeSpan CollectOnStartupAfter { get; set; } = TimeSpan.FromSeconds(60.0);
}
