// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.SOCreatePurchaseReceiptProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO;

public class SOCreatePurchaseReceiptProcess : PXGraph<SOCreatePurchaseReceiptProcess>
{
  public virtual void GeneratePurchaseReceiptsFromShipment(
    List<PX.Objects.SO.SOShipment> shipmentList,
    SOForPurchaseReceiptFilter filter)
  {
    SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
    foreach (PX.Objects.SO.SOShipment shipment in shipmentList)
      SOCreatePurchaseReceiptProcess.SetProcessingResult(this.GeneratePurchaseReceiptFromShipment(instance, shipment, filter));
  }

  public virtual ProcessingResult GeneratePurchaseReceiptFromShipment(
    SOShipmentEntry shipmentEntry,
    PX.Objects.SO.SOShipment shipment,
    SOForPurchaseReceiptFilter filter)
  {
    ProcessingResult receiptFromShipment = new ProcessingResult();
    PXProcessing<PX.Objects.SO.SOShipment>.SetCurrentItem((object) shipment);
    ((PXGraph) shipmentEntry).Clear();
    if (shipment.Excluded.GetValueOrDefault())
    {
      ((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentEntry.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentEntry.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) shipment.ShipmentNbr, Array.Empty<object>()));
      try
      {
        ((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentEntry.Document).Current.ExcludeFromIntercompanyProc = new bool?(true);
        ((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentEntry.Document).UpdateCurrent();
        ((PXAction) shipmentEntry.Save).Press();
      }
      catch (Exception ex)
      {
        receiptFromShipment.AddErrorMessage(ex.Message);
      }
      return receiptFromShipment;
    }
    List<PXResult<SOShipLine, PX.Objects.SO.SOLine>> list = ((IEnumerable<PXResult<SOShipLine>>) PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLine>.On<SOShipLine.FK.OrderLine>>>.Where<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, SOShipLine>, PX.Objects.SO.SOShipment, SOShipLine>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) shipment
    }, Array.Empty<object>())).AsEnumerable<PXResult<SOShipLine>>().Cast<PXResult<SOShipLine, PX.Objects.SO.SOLine>>().ToList<PXResult<SOShipLine, PX.Objects.SO.SOLine>>();
    try
    {
      POReceipt intercompanyPoReceipt = ((PXGraph) shipmentEntry).GetExtension<Intercompany>().GenerateIntercompanyPOReceipt(shipment, list, filter.PutReceiptsOnHold, new DateTime?());
      shipment.IntercompanyPOReceiptNbr = intercompanyPoReceipt.ReceiptNbr;
      receiptFromShipment = ProcessingResultBase<ProcessingResult, object, ProcessingResultMessage>.CreateSuccess((object) intercompanyPoReceipt);
      receiptFromShipment.AddMessage((PXErrorLevel) 1, "The {0} purchase receipt has been created successfully.", (object) intercompanyPoReceipt.ReceiptNbr);
    }
    catch (Exception ex)
    {
      receiptFromShipment.AddErrorMessage(ex.Message);
    }
    return receiptFromShipment;
  }

  private static void SetProcessingResult(ProcessingResult result)
  {
    if (!result.IsSuccess)
      PXProcessing<PX.Objects.SO.SOShipment>.SetError(result.GeneralMessage);
    else if (result.HasWarning)
    {
      PXProcessing<PX.Objects.SO.SOShipment>.SetWarning(result.GeneralMessage);
    }
    else
    {
      PXProcessing<PX.Objects.SO.SOShipment>.SetProcessed();
      PXProcessing<PX.Objects.SO.SOShipment>.SetInfo(result.GeneralMessage);
    }
  }
}
