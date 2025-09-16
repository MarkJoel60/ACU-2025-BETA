// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlMapping
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The interface that mapping classes, which are used for implementation of reusable business logic, should implement.</summary>
public interface IBqlMapping
{
  /// <summary>The DAC to which the mapped cache extension is mapped.</summary>
  System.Type Table { get; }

  /// <summary>The mapped cache extension.</summary>
  System.Type Extension { get; }
}
