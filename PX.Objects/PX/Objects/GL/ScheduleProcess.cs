// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ScheduleProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.Overrides.ScheduleProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class ScheduleProcess : PXGraph<PX.Objects.GL.ScheduleProcess>, IScheduleProcessing
{
  public PXSelect<Schedule> Running_Schedule;
  public PXSelect<BatchNew> Batch_Created;
  public PXSelect<GLTranNew> Tran_Created;
  public PXSelect<PX.Objects.CM.CurrencyInfo> CuryInfo_Created;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public GLSetup GLSetup
  {
    get
    {
      return PXResultset<GLSetup>.op_Implicit(PXSelectBase<GLSetup, PXSelect<GLSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    }
  }

  protected virtual void BatchNew_BatchNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [Obsolete("Please use MakeSchedule", false)]
  private List<ScheduleDet> MakeSchedule(Schedule schedule, short times, DateTime runDate)
  {
    return PX.Objects.GL.ScheduleProcess.MakeSchedule((PXGraph) this, schedule, times, runDate);
  }

  [Obsolete("Please use MakeSchedule", false)]
  public static List<ScheduleDet> MakeSchedule(PXGraph graph, Schedule schedule, short times)
  {
    return PX.Objects.GL.ScheduleProcess.MakeSchedule(graph, schedule, times, graph.Accessinfo.BusinessDate.Value);
  }

  [Obsolete("Please use MakeSchedule", false)]
  public static List<ScheduleDet> MakeSchedule(
    PXGraph graph,
    Schedule schedule,
    short times,
    DateTime runDate)
  {
    return new Scheduler(graph).MakeSchedule(schedule, times, new DateTime?(runDate)).ToList<ScheduleDet>();
  }

  [Obsolete("Please use GetNextRunDate", false)]
  public static DateTime? GetNextRunDate(PXGraph graph, Schedule schedule)
  {
    return new Scheduler(graph).GetNextRunDate(schedule);
  }

  public virtual void GenerateProc(Schedule schedule)
  {
    this.GenerateProc(schedule, (short) 1, ((PXGraph) this).Accessinfo.BusinessDate.Value);
  }

  public virtual void GenerateProc(Schedule schedule, short times, DateTime runDate)
  {
    IEnumerable<ScheduleDet> scheduleDets = new Scheduler((PXGraph) this).MakeSchedule(schedule, times, new DateTime?(runDate));
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (ScheduleDet scheduleDet in scheduleDets)
      {
        foreach (PXResult<Batch, PX.Objects.CM.CurrencyInfo> pxResult1 in PXSelectBase<Batch, PXSelectJoin<Batch, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Batch.curyInfoID>>>, Where<Batch.scheduleID, Equal<Optional<Schedule.scheduleID>>, And<Batch.scheduled, Equal<boolTrue>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) schedule.ScheduleID
        }))
        {
          ((PXGraph) instance).Clear();
          Batch batch1 = PXResult<Batch, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult1);
          PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResult<Batch, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult1);
          Batch copy1 = PXCache<Batch>.CreateCopy(batch1);
          copy1.OrigBatchNbr = copy1.BatchNbr;
          copy1.OrigModule = copy1.Module;
          copy1.NumberCode = "GLREC";
          copy1.CuryDebitTotal = new Decimal?(0M);
          copy1.CuryCreditTotal = new Decimal?(0M);
          copy1.DebitTotal = new Decimal?(0M);
          copy1.CreditTotal = new Decimal?(0M);
          copy1.CuryControlTotal = new Decimal?(0M);
          copy1.ControlTotal = new Decimal?(0M);
          copy1.NoteID = new Guid?();
          copy1.Posted = new bool?(false);
          copy1.Approved = new bool?(false);
          copy1.Released = new bool?(false);
          copy1.Scheduled = new bool?(false);
          copy1.AutoReverseCopy = new bool?(false);
          copy1.Hold = new bool?(true);
          copy1.Status = (string) null;
          PX.Objects.CM.CurrencyInfo currencyInfo2 = new PX.Objects.CM.CurrencyInfo()
          {
            ModuleCode = currencyInfo1.ModuleCode,
            CuryRateTypeID = currencyInfo1.CuryRateTypeID,
            CuryID = currencyInfo1.CuryID,
            BaseCuryID = currencyInfo1.BaseCuryID,
            CuryEffDate = scheduleDet.ScheduledDate
          };
          PX.Objects.CM.CurrencyInfo currencyInfo3 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) instance.currencyinfo).Insert(currencyInfo2);
          copy1.CuryInfoID = currencyInfo3.CuryInfoID;
          copy1.DateEntered = scheduleDet.ScheduledDate;
          FinPeriod valueOrRaiseError = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(copy1.BranchID), scheduleDet.ScheduledPeriod).GetValueOrRaiseError();
          copy1.FinPeriodID = valueOrRaiseError.FinPeriodID;
          copy1.TranPeriodID = (string) null;
          Batch batch2 = ((PXSelectBase<Batch>) instance.BatchModule).Insert(copy1);
          PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) instance.BatchModule).Cache, (object) batch1, ((PXSelectBase) instance.BatchModule).Cache, (object) batch2, (PXNoteAttribute.IPXCopySettings) null);
          foreach (PXResult<GLTran> pxResult2 in PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) batch1.Module,
            (object) batch1.BatchNbr
          }))
          {
            GLTran glTran1 = PXResult<GLTran>.op_Implicit(pxResult2);
            GLTran copy2 = PXCache<GLTran>.CreateCopy(glTran1);
            copy2.OrigBatchNbr = copy2.BatchNbr;
            copy2.OrigModule = copy2.Module;
            copy2.BatchNbr = batch2.BatchNbr;
            copy2.CuryInfoID = batch2.CuryInfoID;
            copy2.CATranID = new long?();
            copy2.NoteID = new Guid?();
            copy2.TranDate = scheduleDet.ScheduledDate;
            FinPeriodIDAttribute.SetPeriodsByMaster<GLTran.finPeriodID>(((PXSelectBase) instance.GLTranModuleBatNbr).Cache, (object) copy2, scheduleDet.ScheduledPeriod);
            GLTran glTran2 = ((PXSelectBase<GLTran>) instance.GLTranModuleBatNbr).Insert(copy2);
            PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) instance.GLTranModuleBatNbr).Cache, (object) glTran1, ((PXSelectBase) instance.GLTranModuleBatNbr).Cache, (object) glTran2, (PXNoteAttribute.IPXCopySettings) null);
          }
          batch2.CuryControlTotal = batch2.CuryDebitTotal;
          batch2.ControlTotal = batch2.DebitTotal;
          ((PXSelectBase<Batch>) instance.BatchModule).Update(batch2);
          ((PXAction) instance.releaseFromHold).Press();
          ((PXGraph) instance).Persist();
        }
        schedule.LastRunDate = scheduleDet.ScheduledDate;
        ((PXSelectBase) this.Running_Schedule).Cache.Update((object) schedule);
      }
      ((PXSelectBase) this.Running_Schedule).Cache.Persist((PXDBOperation) 1);
      ((PXSelectBase) instance.GLTranModuleBatNbr).Cache.Normalize();
      transactionScope.Complete((PXGraph) this);
    }
    ((PXSelectBase) this.Running_Schedule).Cache.Persisted(false);
  }
}
