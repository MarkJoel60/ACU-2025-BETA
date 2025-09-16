// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedMappingTarget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class CABankFeedMappingTarget
{
  public const string ExtRefNbr = "ExtRefNbr";
  public const string TranDesc = "TranDesc";
  public const string UserDesc = "UserDesc";
  public const string CardNumber = "CardNumber";
  public const string InvoiceNbr = "InvoiceNbr";
  public const string PayeeName = "PayeeName";
  public const string TranCode = "TranCode";
  public const string AccountName = "AccountName";
  public const string TransactionID = "TransactionID";
  public const string Date = "Date";
  public const string Amount = "Amount";
  public const string CreditAmount = "CreditAmount";
  public const string DebitAmount = "DebitAmount";
  public const string DebitCreditParameter = "DebitCreditParameter";
  public const string Name = "Name";
  public const string InvoiceInfo = "InvoiceInfo";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CABankFeedMappingTarget.ListAttribute.GetTargets())
    {
    }

    public static (string, string)[] GetTargets()
    {
      return new (string, string)[7]
      {
        ("ExtRefNbr", "Ext. Ref. Nbr."),
        ("TranDesc", "Tran. Desc"),
        ("UserDesc", "Custom Tran. Desc."),
        ("CardNumber", "Card Number"),
        ("InvoiceNbr", "Invoice Nbr."),
        ("PayeeName", "Payee/Payer"),
        ("TranCode", "Tran. Code")
      };
    }
  }
}
