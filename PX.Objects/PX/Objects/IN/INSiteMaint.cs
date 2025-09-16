// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN;

public class INSiteMaint : PXGraph<
#nullable disable
INSiteMaint, INSite>
{
  public FbqlSelect<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.BAccount>.View _bAccount;
  public FbqlSelect<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INSite.siteID, 
  #nullable disable
  IsNotNull>>>, And<BqlOperand<
  #nullable enable
  INSite.siteID, IBqlInt>.IsNotEqual<
  #nullable disable
  SiteAnyAttribute.transitSiteID>>>>.And<MatchUser>>, INSite>.View site;
  public FbqlSelect<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INSite.siteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INSite.siteID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  INSite>.View siteaccounts;
  [PXFilterable(new System.Type[] {})]
  [PXImport(typeof (INSite))]
  public FbqlSelect<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INLocation.siteID>.IsRelatedTo<INSite.siteID>.AsSimpleKey.WithTablesOf<INSite, INLocation>, INSite, INLocation>.SameAsCurrent>, INLocation>.View location;
  [PXFilterable(new System.Type[] {})]
  [PXImport(typeof (INSite))]
  public FbqlSelect<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INCart.siteID>.IsRelatedTo<INSite.siteID>.AsSimpleKey.WithTablesOf<INSite, INCart>, INSite, INCart>.SameAsCurrent>, INCart>.View carts;
  [PXFilterable(new System.Type[] {})]
  [PXImport(typeof (INSite))]
  public FbqlSelect<SelectFromBase<INTote, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INTote.siteID>.IsRelatedTo<INSite.siteID>.AsSimpleKey.WithTablesOf<INSite, INTote>, INSite, INTote>.SameAsCurrent>, INTote>.View totes;
  public FbqlSelect<SelectFromBase<INTote, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<INTote.siteID>.IsRelatedTo<INCart.siteID>, Field<INTote.assignedCartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, INTote>, INCart, INTote>.SameAsCurrent>, INTote>.View totesInCart;
  public PXSetup<PX.Objects.GL.Branch>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INSite.branchID, IBqlInt>.AsOptional>> branch;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<PX.Objects.CR.Address, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Address.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INSite.bAccountID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.CR.Address.addressID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INSite.addressID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.CR.Address>.View Address;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Contact.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INSite.bAccountID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INSite.contactID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.CR.Contact>.View Contact;
  public FbqlSelect<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemSite.siteID>.IsRelatedTo<INSite.siteID>.AsSimpleKey.WithTablesOf<INSite, INItemSite>, INSite, INItemSite>.SameAsCurrent>, INItemSite>.View itemsiterecords;
  public PXSetup<INSetup> insetup;
  public PXChangeID<INSite, INSite.siteCD> changeID;
  public PXAction<INSite> viewRestrictionGroups;
  public PXAction<INSite> validateAddresses;
  public PXAction<INSite> viewOnMap;
  public PXAction<INSite> viewTotesInCart;
  protected string[] _WrongLocations = new string[5];

  [PXLookupButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ViewRestrictionGroups(PXAdapter adapter)
  {
    if (((PXSelectBase<INSite>) this.site).Current != null)
    {
      INAccessDetail instance = PXGraph.CreateInstance<INAccessDetail>();
      ((PXSelectBase<INSite>) instance.Site).Current = PXResultset<INSite>.op_Implicit(((PXSelectBase<INSite>) instance.Site).Search<INSite.siteCD>((object) ((PXSelectBase<INSite>) this.site).Current.SiteCD, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Restricted Groups");
    }
    return adapter.Get();
  }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    INSiteMaint inSiteMaint = this;
    int? defaultSiteId = inSiteMaint.getDefaultSiteID();
    if (!defaultSiteId.HasValue || PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
    {
      INSite inSite1 = (INSite) null;
      foreach (INSite inSite2 in ((PXAction) new PXCancel<INSite>((PXGraph) inSiteMaint, nameof (Cancel))).Press(a))
        inSite1 = inSite2;
      yield return (object) inSite1;
    }
    else
      yield return (object) PXResultset<INSite>.op_Implicit(PXSelectBase<INSite, PXViewOf<INSite>.BasedOn<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSite.siteID, Equal<P.AsInt>>>>>.And<MatchUser>>>.Config>.Select((PXGraph) inSiteMaint, new object[1]
      {
        (object) defaultSiteId
      }));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    if (((PXSelectBase<INSite>) this.site).Current != null)
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

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ViewOnMap(PXAdapter adapter)
  {
    BAccountUtility.ViewOnMap(((PXSelectBase<PX.Objects.CR.Address>) this.Address).Current);
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ViewTotesInCart(PXAdapter adapter)
  {
    ((PXSelectBase<INTote>) this.totesInCart).AskExt();
    return adapter.Get();
  }

  public INSiteMaint()
  {
    if (((PXSelectBase<INSetup>) this.insetup).Current == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (INSetup), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Inventory Preferences")
      });
    if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
    {
      ((PXSelectBase) this.site).Cache.AllowInsert = !this.getDefaultSiteID().HasValue;
      ((PXAction) this.Next).SetVisible(false);
      ((PXAction) this.Previous).SetVisible(false);
      ((PXAction) this.Last).SetVisible(false);
      ((PXAction) this.First).SetVisible(false);
    }
    PXUIFieldAttribute.SetVisible<INSite.pPVAcctID>(((PXSelectBase) this.siteaccounts).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INSite.pPVSubID>(((PXSelectBase) this.siteaccounts).Cache, (object) null, true);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.salutation>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], "Attention");
    PXUIFieldAttribute.SetDisplayName<INSite.overrideInvtAccSub>(((PXSelectBase) this.siteaccounts).Cache, PXAccess.FeatureInstalled<FeaturesSet.subAccount>() ? "Override Inventory Account/Sub." : "Override Inventory Account.");
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.fullName>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], (object) null);
    ((PXSelectBase) this.location).Attributes.OfType<PXImportAttribute>().First<PXImportAttribute>().MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this.MappingPropertiesInit);
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    INSiteMaint.Configure(config.GetScreenConfigurationContext<INSiteMaint, INSite>());
  }

  protected static void Configure(WorkflowContext<INSiteMaint, INSite> context)
  {
    var locationLabels = new
    {
      ActionName = "INLocationLabels",
      ReportID = "IN619000",
      Parameters = new{ Site = "WarehouseID" }
    };
    BoundedTo<INSiteMaint, INSite>.ActionCategory.IConfigured otherCategory = CommonActionCategories.Get<INSiteMaint, INSite>(context).Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<INSiteMaint, INSite>.ScreenConfiguration.IStartConfigScreen, BoundedTo<INSiteMaint, INSite>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<INSiteMaint, INSite>.ScreenConfiguration.IConfigured) ((BoundedTo<INSiteMaint, INSite>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<INSiteMaint, INSite>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<INSiteMaint, PXAction<INSite>>>) (g => g.changeID), (Func<BoundedTo<INSiteMaint, INSite>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INSiteMaint, INSite>.ActionDefinition.IConfigured>) (a => (BoundedTo<INSiteMaint, INSite>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<INSiteMaint, PXAction<INSite>>>) (g => g.validateAddresses), (Func<BoundedTo<INSiteMaint, INSite>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INSiteMaint, INSite>.ActionDefinition.IConfigured>) (a => (BoundedTo<INSiteMaint, INSite>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<INSiteMaint, PXAction<INSite>>>) (g => g.viewRestrictionGroups), (Func<BoundedTo<INSiteMaint, INSite>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INSiteMaint, INSite>.ActionDefinition.IConfigured>) (a => (BoundedTo<INSiteMaint, INSite>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      actions.AddNew(locationLabels.ActionName, (Func<BoundedTo<INSiteMaint, INSite>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INSiteMaint, INSite>.ActionDefinition.IConfigured>) (a => (BoundedTo<INSiteMaint, INSite>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2).DisplayName("Location Labels").IsRunReportScreen((Func<BoundedTo<INSiteMaint, INSite>.NavigationDefinition.IRunReportNeedScreen, BoundedTo<INSiteMaint, INSite>.NavigationDefinition.IConfiguredRunReport>) (report => report.ReportID(locationLabels.ReportID).WithWindowMode((PXBaseRedirectException.WindowMode) 2).WithAssignments((Action<BoundedTo<INSiteMaint, INSite>.NavigationParameter.IContainerFillerNavigationActionParameters>) (rass => rass.Add(locationLabels.Parameters.Site, (Func<BoundedTo<INSiteMaint, INSite>.NavigationParameter.INeedRightOperand, BoundedTo<INSiteMaint, INSite>.NavigationParameter.IConfigured>) (z => z.SetFromField<INSite.siteCD>()))))))));
    })).WithCategories((Action<BoundedTo<INSiteMaint, INSite>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(otherCategory);
      categories.Update((FolderType) 1, (Func<BoundedTo<INSiteMaint, INSite>.ActionCategory.ConfiguratorCategory, BoundedTo<INSiteMaint, INSite>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(otherCategory)));
      categories.Update((FolderType) 2, (Func<BoundedTo<INSiteMaint, INSite>.ActionCategory.ConfiguratorCategory, BoundedTo<INSiteMaint, INSite>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter((FolderType) 1)));
    }))));
  }

  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<INSite.branchID, IBqlInt>.FromCurrent>>>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Address.countryID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (INSite.siteID), DefaultForInsert = true, DefaultForUpdate = true)]
  [PXParent(typeof (INLocation.FK.Site))]
  protected virtual void _(PX.Data.Events.CacheAttached<INLocation.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (LocationAttribute), "CacheGlobal", false)]
  [PXUIVerify]
  protected virtual void _(PX.Data.Events.CacheAttached<INSite.receiptLocationID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (LocationAttribute), "CacheGlobal", false)]
  [PXUIVerify]
  protected virtual void _(PX.Data.Events.CacheAttached<INSite.shipLocationID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (LocationAttribute), "CacheGlobal", false)]
  [PXUIVerify]
  protected virtual void _(PX.Data.Events.CacheAttached<INSite.dropShipLocationID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (LocationAttribute), "CacheGlobal", false)]
  [PXUIVerify]
  protected virtual void _(PX.Data.Events.CacheAttached<INSite.returnLocationID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (LocationAttribute), "CacheGlobal", false)]
  [PXUIVerify]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INSite.nonStockPickingLocationID> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INSite, INSite.active> e)
  {
    if (e.Row != null && ((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INSite, INSite.active>, INSite, object>) e).NewValue).GetValueOrDefault() && PXSelectorAttribute.Select<PX.Objects.GL.Branch.branchID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INSite, INSite.active>>) e).Cache, (object) e.Row) is PX.Objects.GL.Branch branch && !branch.Active.GetValueOrDefault())
      throw new PXSetPropertyException<INSite.active>("The {0} warehouse cannot be activated because the related {1} branch is inactive.", new object[2]
      {
        (object) e.Row.SiteCD,
        (object) branch.BranchCD
      });
  }

  protected virtual void _(PX.Data.Events.RowInserted<INSite> e)
  {
    try
    {
      PX.Objects.CR.Address address = (PX.Objects.CR.Address) ((PXSelectBase) this.Address).Cache.Insert((object) new PX.Objects.CR.Address()
      {
        BAccountID = (int?) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.bAccountID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current)
      });
      PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) ((PXSelectBase) this.Contact).Cache.Insert((object) new PX.Objects.CR.Contact()
      {
        BAccountID = (int?) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.bAccountID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current),
        DefAddressID = address.AddressID
      });
    }
    finally
    {
      ((PXSelectBase) this.Address).Cache.IsDirty = false;
      ((PXSelectBase) this.Contact).Cache.IsDirty = false;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INSite> e)
  {
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INSite>>) e).Cache.ObjectsEqual<INSite.branchID>((object) e.Row, (object) e.OldRow))
    {
      bool flag1 = false;
      PX.Objects.CR.Address address1 = (PX.Objects.CR.Address) null;
      IEnumerator enumerator = ((PXSelectBase) this.Address).Cache.Inserted.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          PX.Objects.CR.Address current = (PX.Objects.CR.Address) enumerator.Current;
          current.BAccountID = (int?) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.bAccountID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current);
          current.CountryID = (string) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.countryID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current);
          flag1 = true;
          address1 = current;
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
      if (!flag1)
      {
        PX.Objects.CR.Address address2 = (PX.Objects.CR.Address) ((PXSelectBase) this.Address).View.SelectSingleBound(new object[2]
        {
          ((PXSelectBase) this.branch).View.SelectSingleBound(new object[1]
          {
            (object) e.OldRow
          }, Array.Empty<object>()),
          (object) e.OldRow
        }, Array.Empty<object>()) ?? new PX.Objects.CR.Address();
        address2.BAccountID = (int?) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.bAccountID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current);
        address2.CountryID = (string) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.countryID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current);
        address1 = ((PXSelectBase<PX.Objects.CR.Address>) this.Address).Update(address2);
      }
      else
        ((PXSelectBase) this.Address).Cache.Normalize();
      bool flag2 = false;
      foreach (PX.Objects.CR.Contact contact in ((PXSelectBase) this.Contact).Cache.Inserted)
      {
        contact.BAccountID = (int?) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.bAccountID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current);
        contact.DefAddressID = (int?) address1?.AddressID;
        flag2 = true;
      }
      if (!flag2)
      {
        PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) ((PXSelectBase) this.Contact).View.SelectSingleBound(new object[2]
        {
          ((PXSelectBase) this.branch).View.SelectSingleBound(new object[1]
          {
            (object) e.OldRow
          }, Array.Empty<object>()),
          (object) e.OldRow
        }, Array.Empty<object>()) ?? new PX.Objects.CR.Contact();
        contact.BAccountID = (int?) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.bAccountID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current);
        contact.DefAddressID = (int?) address1?.AddressID;
        ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Update(contact);
      }
      else
        ((PXSelectBase) this.Contact).Cache.Normalize();
    }
    if (e.Row == null || e.OldRow == null || PXAccess.FeatureInstalled<FeaturesSet.warehouse>() || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INSite>>) e).Cache.ObjectsEqual<INSite.replenishmentClassID>((object) e.Row, (object) e.OldRow))
      return;
    string replenishmentClassId = e.Row.ReplenishmentClassID;
    if (replenishmentClassId == null)
      return;
    foreach (PXResult<INItemSite> pxResult in PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemSite.siteID, IBqlInt>.IsEqual<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      INItemSite itemsite = PXResult<INItemSite>.op_Implicit(pxResult);
      itemsite.ReplenishmentClassID = replenishmentClassId;
      INItemSiteMaint.DefaultItemReplenishment((PXGraph) this, itemsite);
      INItemSiteMaint.DefaultSubItemReplenishment((PXGraph) this, itemsite);
      ((PXGraph) this).Caches[typeof (INItemSite)].Update((object) itemsite);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INSite, INSite.receiptLocationID> e)
  {
    ((PXSelectBase) this.location).Cache.ClearQueryCache();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INSite, INSite.shipLocationID> e)
  {
    ((PXSelectBase) this.location).Cache.ClearQueryCache();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INSite, INSite.returnLocationID> e)
  {
    ((PXSelectBase) this.location).Cache.ClearQueryCache();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INSite, INSite.dropShipLocationID> e)
  {
    ((PXSelectBase) this.location).Cache.ClearQueryCache();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INSite, INSite.nonStockPickingLocationID> e)
  {
    ((PXSelectBase) this.location).Cache.ClearQueryCache();
  }

  protected virtual void _(PX.Data.Events.RowUpdating<INSite> e)
  {
    this.UpateSiteLocation<INSite.receiptLocationID, INSite.receiptLocationIDOverride>(((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<INSite>>) e).Cache, ((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<INSite>>) e).Args);
    this.UpateSiteLocation<INSite.shipLocationID, INSite.shipLocationIDOverride>(((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<INSite>>) e).Cache, ((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<INSite>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INSite> e)
  {
    ((PXSelectBase) this.Address).Cache.Delete((object) ((PXSelectBase<PX.Objects.CR.Address>) this.Address).Current);
    ((PXSelectBase) this.Contact).Cache.Delete((object) ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INSite> e)
  {
    INAcctSubDefault.Required(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSite>>) e).Cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSite>>) e).Args);
    if (e.Row == null)
      return;
    ((PXAction) this.viewRestrictionGroups).SetEnabled(e.Row.SiteCD != null);
    foreach (INLocation inLocation in ((PXSelectBase) this.location).Cache.Cached)
    {
      if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.location).Cache.GetStatus((object) inLocation), (PXEntryStatus) 3, (PXEntryStatus) 4))
      {
        int? nullable1 = inLocation.LocationID;
        int? nullable2 = e.Row.ReceiptLocationID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSite>>) e).Cache.RaiseExceptionHandling<INSite.receiptLocationID>((object) e.Row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The record has been deleted."));
        nullable2 = inLocation.LocationID;
        nullable1 = e.Row.ShipLocationID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSite>>) e).Cache.RaiseExceptionHandling<INSite.shipLocationID>((object) e.Row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The record has been deleted."));
        nullable1 = inLocation.LocationID;
        nullable2 = e.Row.ReturnLocationID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSite>>) e).Cache.RaiseExceptionHandling<INSite.returnLocationID>((object) e.Row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The record has been deleted."));
        nullable2 = inLocation.LocationID;
        nullable1 = e.Row.DropShipLocationID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSite>>) e).Cache.RaiseExceptionHandling<INSite.dropShipLocationID>((object) e.Row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The record has been deleted."));
        nullable1 = inLocation.LocationID;
        nullable2 = e.Row.NonStockPickingLocationID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSite>>) e).Cache.RaiseExceptionHandling<INSite.nonStockPickingLocationID>((object) e.Row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The record has been deleted."));
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INSite> e)
  {
    INAcctSubDefault.Required(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INSite>>) e).Cache, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INSite>>) e).Args);
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    INSite row = e.Row;
    bool? nullable = row.OverrideInvtAccSub;
    if (!nullable.GetValueOrDefault())
    {
      PXDefaultAttribute.SetPersistingCheck<INSite.invtAcctID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INSite>>) e).Cache, (object) e.Row, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<INSite.invtSubID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INSite>>) e).Cache, (object) e.Row, (PXPersistingCheck) 2);
    }
    nullable = row.ReceiptLocationIDOverride;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.ShipLocationIDOverride;
      if (!nullable.GetValueOrDefault())
        goto label_10;
    }
    List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
    nullable = row.ReceiptLocationIDOverride;
    if (nullable.GetValueOrDefault())
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (INItemSite.dfltReceiptLocationID).Name, (PXDbType) 8, (object) row.ReceiptLocationID));
    nullable = row.ShipLocationIDOverride;
    if (nullable.GetValueOrDefault())
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign(typeof (INItemSite.dfltShipLocationID).Name, (PXDbType) 8, (object) row.ShipLocationID));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict(typeof (INItemSite.siteID).Name, (PXDbType) 8, (object) row.SiteID));
    PXDatabase.Update<INItemSite>(pxDataFieldParamList.ToArray());
label_10:
    nullable = row.Active;
    if (nullable.GetValueOrDefault())
      return;
    int num;
    if (PXResultset<INRegister>.op_Implicit(PXSelectBase<INRegister, PXViewOf<INRegister>.BasedOn<SelectFromBase<INRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRegister.released, NotEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRegister.siteID, Equal<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<INRegister.toSiteID, IBqlInt>.IsEqual<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new INSite[1]
    {
      e.Row
    }, Array.Empty<object>())) == null)
      num = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.released, NotEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.siteID, Equal<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<INTran.toSiteID, IBqlInt>.IsEqual<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new INSite[1]
      {
        e.Row
      }, Array.Empty<object>())) != null ? 1 : 0;
    else
      num = 1;
    if (num == 0)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INSite>>) e).Cache.RaiseExceptionHandling<INSite.active>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("Can't deactivate warehouse. It has unreleased transactions."));
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<INSite, INSite.receiptLocationID> e)
  {
    if (!((PXGraph) this).IsImport)
      return;
    this._WrongLocations[0] = ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.receiptLocationID>, INSite, object>) e).NewValue as string;
    ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.receiptLocationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<INSite, INSite.returnLocationID> e)
  {
    if (!((PXGraph) this).IsImport)
      return;
    this._WrongLocations[1] = ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.returnLocationID>, INSite, object>) e).NewValue as string;
    ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.returnLocationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<INSite, INSite.dropShipLocationID> e)
  {
    if (!((PXGraph) this).IsImport)
      return;
    this._WrongLocations[2] = ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.dropShipLocationID>, INSite, object>) e).NewValue as string;
    ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.dropShipLocationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<INSite, INSite.shipLocationID> e)
  {
    if (!((PXGraph) this).IsImport)
      return;
    this._WrongLocations[3] = ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.shipLocationID>, INSite, object>) e).NewValue as string;
    ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.shipLocationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<INSite, INSite.nonStockPickingLocationID> e)
  {
    if (!((PXGraph) this).IsImport)
      return;
    this._WrongLocations[4] = ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.nonStockPickingLocationID>, INSite, object>) e).NewValue as string;
    ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INSite, INSite.nonStockPickingLocationID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowInserting<INLocation> e)
  {
    if (e.Row == null)
      return;
    INLocation inLocation = (INLocation) ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<INLocation>>) e).Cache.Locate((object) e.Row);
    if (inLocation == null)
      return;
    e.Row.LocationID = inLocation.LocationID;
  }

  protected virtual void _(PX.Data.Events.RowInserted<INLocation> e)
  {
    string locationCd;
    if (((PXSelectBase<INSite>) this.site).Current == null || !((PXGraph) this).IsImport || (locationCd = e.Row.LocationCD) == null)
      return;
    if (this._WrongLocations[0] == locationCd)
    {
      ((PXSelectBase<INSite>) this.site).Current.ReceiptLocationID = e.Row.LocationID;
      this._WrongLocations[0] = (string) null;
    }
    if (this._WrongLocations[1] == locationCd)
    {
      ((PXSelectBase<INSite>) this.site).Current.ReturnLocationID = e.Row.LocationID;
      this._WrongLocations[1] = (string) null;
    }
    if (this._WrongLocations[2] == locationCd)
    {
      ((PXSelectBase<INSite>) this.site).Current.DropShipLocationID = e.Row.LocationID;
      this._WrongLocations[2] = (string) null;
    }
    if (this._WrongLocations[3] == locationCd)
    {
      ((PXSelectBase<INSite>) this.site).Current.ShipLocationID = e.Row.LocationID;
      this._WrongLocations[3] = (string) null;
    }
    if (!(this._WrongLocations[4] == locationCd))
      return;
    ((PXSelectBase<INSite>) this.site).Current.NonStockPickingLocationID = e.Row.LocationID;
    this._WrongLocations[4] = (string) null;
  }

  protected virtual void _(PX.Data.Events.RowSelected<INLocation> e)
  {
    if (e.Row == null)
      return;
    int? nullable = e.Row.PrimaryItemID;
    if (nullable.HasValue && InventoryItem.PK.Find((PXGraph) this, e.Row.PrimaryItemID) == null)
      PXUIFieldAttribute.SetWarning<INLocation.primaryItemID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INLocation>>) e).Cache, (object) e.Row, "The item was deleted");
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INLocation>>) e).Cache;
    INLocation row = e.Row;
    nullable = e.Row.ProjectID;
    int num = !nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<INLocation.isCosted>(cache, (object) row, num != 0);
    bool flag = this.ShowWarningProjectLowestPickPriority(e.Row);
    PXUIFieldAttribute.SetWarning<INLocation.pickPriority>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INLocation>>) e).Cache, (object) e.Row, flag ? "There is a location without a project association with the same or lower pick priority. Consider specifying lower pick priority for the current location to ensure correct selection of a location for sales orders unrelated to projects." : (string) null);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INLocation> e)
  {
    if (e.Row == null || PXDBOperationExt.Command(e.Operation) != 3)
      return;
    INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.siteID, Equal<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.dfltReceiptLocationID, Equal<P.AsInt>>>>, Or<BqlOperand<INItemSite.dfltShipLocationID, IBqlInt>.IsEqual<P.AsInt>>>>.Or<BqlOperand<INItemSite.dfltPutawayLocationID, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) e.Row.LocationID,
      (object) e.Row.LocationID,
      (object) e.Row.LocationID
    }));
    if (inItemSite != null)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, inItemSite.InventoryID) ?? new InventoryItem();
      if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INLocation>>) e).Cache.RaiseExceptionHandling<INLocation.locationID>((object) e.Row, (object) e.Row.LocationCD, (Exception) new PXSetPropertyException("Location '{0}' is selected as default location in Item Warehouse Details for Item '{1}' and cannot be deleted.", (PXErrorLevel) 4, new object[2]
      {
        (object) e.Row.LocationCD.TrimEnd(),
        (object) inventoryItem.InventoryCD.TrimEnd()
      })))
        throw new PXRowPersistingException(PXDataUtils.FieldName<INLocation.locationID>(), (object) e.Row.LocationID, "Location '{0}' is selected as default location in Item Warehouse Details for Item '{1}' and cannot be deleted.", new object[2]
        {
          (object) e.Row.LocationCD.TrimEnd(),
          (object) inventoryItem.InventoryCD.TrimEnd()
        });
    }
    INPIClassLocation inpiClassLocation = PXResultset<INPIClassLocation>.op_Implicit(PXSelectBase<INPIClassLocation, PXViewOf<INPIClassLocation>.BasedOn<SelectFromBase<INPIClassLocation, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INPIClass>.On<INPIClassLocation.FK.PIClass>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIClass.siteID, Equal<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INPIClassLocation.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.LocationID
    }));
    if (inpiClassLocation == null)
      return;
    if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INLocation>>) e).Cache.RaiseExceptionHandling<INLocation.locationID>((object) e.Row, (object) e.Row.LocationCD, (Exception) new PXSetPropertyException("Location '{0}' is added to Physical Inventory Type '{1}' and cannot be deleted.", (PXErrorLevel) 4, new object[2]
    {
      (object) e.Row.LocationCD.TrimEnd(),
      (object) inpiClassLocation.PIClassID.TrimEnd()
    })))
      throw new PXRowPersistingException(PXDataUtils.FieldName<INLocation.locationID>(), (object) e.Row.LocationID, "Location '{0}' is added to Physical Inventory Type '{1}' and cannot be deleted.", new object[2]
      {
        (object) e.Row.LocationCD.TrimEnd(),
        (object) inpiClassLocation.PIClassID.TrimEnd()
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INLocation, INLocation.isCosted> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.isCosted>, INLocation, object>) e).NewValue;
    bool flag;
    if (newValue.GetValueOrDefault())
      flag = PXResultset<INLocationStatusByCostCenter>.op_Implicit(PXSelectBase<INLocationStatusByCostCenter, PXViewOf<INLocationStatusByCostCenter>.BasedOn<SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocationStatusByCostCenter.siteID, Equal<BqlField<INLocation.siteID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INLocationStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLocation.locationID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INLocationStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new INLocation[1]
      {
        e.Row
      }, Array.Empty<object>())) == null;
    else
      flag = PXResultset<INCostStatus>.op_Implicit(PXSelectBase<INCostStatus, PXViewOf<INCostStatus>.BasedOn<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.costSiteID, Equal<BqlField<INLocation.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INCostStatus.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new INLocation[1]
      {
        e.Row
      }, Array.Empty<object>())) == null;
    if (!flag)
      throw new PXSetPropertyException("There is non zero Quantity on Hand for this item on selected Warehouse Location. You can only change Cost Separately option when the Qty on Hand is equal to zero", (PXErrorLevel) 4);
    newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.isCosted>, INLocation, object>) e).NewValue;
    if (!newValue.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INLocation, INLocation.isCosted>>) e).Cache.RaiseExceptionHandling<INLocation.isCosted>((object) e.Row, (object) true, (Exception) new PXSetPropertyException("Last Inventory cost on warehouse will not be updated if the item has been received on this Warehouse Location.", (PXErrorLevel) 3));
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.CR.Address> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase<INSite>) this.site).Current.AddressID = e.Row.AddressID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Address, PX.Objects.CR.Address.bAccountID> e)
  {
    if (((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Address, PX.Objects.CR.Address.bAccountID>, PX.Objects.CR.Address, object>) e).NewValue = (object) (int?) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.bAccountID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Address, PX.Objects.CR.Address.bAccountID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Address, PX.Objects.CR.Address.countryID> e)
  {
    e.Row.State = (string) null;
    e.Row.PostalCode = (string) null;
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.CR.Contact> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase<INSite>) this.site).Current.ContactID = e.Row.ContactID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Contact, PX.Objects.CR.Contact.bAccountID> e)
  {
    if (((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Contact, PX.Objects.CR.Contact.bAccountID>, PX.Objects.CR.Contact, object>) e).NewValue = (object) (int?) ((PXSelectBase) this.branch).Cache.GetValue<PX.Objects.GL.Branch.bAccountID>((object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Contact, PX.Objects.CR.Contact.bAccountID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.CR.Contact, PX.Objects.CR.Contact.defAddressID> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.CR.Contact, PX.Objects.CR.Contact.defAddressID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Contact, PX.Objects.CR.Contact.contactType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Contact, PX.Objects.CR.Contact.contactType>, PX.Objects.CR.Contact, object>) e).NewValue = (object) "AP";
  }

  public virtual void Persist()
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (INSite inSite in ((PXSelectBase) this.site).Cache.Deleted)
      {
        PXDatabase.Delete<INSiteStatusByCostCenter>(new PXDataFieldRestrict[3]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteStatusByCostCenter.siteID>((PXDbType) 8, new int?(4), (object) inSite.SiteID, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteStatusByCostCenter.qtyOnHand>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteStatusByCostCenter.qtyAvail>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
        });
        PXDatabase.Delete<INLocationStatusByCostCenter>(new PXDataFieldRestrict[3]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<INLocationStatusByCostCenter.siteID>((PXDbType) 8, new int?(4), (object) inSite.SiteID, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<INLocationStatusByCostCenter.qtyOnHand>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<INLocationStatusByCostCenter.qtyAvail>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
        });
        PXDatabase.Delete<INLotSerialStatusByCostCenter>(new PXDataFieldRestrict[3]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.siteID>((PXDbType) 8, new int?(4), (object) inSite.SiteID, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.qtyOnHand>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0),
          (PXDataFieldRestrict) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.qtyAvail>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
        });
        InventoryItem inventoryItem = PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXViewOf<InventoryItem>.BasedOn<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSiteStatusByCostCenter>.On<INSiteStatusByCostCenter.FK.InventoryItem>>>.Where<BqlOperand<INSiteStatusByCostCenter.siteID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) inSite.SiteID
        }));
        if (inventoryItem != null && inventoryItem.InventoryCD != null)
          throw new PXRowPersistingException(typeof (INSite.siteCD).Name, (object) inSite, "Unable to delete warehouse, item '{0}' has non-zero Quantity On Hand.", new object[1]
          {
            (object) inventoryItem.InventoryCD.TrimEnd()
          });
      }
      transactionScope.Complete();
    }
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.location).Cache.Clear();
  }

  public virtual void Clear()
  {
    ((PXGraph) this).Clear();
    this._WrongLocations[0] = (string) null;
    this._WrongLocations[1] = (string) null;
    this._WrongLocations[2] = (string) null;
    this._WrongLocations[3] = (string) null;
    this._WrongLocations[4] = (string) null;
  }

  protected virtual bool ShowWarningProjectLowestPickPriority(INLocation row)
  {
    return row.ProjectID.HasValue && GraphHelper.RowCast<INLocation>((IEnumerable) ((PXSelectBase<INLocation>) this.location).Select(Array.Empty<object>())).Any<INLocation>((Func<INLocation, bool>) (l =>
    {
      if (!l.Active.GetValueOrDefault() || l.ProjectID.HasValue)
        return false;
      short? pickPriority1 = l.PickPriority;
      int? nullable1 = pickPriority1.HasValue ? new int?((int) pickPriority1.GetValueOrDefault()) : new int?();
      short? pickPriority2 = row.PickPriority;
      int? nullable2 = pickPriority2.HasValue ? new int?((int) pickPriority2.GetValueOrDefault()) : new int?();
      return nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue;
    }));
  }

  private void MappingPropertiesInit(
    object sender,
    PXImportAttribute.MappingPropertiesInitEventArgs e)
  {
    string field1 = ((PXSelectBase) this.location).Cache.GetField(typeof (INLocation.isCosted));
    if (!e.Names.Contains(field1))
    {
      e.Names.Add(field1);
      e.DisplayNames.Add(PXUIFieldAttribute.GetDisplayName<INLocation.taskID>(((PXSelectBase) this.location).Cache));
    }
    string field2 = ((PXSelectBase) this.location).Cache.GetField(typeof (INLocation.taskID));
    if (e.Names.Contains(field2))
      return;
    e.Names.Add(field2);
    e.DisplayNames.Add(PXUIFieldAttribute.GetDisplayName<INLocation.taskID>(((PXSelectBase) this.location).Cache));
  }

  protected void UpateSiteLocation<Field, FieldResult>(PXCache cache, PXRowUpdatingEventArgs e)
    where Field : IBqlField
    where FieldResult : IBqlField
  {
    int? nullable1 = (int?) cache.GetValue<Field>(e.NewRow);
    int? nullable2 = (int?) cache.GetValue<Field>(e.Row);
    int? nullable3 = nullable1;
    if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue || !e.ExternalCall)
      return;
    if (PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      cache.GetValue<INSite.siteID>(e.Row)
    })) != null && ((PXSelectBase<INSite>) this.site).Ask("Warning", "Update default location for all items on this site by selected location?", (MessageButtons) 4) == 6)
      cache.SetValue<FieldResult>(e.NewRow, (object) true);
    else
      cache.SetValue<FieldResult>(e.NewRow, (object) false);
  }

  protected int? getDefaultSiteID()
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INSite>(new PXDataField[2]
    {
      (PXDataField) new PXDataField<INSite.siteID>(),
      (PXDataField) new PXDataFieldOrder<INSite.siteID>()
    }))
    {
      if (pxDataRecord != null)
        return pxDataRecord.GetInt32(0);
    }
    return new int?();
  }

  /// <exclude />
  public class INSiteMaintAddressLookupExtension : 
    AddressLookupExtension<INSiteMaint, INSite, PX.Objects.CR.Address>
  {
    protected override string AddressView => "Address";

    protected override string ViewOnMap => "viewOnMap";
  }
}
