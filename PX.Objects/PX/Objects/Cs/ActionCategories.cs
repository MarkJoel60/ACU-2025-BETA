// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ActionCategories
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.CS;

[PXLocalizable("CS Error")]
public static class ActionCategories
{
  public const string Processing = "Processing";
  public const string DocumentProcessing = "Document Processing";
  public const string PeriodManagement = "Period Management";
  public const string CompanyManagement = "Company Management";
  public const string ReportManagement = "Report Management";
  public const string Other = "Other";

  public static string GetLocal(string message)
  {
    return PXLocalizer.Localize(message, typeof (Messages).FullName);
  }
}
