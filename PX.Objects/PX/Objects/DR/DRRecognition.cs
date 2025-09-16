// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRRecognition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.Objects.DR;

[TableAndChartDashboardType]
public class DRRecognition : PXGraph<
#nullable disable
DRRecognition>
{
  public PXCancel<DRRecognition.ScheduleRecognitionFilter> Cancel;
  public PXAction<DRRecognition.ScheduleRecognitionFilter> viewSchedule;
  public PXFilter<DRRecognition.ScheduleRecognitionFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<DRRecognition.ScheduledTran, DRRecognition.ScheduleRecognitionFilter> Items;
  public PXSetup<DRSetup> Setup;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public virtual IEnumerable items()
  {
    DRRecognition graph = this;
    DRRecognition.ScheduleRecognitionFilter filter = ((PXSelectBase<DRRecognition.ScheduleRecognitionFilter>) graph.Filter).Current;
    if (filter != null)
    {
      bool hasCashReceipt = ((IQueryable<PXResult<DRDeferredCode>>) PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.method, Equal<DeferredMethodType.cashReceipt>>>.Config>.Select((PXGraph) graph, Array.Empty<object>())).Any<PXResult<DRDeferredCode>>();
      if (hasCashReceipt)
      {
        bool found = false;
        foreach (DRRecognition.ScheduledTran scheduledTran in ((PXSelectBase) graph.Items).Cache.Inserted)
        {
          found = true;
          yield return (object) scheduledTran;
        }
        if (found)
          yield break;
      }
      PXSelectBase<DRRecognition.ScheduledTranRec> pxSelectBase1 = (PXSelectBase<DRRecognition.ScheduledTranRec>) new PXSelect<DRRecognition.ScheduledTranRec, Where<DRRecognition.ScheduledTranRec.recDate, LessEqual<Current<DRRecognition.ScheduleRecognitionFilter.recDate>>>, OrderBy<Asc<DRRecognition.ScheduledTranRec.scheduleID, Asc<DRRecognition.ScheduledTranRec.componentID, Asc<DRRecognition.ScheduledTranRec.detailLineNbr, Asc<DRRecognition.ScheduledTranRec.recDate, Asc<DRRecognition.ScheduledTranRec.lineNbr>>>>>>>((PXGraph) graph);
      if (!string.IsNullOrEmpty(filter.DeferredCode))
        pxSelectBase1.WhereAnd<Where<DRRecognition.ScheduledTranRec.defCode, Equal<Current<DRRecognition.ScheduleRecognitionFilter.deferredCode>>>>();
      if (filter.BranchID.HasValue)
        pxSelectBase1.WhereAnd<Where<DRRecognition.ScheduledTranRec.branchID, Equal<Current<DRRecognition.ScheduleRecognitionFilter.branchID>>>>();
      foreach (DRRecognition.ScheduledTranRec scheduledTranRec in hasCashReceipt ? (IEnumerable) ((PXSelectBase) pxSelectBase1).View.SelectMulti(Array.Empty<object>()) : GraphHelper.QuickSelect(((PXSelectBase) pxSelectBase1).View.Graph, ((PXSelectBase) pxSelectBase1).View.BqlSelect, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters)))
      {
        DRRecognition.ScheduledTran scheduledTran = new DRRecognition.ScheduledTran();
        scheduledTran.BranchID = scheduledTranRec.BranchID;
        scheduledTran.AccountID = scheduledTranRec.AccountID;
        scheduledTran.Amount = scheduledTranRec.Amount;
        scheduledTran.ComponentID = scheduledTranRec.ComponentID;
        scheduledTran.DetailLineNbr = scheduledTranRec.DetailLineNbr;
        scheduledTran.DefCode = scheduledTranRec.DefCode;
        scheduledTran.FinPeriodID = scheduledTranRec.FinPeriodID;
        scheduledTran.LineNbr = scheduledTranRec.LineNbr;
        scheduledTran.RecDate = scheduledTranRec.RecDate;
        scheduledTran.ScheduleID = scheduledTranRec.ScheduleID;
        scheduledTran.ScheduleNbr = scheduledTranRec.ScheduleNbr;
        scheduledTran.SubID = scheduledTranRec.SubID;
        scheduledTran.ComponentCD = scheduledTranRec.ComponentCD;
        scheduledTran.DocType = DRScheduleDocumentType.BuildDocumentType(scheduledTranRec.Module, scheduledTranRec.DocType);
        scheduledTran.BaseCuryID = scheduledTranRec.BaseCuryID;
        ((PXSelectBase) graph.Items).Cache.SetStatus((object) scheduledTran, (PXEntryStatus) 2);
        yield return (object) scheduledTran;
      }
      if (hasCashReceipt)
      {
        foreach (PXResult<PX.Objects.AR.ARInvoice> pxResult1 in (string.IsNullOrEmpty(filter.DeferredCode) ? (PXSelectBase<PX.Objects.AR.ARInvoice>) new PXSelectJoinGroupBy<PX.Objects.AR.ARInvoice, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.AR.ARTran.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>, InnerJoin<DRDeferredCode, On<PX.Objects.AR.ARTran.deferredCode, Equal<DRDeferredCode.deferredCodeID>, And<DRDeferredCode.method, Equal<DeferredMethodType.cashReceipt>>>, InnerJoin<DRSchedule, On<PX.Objects.AR.ARTran.tranType, Equal<DRSchedule.docType>, And<PX.Objects.AR.ARTran.refNbr, Equal<DRSchedule.refNbr>, And<PX.Objects.AR.ARTran.lineNbr, Equal<DRSchedule.lineNbr>>>>, InnerJoin<DRScheduleDetail, On<DRSchedule.scheduleID, Equal<DRScheduleDetail.scheduleID>>>>>>, Where<PX.Objects.AR.ARInvoice.released, Equal<True>, And<DRScheduleDetail.isOpen, Equal<True>>>, Aggregate<GroupBy<PX.Objects.AR.ARInvoice.docType, GroupBy<PX.Objects.AR.ARInvoice.refNbr>>>>((PXGraph) graph) : (PXSelectBase<PX.Objects.AR.ARInvoice>) new PXSelectJoinGroupBy<PX.Objects.AR.ARInvoice, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.AR.ARTran.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>, InnerJoin<DRDeferredCode, On<PX.Objects.AR.ARTran.deferredCode, Equal<DRDeferredCode.deferredCodeID>, And<DRDeferredCode.method, Equal<DeferredMethodType.cashReceipt>, And<DRDeferredCode.deferredCodeID, Equal<Current<DRRecognition.ScheduleRecognitionFilter.deferredCode>>>>>, InnerJoin<DRSchedule, On<PX.Objects.AR.ARTran.tranType, Equal<DRSchedule.docType>, And<PX.Objects.AR.ARTran.refNbr, Equal<DRSchedule.refNbr>, And<PX.Objects.AR.ARTran.lineNbr, Equal<DRSchedule.lineNbr>>>>, InnerJoin<DRScheduleDetail, On<DRSchedule.scheduleID, Equal<DRScheduleDetail.scheduleID>>>>>>, Where<PX.Objects.AR.ARInvoice.released, Equal<True>, And<DRScheduleDetail.isOpen, Equal<True>>>, Aggregate<GroupBy<PX.Objects.AR.ARInvoice.docType, GroupBy<PX.Objects.AR.ARInvoice.refNbr>>>>((PXGraph) graph)).Select(Array.Empty<object>()))
        {
          PX.Objects.AR.ARInvoice inv = PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(pxResult1);
          PXSelectBase<PX.Objects.AR.ARTran> pxSelectBase2 = (PXSelectBase<PX.Objects.AR.ARTran>) new PXSelectJoin<PX.Objects.AR.ARTran, InnerJoin<DRDeferredCode, On<PX.Objects.AR.ARTran.deferredCode, Equal<DRDeferredCode.deferredCodeID>, And<DRDeferredCode.method, Equal<DeferredMethodType.cashReceipt>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>((PXGraph) graph);
          object[] objArray = new object[2]
          {
            (object) inv.DocType,
            (object) inv.RefNbr
          };
          foreach (PXResult<PX.Objects.AR.ARTran, DRDeferredCode> pxResult2 in pxSelectBase2.Select(objArray))
          {
            List<DRRecognition.ScheduledTran> scheduledTranList = new List<DRRecognition.ScheduledTran>();
            List<DRRecognition.ScheduledTran> virtualVoidedRecords = new List<DRRecognition.ScheduledTran>();
            PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, DRDeferredCode>.op_Implicit(pxResult2);
            PXResult<PX.Objects.AR.ARTran, DRDeferredCode>.op_Implicit(pxResult2);
            Decimal num1 = 0M;
            Decimal? nullable1;
            if (inv.LineTotal.Value != 0M)
            {
              Decimal num2 = arTran.TranAmt.Value;
              nullable1 = inv.LineTotal;
              Decimal num3 = nullable1.Value;
              num1 = num2 / num3;
            }
            nullable1 = arTran.TranAmt;
            Decimal val2 = nullable1.Value;
            PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) graph, new object[1]
            {
              (object) arTran.InventoryID
            }));
            DRSchedule scheduleByFid = graph.GetScheduleByFID("AR", inv.DocType, inv.RefNbr, arTran.LineNbr);
            DRRecognition drRecognition = graph;
            int? scheduleId = scheduleByFid.ScheduleID;
            int? nullable2 = arTran.InventoryID;
            int? inventoryID = nullable2.HasValue ? arTran.InventoryID : new int?(0);
            DRScheduleDetail scheduleDetailbyId = drRecognition.GetScheduleDetailbyID(scheduleId, inventoryID);
            nullable2 = scheduleDetailbyId.LineCntr;
            int valueOrDefault = nullable2.GetValueOrDefault();
            foreach (PXResult<ARAdjust> pxResult3 in ((PXSelectBase<ARAdjust>) new PXSelectJoin<ARAdjust, LeftJoin<DRScheduleTran, On<ARAdjust.adjgDocType, Equal<DRScheduleTran.adjgDocType>, And<ARAdjust.adjgRefNbr, Equal<DRScheduleTran.adjgRefNbr>>>>, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<DRScheduleTran.scheduleID, IsNull, And<ARAdjust.adjgDocType, NotEqual<ARDocType.creditMemo>>>>>, OrderBy<Asc<ARAdjust.adjgDocDate>>>((PXGraph) graph)).Select(new object[2]
            {
              (object) inv.DocType,
              (object) inv.RefNbr
            }))
            {
              ARAdjust ad = PXResult<ARAdjust>.op_Implicit(pxResult3);
              ++valueOrDefault;
              Decimal num4 = num1;
              nullable1 = ad.AdjAmt;
              Decimal num5 = nullable1.Value;
              Decimal num6 = Math.Min(num4 * num5, val2);
              val2 -= num6;
              Decimal num7 = PXDBCurrencyAttribute.BaseRound((PXGraph) graph, num6);
              DRRecognition.ScheduledTran scheduledTran1 = new DRRecognition.ScheduledTran();
              scheduledTran1.BranchID = ad.AdjgBranchID;
              scheduledTran1.Amount = new Decimal?(num7);
              scheduledTran1.ComponentID = arTran.InventoryID;
              scheduledTran1.DefCode = arTran.DeferredCode;
              scheduledTran1.FinPeriodID = graph.FinPeriodRepository.GetPeriodIDFromDate(ad.AdjgDocDate, PXAccess.GetParentOrganizationID(ad.AdjgBranchID));
              scheduledTran1.LineNbr = new int?(valueOrDefault);
              scheduledTran1.DetailLineNbr = scheduleDetailbyId.LineNbr;
              scheduledTran1.Module = scheduleByFid.Module;
              scheduledTran1.RecDate = ad.AdjgDocDate;
              scheduledTran1.ScheduleID = scheduleByFid.ScheduleID;
              scheduledTran1.ScheduleNbr = scheduleByFid.ScheduleNbr;
              scheduledTran1.DocType = scheduleByFid.DocType;
              scheduledTran1.AdjgDocType = ad.AdjgDocType;
              scheduledTran1.AdjgRefNbr = ad.AdjgRefNbr;
              scheduledTran1.AdjNbr = ad.AdjNbr;
              scheduledTran1.IsVirtual = new bool?(true);
              scheduledTran1.AccountID = scheduleDetailbyId.AccountID;
              scheduledTran1.SubID = scheduleDetailbyId.SubID;
              scheduledTran1.ComponentCD = inventoryItem == null ? "" : inventoryItem.InventoryCD;
              scheduledTran1.BaseCuryID = scheduleByFid.BaseCuryID;
              if (ad.Voided.GetValueOrDefault())
              {
                if (ad.AdjgDocType == "RPM" && virtualVoidedRecords.Count > 0)
                {
                  DRRecognition.ScheduledTran scheduledTran2 = virtualVoidedRecords.FirstOrDefault<DRRecognition.ScheduledTran>((Func<DRRecognition.ScheduledTran, bool>) (v =>
                  {
                    if (!(v.AdjgDocType == "PMT") && !(v.AdjgDocType == "PPM") || !(v.AdjgRefNbr == ad.AdjgRefNbr))
                      return false;
                    int? adjNbr = v.AdjNbr;
                    int? voidAdjNbr = ad.VoidAdjNbr;
                    return adjNbr.GetValueOrDefault() == voidAdjNbr.GetValueOrDefault() & adjNbr.HasValue == voidAdjNbr.HasValue;
                  }));
                  if (scheduledTran2 != null)
                    virtualVoidedRecords.Remove(scheduledTran2);
                }
                else
                  virtualVoidedRecords.Add(scheduledTran1);
              }
              else
                scheduledTranList.Add(scheduledTran1);
            }
            foreach (DRRecognition.ScheduledTran scheduledTran in scheduledTranList)
            {
              ((PXSelectBase) graph.Items).Cache.SetStatus((object) scheduledTran, (PXEntryStatus) 2);
              yield return (object) scheduledTran;
            }
            foreach (DRRecognition.ScheduledTran scheduledTran in virtualVoidedRecords)
            {
              ((PXSelectBase) graph.Items).Cache.SetStatus((object) scheduledTran, (PXEntryStatus) 2);
              yield return (object) scheduledTran;
            }
            virtualVoidedRecords = (List<DRRecognition.ScheduledTran>) null;
          }
          inv = (PX.Objects.AR.ARInvoice) null;
        }
      }
      ((PXSelectBase) graph.Items).Cache.IsDirty = false;
    }
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXEditDetailButton]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    if (((PXSelectBase<DRRecognition.ScheduledTran>) this.Items).Current != null)
      DRRedirectHelper.NavigateToDeferralSchedule((PXGraph) this, ((PXSelectBase<DRRecognition.ScheduledTran>) this.Items).Current.ScheduleID);
    return adapter.Get();
  }

  public DRRecognition()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
    ((PXProcessingBase<DRRecognition.ScheduledTran>) this.Items).SetSelected<DRRecognition.ScheduledTran.selected>();
  }

  protected virtual void ScheduleRecognitionFilter_RowUpdated(
    PXCache cache,
    PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  private IEnumerable<(int, int)> SplitToBatches(IList list, PXParallelProcessingOptions options)
  {
    int num = 0;
    int end = 0;
    for (; num < list.Count; num = end + 1)
    {
      end = Math.Min(num + options.BatchSize - 1, list.Count - 1) - 1;
      int? detailLineNbr1;
      int? detailLineNbr2;
      do
      {
        ++end;
        DRRecognition.ScheduledTran scheduledTran1 = (DRRecognition.ScheduledTran) list[end];
        DRRecognition.ScheduledTran scheduledTran2 = end + 1 < list.Count ? (DRRecognition.ScheduledTran) list[end + 1] : (DRRecognition.ScheduledTran) null;
        int? scheduleId1 = scheduledTran1.ScheduleID;
        int? scheduleId2 = (int?) scheduledTran2?.ScheduleID;
        if (scheduleId1.GetValueOrDefault() == scheduleId2.GetValueOrDefault() & scheduleId1.HasValue == scheduleId2.HasValue)
        {
          int? componentId1 = scheduledTran1.ComponentID;
          int? componentId2 = (int?) scheduledTran2?.ComponentID;
          if (componentId1.GetValueOrDefault() == componentId2.GetValueOrDefault() & componentId1.HasValue == componentId2.HasValue)
          {
            detailLineNbr1 = scheduledTran1.DetailLineNbr;
            detailLineNbr2 = (int?) scheduledTran2?.DetailLineNbr;
          }
          else
            break;
        }
        else
          break;
      }
      while (detailLineNbr1.GetValueOrDefault() == detailLineNbr2.GetValueOrDefault() & detailLineNbr1.HasValue == detailLineNbr2.HasValue);
      yield return (num, end);
    }
  }

  protected virtual void ScheduleRecognitionFilter_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DRRecognition.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new DRRecognition.\u003C\u003Ec__DisplayClass14_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.\u003C\u003E4__this = this;
    DRRecognition.ScheduleRecognitionFilter current = ((PXSelectBase<DRRecognition.ScheduleRecognitionFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.filterDate = current.RecDate;
    if (!WebConfig.ParallelProcessingDisabled)
    {
      // ISSUE: reference to a compiler-generated method
      ((PXProcessingBase<DRRecognition.ScheduledTran>) this.Items).ParallelProcessingOptions = new Action<PXParallelProcessingOptions>(cDisplayClass140.\u003CScheduleRecognitionFilter_RowSelected\u003Eb__0);
    }
    // ISSUE: method pointer
    ((PXProcessingBase<DRRecognition.ScheduledTran>) this.Items).SetProcessDelegate(new PXProcessingBase<DRRecognition.ScheduledTran>.ProcessListDelegate((object) cDisplayClass140, __methodptr(\u003CScheduleRecognitionFilter_RowSelected\u003Eb__1)));
  }

  public static void RunRecognition(List<DRRecognition.ScheduledTran> trans, DateTime? filterDate)
  {
    ScheduleMaint instance1 = PXGraph.CreateInstance<ScheduleMaint>();
    ((PXGraph) instance1).Clear();
    List<DRRecognition.ScheduledTran> validatedItems = DRRecognition.GetValidatedItems(trans, instance1);
    bool flag1 = validatedItems.Count<DRRecognition.ScheduledTran>() < trans.Count<DRRecognition.ScheduledTran>();
    foreach (DRRecognition.ScheduledTran scheduledTran in validatedItems)
    {
      PXProcessing<DRRecognition.ScheduledTran>.SetCurrentItem((object) scheduledTran);
      if (scheduledTran.IsVirtual.GetValueOrDefault())
      {
        try
        {
          ((PXSelectBase<DRScheduleDetail>) instance1.Document).Current = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>, And<DRScheduleDetail.detailLineNbr, Equal<Required<DRScheduleDetail.detailLineNbr>>>>>>.Config>.Select((PXGraph) instance1, new object[3]
          {
            (object) scheduledTran.ScheduleID,
            (object) scheduledTran.ComponentID.GetValueOrDefault(),
            (object) scheduledTran.DetailLineNbr
          }));
          DRScheduleTran drScheduleTran = ((PXSelectBase<DRScheduleTran>) instance1.OpenTransactions).Insert(new DRScheduleTran()
          {
            BranchID = scheduledTran.BranchID,
            AccountID = scheduledTran.AccountID,
            SubID = scheduledTran.SubID,
            AdjgDocType = scheduledTran.AdjgDocType,
            AdjgRefNbr = scheduledTran.AdjgRefNbr,
            AdjNbr = scheduledTran.AdjNbr,
            Amount = scheduledTran.Amount,
            ComponentID = new int?(scheduledTran.ComponentID.GetValueOrDefault()),
            DetailLineNbr = scheduledTran.DetailLineNbr,
            FinPeriodID = scheduledTran.FinPeriodID,
            ScheduleID = scheduledTran.ScheduleID,
            RecDate = scheduledTran.RecDate,
            Status = "O"
          });
          scheduledTran.LineNbr = drScheduleTran.LineNbr;
          instance1.RebuildProjections();
          ((PXAction) instance1.Save).Press();
          byte[] timeStamp = ((PXGraph) instance1).TimeStamp;
          ((PXGraph) instance1).Clear();
          ((PXGraph) instance1).TimeStamp = timeStamp;
          PXProcessing<DRRecognition.ScheduledTran>.SetProcessed();
        }
        catch (Exception ex)
        {
          flag1 = true;
          PXProcessing<DRRecognition.ScheduledTran>.SetError(ex.Message);
        }
      }
      else
        PXProcessing<DRRecognition.ScheduledTran>.SetProcessed();
    }
    PXProcessing<DRRecognition.ScheduledTran>.SetCurrentItem((object) null);
    List<DRRecognition.DRBatch> list = DRRecognition.SplitByFinPeriod(validatedItems);
    DRProcess instance2 = PXGraph.CreateInstance<DRProcess>();
    ((PXGraph) instance2).Clear();
    ((PXGraph) instance2).TimeStamp = ((PXGraph) instance1).TimeStamp;
    List<Batch> batchList = instance2.RunRecognition(list, filterDate);
    if (instance2.Exceptions.Count > 0)
    {
      foreach (Exception exception in instance2.Exceptions)
      {
        Exception e = exception;
        PXProcessing<DRRecognition.ScheduledTran>.SetCurrentItem((object) validatedItems.Where<DRRecognition.ScheduledTran>((Func<DRRecognition.ScheduledTran, bool>) (_ =>
        {
          int? scheduleId = _.ScheduleID;
          int? nullable = (int?) e.Data[(object) typeof (DRSchedule.scheduleID).Name];
          return scheduleId.GetValueOrDefault() == nullable.GetValueOrDefault() & scheduleId.HasValue == nullable.HasValue;
        })).FirstOrDefault<DRRecognition.ScheduledTran>());
        PXProcessing<DRRecognition.ScheduledTran>.SetError(e.Message);
      }
      PXProcessing<DRRecognition.ScheduledTran>.SetCurrentItem((object) null);
    }
    PostGraph instance3 = PXGraph.CreateInstance<PostGraph>();
    bool flag2 = false;
    if (instance3.AutoPost)
    {
      foreach (Batch b in batchList)
      {
        try
        {
          ((PXGraph) instance3).Clear();
          ((PXGraph) instance3).TimeStamp = b.tstamp;
          instance3.PostBatchProc(b);
        }
        catch (Exception ex)
        {
          flag2 = true;
        }
      }
      if (flag2)
        throw new PXException("Auto-Posting failed for one or more Batch.");
    }
    if (flag1)
      throw new PXException("One or more documents could not be released.");
  }

  private static List<DRRecognition.ScheduledTran> GetValidatedItems(
    List<DRRecognition.ScheduledTran> items,
    ScheduleMaint scheduleMaint)
  {
    List<DRRecognition.ScheduledTran> validatedItems = new List<DRRecognition.ScheduledTran>();
    HashSet<Tuple<int, string>> tupleSet = new HashSet<Tuple<int, string>>();
    foreach (DRRecognition.ScheduledTran scheduledTran in items)
    {
      try
      {
        if (!tupleSet.Contains(new Tuple<int, string>(scheduledTran.BranchID.Value, scheduledTran.FinPeriodID)))
        {
          PXProcessing<DRRecognition.ScheduledTran>.SetCurrentItem((object) scheduledTran);
          FinPeriod byId = scheduleMaint.FinPeriodRepository.FindByID(PXAccess.GetParentOrganizationID(scheduledTran.BranchID), scheduledTran.FinPeriodID);
          scheduleMaint.FinPeriodUtils.CanPostToPeriod((IFinPeriod) byId).RaiseIfHasError();
          tupleSet.Add(new Tuple<int, string>(scheduledTran.BranchID.Value, scheduledTran.FinPeriodID));
        }
        validatedItems.Add(scheduledTran);
      }
      catch (Exception ex)
      {
        PXProcessing<DRRecognition.ScheduledTran>.SetError(ex.Message);
      }
    }
    return validatedItems;
  }

  private static List<DRRecognition.DRBatch> SplitByFinPeriod(
    List<DRRecognition.ScheduledTran> items)
  {
    return items.GroupBy(t => new
    {
      FinPeriodID = t.FinPeriodID,
      BranchID = t.BranchID
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType43<string, int?>, DRRecognition.ScheduledTran>, DRRecognition.DRBatch>(b => new DRRecognition.DRBatch(b.Key.FinPeriodID, b.Key.BranchID)
    {
      Trans = b.Select<DRRecognition.ScheduledTran, DRRecognition.DRTranKey>((Func<DRRecognition.ScheduledTran, DRRecognition.DRTranKey>) (tr => new DRRecognition.DRTranKey(tr.ScheduleID, new int?(tr.ComponentID.GetValueOrDefault()), tr.DetailLineNbr, tr.LineNbr))).ToList<DRRecognition.DRTranKey>()
    }).OrderBy<DRRecognition.DRBatch, string>((Func<DRRecognition.DRBatch, string>) (g => g.FinPeriod)).ThenBy<DRRecognition.DRBatch, int?>((Func<DRRecognition.DRBatch, int?>) (g => g.BranchID)).ToList<DRRecognition.DRBatch>();
  }

  public DRSchedule GetScheduleByFID(string module, string docType, string refNbr, int? lineNbr)
  {
    return PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<Required<DRSchedule.module>>, And<DRSchedule.docType, Equal<Required<DRSchedule.docType>>, And<DRSchedule.refNbr, Equal<Required<DRSchedule.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<DRSchedule.lineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) module,
      (object) docType,
      (object) refNbr,
      (object) lineNbr
    }));
  }

  public DRScheduleDetail GetScheduleDetailbyID(int? scheduleID, int? inventoryID)
  {
    return PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) scheduleID,
      (object) inventoryID
    }));
  }

  [Serializable]
  public class ScheduleRecognitionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _RecDate;
    protected string _DeferredCode;

    [Branch(null, null, true, true, true)]
    public virtual int? BranchID { get; set; }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Recognition Date")]
    public virtual DateTime? RecDate
    {
      get => this._RecDate;
      set => this._RecDate = value;
    }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Deferral Code")]
    [PXSelector(typeof (DRDeferredCode.deferredCodeID))]
    [PXRestrictor(typeof (Where<DRDeferredCode.active, Equal<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new Type[] {typeof (DRDeferredCode.deferredCodeID)})]
    public virtual string DeferredCode
    {
      get => this._DeferredCode;
      set => this._DeferredCode = value;
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduleRecognitionFilter.branchID>
    {
    }

    public abstract class recDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      DRRecognition.ScheduleRecognitionFilter.recDate>
    {
    }

    public abstract class deferredCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduleRecognitionFilter.deferredCode>
    {
    }
  }

  [Serializable]
  public class ScheduledTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAccountable
  {
    protected int? _ScheduleID;
    protected int? _ComponentID;
    protected string _ComponentCD;
    protected int? _LineNbr;
    protected int? _BranchID;
    protected string _Module;
    protected DateTime? _RecDate;
    protected Decimal? _Amount;
    protected int? _AccountID;
    protected int? _SubID;
    protected string _FinPeriodID;
    protected string _DefCode;
    protected string _DocType;
    protected string _AdjgDocType;
    protected string _AdjgRefNbr;
    protected int? _AdjNbr;
    protected bool? _IsVirtual = new bool?(false);
    protected bool? _Selected = new bool?(false);

    [PXDBInt(IsKey = true)]
    [PXDefault]
    [PXUIField]
    public virtual int? ScheduleID
    {
      get => this._ScheduleID;
      set => this._ScheduleID = value;
    }

    [PXUIField]
    [PXDBInt(IsKey = true)]
    [PXSelector(typeof (Search2<DRScheduleDetail.componentID, LeftJoin<PX.Objects.IN.InventoryItem, On<DRScheduleDetail.componentID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<DRScheduleDetail.scheduleID, Equal<Current<DRRecognition.ScheduledTran.scheduleID>>>>), new Type[] {typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr)})]
    public virtual int? ComponentID
    {
      get => this._ComponentID;
      set => this._ComponentID = value;
    }

    [PXDBDefault(typeof (DRScheduleDetail.detailLineNbr))]
    [PXDBInt(IsKey = true)]
    public virtual int? DetailLineNbr { get; set; }

    [PXString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Schedule Number")]
    [PXSelector(typeof (DRSchedule.scheduleNbr))]
    public virtual string ScheduleNbr { get; set; }

    [PXUIField]
    [PXDBString]
    public virtual string ComponentCD
    {
      get => this._ComponentCD;
      set => this._ComponentCD = value;
    }

    [PXDBInt(IsKey = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Tran. Nbr.", Enabled = false)]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [Branch(null, null, true, true, true)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXDBString(2, IsFixed = true)]
    [PXDefault("")]
    [PXUIField(DisplayName = "Module")]
    public virtual string Module
    {
      get => this._Module;
      set => this._Module = value;
    }

    [PXDBDate]
    [PXDefault]
    [PXUIField(DisplayName = "Rec. Date")]
    public virtual DateTime? RecDate
    {
      get => this._RecDate;
      set => this._RecDate = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Amount")]
    public virtual Decimal? Amount
    {
      get => this._Amount;
      set => this._Amount = value;
    }

    [PXString(5, IsUnicode = true)]
    [PXUIField(DisplayName = "Currency", Enabled = false)]
    public virtual string BaseCuryID { get; set; }

    [Account]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [PXInt]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
    [PXUIField(DisplayName = "Fin. Period", Enabled = false)]
    public virtual string FinPeriodID
    {
      get => this._FinPeriodID;
      set => this._FinPeriodID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXDefault("")]
    [PXUIField]
    public virtual string DefCode
    {
      get => this._DefCode;
      set => this._DefCode = value;
    }

    [PXDBString(3, IsFixed = true, InputMask = "")]
    [PXUIField(DisplayName = "Doc. Type")]
    [DRScheduleDocumentType.List]
    public virtual string DocType
    {
      get => this._DocType;
      set => this._DocType = value;
    }

    [PXDBString(3, IsFixed = true, InputMask = "")]
    public virtual string AdjgDocType
    {
      get => this._AdjgDocType;
      set => this._AdjgDocType = value;
    }

    [PXDBString(15, IsUnicode = true)]
    public virtual string AdjgRefNbr
    {
      get => this._AdjgRefNbr;
      set => this._AdjgRefNbr = value;
    }

    [PXDBInt]
    public virtual int? AdjNbr
    {
      get => this._AdjNbr;
      set => this._AdjNbr = value;
    }

    [PXBool]
    [PXDefault(false)]
    public bool? IsVirtual
    {
      get => this._IsVirtual;
      set => this._IsVirtual = value;
    }

    [PXBool]
    [PXDefault(false)]
    [PXUIField]
    public bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    public static int CompareFinPeriod(DRRecognition.ScheduledTran a, DRRecognition.ScheduledTran b)
    {
      return a.FinPeriodID.CompareTo(b.FinPeriodID);
    }

    public abstract class scheduleID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.scheduleID>
    {
    }

    public abstract class componentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.componentID>
    {
    }

    public abstract class detailLineNbr : IBqlField, IBqlOperand
    {
    }

    public abstract class scheduleNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.scheduleNbr>
    {
    }

    public abstract class componentCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.componentCD>
    {
    }

    public abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRRecognition.ScheduledTran.lineNbr>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.branchID>
    {
    }

    public abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.module>
    {
    }

    public abstract class recDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.recDate>
    {
    }

    public abstract class amount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.amount>
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.baseCuryID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.accountID>
    {
    }

    public abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRRecognition.ScheduledTran.subID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.finPeriodID>
    {
    }

    public abstract class defCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.defCode>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.docType>
    {
    }

    public abstract class adjgDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.adjgDocType>
    {
    }

    public abstract class adjgRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.adjgRefNbr>
    {
    }

    public abstract class adjNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRRecognition.ScheduledTran.adjNbr>
    {
    }

    public abstract class isVirtual : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.isVirtual>
    {
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      DRRecognition.ScheduledTran.selected>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select2<DRScheduleTran, InnerJoin<DRScheduleDetail, On<DRScheduleTran.scheduleID, Equal<DRScheduleDetail.scheduleID>, And<DRScheduleTran.componentID, Equal<DRScheduleDetail.componentID>, And<DRScheduleTran.detailLineNbr, Equal<DRScheduleDetail.detailLineNbr>>>>, InnerJoin<DRSchedule, On<DRScheduleTran.scheduleID, Equal<DRSchedule.scheduleID>>, LeftJoin<PX.Objects.IN.InventoryItem, On<DRScheduleTran.componentID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>>, Where<DRScheduleTran.status, Equal<DRScheduleTranStatus.OpenStatus>, And<DRScheduleDetail.status, NotEqual<DRScheduleStatus.DraftStatus>>>>))]
  public class ScheduledTranRec : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(IsKey = true, BqlField = typeof (DRScheduleTran.scheduleID))]
    public virtual int? ScheduleID { get; set; }

    [PXDBInt(IsKey = true, BqlField = typeof (DRScheduleTran.componentID))]
    public virtual int? ComponentID { get; set; }

    [PXDBInt(IsKey = true, BqlField = typeof (DRScheduleTran.detailLineNbr))]
    public virtual int? DetailLineNbr { get; set; }

    [PXDBInt(IsKey = true, BqlField = typeof (DRScheduleTran.lineNbr))]
    public virtual int? LineNbr { get; set; }

    [Branch(null, null, true, true, true, BqlField = typeof (DRScheduleTran.branchID))]
    public virtual int? BranchID { get; set; }

    [PXDBDate(BqlField = typeof (DRScheduleTran.recDate))]
    public virtual DateTime? RecDate { get; set; }

    [PXDBDecimal(BqlField = typeof (DRScheduleTran.amount))]
    public virtual Decimal? Amount { get; set; }

    [Account]
    public virtual int? AccountID { get; set; }

    [PXDBInt(BqlField = typeof (DRScheduleTran.subID))]
    public virtual int? SubID { get; set; }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (DRScheduleTran.finPeriodID))]
    public virtual string FinPeriodID { get; set; }

    [PXDBString(10, IsUnicode = true, BqlField = typeof (DRScheduleDetail.defCode))]
    public virtual string DefCode { get; set; }

    [PXDBString(2, IsFixed = true, BqlField = typeof (DRScheduleDetail.module))]
    public virtual string Module { get; set; }

    [PXString(3, IsFixed = true)]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DRScheduleDetail.docType, Equal<ARDocType.invoice>>>>>.And<BqlOperand<DRScheduleDetail.module, IBqlString>.IsEqual<BatchModule.moduleAR>>>, DRScheduleDocumentType.invoiceAR, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DRScheduleDetail.docType, Equal<ARDocType.invoice>>>>>.And<BqlOperand<DRScheduleDetail.module, IBqlString>.IsEqual<BatchModule.moduleAP>>>, DRScheduleDocumentType.invoiceAP>>, DRScheduleDetail.docType>), typeof (string))]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, BqlField = typeof (DRSchedule.scheduleNbr))]
    public virtual string ScheduleNbr { get; set; }

    [PXString]
    [PXDBCalced(typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (string))]
    public virtual string ComponentCD { get; set; }

    [PXDBString(5, IsUnicode = true, BqlField = typeof (DRSchedule.baseCuryID))]
    public virtual string BaseCuryID { get; set; }

    public abstract class scheduleID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.scheduleID>
    {
    }

    public abstract class componentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.componentID>
    {
    }

    public abstract class detailLineNbr : IBqlField, IBqlOperand
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.lineNbr>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.branchID>
    {
    }

    public abstract class recDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.recDate>
    {
    }

    public abstract class amount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.amount>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.accountID>
    {
    }

    public abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRRecognition.ScheduledTranRec.subID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.finPeriodID>
    {
    }

    public abstract class defCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.defCode>
    {
    }

    public abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.module>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.docType>
    {
    }

    public abstract class scheduleNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.scheduleNbr>
    {
    }

    public abstract class componentCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.componentCD>
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRRecognition.ScheduledTranRec.baseCuryID>
    {
    }
  }

  [DebuggerDisplay("{FinPeriod} Trans.Count={Trans.Count}")]
  public class DRBatch
  {
    public string FinPeriod { get; private set; }

    public int? BranchID { get; private set; }

    public List<DRRecognition.DRTranKey> Trans { get; set; }

    public DRBatch(string finPeriod, int? branchID)
    {
      this.FinPeriod = finPeriod;
      this.BranchID = branchID;
      this.Trans = new List<DRRecognition.DRTranKey>();
    }
  }

  [DebuggerDisplay("{ScheduleID}.{LineNbr}")]
  public struct DRTranKey(int? scheduleID, int? componentID, int? detailLineNbr, int? lineNbr)
  {
    public int? ScheduleID = scheduleID;
    public int? ComponentID = componentID;
    public int? DetailLineNbr = detailLineNbr;
    public int? LineNbr = lineNbr;
  }
}
