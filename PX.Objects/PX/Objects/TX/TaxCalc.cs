// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxCalc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.TX;

public enum TaxCalc
{
  NoCalc = 0,
  Calc = 1,
  ManualCalc = 2,
  ManualLineCalc = 3,
  RecalculateAlways = 4,
  RedefaultAlways = 8,
  Flags = 12, // 0x0000000C
}
