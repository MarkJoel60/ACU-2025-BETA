// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.SOInvoiceItemAvailabilityExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.DAC.Projections;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class SOInvoiceItemAvailabilityExtension : 
  SOBaseItemAvailabilityExtension<SOInvoiceEntry, PX.Objects.AR.ARTran, ARTranAsSplit>
{
  protected override ARTranAsSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) this.Base).FindImplementation<SOInvoiceLineSplittingExtension>().EnsureSplit(row);
  }

  protected override Decimal GetUnitRate(PX.Objects.AR.ARTran line)
  {
    return this.GetUnitRate<PX.Objects.AR.ARTran.inventoryID, PX.Objects.AR.ARTran.uOM>(line);
  }

  protected override string GetStatus(PX.Objects.AR.ARTran line)
  {
    string status = string.Empty;
    bool excludeCurrent = line == null || !line.Released.GetValueOrDefault();
    if (ARTranPlan.IsDirectLineNotLinkedToSO(line))
    {
      IStatus availability = this.FetchWithLineUOM(line, excludeCurrent, new int?(0));
      if (availability != null)
      {
        status = this.FormatStatus(availability, line.UOM);
        this.Check((ILSMaster) line, availability);
      }
    }
    return status;
  }

  private string FormatStatus(IStatus availability, string uom)
  {
    return PXMessages.LocalizeFormatNoPrefixNLA("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}", new object[4]
    {
      (object) uom,
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail)
    });
  }

  protected override void AddStatusField()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>())
      return;
    base.AddStatusField();
  }

  public override AvailabilitySigns GetAvailabilitySigns<TStatus>(ARTranAsSplit split)
  {
    return ((PXGraph) this.Base).FindImplementation<IItemPlanHandler<PX.Objects.AR.ARTran>>()?.GetAvailabilitySigns<TStatus>(ARTranAsSplit.ToARTran(split)) ?? new AvailabilitySigns();
  }

  public override IEnumerable<InvoiceSplit> SelectInvoicedRecords(
    string arDocType,
    string arRefNbr,
    int? arLineNbr)
  {
    return this.SelectInvoicedRecords(arDocType, arRefNbr, arLineNbr, true);
  }

  public virtual void MemoOrderCheck(PX.Objects.AR.ARTran line)
  {
    SOBaseItemAvailabilityExtension<SOInvoiceEntry, PX.Objects.AR.ARTran, ARTranAsSplit>.ReturnedQtyResult returnedQtyResult = this.MemoCheckQty(line);
    if (!returnedQtyResult.Success)
    {
      SOBaseItemAvailabilityExtension<SOInvoiceEntry, PX.Objects.AR.ARTran, ARTranAsSplit>.ReturnRecord[] returnRecords = returnedQtyResult.ReturnRecords;
      IEnumerable<string> o = returnRecords != null ? ((IEnumerable<SOBaseItemAvailabilityExtension<SOInvoiceEntry, PX.Objects.AR.ARTran, ARTranAsSplit>.ReturnRecord>) returnRecords).Select<SOBaseItemAvailabilityExtension<SOInvoiceEntry, PX.Objects.AR.ARTran, ARTranAsSplit>.ReturnRecord, string>((Func<SOBaseItemAvailabilityExtension<SOInvoiceEntry, PX.Objects.AR.ARTran, ARTranAsSplit>.ReturnRecord, string>) (x => x.DocumentNbr)).Where<string>((Func<string, bool>) (nbr => nbr != line.RefNbr)) : (IEnumerable<string>) null;
      this.RaiseErrorOn<PX.Objects.AR.ARTran.qty>(true, line, "The return quantity exceeds the quantity available for return for the related invoice line {0}, {1}. Decrease the quantity in the current line, or in the corresponding line of another return document or documents {2} that exist for the invoice line.", ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.origInvoiceNbr>((object) line), ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.inventoryID>((object) line), (object) (o.With<IEnumerable<string>, string>((Func<IEnumerable<string>, string>) (ds => string.Join(", ", ds))) ?? string.Empty));
    }
    this.OrderCheck(line, true);
  }

  protected virtual SOBaseItemAvailabilityExtension<SOInvoiceEntry, PX.Objects.AR.ARTran, ARTranAsSplit>.ReturnedQtyResult MemoCheckQty(
    PX.Objects.AR.ARTran line)
  {
    int num1 = string.IsNullOrEmpty(line.SOOrderNbr) ? 1 : 0;
    bool flag1 = string.IsNullOrEmpty(line.OrigInvoiceType);
    bool flag2 = string.IsNullOrEmpty(line.OrigInvoiceNbr);
    bool flag3 = !line.OrigInvoiceLineNbr.HasValue;
    if (num1 != 0 && (!flag1 || !flag2 || !flag3))
    {
      if (flag1)
        RaiseEmptyFieldException<PX.Objects.AR.ARTran.origInvoiceType>();
      else if (flag2)
        RaiseEmptyFieldException<PX.Objects.AR.ARTran.origInvoiceNbr>();
      else if (flag3)
        RaiseEmptyFieldException<PX.Objects.AR.ARTran.origInvoiceLineNbr>();
    }
    short? invtMult = line.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num2 = 0;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      return new SOBaseItemAvailabilityExtension<SOInvoiceEntry, PX.Objects.AR.ARTran, ARTranAsSplit>.ReturnedQtyResult(true);
    int? inventoryId = line.InventoryID;
    string origInvoiceType = line.OrigInvoiceType;
    string origInvoiceNbr = line.OrigInvoiceNbr;
    int? origInvoiceLineNbr = line.OrigInvoiceLineNbr;
    nullable = new int?();
    int? orderLineNbr = nullable;
    return this.MemoCheckQty(inventoryId, origInvoiceType, origInvoiceNbr, origInvoiceLineNbr, (string) null, (string) null, orderLineNbr);

    void RaiseEmptyFieldException<TField>() where TField : IBqlField
    {
      this.RaiseErrorOn<TField>(true, line, "Cannot save the document because {0} is not specified in the link to original SO invoice line.", (object) PXUIFieldAttribute.GetDisplayName((PXCache) this.LineCache, typeof (TField).Name));
    }
  }

  public virtual void OrderCheck(PX.Objects.AR.ARTran line, bool onPersist = false)
  {
    short? invtMult1 = line.InvtMult;
    int? nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      return;
    if (line.SOOrderType == null && line.SOOrderNbr == null)
    {
      nullable1 = line.SOOrderLineNbr;
      if (!nullable1.HasValue)
        return;
    }
    if (line.Released.GetValueOrDefault())
      return;
    PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find((PXGraph) this.Base, line.SOOrderType, line.SOOrderNbr, line.SOOrderLineNbr);
    if (soLine == null)
    {
      this.RaiseErrorOn<PX.Objects.AR.ARTran.sOOrderNbr>(onPersist, line, "Sales Order line was not found.");
    }
    else
    {
      PX.Objects.SO.SOOrderType soOrderType = PX.Objects.SO.SOOrderType.PK.Find((PXGraph) this.Base, soLine.OrderType);
      bool? nullable2;
      if (soOrderType.OrderType != null)
      {
        nullable2 = soOrderType.RequireShipping;
        bool flag = false;
        if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue) && !(soOrderType.ARDocType == "UND"))
          goto label_10;
      }
      this.RaiseErrorOn<PX.Objects.AR.ARTran.sOOrderType>((onPersist ? 1 : 0) != 0, line, "Cannot add an invoice line linked to the sales order line because the sales order has the {0} order type. Check the settings of the order type.", ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.sOOrderType>((object) line));
label_10:
      nullable2 = soLine.Completed;
      if (nullable2.GetValueOrDefault())
        this.RaiseErrorOn<PX.Objects.AR.ARTran.sOOrderNbr>(onPersist, line, "Cannot add an invoice line linked to the sales order line because the sales order line is completed.");
      nullable1 = soLine.CustomerID;
      int? nullable3 = line.CustomerID;
      if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
        this.RaiseErrorOn<PX.Objects.AR.ARTran.sOOrderNbr>(onPersist, line, "The customer specified in the invoice differs from the customer specified in the linked sales order.");
      nullable2 = soLine.POCreate;
      if (nullable2.GetValueOrDefault())
        this.RaiseErrorOn<PX.Objects.AR.ARTran.sOOrderNbr>(onPersist, line, "Cannot add an invoice line because it is linked to the sales order line that have the Mark for PO check box selected.");
      nullable3 = soLine.InventoryID;
      nullable1 = line.InventoryID;
      if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
        this.RaiseErrorOn<PX.Objects.AR.ARTran.inventoryID>(onPersist, line, "The inventory item specified in the invoice line differs from the inventory item specified in the linked sales order line.");
      short? invtMult2 = line.InvtMult;
      Decimal? nullable4 = invtMult2.HasValue ? new Decimal?((Decimal) invtMult2.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable5 = line.Qty;
      int num2 = Math.Sign((nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault());
      if (num2 != 0 && (soLine.Operation == "R" ? 1 : -1) != num2)
        this.RaiseErrorOn<PX.Objects.AR.ARTran.qty>(onPersist, line, "The operation specified in the invoice line differs from the operation specified in the linked sales order line.");
      Decimal num3 = Math.Abs(line.BaseQty.GetValueOrDefault());
      short? lineSign = soLine.LineSign;
      Decimal? nullable6 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      nullable5 = soLine.BaseOrderQty;
      Decimal num4 = (nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?()).Value;
      lineSign = soLine.LineSign;
      Decimal? nullable7 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      nullable5 = soLine.BaseShippedQty;
      Decimal num5 = (nullable7.HasValue & nullable5.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?()).Value;
      Decimal? nullable8 = soLine.CompleteQtyMax;
      Decimal num6 = nullable8.Value;
      if (PXDBQuantityAttribute.Round(new Decimal?(num4 * num6 / 100M - num5 - num3)) < 0M)
      {
        this.RaiseErrorOn<PX.Objects.AR.ARTran.qty>((onPersist ? 1 : 0) != 0, line, "Item '{0} {1}' in order '{2} {3}' quantity shipped is greater than quantity ordered.", ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.inventoryID>((object) line), ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.subItemID>((object) line), ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.sOOrderType>((object) line), ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.sOOrderNbr>((object) line));
      }
      else
      {
        PXCache cach = ((PXGraph) this.Base).Caches[soLine.LineType != "MI" ? typeof (SOLine2) : typeof (SOMiscLine2)];
        PXBqlTable pxBqlTable;
        if (!(soLine.LineType != "MI"))
        {
          pxBqlTable = (PXBqlTable) new SOMiscLine2()
          {
            OrderType = soLine.OrderType,
            OrderNbr = soLine.OrderNbr,
            LineNbr = soLine.LineNbr
          };
        }
        else
        {
          pxBqlTable = (PXBqlTable) new SOLine2();
          ((SOLine2) pxBqlTable).OrderType = soLine.OrderType;
          ((SOLine2) pxBqlTable).OrderNbr = soLine.OrderNbr;
          ((SOLine2) pxBqlTable).LineNbr = soLine.LineNbr;
        }
        Decimal? nullable9 = (Decimal?) cach.GetValue<PX.Objects.SO.SOLine.baseUnbilledQty>(cach.Locate((object) pxBqlTable));
        nullable5 = nullable9;
        lineSign = soLine.LineSign;
        Decimal? nullable10 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
        nullable8 = nullable5.HasValue & nullable10.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable10.GetValueOrDefault()) : new Decimal?();
        Decimal num7 = 0M;
        if (!(nullable8.GetValueOrDefault() < num7 & nullable8.HasValue))
        {
          nullable5 = nullable9;
          lineSign = soLine.LineSign;
          Decimal? nullable11 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
          nullable8 = nullable5.HasValue & nullable11.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable11.GetValueOrDefault()) : new Decimal?();
          nullable11 = soLine.BaseOrderQty;
          lineSign = soLine.LineSign;
          nullable5 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
          nullable10 = nullable11.HasValue & nullable5.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
          if (!(nullable8.GetValueOrDefault() > nullable10.GetValueOrDefault() & nullable8.HasValue & nullable10.HasValue))
            return;
        }
        int num8 = onPersist ? 1 : 0;
        PX.Objects.AR.ARTran line1 = line;
        lineSign = soLine.LineSign;
        int? nullable12;
        if (!lineSign.HasValue)
        {
          nullable3 = new int?();
          nullable12 = nullable3;
        }
        else
          nullable12 = new int?((int) lineSign.GetValueOrDefault());
        nullable1 = nullable12;
        int num9 = 0;
        string errorMessage = nullable1.GetValueOrDefault() > num9 & nullable1.HasValue ? "The unbilled quantity of the {0} item in the {1} order should not be less than zero or greater than the ordered quantity." : "The unbilled quantity of the {0} item in the {1} order should not be less than the ordered quantity or greater than zero.";
        object[] objArray = new object[2]
        {
          ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.inventoryID>((object) line),
          ((PXCache) this.LineCache).GetValueExt<PX.Objects.AR.ARTran.sOOrderNbr>((object) line)
        };
        this.RaiseErrorOn<PX.Objects.AR.ARTran.qty>(num8 != 0, line1, errorMessage, objArray);
      }
    }
  }

  public virtual IStatus FetchSite(PX.Objects.AR.ARTran line, bool excludeCurrent = false, int? costCenterID = 0)
  {
    IStatus availability = this.FetchSite((ILSDetail) this.EnsureSplit((ILSMaster) line), excludeCurrent, costCenterID);
    if (excludeCurrent)
      availability = this.ExcludeAllocated(line, availability);
    return availability;
  }

  protected virtual IStatus ExcludeAllocated(PX.Objects.AR.ARTran line, IStatus availability)
  {
    if (availability == null)
      return (IStatus) null;
    AvailabilitySigns availabilitySigns = ((PXGraph) this.Base).FindImplementation<IItemPlanHandler<PX.Objects.AR.ARTran>>().GetAvailabilitySigns<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(line);
    Decimal? nullable1 = new Decimal?(0M);
    if (Sign.op_Inequality(availabilitySigns.SignQtyAvail, Sign.Zero))
    {
      Decimal? nullable2 = nullable1;
      Decimal num = Sign.op_Multiply(availabilitySigns.SignQtyAvail, line.BaseQty.GetValueOrDefault());
      nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num) : new Decimal?();
    }
    Decimal? nullable3 = new Decimal?(0M);
    if (Sign.op_Inequality(availabilitySigns.SignQtyHardAvail, Sign.Zero))
    {
      Decimal? nullable4 = nullable3;
      Decimal num = Sign.op_Multiply(availabilitySigns.SignQtyHardAvail, line.BaseQty.GetValueOrDefault());
      nullable3 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - num) : new Decimal?();
    }
    IStatus status1 = availability;
    Decimal? qtyAvail = status1.QtyAvail;
    Decimal? nullable5 = nullable1;
    status1.QtyAvail = qtyAvail.HasValue & nullable5.HasValue ? new Decimal?(qtyAvail.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
    IStatus status2 = availability;
    Decimal? qtyHardAvail = status2.QtyHardAvail;
    Decimal? nullable6 = nullable3;
    status2.QtyHardAvail = qtyHardAvail.HasValue & nullable6.HasValue ? new Decimal?(qtyHardAvail.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
    IStatus status3 = availability;
    nullable6 = nullable1;
    Decimal? nullable7 = nullable6.HasValue ? new Decimal?(-nullable6.GetValueOrDefault()) : new Decimal?();
    status3.QtyNotAvail = nullable7;
    return availability;
  }

  protected override void RaiseQtyExceptionHandling(
    PX.Objects.AR.ARTran line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<PX.Objects.AR.ARTran.qty>((object) line, (object) null, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.LineCache).GetStateExt<PX.Objects.AR.ARTran.inventoryID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<PX.Objects.AR.ARTran.subItemID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<PX.Objects.AR.ARTran.siteID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<PX.Objects.AR.ARTran.locationID>((object) line),
      ((PXCache) this.LineCache).GetValue<PX.Objects.AR.ARTran.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    ARTranAsSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<ARTranAsSplit.qty>((object) split, (object) null, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.SplitCache).GetStateExt<ARTranAsSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<ARTranAsSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<ARTranAsSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<ARTranAsSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetValue<ARTranAsSplit.lotSerialNbr>((object) split)
    }));
  }

  protected virtual void RaiseErrorOn<TField>(
    bool onPersist,
    PX.Objects.AR.ARTran line,
    string errorMessage,
    params object[] args)
    where TField : IBqlField
  {
    PXSetPropertyException propertyException = new PXSetPropertyException(errorMessage, args);
    if (!onPersist)
      throw propertyException;
    object valueExt = ((PXCache) this.LineCache).GetValueExt((object) line, typeof (TField).Name);
    if (((PXCache) this.LineCache).RaiseExceptionHandling(typeof (TField).Name, (object) line, valueExt, (Exception) propertyException))
      throw new PXRowPersistingException(typeof (TField).Name, valueExt, errorMessage, args);
  }
}
