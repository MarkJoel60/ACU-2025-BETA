// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PredefinedRoles
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.GL;

public static class PredefinedRoles
{
  public static string FinancialSupervisor
  {
    get => WebConfig.GetString(nameof (FinancialSupervisor), "Financial Supervisor");
  }

  public static string ProjectAccountant
  {
    get => WebConfig.GetString(nameof (ProjectAccountant), "Project Accountant");
  }
}
