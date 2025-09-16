// Decompiled with JetBrains decompiler
// Type: PX.Data.DashboardTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DashboardTypeAttribute : Attribute
{
  public readonly int[] Types;

  public DashboardTypeAttribute(params int[] type) => this.Types = type;

  /// <exclude />
  public enum Type
  {
    Default,
    WikiArticle,
    Task,
    Announcements,
    Chart,
  }
}
