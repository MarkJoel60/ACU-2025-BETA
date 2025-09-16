// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.OnAccrualSplitDefinedArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions;

public class OnAccrualSplitDefinedArgs
{
  public POReceiptLine ReceiptLineToBill { get; set; }

  public POAccrualSplit SplitToReverse { get; set; }

  public bool IsBaseQty { get; set; }

  public Decimal? ReceiptLineBillQty { get; set; }

  public Decimal? ReceiptLineBillAmount { get; set; }

  public Decimal? PPVAmount { get; set; }
}
