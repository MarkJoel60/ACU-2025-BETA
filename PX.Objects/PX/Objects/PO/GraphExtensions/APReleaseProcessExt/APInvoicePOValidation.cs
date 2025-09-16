// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APReleaseProcessExt.APInvoicePOValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APReleaseProcessExt;

public class APInvoicePOValidation : 
  PXGraphExtension<UpdatePOOnRelease, APReleaseProcess.MultiCurrency, APReleaseProcess>
{
  public PXSelect<POLineBillingRevision> POLineRevision;
  private APInvoicePOValidationService _validationService;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual APInvoicePOValidationService GetValidationService(Lazy<POSetup> poSetup)
  {
    if (this._validationService == null)
      this._validationService = new APInvoicePOValidationService(poSetup);
    return this._validationService;
  }

  public void DeletePOLineRevision(PX.Objects.AP.APInvoice apdoc, PX.Objects.PO.POLine srcLine, PX.Objects.PO.POOrder srcDoc)
  {
    if (((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck)
      return;
    PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.APInvoicePOValidation.POLineDTO poLine = new PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.APInvoicePOValidation.POLineDTO(srcLine, srcDoc.CuryID);
    this.DeletePOLineRevision(apdoc, poLine);
  }

  [PXOverride]
  public virtual PX.Objects.PO.POLine UpdatePOLine(
    PX.Objects.AP.APTran tran,
    PX.Objects.AP.APInvoice apdoc,
    PX.Objects.PO.POOrder srcDoc,
    PX.Objects.PO.POLine updLine,
    bool isPrebooking,
    APInvoicePOValidation.updatePOLineDelegate baseMethod)
  {
    if (!((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck && !isPrebooking)
    {
      APInvoicePOValidationService validationService = this.GetValidationService(Lazy.By<POSetup>((Func<POSetup>) (() => ((PXGraphExtension<APReleaseProcess>) this).Base.posetup)));
      if (validationService.IsLineValidationRequired(tran))
      {
        PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.APInvoicePOValidation.POLineDTO poLine = new PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.APInvoicePOValidation.POLineDTO((PX.Objects.PO.POLine) PrimaryKeyOf<PX.Objects.PO.POLine>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POLine.orderType, PX.Objects.PO.POLine.orderNbr, PX.Objects.PO.POLine.lineNbr>>.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POLine.orderType, PX.Objects.PO.POLine.orderNbr, PX.Objects.PO.POLine.lineNbr>) updLine, (PKFindOptions) 0), srcDoc.CuryID);
        if (validationService.ShouldCreateRevision(((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base).Caches[typeof (PX.Objects.AP.APTran)], tran, apdoc.CuryID, poLine))
          this.SavePOLineRevision(apdoc, poLine);
      }
    }
    return baseMethod(tran, apdoc, srcDoc, updLine, isPrebooking);
  }

  [PXOverride]
  public void PerformPersist(PXGraph.IPersistPerformer persister)
  {
    persister.Insert(((PXSelectBase) this.POLineRevision).Cache);
    persister.Delete(((PXSelectBase) this.POLineRevision).Cache);
  }

  public virtual void SavePOLineRevision(PX.Objects.AP.APInvoice apdoc, PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.APInvoicePOValidation.POLineDTO poLine)
  {
    POLineBillingRevision revision = new POLineBillingRevision();
    revision.APDocType = apdoc.DocType;
    revision.APRefNbr = apdoc.RefNbr;
    revision.LineType = poLine.LineType;
    revision.OrderType = poLine.OrderType;
    revision.OrderNbr = poLine.OrderNbr;
    revision.OrderLineNbr = poLine.OrderLineNbr;
    if (this.FindPOLineRevision(revision) != null)
      return;
    revision.CuryID = poLine.CuryID;
    revision.InventoryID = poLine.InventoryID;
    revision.UOM = poLine.UOM;
    revision.OrderQty = poLine.OrderQty;
    revision.BaseOrderQty = poLine.BaseOrderQty;
    revision.ReceivedQty = poLine.ReceivedQty;
    revision.BaseReceivedQty = poLine.BaseReceivedQty;
    revision.RcptQtyMax = poLine.RcptQtyMax;
    revision.UnbilledQty = poLine.UnbilledQty;
    revision.BaseUnbilledQty = poLine.BaseUnbilledQty;
    revision.CuryUnbilledAmt = poLine.CuryUnbilledAmt;
    revision.UnbilledAmt = poLine.UnbilledAmt;
    revision.CuryUnitCost = poLine.CuryUnitCost;
    revision.UnitCost = poLine.UnitCost;
    ((PXSelectBase<POLineBillingRevision>) this.POLineRevision).Insert(revision);
  }

  public virtual void DeletePOLineRevision(PX.Objects.AP.APInvoice apdoc, PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.APInvoicePOValidation.POLineDTO poLine)
  {
    POLineBillingRevision poLineRevision = this.FindPOLineRevision(new POLineBillingRevision()
    {
      APDocType = apdoc.DocType,
      APRefNbr = apdoc.RefNbr,
      OrderType = poLine.OrderType,
      OrderNbr = poLine.OrderNbr,
      OrderLineNbr = poLine.OrderLineNbr
    });
    if (poLineRevision == null)
      return;
    ((PXSelectBase<POLineBillingRevision>) this.POLineRevision).Delete(poLineRevision);
  }

  private POLineBillingRevision FindPOLineRevision(POLineBillingRevision revision)
  {
    return PXResultset<POLineBillingRevision>.op_Implicit(((PXSelectBase<POLineBillingRevision>) this.POLineRevision).Search<POLineBillingRevision.apDocType, POLineBillingRevision.apRefNbr, POLineBillingRevision.orderType, POLineBillingRevision.orderNbr, POLineBillingRevision.orderLineNbr>((object) revision.APDocType, (object) revision.APRefNbr, (object) revision.OrderType, (object) revision.OrderNbr, (object) revision.OrderLineNbr, Array.Empty<object>()));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<POLineBillingRevision> e)
  {
    POLineBillingRevision row = e.Row;
    if (row == null)
      return;
    bool flag1 = POLineType.IsStock(row.LineType);
    bool flag2 = POLineType.IsNonStock(row.LineType);
    PXDefaultAttribute.SetPersistingCheck<POLineBillingRevision.uOM>(((PXSelectBase) this.POLineRevision).Cache, (object) row, flag1 | flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public delegate PX.Objects.PO.POLine updatePOLineDelegate(
    PX.Objects.AP.APTran tran,
    PX.Objects.AP.APInvoice apdoc,
    PX.Objects.PO.POOrder srcDoc,
    PX.Objects.PO.POLine updLine,
    bool isPrebooking);
}
