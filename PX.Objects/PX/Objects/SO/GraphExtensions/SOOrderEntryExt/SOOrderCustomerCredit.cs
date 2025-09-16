// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderCustomerCredit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOOrderCustomerCredit : SOOrderCustomerCreditExtension<SOOrderEntry>
{
  protected override ARSetup GetARSetup() => ((PXSelectBase<ARSetup>) this.Base.arsetup).Current;

  [PXOverride]
  public virtual IEnumerable ReleaseFromCreditHold(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseMethod)
  {
    EnumerableExtensions.ForEach<PX.Objects.SO.SOOrder>(adapter.Get<PX.Objects.SO.SOOrder>(), (Action<PX.Objects.SO.SOOrder>) (order =>
    {
      if (PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, order.CustomerID)?.Status == "C")
        throw new PXException("The customer status is 'Credit Hold'.");
    }));
    return baseMethod(adapter);
  }
}
