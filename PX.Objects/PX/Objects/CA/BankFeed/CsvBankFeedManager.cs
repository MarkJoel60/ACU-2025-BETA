// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.CsvBankFeedManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.DataSync;

#nullable disable
namespace PX.Objects.CA.BankFeed;

public class CsvBankFeedManager : FileBankFeedManager
{
  protected override (string ProviderType, string[] Extensions) GetProviderTypeAndFileExtension()
  {
    return (typeof (CSVSYProvider).FullName, new string[1]
    {
      ".csv"
    });
  }

  public override string FileFormat => "C";
}
