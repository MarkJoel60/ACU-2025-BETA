// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_SOOrderEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.SO.Workflow.SalesOrder;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class SM_SOOrderEntry : FSPostingBase<SOOrderEntry>, IInvoiceContractGraph
{
  [PXCopyPasteHiddenView]
  public PXFilter<FSCreateServiceOrderFilter> CreateServiceOrderFilter;
  [PXHidden]
  public PXSelect<FSSetup> SetupRecord;
  public PXAction<PX.Objects.SO.SOOrder> OpenAppointmentBoard;
  public PXAction<PX.Objects.SO.SOOrder> CreateServiceOrder;
  public PXAction<PX.Objects.SO.SOOrder> ViewServiceOrder;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltSOSrvOrdType>>))]
  [PXMergeAttributes]
  protected virtual void FSCreateServiceOrderFilter_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  public virtual bool IsFSIntegrationEnabled()
  {
    bool flag = true;
    if (((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current == null)
    {
      flag = false;
    }
    else
    {
      FSxSOOrderType extension = ((PXSelectBase) this.Base.soordertype).Cache.GetExtension<FSxSOOrderType>((object) ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current);
      if (extension == null || !extension.EnableFSIntegration.GetValueOrDefault())
        flag = false;
    }
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current != null)
    {
      FSxSOOrder extension = ((PXSelectBase) this.Base.Document).Cache.GetExtension<FSxSOOrder>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
      if (extension != null)
        extension.IsFSIntegrated = new bool?(flag);
    }
    return flag;
  }

  [PXOverride]
  public virtual void InvoiceOrder(
    Dictionary<string, object> parameters,
    IEnumerable<PX.Objects.SO.SOOrder> list,
    InvoiceList created,
    bool isMassProcess,
    PXQuickProcess.ActionFlow quickProcessFlow,
    bool groupByCustomerOrderNumber,
    SM_SOOrderEntry.InvoiceOrderDelegate del)
  {
    foreach (PX.Objects.SO.SOOrder soOrder in list)
      this.ValidatePostBatchStatus((PXDBOperation) 1, "SO", soOrder.OrderType, soOrder.OrderNbr);
    del(parameters, list, created, isMassProcess, quickProcessFlow, groupByCustomerOrderNumber);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual void openAppointmentBoard()
  {
    if (this.Base.Document == null || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null)
      return;
    if (((PXGraph) this.Base).IsDirty)
      ((PXAction) this.Base.Save).Press();
    FSxSOOrder extension = ((PXSelectBase) this.Base.Document).Cache.GetExtension<FSxSOOrder>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
    if (extension == null || extension.ServiceOrderRefNbr == null)
      return;
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    FSServiceOrder serviceOrderRecord = this.GetServiceOrderRecord(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
    if (serviceOrderRecord == null)
      return;
    ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) serviceOrderRecord.RefNbr, new object[1]
    {
      (object) serviceOrderRecord.SrvOrdType
    }));
    instance.OpenEmployeeBoard();
  }

  [PXOverride]
  public virtual IEnumerable CopyOrder(PXAdapter adapter)
  {
    if (this.Base.Document == null || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null)
      return adapter.Get();
    List<PX.Objects.SO.SOOrder> soOrderList = new List<PX.Objects.SO.SOOrder>();
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    FSxSOOrder extension = PXCache<PX.Objects.SO.SOOrder>.GetExtension<FSxSOOrder>(current);
    if (extension != null && extension.SDEnabled.GetValueOrDefault())
      extension.ServiceOrderRefNbr = (string) null;
    soOrderList.Add(current);
    return (IEnumerable) soOrderList;
  }

  [PXButton]
  [PXUIField]
  public virtual void createServiceOrder()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SM_SOOrderEntry.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new SM_SOOrderEntry.\u003C\u003Ec__DisplayClass12_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.\u003C\u003E4__this = this;
    if (this.Base.Document == null || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null)
      return;
    if (((PXGraph) this.Base).IsDirty)
      ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.soOrderRow = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.fsxSOOrderRow = ((PXSelectBase) this.Base.Document).Cache.GetExtension<FSxSOOrder>((object) cDisplayClass120.soOrderRow);
    if (((PXSelectBase<FSCreateServiceOrderFilter>) this.CreateServiceOrderFilter).AskExt() != 1)
      return;
    if (((PXSelectBase<FSCreateServiceOrderFilter>) this.CreateServiceOrderFilter).Current.SrvOrdType == null)
    {
      ((PXSelectBase) this.CreateServiceOrderFilter).Cache.RaiseExceptionHandling<FSCreateServiceOrderFilter.srvOrdType>((object) ((PXSelectBase<FSCreateServiceOrderFilter>) this.CreateServiceOrderFilter).Current, (object) null, (Exception) new PXSetPropertyException("This element cannot be empty.", (PXErrorLevel) 4));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.fsxSOOrderRow.SDEnabled = new bool?(true);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.fsxSOOrderRow.AssignedEmpID = ((PXSelectBase<FSCreateServiceOrderFilter>) this.CreateServiceOrderFilter).Current.AssignedEmpID;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.fsxSOOrderRow.SrvOrdType = ((PXSelectBase<FSCreateServiceOrderFilter>) this.CreateServiceOrderFilter).Current.SrvOrdType;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.fsxSOOrderRow.SLAETA = ((PXSelectBase<FSCreateServiceOrderFilter>) this.CreateServiceOrderFilter).Current.SLAETA;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass120, __methodptr(\u003CcreateServiceOrder\u003Eb__0)));
    }
  }

  [PXButton]
  [PXUIField]
  public virtual void viewServiceOrder()
  {
    if (this.Base.Document == null || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null)
      return;
    if (((PXGraph) this.Base).IsDirty)
      ((PXAction) this.Base.Save).Press();
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    FSxSOOrder extension = ((PXSelectBase) this.Base.Document).Cache.GetExtension<FSxSOOrder>((object) current);
    if (extension == null || !extension.SDEnabled.GetValueOrDefault() || extension.SrvOrdType == null || extension.ServiceOrderRefNbr == null)
      return;
    FSServiceOrder serviceOrderRecord = this.GetServiceOrderRecord(current);
    CRExtensionHelper.LaunchServiceOrderScreen((PXGraph) this.Base, serviceOrderRecord.RefNbr, serviceOrderRecord.SrvOrdType);
  }

  [PXOverride]
  public virtual System.Type[] GetAlternativeKeyFields(Func<System.Type[]> baseMethod)
  {
    return new List<System.Type>((IEnumerable<System.Type>) baseMethod())
    {
      typeof (FSxSOLine.componentID),
      typeof (FSxSOLine.equipmentComponentLineNbr),
      typeof (FSxSOLine.srvOrdType),
      typeof (FSxSOLine.appointmentRefNbr),
      typeof (FSxSOLine.serviceOrderRefNbr)
    }.ToArray();
  }

  public virtual bool CanCreateServiceOrder(
    PXCache cache,
    PX.Objects.SO.SOOrder soOrderRow,
    FSxSOOrder fsxSOOrderRow)
  {
    if (soOrderRow == null || string.IsNullOrEmpty(soOrderRow.OrderType))
      return false;
    return PXResultset<FSPostInfo>.op_Implicit(PXSelectBase<FSPostInfo, PXSelect<FSPostInfo, Where<FSPostInfo.sOOrderType, Equal<Required<FSPostInfo.sOOrderType>>, And<FSPostInfo.sOOrderNbr, Equal<Required<FSPostInfo.sOOrderNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) soOrderRow.OrderType,
      (object) soOrderRow.OrderNbr
    })) == null;
  }

  /// <summary>
  /// Returns the ServiceOrder record associated to the selected Sales Order.
  /// </summary>
  public virtual FSServiceOrder GetServiceOrderRecord(PX.Objects.SO.SOOrder soOrderRow)
  {
    return PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.sourceType, Equal<ListField_SourceType_ServiceOrder.SalesOrder>, And<FSServiceOrder.sourceDocType, Equal<Required<FSServiceOrder.sourceDocType>>, And<FSServiceOrder.sourceRefNbr, Equal<Required<FSServiceOrder.sourceRefNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) soOrderRow.OrderType,
      (object) soOrderRow.OrderNbr
    }));
  }

  /// <summary>Initializes a new Service Order.</summary>
  public virtual FSServiceOrder InitNewServiceOrder(
    ServiceOrderEntry graphServiceOrder,
    PX.Objects.SO.SOOrder soOrderRow,
    FSxSOOrder fsxSOOrderRow)
  {
    FSServiceOrder fsServiceOrder = ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).Insert(new FSServiceOrder()
    {
      SrvOrdType = fsxSOOrderRow.SrvOrdType,
      SourceType = "SO",
      SourceDocType = soOrderRow.OrderType,
      SourceRefNbr = soOrderRow.OrderNbr,
      Hold = soOrderRow.Hold
    });
    this.CheckAutoNumbering(((PXSelectBase<FSSrvOrdType>) graphServiceOrder.ServiceOrderTypeSelected).SelectSingle(Array.Empty<object>()).SrvOrdNumberingID);
    return fsServiceOrder;
  }

  /// <summary>
  /// Deletes the Service Order and blanks the <c>fsxSOOrderRow.SOID</c> memory field.
  /// </summary>
  public virtual void DeleteServiceOrder(
    ServiceOrderEntry graphServiceOrder,
    FSServiceOrder fsServiceOrderRow,
    FSxSOOrder fsxSOOrderRow)
  {
    ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).Delete(fsServiceOrderRow);
    ((PXAction) graphServiceOrder.Save).Press();
    fsxSOOrderRow.ServiceOrderRefNbr = (string) null;
  }

  /// <summary>
  /// Updates the ServiceOrder information using the Sales Order definition.
  /// </summary>
  public virtual void UpdateServiceOrderHeader(
    ServiceOrderEntry graphServiceOrder,
    FSServiceOrder fsServiceOrderRow,
    PX.Objects.SO.SOOrder soOrderRow,
    FSxSOOrder fsxSOOrderRow,
    PXDBOperation operation)
  {
    bool flag = false;
    SOShippingContact source1 = PXResultset<SOShippingContact>.op_Implicit(((PXSelectBase<SOShippingContact>) this.Base.Shipping_Contact).Select(Array.Empty<object>()));
    SOShippingAddress source2 = PXResultset<SOShippingAddress>.op_Implicit(((PXSelectBase<SOShippingAddress>) this.Base.Shipping_Address).Select(Array.Empty<object>()));
    int? customerId1 = fsServiceOrderRow.CustomerID;
    int? customerId2 = soOrderRow.CustomerID;
    if (!(customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue))
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.customerID>(fsServiceOrderRow, (object) soOrderRow.CustomerID);
      flag = true;
    }
    if (fsServiceOrderRow.CuryID != soOrderRow.CuryID && operation == 2)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.curyID>(fsServiceOrderRow, (object) soOrderRow.CuryID);
      flag = true;
    }
    int? projectId = fsServiceOrderRow.ProjectID;
    int? nullable1 = soOrderRow.ProjectID;
    if (!(projectId.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectId.HasValue == nullable1.HasValue))
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.projectID>(fsServiceOrderRow, (object) soOrderRow.ProjectID);
      flag = true;
    }
    DateTime? orderDate = fsServiceOrderRow.OrderDate;
    DateTime? nullable2 = soOrderRow.RequestDate;
    if ((orderDate.HasValue == nullable2.HasValue ? (orderDate.HasValue ? (orderDate.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.orderDate>(fsServiceOrderRow, (object) soOrderRow.RequestDate);
      flag = true;
    }
    if (soOrderRow.OrderDesc != null && soOrderRow.OrderDesc != fsServiceOrderRow.DocDesc)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.docDesc>(fsServiceOrderRow, (object) soOrderRow.OrderDesc);
      flag = true;
    }
    nullable1 = fsServiceOrderRow.LocationID;
    int? nullable3 = soOrderRow.CustomerLocationID;
    if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.locationID>(fsServiceOrderRow, (object) soOrderRow.CustomerLocationID);
      flag = true;
    }
    nullable3 = ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).Current.BranchLocationID;
    if (!nullable3.HasValue)
    {
      int? branchLocation = this.GetBranchLocation(((PXGraph) this.Base).Accessinfo.BranchID);
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.branchLocationID>(fsServiceOrderRow, (object) branchLocation);
      flag = true;
    }
    FSContact dest1 = PXResultset<FSContact>.op_Implicit(((PXSelectBase<FSContact>) graphServiceOrder.ServiceOrder_Contact).Select(Array.Empty<object>()));
    FSAddress dest2 = PXResultset<FSAddress>.op_Implicit(((PXSelectBase<FSAddress>) graphServiceOrder.ServiceOrder_Address).Select(Array.Empty<object>()));
    bool? nullable4 = dest1.OverrideContact;
    bool? overrideContact = source1.OverrideContact;
    if (nullable4.GetValueOrDefault() == overrideContact.GetValueOrDefault() & nullable4.HasValue == overrideContact.HasValue)
    {
      bool? overrideAddress = dest2.OverrideAddress;
      nullable4 = source2.OverrideAddress;
      if (overrideAddress.GetValueOrDefault() == nullable4.GetValueOrDefault() & overrideAddress.HasValue == nullable4.HasValue)
        goto label_17;
    }
    Guid? noteId = (Guid?) dest1?.NoteID;
    InvoiceHelper.CopyContact((IContact) dest1, (IContact) source1);
    dest1.NoteID = noteId ?? source1.NoteID;
    ((PXSelectBase<FSContact>) graphServiceOrder.ServiceOrder_Contact).Update(dest1);
    InvoiceHelper.CopyAddress((IAddress) dest2, (IAddress) source2);
    ((PXSelectBase<FSAddress>) graphServiceOrder.ServiceOrder_Address).Update(dest2);
    flag = true;
label_17:
    nullable3 = fsServiceOrderRow.AssignedEmpID;
    nullable1 = fsxSOOrderRow.AssignedEmpID;
    if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.assignedEmpID>(fsServiceOrderRow, (object) fsxSOOrderRow.AssignedEmpID);
      flag = true;
    }
    nullable2 = fsServiceOrderRow.SLAETA;
    DateTime? slaeta = fsxSOOrderRow.SLAETA;
    if ((nullable2.HasValue == slaeta.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != slaeta.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.sLAETA>(fsServiceOrderRow, (object) fsxSOOrderRow.SLAETA);
      flag = true;
    }
    if (fsServiceOrderRow.CustPORefNbr != soOrderRow.CustomerOrderNbr)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.custPORefNbr>(fsServiceOrderRow, (object) soOrderRow.CustomerOrderNbr);
      flag = true;
    }
    if (fsServiceOrderRow.CustWorkOrderRefNbr != soOrderRow.CustomerRefNbr)
    {
      ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).SetValueExt<FSServiceOrder.custWorkOrderRefNbr>(fsServiceOrderRow, (object) soOrderRow.CustomerRefNbr);
      flag = true;
    }
    UDFHelper.CopyAttributes((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) graphServiceOrder), (object) soOrderRow, ((PXSelectBase) graphServiceOrder.ServiceOrderRecords).Cache, (object) fsServiceOrderRow, (string) null);
    if (operation == 1 & flag)
      ((PXSelectBase) graphServiceOrder.ServiceOrderRecords).Cache.SetStatus((object) ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).Current, (PXEntryStatus) 1);
    if (fsxSOOrderRow != null && fsxSOOrderRow.ServiceOrderRefNbr != null)
      return;
    PXCache cache1 = ((PXSelectBase) this.Base.Document).Cache;
    PXCache cache2 = ((PXSelectBase) graphServiceOrder.ServiceOrderRecords).Cache;
    PX.Objects.SO.SOOrder srcObj = soOrderRow;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) graphServiceOrder.ServiceOrderRecords).Current;
    PXSetup<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>>> soordertype1 = this.Base.soordertype;
    bool? copyNotes;
    if (soordertype1 == null)
    {
      nullable4 = new bool?();
      copyNotes = nullable4;
    }
    else
      copyNotes = ((PXSelectBase<PX.Objects.SO.SOOrderType>) soordertype1).Current.CopyNotes;
    PXSetup<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>>> soordertype2 = this.Base.soordertype;
    bool? copyFiles;
    if (soordertype2 == null)
    {
      nullable4 = new bool?();
      copyFiles = nullable4;
    }
    else
      copyFiles = ((PXSelectBase<PX.Objects.SO.SOOrderType>) soordertype2).Current.CopyFiles;
    SharedFunctions.CopyNotesAndFiles(cache1, cache2, (object) srcObj, (object) current, copyNotes, copyFiles);
  }

  public virtual int? GetBranchLocation(int? branchID)
  {
    int? nullable = new int?();
    FSBranchLocation fsBranchLocation = PXResultset<FSBranchLocation>.op_Implicit(PXSelectBase<FSBranchLocation, PXSelect<FSBranchLocation, Where<FSBranchLocation.branchID, Equal<Required<FSBranchLocation.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchID
    }));
    if (fsBranchLocation != null)
      nullable = fsBranchLocation.BranchLocationID;
    return nullable.HasValue ? nullable : throw new PXException("The service order cannot be created. There are no available branch locations in the current branch. Select another branch or create a branch location for the current branch on the Branch Locations (FS202500) form.");
  }

  /// <summary>
  /// Adjusts the Header after removing, updating or creating the Sales Order.
  /// </summary>
  public virtual FSServiceOrder InsertUpdateDeleteServiceOrderDocument(
    PX.Objects.SO.SOOrder soOrderRow,
    FSxSOOrder fsxSOOrderRow,
    PXDBOperation salesOrderOperation)
  {
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    FSServiceOrder serviceOrderRecord = this.GetServiceOrderRecord(soOrderRow);
    PXDBOperation operation = (PXDBOperation) 2;
    if (serviceOrderRecord != null)
    {
      ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) serviceOrderRecord.RefNbr, new object[1]
      {
        (object) serviceOrderRecord.SrvOrdType
      }));
      if (salesOrderOperation != 3 && salesOrderOperation != 3)
      {
        bool? sdEnabled = fsxSOOrderRow.SDEnabled;
        bool flag = false;
        if (!(sdEnabled.GetValueOrDefault() == flag & sdEnabled.HasValue))
        {
          operation = (PXDBOperation) 1;
          ((PXGraph) instance).SelectTimeStamp();
          goto label_8;
        }
      }
      this.DeleteServiceOrder(instance, ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current, fsxSOOrderRow);
      return (FSServiceOrder) null;
    }
    bool? sdEnabled1 = fsxSOOrderRow.SDEnabled;
    bool flag1 = false;
    if (sdEnabled1.GetValueOrDefault() == flag1 & sdEnabled1.HasValue)
      return (FSServiceOrder) null;
    ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = this.InitNewServiceOrder(instance, soOrderRow, fsxSOOrderRow);
label_8:
    this.UpdateServiceOrderHeader(instance, ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current, soOrderRow, fsxSOOrderRow, operation);
    this.InsertUpdateDeleteServiceOrderDetail(instance);
    if (((PXGraph) instance).IsDirty || ((PXSelectBase) instance.ServiceOrderRecords).Cache.GetStatus((object) ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current) == 1)
    {
      if (!((PXGraph) this.Base).IsContractBasedAPI && ((PXSelectBase) instance.ServiceOrderRecords).Cache.GetStatus((object) ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current) == 2)
        throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXAction) instance.Save).Press();
    }
    return ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current;
  }

  public virtual void InsertUpdateDeleteFSSODet<SODetType>(
    PXCache cacheFSSODet,
    PX.Objects.SO.SOLine soLineRow,
    FSxSOLine fsxSOLineRow,
    FSSODet fsSODetRow,
    string lineType)
    where SODetType : FSSODet, IBqlTable, new()
  {
    if (fsSODetRow != null)
    {
      bool? sdSelected = fsxSOLineRow.SDSelected;
      bool flag = false;
      if (sdSelected.GetValueOrDefault() == flag & sdSelected.HasValue)
      {
        cacheFSSODet.Delete((object) fsSODetRow);
      }
      else
      {
        cacheFSSODet.Current = (object) fsSODetRow;
        fsSODetRow = (FSSODet) cacheFSSODet.CreateCopy((object) fsSODetRow);
        this.InsertUpdateServiceOrderLine<SODetType>(cacheFSSODet, fsSODetRow, false, lineType, soLineRow, fsxSOLineRow);
      }
    }
    else
    {
      bool? sdSelected = fsxSOLineRow.SDSelected;
      bool flag = false;
      if (sdSelected.GetValueOrDefault() == flag & sdSelected.HasValue)
        return;
      this.InsertUpdateServiceOrderLine<SODetType>(cacheFSSODet, fsSODetRow, true, lineType, soLineRow, fsxSOLineRow);
    }
  }

  /// <summary>Enable or Disable Sales Order lines fields.</summary>
  /// <param name="cache">Sales Order line cache.</param>
  /// <param name="soLineRow">Sales Order line row.</param>
  public virtual void EnableDisableSOline(
    PXCache cache,
    PX.Objects.SO.SOLine soLineRow,
    bool fsIntegrationEnabled)
  {
    PXUIFieldAttribute.SetEnabled<FSxSOLine.sDSelected>(cache, (object) soLineRow, fsIntegrationEnabled);
    PXUIFieldAttribute.SetEnabled<FSxSOLine.equipmentAction>(cache, (object) soLineRow, soLineRow.InventoryID.HasValue & fsIntegrationEnabled);
  }

  public virtual void DeleteFSSODetLinesFromDeletedSOlines(ServiceOrderEntry graphServiceOrder)
  {
    foreach (PX.Objects.SO.SOLine soLine in ((PXSelectBase) this.Base.Transactions).Cache.Deleted)
    {
      if (!((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxSOLine>((object) soLine).SDPosted.GetValueOrDefault())
      {
        ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Current = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Search<FSSODet.sourceLineNbr>((object) soLine.LineNbr, Array.Empty<object>()));
        if (((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Current != null)
          ((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Delete(((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Current);
      }
    }
  }

  public virtual void InsertUpdateDeleteServiceOrderDetail(ServiceOrderEntry graphServiceOrder)
  {
    this.DeleteFSSODetLinesFromDeletedSOlines(graphServiceOrder);
    foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.SO.SOLine soLineRow = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
      FSxSOLine extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxSOLine>((object) soLineRow);
      if (!extension.SDPosted.GetValueOrDefault())
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, soLineRow.InventoryID);
        PXCache cache = ((PXSelectBase) graphServiceOrder.ServiceOrderDetails).Cache;
        FSSODet fsSODetRow = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) graphServiceOrder.ServiceOrderDetails).Search<FSSODet.sourceLineNbr>((object) soLineRow.LineNbr, Array.Empty<object>()));
        string lineType = !(inventoryItem.ItemType == "S") ? (inventoryItem.ItemType == "F" || inventoryItem.ItemType == "M" || inventoryItem.ItemType == "A" ? "SLPRO" : "NSTKI") : "SERVI";
        this.InsertUpdateDeleteFSSODet<FSSODet>(cache, soLineRow, extension, fsSODetRow, lineType);
      }
    }
    if (GraphHelper.RowCast<FSSODet>(((PXSelectBase) graphServiceOrder.ServiceOrderDetails).Cache.Inserted).Where<FSSODet>((Func<FSSODet, bool>) (x => x.IsInventoryItem)).Count<FSSODet>() <= 0)
      return;
    foreach (FSSODet fssoDet1 in ((PXSelectBase) graphServiceOrder.ServiceOrderDetails).Cache.Cached.Cast<FSSODet>().Where<FSSODet>((Func<FSSODet, bool>) (x => x.IsInventoryItem && x.EquipmentAction == "ST")).ToList<FSSODet>())
    {
      foreach (FSSODet fssoDet2 in ((PXSelectBase) graphServiceOrder.ServiceOrderDetails).Cache.Cached.Cast<FSSODet>().Where<FSSODet>((Func<FSSODet, bool>) (x =>
      {
        int? equipmentLineNbr = x.SONewTargetEquipmentLineNbr;
        int num = 0;
        return equipmentLineNbr.GetValueOrDefault() > num & equipmentLineNbr.HasValue;
      })).ToList<FSSODet>())
      {
        int? equipmentLineNbr = fssoDet2.SONewTargetEquipmentLineNbr;
        int? sourceLineNbr = fssoDet1.SourceLineNbr;
        if (equipmentLineNbr.GetValueOrDefault() == sourceLineNbr.GetValueOrDefault() & equipmentLineNbr.HasValue == sourceLineNbr.HasValue && fssoDet2.NewTargetEquipmentLineNbr == null)
          fssoDet2.NewTargetEquipmentLineNbr = fssoDet1.LineRef;
      }
    }
  }

  public virtual void InsertUpdateServiceOrderLine<SODetType>(
    PXCache cacheSODet,
    FSSODet fsSODetRow,
    bool insertRow,
    string lineType,
    PX.Objects.SO.SOLine soLineRow,
    FSxSOLine fsxSOLineRow)
    where SODetType : FSSODet, IBqlTable, new()
  {
    bool flag = false;
    if (insertRow)
    {
      fsSODetRow = (FSSODet) new SODetType();
      fsSODetRow.SourceLineNbr = soLineRow.LineNbr;
      fsSODetRow.IsPrepaid = new bool?(true);
      fsSODetRow.LinkedDocType = soLineRow.OrderType;
      fsSODetRow.LinkedDocRefNbr = soLineRow.OrderNbr;
      fsSODetRow.LinkedEntityType = "SO";
    }
    int? nullable1 = fsSODetRow.InventoryID;
    int? inventoryId = soLineRow.InventoryID;
    if (!(nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue))
    {
      fsSODetRow.InventoryID = soLineRow.InventoryID;
      flag = true;
      if (insertRow)
      {
        fsSODetRow.LineType = lineType;
        if (lineType == "SERVI")
          fsSODetRow.BillingRule = "FLRA";
        fsSODetRow = (FSSODet) cacheSODet.Insert((object) fsSODetRow);
      }
    }
    int? nullable2 = fsSODetRow.SubItemID;
    nullable1 = soLineRow.SubItemID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) && PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      fsSODetRow.SubItemID = soLineRow.SubItemID;
      flag = true;
    }
    nullable1 = fsSODetRow.SiteID;
    nullable2 = soLineRow.SiteID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      fsSODetRow.SiteID = soLineRow.SiteID;
      flag = true;
    }
    nullable2 = fsSODetRow.SiteLocationID;
    nullable1 = soLineRow.LocationID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) && PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
    {
      fsSODetRow.SiteLocationID = soLineRow.LocationID;
      flag = true;
    }
    if (fsSODetRow.UOM != soLineRow.UOM)
    {
      fsSODetRow.UOM = soLineRow.UOM;
      flag = true;
    }
    nullable1 = fsSODetRow.ProjectID;
    nullable2 = soLineRow.ProjectID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) && !ProjectDefaultAttribute.IsNonProject(soLineRow.ProjectID))
    {
      fsSODetRow.ProjectID = soLineRow.ProjectID;
      flag = true;
    }
    nullable2 = fsSODetRow.ProjectTaskID;
    nullable1 = soLineRow.TaskID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
    {
      fsSODetRow.ProjectTaskID = soLineRow.TaskID;
      flag = true;
    }
    nullable1 = fsSODetRow.CostCodeID;
    nullable2 = soLineRow.CostCodeID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      fsSODetRow.CostCodeID = soLineRow.CostCodeID;
      flag = true;
    }
    Decimal? nullable3 = fsSODetRow.DiscPct;
    Decimal? discPct = soLineRow.DiscPct;
    if (!(nullable3.GetValueOrDefault() == discPct.GetValueOrDefault() & nullable3.HasValue == discPct.HasValue))
    {
      fsSODetRow.DiscPct = soLineRow.DiscPct;
      flag = true;
    }
    Decimal? nullable4 = fsSODetRow.CuryDiscAmt;
    nullable3 = soLineRow.CuryDiscAmt;
    if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue))
    {
      fsSODetRow.CuryDiscAmt = soLineRow.CuryDiscAmt;
      flag = true;
    }
    if (fsSODetRow.TranDesc != soLineRow.TranDesc)
    {
      fsSODetRow.TranDesc = soLineRow.TranDesc;
      flag = true;
    }
    nullable3 = fsSODetRow.EstimatedQty;
    nullable4 = soLineRow.OrderQty;
    if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
    {
      nullable4 = fsSODetRow.CuryUnitPrice;
      nullable3 = soLineRow.CuryUnitPrice;
      if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
        goto label_30;
    }
    flag = true;
label_30:
    if (!flag && !insertRow)
      return;
    fsSODetRow.LineType = lineType;
    if (lineType == "SERVI")
      fsSODetRow.BillingRule = "FLRA";
    fsSODetRow.IsFree = soLineRow.IsFree;
    fsSODetRow.ManualDisc = soLineRow.ManualDisc;
    fsSODetRow.EstimatedQty = soLineRow.OrderQty;
    fsSODetRow.CuryUnitPrice = soLineRow.CuryUnitPrice;
    fsSODetRow.ManualPrice = soLineRow.ManualPrice;
    fsSODetRow.EquipmentAction = fsxSOLineRow.EquipmentAction ?? "NO";
    fsSODetRow.SMEquipmentID = fsxSOLineRow.SMEquipmentID;
    fsSODetRow.EquipmentLineRef = fsxSOLineRow.EquipmentComponentLineNbr;
    fsSODetRow.ComponentID = fsxSOLineRow.ComponentID;
    fsSODetRow.SONewTargetEquipmentLineNbr = fsxSOLineRow.NewEquipmentLineNbr;
    SharedFunctions.CopyNotesAndFiles(((PXSelectBase) this.Base.Transactions).Cache, cacheSODet, (object) soLineRow, (object) fsSODetRow, (bool?) ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype)?.Current.CopyNotes, (bool?) ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype)?.Current.CopyFiles);
    cacheSODet.Update((object) fsSODetRow);
  }

  /// <summary>
  /// Clean SM fields depending if SDEnabled checkbox is not selected.
  /// </summary>
  public virtual void CleanSMFieldsBeforeSaving(PX.Objects.SO.SOOrder soOrderRow)
  {
    FSxSOOrder extension = ((PXSelectBase) this.Base.Document).Cache.GetExtension<FSxSOOrder>((object) soOrderRow);
    bool? sdEnabled = extension.SDEnabled;
    bool flag = false;
    if (!(sdEnabled.GetValueOrDefault() == flag & sdEnabled.HasValue))
      return;
    extension.SrvOrdType = (string) null;
    extension.ServiceOrderRefNbr = (string) null;
    extension.AssignedEmpID = new int?();
    extension.SLAETA = new DateTime?();
    extension.Installed = new bool?(false);
    foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
      ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxSOLine>((object) PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult)).SDSelected = new bool?(false);
  }

  /// <summary>
  /// Check if the current selected project belongs to the current customer.
  /// This applies only to a Sales Order that will be related with a Service Order.
  /// </summary>
  public virtual void CheckIfCurrentProjectBelongsToCustomer(PXCache cache, PX.Objects.SO.SOOrder soOrderRow)
  {
    FSxSOOrder extension = cache.GetExtension<FSxSOOrder>((object) soOrderRow);
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (!soOrderRow.ProjectID.HasValue)
      return;
    int? nullable = soOrderRow.CustomerID;
    if (!nullable.HasValue)
      return;
    nullable = soOrderRow.ProjectID;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue || !extension.SDEnabled.GetValueOrDefault())
      return;
    if (PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>, And<PX.Objects.CT.Contract.customerID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) soOrderRow.ProjectID,
      (object) soOrderRow.CustomerID
    })) == null)
      propertyException = new PXSetPropertyException("The project you select must belong to this customer.", (PXErrorLevel) 5);
    if (!(PXUIFieldAttribute.GetError<PX.Objects.SO.SOOrder.customerID>(cache, (object) soOrderRow) == string.Empty) && !(PXUIFieldAttribute.GetError<PX.Objects.SO.SOOrder.customerID>(cache, (object) soOrderRow) == "The project you select must belong to this customer."))
      return;
    cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.customerID>((object) soOrderRow, (object) soOrderRow.CustomerID, (Exception) propertyException);
  }

  /// <summary>
  /// Check if the given Sales Order line is related with any appointment details.
  /// </summary>
  /// <param name="soOrderRow">Sales Order row.</param>
  /// <param name="soLineRow">Sales Order line.</param>
  /// <returns>Returns true if the Sales Order Line is related with at least one appointment detail.</returns>
  public virtual bool IsLineSourceForAppoinmentLine(
    PX.Objects.SO.SOOrder soOrderRow,
    PX.Objects.SO.SOLine soLineRow,
    FSServiceOrder fsServiceOrderRow)
  {
    if (fsServiceOrderRow == null)
      fsServiceOrderRow = this.GetServiceOrderRecord(soOrderRow);
    if (fsServiceOrderRow == null)
      return false;
    return PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>>, Where<FSSODet.sourceLineNbr, Equal<Required<FSSODet.sourceLineNbr>>, And<FSSODet.sOID, Equal<Required<FSSODet.sOID>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) soLineRow.LineNbr,
      (object) fsServiceOrderRow.SOID
    })) != null;
  }

  /// <summary>
  /// Check if the given Sales Order Lines are different in Service Management prepaid related fields.
  /// </summary>
  public virtual bool ArePrepaidFieldsBeingModified(PX.Objects.SO.SOLine soLineRow, PX.Objects.SO.SOLine newSOLineRow)
  {
    FSxSOLine extension1 = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxSOLine>((object) soLineRow);
    FSxSOLine extension2 = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxSOLine>((object) newSOLineRow);
    int? inventoryId1 = soLineRow.InventoryID;
    int? inventoryId2 = newSOLineRow.InventoryID;
    if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
    {
      int? nullable1 = soLineRow.SubItemID;
      int? nullable2 = newSOLineRow.SubItemID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = soLineRow.SiteID;
        nullable1 = newSOLineRow.SiteID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && !(soLineRow.UOM != newSOLineRow.UOM))
        {
          Decimal? nullable3 = soLineRow.UnitPrice;
          Decimal? nullable4 = newSOLineRow.UnitPrice;
          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
          {
            nullable4 = soLineRow.OrderQty;
            nullable3 = newSOLineRow.OrderQty;
            if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
            {
              nullable1 = soLineRow.ProjectID;
              nullable2 = newSOLineRow.ProjectID;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = soLineRow.TaskID;
                nullable1 = newSOLineRow.TaskID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && !(soLineRow.TranDesc != newSOLineRow.TranDesc))
                {
                  bool? sdSelected1 = extension1.SDSelected;
                  bool? sdSelected2 = extension2.SDSelected;
                  return !(sdSelected1.GetValueOrDefault() == sdSelected2.GetValueOrDefault() & sdSelected1.HasValue == sdSelected2.HasValue);
                }
              }
            }
          }
        }
      }
    }
    return true;
  }

  /// <summary>Enables/Disables the "Open Appointment Board" button.</summary>
  protected virtual void EnableDisableActions(
    PXCache cache,
    PX.Objects.SO.SOOrder soOrderRow,
    FSxSOOrder fsxSOOrderRow,
    bool allowCreateServiceOrder,
    bool fsIntegrationEnabled)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (soOrderRow != null && cache.GetStatus((object) soOrderRow) != 2 && fsxSOOrderRow != null)
    {
      if (fsxSOOrderRow.ServiceOrderRefNbr != null)
        flag1 = true;
      else
        flag2 = true;
    }
    ((PXAction) this.OpenAppointmentBoard).SetEnabled(flag1 & fsIntegrationEnabled);
    ((PXAction) this.CreateServiceOrder).SetEnabled(flag2 & allowCreateServiceOrder & fsIntegrationEnabled);
    ((PXAction) this.ViewServiceOrder).SetEnabled(flag1 & fsIntegrationEnabled);
  }

  /// <summary>Enables the Fields if they can be edited.</summary>
  protected virtual void EnableDisable_All(
    PXCache cache,
    PX.Objects.SO.SOOrder soOrderRow,
    bool fsIntegrationEnabled)
  {
    FSxSOOrder extension = cache.GetExtension<FSxSOOrder>((object) soOrderRow);
    bool serviceOrder = this.CanCreateServiceOrder(cache, soOrderRow, extension);
    this.EnableDisableActions(cache, soOrderRow, extension, serviceOrder, fsIntegrationEnabled);
    PXUIFieldAttribute.SetVisible<FSxSOLine.relatedDocument>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, !serviceOrder & fsIntegrationEnabled);
  }

  protected virtual void UpdateQty(PXCache cache, PX.Objects.SO.SOLine soLineRow)
  {
    if (!this.IsFSIntegrationEnabled())
      return;
    FSxSOLine extension = cache.GetExtension<FSxSOLine>((object) soLineRow);
    if (extension == null)
      return;
    int num;
    if (!(extension.EquipmentAction == "RT"))
    {
      if (extension.EquipmentAction == "CC" || extension.EquipmentAction == "RC")
      {
        int? nullable = extension.SMEquipmentID;
        if (nullable.HasValue)
        {
          nullable = extension.ComponentID;
          if (nullable.HasValue)
          {
            num = 1;
            goto label_14;
          }
        }
        nullable = extension.NewEquipmentLineNbr;
        if (nullable.HasValue)
        {
          nullable = extension.ComponentID;
          num = nullable.HasValue ? 1 : 0;
        }
        else
          num = 0;
      }
      else
        num = 0;
    }
    else
      num = 1;
label_14:
    if (num == 0)
      return;
    cache.SetValueExt<PX.Objects.SO.SOLine.orderQty>((object) soLineRow, (object) 1M);
  }

  public virtual DateTime? GetShipDate(FSServiceOrder serviceOrder, FSAppointment appointment)
  {
    return AppointmentEntry.GetShipDateInt((PXGraph) this.Base, serviceOrder, appointment);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, FSxSOOrder.sDEnabled> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.SO.SOOrder row = e.Row;
    FSxSOOrder extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, FSxSOOrder.sDEnabled>>) e).Cache.GetExtension<FSxSOOrder>((object) row);
    if (!this.IsFSIntegrationEnabled())
    {
      extension.SDEnabled = new bool?(false);
    }
    else
    {
      bool? sdEnabled = extension.SDEnabled;
      bool flag = false;
      if (sdEnabled.GetValueOrDefault() == flag & sdEnabled.HasValue)
      {
        FSServiceOrder serviceOrderRecord = this.GetServiceOrderRecord(row);
        extension.SrvOrdType = (string) null;
        if (serviceOrderRecord != null)
        {
          foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
          {
            PX.Objects.SO.SOLine soLineRow = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
            if (this.IsLineSourceForAppoinmentLine(row, soLineRow, serviceOrderRecord))
              throw new PXException("The related service order {0} cannot be deleted. Some document details of the sales order are related to at least one appointment.", new object[1]
              {
                (object) serviceOrderRecord.RefNbr
              });
          }
        }
        this.CleanSMFieldsBeforeSaving(row);
      }
      else
      {
        if (extension.SrvOrdType != null)
          return;
        FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
        if (fsSetup == null)
          return;
        extension.SrvOrdType = fsSetup.DfltSOSrvOrdType;
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.SO.SOOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.SO.SOOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache;
    bool flag1 = this.IsFSIntegrationEnabled();
    this.SetExtensionVisibleInvisible<FSxSOOrder>(cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Args, flag1, false);
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.inventory>() && PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
    this.SetExtensionVisibleInvisible<FSxSOLine>(((PXSelectBase) this.Base.Transactions).Cache, (PXRowSelectedEventArgs) null, flag1, true);
    PXUIFieldAttribute.SetVisibility<FSxSOLine.comment>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, flag2 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<FSxSOLine.equipmentAction>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, flag2 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<FSxSOLine.newEquipmentLineNbr>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, flag2 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    this.EnableDisable_All(cache, row, flag1);
    if (!flag1)
      return;
    this.CheckIfCurrentProjectBelongsToCustomer(cache, row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.SO.SOOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.SO.SOOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOOrder row = e.Row;
    FSxSOOrder extension = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder>>) e).Cache.GetExtension<FSxSOOrder>((object) row);
    if (e.Operation == 3)
    {
      if (extension != null && extension.ServiceOrderRefNbr != null)
        throw new PXException("The sales order cannot be deleted because a service order is linked to it. Delete the associated service order first.");
    }
    else
      this.ValidatePostBatchStatus(e.Operation, "SO", row.OrderType, row.OrderNbr);
    if (!(row.Status != "C"))
      return;
    FSServiceOrder fsServiceOrder = this.InsertUpdateDeleteServiceOrderDocument(row, extension, e.Operation);
    if (fsServiceOrder == null || !(extension.ServiceOrderRefNbr != fsServiceOrder.RefNbr))
      return;
    ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<FSxSOOrder.serviceOrderRefNbr>((object) row, (object) fsServiceOrder.RefNbr);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.SO.SOOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.SO.SOOrder>>) e).Cache;
    FSxSOOrder extension = cache.GetExtension<FSxSOOrder>((object) row);
    if (!this.IsFSIntegrationEnabled())
    {
      if (e.TranStatus != null || e.Operation != 3)
        return;
      this.UpdatePostingInformation(row);
    }
    else
    {
      if (e.TranStatus == null)
      {
        if (e.Operation == 3)
          this.UpdatePostingInformation(row);
        if (!this.CanCreateServiceOrder(cache, row, extension))
        {
          cache.RaiseExceptionHandling<FSxSOOrder.sDEnabled>((object) row, (object) extension.SDEnabled, (Exception) new PXSetPropertyException("This sales order originated from a service order. A new service order cannot be created from the sales order.", (PXErrorLevel) 2));
          return;
        }
        if (extension.SDEnabled.GetValueOrDefault() && extension.SrvOrdType != null && extension.ServiceOrderRefNbr != null)
          PXUpdate<Set<FSServiceOrder.sourceDocType, Required<FSServiceOrder.sourceDocType>, Set<FSServiceOrder.sourceRefNbr, Required<FSServiceOrder.sourceRefNbr>>>, FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Update(cache.Graph, new object[4]
          {
            (object) row.OrderType,
            (object) row.OrderNbr,
            (object) extension.SrvOrdType,
            (object) extension.ServiceOrderRefNbr
          });
      }
      if (e.TranStatus != 2)
        return;
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<FSxSOOrder.serviceOrderRefNbr>((object) row, (object) null);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, FSxSOLine.sDSelected> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    FSxSOOrder extension = ((PXSelectBase) this.Base.Document).Cache.GetExtension<FSxSOOrder>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, FSxSOLine.sDSelected>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) (bool) (!extension.SDEnabled.Value ? 0 : (extension.ServiceOrderRefNbr != null ? 1 : 0));
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled() || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) e).NewValue == null)
      return;
    Decimal num1 = Convert.ToDecimal(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) e).NewValue);
    Decimal? orderQty = e.Row.OrderQty;
    Decimal num2 = num1;
    if (!(orderQty.GetValueOrDefault() == num2 & orderQty.HasValue) && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, (object) e.Row, typeof (PX.Objects.SO.SOLine.orderQty).Name))
      throw new PXSetPropertyException("This value cannot be updated because it is related to an appointment or service order.");
    if (!e.Row.IsStockItem.GetValueOrDefault())
      return;
    FSxSOLine extension = ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>>) e).Cache.GetExtension<FSxSOLine>((object) e.Row);
    if (extension.EquipmentAction != null && extension.EquipmentAction != "NO" && Decimal.Remainder((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) e).NewValue, 1M) != 0M)
      throw new PXSetPropertyException("A decimal number cannot be entered as an item quantity if any equipment action is specified in the line. Specify a whole number for the quantity of this item.", (PXErrorLevel) 4);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.uOM> e)
  {
    if (e.Row != null && this.IsFSIntegrationEnabled() && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, (object) e.Row, typeof (PX.Objects.SO.SOLine.uOM).Name))
      throw new PXSetPropertyException("This value cannot be updated because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    bool updateQty = ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>>) e).ExternalCall && SharedFunctions.SetScreenIDToDotFormat("SO301000") == ((PXGraph) this.Base).Accessinfo.ScreenID;
    SharedFunctions.UpdateEquipmentFields((PXGraph) this.Base, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>>) e).Cache, (object) row, row.InventoryID, updateQty);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.equipmentAction> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.equipmentAction>>) e).Cache;
    if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.equipmentAction>>) e).ExternalCall)
      return;
    SharedFunctions.ResetEquipmentFields(cache, (object) row);
    SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, (object) row);
    this.UpdateQty(cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.sMEquipmentID> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    this.UpdateQty(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.sMEquipmentID>>) e).Cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.componentID> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    this.UpdateQty(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.componentID>>) e).Cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.equipmentComponentLineNbr> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.SO.SOLine row = e.Row;
    FSxSOLine extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, FSxSOLine.equipmentComponentLineNbr>>) e).Cache.GetExtension<FSxSOLine>((object) row);
    if (extension.ComponentID.HasValue)
      return;
    extension.ComponentID = SharedFunctions.GetEquipmentComponentID((PXGraph) this.Base, extension.SMEquipmentID, extension.EquipmentComponentLineNbr);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLine> e)
  {
    bool flag = this.IsFSIntegrationEnabled();
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache;
    this.SetExtensionVisibleInvisible<FSxSOLine>(((PXSelectBase) this.Base.Transactions).Cache, (PXRowSelectedEventArgs) null, flag, true);
    if (e.Row == null)
      return;
    PX.Objects.SO.SOLine row = e.Row;
    this.EnableDisableSOline(cache, row, flag);
    if (!flag)
      return;
    SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.SO.SOLine> e)
  {
  }

  protected virtual void ResetOrderdQtyIfNeeded(PXCache cache, PX.Objects.SO.SOLine line)
  {
    if (line == null)
      return;
    bool flag = SharedFunctions.SetScreenIDToDotFormat("SO301000") == ((PXGraph) this.Base).Accessinfo.ScreenID;
    FSxSOLine extension = cache.GetExtension<FSxSOLine>((object) line);
    if ((EnumerableExtensions.IsNotIn<string>(extension.EquipmentAction, "CC", "UC", "RC") ? 1 : (extension.EquipmentItemClass == "CT" ? 1 : 0)) != 0 || cache.Graph.IsCopyPasteContext || !flag)
      return;
    Decimal? nullable = (Decimal?) cache.GetValue<PX.Objects.SO.SOLine.orderQty>((object) line);
    Decimal num = 1.0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    cache.SetValueExt<PX.Objects.SO.SOLine.orderQty>((object) line, (object) 1.0M);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOLine> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOLineSplit> e)
  {
    if (e.Row == null || !e.ExternalCall)
      return;
    PX.Objects.SO.SOLine line = (PX.Objects.SO.SOLine) LSParentAttribute.SelectParent(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.SO.SOLineSplit>>) e).Cache, (object) e.Row, typeof (PX.Objects.SO.SOLine));
    this.ResetOrderdQtyIfNeeded((PXCache) GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base), line);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.SO.SOLine> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<PX.Objects.SO.SOLine>>) e).Cache;
    cache.GetExtension<FSxSOLine>((object) row);
    PX.Objects.SO.SOLine newRow = e.NewRow;
    cache.GetExtension<FSxSOLine>((object) newRow);
    if (!e.ExternalCall)
      return;
    if (this.ArePrepaidFieldsBeingModified(row, newRow) && this.IsLineSourceForAppoinmentLine(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, row, (FSServiceOrder) null))
    {
      e.Cancel = true;
      throw new PXException("This line cannot be modified because it is linked to at least one appointment.");
    }
    int? nullable = row.InventoryID;
    int? inventoryId = newRow.InventoryID;
    if (nullable.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable.HasValue == inventoryId.HasValue)
    {
      int? subItemId = row.SubItemID;
      nullable = newRow.SubItemID;
      if (subItemId.GetValueOrDefault() == nullable.GetValueOrDefault() & subItemId.HasValue == nullable.HasValue)
        return;
    }
    if (this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, (object) row, (string) null))
      throw new PXException("This value cannot be updated because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine> e)
  {
    if (!e.ExternalCall)
      return;
    this.ResetOrderdQtyIfNeeded(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOLine> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    if (this.IsLineSourceForAppoinmentLine(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, row, (FSServiceOrder) null))
      throw new PXException("This line cannot be modified because it is linked to at least one appointment.");
    if (e.ExternalCall && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, (object) row, (string) null))
      throw new PXException("The line cannot be deleted because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOLine> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine>>) e).Cache;
    FSxSOLine extension = cache.GetExtension<FSxSOLine>((object) row);
    string empty = string.Empty;
    if (e.Operation == 3 || SharedFunctions.AreEquipmentFieldsValid(cache, row.InventoryID, extension.SMEquipmentID, (object) extension.NewEquipmentLineNbr, extension.EquipmentAction, ref empty))
      return;
    cache.RaiseExceptionHandling<FSxSOLine.equipmentAction>((object) row, (object) extension.EquipmentAction, (Exception) new PXSetPropertyException(empty));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.SO.SOLine> e)
  {
    if (e.Row == null || !this.IsFSIntegrationEnabled())
      return;
    PX.Objects.SO.SOLine row = e.Row;
    if (e.TranStatus != 2 || !this.IsInvoiceProcessRunning)
      return;
    MessageHelper.GetRowMessage(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.SO.SOLine>>) e).Cache, (IBqlTable) row, false, false);
  }

  public override List<MessageHelper.ErrorInfo> GetErrorInfo()
  {
    return MessageHelper.GetErrorInfo<PX.Objects.SO.SOLine>(((PXSelectBase) this.Base.Document).Cache, (IBqlTable) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, (PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions, typeof (PX.Objects.SO.SOLine));
  }

  public override void CreateInvoice(
    PXGraph graphProcess,
    List<DocLineExt> docLines,
    short invtMult,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    OnDocumentHeaderInsertedDelegate onDocumentHeaderInserted,
    OnTransactionInsertedDelegate onTransactionInserted,
    PXQuickProcess.ActionFlow quickProcessFlow)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SM_SOOrderEntry.\u003C\u003Ec__DisplayClass67_0 cDisplayClass670 = new SM_SOOrderEntry.\u003C\u003Ec__DisplayClass67_0();
    if (docLines.Count == 0)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass670.fsServiceOrderRow = docLines[0].fsServiceOrder;
    FSSrvOrdType fsSrvOrdType1 = docLines[0].fsSrvOrdType;
    FSPostDoc fsPostDoc1 = docLines[0].fsPostDoc;
    FSAppointment fsAppointment = docLines[0].fsAppointment;
    bool? nullable1 = new bool?(false);
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass670, __methodptr(\u003CCreateInvoice\u003Eb__0));
    try
    {
      ((PXGraph) this.Base).FieldDefaulting.AddHandler<PX.Objects.SO.SOOrder.branchID>(pxFieldDefaulting);
      PX.Objects.SO.SOOrder soOrder1 = new PX.Objects.SO.SOOrder();
      if (invtMult >= (short) 0)
        soOrder1.OrderType = !string.IsNullOrEmpty(fsPostDoc1.PostOrderType) ? fsPostDoc1.PostOrderType : throw new PXException("The sales order cannot be generated because the sales order type has not been specified on the Service Order Types (FS202300) form.");
      else
        soOrder1.OrderType = !string.IsNullOrEmpty(fsPostDoc1.PostOrderTypeNegativeBalance) ? fsPostDoc1.PostOrderTypeNegativeBalance : throw new PXException("The sales order cannot be generated because the sales order type for negative balances has not been specified on the Service Order Types (FS202300) form.");
      soOrder1.InclCustOpenOrders = new bool?(true);
      // ISSUE: reference to a compiler-generated field
      soOrder1.CustomerOrderNbr = cDisplayClass670.fsServiceOrderRow.CustPORefNbr;
      this.CheckAutoNumbering(((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).SelectSingle(new object[1]
      {
        (object) soOrder1.OrderType
      }).OrderNumberingID);
      PX.Objects.SO.SOOrder data1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Insert(soOrder1);
      nullable1 = data1.Hold;
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.hold>((object) data1, (object) true);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.orderDate>((object) data1, (object) invoiceDate);
      // ISSUE: reference to a compiler-generated field
      DateTime? orderDate = cDisplayClass670.fsServiceOrderRow.OrderDate;
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.requestDate>((object) data1, (object) orderDate);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.customerID>((object) data1, (object) cDisplayClass670.fsServiceOrderRow.BillCustomerID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.customerLocationID>((object) data1, (object) cDisplayClass670.fsServiceOrderRow.BillLocationID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.curyID>((object) data1, (object) cDisplayClass670.fsServiceOrderRow.CuryID);
      // ISSUE: reference to a compiler-generated field
      string newValue1 = fsAppointment != null ? fsAppointment.TaxZoneID : cDisplayClass670.fsServiceOrderRow.TaxZoneID;
      if (data1.TaxZoneID != newValue1)
      {
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.overrideTaxZone>((object) data1, (object) true);
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.taxZoneID>((object) data1, (object) newValue1);
      }
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.taxCalcMode>((object) data1, fsAppointment != null ? (object) fsAppointment.TaxCalcMode : (object) cDisplayClass670.fsServiceOrderRow.TaxCalcMode);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.orderDesc>((object) data1, (object) cDisplayClass670.fsServiceOrderRow.DocDesc);
      // ISSUE: reference to a compiler-generated field
      int? nullable2 = cDisplayClass670.fsServiceOrderRow.ProjectID;
      if (nullable2.HasValue)
      {
        // ISSUE: reference to a compiler-generated field
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.projectID>((object) data1, (object) cDisplayClass670.fsServiceOrderRow.ProjectID);
      }
      SOOrderEntry graph = this.Base;
      // ISSUE: reference to a compiler-generated field
      int? billCustomerId = cDisplayClass670.fsServiceOrderRow.BillCustomerID;
      nullable2 = new int?();
      int? vendorID = nullable2;
      string customerOrVendor = this.GetTermsIDFromCustomerOrVendor((PXGraph) graph, billCustomerId, vendorID);
      bool? nullable3;
      int num1;
      if (data1.ARDocType == "CRM")
      {
        nullable3 = ((PXSelectBase<ARSetup>) this.Base.arsetup).Current.TermsInCreditMemos;
        num1 = nullable3.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 1;
      if (num1 != 0)
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.termsID>((object) data1, (object) (customerOrVendor ?? fsSrvOrdType1.DfltTermIDARSO));
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.ownerID>((object) data1, (object) null);
      PXCache cache1 = ((PXSelectBase) this.Base.Document).Cache;
      PX.Objects.SO.SOOrder data2 = data1;
      // ISSUE: reference to a compiler-generated field
      FSServiceOrder fsServiceOrderRow1 = cDisplayClass670.fsServiceOrderRow;
      int? nullable4;
      if (fsServiceOrderRow1 == null)
      {
        nullable2 = new int?();
        nullable4 = nullable2;
      }
      else
        nullable4 = fsServiceOrderRow1.SalesPersonID;
      // ISSUE: variable of a boxed type
      __Boxed<int?> newValue2 = (ValueType) nullable4;
      cache1.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.salesPersonID>((object) data2, (object) newValue2);
      PX.Objects.SO.SOOrder soOrder2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(data1);
      // ISSUE: reference to a compiler-generated field
      this.SetContactAndAddress((PXGraph) this.Base, cDisplayClass670.fsServiceOrderRow);
      if (onDocumentHeaderInserted != null)
        onDocumentHeaderInserted((PXGraph) this.Base, (IBqlTable) soOrder2);
      List<SharedClasses.SOARLineEquipmentComponent> source = new List<SharedClasses.SOARLineEquipmentComponent>();
      foreach (DocLineExt docLine1 in docLines)
      {
        // ISSUE: reference to a compiler-generated field
        bool flag1 = fsAppointment == null ? docLine1.fsSODet == docLines[0].fsSODet || cDisplayClass670.fsServiceOrderRow.RefNbr != docLine1.fsServiceOrder.RefNbr : docLine1.fsAppointmentDet == docLines[0].fsAppointmentDet || fsAppointment.RefNbr != docLine1.fsAppointment.RefNbr;
        IDocLine docLine2 = docLine1.docLine;
        FSSODet fsSoDet = docLine1.fsSODet;
        FSAppointmentDet fsAppointmentDet1 = docLine1.fsAppointmentDet;
        FSPostDoc fsPostDoc2 = docLine1.fsPostDoc;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass670.fsServiceOrderRow = docLine1.fsServiceOrder;
        FSSrvOrdType fsSrvOrdType2 = docLine1.fsSrvOrdType;
        fsAppointment = docLine1.fsAppointment;
        PMTask pmTask = docLine1.pmTask;
        FSAppointmentDet fsAppointmentDet2 = docLine1.fsAppointmentDet;
        if (pmTask != null && pmTask.Status == "F")
        {
          // ISSUE: reference to a compiler-generated field
          throw new PXException("The {1} line of the {0} document cannot be processed because the {2} project task has already been completed.", new object[3]
          {
            (object) cDisplayClass670.fsServiceOrderRow.RefNbr,
            (object) MessageHelper.GetLineDisplayHint((PXGraph) this.Base, docLine2.LineRef, docLine2.TranDesc, docLine2.InventoryID),
            (object) pmTask.TaskCD
          });
        }
        PX.Objects.SO.SOLine copy1 = (PX.Objects.SO.SOLine) ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) (((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Current = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Insert(new PX.Objects.SO.SOLine())));
        copy1.OrderQty = new Decimal?(0M);
        copy1.BranchID = docLine2.BranchID;
        copy1.InventoryID = docLine2.InventoryID;
        if (copy1.LineType == "GI" && PXAccess.FeatureInstalled<FeaturesSet.subItem>())
          copy1.SubItemID = docLine2.SubItemID;
        copy1.SiteID = docLine2.SiteID;
        nullable2 = docLine2.SiteLocationID;
        if (nullable2.HasValue)
          copy1.LocationID = docLine2.SiteLocationID;
        copy1.UOM = docLine2.UOM;
        nullable2 = docLine2.ProjectID;
        if (nullable2.HasValue)
        {
          nullable2 = docLine2.ProjectTaskID;
          if (nullable2.HasValue)
            copy1.TaskID = docLine2.ProjectTaskID;
        }
        copy1.SalesAcctID = docLine2.AcctID;
        copy1.TaxCategoryID = docLine2.TaxCategoryID;
        copy1.TranDesc = docLine2.TranDesc;
        PX.Objects.SO.SOLine soLine1 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy1);
        bool flag2 = SharedFunctions.IsLotSerialRequired(((PXSelectBase) this.Base.Transactions).Cache, soLine1.InventoryID);
        bool flag3 = false;
        if (flag2 && fsAppointment == null)
        {
          foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSSODetSplit.lineNbr>>>>>, OrderBy<Asc<FSSODetSplit.splitLineNbr>>>.Config>.Select((PXGraph) this.Base, new object[3]
          {
            (object) fsSoDet.SrvOrdType,
            (object) fsSoDet.RefNbr,
            (object) fsSoDet.LineNbr
          }))
          {
            FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
            nullable3 = fssoDetSplit.POCreate;
            bool flag4 = false;
            if (nullable3.GetValueOrDefault() == flag4 & nullable3.HasValue && !string.IsNullOrEmpty(fssoDetSplit.LotSerialNbr))
            {
              PX.Objects.SO.SOLineSplit copy2 = (PX.Objects.SO.SOLineSplit) ((PXSelectBase) this.Base.splits).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Insert(new PX.Objects.SO.SOLineSplit()));
              PX.Objects.SO.SOLineSplit soLineSplit1 = copy2;
              nullable2 = fssoDetSplit.SiteID;
              int? nullable5 = nullable2 ?? copy2.SiteID;
              soLineSplit1.SiteID = nullable5;
              PX.Objects.SO.SOLineSplit soLineSplit2 = copy2;
              int? nullable6;
              if (!(soOrder2.OrderType != "SO"))
              {
                nullable2 = new int?();
                nullable6 = nullable2;
              }
              else
              {
                nullable2 = fssoDetSplit.LocationID;
                nullable6 = nullable2 ?? copy2.LocationID;
              }
              soLineSplit2.LocationID = nullable6;
              PX.Objects.SO.SOLineSplit soLineSplit3 = copy2;
              nullable2 = fssoDetSplit.CostCenterID;
              int? nullable7 = nullable2 ?? copy2.CostCenterID;
              soLineSplit3.CostCenterID = nullable7;
              copy2.LotSerialNbr = fssoDetSplit.LotSerialNbr;
              copy2.Qty = fssoDetSplit.Qty;
              ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(copy2);
              flag3 = true;
            }
          }
          soLine1 = (PX.Objects.SO.SOLine) ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) soLine1);
        }
        else if (flag2 && fsAppointment != null)
        {
          foreach (PXResult<FSApptLineSplit> pxResult in PXSelectBase<FSApptLineSplit, PXSelect<FSApptLineSplit, Where<FSApptLineSplit.srvOrdType, Equal<Required<FSApptLineSplit.srvOrdType>>, And<FSApptLineSplit.apptNbr, Equal<Required<FSApptLineSplit.apptNbr>>, And<FSApptLineSplit.lineNbr, Equal<Required<FSApptLineSplit.lineNbr>>>>>, OrderBy<Asc<FSApptLineSplit.splitLineNbr>>>.Config>.Select((PXGraph) this.Base, new object[3]
          {
            (object) fsAppointmentDet1.SrvOrdType,
            (object) fsAppointmentDet1.RefNbr,
            (object) fsAppointmentDet1.LineNbr
          }))
          {
            FSApptLineSplit fsApptLineSplit = PXResult<FSApptLineSplit>.op_Implicit(pxResult);
            if (!string.IsNullOrEmpty(fsApptLineSplit.LotSerialNbr))
            {
              PX.Objects.SO.SOLineSplit copy3 = (PX.Objects.SO.SOLineSplit) ((PXSelectBase) this.Base.splits).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Insert(new PX.Objects.SO.SOLineSplit()));
              PX.Objects.SO.SOLineSplit soLineSplit4 = copy3;
              nullable2 = fsApptLineSplit.SiteID;
              int? nullable8 = nullable2 ?? copy3.SiteID;
              soLineSplit4.SiteID = nullable8;
              PX.Objects.SO.SOLineSplit soLineSplit5 = copy3;
              int? nullable9;
              if (!(soOrder2.OrderType != "SO"))
              {
                nullable2 = new int?();
                nullable9 = nullable2;
              }
              else
              {
                nullable2 = fsApptLineSplit.LocationID;
                nullable9 = nullable2 ?? copy3.LocationID;
              }
              soLineSplit5.LocationID = nullable9;
              PX.Objects.SO.SOLineSplit soLineSplit6 = copy3;
              nullable2 = fsApptLineSplit.CostCenterID;
              int? nullable10 = nullable2 ?? copy3.CostCenterID;
              soLineSplit6.CostCenterID = nullable10;
              copy3.LotSerialNbr = fsApptLineSplit.LotSerialNbr;
              copy3.Qty = fsApptLineSplit.Qty;
              ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(copy3);
              flag3 = true;
            }
          }
          soLine1 = (PX.Objects.SO.SOLine) ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) soLine1);
        }
        Decimal? nullable11;
        if (!flag3)
        {
          soLine1.OrderQty = docLine2.GetQty(FieldType.BillableField);
        }
        else
        {
          Decimal? orderQty = soLine1.OrderQty;
          nullable11 = docLine2.GetQty(FieldType.BillableField);
          if (!(orderQty.GetValueOrDefault() == nullable11.GetValueOrDefault() & orderQty.HasValue == nullable11.HasValue))
            throw new PXException("The quantity in the posted document does not match the quantity in the source document.");
        }
        soLine1.IsFree = docLine2.IsFree;
        soLine1.ManualPrice = docLine2.ManualPrice;
        PX.Objects.SO.SOLine soLine2 = soLine1;
        nullable11 = docLine2.CuryUnitPrice;
        Decimal num2 = (Decimal) invtMult;
        Decimal? nullable12 = nullable11.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * num2) : new Decimal?();
        soLine2.CuryUnitPrice = nullable12;
        PX.Objects.SO.SOLine copy4 = (PX.Objects.SO.SOLine) ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine1));
        PX.Objects.SO.SOLine soLine3 = copy4;
        // ISSUE: reference to a compiler-generated field
        FSServiceOrder fsServiceOrderRow2 = cDisplayClass670.fsServiceOrderRow;
        int? nullable13;
        if (fsServiceOrderRow2 == null)
        {
          nullable2 = new int?();
          nullable13 = nullable2;
        }
        else
          nullable13 = fsServiceOrderRow2.SalesPersonID;
        soLine3.SalesPersonID = nullable13;
        bool flag5 = false;
        if (fsAppointmentDet1 != null)
        {
          nullable3 = fsAppointmentDet1.ManualDisc;
          flag5 = nullable3.Value;
        }
        else if (fsSoDet != null)
        {
          nullable3 = fsSoDet.ManualDisc;
          flag5 = nullable3.Value;
        }
        copy4.ManualDisc = new bool?(flag5);
        nullable3 = copy4.ManualDisc;
        if (nullable3.GetValueOrDefault())
          copy4.DiscPct = docLine2.DiscPct;
        PX.Objects.SO.SOLine soLine4 = copy4;
        nullable11 = docLine2.CuryBillableExtPrice;
        Decimal num3 = (Decimal) invtMult;
        Decimal? nullable14 = nullable11.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * num3) : new Decimal?();
        soLine4.CuryExtPrice = nullable14;
        PX.Objects.SO.SOLine soLine5 = copy4;
        PXCache cache2 = ((PXSelectBase) this.Base.Transactions).Cache;
        PX.Objects.SO.SOLine row1 = copy4;
        nullable11 = copy4.CuryExtPrice;
        Decimal valueOrDefault = nullable11.GetValueOrDefault();
        Decimal? nullable15 = new Decimal?(PXCurrencyAttribute.Round(cache2, (object) row1, valueOrDefault, CMPrecision.TRANCURY));
        soLine5.CuryExtPrice = nullable15;
        PX.Objects.SO.SOLine soLine6 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy4);
        nullable2 = docLine2.SubID;
        if (nullable2.HasValue)
        {
          try
          {
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.salesSubID>((object) soLine6, (object) docLine2.SubID);
          }
          catch (PXException ex)
          {
            soLine6.SalesSubID = new int?();
            soLine6 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine6);
          }
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.SetCombinedSubID((PXGraph) this.Base, ((PXSelectBase) this.Base.Transactions).Cache, (PX.Objects.AR.ARTran) null, (APTran) null, soLine6, fsSrvOrdType2, soLine6.BranchID, soLine6.InventoryID, cDisplayClass670.fsServiceOrderRow.BillLocationID, cDisplayClass670.fsServiceOrderRow.BranchLocationID, cDisplayClass670.fsServiceOrderRow.SalesPersonID, docLine2.IsService);
          soLine6 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine6);
        }
        PXCache cache3 = ((PXSelectBase) this.Base.Transactions).Cache;
        PX.Objects.SO.SOLine data3 = soLine6;
        // ISSUE: reference to a compiler-generated field
        FSServiceOrder fsServiceOrderRow3 = cDisplayClass670.fsServiceOrderRow;
        bool? nullable16;
        if (fsServiceOrderRow3 == null)
        {
          nullable3 = new bool?();
          nullable16 = nullable3;
        }
        else
          nullable16 = fsServiceOrderRow3.Commissionable;
        // ISSUE: variable of a boxed type
        __Boxed<bool?> newValue3 = (ValueType) nullable16;
        cache3.SetValueExtIfDifferent<PX.Objects.SO.SOLine.commissionable>((object) data3, (object) newValue3);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.costCodeID>((object) soLine6, (object) docLine2.CostCodeID);
        FSxSOLine extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxSOLine>((object) soLine6);
        extension.SDPosted = new bool?(true);
        // ISSUE: reference to a compiler-generated field
        extension.SrvOrdType = cDisplayClass670.fsServiceOrderRow.SrvOrdType;
        // ISSUE: reference to a compiler-generated field
        extension.ServiceOrderRefNbr = cDisplayClass670.fsServiceOrderRow.RefNbr;
        extension.AppointmentRefNbr = fsAppointment?.RefNbr;
        FSxSOLine fsxSoLine1 = extension;
        int? nullable17;
        if (fsSoDet == null)
        {
          nullable2 = new int?();
          nullable17 = nullable2;
        }
        else
          nullable17 = fsSoDet.LineNbr;
        fsxSoLine1.ServiceOrderLineNbr = nullable17;
        FSxSOLine fsxSoLine2 = extension;
        int? nullable18;
        if (fsAppointmentDet2 == null)
        {
          nullable2 = new int?();
          nullable18 = nullable2;
        }
        else
          nullable18 = fsAppointmentDet2.LineNbr;
        fsxSoLine2.AppointmentLineNbr = nullable18;
        if (PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() && docLine2.EquipmentAction != null)
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<FSxSOLine.equipmentAction>((object) soLine6, (object) docLine2.EquipmentAction);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<FSxSOLine.sMEquipmentID>((object) soLine6, (object) docLine2.SMEquipmentID);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<FSxSOLine.equipmentComponentLineNbr>((object) soLine6, (object) docLine2.EquipmentLineRef);
          extension.Comment = docLine2.Comment;
          if (docLine2.EquipmentAction == "ST" || (docLine2.EquipmentAction == "CC" || docLine2.EquipmentAction == "UC" || docLine2.EquipmentAction == "NO") && !string.IsNullOrEmpty(docLine2.NewTargetEquipmentLineNbr))
            source.Add(new SharedClasses.SOARLineEquipmentComponent(docLine2, soLine6, extension));
          else
            extension.ComponentID = docLine2.ComponentID;
        }
        if (flag1)
        {
          if (fsAppointment == null)
          {
            // ISSUE: reference to a compiler-generated field
            SharedFunctions.CopyNotesAndFiles(((PXGraph) this.Base).Caches[typeof (FSServiceOrder)], ((PXSelectBase) this.Base.Document).Cache, (object) cDisplayClass670.fsServiceOrderRow, (object) soOrder2, fsSrvOrdType2.CopyNotesToInvoice, fsSrvOrdType2.CopyAttachmentsToInvoice);
          }
          else
            SharedFunctions.CopyNotesAndFiles(((PXGraph) this.Base).Caches[typeof (FSAppointment)], ((PXSelectBase) this.Base.Document).Cache, (object) fsAppointment, (object) soOrder2, fsSrvOrdType2.CopyNotesToInvoice, fsSrvOrdType2.CopyAttachmentsToInvoice);
        }
        SharedFunctions.CopyNotesAndFiles(((PXSelectBase) this.Base.Transactions).Cache, (object) soLine6, docLine2, fsSrvOrdType2);
        PX.Objects.SO.SOLine row2;
        fsPostDoc2.DocLineRef = (object) (row2 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine6));
        if (onTransactionInserted != null)
          onTransactionInserted((PXGraph) this.Base, (IBqlTable) row2);
      }
      if (source.Count > 0)
      {
        foreach (SharedClasses.SOARLineEquipmentComponent equipmentComponent1 in source.Where<SharedClasses.SOARLineEquipmentComponent>((Func<SharedClasses.SOARLineEquipmentComponent, bool>) (x => x.equipmentAction == "ST")))
        {
          foreach (SharedClasses.SOARLineEquipmentComponent equipmentComponent2 in source.Where<SharedClasses.SOARLineEquipmentComponent>((Func<SharedClasses.SOARLineEquipmentComponent, bool>) (x => x.equipmentAction == "CC" || x.equipmentAction == "UC" || x.equipmentAction == "NO")))
          {
            if (equipmentComponent2.sourceNewTargetEquipmentLineNbr == equipmentComponent1.sourceLineRef)
            {
              equipmentComponent2.fsxSOLineRow.ComponentID = equipmentComponent2.componentID;
              equipmentComponent2.fsxSOLineRow.NewEquipmentLineNbr = equipmentComponent1.currentLineRef;
            }
          }
        }
      }
      if (((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.RequireControlTotal.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.curyControlTotal>((object) soOrder2, (object) soOrder2.CuryOrderTotal);
      if (!nullable1.GetValueOrDefault() || quickProcessFlow != null)
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.hold>((object) soOrder2, (object) false);
      ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(soOrder2);
    }
    finally
    {
      ((PXGraph) this.Base).FieldDefaulting.RemoveHandler<PX.Objects.SO.SOOrder.branchID>(pxFieldDefaulting);
    }
  }

  public override FSCreatedDoc PressSave(
    int batchID,
    List<DocLineExt> docLines,
    BeforeSaveDelegate beforeSave)
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null)
      throw new SharedClasses.TransactionScopeException();
    if (beforeSave != null)
      beforeSave((PXGraph) this.Base);
    ((PXGraph) this.Base).SelectTimeStamp();
    SOOrderEntryExternalTax extension = ((PXGraph) this.Base).GetExtension<SOOrderEntryExternalTax>();
    if (extension != null)
      extension.SkipTaxCalcAndSave();
    else
      ((PXAction) this.Base.Save).Press();
    string orderType = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderType;
    string orderNbr = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr;
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) orderType,
      (object) orderNbr
    }));
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    return new FSCreatedDoc()
    {
      BatchID = new int?(batchID),
      PostTo = "SO",
      CreatedDocType = current.OrderType,
      CreatedRefNbr = current.OrderNbr
    };
  }

  public override void Clear() => ((PXGraph) this.Base).Clear((PXClearOption) 3);

  public override PXGraph GetGraph() => (PXGraph) this.Base;

  public override void DeleteDocument(FSCreatedDoc fsCreatedDocRow)
  {
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) fsCreatedDocRow.CreatedRefNbr, new object[1]
    {
      (object) fsCreatedDocRow.CreatedDocType
    }));
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null || !(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr == fsCreatedDocRow.CreatedRefNbr) || !(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderType == fsCreatedDocRow.CreatedDocType))
      return;
    ((PXAction) this.Base.Delete).Press();
  }

  public override void CleanPostInfo(PXGraph cleanerGraph, FSPostDet fsPostDetRow)
  {
    PXUpdate<Set<FSPostInfo.sOLineNbr, Null, Set<FSPostInfo.sOOrderNbr, Null, Set<FSPostInfo.sOOrderType, Null, Set<FSPostInfo.sOPosted, False>>>>, FSPostInfo, Where<FSPostInfo.postID, Equal<Required<FSPostInfo.postID>>, And<FSPostInfo.sOPosted, Equal<True>>>>.Update(cleanerGraph, new object[1]
    {
      (object) fsPostDetRow.PostID
    });
  }

  public virtual bool IsLineCreatedFromAppSO(
    PXGraph cleanerGraph,
    object document,
    object lineDoc,
    string fieldName)
  {
    if (document == null || lineDoc == null || ((PXGraph) this.Base).Accessinfo.ScreenID.Replace(".", "") == "FS300100" || ((PXGraph) this.Base).Accessinfo.ScreenID.Replace(".", "") == "FS300200" || ((PXGraph) this.Base).Accessinfo.ScreenID.Replace(".", "") == "FS500600" || ((PXGraph) this.Base).Accessinfo.ScreenID.Replace(".", "") == "FS500100")
      return false;
    string orderNbr = ((PX.Objects.SO.SOOrder) document).OrderNbr;
    string orderType = ((PX.Objects.SO.SOOrder) document).OrderType;
    int? lineNbr = ((PX.Objects.SO.SOLine) lineDoc).LineNbr;
    return ((IQueryable<PXResult<FSPostInfo>>) PXSelectBase<FSPostInfo, PXSelect<FSPostInfo, Where<FSPostInfo.sOOrderNbr, Equal<Required<FSPostInfo.sOOrderNbr>>, And<FSPostInfo.sOOrderType, Equal<Required<FSPostInfo.sOOrderType>>, And<FSPostInfo.sOLineNbr, Equal<Required<FSPostInfo.sOLineNbr>>, And<FSPostInfo.sOPosted, Equal<True>>>>>>.Config>.Select(cleanerGraph, new object[3]
    {
      (object) orderNbr,
      (object) orderType,
      (object) lineNbr
    })).Count<PXResult<FSPostInfo>>() > 0;
  }

  public virtual FSContractPostDoc CreateInvoiceByContract(
    PXGraph graphProcess,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    FSContractPostBatch fsContractPostBatchRow,
    FSServiceContract fsServiceContractRow,
    FSContractPeriod fsContractPeriodRow,
    List<ContractInvoiceLine> docLines)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SM_SOOrderEntry.\u003C\u003Ec__DisplayClass74_0 cDisplayClass740 = new SM_SOOrderEntry.\u003C\u003Ec__DisplayClass74_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass740.fsServiceContractRow = fsServiceContractRow;
    if (docLines.Count == 0)
      return (FSContractPostDoc) null;
    FSSetup serviceManagementSetup = ServiceManagementSetup.GetServiceManagementSetup(graphProcess);
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass740, __methodptr(\u003CCreateInvoiceByContract\u003Eb__0));
    PX.Objects.SO.SOOrder data1 = (PX.Objects.SO.SOOrder) null;
    try
    {
      ((PXGraph) this.Base).FieldDefaulting.AddHandler<PX.Objects.SO.SOOrder.branchID>(pxFieldDefaulting);
      // ISSUE: reference to a compiler-generated field
      FSBranchLocation.PK.Find(graphProcess, cDisplayClass740.fsServiceContractRow.BranchLocationID);
      ((PXSelectBase) this.Base.Document).Cache.Clear();
      ((PXSelectBase) this.Base.Document).Cache.ClearQueryCacheObsolete();
      ((PXSelectBase) this.Base.Document).View.Clear();
      data1 = new PX.Objects.SO.SOOrder();
      data1.OrderType = serviceManagementSetup.ContractPostOrderType;
      data1.InclCustOpenOrders = new bool?(true);
      data1.Hold = new bool?(true);
      this.CheckAutoNumbering(((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).SelectSingle(new object[1]
      {
        (object) data1.OrderType
      }).OrderNumberingID);
      data1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Insert(data1);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.orderDate>((object) data1, (object) invoiceDate);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.requestDate>((object) data1, (object) invoiceDate);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.customerID>((object) data1, (object) cDisplayClass740.fsServiceContractRow.BillCustomerID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.customerLocationID>((object) data1, (object) cDisplayClass740.fsServiceContractRow.BillLocationID);
      // ISSUE: reference to a compiler-generated field
      string localizedLabel = PXStringListAttribute.GetLocalizedLabel<FSServiceContract.billingType>(graphProcess.Caches[typeof (FSServiceContract)], (object) cDisplayClass740.fsServiceContractRow);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.orderDesc>((object) data1, (object) PXMessages.LocalizeFormatNoPrefix("{0} Contract: {1} {2}", new object[3]
      {
        (object) localizedLabel,
        (object) cDisplayClass740.fsServiceContractRow.RefNbr,
        string.IsNullOrEmpty(cDisplayClass740.fsServiceContractRow.DocDesc) ? (object) string.Empty : (object) cDisplayClass740.fsServiceContractRow.DocDesc
      }));
      // ISSUE: reference to a compiler-generated field
      string customerOrVendor = this.GetTermsIDFromCustomerOrVendor((PXGraph) this.Base, cDisplayClass740.fsServiceContractRow.BillCustomerID, new int?());
      if ((!(data1.ARDocType == "CRM") ? 1 : (((PXSelectBase<ARSetup>) this.Base.arsetup).Current.TermsInCreditMemos.GetValueOrDefault() ? 1 : 0)) != 0)
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.termsID>((object) data1, (object) (customerOrVendor ?? serviceManagementSetup.DfltContractTermIDARSO));
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.ownerID>((object) data1, (object) null);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.projectID>((object) data1, (object) cDisplayClass740.fsServiceContractRow.ProjectID);
      data1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(data1);
      ((PXSelectBase) this.Base.Transactions).Cache.Clear();
      ((PXSelectBase) this.Base.Transactions).Cache.ClearQueryCacheObsolete();
      ((PXSelectBase) this.Base.Transactions).View.Clear();
      PX.Objects.SO.SOLine soLine1 = (PX.Objects.SO.SOLine) null;
      List<SharedClasses.SOARLineEquipmentComponent> source = new List<SharedClasses.SOARLineEquipmentComponent>();
      foreach (ContractInvoiceLine docLine in docLines)
      {
        PX.Objects.SO.SOLine data2 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Current = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Insert(new PX.Objects.SO.SOLine());
        FSSODet fsSoDet = docLine.fsSODet;
        FSAppointmentDet fsAppointmentDet1 = docLine.fsAppointmentDet;
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.inventoryID>((object) data2, (object) docLine.InventoryID);
        int? nullable1 = data2.SubItemID;
        if (!nullable1.HasValue)
        {
          nullable1 = docLine.SubItemID;
          if (nullable1.HasValue && data2.LineType == "GI" && PXAccess.FeatureInstalled<FeaturesSet.subItem>())
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.subItemID>((object) data2, (object) docLine.SubItemID);
        }
        nullable1 = docLine.SiteID;
        if (nullable1.HasValue)
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.siteID>((object) data2, (object) docLine.SiteID);
        nullable1 = docLine.SiteLocationID;
        if (nullable1.HasValue)
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.locationID>((object) data2, (object) docLine.SiteLocationID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.uOM>((object) data2, (object) docLine.UOM);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.salesPersonID>((object) data2, (object) docLine.SalesPersonID);
        PX.Objects.SO.SOLine soLine2 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(data2);
        nullable1 = docLine.AcctID;
        int? newValue1;
        if (nullable1.HasValue)
        {
          newValue1 = docLine.AcctID;
        }
        else
        {
          PXGraph graph = graphProcess;
          string contractSalesAcctSource = serviceManagementSetup.ContractSalesAcctSource;
          int? inventoryId = docLine.InventoryID;
          // ISSUE: reference to a compiler-generated field
          FSServiceContract serviceContractRow1 = cDisplayClass740.fsServiceContractRow;
          int? customerID;
          if (serviceContractRow1 == null)
          {
            nullable1 = new int?();
            customerID = nullable1;
          }
          else
            customerID = serviceContractRow1.CustomerID;
          // ISSUE: reference to a compiler-generated field
          FSServiceContract serviceContractRow2 = cDisplayClass740.fsServiceContractRow;
          int? locationID;
          if (serviceContractRow2 == null)
          {
            nullable1 = new int?();
            locationID = nullable1;
          }
          else
            locationID = serviceContractRow2.CustomerLocationID;
          newValue1 = this.Get_INItemAcctID_DefaultValue(graph, contractSalesAcctSource, inventoryId, customerID, locationID);
        }
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.salesAcctID>((object) soLine2, (object) newValue1);
        nullable1 = docLine.SubID;
        if (nullable1.HasValue)
        {
          try
          {
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.salesSubID>((object) soLine2, (object) docLine.SubID);
          }
          catch (PXException ex)
          {
            soLine2.SalesSubID = new int?();
          }
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.SetCombinedSubID((PXGraph) this.Base, ((PXSelectBase) this.Base.Transactions).Cache, (PX.Objects.AR.ARTran) null, (APTran) null, soLine2, serviceManagementSetup, soLine2.BranchID, soLine2.InventoryID, cDisplayClass740.fsServiceContractRow.BillLocationID, cDisplayClass740.fsServiceContractRow.BranchLocationID);
        }
        bool flag1 = SharedFunctions.IsLotSerialRequired(((PXSelectBase) this.Base.Transactions).Cache, soLine2.InventoryID);
        bool flag2 = false;
        bool? nullable2;
        if (flag1)
        {
          nullable1 = docLine.AppDetID;
          if (!nullable1.HasValue)
          {
            foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSSODetSplit.lineNbr>>>>>, OrderBy<Asc<FSSODetSplit.splitLineNbr>>>.Config>.Select((PXGraph) this.Base, new object[3]
            {
              (object) fsSoDet.SrvOrdType,
              (object) fsSoDet.RefNbr,
              (object) fsSoDet.LineNbr
            }))
            {
              FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
              nullable2 = fssoDetSplit.POCreate;
              bool flag3 = false;
              if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue && !string.IsNullOrEmpty(fssoDetSplit.LotSerialNbr))
              {
                PX.Objects.SO.SOLineSplit copy = (PX.Objects.SO.SOLineSplit) ((PXSelectBase) this.Base.splits).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Insert(new PX.Objects.SO.SOLineSplit()));
                PX.Objects.SO.SOLineSplit soLineSplit1 = copy;
                nullable1 = fssoDetSplit.SiteID;
                int? nullable3 = nullable1.HasValue ? fssoDetSplit.SiteID : copy.SiteID;
                soLineSplit1.SiteID = nullable3;
                PX.Objects.SO.SOLineSplit soLineSplit2 = copy;
                nullable1 = fssoDetSplit.LocationID;
                int? nullable4 = nullable1.HasValue ? fssoDetSplit.LocationID : copy.LocationID;
                soLineSplit2.LocationID = nullable4;
                copy.LotSerialNbr = fssoDetSplit.LotSerialNbr;
                copy.Qty = fssoDetSplit.Qty;
                ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(copy);
                flag2 = true;
              }
            }
            soLine2 = (PX.Objects.SO.SOLine) ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) soLine2);
            goto label_49;
          }
        }
        if (flag1)
        {
          nullable1 = docLine.AppDetID;
          if (nullable1.HasValue)
          {
            foreach (PXResult<FSApptLineSplit> pxResult in PXSelectBase<FSApptLineSplit, PXSelect<FSApptLineSplit, Where<FSApptLineSplit.srvOrdType, Equal<Required<FSApptLineSplit.srvOrdType>>, And<FSApptLineSplit.apptNbr, Equal<Required<FSApptLineSplit.apptNbr>>, And<FSApptLineSplit.lineNbr, Equal<Required<FSApptLineSplit.lineNbr>>>>>, OrderBy<Asc<FSApptLineSplit.splitLineNbr>>>.Config>.Select((PXGraph) this.Base, new object[3]
            {
              (object) fsAppointmentDet1.SrvOrdType,
              (object) fsAppointmentDet1.RefNbr,
              (object) fsAppointmentDet1.LineNbr
            }))
            {
              FSApptLineSplit fsApptLineSplit = PXResult<FSApptLineSplit>.op_Implicit(pxResult);
              if (!string.IsNullOrEmpty(fsApptLineSplit.LotSerialNbr))
              {
                PX.Objects.SO.SOLineSplit copy = (PX.Objects.SO.SOLineSplit) ((PXSelectBase) this.Base.splits).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Insert(new PX.Objects.SO.SOLineSplit()));
                PX.Objects.SO.SOLineSplit soLineSplit3 = copy;
                nullable1 = fsApptLineSplit.SiteID;
                int? nullable5 = nullable1 ?? copy.SiteID;
                soLineSplit3.SiteID = nullable5;
                PX.Objects.SO.SOLineSplit soLineSplit4 = copy;
                nullable1 = fsApptLineSplit.LocationID;
                int? nullable6 = nullable1 ?? copy.LocationID;
                soLineSplit4.LocationID = nullable6;
                PX.Objects.SO.SOLineSplit soLineSplit5 = copy;
                nullable1 = fsApptLineSplit.CostCenterID;
                int? nullable7 = nullable1 ?? copy.CostCenterID;
                soLineSplit5.CostCenterID = nullable7;
                copy.LotSerialNbr = fsApptLineSplit.LotSerialNbr;
                copy.Qty = fsApptLineSplit.Qty;
                ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(copy);
                flag2 = true;
              }
            }
            soLine2 = (PX.Objects.SO.SOLine) ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) soLine2);
          }
        }
label_49:
        if (!flag2)
        {
          soLine2.OrderQty = docLine.Qty;
        }
        else
        {
          Decimal? orderQty = soLine2.OrderQty;
          Decimal? qty = docLine.Qty;
          if (!(orderQty.GetValueOrDefault() == qty.GetValueOrDefault() & orderQty.HasValue == qty.HasValue))
            throw new PXException("The quantity in the posted document does not match the quantity in the source document.");
        }
        PXCache cache1 = ((PXSelectBase) this.Base.Transactions).Cache;
        PX.Objects.SO.SOLine data3 = soLine2;
        nullable2 = docLine.IsFree;
        // ISSUE: variable of a boxed type
        __Boxed<bool> valueOrDefault1 = (ValueType) nullable2.GetValueOrDefault();
        cache1.SetValueExtIfDifferent<PX.Objects.SO.SOLine.isFree>((object) data3, (object) valueOrDefault1);
        nullable2 = docLine.IsFree;
        if (nullable2.GetValueOrDefault())
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.manualPrice>((object) soLine2, (object) true);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.curyUnitPrice>((object) soLine2, (object) 0M);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.manualDisc>((object) soLine2, (object) true);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.discPct>((object) soLine2, (object) 0M);
        }
        else
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.manualPrice>((object) soLine2, (object) docLine.ManualPrice);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.curyUnitPrice>((object) soLine2, (object) docLine.CuryUnitPrice);
          bool newValue2 = false;
          if (fsAppointmentDet1 != null)
          {
            nullable2 = fsAppointmentDet1.ManualDisc;
            newValue2 = nullable2.Value;
          }
          else if (fsSoDet != null)
          {
            nullable2 = fsSoDet.ManualDisc;
            newValue2 = nullable2.Value;
          }
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.manualDisc>((object) soLine2, (object) newValue2);
          if (newValue2)
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.discPct>((object) soLine2, (object) docLine.DiscPct);
        }
        nullable1 = docLine.ServiceContractID;
        if (nullable1.HasValue)
        {
          nullable2 = docLine.ContractRelated;
          bool flag4 = false;
          if (nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue)
          {
            nullable1 = docLine.SODetID;
            if (!nullable1.HasValue)
            {
              nullable1 = docLine.AppDetID;
              if (!nullable1.HasValue)
                goto label_66;
            }
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.curyExtPrice>((object) soLine2, (object) docLine.CuryBillableExtPrice);
          }
        }
label_66:
        PX.Objects.SO.SOLine soLine3 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine2);
        PXCache cache2 = ((PXSelectBase) this.Base.Transactions).Cache;
        PX.Objects.SO.SOLine data4 = soLine3;
        nullable2 = docLine.Commissionable;
        // ISSUE: variable of a boxed type
        __Boxed<bool> valueOrDefault2 = (ValueType) nullable2.GetValueOrDefault();
        cache2.SetValueExtIfDifferent<PX.Objects.SO.SOLine.commissionable>((object) data4, (object) valueOrDefault2);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.tranDesc>((object) soLine3, (object) (docLine.TranDescPrefix + soLine3.TranDesc));
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.taskID>((object) soLine3, (object) docLine.ProjectTaskID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOLine.costCodeID>((object) soLine3, (object) docLine.CostCodeID);
        FSxSOLine extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxSOLine>((object) soLine3);
        extension.SDPosted = new bool?(true);
        // ISSUE: reference to a compiler-generated field
        extension.ServiceContractRefNbr = cDisplayClass740.fsServiceContractRow.RefNbr;
        extension.ServiceContractPeriodID = fsContractPeriodRow.ContractPeriodID;
        nullable2 = docLine.ContractRelated;
        bool flag5 = false;
        if (nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue)
        {
          extension.SrvOrdType = docLine.SrvOrdType;
          extension.ServiceOrderRefNbr = fsSoDet.RefNbr;
          extension.ServiceOrderLineNbr = fsSoDet.LineNbr;
          extension.AppointmentRefNbr = docLine.fsAppointmentDet?.RefNbr;
          FSxSOLine fsxSoLine = extension;
          FSAppointmentDet fsAppointmentDet2 = docLine.fsAppointmentDet;
          int? nullable8;
          if (fsAppointmentDet2 == null)
          {
            nullable1 = new int?();
            nullable8 = nullable1;
          }
          else
            nullable8 = fsAppointmentDet2.LineNbr;
          fsxSoLine.AppointmentLineNbr = nullable8;
        }
        if (PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() && docLine.EquipmentAction != null)
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<FSxSOLine.equipmentAction>((object) soLine3, (object) docLine.EquipmentAction);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<FSxSOLine.sMEquipmentID>((object) soLine3, (object) docLine.SMEquipmentID);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<FSxSOLine.equipmentComponentLineNbr>((object) soLine3, (object) docLine.EquipmentLineRef);
          if (docLine.EquipmentAction == "ST" || (docLine.EquipmentAction == "CC" || docLine.EquipmentAction == "UC" || docLine.EquipmentAction == "NO") && !string.IsNullOrEmpty(docLine.NewTargetEquipmentLineNbr))
            source.Add(new SharedClasses.SOARLineEquipmentComponent(docLine, soLine3, extension));
          else
            extension.ComponentID = docLine.ComponentID;
        }
        soLine1 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine3);
      }
      // ISSUE: reference to a compiler-generated field
      this.SetChildCustomerShipToInfo(cDisplayClass740.fsServiceContractRow.ServiceContractID);
      if (source.Count > 0)
      {
        foreach (SharedClasses.SOARLineEquipmentComponent equipmentComponent1 in source.Where<SharedClasses.SOARLineEquipmentComponent>((Func<SharedClasses.SOARLineEquipmentComponent, bool>) (x => x.equipmentAction == "ST")))
        {
          foreach (SharedClasses.SOARLineEquipmentComponent equipmentComponent2 in source.Where<SharedClasses.SOARLineEquipmentComponent>((Func<SharedClasses.SOARLineEquipmentComponent, bool>) (x => x.equipmentAction == "CC" || x.equipmentAction == "UC" || x.equipmentAction == "NO")))
          {
            if (equipmentComponent2.sourceNewTargetEquipmentLineNbr == equipmentComponent1.sourceLineRef)
            {
              equipmentComponent2.fsxSOLineRow.ComponentID = equipmentComponent2.componentID;
              equipmentComponent2.fsxSOLineRow.NewEquipmentLineNbr = equipmentComponent1.currentLineRef;
            }
          }
        }
      }
      if (((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.RequireControlTotal.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.curyControlTotal>((object) data1, (object) data1.CuryOrderTotal);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.SO.SOOrder.hold>((object) data1, (object) false);
      data1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(data1);
      data1.Status = "N";
    }
    finally
    {
      ((PXGraph) this.Base).FieldDefaulting.RemoveHandler<PX.Objects.SO.SOOrder.branchID>(pxFieldDefaulting);
    }
    Exception exception = (Exception) null;
    try
    {
      ((PXAction) this.Base.Save).Press();
    }
    catch (Exception ex)
    {
      exception = this.GetErrorInfoInLines(this.GetErrorInfo(), ex);
    }
    if (exception != null)
      throw exception;
    // ISSUE: reference to a compiler-generated field
    return new FSContractPostDoc()
    {
      ContractPeriodID = fsContractPeriodRow.ContractPeriodID,
      ContractPostBatchID = fsContractPostBatchRow.ContractPostBatchID,
      PostDocType = data1.OrderType,
      PostedTO = "SO",
      PostRefNbr = data1.OrderNbr,
      ServiceContractID = cDisplayClass740.fsServiceContractRow.ServiceContractID
    };
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<PX.Objects.SO.SOOrder>((PXGraph) this.Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }

  public virtual int? Get_INItemAcctID_DefaultValue(
    PXGraph graph,
    string salesAcctSource,
    int? inventoryID,
    int? customerID,
    int? locationID)
  {
    return ServiceOrderEntry.Get_INItemAcctID_DefaultValueInt(graph, salesAcctSource, inventoryID, customerID, locationID);
  }

  protected virtual bool IsRunningServiceContractBilling(PXGraph graph)
  {
    return InvoiceHelper.IsRunningServiceContractBilling(graph);
  }

  protected virtual void GetChildCustomerShippingContactAndAddress(
    PXGraph graph,
    int? serviceContractID,
    out PX.Objects.CR.Contact shippingContact,
    out PX.Objects.CR.Address shippingAddress)
  {
    InvoiceHelper.GetChildCustomerShippingContactAndAddress(graph, serviceContractID, out shippingContact, out shippingAddress);
  }

  private void UpdatePostingInformation(PX.Objects.SO.SOOrder soRow)
  {
    FSAllocationProcess.ReallocateServiceOrderSplits(FSAllocationProcess.GetRequiredAllocationList((PXGraph) this.Base, (object) soRow));
    this.CleanPostingInfoLinkedToDoc((object) soRow);
    this.CleanContractPostingInfoLinkedToDoc((object) soRow);
  }

  public virtual void SetChildCustomerShipToInfo(int? serviceContractID)
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null || !this.IsRunningServiceContractBilling((PXGraph) this.Base) || ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current) != 2)
      return;
    PX.Objects.CR.Contact shippingContact = (PX.Objects.CR.Contact) null;
    PX.Objects.CR.Address shippingAddress = (PX.Objects.CR.Address) null;
    this.GetChildCustomerShippingContactAndAddress((PXGraph) this.Base, serviceContractID, out shippingContact, out shippingAddress);
    if (shippingContact != null)
    {
      SOShippingContact soShippingContact;
      ((PXSelectBase<SOShippingContact>) this.Base.Shipping_Contact).Current = soShippingContact = ((PXSelectBase<SOShippingContact>) this.Base.Shipping_Contact).SelectSingle(Array.Empty<object>());
      if (soShippingContact != null)
      {
        ((PXSelectBase) this.Base.Shipping_Contact).Cache.SetValueExt<SOShippingContact.overrideContact>((object) soShippingContact, (object) true);
        SOShippingContact copy = (SOShippingContact) ((PXSelectBase) this.Base.Shipping_Contact).Cache.CreateCopy((object) ((PXSelectBase<SOShippingContact>) this.Base.Shipping_Contact).SelectSingle(Array.Empty<object>()));
        int? customerId = copy.CustomerID;
        int? customerContactId = copy.CustomerContactID;
        InvoiceHelper.CopyContact((IContact) copy, (IContact) shippingContact);
        copy.CustomerID = customerId;
        copy.CustomerContactID = customerContactId;
        copy.RevisionID = new int?(0);
        copy.IsDefaultContact = new bool?(false);
        ((PXSelectBase<SOShippingContact>) this.Base.Shipping_Contact).Update(copy);
      }
    }
    if (shippingAddress == null)
      return;
    SOShippingAddress soShippingAddress;
    ((PXSelectBase<SOShippingAddress>) this.Base.Shipping_Address).Current = soShippingAddress = ((PXSelectBase<SOShippingAddress>) this.Base.Shipping_Address).SelectSingle(Array.Empty<object>());
    if (soShippingAddress == null)
      return;
    ((PXSelectBase) this.Base.Shipping_Address).Cache.SetValueExt<SOShippingAddress.overrideAddress>((object) soShippingAddress, (object) true);
    SOShippingAddress copy1 = (SOShippingAddress) ((PXSelectBase) this.Base.Shipping_Address).Cache.CreateCopy((object) ((PXSelectBase<SOShippingAddress>) this.Base.Shipping_Address).SelectSingle(Array.Empty<object>()));
    int? customerId1 = copy1.CustomerID;
    int? customerAddressId = copy1.CustomerAddressID;
    InvoiceHelper.CopyAddress((IAddress) copy1, shippingAddress);
    copy1.CustomerID = customerId1;
    copy1.CustomerAddressID = customerAddressId;
    copy1.RevisionID = new int?(0);
    copy1.IsDefaultAddress = new bool?(false);
    ((PXSelectBase<SOShippingAddress>) this.Base.Shipping_Address).Update(copy1);
  }

  public class WorkflowChanges : PXGraphExtension<ScreenConfiguration, SOOrderEntry>
  {
    public static bool IsActive() => SM_SOOrderEntry.IsActive();

    public virtual void Configure(PXScreenConfiguration config)
    {
      SM_SOOrderEntry.WorkflowChanges.Configure(config.GetScreenConfigurationContext<SOOrderEntry, PX.Objects.SO.SOOrder>());
    }

    protected static void Configure(WorkflowContext<SOOrderEntry, PX.Objects.SO.SOOrder> context)
    {
      SM_SOOrderEntry.WorkflowChanges.Conditions conditions = context.Conditions.GetPack<SM_SOOrderEntry.WorkflowChanges.Conditions>();
      BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionCategory.IConfigured servicesCategory = context.Categories.CreateNew("Services Category", (Func<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionCategory.IConfigured) category.DisplayName("Services").PlaceAfter("Replenishment Category")));
      context.UpdateScreenConfigurationFor((Func<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.ContainerAdjusterActions>) (actions =>
      {
        actions.Add<SM_SOOrderEntry>((Expression<Func<SM_SOOrderEntry, PXAction<PX.Objects.SO.SOOrder>>>) (g => g.CreateServiceOrder), (Func<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IConfigured) a.WithCategory(servicesCategory).IsHiddenWhen((BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ISharedCondition) BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.Condition.op_LogicalNot(conditions.IsFSActivatedForOrderType))));
        actions.Add<SM_SOOrderEntry>((Expression<Func<SM_SOOrderEntry, PXAction<PX.Objects.SO.SOOrder>>>) (g => g.OpenAppointmentBoard), (Func<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IConfigured) a.WithCategory(servicesCategory).IsHiddenWhen((BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ISharedCondition) BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.Condition.op_LogicalNot(conditions.IsFSActivatedForOrderType))));
        actions.Add<SM_SOOrderEntry>((Expression<Func<SM_SOOrderEntry, PXAction<PX.Objects.SO.SOOrder>>>) (g => g.ViewServiceOrder), (Func<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1).IsHiddenWhen((BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ISharedCondition) BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.Condition.op_LogicalNot(conditions.IsFSActivatedForOrderType))));
      })).WithCategories((Action<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionCategory.ContainerAdjusterCategories>) (categories => categories.Add(servicesCategory)))));
    }

    public class Conditions : BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.Condition.Pack
    {
      public BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.Condition IsFSActivatedForOrderType
      {
        get
        {
          return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.Condition>) (b => b.FromBql<BqlOperand<Current<FSxSOOrderType.enableFSIntegration>, IBqlBool>.IsEqual<True>>()), nameof (IsFSActivatedForOrderType));
        }
      }
    }

    public static class ActionCategories
    {
      public const string ServicesCategoryID = "Services Category";

      [PXLocalizable]
      public static class DisplayNames
      {
        public const string Services = "Services";
      }
    }
  }

  public delegate void InvoiceOrderDelegate(
    Dictionary<string, object> parameters,
    IEnumerable<PX.Objects.SO.SOOrder> list,
    InvoiceList created,
    bool isMassProcess,
    PXQuickProcess.ActionFlow quickProcessFlow,
    bool groupByCustomerOrderNumber);
}
