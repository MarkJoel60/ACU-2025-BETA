// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.IAddARTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.CA;

public interface IAddARTransaction
{
  PX.Objects.AR.ARPayment InitializeARPayment(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    bool aOnHold);

  void InitializeCurrencyInfo(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    PX.Objects.AR.ARPayment doc);

  Decimal InitializeARAdjustment(ARPaymentEntry graph, ARAdjust adjustment, Decimal curyAppliedAmt);
}
