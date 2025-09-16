// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.ImportResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CA.BankFeed;

public class ImportResult
{
  public bool IsOk { get; set; }

  public int TransactionIndex { get; set; }

  public string LastUpdatedStatementRefNbr { get; set; }

  public BankFeedImportException ImportException { get; set; }
}
