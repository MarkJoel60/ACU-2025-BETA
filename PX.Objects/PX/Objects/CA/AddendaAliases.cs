// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AddendaAliases
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public static class AddendaAliases
{
  public static readonly Dictionary<string, string> Direct = new Dictionary<string, string>()
  {
    {
      "APPayment",
      "Payment"
    },
    {
      "APAdjust",
      "Adjustment"
    },
    {
      "APInvoice",
      "Bill"
    },
    {
      "CashAccountPaymentMethodDetail",
      "Remittances"
    },
    {
      "VendorPaymentMethodDetail",
      "APSettings"
    }
  };
  public static readonly Dictionary<string, string> Reverse = new Dictionary<string, string>()
  {
    {
      "Payment",
      "APPayment"
    },
    {
      "Adjustment",
      "APAdjust"
    },
    {
      "Bill",
      "APInvoice"
    },
    {
      "Remittances",
      "CashAccountPaymentMethodDetail"
    },
    {
      "APSettings",
      "VendorPaymentMethodDetail"
    }
  };
}
