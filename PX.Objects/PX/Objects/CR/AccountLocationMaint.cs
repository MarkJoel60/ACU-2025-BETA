// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.AccountLocationMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR.Extensions.Relational;
using System.Collections;

#nullable disable
namespace PX.Objects.CR;

public class AccountLocationMaint : LocationMaint
{
  public PXAction<PX.Objects.CR.Location> ViewCustomerLocation;
  public PXAction<PX.Objects.CR.Location> ViewVendorLocation;

  [PXUIField(DisplayName = "View Customer Location")]
  [PXButton]
  protected virtual IEnumerable viewCustomerLocation(PXAdapter adapter)
  {
    PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current;
    if (current != null && EnumerableExtensions.IsIn<string>(current.LocType, "CU", "VC"))
    {
      CustomerLocationMaint instance = PXGraph.CreateInstance<CustomerLocationMaint>();
      ((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Current = current;
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Vendor Location")]
  [PXButton]
  protected virtual IEnumerable viewVendorLocation(PXAdapter adapter)
  {
    PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current;
    if (current != null && EnumerableExtensions.IsIn<string>(current.LocType, "VE", "VC"))
    {
      VendorLocationMaint instance = PXGraph.CreateInstance<VendorLocationMaint>();
      ((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Current = current;
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Default Branch")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.cBranchID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Address.latitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Address.longitude> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.Location> e)
  {
    PX.Objects.CR.Location row = e.Row;
    if (row == null)
      return;
    ((PXAction) this.ViewCustomerLocation).SetEnabled(EnumerableExtensions.IsIn<string>(row.LocType, "CU", "VC"));
    ((PXAction) this.ViewVendorLocation).SetEnabled(EnumerableExtensions.IsIn<string>(row.LocType, "VE", "VC"));
  }

  /// <exclude />
  public class LocationBAccountSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedContactOverrideGraphExt>
  {
    protected override CRParentChild<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.RelatedMapping(typeof (BAccount))
      {
        RelatedID = typeof (BAccount.bAccountID),
        ChildID = typeof (BAccount.defContactID)
      };
    }

    protected override CRParentChild<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class LocationBAccountSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>
  {
    protected override CRParentChild<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.RelatedMapping(typeof (BAccount))
      {
        RelatedID = typeof (BAccount.bAccountID),
        ChildID = typeof (BAccount.defAddressID)
      };
    }

    protected override CRParentChild<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<AccountLocationMaint, AccountLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }
}
