// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_OpportunityMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.Workflows;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class SM_OpportunityMaint : PXGraphExtension<OpportunityMaint.Discount, OpportunityMaint>
{
  [PXHidden]
  public PXSelect<FSSetup> SetupRecord;
  [PXHidden]
  public PXSetup<CRSetup> CRSetupRecord;
  public PXSetup<FSSrvOrdType>.Where<Where<FSSrvOrdType.srvOrdType, Equal<Optional<FSxCROpportunity.srvOrdType>>>> ServiceOrderTypeSelected;
  [PXCopyPasteHiddenView]
  public PXFilter<FSCreateServiceOrderFilter> CreateServiceOrderFilter;
  public PXMenuInquiry<CROpportunity> InqueriesMenu;
  public PXAction<CROpportunity> ViewServiceOrder;
  public PXAction<CROpportunity> OpenAppointmentBoard;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrencyPriceCost(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.unitPrice))]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<Current<CROpportunityProducts.stockItemType>, Equal<INItemTypes.serviceItem>, And<FSxCROpportunityProducts.billingRule, Equal<ListField_BillingRule.None>>>, SharedClasses.decimal_0>, CROpportunityProducts.curyUnitPrice>))]
  public virtual void CROpportunityProducts_CuryUnitPrice_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.extPrice))]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXFormula(typeof (Switch<Case<Where<Current<CROpportunityProducts.stockItemType>, Equal<INItemTypes.serviceItem>, And<FSxCROpportunityProducts.billingRule, Equal<ListField_BillingRule.None>>>, SharedClasses.decimal_0>, Mult<CROpportunityProducts.curyUnitPrice, CROpportunityProducts.quantity>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual void CROpportunityProducts_CuryExtPrice_CacheAttached(PXCache sender)
  {
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<Search2<INItemSite.tranUnitCost, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<INItemSite.inventoryID>>>, Where<INItemSite.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>, And<INItemSite.siteID, Equal<Current<CROpportunityProducts.siteID>>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>>>>, Search2<InventoryItemCurySettings.stdCost, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<InventoryItemCurySettings.inventoryID>>>, Where<InventoryItemCurySettings.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<CROpportunity.branchID>>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>>>>))]
  public virtual void CROpportunityProducts_UnitCost_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrencyPriceCost(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.unitCost))]
  [PXUIField(DisplayName = "Unit Cost")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<CROpportunityProducts.pOCreate>))]
  public virtual void CROpportunityProducts_CuryUnitCost_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [PXMergeAttributes]
  protected virtual void FSCreateServiceOrderFilter_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  public FSSetup GetFSSetup()
  {
    return ((PXSelectBase<FSSetup>) this.SetupRecord).Current == null ? PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.SetupRecord).Select(Array.Empty<object>())) : ((PXSelectBase<FSSetup>) this.SetupRecord).Current;
  }

  public virtual void EnableDisableExtensionFields(
    PXCache cache,
    FSxCROpportunity fsxCROpportunityRow,
    FSServiceOrder fsServiceOrderRow)
  {
    if (fsxCROpportunityRow == null)
      return;
    bool flag = this.GetFSSetup() != null;
    PXUIFieldAttribute.SetEnabled<FSxCROpportunity.sDEnabled>(cache, (object) null, flag && fsServiceOrderRow == null);
    PXCache pxCache1 = cache;
    bool? sdEnabled;
    int num1;
    if (flag)
    {
      sdEnabled = fsxCROpportunityRow.SDEnabled;
      if (sdEnabled.GetValueOrDefault())
      {
        num1 = fsServiceOrderRow == null ? 1 : 0;
        goto label_5;
      }
    }
    num1 = 0;
label_5:
    PXUIFieldAttribute.SetEnabled<FSxCROpportunity.srvOrdType>(pxCache1, (object) null, num1 != 0);
    PXCache pxCache2 = cache;
    int num2;
    if (flag)
    {
      sdEnabled = fsxCROpportunityRow.SDEnabled;
      num2 = sdEnabled.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<FSxCROpportunity.branchLocationID>(pxCache2, (object) null, num2 != 0);
  }

  public virtual void EnableDisableActions(
    PXCache cache,
    CROpportunity crOpportunityRow,
    FSxCROpportunity fsxCROpportunityRow,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    bool flag = this.GetFSSetup() != null;
    ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache.GetStatus((object) ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current);
    ((PXAction) this.ViewServiceOrder).SetEnabled(flag && crOpportunityRow != null && crOpportunityRow.OpportunityID != null && fsServiceOrderRow != null);
    ((PXAction) this.OpenAppointmentBoard).SetEnabled(fsServiceOrderRow != null);
    if (fsServiceOrderRow == null)
      return;
    ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).Actions["CreateSalesOrder"].SetEnabled(false);
    ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).Actions["CreateInvoice"].SetEnabled(false);
  }

  public virtual void SetPersistingChecks(
    PXCache cache,
    CROpportunity crOpportunityRow,
    FSxCROpportunity fsxCROpportunityRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsxCROpportunityRow.SDEnabled.GetValueOrDefault())
    {
      PXDefaultAttribute.SetPersistingCheck<FSxCROpportunity.srvOrdType>(cache, (object) crOpportunityRow, (PXPersistingCheck) 1);
      PXDefaultAttribute.SetPersistingCheck<FSxCROpportunity.branchLocationID>(cache, (object) crOpportunityRow, (PXPersistingCheck) 1);
      if (fsSrvOrdTypeRow == null)
        fsSrvOrdTypeRow = CRExtensionHelper.GetServiceOrderType((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, fsxCROpportunityRow.SrvOrdType);
      if (fsSrvOrdTypeRow == null || !(fsSrvOrdTypeRow.Behavior != "IN"))
        return;
      PXDefaultAttribute.SetPersistingCheck<CROpportunity.bAccountID>(cache, (object) crOpportunityRow, (PXPersistingCheck) 1);
      PXDefaultAttribute.SetPersistingCheck<CROpportunity.locationID>(cache, (object) crOpportunityRow, (PXPersistingCheck) 1);
    }
    else
    {
      PXDefaultAttribute.SetPersistingCheck<FSxCROpportunity.srvOrdType>(cache, (object) crOpportunityRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<FSxCROpportunity.branchLocationID>(cache, (object) crOpportunityRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<CROpportunity.bAccountID>(cache, (object) crOpportunityRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<CROpportunity.locationID>(cache, (object) crOpportunityRow, (PXPersistingCheck) 2);
    }
  }

  public virtual void SetBranchLocationID(
    PXGraph graph,
    CROpportunity crOpportunityRow,
    FSxCROpportunity fsxCROpportunityRow)
  {
    if (crOpportunityRow.BranchID.HasValue)
    {
      UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>.Config>.Select(graph, Array.Empty<object>()));
      if (userPreferences != null)
      {
        int? defBranchId = userPreferences.DefBranchID;
        int? branchId = crOpportunityRow.BranchID;
        if (defBranchId.GetValueOrDefault() == branchId.GetValueOrDefault() & defBranchId.HasValue == branchId.HasValue)
        {
          FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(userPreferences);
          if (extension == null)
            return;
          fsxCROpportunityRow.BranchLocationID = extension.DfltBranchLocationID;
          return;
        }
      }
      fsxCROpportunityRow.BranchLocationID = new int?();
    }
    else
      fsxCROpportunityRow.BranchLocationID = new int?();
  }

  public virtual void HideOrShowFieldsActionsByInventoryFeature()
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.inventory>();
    PXUIVisibility pxuiVisibility = flag ? (PXUIVisibility) 3 : (PXUIVisibility) 1;
    PXUIFieldAttribute.SetVisibility<CROpportunityProducts.pOCreate>(((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<CROpportunityProducts.vendorID>(((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<CROpportunityProducts.curyUnitCost>(((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<FSxCROpportunityProducts.vendorLocationID>(((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisible<CROpportunityProducts.pOCreate>(((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CROpportunityProducts.vendorID>(((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CROpportunityProducts.curyUnitCost>(((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<FSxCROpportunityProducts.vendorLocationID>(((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, (object) null, flag);
  }

  [PXButton]
  [PXUIField(DisplayName = "Inquiries")]
  public virtual IEnumerable inqueriesMenu(PXAdapter adapter) => adapter.Get();

  [PXButton]
  [PXUIField]
  public virtual void viewServiceOrder()
  {
    if (((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity == null || ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current == null)
      return;
    if (((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsDirty)
      ((PXAction) ((PXGraphExtension<OpportunityMaint>) this).Base.Save).Press();
    FSxCROpportunity extension = ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache.GetExtension<FSxCROpportunity>((object) ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current);
    if (extension == null || extension.ServiceOrderRefNbr == null)
      return;
    CRExtensionHelper.LaunchServiceOrderScreen((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, extension.ServiceOrderRefNbr, extension.SrvOrdType);
  }

  [PXButton]
  [PXUIField]
  public virtual void openAppointmentBoard()
  {
    if (((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity == null || ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current == null)
      return;
    if (((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsDirty)
      ((PXAction) ((PXGraphExtension<OpportunityMaint>) this).Base.Save).Press();
    FSxCROpportunity extension = ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache.GetExtension<FSxCROpportunity>((object) ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current);
    if (extension == null || extension.ServiceOrderRefNbr == null)
      return;
    CRExtensionHelper.LaunchEmployeeBoard((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, extension.ServiceOrderRefNbr, extension.SrvOrdType);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunity, FSxCROpportunity.sDEnabled> e)
  {
    if (e.Row == null)
      return;
    CROpportunity row = e.Row;
    FSxCROpportunity extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunity, FSxCROpportunity.sDEnabled>>) e).Cache.GetExtension<FSxCROpportunity>((object) row);
    if (extension.SDEnabled.GetValueOrDefault())
    {
      FSSetup fsSetup = this.GetFSSetup();
      if (fsSetup != null && fsSetup.DfltOpportunitySrvOrdType != null)
        extension.SrvOrdType = fsSetup.DfltOpportunitySrvOrdType;
      this.SetBranchLocationID((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, row, extension);
      ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).SetValueExt<CROpportunity.allowOverrideContactAddress>(row, (object) true);
    }
    else
      extension.BranchLocationID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunity, CROpportunity.branchID> e)
  {
    if (e.Row == null)
      return;
    CROpportunity row = e.Row;
    FSxCROpportunity extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunity, CROpportunity.branchID>>) e).Cache.GetExtension<FSxCROpportunity>((object) row);
    this.SetBranchLocationID((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, row, extension);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<CROpportunity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CROpportunity> e)
  {
    if (e.Row == null)
      return;
    CROpportunity row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CROpportunity>>) e).Cache;
    FSxCROpportunity extension = cache.GetExtension<FSxCROpportunity>((object) row);
    FSServiceOrder relatedServiceOrder = CRExtensionHelper.GetRelatedServiceOrder((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, cache, (IBqlTable) row, extension.ServiceOrderRefNbr, extension.SrvOrdType);
    FSSrvOrdType fsSrvOrdTypeRow = (FSSrvOrdType) null;
    if (relatedServiceOrder != null)
      fsSrvOrdTypeRow = CRExtensionHelper.GetServiceOrderType((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, relatedServiceOrder.SrvOrdType);
    this.EnableDisableExtensionFields(cache, extension, relatedServiceOrder);
    this.EnableDisableActions(cache, row, extension, relatedServiceOrder, fsSrvOrdTypeRow);
    this.SetPersistingChecks(cache, row, extension, fsSrvOrdTypeRow);
    this.HideOrShowFieldsActionsByInventoryFeature();
  }

  protected virtual void _(PX.Data.Events.RowInserting<CROpportunity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<CROpportunity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<CROpportunity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CROpportunity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<CROpportunity> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base))
      return;
    FSxCROpportunity extension = ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache.GetExtension<FSxCROpportunity>((object) ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current);
    if (extension == null || extension.ServiceOrderRefNbr == null || ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Ask("The document is associated with a field service document. Do you want to delete this document?", (MessageButtons) 1) == 1)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CROpportunity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CROpportunity> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<CROpportunity> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.quantity> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    FSxCROpportunityProducts extension = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.quantity>>) e).Cache.GetExtension<FSxCROpportunityProducts>((object) row);
    if (extension != null && extension.BillingRule == "TIME")
    {
      int? estimatedDuration = extension.EstimatedDuration;
      if (estimatedDuration.HasValue)
      {
        estimatedDuration = extension.EstimatedDuration;
        int num = 0;
        if (estimatedDuration.GetValueOrDefault() > num & estimatedDuration.HasValue)
        {
          PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.quantity> fieldDefaulting = e;
          estimatedDuration = extension.EstimatedDuration;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) (estimatedDuration.HasValue ? new Decimal?((Decimal) estimatedDuration.GetValueOrDefault() / 60M) : new Decimal?());
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.quantity>, CROpportunityProducts, object>) fieldDefaulting).NewValue = (object) local;
          return;
        }
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.quantity>, CROpportunityProducts, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.curyUnitCost> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.curyUnitCost>>) e).Cache;
    cache.GetExtension<FSxCROpportunityProducts>((object) row);
    if (string.IsNullOrEmpty(row.UOM) || !row.InventoryID.HasValue || !row.POCreate.GetValueOrDefault())
      return;
    object baseval;
    cache.RaiseFieldDefaulting<CROpportunityProducts.unitCost>((object) e.Row, ref baseval);
    if (baseval == null || !((Decimal) baseval != 0M))
      return;
    Decimal curyval = INUnitAttribute.ConvertToBase<CROpportunityProducts.inventoryID, CROpportunityProducts.uOM>(cache, (object) row, (Decimal) baseval, INPrecision.NOROUND);
    IPXCurrencyHelper implementation = ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).FindImplementation<IPXCurrencyHelper>();
    if (implementation != null)
      curyval = implementation.GetDefaultCurrencyInfo().CuryConvCury((Decimal) baseval);
    else
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(cache, (object) row, curyval, out curyval, true);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.curyUnitCost>, CROpportunityProducts, object>) e).NewValue = (object) Math.Round(curyval, CommonSetupDecPl.PrcCst, MidpointRounding.AwayFromZero);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.curyUnitCost>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.billingRule> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.billingRule>>) e).Cache.GetExtension<FSxCROpportunityProducts>((object) row);
    if (!row.InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, row.InventoryID);
    if (inventoryItemRow.ItemType == "S")
    {
      FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItemRow);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.billingRule>, CROpportunityProducts, object>) e).NewValue = (object) extension?.BillingRule;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.billingRule>>) e).Cancel = true;
    }
    else
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.billingRule>, CROpportunityProducts, object>) e).NewValue = (object) "FLRA";
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.billingRule>>) e).Cancel = true;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.vendorLocationID> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.vendorLocationID>>) e).Cache.GetExtension<FSxCROpportunityProducts>((object) row);
    bool? poCreate = row.POCreate;
    bool flag = false;
    if (!(poCreate.GetValueOrDefault() == flag & poCreate.HasValue) && row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.vendorLocationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.estimatedDuration> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    if (((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.estimatedDuration>>) e).Cache.GetExtension<FSxCROpportunityProducts>((object) row) == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, row.InventoryID);
    if (inventoryItemRow == null)
      return;
    FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItemRow);
    if (!(inventoryItemRow.ItemType == "S") && !(inventoryItemRow.ItemType == "N"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.estimatedDuration>, CROpportunityProducts, object>) e).NewValue = (object) (int?) extension?.EstimatedDuration;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, FSxCROpportunityProducts.estimatedDuration>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, FSxCROpportunityProducts.estimatedDuration> e)
  {
    if (e.Row == null || ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsImportFromExcel || ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsImport)
      return;
    CROpportunityProducts row = e.Row;
    FSxCROpportunityProducts extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, FSxCROpportunityProducts.estimatedDuration>>) e).Cache.GetExtension<FSxCROpportunityProducts>((object) row);
    if (!extension.EstimatedDuration.HasValue || !(extension.BillingRule == "TIME"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, FSxCROpportunityProducts.estimatedDuration>>) e).Cache.SetDefaultExt<CROpportunityProducts.quantity>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, FSxCROpportunityProducts.billingRule> e)
  {
    if (e.Row == null || ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsImportFromExcel || ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsImport)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, FSxCROpportunityProducts.billingRule>>) e).Cache.SetDefaultExt<CROpportunityProducts.quantity>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.uOM> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.uOM>>) e).Cache;
    if (cache.GetExtension<FSxCROpportunityProducts>((object) row) == null || ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsImportFromExcel)
      return;
    cache.SetDefaultExt<CROpportunityProducts.curyUnitCost>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.siteID> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.siteID>>) e).Cache;
    if (cache.GetExtension<FSxCROpportunityProducts>((object) row) == null || ((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsImportFromExcel)
      return;
    cache.SetDefaultExt<CROpportunityProducts.curyUnitCost>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID>>) e).Cache;
    if (cache.GetExtension<FSxCROpportunityProducts>((object) row) == null)
      return;
    if (!((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base).IsImportFromExcel)
      cache.SetDefaultExt<CROpportunityProducts.curyUnitCost>((object) e.Row);
    cache.SetDefaultExt<FSxCROpportunityProducts.billingRule>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<CROpportunityProducts> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CROpportunityProducts> e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CROpportunityProducts>>) e).Cache;
    FSxCROpportunityProducts extension = cache.GetExtension<FSxCROpportunityProducts>((object) row);
    if (extension == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, row.InventoryID);
    if (inventoryItemRow == null)
      return;
    PXUIFieldAttribute.SetEnabled<FSxCROpportunityProducts.billingRule>(cache, (object) row, inventoryItemRow.ItemType == "S");
    PXUIFieldAttribute.SetEnabled<FSxCROpportunityProducts.estimatedDuration>(cache, (object) row, inventoryItemRow.ItemType == "S" || inventoryItemRow.ItemType == "N");
    PXCache pxCache1 = cache;
    CROpportunityProducts opportunityProducts1 = row;
    bool? poCreate = row.POCreate;
    int num1 = poCreate.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CROpportunityProducts.curyUnitCost>(pxCache1, (object) opportunityProducts1, num1 != 0);
    PXCache pxCache2 = cache;
    CROpportunityProducts opportunityProducts2 = row;
    poCreate = row.POCreate;
    int num2 = poCreate.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSxCROpportunityProducts.vendorLocationID>(pxCache2, (object) opportunityProducts2, num2 != 0);
    PXUIFieldAttribute.SetEnabled<CROpportunityProducts.quantity>(cache, (object) row, extension.BillingRule != "TIME");
  }

  protected virtual void _(PX.Data.Events.RowInserting<CROpportunityProducts> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<CROpportunityProducts> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<CROpportunityProducts> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CROpportunityProducts> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<CROpportunityProducts> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CROpportunityProducts> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CROpportunityProducts> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<CROpportunityProducts> e)
  {
  }

  public class WorkflowChanges : PXGraphExtension<OpportunityWorkflow, OpportunityMaint>
  {
    public static bool IsActive() => SM_OpportunityMaint.IsActive();

    public virtual void Configure(PXScreenConfiguration config)
    {
      SM_OpportunityMaint.WorkflowChanges.Configure(config.GetScreenConfigurationContext<OpportunityMaint, CROpportunity>());
    }

    protected static void Configure(
      WorkflowContext<OpportunityMaint, CROpportunity> context)
    {
      BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured categoryServices = context.Categories.CreateNew("Services", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured) c.DisplayName("Services")));
      BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured actionCreateServiceOrder = context.ActionDefinitions.CreateExisting<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateSrvOrdDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (a => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) a.WithCategory(categoryServices)));
      BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured actionViewServiceOrder = context.ActionDefinitions.CreateExisting<SM_OpportunityMaint>((Expression<Func<SM_OpportunityMaint, PXAction<CROpportunity>>>) (e => e.ViewServiceOrder), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (a => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) a.WithCategory(categoryServices).PlaceAfter(actionCreateServiceOrder)));
      BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured actionOpenAppointmentBoard = context.ActionDefinitions.CreateExisting<SM_OpportunityMaint>((Expression<Func<SM_OpportunityMaint, PXAction<CROpportunity>>>) (e => e.OpenAppointmentBoard), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (a => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) a.WithCategory(categoryServices, actionViewServiceOrder).PlaceAfter(actionViewServiceOrder)));
      context.UpdateScreenConfigurationFor((Func<BoundedTo<OpportunityMaint, CROpportunity>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<OpportunityMaint, CROpportunity>.ScreenConfiguration.ConfiguratorScreen>) (config => config.WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.ContainerAdjusterActions>) (a =>
      {
        a.Add(actionViewServiceOrder);
        a.Add(actionOpenAppointmentBoard);
      }))));
    }
  }
}
