// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.PPVCalculationResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions;

public class PPVCalculationResult
{
  public Decimal PPVAmount { get; set; }

  public bool IsBaseQty { get; set; }

  public Decimal Sign { get; set; }

  public Decimal? BillQty { get; set; }

  public Decimal? AccrualBilledQty { get; set; }

  public PPVCalculationResult(Decimal ppvAmount) => this.PPVAmount = ppvAmount;
}
