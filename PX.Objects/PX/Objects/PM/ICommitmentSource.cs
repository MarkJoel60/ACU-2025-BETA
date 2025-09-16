// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ICommitmentSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PM;

internal interface ICommitmentSource
{
  Guid? CommitmentID { get; }

  int? ExpenseAcctID { get; }

  int? ProjectID { get; }

  int? TaskID { get; }

  int? InventoryID { get; }

  int? CostCodeID { get; }

  int? BranchID { get; }

  string UOM { get; }

  long? CuryInfoID { get; }

  Decimal? OrigExtCost { get; }

  Decimal? OrigOrderQty { get; }

  Decimal? CuryExtCost { get; }

  Decimal? ExtCost { get; }

  Decimal? OrderQty { get; }

  Decimal? CuryRetainageAmt { get; }

  Decimal? CompletedQty { get; }

  Decimal? ReceivedQty { get; }

  Decimal? BilledQty { get; }

  Decimal? CuryBilledAmt { get; }

  Decimal? BilledAmt { get; }

  Decimal? RetainageAmt { get; }

  bool? Completed { get; }

  bool? Closed { get; }

  bool? Cancelled { get; }

  string CompletePOLine { get; }

  string LineType { get; }
}
