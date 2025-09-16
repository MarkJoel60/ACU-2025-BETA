// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptEntryReportsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PO;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public class POReceiptEntryReportsAttribute : PXStringListAttribute
{
  private static Tuple<string, string>[] ValuesToLabels
  {
    get
    {
      return new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("PO646000", "Print Purchase Receipt"),
        PXStringListAttribute.Pair("PO632000", "Purchase Receipt Billing History"),
        PXStringListAttribute.Pair("PO622000", "Purchase Receipt Allocated and Backordered")
      };
    }
  }

  public POReceiptEntryReportsAttribute()
    : base(POReceiptEntryReportsAttribute.ValuesToLabels)
  {
  }

  public static class Values
  {
    public const string PurchaseReceipt = "PO646000";
    public const string BillingDetails = "PO632000";
    public const string Allocated = "PO622000";
  }

  [PXLocalizable]
  public static class DisplayNames
  {
    public const string PurchaseReceipt = "Print Purchase Receipt";
    public const string BillingDetails = "Purchase Receipt Billing History";
    public const string Allocated = "Purchase Receipt Allocated and Backordered";
  }
}
