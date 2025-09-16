// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MultiCurrency.CABankTransactionsMatchingProcessMultiCurrency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA.MultiCurrency;

public sealed class CABankTransactionsMatchingProcessMultiCurrency : 
  CABankTransactionsBaseMultiCurrency<CABankMatchingProcess>
{
  protected override PXSelectBase[] GetChildren()
  {
    return new PXSelectBase[2]
    {
      (PXSelectBase) this.Base.TranMatch,
      (PXSelectBase) this.Base.TranSplit
    };
  }

  protected override PX.Objects.Extensions.MultiCurrency.CurySource CurrentSourceSelect()
  {
    return ((PXSelectBase<PX.Objects.Extensions.MultiCurrency.CurySource>) this.CurySource).Current ?? PXResultset<PX.Objects.Extensions.MultiCurrency.CurySource>.op_Implicit(((PXSelectBase<PX.Objects.Extensions.MultiCurrency.CurySource>) this.CurySource).Select(Array.Empty<object>()));
  }
}
