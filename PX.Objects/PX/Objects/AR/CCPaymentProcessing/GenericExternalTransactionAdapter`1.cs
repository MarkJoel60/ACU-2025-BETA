// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.GenericExternalTransactionAdapter`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

internal class GenericExternalTransactionAdapter<T> : IExternalTransactionAdapter where T : class, IBqlTable, IExternalTransaction, new()
{
  private PXSelectBase<T> externalTransaction;

  public PXCache Cache => ((PXSelectBase) this.externalTransaction).Cache;

  public GenericExternalTransactionAdapter(PXSelectBase<T> externalTransaction)
  {
    this.externalTransaction = externalTransaction;
  }

  public IEnumerable<IExternalTransaction> Select(params object[] arguments)
  {
    foreach (PXResult<T> pxResult in this.externalTransaction.Select(arguments))
      yield return (IExternalTransaction) PXResult<T>.op_Implicit(pxResult);
  }
}
