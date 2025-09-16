// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.DR.Descriptor;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

public class DRProcess : 
  PXGraph<DRProcess>,
  IDREntityStorage,
  IBusinessAccountProvider,
  IInventoryItemProvider
{
  public PXSelect<DRSchedule> Schedule;
  public PXSelect<DRScheduleDetail> ScheduleDetail;
  public PXSelect<DRScheduleTran> Transactions;
  public PXSelect<DRExpenseBalance> ExpenseBalance;
  public PXSelect<DRExpenseProjectionAccum> ExpenseProjection;
  public PXSelect<DRRevenueBalance> RevenueBalance;
  public PXSelect<DRRevenueProjectionAccum> RevenueProjection;
  public PXSetup<DRSetup> Setup;
  private List<PXException> _Exceptions = new List<PXException>();
  private Dictionary<Tuple<int, int, int>, DRScheduleDetail> ScheduleDetailForComponent = new Dictionary<Tuple<int, int, int>, DRScheduleDetail>(1000);

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public List<PXException> Exceptions => this._Exceptions;

  public DRProcess()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
  }

  protected virtual void DRScheduleDetail_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleDetail_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRSchedule_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2022R2.")]
  protected virtual void DRScheduleDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
  }

  protected virtual void DRScheduleDetail_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row) || row.IsResidual.GetValueOrDefault() || !string.IsNullOrEmpty(row.DefCode))
      return;
    sender.RaiseExceptionHandling<DRScheduleDetail.defCode>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
  }

  /// <summary>
  /// This is auto setting of the <see cref="T:PX.Objects.DR.DRScheduleDetail.curyDefAmt" /> field because of only the base currency are allowed for DR Schedules for now.
  /// </summary>
  protected virtual void DRScheduleDetail_DefAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>() || !(e.Row is DRScheduleDetail row1))
      return;
    Decimal? nullable = row1.DefAmt;
    if (!nullable.HasValue)
      return;
    nullable = row1.CuryDefAmt;
    if (nullable.HasValue)
      return;
    Decimal num = 0M;
    PXCache sender1 = sender;
    DRScheduleDetail row2 = row1;
    nullable = row1.DefAmt;
    Decimal baseval = nullable.Value;
    ref Decimal local = ref num;
    PXCurrencyAttribute.CuryConvCury<DRScheduleDetail.curyInfoID>(sender1, (object) row2, baseval, out local);
    row1.CuryDefAmt = new Decimal?(num);
  }

  /// <summary>
  /// This is auto setting of the <see cref="T:PX.Objects.DR.DRScheduleDetail.curyTotalAmt" /> field because of only the base currency are allowed for DR Schedules for now.
  /// </summary>
  protected virtual void DRScheduleDetail_TotalAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>() || !(e.Row is DRScheduleDetail row1))
      return;
    Decimal? nullable = row1.TotalAmt;
    if (!nullable.HasValue)
      return;
    nullable = row1.CuryTotalAmt;
    if (nullable.HasValue)
      return;
    Decimal num = 0M;
    PXCache sender1 = sender;
    DRScheduleDetail row2 = row1;
    nullable = row1.TotalAmt;
    Decimal baseval = nullable.Value;
    ref Decimal local = ref num;
    PXCurrencyAttribute.CuryConvCury<DRScheduleDetail.curyInfoID>(sender1, (object) row2, baseval, out local);
    row1.CuryTotalAmt = new Decimal?(num);
  }

  public List<Batch> RunRecognition(List<DRRecognition.DRBatch> list, DateTime? recDate)
  {
    List<Batch> batchList = new List<Batch>(list.Count);
    this.Exceptions.Clear();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (DRRecognition.DRBatch drBatch in list)
      {
        JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.GL.GLTran.referenceID>(DRProcess.\u003C\u003Ec.\u003C\u003E9__27_0 ?? (DRProcess.\u003C\u003Ec.\u003C\u003E9__27_0 = new PXFieldVerifying((object) DRProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRunRecognition\u003Eb__27_0))));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.GL.GLTran.inventoryID>(DRProcess.\u003C\u003Ec.\u003C\u003E9__27_1 ?? (DRProcess.\u003C\u003Ec.\u003C\u003E9__27_1 = new PXFieldVerifying((object) DRProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRunRecognition\u003Eb__27_1))));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.GL.GLTran.projectID>(DRProcess.\u003C\u003Ec.\u003C\u003E9__27_2 ?? (DRProcess.\u003C\u003Ec.\u003C\u003E9__27_2 = new PXFieldVerifying((object) DRProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRunRecognition\u003Eb__27_2))));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.GL.GLTran.taskID>(DRProcess.\u003C\u003Ec.\u003C\u003E9__27_3 ?? (DRProcess.\u003C\u003Ec.\u003C\u003E9__27_3 = new PXFieldVerifying((object) DRProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRunRecognition\u003Eb__27_3))));
        DateTime? nullable1 = recDate;
        FinPeriod byBranch = FinPeriod.PK.FindByBranch((PXGraph) this, drBatch.BranchID, drBatch.FinPeriod);
        if (byBranch != null)
        {
          nullable1 = new DateTime?(byBranch.EndDate.Value.AddDays(-1.0));
          DateTime? nullable2 = recDate;
          DateTime? nullable3 = nullable1;
          nullable1 = (nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? nullable1 : recDate;
        }
        ((PXSelectBase<Batch>) instance.BatchModule).Insert(new Batch()
        {
          Module = "DR",
          Status = "U",
          Released = new bool?(true),
          Hold = new bool?(false),
          BranchID = drBatch.BranchID,
          FinPeriodID = drBatch.FinPeriod,
          TranPeriodID = drBatch.FinPeriod,
          DateEntered = nullable1
        });
        List<DRScheduleTran> drScheduleTranList = new List<DRScheduleTran>();
        PX.Objects.GL.GLTran glTran1 = (PX.Objects.GL.GLTran) null;
        PX.Objects.GL.GLTran glTran2 = (PX.Objects.GL.GLTran) null;
        foreach (DRRecognition.DRTranKey tran in drBatch.Trans)
        {
          try
          {
            PXResult<DRSchedule> pxResult = ((IQueryable<PXResult<DRSchedule>>) PXSelectBase<DRSchedule, PXSelectReadonly2<DRSchedule, InnerJoin<DRScheduleDetail, On<DRScheduleDetail.scheduleID, Equal<DRSchedule.scheduleID>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>, And<DRScheduleDetail.detailLineNbr, Equal<Required<DRScheduleDetail.detailLineNbr>>>>>, InnerJoin<DRScheduleTran, On<DRScheduleTran.scheduleID, Equal<DRSchedule.scheduleID>, And<DRScheduleTran.componentID, Equal<DRScheduleDetail.componentID>, And<DRScheduleTran.detailLineNbr, Equal<DRScheduleDetail.detailLineNbr>, And<DRScheduleTran.lineNbr, Equal<Required<DRScheduleTran.lineNbr>>>>>>>>, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) instance, new object[4]
            {
              (object) tran.ComponentID,
              (object) tran.DetailLineNbr,
              (object) tran.LineNbr,
              (object) tran.ScheduleID
            })).FirstOrDefault<PXResult<DRSchedule>>();
            DRSchedule drSchedule = PXResult.Unwrap<DRSchedule>((object) pxResult);
            DRScheduleDetail scheduleDetail = PXResult.Unwrap<DRScheduleDetail>((object) pxResult);
            DRScheduleTran drScheduleTran = PXResult.Unwrap<DRScheduleTran>((object) pxResult);
            if (!this.ScheduleDetailForComponent.ContainsKey(new Tuple<int, int, int>(tran.ScheduleID.Value, tran.ComponentID.Value, tran.DetailLineNbr.Value)))
              this.ScheduleDetailForComponent[new Tuple<int, int, int>(tran.ScheduleID.Value, tran.ComponentID.Value, tran.DetailLineNbr.Value)] = scheduleDetail;
            glTran1 = new PX.Objects.GL.GLTran();
            glTran1.SummPost = new bool?(false);
            glTran1.TranType = drSchedule.DocType;
            glTran1.RefNbr = drSchedule.ScheduleNbr;
            int? nullable4 = scheduleDetail.BAccountID;
            if (nullable4.HasValue)
            {
              nullable4 = scheduleDetail.BAccountID;
              int num = 0;
              if (!(nullable4.GetValueOrDefault() == num & nullable4.HasValue))
                glTran1.ReferenceID = scheduleDetail.BAccountID;
            }
            nullable4 = scheduleDetail.ComponentID;
            if (nullable4.HasValue)
            {
              nullable4 = scheduleDetail.ComponentID;
              int num = 0;
              if (!(nullable4.GetValueOrDefault() == num & nullable4.HasValue))
                glTran1.InventoryID = scheduleDetail.ComponentID;
            }
            bool flag = DRProcess.IsReversed(scheduleDetail);
            Decimal? amount = drScheduleTran.Amount;
            Decimal num1 = 0M;
            if (!(amount.GetValueOrDefault() >= num1 & amount.HasValue) || flag)
            {
              amount = drScheduleTran.Amount;
              Decimal num2 = 0M;
              if (!(amount.GetValueOrDefault() < num2 & amount.HasValue & flag))
              {
                glTran1.AccountID = drScheduleTran.AccountID;
                glTran1.SubID = drScheduleTran.SubID;
                goto label_20;
              }
            }
            glTran1.AccountID = scheduleDetail.DefAcctID;
            glTran1.SubID = scheduleDetail.DefSubID;
            glTran1.ReclassificationProhibited = new bool?(true);
label_20:
            glTran1.BranchID = drScheduleTran.BranchID;
            PX.Objects.GL.GLTran glTran3 = glTran1;
            Decimal num3;
            if (!(scheduleDetail.Module == "AR"))
            {
              num3 = 0M;
            }
            else
            {
              amount = drScheduleTran.Amount;
              num3 = Math.Abs(amount.Value);
            }
            Decimal? nullable5 = new Decimal?(num3);
            glTran3.CuryDebitAmt = nullable5;
            PX.Objects.GL.GLTran glTran4 = glTran1;
            Decimal num4;
            if (!(scheduleDetail.Module == "AR"))
            {
              num4 = 0M;
            }
            else
            {
              amount = drScheduleTran.Amount;
              num4 = Math.Abs(amount.Value);
            }
            Decimal? nullable6 = new Decimal?(num4);
            glTran4.DebitAmt = nullable6;
            PX.Objects.GL.GLTran glTran5 = glTran1;
            Decimal num5;
            if (!(scheduleDetail.Module == "AR"))
            {
              amount = drScheduleTran.Amount;
              num5 = Math.Abs(amount.Value);
            }
            else
              num5 = 0M;
            Decimal? nullable7 = new Decimal?(num5);
            glTran5.CuryCreditAmt = nullable7;
            PX.Objects.GL.GLTran glTran6 = glTran1;
            Decimal num6;
            if (!(scheduleDetail.Module == "AR"))
            {
              amount = drScheduleTran.Amount;
              num6 = Math.Abs(amount.Value);
            }
            else
              num6 = 0M;
            Decimal? nullable8 = new Decimal?(num6);
            glTran6.CreditAmt = nullable8;
            glTran1.TranDesc = drSchedule.TranDesc;
            glTran1.Released = new bool?(true);
            glTran1.TranDate = drScheduleTran.RecDate;
            glTran1.ProjectID = drSchedule.ProjectID;
            glTran1.TaskID = drSchedule.TaskID;
            glTran1.TranLineNbr = drScheduleTran.LineNbr;
            glTran1 = ((PXSelectBase<PX.Objects.GL.GLTran>) instance.GLTranModuleBatNbr).Insert(glTran1);
            glTran2 = new PX.Objects.GL.GLTran();
            glTran2.SummPost = new bool?(false);
            glTran2.TranType = drSchedule.DocType;
            glTran2.RefNbr = drSchedule.ScheduleNbr;
            nullable4 = scheduleDetail.BAccountID;
            if (nullable4.HasValue)
            {
              nullable4 = scheduleDetail.BAccountID;
              int num7 = 0;
              if (!(nullable4.GetValueOrDefault() == num7 & nullable4.HasValue))
                glTran2.ReferenceID = scheduleDetail.BAccountID;
            }
            nullable4 = scheduleDetail.ComponentID;
            if (nullable4.HasValue)
            {
              nullable4 = scheduleDetail.ComponentID;
              int num8 = 0;
              if (!(nullable4.GetValueOrDefault() == num8 & nullable4.HasValue))
                glTran2.InventoryID = scheduleDetail.ComponentID;
            }
            amount = drScheduleTran.Amount;
            Decimal num9 = 0M;
            if (!(amount.GetValueOrDefault() >= num9 & amount.HasValue) || flag)
            {
              amount = drScheduleTran.Amount;
              Decimal num10 = 0M;
              if (!(amount.GetValueOrDefault() < num10 & amount.HasValue & flag))
              {
                glTran2.AccountID = scheduleDetail.DefAcctID;
                glTran2.SubID = scheduleDetail.DefSubID;
                glTran2.ReclassificationProhibited = new bool?(true);
                goto label_42;
              }
            }
            glTran2.AccountID = drScheduleTran.AccountID;
            glTran2.SubID = drScheduleTran.SubID;
label_42:
            glTran2.BranchID = drScheduleTran.BranchID;
            PX.Objects.GL.GLTran glTran7 = glTran2;
            Decimal num11;
            if (!(scheduleDetail.Module == "AR"))
            {
              amount = drScheduleTran.Amount;
              num11 = Math.Abs(amount.Value);
            }
            else
              num11 = 0M;
            Decimal? nullable9 = new Decimal?(num11);
            glTran7.CuryDebitAmt = nullable9;
            PX.Objects.GL.GLTran glTran8 = glTran2;
            Decimal num12;
            if (!(scheduleDetail.Module == "AR"))
            {
              amount = drScheduleTran.Amount;
              num12 = Math.Abs(amount.Value);
            }
            else
              num12 = 0M;
            Decimal? nullable10 = new Decimal?(num12);
            glTran8.DebitAmt = nullable10;
            PX.Objects.GL.GLTran glTran9 = glTran2;
            Decimal num13;
            if (!(scheduleDetail.Module == "AR"))
            {
              num13 = 0M;
            }
            else
            {
              amount = drScheduleTran.Amount;
              num13 = Math.Abs(amount.Value);
            }
            Decimal? nullable11 = new Decimal?(num13);
            glTran9.CuryCreditAmt = nullable11;
            PX.Objects.GL.GLTran glTran10 = glTran2;
            Decimal num14;
            if (!(scheduleDetail.Module == "AR"))
            {
              num14 = 0M;
            }
            else
            {
              amount = drScheduleTran.Amount;
              num14 = Math.Abs(amount.Value);
            }
            Decimal? nullable12 = new Decimal?(num14);
            glTran10.CreditAmt = nullable12;
            glTran2.TranDesc = drSchedule.TranDesc;
            glTran2.Released = new bool?(true);
            glTran2.TranDate = drScheduleTran.RecDate;
            glTran2.ProjectID = drSchedule.ProjectID;
            glTran2.TaskID = drSchedule.TaskID;
            glTran2.TranLineNbr = drScheduleTran.LineNbr;
            glTran2 = ((PXSelectBase<PX.Objects.GL.GLTran>) instance.GLTranModuleBatNbr).Insert(glTran2);
            drScheduleTranList.Add(drScheduleTran);
          }
          catch (PXException ex)
          {
            if (glTran1 != null)
              ((PXSelectBase<PX.Objects.GL.GLTran>) instance.GLTranModuleBatNbr).Delete(glTran1);
            if (glTran2 != null)
              ((PXSelectBase<PX.Objects.GL.GLTran>) instance.GLTranModuleBatNbr).Delete(glTran2);
            PXTrace.WriteError(ex.ToString());
            ((Exception) ex).Data.Add((object) typeof (DRSchedule.scheduleID).Name, (object) tran.ScheduleID);
            this.Exceptions.Add(ex);
          }
        }
        ((PXAction) instance.Save).Press();
        batchList.Add(((PXSelectBase<Batch>) instance.BatchModule).Current);
        foreach (DRScheduleTran deferralTransaction in drScheduleTranList)
        {
          deferralTransaction.BatchNbr = ((PXSelectBase<Batch>) instance.BatchModule).Current.BatchNbr;
          deferralTransaction.Status = "P";
          deferralTransaction.TranDate = ((PXSelectBase<Batch>) instance.BatchModule).Current.DateEntered;
          deferralTransaction.FinPeriodID = ((PXSelectBase<Batch>) instance.BatchModule).Current.FinPeriodID;
          ((PXSelectBase) this.Transactions).Cache.DisableReadItem = true;
          ((PXSelectBase<DRScheduleTran>) this.Transactions).Update(deferralTransaction);
          DRScheduleDetail detailForComponent = this.GetScheduleDetailForComponent(deferralTransaction.ScheduleID, deferralTransaction.ComponentID, deferralTransaction.DetailLineNbr);
          Decimal valueOrDefault1 = deferralTransaction.Amount.GetValueOrDefault();
          DRScheduleDetail drScheduleDetail = detailForComponent;
          Decimal? defAmt = drScheduleDetail.DefAmt;
          Decimal num15 = (Decimal) Math.Sign(valueOrDefault1) * Math.Min(Math.Abs(valueOrDefault1), Math.Abs(detailForComponent.DefAmt.GetValueOrDefault()));
          drScheduleDetail.DefAmt = defAmt.HasValue ? new Decimal?(defAmt.GetValueOrDefault() - num15) : new Decimal?();
          Decimal num16 = 0M;
          PXCache cache = ((PXSelectBase) this.ScheduleDetail).Cache;
          DRScheduleDetail row = detailForComponent;
          defAmt = detailForComponent.DefAmt;
          Decimal valueOrDefault2 = defAmt.GetValueOrDefault();
          ref Decimal local = ref num16;
          PXCurrencyAttribute.CuryConvCury<DRScheduleDetail.curyInfoID>(cache, (object) row, valueOrDefault2, out local);
          detailForComponent.CuryDefAmt = new Decimal?(num16);
          detailForComponent.LastRecFinPeriodID = deferralTransaction.FinPeriodID;
          defAmt = detailForComponent.DefAmt;
          Decimal num17 = 0M;
          if (defAmt.GetValueOrDefault() == num17 & defAmt.HasValue)
          {
            detailForComponent.Status = "C";
            detailForComponent.CloseFinPeriodID = deferralTransaction.FinPeriodID;
            detailForComponent.IsOpen = new bool?(false);
          }
          ((PXSelectBase) this.ScheduleDetail).Cache.DisableReadItem = true;
          ((PXSelectBase<DRScheduleDetail>) this.ScheduleDetail).Update(detailForComponent);
          DRDeferredCode drDeferredCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) detailForComponent.DefCode
          }));
          this.UpdateBalance(deferralTransaction, detailForComponent, drDeferredCode.AccountType);
        }
        ((PXSelectBase) this.Transactions).Cache.Persist((PXDBOperation) 1);
        transactionScope.Complete();
      }
      PXDBCurrencyAttribute.SetBaseCalc<DRScheduleDetail.curyDefAmt>(((PXSelectBase) this.ScheduleDetail).Cache, (object) null, false);
      ((PXGraph) this).Actions.PressSave();
      PXDBCurrencyAttribute.SetBaseCalc<DRScheduleDetail.curyDefAmt>(((PXSelectBase) this.ScheduleDetail).Cache, (object) null, true);
    }
    return batchList;
  }

  /// <summary>
  /// Creates a <see cref="T:PX.Objects.DR.DRSchedule" /> record with multiple <see cref="T:PX.Objects.DR.DRScheduleDetail" />
  /// records depending on the original AR document and document line transactions, as well as
  /// on the deferral code parameters.
  /// </summary>
  /// <param name="arTransaction">An AR document line transaction that corresponds to the original document.</param>
  /// <param name="deferralCode">The deferral code to be used in schedule details.</param>
  /// <param name="originalDocument">Original AR document.</param>
  /// <remarks>
  /// Records are created only in the cache. You have to persist them manually.
  /// </remarks>
  public virtual void CreateSchedule(
    PX.Objects.AR.ARTran arTransaction,
    DRDeferredCode deferralCode,
    PX.Objects.AR.ARRegister originalDocument,
    Decimal amount,
    bool isDraft)
  {
    if (arTransaction == null)
      throw new ArgumentNullException(nameof (arTransaction));
    if (deferralCode == null)
      throw new ArgumentNullException(nameof (deferralCode));
    PX.Objects.IN.InventoryItem inventoryItem = this.GetInventoryItem(arTransaction.InventoryID);
    PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    DRSchedule schedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) arTransaction.TranType,
      (object) arTransaction.RefNbr,
      (object) arTransaction.LineNbr
    }));
    int? nullable = arTransaction.DefScheduleID;
    if (!nullable.HasValue)
    {
      Decimal valueOrDefault = arTransaction.Qty.GetValueOrDefault();
      nullable = arTransaction.InventoryID;
      if (nullable.HasValue)
        valueOrDefault = INUnitAttribute.ConvertToBase(((PXGraph) this).Caches[typeof (PX.Objects.AR.ARTran)], arTransaction.InventoryID, arTransaction.UOM, arTransaction.Qty.GetValueOrDefault(), INPrecision.QUANTITY);
      Decimal? compoundDiscountRate = new Decimal?(1.0M - (arTransaction.DiscPctDR ?? 0.0M) * 0.01M);
      Decimal baseval;
      PXCurrencyAttribute.CuryConvBase<PX.Objects.AR.ARTran.curyInfoID>(((PXGraph) this).Caches[typeof (PX.Objects.AR.ARTran)], (object) arTransaction, arTransaction.CuryUnitPriceDR ?? 0.0M, out baseval);
      DRProcess.DRScheduleParameters scheduleParameters = DRProcess.GetScheduleParameters(originalDocument, arTransaction);
      ScheduleCreator scheduleCreator = new ScheduleCreator((IDREntityStorage) this, (ISubaccountProvider) new DRProcess.ARSubaccountProvider((PXGraph) this), (IBusinessAccountProvider) this, (IInventoryItemProvider) this, this.FinPeriodRepository, (Func<Decimal, Decimal>) (x => PXCurrencyAttribute.BaseRound((PXGraph) this, x)), arTransaction.BranchID, isDraft);
      if (schedule == null)
        scheduleCreator.CreateOriginalSchedule(scheduleParameters, deferralCode, inventoryItem, new AccountSubaccountPair(arTransaction.AccountID, arTransaction.SubID), new Decimal?(amount), new Decimal?(baseval), compoundDiscountRate, new Decimal?(valueOrDefault));
      else
        scheduleCreator.ReevaluateSchedule(schedule, scheduleParameters, deferralCode, new Decimal?(amount), false);
    }
    else if (deferralCode.Method == "C")
    {
      if (!(originalDocument.DocType == "CRM"))
        return;
      this.UpdateOriginalSchedule(arTransaction, deferralCode, amount, originalDocument.DocDate, originalDocument.FinPeriodID, originalDocument.CustomerID, originalDocument.CustomerLocationID);
    }
    else if (originalDocument.DocType == "CRM")
    {
      if (schedule == null)
      {
        this.CreateRelatedSchedule(DRProcess.GetScheduleParameters(originalDocument, arTransaction), arTransaction.BranchID, arTransaction.DefScheduleID, new Decimal?(amount), deferralCode, inventoryItem, arTransaction.AccountID, arTransaction.SubID, isDraft, true);
      }
      else
      {
        DRProcess.DRScheduleParameters scheduleParameters = DRProcess.GetScheduleParameters(originalDocument, arTransaction);
        new ScheduleCreator((IDREntityStorage) this, (ISubaccountProvider) new DRProcess.ARSubaccountProvider((PXGraph) this), (IBusinessAccountProvider) this, (IInventoryItemProvider) this, this.FinPeriodRepository, (Func<Decimal, Decimal>) (x => PXCurrencyAttribute.BaseRound((PXGraph) this, x)), arTransaction.BranchID, isDraft).ReevaluateSchedule(schedule, scheduleParameters, deferralCode, new Decimal?(amount), true);
      }
    }
    else if (originalDocument.DocType == "DRM")
    {
      if (schedule == null)
      {
        this.CreateRelatedSchedule(DRProcess.GetScheduleParameters(originalDocument, arTransaction), arTransaction.BranchID, arTransaction.DefScheduleID, new Decimal?(amount), deferralCode, inventoryItem, arTransaction.AccountID, arTransaction.SubID, isDraft, false);
      }
      else
      {
        DRProcess.DRScheduleParameters scheduleParameters = DRProcess.GetScheduleParameters(originalDocument, arTransaction);
        new ScheduleCreator((IDREntityStorage) this, (ISubaccountProvider) new DRProcess.ARSubaccountProvider((PXGraph) this), (IBusinessAccountProvider) this, (IInventoryItemProvider) this, this.FinPeriodRepository, (Func<Decimal, Decimal>) (x => PXCurrencyAttribute.BaseRound((PXGraph) this, x)), arTransaction.BranchID, isDraft).ReevaluateSchedule(schedule, scheduleParameters, deferralCode, new Decimal?(amount), true);
      }
    }
    else
    {
      if (!(originalDocument.OrigModule == "SO") || !(originalDocument.DocType == "INV") || string.IsNullOrEmpty(originalDocument.OrigRefNbr))
        return;
      if (schedule == null)
      {
        this.CreateRelatedSchedule(DRProcess.GetScheduleParameters(originalDocument, arTransaction), arTransaction.BranchID, arTransaction.DefScheduleID, new Decimal?(amount), deferralCode, inventoryItem, arTransaction.AccountID, arTransaction.SubID, isDraft, false);
      }
      else
      {
        DRProcess.DRScheduleParameters scheduleParameters = DRProcess.GetScheduleParameters(originalDocument, arTransaction);
        new ScheduleCreator((IDREntityStorage) this, (ISubaccountProvider) new DRProcess.ARSubaccountProvider((PXGraph) this), (IBusinessAccountProvider) this, (IInventoryItemProvider) this, this.FinPeriodRepository, (Func<Decimal, Decimal>) (x => PXCurrencyAttribute.BaseRound((PXGraph) this, x)), arTransaction.BranchID, isDraft).ReevaluateSchedule(schedule, scheduleParameters, deferralCode, new Decimal?(amount), true);
      }
    }
  }

  /// <summary>
  /// Creates DRSchedule record with multiple DRScheduleDetail records depending on the DeferredCode schedule.
  /// </summary>
  /// <param name="tran">AP Transaction</param>
  /// <param name="defCode">Deferred Code</param>
  /// <param name="document">AP Invoice</param>
  /// <remarks>
  /// Records are created only in the Cache. You have to manually call Perist method.
  /// </remarks>
  public virtual void CreateSchedule(
    APTran tran,
    DRDeferredCode defCode,
    PX.Objects.AP.APInvoice document,
    Decimal amount,
    bool isDraft)
  {
    if (tran == null)
      throw new ArgumentNullException(nameof (tran));
    if (defCode == null)
      throw new ArgumentNullException(nameof (defCode));
    PX.Objects.IN.InventoryItem inventoryItem = this.GetInventoryItem(tran.InventoryID);
    PXResultset<APSetup>.op_Implicit(PXSelectBase<APSetup, PXSelect<APSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    DRSchedule schedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAP>, And<DRSchedule.docType, Equal<Required<APTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<APTran.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<APTran.lineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) tran.TranType,
      (object) tran.RefNbr,
      (object) tran.LineNbr
    }));
    int? nullable = tran.DefScheduleID;
    if (!nullable.HasValue)
    {
      Decimal valueOrDefault = tran.Qty.GetValueOrDefault();
      nullable = tran.InventoryID;
      if (nullable.HasValue)
        valueOrDefault = INUnitAttribute.ConvertToBase(((PXGraph) this).Caches[typeof (APTran)], tran.InventoryID, tran.UOM, tran.Qty.GetValueOrDefault(), INPrecision.QUANTITY);
      DRProcess.DRScheduleParameters scheduleParameters = DRProcess.GetScheduleParameters((PX.Objects.AP.APRegister) document, tran);
      ScheduleCreator scheduleCreator = new ScheduleCreator((IDREntityStorage) this, (ISubaccountProvider) new DRProcess.APSubaccountProvider((PXGraph) this), (IBusinessAccountProvider) this, (IInventoryItemProvider) this, this.FinPeriodRepository, (Func<Decimal, Decimal>) (x => PXCurrencyAttribute.BaseRound((PXGraph) this, x)), tran.BranchID, isDraft);
      if (schedule != null)
        scheduleCreator.ReevaluateSchedule(schedule, scheduleParameters, defCode, new Decimal?(amount), false);
      else
        scheduleCreator.CreateOriginalSchedule(scheduleParameters, defCode, inventoryItem, new AccountSubaccountPair(tran.AccountID, tran.SubID), new Decimal?(amount), new Decimal?(), new Decimal?(), new Decimal?(valueOrDefault));
    }
    else
    {
      if (!(document.DocType == "ADR") && !(document.DocType == "ACR"))
        return;
      if (schedule != null)
      {
        DRProcess.DRScheduleParameters scheduleParameters = DRProcess.GetScheduleParameters((PX.Objects.AP.APRegister) document, tran);
        new ScheduleCreator((IDREntityStorage) this, (ISubaccountProvider) new DRProcess.APSubaccountProvider((PXGraph) this), (IBusinessAccountProvider) this, (IInventoryItemProvider) this, this.FinPeriodRepository, (Func<Decimal, Decimal>) (x => PXCurrencyAttribute.BaseRound((PXGraph) this, x)), tran.BranchID, isDraft).ReevaluateSchedule(schedule, scheduleParameters, defCode, new Decimal?(amount), true);
      }
      else
        this.CreateRelatedSchedule(DRProcess.GetScheduleParameters((PX.Objects.AP.APRegister) document, tran), tran.BranchID, tran.DefScheduleID, new Decimal?(amount), defCode, inventoryItem, tran.AccountID, tran.SubID, isDraft, document.DocType == "ADR");
    }
  }

  public virtual void RunIntegrityCheck(
    List<DRBalanceValidation.DRBalanceType> list,
    string finPeriodID)
  {
    bool flag = false;
    foreach (DRBalanceValidation.DRBalanceType drBalanceType in list)
    {
      PXProcessing<DRBalanceValidation.DRBalanceType>.SetCurrentItem((object) drBalanceType);
      try
      {
        this.RunIntegrityCheck(drBalanceType, finPeriodID);
        PXProcessing<DRBalanceValidation.DRBalanceType>.SetProcessed();
      }
      catch (Exception ex)
      {
        flag = true;
        PXProcessing<DRBalanceValidation.DRBalanceType>.SetError(ex.Message);
      }
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException("One or more documents could not be released.");
  }

  /// <summary>Rebuilds DR Balance History Tables.</summary>
  /// <param name="item">Type of Balance to rebuild</param>
  public virtual void RunIntegrityCheck(DRBalanceValidation.DRBalanceType item, string finPeriodID)
  {
    string requiredModule;
    switch (item.AccountType)
    {
      case "I":
        requiredModule = "AR";
        break;
      case "E":
        requiredModule = "AP";
        break;
      default:
        throw new PXException("Invalid Deferral Account Type. Only {0} and {1} are supported but {2} was supplied.", new object[3]
        {
          (object) "E",
          (object) "I",
          (object) item.AccountType
        });
    }
    this.ValidateFinPeriod(finPeriodID, requiredModule);
    foreach (PXResult<DRScheduleTran, DRScheduleDetail> pxResult in ((PXSelectBase<DRScheduleTran>) new PXSelectJoin<DRScheduleTran, InnerJoin<DRScheduleDetail, On<DRScheduleDetail.scheduleID, Equal<DRScheduleTran.scheduleID>, And<DRScheduleDetail.componentID, Equal<DRScheduleTran.componentID>, And<DRScheduleDetail.detailLineNbr, Equal<DRScheduleTran.detailLineNbr>>>>>, Where<DRScheduleTran.lineNbr, Equal<DRScheduleDetail.creditLineNbr>, And<DRScheduleDetail.module, Equal<Required<DRScheduleDetail.module>>, And<DRScheduleDetail.status, NotEqual<DRScheduleStatus.DraftStatus>, And<DRScheduleTran.tranPeriodID, GreaterEqual<Required<DRScheduleTran.tranPeriodID>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) requiredModule,
      (object) finPeriodID
    }))
      this.InitBalance(PXResult<DRScheduleTran, DRScheduleDetail>.op_Implicit(pxResult), PXResult<DRScheduleTran, DRScheduleDetail>.op_Implicit(pxResult), item.AccountType);
    foreach (PXResult<DRScheduleTran, DRScheduleDetail> pxResult in ((PXSelectBase<DRScheduleTran>) new PXSelectJoin<DRScheduleTran, InnerJoin<DRScheduleDetail, On<DRScheduleDetail.scheduleID, Equal<DRScheduleTran.scheduleID>, And<DRScheduleDetail.componentID, Equal<DRScheduleTran.componentID>, And<DRScheduleDetail.detailLineNbr, Equal<DRScheduleTran.detailLineNbr>>>>>, Where<DRScheduleTran.lineNbr, NotEqual<DRScheduleDetail.creditLineNbr>, And<DRScheduleDetail.module, Equal<Required<DRScheduleDetail.module>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.OpenStatus>, And<DRScheduleDetail.status, NotEqual<DRScheduleStatus.DraftStatus>, And<DRScheduleTran.tranPeriodID, GreaterEqual<Required<DRScheduleTran.tranPeriodID>>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) requiredModule,
      (object) finPeriodID
    }))
      this.UpdateBalanceProjection(PXResult<DRScheduleTran, DRScheduleDetail>.op_Implicit(pxResult), PXResult<DRScheduleTran, DRScheduleDetail>.op_Implicit(pxResult), item.AccountType);
    foreach (PXResult<DRScheduleTran, DRScheduleDetail> pxResult in ((PXSelectBase<DRScheduleTran>) new PXSelectJoin<DRScheduleTran, InnerJoin<DRScheduleDetail, On<DRScheduleDetail.scheduleID, Equal<DRScheduleTran.scheduleID>, And<DRScheduleDetail.componentID, Equal<DRScheduleTran.componentID>, And<DRScheduleDetail.detailLineNbr, Equal<DRScheduleTran.detailLineNbr>>>>>, Where<DRScheduleTran.lineNbr, NotEqual<DRScheduleDetail.creditLineNbr>, And<DRScheduleDetail.module, Equal<Required<DRScheduleDetail.module>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.PostedStatus>, And<DRScheduleTran.tranPeriodID, GreaterEqual<Required<DRScheduleTran.tranPeriodID>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) requiredModule,
      (object) finPeriodID
    }))
      this.UpdateBalance(PXResult<DRScheduleTran, DRScheduleDetail>.op_Implicit(pxResult), PXResult<DRScheduleTran, DRScheduleDetail>.op_Implicit(pxResult), item.AccountType, true);
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(new int?(0), finPeriodID);
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        switch (requiredModule)
        {
          case "AR":
            PXUpdateJoin<Set<DRRevenueBalance.begBalance, IsNull<DRRevenueBalance2.endBalance, Zero>, Set<DRRevenueBalance.begProjected, IsNull<DRRevenueBalance2.endProjected, Zero>, Set<DRRevenueBalance.pTDDeferred, Zero, Set<DRRevenueBalance.pTDRecognized, Zero, Set<DRRevenueBalance.pTDRecognizedSamePeriod, Zero, Set<DRRevenueBalance.pTDProjected, Zero, Set<DRRevenueBalance.endBalance, IsNull<DRRevenueBalance2.endBalance, Zero>, Set<DRRevenueBalance.endProjected, IsNull<DRRevenueBalance2.endProjected, Zero>>>>>>>>>, DRRevenueBalance, LeftJoin<PX.Objects.GL.Branch, On<DRRevenueBalance.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<DRRevenueBalance.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<OrganizationFinPeriodExt, On<OrganizationFinPeriodExt.masterFinPeriodID, Equal<Required<OrganizationFinPeriodExt.masterFinPeriodID>>, And<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriodExt.organizationID>>>, LeftJoin<DRRevenueBalanceByPeriod, On<DRRevenueBalanceByPeriod.branchID, Equal<DRRevenueBalance.branchID>, And<DRRevenueBalanceByPeriod.acctID, Equal<DRRevenueBalance.acctID>, And<DRRevenueBalanceByPeriod.subID, Equal<DRRevenueBalance.subID>, And<DRRevenueBalanceByPeriod.componentID, Equal<DRRevenueBalance.componentID>, And<DRRevenueBalanceByPeriod.customerID, Equal<DRRevenueBalance.customerID>, And<DRRevenueBalanceByPeriod.projectID, Equal<DRRevenueBalance.projectID>, And<DRRevenueBalanceByPeriod.finPeriodID, Equal<OrganizationFinPeriodExt.prevFinPeriodID>>>>>>>>, LeftJoin<DRRevenueBalance2, On<DRRevenueBalance2.branchID, Equal<DRRevenueBalance.branchID>, And<DRRevenueBalance2.acctID, Equal<DRRevenueBalance.acctID>, And<DRRevenueBalance2.subID, Equal<DRRevenueBalance.subID>, And<DRRevenueBalance2.componentID, Equal<DRRevenueBalance.componentID>, And<DRRevenueBalance2.customerID, Equal<DRRevenueBalance.customerID>, And<DRRevenueBalance2.projectID, Equal<DRRevenueBalance.projectID>, And<DRRevenueBalance2.finPeriodID, Equal<DRRevenueBalanceByPeriod.lastActivityPeriod>>>>>>>>>>>>>, Where<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.masterFinPeriodID>>>>.Update((PXGraph) this, new object[2]
            {
              (object) finPeriodID,
              (object) finPeriodID
            });
            PXUpdateJoin<Set<DRRevenueBalance.tranBegBalance, IsNull<DRRevenueBalance2.tranEndBalance, Zero>, Set<DRRevenueBalance.tranBegProjected, IsNull<DRRevenueBalance2.tranEndProjected, Zero>, Set<DRRevenueBalance.tranPTDDeferred, Zero, Set<DRRevenueBalance.tranPTDRecognized, Zero, Set<DRRevenueBalance.tranPTDRecognizedSamePeriod, Zero, Set<DRRevenueBalance.tranPTDProjected, Zero, Set<DRRevenueBalance.tranEndBalance, IsNull<DRRevenueBalance2.tranEndBalance, Zero>, Set<DRRevenueBalance.tranEndProjected, IsNull<DRRevenueBalance2.tranEndProjected, Zero>>>>>>>>>, DRRevenueBalance, LeftJoin<DRRevenueBalanceByPeriod, On<DRRevenueBalanceByPeriod.branchID, Equal<DRRevenueBalance.branchID>, And<DRRevenueBalanceByPeriod.acctID, Equal<DRRevenueBalance.acctID>, And<DRRevenueBalanceByPeriod.subID, Equal<DRRevenueBalance.subID>, And<DRRevenueBalanceByPeriod.componentID, Equal<DRRevenueBalance.componentID>, And<DRRevenueBalanceByPeriod.customerID, Equal<DRRevenueBalance.customerID>, And<DRRevenueBalanceByPeriod.projectID, Equal<DRRevenueBalance.projectID>, And<DRRevenueBalanceByPeriod.finPeriodID, Equal<Required<FinPeriod.masterFinPeriodID>>>>>>>>>, LeftJoin<DRRevenueBalance2, On<DRRevenueBalance2.branchID, Equal<DRRevenueBalance.branchID>, And<DRRevenueBalance2.acctID, Equal<DRRevenueBalance.acctID>, And<DRRevenueBalance2.subID, Equal<DRRevenueBalance.subID>, And<DRRevenueBalance2.componentID, Equal<DRRevenueBalance.componentID>, And<DRRevenueBalance2.customerID, Equal<DRRevenueBalance.customerID>, And<DRRevenueBalance2.projectID, Equal<DRRevenueBalance.projectID>, And<DRRevenueBalance2.finPeriodID, Equal<DRRevenueBalanceByPeriod.lastActivityPeriod>>>>>>>>>>, Where<DRRevenueBalance.finPeriodID, GreaterEqual<Required<DRRevenueBalance.finPeriodID>>>>.Update((PXGraph) this, new object[2]
            {
              (object) prevPeriod?.FinPeriodID,
              (object) finPeriodID
            });
            PXUpdateJoin<Set<DRRevenueProjection.pTDRecognized, Zero, Set<DRRevenueProjection.pTDRecognizedSamePeriod, Zero, Set<DRRevenueProjection.pTDProjected, Zero>>>, DRRevenueProjection, LeftJoin<PX.Objects.GL.Branch, On<DRRevenueProjection.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<DRRevenueProjection.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>>>, Where<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.masterFinPeriodID>>>>.Update((PXGraph) this, new object[1]
            {
              (object) finPeriodID
            });
            PXUpdate<Set<DRRevenueProjection.tranPTDRecognized, Zero, Set<DRRevenueProjection.tranPTDRecognizedSamePeriod, Zero, Set<DRRevenueProjection.tranPTDProjected, Zero>>>, DRRevenueProjection, Where<DRRevenueProjection.finPeriodID, GreaterEqual<Required<DRRevenueProjection.finPeriodID>>>>.Update((PXGraph) this, new object[1]
            {
              (object) finPeriodID
            });
            if (((PXSelectBase<DRSetup>) this.Setup).Current.PendingRevenueValidate.GetValueOrDefault())
            {
              PXDatabase.Update<DRSetup>(new PXDataFieldParam[1]
              {
                (PXDataFieldParam) new PXDataFieldAssign<DRSetup.pendingRevenueValidate>((object) false)
              });
              break;
            }
            break;
          case "AP":
            PXUpdateJoin<Set<DRExpenseBalance.begBalance, IsNull<DRExpenseBalance2.endBalance, Zero>, Set<DRExpenseBalance.begProjected, IsNull<DRExpenseBalance2.endProjected, Zero>, Set<DRExpenseBalance.pTDDeferred, Zero, Set<DRExpenseBalance.pTDRecognized, Zero, Set<DRExpenseBalance.pTDRecognizedSamePeriod, Zero, Set<DRExpenseBalance.pTDProjected, Zero, Set<DRExpenseBalance.endBalance, IsNull<DRExpenseBalance2.endBalance, Zero>, Set<DRExpenseBalance.endProjected, IsNull<DRExpenseBalance2.endProjected, Zero>>>>>>>>>, DRExpenseBalance, LeftJoin<PX.Objects.GL.Branch, On<DRExpenseBalance.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<DRExpenseBalance.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>, LeftJoin<OrganizationFinPeriodExt, On<OrganizationFinPeriodExt.masterFinPeriodID, Equal<Required<OrganizationFinPeriodExt.masterFinPeriodID>>, And<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriodExt.organizationID>>>, LeftJoin<DRExpenseBalanceByPeriod, On<DRExpenseBalanceByPeriod.branchID, Equal<DRExpenseBalance.branchID>, And<DRExpenseBalanceByPeriod.acctID, Equal<DRExpenseBalance.acctID>, And<DRExpenseBalanceByPeriod.subID, Equal<DRExpenseBalance.subID>, And<DRExpenseBalanceByPeriod.componentID, Equal<DRExpenseBalance.componentID>, And<DRExpenseBalanceByPeriod.vendorID, Equal<DRExpenseBalance.vendorID>, And<DRExpenseBalanceByPeriod.projectID, Equal<DRExpenseBalance.projectID>, And<DRExpenseBalanceByPeriod.finPeriodID, Equal<OrganizationFinPeriodExt.prevFinPeriodID>>>>>>>>, LeftJoin<DRExpenseBalance2, On<DRExpenseBalance2.branchID, Equal<DRExpenseBalance.branchID>, And<DRExpenseBalance2.acctID, Equal<DRExpenseBalance.acctID>, And<DRExpenseBalance2.subID, Equal<DRExpenseBalance.subID>, And<DRExpenseBalance2.componentID, Equal<DRExpenseBalance.componentID>, And<DRExpenseBalance2.vendorID, Equal<DRExpenseBalance.vendorID>, And<DRExpenseBalance2.projectID, Equal<DRExpenseBalance.projectID>, And<DRExpenseBalance2.finPeriodID, Equal<DRExpenseBalanceByPeriod.lastActivityPeriod>>>>>>>>>>>>>, Where<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.masterFinPeriodID>>>>.Update((PXGraph) this, new object[2]
            {
              (object) finPeriodID,
              (object) finPeriodID
            });
            PXUpdateJoin<Set<DRExpenseBalance.tranBegBalance, IsNull<DRExpenseBalance2.tranEndBalance, Zero>, Set<DRExpenseBalance.tranBegProjected, IsNull<DRExpenseBalance2.tranEndProjected, Zero>, Set<DRExpenseBalance.tranPTDDeferred, Zero, Set<DRExpenseBalance.tranPTDRecognized, Zero, Set<DRExpenseBalance.tranPTDRecognizedSamePeriod, Zero, Set<DRExpenseBalance.tranPTDProjected, Zero, Set<DRExpenseBalance.tranEndBalance, IsNull<DRExpenseBalance2.tranEndBalance, Zero>, Set<DRExpenseBalance.tranEndProjected, IsNull<DRExpenseBalance2.tranEndProjected, Zero>>>>>>>>>, DRExpenseBalance, LeftJoin<DRExpenseBalanceByPeriod, On<DRExpenseBalanceByPeriod.branchID, Equal<DRExpenseBalance.branchID>, And<DRExpenseBalanceByPeriod.acctID, Equal<DRExpenseBalance.acctID>, And<DRExpenseBalanceByPeriod.subID, Equal<DRExpenseBalance.subID>, And<DRExpenseBalanceByPeriod.componentID, Equal<DRExpenseBalance.componentID>, And<DRExpenseBalanceByPeriod.vendorID, Equal<DRExpenseBalance.vendorID>, And<DRExpenseBalanceByPeriod.projectID, Equal<DRExpenseBalance.projectID>, And<DRExpenseBalanceByPeriod.finPeriodID, Equal<Required<FinPeriod.masterFinPeriodID>>>>>>>>>, LeftJoin<DRExpenseBalance2, On<DRExpenseBalance2.branchID, Equal<DRExpenseBalance.branchID>, And<DRExpenseBalance2.acctID, Equal<DRExpenseBalance.acctID>, And<DRExpenseBalance2.subID, Equal<DRExpenseBalance.subID>, And<DRExpenseBalance2.componentID, Equal<DRExpenseBalance.componentID>, And<DRExpenseBalance2.vendorID, Equal<DRExpenseBalance.vendorID>, And<DRExpenseBalance2.projectID, Equal<DRExpenseBalance.projectID>, And<DRExpenseBalance2.finPeriodID, Equal<DRExpenseBalanceByPeriod.lastActivityPeriod>>>>>>>>>>, Where<DRExpenseBalance.finPeriodID, GreaterEqual<Required<DRExpenseBalance.finPeriodID>>>>.Update((PXGraph) this, new object[2]
            {
              (object) prevPeriod?.FinPeriodID,
              (object) finPeriodID
            });
            PXUpdateJoin<Set<DRExpenseProjection.pTDRecognized, Zero, Set<DRExpenseProjection.pTDRecognizedSamePeriod, Zero, Set<DRExpenseProjection.pTDProjected, Zero>>>, DRExpenseProjection, LeftJoin<PX.Objects.GL.Branch, On<DRExpenseProjection.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FinPeriod, On<DRExpenseProjection.finPeriodID, Equal<FinPeriod.finPeriodID>, And<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>>>>, Where<FinPeriod.masterFinPeriodID, GreaterEqual<Required<FinPeriod.masterFinPeriodID>>>>.Update((PXGraph) this, new object[1]
            {
              (object) finPeriodID
            });
            PXUpdate<Set<DRExpenseProjection.tranPTDRecognized, Zero, Set<DRExpenseProjection.tranPTDRecognizedSamePeriod, Zero, Set<DRExpenseProjection.tranPTDProjected, Zero>>>, DRExpenseProjection, Where<DRExpenseProjection.finPeriodID, GreaterEqual<Required<DRExpenseProjection.finPeriodID>>>>.Update((PXGraph) this, new object[1]
            {
              (object) finPeriodID
            });
            if (((PXSelectBase<DRSetup>) this.Setup).Current.PendingExpenseValidate.GetValueOrDefault())
            {
              PXDatabase.Update<DRSetup>(new PXDataFieldParam[1]
              {
                (PXDataFieldParam) new PXDataFieldAssign<DRSetup.pendingExpenseValidate>((object) false)
              });
              break;
            }
            break;
        }
        ((PXGraph) this).Actions.PressSave();
        transactionScope.Complete((PXGraph) this);
      }
    }
  }

  private void ValidateFinPeriod(string finPeriodID, string requiredModule)
  {
    if (((PXSelectBase<DRSetup>) this.Setup).Current.PendingRevenueValidate.GetValueOrDefault() && requiredModule == "AR")
    {
      string str = finPeriodID;
      DRRevenueBalance drRevenueBalance = PXResultset<DRRevenueBalance>.op_Implicit(PXSelectBase<DRRevenueBalance, PXSelectOrderBy<DRRevenueBalance, OrderBy<Asc<DRRevenueBalance.finPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
      if (drRevenueBalance != null && string.Compare(drRevenueBalance.FinPeriodID, str) < 0)
        str = drRevenueBalance.FinPeriodID;
      DRRevenueProjection revenueProjection = PXResultset<DRRevenueProjection>.op_Implicit(PXSelectBase<DRRevenueProjection, PXSelectOrderBy<DRRevenueProjection, OrderBy<Asc<DRRevenueProjection.finPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
      if (revenueProjection != null && string.Compare(revenueProjection.FinPeriodID, str) < 0)
        str = revenueProjection.FinPeriodID;
      if (string.Compare(str, finPeriodID) < 0)
        throw new PXException("Revenue deferrals have to be validated. Start validation from the {0} financial period.", new object[1]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(str)
        });
    }
    if (!((PXSelectBase<DRSetup>) this.Setup).Current.PendingExpenseValidate.GetValueOrDefault() || !(requiredModule == "AP"))
      return;
    string str1 = finPeriodID;
    DRExpenseBalance drExpenseBalance = PXResultset<DRExpenseBalance>.op_Implicit(PXSelectBase<DRExpenseBalance, PXSelectOrderBy<DRExpenseBalance, OrderBy<Asc<DRExpenseBalance.finPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
    if (drExpenseBalance != null && string.Compare(drExpenseBalance.FinPeriodID, str1) < 0)
      str1 = drExpenseBalance.FinPeriodID;
    DRExpenseProjection expenseProjection = PXResultset<DRExpenseProjection>.op_Implicit(PXSelectBase<DRExpenseProjection, PXSelectOrderBy<DRExpenseProjection, OrderBy<Asc<DRExpenseProjection.finPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
    if (expenseProjection != null && string.Compare(expenseProjection.FinPeriodID, str1) < 0)
      str1 = expenseProjection.FinPeriodID;
    if (string.Compare(str1, finPeriodID) < 0)
      throw new PXException("Expense deferrals have to be validated. Start validation from the {0} financial period.", new object[1]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(str1)
      });
  }

  /// <summary>
  /// Gets the deferral schedule parameters based on the
  /// original document and document line transaction.
  /// </summary>
  public static DRProcess.DRScheduleParameters GetScheduleParameters(
    PX.Objects.AR.ARRegister document,
    PX.Objects.AR.ARTran tran)
  {
    DRProcess.DRScheduleParameters scheduleParameters = new DRProcess.DRScheduleParameters();
    scheduleParameters.Module = "AR";
    scheduleParameters.DocType = tran.TranType;
    scheduleParameters.RefNbr = tran.RefNbr;
    scheduleParameters.LineNbr = tran.LineNbr;
    scheduleParameters.DocDate = document.DocDate;
    scheduleParameters.BAccountID = document.CustomerID;
    scheduleParameters.BAccountLocID = document.CustomerLocationID;
    scheduleParameters.BaseCuryID = PXAccess.GetBranch(document.BranchID).BaseCuryID;
    scheduleParameters.FinPeriodID = tran.TranPeriodID;
    scheduleParameters.TranDesc = tran.TranDesc;
    scheduleParameters.ProjectID = tran.ProjectID;
    scheduleParameters.TaskID = tran.TaskID;
    scheduleParameters.EmployeeID = tran.EmployeeID;
    scheduleParameters.SalesPersonID = tran.SalesPersonID;
    scheduleParameters.SubID = tran.SubID;
    scheduleParameters.TermStartDate = tran.DRTermStartDate;
    scheduleParameters.TermEndDate = tran.DRTermEndDate;
    return scheduleParameters;
  }

  /// <summary>
  /// Gets the deferral schedule parameters based on the
  /// original document and document line transaction.
  /// </summary>
  public static DRProcess.DRScheduleParameters GetScheduleParameters(
    PX.Objects.AP.APRegister document,
    APTran tran)
  {
    DRProcess.DRScheduleParameters scheduleParameters = new DRProcess.DRScheduleParameters();
    scheduleParameters.Module = "AP";
    scheduleParameters.DocType = tran.TranType;
    scheduleParameters.RefNbr = tran.RefNbr;
    scheduleParameters.LineNbr = tran.LineNbr;
    scheduleParameters.DocDate = document.DocDate;
    scheduleParameters.BAccountID = document.VendorID;
    scheduleParameters.BAccountLocID = document.VendorLocationID;
    scheduleParameters.BaseCuryID = PXAccess.GetBranch(document.BranchID).BaseCuryID;
    scheduleParameters.FinPeriodID = tran.TranPeriodID;
    scheduleParameters.TranDesc = tran.TranDesc;
    scheduleParameters.ProjectID = tran.ProjectID;
    scheduleParameters.TaskID = tran.TaskID;
    scheduleParameters.EmployeeID = tran.EmployeeID;
    scheduleParameters.SubID = tran.SubID;
    scheduleParameters.TermStartDate = tran.DRTermStartDate;
    scheduleParameters.TermEndDate = tran.DRTermEndDate;
    return scheduleParameters;
  }

  /// <param name="takeFromSales">
  /// If <c>true</c>, deferral transactions that are already posted will be handled.
  /// If <c>false</c>, only open deferral transactions will be used to create a related schedule.
  /// </param>
  private void CreateRelatedSchedule(
    DRProcess.DRScheduleParameters scheduleParameters,
    int? branchID,
    int? defScheduleID,
    Decimal? tranAmt,
    DRDeferredCode defCode,
    PX.Objects.IN.InventoryItem inventoryItem,
    int? acctID,
    int? subID,
    bool isDraft,
    bool accountForPostedTransactions)
  {
    DRSchedule copy = ((IDREntityStorage) this).CreateCopy((DRSchedule) scheduleParameters);
    copy.IsDraft = new bool?(isDraft);
    copy.IsCustom = new bool?(false);
    DRSchedule drSchedule = ((PXSelectBase<DRSchedule>) this.Schedule).Insert(copy);
    PXResultset<DRScheduleDetail> details = PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.GetDeferralSchedule(defScheduleID).ScheduleID
    });
    Decimal num1 = this.SumTotal(details);
    Decimal num2 = tranAmt.Value;
    List<DRScheduleDetail> list = GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) details).Where<DRScheduleDetail>((Func<DRScheduleDetail, bool>) (detail => !detail.IsResidual.GetValueOrDefault())).ToList<DRScheduleDetail>();
    Decimal num3 = 0M;
    foreach (DRScheduleDetail drScheduleDetail1 in list)
    {
      Decimal? nullable;
      Decimal num4;
      if (!(num1 == 0M))
      {
        nullable = drScheduleDetail1.CuryTotalAmt;
        num4 = nullable.Value * num2 / num1;
      }
      else
        num4 = 0M;
      Decimal num5 = num4;
      Decimal amount = PXDBCurrencyAttribute.BaseRound((PXGraph) this, num5);
      Decimal num6 = 0M;
      if (accountForPostedTransactions)
      {
        nullable = drScheduleDetail1.CuryTotalAmt;
        if (nullable.Value != 0M)
        {
          Decimal num7 = num5;
          nullable = drScheduleDetail1.CuryTotalAmt;
          Decimal num8 = nullable.Value;
          nullable = drScheduleDetail1.CuryDefAmt;
          Decimal num9 = nullable.Value;
          Decimal num10 = num8 - num9;
          Decimal num11 = num7 * num10;
          nullable = drScheduleDetail1.CuryTotalAmt;
          Decimal num12 = nullable.Value;
          num6 = num11 / num12;
        }
      }
      Decimal amountToDistributeForPosted = PXDBCurrencyAttribute.BaseRound((PXGraph) this, num6);
      Decimal amountToDistributeForUnposted = PXDBCurrencyAttribute.BaseRound((PXGraph) this, num5 - amountToDistributeForPosted);
      INComponent component = (INComponent) null;
      DRDeferredCode defCode1 = (DRDeferredCode) null;
      if (inventoryItem != null)
      {
        component = this.GetInventoryItemComponent(inventoryItem.InventoryID, drScheduleDetail1.ComponentID);
        if (component != null)
          defCode1 = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) component.DeferredCode
          }));
      }
      PX.Objects.IN.InventoryItem inventoryItem1 = this.GetInventoryItem(drScheduleDetail1.ComponentID);
      DRScheduleDetail drScheduleDetail2 = defCode1 == null ? this.InsertScheduleDetail(branchID, drSchedule, inventoryItem1 == null ? new int?(0) : inventoryItem1.InventoryID, defCode, amount, drScheduleDetail1.DefAcctID, drScheduleDetail1.DefSubID, acctID, subID, isDraft) : this.InsertScheduleDetail(branchID, drSchedule, component, inventoryItem1, defCode1, amount, drScheduleDetail1.DefAcctID, drScheduleDetail1.DefSubID, isDraft);
      num3 += amount;
      IList<DRScheduleTran> tranList = (IList<DRScheduleTran>) new List<DRScheduleTran>();
      DRDeferredCode deferralCode = defCode1 ?? defCode;
      IEnumerable<DRScheduleTran> originalPostedTransactions = (IEnumerable<DRScheduleTran>) null;
      if (accountForPostedTransactions)
        originalPostedTransactions = GraphHelper.RowCast<DRScheduleTran>((IEnumerable) PXSelectBase<DRScheduleTran, PXSelect<DRScheduleTran, Where<DRScheduleTran.status, Equal<DRScheduleTranStatus.PostedStatus>, And<DRScheduleTran.scheduleID, Equal<Required<DRScheduleTran.scheduleID>>, And<DRScheduleTran.componentID, Equal<Required<DRScheduleTran.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Required<DRScheduleTran.detailLineNbr>>, And<DRScheduleTran.lineNbr, NotEqual<Required<DRScheduleTran.lineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
        {
          (object) drScheduleDetail1.ScheduleID,
          (object) drScheduleDetail1.ComponentID,
          (object) drScheduleDetail1.DetailLineNbr,
          (object) drScheduleDetail1.CreditLineNbr
        }));
      if (amountToDistributeForUnposted != 0M || accountForPostedTransactions && amountToDistributeForPosted != 0M)
      {
        IEnumerable<DRScheduleTran> originalOpenTransactions = GraphHelper.RowCast<DRScheduleTran>((IEnumerable) PXSelectBase<DRScheduleTran, PXSelect<DRScheduleTran, Where<DRScheduleTran.status, Equal<Required<DRScheduleTran.status>>, And<DRScheduleTran.scheduleID, Equal<Required<DRScheduleTran.scheduleID>>, And<DRScheduleTran.componentID, Equal<Required<DRScheduleTran.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Required<DRScheduleTran.detailLineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[4]
        {
          (object) (deferralCode.Method == "C" ? "J" : "O"),
          (object) drScheduleDetail1.ScheduleID,
          (object) drScheduleDetail1.ComponentID,
          (object) drScheduleDetail1.DetailLineNbr
        }));
        foreach (DRScheduleTran relatedTransaction in (IEnumerable<DRScheduleTran>) this.GetTransactionsGenerator(deferralCode).GenerateRelatedTransactions(drScheduleDetail2, originalOpenTransactions, originalPostedTransactions, amountToDistributeForUnposted, amountToDistributeForPosted, branchID))
        {
          ((PXSelectBase<DRScheduleTran>) this.Transactions).Insert(relatedTransaction);
          tranList.Add(relatedTransaction);
        }
      }
      this.UpdateBalanceProjection((IEnumerable<DRScheduleTran>) tranList, drScheduleDetail2, defCode.AccountType);
    }
    DRScheduleDetail reidualDetail = GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) details).FirstOrDefault<DRScheduleDetail>((Func<DRScheduleDetail, bool>) (detail => detail.IsResidual.GetValueOrDefault()));
    if (reidualDetail == null)
      return;
    Decimal amount1 = num2 - num3;
    this.InsertResidualScheduleDetail(drSchedule, reidualDetail, amount1, isDraft);
  }

  protected void UpdateOriginalSchedule(
    PX.Objects.AR.ARTran tran,
    DRDeferredCode defCode,
    Decimal amount,
    DateTime? docDate,
    string docFinPeriod,
    int? bAccountID,
    int? locationID)
  {
    DRScheduleDetail sd = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.GetDeferralSchedule(tran.DefScheduleID).ScheduleID
    }));
    Decimal num1 = sd.CuryTotalAmt.Value;
    Decimal num2;
    if (num1 >= 0M && num1 <= amount || num1 < 0M && num1 >= amount)
    {
      num2 = num1;
      Decimal num3 = amount - num1;
    }
    else
      num2 = amount;
    Decimal num4 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, sd.CuryTotalAmt.Value * num2 / num1 * (sd.CuryTotalAmt.Value - sd.CuryDefAmt.Value) / sd.CuryTotalAmt.Value);
    Decimal num5 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, sd.CuryTotalAmt.Value * amount / num1 - num4);
    this.GetInventoryItem(sd.ComponentID);
    Decimal? nullable1;
    if (num4 != 0M)
    {
      DRScheduleTran tran1 = new DRScheduleTran();
      tran1.BranchID = sd.BranchID;
      tran1.AccountID = sd.AccountID;
      tran1.SubID = sd.SubID;
      tran1.Amount = new Decimal?(-num4);
      tran1.RecDate = ((PXGraph) this).Accessinfo.BusinessDate;
      tran1.FinPeriodID = docFinPeriod;
      tran1.ScheduleID = sd.ScheduleID;
      tran1.ComponentID = sd.ComponentID;
      tran1.DetailLineNbr = sd.DetailLineNbr;
      tran1.Status = "O";
      ((PXSelectBase<DRScheduleTran>) this.Transactions).Insert(tran1);
      this.UpdateBalanceProjection(tran1, sd, defCode.AccountType);
      DRScheduleDetail drScheduleDetail = sd;
      nullable1 = drScheduleDetail.CuryDefAmt;
      Decimal num6 = num4;
      drScheduleDetail.CuryDefAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num6) : new Decimal?();
    }
    if (num5 != 0M)
    {
      DRScheduleDetail drScheduleDetail = sd;
      nullable1 = drScheduleDetail.CuryDefAmt;
      Decimal num7 = num5;
      drScheduleDetail.CuryDefAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num7) : new Decimal?();
      PXResultset<DRScheduleTran> pxResultset = ((PXSelectBase<DRScheduleTran>) new PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Required<DRSchedule.scheduleID>>, And<DRScheduleTran.componentID, Equal<Required<DRScheduleTran.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Required<DRScheduleTran.detailLineNbr>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.ProjectedStatus>>>>>>((PXGraph) this)).Select(new object[3]
      {
        (object) sd.ScheduleID,
        (object) sd.ComponentID,
        (object) sd.DetailLineNbr
      });
      Decimal num8 = PXCurrencyAttribute.BaseRound((PXGraph) this, num5 / (Decimal) pxResultset.Count);
      foreach (PXResult<DRScheduleTran> pxResult in pxResultset)
      {
        DRScheduleTran drScheduleTran1 = PXResult<DRScheduleTran>.op_Implicit(pxResult);
        DRRevenueBalance drRevenueBalance1 = new DRRevenueBalance();
        drRevenueBalance1.FinPeriodID = tran.FinPeriodID;
        drRevenueBalance1.BranchID = sd.BranchID;
        drRevenueBalance1.AcctID = sd.DefAcctID;
        drRevenueBalance1.SubID = sd.DefSubID;
        DRRevenueBalance drRevenueBalance2 = drRevenueBalance1;
        int? nullable2 = sd.ComponentID;
        int? nullable3 = new int?(nullable2.GetValueOrDefault());
        drRevenueBalance2.ComponentID = nullable3;
        DRRevenueBalance drRevenueBalance3 = drRevenueBalance1;
        nullable2 = sd.ProjectID;
        int? nullable4 = new int?(nullable2.GetValueOrDefault());
        drRevenueBalance3.ProjectID = nullable4;
        drRevenueBalance1.CustomerID = sd.BAccountID;
        DRRevenueBalance drRevenueBalance4 = ((PXSelectBase<DRRevenueBalance>) this.RevenueBalance).Insert(drRevenueBalance1);
        DRRevenueBalance drRevenueBalance5 = drRevenueBalance4;
        nullable1 = drRevenueBalance5.PTDProjected;
        Decimal? nullable5 = drScheduleTran1.Amount;
        drRevenueBalance5.PTDProjected = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        DRRevenueBalance drRevenueBalance6 = drRevenueBalance4;
        nullable5 = drRevenueBalance6.EndProjected;
        nullable1 = drScheduleTran1.Amount;
        drRevenueBalance6.EndProjected = nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        DRRevenueProjectionAccum revenueProjectionAccum1 = new DRRevenueProjectionAccum();
        revenueProjectionAccum1.FinPeriodID = tran.FinPeriodID;
        revenueProjectionAccum1.BranchID = sd.BranchID;
        revenueProjectionAccum1.AcctID = sd.AccountID;
        revenueProjectionAccum1.SubID = sd.SubID;
        DRRevenueProjectionAccum revenueProjectionAccum2 = revenueProjectionAccum1;
        nullable2 = sd.ComponentID;
        int? nullable6 = new int?(nullable2.GetValueOrDefault());
        revenueProjectionAccum2.ComponentID = nullable6;
        DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum1;
        nullable2 = sd.ProjectID;
        int? nullable7 = new int?(nullable2.GetValueOrDefault());
        revenueProjectionAccum3.ProjectID = nullable7;
        revenueProjectionAccum1.CustomerID = sd.BAccountID;
        DRRevenueProjectionAccum revenueProjectionAccum4 = ((PXSelectBase<DRRevenueProjectionAccum>) this.RevenueProjection).Insert(revenueProjectionAccum1);
        nullable1 = revenueProjectionAccum4.PTDProjected;
        nullable5 = drScheduleTran1.Amount;
        revenueProjectionAccum4.PTDProjected = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        DRScheduleTran drScheduleTran2 = drScheduleTran1;
        nullable5 = drScheduleTran2.Amount;
        Decimal num9 = num8;
        Decimal? nullable8;
        if (!nullable5.HasValue)
        {
          nullable1 = new Decimal?();
          nullable8 = nullable1;
        }
        else
          nullable8 = new Decimal?(nullable5.GetValueOrDefault() - num9);
        drScheduleTran2.Amount = nullable8;
        ((PXSelectBase<DRScheduleTran>) this.Transactions).Update(drScheduleTran1);
        DRRevenueBalance drRevenueBalance7 = new DRRevenueBalance();
        drRevenueBalance7.FinPeriodID = tran.FinPeriodID;
        drRevenueBalance7.BranchID = sd.BranchID;
        drRevenueBalance7.AcctID = sd.DefAcctID;
        drRevenueBalance7.SubID = sd.DefSubID;
        DRRevenueBalance drRevenueBalance8 = drRevenueBalance7;
        nullable2 = sd.ComponentID;
        int? nullable9 = new int?(nullable2.GetValueOrDefault());
        drRevenueBalance8.ComponentID = nullable9;
        DRRevenueBalance drRevenueBalance9 = drRevenueBalance7;
        nullable2 = sd.ProjectID;
        int? nullable10 = new int?(nullable2.GetValueOrDefault());
        drRevenueBalance9.ProjectID = nullable10;
        drRevenueBalance7.CustomerID = sd.BAccountID;
        DRRevenueBalance drRevenueBalance10 = ((PXSelectBase<DRRevenueBalance>) this.RevenueBalance).Insert(drRevenueBalance7);
        DRRevenueBalance drRevenueBalance11 = drRevenueBalance10;
        nullable5 = drRevenueBalance11.PTDProjected;
        nullable1 = drScheduleTran1.Amount;
        drRevenueBalance11.PTDProjected = nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        DRRevenueBalance drRevenueBalance12 = drRevenueBalance10;
        nullable1 = drRevenueBalance12.EndProjected;
        nullable5 = drScheduleTran1.Amount;
        drRevenueBalance12.EndProjected = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        DRRevenueProjectionAccum revenueProjectionAccum5 = new DRRevenueProjectionAccum();
        revenueProjectionAccum5.FinPeriodID = tran.FinPeriodID;
        revenueProjectionAccum5.BranchID = sd.BranchID;
        revenueProjectionAccum5.AcctID = sd.AccountID;
        revenueProjectionAccum5.SubID = sd.SubID;
        DRRevenueProjectionAccum revenueProjectionAccum6 = revenueProjectionAccum5;
        nullable2 = sd.ComponentID;
        int? nullable11 = new int?(nullable2.GetValueOrDefault());
        revenueProjectionAccum6.ComponentID = nullable11;
        DRRevenueProjectionAccum revenueProjectionAccum7 = revenueProjectionAccum5;
        nullable2 = sd.ProjectID;
        int? nullable12 = new int?(nullable2.GetValueOrDefault());
        revenueProjectionAccum7.ProjectID = nullable12;
        revenueProjectionAccum5.CustomerID = sd.BAccountID;
        DRRevenueProjectionAccum revenueProjectionAccum8 = ((PXSelectBase<DRRevenueProjectionAccum>) this.RevenueProjection).Insert(revenueProjectionAccum5);
        nullable5 = revenueProjectionAccum8.PTDProjected;
        nullable1 = drScheduleTran1.Amount;
        revenueProjectionAccum8.PTDProjected = nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      }
    }
    ((PXSelectBase<DRScheduleDetail>) this.ScheduleDetail).Update(sd);
    DRRevenueBalance drRevenueBalance13 = new DRRevenueBalance();
    drRevenueBalance13.FinPeriodID = docFinPeriod;
    drRevenueBalance13.BranchID = sd.BranchID;
    drRevenueBalance13.AcctID = sd.DefAcctID;
    drRevenueBalance13.SubID = sd.DefSubID;
    DRRevenueBalance drRevenueBalance14 = drRevenueBalance13;
    int? nullable13 = sd.ComponentID;
    int? nullable14 = new int?(nullable13.GetValueOrDefault());
    drRevenueBalance14.ComponentID = nullable14;
    DRRevenueBalance drRevenueBalance15 = drRevenueBalance13;
    nullable13 = sd.ProjectID;
    int? nullable15 = new int?(nullable13.GetValueOrDefault());
    drRevenueBalance15.ProjectID = nullable15;
    drRevenueBalance13.CustomerID = sd.BAccountID;
    DRRevenueBalance drRevenueBalance16 = ((PXSelectBase<DRRevenueBalance>) this.RevenueBalance).Insert(drRevenueBalance13);
    DRRevenueBalance drRevenueBalance17 = drRevenueBalance16;
    nullable1 = drRevenueBalance17.PTDDeferred;
    Decimal num10 = amount;
    drRevenueBalance17.PTDDeferred = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num10) : new Decimal?();
    DRRevenueBalance drRevenueBalance18 = drRevenueBalance16;
    nullable1 = drRevenueBalance18.EndBalance;
    Decimal num11 = amount;
    drRevenueBalance18.EndBalance = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num11) : new Decimal?();
  }

  private Decimal SumTotal(PXResultset<DRScheduleDetail> details)
  {
    Decimal num = 0M;
    foreach (PXResult<DRScheduleDetail> detail in details)
    {
      DRScheduleDetail drScheduleDetail = PXResult<DRScheduleDetail>.op_Implicit(detail);
      num += drScheduleDetail.CuryTotalAmt.Value;
    }
    return num;
  }

  private DRSchedule GetDeferralSchedule(int? scheduleID)
  {
    return PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) scheduleID
    }));
  }

  protected PX.Objects.IN.InventoryItem GetInventoryItem(int? inventoryItemID)
  {
    return PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryItemID
    }));
  }

  protected INComponent GetInventoryItemComponent(int? inventoryID, int? componentID)
  {
    return PXResultset<INComponent>.op_Implicit(PXSelectBase<INComponent, PXSelect<INComponent, Where<INComponent.inventoryID, Equal<Required<INComponent.inventoryID>>, And<INComponent.componentID, Equal<Required<INComponent.componentID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) inventoryID,
      (object) componentID
    }));
  }

  public PXResultset<DRScheduleDetail> GetScheduleDetailByOrigLineNbr(int? scheduleID, int? lineNbr)
  {
    return PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) scheduleID,
      (object) lineNbr
    });
  }

  /// <summary>
  /// Retrieves all schedule details for the specified
  /// deferral schedule ID.
  /// </summary>
  public IList<DRScheduleDetail> GetScheduleDetails(int? scheduleID)
  {
    return (IList<DRScheduleDetail>) GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) scheduleID
    })).ToList<DRScheduleDetail>();
  }

  public EPEmployee GetEmployee(int? employeeID)
  {
    return PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) employeeID
    }));
  }

  public PX.Objects.CR.Location GetLocation(int? businessAccountID, int? businessAccountLocationId)
  {
    return PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) businessAccountID,
      (object) businessAccountLocationId
    }));
  }

  /// <summary>
  /// Generates and adds deferral transactions to the specified
  /// deferral schedule and schedule detail.
  /// </summary>
  public IEnumerable<DRScheduleTran> GenerateAndAddDeferralTransactions(
    DRSchedule deferralSchedule,
    DRScheduleDetail scheduleDetail,
    DRDeferredCode deferralCode)
  {
    return (IEnumerable<DRScheduleTran>) this.GetTransactionsGenerator(deferralCode).GenerateTransactions(deferralSchedule, scheduleDetail).Select<DRScheduleTran, DRScheduleTran>((Func<DRScheduleTran, DRScheduleTran>) (transaction => ((PXSelectBase<DRScheduleTran>) this.Transactions).Insert(transaction))).ToArray<DRScheduleTran>();
  }

  protected void UpdateBalanceProjection(
    IEnumerable<DRScheduleTran> tranList,
    DRScheduleDetail sd,
    string deferredAccountType)
  {
    foreach (DRScheduleTran tran in tranList)
      this.UpdateBalanceProjection(tran, sd, deferredAccountType);
  }

  private void UpdateBalanceProjection(
    DRScheduleTran tran,
    DRScheduleDetail sd,
    string deferredAccountType)
  {
    switch (deferredAccountType)
    {
      case "E":
        this.UpdateExpenseBalanceProjection(tran, sd);
        this.UpdateExpenseProjection(tran, sd);
        break;
      case "I":
        this.UpdateRevenueBalanceProjection(tran, sd);
        this.UpdateRevenueProjection(tran, sd);
        break;
      default:
        throw new PXException("Invalid Deferral Account Type. Only {0} and {1} are supported but {2} was supplied.", new object[3]
        {
          (object) "E",
          (object) "I",
          (object) deferredAccountType
        });
    }
  }

  private void UpdateRevenueBalanceProjection(DRScheduleTran tran, DRScheduleDetail scheduleDetail)
  {
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(scheduleDetail, tran.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(scheduleDetail, tran.TranPeriodID);
    if (DRProcess.IsReversed(scheduleDetail))
    {
      DRRevenueBalance drRevenueBalance3 = drRevenueBalance1;
      Decimal? ptdProjected = drRevenueBalance3.PTDProjected;
      Decimal? amount1 = tran.Amount;
      drRevenueBalance3.PTDProjected = ptdProjected.HasValue & amount1.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance4 = drRevenueBalance1;
      Decimal? endProjected = drRevenueBalance4.EndProjected;
      Decimal? amount2 = tran.Amount;
      drRevenueBalance4.EndProjected = endProjected.HasValue & amount2.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount2.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance5 = drRevenueBalance2;
      Decimal? tranPtdProjected = drRevenueBalance5.TranPTDProjected;
      Decimal? amount3 = tran.Amount;
      drRevenueBalance5.TranPTDProjected = tranPtdProjected.HasValue & amount3.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance6 = drRevenueBalance2;
      Decimal? tranEndProjected = drRevenueBalance6.TranEndProjected;
      Decimal? amount4 = tran.Amount;
      drRevenueBalance6.TranEndProjected = tranEndProjected.HasValue & amount4.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount4.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRRevenueBalance drRevenueBalance7 = drRevenueBalance1;
      Decimal? ptdProjected = drRevenueBalance7.PTDProjected;
      Decimal? amount5 = tran.Amount;
      drRevenueBalance7.PTDProjected = ptdProjected.HasValue & amount5.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() + amount5.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance8 = drRevenueBalance1;
      Decimal? endProjected = drRevenueBalance8.EndProjected;
      Decimal? amount6 = tran.Amount;
      drRevenueBalance8.EndProjected = endProjected.HasValue & amount6.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance9 = drRevenueBalance2;
      Decimal? tranPtdProjected = drRevenueBalance9.TranPTDProjected;
      Decimal? amount7 = tran.Amount;
      drRevenueBalance9.TranPTDProjected = tranPtdProjected.HasValue & amount7.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance10 = drRevenueBalance2;
      Decimal? tranEndProjected = drRevenueBalance10.TranEndProjected;
      Decimal? amount8 = tran.Amount;
      drRevenueBalance10.TranEndProjected = tranEndProjected.HasValue & amount8.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount8.GetValueOrDefault()) : new Decimal?();
    }
  }

  private void UpdateExpenseBalanceProjection(DRScheduleTran tran, DRScheduleDetail scheduleDetail)
  {
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(scheduleDetail, tran.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(scheduleDetail, tran.TranPeriodID);
    DRExpenseBalance drExpenseBalance3 = ((PXSelectBase<DRExpenseBalance>) this.ExpenseBalance).Insert(drExpenseBalance1);
    if (DRProcess.IsReversed(scheduleDetail))
    {
      DRExpenseBalance drExpenseBalance4 = drExpenseBalance3;
      Decimal? ptdProjected = drExpenseBalance4.PTDProjected;
      Decimal? amount1 = tran.Amount;
      drExpenseBalance4.PTDProjected = ptdProjected.HasValue & amount1.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance5 = drExpenseBalance3;
      Decimal? endProjected = drExpenseBalance5.EndProjected;
      Decimal? amount2 = tran.Amount;
      drExpenseBalance5.EndProjected = endProjected.HasValue & amount2.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount2.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance6 = drExpenseBalance2;
      Decimal? tranPtdProjected = drExpenseBalance6.TranPTDProjected;
      Decimal? amount3 = tran.Amount;
      drExpenseBalance6.TranPTDProjected = tranPtdProjected.HasValue & amount3.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance7 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance7.TranEndProjected;
      Decimal? amount4 = tran.Amount;
      drExpenseBalance7.TranEndProjected = tranEndProjected.HasValue & amount4.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount4.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRExpenseBalance drExpenseBalance8 = drExpenseBalance3;
      Decimal? ptdProjected = drExpenseBalance8.PTDProjected;
      Decimal? amount5 = tran.Amount;
      drExpenseBalance8.PTDProjected = ptdProjected.HasValue & amount5.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() + amount5.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance9 = drExpenseBalance3;
      Decimal? endProjected = drExpenseBalance9.EndProjected;
      Decimal? amount6 = tran.Amount;
      drExpenseBalance9.EndProjected = endProjected.HasValue & amount6.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance10 = drExpenseBalance2;
      Decimal? tranPtdProjected = drExpenseBalance10.TranPTDProjected;
      Decimal? amount7 = tran.Amount;
      drExpenseBalance10.TranPTDProjected = tranPtdProjected.HasValue & amount7.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance11 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance11.TranEndProjected;
      Decimal? amount8 = tran.Amount;
      drExpenseBalance11.TranEndProjected = tranEndProjected.HasValue & amount8.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount8.GetValueOrDefault()) : new Decimal?();
    }
  }

  /// <summary>
  /// Using the information from the provided <see cref="T:PX.Objects.DR.DRScheduleTran" /> and <see cref="T:PX.Objects.DR.DRScheduleDetail" /> objects
  /// to update the deferral balances and projections.
  /// </summary>
  private void UpdateBalance(
    DRScheduleTran deferralTransaction,
    DRScheduleDetail deferralScheduleDetail,
    string deferredAccountType,
    bool isIntegrityCheck = false)
  {
    switch (deferredAccountType)
    {
      case "E":
        this.UpdateExpenseBalance(deferralTransaction, deferralScheduleDetail, isIntegrityCheck);
        this.UpdateExpenseProjectionUponRecognition(deferralTransaction, deferralScheduleDetail, isIntegrityCheck, isIntegrityCheck);
        break;
      case "I":
        this.UpdateRevenueBalance(deferralTransaction, deferralScheduleDetail, isIntegrityCheck);
        this.UpdateRevenueProjectionUponRecognition(deferralTransaction, deferralScheduleDetail, isIntegrityCheck, isIntegrityCheck);
        break;
      default:
        throw new PXException("Invalid Deferral Account Type. Only {0} and {1} are supported but {2} was supplied.", new object[3]
        {
          (object) "E",
          (object) "I",
          (object) deferredAccountType
        });
    }
  }

  /// <param name="updateEndProjected">
  /// A boolean flag indicating whether <see cref="P:PX.Objects.DR.DRRevenueBalance.EndProjected" /> should be updated.
  /// This can be required during the Balance Validation process.
  /// </param>
  private void UpdateRevenueBalance(
    DRScheduleTran tran,
    DRScheduleDetail scheduleDetail,
    bool updateEndProjected = false)
  {
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(scheduleDetail, tran.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(scheduleDetail, tran.TranPeriodID);
    if (DRProcess.IsReversed(scheduleDetail))
    {
      DRRevenueBalance drRevenueBalance3 = drRevenueBalance1;
      Decimal? ptdRecognized = drRevenueBalance3.PTDRecognized;
      Decimal? amount1 = tran.Amount;
      drRevenueBalance3.PTDRecognized = ptdRecognized.HasValue & amount1.HasValue ? new Decimal?(ptdRecognized.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance4 = drRevenueBalance1;
      Decimal? endBalance = drRevenueBalance4.EndBalance;
      Decimal? amount2 = tran.Amount;
      drRevenueBalance4.EndBalance = endBalance.HasValue & amount2.HasValue ? new Decimal?(endBalance.GetValueOrDefault() + amount2.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance5 = drRevenueBalance2;
      Decimal? tranPtdRecognized = drRevenueBalance5.TranPTDRecognized;
      Decimal? amount3 = tran.Amount;
      drRevenueBalance5.TranPTDRecognized = tranPtdRecognized.HasValue & amount3.HasValue ? new Decimal?(tranPtdRecognized.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance6 = drRevenueBalance2;
      Decimal? tranEndBalance = drRevenueBalance6.TranEndBalance;
      Decimal? amount4 = tran.Amount;
      drRevenueBalance6.TranEndBalance = tranEndBalance.HasValue & amount4.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() + amount4.GetValueOrDefault()) : new Decimal?();
      if (tran.FinPeriodID == scheduleDetail.FinPeriodID)
      {
        DRRevenueBalance drRevenueBalance7 = drRevenueBalance1;
        Decimal? recognizedSamePeriod1 = drRevenueBalance7.PTDRecognizedSamePeriod;
        Decimal? amount5 = tran.Amount;
        drRevenueBalance7.PTDRecognizedSamePeriod = recognizedSamePeriod1.HasValue & amount5.HasValue ? new Decimal?(recognizedSamePeriod1.GetValueOrDefault() - amount5.GetValueOrDefault()) : new Decimal?();
        DRRevenueBalance drRevenueBalance8 = drRevenueBalance2;
        Decimal? recognizedSamePeriod2 = drRevenueBalance8.TranPTDRecognizedSamePeriod;
        Decimal? amount6 = tran.Amount;
        drRevenueBalance8.TranPTDRecognizedSamePeriod = recognizedSamePeriod2.HasValue & amount6.HasValue ? new Decimal?(recognizedSamePeriod2.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
      }
      if (!updateEndProjected)
        return;
      DRRevenueBalance drRevenueBalance9 = drRevenueBalance1;
      Decimal? endProjected = drRevenueBalance9.EndProjected;
      Decimal? amount7 = tran.Amount;
      drRevenueBalance9.EndProjected = endProjected.HasValue & amount7.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance10 = drRevenueBalance2;
      Decimal? tranEndProjected = drRevenueBalance10.TranEndProjected;
      Decimal? amount8 = tran.Amount;
      drRevenueBalance10.TranEndProjected = tranEndProjected.HasValue & amount8.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRRevenueBalance drRevenueBalance11 = drRevenueBalance1;
      Decimal? ptdRecognized = drRevenueBalance11.PTDRecognized;
      Decimal? amount9 = tran.Amount;
      drRevenueBalance11.PTDRecognized = ptdRecognized.HasValue & amount9.HasValue ? new Decimal?(ptdRecognized.GetValueOrDefault() + amount9.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance12 = drRevenueBalance1;
      Decimal? endBalance = drRevenueBalance12.EndBalance;
      Decimal? amount10 = tran.Amount;
      drRevenueBalance12.EndBalance = endBalance.HasValue & amount10.HasValue ? new Decimal?(endBalance.GetValueOrDefault() - amount10.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance13 = drRevenueBalance2;
      Decimal? tranPtdRecognized = drRevenueBalance13.TranPTDRecognized;
      Decimal? amount11 = tran.Amount;
      drRevenueBalance13.TranPTDRecognized = tranPtdRecognized.HasValue & amount11.HasValue ? new Decimal?(tranPtdRecognized.GetValueOrDefault() + amount11.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance14 = drRevenueBalance2;
      Decimal? tranEndBalance = drRevenueBalance14.TranEndBalance;
      Decimal? amount12 = tran.Amount;
      drRevenueBalance14.TranEndBalance = tranEndBalance.HasValue & amount12.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() - amount12.GetValueOrDefault()) : new Decimal?();
      if (tran.FinPeriodID == scheduleDetail.FinPeriodID)
      {
        DRRevenueBalance drRevenueBalance15 = drRevenueBalance1;
        Decimal? recognizedSamePeriod3 = drRevenueBalance15.PTDRecognizedSamePeriod;
        Decimal? amount13 = tran.Amount;
        drRevenueBalance15.PTDRecognizedSamePeriod = recognizedSamePeriod3.HasValue & amount13.HasValue ? new Decimal?(recognizedSamePeriod3.GetValueOrDefault() + amount13.GetValueOrDefault()) : new Decimal?();
        DRRevenueBalance drRevenueBalance16 = drRevenueBalance2;
        Decimal? recognizedSamePeriod4 = drRevenueBalance16.TranPTDRecognizedSamePeriod;
        Decimal? amount14 = tran.Amount;
        drRevenueBalance16.TranPTDRecognizedSamePeriod = recognizedSamePeriod4.HasValue & amount14.HasValue ? new Decimal?(recognizedSamePeriod4.GetValueOrDefault() + amount14.GetValueOrDefault()) : new Decimal?();
      }
      if (!updateEndProjected)
        return;
      DRRevenueBalance drRevenueBalance17 = drRevenueBalance1;
      Decimal? endProjected = drRevenueBalance17.EndProjected;
      Decimal? amount15 = tran.Amount;
      drRevenueBalance17.EndProjected = endProjected.HasValue & amount15.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount15.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance18 = drRevenueBalance2;
      Decimal? tranEndProjected = drRevenueBalance18.TranEndProjected;
      Decimal? amount16 = tran.Amount;
      drRevenueBalance18.TranEndProjected = tranEndProjected.HasValue & amount16.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount16.GetValueOrDefault()) : new Decimal?();
    }
  }

  /// <param name="updateEndProjected">
  /// A boolean flag indicating whether <see cref="P:PX.Objects.DR.DRExpenseBalance.EndProjected" /> balances should be updated.
  /// This can be required during the Balance Validation process.
  /// </param>
  private void UpdateExpenseBalance(
    DRScheduleTran tran,
    DRScheduleDetail scheduleDetail,
    bool updateEndProjected = false)
  {
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(scheduleDetail, tran.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(scheduleDetail, tran.TranPeriodID);
    if (DRProcess.IsReversed(scheduleDetail))
    {
      DRExpenseBalance drExpenseBalance3 = drExpenseBalance1;
      Decimal? ptdRecognized = drExpenseBalance3.PTDRecognized;
      Decimal? amount1 = tran.Amount;
      drExpenseBalance3.PTDRecognized = ptdRecognized.HasValue & amount1.HasValue ? new Decimal?(ptdRecognized.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance4 = drExpenseBalance1;
      Decimal? endBalance = drExpenseBalance4.EndBalance;
      Decimal? amount2 = tran.Amount;
      drExpenseBalance4.EndBalance = endBalance.HasValue & amount2.HasValue ? new Decimal?(endBalance.GetValueOrDefault() + amount2.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance5 = drExpenseBalance2;
      Decimal? tranPtdRecognized = drExpenseBalance5.TranPTDRecognized;
      Decimal? amount3 = tran.Amount;
      drExpenseBalance5.TranPTDRecognized = tranPtdRecognized.HasValue & amount3.HasValue ? new Decimal?(tranPtdRecognized.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance6 = drExpenseBalance2;
      Decimal? tranEndBalance = drExpenseBalance6.TranEndBalance;
      Decimal? amount4 = tran.Amount;
      drExpenseBalance6.TranEndBalance = tranEndBalance.HasValue & amount4.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() + amount4.GetValueOrDefault()) : new Decimal?();
      if (tran.FinPeriodID == scheduleDetail.FinPeriodID)
      {
        DRExpenseBalance drExpenseBalance7 = drExpenseBalance1;
        Decimal? recognizedSamePeriod1 = drExpenseBalance7.PTDRecognizedSamePeriod;
        Decimal? amount5 = tran.Amount;
        drExpenseBalance7.PTDRecognizedSamePeriod = recognizedSamePeriod1.HasValue & amount5.HasValue ? new Decimal?(recognizedSamePeriod1.GetValueOrDefault() - amount5.GetValueOrDefault()) : new Decimal?();
        DRExpenseBalance drExpenseBalance8 = drExpenseBalance2;
        Decimal? recognizedSamePeriod2 = drExpenseBalance8.TranPTDRecognizedSamePeriod;
        Decimal? amount6 = tran.Amount;
        drExpenseBalance8.TranPTDRecognizedSamePeriod = recognizedSamePeriod2.HasValue & amount6.HasValue ? new Decimal?(recognizedSamePeriod2.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
      }
      if (!updateEndProjected)
        return;
      DRExpenseBalance drExpenseBalance9 = drExpenseBalance1;
      Decimal? endProjected = drExpenseBalance9.EndProjected;
      Decimal? amount7 = tran.Amount;
      drExpenseBalance9.EndProjected = endProjected.HasValue & amount7.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance10 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance10.TranEndProjected;
      Decimal? amount8 = tran.Amount;
      drExpenseBalance10.TranEndProjected = tranEndProjected.HasValue & amount8.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRExpenseBalance drExpenseBalance11 = drExpenseBalance1;
      Decimal? ptdRecognized = drExpenseBalance11.PTDRecognized;
      Decimal? amount9 = tran.Amount;
      drExpenseBalance11.PTDRecognized = ptdRecognized.HasValue & amount9.HasValue ? new Decimal?(ptdRecognized.GetValueOrDefault() + amount9.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance12 = drExpenseBalance1;
      Decimal? endBalance = drExpenseBalance12.EndBalance;
      Decimal? amount10 = tran.Amount;
      drExpenseBalance12.EndBalance = endBalance.HasValue & amount10.HasValue ? new Decimal?(endBalance.GetValueOrDefault() - amount10.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance13 = drExpenseBalance2;
      Decimal? tranPtdRecognized = drExpenseBalance13.TranPTDRecognized;
      Decimal? amount11 = tran.Amount;
      drExpenseBalance13.TranPTDRecognized = tranPtdRecognized.HasValue & amount11.HasValue ? new Decimal?(tranPtdRecognized.GetValueOrDefault() + amount11.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance14 = drExpenseBalance2;
      Decimal? tranEndBalance = drExpenseBalance14.TranEndBalance;
      Decimal? amount12 = tran.Amount;
      drExpenseBalance14.TranEndBalance = tranEndBalance.HasValue & amount12.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() - amount12.GetValueOrDefault()) : new Decimal?();
      if (tran.FinPeriodID == scheduleDetail.FinPeriodID)
      {
        DRExpenseBalance drExpenseBalance15 = drExpenseBalance1;
        Decimal? recognizedSamePeriod3 = drExpenseBalance15.PTDRecognizedSamePeriod;
        Decimal? amount13 = tran.Amount;
        drExpenseBalance15.PTDRecognizedSamePeriod = recognizedSamePeriod3.HasValue & amount13.HasValue ? new Decimal?(recognizedSamePeriod3.GetValueOrDefault() + amount13.GetValueOrDefault()) : new Decimal?();
        DRExpenseBalance drExpenseBalance16 = drExpenseBalance2;
        Decimal? recognizedSamePeriod4 = drExpenseBalance16.TranPTDRecognizedSamePeriod;
        Decimal? amount14 = tran.Amount;
        drExpenseBalance16.TranPTDRecognizedSamePeriod = recognizedSamePeriod4.HasValue & amount14.HasValue ? new Decimal?(recognizedSamePeriod4.GetValueOrDefault() + amount14.GetValueOrDefault()) : new Decimal?();
      }
      if (!updateEndProjected)
        return;
      DRExpenseBalance drExpenseBalance17 = drExpenseBalance1;
      Decimal? endProjected = drExpenseBalance17.EndProjected;
      Decimal? amount15 = tran.Amount;
      drExpenseBalance17.EndProjected = endProjected.HasValue & amount15.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount15.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance18 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance18.TranEndProjected;
      Decimal? amount16 = tran.Amount;
      drExpenseBalance18.TranEndProjected = tranEndProjected.HasValue & amount16.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount16.GetValueOrDefault()) : new Decimal?();
    }
  }

  private void UpdateRevenueProjection(DRScheduleTran tran, DRScheduleDetail scheduleDetail)
  {
    DRRevenueProjectionAccum revenueProjectionAccum1 = this.CreateDRRevenueProjectionAccum(scheduleDetail, tran.FinPeriodID);
    DRRevenueProjectionAccum revenueProjectionAccum2 = this.CreateDRRevenueProjectionAccum(scheduleDetail, tran.TranPeriodID);
    if (DRProcess.IsReversed(scheduleDetail))
    {
      if (tran.FinPeriodID == scheduleDetail.FinPeriodID)
      {
        DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum1;
        Decimal? recognizedSamePeriod1 = revenueProjectionAccum3.PTDRecognizedSamePeriod;
        Decimal? amount1 = tran.Amount;
        revenueProjectionAccum3.PTDRecognizedSamePeriod = recognizedSamePeriod1.HasValue & amount1.HasValue ? new Decimal?(recognizedSamePeriod1.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
        DRRevenueProjectionAccum revenueProjectionAccum4 = revenueProjectionAccum2;
        Decimal? recognizedSamePeriod2 = revenueProjectionAccum4.TranPTDRecognizedSamePeriod;
        Decimal? amount2 = tran.Amount;
        revenueProjectionAccum4.TranPTDRecognizedSamePeriod = recognizedSamePeriod2.HasValue & amount2.HasValue ? new Decimal?(recognizedSamePeriod2.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
      }
      DRRevenueProjectionAccum revenueProjectionAccum5 = revenueProjectionAccum1;
      Decimal? ptdProjected = revenueProjectionAccum5.PTDProjected;
      Decimal? amount3 = tran.Amount;
      revenueProjectionAccum5.PTDProjected = ptdProjected.HasValue & amount3.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRRevenueProjectionAccum revenueProjectionAccum6 = revenueProjectionAccum2;
      Decimal? tranPtdProjected = revenueProjectionAccum6.TranPTDProjected;
      Decimal? amount4 = tran.Amount;
      revenueProjectionAccum6.TranPTDProjected = tranPtdProjected.HasValue & amount4.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() - amount4.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      if (tran.FinPeriodID == scheduleDetail.FinPeriodID)
      {
        DRRevenueProjectionAccum revenueProjectionAccum7 = revenueProjectionAccum1;
        Decimal? recognizedSamePeriod3 = revenueProjectionAccum7.PTDRecognizedSamePeriod;
        Decimal? amount5 = tran.Amount;
        revenueProjectionAccum7.PTDRecognizedSamePeriod = recognizedSamePeriod3.HasValue & amount5.HasValue ? new Decimal?(recognizedSamePeriod3.GetValueOrDefault() + amount5.GetValueOrDefault()) : new Decimal?();
        DRRevenueProjectionAccum revenueProjectionAccum8 = revenueProjectionAccum2;
        Decimal? recognizedSamePeriod4 = revenueProjectionAccum8.TranPTDRecognizedSamePeriod;
        Decimal? amount6 = tran.Amount;
        revenueProjectionAccum8.TranPTDRecognizedSamePeriod = recognizedSamePeriod4.HasValue & amount6.HasValue ? new Decimal?(recognizedSamePeriod4.GetValueOrDefault() + amount6.GetValueOrDefault()) : new Decimal?();
      }
      DRRevenueProjectionAccum revenueProjectionAccum9 = revenueProjectionAccum1;
      Decimal? ptdProjected = revenueProjectionAccum9.PTDProjected;
      Decimal? amount7 = tran.Amount;
      revenueProjectionAccum9.PTDProjected = ptdProjected.HasValue & amount7.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRRevenueProjectionAccum revenueProjectionAccum10 = revenueProjectionAccum2;
      Decimal? tranPtdProjected = revenueProjectionAccum10.TranPTDProjected;
      Decimal? amount8 = tran.Amount;
      revenueProjectionAccum10.TranPTDProjected = tranPtdProjected.HasValue & amount8.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
    }
  }

  private void UpdateExpenseProjection(DRScheduleTran tran, DRScheduleDetail scheduleDetail)
  {
    DRExpenseProjectionAccum expenseProjectionAccum1 = this.CreateDRExpenseProjectionAccum(scheduleDetail, tran.FinPeriodID);
    DRExpenseProjectionAccum expenseProjectionAccum2 = this.CreateDRExpenseProjectionAccum(scheduleDetail, tran.TranPeriodID);
    if (DRProcess.IsReversed(scheduleDetail))
    {
      if (tran.FinPeriodID == scheduleDetail.FinPeriodID)
      {
        DRExpenseProjectionAccum expenseProjectionAccum3 = expenseProjectionAccum1;
        Decimal? recognizedSamePeriod1 = expenseProjectionAccum3.PTDRecognizedSamePeriod;
        Decimal? amount1 = tran.Amount;
        expenseProjectionAccum3.PTDRecognizedSamePeriod = recognizedSamePeriod1.HasValue & amount1.HasValue ? new Decimal?(recognizedSamePeriod1.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
        DRExpenseProjectionAccum expenseProjectionAccum4 = expenseProjectionAccum2;
        Decimal? recognizedSamePeriod2 = expenseProjectionAccum4.TranPTDRecognizedSamePeriod;
        Decimal? amount2 = tran.Amount;
        expenseProjectionAccum4.TranPTDRecognizedSamePeriod = recognizedSamePeriod2.HasValue & amount2.HasValue ? new Decimal?(recognizedSamePeriod2.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
      }
      DRExpenseProjectionAccum expenseProjectionAccum5 = expenseProjectionAccum1;
      Decimal? ptdProjected = expenseProjectionAccum5.PTDProjected;
      Decimal? amount3 = tran.Amount;
      expenseProjectionAccum5.PTDProjected = ptdProjected.HasValue & amount3.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRExpenseProjectionAccum expenseProjectionAccum6 = expenseProjectionAccum2;
      Decimal? tranPtdProjected = expenseProjectionAccum6.TranPTDProjected;
      Decimal? amount4 = tran.Amount;
      expenseProjectionAccum6.TranPTDProjected = tranPtdProjected.HasValue & amount4.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() - amount4.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      if (tran.FinPeriodID == scheduleDetail.FinPeriodID)
      {
        DRExpenseProjectionAccum expenseProjectionAccum7 = expenseProjectionAccum1;
        Decimal? recognizedSamePeriod3 = expenseProjectionAccum7.PTDRecognizedSamePeriod;
        Decimal? amount5 = tran.Amount;
        expenseProjectionAccum7.PTDRecognizedSamePeriod = recognizedSamePeriod3.HasValue & amount5.HasValue ? new Decimal?(recognizedSamePeriod3.GetValueOrDefault() + amount5.GetValueOrDefault()) : new Decimal?();
        DRExpenseProjectionAccum expenseProjectionAccum8 = expenseProjectionAccum2;
        Decimal? recognizedSamePeriod4 = expenseProjectionAccum8.TranPTDRecognizedSamePeriod;
        Decimal? amount6 = tran.Amount;
        expenseProjectionAccum8.TranPTDRecognizedSamePeriod = recognizedSamePeriod4.HasValue & amount6.HasValue ? new Decimal?(recognizedSamePeriod4.GetValueOrDefault() + amount6.GetValueOrDefault()) : new Decimal?();
      }
      DRExpenseProjectionAccum expenseProjectionAccum9 = expenseProjectionAccum1;
      Decimal? ptdProjected = expenseProjectionAccum9.PTDProjected;
      Decimal? amount7 = tran.Amount;
      expenseProjectionAccum9.PTDProjected = ptdProjected.HasValue & amount7.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRExpenseProjectionAccum expenseProjectionAccum10 = expenseProjectionAccum2;
      Decimal? tranPtdProjected = expenseProjectionAccum10.TranPTDProjected;
      Decimal? amount8 = tran.Amount;
      expenseProjectionAccum10.TranPTDProjected = tranPtdProjected.HasValue & amount8.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
    }
  }

  /// <summary>
  /// Updates revenue projection history table to reflect the recognition of a deferral transaction.
  /// </summary>
  /// <param name="updatePTDRecognizedSamePeriod">
  /// A boolean flag indicating whether <see cref="P:PX.Objects.DR.DRRevenueProjection.PTDRecognizedSamePeriod" /> balances
  /// should be updated. This can be required during the Balance Validation process.
  /// </param>
  /// <param name="updatePTDProjected">
  /// A boolean flag indicating whether <see cref="P:PX.Objects.DR.DRRevenueProjection.PTDProjected" /> balances
  /// should be updated. This can be required during the Balance Validation process, but undesirable
  /// during normal recognition.
  /// </param>
  private void UpdateRevenueProjectionUponRecognition(
    DRScheduleTran transaction,
    DRScheduleDetail scheduleDetail,
    bool updatePTDRecognizedSamePeriod = false,
    bool updatePTDProjected = false)
  {
    DRRevenueProjectionAccum revenueProjectionAccum1 = this.CreateDRRevenueProjectionAccum(scheduleDetail, transaction.FinPeriodID);
    DRRevenueProjectionAccum revenueProjectionAccum2 = this.CreateDRRevenueProjectionAccum(scheduleDetail, transaction.TranPeriodID);
    Decimal? nullable1;
    Decimal? nullable2;
    if (!DRProcess.IsReversed(scheduleDetail))
    {
      nullable2 = transaction.Amount;
    }
    else
    {
      nullable1 = transaction.Amount;
      nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    }
    Decimal? nullable3 = nullable2;
    DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum1;
    nullable1 = revenueProjectionAccum3.PTDRecognized;
    Decimal? nullable4 = nullable3;
    revenueProjectionAccum3.PTDRecognized = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    DRRevenueProjectionAccum revenueProjectionAccum4 = revenueProjectionAccum2;
    Decimal? tranPtdRecognized = revenueProjectionAccum4.TranPTDRecognized;
    nullable1 = nullable3;
    revenueProjectionAccum4.TranPTDRecognized = tranPtdRecognized.HasValue & nullable1.HasValue ? new Decimal?(tranPtdRecognized.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    if (updatePTDRecognizedSamePeriod && transaction.FinPeriodID == scheduleDetail.FinPeriodID)
    {
      DRRevenueProjectionAccum revenueProjectionAccum5 = revenueProjectionAccum1;
      nullable1 = revenueProjectionAccum5.PTDRecognizedSamePeriod;
      Decimal? nullable5 = nullable3;
      revenueProjectionAccum5.PTDRecognizedSamePeriod = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      DRRevenueProjectionAccum revenueProjectionAccum6 = revenueProjectionAccum2;
      Decimal? recognizedSamePeriod = revenueProjectionAccum6.TranPTDRecognizedSamePeriod;
      nullable1 = nullable3;
      revenueProjectionAccum6.TranPTDRecognizedSamePeriod = recognizedSamePeriod.HasValue & nullable1.HasValue ? new Decimal?(recognizedSamePeriod.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    if (!updatePTDProjected)
      return;
    DRRevenueProjectionAccum revenueProjectionAccum7 = revenueProjectionAccum1;
    nullable1 = revenueProjectionAccum7.PTDProjected;
    Decimal? nullable6 = nullable3;
    revenueProjectionAccum7.PTDProjected = nullable1.HasValue & nullable6.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
    DRRevenueProjectionAccum revenueProjectionAccum8 = revenueProjectionAccum2;
    Decimal? tranPtdProjected = revenueProjectionAccum8.TranPTDProjected;
    nullable1 = nullable3;
    revenueProjectionAccum8.TranPTDProjected = tranPtdProjected.HasValue & nullable1.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  /// <summary>
  /// Updates expense projection history table to reflect the recognition of a deferral transaction.
  /// </summary>
  /// <param name="updatePTDRecognizedSamePeriod">
  /// A boolean flag indicating whether <see cref="P:PX.Objects.DR.DRRevenueProjection.PTDRecognizedSamePeriod" /> balances
  /// should be updated. This can be required during the Balance Validation process.
  /// </param>
  /// <param name="updatePTDProjected">
  /// A boolean flag indicating whether <see cref="P:PX.Objects.DR.DRRevenueProjection.PTDProjected" /> balances
  /// should be updated. This can be required during the Balance Validation process, but undesirable
  /// during normal recognition.
  /// </param>
  private void UpdateExpenseProjectionUponRecognition(
    DRScheduleTran transaction,
    DRScheduleDetail scheduleDetail,
    bool updatePTDRecognizedSamePeriod = false,
    bool updatePTDProjected = false)
  {
    DRExpenseProjectionAccum expenseProjectionAccum1 = this.CreateDRExpenseProjectionAccum(scheduleDetail, transaction.FinPeriodID);
    DRExpenseProjectionAccum expenseProjectionAccum2 = this.CreateDRExpenseProjectionAccum(scheduleDetail, transaction.TranPeriodID);
    Decimal? nullable1;
    Decimal? nullable2;
    if (!DRProcess.IsReversed(scheduleDetail))
    {
      nullable2 = transaction.Amount;
    }
    else
    {
      nullable1 = transaction.Amount;
      nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    }
    Decimal? nullable3 = nullable2;
    DRExpenseProjectionAccum expenseProjectionAccum3 = expenseProjectionAccum1;
    nullable1 = expenseProjectionAccum3.PTDRecognized;
    Decimal? nullable4 = nullable3;
    expenseProjectionAccum3.PTDRecognized = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    DRExpenseProjectionAccum expenseProjectionAccum4 = expenseProjectionAccum2;
    Decimal? tranPtdRecognized = expenseProjectionAccum4.TranPTDRecognized;
    nullable1 = nullable3;
    expenseProjectionAccum4.TranPTDRecognized = tranPtdRecognized.HasValue & nullable1.HasValue ? new Decimal?(tranPtdRecognized.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    if (updatePTDRecognizedSamePeriod && transaction.FinPeriodID == scheduleDetail.FinPeriodID)
    {
      DRExpenseProjectionAccum expenseProjectionAccum5 = expenseProjectionAccum1;
      nullable1 = expenseProjectionAccum5.PTDRecognizedSamePeriod;
      Decimal? nullable5 = nullable3;
      expenseProjectionAccum5.PTDRecognizedSamePeriod = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      DRExpenseProjectionAccum expenseProjectionAccum6 = expenseProjectionAccum2;
      Decimal? recognizedSamePeriod = expenseProjectionAccum6.TranPTDRecognizedSamePeriod;
      nullable1 = nullable3;
      expenseProjectionAccum6.TranPTDRecognizedSamePeriod = recognizedSamePeriod.HasValue & nullable1.HasValue ? new Decimal?(recognizedSamePeriod.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    if (!updatePTDProjected)
      return;
    DRExpenseProjectionAccum expenseProjectionAccum7 = expenseProjectionAccum1;
    nullable1 = expenseProjectionAccum7.PTDProjected;
    Decimal? nullable6 = nullable3;
    expenseProjectionAccum7.PTDProjected = nullable1.HasValue & nullable6.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
    DRExpenseProjectionAccum expenseProjectionAccum8 = expenseProjectionAccum2;
    Decimal? tranPtdProjected = expenseProjectionAccum8.TranPTDProjected;
    nullable1 = nullable3;
    expenseProjectionAccum8.TranPTDProjected = tranPtdProjected.HasValue & nullable1.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void InitBalance(
    DRScheduleTran transaction,
    DRScheduleDetail scheduleDetail,
    string deferredAccountType)
  {
    switch (deferredAccountType)
    {
      case "E":
        this.InitExpenseBalance(transaction, scheduleDetail);
        break;
      case "I":
        this.InitRevenueBalance(transaction, scheduleDetail);
        break;
      default:
        throw new PXException("Invalid Deferral Account Type. Only {0} and {1} are supported but {2} was supplied.", new object[3]
        {
          (object) "E",
          (object) "I",
          (object) deferredAccountType
        });
    }
  }

  private void InitRevenueBalance(DRScheduleTran transaction, DRScheduleDetail scheduleDetail)
  {
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(scheduleDetail, transaction.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(scheduleDetail, transaction.TranPeriodID);
    if (DRProcess.IsReversed(scheduleDetail))
    {
      DRRevenueBalance drRevenueBalance3 = drRevenueBalance1;
      Decimal? ptdDeferred = drRevenueBalance3.PTDDeferred;
      Decimal? amount1 = transaction.Amount;
      drRevenueBalance3.PTDDeferred = ptdDeferred.HasValue & amount1.HasValue ? new Decimal?(ptdDeferred.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance4 = drRevenueBalance1;
      Decimal? endBalance = drRevenueBalance4.EndBalance;
      Decimal? amount2 = transaction.Amount;
      drRevenueBalance4.EndBalance = endBalance.HasValue & amount2.HasValue ? new Decimal?(endBalance.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance5 = drRevenueBalance1;
      Decimal? endProjected = drRevenueBalance5.EndProjected;
      Decimal? amount3 = transaction.Amount;
      drRevenueBalance5.EndProjected = endProjected.HasValue & amount3.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance6 = drRevenueBalance2;
      Decimal? tranPtdDeferred = drRevenueBalance6.TranPTDDeferred;
      Decimal? amount4 = transaction.Amount;
      drRevenueBalance6.TranPTDDeferred = tranPtdDeferred.HasValue & amount4.HasValue ? new Decimal?(tranPtdDeferred.GetValueOrDefault() - amount4.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance7 = drRevenueBalance2;
      Decimal? tranEndBalance = drRevenueBalance7.TranEndBalance;
      Decimal? amount5 = transaction.Amount;
      drRevenueBalance7.TranEndBalance = tranEndBalance.HasValue & amount5.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() - amount5.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance8 = drRevenueBalance2;
      Decimal? tranEndProjected = drRevenueBalance8.TranEndProjected;
      Decimal? amount6 = transaction.Amount;
      drRevenueBalance8.TranEndProjected = tranEndProjected.HasValue & amount6.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRRevenueBalance drRevenueBalance9 = drRevenueBalance1;
      Decimal? ptdDeferred = drRevenueBalance9.PTDDeferred;
      Decimal? amount7 = transaction.Amount;
      drRevenueBalance9.PTDDeferred = ptdDeferred.HasValue & amount7.HasValue ? new Decimal?(ptdDeferred.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance10 = drRevenueBalance1;
      Decimal? endBalance = drRevenueBalance10.EndBalance;
      Decimal? amount8 = transaction.Amount;
      drRevenueBalance10.EndBalance = endBalance.HasValue & amount8.HasValue ? new Decimal?(endBalance.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance11 = drRevenueBalance1;
      Decimal? endProjected = drRevenueBalance11.EndProjected;
      Decimal? amount9 = transaction.Amount;
      drRevenueBalance11.EndProjected = endProjected.HasValue & amount9.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount9.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance12 = drRevenueBalance2;
      Decimal? tranPtdDeferred = drRevenueBalance12.TranPTDDeferred;
      Decimal? amount10 = transaction.Amount;
      drRevenueBalance12.TranPTDDeferred = tranPtdDeferred.HasValue & amount10.HasValue ? new Decimal?(tranPtdDeferred.GetValueOrDefault() + amount10.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance13 = drRevenueBalance2;
      Decimal? tranEndBalance = drRevenueBalance13.TranEndBalance;
      Decimal? amount11 = transaction.Amount;
      drRevenueBalance13.TranEndBalance = tranEndBalance.HasValue & amount11.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() + amount11.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance14 = drRevenueBalance2;
      Decimal? tranEndProjected = drRevenueBalance14.TranEndProjected;
      Decimal? amount12 = transaction.Amount;
      drRevenueBalance14.TranEndProjected = tranEndProjected.HasValue & amount12.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount12.GetValueOrDefault()) : new Decimal?();
    }
  }

  private void InitExpenseBalance(DRScheduleTran transaction, DRScheduleDetail scheduleDetail)
  {
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(scheduleDetail, transaction.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(scheduleDetail, transaction.TranPeriodID);
    if (DRProcess.IsReversed(scheduleDetail))
    {
      DRExpenseBalance drExpenseBalance3 = drExpenseBalance1;
      Decimal? ptdDeferred = drExpenseBalance3.PTDDeferred;
      Decimal? amount1 = transaction.Amount;
      drExpenseBalance3.PTDDeferred = ptdDeferred.HasValue & amount1.HasValue ? new Decimal?(ptdDeferred.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance4 = drExpenseBalance1;
      Decimal? endBalance = drExpenseBalance4.EndBalance;
      Decimal? amount2 = transaction.Amount;
      drExpenseBalance4.EndBalance = endBalance.HasValue & amount2.HasValue ? new Decimal?(endBalance.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance5 = drExpenseBalance1;
      Decimal? endProjected = drExpenseBalance5.EndProjected;
      Decimal? amount3 = transaction.Amount;
      drExpenseBalance5.EndProjected = endProjected.HasValue & amount3.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance6 = drExpenseBalance2;
      Decimal? tranPtdDeferred = drExpenseBalance6.TranPTDDeferred;
      Decimal? amount4 = transaction.Amount;
      drExpenseBalance6.TranPTDDeferred = tranPtdDeferred.HasValue & amount4.HasValue ? new Decimal?(tranPtdDeferred.GetValueOrDefault() - amount4.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance7 = drExpenseBalance2;
      Decimal? tranEndBalance = drExpenseBalance7.TranEndBalance;
      Decimal? amount5 = transaction.Amount;
      drExpenseBalance7.TranEndBalance = tranEndBalance.HasValue & amount5.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() - amount5.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance8 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance8.TranEndProjected;
      Decimal? amount6 = transaction.Amount;
      drExpenseBalance8.TranEndProjected = tranEndProjected.HasValue & amount6.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRExpenseBalance drExpenseBalance9 = drExpenseBalance1;
      Decimal? ptdDeferred = drExpenseBalance9.PTDDeferred;
      Decimal? amount7 = transaction.Amount;
      drExpenseBalance9.PTDDeferred = ptdDeferred.HasValue & amount7.HasValue ? new Decimal?(ptdDeferred.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance10 = drExpenseBalance1;
      Decimal? endBalance = drExpenseBalance10.EndBalance;
      Decimal? amount8 = transaction.Amount;
      drExpenseBalance10.EndBalance = endBalance.HasValue & amount8.HasValue ? new Decimal?(endBalance.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance11 = drExpenseBalance1;
      Decimal? endProjected = drExpenseBalance11.EndProjected;
      Decimal? amount9 = transaction.Amount;
      drExpenseBalance11.EndProjected = endProjected.HasValue & amount9.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount9.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance12 = drExpenseBalance2;
      Decimal? tranPtdDeferred = drExpenseBalance12.TranPTDDeferred;
      Decimal? amount10 = transaction.Amount;
      drExpenseBalance12.TranPTDDeferred = tranPtdDeferred.HasValue & amount10.HasValue ? new Decimal?(tranPtdDeferred.GetValueOrDefault() + amount10.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance13 = drExpenseBalance2;
      Decimal? tranEndBalance = drExpenseBalance13.TranEndBalance;
      Decimal? amount11 = transaction.Amount;
      drExpenseBalance13.TranEndBalance = tranEndBalance.HasValue & amount11.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() + amount11.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance14 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance14.TranEndProjected;
      Decimal? amount12 = transaction.Amount;
      drExpenseBalance14.TranEndProjected = tranEndProjected.HasValue & amount12.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount12.GetValueOrDefault()) : new Decimal?();
    }
  }

  public void Subtract(
    DRScheduleDetail scheduleDetail,
    DRScheduleTran transaction,
    string deferralCodeType)
  {
    this.Add(scheduleDetail, transaction, deferralCodeType, -1M);
  }

  public void Add(
    DRScheduleDetail scheduleDetail,
    DRScheduleTran transaction,
    string deferralCodeType,
    Decimal amountMultiplier = 1M)
  {
    switch (deferralCodeType)
    {
      case "E":
        this.AddExpenseToProjection(transaction, scheduleDetail, amountMultiplier);
        this.AddExpenseToBalance(transaction, scheduleDetail, amountMultiplier);
        break;
      case "I":
        this.AddRevenueToProjection(transaction, scheduleDetail, amountMultiplier);
        this.AddRevenueToBalance(transaction, scheduleDetail, amountMultiplier);
        break;
      default:
        throw new PXArgumentException("Invalid Deferral Account Type. Only {0} and {1} are supported but {2} was supplied.", "E", new object[2]
        {
          (object) "I",
          (object) deferralCodeType
        });
    }
  }

  private void AddRevenueToProjection(
    DRScheduleTran transaction,
    DRScheduleDetail scheduleDetail,
    Decimal amountMultiplier)
  {
    DRRevenueProjectionAccum revenueProjectionAccum1 = this.CreateDRRevenueProjectionAccum(scheduleDetail, transaction.FinPeriodID);
    DRRevenueProjectionAccum revenueProjectionAccum2 = this.CreateDRRevenueProjectionAccum(scheduleDetail, transaction.TranPeriodID);
    Decimal? nullable1 = revenueProjectionAccum1.PTDProjected;
    Decimal num1 = amountMultiplier;
    Decimal? amount = transaction.Amount;
    Decimal? nullable2 = amount.HasValue ? new Decimal?(num1 * amount.GetValueOrDefault()) : new Decimal?();
    revenueProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum2;
    Decimal? tranPtdProjected = revenueProjectionAccum3.TranPTDProjected;
    Decimal num2 = amountMultiplier;
    Decimal? nullable3 = transaction.Amount;
    nullable1 = nullable3.HasValue ? new Decimal?(num2 * nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4;
    if (!(tranPtdProjected.HasValue & nullable1.HasValue))
    {
      nullable3 = new Decimal?();
      nullable4 = nullable3;
    }
    else
      nullable4 = new Decimal?(tranPtdProjected.GetValueOrDefault() + nullable1.GetValueOrDefault());
    revenueProjectionAccum3.TranPTDProjected = nullable4;
  }

  private void AddExpenseToProjection(
    DRScheduleTran transaction,
    DRScheduleDetail scheduleDetail,
    Decimal amountMultiplier)
  {
    DRExpenseProjectionAccum expenseProjectionAccum1 = this.CreateDRExpenseProjectionAccum(scheduleDetail, transaction.FinPeriodID);
    DRExpenseProjectionAccum expenseProjectionAccum2 = this.CreateDRExpenseProjectionAccum(scheduleDetail, transaction.TranPeriodID);
    Decimal? nullable1 = expenseProjectionAccum1.PTDProjected;
    Decimal num1 = amountMultiplier;
    Decimal? amount = transaction.Amount;
    Decimal? nullable2 = amount.HasValue ? new Decimal?(num1 * amount.GetValueOrDefault()) : new Decimal?();
    expenseProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseProjectionAccum expenseProjectionAccum3 = expenseProjectionAccum2;
    Decimal? tranPtdProjected = expenseProjectionAccum3.TranPTDProjected;
    Decimal num2 = amountMultiplier;
    Decimal? nullable3 = transaction.Amount;
    nullable1 = nullable3.HasValue ? new Decimal?(num2 * nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4;
    if (!(tranPtdProjected.HasValue & nullable1.HasValue))
    {
      nullable3 = new Decimal?();
      nullable4 = nullable3;
    }
    else
      nullable4 = new Decimal?(tranPtdProjected.GetValueOrDefault() + nullable1.GetValueOrDefault());
    expenseProjectionAccum3.TranPTDProjected = nullable4;
  }

  private void AddRevenueToBalance(
    DRScheduleTran transaction,
    DRScheduleDetail scheduleDetail,
    Decimal amountMultiplier)
  {
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(scheduleDetail, transaction.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(scheduleDetail, transaction.TranPeriodID);
    Decimal? nullable1 = drRevenueBalance1.PTDProjected;
    Decimal num1 = amountMultiplier;
    Decimal? amount1 = transaction.Amount;
    Decimal? nullable2 = amount1.HasValue ? new Decimal?(num1 * amount1.GetValueOrDefault()) : new Decimal?();
    drRevenueBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = drRevenueBalance1.EndProjected;
    Decimal num2 = amountMultiplier;
    Decimal? amount2 = transaction.Amount;
    nullable1 = amount2.HasValue ? new Decimal?(num2 * amount2.GetValueOrDefault()) : new Decimal?();
    drRevenueBalance1.EndProjected = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance3 = drRevenueBalance2;
    nullable1 = drRevenueBalance3.TranPTDProjected;
    Decimal num3 = amountMultiplier;
    Decimal? nullable4 = transaction.Amount;
    nullable3 = nullable4.HasValue ? new Decimal?(num3 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable4 = new Decimal?();
      nullable5 = nullable4;
    }
    else
      nullable5 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    drRevenueBalance3.TranPTDProjected = nullable5;
    DRRevenueBalance drRevenueBalance4 = drRevenueBalance2;
    nullable3 = drRevenueBalance4.TranEndProjected;
    Decimal num4 = amountMultiplier;
    nullable4 = transaction.Amount;
    nullable1 = nullable4.HasValue ? new Decimal?(num4 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable6;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable6 = nullable4;
    }
    else
      nullable6 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
    drRevenueBalance4.TranEndProjected = nullable6;
  }

  private void AddExpenseToBalance(
    DRScheduleTran transaction,
    DRScheduleDetail scheduleDetail,
    Decimal amountMultiplier)
  {
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(scheduleDetail, transaction.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(scheduleDetail, transaction.TranPeriodID);
    Decimal? nullable1 = drExpenseBalance1.PTDProjected;
    Decimal num1 = amountMultiplier;
    Decimal? amount1 = transaction.Amount;
    Decimal? nullable2 = amount1.HasValue ? new Decimal?(num1 * amount1.GetValueOrDefault()) : new Decimal?();
    drExpenseBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = drExpenseBalance1.EndProjected;
    Decimal num2 = amountMultiplier;
    Decimal? amount2 = transaction.Amount;
    nullable1 = amount2.HasValue ? new Decimal?(num2 * amount2.GetValueOrDefault()) : new Decimal?();
    drExpenseBalance1.EndProjected = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance3 = drExpenseBalance2;
    nullable1 = drExpenseBalance3.TranPTDProjected;
    Decimal num3 = amountMultiplier;
    Decimal? nullable4 = transaction.Amount;
    nullable3 = nullable4.HasValue ? new Decimal?(num3 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable4 = new Decimal?();
      nullable5 = nullable4;
    }
    else
      nullable5 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    drExpenseBalance3.TranPTDProjected = nullable5;
    DRExpenseBalance drExpenseBalance4 = drExpenseBalance2;
    nullable3 = drExpenseBalance4.TranEndProjected;
    Decimal num4 = amountMultiplier;
    nullable4 = transaction.Amount;
    nullable1 = nullable4.HasValue ? new Decimal?(num4 * nullable4.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable6;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable6 = nullable4;
    }
    else
      nullable6 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
    drExpenseBalance4.TranEndProjected = nullable6;
  }

  protected DRScheduleDetail InsertScheduleDetail(
    int? branchID,
    DRSchedule sc,
    INComponent component,
    PX.Objects.IN.InventoryItem compItem,
    DRDeferredCode defCode,
    Decimal amount,
    int? defAcctID,
    int? defSubID,
    bool isDraft)
  {
    return this.InsertScheduleDetail(branchID, sc, component, compItem, defCode, amount, defAcctID, defSubID, isDraft, new DateTime?(), new DateTime?());
  }

  protected DRScheduleDetail InsertScheduleDetail(
    int? branchID,
    DRSchedule sc,
    INComponent component,
    PX.Objects.IN.InventoryItem compItem,
    DRDeferredCode defCode,
    Decimal amount,
    int? defAcctID,
    int? defSubID,
    bool isDraft,
    DateTime? termStartDate,
    DateTime? termEndDate)
  {
    int? acctID = sc.Module == "AP" ? compItem.COGSAcctID : component.SalesAcctID;
    int? subID = sc.Module == "AP" ? compItem.COGSSubID : component.SalesSubID;
    return this.InsertScheduleDetail(branchID, sc, compItem.InventoryID, defCode, amount, defAcctID, defSubID, acctID, subID, isDraft, termStartDate, termEndDate);
  }

  protected DRScheduleDetail InsertScheduleDetail(
    int? branchID,
    DRSchedule sc,
    int? componentID,
    DRDeferredCode defCode,
    Decimal amount,
    int? defAcctID,
    int? defSubID,
    int? acctID,
    int? subID,
    bool isDraft)
  {
    return this.InsertScheduleDetail(branchID, sc, componentID, defCode, amount, defAcctID, defSubID, acctID, subID, isDraft, new DateTime?(), new DateTime?());
  }

  protected DRScheduleDetail InsertScheduleDetail(
    int? branchID,
    DRSchedule sc,
    int? componentID,
    DRDeferredCode defCode,
    Decimal amount,
    int? defAcctID,
    int? defSubID,
    int? acctID,
    int? subID,
    bool isDraft,
    DateTime? termStartDate,
    DateTime? termEndDate)
  {
    DRScheduleDetail row = new DRScheduleDetail();
    row.ScheduleID = sc.ScheduleID;
    row.BranchID = branchID;
    row.ComponentID = componentID;
    row.CuryTotalAmt = new Decimal?(amount);
    row.CuryDefAmt = new Decimal?(amount);
    row.DefCode = defCode.DeferredCodeID;
    row.Status = "O";
    row.IsOpen = new bool?(true);
    row.Module = sc.Module;
    row.DocType = sc.DocType;
    row.RefNbr = sc.RefNbr;
    row.LineNbr = sc.LineNbr;
    FinPeriodIDAttribute.SetPeriodsByMaster<DRScheduleDetail.finPeriodID>(((PXSelectBase) this.ScheduleDetail).Cache, (object) row, sc.FinPeriodID);
    row.BAccountID = sc.BAccountID;
    row.AccountID = acctID;
    row.SubID = subID;
    row.DefAcctID = defAcctID;
    row.DefSubID = defSubID;
    row.CreditLineNbr = new int?(0);
    row.DocDate = sc.DocDate;
    row.BAccountType = sc.Module == "AP" ? "VE" : "CU";
    DRScheduleDetail scheduleDetail = ((PXSelectBase<DRScheduleDetail>) this.ScheduleDetail).Insert(row);
    scheduleDetail.Status = isDraft ? "D" : "O";
    scheduleDetail.IsCustom = new bool?(false);
    scheduleDetail.TermStartDate = termStartDate;
    scheduleDetail.TermEndDate = termEndDate;
    if (!isDraft)
      this.CreateCreditLine(scheduleDetail, defCode, branchID);
    return scheduleDetail;
  }

  private void InsertResidualScheduleDetail(
    DRSchedule schedule,
    DRScheduleDetail reidualDetail,
    Decimal amount,
    bool isDraft)
  {
    DRScheduleDetail row = new DRScheduleDetail()
    {
      ScheduleID = schedule.ScheduleID,
      BranchID = reidualDetail.BranchID,
      ComponentID = reidualDetail.ComponentID,
      CuryTotalAmt = new Decimal?(amount),
      CuryDefAmt = new Decimal?(0.0M),
      DefCode = (string) null,
      IsOpen = new bool?(false)
    };
    row.CloseFinPeriodID = row.FinPeriodID;
    row.Module = schedule.Module;
    row.DocType = schedule.DocType;
    row.RefNbr = schedule.RefNbr;
    row.LineNbr = schedule.LineNbr;
    FinPeriodIDAttribute.SetPeriodsByMaster<DRScheduleDetail.finPeriodID>(((PXSelectBase) this.ScheduleDetail).Cache, (object) row, schedule.FinPeriodID);
    row.BAccountID = schedule.BAccountID;
    row.AccountID = reidualDetail.AccountID;
    row.SubID = reidualDetail.SubID;
    row.DefAcctID = reidualDetail.AccountID;
    row.DefSubID = reidualDetail.SubID;
    row.CreditLineNbr = new int?(0);
    row.DocDate = schedule.DocDate;
    row.BAccountType = schedule.Module == "AP" ? "VE" : "CU";
    row.Status = isDraft ? "D" : "C";
    row.IsCustom = new bool?(false);
    row.IsResidual = new bool?(true);
    ((PXSelectBase<DRScheduleDetail>) this.ScheduleDetail).Insert(row);
  }

  public void CreateCreditLine(
    DRScheduleDetail scheduleDetail,
    DRDeferredCode deferralCode,
    int? branchID)
  {
    this.InitBalance(((PXSelectBase<DRScheduleTran>) this.Transactions).Insert(new DRScheduleTran()
    {
      BranchID = branchID,
      AccountID = scheduleDetail.AccountID,
      SubID = scheduleDetail.SubID,
      Amount = scheduleDetail.TotalAmt,
      RecDate = ((PXGraph) this).Accessinfo.BusinessDate,
      TranDate = ((PXGraph) this).Accessinfo.BusinessDate,
      FinPeriodID = scheduleDetail.FinPeriodID,
      LineNbr = new int?(0),
      DetailLineNbr = scheduleDetail.DetailLineNbr,
      ScheduleID = scheduleDetail.ScheduleID,
      ComponentID = scheduleDetail.ComponentID,
      Status = "P"
    }), scheduleDetail, deferralCode.AccountType);
  }

  /// <summary>
  /// Determines whether a given deferral schedule detail originates
  /// from a reversal document.
  /// </summary>
  private static bool IsReversed(DRScheduleDetail scheduleDetail)
  {
    return scheduleDetail.DocType == "CRM" || scheduleDetail.DocType == "ADR" || scheduleDetail.DocType == "RCS" || scheduleDetail.DocType == "VQC" || scheduleDetail.DocType == "RQC";
  }

  private DRExpenseBalance CreateDRExpenseBalance(DRScheduleDetail scheduleDetail, string periodID)
  {
    return ((PXSelectBase<DRExpenseBalance>) this.ExpenseBalance).Insert(new DRExpenseBalance()
    {
      FinPeriodID = periodID,
      BranchID = scheduleDetail.BranchID,
      AcctID = scheduleDetail.DefAcctID,
      SubID = scheduleDetail.DefSubID,
      ComponentID = new int?(scheduleDetail.ComponentID.GetValueOrDefault()),
      ProjectID = new int?(scheduleDetail.ProjectID.GetValueOrDefault()),
      VendorID = new int?(scheduleDetail.BAccountID.GetValueOrDefault())
    });
  }

  private DRRevenueBalance CreateDRRevenueBalance(DRScheduleDetail scheduleDetail, string periodID)
  {
    return ((PXSelectBase<DRRevenueBalance>) this.RevenueBalance).Insert(new DRRevenueBalance()
    {
      FinPeriodID = periodID,
      BranchID = scheduleDetail.BranchID,
      AcctID = scheduleDetail.DefAcctID,
      SubID = scheduleDetail.DefSubID,
      ComponentID = new int?(scheduleDetail.ComponentID.GetValueOrDefault()),
      ProjectID = new int?(scheduleDetail.ProjectID.GetValueOrDefault()),
      CustomerID = new int?(scheduleDetail.BAccountID.GetValueOrDefault())
    });
  }

  private DRExpenseProjectionAccum CreateDRExpenseProjectionAccum(
    DRScheduleDetail scheduleDetail,
    string periodID)
  {
    DRExpenseProjectionAccum expenseProjectionAccum = new DRExpenseProjectionAccum();
    expenseProjectionAccum.FinPeriodID = periodID;
    expenseProjectionAccum.BranchID = scheduleDetail.BranchID;
    expenseProjectionAccum.AcctID = scheduleDetail.AccountID;
    expenseProjectionAccum.SubID = scheduleDetail.SubID;
    expenseProjectionAccum.ComponentID = new int?(scheduleDetail.ComponentID.GetValueOrDefault());
    expenseProjectionAccum.ProjectID = new int?(scheduleDetail.ProjectID.GetValueOrDefault());
    expenseProjectionAccum.VendorID = new int?(scheduleDetail.BAccountID.GetValueOrDefault());
    return ((PXSelectBase<DRExpenseProjectionAccum>) this.ExpenseProjection).Insert(expenseProjectionAccum);
  }

  private DRRevenueProjectionAccum CreateDRRevenueProjectionAccum(
    DRScheduleDetail scheduleDetail,
    string periodID)
  {
    DRRevenueProjectionAccum revenueProjectionAccum = new DRRevenueProjectionAccum();
    revenueProjectionAccum.FinPeriodID = periodID;
    revenueProjectionAccum.BranchID = scheduleDetail.BranchID;
    revenueProjectionAccum.AcctID = scheduleDetail.AccountID;
    revenueProjectionAccum.SubID = scheduleDetail.SubID;
    revenueProjectionAccum.ComponentID = new int?(scheduleDetail.ComponentID.GetValueOrDefault());
    revenueProjectionAccum.ProjectID = new int?(scheduleDetail.ProjectID.GetValueOrDefault());
    revenueProjectionAccum.CustomerID = new int?(scheduleDetail.BAccountID.GetValueOrDefault());
    return ((PXSelectBase<DRRevenueProjectionAccum>) this.RevenueProjection).Insert(revenueProjectionAccum);
  }

  public virtual TransactionsGenerator GetTransactionsGenerator(DRDeferredCode deferralCode)
  {
    return new TransactionsGenerator((PXGraph) this, deferralCode);
  }

  PX.Objects.AR.SalesPerson IBusinessAccountProvider.GetSalesPerson(int? salesPersonID)
  {
    return PXResultset<PX.Objects.AR.SalesPerson>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesPerson, PXSelect<PX.Objects.AR.SalesPerson, Where<PX.Objects.AR.SalesPerson.salesPersonID, Equal<Required<PX.Objects.AR.SalesPerson.salesPersonID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) salesPersonID
    }));
  }

  IEnumerable<InventoryItemComponentInfo> IInventoryItemProvider.GetInventoryItemComponents(
    int? inventoryItemID,
    string requiredAllocationMethod)
  {
    return requiredAllocationMethod != "R" ? ((IEnumerable<PXResult<INComponent>>) PXSelectBase<INComponent, PXSelectJoin<INComponent, InnerJoin<DRDeferredCode, On<INComponent.deferredCode, Equal<DRDeferredCode.deferredCodeID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<INComponent.componentID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>, Where<INComponent.inventoryID, Equal<Required<INComponent.inventoryID>>, And<INComponent.amtOption, Equal<Required<INComponent.amtOption>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) inventoryItemID,
      (object) requiredAllocationMethod
    })).AsEnumerable<PXResult<INComponent>>().Cast<PXResult<INComponent, DRDeferredCode, PX.Objects.IN.InventoryItem>>().Select<PXResult<INComponent, DRDeferredCode, PX.Objects.IN.InventoryItem>, InventoryItemComponentInfo>((Func<PXResult<INComponent, DRDeferredCode, PX.Objects.IN.InventoryItem>, InventoryItemComponentInfo>) (result => new InventoryItemComponentInfo()
    {
      Component = PXResult<INComponent, DRDeferredCode, PX.Objects.IN.InventoryItem>.op_Implicit(result),
      Item = PXResult<INComponent, DRDeferredCode, PX.Objects.IN.InventoryItem>.op_Implicit(result),
      DeferralCode = PXResult<INComponent, DRDeferredCode, PX.Objects.IN.InventoryItem>.op_Implicit(result)
    })) : ((IEnumerable<PXResult<INComponent>>) PXSelectBase<INComponent, PXSelectJoin<INComponent, InnerJoin<PX.Objects.IN.InventoryItem, On<INComponent.componentID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<INComponent.inventoryID, Equal<Required<INComponent.inventoryID>>, And<INComponent.amtOption, Equal<Required<INComponent.amtOption>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) inventoryItemID,
      (object) requiredAllocationMethod
    })).AsEnumerable<PXResult<INComponent>>().Cast<PXResult<INComponent, PX.Objects.IN.InventoryItem>>().Select<PXResult<INComponent, PX.Objects.IN.InventoryItem>, InventoryItemComponentInfo>((Func<PXResult<INComponent, PX.Objects.IN.InventoryItem>, InventoryItemComponentInfo>) (result => new InventoryItemComponentInfo()
    {
      Component = PXResult<INComponent, PX.Objects.IN.InventoryItem>.op_Implicit(result),
      Item = PXResult<INComponent, PX.Objects.IN.InventoryItem>.op_Implicit(result),
      DeferralCode = (DRDeferredCode) null
    }));
  }

  string IInventoryItemProvider.GetComponentName(INComponent component)
  {
    return ((PXGraph) this).Caches[typeof (INComponent)].GetValueExt<INComponent.componentID>((object) component) as string;
  }

  public PX.Objects.IN.InventoryItem GetInventoryItemByID(int? inventoryItemID)
  {
    return PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryItemID
    }));
  }

  DRSchedule IDREntityStorage.CreateCopy(DRSchedule originalSchedule)
  {
    return ((PXSelectBase) this.Schedule).Cache.CreateCopy((object) originalSchedule) as DRSchedule;
  }

  DRScheduleTran IDREntityStorage.CreateCopy(DRScheduleTran originalTransaction)
  {
    return ((PXSelectBase) this.Transactions).Cache.CreateCopy((object) originalTransaction) as DRScheduleTran;
  }

  IList<DRScheduleTran> IDREntityStorage.GetDeferralTransactions(
    int? scheduleID,
    int? componentID,
    int? detailLineNbr)
  {
    return (IList<DRScheduleTran>) GraphHelper.RowCast<DRScheduleTran>((IEnumerable) PXSelectBase<DRScheduleTran, PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Required<DRScheduleTran.scheduleID>>, And<DRScheduleTran.componentID, Equal<Required<DRScheduleTran.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Required<DRScheduleTran.detailLineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) scheduleID,
      (object) componentID,
      (object) detailLineNbr
    })).ToList<DRScheduleTran>();
  }

  DRDeferredCode IDREntityStorage.GetDeferralCode(string deferralCodeID)
  {
    return PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) deferralCodeID
    }));
  }

  DRSchedule IDREntityStorage.Insert(DRSchedule schedule)
  {
    long? curyInfoId = schedule.CuryInfoID;
    schedule.CuryInfoID = new long?();
    return ((IDREntityStorage) this).UpdateCuryInfo(((PXSelectBase<DRSchedule>) this.Schedule).Insert(schedule), curyInfoId);
  }

  DRSchedule IDREntityStorage.Update(DRSchedule schedule)
  {
    return ((PXSelectBase<DRSchedule>) this.Schedule).Update(schedule);
  }

  DRSchedule IDREntityStorage.UpdateCuryInfo(DRSchedule schedule, long? curyInfoID)
  {
    PXResultset<PX.Objects.CM.CurrencyInfo> pxResultset;
    if (curyInfoID.HasValue)
      pxResultset = PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) curyInfoID
      });
    else
      pxResultset = (PXResultset<PX.Objects.CM.CurrencyInfo>) null;
    PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResultset);
    if (currencyInfo1 != null)
    {
      ((PXCache) GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>((PXGraph) this)).Clear();
      PX.Objects.CM.CurrencyInfo copy1 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo1);
      copy1.CuryInfoID = new long?();
      copy1.IsReadOnly = new bool?(false);
      PX.Objects.CM.CurrencyInfo copy2 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>((PXGraph) this).Insert(copy1));
      schedule.CuryInfoID = copy2.CuryInfoID;
      schedule.CuryID = copy2.CuryID;
      schedule.BaseCuryID = copy2.BaseCuryID;
    }
    else
    {
      PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) schedule.CuryInfoID
      }));
      currencyInfo2.CuryID = schedule.CuryID ?? schedule.BaseCuryID;
      currencyInfo2.BaseCuryID = schedule.BaseCuryID;
      GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>((PXGraph) this).Update(currencyInfo2);
      schedule.CuryID = currencyInfo2.CuryID;
      schedule.BaseCuryID = currencyInfo2.BaseCuryID;
    }
    return schedule;
  }

  DRScheduleDetail IDREntityStorage.Insert(DRScheduleDetail scheduleDetail)
  {
    return ((PXSelectBase<DRScheduleDetail>) this.ScheduleDetail).Insert(scheduleDetail);
  }

  DRScheduleDetail IDREntityStorage.Update(DRScheduleDetail scheduleDetail)
  {
    return ((PXSelectBase<DRScheduleDetail>) this.ScheduleDetail).Update(scheduleDetail);
  }

  void IDREntityStorage.ScheduleTransactionModified(
    DRScheduleDetail scheduleDetail,
    DRDeferredCode deferralCode,
    DRScheduleTran oldTransaction,
    DRScheduleTran newTransaction)
  {
    this.Subtract(scheduleDetail, oldTransaction, deferralCode.AccountType);
    this.Add(scheduleDetail, newTransaction, deferralCode.AccountType);
    ((PXSelectBase<DRScheduleTran>) this.Transactions).Update(newTransaction);
  }

  IEnumerable<DRScheduleTran> IDREntityStorage.CreateDeferralTransactions(
    DRSchedule deferralSchedule,
    DRScheduleDetail scheduleDetail,
    DRDeferredCode deferralCode,
    int? branchID)
  {
    return this.GenerateAndAddDeferralTransactions(deferralSchedule, scheduleDetail, deferralCode);
  }

  void IDREntityStorage.CreateCreditLineTransaction(
    DRScheduleDetail scheduleDetail,
    DRDeferredCode deferralCode,
    int? branchID)
  {
    this.CreateCreditLine(scheduleDetail, deferralCode, branchID);
  }

  void IDREntityStorage.NonDraftDeferralTransactionsPrepared(
    DRScheduleDetail scheduleDetail,
    DRDeferredCode deferralCode,
    IEnumerable<DRScheduleTran> deferralTransactions)
  {
    this.UpdateBalanceProjection(deferralTransactions, scheduleDetail, deferralCode.AccountType);
  }

  private DRScheduleDetail GetScheduleDetailForComponent(
    int? scheduleID,
    int? componentID,
    int? detailLineNbr)
  {
    DRScheduleDetail detailForComponent;
    this.ScheduleDetailForComponent.TryGetValue(new Tuple<int, int, int>(scheduleID.Value, componentID.Value, detailLineNbr.Value), out detailForComponent);
    if (detailForComponent == null)
    {
      detailForComponent = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>, And<DRScheduleDetail.detailLineNbr, Equal<Required<DRScheduleDetail.detailLineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) scheduleID,
        (object) componentID,
        (object) detailLineNbr
      }));
      if (detailForComponent != null)
        this.ScheduleDetailForComponent[new Tuple<int, int, int>(scheduleID.Value, componentID.Value, detailLineNbr.Value)] = detailForComponent;
    }
    return detailForComponent;
  }

  /// <summary>
  /// Encapsulates the information necessary to create
  /// a deferral schedule. Includes all fields from DRSchedule
  /// along with the necessary document transaction information.
  /// </summary>
  public class DRScheduleParameters : DRSchedule
  {
    public int? EmployeeID { get; set; }

    public int? SalesPersonID { get; set; }

    public int? SubID { get; set; }
  }

  private class APSubaccountProvider(PXGraph graph) : SubaccountProviderBase(graph)
  {
    public override string MakeSubaccount<Field>(
      string mask,
      object[] sourceFieldValues,
      System.Type[] sourceFields)
    {
      return SubAccountMaskAPAttribute.MakeSub<Field>(this._graph, mask, sourceFieldValues, sourceFields);
    }
  }

  protected class ARSubaccountProvider(PXGraph graph) : SubaccountProviderBase(graph)
  {
    public override string MakeSubaccount<Field>(
      string mask,
      object[] sourceFieldValues,
      System.Type[] sourceFields)
    {
      return SubAccountMaskARAttribute.MakeSub<Field>(this._graph, mask, sourceFieldValues, sourceFields);
    }
  }
}
