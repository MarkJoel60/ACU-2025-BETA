// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_CustomerMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class SM_CustomerMaint : PXGraphExtension<CustomerMaint>
{
  private bool doCopyBillingSettings;
  [PXHidden]
  public PXSelect<FSSetup> Setup;
  public PXSelectJoin<FSCustomerBillingSetup, CrossJoin<FSSetup>, Where<FSCustomerBillingSetup.customerID, Equal<Current<PX.Objects.AR.Customer.bAccountID>>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, IsNotNull>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>> CustomerBillingCycles;
  public PXAction<PX.Objects.AR.Customer> viewServiceOrderHistory;
  public PXAction<PX.Objects.AR.Customer> viewAppointmentHistory;
  public PXAction<PX.Objects.AR.Customer> viewEquipmentSummary;
  public PXAction<PX.Objects.AR.Customer> viewContractScheduleSummary;
  public PXAction<PX.Objects.AR.Customer> openMultipleStaffMemberBoard;
  public PXAction<PX.Objects.AR.Customer> openSingleStaffMemberBoard;

  [InjectDependency]
  protected PXSiteMapProvider SiteMapProvider { get; private set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public PX.Objects.AR.Customer PersistedCustomerWithBillingOptionsChanged { get; set; }

  public FSSetup GetFSSetup()
  {
    return ((PXSelectBase<FSSetup>) this.Setup).Current == null ? PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.Setup).Select(Array.Empty<object>())) : ((PXSelectBase<FSSetup>) this.Setup).Current;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewServiceOrderHistory(PXAdapter adapter)
  {
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.CurrentCustomer).Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        PX.SM.Branch branch = PXResultset<PX.SM.Branch>.op_Implicit(PXSelectBase<PX.SM.Branch, PXSelect<PX.SM.Branch, Where<PX.SM.Branch.branchID, Equal<Required<PX.SM.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) ((PXGraph) this.Base).Accessinfo.BranchID
        }));
        parameters["BranchID"] = branch.BranchCD;
        parameters["CustomerID"] = current.AcctCD;
        throw new PXRedirectToGIWithParametersRequiredException(new Guid("84b92648-c42e-41e8-855c-4aa9144b9eda"), parameters);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewAppointmentHistory(PXAdapter adapter)
  {
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.CurrentCustomer).Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        AppointmentInq instance = PXGraph.CreateInstance<AppointmentInq>();
        ((PXSelectBase<AppointmentInq.AppointmentInqFilter>) instance.Filter).Current.BranchID = ((PXGraph) this.Base).Accessinfo.BranchID;
        ((PXSelectBase<AppointmentInq.AppointmentInqFilter>) instance.Filter).Current.CustomerID = current.BAccountID;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewEquipmentSummary(PXAdapter adapter)
  {
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.CurrentCustomer).Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXRedirectToGIWithParametersRequiredException(new Guid("e850784d-9b5c-45f9-a7ca-085aa07cdcdb"), new Dictionary<string, string>()
        {
          ["CustomerID"] = current.AcctCD
        });
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewContractScheduleSummary(PXAdapter adapter)
  {
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.CurrentCustomer).Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXRedirectToGIWithParametersRequiredException(new Guid("09c88688-263d-426a-a19e-de1d0c3d3ad3"), new Dictionary<string, string>()
        {
          ["CustomerID"] = current.AcctCD
        });
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenMultipleStaffMemberBoard(PXAdapter adapter)
  {
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.CurrentCustomer).Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        KeyValuePair<string, string>[] keyValuePairArray1 = new KeyValuePair<string, string>[1];
        string name = typeof (FSServiceOrder.customerID).Name;
        baccountId = current.BAccountID;
        string str = baccountId.Value.ToString();
        keyValuePairArray1[0] = new KeyValuePair<string, string>(name, str);
        KeyValuePair<string, string>[] keyValuePairArray2 = keyValuePairArray1;
        PXSiteMapProvider siteMapProvider = this.SiteMapProvider;
        KeyValuePair<string, string>[] parameters = keyValuePairArray2;
        MainAppointmentFilter calendarFilter = new MainAppointmentFilter();
        baccountId = current.BAccountID;
        calendarFilter.InitialCustomerID = baccountId.Value.ToString();
        throw PXRedirectToBoardRequiredException.GenerateMultiEmployeeRedirect(siteMapProvider, parameters, calendarFilter);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenSingleStaffMemberBoard(PXAdapter adapter)
  {
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.CurrentCustomer).Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        KeyValuePair<string, string>[] parameters = new KeyValuePair<string, string>[1];
        string name = typeof (FSServiceOrder.customerID).Name;
        baccountId = current.BAccountID;
        string str = baccountId.Value.ToString();
        parameters[0] = new KeyValuePair<string, string>(name, str);
        throw new PXRedirectToBoardRequiredException("pages/fs/calendars/SingleEmpDispatch/FS300400.aspx", parameters);
      }
    }
    return adapter.Get();
  }

  [PXOverride]
  public virtual void Persist(System.Action baseMethod)
  {
    if (baseMethod == null)
      return;
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.BAccount).Current;
    if (current == null)
    {
      baseMethod();
    }
    else
    {
      PXEntryStatus status = ((PXSelectBase) this.Base.BAccount).Cache.GetStatus((object) current);
      if (status != null && status != 1)
      {
        baseMethod();
      }
      else
      {
        this.PersistedCustomerWithBillingOptionsChanged = (PX.Objects.AR.Customer) null;
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          try
          {
            ((PXGraph) this.Base).Persist(typeof (FSCustomerBillingSetup), (PXDBOperation) 1);
            ((PXGraph) this.Base).Persist(typeof (FSCustomerBillingSetup), (PXDBOperation) 2);
          }
          catch
          {
            ((PXGraph) this.Base).Caches[typeof (FSCustomerBillingSetup)].Persisted(true);
            throw;
          }
          try
          {
            baseMethod();
          }
          catch
          {
            ((PXGraph) this.Base).Caches[typeof (FSCustomerBillingSetup)].Persisted(true);
            throw;
          }
          transactionScope.Complete();
        }
      }
    }
  }

  /// <summary>
  /// Sets the Customer Billing Cycle from its Customer Class.
  /// </summary>
  public virtual void SetBillingCycleFromCustomerClass(PXCache cache, PX.Objects.AR.Customer customerRow)
  {
    if (customerRow.CustomerClassID == null)
      return;
    FSSetup fsSetup = this.GetFSSetup();
    bool? multipleBillingOptions;
    if (fsSetup != null)
    {
      multipleBillingOptions = fsSetup.CustomerMultipleBillingOptions;
      if (multipleBillingOptions.GetValueOrDefault())
      {
        PXSelect<PX.Objects.AR.Customer, Where2<Match<Current<AccessInfo.userName>>, And<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>>>> baccount = this.Base.BAccount;
        int num;
        if (baccount == null)
        {
          num = 0;
        }
        else
        {
          PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) baccount).Current;
          num = current != null ? (current.BAccountID.HasValue ? 1 : 0) : 0;
        }
        if (num != 0)
        {
          if (((IQueryable<PXResult<FSCustomerBillingSetup>>) PXSelectBase<FSCustomerBillingSetup, PXSelectJoin<FSCustomerBillingSetup, InnerJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSCustomerBillingSetup.srvOrdType>>, CrossJoin<FSSetup>>, Where<FSCustomerBillingSetup.customerID, Equal<Current<PX.Objects.AR.Customer.bAccountID>>, And<FSCustomerBillingSetup.active, Equal<True>, And<FSServiceOrder.customerID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<Where<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, IsNotNull>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.BAccount).Current.BAccountID
          })).Any<PXResult<FSCustomerBillingSetup>>())
          {
            ((PXSelectBase) this.Base.BAccount).Cache.RaiseExceptionHandling<PX.Objects.AR.Customer.customerClassID>((object) customerRow, (object) customerRow.CustomerClassID, (Exception) new PXSetPropertyException("Billing settings cannot be modified because at least one service order or appointment has been created for the customer.", (PXErrorLevel) 2));
            return;
          }
        }
      }
    }
    if (fsSetup != null)
    {
      multipleBillingOptions = fsSetup.CustomerMultipleBillingOptions;
      if (multipleBillingOptions.GetValueOrDefault())
      {
        foreach (PXResult<FSCustomerBillingSetup> pxResult in ((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Select(Array.Empty<object>()))
          ((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Delete(PXResult<FSCustomerBillingSetup>.op_Implicit(pxResult));
        using (IEnumerator<PXResult<FSCustomerClassBillingSetup>> enumerator = PXSelectBase<FSCustomerClassBillingSetup, PXSelect<FSCustomerClassBillingSetup, Where<FSCustomerClassBillingSetup.customerClassID, Equal<Required<FSCustomerClassBillingSetup.customerClassID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) customerRow.CustomerClassID
        }).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            FSCustomerClassBillingSetup classBillingSetup = PXResult<FSCustomerClassBillingSetup>.op_Implicit(enumerator.Current);
            FSCustomerBillingSetup customerBillingSetup = new FSCustomerBillingSetup();
            customerBillingSetup.SrvOrdType = classBillingSetup.SrvOrdType;
            customerBillingSetup.BillingCycleID = classBillingSetup.BillingCycleID;
            customerBillingSetup.SendInvoicesTo = classBillingSetup.SendInvoicesTo;
            customerBillingSetup.BillShipmentSource = classBillingSetup.BillShipmentSource;
            customerBillingSetup.FrequencyType = classBillingSetup.FrequencyType;
            using (new ReadOnlyScope(new PXCache[1]
            {
              ((PXSelectBase) this.CustomerBillingCycles).Cache
            }))
              ((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Insert(customerBillingSetup);
          }
          return;
        }
      }
    }
    this.SetSingleBillingSettings(cache, customerRow);
  }

  public virtual void SetSingleBillingSettings(PXCache cache, PX.Objects.AR.Customer customerRow)
  {
    if (customerRow.CustomerClassID == null)
      return;
    cache.GetExtension<FSxCustomer>((object) customerRow);
    FSxCustomerClass extension = ((PXSelectBase) this.Base.CustomerClass).Cache.GetExtension<FSxCustomerClass>((object) ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current);
    if (extension == null)
      return;
    if (extension.DefaultBillingCycleID.HasValue)
      cache.SetValueExt<FSxCustomer.billingCycleID>((object) customerRow, (object) extension.DefaultBillingCycleID);
    if (extension.SendInvoicesTo != null)
      cache.SetValueExt<FSxCustomer.sendInvoicesTo>((object) customerRow, (object) extension.SendInvoicesTo);
    if (extension.BillShipmentSource == null)
      return;
    cache.SetValueExt<FSxCustomer.billShipmentSource>((object) customerRow, (object) extension.BillShipmentSource);
  }

  /// <summary>
  /// Resets the values of the Frequency Fields depending on the Frequency Type value.
  /// </summary>
  /// <param name="fsCustomerBillingSetupRow"><c>fsCustomerBillingRow</c> row.</param>
  public virtual void ResetTimeCycleOptions(FSCustomerBillingSetup fsCustomerBillingSetupRow)
  {
    switch (fsCustomerBillingSetupRow.FrequencyType)
    {
      case "MT":
        fsCustomerBillingSetupRow.MonthlyFrequency = new int?(31 /*0x1F*/);
        break;
      case "WK":
        fsCustomerBillingSetupRow.WeeklyFrequency = new int?(5);
        break;
      default:
        fsCustomerBillingSetupRow.WeeklyFrequency = new int?();
        fsCustomerBillingSetupRow.MonthlyFrequency = new int?();
        break;
    }
  }

  /// <summary>
  /// Configures the Multiple Services Billing options for the given Customer.
  /// </summary>
  /// <param name="cache">Cache of the view.</param>
  /// <param name="customerRow">Customer row.</param>
  public virtual void DisplayCustomerBillingOptions(
    PXCache cache,
    PX.Objects.AR.Customer customerRow,
    FSxCustomer fsxCustomerRow)
  {
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    bool flag1 = fsSetup != null && fsSetup.CustomerMultipleBillingOptions.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<FSxCustomer.billingCycleID>(cache, (object) customerRow, !flag1);
    PXUIFieldAttribute.SetVisible<FSxCustomer.sendInvoicesTo>(cache, (object) customerRow, !flag1);
    PXUIFieldAttribute.SetVisible<FSxCustomer.billShipmentSource>(cache, (object) customerRow, !flag1);
    ((PXSelectBase) this.CustomerBillingCycles).AllowSelect = flag1;
    if (fsxCustomerRow == null)
      return;
    bool flag2 = SharedFunctions.IsNotAllowedBillingOptionsModification(FSBillingCycle.PK.Find((PXGraph) this.Base, fsxCustomerRow.BillingCycleID));
    PXUIFieldAttribute.SetEnabled<FSxCustomer.sendInvoicesTo>(cache, (object) customerRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSxCustomer.billShipmentSource>(cache, (object) customerRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSxCustomer.billingCycleID>(cache, (object) customerRow);
  }

  /// <summary>
  /// Resets the value from Send to Invoices dropdown if the billing cycle can not be sent to specific locations.
  /// </summary>
  public virtual void ResetSendInvoicesToFromBillingCycle(
    PX.Objects.AR.Customer customerRow,
    FSCustomerBillingSetup fsCustomerBillingSetupRow)
  {
    List<object> objectList = new List<object>();
    PXView pxView = new PXView((PXGraph) this.Base, true, (BqlCommand) new Select<FSBillingCycle, Where<FSBillingCycle.billingCycleID, Equal<Required<FSBillingCycle.billingCycleID>>>>());
    if (customerRow != null)
    {
      FSxCustomer extension = PXCache<PX.Objects.AR.Customer>.GetExtension<FSxCustomer>(customerRow);
      objectList.Add((object) extension.BillingCycleID);
      FSBillingCycle fsBillingCycleRow = (FSBillingCycle) pxView.SelectSingle(objectList.ToArray());
      if (fsBillingCycleRow == null)
        return;
      if (SharedFunctions.IsNotAllowedBillingOptionsModification(fsBillingCycleRow))
      {
        extension.SendInvoicesTo = "BT";
        extension.BillShipmentSource = "SO";
      }
      if (extension.SendInvoicesTo == null)
        ((PXSelectBase) this.Base.CurrentCustomer).Cache.SetDefaultExt<FSxCustomer.sendInvoicesTo>((object) customerRow);
      if (extension.BillShipmentSource == null)
        ((PXSelectBase) this.Base.CurrentCustomer).Cache.SetDefaultExt<FSxCustomer.billShipmentSource>((object) customerRow);
      if (extension.DefaultBillingCustomerSource != null)
        return;
      ((PXSelectBase) this.Base.CurrentCustomer).Cache.SetDefaultExt<FSxCustomer.defaultBillingCustomerSource>((object) customerRow);
    }
    else
    {
      if (fsCustomerBillingSetupRow == null)
        return;
      objectList.Add((object) fsCustomerBillingSetupRow.BillingCycleID);
      FSBillingCycle fsBillingCycleRow = (FSBillingCycle) pxView.SelectSingle(objectList.ToArray());
      if (fsBillingCycleRow == null || !SharedFunctions.IsNotAllowedBillingOptionsModification(fsBillingCycleRow))
        return;
      fsCustomerBillingSetupRow.SendInvoicesTo = "BT";
      fsCustomerBillingSetupRow.BillShipmentSource = "SO";
    }
  }

  public virtual void InsertUpdateCustomerBillingSetup(
    PXCache cache,
    PX.Objects.AR.Customer customerRow,
    FSxCustomer fsxCustomerRow)
  {
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (fsSetup == null)
      return;
    bool? multipleBillingOptions = fsSetup.CustomerMultipleBillingOptions;
    bool flag = false;
    if (!(multipleBillingOptions.GetValueOrDefault() == flag & multipleBillingOptions.HasValue))
      return;
    FSCustomerBillingSetup customerBillingSetup = PXResultset<FSCustomerBillingSetup>.op_Implicit(((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Select(Array.Empty<object>()));
    if (!fsxCustomerRow.BillingCycleID.HasValue)
    {
      ((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Delete(customerBillingSetup);
    }
    else
    {
      if (customerBillingSetup == null)
        customerBillingSetup = ((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Insert(new FSCustomerBillingSetup()
        {
          CustomerID = customerRow.BAccountID
        });
      customerBillingSetup.BillingCycleID = fsxCustomerRow.BillingCycleID;
      customerBillingSetup.SendInvoicesTo = fsxCustomerRow.SendInvoicesTo;
      customerBillingSetup.BillShipmentSource = fsxCustomerRow.BillShipmentSource;
      customerBillingSetup.FrequencyType = "NO";
      ((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Update(customerBillingSetup);
    }
  }

  public virtual void SetBillingCustomerSetting(PXCache cache, PX.Objects.AR.Customer customerRow)
  {
    FSxCustomer extension1 = cache.GetExtension<FSxCustomer>((object) customerRow);
    FSxCustomerClass extension2 = ((PXSelectBase) this.Base.CustomerClass).Cache.GetExtension<FSxCustomerClass>((object) ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current);
    extension1.DefaultBillingCustomerSource = extension2.DefaultBillingCustomerSource;
    extension1.BillCustomerID = extension2.BillCustomerID;
    extension1.BillLocationID = extension2.BillLocationID;
  }

  public virtual void EnableDisableCustomerBilling(
    PXCache cache,
    PX.Objects.AR.Customer customerRow,
    FSxCustomer fsxCustomerRow)
  {
    bool flag = fsxCustomerRow.DefaultBillingCustomerSource == "LC";
    PXUIFieldAttribute.SetVisible<FSxCustomer.billCustomerID>(cache, (object) customerRow, flag);
    PXUIFieldAttribute.SetVisible<FSxCustomer.billLocationID>(cache, (object) customerRow, flag);
    PXDefaultAttribute.SetPersistingCheck<FSxCustomer.billCustomerID>(cache, (object) customerRow, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<FSxCustomer.billLocationID>(cache, (object) customerRow, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (flag)
      return;
    fsxCustomerRow.BillCustomerID = new int?();
    fsxCustomerRow.BillLocationID = new int?();
  }

  public virtual void VerifyPrepaidContractRelated(
    PXCache cache,
    FSCustomerBillingSetup fsCustomerBillingSetupRow)
  {
    int? valueOriginal = (int?) cache.GetValueOriginal<FSCustomerBillingSetup.billingCycleID>((object) fsCustomerBillingSetupRow);
    if (!valueOriginal.HasValue)
      return;
    int? billingCycleId = fsCustomerBillingSetupRow.BillingCycleID;
    int? nullable = valueOriginal;
    if (billingCycleId.GetValueOrDefault() == nullable.GetValueOrDefault() & billingCycleId.HasValue == nullable.HasValue)
      return;
    FSBillingCycle fsBillingCycle1 = PXResultset<FSBillingCycle>.op_Implicit(PXSelectBase<FSBillingCycle, PXSelect<FSBillingCycle, Where<FSBillingCycle.billingCycleID, Equal<Required<FSBillingCycle.billingCycleID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) fsCustomerBillingSetupRow.BillingCycleID
    }));
    FSBillingCycle fsBillingCycle2 = PXResultset<FSBillingCycle>.op_Implicit(PXSelectBase<FSBillingCycle, PXSelect<FSBillingCycle, Where<FSBillingCycle.billingCycleID, Equal<Required<FSBillingCycle.billingCycleID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) valueOriginal
    }));
    if (!(fsBillingCycle1.BillingBy != fsBillingCycle2.BillingBy))
      return;
    List<object> objectList = new List<object>();
    BqlCommand bqlCommand = (BqlCommand) null;
    string str = "Service Orders";
    if (fsBillingCycle2.BillingBy == "SO")
      bqlCommand = (BqlCommand) new Select2<FSServiceOrder, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSServiceOrder.billServiceContractID>>, InnerJoin<FSContractPeriod, On<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPeriod.contractPeriodID, Equal<FSServiceOrder.billContractPeriodID>>>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.billCustomerID, Equal<Required<FSServiceOrder.billCustomerID>>, And<FSServiceOrder.canceled, Equal<False>, And<FSServiceContract.status, NotEqual<ListField_Status_ServiceContract.Canceled>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>>>>();
    else if (fsBillingCycle2.BillingBy == "AP")
    {
      bqlCommand = (BqlCommand) new Select2<FSServiceOrder, InnerJoin<FSAppointment, On<FSAppointment.sOID, Equal<FSServiceOrder.sOID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSAppointment.billServiceContractID>>, InnerJoin<FSContractPeriod, On<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPeriod.contractPeriodID, Equal<FSAppointment.billContractPeriodID>>>>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.billCustomerID, Equal<Required<FSServiceOrder.billCustomerID>>, And<FSAppointment.canceled, Equal<False>, And<FSServiceContract.status, NotEqual<ListField_Status_ServiceContract.Canceled>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>>>>();
      str = "Appointments";
    }
    objectList.Add((object) fsCustomerBillingSetupRow.SrvOrdType);
    objectList.Add((object) fsCustomerBillingSetupRow.CustomerID);
    if (new PXView(new PXGraph(), true, bqlCommand).SelectSingle(objectList.ToArray()) != null)
    {
      PXException pxException = (PXException) new PXSetPropertyException("The billing cycle cannot be modified because it has been assigned to at least one customer whose {0} are related to prepaid contracts.", (PXErrorLevel) 4, new object[1]
      {
        (object) str
      });
      FSSetup fsSetup = this.GetFSSetup();
      int num;
      if (fsSetup == null)
      {
        num = 0;
      }
      else
      {
        bool? multipleBillingOptions = fsSetup.CustomerMultipleBillingOptions;
        bool flag = false;
        num = multipleBillingOptions.GetValueOrDefault() == flag & multipleBillingOptions.HasValue ? 1 : 0;
      }
      if (num != 0)
      {
        FSxCustomer extension = ((PXSelectBase) this.Base.CurrentCustomer).Cache.GetExtension<FSxCustomer>((object) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.CurrentCustomer).Current);
        if (extension != null)
          ((PXSelectBase) this.Base.CurrentCustomer).Cache.RaiseExceptionHandling<FSxCustomer.billingCycleID>((object) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.CurrentCustomer).Current, (object) extension.BillingCycleID, (Exception) pxException);
      }
      else
        cache.RaiseExceptionHandling<FSCustomerBillingSetup.srvOrdType>((object) fsCustomerBillingSetupRow, (object) fsCustomerBillingSetupRow.SrvOrdType, (Exception) pxException);
      throw pxException;
    }
  }

  public virtual void UpdateBillCustomerInfoInDocsExtendCustomer(
    PXGraph callerGraph,
    int? currentCustomerID)
  {
    ServiceOrderEntry serviceOrderEntry = (ServiceOrderEntry) null;
    foreach (PXResult<FSServiceOrder> pxResult in PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.customerID, Equal<Required<FSServiceOrder.customerID>>, And<FSServiceOrder.billCustomerID, IsNull, And<FSServiceOrder.billLocationID, IsNull>>>>.Config>.Select(callerGraph, new object[1]
    {
      (object) currentCustomerID
    }))
    {
      FSServiceOrder fsServiceOrder = PXResult<FSServiceOrder>.op_Implicit(pxResult);
      if (serviceOrderEntry == null)
        serviceOrderEntry = PXGraph.CreateInstance<ServiceOrderEntry>();
      ((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) fsServiceOrder.RefNbr, new object[1]
      {
        (object) fsServiceOrder.SrvOrdType
      }));
      if (((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current != null)
      {
        ((PXSelectBase) serviceOrderEntry.ServiceOrderRecords).Cache.SetValueExt<FSServiceOrder.billCustomerID>((object) ((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current, (object) fsServiceOrder.CustomerID);
        ((PXSelectBase) serviceOrderEntry.ServiceOrderRecords).Cache.SetValueExt<FSServiceOrder.billLocationID>((object) ((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current, (object) fsServiceOrder.LocationID);
        ((PXAction) serviceOrderEntry.Save).Press();
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CR.BAccount> e)
  {
    if (e.Row == null)
      return;
    FSxCustomer extension = ((PXSelectBase) this.Base.BAccount).Cache.GetExtension<FSxCustomer>((object) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.BAccount).Current);
    if (!(e.OldRow.Type == "PR") || !(e.Row.Type == "CU"))
      return;
    extension.IsExtendingToCustomer = new bool?(true);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.AR.Customer, FSxCustomer.billLocationID> e)
  {
    if (e.Row == null)
      return;
    FSxCustomer extension = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.AR.Customer, FSxCustomer.billLocationID>>) e).Cache.GetExtension<FSxCustomer>((object) e.Row);
    BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) extension.BillCustomerID
    }));
    int? nullable;
    if (baccountR != null)
    {
      nullable = baccountR.DefLocationID;
      if (nullable.HasValue)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AR.Customer, FSxCustomer.billLocationID>, PX.Objects.AR.Customer, object>) e).NewValue = (object) baccountR.DefLocationID;
        return;
      }
    }
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelect<PX.Objects.CR.Standalone.Location, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Required<PX.Objects.CR.Standalone.Location.bAccountID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) extension.BillCustomerID
    }));
    if (location == null)
      return;
    nullable = location.LocationID;
    if (!nullable.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AR.Customer, FSxCustomer.billLocationID>, PX.Objects.AR.Customer, object>) e).NewValue = (object) location.LocationID;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.AR.Customer, PX.Objects.AR.Customer.customerClassID> e)
  {
    PX.Objects.AR.Customer row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.AR.Customer, PX.Objects.AR.Customer.customerClassID>>) e).Cache;
    PX.Objects.AR.CustomerClass customerClass = (PX.Objects.AR.CustomerClass) PXSelectorAttribute.Select<PX.Objects.AR.Customer.customerClassID>(cache, (object) row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.AR.Customer, PX.Objects.AR.Customer.customerClassID>, PX.Objects.AR.Customer, object>) e).NewValue);
    this.doCopyBillingSettings = false;
    if (customerClass == null)
      return;
    this.doCopyBillingSettings = true;
    if (cache.GetStatus((object) row) == 2 || ((PXGraph) this.Base).UnattendedMode || ((PXGraph) this.Base).IsContractBasedAPI || ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.BAccount).Ask("Update Billing Settings", "Please confirm if you want to update the current customer billing settings with the customer class defaults. Otherwise, the original billing settings will be preserved.", (MessageButtons) 4) != 7)
      return;
    this.doCopyBillingSettings = false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, PX.Objects.AR.Customer.customerClassID> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, PX.Objects.AR.Customer.customerClassID>>) e).Cache;
    if (this.doCopyBillingSettings)
    {
      FSxCustomer extension = cache.GetExtension<FSxCustomer>((object) row);
      this.SetBillingCycleFromCustomerClass(cache, row);
      this.InsertUpdateCustomerBillingSetup(cache, row, extension);
    }
    this.SetBillingCustomerSetting(cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billingCycleID> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billingCycleID>>) e).Cache;
    FSxCustomer extension = cache.GetExtension<FSxCustomer>((object) row);
    if (!extension.BillingCycleID.HasValue)
      extension.SendInvoicesTo = "DF";
    this.ResetSendInvoicesToFromBillingCycle(row, (FSCustomerBillingSetup) null);
    this.InsertUpdateCustomerBillingSetup(cache, row, extension);
    if (e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billingCycleID>, PX.Objects.AR.Customer, object>) e).OldValue)
      return;
    PXUIFieldAttribute.SetWarning<FSxCustomer.billingCycleID>(cache, (object) row, "Note that at the time the billing process is performed, the system uses the billing cycle specified in this box. If you change the billing cycle, the system will use the newly specified billing cycle to process the customer's service orders and appointments that have not been billed yet, and to process any service orders and appointments whose billing documents will be corrected and for which billing will run again.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.sendInvoicesTo> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.sendInvoicesTo>>) e).Cache;
    FSxCustomer extension = cache.GetExtension<FSxCustomer>((object) row);
    this.InsertUpdateCustomerBillingSetup(cache, row, extension);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billShipmentSource> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billShipmentSource>>) e).Cache;
    FSxCustomer extension = cache.GetExtension<FSxCustomer>((object) row);
    this.InsertUpdateCustomerBillingSetup(cache, row, extension);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billCustomerID> e)
  {
    if (e.Row == null)
      return;
    FSxCustomer extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billCustomerID>>) e).Cache.GetExtension<FSxCustomer>((object) e.Row);
    int? oldValue = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billCustomerID>, PX.Objects.AR.Customer, object>) e).OldValue;
    int? billCustomerId = extension.BillCustomerID;
    if (oldValue.GetValueOrDefault() == billCustomerId.GetValueOrDefault() & oldValue.HasValue == billCustomerId.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.Customer, FSxCustomer.billCustomerID>>) e).Cache.SetDefaultExt<FSxCustomer.billLocationID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.AR.Customer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.Customer> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.Customer>>) e).Cache;
    FSxCustomer extension = cache.GetExtension<FSxCustomer>((object) row);
    PXUIFieldAttribute.SetEnabled<FSxCustomer.sendInvoicesTo>(cache, (object) row, extension.BillingCycleID.HasValue);
    PXUIFieldAttribute.SetEnabled<FSxCustomer.billShipmentSource>(cache, (object) row, extension.BillingCycleID.HasValue);
    this.DisplayCustomerBillingOptions(cache, row, extension);
    PXAction<PX.Objects.AR.Customer> serviceOrderHistory = this.viewServiceOrderHistory;
    int? baccountId1 = row.BAccountID;
    int num1 = 0;
    int num2 = baccountId1.GetValueOrDefault() > num1 & baccountId1.HasValue ? 1 : 0;
    ((PXAction) serviceOrderHistory).SetEnabled(num2 != 0);
    PXAction<PX.Objects.AR.Customer> appointmentHistory = this.viewAppointmentHistory;
    int? baccountId2 = row.BAccountID;
    int num3 = 0;
    int num4 = baccountId2.GetValueOrDefault() > num3 & baccountId2.HasValue ? 1 : 0;
    ((PXAction) appointmentHistory).SetEnabled(num4 != 0);
    PXAction<PX.Objects.AR.Customer> equipmentSummary = this.viewEquipmentSummary;
    int? baccountId3 = row.BAccountID;
    int num5 = 0;
    int num6 = baccountId3.GetValueOrDefault() > num5 & baccountId3.HasValue ? 1 : 0;
    ((PXAction) equipmentSummary).SetEnabled(num6 != 0);
    PXAction<PX.Objects.AR.Customer> contractScheduleSummary = this.viewContractScheduleSummary;
    int? baccountId4 = row.BAccountID;
    int num7 = 0;
    int num8 = baccountId4.GetValueOrDefault() > num7 & baccountId4.HasValue ? 1 : 0;
    ((PXAction) contractScheduleSummary).SetEnabled(num8 != 0);
    PXAction<PX.Objects.AR.Customer> staffMemberBoard1 = this.openMultipleStaffMemberBoard;
    int? baccountId5 = row.BAccountID;
    int num9 = 0;
    int num10 = baccountId5.GetValueOrDefault() > num9 & baccountId5.HasValue ? 1 : 0;
    ((PXAction) staffMemberBoard1).SetEnabled(num10 != 0);
    PXAction<PX.Objects.AR.Customer> staffMemberBoard2 = this.openSingleStaffMemberBoard;
    int? baccountId6 = row.BAccountID;
    int num11 = 0;
    int num12 = baccountId6.GetValueOrDefault() > num11 & baccountId6.HasValue ? 1 : 0;
    ((PXAction) staffMemberBoard2).SetEnabled(num12 != 0);
    this.EnableDisableCustomerBilling(cache, row, extension);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.AR.Customer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.AR.Customer> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer row = e.Row;
    if (this.doCopyBillingSettings)
      return;
    this.SetBillingCycleFromCustomerClass(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.AR.Customer>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.AR.Customer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AR.Customer> e)
  {
    if (e.Row == null || !((PXGraph) this.Base).IsCopyPasteContext)
      return;
    foreach (PXResult<FSCustomerBillingSetup> pxResult in ((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Select(Array.Empty<object>()))
      ((PXSelectBase<FSCustomerBillingSetup>) this.CustomerBillingCycles).Delete(PXResult<FSCustomerBillingSetup>.op_Implicit(pxResult));
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.AR.Customer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.Customer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AR.Customer> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AR.Customer>>) e).Cache;
    FSxCustomer extension = cache.GetExtension<FSxCustomer>((object) row);
    if (e.Operation != 2 || this.doCopyBillingSettings)
      return;
    this.InsertUpdateCustomerBillingSetup(cache, row, extension);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AR.Customer> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.Customer row = e.Row;
    FSxCustomer extension = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.AR.Customer>>) e).Cache.GetExtension<FSxCustomer>((object) row);
    if (e.Operation != 1 || e.TranStatus != 1 || !extension.IsExtendingToCustomer.GetValueOrDefault())
      return;
    this.UpdateBillCustomerInfoInDocsExtendCustomer((PXGraph) this.Base, row.BAccountID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSCustomerBillingSetup, FSCustomerBillingSetup.frequencyType> e)
  {
    if (e.Row == null)
      return;
    this.ResetTimeCycleOptions(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSCustomerBillingSetup, FSCustomerBillingSetup.billingCycleID> e)
  {
    if (e.Row == null)
      return;
    this.ResetSendInvoicesToFromBillingCycle((PX.Objects.AR.Customer) null, e.Row);
    if (e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSCustomerBillingSetup, FSCustomerBillingSetup.billingCycleID>, FSCustomerBillingSetup, object>) e).OldValue)
      return;
    PXUIFieldAttribute.SetWarning<FSxCustomer.billingCycleID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSCustomerBillingSetup, FSCustomerBillingSetup.billingCycleID>>) e).Cache, (object) e.Row, "Note that at the time the billing process is performed, the system uses the billing cycle specified in this box. If you change the billing cycle, the system will use the newly specified billing cycle to process the customer's service orders and appointments that have not been billed yet, and to process any service orders and appointments whose billing documents will be corrected and for which billing will run again.");
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSCustomerBillingSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSCustomerBillingSetup> e)
  {
    if (e.Row == null)
      return;
    FSCustomerBillingSetup row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSCustomerBillingSetup>>) e).Cache;
    bool flag1 = !string.IsNullOrEmpty(row.SrvOrdType);
    bool flag2 = !flag1 || cache.GetStatus((object) row) == 2;
    PXUIFieldAttribute.SetEnabled<FSCustomerBillingSetup.srvOrdType>(cache, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<FSCustomerBillingSetup.billingCycleID>(cache, (object) row, flag1);
    if (row.BillingCycleID.HasValue)
    {
      FSBillingCycle fsBillingCycleRow = FSBillingCycle.PK.Find((PXGraph) this.Base, row.BillingCycleID);
      PXUIFieldAttribute.SetEnabled<FSCustomerBillingSetup.frequencyType>(cache, (object) row, fsBillingCycleRow.BillingCycleType != "TC");
      bool flag3 = SharedFunctions.IsNotAllowedBillingOptionsModification(fsBillingCycleRow);
      PXUIFieldAttribute.SetEnabled<FSCustomerBillingSetup.sendInvoicesTo>(cache, (object) row, !flag3);
      PXUIFieldAttribute.SetEnabled<FSCustomerBillingSetup.billShipmentSource>(cache, (object) row, !flag3);
    }
    else
      PXUIFieldAttribute.SetEnabled<FSCustomerBillingSetup.frequencyType>(cache, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSCustomerBillingSetup> e)
  {
    if (e.Row == null)
      return;
    this.VerifyPrepaidContractRelated(((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<FSCustomerBillingSetup>>) e).Cache, e.Row);
  }

  public class WorkflowChanges : PXGraphExtension<CustomerMaint_Workflow, CustomerMaint>
  {
    public static bool IsActive() => SM_CustomerMaint.IsActive();

    public virtual void Configure(PXScreenConfiguration config)
    {
      SM_CustomerMaint.WorkflowChanges.Configure(config.GetScreenConfigurationContext<CustomerMaint, PX.Objects.AR.Customer>());
    }

    protected static void Configure(WorkflowContext<CustomerMaint, PX.Objects.AR.Customer> context)
    {
      BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionCategory.IConfigured servicesCategory = context.Categories.Get("ServicesID");
      var conditions = new
      {
        IsOpenMultipleStaffMemberBoardDisabled = Bql<BqlOperand<PX.Objects.AR.Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime>>(),
        IsOpenSingleStaffMemberBoardDisabled = Bql<BqlOperand<PX.Objects.AR.Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime>>()
      }.AutoNameConditions();
      context.UpdateScreenConfigurationFor((Func<BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ScreenConfiguration.ConfiguratorScreen>) (config => config.WithActions((Action<BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.ContainerAdjusterActions>) (actions =>
      {
        actions.Add<SM_CustomerMaint>((Expression<Func<SM_CustomerMaint, PXAction<PX.Objects.AR.Customer>>>) (g => g.openMultipleStaffMemberBoard), (Func<BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured) a.WithCategory(servicesCategory).IsDisabledWhen((BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ISharedCondition) conditions.IsOpenMultipleStaffMemberBoardDisabled)));
        actions.Add<SM_CustomerMaint>((Expression<Func<SM_CustomerMaint, PXAction<PX.Objects.AR.Customer>>>) (g => g.openSingleStaffMemberBoard), (Func<BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured) a.WithCategory(servicesCategory, "openMultipleStaffMemberBoard").IsDisabledWhen((BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ISharedCondition) conditions.IsOpenSingleStaffMemberBoardDisabled)));
        actions.Add<SM_CustomerMaint>((Expression<Func<SM_CustomerMaint, PXAction<PX.Objects.AR.Customer>>>) (g => g.viewServiceOrderHistory), (Func<BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
        actions.Add<SM_CustomerMaint>((Expression<Func<SM_CustomerMaint, PXAction<PX.Objects.AR.Customer>>>) (g => g.viewAppointmentHistory), (Func<BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
        actions.Add<SM_CustomerMaint>((Expression<Func<SM_CustomerMaint, PXAction<PX.Objects.AR.Customer>>>) (g => g.viewEquipmentSummary), (Func<BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
        actions.Add<SM_CustomerMaint>((Expression<Func<SM_CustomerMaint, PXAction<PX.Objects.AR.Customer>>>) (g => g.viewContractScheduleSummary), (Func<BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      }))));

      BoundedTo<CustomerMaint, PX.Objects.AR.Customer>.Condition Bql<T>() where T : IBqlUnary, new()
      {
        return context.Conditions.FromBql<T>();
      }
    }
  }
}
