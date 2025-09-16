// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_INReleaseProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.InventoryRelease;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_INReleaseProcess : PXGraphExtension<INReleaseProcess.SO2POSync, INReleaseProcess>
{
  public bool updateCosts;
  public PXSelect<FSServiceOrder> serviceOrderView;
  public PXSelect<FSSODetSplit> soDetSplitView;
  public PXSelect<FSAppointmentDet> apptDetView;
  public PXSelect<FSApptLineSplit> apptSplitView;

  public virtual void AddNewSODet(
    List<SM_INReleaseProcess.SrvOrder> srvOrderList,
    int? sOID,
    string srvOrdType,
    int? sODetID,
    string serialNbr,
    Decimal? curyUnitCost)
  {
    SM_INReleaseProcess.SrvOrder srvOrder1 = srvOrderList.Find((Predicate<SM_INReleaseProcess.SrvOrder>) (x =>
    {
      int? soid = x.SOID;
      int? nullable = sOID;
      return soid.GetValueOrDefault() == nullable.GetValueOrDefault() & soid.HasValue == nullable.HasValue;
    }));
    if (srvOrder1 != null)
    {
      SM_INReleaseProcess.SODet soDet = srvOrder1.Details.Find((Predicate<SM_INReleaseProcess.SODet>) (x =>
      {
        int? soDetId = x.SODetID;
        int? nullable = sODetID;
        return soDetId.GetValueOrDefault() == nullable.GetValueOrDefault() & soDetId.HasValue == nullable.HasValue;
      }));
      if (soDet == null || !soDet.SODetID.HasValue)
      {
        srvOrder1.Details.Add(string.IsNullOrEmpty(serialNbr) ? new SM_INReleaseProcess.SODet(sODetID) : new SM_INReleaseProcess.SODet(sODetID, serialNbr, curyUnitCost, new bool?(true)));
      }
      else
      {
        if (string.IsNullOrEmpty(serialNbr))
          return;
        SM_INReleaseProcess.SODetSplit soDetSplit = soDet.Splits.Find((Predicate<SM_INReleaseProcess.SODetSplit>) (x => string.Equals(x.LotSerialNbr, serialNbr, StringComparison.OrdinalIgnoreCase)));
        if (soDetSplit != null && !string.IsNullOrEmpty(soDetSplit.LotSerialNbr))
          return;
        soDet.Splits.Add(new SM_INReleaseProcess.SODetSplit(serialNbr, curyUnitCost));
      }
    }
    else
    {
      SM_INReleaseProcess.SrvOrder srvOrder2 = string.IsNullOrEmpty(serialNbr) ? new SM_INReleaseProcess.SrvOrder(sOID, srvOrdType, sODetID) : new SM_INReleaseProcess.SrvOrder(sOID, srvOrdType, sODetID, serialNbr, curyUnitCost, new bool?(true));
      srvOrderList.Add(srvOrder2);
    }
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBString(IsKey = true, IsUnicode = true)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSSODetSplit.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSSODetSplit.refNbr>>>>>))]
  [PXDefault]
  protected virtual void FSSODetSplit_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault]
  protected virtual void FSSODetSplit_OrderDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void FSSODetSplit_SiteID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void FSSODetSplit_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString]
  protected void _(PX.Data.Events.CacheAttached<FSSODetSplit.lotSerialNbr> e)
  {
  }

  [PXDBString]
  protected void _(
    PX.Data.Events.CacheAttached<FSAppointmentDet.lotSerialNbr> e)
  {
  }

  [PXDBString]
  protected void _(
    PX.Data.Events.CacheAttached<FSApptLineSplit.lotSerialNbr> e)
  {
  }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "")]
  protected void _(PX.Data.Events.CacheAttached<FSApptLineSplit.apptNbr> e)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  protected void _(
    PX.Data.Events.CacheAttached<FSApptLineSplit.origSrvOrdNbr> e)
  {
  }

  [PXDBInt]
  protected void _(
    PX.Data.Events.CacheAttached<FSApptLineSplit.origLineNbr> e)
  {
  }

  [PXDBDate]
  protected void _(PX.Data.Events.CacheAttached<FSApptLineSplit.apptDate> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSAppointment.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  [PXFormula(null, typeof (MaxCalc<FSAppointment.maxLineNbr>))]
  protected void _(PX.Data.Events.CacheAttached<FSAppointmentDet.lineNbr> e)
  {
  }

  public virtual List<PXResult<INItemPlan, INPlanType>> ProcessPOReceipt(
    PXGraph graph,
    IEnumerable<PXResult<INItemPlan, INPlanType>> list,
    string POReceiptType,
    string POReceiptNbr)
  {
    return FSPOReceiptProcess.ProcessPOReceipt(graph, list, POReceiptType, POReceiptNbr, true);
  }

  [PXOverride]
  public virtual void Persist(SM_INReleaseProcess.PersistDelegate baseMethod)
  {
    if (!SharedFunctions.isFSSetupSet((PXGraph) ((PXGraphExtension<INReleaseProcess>) this).Base))
    {
      baseMethod();
    }
    else
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        baseMethod();
        this.UpdateCosts(((PXSelectBase<INRegister>) ((PXGraphExtension<INReleaseProcess>) this).Base.inregister).Current);
        transactionScope.Complete();
      }
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.ReleaseDocProc(PX.Objects.GL.JournalEntry,PX.Objects.IN.INRegister,System.Boolean)" />
  /// </summary>
  [PXOverride]
  public virtual void ReleaseDocProc(
    JournalEntry je,
    INRegister doc,
    bool releaseFromHold,
    SM_INReleaseProcess.ReleaseDocProcDelegate del)
  {
    this.ValidatePostBatchStatus((PXDBOperation) 1, "IN", doc.DocType, doc.RefNbr);
    if (del == null)
      return;
    del(je, doc, releaseFromHold);
  }

  [PXOverride]
  public void Process(
    (string Type, string Nbr) poReceiptKey,
    IEnumerable<PXResult<INItemPlan, INPlanType>> poDemands,
    Action<(string Type, string Nbr), IEnumerable<PXResult<INItemPlan, INPlanType>>> base_Process)
  {
    List<PXResult<INItemPlan, INPlanType>> pxResultList = this.ProcessPOReceipt((PXGraph) ((PXGraphExtension<INReleaseProcess>) this).Base, poDemands, poReceiptKey.Type, poReceiptKey.Nbr);
    base_Process(poReceiptKey, (IEnumerable<PXResult<INItemPlan, INPlanType>>) pxResultList);
  }

  public virtual void UpdateCosts(INRegister inRegisterRow)
  {
    if (inRegisterRow == null)
      return;
    FSPostRegister postRegister = this.GetPostRegister(inRegisterRow);
    if (postRegister == null || string.IsNullOrEmpty(postRegister.RefNbr))
      return;
    List<SM_INReleaseProcess.INTranProcess> inTranProcessList = new List<SM_INReleaseProcess.INTranProcess>();
    if (postRegister.PostedTO == "SO")
    {
      foreach (PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet> pxResult in PXSelectBase<FSPostDet, PXSelectJoin<FSPostDet, InnerJoin<INTran, On<FSPostDet.sOPosted, Equal<True>, And<INTran.sOOrderType, Equal<FSPostDet.sOOrderType>, And<INTran.sOOrderNbr, Equal<FSPostDet.sOOrderNbr>, And<INTran.sOOrderLineNbr, Equal<FSPostDet.sOLineNbr>>>>>, LeftJoin<INTranSplit, On<INTranSplit.docType, Equal<INTran.docType>, And<INTranSplit.refNbr, Equal<INTran.refNbr>, And<INTranSplit.lineNbr, Equal<INTran.lineNbr>>>>, LeftJoin<FSSODet, On<FSSODet.postID, Equal<FSPostDet.postID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>>>>>>, Where<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>>>, OrderBy<Asc<FSSODet.sOID, Asc<FSAppointmentDet.appointmentID, Asc<INTran.inventoryID>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<INReleaseProcess>) this).Base, new object[2]
      {
        (object) inRegisterRow.DocType,
        (object) inRegisterRow.RefNbr
      }))
        inTranProcessList.Add(new SM_INReleaseProcess.INTranProcess()
        {
          appDet = PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet>.op_Implicit(pxResult),
          soDet = PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet>.op_Implicit(pxResult),
          inTran = PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet>.op_Implicit(pxResult),
          inTranSplit = PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet>.op_Implicit(pxResult)
        });
    }
    if (postRegister.PostedTO == "SI")
    {
      if (postRegister.EntityType == "SO")
      {
        foreach (PXResult<INTran, PX.Objects.AR.ARTran, FSSODet, FSPostDet> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<PX.Objects.AR.ARTran, On<INTran.aRDocType, Equal<PX.Objects.AR.ARTran.tranType>, And<INTran.aRRefNbr, Equal<PX.Objects.AR.ARTran.refNbr>, And<INTran.aRLineNbr, Equal<PX.Objects.AR.ARTran.lineNbr>>>>, LeftJoin<FSSODet, On<FSSODet.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSSODet.refNbr, Equal<FSxARTran.serviceOrderRefNbr>, And<FSSODet.lineNbr, Equal<FSxARTran.serviceOrderLineNbr>, And<FSxARTran.appointmentRefNbr, IsNull>>>>, LeftJoin<FSPostDet, On<FSPostDet.postID, Equal<FSSODet.postID>>>>>, Where<FSPostDet.sOInvPosted, Equal<True>, And<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>>>>, OrderBy<Asc<FSSODet.sOID, Asc<INTran.inventoryID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<INReleaseProcess>) this).Base, new object[2]
        {
          (object) inRegisterRow.DocType,
          (object) inRegisterRow.RefNbr
        }))
          inTranProcessList.Add(new SM_INReleaseProcess.INTranProcess()
          {
            appDet = (FSAppointmentDet) null,
            soDet = PXResult<INTran, PX.Objects.AR.ARTran, FSSODet, FSPostDet>.op_Implicit(pxResult),
            inTran = PXResult<INTran, PX.Objects.AR.ARTran, FSSODet, FSPostDet>.op_Implicit(pxResult)
          });
      }
      else if (postRegister.EntityType == "AP")
      {
        foreach (PXResult<INTran, PX.Objects.AR.ARTran, FSAppointmentDet, FSPostDet> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<PX.Objects.AR.ARTran, On<INTran.aRDocType, Equal<PX.Objects.AR.ARTran.tranType>, And<INTran.aRRefNbr, Equal<PX.Objects.AR.ARTran.refNbr>, And<INTran.aRLineNbr, Equal<PX.Objects.AR.ARTran.lineNbr>>>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSAppointmentDet.refNbr, Equal<FSxARTran.appointmentRefNbr>, And<FSAppointmentDet.lineNbr, Equal<FSxARTran.appointmentLineNbr>>>>, LeftJoin<FSPostDet, On<FSPostDet.postID, Equal<FSAppointmentDet.postID>>>>>, Where<FSPostDet.sOInvPosted, Equal<True>, And<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>>>>, OrderBy<Asc<FSAppointmentDet.appointmentID, Asc<INTran.inventoryID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<INReleaseProcess>) this).Base, new object[2]
        {
          (object) inRegisterRow.DocType,
          (object) inRegisterRow.RefNbr
        }))
          inTranProcessList.Add(new SM_INReleaseProcess.INTranProcess()
          {
            appDet = PXResult<INTran, PX.Objects.AR.ARTran, FSAppointmentDet, FSPostDet>.op_Implicit(pxResult),
            soDet = (FSSODet) null,
            inTran = PXResult<INTran, PX.Objects.AR.ARTran, FSAppointmentDet, FSPostDet>.op_Implicit(pxResult)
          });
      }
    }
    if (postRegister.PostedTO == "IN")
    {
      foreach (PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet> pxResult in PXSelectBase<FSPostDet, PXSelectJoin<FSPostDet, InnerJoin<INTran, On<FSPostDet.iNPosted, Equal<True>, And<INTran.docType, Equal<FSPostDet.iNDocType>, And<INTran.refNbr, Equal<FSPostDet.iNRefNbr>, And<INTran.lineNbr, Equal<FSPostDet.iNLineNbr>>>>>, LeftJoin<INTranSplit, On<INTranSplit.docType, Equal<INTran.docType>, And<INTranSplit.refNbr, Equal<INTran.refNbr>, And<INTranSplit.lineNbr, Equal<INTran.lineNbr>>>>, LeftJoin<FSSODet, On<FSSODet.postID, Equal<FSPostDet.postID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>>>>>>, Where<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>>>, OrderBy<Asc<FSSODet.sOID, Asc<FSAppointmentDet.appointmentID, Asc<INTran.inventoryID>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<INReleaseProcess>) this).Base, new object[2]
      {
        (object) inRegisterRow.DocType,
        (object) inRegisterRow.RefNbr
      }))
        inTranProcessList.Add(new SM_INReleaseProcess.INTranProcess()
        {
          appDet = PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet>.op_Implicit(pxResult),
          soDet = PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet>.op_Implicit(pxResult),
          inTran = PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet>.op_Implicit(pxResult),
          inTranSplit = PXResult<FSPostDet, INTran, INTranSplit, FSSODet, FSAppointmentDet>.op_Implicit(pxResult)
        });
    }
    if (inTranProcessList.Count <= 0)
      return;
    AppointmentEntry instance1 = PXGraph.CreateInstance<AppointmentEntry>();
    ServiceOrderEntry instance2 = PXGraph.CreateInstance<ServiceOrderEntry>();
    ((PXGraph) instance2).Clear((PXClearOption) 3);
    ((PXGraph) instance1).Clear((PXClearOption) 3);
    PXResultset<FSApptLineSplit> pxResultset1 = (PXResultset<FSApptLineSplit>) null;
    PXResultset<FSSODetSplit> pxResultset2 = (PXResultset<FSSODetSplit>) null;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    List<SM_INReleaseProcess.SrvOrder> srvOrderList1 = new List<SM_INReleaseProcess.SrvOrder>();
    FSAppointmentDet fsAppointmentDet1 = (FSAppointmentDet) null;
    FSSODet fssoDet1 = (FSSODet) null;
    FSSrvOrdType fsSrvOrdType = (FSSrvOrdType) null;
    foreach (SM_INReleaseProcess.INTranProcess inTranProcess in inTranProcessList)
    {
      FSAppointmentDet appDet = inTranProcess.appDet;
      FSSODet soDet = inTranProcess.soDet;
      INTran inTran = inTranProcess.inTran;
      INTranSplit inTranSplit = inTranProcess.inTranSplit;
      int? nullable3;
      int? lineNbr1;
      Decimal? nullable4;
      Decimal? nullable5;
      if (soDet != null)
      {
        nullable3 = soDet.SODetID;
        if (nullable3.HasValue)
        {
          if (fsSrvOrdType == null || fsSrvOrdType.SrvOrdType != soDet.SrvOrdType)
            fsSrvOrdType = FSSrvOrdType.PK.Find((PXGraph) instance2, soDet.SrvOrdType);
          if (((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Current == null || ((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Current.SrvOrdType != soDet.SrvOrdType || ((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Current.RefNbr != soDet.RefNbr)
          {
            if (((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Current != null && ((PXGraph) instance2).IsDirty)
            {
              ((PXAction) instance2.Save).Press();
              ((PXGraph) instance2).Clear((PXClearOption) 3);
            }
            ((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) soDet.RefNbr, new object[1]
            {
              (object) soDet.SrvOrdType
            }));
            ((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Current.IsINReleaseProcess = true;
            nullable2 = new int?();
          }
          if (nullable2.HasValue)
          {
            nullable3 = nullable2;
            lineNbr1 = soDet.LineNbr;
            if (nullable3.GetValueOrDefault() == lineNbr1.GetValueOrDefault() & nullable3.HasValue == lineNbr1.HasValue)
              goto label_51;
          }
          nullable2 = soDet.LineNbr;
          pxResultset2 = PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSApptLineSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSApptLineSplit.lineNbr>>, And<FSSODetSplit.pOCreate, Equal<False>>>>>>.Config>.Select((PXGraph) instance2, new object[3]
          {
            (object) soDet.SrvOrdType,
            (object) soDet.RefNbr,
            (object) soDet.LineNbr
          });
label_51:
          Decimal? nullable6 = new Decimal?(0M);
          Decimal? nullable7 = new Decimal?(0M);
          foreach (PXResult<FSSODetSplit> pxResult in pxResultset2)
          {
            FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
            if (!SharedFunctions.IsLotSerialRequired(((PXSelectBase) instance2.ServiceOrderDetails).Cache, fssoDetSplit.InventoryID) || string.Equals(fssoDetSplit.LotSerialNbr, inTran.LotSerialNbr, StringComparison.OrdinalIgnoreCase) || inTranSplit != null && string.Equals(fssoDetSplit.LotSerialNbr, inTranSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
            {
              fssoDetSplit.UnitCost = inTran.UnitCost;
              PXCache cache = ((PXSelectBase) instance2.Splits).Cache;
              FSSODetSplit row = fssoDetSplit;
              nullable4 = fssoDetSplit.UnitCost;
              Decimal baseval = nullable4.Value;
              Decimal num;
              ref Decimal local = ref num;
              int prcCst = CommonSetupDecPl.PrcCst;
              PXDBCurrencyAttribute.CuryConvCury(cache, (object) row, baseval, out local, prcCst);
              fssoDetSplit.CuryUnitCost = new Decimal?(num);
              ((PXSelectBase<FSSODetSplit>) instance2.Splits).Update(fssoDetSplit);
            }
            nullable4 = nullable7;
            nullable5 = fssoDetSplit.Qty;
            nullable7 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
            nullable5 = nullable6;
            Decimal? qty = fssoDetSplit.Qty;
            Decimal? nullable8 = fssoDetSplit.UnitCost;
            nullable4 = qty.HasValue & nullable8.HasValue ? new Decimal?(qty.GetValueOrDefault() * nullable8.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable9;
            if (!(nullable5.HasValue & nullable4.HasValue))
            {
              nullable8 = new Decimal?();
              nullable9 = nullable8;
            }
            else
              nullable9 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
            nullable6 = nullable9;
          }
          if (fssoDet1 != null && !(fssoDet1.SrvOrdType != soDet.SrvOrdType) && !(fssoDet1.RefNbr != soDet.RefNbr))
          {
            int? lineNbr2 = fssoDet1.LineNbr;
            int? lineNbr3 = soDet.LineNbr;
            if (lineNbr2.GetValueOrDefault() == lineNbr3.GetValueOrDefault() & lineNbr2.HasValue == lineNbr3.HasValue)
              goto label_66;
          }
          fssoDet1 = FSSODet.PK.Find((PXGraph) instance2, soDet.SrvOrdType, soDet.RefNbr, soDet.LineNbr);
label_66:
          FSSODet fssoDet2 = fssoDet1;
          Decimal? nullable10 = nullable7;
          Decimal num1 = 0M;
          Decimal? nullable11;
          Decimal? nullable12;
          if (!(nullable10.GetValueOrDefault() > num1 & nullable10.HasValue))
          {
            nullable12 = inTran.UnitCost;
          }
          else
          {
            Decimal? nullable13 = nullable6;
            nullable11 = nullable7;
            nullable12 = nullable13.HasValue & nullable11.HasValue ? new Decimal?(nullable13.GetValueOrDefault() / nullable11.GetValueOrDefault()) : new Decimal?();
          }
          fssoDet2.UnitCost = nullable12;
          PXCache cache1 = ((PXSelectBase) instance2.ServiceOrderDetails).Cache;
          FSSODet row1 = fssoDet1;
          nullable11 = fssoDet1.UnitCost;
          Decimal baseval1 = nullable11.Value;
          Decimal num2;
          ref Decimal local1 = ref num2;
          int prcCst1 = CommonSetupDecPl.PrcCst;
          PXDBCurrencyAttribute.CuryConvCury(cache1, (object) row1, baseval1, out local1, prcCst1);
          fssoDet1.CuryUnitCost = new Decimal?(num2);
          if (fsSrvOrdType.PostTo == "PM" && fsSrvOrdType.BillingType == "CC")
            fssoDet1.CuryUnitPrice = new Decimal?(num2);
          fssoDet1 = (FSSODet) ((PXSelectBase) instance2.ServiceOrderDetails).Cache.CreateCopy((object) ((PXSelectBase<FSSODet>) instance2.ServiceOrderDetails).Update(fssoDet1));
          foreach (PXResult<FSAppointmentDet> pxResult in PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>>, OrderBy<Asc<FSAppointmentDet.appointmentID>>>.Config>.Select((PXGraph) instance1, new object[1]
          {
            (object) fssoDet1.SODetID
          }))
          {
            FSAppointmentDet fsAppointmentDet2 = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
            if (((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current == null || ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.SrvOrdType != fsAppointmentDet2.SrvOrdType || ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.RefNbr != fsAppointmentDet2.RefNbr)
            {
              if (((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current != null && ((PXGraph) instance1).IsDirty)
                ((PXAction) instance1.Save).Press();
              ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointmentDet2.RefNbr, new object[1]
              {
                (object) fsAppointmentDet2.SrvOrdType
              }));
              ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.MustUpdateServiceOrder = new bool?(false);
            }
            FSAppointmentDet fsAppointmentDet3 = FSAppointmentDet.PK.Find((PXGraph) instance1, fsAppointmentDet2.SrvOrdType, fsAppointmentDet2.RefNbr, fsAppointmentDet2.LineNbr);
            if (fsAppointmentDet3 != null)
            {
              fsAppointmentDet3.CuryUnitCost = fssoDet1.CuryUnitCost;
              ((PXSelectBase<FSAppointmentDet>) instance1.AppointmentDetails).Update(fsAppointmentDet3);
              ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.MustUpdateServiceOrder = new bool?(false);
            }
          }
        }
      }
      if (appDet != null)
      {
        nullable3 = appDet.AppDetID;
        if (nullable3.HasValue)
        {
          if (fsSrvOrdType == null || fsSrvOrdType.SrvOrdType != appDet.SrvOrdType)
            fsSrvOrdType = FSSrvOrdType.PK.Find((PXGraph) instance1, appDet.SrvOrdType);
          if (((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current == null || ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.SrvOrdType != appDet.SrvOrdType || ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.RefNbr != appDet.RefNbr)
          {
            if (((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current != null && ((PXGraph) instance1).IsDirty)
            {
              ((PXAction) instance1.Save).Press();
              ((PXGraph) instance1).Clear((PXClearOption) 3);
            }
            ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Search<FSAppointment.refNbr>((object) appDet.RefNbr, new object[1]
            {
              (object) appDet.SrvOrdType
            }));
            ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.IsINReleaseProcess = true;
            nullable1 = new int?();
          }
          if (nullable1.HasValue)
          {
            nullable3 = nullable1;
            lineNbr1 = appDet.LineNbr;
            if (nullable3.GetValueOrDefault() == lineNbr1.GetValueOrDefault() & nullable3.HasValue == lineNbr1.HasValue)
              goto label_95;
          }
          nullable1 = appDet.LineNbr;
          pxResultset1 = PXSelectBase<FSApptLineSplit, PXSelect<FSApptLineSplit, Where<FSApptLineSplit.srvOrdType, Equal<Required<FSApptLineSplit.srvOrdType>>, And<FSApptLineSplit.apptNbr, Equal<Required<FSApptLineSplit.apptNbr>>, And<FSApptLineSplit.lineNbr, Equal<Required<FSApptLineSplit.lineNbr>>>>>>.Config>.Select((PXGraph) instance1, new object[3]
          {
            (object) appDet.SrvOrdType,
            (object) appDet.RefNbr,
            (object) appDet.LineNbr
          });
label_95:
          Decimal? nullable14 = new Decimal?(0M);
          Decimal? nullable15 = new Decimal?(0M);
          foreach (PXResult<FSApptLineSplit> pxResult in pxResultset1)
          {
            FSApptLineSplit fsApptLineSplit = PXResult<FSApptLineSplit>.op_Implicit(pxResult);
            if (!SharedFunctions.IsLotSerialRequired(((PXSelectBase) instance1.AppointmentDetails).Cache, fsApptLineSplit.InventoryID) || string.Equals(fsApptLineSplit.LotSerialNbr, inTran.LotSerialNbr, StringComparison.OrdinalIgnoreCase) || inTranSplit != null && string.Equals(fsApptLineSplit.LotSerialNbr, inTranSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
            {
              fsApptLineSplit.UnitCost = inTran.UnitCost;
              PXCache cache = ((PXSelectBase) instance1.Splits).Cache;
              FSApptLineSplit row = fsApptLineSplit;
              nullable5 = fsApptLineSplit.UnitCost;
              Decimal baseval = nullable5.Value;
              Decimal num;
              ref Decimal local = ref num;
              int prcCst = CommonSetupDecPl.PrcCst;
              PXDBCurrencyAttribute.CuryConvCury(cache, (object) row, baseval, out local, prcCst);
              fsApptLineSplit.CuryUnitCost = new Decimal?(num);
              ((PXSelectBase<FSApptLineSplit>) instance1.Splits).Update(fsApptLineSplit);
              this.AddNewSODet(srvOrderList1, ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.SOID, ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.SrvOrdType, appDet.SODetID, fsApptLineSplit.LotSerialNbr, fsApptLineSplit.CuryUnitCost);
            }
            nullable5 = nullable15;
            nullable4 = fsApptLineSplit.Qty;
            nullable15 = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            nullable4 = nullable14;
            Decimal? qty = fsApptLineSplit.Qty;
            Decimal? nullable16 = fsApptLineSplit.UnitCost;
            nullable5 = qty.HasValue & nullable16.HasValue ? new Decimal?(qty.GetValueOrDefault() * nullable16.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable17;
            if (!(nullable4.HasValue & nullable5.HasValue))
            {
              nullable16 = new Decimal?();
              nullable17 = nullable16;
            }
            else
              nullable17 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
            nullable14 = nullable17;
          }
          if (fsAppointmentDet1 != null && !(fsAppointmentDet1.SrvOrdType != appDet.SrvOrdType) && !(fsAppointmentDet1.RefNbr != appDet.RefNbr))
          {
            lineNbr1 = fsAppointmentDet1.LineNbr;
            nullable3 = appDet.LineNbr;
            if (lineNbr1.GetValueOrDefault() == nullable3.GetValueOrDefault() & lineNbr1.HasValue == nullable3.HasValue)
              goto label_110;
          }
          fsAppointmentDet1 = FSAppointmentDet.PK.Find((PXGraph) instance1, appDet.SrvOrdType, appDet.RefNbr, appDet.LineNbr);
label_110:
          nullable5 = fsAppointmentDet1.UnitCost;
          nullable4 = inTran.UnitCost;
          if (!(nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue))
          {
            FSAppointmentDet fsAppointmentDet4 = fsAppointmentDet1;
            nullable4 = nullable15;
            Decimal num3 = 0M;
            Decimal? nullable18;
            if (!(nullable4.GetValueOrDefault() > num3 & nullable4.HasValue))
            {
              nullable18 = inTran.UnitCost;
            }
            else
            {
              nullable4 = nullable14;
              nullable5 = nullable15;
              nullable18 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() / nullable5.GetValueOrDefault()) : new Decimal?();
            }
            fsAppointmentDet4.UnitCost = nullable18;
            PXCache cache = ((PXSelectBase) instance1.AppointmentDetails).Cache;
            FSAppointmentDet row = fsAppointmentDet1;
            nullable5 = fsAppointmentDet1.UnitCost;
            Decimal baseval = nullable5.Value;
            Decimal num4;
            ref Decimal local = ref num4;
            int prcCst = CommonSetupDecPl.PrcCst;
            PXDBCurrencyAttribute.CuryConvCury(cache, (object) row, baseval, out local, prcCst);
            fsAppointmentDet1.CuryUnitCost = new Decimal?(num4);
            if (fsSrvOrdType.PostTo == "PM" && fsSrvOrdType.BillingType == "CC")
              fsAppointmentDet1.CuryUnitPrice = new Decimal?(num4);
            fsAppointmentDet1 = (FSAppointmentDet) ((PXSelectBase) instance1.AppointmentDetails).Cache.CreateCopy((object) ((PXSelectBase<FSAppointmentDet>) instance1.AppointmentDetails).Update(fsAppointmentDet1));
            List<SM_INReleaseProcess.SrvOrder> srvOrderList2 = srvOrderList1;
            int? soid = ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.SOID;
            string srvOrdType = ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current.SrvOrdType;
            int? soDetId = appDet.SODetID;
            string empty = string.Empty;
            nullable5 = new Decimal?();
            Decimal? curyUnitCost = nullable5;
            this.AddNewSODet(srvOrderList2, soid, srvOrdType, soDetId, empty, curyUnitCost);
          }
        }
      }
    }
    if (((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current != null && ((PXGraph) instance1).IsDirty)
    {
      ((PXAction) instance1.Save).Press();
      ((PXGraph) instance1).Clear((PXClearOption) 3);
    }
    if (((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Current != null && ((PXGraph) instance2).IsDirty)
    {
      ((PXAction) instance2.Save).Press();
      ((PXGraph) instance2).Clear((PXClearOption) 3);
    }
    if (srvOrderList1.Count <= 0)
      return;
    this.UpdateServiceOrderAffectedCost(instance2, srvOrderList1);
  }

  private void UpdateServiceOrderAffectedCost(
    ServiceOrderEntry soGraph,
    List<SM_INReleaseProcess.SrvOrder> srvOrderList)
  {
    foreach (SM_INReleaseProcess.SrvOrder srvOrder in srvOrderList)
    {
      ((PXSelectBase<FSServiceOrder>) soGraph.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) soGraph.ServiceOrderRecords).Search<FSServiceOrder.sOID>((object) srvOrder.SOID, new object[1]
      {
        (object) srvOrder.srvOrdType
      }));
      ((PXSelectBase<FSServiceOrder>) soGraph.ServiceOrderRecords).Current.IsINReleaseProcess = true;
      foreach (SM_INReleaseProcess.SODet detail in srvOrder.Details)
      {
        if (detail.SODetID.HasValue)
        {
          FSSODet fssoDet1 = FSSODet.UK.Find((PXGraph) soGraph, detail.SODetID);
          PXResultset<FSAppointmentDet> pxResultset = PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>>>.Config>.Select((PXGraph) soGraph, new object[1]
          {
            (object) fssoDet1.SODetID
          });
          Decimal? nullable1 = new Decimal?(0M);
          foreach (PXResult<FSSODetSplit> pxResult1 in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSApptLineSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSApptLineSplit.lineNbr>>, And<FSSODetSplit.pOCreate, Equal<False>>>>>>.Config>.Select((PXGraph) soGraph, new object[3]
          {
            (object) fssoDet1.SrvOrdType,
            (object) fssoDet1.RefNbr,
            (object) fssoDet1.LineNbr
          }))
          {
            FSSODetSplit fsSODetSplitRow = PXResult<FSSODetSplit>.op_Implicit(pxResult1);
            if (!string.IsNullOrEmpty(fsSODetSplitRow.LotSerialNbr))
            {
              if (detail.Splits != null && detail.Splits.Exists((Predicate<SM_INReleaseProcess.SODetSplit>) (x => string.Equals(x.LotSerialNbr, fsSODetSplitRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))))
              {
                fsSODetSplitRow.CuryUnitCost = detail.Splits.Find((Predicate<SM_INReleaseProcess.SODetSplit>) (x => string.Equals(x.LotSerialNbr, fsSODetSplitRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).CuryUnitCost;
                ((PXSelectBase<FSSODetSplit>) soGraph.Splits).Update(fsSODetSplitRow);
              }
              Decimal? nullable2 = nullable1;
              Decimal? curyUnitCost = fsSODetSplitRow.CuryUnitCost;
              Decimal? nullable3 = fsSODetSplitRow.Qty;
              Decimal? nullable4 = curyUnitCost.HasValue & nullable3.HasValue ? new Decimal?(curyUnitCost.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable5;
              if (!(nullable2.HasValue & nullable4.HasValue))
              {
                nullable3 = new Decimal?();
                nullable5 = nullable3;
              }
              else
                nullable5 = new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault());
              nullable1 = nullable5;
            }
            else
            {
              Decimal? nullable6 = new Decimal?(0M);
              Decimal? nullable7 = fsSODetSplitRow.Qty;
              bool? isSerialized = detail.IsSerialized;
              bool flag = false;
              if (isSerialized.GetValueOrDefault() == flag & isSerialized.HasValue)
              {
                foreach (PXResult<FSAppointmentDet> pxResult2 in pxResultset)
                {
                  FSAppointmentDet fsAppointmentDet1 = PXResult<FSAppointmentDet>.op_Implicit(pxResult2);
                  Decimal? nullable8 = fsAppointmentDet1.INOpenQty;
                  if (!nullable8.HasValue)
                    fsAppointmentDet1.INOpenQty = fsAppointmentDet1.BillableQty;
                  nullable8 = fsAppointmentDet1.INOpenQty;
                  Decimal? nullable9 = nullable7;
                  if (nullable8.GetValueOrDefault() < nullable9.GetValueOrDefault() & nullable8.HasValue & nullable9.HasValue)
                  {
                    nullable9 = nullable7;
                    nullable8 = fsAppointmentDet1.INOpenQty;
                    nullable7 = nullable9.HasValue & nullable8.HasValue ? new Decimal?(nullable9.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
                    nullable8 = nullable6;
                    Decimal? inOpenQty = fsAppointmentDet1.INOpenQty;
                    Decimal? nullable10 = fsAppointmentDet1.CuryUnitCost;
                    nullable9 = inOpenQty.HasValue & nullable10.HasValue ? new Decimal?(inOpenQty.GetValueOrDefault() * nullable10.GetValueOrDefault()) : new Decimal?();
                    Decimal? nullable11;
                    if (!(nullable8.HasValue & nullable9.HasValue))
                    {
                      nullable10 = new Decimal?();
                      nullable11 = nullable10;
                    }
                    else
                      nullable11 = new Decimal?(nullable8.GetValueOrDefault() + nullable9.GetValueOrDefault());
                    nullable6 = nullable11;
                    fsAppointmentDet1.INOpenQty = new Decimal?(0M);
                  }
                  else
                  {
                    FSAppointmentDet fsAppointmentDet2 = fsAppointmentDet1;
                    nullable9 = fsAppointmentDet1.INOpenQty;
                    nullable8 = nullable7;
                    Decimal? nullable12 = nullable9.HasValue & nullable8.HasValue ? new Decimal?(nullable9.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
                    fsAppointmentDet2.INOpenQty = nullable12;
                    nullable8 = nullable6;
                    Decimal? nullable13 = nullable7;
                    Decimal? nullable14 = fsAppointmentDet1.CuryUnitCost;
                    nullable9 = nullable13.HasValue & nullable14.HasValue ? new Decimal?(nullable13.GetValueOrDefault() * nullable14.GetValueOrDefault()) : new Decimal?();
                    Decimal? nullable15;
                    if (!(nullable8.HasValue & nullable9.HasValue))
                    {
                      nullable14 = new Decimal?();
                      nullable15 = nullable14;
                    }
                    else
                      nullable15 = new Decimal?(nullable8.GetValueOrDefault() + nullable9.GetValueOrDefault());
                    nullable6 = nullable15;
                    nullable7 = new Decimal?(0M);
                    break;
                  }
                }
              }
              Decimal? nullable16 = nullable6;
              Decimal? nullable17 = nullable7;
              Decimal? nullable18 = fssoDet1.CuryOrigUnitCost;
              Decimal? nullable19 = nullable17.HasValue & nullable18.HasValue ? new Decimal?(nullable17.GetValueOrDefault() * nullable18.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable20;
              if (!(nullable16.HasValue & nullable19.HasValue))
              {
                nullable18 = new Decimal?();
                nullable20 = nullable18;
              }
              else
                nullable20 = new Decimal?(nullable16.GetValueOrDefault() + nullable19.GetValueOrDefault());
              nullable6 = nullable20;
              FSSODetSplit fssoDetSplit = fsSODetSplitRow;
              nullable19 = fsSODetSplitRow.Qty;
              Decimal num = 0M;
              Decimal? nullable21;
              Decimal? nullable22;
              if (!(nullable19.GetValueOrDefault() == num & nullable19.HasValue))
              {
                nullable19 = nullable6;
                nullable21 = fsSODetSplitRow.Qty;
                if (!(nullable19.HasValue & nullable21.HasValue))
                {
                  nullable18 = new Decimal?();
                  nullable22 = nullable18;
                }
                else
                  nullable22 = new Decimal?(nullable19.GetValueOrDefault() / nullable21.GetValueOrDefault());
              }
              else
                nullable22 = fsSODetSplitRow.CuryUnitCost;
              fssoDetSplit.CuryUnitCost = nullable22;
              nullable21 = nullable1;
              nullable19 = nullable6;
              Decimal? nullable23;
              if (!(nullable21.HasValue & nullable19.HasValue))
              {
                nullable18 = new Decimal?();
                nullable23 = nullable18;
              }
              else
                nullable23 = new Decimal?(nullable21.GetValueOrDefault() + nullable19.GetValueOrDefault());
              nullable1 = nullable23;
              ((PXSelectBase<FSSODetSplit>) soGraph.Splits).Update(fsSODetSplitRow);
            }
          }
          FSSODet fssoDet2 = fssoDet1;
          Decimal? nullable24 = fssoDet1.Qty;
          Decimal num1 = 0M;
          Decimal? nullable25;
          if (!(nullable24.GetValueOrDefault() == num1 & nullable24.HasValue))
          {
            nullable24 = nullable1;
            Decimal? qty = fssoDet1.Qty;
            nullable25 = nullable24.HasValue & qty.HasValue ? new Decimal?(nullable24.GetValueOrDefault() / qty.GetValueOrDefault()) : new Decimal?();
          }
          else
            nullable25 = fssoDet1.CuryUnitCost;
          fssoDet2.CuryUnitCost = nullable25;
          if (((PXSelectBase<FSSrvOrdType>) soGraph.ServiceOrderTypeSelected).Current != null && ((PXSelectBase<FSSrvOrdType>) soGraph.ServiceOrderTypeSelected).Current.PostTo == "PM" && ((PXSelectBase<FSSrvOrdType>) soGraph.ServiceOrderTypeSelected).Current.BillingType == "CC")
            fssoDet1.CuryUnitPrice = fssoDet1.CuryUnitCost;
          ((PXSelectBase<FSSODet>) soGraph.ServiceOrderDetails).Update(fssoDet1);
        }
      }
      if (((PXSelectBase<FSServiceOrder>) soGraph.ServiceOrderRecords).Current != null && ((PXGraph) soGraph).IsDirty)
        ((PXAction) soGraph.Save).Press();
    }
  }

  public virtual FSPostRegister GetPostRegister(INRegister inRegisterRow)
  {
    if (((PXSelectBase<INRegister>) ((PXGraphExtension<INReleaseProcess>) this).Base.inregister).Current == null)
      return (FSPostRegister) null;
    return PXResult<FSPostRegister>.op_Implicit(((IQueryable<PXResult<FSPostRegister>>) PXSelectBase<FSPostRegister, PXSelect<FSPostRegister, Where2<Where<FSPostRegister.postedTO, Equal<ListField_PostTo.SO>, And<FSPostRegister.postDocType, Equal<Required<FSPostRegister.postDocType>>, And<FSPostRegister.postRefNbr, Equal<Required<FSPostRegister.postRefNbr>>>>>, Or2<Where<FSPostRegister.postedTO, Equal<ListField_PostTo.SI>, And<FSPostRegister.postDocType, Equal<Required<FSPostRegister.postDocType>>, And<FSPostRegister.postRefNbr, Equal<Required<FSPostRegister.postRefNbr>>>>>, Or<Where<FSPostRegister.postedTO, Equal<ListField_PostTo.IN>, And<FSPostRegister.postDocType, Equal<Required<FSPostRegister.postDocType>>, And<FSPostRegister.postRefNbr, Equal<Required<FSPostRegister.postRefNbr>>>>>>>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<INReleaseProcess>) this).Base, 0, 1, new object[6]
    {
      (object) inRegisterRow.SOOrderType,
      (object) inRegisterRow.SOOrderNbr,
      (object) inRegisterRow.SrcDocType,
      (object) inRegisterRow.SrcRefNbr,
      (object) inRegisterRow.DocType,
      (object) inRegisterRow.RefNbr
    })).FirstOrDefault<PXResult<FSPostRegister>>());
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<INRegister>((PXGraph) ((PXGraphExtension<INReleaseProcess>) this).Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }

  public class SODet
  {
    public int? SODetID;
    public bool? IsSerialized;
    public List<SM_INReleaseProcess.SODetSplit> Splits;

    public SODet(int? id)
    {
      this.SODetID = id;
      this.IsSerialized = new bool?(false);
      this.Splits = new List<SM_INReleaseProcess.SODetSplit>();
    }

    public SODet(int? id, string serialNbr, Decimal? curyUnitCost, bool? _isSerialized)
    {
      this.SODetID = id;
      this.IsSerialized = _isSerialized;
      this.Splits = new List<SM_INReleaseProcess.SODetSplit>();
      this.Splits.Add(new SM_INReleaseProcess.SODetSplit(serialNbr, curyUnitCost));
    }
  }

  public class SODetSplit
  {
    public string LotSerialNbr;
    public Decimal? CuryUnitCost;

    public SODetSplit(string serialNbr, Decimal? cost)
    {
      this.LotSerialNbr = serialNbr;
      this.CuryUnitCost = cost;
    }
  }

  public class SrvOrder
  {
    public int? SOID;
    public string srvOrdType;
    public List<SM_INReleaseProcess.SODet> Details;

    public SrvOrder() => this.Details = new List<SM_INReleaseProcess.SODet>();

    public SrvOrder(int? sOIDs, string srvOrdType, int? sODetID)
    {
      this.SOID = sOIDs;
      this.srvOrdType = srvOrdType;
      this.Details = new List<SM_INReleaseProcess.SODet>();
      this.Details.Add(new SM_INReleaseProcess.SODet(sODetID));
    }

    public SrvOrder(
      int? sOIDs,
      string srvOrdType,
      int? sODetID,
      string serialNbr,
      Decimal? curyUnitCost,
      bool? IsSerialized)
    {
      this.SOID = sOIDs;
      this.srvOrdType = srvOrdType;
      this.Details = new List<SM_INReleaseProcess.SODet>();
      this.Details.Add(new SM_INReleaseProcess.SODet(sODetID, serialNbr, curyUnitCost, IsSerialized));
    }
  }

  public class INTranProcess
  {
    public FSAppointmentDet appDet;
    public FSSODet soDet;
    public INTran inTran;
    public INTranSplit inTranSplit;
  }

  public class FSSODetSplitPlanSyncOnly : ItemPlanSyncOnly<INReleaseProcess, FSSODetSplit>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
    }
  }

  public delegate void PersistDelegate();

  public delegate void ReleaseDocProcDelegate(
    JournalEntry je,
    INRegister doc,
    bool releaseFromHold);
}
