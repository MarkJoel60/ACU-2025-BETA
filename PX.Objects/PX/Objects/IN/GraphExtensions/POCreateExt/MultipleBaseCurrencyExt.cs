// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.POCreateExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.PO;
using PX.Objects.SO.POCreateExt;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.POCreateExt;

public class MultipleBaseCurrencyExt : PXGraphExtension<POCreateSOExtension, POCreate>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<POFixedDemand>) ((PXGraphExtension<POCreate>) this).Base.FixedDemand).Join<InnerJoin<INSite, On<POFixedDemand.siteID, Equal<INSite.siteID>>>>();
    ((PXSelectBase<POFixedDemand>) ((PXGraphExtension<POCreate>) this).Base.FixedDemand).WhereAnd<Where<INSite.baseCuryID, EqualBaseCuryID<Current2<POCreate.POCreateFilter.branchID>>>>();
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (POCreate.POCreateFilter.branchID), null)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POCreate.POCreateFilter.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<POCreate.POCreateFilter.branchID>, IsNull, Or<Customer.baseCuryID, EqualBaseCuryID<Current2<POCreate.POCreateFilter.branchID>>, Or<Customer.baseCuryID, IsNull>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (Customer.cOrgBAccountID), typeof (Customer.acctCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOxPOCreateFilter.customerID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<POCreate.POCreateFilter.branchID>, IsNull, Or<PX.Objects.AP.Vendor.baseCuryID, EqualBaseCuryID<Current2<POCreate.POCreateFilter.branchID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (PX.Objects.AP.Vendor.vOrgBAccountID), typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POCreate.POCreateFilter.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (POCreate.POCreateFilter.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<POFixedDemand.pOSiteID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (POCreate.POCreateFilter.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<POFixedDemand.sourceSiteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<POCreate.POCreateFilter.branchID>, IsNull, Or<PX.Objects.AP.Vendor.baseCuryID, EqualBaseCuryID<Current2<POCreate.POCreateFilter.branchID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (PX.Objects.AP.Vendor.vOrgBAccountID), typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<POFixedDemand.vendorID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<POFixedDemand> e)
  {
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) ((PXGraphExtension<POCreate>) this).Base, (int?) e.Row?.VendorID);
    if (vendor != null && EnumerableExtensions.IsNotIn<int?>(vendor.VOrgBAccountID, new int?(0), new int?()))
    {
      if (!((BqlCommand) new Select<POCreate.POCreateFilter, Where<POCreate.POCreateFilter.branchID, InsideBranchesOf<Required<PX.Objects.AP.Vendor.vOrgBAccountID>>>>()).Meet(((PXSelectBase) ((PXGraphExtension<POCreate>) this).Base.Filter).Cache, (object) ((PXSelectBase<POCreate.POCreateFilter>) ((PXGraphExtension<POCreate>) this).Base.Filter).Current, new object[1]
      {
        (object) vendor.VOrgBAccountID
      }))
      {
        PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) ((PXGraphExtension<POCreate>) this).Base, (int?) ((PXSelectBase<POCreate.POCreateFilter>) ((PXGraphExtension<POCreate>) this).Base.Filter).Current?.BranchID);
        propertyException = new PXSetPropertyException("The usage of the {0} vendor is restricted in the {1} branch.", (PXErrorLevel) 3, new object[2]
        {
          (object) vendor.AcctCD.TrimEnd(),
          (object) branch?.BranchCD
        });
      }
    }
    if (PXUIFieldAttribute.GetErrorOnly<POFixedDemand.vendorID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POFixedDemand>>) e).Cache, (object) e.Row) != null)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POFixedDemand>>) e).Cache.RaiseExceptionHandling<POFixedDemand.vendorID>((object) e.Row, (object) vendor?.AcctCD, (Exception) propertyException);
  }
}
