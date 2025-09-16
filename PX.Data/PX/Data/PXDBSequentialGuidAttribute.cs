// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBSequentialGuidAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// GUID, defaulting by "old" algorithm (timestamp + ethernet MAC address)
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBSequentialGuidAttribute : PXDBGuidAttribute
{
  public PXDBSequentialGuidAttribute()
    : base(true)
  {
  }

  protected override Guid newGuid() => SequentialGuid.Generate();
}
