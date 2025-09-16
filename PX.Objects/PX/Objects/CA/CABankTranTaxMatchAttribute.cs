// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranTaxMatchAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.TX;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CABankTranTaxMatchAttribute(
  Type parentType,
  Type taxType,
  Type taxSumType,
  Type calcMode = null,
  Type parentBranchIDField = null) : CABankTranTaxAttribute(parentType, taxType, taxSumType, calcMode, parentBranchIDField)
{
  protected override object GetCurrent(PXGraph graph)
  {
    return (object) ((PXSelectBase<CABankTran>) ((CABankMatchingProcess) graph).CABankTran).Current;
  }

  public override void CacheAttached(PXCache sender)
  {
    if (sender.Graph is CABankMatchingProcess)
      base.CacheAttached(sender);
    else
      this.TaxCalc = TaxCalc.NoCalc;
  }
}
