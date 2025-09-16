// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BaiBankFeedManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CA.BankFeed.DataSync;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankFeed;

public class BaiBankFeedManager : FileBankFeedManager
{
  protected override (string ProviderType, string[] Extensions) GetProviderTypeAndFileExtension()
  {
    return (typeof (BAISYProvider).FullName, new string[4]
    {
      ".txt",
      ".btrs",
      ".bai2",
      ".bai"
    });
  }

  public override string PredefinedAmountFormat => "S";

  public override string FileFormat => "B";

  public override bool NeedDataSample => false;

  public override bool CanChangeRequiredFieldsMapping => false;

  public override Dictionary<string, string> DefaultFieldMapping
  {
    get
    {
      return new Dictionary<string, string>()
      {
        {
          "AccountName",
          "Customer Account Number"
        },
        {
          "TransactionID",
          "Bank Reference Number"
        },
        {
          "Date",
          "As-of-date"
        },
        {
          "Amount",
          "Amount"
        },
        {
          "Name",
          "=Concat( [Tran Desc], [Additional Tran Desc])"
        },
        {
          "ExtRefNbr",
          "Bank Reference Number"
        },
        {
          "InvoiceInfo",
          "Customer Reference Number"
        },
        {
          "TranCode",
          "Type Code"
        }
      };
    }
  }
}
