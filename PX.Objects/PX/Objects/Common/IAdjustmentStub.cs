// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.IAdjustmentStub
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common;

public interface IAdjustmentStub
{
  string StubNbr { get; set; }

  int? CashAccountID { get; set; }

  string PaymentMethodID { get; set; }

  bool Persistent { get; }

  Decimal? CuryAdjgAmt { get; }

  Decimal? CuryAdjgDiscAmt { get; }

  string AdjdDocType { get; }

  Decimal? CuryOutstandingBalance { get; }

  DateTime? OutstandingBalanceDate { get; }

  bool? IsRequest { get; }
}
