// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL;

public class FinPeriodType
{
  public const string Month = "MO";
  public const string BiMonth = "BM";
  public const string Quarter = "QR";
  public const string Week = "WK";
  public const string BiWeek = "BW";
  public const string FourWeek = "FW";
  public const string Decade = "DC";
  public const string FourFourFive = "FF";
  public const string FourFiveFour = "FI";
  public const string FiveFourFour = "IF";
  public const string CustomPeriodLength = "CL";
  public const string CustomPeriodsNumber = "CN";

  public static FiscalPeriodSetupCreator.FPType GetFPType(string aFinPeriodType)
  {
    if (aFinPeriodType != null && aFinPeriodType.Length == 2)
    {
      switch (aFinPeriodType[1])
      {
        case 'C':
          if (aFinPeriodType == "DC")
            return FiscalPeriodSetupCreator.FPType.Decade;
          break;
        case 'F':
          switch (aFinPeriodType)
          {
            case "FF":
              return FiscalPeriodSetupCreator.FPType.FourFourFive;
            case "IF":
              return FiscalPeriodSetupCreator.FPType.FiveFourFour;
          }
          break;
        case 'I':
          if (aFinPeriodType == "FI")
            return FiscalPeriodSetupCreator.FPType.FourFiveFour;
          break;
        case 'K':
          if (aFinPeriodType == "WK")
            return FiscalPeriodSetupCreator.FPType.Week;
          break;
        case 'M':
          if (aFinPeriodType == "BM")
            return FiscalPeriodSetupCreator.FPType.BiMonth;
          break;
        case 'N':
          if (aFinPeriodType == "CN")
            return FiscalPeriodSetupCreator.FPType.Custom;
          break;
        case 'O':
          if (aFinPeriodType == "MO")
            return FiscalPeriodSetupCreator.FPType.Month;
          break;
        case 'R':
          if (aFinPeriodType == "QR")
            return FiscalPeriodSetupCreator.FPType.Quarter;
          break;
        case 'W':
          switch (aFinPeriodType)
          {
            case "BW":
              return FiscalPeriodSetupCreator.FPType.BiWeek;
            case "FW":
              return FiscalPeriodSetupCreator.FPType.FourWeek;
          }
          break;
      }
    }
    throw new PXException("The financial period type is unknown.");
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[10]
      {
        "MO",
        "BM",
        "QR",
        "WK",
        "BW",
        "FW",
        "FF",
        "FI",
        "IF",
        "CN"
      }, new string[10]
      {
        "Month",
        "Two Months",
        "Quarter",
        "Week",
        "Two Weeks",
        "Four Weeks",
        "4-4-5 Week",
        "4-5-4 Week",
        "5-4-4 Week",
        "Custom Number of Periods"
      })
    {
    }
  }
}
