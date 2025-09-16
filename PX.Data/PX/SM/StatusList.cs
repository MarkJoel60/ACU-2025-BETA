// Decompiled with JetBrains decompiler
// Type: PX.SM.StatusList
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public static class StatusList
{
  public const int DoNotChange = 0;
  public const int OnHold = 1;
  public const int Published = 2;
  public const int Approved = 3;
  public const int Rejected = 4;

  public static int[] FullValues
  {
    get => new int[5]{ 0, 1, 2, 3, 4 };
  }

  public static string[] FullLabels
  {
    get
    {
      return new string[5]
      {
        PXMessages.LocalizeNoPrefix("Do Not Change"),
        PXMessages.LocalizeNoPrefix("On Hold"),
        PXMessages.LocalizeNoPrefix("Published"),
        PXMessages.LocalizeNoPrefix("Approved"),
        PXMessages.LocalizeNoPrefix("Rejected")
      };
    }
  }

  public static int[] ReducedValues => new int[3]{ 0, 1, 2 };

  public static string[] ReducedLabels
  {
    get
    {
      return new string[3]
      {
        PXMessages.LocalizeNoPrefix("Do Not Change"),
        PXMessages.LocalizeNoPrefix("On Hold"),
        PXMessages.LocalizeNoPrefix("Published")
      };
    }
  }
}
