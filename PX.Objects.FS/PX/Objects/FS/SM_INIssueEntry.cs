// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_INIssueEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_INIssueEntry : FSPostingBase<INIssueEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  protected virtual void _(PX.Data.Events.RowPersisting<INRegister> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    this.ValidatePostBatchStatus(e.Operation, "IN", e.Row.DocType, e.Row.RefNbr);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<INRegister> e)
  {
    if (e.TranStatus != null || e.Operation != 3)
      return;
    FSAllocationProcess.ReallocateServiceOrderSplits(FSAllocationProcess.GetRequiredAllocationList((PXGraph) this.Base, (object) e.Row));
    this.CleanPostingInfoLinkedToDoc((object) e.Row);
  }

  public override List<MessageHelper.ErrorInfo> GetErrorInfo()
  {
    return MessageHelper.GetErrorInfo<INTran>(((PXSelectBase) this.Base.issue).Cache, (IBqlTable) ((PXSelectBase<INRegister>) this.Base.issue).Current, (PXSelectBase<INTran>) this.Base.transactions, typeof (INTran));
  }

  public virtual string GetLineDisplayHint(
    PXGraph graph,
    string lineRefNbr,
    string lineDescr,
    int? inventoryID)
  {
    return MessageHelper.GetLineDisplayHint(graph, lineRefNbr, lineDescr, inventoryID);
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
    SM_INIssueEntry.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new SM_INIssueEntry.\u003C\u003Ec__DisplayClass5_0();
    if (docLines.Count == 0)
      return;
    bool? nullable1 = new bool?(false);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.fsServiceOrderRow = docLines[0].fsServiceOrder;
    FSSrvOrdType fsSrvOrdType1 = docLines[0].fsSrvOrdType;
    FSPostDoc fsPostDoc1 = docLines[0].fsPostDoc;
    FSAppointment fsAppointment1 = docLines[0].fsAppointment;
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass50, __methodptr(\u003CCreateInvoice\u003Eb__0));
    try
    {
      ((PXGraph) this.Base).FieldDefaulting.AddHandler<INRegister.branchID>(pxFieldDefaulting);
      INRegister inRegister = new INRegister();
      inRegister.DocType = "I";
      this.CheckAutoNumbering(((PXSelectBase<INSetup>) this.Base.insetup).SelectSingle(Array.Empty<object>()).IssueNumberingID);
      inRegister.TranDate = invoiceDate;
      inRegister.FinPeriodID = invoiceFinPeriodID;
      // ISSUE: reference to a compiler-generated field
      inRegister.TranDesc = fsAppointment1 != null ? fsAppointment1.DocDesc : cDisplayClass50.fsServiceOrderRow.DocDesc;
      INRegister copy1 = PXCache<INRegister>.CreateCopy(((PXSelectBase<INRegister>) this.Base.issue).Insert(inRegister));
      bool? hold = copy1.Hold;
      ((PXSelectBase) this.Base.issue).Cache.SetValueExtIfDifferent<INRegister.hold>((object) copy1, (object) true);
      INRegister row1 = ((PXSelectBase<INRegister>) this.Base.issue).Update(copy1);
      if (onDocumentHeaderInserted != null)
        onDocumentHeaderInserted((PXGraph) this.Base, (IBqlTable) row1);
      PMTask pmTask1 = (PMTask) null;
      List<DocLineExt> list = docLines.Where<DocLineExt>((Func<DocLineExt, bool>) (x => x.docLine.LineType == "SLPRO" && x.docLine.InventoryID.HasValue)).ToList<DocLineExt>();
      foreach (DocLineExt docLineExt in list)
      {
        IDocLine docLine = docLineExt.docLine;
        FSSODet fsSoDet1 = docLineExt.fsSODet;
        FSAppointmentDet fsAppointmentDet1 = docLineExt.fsAppointmentDet;
        FSPostDoc fsPostDoc2 = docLineExt.fsPostDoc;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass50.fsServiceOrderRow = docLineExt.fsServiceOrder;
        FSSrvOrdType fsSrvOrdType2 = docLineExt.fsSrvOrdType;
        FSAppointment fsAppointment2 = docLineExt.fsAppointment;
        pmTask1 = docLineExt.pmTask;
        FSAppointmentDet fsAppointmentDet2 = docLineExt.fsAppointmentDet;
        FSSODet fsSoDet2 = docLineExt.fsSODet;
        INTran copy2 = PXCache<INTran>.CreateCopy(((PXSelectBase<INTran>) this.Base.transactions).Insert(new INTran()
        {
          BranchID = docLine.BranchID,
          TranType = "III"
        }));
        copy2.InventoryID = docLine.InventoryID;
        copy2.UOM = docLine.UOM;
        PMTask pmTask2 = docLineExt.pmTask;
        if (pmTask2 != null && pmTask2.Status == "F")
        {
          // ISSUE: reference to a compiler-generated field
          throw new PXException("The {1} line of the {0} document cannot be processed because the {2} project task has already been completed.", new object[3]
          {
            (object) cDisplayClass50.fsServiceOrderRow.RefNbr,
            (object) this.GetLineDisplayHint((PXGraph) this.Base, docLine.LineRef, docLine.TranDesc, docLine.InventoryID),
            (object) pmTask2.TaskCD
          });
        }
        if (docLine.ProjectID.HasValue && docLine.ProjectTaskID.HasValue)
        {
          copy2.ProjectID = docLine.ProjectID;
          copy2.TaskID = docLine.ProjectTaskID;
        }
        copy2.SiteID = docLine.SiteID;
        copy2.LocationID = docLine.SiteLocationID;
        copy2.CostCenterID = docLine.CostCenterID;
        copy2.TranDesc = docLine.TranDesc;
        copy2.CostCodeID = docLine.CostCodeID;
        copy2.ReasonCode = fsSrvOrdType2.ReasonCode;
        this.Base.CostCenterDispatcherExt?.SetInventorySource(copy2);
        INTran copy3 = PXCache<INTran>.CreateCopy(((PXSelectBase<INTran>) this.Base.transactions).Update(copy2));
        int num1 = SharedFunctions.IsLotSerialRequired(((PXSelectBase) this.Base.transactions).Cache, copy3.InventoryID) ? 1 : 0;
        bool flag1 = false;
        bool? nullable2;
        if (num1 != 0)
        {
          INTranSplit inTranSplit1 = PXResultset<INTranSplit>.op_Implicit(((PXSelectBase<INTranSplit>) this.Base.splits).Select(Array.Empty<object>()));
          if (inTranSplit1 != null)
            ((PXSelectBase<INTranSplit>) this.Base.splits).Delete(inTranSplit1);
          if (fsAppointment2 == null)
          {
            foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSSODetSplit.lineNbr>>>>>, OrderBy<Asc<FSSODetSplit.splitLineNbr>>>.Config>.Select((PXGraph) this.Base, new object[3]
            {
              (object) fsSoDet1.SrvOrdType,
              (object) fsSoDet1.RefNbr,
              (object) fsSoDet1.LineNbr
            }))
            {
              FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
              nullable2 = fssoDetSplit.Completed;
              bool flag2 = false;
              if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue && !string.IsNullOrEmpty(fssoDetSplit.LotSerialNbr))
              {
                INTranSplit copy4 = (INTranSplit) ((PXSelectBase) this.Base.splits).Cache.CreateCopy((object) ((PXSelectBase<INTranSplit>) this.Base.splits).Insert(new INTranSplit()));
                INTranSplit inTranSplit2 = copy4;
                int? nullable3 = fssoDetSplit.SiteID;
                int? nullable4 = nullable3.HasValue ? fssoDetSplit.SiteID : copy4.SiteID;
                inTranSplit2.SiteID = nullable4;
                INTranSplit inTranSplit3 = copy4;
                nullable3 = fssoDetSplit.LocationID;
                int? nullable5 = nullable3.HasValue ? fssoDetSplit.LocationID : copy4.LocationID;
                inTranSplit3.LocationID = nullable5;
                copy4.LotSerialNbr = fssoDetSplit.LotSerialNbr;
                copy4.Qty = fssoDetSplit.Qty;
                ((PXSelectBase<INTranSplit>) this.Base.splits).Update(copy4);
                flag1 = true;
              }
            }
            copy3 = (INTran) ((PXSelectBase) this.Base.transactions).Cache.CreateCopy((object) ((PXSelectBase<INTran>) this.Base.transactions).Current);
          }
          else
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
                INTranSplit copy5 = (INTranSplit) ((PXSelectBase) this.Base.splits).Cache.CreateCopy((object) ((PXSelectBase<INTranSplit>) this.Base.splits).Insert(new INTranSplit()));
                INTranSplit inTranSplit4 = copy5;
                int? nullable6 = fsApptLineSplit.SiteID;
                int? nullable7 = nullable6.HasValue ? fsApptLineSplit.SiteID : copy5.SiteID;
                inTranSplit4.SiteID = nullable7;
                INTranSplit inTranSplit5 = copy5;
                nullable6 = fsApptLineSplit.LocationID;
                int? nullable8 = nullable6.HasValue ? fsApptLineSplit.LocationID : copy5.LocationID;
                inTranSplit5.LocationID = nullable8;
                copy5.LotSerialNbr = fsApptLineSplit.LotSerialNbr;
                copy5.Qty = fsApptLineSplit.Qty;
                ((PXSelectBase<INTranSplit>) this.Base.splits).Update(copy5);
                flag1 = true;
              }
            }
            copy3 = (INTran) ((PXSelectBase) this.Base.transactions).Cache.CreateCopy((object) ((PXSelectBase<INTran>) this.Base.transactions).Current);
          }
        }
        Decimal? nullable9;
        if (!flag1)
        {
          copy3.Qty = docLine.GetQty(FieldType.BillableField);
        }
        else
        {
          Decimal? qty = copy3.Qty;
          nullable9 = docLine.GetQty(FieldType.BillableField);
          if (!(qty.GetValueOrDefault() == nullable9.GetValueOrDefault() & qty.HasValue == nullable9.HasValue))
            throw new PXException("The quantity in the posted document does not match the quantity in the source document.");
        }
        INTran inTran1 = copy3;
        nullable2 = docLine.IsFree;
        bool flag3 = false;
        Decimal? nullable10;
        if (!(nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue))
        {
          nullable10 = new Decimal?(0M);
        }
        else
        {
          nullable9 = docLine.CuryUnitPrice;
          Decimal num2 = (Decimal) invtMult;
          nullable10 = nullable9.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * num2) : new Decimal?();
        }
        inTran1.UnitPrice = nullable10;
        INTran inTran2 = copy3;
        nullable9 = docLine.GetTranAmt(FieldType.BillableField);
        Decimal num3 = (Decimal) invtMult;
        Decimal? nullable11 = nullable9.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * num3) : new Decimal?();
        inTran2.TranAmt = nullable11;
        FSxINTran extension = ((PXSelectBase) this.Base.transactions).Cache.GetExtension<FSxINTran>((object) copy3);
        // ISSUE: reference to a compiler-generated field
        extension.SrvOrdType = cDisplayClass50.fsServiceOrderRow.SrvOrdType;
        // ISSUE: reference to a compiler-generated field
        extension.ServiceOrderRefNbr = cDisplayClass50.fsServiceOrderRow.RefNbr;
        extension.AppointmentRefNbr = fsAppointment2?.RefNbr;
        extension.ServiceOrderLineNbr = (int?) fsSoDet2?.LineNbr;
        extension.AppointmentLineNbr = (int?) fsAppointmentDet2?.LineNbr;
        SharedFunctions.CopyNotesAndFiles(((PXSelectBase) this.Base.transactions).Cache, (object) copy3, docLine, fsSrvOrdType2);
        INTran row2;
        fsPostDoc2.INDocLineRef = (object) (row2 = ((PXSelectBase<INTran>) this.Base.transactions).Update(copy3));
        if (onTransactionInserted != null)
          onTransactionInserted((PXGraph) this.Base, (IBqlTable) row2);
      }
      INRegister data = ((PXSelectBase<INRegister>) this.Base.issue).Update(row1);
      INSetup current = ((PXSelectBase<INSetup>) this.Base.insetup).Current;
      if ((current != null ? (current.RequireControlTotal.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        ((PXSelectBase) this.Base.issue).Cache.SetValueExtIfDifferent<INRegister.controlQty>((object) data, (object) data.TotalQty);
        ((PXSelectBase) this.Base.issue).Cache.SetValueExtIfDifferent<INRegister.controlAmount>((object) data, (object) data.TotalAmount);
      }
      if (!hold.GetValueOrDefault())
        ((PXSelectBase) this.Base.issue).Cache.SetValueExtIfDifferent<INRegister.hold>((object) data, (object) false);
      ((PXSelectBase<INRegister>) this.Base.issue).Update(data);
    }
    finally
    {
      ((PXGraph) this.Base).FieldDefaulting.RemoveHandler<INRegister.branchID>(pxFieldDefaulting);
    }
  }

  public override FSCreatedDoc PressSave(
    int batchID,
    List<DocLineExt> docLines,
    BeforeSaveDelegate beforeSave)
  {
    if (((PXSelectBase<INRegister>) this.Base.issue).Current == null)
      throw new SharedClasses.TransactionScopeException();
    if (beforeSave != null)
      beforeSave((PXGraph) this.Base);
    ((PXAction) this.Base.Save).Press();
    INRegister inRegister = ((PXSelectBase<INRegister>) this.Base.issue).Current != null ? ((PXSelectBase<INRegister>) this.Base.issue).Current : PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) this.Base.issue).Select(Array.Empty<object>()));
    string docType = ((PXSelectBase<INRegister>) this.Base.issue).Current.DocType;
    string refNbr = ((PXSelectBase<INRegister>) this.Base.issue).Current.RefNbr;
    return new FSCreatedDoc()
    {
      BatchID = new int?(batchID),
      PostTo = "IN",
      CreatedDocType = inRegister.DocType,
      CreatedRefNbr = inRegister.RefNbr
    };
  }

  public override void Clear() => ((PXGraph) this.Base).Clear((PXClearOption) 3);

  public override PXGraph GetGraph() => (PXGraph) this.Base;

  public override void DeleteDocument(FSCreatedDoc fsCreatedDocRow)
  {
    ((PXSelectBase<INRegister>) this.Base.issue).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) this.Base.issue).Search<INRegister.refNbr>((object) fsCreatedDocRow.CreatedRefNbr, Array.Empty<object>()));
    if (((PXSelectBase<INRegister>) this.Base.issue).Current == null || !(((PXSelectBase<INRegister>) this.Base.issue).Current.RefNbr == fsCreatedDocRow.CreatedRefNbr))
      return;
    ((PXAction) this.Base.Delete).Press();
  }

  public override void CleanPostInfo(PXGraph cleanerGraph, FSPostDet fsPostDetRow)
  {
    PXUpdate<Set<FSPostInfo.iNDocType, Null, Set<FSPostInfo.iNRefNbr, Null, Set<FSPostInfo.iNLineNbr, Null, Set<FSPostInfo.iNPosted, False>>>>, FSPostInfo, Where<FSPostInfo.postID, Equal<Required<FSPostInfo.postID>>, And<FSPostInfo.iNPosted, Equal<True>>>>.Update(cleanerGraph, new object[1]
    {
      (object) fsPostDetRow.PostID
    });
  }

  public virtual INRegister RevertInvoice()
  {
    if (((PXSelectBase<INRegister>) this.Base.issue).Current == null)
      return (INRegister) null;
    INRegister data = (INRegister) null;
    INRegister current = ((PXSelectBase<INRegister>) this.Base.issue).Current;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      INIssueEntry instance = PXGraph.CreateInstance<INIssueEntry>();
      data = new INRegister();
      data.DocType = "I";
      this.CheckAutoNumbering(((PXSelectBase<INSetup>) instance.insetup).SelectSingle(Array.Empty<object>()).IssueNumberingID);
      data.TranDate = current.TranDate;
      data.FinPeriodID = current.FinPeriodID;
      data.TranDesc = current.TranDesc;
      data = PXCache<INRegister>.CreateCopy(((PXSelectBase<INRegister>) instance.issue).Insert(data));
      data.TranDesc = PXMessages.LocalizeFormatNoPrefix("Reverse {0}", new object[1]
      {
        (object) current.TranDesc
      });
      ((PXSelectBase<INRegister>) instance.issue).Update(data);
      foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) this.Base.transactions).Select(Array.Empty<object>()))
      {
        INTran tran = PXResult<INTran>.op_Implicit(pxResult);
        ((PXSelectBase<INTran>) this.Base.transactions).Current = tran;
        List<INTranSplit> list = GraphHelper.RowCast<INTranSplit>((IEnumerable) ((PXSelectBase<INTranSplit>) this.Base.splits).Select(Array.Empty<object>())).ToList<INTranSplit>();
        INTran inTran = this.ReverseTran(tran);
        if (SharedFunctions.IsLotSerialRequired(((PXSelectBase) this.Base.transactions).Cache, PXCache<INTran>.CreateCopy(((PXSelectBase<INTran>) instance.transactions).Insert(inTran)).InventoryID))
        {
          foreach (INTranSplit split in list.Where<INTranSplit>((Func<INTranSplit, bool>) (x =>
          {
            if (!(x.DocType == tran.DocType) || !(x.RefNbr == tran.RefNbr))
              return false;
            int? lineNbr1 = x.LineNbr;
            int? lineNbr2 = tran.LineNbr;
            return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
          })))
          {
            ((PXSelectBase<INTranSplit>) instance.splits).Insert(this.ReverseTranSplit(split));
            Decimal? qty1 = split.Qty;
            Decimal? qty2 = ((PXSelectBase<INTranSplit>) instance.splits).Current.Qty;
            if (!(qty1.GetValueOrDefault() == qty2.GetValueOrDefault() & qty1.HasValue == qty2.HasValue))
              throw new PXException("The quantity in the posted document does not match the quantity in the source document.");
          }
        }
      }
      ((PXAction) instance.Save).Press();
      data = ((PXSelectBase<INRegister>) instance.issue).Current;
      if (data.Hold.GetValueOrDefault())
      {
        ((PXSelectBase) instance.issue).Cache.SetValueExtIfDifferent<INRegister.hold>((object) data, (object) false);
        data = ((PXSelectBase<INRegister>) instance.issue).Update(data);
      }
      ((PXAction) instance.release).Press();
      if (((PXSelectBase<INRegister>) instance.issue).Current.Status != "R")
        throw new PXInvalidOperationException();
      data = ((PXSelectBase<INRegister>) instance.issue).Current;
      transactionScope.Complete();
    }
    return data;
  }

  public virtual INTran ReverseTran(INTran tran)
  {
    INTran copy = PXCache<INTran>.CreateCopy(tran);
    copy.OrigDocType = copy.DocType;
    copy.OrigTranType = copy.TranType;
    copy.OrigRefNbr = copy.RefNbr;
    copy.OrigLineNbr = copy.LineNbr;
    copy.TranType = "CRM";
    copy.RefNbr = (string) null;
    copy.LineNbr = new int?();
    copy.InvtMult = new short?((short) 1);
    copy.LotSerialNbr = "";
    copy.ARDocType = (string) null;
    copy.ARRefNbr = (string) null;
    copy.ARLineNbr = new int?();
    copy.NoteID = new Guid?();
    return copy;
  }

  public virtual INTranSplit ReverseTranSplit(INTranSplit split)
  {
    INTranSplit copy = PXCache<INTranSplit>.CreateCopy(split);
    copy.TranType = (string) null;
    copy.DocType = (string) null;
    copy.RefNbr = (string) null;
    copy.LineNbr = new int?();
    copy.SplitLineNbr = new int?();
    copy.InvtMult = new short?((short) 1);
    return copy;
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<INRegister>((PXGraph) this.Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }
}
