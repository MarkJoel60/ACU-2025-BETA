// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractAutoNumberAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class ServiceContractAutoNumberAttribute : AlternateAutoNumberAttribute
{
  protected override string GetInitialRefNbr(string baseRefNbr) => "000001";

  /// <summary>
  /// Allows to calculate the <c>RefNbr</c> sequence when trying to insert a new register
  /// It's called from the Persisting event of FSServiceContract.
  /// </summary>
  protected override bool SetRefNbr(PXCache cache, object row)
  {
    ((FSServiceContract) row).CustomerContractNbr = this.GetNextRefNbr((string) null, PXResultset<FSServiceContract>.op_Implicit(PXSelectBase<FSServiceContract, PXSelectGroupBy<FSServiceContract, Where<FSServiceContract.customerID, Equal<Current<FSServiceContract.customerID>>>, Aggregate<Max<FSServiceContract.customerContractNbr, GroupBy<FSServiceContract.customerID>>>>.Config>.Select(cache.Graph, Array.Empty<object>()))?.CustomerContractNbr);
    return true;
  }
}
