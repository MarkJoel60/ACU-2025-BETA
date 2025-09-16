// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CustomerMaintBridge
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.FS;

public class CustomerMaintBridge : PXGraph<CustomerMaintBridge, FSCustomer>
{
  public PXSelect<FSCustomer, Where2<Match<Current<AccessInfo.userName>>, And<Where<PX.Objects.AR.Customer.type, Equal<BAccountType.customerType>, Or<PX.Objects.AR.Customer.type, Equal<BAccountType.combinedType>>>>>> Customers;

  protected virtual void _(PX.Data.Events.RowSelected<FSCustomer> e)
  {
    if (e.Row != null)
    {
      FSCustomer row = e.Row;
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      int? baccountId = row.BAccountID;
      int num = 0;
      if (baccountId.GetValueOrDefault() >= num & baccountId.HasValue)
        ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Search<PX.Objects.AR.Customer.bAccountID>((object) row.BAccountID, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
      throw requiredException;
    }
  }
}
