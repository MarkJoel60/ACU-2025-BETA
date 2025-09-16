// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.VendorSVATTaxEntryRefNbr
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public class VendorSVATTaxEntryRefNbr
{
  public const 
  #nullable disable
  string DocumentRefNbr = "D";
  public const string PaymentRefNbr = "P";
  public const string TaxInvoiceNbr = "T";
  public const string ManuallyEntered = "M";

  public class InputListAttribute : PXStringListAttribute
  {
    public InputListAttribute()
      : base(new string[3]{ "D", "P", "M" }, new string[3]
      {
        "Document Ref. Nbr.",
        "Payment Ref. Nbr.",
        "Manually Entered"
      })
    {
    }
  }

  public class OutputListAttribute : PXStringListAttribute
  {
    public OutputListAttribute()
      : base(new string[4]{ "D", "P", "T", "M" }, new string[4]
      {
        "Document Ref. Nbr.",
        "Payment Ref. Nbr.",
        "Tax Invoice Nbr.",
        "Manually Entered"
      })
    {
    }
  }

  public class documentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    VendorSVATTaxEntryRefNbr.documentRefNbr>
  {
    public documentRefNbr()
      : base("D")
    {
    }
  }

  public class paymentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    VendorSVATTaxEntryRefNbr.paymentRefNbr>
  {
    public paymentRefNbr()
      : base("P")
    {
    }
  }

  public class taxInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    VendorSVATTaxEntryRefNbr.taxInvoiceNbr>
  {
    public taxInvoiceNbr()
      : base("T")
    {
    }
  }

  public class manuallyEntered : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    VendorSVATTaxEntryRefNbr.manuallyEntered>
  {
    public manuallyEntered()
      : base("M")
    {
    }
  }
}
