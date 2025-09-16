// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.AddReturnLineToDirectInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.DAC.Projections;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class AddReturnLineToDirectInvoice : PXGraphExtension<SOInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<ARTranForDirectInvoice, Where<ARTranForDirectInvoice.customerID, Equal<Current<PX.Objects.AR.ARInvoice.customerID>>>> arTranList;
  public PXAction<PX.Objects.AR.ARInvoice> selectARTran;
  public PXAction<PX.Objects.AR.ARInvoice> addARTran;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable SelectARTran(PXAdapter adapter)
  {
    if (((PXSelectBase) this.Base.Document).Cache.AllowInsert && ((PXSelectBase<ARTranForDirectInvoice>) this.arTranList).AskExt() == 1)
      this.AddARTran(adapter);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddARTran(PXAdapter adapter)
  {
    foreach (ARTranForDirectInvoice forDirectInvoice in ((PXSelectBase) this.arTranList).Cache.Updated)
    {
      if (forDirectInvoice.Selected.GetValueOrDefault())
      {
        PX.Objects.AR.ARTran instance = (PX.Objects.AR.ARTran) ((PXSelectBase) this.Base.Transactions).Cache.CreateInstance();
        instance.InventoryID = forDirectInvoice.InventoryID;
        instance.SubItemID = forDirectInvoice.SubItemID;
        instance.SiteID = forDirectInvoice.SiteID;
        instance.LocationID = forDirectInvoice.LocationID;
        instance.LotSerialNbr = forDirectInvoice.LotSerialNbr;
        instance.ExpireDate = forDirectInvoice.ExpireDate;
        instance.UOM = forDirectInvoice.UOM;
        PX.Objects.AR.ARTran arTran = instance;
        short? nullable1 = INTranType.InvtMultFromInvoiceType(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocType);
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal num = Math.Abs(forDirectInvoice.Qty.GetValueOrDefault());
        Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num) : new Decimal?();
        arTran.Qty = nullable3;
        instance.CuryUnitPrice = forDirectInvoice.CuryUnitPrice;
        instance.OrigInvoiceType = forDirectInvoice.TranType;
        instance.OrigInvoiceNbr = forDirectInvoice.RefNbr;
        instance.OrigInvoiceLineNbr = forDirectInvoice.LineNbr;
        this.Base.InsertInvoiceDirectLine(instance);
        forDirectInvoice.Selected = new bool?(false);
      }
    }
    return adapter.Get();
  }

  protected void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.AR.ARInvoice row = (PX.Objects.AR.ARInvoice) e.Row;
    ((PXAction) this.selectARTran).SetEnabled(((PXSelectBase) this.Base.Transactions).AllowInsert && row != null && row.CustomerID.HasValue);
  }
}
