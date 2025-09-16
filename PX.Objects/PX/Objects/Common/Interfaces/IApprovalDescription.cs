// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Interfaces.IApprovalDescription
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Interfaces;

/// <summary>
/// The interface for the approvable documents that have descriptions of details.
/// The short description is a string that the approver can see in a column of the grid with documents.
/// </summary>
public interface IApprovalDescription
{
  int? CashAccountID { get; }

  string PaymentMethodID { get; }

  Decimal? CuryChargeAmt { get; }

  long? CuryInfoID { get; }
}
