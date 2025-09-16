// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.TableAndChartDashboardTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Graphs decorated with the given attribute will expose there primary View as a source for both Dashboard Table and Dashbprd Chart Controls.
/// Usually an Inquiry Graph is decorated with this attribute.
/// </summary>
/// <example>
/// [DashboardType(PX.TM.OwnedFilter.DASHBOARD_TYPE, GL.TableAndChartDashboardTypeAttribute._AMCHARTS_DASHBOART_TYPE)]
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class TableAndChartDashboardTypeAttribute : DashboardTypeAttribute
{
  public const int _AMCHARTS_DASHBOART_TYPE = 20;

  public TableAndChartDashboardTypeAttribute()
    : base(new int[3]{ 0, 4, 20 })
  {
  }
}
