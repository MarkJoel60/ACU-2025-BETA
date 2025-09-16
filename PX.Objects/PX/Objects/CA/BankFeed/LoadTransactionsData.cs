// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.LoadTransactionsData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CA.BankFeed;

public class LoadTransactionsData
{
  public DateTime StartDate { get; set; }

  public DateTime EndDate { get; set; }

  public string[] AccountsID { get; set; }

  public int? TransactionsLimit { get; set; }

  public bool TestMode { get; set; }

  public bool IgnoreDates { get; set; }

  public LoadTransactionsData.Order TransactionsOrder { get; set; }

  public CABankFeedDetail[] Details { get; set; }

  public enum Order
  {
    AscDate,
    DescDate,
    CustomAccountAscDate,
  }
}
