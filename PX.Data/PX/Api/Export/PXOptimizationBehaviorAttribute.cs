// Decompiled with JetBrains decompiler
// Type: PX.Api.Export.PXOptimizationBehaviorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Api.Export;

/// <summary>Attribute can change behavior of optimized export.</summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class PXOptimizationBehaviorAttribute : Attribute
{
  /// <summary>
  /// This flag says that bql delegate method is not change output rows of view and can be ignored in optimized export
  /// </summary>
  public bool IgnoreBqlDelegate { get; set; }
}
