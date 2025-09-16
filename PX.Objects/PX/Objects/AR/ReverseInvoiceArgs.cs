// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ReverseInvoiceArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR;

/// <exclude />
public class ReverseInvoiceArgs
{
  public bool ApplyToOriginalDocument { get; set; }

  public bool PreserveOriginalDocumentSign { get; set; }

  public bool? OverrideDocumentHold { get; set; }

  public string OverrideDocumentDescr { get; set; }

  public bool? ReverseINTransaction { get; set; }

  public ReverseInvoiceArgs.CopyOption DateOption { get; set; }

  public DateTime? DocumentDate { get; set; }

  public string DocumentFinPeriodID { get; set; }

  public ReverseInvoiceArgs.CopyOption CurrencyRateOption { get; set; }

  public PX.Objects.CM.Extensions.CurrencyInfo CurrencyRate { get; set; }

  public enum CopyOption
  {
    SetOriginal,
    SetDefault,
    Override,
  }
}
