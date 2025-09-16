// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_RegisterEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_RegisterEntry : FSPostingBase<RegisterEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Initialize() => ((PXGraphExtension) this).Initialize();

  [PXOverride]
  public virtual void ReleaseDocument(
    PMRegister doc,
    SM_RegisterEntry.ReleaseDocumentDelegate baseMethod)
  {
    if (!SharedFunctions.isFSSetupSet((PXGraph) this.Base))
    {
      baseMethod(doc);
    }
    else
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.UpdateCosts(doc);
        baseMethod(doc);
        transactionScope.Complete();
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMRegister> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    this.ValidatePostBatchStatus(e.Operation, "PM", e.Row.Module, e.Row.RefNbr);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PMRegister> e)
  {
    if (e.Operation != 3 || e.TranStatus != null)
      return;
    this.CleanPostingInfoLinkedToDoc((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<EPActivityApprove> e)
  {
    if (e.Row == null || !TimeCardHelper.IsTheTimeCardIntegrationEnabled((PXGraph) this.Base) || !e.Row.Released.GetValueOrDefault() || (bool) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPActivityApprove>>) e).Cache.GetValueOriginal<PMTimeActivity.released>((object) e.Row) || e.TranStatus != null)
      return;
    this.UpdateAppointmentApprovedTime(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPActivityApprove>>) e).Cache, (PMTimeActivity) e.Row);
  }

  public virtual void UpdateCosts(PMRegister pmRegisterRow)
  {
    if (pmRegisterRow == null)
      return;
    PXResultset<FSPostDet> source = PXSelectBase<FSPostDet, PXSelectJoin<FSPostDet, InnerJoin<PMTran, On<FSPostDet.pMPosted, Equal<True>, And<PMTran.tranType, Equal<FSPostDet.pMDocType>, And<PMTran.refNbr, Equal<FSPostDet.pMRefNbr>, And<PMTran.tranID, Equal<FSPostDet.pMTranID>>>>>, LeftJoin<FSSODet, On<FSSODet.postID, Equal<FSPostDet.postID>, And<FSSODet.lineType, Equal<FSLineType.Inventory_Item>>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>, And<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>>>, LeftJoin<FSSrvOrdType, On<Where2<Where<FSSODet.sODetID, IsNotNull, And<FSSrvOrdType.srvOrdType, Equal<FSSODet.srvOrdType>>>, Or<Where<FSAppointmentDet.appDetID, IsNotNull, And<FSSrvOrdType.srvOrdType, Equal<FSAppointmentDet.srvOrdType>>>>>>>>>>, Where<PMTran.tranType, Equal<Required<PMTran.tranType>>, And<PMTran.refNbr, Equal<Required<PMTran.refNbr>>, And<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>, And<FSSrvOrdType.billingType, Equal<ListField_SrvOrdType_BillingType.CostAsCost>>>>>, OrderBy<Asc<PMTran.tranType, Asc<PMTran.refNbr>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) pmRegisterRow.Module,
      (object) pmRegisterRow.RefNbr
    });
    if (((IQueryable<PXResult<FSPostDet>>) source).Count<PXResult<FSPostDet>>() <= 0)
      return;
    RegisterEntry registerEntry = this.Base;
    foreach (PXResult<FSPostDet, PMTran, FSSODet, FSAppointmentDet, FSSrvOrdType> pxResult in source)
    {
      PMTran pmTran1 = PXResult<FSPostDet, PMTran, FSSODet, FSAppointmentDet, FSSrvOrdType>.op_Implicit(pxResult);
      FSPostDet fsPostDet = PXResult<FSPostDet, PMTran, FSSODet, FSAppointmentDet, FSSrvOrdType>.op_Implicit(pxResult);
      if (((PXSelectBase<PMRegister>) registerEntry.Document).Current == null || ((PXSelectBase<PMRegister>) registerEntry.Document).Current.Module != pmTran1.TranType || ((PXSelectBase<PMRegister>) registerEntry.Document).Current.RefNbr != pmTran1.RefNbr)
        throw new PXInvalidOperationException();
      INTran inTran = PXResult<FSPostDet, INTran>.op_Implicit((PXResult<FSPostDet, INTran>) PXResultset<FSPostDet>.op_Implicit(PXSelectBase<FSPostDet, PXSelectJoin<FSPostDet, InnerJoin<INTran, On<FSPostDet.iNPosted, Equal<True>, And<INTran.docType, Equal<FSPostDet.iNDocType>, And<INTran.refNbr, Equal<FSPostDet.iNRefNbr>, And<INTran.lineNbr, Equal<FSPostDet.iNLineNbr>, And<INTran.released, Equal<True>>>>>>>, Where<FSPostDet.postID, Equal<Required<FSPostDet.postID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) fsPostDet.PostID
      })));
      PMTran pmTran2 = (PMTran) PrimaryKeyOf<PMTran>.By<PMTran.tranID>.Find((PXGraph) this.Base, (PMTran.tranID) pmTran1, (PKFindOptions) 0);
      if (inTran != null && pmTran2 != null)
      {
        Decimal d = ((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(pmTran2.BaseCuryInfoID).CuryConvCuryRaw(inTran.UnitCost.Value);
        pmTran2.TranCuryUnitRate = new Decimal?(Math.Round(d, CommonSetupDecPl.PrcCst, MidpointRounding.AwayFromZero));
        ((PXSelectBase<PMTran>) registerEntry.Transactions).Update(pmTran2);
      }
    }
    if (((PXSelectBase<PMRegister>) registerEntry.Document).Current == null || !((PXGraph) registerEntry).IsDirty)
      return;
    ((PXAction) registerEntry.Save).Press();
  }

  public virtual void UpdateAppointmentApprovedTime(PXCache cache, PMTimeActivity timeActivity)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
      return;
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    FSxPMTimeActivity extension = cache.GetExtension<FSxPMTimeActivity>((object) timeActivity);
    if (extension == null || !extension.LogLineNbr.HasValue)
      return;
    FSAppointmentLog fsAppointmentLog = PXResultset<FSAppointmentLog>.op_Implicit(PXSelectBase<FSAppointmentLog, PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.lineNbr, Equal<Required<FSAppointmentLog.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) extension.AppointmentID,
      (object) extension.LogLineNbr
    }));
    if (fsAppointmentLog == null)
      return;
    instance.SkipTimeCardUpdate = true;
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) fsAppointmentLog.DocID, new object[1]
    {
      (object) fsAppointmentLog.DocType
    }));
    fsAppointmentLog.ApprovedTime = new bool?(true);
    ((PXSelectBase<FSAppointmentLog>) instance.LogRecords).Update(fsAppointmentLog);
    ((PXAction) instance.Save).Press();
  }

  public override List<MessageHelper.ErrorInfo> GetErrorInfo()
  {
    return MessageHelper.GetErrorInfo<PMTran>(((PXSelectBase) this.Base.Document).Cache, (IBqlTable) ((PXSelectBase<PMRegister>) this.Base.Document).Current, (PXSelectBase<PMTran>) this.Base.Transactions, typeof (PMTran));
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
    if (docLines.Count == 0)
      return;
    FSServiceOrder fsServiceOrder1 = docLines[0].fsServiceOrder;
    FSSrvOrdType fsSrvOrdType1 = docLines[0].fsSrvOrdType;
    FSPostDoc fsPostDoc1 = docLines[0].fsPostDoc;
    FSAppointment fsAppointment1 = docLines[0].fsAppointment;
    FSAppointmentDet fsAppointmentDetRow = docLines[0].fsAppointmentDet;
    PMRegister pmRegister = new PMRegister();
    IEnumerable<FSLog> fsLogs;
    if (fsAppointment1 == null)
      fsLogs = Enumerable.Empty<FSLog>();
    else
      fsLogs = GraphHelper.RowCast<FSLog>((IEnumerable) PXSelectBase<FSLog, PXSelect<FSLog, Where<FSLog.docID, Equal<Required<FSAppointment.appointmentID>>>, OrderBy<Asc<FSLog.dateTimeBegin>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) fsAppointment1.AppointmentID
      }));
    IEnumerable<FSLog> source1 = fsLogs;
    pmRegister.Module = "PM";
    pmRegister.Date = invoiceDate;
    pmRegister.Description = fsAppointment1 != null ? fsAppointment1.DocDesc : fsServiceOrder1.DocDesc;
    pmRegister.Status = "B";
    pmRegister.OrigDocType = fsAppointment1 != null ? "SO" : "AP";
    pmRegister.OrigNoteID = fsAppointment1 != null ? fsAppointment1.NoteID : fsServiceOrder1.NoteID;
    PXCache<PMRegister>.CreateCopy(((PXSelectBase<PMRegister>) this.Base.Document).Insert(pmRegister));
    List<\u003C\u003Ef__AnonymousType11<string, DocLineExt>> list = docLines.Select(l => new
    {
      LineType = l.fsAppointmentDet?.LineType ?? l.fsSODet.LineType,
      Line = l
    }).Where(l => EnumerableExtensions.IsIn<string>(l.LineType, "SERVI", "NSTKI", "SLPRO")).ToList();
    if (list.Count == 0)
      return;
    PMTran pmTran1 = (PMTran) null;
    foreach (var data in list)
    {
      DocLineExt line = data.Line;
      IDocLine docLine = line.docLine;
      FSPostDoc fsPostDoc2 = line.fsPostDoc;
      FSServiceOrder fsServiceOrder2 = line.fsServiceOrder;
      FSSrvOrdType fsSrvOrdType2 = line.fsSrvOrdType;
      FSAppointment fsAppointment2 = line.fsAppointment;
      fsAppointmentDetRow = line.fsAppointmentDet;
      PMTran pmTran2 = new PMTran();
      if (fsAppointmentDetRow != null && fsAppointmentDetRow.LineType == "SERVI")
      {
        IEnumerable<FSLog> source2 = source1.Where<FSLog>((Func<FSLog, bool>) (x => x.DetLineRef == fsAppointmentDetRow.LineRef));
        pmTran2.Date = source2.Any<FSLog>() ? source2.First<FSLog>().DateTimeBegin : fsAppointment2.ActualDateTimeBegin;
      }
      else
        pmTran2.Date = invoiceDate;
      pmTran2.BranchID = docLine.BranchID;
      PMTran copy = PXCache<PMTran>.CreateCopy(((PXSelectBase<PMTran>) this.Base.Transactions).Insert(pmTran2));
      PMTask pmTask = line.pmTask;
      if (pmTask != null && pmTask.Status == "F")
        throw new PXException("The {1} line of the {0} document cannot be processed because the {2} project task has already been completed.", new object[3]
        {
          (object) fsServiceOrder2.RefNbr,
          (object) this.GetLineDisplayHint((PXGraph) this.Base, docLine.LineRef, docLine.TranDesc, docLine.InventoryID),
          (object) pmTask.TaskCD
        });
      int? nullable1 = docLine.ProjectID;
      if (nullable1.HasValue)
        copy.ProjectID = docLine.ProjectID;
      nullable1 = docLine.ProjectID;
      if (nullable1.HasValue)
      {
        nullable1 = docLine.ProjectTaskID;
        if (nullable1.HasValue)
          copy.TaskID = docLine.ProjectTaskID;
      }
      bool flag1 = data.LineType == "SLPRO";
      bool flag2 = data.LineType == "SERVI";
      copy.TranCuryID = fsAppointment2 != null ? fsAppointment2.CuryID : fsServiceOrder2.CuryID;
      copy.UOM = docLine.UOM;
      copy.BAccountID = fsServiceOrder2.BillCustomerID;
      copy.Billable = new bool?(!flag1);
      copy.InventoryID = docLine.InventoryID;
      copy.CostCodeID = docLine.CostCodeID;
      copy.LocationID = fsServiceOrder2.BillLocationID;
      copy.Qty = flag1 ? new Decimal?(0.0M) : docLine.GetQty(FieldType.BillableField);
      copy.FinPeriodID = invoiceFinPeriodID;
      PMTran pmTran3 = copy;
      bool? isFree = docLine.IsFree;
      bool flag3 = false;
      Decimal? nullable2;
      Decimal? nullable3;
      if (!(isFree.GetValueOrDefault() == flag3 & isFree.HasValue))
      {
        nullable3 = new Decimal?(0M);
      }
      else
      {
        nullable2 = docLine.CuryUnitPrice;
        Decimal num = (Decimal) invtMult;
        nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num) : new Decimal?();
      }
      pmTran3.TranCuryUnitRate = nullable3;
      PMTran pmTran4 = copy;
      Decimal? nullable4;
      if (!flag1)
      {
        nullable2 = docLine.GetTranAmt(FieldType.BillableField);
        Decimal num = (Decimal) invtMult;
        nullable4 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num) : new Decimal?();
      }
      else
        nullable4 = new Decimal?(0.0M);
      pmTran4.TranCuryAmount = nullable4;
      copy.AccountGroupID = fsSrvOrdType2.AccountGroupID;
      copy.Description = flag1 ? "Service billing of stock. For related costs, see the respective inventory issue." : docLine.TranDesc;
      FSAppointmentDet fsAppointmentDet = fsAppointmentDetRow;
      int num1;
      if (fsAppointmentDet == null)
      {
        num1 = 0;
      }
      else
      {
        nullable1 = fsAppointmentDet.StaffID;
        num1 = nullable1.HasValue ? 1 : 0;
      }
      bool flag4 = num1 != 0;
      bool flag5 = fsAppointment2 != null && ProjectDefaultAttribute.IsProject((PXGraph) this.Base, fsAppointment2.ProjectID);
      bool flag6 = true;
      if (flag5 & flag2 & flag4)
        flag6 = this.VerifyEmployeeRestriction(fsAppointment2.ProjectID, fsAppointmentDetRow.StaffID);
      if (flag2 & flag4 && !flag5 | flag6)
        copy.ResourceID = fsAppointmentDetRow.StaffID;
      fsPostDoc2.DocLineRef = (object) (pmTran1 = ((PXSelectBase<PMTran>) this.Base.Transactions).Update(copy));
    }
  }

  public override FSCreatedDoc PressSave(
    int batchID,
    List<DocLineExt> docLines,
    BeforeSaveDelegate beforeSave)
  {
    if (((PXSelectBase<PMRegister>) this.Base.Document).Current == null)
      throw new SharedClasses.TransactionScopeException();
    if (beforeSave != null)
      beforeSave((PXGraph) this.Base);
    ((PXAction) this.Base.Save).Press();
    return new FSCreatedDoc()
    {
      BatchID = new int?(batchID),
      PostTo = "PM",
      CreatedDocType = ((PXSelectBase<PMRegister>) this.Base.Document).Current.Module,
      CreatedRefNbr = ((PXSelectBase<PMRegister>) this.Base.Document).Current.RefNbr
    };
  }

  public override void Clear() => ((PXGraph) this.Base).Clear((PXClearOption) 3);

  public override PXGraph GetGraph() => (PXGraph) this.Base;

  public override void DeleteDocument(FSCreatedDoc fsCreatedDocRow)
  {
    ((PXSelectBase<PMRegister>) this.Base.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) this.Base.Document).Search<PMRegister.refNbr>((object) fsCreatedDocRow.CreatedRefNbr, new object[1]
    {
      (object) fsCreatedDocRow.CreatedDocType
    }));
    if (((PXSelectBase<PMRegister>) this.Base.Document).Current == null || !(((PXSelectBase<PMRegister>) this.Base.Document).Current.RefNbr == fsCreatedDocRow.CreatedRefNbr) || !(((PXSelectBase<PMRegister>) this.Base.Document).Current.Module == fsCreatedDocRow.CreatedDocType))
      return;
    ((PXAction) this.Base.Delete).Press();
  }

  public override void CleanPostInfo(PXGraph cleanerGraph, FSPostDet fsPostDetRow)
  {
    PXUpdate<Set<FSPostInfo.pMDocType, Null, Set<FSPostInfo.pMRefNbr, Null, Set<FSPostInfo.pMTranID, Null, Set<FSPostInfo.pMPosted, False>>>>, FSPostInfo, Where<FSPostInfo.postID, Equal<Required<FSPostInfo.postID>>, And<FSPostInfo.pMPosted, Equal<True>>>>.Update(cleanerGraph, new object[1]
    {
      (object) fsPostDetRow.PostID
    });
  }

  public virtual PMRegister RevertInvoice()
  {
    if (((PXSelectBase<PMRegister>) this.Base.Document).Current == null)
      return (PMRegister) null;
    PMRegister data = (PMRegister) null;
    PMRegister current = ((PXSelectBase<PMRegister>) this.Base.Document).Current;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
      data = new PMRegister();
      data.Module = current.Module;
      data.Date = current.Date;
      data.Description = current.Description;
      data.OrigDocType = current.OrigDocType;
      data.OrigNoteID = current.OrigNoteID;
      PMRegister copy = (PMRegister) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) ((PXSelectBase<PMRegister>) instance.Document).Insert(data));
      copy.Description = PXMessages.LocalizeFormatNoPrefix("Reverse {0}", new object[1]
      {
        (object) current.Description
      });
      ((PXSelectBase<PMRegister>) instance.Document).Update(copy);
      foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) this.Base.Transactions).Select(Array.Empty<object>()))
      {
        PMTran pmTran = this.ReverseTran(PXResult<PMTran>.op_Implicit(pxResult));
        ((PXSelectBase<PMTran>) instance.Transactions).Insert(pmTran);
      }
      ((PXAction) instance.Save).Press();
      data = ((PXSelectBase<PMRegister>) instance.Document).Current;
      if (data.Hold.GetValueOrDefault())
      {
        ((PXSelectBase) instance.Document).Cache.SetValueExtIfDifferent<PMRegister.hold>((object) data, (object) false);
        data = ((PXSelectBase<PMRegister>) instance.Document).Update(data);
      }
      instance.ReleaseDocument(((PXSelectBase<PMRegister>) instance.Document).Current);
      if (((PXSelectBase<PMRegister>) instance.Document).Current.Status != "R")
        throw new PXInvalidOperationException();
      data = ((PXSelectBase<PMRegister>) instance.Document).Current;
      transactionScope.Complete();
    }
    return data;
  }

  public virtual PMTran ReverseTran(PMTran tran)
  {
    PMTran copy = PXCache<PMTran>.CreateCopy(tran);
    copy.OrigTranID = tran.TranID;
    copy.TranID = new long?();
    copy.TranType = (string) null;
    copy.TranDate = new DateTime?();
    copy.TranPeriodID = (string) null;
    copy.RefNbr = (string) null;
    copy.ARRefNbr = (string) null;
    copy.ARTranType = (string) null;
    copy.RefLineNbr = new int?();
    copy.ProformaRefNbr = (string) null;
    copy.ProformaLineNbr = new int?();
    copy.BatchNbr = (string) null;
    copy.RemainderOfTranID = new long?();
    copy.OrigProjectID = new int?();
    copy.OrigTaskID = new int?();
    copy.OrigAccountGroupID = new int?();
    copy.NoteID = new Guid?();
    copy.AllocationID = (string) null;
    PMTran pmTran1 = copy;
    Decimal? tranCuryAmount = pmTran1.TranCuryAmount;
    Decimal num1 = (Decimal) -1;
    pmTran1.TranCuryAmount = tranCuryAmount.HasValue ? new Decimal?(tranCuryAmount.GetValueOrDefault() * num1) : new Decimal?();
    PMTran pmTran2 = copy;
    Decimal? projectCuryAmount = pmTran2.ProjectCuryAmount;
    Decimal num2 = (Decimal) -1;
    pmTran2.ProjectCuryAmount = projectCuryAmount.HasValue ? new Decimal?(projectCuryAmount.GetValueOrDefault() * num2) : new Decimal?();
    copy.TranCuryAmountCopy = new Decimal?();
    PMTran pmTran3 = copy;
    Decimal? amount = pmTran3.Amount;
    Decimal num3 = (Decimal) -1;
    pmTran3.Amount = amount.HasValue ? new Decimal?(amount.GetValueOrDefault() * num3) : new Decimal?();
    PMTran pmTran4 = copy;
    Decimal? qty = pmTran4.Qty;
    Decimal num4 = (Decimal) -1;
    pmTran4.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() * num4) : new Decimal?();
    PMTran pmTran5 = copy;
    Decimal? billableQty = pmTran5.BillableQty;
    Decimal num5 = (Decimal) -1;
    pmTran5.BillableQty = billableQty.HasValue ? new Decimal?(billableQty.GetValueOrDefault() * num5) : new Decimal?();
    copy.Billable = tran.Billable;
    copy.Released = new bool?(false);
    copy.Billed = new bool?(false);
    copy.Allocated = new bool?(false);
    copy.IsNonGL = new bool?(false);
    copy.ExcludedFromBilling = tran.ExcludedFromBilling;
    copy.ExcludedFromAllocation = tran.ExcludedFromAllocation;
    copy.ExcludedFromBillingReason = PXMessages.LocalizeFormatNoPrefix("Reversal of Tran. ID {0}", new object[1]
    {
      (object) tran.TranID
    });
    copy.Reverse = "N";
    return copy;
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<PMRegister>((PXGraph) this.Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }

  private bool VerifyEmployeeRestriction(int? projectID, int? staffID)
  {
    if (!projectID.HasValue || !staffID.HasValue)
      return false;
    if (!PMProject.PK.Find((PXGraph) this.Base, projectID).RestrictToEmployeeList.GetValueOrDefault())
      return true;
    return PXResultset<EPEmployeeContract>.op_Implicit(PXSelectBase<EPEmployeeContract, PXViewOf<EPEmployeeContract>.BasedOn<SelectFromBase<EPEmployeeContract, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPEmployeeContract.contractID, Equal<P.AsInt>>>>>.And<BqlOperand<EPEmployeeContract.employeeID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) projectID,
      (object) staffID
    })) != null;
  }

  public delegate void ReleaseDocumentDelegate(PMRegister doc);
}
