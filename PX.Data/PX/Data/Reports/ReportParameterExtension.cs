// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.ReportParameterExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Reports;

#nullable disable
namespace PX.Data.Reports;

[PXInternalUseOnly]
public static class ReportParameterExtension
{
  public static bool IsRequired(this ReportParameter p)
  {
    if (p.Required.HasValue)
    {
      bool? required = p.Required;
      bool flag = true;
      return required.GetValueOrDefault() == flag & required.HasValue;
    }
    if (!(p.ProcessedView is PXFieldState processedView))
      return false;
    bool? required1 = processedView.Required;
    bool flag1 = true;
    return required1.GetValueOrDefault() == flag1 & required1.HasValue;
  }

  public static bool IsVisible(this ReportParameter p)
  {
    if (p.Visible.HasValue)
    {
      bool? visible = p.Visible;
      bool flag = true;
      return visible.GetValueOrDefault() == flag & visible.HasValue;
    }
    return p.ProcessedView is PXFieldState processedView && processedView.Visible;
  }
}
