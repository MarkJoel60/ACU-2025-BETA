// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedMatchField
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public static class CABankFeedMatchField
{
  private static readonly (string, string)[] defaultCorporateCardFilters = new (string, string)[3]
  {
    ("N", " "),
    ("O", "Account Owner"),
    ("A", nameof (Name))
  };
  private static readonly (string, string)[] defaultExpenseReceiptsFilters = new (string, string)[2]
  {
    ("C", nameof (Category)),
    ("A", nameof (Name))
  };
  public const string Empty = "N";
  public const string AccountOwner = "O";
  public const string Category = "C";
  public const string Name = "A";
  public const string CheckNumber = "C";
  public const string Memo = "M";
  public const string PartnerAccountId = "P";
  public const string UserDesc = "U";
  public const string CardNumber = "R";
  public const string PayerName = "Y";
  public const string EmptyLabel = " ";

  public static (string, string)[] AllowedList(CABankFeedMatchField.SetOfValues setOfValues)
  {
    (string, string)[] valueTupleArray = new (string, string)[0];
    switch (setOfValues)
    {
      case CABankFeedMatchField.SetOfValues.CorporateCard:
        valueTupleArray = CABankFeedMatchField.defaultCorporateCardFilters;
        break;
      case CABankFeedMatchField.SetOfValues.ExpenseReceipts:
        valueTupleArray = CABankFeedMatchField.defaultExpenseReceiptsFilters;
        break;
    }
    return valueTupleArray;
  }

  public enum SetOfValues
  {
    CorporateCard,
    ExpenseReceipts,
    ExpenseFileReceipts,
  }

  public enum ValueOrLabel
  {
    Value,
    Label,
  }

  public class ListAttribute(CABankFeedMatchField.SetOfValues setOfValues) : PXStringListAttribute(CABankFeedMatchField.AllowedList(setOfValues))
  {
  }
}
