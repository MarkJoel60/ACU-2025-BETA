// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.InvoiceOrderArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public class InvoiceOrderArgs
{
  public DateTime InvoiceDate { get; set; }

  public SOOrderShipment OrderShipment { get; set; }

  public SOOrder SOOrder { get; set; }

  public PX.Objects.CM.Extensions.CurrencyInfo SoCuryInfo { get; set; }

  public SOAddress SoBillAddress { get; set; }

  public SOContact SoBillContact { get; set; }

  public PXResultset<SOShipLine, SOLine> Details { get; set; }

  public PX.Objects.AR.Customer Customer { get; set; }

  public InvoiceList List { get; set; }

  public PXQuickProcess.ActionFlow QuickProcessFlow { get; set; }

  public bool GroupByDefaultOperation { get; set; }

  public bool GroupByCustomerOrderNumber { get; set; }

  public bool OptimizeExternalTaxCalc { get; set; }

  public InvoiceOrderArgs()
  {
  }

  public InvoiceOrderArgs(
    PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact> order)
  {
    this.OrderShipment = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
    this.SOOrder = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
    this.SoCuryInfo = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
    this.SoBillAddress = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
    this.SoBillContact = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
  }

  public InvoiceOrderArgs(
    PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact> order)
  {
    this.OrderShipment = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
    this.SOOrder = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
    this.SoCuryInfo = PX.Objects.CM.Extensions.CurrencyInfo.GetEX(PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order));
    this.SoBillAddress = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
    this.SoBillContact = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(order);
  }
}
