// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Interfaces.ICreatePaymentDocument
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.SO.Interfaces;

public interface ICreatePaymentDocument
{
  int? CustomerID { get; }

  int? CustomerLocationID { get; }

  string PaymentMethodID { get; }

  int? PMInstanceID { get; }

  int? CashAccountID { get; }

  string CuryID { get; }

  Decimal? CuryUnpaidBalance { get; }

  Decimal? UnpaidBalance { get; }
}
