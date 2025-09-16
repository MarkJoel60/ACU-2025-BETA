// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.StatusCodes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PM;

[Flags]
public enum StatusCodes
{
  Valid = 0,
  Warning = 1,
  Error = 2,
  InclusiveTaxesInRevenueBudgetIntroduced = 4,
  DateSensitiveActualsIntroduced = 8,
  ProjectBudgetHistoryIntroduced = 16, // 0x00000010
}
