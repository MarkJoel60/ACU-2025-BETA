// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderShipment_ExtensionMethods
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO;

public static class SOOrderShipment_ExtensionMethods
{
  public static SOOrderShipment LinkInvoice(
    this SOOrderShipment self,
    SOInvoice invoice,
    PXGraph graph)
  {
    return self.LinkInvoice(invoice, graph, true);
  }

  public static SOOrderShipment LinkInvoice(
    this SOOrderShipment self,
    SOInvoice invoice,
    PXGraph graph,
    bool allocateTaxes)
  {
    if (self == null || invoice == null)
      return self;
    if (allocateTaxes && invoice.DisableAutomaticTaxCalculation.GetValueOrDefault())
      self.OrderTaxAllocated = new bool?(true);
    self.InvoiceType = invoice.DocType;
    self.InvoiceNbr = invoice.RefNbr;
    self = GraphHelper.Caches<SOOrderShipment>(graph).Update(self);
    ((SelectedEntityEvent<SOOrderShipment, SOInvoice>) PXEntityEventBase<SOOrderShipment>.Container<SOOrderShipment.Events>.Select<SOInvoice>((Expression<Func<SOOrderShipment.Events, PXEntityEvent<SOOrderShipment.Events, SOInvoice>>>) (e => e.InvoiceLinked))).FireOn(graph, self, invoice);
    return self;
  }

  public static SOOrderShipment UnlinkInvoice(this SOOrderShipment self, PXGraph graph)
  {
    if (self == null || self.InvoiceNbr == null)
      return self;
    SOInvoice parent = KeysRelation<CompositeKey<Field<SOOrderShipment.invoiceType>.IsRelatedTo<SOInvoice.docType>, Field<SOOrderShipment.invoiceNbr>.IsRelatedTo<SOInvoice.refNbr>>.WithTablesOf<SOInvoice, SOOrderShipment>, SOInvoice, SOOrderShipment>.FindParent(graph, self, (PKFindOptions) 0);
    self.OrderTaxAllocated = new bool?(false);
    self.InvoiceType = (string) null;
    self.InvoiceNbr = (string) null;
    self = GraphHelper.Caches<SOOrderShipment>(graph).Update(self);
    ((SelectedEntityEvent<SOOrderShipment, SOInvoice>) PXEntityEventBase<SOOrderShipment>.Container<SOOrderShipment.Events>.Select<SOInvoice>((Expression<Func<SOOrderShipment.Events, PXEntityEvent<SOOrderShipment.Events, SOInvoice>>>) (e => e.InvoiceUnlinked))).FireOn(graph, self, parent);
    return self;
  }

  public static SOOrderShipment LinkShipment(
    this SOOrderShipment self,
    SOShipment shipment,
    PXGraph graph)
  {
    if (self == null || shipment == null)
      return self;
    int num;
    if (!(self.ShipmentType != shipment.ShipmentType) && !(self.ShipmentNbr != shipment.ShipmentNbr))
    {
      Guid? shippingRefNoteId = self.ShippingRefNoteID;
      Guid? noteId = shipment.NoteID;
      if ((shippingRefNoteId.HasValue == noteId.HasValue ? (shippingRefNoteId.HasValue ? (shippingRefNoteId.GetValueOrDefault() != noteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      {
        num = self.Operation != shipment.Operation ? 1 : 0;
        goto label_6;
      }
    }
    num = 1;
label_6:
    if (num != 0)
    {
      self.ShipmentType = shipment.ShipmentType;
      self.ShipmentNbr = shipment.ShipmentNbr;
      self.ShippingRefNoteID = shipment.NoteID;
      self.Operation = shipment.Operation;
      self = GraphHelper.Caches<SOOrderShipment>(graph).Update(self);
    }
    ((SelectedEntityEvent<SOOrderShipment, SOShipment>) PXEntityEventBase<SOOrderShipment>.Container<SOOrderShipment.Events>.Select<SOShipment>((Expression<Func<SOOrderShipment.Events, PXEntityEvent<SOOrderShipment.Events, SOShipment>>>) (e => e.ShipmentLinked))).FireOn(graph, self, shipment);
    return self;
  }

  public static SOOrderShipment UnlinkShipment(this SOOrderShipment self, PXGraph graph)
  {
    if (self == null || self.ShipmentNbr == null)
      return self;
    SOShipment parent = KeysRelation<CompositeKey<Field<SOOrderShipment.shipmentType>.IsRelatedTo<SOShipment.shipmentType>, Field<SOOrderShipment.shipmentNbr>.IsRelatedTo<SOShipment.shipmentNbr>>.WithTablesOf<SOShipment, SOOrderShipment>, SOShipment, SOOrderShipment>.FindParent(graph, self, (PKFindOptions) 0);
    self.ShipmentType = (string) null;
    self.ShipmentNbr = (string) null;
    self.ShippingRefNoteID = new Guid?();
    self.Operation = (string) null;
    self = GraphHelper.Caches<SOOrderShipment>(graph).Update(self);
    ((SelectedEntityEvent<SOOrderShipment, SOShipment>) PXEntityEventBase<SOOrderShipment>.Container<SOOrderShipment.Events>.Select<SOShipment>((Expression<Func<SOOrderShipment.Events, PXEntityEvent<SOOrderShipment.Events, SOShipment>>>) (e => e.ShipmentUnlinked))).FireOn(graph, self, parent);
    return self;
  }
}
