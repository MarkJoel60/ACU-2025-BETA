// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerLocationMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Discount;
using PX.Objects.CR;
using PX.Objects.CR.Extensions.Relational;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class CustomerLocationMaint : LocationMaint
{
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>> Customer;
  public PXAction<PX.Objects.CR.Location> ViewAccountLocation;

  public CustomerLocationMaint()
  {
    ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Join<LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>>>();
    ((PXSelectBase<PX.Objects.CR.Location>) this.Location).WhereAnd<Where<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>();
    ((PXSelectBase<PX.Objects.CR.Location>) this.Location).WhereAnd<Where<PX.Objects.CR.Location.locType, Equal<LocTypeList.customerLoc>, Or<PX.Objects.CR.Location.locType, Equal<LocTypeList.combinedLoc>>>>();
  }

  [PXUIField]
  [PXCancelButton]
  protected override IEnumerable Cancel(PXAdapter adapter)
  {
    int? nullable1 = ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current != null ? ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current.BAccountID : new int?();
    IEnumerator enumerator1 = ((PXAction) new PXCancel<PX.Objects.CR.Location>((PXGraph) this, nameof (Cancel))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator1.MoveNext())
      {
        object current = enumerator1.Current;
        PX.Objects.CR.Location location = PXResult.Unwrap<PX.Objects.CR.Location>(current);
        if (!((PXGraph) this).IsImport && ((PXSelectBase) this.Location).Cache.GetStatus((object) location) == 2)
        {
          int? nullable2 = nullable1;
          int? baccountId = location.BAccountID;
          if (!(nullable2.GetValueOrDefault() == baccountId.GetValueOrDefault() & nullable2.HasValue == baccountId.HasValue) || string.IsNullOrEmpty(location.LocationCD))
          {
            IEnumerator enumerator2 = ((PXAction) this.First).Press(adapter).GetEnumerator();
            try
            {
              if (enumerator2.MoveNext())
                return (IEnumerable) new object[1]
                {
                  enumerator2.Current
                };
            }
            finally
            {
              if (enumerator2 is IDisposable disposable)
                disposable.Dispose();
            }
            location.LocationCD = (string) null;
            return (IEnumerable) new object[1]{ current };
          }
        }
        return (IEnumerable) new object[1]{ current };
      }
    }
    finally
    {
      if (enumerator1 is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField]
  [PXPreviousButton]
  protected override IEnumerable Previous(PXAdapter adapter)
  {
    IEnumerator enumerator = ((PXAction) new PXPrevious<PX.Objects.CR.Location>((PXGraph) this, "Prev")).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        if (((PXSelectBase) this.Location).Cache.GetStatus((object) PXResult.Unwrap<PX.Objects.CR.Location>(current)) == 2)
          return ((PXAction) this.Last).Press(adapter);
        return (IEnumerable) new object[1]{ current };
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField]
  [PXNextButton]
  protected override IEnumerable Next(PXAdapter adapter)
  {
    IEnumerator enumerator = ((PXAction) new PXNext<PX.Objects.CR.Location>((PXGraph) this, nameof (Next))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        if (((PXSelectBase) this.Location).Cache.GetStatus((object) PXResult.Unwrap<PX.Objects.CR.Location>(current)) == 2)
          return ((PXAction) this.First).Press(adapter);
        return (IEnumerable) new object[1]{ current };
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField(DisplayName = "View Account Location")]
  [PXButton]
  protected virtual void viewAccountLocation()
  {
    PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current;
    if (current == null)
      return;
    AccountLocationMaint instance = PXGraph.CreateInstance<AccountLocationMaint>();
    ((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Current = current;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
  }

  protected override void Location_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    base.Location_RowPersisted(sender, e);
    if (e.TranStatus != 1)
      return;
    DiscountEngine.RemoveFromCachedCustomerPriceClasses(((PX.Objects.CR.Location) e.Row).BAccountID);
  }

  [PXDefault(typeof (PX.Objects.CR.Location.bAccountID))]
  [PX.Objects.AR.Customer(typeof (Search<PX.Objects.AR.Customer.bAccountID, Where<Where<PX.Objects.AR.Customer.type, Equal<BAccountType.customerType>, Or<PX.Objects.AR.Customer.type, Equal<BAccountType.prospectType>, Or<PX.Objects.AR.Customer.type, Equal<BAccountType.combinedType>>>>>>), IsKey = true, TabOrder = 0)]
  [PXParent(typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>>))]
  protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.bAccountID> e)
  {
  }

  [PXUIField(DisplayName = "Default Branch")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.cBranchID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Address.latitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Address.longitude> e)
  {
  }

  protected override void EstablishVTaxZoneRule(Action<System.Type, bool> setCheck)
  {
    setCheck(typeof (PX.Objects.CR.Location.vTaxZoneID), false);
  }

  /// <exclude />
  public class LocationBAccountSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedContactOverrideGraphExt>
  {
    protected override CRParentChild<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.AR.Customer))
      {
        RelatedID = typeof (PX.Objects.AR.Customer.bAccountID),
        ChildID = typeof (PX.Objects.AR.Customer.defContactID)
      };
    }

    protected override CRParentChild<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class LocationBAccountSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>
  {
    protected override CRParentChild<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.AR.Customer))
      {
        RelatedID = typeof (PX.Objects.AR.Customer.bAccountID),
        ChildID = typeof (PX.Objects.AR.Customer.defAddressID)
      };
    }

    protected override CRParentChild<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<CustomerLocationMaint, CustomerLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }
}
