// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.IPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.GL;

public interface IPeriod : IPeriodSetup
{
  string FinYear { get; set; }

  string FinPeriodID { get; set; }

  int? OrganizationID { get; set; }

  bool? DateLocked { get; set; }
}
