// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteBuildingMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.IN;

public class INSiteBuildingMaint : PXGraph<
#nullable disable
INSiteBuildingMaint, INSiteBuilding>
{
  public FbqlSelect<SelectFromBase<INSiteBuilding, TypeArrayOf<IFbqlJoin>.Empty>, INSiteBuilding>.View Buildings;
  public FbqlSelect<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<Field<INSite.buildingID>.IsRelatedTo<INSiteBuilding.buildingID>.AsSimpleKey.WithTablesOf<INSiteBuilding, INSite>, INSiteBuilding, INSite>.SameAsCurrent>, And<BqlOperand<
  #nullable enable
  INSite.siteID, IBqlInt>.IsNotEqual<
  #nullable disable
  SiteAnyAttribute.transitSiteID>>>>.And<Match<BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>, 
  #nullable disable
  INSite>.View Sites;
  public PXSetup<PX.Objects.GL.Branch>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INSiteBuilding.branchID, IBqlInt>.AsOptional>> Branch;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<PX.Objects.CR.Address, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Address.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.GL.Branch.bAccountID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.CR.Address.addressID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INSiteBuilding.addressID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.CR.Address>.View Address;
  public PXChangeID<INSiteBuilding, INSiteBuilding.buildingCD> changeID;
  public PXAction<INSiteBuilding> validateAddresses;
  public PXAction<INSiteBuilding> viewOnMap;

  public INSiteBuildingMaint()
  {
    ((PXSelectBase) this.Sites).Cache.AllowInsert = ((PXSelectBase) this.Sites).Cache.AllowUpdate = false;
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    if (((PXSelectBase<INSiteBuilding>) this.Buildings).Current != null)
    {
      PX.Objects.CR.Address current = ((PXSelectBase<PX.Objects.CR.Address>) this.Address).Current;
      if (current != null)
      {
        bool? isValidated = current.IsValidated;
        bool flag = false;
        if (isValidated.GetValueOrDefault() == flag & isValidated.HasValue)
          PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this, current, true, true);
      }
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable ViewOnMap(PXAdapter adapter)
  {
    BAccountUtility.ViewOnMap(((PXSelectBase<PX.Objects.CR.Address>) this.Address).Current);
    return adapter.Get();
  }

  [PXDefault(typeof (SearchFor<PX.Objects.GL.Branch.countryID>.In<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<INSiteBuilding.branchID, IBqlInt>.FromCurrent>>>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Address.countryID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<INSiteBuilding> e)
  {
    try
    {
      ((PXSelectBase) this.Address).Cache.Insert((object) new PX.Objects.CR.Address()
      {
        BAccountID = (int?) ((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current?.BAccountID
      });
    }
    finally
    {
      ((PXSelectBase) this.Address).Cache.IsDirty = false;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INSiteBuilding> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INSiteBuilding>>) e).Cache.ObjectsEqual<INSiteBuilding.branchID>((object) e.Row, (object) e.OldRow))
      return;
    bool flag = false;
    foreach (PX.Objects.CR.Address address in ((PXSelectBase) this.Address).Cache.Inserted)
    {
      address.BAccountID = (int?) ((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current?.BAccountID;
      address.CountryID = ((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current?.CountryID;
      flag = true;
    }
    if (!flag)
    {
      PX.Objects.CR.Address address = (PX.Objects.CR.Address) ((PXSelectBase) this.Address).View.SelectSingleBound(new object[2]
      {
        ((PXSelectBase) this.Branch).View.SelectSingleBound(new object[1]
        {
          (object) e.OldRow
        }, Array.Empty<object>()),
        (object) e.OldRow
      }, Array.Empty<object>()) ?? new PX.Objects.CR.Address();
      address.BAccountID = ((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current.BAccountID;
      address.CountryID = ((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current.CountryID;
      address.AddressID = new int?();
      ((PXSelectBase) this.Address).Cache.Insert((object) address);
    }
    else
      ((PXSelectBase) this.Address).Cache.Normalize();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INSiteBuilding> e)
  {
    ((PXSelectBase) this.Address).Cache.Delete((object) ((PXSelectBase<PX.Objects.CR.Address>) this.Address).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<INSite> e)
  {
    if (e.Row == null)
      return;
    e.Row.BuildingID = new int?();
    ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<INSite>>) e).Cache.Update((object) e.Row);
    e.Cancel = true;
    ((PXSelectBase) this.Sites).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.CR.Address> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase<INSiteBuilding>) this.Buildings).Current.AddressID = e.Row.AddressID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Address, PX.Objects.CR.Address.bAccountID> e)
  {
    if (((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Address, PX.Objects.CR.Address.bAccountID>, PX.Objects.CR.Address, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current.BAccountID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Address, PX.Objects.CR.Address.bAccountID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Address, PX.Objects.CR.Address.countryID> e)
  {
    e.Row.State = (string) null;
    e.Row.PostalCode = (string) null;
  }

  /// <exclude />
  public class INSiteBuildingMaintAddressLookupExtension : 
    AddressLookupExtension<INSiteBuildingMaint, INSiteBuilding, PX.Objects.CR.Address>
  {
    protected override string AddressView => "Address";

    protected override string ViewOnMap => "viewOnMap";
  }
}
