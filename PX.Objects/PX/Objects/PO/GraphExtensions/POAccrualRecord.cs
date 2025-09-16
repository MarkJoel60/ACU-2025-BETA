// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POAccrualRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions;

public class POAccrualRecord
{
  public string ReceivedUOM { get; set; }

  public Decimal? ReceivedQty { get; set; } = new Decimal?(0M);

  public Decimal? BaseReceivedQty { get; set; } = new Decimal?(0M);

  public Decimal? ReceivedCost { get; set; } = new Decimal?(0M);

  public string BilledUOM { get; set; }

  public Decimal? BilledQty { get; set; } = new Decimal?(0M);

  public Decimal? BaseBilledQty { get; set; } = new Decimal?(0M);

  public string BillCuryID { get; set; }

  public Decimal? CuryBilledAmt { get; set; } = new Decimal?(0M);

  public Decimal? BilledAmt { get; set; } = new Decimal?(0M);

  public Decimal? CuryBilledCost { get; set; } = new Decimal?(0M);

  public Decimal? BilledCost { get; set; } = new Decimal?(0M);

  public Decimal? CuryBilledDiscAmt { get; set; } = new Decimal?(0M);

  public Decimal? BilledDiscAmt { get; set; } = new Decimal?(0M);

  public Decimal? PPVAmt { get; set; } = new Decimal?(0M);

  public static POAccrualRecord FromPOAccrualStatus(POAccrualStatus s)
  {
    return new POAccrualRecord()
    {
      ReceivedUOM = s.ReceivedUOM,
      ReceivedQty = s.ReceivedQty,
      BaseReceivedQty = s.BaseReceivedQty,
      ReceivedCost = s.ReceivedCost,
      BilledUOM = s.BilledUOM,
      BilledQty = s.BilledQty,
      BaseBilledQty = s.BaseBilledQty,
      BillCuryID = s.BillCuryID,
      CuryBilledAmt = s.CuryBilledAmt,
      BilledAmt = s.BilledAmt,
      CuryBilledCost = s.CuryBilledCost,
      BilledCost = s.BilledCost,
      CuryBilledDiscAmt = s.CuryBilledDiscAmt,
      BilledDiscAmt = s.BilledDiscAmt,
      PPVAmt = s.PPVAmt
    };
  }
}
