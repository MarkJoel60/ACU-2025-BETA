// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.DataSync.BAIFundsType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CA.BankFeed.DataSync;

/// <summary>Code of funds type (record 16, position 4)</summary>
public static class BAIFundsType
{
  public const string Unknown = "Z";
  public const string ImmediateAvailability = "0";
  public const string OneDayAvailability = "1";
  public const string TwoOrMoreDaysAvailability = "2";
  public const string DistributedAvailability = "S";
  public const string ValueDated = "V";
}
