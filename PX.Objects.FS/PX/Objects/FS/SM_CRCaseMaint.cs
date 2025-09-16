// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_CRCaseMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.SM;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class SM_CRCaseMaint : PXGraphExtension<CRCaseMaint>
{
  [PXHidden]
  public PXSelect<FSSetup> SetupRecord;
  [PXCopyPasteHiddenView]
  public PXFilter<FSCreateServiceOrderOnCaseFilter> CreateServiceOrderFilter;
  public PXAction<CRCase> CreateServiceOrder;
  public PXAction<CRCase> OpenAppointmentBoard;
  public PXAction<CRCase> ViewServiceOrder;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXAction) this.Base.Action).AddMenuAction((PXAction) this.CreateServiceOrder);
    ((PXAction) this.Base.Action).AddMenuAction((PXAction) this.OpenAppointmentBoard);
    ((PXAction) this.Base.Inquiry).AddMenuAction((PXAction) this.ViewServiceOrder);
  }

  [PXDefault]
  [PXMergeAttributes]
  protected virtual void CRCase_ContactID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<CRCase.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<CRCase.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>>))]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CRCase.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXFormula(typeof (Switch<Case<Where<Current<CRCase.locationID>, IsNull, And<Current<CRCase.contractID>, IsNotNull>>, IsNull<Selector<CRCase.contractID, Selector<ContractBillingSchedule.locationID, PX.Objects.CR.Location.locationCD>>, Selector<CRCase.contractID, Selector<PX.Objects.CT.Contract.locationID, PX.Objects.CR.Location.locationCD>>>, Case<Where<Current<CRCase.locationID>, IsNull, And<Current<CRCase.customerID>, IsNotNull>>, Selector<CRCase.customerID, Selector<PX.Objects.CR.BAccount.defLocationID, PX.Objects.CR.Location.locationCD>>, Case<Where<Current<CRCase.customerID>, IsNull>, Null>>>, CRCase.locationID>))]
  [PXFormula(typeof (Default<CRCase.customerID>))]
  protected virtual void CRCase_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField]
  public virtual void createServiceOrder()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SM_CRCaseMaint.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new SM_CRCaseMaint.\u003C\u003Ec__DisplayClass7_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.crCaseRow = ((PXSelectBase<CRCase>) this.Base.Case).Current;
    // ISSUE: reference to a compiler-generated field
    FSxCRCase extension = ((PXSelectBase) this.Base.Case).Cache.GetExtension<FSxCRCase>((object) cDisplayClass70.crCaseRow);
    if (((PXSelectBase<FSCreateServiceOrderOnCaseFilter>) this.CreateServiceOrderFilter).AskExt() != 1)
      return;
    extension.SDEnabled = new bool?(true);
    extension.BranchLocationID = ((PXSelectBase<FSCreateServiceOrderOnCaseFilter>) this.CreateServiceOrderFilter).Current.BranchLocationID;
    extension.SrvOrdType = ((PXSelectBase<FSCreateServiceOrderOnCaseFilter>) this.CreateServiceOrderFilter).Current.SrvOrdType;
    extension.AssignedEmpID = ((PXSelectBase<FSCreateServiceOrderOnCaseFilter>) this.CreateServiceOrderFilter).Current.AssignedEmpID;
    extension.ProblemID = ((PXSelectBase<FSCreateServiceOrderOnCaseFilter>) this.CreateServiceOrderFilter).Current.ProblemID;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass70, __methodptr(\u003CcreateServiceOrder\u003Eb__0)));
  }

  [PXButton]
  [PXUIField]
  public virtual void openAppointmentBoard()
  {
    if (this.Base.Case == null || ((PXSelectBase<CRCase>) this.Base.Case).Current == null)
      return;
    if (((PXGraph) this.Base).IsDirty)
      ((PXAction) this.Base.Save).Press();
    FSxCRCase extension = ((PXSelectBase) this.Base.Case).Cache.GetExtension<FSxCRCase>((object) ((PXSelectBase<CRCase>) this.Base.Case).Current);
    if (extension == null || extension.ServiceOrderRefNbr == null)
      return;
    CRExtensionHelper.LaunchEmployeeBoard((PXGraph) this.Base, extension.ServiceOrderRefNbr, extension.SrvOrdType);
  }

  [PXButton]
  [PXUIField]
  public virtual void viewServiceOrder()
  {
    if (this.Base.Case == null || ((PXSelectBase<CRCase>) this.Base.Case).Current == null)
      return;
    if (((PXGraph) this.Base).IsDirty)
      ((PXAction) this.Base.Save).Press();
    FSxCRCase extension = ((PXSelectBase) this.Base.Case).Cache.GetExtension<FSxCRCase>((object) ((PXSelectBase<CRCase>) this.Base.Case).Current);
    if (extension == null || extension.ServiceOrderRefNbr == null)
      return;
    CRExtensionHelper.LaunchServiceOrderScreen((PXGraph) this.Base, extension.ServiceOrderRefNbr, extension.SrvOrdType);
  }

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    SM_CRCaseMaint.Configure(configuration.GetScreenConfigurationContext<CRCaseMaint, CRCase>());
  }

  protected static void Configure(WorkflowContext<CRCaseMaint, CRCase> context)
  {
    BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured categoryServices = context.Categories.CreateNew("CustomerServices", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured>) (c => (BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured) c.DisplayName("Customer Services")));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionCreateServiceOrder = context.ActionDefinitions.CreateExisting<SM_CRCaseMaint>((Expression<Func<SM_CRCaseMaint, PXAction<CRCase>>>) (e => e.CreateServiceOrder), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryServices)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionViewServiceOrder = context.ActionDefinitions.CreateExisting<SM_CRCaseMaint>((Expression<Func<SM_CRCaseMaint, PXAction<CRCase>>>) (e => e.ViewServiceOrder), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryServices)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionOpenAppointmentBoard = context.ActionDefinitions.CreateExisting<SM_CRCaseMaint>((Expression<Func<SM_CRCaseMaint, PXAction<CRCase>>>) (e => e.OpenAppointmentBoard), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryServices)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<CRCaseMaint, CRCase>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<CRCaseMaint, CRCase>.ScreenConfiguration.ConfiguratorScreen>) (config => config.WithActions((Action<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.ContainerAdjusterActions>) (a =>
    {
      a.Add(actionCreateServiceOrder);
      a.Add(actionViewServiceOrder);
      a.Add(actionOpenAppointmentBoard);
    }))));
  }

  private FSSetup GetFSSetup()
  {
    return ((PXSelectBase<FSSetup>) this.SetupRecord).Current == null ? PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.SetupRecord).Select(Array.Empty<object>())) : ((PXSelectBase<FSSetup>) this.SetupRecord).Current;
  }

  public virtual void SetPersistingChecks(
    PXCache cache,
    CRCase crCaseRow,
    FSxCRCase fsxCRCaseRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsxCRCaseRow.SDEnabled.GetValueOrDefault())
    {
      PXDefaultAttribute.SetPersistingCheck<FSxCRCase.srvOrdType>(cache, (object) crCaseRow, (PXPersistingCheck) 1);
      PXDefaultAttribute.SetPersistingCheck<FSxCRCase.branchLocationID>(cache, (object) crCaseRow, (PXPersistingCheck) 1);
      if (fsSrvOrdTypeRow == null)
        fsSrvOrdTypeRow = CRExtensionHelper.GetServiceOrderType((PXGraph) this.Base, fsxCRCaseRow.SrvOrdType);
      if (fsSrvOrdTypeRow == null || !(fsSrvOrdTypeRow.Behavior != "IN"))
        return;
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.CROpportunity.bAccountID>(cache, (object) crCaseRow, (PXPersistingCheck) 1);
      if (!PXAccess.FeatureInstalled<FeaturesSet.accountLocations>())
        return;
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.CROpportunity.locationID>(cache, (object) crCaseRow, (PXPersistingCheck) 1);
    }
    else
    {
      PXDefaultAttribute.SetPersistingCheck<FSxCROpportunity.srvOrdType>(cache, (object) crCaseRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<FSxCROpportunity.branchLocationID>(cache, (object) crCaseRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.CROpportunity.bAccountID>(cache, (object) crCaseRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.CROpportunity.locationID>(cache, (object) crCaseRow, (PXPersistingCheck) 2);
    }
  }

  public virtual void EnableDisableCustomFields(
    PXCache cache,
    CRCase crCaseRow,
    FSxCRCase fsxCRCaseRow,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsSrvOrdTypeRow != null)
    {
      bool flag = fsSrvOrdTypeRow.Behavior == "IN";
      PXUIFieldAttribute.SetEnabled<CRCase.customerID>(cache, (object) null, flag || !flag && fsServiceOrderRow == null);
    }
    else
      PXUIFieldAttribute.SetEnabled<CRCase.customerID>(cache, (object) null, true);
  }

  public virtual void EnableDisableActions(
    PXCache cache,
    CRCase crCaseRow,
    FSxCRCase fsxCRCaseRow,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    bool flag1 = this.GetFSSetup() != null;
    bool flag2 = ((PXSelectBase) this.Base.Case).Cache.GetStatus((object) crCaseRow) == 2;
    ((PXAction) this.CreateServiceOrder).SetEnabled(flag1 && crCaseRow != null && !flag2 && fsServiceOrderRow == null);
    ((PXAction) this.ViewServiceOrder).SetEnabled(flag1 && crCaseRow != null && !flag2 && fsServiceOrderRow != null);
    ((PXAction) this.OpenAppointmentBoard).SetEnabled(fsServiceOrderRow != null);
  }

  public virtual void SetBranchLocationID(PXGraph graph, FSxCRCase fsxCaseRow)
  {
    UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>.Config>.Select(graph, Array.Empty<object>()));
    if (userPreferences != null)
    {
      FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(userPreferences);
      if (extension == null)
        return;
      fsxCaseRow.BranchLocationID = extension.DfltBranchLocationID;
    }
    else
      fsxCaseRow.BranchLocationID = new int?();
  }

  public virtual void CreateServiceOrderDocument(
    CRCaseMaint graphCRCaseMaint,
    CRCase crCaseRow,
    FSCreateServiceOrderOnCaseFilter fsCreateServiceOrderOnCaseFilterRow)
  {
    if (graphCRCaseMaint == null || crCaseRow == null || fsCreateServiceOrderOnCaseFilterRow == null)
      return;
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    FSServiceOrder fsServiceOrder = CRExtensionHelper.InitNewServiceOrder(((PXSelectBase<FSCreateServiceOrderOnCaseFilter>) this.CreateServiceOrderFilter).Current.SrvOrdType, "CR");
    ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Insert(fsServiceOrder);
    DocGenerationHelper.ValidateAutoNumbering((PXGraph) this.Base, ((PXSelectBase<FSSrvOrdType>) instance.ServiceOrderTypeSelected).SelectSingle(Array.Empty<object>())?.SrvOrdNumberingID);
    this.UpdateServiceOrderHeader(instance, ((PXSelectBase) this.Base.Case).Cache, crCaseRow, fsCreateServiceOrderOnCaseFilterRow, ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current, true);
    ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current.SourceRefNbr = crCaseRow.CaseCD;
    if (!((PXGraph) this.Base).IsContractBasedAPI)
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
  }

  public virtual void UpdateServiceOrderHeader(
    ServiceOrderEntry graphServiceOrderEntry,
    PXCache cache,
    CRCase crCaseRow,
    FSCreateServiceOrderOnCaseFilter fsCreateServiceOrderOnCaseFilterRow,
    FSServiceOrder fsServiceOrderRow,
    bool updatingExistingSO)
  {
    if (fsServiceOrderRow.Closed.GetValueOrDefault())
      return;
    bool flag = false;
    int? nullable1;
    int? nullable2;
    if (CRExtensionHelper.GetServiceOrderType((PXGraph) graphServiceOrderEntry, fsServiceOrderRow.SrvOrdType).Behavior != "IN")
    {
      nullable1 = fsServiceOrderRow.CustomerID;
      nullable2 = crCaseRow.CustomerID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.customerID>(fsServiceOrderRow, (object) crCaseRow.CustomerID);
        flag = true;
      }
      nullable2 = fsServiceOrderRow.LocationID;
      nullable1 = crCaseRow.LocationID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.locationID>(fsServiceOrderRow, (object) crCaseRow.LocationID);
        flag = true;
      }
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) graphServiceOrderEntry, new object[1]
      {
        (object) crCaseRow.CustomerID
      }));
      if (customer != null && fsServiceOrderRow.CuryID != customer.CuryID && customer.CuryID != null)
      {
        ((PXSelectBase) graphServiceOrderEntry.ServiceOrderRecords).Cache.SetValueExt<FSServiceOrder.curyID>((object) fsServiceOrderRow, (object) customer.CuryID);
        flag = true;
      }
    }
    nullable1 = fsServiceOrderRow.BranchLocationID;
    nullable2 = fsCreateServiceOrderOnCaseFilterRow.BranchLocationID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.branchLocationID>(fsServiceOrderRow, (object) fsCreateServiceOrderOnCaseFilterRow.BranchLocationID);
      flag = true;
    }
    nullable2 = fsServiceOrderRow.ContactID;
    nullable1 = crCaseRow.ContactID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.contactID>(fsServiceOrderRow, (object) crCaseRow.ContactID);
      flag = true;
    }
    if (fsServiceOrderRow.DocDesc != crCaseRow.Subject)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.docDesc>(fsServiceOrderRow, (object) crCaseRow.Subject);
      flag = true;
    }
    nullable1 = crCaseRow.OwnerID;
    if (nullable1.HasValue)
    {
      nullable1 = crCaseRow.OwnerID;
      nullable2 = (int?) cache.GetValueOriginal<PX.Objects.CR.CROpportunity.ownerID>((object) crCaseRow);
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        int? salesPersonId = CRExtensionHelper.GetSalesPersonID((PXGraph) graphServiceOrderEntry, crCaseRow.OwnerID);
        if (salesPersonId.HasValue)
        {
          ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.salesPersonID>(fsServiceOrderRow, (object) salesPersonId);
          flag = true;
        }
      }
    }
    DateTime? slaeta1 = fsServiceOrderRow.SLAETA;
    DateTime? slaeta2 = crCaseRow.SLAETA;
    if ((slaeta1.HasValue == slaeta2.HasValue ? (slaeta1.HasValue ? (slaeta1.GetValueOrDefault() != slaeta2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.sLAETA>(fsServiceOrderRow, (object) crCaseRow.SLAETA);
      flag = true;
    }
    nullable2 = fsServiceOrderRow.AssignedEmpID;
    nullable1 = fsCreateServiceOrderOnCaseFilterRow.AssignedEmpID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.assignedEmpID>(fsServiceOrderRow, (object) fsCreateServiceOrderOnCaseFilterRow.AssignedEmpID);
      flag = true;
    }
    nullable1 = fsServiceOrderRow.ProblemID;
    nullable2 = fsCreateServiceOrderOnCaseFilterRow.ProblemID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.problemID>(fsServiceOrderRow, (object) fsCreateServiceOrderOnCaseFilterRow.ProblemID);
      flag = true;
    }
    if (fsServiceOrderRow.LongDescr != crCaseRow.Description)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.longDescr>(fsServiceOrderRow, (object) crCaseRow.Description);
      flag = true;
    }
    if (fsServiceOrderRow.Priority != crCaseRow.Priority)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.priority>(fsServiceOrderRow, (object) crCaseRow.Priority);
      flag = true;
    }
    if (fsServiceOrderRow.Severity != crCaseRow.Severity)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).SetValueExt<FSServiceOrder.severity>(fsServiceOrderRow, (object) crCaseRow.Severity);
      flag = true;
    }
    UDFHelper.CopyAttributes(cache, (object) crCaseRow, ((PXSelectBase) graphServiceOrderEntry.ServiceOrderRecords).Cache, (object) fsServiceOrderRow, (string) null);
    if (!(flag & updatingExistingSO))
      return;
    ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).Update(fsServiceOrderRow);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<CRCase, FSxCRCase.sDEnabled> e)
  {
    if (e.Row == null)
      return;
    CRCase row = e.Row;
    FSxCRCase extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CRCase, FSxCRCase.sDEnabled>>) e).Cache.GetExtension<FSxCRCase>((object) row);
    if (extension.SDEnabled.GetValueOrDefault())
    {
      FSSetup fsSetup = this.GetFSSetup();
      if (fsSetup != null && fsSetup.DfltCasesSrvOrdType != null)
        extension.SrvOrdType = fsSetup.DfltCasesSrvOrdType;
      this.SetBranchLocationID((PXGraph) this.Base, extension);
    }
    else
    {
      extension.SrvOrdType = (string) null;
      extension.BranchLocationID = new int?();
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<CRCase> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRCase> e)
  {
    if (e.Row == null)
      return;
    CRCase row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache;
    FSxCRCase extension = cache.GetExtension<FSxCRCase>((object) row);
    FSServiceOrder relatedServiceOrder = CRExtensionHelper.GetRelatedServiceOrder((PXGraph) this.Base, cache, (IBqlTable) row, extension.ServiceOrderRefNbr, extension.SrvOrdType);
    FSSrvOrdType fsSrvOrdTypeRow = (FSSrvOrdType) null;
    if (relatedServiceOrder != null)
      fsSrvOrdTypeRow = CRExtensionHelper.GetServiceOrderType((PXGraph) this.Base, relatedServiceOrder.SrvOrdType);
    this.EnableDisableCustomFields(cache, row, extension, relatedServiceOrder, fsSrvOrdTypeRow);
    this.EnableDisableActions(cache, row, extension, relatedServiceOrder, fsSrvOrdTypeRow);
    this.SetPersistingChecks(cache, row, extension, fsSrvOrdTypeRow);
  }

  protected virtual void _(PX.Data.Events.RowInserting<CRCase> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<CRCase> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<CRCase> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CRCase> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<CRCase> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CRCase> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRCase> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<CRCase> e)
  {
  }
}
