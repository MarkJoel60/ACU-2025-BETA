// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ARReleaseProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class SM_ARReleaseProcess : PXGraphExtension<ARReleaseProcess>
{
  public PXSelectJoin<PX.Objects.PO.POReceiptLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>>>>, Where<PX.Objects.PO.POOrder.sOOrderType, Equal<Required<PX.Objects.PO.POOrder.sOOrderType>>, And<PX.Objects.PO.POOrder.sOOrderNbr, Equal<Required<PX.Objects.PO.POOrder.sOOrderNbr>>>>> POReceiptLines;
  public bool processEquipmentAndComponents;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public void Persist(SM_ARReleaseProcess.PersistDelegate baseMethod)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      {
        PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.ARInvoice_DocType_RefNbr).Current;
        if (current != null)
        {
          if (current.DocType == "CRM" && current.CreatedByScreenID.Substring(0, 2) != "FS" && current.Released.GetValueOrDefault())
            this.CleanPostingInfoCreditMemo((PX.Objects.AR.ARRegister) current);
          if (PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() && this.processEquipmentAndComponents)
          {
            Dictionary<int?, int?> newEquiments = new Dictionary<int?, int?>();
            SMEquipmentMaint instance = PXGraph.CreateInstance<SMEquipmentMaint>();
            this.CreateEquipments(instance, (PX.Objects.AR.ARRegister) current, newEquiments);
            this.ReplaceEquipments(instance, (PX.Objects.AR.ARRegister) current);
            this.UpgradeEquipmentComponents(instance, (PX.Objects.AR.ARRegister) current, newEquiments);
            this.CreateEquipmentComponents(instance, (PX.Objects.AR.ARRegister) current, newEquiments);
            this.ReplaceComponents(instance, (PX.Objects.AR.ARRegister) current);
          }
        }
      }
      baseMethod();
      transactionScope.Complete();
    }
  }

  [PXOverride]
  public virtual PX.Objects.AR.ARRegister OnBeforeRelease(
    PX.Objects.AR.ARRegister ardoc,
    SM_ARReleaseProcess.OnBeforeReleaseDelegate del)
  {
    this.ValidatePostBatchStatus((PXDBOperation) 1, "AR", ardoc.DocType, ardoc.RefNbr);
    return del != null ? del(ardoc) : (PX.Objects.AR.ARRegister) null;
  }

  public virtual void CleanPostingInfoCreditMemo(PX.Objects.AR.ARRegister arRegisterRow)
  {
    if (!GraphHelper.RowCast<PX.Objects.AR.ARTran>((IEnumerable) PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) arRegisterRow.DocType,
      (object) arRegisterRow.RefNbr
    })).Where<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (_ => ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARTran)].GetExtension<FSxARTran>((object) _).IsFSRelated.GetValueOrDefault())).Any<PX.Objects.AR.ARTran>())
      return;
    PX.Objects.SO.SOInvoice crmSOInvoiceRow = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(PXSelectBase<PX.Objects.SO.SOInvoice, PXSelect<PX.Objects.SO.SOInvoice, Where<PX.Objects.SO.SOInvoice.docType, Equal<Required<PX.Objects.SO.SOInvoice.docType>>, And<PX.Objects.SO.SOInvoice.refNbr, Equal<Required<PX.Objects.SO.SOInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) arRegisterRow.DocType,
      (object) arRegisterRow.RefNbr
    }));
    if (crmSOInvoiceRow != null)
    {
      SM_SOInvoiceEntry extension = ((PXGraph) PXGraph.CreateInstance<SOInvoiceEntry>()).GetExtension<SM_SOInvoiceEntry>();
      extension.CleanPostingInfoFromSOCreditMemo((PXGraph) this.Base, crmSOInvoiceRow);
      extension.CreateBillHistoryRowsForDocument((PXGraph) this.Base, "PXSM", arRegisterRow.DocType, arRegisterRow.RefNbr, (string) null, (string) null, (string) null);
    }
    else
    {
      SM_ARInvoiceEntry extension = ((PXGraph) PXGraph.CreateInstance<ARInvoiceEntry>()).GetExtension<SM_ARInvoiceEntry>();
      PX.Objects.AR.ARInvoice arInvoice = PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(((IQueryable<PXResult<PX.Objects.AR.ARInvoice>>) PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) arRegisterRow.OrigDocType,
        (object) arRegisterRow.OrigRefNbr
      })).FirstOrDefault<PXResult<PX.Objects.AR.ARInvoice>>());
      Decimal? origDocAmt1 = arInvoice.OrigDocAmt;
      Decimal? origDocAmt2 = arRegisterRow.OrigDocAmt;
      if (!(origDocAmt1.GetValueOrDefault() == origDocAmt2.GetValueOrDefault() & origDocAmt1.HasValue == origDocAmt2.HasValue) || !(arInvoice.CuryID == arRegisterRow.CuryID))
        return;
      extension.CleanPostingInfoLinkedToDoc((object) arInvoice);
      extension.CleanContractPostingInfoLinkedToDoc((object) arInvoice);
      extension.CreateBillHistoryRowsForDocument((PXGraph) this.Base, "PXAM", arRegisterRow.DocType, arRegisterRow.RefNbr, "PXAR", arRegisterRow.OrigDocType, arRegisterRow.OrigRefNbr);
    }
  }

  public virtual void CreateEquipments(
    SMEquipmentMaint graphSMEquipmentMaint,
    PX.Objects.AR.ARRegister arRegisterRow,
    Dictionary<int?, int?> newEquiments)
  {
    PXResultset<PX.Objects.IN.InventoryItem> arTranLines = PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.AR.ARTran.tranType, Equal<ARDocType.invoice>>>, LeftJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.AR.ARTran.sOOrderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.AR.ARTran.sOOrderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>, LeftJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSxARTran.serviceOrderRefNbr>>>, LeftJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSAppointment.refNbr, Equal<FSxARTran.appointmentRefNbr>>>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<FSxEquipmentModel.eQEnabled, Equal<True>, And<FSxARTran.equipmentAction, Equal<ListField_EquipmentActionBase.SellingTargetEquipment>, And<FSxARTran.sMEquipmentID, IsNull, And<FSxARTran.newEquipmentLineNbr, IsNull, And<FSxARTran.componentID, IsNull>>>>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) arRegisterRow.DocType,
      (object) arRegisterRow.RefNbr
    });
    this.Create_Replace_Equipments(graphSMEquipmentMaint, arTranLines, arRegisterRow, newEquiments, "ST");
  }

  public virtual void UpgradeEquipmentComponents(
    SMEquipmentMaint graphSMEquipmentMaint,
    PX.Objects.AR.ARRegister arRegisterRow,
    Dictionary<int?, int?> newEquiments)
  {
    foreach (PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment> pxResult in PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.AR.ARTran.tranType, Equal<ARDocType.invoice>>>, LeftJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.AR.ARTran.sOOrderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.AR.ARTran.sOOrderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>, LeftJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSxARTran.serviceOrderRefNbr>>>, LeftJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSAppointment.refNbr, Equal<FSxARTran.appointmentRefNbr>>>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<FSxARTran.equipmentAction, Equal<ListField_EquipmentAction.UpgradingComponent>, And<FSxARTran.sMEquipmentID, IsNull, And<FSxARTran.newEquipmentLineNbr, IsNotNull, And<FSxARTran.componentID, IsNotNull, And<FSxARTran.equipmentComponentLineNbr, IsNull>>>>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) arRegisterRow.DocType,
      (object) arRegisterRow.RefNbr
    }))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSxARTran fsxARTranRow = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARTran)].GetExtension<FSxARTran>((object) arTran);
      FSServiceOrder fsServiceOrder = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSAppointment fsAppointment = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      int? nullable1 = new int?(-1);
      if (newEquiments.TryGetValue(fsxARTranRow.NewEquipmentLineNbr, out nullable1))
      {
        ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current = PXResultset<FSEquipment>.op_Implicit(((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Search<FSEquipment.SMequipmentID>((object) nullable1, Array.Empty<object>()));
        FSEquipmentComponent equipmentComponent1 = PXResult<FSEquipmentComponent>.op_Implicit(((IQueryable<PXResult<FSEquipmentComponent>>) ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).Select(Array.Empty<object>())).Where<PXResult<FSEquipmentComponent>>((Expression<Func<PXResult<FSEquipmentComponent>, bool>>) (x => ((FSEquipmentComponent) x).ComponentID == fsxARTranRow.ComponentID)).FirstOrDefault<PXResult<FSEquipmentComponent>>());
        if (equipmentComponent1 != null)
        {
          equipmentComponent1.SalesOrderNbr = arTran.SOOrderNbr;
          equipmentComponent1.SalesOrderType = arTran.SOOrderType;
          equipmentComponent1.LongDescr = arTran.TranDesc;
          equipmentComponent1.InvoiceRefNbr = arTran.RefNbr;
          FSEquipmentComponent equipmentComponent2 = equipmentComponent1;
          DateTime? nullable2 = arTran.TranDate;
          DateTime? nullable3 = nullable2.HasValue ? arTran.TranDate : arRegisterRow.DocDate;
          equipmentComponent2.InstallationDate = nullable3;
          if (fsxARTranRow != null)
          {
            if (!string.IsNullOrEmpty(fsxARTranRow.AppointmentRefNbr))
            {
              equipmentComponent1.InstSrvOrdType = fsxARTranRow.SrvOrdType;
              equipmentComponent1.InstAppointmentRefNbr = fsxARTranRow.AppointmentRefNbr;
              FSEquipmentComponent equipmentComponent3 = equipmentComponent1;
              DateTime? nullable4;
              if (fsAppointment == null)
              {
                nullable2 = new DateTime?();
                nullable4 = nullable2;
              }
              else
                nullable4 = fsAppointment.ExecutionDate;
              equipmentComponent3.InstallationDate = nullable4;
            }
            else if (!string.IsNullOrEmpty(fsxARTranRow.ServiceOrderRefNbr))
            {
              equipmentComponent1.InstSrvOrdType = fsxARTranRow.SrvOrdType;
              equipmentComponent1.InstServiceOrderRefNbr = fsxARTranRow.ServiceOrderRefNbr;
              FSEquipmentComponent equipmentComponent4 = equipmentComponent1;
              DateTime? nullable5;
              if (fsServiceOrder == null)
              {
                nullable2 = new DateTime?();
                nullable5 = nullable2;
              }
              else
                nullable5 = fsServiceOrder.OrderDate;
              equipmentComponent4.InstallationDate = nullable5;
            }
            equipmentComponent1.Comment = fsxARTranRow.Comment;
          }
          equipmentComponent1.SerialNumber = arTran.LotSerialNbr;
          FSEquipmentComponent equipmentComponent5 = ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).Update(equipmentComponent1);
          ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).SetValueExt<FSEquipmentComponent.inventoryID>(equipmentComponent5, (object) arTran.InventoryID);
          PXSelect<FSEquipmentComponent, Where<FSEquipmentComponent.SMequipmentID, Equal<Current<FSEquipment.SMequipmentID>>>, OrderBy<Asc<FSEquipmentComponent.lineNbr>>> equipmentWarranties = graphSMEquipmentMaint.EquipmentWarranties;
          FSEquipmentComponent equipmentComponent6 = equipmentComponent5;
          DateTime? nullable6;
          if (soLine != null)
          {
            nullable2 = soLine.OrderDate;
            if (nullable2.HasValue)
            {
              nullable6 = soLine.OrderDate;
              goto label_20;
            }
          }
          nullable6 = arTran.TranDate;
label_20:
          // ISSUE: variable of a boxed type
          __Boxed<DateTime?> local = (ValueType) nullable6;
          ((PXSelectBase<FSEquipmentComponent>) equipmentWarranties).SetValueExt<FSEquipmentComponent.salesDate>(equipmentComponent6, (object) local);
          ((PXAction) graphSMEquipmentMaint.Save).Press();
        }
      }
    }
  }

  public virtual void CreateEquipmentComponents(
    SMEquipmentMaint graphSMEquipmentMaint,
    PX.Objects.AR.ARRegister arRegisterRow,
    Dictionary<int?, int?> newEquiments)
  {
    foreach (PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment> pxResult in PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.AR.ARTran.tranType, Equal<ARDocType.invoice>>>, LeftJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.AR.ARTran.sOOrderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.AR.ARTran.sOOrderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>, LeftJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSxARTran.serviceOrderRefNbr>>>, LeftJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSAppointment.refNbr, Equal<FSxARTran.appointmentRefNbr>>>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<FSxARTran.equipmentAction, Equal<ListField_EquipmentAction.CreatingComponent>, And<FSxARTran.componentID, IsNotNull, And<FSxARTran.equipmentComponentLineNbr, IsNull>>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) arRegisterRow.DocType,
      (object) arRegisterRow.RefNbr
    }))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSxARTran extension = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARTran)].GetExtension<FSxARTran>((object) arTran);
      PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSServiceOrder fsServiceOrder = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSAppointment fsAppointment = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      int? nullable1 = new int?(-1);
      int? nullable2 = extension.NewEquipmentLineNbr;
      if (nullable2.HasValue)
      {
        nullable2 = extension.SMEquipmentID;
        if (!nullable2.HasValue && newEquiments.TryGetValue(extension.NewEquipmentLineNbr, out nullable1))
          ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current = PXResultset<FSEquipment>.op_Implicit(((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Search<FSEquipment.SMequipmentID>((object) nullable1, Array.Empty<object>()));
      }
      nullable2 = extension.NewEquipmentLineNbr;
      if (!nullable2.HasValue)
      {
        nullable2 = extension.SMEquipmentID;
        if (nullable2.HasValue)
          ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current = PXResultset<FSEquipment>.op_Implicit(((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Search<FSEquipment.SMequipmentID>((object) extension.SMEquipmentID, Array.Empty<object>()));
      }
      if (((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current != null)
      {
        FSEquipmentComponent equipmentComponent1 = ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).Insert(new FSEquipmentComponent()
        {
          ComponentID = extension.ComponentID
        });
        equipmentComponent1.SalesOrderNbr = arTran.SOOrderNbr;
        equipmentComponent1.SalesOrderType = arTran.SOOrderType;
        equipmentComponent1.InvoiceRefNbr = arTran.RefNbr;
        FSEquipmentComponent equipmentComponent2 = equipmentComponent1;
        DateTime? nullable3 = arTran.TranDate;
        DateTime? nullable4 = nullable3.HasValue ? arTran.TranDate : arRegisterRow.DocDate;
        equipmentComponent2.InstallationDate = nullable4;
        if (!string.IsNullOrEmpty(extension.AppointmentRefNbr))
        {
          equipmentComponent1.InstSrvOrdType = extension.SrvOrdType;
          equipmentComponent1.InstAppointmentRefNbr = extension.AppointmentRefNbr;
          FSEquipmentComponent equipmentComponent3 = equipmentComponent1;
          DateTime? nullable5;
          if (fsAppointment == null)
          {
            nullable3 = new DateTime?();
            nullable5 = nullable3;
          }
          else
            nullable5 = fsAppointment.ExecutionDate;
          equipmentComponent3.InstallationDate = nullable5;
        }
        else if (!string.IsNullOrEmpty(extension.ServiceOrderRefNbr))
        {
          equipmentComponent1.InstSrvOrdType = extension.SrvOrdType;
          equipmentComponent1.InstServiceOrderRefNbr = extension.ServiceOrderRefNbr;
          FSEquipmentComponent equipmentComponent4 = equipmentComponent1;
          DateTime? nullable6;
          if (fsServiceOrder == null)
          {
            nullable3 = new DateTime?();
            nullable6 = nullable3;
          }
          else
            nullable6 = fsServiceOrder.OrderDate;
          equipmentComponent4.InstallationDate = nullable6;
        }
        equipmentComponent1.Comment = extension.Comment;
        equipmentComponent1.SerialNumber = arTran.LotSerialNbr;
        FSEquipmentComponent equipmentComponent5 = ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).Update(equipmentComponent1);
        ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).SetValueExt<FSEquipmentComponent.inventoryID>(equipmentComponent5, (object) arTran.InventoryID);
        PXSelect<FSEquipmentComponent, Where<FSEquipmentComponent.SMequipmentID, Equal<Current<FSEquipment.SMequipmentID>>>, OrderBy<Asc<FSEquipmentComponent.lineNbr>>> equipmentWarranties = graphSMEquipmentMaint.EquipmentWarranties;
        FSEquipmentComponent equipmentComponent6 = equipmentComponent5;
        DateTime? nullable7;
        if (soLine != null)
        {
          nullable3 = soLine.OrderDate;
          if (nullable3.HasValue)
          {
            nullable7 = soLine.OrderDate;
            goto label_23;
          }
        }
        nullable7 = arTran.TranDate;
label_23:
        // ISSUE: variable of a boxed type
        __Boxed<DateTime?> local = (ValueType) nullable7;
        ((PXSelectBase<FSEquipmentComponent>) equipmentWarranties).SetValueExt<FSEquipmentComponent.salesDate>(equipmentComponent6, (object) local);
        ((PXAction) graphSMEquipmentMaint.Save).Press();
      }
    }
  }

  public virtual void ReplaceEquipments(
    SMEquipmentMaint graphSMEquipmentMaint,
    PX.Objects.AR.ARRegister arRegisterRow)
  {
    PXResultset<PX.Objects.IN.InventoryItem> arTranLines = PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.AR.ARTran.tranType, Equal<ARDocType.invoice>>>, LeftJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.AR.ARTran.sOOrderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.AR.ARTran.sOOrderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>, LeftJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSxARTran.serviceOrderRefNbr>>>, LeftJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSAppointment.refNbr, Equal<FSxARTran.appointmentRefNbr>>>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<FSxEquipmentModel.eQEnabled, Equal<True>, And<FSxARTran.equipmentAction, Equal<ListField_EquipmentAction.ReplacingTargetEquipment>, And<FSxARTran.sMEquipmentID, IsNotNull, And<FSxARTran.newEquipmentLineNbr, IsNull, And<FSxARTran.componentID, IsNull>>>>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) arRegisterRow.DocType,
      (object) arRegisterRow.RefNbr
    });
    this.Create_Replace_Equipments(graphSMEquipmentMaint, arTranLines, arRegisterRow, (Dictionary<int?, int?>) null, "RT");
  }

  public virtual void ReplaceComponents(
    SMEquipmentMaint graphSMEquipmentMaint,
    PX.Objects.AR.ARRegister arRegisterRow)
  {
    foreach (PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment> pxResult in PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.AR.ARTran.tranType, Equal<ARDocType.invoice>>>, LeftJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.AR.ARTran.sOOrderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.AR.ARTran.sOOrderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>, LeftJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSxARTran.serviceOrderRefNbr>>>, LeftJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSxARTran.srvOrdType>, And<FSAppointment.refNbr, Equal<FSxARTran.appointmentRefNbr>>>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<FSxEquipmentModel.eQEnabled, Equal<True>, And<FSxARTran.equipmentAction, Equal<ListField_EquipmentActionBase.ReplacingComponent>, And<FSxARTran.sMEquipmentID, IsNotNull, And<FSxARTran.newEquipmentLineNbr, IsNull, And<FSxARTran.equipmentComponentLineNbr, IsNotNull>>>>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) arRegisterRow.DocType,
      (object) arRegisterRow.RefNbr
    }))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSxARTran fsxARTranRow = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARTran)].GetExtension<FSxARTran>((object) arTran);
      FSServiceOrder fsServiceOrder = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSAppointment fsAppointment = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current = PXResultset<FSEquipment>.op_Implicit(((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Search<FSEquipment.SMequipmentID>((object) fsxARTranRow.SMEquipmentID, Array.Empty<object>()));
      FSEquipmentComponent replacedCompt = PXResult<FSEquipmentComponent>.op_Implicit(((IQueryable<PXResult<FSEquipmentComponent>>) ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).Select(Array.Empty<object>())).Where<PXResult<FSEquipmentComponent>>((Expression<Func<PXResult<FSEquipmentComponent>, bool>>) (x => ((FSEquipmentComponent) x).LineNbr == fsxARTranRow.EquipmentComponentLineNbr)).FirstOrDefault<PXResult<FSEquipmentComponent>>());
      FSEquipmentComponent equipmentComponent1 = graphSMEquipmentMaint.ApplyComponentReplacement(replacedCompt, new FSEquipmentComponent()
      {
        ComponentID = fsxARTranRow.ComponentID
      });
      equipmentComponent1.SalesOrderNbr = arTran.SOOrderNbr;
      equipmentComponent1.SalesOrderType = arTran.SOOrderType;
      equipmentComponent1.InvoiceRefNbr = arTran.RefNbr;
      FSEquipmentComponent equipmentComponent2 = equipmentComponent1;
      DateTime? nullable1 = arTran.TranDate;
      DateTime? nullable2 = nullable1.HasValue ? arTran.TranDate : arRegisterRow.DocDate;
      equipmentComponent2.InstallationDate = nullable2;
      if (fsxARTranRow != null)
      {
        if (!string.IsNullOrEmpty(fsxARTranRow.AppointmentRefNbr))
        {
          equipmentComponent1.InstSrvOrdType = fsxARTranRow.SrvOrdType;
          equipmentComponent1.InstAppointmentRefNbr = fsxARTranRow.AppointmentRefNbr;
          FSEquipmentComponent equipmentComponent3 = equipmentComponent1;
          DateTime? nullable3;
          if (fsAppointment == null)
          {
            nullable1 = new DateTime?();
            nullable3 = nullable1;
          }
          else
            nullable3 = fsAppointment.ExecutionDate;
          equipmentComponent3.InstallationDate = nullable3;
        }
        else if (!string.IsNullOrEmpty(fsxARTranRow.ServiceOrderRefNbr))
        {
          equipmentComponent1.InstSrvOrdType = fsxARTranRow.SrvOrdType;
          equipmentComponent1.InstServiceOrderRefNbr = fsxARTranRow.ServiceOrderRefNbr;
          FSEquipmentComponent equipmentComponent4 = equipmentComponent1;
          DateTime? nullable4;
          if (fsServiceOrder == null)
          {
            nullable1 = new DateTime?();
            nullable4 = nullable1;
          }
          else
            nullable4 = fsServiceOrder.OrderDate;
          equipmentComponent4.InstallationDate = nullable4;
        }
        equipmentComponent1.Comment = fsxARTranRow.Comment;
      }
      equipmentComponent1.LongDescr = arTran.TranDesc;
      equipmentComponent1.SerialNumber = arTran.LotSerialNbr;
      FSEquipmentComponent equipmentComponent5 = ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).Update(equipmentComponent1);
      ((PXSelectBase<FSEquipmentComponent>) graphSMEquipmentMaint.EquipmentWarranties).SetValueExt<FSEquipmentComponent.inventoryID>(equipmentComponent5, (object) arTran.InventoryID);
      PXSelect<FSEquipmentComponent, Where<FSEquipmentComponent.SMequipmentID, Equal<Current<FSEquipment.SMequipmentID>>>, OrderBy<Asc<FSEquipmentComponent.lineNbr>>> equipmentWarranties = graphSMEquipmentMaint.EquipmentWarranties;
      FSEquipmentComponent equipmentComponent6 = equipmentComponent5;
      DateTime? nullable5;
      if (soLine != null)
      {
        nullable1 = soLine.OrderDate;
        if (nullable1.HasValue)
        {
          nullable5 = soLine.OrderDate;
          goto label_18;
        }
      }
      nullable5 = arTran.TranDate;
label_18:
      // ISSUE: variable of a boxed type
      __Boxed<DateTime?> local = (ValueType) nullable5;
      ((PXSelectBase<FSEquipmentComponent>) equipmentWarranties).SetValueExt<FSEquipmentComponent.salesDate>(equipmentComponent6, (object) local);
      ((PXAction) graphSMEquipmentMaint.Save).Press();
    }
  }

  public virtual void Create_Replace_Equipments(
    SMEquipmentMaint graphSMEquipmentMaint,
    PXResultset<PX.Objects.IN.InventoryItem> arTranLines,
    PX.Objects.AR.ARRegister arRegisterRow,
    Dictionary<int?, int?> newEquiments,
    string action)
  {
    PXCache cach = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARTran)];
    bool flag = false;
    foreach (PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment> arTranLine in arTranLines)
    {
      PX.Objects.AR.ARTran arTran1 = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(arTranLine);
      PX.Objects.AR.ARTran arTran2 = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
      {
        (object) arTran1.TranType,
        (object) arTran1.RefNbr,
        (object) arTran1.LineNbr
      }));
      PX.Objects.IN.InventoryItem inventoryItemRow = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(arTranLine);
      PX.Objects.SO.SOLine soLineRow = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(arTranLine);
      FSServiceOrder fsServiceOrder = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(arTranLine);
      FSAppointment fsAppointment = PXResult<PX.Objects.IN.InventoryItem, PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, FSServiceOrder, FSAppointment>.op_Implicit(arTranLine);
      FSEquipment fsEquipment = (FSEquipment) null;
      FSxEquipmentModel extension1 = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxEquipmentModel>(inventoryItemRow);
      FSxARTran extension2 = cach.GetExtension<FSxARTran>((object) arTran2);
      List<SM_ARReleaseProcess.ItemInfo> source = this.GetDifferentItemList((PXGraph) this.Base, arTran2, true);
      PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryItem)], inventoryItemRow.InventoryID);
      if (!source.Any<SM_ARReleaseProcess.ItemInfo>() || PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "S" && soLineRow.POSource == "D")
        source = this.GetItemListFromPOReceipt(soLineRow);
      DateTime? nullable1;
      foreach (SM_ARReleaseProcess.ItemInfo itemInfo in source)
      {
        SoldInventoryItem soldInventoryItemRow = new SoldInventoryItem();
        soldInventoryItemRow.CustomerID = arRegisterRow.CustomerID;
        soldInventoryItemRow.CustomerLocationID = arRegisterRow.CustomerLocationID;
        soldInventoryItemRow.InventoryID = inventoryItemRow.InventoryID;
        soldInventoryItemRow.InventoryCD = inventoryItemRow.InventoryCD;
        soldInventoryItemRow.InvoiceRefNbr = arTran2.RefNbr;
        soldInventoryItemRow.InvoiceLineNbr = arTran2.LineNbr;
        soldInventoryItemRow.DocType = arRegisterRow.DocType;
        SoldInventoryItem soldInventoryItem = soldInventoryItemRow;
        nullable1 = arTran2.TranDate;
        DateTime? nullable2 = nullable1.HasValue ? arTran2.TranDate : arRegisterRow.DocDate;
        soldInventoryItem.DocDate = nullable2;
        if (!string.IsNullOrEmpty(extension2.AppointmentRefNbr))
          soldInventoryItemRow.DocDate = fsAppointment.ExecutionDate;
        else if (!string.IsNullOrEmpty(extension2.ServiceOrderRefNbr))
          soldInventoryItemRow.DocDate = fsServiceOrder.OrderDate;
        soldInventoryItemRow.Descr = inventoryItemRow.Descr;
        soldInventoryItemRow.SiteID = arTran2.SiteID;
        soldInventoryItemRow.ItemClassID = inventoryItemRow.ItemClassID;
        soldInventoryItemRow.SOOrderType = arTran2.SOOrderType;
        soldInventoryItemRow.SOOrderNbr = arTran2.SOOrderNbr;
        soldInventoryItemRow.SOOrderDate = soLineRow.OrderDate;
        soldInventoryItemRow.EquipmentTypeID = extension1.EquipmentTypeID;
        soldInventoryItemRow.LotSerialNumber = itemInfo.LotSerialNbr;
        fsEquipment = SharedFunctions.CreateSoldEquipment(graphSMEquipmentMaint, soldInventoryItemRow, arTran2, extension2, soLineRow, action, inventoryItemRow);
      }
      if (fsEquipment != null)
      {
        if (!extension2.ReplaceSMEquipmentID.HasValue && action == "RT")
          extension2.ReplaceSMEquipmentID = extension2.SMEquipmentID;
        extension2.SMEquipmentID = fsEquipment.SMEquipmentID;
        PX.Objects.AR.ARTran arTran3 = (PX.Objects.AR.ARTran) cach.Update((object) arTran2);
        flag = true;
        if (action == "ST")
        {
          int? nullable3 = new int?(-1);
          if (!newEquiments.TryGetValue(arTran3.LineNbr, out nullable3))
            newEquiments.Add(arTran3.LineNbr, fsEquipment.SMEquipmentID);
        }
        else if (action == "RT" && extension2 != null)
        {
          ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current = PXResultset<FSEquipment>.op_Implicit(((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Search<FSEquipment.SMequipmentID>((object) extension2.ReplaceSMEquipmentID, Array.Empty<object>()));
          ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current.ReplaceEquipmentID = fsEquipment.SMEquipmentID;
          ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current.Status = "D";
          FSEquipment current = ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current;
          nullable1 = soLineRow.OrderDate;
          DateTime? nullable4 = nullable1.HasValue ? soLineRow.OrderDate : arTran3.TranDate;
          current.DisposalDate = nullable4;
          ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current.DispSrvOrdType = extension2.SrvOrdType;
          ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current.DispServiceOrderRefNbr = extension2.ServiceOrderRefNbr;
          ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current.DispAppointmentRefNbr = extension2.AppointmentRefNbr;
          ((PXSelectBase) graphSMEquipmentMaint.EquipmentRecords).Cache.SetStatus((object) ((PXSelectBase<FSEquipment>) graphSMEquipmentMaint.EquipmentRecords).Current, (PXEntryStatus) 1);
          ((PXAction) graphSMEquipmentMaint.Save).Press();
        }
      }
    }
    if (!flag)
      return;
    cach.Persist((PXDBOperation) 1);
  }

  public virtual List<SM_ARReleaseProcess.ItemInfo> GetDifferentItemList(
    PXGraph graph,
    PX.Objects.AR.ARTran arTran,
    bool createDifferentEntriesForQtyGreaterThan1)
  {
    return SharedFunctions.GetDifferentItemList(graph, arTran, createDifferentEntriesForQtyGreaterThan1);
  }

  public virtual List<SM_ARReleaseProcess.ItemInfo> GetVerifiedDifferentItemList(
    PXGraph graph,
    PX.Objects.AR.ARTran arTran,
    List<SM_ARReleaseProcess.ItemInfo> lotSerialList)
  {
    return SharedFunctions.GetVerifiedDifferentItemList(graph, arTran, lotSerialList);
  }

  /// <summary>Workaround method to obtain serial numbers</summary>
  /// <param name="soLineRow"></param>
  /// <param name="arTran"></param>
  /// <returns></returns>
  private List<SM_ARReleaseProcess.ItemInfo> GetItemListFromPOReceipt(PX.Objects.SO.SOLine soLineRow)
  {
    return ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>) ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.POReceiptLines).Select(new object[2]
    {
      (object) soLineRow.OrderType,
      (object) soLineRow.OrderNbr
    })).ToList<PXResult<PX.Objects.PO.POReceiptLine>>().Select<PXResult<PX.Objects.PO.POReceiptLine>, SM_ARReleaseProcess.ItemInfo>((Func<PXResult<PX.Objects.PO.POReceiptLine>, SM_ARReleaseProcess.ItemInfo>) (r => new SM_ARReleaseProcess.ItemInfo(PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(r)))).ToList<SM_ARReleaseProcess.ItemInfo>();
  }

  protected virtual PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> ReadInventoryItem(
    PXCache sender,
    int? inventoryID)
  {
    if (!inventoryID.HasValue)
      return (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>) null;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
    if (inventoryItem == null)
      throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
      {
        (object) "Inventory Item",
        (object) inventoryID
      });
    INLotSerClass inLotSerClass;
    if (inventoryItem.StkItem.GetValueOrDefault())
    {
      inLotSerClass = INLotSerClass.PK.Find(sender.Graph, inventoryItem.LotSerClassID);
      if (inLotSerClass == null)
        throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
        {
          (object) "Lot/Serial Class",
          (object) inventoryItem.LotSerClassID
        });
    }
    else
      inLotSerClass = new INLotSerClass();
    return new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(inventoryItem, inLotSerClass);
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<PX.Objects.AR.ARRegister>((PXGraph) this.Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }

  public class ItemInfo
  {
    public virtual string LotSerialNbr { get; set; }

    public virtual string UOM { get; set; }

    public virtual Decimal? Qty { get; set; }

    public virtual Decimal? BaseQty { get; set; }

    public ItemInfo(SOShipLineSplit split)
    {
      this.LotSerialNbr = split.LotSerialNbr;
      this.UOM = split.UOM;
      this.Qty = split.Qty;
      this.BaseQty = split.BaseQty;
    }

    public ItemInfo(PX.Objects.SO.SOLineSplit split)
    {
      this.LotSerialNbr = split.LotSerialNbr;
      this.UOM = split.UOM;
      this.Qty = split.Qty;
      this.BaseQty = split.BaseQty;
    }

    public ItemInfo(PX.Objects.AR.ARTran arTran)
    {
      this.LotSerialNbr = arTran.LotSerialNbr;
      this.UOM = arTran.UOM;
      this.Qty = arTran.Qty;
      this.BaseQty = arTran.BaseQty;
    }

    public ItemInfo(PX.Objects.PO.POReceiptLine poRLine)
    {
      this.LotSerialNbr = poRLine.LotSerialNbr;
      this.UOM = poRLine.UOM;
      this.Qty = poRLine.Qty;
      this.BaseQty = poRLine.BaseQty;
    }
  }

  public delegate void PersistDelegate();

  public delegate PX.Objects.AR.ARRegister OnBeforeReleaseDelegate(PX.Objects.AR.ARRegister ardoc);
}
