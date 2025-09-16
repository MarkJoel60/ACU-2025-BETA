// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_CustomerClassMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class SM_CustomerClassMaint : PXGraphExtension<CustomerClassMaint>
{
  public PXSelectJoin<FSCustomerClassBillingSetup, CrossJoin<FSSetup>, Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerClassBillingSetup.customerClassID, Equal<Current<PX.Objects.AR.CustomerClass.customerClassID>>, And<FSCustomerClassBillingSetup.srvOrdType, IsNotNull>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerClassBillingSetup.customerClassID, Equal<Current<PX.Objects.AR.CustomerClass.customerClassID>>, And<FSCustomerClassBillingSetup.srvOrdType, IsNull>>>>>> BillingCycles;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual bool IsThisLineValid(
    FSCustomerClassBillingSetup fsCustomerClassBillingSetupRow_Current)
  {
    int num = 0;
    foreach (PXResult<FSCustomerClassBillingSetup> pxResult in ((PXSelectBase<FSCustomerClassBillingSetup>) this.BillingCycles).Select(Array.Empty<object>()))
    {
      FSCustomerClassBillingSetup classBillingSetup = PXResult<FSCustomerClassBillingSetup>.op_Implicit(pxResult);
      if (fsCustomerClassBillingSetupRow_Current.SrvOrdType != null && fsCustomerClassBillingSetupRow_Current.SrvOrdType.Equals(classBillingSetup.SrvOrdType))
        ++num;
    }
    return num <= 1;
  }

  public virtual void DisplayBillingOptions(
    PXCache cache,
    PX.Objects.AR.CustomerClass customerClassRow,
    FSxCustomerClass fsxCustomerClassRow)
  {
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    bool flag1 = fsSetup != null && fsSetup.CustomerMultipleBillingOptions.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<FSxCustomerClass.defaultBillingCycleID>(cache, (object) customerClassRow, !flag1);
    PXUIFieldAttribute.SetVisible<FSxCustomerClass.sendInvoicesTo>(cache, (object) customerClassRow, !flag1);
    PXUIFieldAttribute.SetVisible<FSxCustomerClass.billShipmentSource>(cache, (object) customerClassRow, !flag1);
    ((PXSelectBase) this.BillingCycles).AllowSelect = flag1;
    if (fsxCustomerClassRow == null)
      return;
    bool flag2 = SharedFunctions.IsNotAllowedBillingOptionsModification(FSBillingCycle.PK.Find((PXGraph) this.Base, fsxCustomerClassRow.DefaultBillingCycleID));
    PXUIFieldAttribute.SetEnabled<FSxCustomerClass.sendInvoicesTo>(cache, (object) customerClassRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSxCustomerClass.billShipmentSource>(cache, (object) customerClassRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSxCustomerClass.defaultBillingCycleID>(cache, (object) customerClassRow);
    if (fsxCustomerClassRow.DefaultBillingCycleID.HasValue && !flag2)
    {
      PXDefaultAttribute.SetPersistingCheck<FSxCustomerClass.sendInvoicesTo>(cache, (object) customerClassRow, (PXPersistingCheck) 1);
      PXDefaultAttribute.SetPersistingCheck<FSxCustomerClass.billShipmentSource>(cache, (object) customerClassRow, (PXPersistingCheck) 1);
    }
    else
    {
      PXDefaultAttribute.SetPersistingCheck<FSxCustomerClass.sendInvoicesTo>(cache, (object) customerClassRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<FSxCustomerClass.billShipmentSource>(cache, (object) customerClassRow, (PXPersistingCheck) 2);
    }
  }

  public virtual void ResetSendInvoicesToFromBillingCycle(
    PX.Objects.AR.CustomerClass customerClassRow,
    FSCustomerClassBillingSetup fsCustomerClassBillingSetupRow)
  {
    List<object> objectList = new List<object>();
    PXView pxView = new PXView((PXGraph) this.Base, true, (BqlCommand) new Select<FSBillingCycle, Where<FSBillingCycle.billingCycleID, Equal<Required<FSBillingCycle.billingCycleID>>>>());
    if (customerClassRow != null)
    {
      FSxCustomerClass extension = PXCache<PX.Objects.AR.CustomerClass>.GetExtension<FSxCustomerClass>(customerClassRow);
      objectList.Add((object) extension.DefaultBillingCycleID);
      FSBillingCycle fsBillingCycleRow = (FSBillingCycle) pxView.SelectSingle(objectList.ToArray());
      if (fsBillingCycleRow == null || !SharedFunctions.IsNotAllowedBillingOptionsModification(fsBillingCycleRow))
        return;
      extension.SendInvoicesTo = "BT";
      extension.BillShipmentSource = "SO";
    }
    else
    {
      if (fsCustomerClassBillingSetupRow == null)
        return;
      objectList.Add((object) fsCustomerClassBillingSetupRow.BillingCycleID);
      FSBillingCycle fsBillingCycleRow = (FSBillingCycle) pxView.SelectSingle(objectList.ToArray());
      if (fsBillingCycleRow == null || !SharedFunctions.IsNotAllowedBillingOptionsModification(fsBillingCycleRow))
        return;
      fsCustomerClassBillingSetupRow.SendInvoicesTo = "BT";
      fsCustomerClassBillingSetupRow.BillShipmentSource = "SO";
    }
  }

  public virtual void EnableDisableCustomerBilling(
    PXCache cache,
    PX.Objects.AR.CustomerClass customerClassRow,
    FSxCustomerClass fsxCustomerClassRow)
  {
    bool flag = fsxCustomerClassRow.DefaultBillingCustomerSource == "LC";
    PXUIFieldAttribute.SetVisible<FSxCustomerClass.billCustomerID>(cache, (object) customerClassRow, flag);
    PXUIFieldAttribute.SetVisible<FSxCustomerClass.billLocationID>(cache, (object) customerClassRow, flag);
    PXDefaultAttribute.SetPersistingCheck<FSxCustomerClass.billCustomerID>(cache, (object) customerClassRow, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<FSxCustomerClass.billLocationID>(cache, (object) customerClassRow, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (flag)
      return;
    fsxCustomerClassRow.BillCustomerID = new int?();
    fsxCustomerClassRow.BillLocationID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.AR.CustomerClass, FSxCustomerClass.billLocationID> e)
  {
    if (e.Row == null)
      return;
    FSxCustomerClass extension = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.AR.CustomerClass, FSxCustomerClass.billLocationID>>) e).Cache.GetExtension<FSxCustomerClass>((object) e.Row);
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
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AR.CustomerClass, FSxCustomerClass.billLocationID>, PX.Objects.AR.CustomerClass, object>) e).NewValue = (object) baccountR.DefLocationID;
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
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AR.CustomerClass, FSxCustomerClass.billLocationID>, PX.Objects.AR.CustomerClass, object>) e).NewValue = (object) location.LocationID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.CustomerClass, FSxCustomerClass.billCustomerID> e)
  {
    if (e.Row == null)
      return;
    FSxCustomerClass extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.CustomerClass, FSxCustomerClass.billCustomerID>>) e).Cache.GetExtension<FSxCustomerClass>((object) e.Row);
    int? oldValue = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.CustomerClass, FSxCustomerClass.billCustomerID>, PX.Objects.AR.CustomerClass, object>) e).OldValue;
    int? billCustomerId = extension.BillCustomerID;
    if (oldValue.GetValueOrDefault() == billCustomerId.GetValueOrDefault() & oldValue.HasValue == billCustomerId.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.CustomerClass, FSxCustomerClass.billCustomerID>>) e).Cache.SetDefaultExt<FSxCustomerClass.billLocationID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.CustomerClass> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.CustomerClass row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.CustomerClass>>) e).Cache;
    FSxCustomerClass extension = cache.GetExtension<FSxCustomerClass>((object) row);
    this.DisplayBillingOptions(cache, row, extension);
    this.EnableDisableCustomerBilling(cache, row, extension);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AR.CustomerClass> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSCustomerClassBillingSetup, FSCustomerClassBillingSetup.billingCycleID> e)
  {
    if (e.Row == null)
      return;
    this.ResetSendInvoicesToFromBillingCycle((PX.Objects.AR.CustomerClass) null, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSCustomerClassBillingSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSCustomerClassBillingSetup> e)
  {
    if (e.Row == null)
      return;
    FSCustomerClassBillingSetup row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSCustomerClassBillingSetup>>) e).Cache;
    if (!this.IsThisLineValid(row))
    {
      PXUIFieldAttribute.SetEnabled<FSCustomerClassBillingSetup.billingCycleID>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<FSCustomerClassBillingSetup.sendInvoicesTo>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<FSCustomerClassBillingSetup.billShipmentSource>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<FSCustomerClassBillingSetup.frequencyType>(cache, (object) row, false);
    }
    else
    {
      int? billingCycleId = row.BillingCycleID;
      bool flag1 = !billingCycleId.HasValue && !string.IsNullOrEmpty(row.SrvOrdType);
      PXUIFieldAttribute.SetEnabled<FSCustomerBillingSetup.billingCycleID>(cache, (object) row, flag1);
      billingCycleId = row.BillingCycleID;
      if (billingCycleId.HasValue)
      {
        FSBillingCycle fsBillingCycleRow = FSBillingCycle.PK.Find((PXGraph) this.Base, row.BillingCycleID);
        PXUIFieldAttribute.SetEnabled<FSCustomerClassBillingSetup.frequencyType>(cache, (object) row, fsBillingCycleRow.BillingCycleType != "TC");
        bool flag2 = SharedFunctions.IsNotAllowedBillingOptionsModification(fsBillingCycleRow);
        PXUIFieldAttribute.SetEnabled<FSCustomerClassBillingSetup.sendInvoicesTo>(cache, (object) row, !flag2);
        PXUIFieldAttribute.SetEnabled<FSCustomerClassBillingSetup.billShipmentSource>(cache, (object) row, !flag2);
      }
      else
        PXUIFieldAttribute.SetEnabled<FSCustomerClassBillingSetup.frequencyType>(cache, (object) row, false);
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSCustomerClassBillingSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSCustomerClassBillingSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSCustomerClassBillingSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSCustomerClassBillingSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSCustomerClassBillingSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSCustomerClassBillingSetup> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<FSCustomerClassBillingSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSCustomerClassBillingSetup> e)
  {
  }
}
