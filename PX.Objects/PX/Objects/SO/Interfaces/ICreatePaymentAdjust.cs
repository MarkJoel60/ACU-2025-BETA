// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Interfaces.ICreatePaymentAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.SO.Interfaces;

public interface ICreatePaymentAdjust
{
  string AdjdOrderNbr { get; }

  string AdjdOrderType { get; }

  string AdjgRefNbr { get; }

  string AdjgDocType { get; }

  Decimal? CuryAdjdAmt { get; }

  Decimal? CuryAdjgAmt { get; }

  bool? IsCCPayment { get; }

  bool? IsCCAuthorized { get; }

  bool? IsCCCaptured { get; }

  bool? Voided { get; }

  bool? Released { get; }
}
