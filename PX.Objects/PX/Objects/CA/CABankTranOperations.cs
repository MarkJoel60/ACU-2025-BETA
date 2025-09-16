// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranOperations
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

public class CABankTranOperations
{
  public const 
  #nullable disable
  string All = "AA";
  public const string AutoMatchPaymentsOnly = "DP";
  private static readonly Dictionary<char, CABankTranOperations.MatchTo[]> AllowedMatchTo = new Dictionary<char, CABankTranOperations.MatchTo[]>()
  {
    {
      'A',
      new CABankTranOperations.MatchTo[4]
      {
        CABankTranOperations.MatchTo.Payments,
        CABankTranOperations.MatchTo.Invoices,
        CABankTranOperations.MatchTo.ExpenseReceipts,
        CABankTranOperations.MatchTo.CreateDocument
      }
    },
    {
      'P',
      new CABankTranOperations.MatchTo[1]
    },
    {
      'D',
      new CABankTranOperations.MatchTo[0]
    }
  };

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "AA", "DP" }, new string[2]
      {
        "All",
        "Auto-Match Only"
      })
    {
    }
  }

  public enum MatchingType
  {
    Manual,
    AutoMatch,
  }

  public enum MatchTo
  {
    Payments,
    Invoices,
    ExpenseReceipts,
    CreateDocument,
  }

  public class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankTranOperations.all>
  {
    public all()
      : base("AA")
    {
    }
  }

  public class autoMatchPaymentsOnly : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CABankTranOperations.autoMatchPaymentsOnly>
  {
    public autoMatchPaymentsOnly()
      : base("DP")
    {
    }
  }

  private static class Options
  {
    public const char All = 'A';
    public const char Payments = 'P';
    public const char Invoices = 'I';
    public const char ExpenseReceipt = 'E';
    public const char CreateDocument = 'C';
    public const char Disabled = 'D';
  }

  public static class Verify
  {
    private static char GetAllowedOperationForManualMatching(CABankTran transaction)
    {
      return (transaction?.AllowedOperations ?? "AA")[0];
    }

    private static char GetAllowedOperationForAutoMatching(CABankTran transaction)
    {
      return (transaction?.AllowedOperations ?? "AA")[1];
    }

    public static bool PaymentMatchingAllowed(
      CABankTran transaction,
      CABankTranOperations.MatchingType matchingType)
    {
      return ((IEnumerable<CABankTranOperations.MatchTo>) CABankTranOperations.AllowedMatchTo[matchingType == CABankTranOperations.MatchingType.AutoMatch ? CABankTranOperations.Verify.GetAllowedOperationForAutoMatching(transaction) : CABankTranOperations.Verify.GetAllowedOperationForManualMatching(transaction)]).Contains<CABankTranOperations.MatchTo>(CABankTranOperations.MatchTo.Payments);
    }

    public static bool InvoiceMatchingAllowed(
      CABankTran transaction,
      CABankTranOperations.MatchingType matchingType)
    {
      return ((IEnumerable<CABankTranOperations.MatchTo>) CABankTranOperations.AllowedMatchTo[matchingType == CABankTranOperations.MatchingType.AutoMatch ? CABankTranOperations.Verify.GetAllowedOperationForAutoMatching(transaction) : CABankTranOperations.Verify.GetAllowedOperationForManualMatching(transaction)]).Contains<CABankTranOperations.MatchTo>(CABankTranOperations.MatchTo.Invoices);
    }

    public static bool ExpenseReceiptMatchingAllowed(
      CABankTran transaction,
      CABankTranOperations.MatchingType matchingType)
    {
      return ((IEnumerable<CABankTranOperations.MatchTo>) CABankTranOperations.AllowedMatchTo[matchingType == CABankTranOperations.MatchingType.AutoMatch ? CABankTranOperations.Verify.GetAllowedOperationForAutoMatching(transaction) : CABankTranOperations.Verify.GetAllowedOperationForManualMatching(transaction)]).Contains<CABankTranOperations.MatchTo>(CABankTranOperations.MatchTo.ExpenseReceipts);
    }

    public static bool DocumentCreationAllowed(
      CABankTran transaction,
      CABankTranOperations.MatchingType matchingType)
    {
      return ((IEnumerable<CABankTranOperations.MatchTo>) CABankTranOperations.AllowedMatchTo[matchingType == CABankTranOperations.MatchingType.AutoMatch ? CABankTranOperations.Verify.GetAllowedOperationForAutoMatching(transaction) : CABankTranOperations.Verify.GetAllowedOperationForManualMatching(transaction)]).Contains<CABankTranOperations.MatchTo>(CABankTranOperations.MatchTo.CreateDocument);
    }
  }
}
