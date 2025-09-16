// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.BillingCycleMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class BillingCycleMaint : PXGraph<BillingCycleMaint, FSBillingCycle>
{
  public PXSelect<FSBillingCycle> BillingCycleRecords;
  [PXHidden]
  public PXSetup<FSSetup> Setup;

  [PXMergeAttributes]
  [PXSelector(typeof (FSBillingCycle.billingCycleCD))]
  protected virtual void _(
    Events.CacheAttached<FSBillingCycle.billingCycleCD> e)
  {
  }

  /// <summary>
  /// Show/Hide fields and make them Required/Not Required depending on the Billing Cycle Type selected.
  /// </summary>
  /// <param name="cache">BillingCycleRecords cache.</param>
  /// <param name="fsBillingCycleRow">FSBillingCycle row.</param>
  public virtual void BillingCycleTypeFieldsSetup(PXCache cache, FSBillingCycle fsBillingCycleRow)
  {
    switch (fsBillingCycleRow.BillingCycleType)
    {
      case "AP":
      case "SO":
        PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleType>(cache, (object) fsBillingCycleRow, false);
        PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleWeekDay>(cache, (object) fsBillingCycleRow, false);
        PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleDayOfMonth>(cache, (object) fsBillingCycleRow, false);
        PXUIFieldAttribute.SetEnabled<FSBillingCycle.groupBillByLocations>(cache, (object) fsBillingCycleRow, false);
        PXDefaultAttribute.SetPersistingCheck<FSBillingCycle.timeCycleDayOfMonth>(cache, (object) fsBillingCycleRow, (PXPersistingCheck) 2);
        break;
      case "PO":
      case "WO":
        PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleType>(cache, (object) fsBillingCycleRow, false);
        PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleWeekDay>(cache, (object) fsBillingCycleRow, false);
        PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleDayOfMonth>(cache, (object) fsBillingCycleRow, false);
        PXUIFieldAttribute.SetEnabled<FSBillingCycle.groupBillByLocations>(cache, (object) fsBillingCycleRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSBillingCycle.timeCycleDayOfMonth>(cache, (object) fsBillingCycleRow, (PXPersistingCheck) 2);
        break;
      case "TC":
        PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleType>(cache, (object) fsBillingCycleRow, true);
        PXUIFieldAttribute.SetEnabled<FSBillingCycle.groupBillByLocations>(cache, (object) fsBillingCycleRow, true);
        switch (fsBillingCycleRow.TimeCycleType)
        {
          case "WK":
            PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleDayOfMonth>(cache, (object) fsBillingCycleRow, false);
            PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleWeekDay>(cache, (object) fsBillingCycleRow, true);
            PXDefaultAttribute.SetPersistingCheck<FSBillingCycle.timeCycleWeekDay>(cache, (object) fsBillingCycleRow, (PXPersistingCheck) 1);
            PXDefaultAttribute.SetPersistingCheck<FSBillingCycle.timeCycleDayOfMonth>(cache, (object) fsBillingCycleRow, (PXPersistingCheck) 2);
            return;
          case "MT":
            PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleWeekDay>(cache, (object) fsBillingCycleRow, false);
            PXUIFieldAttribute.SetVisible<FSBillingCycle.timeCycleDayOfMonth>(cache, (object) fsBillingCycleRow, true);
            PXDefaultAttribute.SetPersistingCheck<FSBillingCycle.timeCycleDayOfMonth>(cache, (object) fsBillingCycleRow, (PXPersistingCheck) 1);
            PXDefaultAttribute.SetPersistingCheck<FSBillingCycle.timeCycleWeekDay>(cache, (object) fsBillingCycleRow, (PXPersistingCheck) 2);
            return;
          default:
            return;
        }
    }
  }

  /// <summary>
  /// Resets the values of the Time Cycle options depending on the Billing and Time Cycle Types.
  /// </summary>
  /// <param name="fsBillingCycleRow">FSBillingCycle row.</param>
  public virtual void ResetTimeCycleOptions(FSBillingCycle fsBillingCycleRow)
  {
    if (fsBillingCycleRow.BillingCycleType != "TC")
    {
      fsBillingCycleRow.TimeCycleWeekDay = new int?();
      fsBillingCycleRow.TimeCycleDayOfMonth = new int?();
    }
    else
    {
      switch (fsBillingCycleRow.TimeCycleType)
      {
        case "MT":
          fsBillingCycleRow.TimeCycleWeekDay = new int?();
          break;
        case "WK":
          fsBillingCycleRow.TimeCycleDayOfMonth = new int?();
          break;
      }
    }
  }

  public virtual void VerifyPrepaidContractRelated(PXCache cache, FSBillingCycle fsBillingCycleRow)
  {
    if (fsBillingCycleRow.BillingBy == (string) cache.GetValueOriginal<FSBillingCycle.billingBy>((object) fsBillingCycleRow) || ((PXSelectBase<FSSetup>) this.Setup).Current == null)
      return;
    string valueOriginal = (string) cache.GetValueOriginal<FSBillingCycle.billingBy>((object) fsBillingCycleRow);
    List<object> objectList = new List<object>();
    BqlCommand bqlCommand = (BqlCommand) null;
    string str = "Service Orders";
    switch (valueOriginal)
    {
      case "SO":
        bqlCommand = (BqlCommand) new Select2<FSCustomerBillingSetup, CrossJoin<FSSetup, InnerJoin<FSServiceOrder, On<FSCustomerBillingSetup.customerID, Equal<FSServiceOrder.billCustomerID>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<FSServiceOrder.srvOrdType>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSServiceOrder.billServiceContractID>>, InnerJoin<FSContractPeriod, On<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPeriod.contractPeriodID, Equal<FSServiceOrder.billContractPeriodID>>>>>>>, Where<FSCustomerBillingSetup.billingCycleID, Equal<Required<FSCustomerBillingSetup.billingCycleID>>, And<FSServiceOrder.canceled, Equal<False>, And<FSServiceContract.status, NotEqual<ListField_Status_ServiceContract.Canceled>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>>>();
        break;
      case "AP":
        bqlCommand = (BqlCommand) new Select2<FSCustomerBillingSetup, CrossJoin<FSSetup, InnerJoin<FSServiceOrder, On<FSCustomerBillingSetup.customerID, Equal<FSServiceOrder.billCustomerID>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<FSServiceOrder.srvOrdType>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>, InnerJoin<FSAppointment, On<FSAppointment.sOID, Equal<FSServiceOrder.sOID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSAppointment.billServiceContractID>>, InnerJoin<FSContractPeriod, On<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPeriod.contractPeriodID, Equal<FSAppointment.billContractPeriodID>>>>>>>>, Where<FSCustomerBillingSetup.billingCycleID, Equal<Required<FSCustomerBillingSetup.billingCycleID>>, And<FSAppointment.canceled, Equal<False>, And<FSServiceContract.status, NotEqual<ListField_Status_ServiceContract.Canceled>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>>>();
        str = "Appointments";
        break;
    }
    objectList.Add((object) fsBillingCycleRow.BillingCycleID);
    if (new PXView((PXGraph) this, true, bqlCommand).SelectSingle(objectList.ToArray()) != null)
    {
      PXException pxException = (PXException) new PXSetPropertyException("The billing cycle cannot be modified because it has been assigned to at least one customer whose {0} are related to prepaid contracts.", (PXErrorLevel) 4, new object[1]
      {
        (object) str
      });
      cache.RaiseExceptionHandling<FSBillingCycle.billingCycleCD>((object) fsBillingCycleRow, (object) fsBillingCycleRow.BillingCycleCD, (Exception) pxException);
      throw pxException;
    }
  }

  protected virtual void _(
    Events.FieldUpdated<FSBillingCycle, FSBillingCycle.billingCycleType> e)
  {
    if (e.Row == null)
      return;
    FSBillingCycle row = e.Row;
    if (row.BillingCycleType == "AP" || row.BillingCycleType == "SO")
      row.GroupBillByLocations = new bool?(false);
    this.ResetTimeCycleOptions(row);
  }

  protected virtual void _(
    Events.FieldUpdated<FSBillingCycle, FSBillingCycle.timeCycleType> e)
  {
    if (e.Row == null)
      return;
    this.ResetTimeCycleOptions(e.Row);
  }

  protected virtual void _(Events.RowSelecting<FSBillingCycle> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSBillingCycle> e)
  {
    if (e.Row == null)
      return;
    this.BillingCycleTypeFieldsSetup(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSBillingCycle>>) e).Cache, e.Row);
    PXUIFieldAttribute.SetVisible<FSBillingCycle.groupBillByLocations>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSBillingCycle>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.accountLocations>());
  }

  protected virtual void _(Events.RowInserting<FSBillingCycle> e)
  {
  }

  protected virtual void _(Events.RowInserted<FSBillingCycle> e)
  {
  }

  protected virtual void _(Events.RowUpdating<FSBillingCycle> e)
  {
  }

  protected virtual void _(Events.RowUpdated<FSBillingCycle> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSBillingCycle> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSBillingCycle> e)
  {
  }

  protected virtual void _(Events.RowPersisting<FSBillingCycle> e)
  {
    if (e.Row == null)
      return;
    FSBillingCycle row = e.Row;
    if (row.BillingBy == "SO" && row.BillingCycleType == "AP")
      throw new PXException("The record cannot be saved. A billing cycle billed by service orders cannot be grouped by appointments. Select another invoice grouping option.");
    if (e.Operation == 3)
    {
      int? rowCount = PXSelectBase<FSCustomerBillingSetup, PXSelectJoinGroupBy<FSCustomerBillingSetup, CrossJoin<FSSetup>, Where<FSCustomerBillingSetup.billingCycleID, Equal<Required<FSCustomerBillingSetup.billingCycleID>>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, IsNotNull>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.BillingCycleID
      }).RowCount;
      int num = 0;
      if (rowCount.GetValueOrDefault() > num & rowCount.HasValue)
        throw new PXException("The billing cycle cannot be deleted because it is assigned to at least one customer.", new object[1]
        {
          (object) row
        });
    }
    if (e.Operation != 1)
      return;
    this.VerifyPrepaidContractRelated(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<FSBillingCycle>>) e).Cache, row);
  }

  protected class BillingCycleIDAndDate
  {
    public int? BillingCycleID;
    public DateTime? DocDate;
  }
}
