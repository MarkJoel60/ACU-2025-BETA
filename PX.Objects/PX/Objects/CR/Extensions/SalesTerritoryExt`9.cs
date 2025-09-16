// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SalesTerritoryExt`9
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <exclude />
public abstract class SalesTerritoryExt<TGraph, TMaster, FMasterAddressID, FOverrideSalesTerritory, FSalesTerritoryID, TAddress, FAddressAddressID, FAddressCountryID, FAddressState> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TMaster : class, IBqlTable, new()
  where FMasterAddressID : class, IBqlField
  where FOverrideSalesTerritory : class, IBqlField
  where FSalesTerritoryID : class, IBqlField
  where TAddress : class, IAddressBase, IBqlTable, new()
  where FAddressAddressID : class, IBqlField
  where FAddressCountryID : class, IBqlField
  where FAddressState : class, IBqlField
{
  protected static bool IsExtensionActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.salesTerritoryManagement>();
  }

  protected abstract IAddressBase CurrentAddress { get; }

  protected virtual void _(Events.RowInserted<TMaster> e)
  {
    if ((object) e.Row == null)
      return;
    this.AssignDefaultSalesTerritory(this.CurrentAddress);
  }

  protected virtual void _(Events.FieldUpdated<FMasterAddressID> e)
  {
    if (e.Row == null)
      return;
    this.AssignDefaultSalesTerritory(this.CurrentAddress);
  }

  protected virtual void _(Events.RowInserted<TAddress> e)
  {
    if ((object) e.Row == null)
      return;
    this.AssignDefaultSalesTerritory((IAddressBase) e.Row);
  }

  protected virtual void _(Events.RowUpdated<TAddress> e)
  {
    IAddressBase currentAddress = this.CurrentAddress;
    if ((object) e.Row == null || !((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TAddress>>) e).Cache.ObjectsEqual<FAddressAddressID>((object) e.Row, (object) currentAddress) || ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TAddress>>) e).Cache.ObjectsEqual<FAddressCountryID, FAddressState>((object) e.Row, (object) e.OldRow))
      return;
    this.AssignDefaultSalesTerritory((IAddressBase) e.Row);
  }

  protected virtual void _(
    Events.FieldUpdated<TMaster, FOverrideSalesTerritory> e)
  {
    if ((object) e.Row == null || !(e.NewValue is bool newValue) || newValue)
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TMaster, FOverrideSalesTerritory>>) e).Cache.SetValue<FSalesTerritoryID>((object) e.Row, (object) this.GetSalesTerritory(this.CurrentAddress));
  }

  protected virtual void UpdateRelatedContacts(
    int? mainContactID,
    int? addressID,
    string salesTerritoryID)
  {
    foreach (PXResult<Contact> pxResult in PXSelectBase<Contact, PXViewOf<Contact>.BasedOn<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.defAddressID, Equal<P.AsInt>>>>, And<BqlOperand<Contact.contactID, IBqlInt>.IsNotEqual<P.AsInt>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.overrideSalesTerritory, Equal<False>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.salesTerritoryID, IsNull>>>>.Or<BqlOperand<Contact.salesTerritoryID, IBqlString>.IsNotEqual<P.AsString>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) addressID,
      (object) mainContactID,
      (object) salesTerritoryID
    }))
    {
      Contact contact = PXResult<Contact>.op_Implicit(pxResult);
      contact.SalesTerritoryID = salesTerritoryID;
      this.Base.Caches[typeof (Contact)].Update((object) contact);
    }
  }

  protected virtual void UpdateRelatedBAccount(int? addressID, string salesTerritoryID)
  {
    foreach (PXResult<BAccount> pxResult in PXSelectBase<BAccount, PXViewOf<BAccount>.BasedOn<SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.defAddressID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.overrideSalesTerritory, Equal<False>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.salesTerritoryID, IsNull>>>>.Or<BqlOperand<BAccount.salesTerritoryID, IBqlString>.IsNotEqual<P.AsString>>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) addressID,
      (object) salesTerritoryID
    }))
    {
      BAccount baccount = PXResult<BAccount>.op_Implicit(pxResult);
      baccount.SalesTerritoryID = salesTerritoryID;
      this.Base.Caches[typeof (BAccount)].Update((object) baccount);
    }
  }

  protected virtual void AssignDefaultSalesTerritory(IAddressBase address)
  {
    object currentPrimaryObject = GraphHelper.GetCurrentPrimaryObject((PXGraph) this.Base);
    if (currentPrimaryObject == null)
      return;
    PXCache primaryCache = GraphHelper.GetPrimaryCache((PXGraph) this.Base);
    if (primaryCache.GetValue<FOverrideSalesTerritory>(currentPrimaryObject) is bool flag && flag)
      return;
    primaryCache.SetValue<FSalesTerritoryID>(currentPrimaryObject, (object) this.GetSalesTerritory(address));
  }

  public virtual string GetSalesTerritory(IAddressBase address)
  {
    PX.Objects.CS.SalesTerritory salesTerritory = (PX.Objects.CS.SalesTerritory) null;
    if (address == null)
      return (string) null;
    if (address != null && address.CountryID != null && address != null && address.State != null)
      salesTerritory = PXResultset<PX.Objects.CS.SalesTerritory>.op_Implicit(PXSelectBase<PX.Objects.CS.SalesTerritory, PXViewOf<PX.Objects.CS.SalesTerritory>.BasedOn<SelectFromBase<PX.Objects.CS.SalesTerritory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CS.State>.On<BqlOperand<PX.Objects.CS.State.salesTerritoryID, IBqlString>.IsEqual<PX.Objects.CS.SalesTerritory.salesTerritoryID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CS.SalesTerritory.salesTerritoryType, Equal<SalesTerritoryTypeAttribute.byState>>>>, And<BqlOperand<PX.Objects.CS.SalesTerritory.isActive, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.CS.State.countryID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.CS.State.stateID, IBqlString>.IsEqual<P.AsString>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
      {
        (object) address.CountryID,
        (object) address.State
      }));
    if (address != null && address.CountryID != null && salesTerritory == null)
      salesTerritory = PXResultset<PX.Objects.CS.SalesTerritory>.op_Implicit(PXSelectBase<PX.Objects.CS.SalesTerritory, PXViewOf<PX.Objects.CS.SalesTerritory>.BasedOn<SelectFromBase<PX.Objects.CS.SalesTerritory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CS.Country>.On<BqlOperand<PX.Objects.CS.Country.salesTerritoryID, IBqlString>.IsEqual<PX.Objects.CS.SalesTerritory.salesTerritoryID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CS.SalesTerritory.salesTerritoryType, Equal<SalesTerritoryTypeAttribute.byCountry>>>>, And<BqlOperand<PX.Objects.CS.SalesTerritory.isActive, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.CS.Country.countryID, IBqlString>.IsEqual<P.AsString>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) address.CountryID
      }));
    return salesTerritory?.SalesTerritoryID;
  }
}
