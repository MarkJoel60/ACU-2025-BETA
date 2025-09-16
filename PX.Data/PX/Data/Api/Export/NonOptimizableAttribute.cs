// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.NonOptimizableAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Api.Export;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class NonOptimizableAttribute : Attribute
{
  public System.Type[] FailedFields { get; }

  public NonOptimizableAttribute()
  {
  }

  public NonOptimizableAttribute(System.Type[] failedFields) => this.FailedFields = failedFields;

  public bool IgnoreOptimizationBehavior { get; set; }
}
