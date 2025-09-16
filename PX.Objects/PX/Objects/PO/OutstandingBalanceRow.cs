// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.OutstandingBalanceRow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common;
using System;

#nullable disable
namespace PX.Objects.PO;

internal class OutstandingBalanceRow : IAdjustmentStub
{
  public string StubNbr { get; set; }

  public int? CashAccountID { get; set; }

  public string PaymentMethodID { get; set; }

  public bool Persistent => false;

  public Decimal? CuryOutstandingBalance { get; set; }

  public DateTime? OutstandingBalanceDate { get; set; }

  public Decimal? CuryAdjgAmt => new Decimal?(0M);

  public Decimal? CuryAdjgDiscAmt => new Decimal?(0M);

  public string AdjdDocType => (string) null;

  public bool? IsRequest => new bool?();
}
