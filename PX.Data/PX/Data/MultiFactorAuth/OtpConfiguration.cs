// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.OtpConfiguration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.MultiFactorAuth;

[PXInternalUseOnly]
public class OtpConfiguration
{
  public int Window { get; internal set; } = 30;

  public string Algorithm { get; internal set; } = "Sha1";

  public int Length { get; internal set; } = 6;

  public int Multiplier { get; internal set; }
}
