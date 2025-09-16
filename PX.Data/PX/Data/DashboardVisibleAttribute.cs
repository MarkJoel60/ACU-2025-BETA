// Decompiled with JetBrains decompiler
// Type: PX.Data.DashboardVisibleAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class DashboardVisibleAttribute : PXEventSubscriberAttribute
{
  private readonly bool _visible;

  public DashboardVisibleAttribute(bool visible) => this._visible = visible;

  public DashboardVisibleAttribute()
    : this(true)
  {
  }

  /// <summary>Get.</summary>
  public bool Visible => this._visible;
}
