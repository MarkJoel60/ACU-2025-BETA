// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.IAddAPTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AP;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public interface IAddAPTransaction
{
  PX.Objects.AP.APPayment InitializeAPPayment(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    IList<ICADocAdjust> aAdjustments,
    bool aOnHold);

  void InitializeCurrencyInfo(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    PX.Objects.AP.APPayment doc);

  APAdjust InitializeAPAdjustment(APPaymentEntry graph, ICADocAdjust adjustment);
}
