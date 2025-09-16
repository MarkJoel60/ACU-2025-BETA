// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CloneAppointmentProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class CloneAppointmentProcess : PXGraph<CloneAppointmentProcess>
{
  private PXGraph ReadingGraph;
  public PXFilter<FSCloneAppointmentFilter> Filter;
  public PXAction<FSCloneAppointmentFilter> cancel;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXSelectJoin<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>>, Where<FSAppointment.srvOrdType, Equal<Current<FSCloneAppointmentFilter.srvOrdType>>, And<FSAppointment.refNbr, Equal<Current<FSCloneAppointmentFilter.refNbr>>>>> AppointmentSelected;
  public PXSelectReadonly<FSAppointmentFSServiceOrder, Where<FSAppointment.originalAppointmentID, Equal<Current<FSAppointment.appointmentID>>>, OrderBy<Desc<FSAppointmentFSServiceOrder.appointmentID>>> AppointmentClones;
  public AppointmentEntry.ServiceOrderRelated_View ServiceOrderRelated;
  public PXAction<FSCloneAppointmentFilter> OpenAppointment;
  public PXAction<FSCloneAppointmentFilter> clone;

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  protected virtual void FSAppointment_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  protected virtual void FSAppointment_SORefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(4, IsFixed = true, IsKey = true)]
  [PXUIField(DisplayName = "Service Order Type", Enabled = false)]
  protected virtual void FSAppointment_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Confirmed", Enabled = false)]
  protected virtual void FSAppointment_Confirmed_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected void openAppointment()
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    if (((PXSelectBase<FSAppointmentFSServiceOrder>) this.AppointmentClones).Current != null && ((PXSelectBase<FSAppointmentFSServiceOrder>) this.AppointmentClones).Current.RefNbr != null)
    {
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) ((PXSelectBase<FSAppointmentFSServiceOrder>) this.AppointmentClones).Current.RefNbr, new object[1]
      {
        (object) ((PXSelectBase<FSAppointmentFSServiceOrder>) this.AppointmentClones).Current.SrvOrdType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Clone(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CloneAppointmentProcess.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new CloneAppointmentProcess.\u003C\u003Ec__DisplayClass15_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.\u003C\u003E4__this = this;
    if (((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSAppointment))
      });
    bool flag = false;
    if (((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.CloningType == "MU")
    {
      DateTime? nullable = ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.MultGenerationFromDate;
      if (!nullable.HasValue)
      {
        this.FieldCannotBeEmptyError<FSCloneAppointmentFilter.multGenerationFromDate>(((PXSelectBase) this.Filter).Cache, (object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current);
        flag = true;
      }
      nullable = ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.MultGenerationToDate;
      if (!nullable.HasValue)
      {
        this.FieldCannotBeEmptyError<FSCloneAppointmentFilter.multGenerationToDate>(((PXSelectBase) this.Filter).Cache, (object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current);
        flag = true;
      }
      if (!flag)
      {
        nullable = ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.MultGenerationFromDate;
        DateTime? generationToDate = ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.MultGenerationToDate;
        if ((nullable.HasValue & generationToDate.HasValue ? (nullable.GetValueOrDefault() > generationToDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<FSCloneAppointmentFilter.multGenerationToDate>((object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current, (object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.MultGenerationToDate, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("The dates are invalid. The end date cannot be earlier than the start date.", Array.Empty<object>()), (PXErrorLevel) 4));
          flag = true;
        }
      }
    }
    else if (!((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.SingleGenerationDate.HasValue)
    {
      this.FieldCannotBeEmptyError<FSCloneAppointmentFilter.singleGenerationDate>(((PXSelectBase) this.Filter).Cache, (object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current);
      flag = true;
    }
    if (flag)
      return adapter.Get();
    FSServiceOrder fsServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelectJoin<FSServiceOrder, InnerJoin<FSAppointment, On<FSAppointment.sOID, Equal<FSServiceOrder.sOID>>>, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.AppointmentID
    }));
    if (fsServiceOrder == null)
      return adapter.Get();
    if (fsServiceOrder != null && fsServiceOrder.Completed.GetValueOrDefault())
      throw new PXException("The appointment cannot be cloned because the associated service order has been completed.");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.fsCloneAppointmentFilterRow = (FSCloneAppointmentFilter) ((PXSelectBase) this.Filter).Cache.CreateCopy((object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass150, __methodptr(\u003CClone\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<FSCloneAppointmentFilter.scheduledStartTime>((object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current);
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<FSCloneAppointmentFilter.overrideApptDuration>((object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current);
    return (IEnumerable) new object[1]
    {
      (object) ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current
    };
  }

  public virtual void CloneAppointment(
    AppointmentEntry gOriginalAppt,
    AppointmentEntry gNewAppt,
    DateTime newApptBeginDate,
    int newApptDuration)
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return;
    ((PXGraph) gNewAppt).Clear((PXClearOption) 3);
    gNewAppt.ClearServiceOrderEntry();
    FSAppointment copy = PXCache<FSAppointment>.CreateCopy(((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current);
    Dictionary<string, string> itemLineRef = new Dictionary<string, string>();
    copy.RefNbr = (string) null;
    copy.AppointmentID = new int?();
    copy.NoteID = new Guid?();
    copy.CuryInfoID = new long?();
    copy.FullNameSignature = (string) null;
    copy.customerSignaturePath = (string) null;
    FSAppointment fsAppointment1 = copy;
    bool? nullable1 = copy.HandleManuallyScheduleTime;
    int num;
    if (!nullable1.Value)
    {
      nullable1 = ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.OverrideApptDuration;
      num = nullable1.Value ? 1 : 0;
    }
    else
      num = 1;
    bool? nullable2 = new bool?(num != 0);
    fsAppointment1.HandleManuallyScheduleTime = nullable2;
    copy.HandleManuallyActualTime = new bool?(false);
    copy.LogLineCntr = new int?(0);
    copy.StaffCntr = new int?(0);
    copy.FinPeriodID = (string) null;
    copy.PostingStatusAPARSO = "PP";
    copy.PendingAPARSOPost = new bool?(true);
    copy.ActualDateTimeBegin = new DateTime?();
    copy.ActualDateTimeEnd = new DateTime?();
    copy.MinLogTimeBegin = new DateTime?();
    copy.MaxLogTimeEnd = new DateTime?();
    copy.NotStarted = new bool?(false);
    copy.Awaiting = new bool?(false);
    copy.InProcess = new bool?(false);
    copy.Paused = new bool?(false);
    copy.Hold = new bool?(false);
    copy.Completed = new bool?(false);
    copy.Closed = new bool?(false);
    copy.Billed = new bool?(false);
    copy.OriginalAppointmentID = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.AppointmentID;
    copy.ScheduledDateTimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(new DateTime?(newApptBeginDate), ((PXSelectBase<FSCloneAppointmentFilter>) this.Filter).Current.ScheduledStartTime);
    DateTime? nullable3;
    ref DateTime? local = ref nullable3;
    DateTime? scheduledDateTimeBegin = copy.ScheduledDateTimeBegin;
    DateTime dateTime = scheduledDateTimeBegin.Value.AddMinutes((double) newApptDuration);
    local = new DateTime?(dateTime);
    FSAppointment fsAppointment2 = copy;
    scheduledDateTimeBegin = copy.ScheduledDateTimeBegin;
    DateTime? nullable4 = new DateTime?(scheduledDateTimeBegin.Value.Date);
    fsAppointment2.ExecutionDate = nullable4;
    copy.Hold = new bool?(false);
    copy.EstimatedDurationTotal = new int?(0);
    copy.ActualDurationTotal = new int?(0);
    copy.CuryEstimatedLineTotal = new Decimal?(0M);
    copy.CuryLineTotal = new Decimal?(0M);
    copy.CuryBillableLineTotal = new Decimal?(0M);
    copy.CuryCostTotal = new Decimal?(0M);
    copy.CuryEstimatedCostTotal = new Decimal?(0M);
    copy.EstimatedLineTotal = new Decimal?(0M);
    copy.LineTotal = new Decimal?(0M);
    copy.BillableLineTotal = new Decimal?(0M);
    copy.CostTotal = new Decimal?(0M);
    copy.LineCntr = new int?(0);
    copy.EstimatedCostTotal = new Decimal?(0M);
    gNewAppt.IsCloningAppointment = true;
    FSAppointment fsAppointment3 = ((PXSelectBase<FSAppointment>) gNewAppt.AppointmentRecords).Insert(copy);
    gNewAppt.Answers.Current = PXResultset<CSAnswers>.op_Implicit(gNewAppt.Answers.Select(Array.Empty<object>()));
    gNewAppt.Answers.CopyAllAttributes((object) ((PXSelectBase<FSAppointment>) gNewAppt.AppointmentRecords).Current, (object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current);
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.AppointmentSelected).Cache, (object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current, ((PXSelectBase) gNewAppt.AppointmentSelected).Cache, (object) fsAppointment3, new bool?(true), new bool?(false));
    foreach (FSAppointmentDet sourceRow in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) gOriginalAppt.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (row => string.IsNullOrEmpty(row.LinkedEntityType) && string.IsNullOrEmpty(row.LinkedDocRefNbr))))
    {
      if (sourceRow.IsInventoryItem)
        this.CloneParts(gOriginalAppt, gNewAppt, sourceRow);
      else if (!sourceRow.IsInventoryItem && !sourceRow.IsPickupDelivery)
        this.CloneServices(gOriginalAppt, gNewAppt, sourceRow, itemLineRef);
    }
    this.CloneStaff(gOriginalAppt, gNewAppt, itemLineRef);
    this.CloneResources(gOriginalAppt, gNewAppt);
    DateTime? scheduledDateTimeEnd = ((PXSelectBase<FSAppointment>) gNewAppt.AppointmentRecords).Current.ScheduledDateTimeEnd;
    DateTime? nullable5 = nullable3;
    if ((scheduledDateTimeEnd.HasValue == nullable5.HasValue ? (scheduledDateTimeEnd.HasValue ? (scheduledDateTimeEnd.GetValueOrDefault() != nullable5.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      bool? manuallyScheduleTime = fsAppointment3.HandleManuallyScheduleTime;
      bool flag = false;
      if (manuallyScheduleTime.GetValueOrDefault() == flag & manuallyScheduleTime.HasValue)
        ((PXSelectBase) gNewAppt.AppointmentRecords).Cache.SetValueExt<FSAppointment.handleManuallyScheduleTime>((object) fsAppointment3, (object) true);
      ((PXSelectBase) gNewAppt.AppointmentRecords).Cache.SetValueExt<FSAppointment.scheduledDateTimeEnd>((object) fsAppointment3, (object) nullable3);
    }
    ((PXSelectBase) gNewAppt.AppointmentRecords).Cache.SetDefaultExt<FSAppointment.billContractPeriodID>((object) fsAppointment3);
    ((PXAction) gNewAppt.Save).Press();
  }

  public virtual void ClearSourceLineBeforeCopy(FSAppointmentDet apptDet)
  {
    if (apptDet.Status == "NP")
    {
      bool? isFree = apptDet.IsFree;
      bool flag = false;
      if (isFree.GetValueOrDefault() == flag & isFree.HasValue && apptDet.InventoryID.HasValue)
        apptDet.IsBillable = new bool?(true);
    }
    apptDet.Status = "NS";
    apptDet.ActualDuration = new int?(0);
    apptDet.ActualQty = new Decimal?(0M);
    apptDet.CuryTranAmt = new Decimal?(0M);
    apptDet.CuryBillableExtPrice = new Decimal?(0M);
    apptDet.CuryBillableTranAmt = new Decimal?(0M);
    apptDet.PostID = new int?();
    apptDet.NoteID = new Guid?();
    apptDet.LogActualDuration = new int?(0);
  }

  public virtual void CloneServices(
    AppointmentEntry gOriginalAppt,
    AppointmentEntry gNewAppt,
    FSAppointmentDet sourceRow,
    Dictionary<string, string> itemLineRef)
  {
    if (sourceRow == null || sourceRow.Status == "CC")
      return;
    FSAppointmentDet copy = PXCache<FSAppointmentDet>.CreateCopy(sourceRow);
    this.ClearSourceLineBeforeCopy(copy);
    FSAppointmentDet objNewRow = new FSAppointmentDet();
    FSAppointmentDet fsAppointmentDet = AppointmentEntry.InsertDetailLine<FSAppointmentDet, FSAppointmentDet>(((PXSelectBase) gNewAppt.AppointmentDetails).Cache, (object) objNewRow, ((PXSelectBase) gOriginalAppt.AppointmentDetails).Cache, (object) copy, new Guid?(), copy.SODetID, false, copy.TranDate, true, false);
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) gOriginalAppt.AppointmentDetails).Cache, (object) sourceRow, ((PXSelectBase) gNewAppt.AppointmentDetails).Cache, (object) fsAppointmentDet, new bool?(true), new bool?(false));
    ((PXSelectBase<FSAppointmentDet>) gNewAppt.AppointmentDetails).SetValueExt<FSAppointmentDet.acctID>(fsAppointmentDet, (object) copy.AcctID);
    ((PXSelectBase<FSAppointmentDet>) gNewAppt.AppointmentDetails).SetValueExt<FSAppointmentDet.subID>(fsAppointmentDet, (object) copy.SubID);
    itemLineRef.Add(sourceRow.LineRef, fsAppointmentDet.LineRef);
  }

  public virtual void CloneParts(
    AppointmentEntry gOriginalAppt,
    AppointmentEntry gNewAppt,
    FSAppointmentDet sourceRow)
  {
    if (sourceRow == null || sourceRow.Status == "CC")
      return;
    FSSODet fromAppointmentDet = this.GetSODetFromAppointmentDet((PXGraph) gOriginalAppt, sourceRow);
    if (fromAppointmentDet == null)
      return;
    if (this.ReadingGraph == null)
      this.ReadingGraph = new PXGraph();
    FSAppointmentDet fsAppointmentDet1 = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelectJoinGroupBy<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSAppointmentDet.srvOrdType>, And<FSAppointment.refNbr, Equal<FSAppointmentDet.refNbr>>>>, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<FSAppointment.canceled, Equal<False>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>>>>, Aggregate<GroupBy<FSAppointmentDet.sODetID, Sum<FSAppointmentDet.billableQty>>>>.Config>.Select(this.ReadingGraph, new object[1]
    {
      (object) sourceRow.SODetID
    }));
    Decimal? billableQty = fromAppointmentDet.BillableQty;
    Decimal? nullable1 = (Decimal?) fsAppointmentDet1?.BillableQty;
    Decimal valueOrDefault = nullable1.GetValueOrDefault();
    Decimal? nullable2;
    if (!billableQty.HasValue)
    {
      nullable1 = new Decimal?();
      nullable2 = nullable1;
    }
    else
      nullable2 = new Decimal?(billableQty.GetValueOrDefault() - valueOrDefault);
    Decimal? nullable3 = nullable2;
    Decimal? nullable4 = nullable3;
    nullable1 = sourceRow.BillableQty;
    Decimal? nullable5;
    if (!(nullable4.GetValueOrDefault() >= nullable1.GetValueOrDefault() & nullable4.HasValue & nullable1.HasValue))
    {
      nullable1 = nullable3;
      Decimal num = 0M;
      nullable5 = nullable1.GetValueOrDefault() > num & nullable1.HasValue ? nullable3 : new Decimal?(0M);
    }
    else
      nullable5 = sourceRow.BillableQty;
    Decimal? nullable6 = nullable5;
    nullable1 = sourceRow.BillableQty;
    nullable4 = nullable6;
    Decimal? nullable7 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
    FSAppointmentDet fsAppointmentDet2 = (FSAppointmentDet) null;
    FSAppointmentDet copy = PXCache<FSAppointmentDet>.CreateCopy(sourceRow);
    this.ClearSourceLineBeforeCopy(copy);
    nullable4 = nullable6;
    Decimal num1 = 0M;
    if (nullable4.GetValueOrDefault() > num1 & nullable4.HasValue)
    {
      copy.EstimatedQty = nullable6;
      copy.BillableQty = nullable6;
      FSAppointmentDet objNewRow = new FSAppointmentDet();
      fsAppointmentDet2 = AppointmentEntry.InsertDetailLine<FSAppointmentDet, FSAppointmentDet>(((PXSelectBase) gNewAppt.AppointmentDetails).Cache, (object) objNewRow, ((PXSelectBase) gOriginalAppt.AppointmentDetails).Cache, (object) copy, new Guid?(), copy.SODetID, false, copy.TranDate, true, false);
    }
    nullable4 = nullable7;
    Decimal num2 = 0M;
    if (nullable4.GetValueOrDefault() > num2 & nullable4.HasValue)
    {
      copy.EstimatedQty = nullable7;
      copy.BillableQty = nullable7;
      FSAppointmentDet objNewRow = new FSAppointmentDet();
      FSAppointmentDet fsAppointmentDet3 = AppointmentEntry.InsertDetailLine<FSAppointmentDet, FSAppointmentDet>(((PXSelectBase) gNewAppt.AppointmentDetails).Cache, (object) objNewRow, ((PXSelectBase) gOriginalAppt.AppointmentDetails).Cache, (object) copy, new Guid?(), new int?(), false, copy.TranDate, true, false);
      if (fsAppointmentDet2 == null)
        fsAppointmentDet2 = fsAppointmentDet3;
    }
    if (fsAppointmentDet2 == null)
      return;
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) gOriginalAppt.AppointmentDetails).Cache, (object) sourceRow, ((PXSelectBase) gNewAppt.AppointmentDetails).Cache, (object) fsAppointmentDet2, new bool?(true), new bool?(false));
  }

  public virtual void CloneStaff(
    AppointmentEntry gOriginalAppt,
    AppointmentEntry gNewAppt,
    Dictionary<string, string> itemLineRef)
  {
    foreach (PXResult<FSAppointmentEmployee> pxResult in ((PXSelectBase<FSAppointmentEmployee>) gOriginalAppt.AppointmentServiceEmployees).Select(Array.Empty<object>()))
    {
      FSAppointmentEmployee copy = PXCache<FSAppointmentEmployee>.CreateCopy(PXResult<FSAppointmentEmployee>.op_Implicit(pxResult));
      copy.RefNbr = (string) null;
      copy.AppointmentID = ((PXSelectBase<FSAppointment>) gNewAppt.AppointmentSelected).Current.AppointmentID;
      copy.NoteID = new Guid?();
      if (!string.IsNullOrEmpty(copy.ServiceLineRef) && itemLineRef.ContainsKey(copy.ServiceLineRef))
        copy.ServiceLineRef = itemLineRef[copy.ServiceLineRef];
      ((PXSelectBase<FSAppointmentEmployee>) gNewAppt.AppointmentServiceEmployees).Insert(copy);
    }
  }

  public virtual void CloneResources(AppointmentEntry gOriginalAppt, AppointmentEntry gNewAppt)
  {
    foreach (PXResult<FSAppointmentResource> pxResult in ((PXSelectBase<FSAppointmentResource>) gOriginalAppt.AppointmentResources).Select(Array.Empty<object>()))
    {
      FSAppointmentResource copy = PXCache<FSAppointmentResource>.CreateCopy(PXResult<FSAppointmentResource>.op_Implicit(pxResult));
      copy.RefNbr = (string) null;
      copy.AppointmentID = ((PXSelectBase<FSAppointment>) gNewAppt.AppointmentSelected).Current.AppointmentID;
      ((PXSelectBase<FSAppointmentResource>) gNewAppt.AppointmentResources).Insert(copy);
    }
  }

  public virtual void FieldCannotBeEmptyError<Field>(PXCache cache, object row) where Field : IBqlField
  {
    cache.RaiseExceptionHandling<Field>(row, (object) null, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<Field>(cache)
    }), (PXErrorLevel) 4));
  }

  /// <summary>
  /// Check the ManageRooms value on Setup to check/hide the Rooms Values options.
  /// </summary>
  public virtual void HideRooms()
  {
    bool flag = ServiceManagementSetup.IsRoomManagementActive((PXGraph) this, ((PXSelectBase<FSSetup>) this.SetupRecord)?.Current);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.roomID>(((PXSelectBase) this.ServiceOrderRelated).Cache, (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).SelectSingle(Array.Empty<object>()), flag);
  }

  public virtual FSSODet GetSODetFromAppointmentDet(
    PXGraph graph,
    FSAppointmentDet fsAppointmentDetRow)
  {
    return AppointmentEntry.GetSODetFromAppointmentDetInt(graph, fsAppointmentDetRow);
  }

  protected virtual void _(Events.RowSelecting<FSCloneAppointmentFilter> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSCloneAppointmentFilter> e)
  {
    if (e.Row == null)
      return;
    this.HideRooms();
  }

  protected virtual void _(Events.RowInserting<FSCloneAppointmentFilter> e)
  {
  }

  protected virtual void _(Events.RowInserted<FSCloneAppointmentFilter> e)
  {
  }

  protected virtual void _(Events.RowUpdating<FSCloneAppointmentFilter> e)
  {
  }

  protected virtual void _(Events.RowUpdated<FSCloneAppointmentFilter> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSCloneAppointmentFilter> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSCloneAppointmentFilter> e)
  {
  }

  protected virtual void _(Events.RowPersisting<FSCloneAppointmentFilter> e)
  {
  }

  protected virtual void _(Events.RowPersisted<FSCloneAppointmentFilter> e)
  {
  }
}
