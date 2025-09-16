// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.Scopes.AllowZeroReceiptQtyScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.PO.Scopes;

[PXInternalUseOnly]
public class AllowZeroReceiptQtyScope : IDisposable
{
  private static readonly string scopeKeyPrefix = nameof (AllowZeroReceiptQtyScope);
  private readonly string receiptKey;

  private static string GetKeyForReceipt(POReceipt receipt)
  {
    return $"{receipt.ReceiptNbr}.{receipt.ReceiptType}";
  }

  public AllowZeroReceiptQtyScope(POReceipt receipt)
  {
    this.receiptKey = AllowZeroReceiptQtyScope.GetKeyForReceipt(receipt);
    PXContext.SetSlot<bool>($"{AllowZeroReceiptQtyScope.scopeKeyPrefix}.{this.receiptKey}", true);
  }

  void IDisposable.Dispose()
  {
    PXContext.SetSlot<bool>($"{AllowZeroReceiptQtyScope.scopeKeyPrefix}.{this.receiptKey}", false);
  }

  public static bool IsActive(POReceipt receipt)
  {
    return PXContext.GetSlot<bool>($"{AllowZeroReceiptQtyScope.scopeKeyPrefix}.{AllowZeroReceiptQtyScope.GetKeyForReceipt(receipt)}");
  }
}
