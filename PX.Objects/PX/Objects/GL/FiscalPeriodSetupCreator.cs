// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FiscalPeriodSetupCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL;

public abstract class FiscalPeriodSetupCreator
{
  public const string CS_YEAR_FORMAT = "0000";
  public const string CS_PERIOD_NBR_FORMAT = "00";

  public static bool IsFixedLengthPeriod(FiscalPeriodSetupCreator.FPType aType)
  {
    return aType == FiscalPeriodSetupCreator.FPType.Week || aType == FiscalPeriodSetupCreator.FPType.BiWeek || aType == FiscalPeriodSetupCreator.FPType.FourWeek || aType == FiscalPeriodSetupCreator.FPType.Decade || aType == FiscalPeriodSetupCreator.FPType.FixedLength || aType == FiscalPeriodSetupCreator.FPType.FourFourFive || aType == FiscalPeriodSetupCreator.FPType.FourFiveFour || aType == FiscalPeriodSetupCreator.FPType.FiveFourFour;
  }

  public static short? GetPeriodLength(FiscalPeriodSetupCreator.FPType aType)
  {
    return FiscalPeriodSetupCreator.GetPeriodLength(aType, 0);
  }

  public static short? GetPeriodLength(FiscalPeriodSetupCreator.FPType aType, int periodNum)
  {
    short? periodLength = new short?();
    switch (aType)
    {
      case FiscalPeriodSetupCreator.FPType.Week:
        periodLength = new short?((short) 7);
        break;
      case FiscalPeriodSetupCreator.FPType.BiWeek:
        periodLength = new short?((short) 14);
        break;
      case FiscalPeriodSetupCreator.FPType.Decade:
        periodLength = new short?((short) 10);
        break;
      case FiscalPeriodSetupCreator.FPType.FourWeek:
        periodLength = new short?((short) 28);
        break;
      case FiscalPeriodSetupCreator.FPType.FourFourFive:
        periodLength = new short?(periodNum == 0 || periodNum % 3 != 0 ? (short) 28 : (short) 35);
        break;
      case FiscalPeriodSetupCreator.FPType.FourFiveFour:
        periodLength = new short?(periodNum == 0 || (periodNum + 1) % 3 != 0 ? (short) 28 : (short) 35);
        break;
      case FiscalPeriodSetupCreator.FPType.FiveFourFour:
        periodLength = new short?(periodNum == 0 || (periodNum + 2) % 3 != 0 ? (short) 28 : (short) 35);
        break;
    }
    return periodLength;
  }

  public static string FormatYear(DateTime aDate) => aDate.Year.ToString("0000");

  public static string FormatYear(int aYear) => aYear.ToString("0000");

  public enum FPType
  {
    Month,
    BiMonth,
    Quarter,
    Week,
    BiWeek,
    Decade,
    FixedLength,
    FourWeek,
    FourFourFive,
    FourFiveFour,
    FiveFourFour,
    Custom,
  }
}
