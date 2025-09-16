// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.DataSync.BAITransactionHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CA.BankFeed.DataSync;

/// <summary>Information necessary for creating transaction</summary>
public class BAITransactionHeader
{
  public string FileIdentificationNumber { get; set; }

  public string FileCreationDate { get; set; }

  public DateTime CurrentDate { get; set; }

  public string CurrentAccount { get; set; }

  public string AccountCurrencyCode { get; set; }

  public int DecimalPlaces { get; set; }
}
