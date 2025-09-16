// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ScheduleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.DR;

[Serializable]
public class ScheduleMaint : PXGraph<
#nullable disable
ScheduleMaint>
{
  public PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>> Schedule;
  public PXSelect<DRScheduleDetail> Document;
  public PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<DRScheduleDetail.detailLineNbr, Equal<Current<DRScheduleDetail.detailLineNbr>>>>>> DocumentProperties;
  public PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>, And<DRScheduleTran.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Current<DRScheduleDetail.detailLineNbr>>, And<DRScheduleTran.lineNbr, NotEqual<Current<DRScheduleDetail.creditLineNbr>>>>>>> Transactions;
  public PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>, And<DRScheduleTran.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Current<DRScheduleDetail.detailLineNbr>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.OpenStatus>, And<DRScheduleTran.lineNbr, NotEqual<Current<DRScheduleDetail.creditLineNbr>>>>>>>> OpenTransactions;
  public PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>, And<DRScheduleTran.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Current<DRScheduleDetail.detailLineNbr>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.ProjectedStatus>, And<DRScheduleTran.lineNbr, NotEqual<Current<DRScheduleDetail.creditLineNbr>>>>>>>> ProjectedTransactions;
  public PXSelect<ScheduleMaint.DRScheduleDetailEx> Associated;
  public PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Current<DRScheduleDetail.defCode>>>> DeferredCode;
  public PXSelect<DRExpenseBalance> ExpenseBalance;
  public PXSelect<DRExpenseProjectionAccum> ExpenseProjection;
  public PXSelect<DRRevenueBalance> RevenueBalance;
  public PXSelect<DRRevenueProjectionAccum> RevenueProjection;
  public PXSetup<DRSetup> Setup;
  public PXAction<DRScheduleDetail> cancelex;
  public PXSave<DRScheduleDetail> Save;
  public PXCancel<DRScheduleDetail> Cancel;
  public PXInsert<DRScheduleDetail> Insert;
  public PXDelete<DRScheduleDetail> Delete;
  public PXFirst<DRScheduleDetail> First;
  public PXPrevious<DRScheduleDetail> Prev;
  public PXNext<DRScheduleDetail> Next;
  public PXLast<DRScheduleDetail> Last;
  public PXAction<DRScheduleDetail> viewDoc;
  public PXAction<DRScheduleDetail> viewSchedule;
  public PXAction<DRScheduleDetail> viewBatch;
  public PXAction<DRScheduleDetail> release;
  public PXAction<DRScheduleDetail> genTran;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public ScheduleMaint()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
  }

  public virtual IEnumerable associated([PXDBInt] int? scheduleID, [PXDBString] string componentID)
  {
    if (scheduleID.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem>.Config>.Search<PX.Objects.IN.InventoryItem.inventoryCD>((PXGraph) this, (object) componentID, Array.Empty<object>()));
      if (inventoryItem != null)
        ((PXSelectBase<DRScheduleDetail>) this.Document).Current = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) scheduleID,
          (object) inventoryItem.InventoryID
        }));
      else
        ((PXSelectBase<DRScheduleDetail>) this.Document).Current = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) scheduleID,
          (object) 0
        }));
      DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (drSchedule != null)
      {
        if (drSchedule.Module == "AR")
        {
          if (drSchedule.DocType == "CRM")
          {
            PX.Objects.AR.ARTran arTran = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<DRScheduleDetail.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<DRScheduleDetail.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Required<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) drSchedule.LineNbr
            }));
            if (arTran != null)
              return (IEnumerable) PXSelectBase<ScheduleMaint.DRScheduleDetailEx, PXSelect<ScheduleMaint.DRScheduleDetailEx, Where<ScheduleMaint.DRScheduleDetailEx.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<ScheduleMaint.DRScheduleDetailEx.scheduleID, Equal<Required<ScheduleMaint.DRScheduleDetailEx.scheduleID>>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) arTran.DefScheduleID
              });
          }
          else if (drSchedule.DocType == "INV" || drSchedule.DocType == "DRM")
          {
            List<ScheduleMaint.DRScheduleDetailEx> scheduleDetailExList = new List<ScheduleMaint.DRScheduleDetailEx>();
            foreach (PXResult<PX.Objects.AR.ARTran> pxResult in PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.defScheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
            {
              PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
              scheduleDetailExList.Add(PXResultset<ScheduleMaint.DRScheduleDetailEx>.op_Implicit(PXSelectBase<ScheduleMaint.DRScheduleDetailEx, PXSelectJoin<ScheduleMaint.DRScheduleDetailEx, InnerJoin<DRSchedule, On<DRSchedule.scheduleID, Equal<ScheduleMaint.DRScheduleDetailEx.scheduleID>>>, Where<DRScheduleDetail.module, Equal<BatchModule.moduleAR>, And<DRScheduleDetail.docType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<DRScheduleDetail.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<ScheduleMaint.DRScheduleDetailEx.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<DRSchedule.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
              {
                (object) arTran.TranType,
                (object) arTran.RefNbr,
                (object) arTran.LineNbr
              })));
            }
            return (IEnumerable) scheduleDetailExList;
          }
        }
        else if (drSchedule.Module == "AP")
        {
          if (drSchedule.DocType == "ADR")
          {
            APTran apTran = PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<DRScheduleDetail.docType>>, And<APTran.refNbr, Equal<Current<DRScheduleDetail.refNbr>>, And<APTran.lineNbr, Equal<Required<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) drSchedule.LineNbr
            }));
            if (apTran != null)
              return (IEnumerable) PXSelectBase<ScheduleMaint.DRScheduleDetailEx, PXSelect<ScheduleMaint.DRScheduleDetailEx, Where<ScheduleMaint.DRScheduleDetailEx.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<ScheduleMaint.DRScheduleDetailEx.scheduleID, Equal<Required<ScheduleMaint.DRScheduleDetailEx.scheduleID>>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) apTran.DefScheduleID
              });
          }
          else if (drSchedule.DocType == "INV" || drSchedule.DocType == "ACR")
          {
            List<DRScheduleDetail> drScheduleDetailList = new List<DRScheduleDetail>();
            foreach (PXResult<APTran> pxResult in PXSelectBase<APTran, PXSelect<APTran, Where<APTran.defScheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
            {
              APTran apTran = PXResult<APTran>.op_Implicit(pxResult);
              drScheduleDetailList.Add((DRScheduleDetail) PXResultset<ScheduleMaint.DRScheduleDetailEx>.op_Implicit(PXSelectBase<ScheduleMaint.DRScheduleDetailEx, PXSelectJoin<ScheduleMaint.DRScheduleDetailEx, InnerJoin<DRSchedule, On<DRSchedule.scheduleID, Equal<DRScheduleDetail.scheduleID>>>, Where<DRScheduleDetail.module, Equal<BatchModule.moduleAP>, And<DRScheduleDetail.docType, Equal<Required<APTran.tranType>>, And<DRScheduleDetail.refNbr, Equal<Required<APTran.refNbr>>, And<ScheduleMaint.DRScheduleDetailEx.componentID, Equal<Current<DRScheduleDetail.componentID>>, And<DRSchedule.lineNbr, Equal<Required<APTran.lineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
              {
                (object) apTran.TranType,
                (object) apTran.RefNbr,
                (object) apTran.LineNbr
              })));
            }
            return (IEnumerable) drScheduleDetailList;
          }
        }
      }
    }
    return (IEnumerable) new List<DRScheduleDetail>();
  }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable CancelEx(PXAdapter a)
  {
    ScheduleMaint scheduleMaint = this;
    DRScheduleDetail drScheduleDetail1 = (DRScheduleDetail) null;
    int? nullable1 = new int?();
    string str = (string) null;
    int? nullable2 = new int?(0);
    if (a.Searches != null)
    {
      if (a.Searches.Length != 0 && a.Searches[0] != null)
        nullable1 = new int?(int.Parse(a.Searches[0].ToString()));
      if (a.Searches.Length > 1 && a.Searches[1] != null)
        str = a.Searches[1].ToString();
    }
    if (!string.IsNullOrEmpty(str))
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem>.Config>.Search<PX.Objects.IN.InventoryItem.inventoryCD>((PXGraph) scheduleMaint, (object) str, Array.Empty<object>()));
      if (inventoryItem != null)
        nullable2 = inventoryItem.InventoryID;
    }
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelectReadonly<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) scheduleMaint, new object[1]
    {
      (object) nullable1
    }));
    if (((PXSelectBase<DRScheduleDetail>) scheduleMaint.Document).Current == null)
    {
      foreach (DRScheduleDetail drScheduleDetail2 in ((PXAction) scheduleMaint.Cancel).Press(a))
        drScheduleDetail1 = drScheduleDetail2;
    }
    else if (drSchedule != null)
    {
      int? nullable3 = ((PXSelectBase<DRScheduleDetail>) scheduleMaint.Document).Current.ScheduleID;
      int? nullable4 = nullable1;
      if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
      {
        DRScheduleDetail drScheduleDetail3 = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>>>.Config>.SelectWindowed((PXGraph) scheduleMaint, 0, 1, new object[1]
        {
          (object) nullable1
        }));
        PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) null;
        if (drScheduleDetail3 != null)
          inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) scheduleMaint, new object[1]
          {
            (object) drScheduleDetail3.ComponentID
          }));
        a.Searches[1] = inventoryItem != null ? (object) inventoryItem.InventoryCD : (object) null;
        foreach (DRScheduleDetail drScheduleDetail4 in ((PXAction) scheduleMaint.Cancel).Press(a))
          drScheduleDetail1 = drScheduleDetail4;
      }
      else
      {
        PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) scheduleMaint, new object[1]
        {
          (object) nullable2
        }));
        int? nullable5 = inventoryItem == null ? new int?(0) : inventoryItem.InventoryID;
        int? componentId = ((PXSelectBase<DRScheduleDetail>) scheduleMaint.Document).Current.ComponentID;
        nullable3 = nullable5;
        if (!(componentId.GetValueOrDefault() == nullable3.GetValueOrDefault() & componentId.HasValue == nullable3.HasValue))
        {
          DRScheduleDetail drScheduleDetail5 = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>>>>.Config>.Select((PXGraph) scheduleMaint, new object[2]
          {
            (object) nullable1,
            (object) nullable5
          }));
          if (drScheduleDetail5 != null)
          {
            drScheduleDetail1 = drScheduleDetail5;
          }
          else
          {
            foreach (DRScheduleDetail drScheduleDetail6 in ((PXAction) scheduleMaint.Cancel).Press(a))
              drScheduleDetail1 = drScheduleDetail6;
          }
        }
        else
        {
          foreach (DRScheduleDetail drScheduleDetail7 in ((PXAction) scheduleMaint.Cancel).Press(a))
            drScheduleDetail1 = drScheduleDetail7;
        }
      }
    }
    else
    {
      ((PXSelectBase) scheduleMaint.Document).Cache.Remove((object) ((PXSelectBase<DRScheduleDetail>) scheduleMaint.Document).Current);
      ((PXSelectBase<DRScheduleDetail>) scheduleMaint.Document).Current = (DRScheduleDetail) null;
      drScheduleDetail1 = ((PXSelectBase<DRScheduleDetail>) scheduleMaint.Document).Insert(new DRScheduleDetail()
      {
        ScheduleID = nullable1,
        ComponentID = nullable2
      });
      ((PXSelectBase) scheduleMaint.Document).Cache.IsDirty = false;
    }
    yield return (object) drScheduleDetail1;
  }

  [PXUIField(DisplayName = "View Document")]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current != null)
      DRRedirectHelper.NavigateToOriginalDocument((PXGraph) this, ((PXSelectBase<DRSchedule>) this.Schedule).Current);
    else if (((PXSelectBase<DRScheduleDetail>) this.Document).Current != null)
      DRRedirectHelper.NavigateToOriginalDocument((PXGraph) this, ((PXSelectBase<DRScheduleDetail>) this.Document).Current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Schedule")]
  [PXButton(ImageKey = "Inquiry")]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    if (((PXSelectBase<ScheduleMaint.DRScheduleDetailEx>) this.Associated).Current != null)
    {
      ScheduleMaint instance = PXGraph.CreateInstance<ScheduleMaint>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<DRScheduleDetail>) instance.Document).Current = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) ((PXSelectBase<ScheduleMaint.DRScheduleDetailEx>) this.Associated).Current.ScheduleID,
        (object) ((PXSelectBase<ScheduleMaint.DRScheduleDetailEx>) this.Associated).Current.ComponentID
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, "View Referenced Schedule");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View GL Batch")]
  [PXButton(ImageKey = "Inquiry")]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXGraph) instance).Clear();
    Batch batch = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.module, Equal<BatchModule.moduleDR>, And<Batch.batchNbr, Equal<Current<DRScheduleTran.batchNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (batch != null)
    {
      ((PXSelectBase<Batch>) instance.BatchModule).Current = batch;
      throw new PXRedirectRequiredException((PXGraph) instance, nameof (ViewBatch));
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Release", Enabled = false)]
  [PXButton(ImageKey = "Process")]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    this.ReleaseCustomScheduleDetail();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Create Transactions")]
  [PXButton(ImageKey = "Process")]
  public virtual IEnumerable GenTran(PXAdapter adapter)
  {
    if (((PXSelectBase<DRScheduleDetail>) this.Document).Current != null)
    {
      DRDeferredCode defCode = PXResultset<DRDeferredCode>.op_Implicit(((PXSelectBase<DRDeferredCode>) this.DeferredCode).Select(Array.Empty<object>()));
      if (defCode != null)
      {
        if (((PXSelectBase<DRScheduleTran>) this.Transactions).Select(Array.Empty<object>()).Count > 0)
        {
          if (((PXSelectBase) this.Document).View.Ask((object) ((PXSelectBase<DRScheduleDetail>) this.Document).Current, "Confirmation", "Transactions already exist. Do you want to recreate them?", (MessageButtons) 4, (MessageIcon) 2) == 6)
            this.CreateTransactions(defCode);
        }
        else
          this.CreateTransactions(defCode);
      }
    }
    return adapter.Get();
  }

  protected virtual void DRScheduleDetail_ScheduleID_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    int? returnValue = (int?) e.ReturnValue;
    int num = 0;
    if (!(returnValue.GetValueOrDefault() < num & returnValue.HasValue))
      return;
    e.ReturnValue = (object) null;
  }

  protected virtual void DRSchedule_DocDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is DRSchedule))
      return;
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void DRSchedule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    if (string.IsNullOrEmpty(row.Module))
      ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<DRScheduleDetail.documentType>((object) ((PXSelectBase<DRScheduleDetail>) this.Document).Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[documentType]"
      }));
    if (!string.IsNullOrEmpty(row.FinPeriodID))
      return;
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<DRScheduleDetail.finPeriodID>((object) ((PXSelectBase<DRScheduleDetail>) this.Document).Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[finPeriodID]"
    }));
  }

  protected virtual void DRScheduleDetail_DocumentType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    string module = DRScheduleDocumentType.ExtractModule(row.DocumentType);
    row.DocType = DRScheduleDocumentType.ExtractDocType(row.DocumentType);
    if (!(row.Module != module))
      return;
    row.Module = module;
    row.DefCode = (string) null;
    row.DefAcctID = new int?();
    row.DefSubID = new int?();
    row.BAccountID = new int?();
    row.AccountID = new int?();
    row.SubID = new int?();
    row.RefNbr = (string) null;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<DRScheduleDetail.componentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ComponentID
    }));
    if (inventoryItem == null)
      return;
    row.AccountID = row.Module == "AP" ? inventoryItem.COGSAcctID : inventoryItem.SalesAcctID;
    row.SubID = row.Module == "AP" ? inventoryItem.COGSSubID : inventoryItem.SalesSubID;
  }

  protected virtual void DRScheduleDetail_TotalAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row) || !(row.Status == "D"))
      return;
    row.DefAmt = row.TotalAmt;
  }

  protected virtual void DRScheduleDetail_DefCode_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    DRDeferredCode drDeferredCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRScheduleDetail.defCode>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DefCode
    }));
    if (drDeferredCode == null)
      return;
    row.DefCode = drDeferredCode.DeferredCodeID;
    row.DefAcctID = drDeferredCode.AccountID;
    row.DefSubID = drDeferredCode.SubID;
  }

  protected virtual void DRScheduleDetail_DocDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail))
      return;
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void DRScheduleDetail_BAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    int? nullable = row.ComponentID;
    if (nullable.HasValue)
    {
      nullable = row.ComponentID;
      int num = 0;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        nullable = row.AccountID;
        if (nullable.HasValue)
          return;
      }
    }
    switch (row.Module)
    {
      case "AP":
        PX.Objects.CR.Location location1 = (PX.Objects.CR.Location) ((PXResult) PXSelectBase<PX.Objects.AP.Vendor, PXSelectJoin<PX.Objects.AP.Vendor, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.AP.Vendor.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<DRScheduleDetail.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())[0])[1];
        nullable = location1.VExpenseAcctID;
        if (!nullable.HasValue)
          break;
        row.AccountID = location1.VExpenseAcctID;
        row.SubID = location1.VExpenseSubID;
        break;
      case "AR":
        PX.Objects.CR.Location location2 = (PX.Objects.CR.Location) ((PXResult) PXSelectBase<PX.Objects.AR.Customer, PXSelectJoin<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.AR.Customer.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<DRScheduleDetail.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())[0])[1];
        nullable = location2.CSalesAcctID;
        if (!nullable.HasValue)
          break;
        row.AccountID = location2.CSalesAcctID;
        row.SubID = location2.CSalesSubID;
        break;
    }
  }

  protected virtual void DRScheduleDetail_ScheduleID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleDetail_RefNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    if (row.Module == "AR")
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<DRScheduleDetail.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<DRScheduleDetail.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (arInvoice == null)
        return;
      object copy = sender.CreateCopy((object) row);
      row.BAccountID = arInvoice.CustomerID;
      sender.RaiseFieldUpdated<DRScheduleDetail.bAccountID>((object) row, copy);
    }
    else
    {
      if (!(row.Module == "AP"))
        return;
      PX.Objects.AP.APInvoice apInvoice = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<DRScheduleDetail.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Current<DRScheduleDetail.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (apInvoice == null)
        return;
      object copy = sender.CreateCopy((object) row);
      row.BAccountID = apInvoice.VendorID;
      sender.RaiseFieldUpdated<DRScheduleDetail.bAccountID>((object) row, copy);
    }
  }

  protected virtual void DRScheduleDetail_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row is DRScheduleDetail row)
    {
      row.Status = "D";
      row.IsCustom = new bool?(true);
      if (!row.ComponentID.HasValue)
        row.ComponentID = new int?(0);
      ((PXSelectBase) this.Schedule).Cache.Clear();
      DRSchedule drSchedule = ((PXSelectBase<DRSchedule>) this.Schedule).Insert(new DRSchedule());
      row.ScheduleID = drSchedule.ScheduleID;
      ((PXSelectBase) this.Schedule).Cache.IsDirty = false;
      sender.Normalize();
    }
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<DRScheduleDetail.componentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ComponentID
    }));
    if (inventoryItem == null)
      return;
    row.AccountID = row.Module == "AP" ? inventoryItem.COGSAcctID : inventoryItem.SalesAcctID;
    row.SubID = row.Module == "AP" ? inventoryItem.COGSSubID : inventoryItem.SalesSubID;
    DRDeferredCode drDeferredCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRScheduleDetail.defCode>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryItem.DeferredCode
    }));
    if (drDeferredCode == null)
      return;
    row.DefCode = drDeferredCode.DeferredCodeID;
    row.DefAcctID = drDeferredCode.AccountID;
    row.DefSubID = drDeferredCode.SubID;
  }

  protected virtual void DRScheduleDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row) || !((PXSelectBase) this.Schedule).Cache.AllowUpdate)
      return;
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(((PXSelectBase<DRSchedule>) this.Schedule).Select(Array.Empty<object>()));
    if (drSchedule == null)
      return;
    drSchedule.Module = row.Module;
    drSchedule.DocType = row.DocType;
    drSchedule.RefNbr = row.RefNbr;
    drSchedule.LineNbr = row.LineNbr;
    drSchedule.DocDate = row.DocDate;
    drSchedule.FinPeriodID = row.TranPeriodID;
    ((PXSelectBase<DRSchedule>) this.Schedule).Update(drSchedule);
  }

  protected virtual void DRSchedule_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    sender.AllowDelete = row.IsDraft.GetValueOrDefault();
    ((PXAction) this.release).SetVisible(row.IsCustom.GetValueOrDefault());
    ((PXAction) this.release).SetEnabled(row.IsDraft.GetValueOrDefault());
  }

  protected virtual void DRScheduleDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    row.DocumentType = DRScheduleDocumentType.BuildDocumentType(row.Module, row.DocType);
    row.DefTotal = new Decimal?(this.SumOpenAndProjectedTransactions());
    PXCache pxCache = sender;
    DRScheduleDetail drScheduleDetail = row;
    int? componentId = row.ComponentID;
    int num1 = 0;
    int num2 = !(componentId.GetValueOrDefault() == num1 & componentId.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.componentID>(pxCache, (object) drScheduleDetail, num2 != 0);
    if (!(row.Status == "D"))
      return;
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.finPeriodID>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.defAcctID>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.defSubID>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.totalAmt>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.documentType>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.refNbr>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.lineNbr>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.accountID>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.subID>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.bAccountID>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.defCode>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.docDate>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.componentID>(sender, (object) row, true);
  }

  protected virtual void DRScheduleDetail_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row) || row.IsResidual.GetValueOrDefault() || !string.IsNullOrEmpty(row.DefCode))
      return;
    sender.RaiseExceptionHandling<DRScheduleDetail.defCode>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
  }

  protected virtual void DRScheduleDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row) || !(row.Status == "D"))
      return;
    ((PXSelectBase<DRSchedule>) this.Schedule).Delete(PXResultset<DRSchedule>.op_Implicit(((PXSelectBase<DRSchedule>) this.Schedule).Select(Array.Empty<object>())));
  }

  protected virtual void DRScheduleTran_RecDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRScheduleTran row))
      return;
    row.FinPeriodID = this.FinPeriodRepository.GetPeriodIDFromDate(row.RecDate, new int?(0));
  }

  protected virtual void DRScheduleTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is DRScheduleTran) || ((PXSelectBase<DRScheduleDetail>) this.Document).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Document).Current.Status != "D"))
      return;
    DRDeferredCode dc = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Current<DRScheduleDetail.defCode>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    bool flag = true;
    if (this.SkipTotalCheck(dc))
      flag = false;
    if (!flag)
      return;
    Decimal num1 = this.SumOpenAndProjectedTransactions();
    Decimal num2 = num1;
    Decimal? defAmt = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.DefAmt;
    Decimal valueOrDefault = defAmt.GetValueOrDefault();
    if (!(num2 == valueOrDefault & defAmt.HasValue) && sender.RaiseExceptionHandling<DRScheduleDetail.defTotal>(e.Row, (object) num1, (Exception) new PXSetPropertyException("Sum of all open deferred transactions must be equal to Deferred Amount.")))
      throw new PXRowPersistingException(typeof (DRScheduleDetail.defTotal).Name, (object) num1, "Sum of all open deferred transactions must be equal to Deferred Amount.");
  }

  public virtual bool SkipTotalCheck(DRDeferredCode dc) => dc != null && dc.Method == "C";

  protected virtual void DRScheduleTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRScheduleTran row))
      return;
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.recDate>(sender, (object) row, row.Status == "O" || row.Status == "J");
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.amount>(sender, (object) row, row.Status == "O" || row.Status == "J");
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.accountID>(sender, (object) row, row.Status == "O" || row.Status == "J");
    PXUIFieldAttribute.SetEnabled<DRScheduleTran.subID>(sender, (object) row, row.Status == "O" || row.Status == "J");
  }

  protected virtual void DRScheduleTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    DRScheduleTran row = e.Row as DRScheduleTran;
    if (sender.ObjectsEqual<DRScheduleTran.finPeriodID, DRScheduleTran.accountID, DRScheduleTran.subID, DRScheduleTran.amount>(e.Row, e.OldRow) || ((PXSelectBase<DRScheduleDetail>) this.Document).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Document).Current.Status == "O"))
      return;
    this.Subtract((DRScheduleTran) e.OldRow);
    this.Add(row);
  }

  protected virtual void DRScheduleTran_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is DRScheduleTran row) || ((PXSelectBase<DRScheduleDetail>) this.Document).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Document).Current.Status == "O"))
      return;
    this.Add(row);
  }

  protected virtual void DRScheduleTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is DRScheduleTran row) || ((PXSelectBase<DRScheduleDetail>) this.Document).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Document).Current.Status == "O"))
      return;
    this.Subtract(row);
  }

  private void CreateTransactions(DRDeferredCode defCode)
  {
    if (((PXSelectBase<DRScheduleDetail>) this.Document).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Document).Current.Status == "D"))
      return;
    foreach (PXResult<DRScheduleTran> pxResult in ((PXSelectBase<DRScheduleTran>) this.Transactions).Select(Array.Empty<object>()))
      ((PXSelectBase<DRScheduleTran>) this.Transactions).Delete(PXResult<DRScheduleTran>.op_Implicit(pxResult));
    if (((PXSelectBase<DRSchedule>) this.Schedule).Current == null || ((PXSelectBase<DRScheduleDetail>) this.Document).Current == null || defCode == null)
      return;
    foreach (DRScheduleTran transaction in (IEnumerable<DRScheduleTran>) this.GetTransactionsGenerator(defCode).GenerateTransactions(((PXSelectBase<DRSchedule>) this.Schedule).Current, ((PXSelectBase<DRScheduleDetail>) this.Document).Current))
      ((PXSelectBase<DRScheduleTran>) this.Transactions).Insert(transaction);
  }

  public static void ReleaseCustomSchedules(IEnumerable<DRSchedule> schedules)
  {
    ScheduleMaint instance = PXGraph.CreateInstance<ScheduleMaint>();
    foreach (DRSchedule schedule in schedules)
    {
      ((PXGraph) instance).Clear();
      instance.ReleaseCustomSchedule(schedule);
    }
  }

  public virtual void ReleaseCustomSchedule(DRSchedule schedule)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      IEnumerable<DRScheduleDetail> drScheduleDetails = GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRSchedule.scheduleID>>, And<DRScheduleDetail.isResidual, Equal<False>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) schedule.ScheduleID
      }));
      this.PerformReleaseCustomScheduleValidations(schedule, drScheduleDetails);
      this.FinPeriodUtils.ValidateFinPeriod<DRScheduleDetail>(drScheduleDetails, (Func<DRScheduleDetail, string>) (m => schedule.FinPeriodID), (Func<DRScheduleDetail, int?[]>) (m => m.BranchID.SingleToArray<int?>()));
      foreach (DRScheduleDetail drScheduleDetail in drScheduleDetails)
      {
        ((PXGraph) this).Clear();
        ((PXSelectBase<DRScheduleDetail>) this.Document).Current = drScheduleDetail;
        this.ReleaseCustomScheduleDetail();
      }
      transactionScope.Complete();
    }
  }

  public virtual void PerformReleaseCustomScheduleValidations(
    DRSchedule schedule,
    IEnumerable<DRScheduleDetail> details)
  {
  }

  internal void ReleaseCustomScheduleDetail()
  {
    if (((PXSelectBase<DRScheduleDetail>) this.Document).Current == null || !(((PXSelectBase<DRScheduleDetail>) this.Document).Current.Status == "D"))
      return;
    this.CreateIncomingTransaction();
    this.UpdateHistory();
    ((PXSelectBase<DRScheduleDetail>) this.Document).Current.Status = "O";
    ((PXSelectBase<DRScheduleDetail>) this.Document).Current.IsOpen = new bool?(true);
    ((PXSelectBase<DRScheduleDetail>) this.Document).Update(((PXSelectBase<DRScheduleDetail>) this.Document).Current);
    ((PXSelectBase<DRSchedule>) this.Schedule).Current.IsDraft = new bool?(false);
    ((PXSelectBase<DRSchedule>) this.Schedule).Update(((PXSelectBase<DRSchedule>) this.Schedule).Current);
    ((PXAction) this.Save).Press();
  }

  private void CreateIncomingTransaction()
  {
    DRScheduleTran tran = ((PXSelectBase<DRScheduleTran>) this.Transactions).Insert(new DRScheduleTran()
    {
      BranchID = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.BranchID,
      AccountID = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.AccountID,
      SubID = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.SubID,
      Amount = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.TotalAmt,
      RecDate = ((PXGraph) this).Accessinfo.BusinessDate,
      TranDate = ((PXGraph) this).Accessinfo.BusinessDate,
      FinPeriodID = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.FinPeriodID,
      LineNbr = new int?(0),
      ScheduleID = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.ScheduleID,
      ComponentID = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.ComponentID,
      Status = "P"
    });
    if (((PXSelectBase<DRScheduleDetail>) this.Document).Current.Module == "AR")
      this.InitBalance(tran, ((PXSelectBase<DRScheduleDetail>) this.Document).Current, "I");
    else
      this.InitBalance(tran, ((PXSelectBase<DRScheduleDetail>) this.Document).Current, "E");
  }

  private void UpdateHistory()
  {
    foreach (PXResult<DRScheduleTran> pxResult in ((PXSelectBase<DRScheduleTran>) this.OpenTransactions).Select(Array.Empty<object>()))
    {
      DRScheduleTran tran = PXResult<DRScheduleTran>.op_Implicit(pxResult);
      if (((PXSelectBase<DRScheduleDetail>) this.Document).Current.Module == "AR")
        this.UpdateBalanceProjection(tran, ((PXSelectBase<DRScheduleDetail>) this.Document).Current, "I");
      else
        this.UpdateBalanceProjection(tran, ((PXSelectBase<DRScheduleDetail>) this.Document).Current, "E");
    }
  }

  public virtual void RebuildProjections()
  {
    List<DRScheduleTran> existingTrans = this.GetExistingTrans(((PXSelectBase<DRScheduleDetail>) this.Document).Current);
    Decimal val2 = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.TotalAmt.Value;
    List<string> stringList = new List<string>();
    foreach (DRScheduleTran drScheduleTran in existingTrans)
    {
      if (!stringList.Contains(drScheduleTran.FinPeriodID))
        stringList.Add(drScheduleTran.FinPeriodID);
      val2 -= Math.Min(drScheduleTran.Amount ?? 0M, val2);
    }
    DRSchedule deferralSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    IList<DRScheduleTran> transactions = this.GetTransactionsGenerator(PXResultset<DRDeferredCode>.op_Implicit(((PXSelectBase<DRDeferredCode>) this.DeferredCode).Select(Array.Empty<object>()))).GenerateTransactions(deferralSchedule, ((PXSelectBase<DRScheduleDetail>) this.Document).Current);
    if (existingTrans.Count > 0)
    {
      int? parentOrganizationId = PXAccess.GetParentOrganizationID(((PXSelectBase<DRScheduleDetail>) this.Document).Current.BranchID);
      DateTime dateTime = this.FinPeriodRepository.PeriodStartDate(stringList[stringList.Count - 1], parentOrganizationId);
      List<DRScheduleTran> drScheduleTranList = new List<DRScheduleTran>();
      for (int index = 0; index < transactions.Count - 1; ++index)
      {
        if (this.FinPeriodRepository.PeriodStartDate(transactions[index].FinPeriodID, parentOrganizationId) <= dateTime || stringList.Contains(transactions[index].FinPeriodID))
          drScheduleTranList.Add(transactions[index]);
      }
      Decimal? defAmt = ((PXSelectBase<DRScheduleDetail>) this.Document).Current.DefAmt;
      Decimal num1 = 0M;
      if (defAmt.GetValueOrDefault() == num1 & defAmt.HasValue)
        drScheduleTranList.Add(transactions[transactions.Count - 1]);
      foreach (DRScheduleTran drScheduleTran in drScheduleTranList)
        transactions.Remove(drScheduleTran);
      if (transactions.Count > 0)
      {
        Decimal num2 = PXCurrencyAttribute.BaseRound((PXGraph) this, val2 / (Decimal) transactions.Count);
        for (int index = 0; index < transactions.Count - 1; ++index)
        {
          transactions[index].Amount = new Decimal?(num2);
          val2 -= num2;
        }
        transactions[transactions.Count - 1].Amount = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, val2));
      }
    }
    if (transactions.Count <= 0)
      return;
    foreach (PXResult<DRScheduleTran> pxResult in ((PXSelectBase<DRScheduleTran>) this.ProjectedTransactions).Select(Array.Empty<object>()))
      ((PXSelectBase<DRScheduleTran>) this.ProjectedTransactions).Delete(PXResult<DRScheduleTran>.op_Implicit(pxResult));
    foreach (DRScheduleTran drScheduleTran in (IEnumerable<DRScheduleTran>) transactions)
      ((PXSelectBase<DRScheduleTran>) this.ProjectedTransactions).Insert(drScheduleTran);
  }

  private List<DRScheduleTran> GetExistingTrans(DRScheduleDetail sd)
  {
    PXResultset<DRScheduleTran> pxResultset = PXSelectBase<DRScheduleTran, PXSelect<DRScheduleTran, Where<DRScheduleTran.scheduleID, Equal<Required<DRScheduleTran.scheduleID>>, And<DRScheduleTran.componentID, Equal<Required<DRScheduleTran.componentID>>, And<DRScheduleTran.detailLineNbr, Equal<Required<DRScheduleTran.detailLineNbr>>, And<DRScheduleTran.lineNbr, NotEqual<Required<DRScheduleTran.lineNbr>>, And<DRScheduleTran.status, NotEqual<DRScheduleTranStatus.ProjectedStatus>>>>>>, OrderBy<Asc<DRScheduleTran.finPeriodID>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) sd.ScheduleID,
      (object) sd.ComponentID,
      (object) sd.DetailLineNbr,
      (object) sd.CreditLineNbr
    });
    List<DRScheduleTran> existingTrans = new List<DRScheduleTran>(pxResultset.Count);
    foreach (PXResult<DRScheduleTran> pxResult in pxResultset)
    {
      DRScheduleTran drScheduleTran = PXResult<DRScheduleTran>.op_Implicit(pxResult);
      existingTrans.Add(drScheduleTran);
    }
    return existingTrans;
  }

  private void Subtract(DRScheduleTran tran)
  {
    if (PXResultset<DRDeferredCode>.op_Implicit(((PXSelectBase<DRDeferredCode>) this.DeferredCode).Select(Array.Empty<object>())).AccountType == "E")
    {
      this.SubtractExpenseFromProjection(tran);
      this.SubtractExpenseFromBalance(tran);
    }
    else
    {
      this.SubtractRevenueFromProjection(tran);
      this.SubtractRevenueFromBalance(tran);
    }
  }

  private void SubtractRevenueFromProjection(DRScheduleTran tran)
  {
    DRRevenueProjectionAccum revenueProjectionAccum1 = this.CreateDRRevenueProjectionAccum(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.FinPeriodID);
    DRRevenueProjectionAccum revenueProjectionAccum2 = this.CreateDRRevenueProjectionAccum(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.TranPeriodID);
    Decimal? nullable1 = revenueProjectionAccum1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    revenueProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum2;
    nullable2 = revenueProjectionAccum3.TranPTDProjected;
    nullable1 = tran.Amount;
    revenueProjectionAccum3.TranPTDProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void SubtractExpenseFromProjection(DRScheduleTran tran)
  {
    DRExpenseProjectionAccum expenseProjectionAccum1 = this.CreateDRExpenseProjectionAccum(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.FinPeriodID);
    DRExpenseProjectionAccum expenseProjectionAccum2 = this.CreateDRExpenseProjectionAccum(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.TranPeriodID);
    Decimal? nullable1 = expenseProjectionAccum1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    expenseProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseProjectionAccum expenseProjectionAccum3 = expenseProjectionAccum2;
    nullable2 = expenseProjectionAccum3.TranPTDProjected;
    nullable1 = tran.Amount;
    expenseProjectionAccum3.TranPTDProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void SubtractRevenueFromBalance(DRScheduleTran tran)
  {
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.TranPeriodID);
    Decimal? nullable1 = drRevenueBalance1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    drRevenueBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = drRevenueBalance1.EndProjected;
    nullable1 = tran.Amount;
    drRevenueBalance1.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance3 = drRevenueBalance2;
    nullable1 = drRevenueBalance3.TranPTDProjected;
    nullable2 = tran.Amount;
    drRevenueBalance3.TranPTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance4 = drRevenueBalance2;
    nullable2 = drRevenueBalance4.TranEndProjected;
    nullable1 = tran.Amount;
    drRevenueBalance4.TranEndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void SubtractExpenseFromBalance(DRScheduleTran tran)
  {
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.TranPeriodID);
    Decimal? nullable1 = drExpenseBalance1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    drExpenseBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = drExpenseBalance1.EndProjected;
    nullable1 = tran.Amount;
    drExpenseBalance1.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance3 = drExpenseBalance2;
    nullable1 = drExpenseBalance3.PTDProjected;
    nullable2 = tran.Amount;
    drExpenseBalance3.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance4 = drExpenseBalance2;
    nullable2 = drExpenseBalance4.EndProjected;
    nullable1 = tran.Amount;
    drExpenseBalance4.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void Add(DRScheduleTran tran)
  {
    if (PXResultset<DRDeferredCode>.op_Implicit(((PXSelectBase<DRDeferredCode>) this.DeferredCode).Select(Array.Empty<object>())).AccountType == "E")
    {
      this.AddExpenseToProjection(tran);
      this.AddExpenseToBalance(tran);
    }
    else
    {
      this.AddRevenueToProjection(tran);
      this.AddRevenueToBalance(tran);
    }
  }

  private void AddRevenueToProjection(DRScheduleTran tran)
  {
    DRRevenueProjectionAccum revenueProjectionAccum1 = this.CreateDRRevenueProjectionAccum(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.FinPeriodID);
    DRRevenueProjectionAccum revenueProjectionAccum2 = this.CreateDRRevenueProjectionAccum(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.TranPeriodID);
    Decimal? nullable1 = revenueProjectionAccum1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    revenueProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum2;
    nullable2 = revenueProjectionAccum3.TranPTDProjected;
    nullable1 = tran.Amount;
    revenueProjectionAccum3.TranPTDProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void AddExpenseToProjection(DRScheduleTran tran)
  {
    DRExpenseProjectionAccum expenseProjectionAccum1 = this.CreateDRExpenseProjectionAccum(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.FinPeriodID);
    DRExpenseProjectionAccum expenseProjectionAccum2 = this.CreateDRExpenseProjectionAccum(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.TranPeriodID);
    Decimal? nullable1 = expenseProjectionAccum1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    expenseProjectionAccum1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseProjectionAccum expenseProjectionAccum3 = expenseProjectionAccum2;
    nullable2 = expenseProjectionAccum3.TranPTDProjected;
    nullable1 = tran.Amount;
    expenseProjectionAccum3.TranPTDProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void AddRevenueToBalance(DRScheduleTran tran)
  {
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.TranPeriodID);
    Decimal? nullable1 = drRevenueBalance1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    drRevenueBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = drRevenueBalance1.EndProjected;
    nullable1 = tran.Amount;
    drRevenueBalance1.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance3 = drRevenueBalance2;
    nullable1 = drRevenueBalance3.TranPTDProjected;
    nullable2 = tran.Amount;
    drRevenueBalance3.TranPTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRRevenueBalance drRevenueBalance4 = drRevenueBalance2;
    nullable2 = drRevenueBalance4.TranEndProjected;
    nullable1 = tran.Amount;
    drRevenueBalance4.TranEndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private void AddExpenseToBalance(DRScheduleTran tran)
  {
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(((PXSelectBase<DRScheduleDetail>) this.Document).Current, tran.TranPeriodID);
    Decimal? nullable1 = drExpenseBalance1.PTDProjected;
    Decimal? nullable2 = tran.Amount;
    drExpenseBalance1.PTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = drExpenseBalance1.EndProjected;
    nullable1 = tran.Amount;
    drExpenseBalance1.EndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance3 = drExpenseBalance2;
    nullable1 = drExpenseBalance3.TranPTDProjected;
    nullable2 = tran.Amount;
    drExpenseBalance3.TranPTDProjected = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    DRExpenseBalance drExpenseBalance4 = drExpenseBalance2;
    nullable2 = drExpenseBalance4.TranEndProjected;
    nullable1 = tran.Amount;
    drExpenseBalance4.TranEndProjected = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }

  private Decimal SumOpenAndProjectedTransactions()
  {
    Decimal num = 0M;
    foreach (PXResult<DRScheduleTran> pxResult in ((PXSelectBase<DRScheduleTran>) this.OpenTransactions).Select(Array.Empty<object>()))
    {
      DRScheduleTran drScheduleTran = PXResult<DRScheduleTran>.op_Implicit(pxResult);
      num += drScheduleTran.Amount.Value;
    }
    foreach (PXResult<DRScheduleTran> pxResult in ((PXSelectBase<DRScheduleTran>) this.ProjectedTransactions).Select(Array.Empty<object>()))
    {
      DRScheduleTran drScheduleTran = PXResult<DRScheduleTran>.op_Implicit(pxResult);
      num += drScheduleTran.Amount.Value;
    }
    return num;
  }

  private void InitBalance(DRScheduleTran tran, DRScheduleDetail sd, string deferredAccountType)
  {
    switch (deferredAccountType)
    {
      case "E":
        this.InitExpenseBalance(tran, sd);
        break;
      case "I":
        this.InitRevenueBalance(tran, sd);
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

  private void InitRevenueBalance(DRScheduleTran tran, DRScheduleDetail sd)
  {
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(sd, tran.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(sd, tran.TranPeriodID);
    if (ScheduleMaint.IsReversed(sd))
    {
      DRRevenueBalance drRevenueBalance3 = drRevenueBalance1;
      Decimal? ptdDeferred = drRevenueBalance3.PTDDeferred;
      Decimal? amount1 = tran.Amount;
      drRevenueBalance3.PTDDeferred = ptdDeferred.HasValue & amount1.HasValue ? new Decimal?(ptdDeferred.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance4 = drRevenueBalance1;
      Decimal? endBalance = drRevenueBalance4.EndBalance;
      Decimal? amount2 = tran.Amount;
      drRevenueBalance4.EndBalance = endBalance.HasValue & amount2.HasValue ? new Decimal?(endBalance.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance5 = drRevenueBalance1;
      Decimal? endProjected = drRevenueBalance5.EndProjected;
      Decimal? amount3 = tran.Amount;
      drRevenueBalance5.EndProjected = endProjected.HasValue & amount3.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance6 = drRevenueBalance2;
      Decimal? tranPtdDeferred = drRevenueBalance6.TranPTDDeferred;
      Decimal? amount4 = tran.Amount;
      drRevenueBalance6.TranPTDDeferred = tranPtdDeferred.HasValue & amount4.HasValue ? new Decimal?(tranPtdDeferred.GetValueOrDefault() - amount4.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance7 = drRevenueBalance2;
      Decimal? tranEndBalance = drRevenueBalance7.TranEndBalance;
      Decimal? amount5 = tran.Amount;
      drRevenueBalance7.TranEndBalance = tranEndBalance.HasValue & amount5.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() - amount5.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance8 = drRevenueBalance2;
      Decimal? tranEndProjected = drRevenueBalance8.TranEndProjected;
      Decimal? amount6 = tran.Amount;
      drRevenueBalance8.TranEndProjected = tranEndProjected.HasValue & amount6.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRRevenueBalance drRevenueBalance9 = drRevenueBalance1;
      Decimal? ptdDeferred = drRevenueBalance9.PTDDeferred;
      Decimal? amount7 = tran.Amount;
      drRevenueBalance9.PTDDeferred = ptdDeferred.HasValue & amount7.HasValue ? new Decimal?(ptdDeferred.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance10 = drRevenueBalance1;
      Decimal? endBalance = drRevenueBalance10.EndBalance;
      Decimal? amount8 = tran.Amount;
      drRevenueBalance10.EndBalance = endBalance.HasValue & amount8.HasValue ? new Decimal?(endBalance.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance11 = drRevenueBalance1;
      Decimal? endProjected = drRevenueBalance11.EndProjected;
      Decimal? amount9 = tran.Amount;
      drRevenueBalance11.EndProjected = endProjected.HasValue & amount9.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount9.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance12 = drRevenueBalance2;
      Decimal? tranPtdDeferred = drRevenueBalance12.TranPTDDeferred;
      Decimal? amount10 = tran.Amount;
      drRevenueBalance12.TranPTDDeferred = tranPtdDeferred.HasValue & amount10.HasValue ? new Decimal?(tranPtdDeferred.GetValueOrDefault() + amount10.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance13 = drRevenueBalance2;
      Decimal? tranEndBalance = drRevenueBalance13.TranEndBalance;
      Decimal? amount11 = tran.Amount;
      drRevenueBalance13.TranEndBalance = tranEndBalance.HasValue & amount11.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() + amount11.GetValueOrDefault()) : new Decimal?();
      DRRevenueBalance drRevenueBalance14 = drRevenueBalance2;
      Decimal? tranEndProjected = drRevenueBalance14.TranEndProjected;
      Decimal? amount12 = tran.Amount;
      drRevenueBalance14.TranEndProjected = tranEndProjected.HasValue & amount12.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount12.GetValueOrDefault()) : new Decimal?();
    }
  }

  private void InitExpenseBalance(DRScheduleTran tran, DRScheduleDetail sd)
  {
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(sd, tran.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(sd, tran.TranPeriodID);
    if (ScheduleMaint.IsReversed(sd))
    {
      DRExpenseBalance drExpenseBalance3 = drExpenseBalance1;
      Decimal? ptdDeferred = drExpenseBalance3.PTDDeferred;
      Decimal? amount1 = tran.Amount;
      drExpenseBalance3.PTDDeferred = ptdDeferred.HasValue & amount1.HasValue ? new Decimal?(ptdDeferred.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance4 = drExpenseBalance1;
      Decimal? endBalance = drExpenseBalance4.EndBalance;
      Decimal? amount2 = tran.Amount;
      drExpenseBalance4.EndBalance = endBalance.HasValue & amount2.HasValue ? new Decimal?(endBalance.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance5 = drExpenseBalance1;
      Decimal? endProjected = drExpenseBalance5.EndProjected;
      Decimal? amount3 = tran.Amount;
      drExpenseBalance5.EndProjected = endProjected.HasValue & amount3.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance6 = drExpenseBalance2;
      Decimal? tranPtdDeferred = drExpenseBalance6.TranPTDDeferred;
      Decimal? amount4 = tran.Amount;
      drExpenseBalance6.TranPTDDeferred = tranPtdDeferred.HasValue & amount4.HasValue ? new Decimal?(tranPtdDeferred.GetValueOrDefault() - amount4.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance7 = drExpenseBalance2;
      Decimal? tranEndBalance = drExpenseBalance7.TranEndBalance;
      Decimal? amount5 = tran.Amount;
      drExpenseBalance7.TranEndBalance = tranEndBalance.HasValue & amount5.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() - amount5.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance8 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance8.TranEndProjected;
      Decimal? amount6 = tran.Amount;
      drExpenseBalance8.TranEndProjected = tranEndProjected.HasValue & amount6.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRExpenseBalance drExpenseBalance9 = drExpenseBalance1;
      Decimal? ptdDeferred = drExpenseBalance9.PTDDeferred;
      Decimal? amount7 = tran.Amount;
      drExpenseBalance9.PTDDeferred = ptdDeferred.HasValue & amount7.HasValue ? new Decimal?(ptdDeferred.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance10 = drExpenseBalance1;
      Decimal? endBalance = drExpenseBalance10.EndBalance;
      Decimal? amount8 = tran.Amount;
      drExpenseBalance10.EndBalance = endBalance.HasValue & amount8.HasValue ? new Decimal?(endBalance.GetValueOrDefault() + amount8.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance11 = drExpenseBalance1;
      Decimal? endProjected = drExpenseBalance11.EndProjected;
      Decimal? amount9 = tran.Amount;
      drExpenseBalance11.EndProjected = endProjected.HasValue & amount9.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount9.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance12 = drExpenseBalance2;
      Decimal? tranPtdDeferred = drExpenseBalance12.TranPTDDeferred;
      Decimal? amount10 = tran.Amount;
      drExpenseBalance12.TranPTDDeferred = tranPtdDeferred.HasValue & amount10.HasValue ? new Decimal?(tranPtdDeferred.GetValueOrDefault() + amount10.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance13 = drExpenseBalance2;
      Decimal? tranEndBalance = drExpenseBalance13.TranEndBalance;
      Decimal? amount11 = tran.Amount;
      drExpenseBalance13.TranEndBalance = tranEndBalance.HasValue & amount11.HasValue ? new Decimal?(tranEndBalance.GetValueOrDefault() + amount11.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance14 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance14.TranEndProjected;
      Decimal? amount12 = tran.Amount;
      drExpenseBalance14.TranEndProjected = tranEndProjected.HasValue & amount12.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount12.GetValueOrDefault()) : new Decimal?();
    }
  }

  private static bool IsReversed(DRScheduleDetail sd) => sd.DocType == "CRM" || sd.DocType == "ADR";

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

  private void UpdateRevenueBalanceProjection(DRScheduleTran tran, DRScheduleDetail sd)
  {
    DRRevenueBalance drRevenueBalance1 = this.CreateDRRevenueBalance(sd, tran.FinPeriodID);
    DRRevenueBalance drRevenueBalance2 = this.CreateDRRevenueBalance(sd, tran.TranPeriodID);
    if (ScheduleMaint.IsReversed(sd))
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

  private void UpdateExpenseBalanceProjection(DRScheduleTran tran, DRScheduleDetail sd)
  {
    DRExpenseBalance drExpenseBalance1 = this.CreateDRExpenseBalance(sd, tran.FinPeriodID);
    DRExpenseBalance drExpenseBalance2 = this.CreateDRExpenseBalance(sd, tran.TranPeriodID);
    if (ScheduleMaint.IsReversed(sd))
    {
      DRExpenseBalance drExpenseBalance3 = drExpenseBalance1;
      Decimal? ptdProjected = drExpenseBalance3.PTDProjected;
      Decimal? amount1 = tran.Amount;
      drExpenseBalance3.PTDProjected = ptdProjected.HasValue & amount1.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance4 = drExpenseBalance1;
      Decimal? endProjected = drExpenseBalance4.EndProjected;
      Decimal? amount2 = tran.Amount;
      drExpenseBalance4.EndProjected = endProjected.HasValue & amount2.HasValue ? new Decimal?(endProjected.GetValueOrDefault() + amount2.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance5 = drExpenseBalance2;
      Decimal? tranPtdProjected = drExpenseBalance5.TranPTDProjected;
      Decimal? amount3 = tran.Amount;
      drExpenseBalance5.TranPTDProjected = tranPtdProjected.HasValue & amount3.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() - amount3.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance6 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance6.TranEndProjected;
      Decimal? amount4 = tran.Amount;
      drExpenseBalance6.TranEndProjected = tranEndProjected.HasValue & amount4.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() + amount4.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRExpenseBalance drExpenseBalance7 = drExpenseBalance1;
      Decimal? ptdProjected = drExpenseBalance7.PTDProjected;
      Decimal? amount5 = tran.Amount;
      drExpenseBalance7.PTDProjected = ptdProjected.HasValue & amount5.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() + amount5.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance8 = drExpenseBalance1;
      Decimal? endProjected = drExpenseBalance8.EndProjected;
      Decimal? amount6 = tran.Amount;
      drExpenseBalance8.EndProjected = endProjected.HasValue & amount6.HasValue ? new Decimal?(endProjected.GetValueOrDefault() - amount6.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance9 = drExpenseBalance2;
      Decimal? tranPtdProjected = drExpenseBalance9.TranPTDProjected;
      Decimal? amount7 = tran.Amount;
      drExpenseBalance9.TranPTDProjected = tranPtdProjected.HasValue & amount7.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + amount7.GetValueOrDefault()) : new Decimal?();
      DRExpenseBalance drExpenseBalance10 = drExpenseBalance2;
      Decimal? tranEndProjected = drExpenseBalance10.TranEndProjected;
      Decimal? amount8 = tran.Amount;
      drExpenseBalance10.TranEndProjected = tranEndProjected.HasValue & amount8.HasValue ? new Decimal?(tranEndProjected.GetValueOrDefault() - amount8.GetValueOrDefault()) : new Decimal?();
    }
  }

  private void UpdateRevenueProjection(DRScheduleTran tran, DRScheduleDetail sd)
  {
    DRRevenueProjectionAccum revenueProjectionAccum1 = this.CreateDRRevenueProjectionAccum(sd, tran.FinPeriodID);
    DRRevenueProjectionAccum revenueProjectionAccum2 = this.CreateDRRevenueProjectionAccum(sd, tran.TranPeriodID);
    if (ScheduleMaint.IsReversed(sd))
    {
      DRRevenueProjectionAccum revenueProjectionAccum3 = revenueProjectionAccum1;
      Decimal? ptdProjected = revenueProjectionAccum3.PTDProjected;
      Decimal? amount1 = tran.Amount;
      revenueProjectionAccum3.PTDProjected = ptdProjected.HasValue & amount1.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRRevenueProjectionAccum revenueProjectionAccum4 = revenueProjectionAccum2;
      Decimal? tranPtdProjected = revenueProjectionAccum4.TranPTDProjected;
      Decimal? amount2 = tran.Amount;
      revenueProjectionAccum4.TranPTDProjected = tranPtdProjected.HasValue & amount2.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRRevenueProjectionAccum revenueProjectionAccum5 = revenueProjectionAccum1;
      Decimal? ptdProjected = revenueProjectionAccum5.PTDProjected;
      Decimal? amount3 = tran.Amount;
      revenueProjectionAccum5.PTDProjected = ptdProjected.HasValue & amount3.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() + amount3.GetValueOrDefault()) : new Decimal?();
      DRRevenueProjectionAccum revenueProjectionAccum6 = revenueProjectionAccum2;
      Decimal? tranPtdProjected = revenueProjectionAccum6.TranPTDProjected;
      Decimal? amount4 = tran.Amount;
      revenueProjectionAccum6.TranPTDProjected = tranPtdProjected.HasValue & amount4.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + amount4.GetValueOrDefault()) : new Decimal?();
    }
  }

  private void UpdateExpenseProjection(DRScheduleTran tran, DRScheduleDetail sd)
  {
    DRExpenseProjectionAccum expenseProjectionAccum1 = this.CreateDRExpenseProjectionAccum(sd, tran.FinPeriodID);
    DRExpenseProjectionAccum expenseProjectionAccum2 = this.CreateDRExpenseProjectionAccum(sd, tran.TranPeriodID);
    if (ScheduleMaint.IsReversed(sd))
    {
      DRExpenseProjectionAccum expenseProjectionAccum3 = expenseProjectionAccum1;
      Decimal? ptdProjected = expenseProjectionAccum3.PTDProjected;
      Decimal? amount1 = tran.Amount;
      expenseProjectionAccum3.PTDProjected = ptdProjected.HasValue & amount1.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() - amount1.GetValueOrDefault()) : new Decimal?();
      DRExpenseProjectionAccum expenseProjectionAccum4 = expenseProjectionAccum2;
      Decimal? tranPtdProjected = expenseProjectionAccum4.TranPTDProjected;
      Decimal? amount2 = tran.Amount;
      expenseProjectionAccum4.TranPTDProjected = tranPtdProjected.HasValue & amount2.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() - amount2.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      DRExpenseProjectionAccum expenseProjectionAccum5 = expenseProjectionAccum1;
      Decimal? ptdProjected = expenseProjectionAccum5.PTDProjected;
      Decimal? amount3 = tran.Amount;
      expenseProjectionAccum5.PTDProjected = ptdProjected.HasValue & amount3.HasValue ? new Decimal?(ptdProjected.GetValueOrDefault() + amount3.GetValueOrDefault()) : new Decimal?();
      DRExpenseProjectionAccum expenseProjectionAccum6 = expenseProjectionAccum2;
      Decimal? tranPtdProjected = expenseProjectionAccum6.TranPTDProjected;
      Decimal? amount4 = tran.Amount;
      expenseProjectionAccum6.TranPTDProjected = tranPtdProjected.HasValue & amount4.HasValue ? new Decimal?(tranPtdProjected.GetValueOrDefault() + amount4.GetValueOrDefault()) : new Decimal?();
    }
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

  protected virtual TransactionsGenerator GetTransactionsGenerator(DRDeferredCode deferralCode)
  {
    return new TransactionsGenerator((PXGraph) this, deferralCode);
  }

  [Serializable]
  public class DRScheduleDetailEx : DRScheduleDetail
  {
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Schedule ID")]
    public override int? ScheduleID
    {
      get => this._ScheduleID;
      set => this._ScheduleID = value;
    }

    [PXDBInt(IsKey = true)]
    [PXUIField]
    [PXSelector(typeof (Search2<DRScheduleDetail.componentID, LeftJoin<PX.Objects.IN.InventoryItem, On<DRScheduleDetail.componentID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<DRScheduleDetail.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>), new System.Type[] {typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr)})]
    public override int? ComponentID
    {
      get => this._ComponentID;
      set => this._ComponentID = value;
    }

    public new abstract class scheduleID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ScheduleMaint.DRScheduleDetailEx.scheduleID>
    {
    }

    public new abstract class componentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ScheduleMaint.DRScheduleDetailEx.componentID>
    {
    }
  }
}
