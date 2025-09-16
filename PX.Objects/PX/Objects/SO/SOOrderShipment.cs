// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderShipment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO.Attributes;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXPrimaryGraph(new System.Type[] {typeof (SOShipmentEntry), typeof (POReceiptEntry), typeof (SOInvoiceEntry)}, new System.Type[] {typeof (Select2<SOShipment, LeftJoin<PX.Objects.IN.INSite, On<SOShipment.FK.Site>>, Where<SOShipment.noteID, Equal<Current<SOOrderShipment.shippingRefNoteID>>, And<INDocType.dropShip, NotEqual<Current<SOOrderShipment.shipmentType>>, And<Where<PX.Objects.IN.INSite.siteID, IsNull, Or<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>>), typeof (Select<PX.Objects.PO.POReceipt, Where<PX.Objects.PO.POReceipt.noteID, Equal<Current<SOOrderShipment.shippingRefNoteID>>, And<INDocType.dropShip, Equal<Current<SOOrderShipment.shipmentType>>>>>), typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.noteID, Equal<Current<SOOrderShipment.shippingRefNoteID>>, And<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>>>>)}, UseParent = false)]
[PXCacheName("Sales Order Shipment")]
[Serializable]
public class SOOrderShipment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected string _Operation;
  protected string _ShipmentType;
  protected string _ShipmentNbr;
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected int? _ShipAddressID;
  protected int? _ShipContactID;
  protected int? _LineCntr;
  protected DateTime? _ShipDate;
  protected int? _SiteID;
  protected int? _ProjectID;
  protected bool? _Hold;
  protected bool? _Confirmed;
  protected string _ShipComplete;
  protected Decimal? _ShipmentQty;
  protected Decimal? _ShipmentWeight;
  protected Decimal? _ShipmentVolume;
  protected Decimal? _LineTotal;
  protected string _InvoiceType;
  protected string _InvoiceNbr;
  protected bool? _InvoiceReleased;
  protected bool? _CreateINDoc;
  protected string _InvtDocType;
  protected string _InvtRefNbr;
  protected bool? _HasDetailDeleted;
  protected Guid? _ShipmentNoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  [PXSelector(typeof (Search<SOOrderType.orderType>))]
  [PXDefault]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsKey = true, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOOrderShipment.orderType>>>>))]
  [PXParent(typeof (SOOrderShipment.FK.Order))]
  [PXDefault]
  [PXFormula(null, typeof (CountCalc<SOShipment.orderCntr>), ValidateAggregateCalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<Selector<SOOrderShipment.orderNbr, SOOrder.billSeparately>, Equal<True>>, int1>, int0>), typeof (SumCalc<SOShipment.billSeparatelyCntr>))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField(DisplayName = "Operation")]
  [SOOperation.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (SOShipment.noteID))]
  public virtual Guid? ShippingRefNoteID { get; set; }

  [ShippingRefNote]
  [PXFormula(typeof (SOOrderShipment.shippingRefNoteID))]
  [PXUIField(DisplayName = "Document Nbr.")]
  public virtual Guid? DisplayShippingRefNoteID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SOShipment.shipmentType))]
  [SOShipmentType.List]
  [PXUIField(DisplayName = "Shipment Type")]
  public virtual string ShipmentType
  {
    get => this._ShipmentType;
    set => this._ShipmentType = value;
  }

  [PXDBString(15, InputMask = "", IsUnicode = true)]
  [PXDBDefault(typeof (SOShipment.shipmentNbr), DefaultForInsert = false, DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Shipment Nbr.", Visible = false, Enabled = false)]
  [PXParent(typeof (Select<SOShipment, Where<SOShipment.shipmentNbr, Equal<Current<SOOrderShipment.shipmentNbr>>, And<SOShipment.shipmentType, Equal<Current<SOOrderShipment.shipmentType>>>>>))]
  public virtual string ShipmentNbr
  {
    get => this._ShipmentNbr;
    set => this._ShipmentNbr = value;
  }

  [Customer(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))]
  [CustomerOrOrganizationRestrictor(typeof (SOOrderShipment.shipmentType))]
  [PXDefault(typeof (SOShipment.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrderShipment.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDefault(typeof (SOShipment.customerLocationID))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (SOShipment.shipAddressID), DefaultForInsert = false, DefaultForUpdate = false)]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (SOShipment.shipContactID), DefaultForInsert = false, DefaultForUpdate = false)]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (SOShipment.shipDate))]
  [PXUIField(DisplayName = "Shipment Date")]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  [Site(DisplayName = "Warehouse ID", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXDefault(typeof (SOShipment.siteID))]
  [PXParent(typeof (SOOrderShipment.FK.OrderSite), LeaveChildren = true)]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Confirmed
  {
    get => this._Confirmed;
    set => this._Confirmed = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Search<SOOrder.shipComplete, Where<SOOrder.orderType, Equal<Current<SOOrderShipment.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOOrderShipment.orderNbr>>>>>))]
  public virtual string ShipComplete
  {
    get => this._ShipComplete;
    set => this._ShipComplete = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipped Qty.", Enabled = false)]
  public virtual Decimal? ShipmentQty
  {
    get => this._ShipmentQty;
    set => this._ShipmentQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipped Weight", Enabled = false)]
  public virtual Decimal? ShipmentWeight
  {
    get => this._ShipmentWeight;
    set => this._ShipmentWeight = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipped Volume", Enabled = false)]
  public virtual Decimal? ShipmentVolume
  {
    get => this._ShipmentVolume;
    set => this._ShipmentVolume = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Line Total")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Invoice Type", Enabled = false)]
  [ARDocType.List]
  public virtual string InvoiceType
  {
    get => this._InvoiceType;
    set => this._InvoiceType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Invoice Nbr.", Enabled = false)]
  [PXParent(typeof (Select<SOInvoice, Where<SOInvoice.docType, Equal<Current<SOOrderShipment.invoiceType>>, And<SOInvoice.refNbr, Equal<Current<SOOrderShipment.invoiceNbr>>>>>), LeaveChildren = true)]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.refNbr))]
  [PXSelector(typeof (Search<SOInvoice.refNbr, Where<SOInvoice.docType, Equal<Current<SOOrderShipment.invoiceType>>>>), DirtyRead = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<Current2<SOOrderShipment.invoiceNbr>, IsNull, And<SOOrderShipment.createARDoc, Equal<True>>>, int1>, int0>), typeof (SumCalc<SOShipment.unbilledOrderCntr>), ValidateAggregateCalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<Current2<SOOrderShipment.invoiceNbr>, IsNotNull, And<SOOrderShipment.invoiceReleased, Equal<False>>>, int1>, int0>), typeof (SumCalc<SOShipment.billedOrderCntr>), ValidateAggregateCalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<Current2<SOOrderShipment.invoiceNbr>, IsNotNull, And<SOOrderShipment.invoiceReleased, Equal<False>>>, int1>, int0>), typeof (SumCalc<SOOrder.billedCntr>), ValidateAggregateCalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<Current2<SOOrderShipment.invoiceNbr>, IsNotNull, And<SOOrderShipment.invoiceReleased, Equal<True>>>, int1>, int0>), typeof (SumCalc<SOShipment.releasedOrderCntr>), ValidateAggregateCalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<Current2<SOOrderShipment.invoiceNbr>, IsNotNull, And<SOOrderShipment.invoiceReleased, Equal<True>>>, int1>, int0>), typeof (SumCalc<SOOrder.releasedCntr>), ValidateAggregateCalculation = true)]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? InvoiceReleased
  {
    get => this._InvoiceReleased;
    set => this._InvoiceReleased = value;
  }

  /// <summary>
  /// The flag indicates that the Order Shipment contains at least one line with a Stock Item, therefore an Inventory Document should be created.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CreateINDoc
  {
    get => this._CreateINDoc;
    set => this._CreateINDoc = value;
  }

  [PXDBBool]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<Selector<SOOrderShipment.orderType, SOOrderType.aRDocType>, NotEqual<ARDocType.noUpdate>>, True>, False>))]
  public virtual bool? CreateARDoc { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Inventory Doc. Type", Enabled = false)]
  [INDocType.List]
  public virtual string InvtDocType
  {
    get => this._InvtDocType;
    set => this._InvtDocType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Inventory Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<SOOrderShipment.invtDocType>>>>))]
  public virtual string InvtRefNbr
  {
    get => this._InvtRefNbr;
    set => this._InvtRefNbr = value;
  }

  /// <summary>
  /// The flag indicates that the Order Freight Price is fully allocated to the Invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? OrderFreightAllocated { get; set; }

  /// <summary>
  /// The flag indicates that the Order Taxes are fully allocated to the Invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? OrderTaxAllocated { get; set; }

  [PXBool]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public virtual bool? HasDetailDeleted
  {
    get => this._HasDetailDeleted;
    set => this._HasDetailDeleted = value;
  }

  /// <exclude />
  [PXBool]
  public virtual bool? IsPartialInvoiceConstraintViolated { get; set; }

  /// <summary>
  /// The flag indicates that during adding the Order Shipment into the Invoice one or more errors
  /// have occured, therefore the state of the Invoice may be inconsistent.
  /// </summary>
  [PXBool]
  public virtual bool? HasUnhandledErrors { get; set; }

  [PXDBGuid(false, IsImmutable = true)]
  [PXDefault(typeof (SOShipment.noteID))]
  public virtual Guid? ShipmentNoteID
  {
    get => this._ShipmentNoteID;
    set => this._ShipmentNoteID = value;
  }

  [PXDBGuid(false, IsImmutable = true)]
  public virtual Guid? InvtNoteID { get; set; }

  [CopiedNoteID(typeof (SOOrder))]
  public virtual Guid? OrderNoteID { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the related shipment has been canceled.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PO.POReceipt.Canceled" /> field.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Canceled { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the shipment should be invoiced by a separate invoice.
  /// </summary>
  [PXBool]
  public virtual bool? BillShipmentSeparately { get; set; }

  public static SOOrderShipment FromSalesOrder(SOOrder item, bool miscOnly = false)
  {
    SOOrderShipment soOrderShipment = new SOOrderShipment();
    soOrderShipment.OrderType = item.OrderType;
    soOrderShipment.OrderNbr = item.OrderNbr;
    soOrderShipment.ShipAddressID = item.ShipAddressID;
    soOrderShipment.ShipContactID = item.ShipContactID;
    soOrderShipment.ShipmentType = (string) null;
    soOrderShipment.ShipmentNbr = "<NEW>";
    soOrderShipment.ShippingRefNoteID = new Guid?();
    soOrderShipment.Operation = item.DefaultOperation;
    soOrderShipment.ShipDate = item.ShipDate;
    soOrderShipment.CustomerID = item.CustomerID;
    soOrderShipment.CustomerLocationID = item.CustomerLocationID;
    soOrderShipment.SiteID = new int?();
    if (!miscOnly)
    {
      soOrderShipment.ShipmentQty = item.OrderQty;
      soOrderShipment.ShipmentVolume = item.OrderVolume;
      soOrderShipment.ShipmentWeight = item.OrderWeight;
    }
    soOrderShipment.LineTotal = item.LineTotal;
    soOrderShipment.Confirmed = new bool?(true);
    soOrderShipment.CreateINDoc = new bool?(true);
    soOrderShipment.OrderNoteID = item.NoteID;
    return soOrderShipment;
  }

  public static SOOrderShipment FromDirectInvoice(
    PX.Objects.AR.ARRegister ardoc,
    string soOrderType,
    string soOrderNbr)
  {
    return new SOOrderShipment()
    {
      OrderType = soOrderType,
      OrderNbr = soOrderNbr,
      ShippingRefNoteID = ardoc.NoteID,
      ShipmentType = "N",
      ShipmentNbr = "<NEW>",
      Confirmed = new bool?(true),
      CustomerID = ardoc.CustomerID,
      CustomerLocationID = ardoc.CustomerLocationID,
      SiteID = new int?(),
      ShipDate = ardoc.DocDate,
      LineCntr = new int?(0),
      ShipmentQty = new Decimal?(0M),
      LineTotal = new Decimal?(0M),
      CreateINDoc = new bool?(false),
      InvoiceType = ardoc.DocType,
      InvoiceNbr = ardoc.RefNbr,
      InvoiceReleased = new bool?(true)
    };
  }

  public static SOOrderShipment FromDropshipPOReceipt(
    PX.Objects.PO.POReceipt rec,
    SOOrder order,
    PX.Objects.PO.POReceiptLine line)
  {
    return new SOOrderShipment()
    {
      OrderType = order.OrderType,
      OrderNbr = order.OrderNbr,
      ShipAddressID = order.ShipAddressID,
      ShipContactID = order.ShipContactID,
      ShipmentType = "H",
      ShipmentNbr = line.ReceiptNbr,
      ShippingRefNoteID = rec.NoteID,
      Operation = rec.ReceiptType == "RN" ? "R" : "I",
      ShipDate = line.ReceiptDate,
      CustomerID = order.CustomerID,
      CustomerLocationID = order.CustomerLocationID,
      SiteID = new int?(),
      ShipmentWeight = line.ExtWeight,
      ShipmentVolume = line.ExtVolume,
      ShipmentQty = line.ReceiptQty,
      LineTotal = new Decimal?(0M),
      Confirmed = new bool?(true),
      CreateINDoc = new bool?(true),
      OrderNoteID = order.NoteID
    };
  }

  public class PK : 
    PrimaryKeyOf<SOOrderShipment>.By<SOOrderShipment.orderType, SOOrderShipment.orderNbr, SOOrderShipment.shippingRefNoteID>
  {
    public static SOOrderShipment Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      Guid? shippingRefNoteID,
      PKFindOptions options = 0)
    {
      return (SOOrderShipment) PrimaryKeyOf<SOOrderShipment>.By<SOOrderShipment.orderType, SOOrderShipment.orderNbr, SOOrderShipment.shippingRefNoteID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) shippingRefNoteID, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<SOOrderShipment>.By<SOOrderShipment.shipmentType, SOOrderShipment.shipmentNbr, SOOrderShipment.orderType, SOOrderShipment.orderNbr>
  {
    public static SOOrderShipment Find(
      PXGraph graph,
      string shipmentType,
      string shipmentNbr,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (SOOrderShipment) PrimaryKeyOf<SOOrderShipment>.By<SOOrderShipment.shipmentType, SOOrderShipment.shipmentNbr, SOOrderShipment.orderType, SOOrderShipment.orderNbr>.FindBy(graph, (object) shipmentType, (object) shipmentNbr, (object) orderType, (object) orderNbr, options);
    }
  }

  public static class FK
  {
    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentType, SOShipment.shipmentNbr>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.shipmentType, SOOrderShipment.shipmentNbr>
    {
    }

    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.orderType, SOOrderShipment.orderNbr>
    {
    }

    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.orderType>
    {
    }

    public class OrderTypeOperation : 
      PrimaryKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderType, SOOrderTypeOperation.operation>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.orderType, SOOrderShipment.operation>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<SOInvoice>.By<SOInvoice.docType, SOInvoice.refNbr>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.invoiceType, SOOrderShipment.invoiceNbr>
    {
    }

    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.invoiceType, SOOrderShipment.invoiceNbr>
    {
    }

    public class ARRegister : 
      PrimaryKeyOf<PX.Objects.AR.ARRegister>.By<PX.Objects.AR.ARRegister.docType, PX.Objects.AR.ARRegister.refNbr>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.invoiceType, SOOrderShipment.invoiceNbr>
    {
    }

    public class INRegister : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.invtDocType, SOOrderShipment.invtRefNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.siteID>
    {
    }

    public class ShipAddress : 
      PrimaryKeyOf<SOShipmentAddress>.By<SOShipmentAddress.addressID>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.shipAddressID>
    {
    }

    public class ShipContact : 
      PrimaryKeyOf<SOShipmentContact>.By<SOShipmentContact.contactID>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.shipContactID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.customerID, SOOrderShipment.customerLocationID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.projectID>
    {
    }

    public class OrderSite : 
      PrimaryKeyOf<SOOrderSite>.By<SOOrderSite.orderType, SOOrderSite.orderNbr, SOOrderSite.siteID>.ForeignKeyOf<SOOrderShipment>.By<SOOrderShipment.orderType, SOOrderShipment.orderNbr, SOOrderShipment.siteID>
    {
    }
  }

  public class Events : PXEntityEventBase<SOOrderShipment>.Container<SOOrderShipment.Events>
  {
    public PXEntityEvent<SOOrderShipment, SOShipment> ShipmentLinked;
    public PXEntityEvent<SOOrderShipment, SOShipment> ShipmentUnlinked;
    public PXEntityEvent<SOOrderShipment, SOInvoice> InvoiceLinked;
    public PXEntityEvent<SOOrderShipment, SOInvoice> InvoiceUnlinked;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderShipment.selected>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.orderNbr>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.operation>
  {
  }

  public abstract class shippingRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderShipment.shippingRefNoteID>
  {
  }

  public abstract class displayShippingRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderShipment.displayShippingRefNoteID>
  {
  }

  public abstract class shipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderShipment.shipmentType>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.shipmentNbr>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderShipment.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderShipment.customerLocationID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderShipment.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderShipment.shipContactID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderShipment.lineCntr>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrderShipment.shipDate>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderShipment.siteID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderShipment.projectID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderShipment.hold>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderShipment.confirmed>
  {
  }

  public abstract class shipComplete : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderShipment.shipComplete>
  {
  }

  public abstract class shipmentQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderShipment.shipmentQty>
  {
  }

  public abstract class shipmentWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderShipment.shipmentWeight>
  {
  }

  public abstract class shipmentVolume : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderShipment.shipmentVolume>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrderShipment.lineTotal>
  {
  }

  public abstract class invoiceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.invoiceType>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.invoiceNbr>
  {
  }

  public abstract class invoiceReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderShipment.invoiceReleased>
  {
  }

  public abstract class createINDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderShipment.createINDoc>
  {
  }

  public abstract class createARDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderShipment.createARDoc>
  {
  }

  public abstract class invtDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.invtDocType>
  {
  }

  public abstract class invtRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderShipment.invtRefNbr>
  {
  }

  public abstract class orderFreightAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderShipment.orderFreightAllocated>
  {
  }

  public abstract class orderTaxAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderShipment.orderTaxAllocated>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public abstract class hasDetailDeleted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderShipment.hasDetailDeleted>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderShipment.IsPartialInvoiceConstraintViolated" />
  public abstract class isPartialInvoiceConstraintViolated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderShipment.isPartialInvoiceConstraintViolated>
  {
  }

  public abstract class hasUnhandledErrors : IBqlField, IBqlOperand
  {
  }

  public abstract class shipmentNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderShipment.shipmentNoteID>
  {
  }

  public abstract class invtNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrderShipment.invtNoteID>
  {
  }

  public abstract class orderNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrderShipment.orderNoteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderShipment.Canceled" />
  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderShipment.canceled>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrderShipment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderShipment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderShipment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderShipment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderShipment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderShipment.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOOrderShipment.Tstamp>
  {
  }

  public abstract class billShipmentSeparately : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderShipment.billShipmentSeparately>
  {
  }
}
