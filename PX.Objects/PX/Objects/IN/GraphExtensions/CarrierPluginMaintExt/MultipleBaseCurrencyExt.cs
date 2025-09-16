// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.CarrierPluginMaintExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.CarrierPluginMaintExt;

public class MultipleBaseCurrencyExt : PXGraphExtension<CarrierPluginMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictorWithParameters(typeof (Where<Current2<CarrierPlugin.siteID>, IsNull, Or<Customer.baseCuryID, EqualSiteBaseCuryID<Current2<CarrierPlugin.siteID>>, Or<Customer.baseCuryID, IsNull>>>), "The {0} branch specified for the {1} warehouse is restricted in the {2} account.", new Type[] {typeof (Selector<CarrierPlugin.siteID, INSite.branchID>), typeof (Current<CarrierPlugin.siteID>), typeof (Customer.acctCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CarrierPluginCustomer.customerID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CarrierPlugin> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CarrierPlugin>>) e).Cache.ObjectsEqual<CarrierPlugin.siteID>((object) e.OldRow, (object) e.Row) || string.Equals(((INSite) PXSelectorAttribute.Select<CarrierPlugin.siteID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CarrierPlugin>>) e).Cache, (object) e.Row))?.BaseCuryID, ((INSite) PXSelectorAttribute.Select<CarrierPlugin.siteID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CarrierPlugin>>) e).Cache, (object) e.OldRow))?.BaseCuryID, StringComparison.OrdinalIgnoreCase))
      return;
    foreach (PXResult<CarrierPluginCustomer> pxResult in ((PXSelectBase<CarrierPluginCustomer>) this.Base.CustomerAccounts).Select(Array.Empty<object>()))
    {
      CarrierPluginCustomer row = PXResult<CarrierPluginCustomer>.op_Implicit(pxResult);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.CustomerAccounts).Cache, (object) row, true);
      ((PXSelectBase) this.Base.CustomerAccounts).Cache.VerifyFieldAndRaiseException<CarrierPluginCustomer.customerID>((object) row);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CarrierPluginCustomer> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CarrierPluginCustomer>>) e).Cache.VerifyFieldAndRaiseException<CarrierPluginCustomer.customerID>((object) e.Row);
  }
}
