// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.TableDashboardTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Graphs decorated with the given attribute will expose there primary View as a source for Dashboard Table Controls.
/// Usually an Inquiry Graph is decorated with this attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class TableDashboardTypeAttribute : DashboardTypeAttribute
{
  public TableDashboardTypeAttribute()
    : base(new int[1])
  {
  }
}
