// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPostingBase`1
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public abstract class FSPostingBase<TGraph> : PXGraphExtension<TGraph>, IInvoiceGraph where TGraph : PXGraph
{
  public abstract void CreateInvoice(
    PXGraph graphProcess,
    List<DocLineExt> docLines,
    short invtMult,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    OnDocumentHeaderInsertedDelegate onDocumentHeaderInserted,
    OnTransactionInsertedDelegate onTransactionInserted,
    PXQuickProcess.ActionFlow quickProcessFlow);

  public abstract FSCreatedDoc PressSave(
    int batchID,
    List<DocLineExt> docLines,
    BeforeSaveDelegate beforeSave);

  public abstract void Clear();

  public abstract PXGraph GetGraph();

  public abstract void DeleteDocument(FSCreatedDoc fsCreatedDocRow);

  public abstract void CleanPostInfo(PXGraph cleanerGraph, FSPostDet fsPostDetRow);

  public abstract List<MessageHelper.ErrorInfo> GetErrorInfo();

  public bool IsInvoiceProcessRunning { get; set; }

  public virtual void CheckAutoNumbering(string numberingID)
  {
    DocGenerationHelper.ValidateAutoNumbering((PXGraph) this.Base, numberingID);
  }

  public virtual PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address> GetContactAndAddressFromLocation(
    PXGraph graph,
    int? locationID)
  {
    return (PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address>) PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>>, Where<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>.Config>.Select(graph, new object[1]
    {
      (object) locationID
    }));
  }

  public virtual PXResult<PX.Objects.CR.Location, PX.Objects.AR.Customer, PX.Objects.CR.Contact, PX.Objects.CR.Address> GetContactAddressFromDefaultLocation(
    PXGraph graph,
    int? bAccountID)
  {
    return (PXResult<PX.Objects.CR.Location, PX.Objects.AR.Customer, PX.Objects.CR.Contact, PX.Objects.CR.Address>) PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.AR.Customer.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bAccountID
    }));
  }

  public virtual void GetSrvOrdContactAddress(
    PXGraph graph,
    FSServiceOrder fsServiceOrder,
    out FSContact fsContact,
    out FSAddress fsAddress)
  {
    fsContact = FSContact.PK.Find(graph, fsServiceOrder.ServiceOrderContactID);
    fsAddress = FSAddress.PK.Find(graph, fsServiceOrder.ServiceOrderAddressID);
  }

  public virtual void SetContactAndAddress(PXGraph graph, FSServiceOrder fsServiceOrderRow)
  {
    int? billCustomerId = fsServiceOrderRow.BillCustomerID;
    int? billLocationId = fsServiceOrderRow.BillLocationID;
    PX.Objects.AR.Customer customerRow = SharedFunctions.GetCustomerRow(graph, billCustomerId);
    FSPostingBase<TGraph>.ContactAddressSource contactAddressSource = this.GetBillingContactAddressSource(graph, fsServiceOrderRow, customerRow);
    if (contactAddressSource == null || contactAddressSource != null && string.IsNullOrEmpty(contactAddressSource.BillingSource))
      throw new PXException("No billing address source has been specified for the customer. Specify the billing address source in the Bill-To box on the Billing Settings tab of the Customers (AR303000) form.");
    IAddress source1 = (IAddress) null;
    IContact source2 = (IContact) null;
    switch (contactAddressSource.BillingSource)
    {
      case "BT":
        source2 = this.GetIContact(PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select(graph, new object[1]
        {
          (object) customerRow.DefBillContactID
        })));
        source1 = this.GetIAddress(PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select(graph, new object[1]
        {
          (object) customerRow.DefBillAddressID
        })));
        break;
      case "LC":
        PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address> addressFromLocation = this.GetContactAndAddressFromLocation(graph, billLocationId);
        if (addressFromLocation != null)
        {
          source1 = this.GetIAddress(PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(addressFromLocation));
          source2 = this.GetIContact(PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(addressFromLocation));
          break;
        }
        break;
      case "SO":
        FSContact fsContact;
        FSAddress fsAddress;
        this.GetSrvOrdContactAddress(graph, fsServiceOrderRow, out fsContact, out fsAddress);
        source2 = (IContact) fsContact;
        source1 = (IAddress) fsAddress;
        break;
      default:
        PXResult<PX.Objects.CR.Location, PX.Objects.AR.Customer, PX.Objects.CR.Contact, PX.Objects.CR.Address> fromDefaultLocation = this.GetContactAddressFromDefaultLocation(graph, billCustomerId);
        if (fromDefaultLocation != null)
        {
          source1 = this.GetIAddress(PXResult<PX.Objects.CR.Location, PX.Objects.AR.Customer, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(fromDefaultLocation));
          source2 = this.GetIContact(PXResult<PX.Objects.CR.Location, PX.Objects.AR.Customer, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(fromDefaultLocation));
          break;
        }
        break;
    }
    if (source1 == null)
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("{0} must be specified to process this item.", new object[1]
      {
        (object) "Address"
      }), new object[1]{ (object) (PXErrorLevel) 4 });
    if (source2 == null)
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("{0} must be specified to process this item.", new object[1]
      {
        (object) "Contact"
      }), new object[1]{ (object) (PXErrorLevel) 4 });
    switch (graph)
    {
      case SOOrderEntry _:
        SOOrderEntry soOrderEntry = (SOOrderEntry) graph;
        SOBillingContact dest1 = new SOBillingContact();
        SOBillingAddress dest2 = new SOBillingAddress();
        InvoiceHelper.CopyContact((IContact) dest1, source2);
        dest1.CustomerID = ((PXSelectBase<PX.Objects.AR.Customer>) soOrderEntry.customer).Current.BAccountID;
        dest1.RevisionID = new int?(0);
        InvoiceHelper.CopyAddress((IAddress) dest2, source1);
        dest2.CustomerID = ((PXSelectBase<PX.Objects.AR.Customer>) soOrderEntry.customer).Current.BAccountID;
        dest2.CustomerAddressID = ((PXSelectBase<PX.Objects.AR.Customer>) soOrderEntry.customer).Current.DefAddressID;
        dest2.RevisionID = new int?(0);
        dest1.IsDefaultContact = new bool?(false);
        dest2.IsDefaultAddress = new bool?(false);
        SOBillingContact soBillingContact;
        ((PXSelectBase<SOBillingContact>) soOrderEntry.Billing_Contact).Current = soBillingContact = ((PXSelectBase<SOBillingContact>) soOrderEntry.Billing_Contact).Insert(dest1);
        SOBillingAddress soBillingAddress;
        ((PXSelectBase<SOBillingAddress>) soOrderEntry.Billing_Address).Current = soBillingAddress = ((PXSelectBase<SOBillingAddress>) soOrderEntry.Billing_Address).Insert(dest2);
        ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.BillAddressID = soBillingAddress.AddressID;
        ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.BillContactID = soBillingContact.ContactID;
        IAddress addressRow1 = (IAddress) null;
        IContact contactRow1 = (IContact) null;
        this.GetShippingContactAddress(graph, contactAddressSource.ShippingSource, billCustomerId, fsServiceOrderRow, out contactRow1, out addressRow1);
        if (addressRow1 == null)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("{0} must be specified to process this item.", new object[1]
          {
            (object) "Shipping Address"
          }), new object[1]{ (object) (PXErrorLevel) 4 });
        if (contactRow1 == null)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("{0} must be specified to process this item.", new object[1]
          {
            (object) "Shipping Contact"
          }), new object[1]{ (object) (PXErrorLevel) 4 });
        SOShippingContact dest3 = new SOShippingContact();
        SOShippingAddress dest4 = new SOShippingAddress();
        InvoiceHelper.CopyContact((IContact) dest3, contactRow1);
        dest3.CustomerID = ((PXSelectBase<PX.Objects.AR.Customer>) soOrderEntry.customer).Current.BAccountID;
        dest3.RevisionID = new int?(0);
        InvoiceHelper.CopyAddress((IAddress) dest4, addressRow1);
        dest4.CustomerID = ((PXSelectBase<PX.Objects.AR.Customer>) soOrderEntry.customer).Current.BAccountID;
        dest4.CustomerAddressID = ((PXSelectBase<PX.Objects.AR.Customer>) soOrderEntry.customer).Current.DefAddressID;
        dest4.RevisionID = new int?(0);
        dest3.IsDefaultContact = new bool?(false);
        dest4.IsDefaultAddress = new bool?(false);
        SOShippingContact soShippingContact;
        ((PXSelectBase<SOShippingContact>) soOrderEntry.Shipping_Contact).Current = soShippingContact = ((PXSelectBase<SOShippingContact>) soOrderEntry.Shipping_Contact).Insert(dest3);
        SOShippingAddress soShippingAddress;
        ((PXSelectBase<SOShippingAddress>) soOrderEntry.Shipping_Address).Current = soShippingAddress = ((PXSelectBase<SOShippingAddress>) soOrderEntry.Shipping_Address).Insert(dest4);
        ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.ShipAddressID = soShippingAddress.AddressID;
        ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.ShipContactID = soShippingContact.ContactID;
        break;
      case ARInvoiceEntry _:
        ARInvoiceEntry arInvoiceEntry = (ARInvoiceEntry) graph;
        ARContact dest5 = new ARContact();
        ARAddress dest6 = new ARAddress();
        InvoiceHelper.CopyContact((IContact) dest5, source2);
        dest5.CustomerID = ((PXSelectBase<PX.Objects.AR.Customer>) arInvoiceEntry.customer).Current.BAccountID;
        dest5.RevisionID = new int?(0);
        dest5.IsDefaultContact = new bool?(false);
        InvoiceHelper.CopyAddress((IAddress) dest6, source1);
        dest6.CustomerID = ((PXSelectBase<PX.Objects.AR.Customer>) arInvoiceEntry.customer).Current.BAccountID;
        dest6.CustomerAddressID = ((PXSelectBase<PX.Objects.AR.Customer>) arInvoiceEntry.customer).Current.DefAddressID;
        dest6.RevisionID = new int?(0);
        dest6.IsDefaultBillAddress = new bool?(false);
        ARContact arContact;
        ((PXSelectBase<ARContact>) arInvoiceEntry.Billing_Contact).Current = arContact = ((PXSelectBase<ARContact>) arInvoiceEntry.Billing_Contact).Update(dest5);
        ARAddress arAddress;
        ((PXSelectBase<ARAddress>) arInvoiceEntry.Billing_Address).Current = arAddress = ((PXSelectBase<ARAddress>) arInvoiceEntry.Billing_Address).Update(dest6);
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) arInvoiceEntry.Document).Current.BillAddressID = arAddress.AddressID;
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) arInvoiceEntry.Document).Current.BillContactID = arContact.ContactID;
        IAddress addressRow2 = (IAddress) null;
        IContact contactRow2 = (IContact) null;
        this.GetShippingContactAddress(graph, contactAddressSource.ShippingSource, billCustomerId, fsServiceOrderRow, out contactRow2, out addressRow2);
        if (addressRow2 == null)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("{0} must be specified to process this item.", new object[1]
          {
            (object) "Shipping Address"
          }), new object[1]{ (object) (PXErrorLevel) 4 });
        if (contactRow2 == null)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("{0} must be specified to process this item.", new object[1]
          {
            (object) "Shipping Contact"
          }), new object[1]{ (object) (PXErrorLevel) 4 });
        ARShippingContact dest7 = new ARShippingContact();
        ARShippingAddress dest8 = new ARShippingAddress();
        InvoiceHelper.CopyContact((IContact) dest7, contactRow2);
        dest7.CustomerID = ((PXSelectBase<PX.Objects.AR.Customer>) arInvoiceEntry.customer).Current.BAccountID;
        dest7.RevisionID = new int?(0);
        InvoiceHelper.CopyAddress((IAddress) dest8, addressRow2);
        dest8.CustomerID = ((PXSelectBase<PX.Objects.AR.Customer>) arInvoiceEntry.customer).Current.BAccountID;
        dest8.CustomerAddressID = ((PXSelectBase<PX.Objects.AR.Customer>) arInvoiceEntry.customer).Current.DefAddressID;
        dest8.RevisionID = new int?(0);
        dest7.IsDefaultContact = new bool?(false);
        dest8.IsDefaultAddress = new bool?(false);
        ARShippingContact arShippingContact;
        ((PXSelectBase<ARShippingContact>) arInvoiceEntry.Shipping_Contact).Current = arShippingContact = ((PXSelectBase<ARShippingContact>) arInvoiceEntry.Shipping_Contact).Insert(dest7);
        ARShippingAddress arShippingAddress;
        ((PXSelectBase<ARShippingAddress>) arInvoiceEntry.Shipping_Address).Current = arShippingAddress = ((PXSelectBase<ARShippingAddress>) arInvoiceEntry.Shipping_Address).Insert(dest8);
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) arInvoiceEntry.Document).Current.ShipAddressID = arShippingAddress.AddressID;
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) arInvoiceEntry.Document).Current.ShipContactID = arShippingContact.ContactID;
        break;
    }
  }

  public virtual FSPostingBase<TGraph>.ContactAddressSource GetBillingContactAddressSource(
    PXGraph graph,
    FSServiceOrder fsServiceOrderRow,
    PX.Objects.AR.Customer billCustomer)
  {
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select(graph, Array.Empty<object>()));
    FSPostingBase<TGraph>.ContactAddressSource contactAddressSource = (FSPostingBase<TGraph>.ContactAddressSource) null;
    if (fsSetup != null)
    {
      contactAddressSource = new FSPostingBase<TGraph>.ContactAddressSource();
      if (fsSetup.CustomerMultipleBillingOptions.GetValueOrDefault())
      {
        FSCustomerBillingSetup customerBillingSetup = PXResultset<FSCustomerBillingSetup>.op_Implicit(PXSelectBase<FSCustomerBillingSetup, PXSelect<FSCustomerBillingSetup, Where<FSCustomerBillingSetup.customerID, Equal<Required<FSCustomerBillingSetup.customerID>>, And<FSCustomerBillingSetup.srvOrdType, Equal<Required<FSCustomerBillingSetup.srvOrdType>>>>>.Config>.Select(graph, new object[2]
        {
          (object) billCustomer.BAccountID,
          (object) fsServiceOrderRow.SrvOrdType
        }));
        if (customerBillingSetup != null)
        {
          contactAddressSource.BillingSource = customerBillingSetup.SendInvoicesTo;
          contactAddressSource.ShippingSource = customerBillingSetup.BillShipmentSource;
        }
      }
      else
      {
        bool? multipleBillingOptions = fsSetup.CustomerMultipleBillingOptions;
        bool flag = false;
        if (multipleBillingOptions.GetValueOrDefault() == flag & multipleBillingOptions.HasValue && billCustomer != null)
        {
          FSxCustomer extension = PXCache<PX.Objects.AR.Customer>.GetExtension<FSxCustomer>(billCustomer);
          contactAddressSource.BillingSource = extension.SendInvoicesTo;
          contactAddressSource.ShippingSource = extension.BillShipmentSource;
        }
      }
    }
    return contactAddressSource;
  }

  public virtual void GetShippingContactAddress(
    PXGraph graph,
    string contactAddressSource,
    int? billCustomerID,
    FSServiceOrder fsServiceOrderRow,
    out IContact contactRow,
    out IAddress addressRow)
  {
    contactRow = (IContact) null;
    addressRow = (IAddress) null;
    switch (contactAddressSource)
    {
      case "BT":
        PXResult<PX.Objects.CR.Location, PX.Objects.AR.Customer, PX.Objects.CR.Contact, PX.Objects.CR.Address> fromDefaultLocation = this.GetContactAddressFromDefaultLocation(graph, billCustomerID);
        contactRow = this.GetIContact(PXResult<PX.Objects.CR.Location, PX.Objects.AR.Customer, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(fromDefaultLocation));
        addressRow = this.GetIAddress(PXResult<PX.Objects.CR.Location, PX.Objects.AR.Customer, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(fromDefaultLocation));
        break;
      case "SO":
        FSContact fsContact;
        FSAddress fsAddress;
        this.GetSrvOrdContactAddress(graph, fsServiceOrderRow, out fsContact, out fsAddress);
        contactRow = (IContact) fsContact;
        addressRow = (IAddress) fsAddress;
        break;
      case "LC":
        PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address> addressFromLocation1 = this.GetContactAndAddressFromLocation(graph, fsServiceOrderRow.LocationID);
        contactRow = this.GetIContact(PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(addressFromLocation1));
        addressRow = this.GetIAddress(PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(addressFromLocation1));
        break;
      case "BL":
        PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address> addressFromLocation2 = this.GetContactAndAddressFromLocation(graph, fsServiceOrderRow.BillLocationID);
        contactRow = this.GetIContact(PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(addressFromLocation2));
        addressRow = this.GetIAddress(PXResult<PX.Objects.CR.Location, PX.Objects.CR.Contact, PX.Objects.CR.Address>.op_Implicit(addressFromLocation2));
        break;
    }
  }

  public virtual IAddress GetIAddress(PX.Objects.CR.Address source)
  {
    if (source == null)
      return (IAddress) null;
    return (IAddress) new CRAddress()
    {
      BAccountID = source.BAccountID,
      RevisionID = source.RevisionID,
      IsDefaultAddress = new bool?(false),
      AddressLine1 = source.AddressLine1,
      AddressLine2 = source.AddressLine2,
      AddressLine3 = source.AddressLine3,
      City = source.City,
      CountryID = source.CountryID,
      State = source.State,
      PostalCode = source.PostalCode,
      Latitude = source.Latitude,
      Longitude = source.Longitude,
      IsValidated = source.IsValidated
    };
  }

  public virtual IContact GetIContact(PX.Objects.CR.Contact source)
  {
    if (source == null)
      return (IContact) null;
    return (IContact) new PX.Objects.CR.CRContact()
    {
      BAccountID = source.BAccountID,
      RevisionID = source.RevisionID,
      IsDefaultContact = new bool?(false),
      FullName = source.FullName,
      Salutation = source.Salutation,
      Title = source.Title,
      Phone1 = source.Phone1,
      Phone1Type = source.Phone1Type,
      Phone2 = source.Phone2,
      Phone2Type = source.Phone2Type,
      Phone3 = source.Phone3,
      Phone3Type = source.Phone3Type,
      Fax = source.Fax,
      FaxType = source.FaxType,
      Email = source.EMail,
      NoteID = new Guid?(),
      Attention = source.Attention
    };
  }

  /// <summary>Sets the SubID for AR or SalesSubID for SO.</summary>
  public virtual void SetCombinedSubID(
    PXGraph graph,
    PXCache sender,
    PX.Objects.AR.ARTran tranARRow,
    APTran tranAPRow,
    PX.Objects.SO.SOLine tranSORow,
    FSSetup fsSetupRow,
    int? branchID,
    int? inventoryID,
    int? customerLocationID,
    int? branchLocationID)
  {
    if (!branchID.HasValue || !inventoryID.HasValue || !customerLocationID.HasValue || !branchLocationID.HasValue)
      throw new PXException("Some subaccount segment source is not specified in the Combine Sales Sub. From box on the Service Order Types (FS202300) form.");
    if ((tranARRow == null || !tranARRow.AccountID.HasValue) && (tranAPRow == null || !tranAPRow.AccountID.HasValue) && (tranSORow == null || !tranSORow.SalesAcctID.HasValue))
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(graph, inventoryID);
    PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.AR.ARTran.branchID>>>>.Config>.Select(graph, new object[1]
    {
      (object) branchID
    }));
    PX.Objects.CR.Location location2 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>.Config>.Select(graph, new object[1]
    {
      (object) customerLocationID
    }));
    FSBranchLocation fsBranchLocation = FSBranchLocation.PK.Find(graph, branchLocationID);
    int? csalesSubId = location2.CSalesSubID;
    int? salesSubId = inventoryItem.SalesSubID;
    int? cmpSalesSubId = location1.CMPSalesSubID;
    int? subId = fsBranchLocation.SubID;
    try
    {
      if (tranARRow != null)
      {
        object obj = (object) SubAccountMaskAttribute.MakeSub<ARSetup.salesSubMask>(graph, fsSetupRow.ContractCombineSubFrom, new object[4]
        {
          (object) csalesSubId,
          (object) salesSubId,
          (object) cmpSalesSubId,
          (object) subId
        }, new System.Type[4]
        {
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (PX.Objects.CR.Location.cMPSalesSubID),
          typeof (FSBranchLocation.subID)
        }, true);
        sender.RaiseFieldUpdating<PX.Objects.AR.ARTran.subID>((object) tranARRow, ref obj);
        tranARRow.SubID = (int?) obj;
      }
      else if (tranAPRow != null)
      {
        object obj = (object) SubAccountMaskAttribute.MakeSub<APSetup.expenseSubMask>(graph, fsSetupRow.ContractCombineSubFrom, new object[4]
        {
          (object) csalesSubId,
          (object) salesSubId,
          (object) cmpSalesSubId,
          (object) subId
        }, new System.Type[4]
        {
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (PX.Objects.CR.Location.cMPSalesSubID),
          typeof (FSBranchLocation.subID)
        }, true);
        sender.RaiseFieldUpdating<APTran.subID>((object) tranSORow, ref obj);
        tranAPRow.SubID = (int?) obj;
      }
      else
      {
        if (tranSORow == null)
          return;
        object obj = (object) SubAccountMaskAttribute.MakeSub<PX.Objects.SO.SOOrderType.salesSubMask>(graph, fsSetupRow.ContractCombineSubFrom, new object[4]
        {
          (object) csalesSubId,
          (object) salesSubId,
          (object) cmpSalesSubId,
          (object) subId
        }, new System.Type[4]
        {
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (PX.Objects.CR.Location.cMPSalesSubID),
          typeof (FSBranchLocation.subID)
        }, true);
        sender.RaiseFieldUpdating<PX.Objects.SO.SOLine.salesSubID>((object) tranSORow, ref obj);
        tranSORow.SalesSubID = (int?) obj;
      }
    }
    catch (PXException ex)
    {
      if (tranARRow != null)
      {
        tranARRow.SubID = new int?();
      }
      else
      {
        if (tranSORow == null)
          return;
        tranSORow.SalesSubID = new int?();
      }
    }
  }

  /// <summary>Sets the SubID for AR or SalesSubID for SO.</summary>
  public virtual void SetCombinedSubID(
    PXGraph graph,
    PXCache sender,
    PX.Objects.AR.ARTran tranARRow,
    APTran tranAPRow,
    PX.Objects.SO.SOLine tranSORow,
    FSSrvOrdType fsSrvOrdTypeRow,
    int? branchID,
    int? inventoryID,
    int? customerLocationID,
    int? branchLocationID,
    int? salesPersonID,
    bool isService)
  {
    if (string.IsNullOrEmpty(fsSrvOrdTypeRow.CombineSubFrom))
      throw new PXException("The sales subaccount mask is not defined for the {0} service order type in the Combine Sales Sub. From box on the Service Order Types (FS202300) form.", new object[1]
      {
        (object) fsSrvOrdTypeRow.SrvOrdType
      });
    if (!branchID.HasValue || !inventoryID.HasValue || !customerLocationID.HasValue || !branchLocationID.HasValue)
      throw new PXException("Some subaccount segment source is not specified in the Combine Sales Sub. From box on the Service Order Types (FS202300) form.");
    int? nullable;
    if (tranARRow != null)
    {
      nullable = tranARRow.AccountID;
      if (nullable.HasValue)
        goto label_10;
    }
    if (tranAPRow != null)
    {
      nullable = tranAPRow.AccountID;
      if (nullable.HasValue)
        goto label_10;
    }
    if (tranSORow == null)
      return;
    nullable = tranSORow.SalesAcctID;
    if (!nullable.HasValue)
      return;
label_10:
    nullable = (int?) tranARRow?.SiteID;
    int? siteID = (int?) (nullable ?? (int?) tranAPRow?.SiteID ?? tranSORow?.SiteID);
    SharedClasses.SubAccountIDTupla subAccountIds = SharedFunctions.GetSubAccountIDs(graph, fsSrvOrdTypeRow, inventoryID, branchID, customerLocationID, branchLocationID, salesPersonID, siteID);
    try
    {
      if (tranARRow != null)
      {
        object obj = (object) SubAccountMaskAttribute.MakeSub<ARSetup.salesSubMask>(graph, fsSrvOrdTypeRow.CombineSubFrom, new object[8]
        {
          (object) subAccountIds.branchLocation_SubID,
          (object) subAccountIds.branch_SubID,
          (object) subAccountIds.inventoryItem_SubID,
          (object) subAccountIds.customerLocation_SubID,
          (object) subAccountIds.postingClass_SubID,
          (object) subAccountIds.salesPerson_SubID,
          (object) subAccountIds.srvOrdType_SubID,
          (object) subAccountIds.warehouse_SubID
        }, new System.Type[8]
        {
          typeof (FSBranchLocation.subID),
          typeof (PX.Objects.CR.Location.cMPSalesSubID),
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (INPostClass.salesSubID),
          typeof (PX.Objects.AR.SalesPerson.salesSubID),
          typeof (FSSrvOrdType.subID),
          isService ? typeof (PX.Objects.IN.INSite.salesSubID) : typeof (PX.Objects.IN.InventoryItem.salesSubID)
        });
        sender.RaiseFieldUpdating<PX.Objects.AR.ARTran.subID>((object) tranARRow, ref obj);
        tranARRow.SubID = (int?) obj;
      }
      else if (tranAPRow != null)
      {
        object obj = (object) SubAccountMaskAttribute.MakeSub<APSetup.expenseSubMask>(graph, fsSrvOrdTypeRow.CombineSubFrom, new object[8]
        {
          (object) subAccountIds.branchLocation_SubID,
          (object) subAccountIds.branch_SubID,
          (object) subAccountIds.inventoryItem_SubID,
          (object) subAccountIds.customerLocation_SubID,
          (object) subAccountIds.postingClass_SubID,
          (object) subAccountIds.salesPerson_SubID,
          (object) subAccountIds.srvOrdType_SubID,
          (object) subAccountIds.warehouse_SubID
        }, new System.Type[8]
        {
          typeof (FSBranchLocation.subID),
          typeof (PX.Objects.CR.Location.cMPSalesSubID),
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (INPostClass.salesSubID),
          typeof (PX.Objects.AR.SalesPerson.salesSubID),
          typeof (FSSrvOrdType.subID),
          isService ? typeof (PX.Objects.IN.INSite.salesSubID) : typeof (PX.Objects.IN.InventoryItem.salesSubID)
        });
        sender.RaiseFieldUpdating<APTran.subID>((object) tranSORow, ref obj);
        tranAPRow.SubID = (int?) obj;
      }
      else
      {
        if (tranSORow == null)
          return;
        object obj = (object) SubAccountMaskAttribute.MakeSub<PX.Objects.SO.SOOrderType.salesSubMask>(graph, fsSrvOrdTypeRow.CombineSubFrom, new object[8]
        {
          (object) subAccountIds.branchLocation_SubID,
          (object) subAccountIds.branch_SubID,
          (object) subAccountIds.inventoryItem_SubID,
          (object) subAccountIds.customerLocation_SubID,
          (object) subAccountIds.postingClass_SubID,
          (object) subAccountIds.salesPerson_SubID,
          (object) subAccountIds.srvOrdType_SubID,
          (object) subAccountIds.warehouse_SubID
        }, new System.Type[8]
        {
          typeof (FSBranchLocation.subID),
          typeof (PX.Objects.CR.Location.cMPSalesSubID),
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (INPostClass.salesSubID),
          typeof (PX.Objects.AR.SalesPerson.salesSubID),
          typeof (FSSrvOrdType.subID),
          isService ? typeof (PX.Objects.IN.INSite.salesSubID) : typeof (PX.Objects.IN.InventoryItem.salesSubID)
        });
        sender.RaiseFieldUpdating<PX.Objects.SO.SOLine.salesSubID>((object) tranSORow, ref obj);
        tranSORow.SalesSubID = (int?) obj;
      }
    }
    catch (PXException ex)
    {
      if (tranARRow != null)
      {
        tranARRow.SubID = new int?();
      }
      else
      {
        if (tranSORow == null)
          return;
        tranSORow.SalesSubID = new int?();
      }
    }
  }

  /// <summary>Returns the TermID from the Vendor or Customer.</summary>
  public virtual string GetTermsIDFromCustomerOrVendor(
    PXGraph graph,
    int? customerID,
    int? vendorID)
  {
    if (customerID.HasValue)
      return PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select(graph, new object[1]
      {
        (object) customerID
      }))?.TermsID;
    if (!vendorID.HasValue)
      return (string) null;
    return PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) vendorID
    }))?.TermsID;
  }

  public virtual Exception GetErrorInfoInLines(
    List<MessageHelper.ErrorInfo> errorInfoList,
    Exception e)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(e.Message.EnsureEndsWithDot() + " ");
    foreach (MessageHelper.ErrorInfo errorInfo in errorInfoList)
      stringBuilder.Append(errorInfo.ErrorMessage.EnsureEndsWithDot() + " ");
    return (Exception) new PXException(stringBuilder.ToString().TrimEnd());
  }

  /// <summary>
  /// Cleans the posting information <c>(FSCreatedDoc, FSPostRegister, FSPostDoc, FSPostInfo, FSPostDet, FSPostBatch)</c> for the document created by the billing process.
  /// </summary>
  public virtual void CleanPostingInfoLinkedToDoc(object createdDoc)
  {
    if (createdDoc == null)
      return;
    PXGraph pxGraph = new PXGraph();
    bool flag = false;
    string str1;
    string str2;
    string str3;
    PXResultset<FSPostDet> source;
    if (createdDoc is PX.Objects.SO.SOOrder)
    {
      str1 = "SO";
      PX.Objects.SO.SOOrder soOrder = (PX.Objects.SO.SOOrder) createdDoc;
      str2 = soOrder.OrderType;
      str3 = soOrder.OrderNbr;
      source = PXSelectBase<FSPostDet, PXSelect<FSPostDet, Where<FSPostDet.sOOrderType, Equal<Required<FSPostDet.sOOrderType>>, And<FSPostDet.sOOrderNbr, Equal<Required<FSPostDet.sOOrderNbr>>>>>.Config>.Select(pxGraph, new object[2]
      {
        (object) str2,
        (object) str3
      });
    }
    else if (createdDoc is PX.Objects.SO.SOInvoice)
    {
      str1 = "SI";
      PX.Objects.SO.SOInvoice soInvoice = (PX.Objects.SO.SOInvoice) createdDoc;
      str2 = soInvoice.DocType;
      str3 = soInvoice.RefNbr;
      source = PXSelectBase<FSPostDet, PXSelect<FSPostDet, Where<FSPostDet.sOInvDocType, Equal<Required<FSPostDet.sOInvDocType>>, And<FSPostDet.sOInvRefNbr, Equal<Required<FSPostDet.sOInvRefNbr>>>>>.Config>.Select(pxGraph, new object[2]
      {
        (object) str2,
        (object) str3
      });
    }
    else if (createdDoc is PX.Objects.AR.ARInvoice)
    {
      str1 = "AR";
      PX.Objects.AR.ARInvoice arInvoice = (PX.Objects.AR.ARInvoice) createdDoc;
      str2 = arInvoice.DocType;
      str3 = arInvoice.RefNbr;
      source = PXSelectBase<FSPostDet, PXSelect<FSPostDet, Where<FSPostDet.arDocType, Equal<Required<FSPostDet.arDocType>>, And<FSPostDet.arRefNbr, Equal<Required<FSPostDet.arRefNbr>>>>>.Config>.Select(pxGraph, new object[2]
      {
        (object) str2,
        (object) str3
      });
    }
    else if (createdDoc is PX.Objects.AP.APInvoice)
    {
      str1 = "AP";
      PX.Objects.AP.APInvoice apInvoice = (PX.Objects.AP.APInvoice) createdDoc;
      str2 = apInvoice.DocType;
      str3 = apInvoice.RefNbr;
      source = PXSelectBase<FSPostDet, PXSelect<FSPostDet, Where<FSPostDet.apDocType, Equal<Required<FSPostDet.apDocType>>, And<FSPostDet.apRefNbr, Equal<Required<FSPostDet.apRefNbr>>>>>.Config>.Select(pxGraph, new object[2]
      {
        (object) str2,
        (object) str3
      });
    }
    else if (createdDoc is PMRegister)
    {
      str1 = "PM";
      PMRegister pmRegister = (PMRegister) createdDoc;
      str2 = pmRegister.Module;
      str3 = pmRegister.RefNbr;
      source = PXSelectBase<FSPostDet, PXSelect<FSPostDet, Where<FSPostDet.pMDocType, Equal<Required<FSPostDet.pMDocType>>, And<FSPostDet.pMRefNbr, Equal<Required<FSPostDet.pMRefNbr>>>>>.Config>.Select(pxGraph, new object[2]
      {
        (object) str2,
        (object) str3
      });
      flag = true;
    }
    else
    {
      if (!(createdDoc is PX.Objects.IN.INRegister))
        throw new NotImplementedException();
      str1 = "IN";
      PX.Objects.IN.INRegister inRegister = (PX.Objects.IN.INRegister) createdDoc;
      str2 = inRegister.DocType;
      str3 = inRegister.RefNbr;
      source = PXSelectBase<FSPostDet, PXSelect<FSPostDet, Where<FSPostDet.iNDocType, Equal<Required<FSPostDet.iNDocType>>, And<FSPostDet.iNRefNbr, Equal<Required<FSPostDet.iNRefNbr>>>>>.Config>.Select(pxGraph, new object[2]
      {
        (object) str2,
        (object) str3
      });
      flag = true;
    }
    List<FSPostRegister> list = GraphHelper.RowCast<FSPostRegister>((IEnumerable) PXSelectBase<FSPostRegister, PXSelect<FSPostRegister, Where<FSPostRegister.postedTO, Equal<Required<FSPostRegister.postedTO>>, And<FSPostRegister.postDocType, Equal<Required<FSPostRegister.postDocType>>, And<FSPostRegister.postRefNbr, Equal<Required<FSPostRegister.postRefNbr>>>>>>.Config>.Select(pxGraph, new object[3]
    {
      (object) str1,
      (object) str2,
      (object) str3
    })).ToList<FSPostRegister>();
    PXDatabase.Delete<FSCreatedDoc>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSCreatedDoc.postTo>((object) str1),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSCreatedDoc.createdDocType>((object) str2),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSCreatedDoc.createdRefNbr>((object) str3)
    });
    PXDatabase.Delete<FSPostRegister>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostRegister.postedTO>((object) str1),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostRegister.postDocType>((object) str2),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostRegister.postRefNbr>((object) str3)
    });
    PXDatabase.Delete<FSPostDoc>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostDoc.postedTO>((object) str1),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostDoc.postDocType>((object) str2),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSPostDoc.postRefNbr>((object) str3)
    });
    AppointmentEntry instance1 = PXGraph.CreateInstance<AppointmentEntry>();
    ServiceOrderEntry instance2 = PXGraph.CreateInstance<ServiceOrderEntry>();
    if (source != null && source.Count > 0)
    {
      if (GraphHelper.RowCast<FSPostDet>((IEnumerable) source).GroupBy<FSPostDet, int?>((Func<FSPostDet, int?>) (e => e.BatchID)).Count<IGrouping<int?, FSPostDet>>() > 1)
        throw new NotImplementedException();
      int? batchId = ((PXResult) ((IQueryable<PXResult<FSPostDet>>) source).FirstOrDefault<PXResult<FSPostDet>>()).GetItem<FSPostDet>().BatchID;
      PXCache cach = pxGraph.Caches[typeof (FSPostDet)];
      foreach (PXResult<FSPostDet> pxResult in source)
      {
        FSPostDet fsPostDetRow = PXResult<FSPostDet>.op_Implicit(pxResult);
        this.CleanPostInfo(pxGraph, fsPostDetRow);
        cach.Delete((object) fsPostDetRow);
      }
      cach.Persist((PXDBOperation) 3);
      FSPostingBase<TGraph>.UpdatePostingBatch(pxGraph, batchId);
    }
    foreach (FSPostRegister postRegister in list)
    {
      FSPostRegister fsPostRegister = (FSPostRegister) null;
      if (flag)
        fsPostRegister = PXResultset<FSPostRegister>.op_Implicit(PXSelectBase<FSPostRegister, PXSelect<FSPostRegister, Where2<Where<FSPostRegister.entityType, Equal<Required<FSPostRegister.entityType>>, And<FSPostRegister.srvOrdType, Equal<Required<FSPostRegister.srvOrdType>>, And<FSPostRegister.refNbr, Equal<Required<FSPostRegister.refNbr>>>>>, And<Where<FSPostRegister.postedTO, NotEqual<Required<FSPostRegister.postedTO>>, Or<FSPostRegister.postDocType, NotEqual<Required<FSPostRegister.postDocType>>, Or<FSPostRegister.postRefNbr, NotEqual<Required<FSPostRegister.postRefNbr>>>>>>>>.Config>.Select(pxGraph, new object[6]
        {
          (object) postRegister.EntityType,
          (object) postRegister.SrvOrdType,
          (object) postRegister.RefNbr,
          (object) postRegister.PostedTO,
          (object) postRegister.PostDocType,
          (object) postRegister.PostRefNbr
        }));
      if (fsPostRegister == null)
        FSPostingBase<TGraph>.CleanPostingInfoOnServiceOrderAppointment(pxGraph, instance1, instance2, postRegister);
    }
  }

  public static PXResultset<FSPostRegister> GetOtherPostRegistersRelated(
    PXGraph graph,
    string srvOrdType,
    string serviceOrderRefNbr,
    FSPostRegister postRegisterToIgnore)
  {
    if (srvOrdType == null || serviceOrderRefNbr == null)
      return (PXResultset<FSPostRegister>) null;
    string str1 = (string) null;
    string str2 = (string) null;
    string str3 = (string) null;
    if (postRegisterToIgnore != null)
    {
      str1 = postRegisterToIgnore.PostedTO;
      str2 = postRegisterToIgnore.PostDocType;
      str3 = postRegisterToIgnore.PostRefNbr;
    }
    return PXSelectBase<FSPostRegister, PXSelectJoin<FSPostRegister, LeftJoin<FSAppointment, On<FSPostRegister.entityType, Equal<ListField_PostDoc_EntityType.Appointment>, And<FSAppointment.srvOrdType, Equal<FSPostRegister.srvOrdType>, And<FSAppointment.refNbr, Equal<FSPostRegister.refNbr>>>>>, Where2<Where<Where2<Where<FSPostRegister.entityType, Equal<ListField_PostDoc_EntityType.Service_Order>, And<FSPostRegister.srvOrdType, Equal<Required<FSPostRegister.srvOrdType>>, And<FSPostRegister.refNbr, Equal<Required<FSPostRegister.refNbr>>>>>, Or<Where<FSAppointment.refNbr, IsNotNull, And<FSAppointment.srvOrdType, Equal<Required<FSAppointment.srvOrdType>>, And<FSAppointment.soRefNbr, Equal<Required<FSAppointment.soRefNbr>>>>>>>>, And<Where<FSPostRegister.postedTO, NotEqual<Required<FSPostRegister.postedTO>>, Or<FSPostRegister.postDocType, NotEqual<Required<FSPostRegister.postDocType>>, Or<FSPostRegister.postRefNbr, NotEqual<Required<FSPostRegister.postRefNbr>>>>>>>>.Config>.Select(graph, new object[7]
    {
      (object) srvOrdType,
      (object) serviceOrderRefNbr,
      (object) srvOrdType,
      (object) serviceOrderRefNbr,
      (object) str1,
      (object) str2,
      (object) str3
    });
  }

  private static void CleanPostingInfoOnServiceOrderAppointment(
    PXGraph graph,
    AppointmentEntry apptGraph,
    ServiceOrderEntry srvOrdGraph,
    FSPostRegister postRegister)
  {
    if (postRegister == null)
      return;
    string str1 = postRegister.EntityType;
    string srvOrdType = postRegister.SrvOrdType;
    string str2 = postRegister.RefNbr;
    if (str1 == "AP")
    {
      ((PXGraph) apptGraph).Clear((PXClearOption) 3);
      PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords1 = apptGraph.AppointmentRecords;
      PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords2 = apptGraph.AppointmentRecords;
      string str3 = str2;
      object[] objArray = new object[1]
      {
        (object) srvOrdType
      };
      FSAppointment fsAppointment1;
      FSAppointment fsAppointment2 = fsAppointment1 = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) appointmentRecords2).Search<FSAppointment.refNbr>((object) str3, objArray));
      ((PXSelectBase<FSAppointment>) appointmentRecords1).Current = fsAppointment1;
      FSAppointment fsAppointment3 = fsAppointment2;
      FSAppointment copy = (FSAppointment) ((PXSelectBase) apptGraph.AppointmentRecords).Cache.CreateCopy((object) fsAppointment3);
      copy.PostingStatusAPARSO = "PP";
      copy.PendingAPARSOPost = new bool?(true);
      ((SelectedEntityEvent<FSAppointment>) PXEntityEventBase<FSAppointment>.Container<FSAppointment.Events>.Select((Expression<Func<FSAppointment.Events, PXEntityEvent<FSAppointment.Events>>>) (ev => ev.AppointmentUnposted))).FireOn((PXGraph) apptGraph, copy);
      ((PXSelectBase) apptGraph.AppointmentRecords).Cache.Update((object) copy);
      ((PXSelectBase) apptGraph.AppointmentRecords).Cache.SetValue<FSAppointment.finPeriodID>((object) copy, (object) null);
      copy.SkipExternalTaxCalculation = new bool?(true);
      ((PXAction) apptGraph.Save).Press();
      if (FSPostingBase<TGraph>.GetOtherPostRegistersRelated(graph, postRegister.SrvOrdType, copy.SORefNbr, postRegister).Count != 0)
        return;
      str1 = "SO";
      str2 = copy.SORefNbr;
    }
    if (!(str1 == "SO"))
      throw new NotImplementedException();
    ((PXGraph) srvOrdGraph).Clear((PXClearOption) 3);
    PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords1 = srvOrdGraph.ServiceOrderRecords;
    PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords2 = srvOrdGraph.ServiceOrderRecords;
    string str4 = str2;
    object[] objArray1 = new object[1]
    {
      (object) srvOrdType
    };
    FSServiceOrder fsServiceOrder1;
    FSServiceOrder fsServiceOrder2 = fsServiceOrder1 = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderRecords2).Search<FSServiceOrder.refNbr>((object) str4, objArray1));
    ((PXSelectBase<FSServiceOrder>) serviceOrderRecords1).Current = fsServiceOrder1;
    FSServiceOrder fsServiceOrder3 = fsServiceOrder2;
    FSServiceOrder copy1 = (FSServiceOrder) ((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache.CreateCopy((object) fsServiceOrder3);
    copy1.PostedBy = (string) null;
    copy1.Billed = new bool?(false);
    copy1.PendingAPARSOPost = new bool?(true);
    ((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache.Update((object) copy1);
    ((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache.SetValue<FSServiceOrder.finPeriodID>((object) copy1, (object) null);
    ((PXAction) srvOrdGraph.Save).Press();
  }

  private static void UpdatePostingBatch(PXGraph graph, int? batchID)
  {
    if (!batchID.HasValue)
      return;
    PXSelect<FSPostBatch, Where<FSPostBatch.batchID, Equal<Required<FSPostBatch.batchID>>>> pxSelect = new PXSelect<FSPostBatch, Where<FSPostBatch.batchID, Equal<Required<FSPostBatch.batchID>>>>(graph);
    FSPostBatch fsPostBatch = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) pxSelect).Select(new object[1]
    {
      (object) batchID
    }));
    if (fsPostBatch == null)
      return;
    graph.Caches[typeof (FSPostDet)].ClearQueryCache();
    graph.Caches[typeof (FSPostDoc)].ClearQueryCache();
    int num = ((IQueryable<PXResult<FSPostDoc>>) PXSelectBase<FSPostDoc, PXSelectReadonly<FSPostDoc, Where<FSPostDoc.batchID, Equal<Required<FSPostDoc.batchID>>>>.Config>.Select(graph, new object[1]
    {
      (object) batchID
    })).Count<PXResult<FSPostDoc>>();
    if (num == 0)
    {
      ((PXSelectBase<FSPostBatch>) pxSelect).Delete(fsPostBatch);
      ((PXSelectBase) pxSelect).Cache.Persist((object) fsPostBatch, (PXDBOperation) 3);
    }
    else
    {
      fsPostBatch.QtyDoc = new int?(num);
      ((PXSelectBase<FSPostBatch>) pxSelect).Update(fsPostBatch);
      ((PXSelectBase) pxSelect).Cache.Persist((object) fsPostBatch, (PXDBOperation) 1);
    }
  }

  /// <summary>
  /// Cleans the posting information <c>(FSContractPostDoc, FSContractPostDet, FSContractPostBatch, FSContractPostRegister)</c>
  /// when erasing the entire posted document (SO, AR) coming from a contract.
  /// </summary>
  public virtual void CleanContractPostingInfoLinkedToDoc(object postedDoc)
  {
    if (postedDoc == null)
      return;
    PXGraph pxGraph = new PXGraph();
    string str1;
    string str2;
    string str3;
    if (postedDoc is PX.Objects.SO.SOOrder)
    {
      PX.Objects.SO.SOOrder soOrder = (PX.Objects.SO.SOOrder) postedDoc;
      str1 = soOrder.OrderType;
      str2 = soOrder.OrderNbr;
      str3 = "SO";
    }
    else
    {
      PX.Objects.AR.ARInvoice arInvoice = postedDoc is PX.Objects.AR.ARInvoice ? (PX.Objects.AR.ARInvoice) postedDoc : throw new NotImplementedException();
      str1 = arInvoice.DocType;
      str2 = arInvoice.RefNbr;
      str3 = "AR";
    }
    PXResultset<FSContractPostDoc> source = PXSelectBase<FSContractPostDoc, PXSelect<FSContractPostDoc, Where<FSContractPostDoc.postDocType, Equal<Required<FSContractPostDoc.postDocType>>, And<FSContractPostDoc.postRefNbr, Equal<Required<FSContractPostDoc.postRefNbr>>, And<FSContractPostDoc.postedTO, Equal<Required<FSContractPostDoc.postedTO>>>>>>.Config>.Select(pxGraph, new object[3]
    {
      (object) str1,
      (object) str2,
      (object) str3
    });
    if (source.Count <= 0)
      return;
    int? contractPostBatchId = ((PXResult) ((IQueryable<PXResult<FSContractPostDoc>>) source).FirstOrDefault<PXResult<FSContractPostDoc>>()).GetItem<FSContractPostDoc>().ContractPostBatchID;
    int? contractPostDocId = ((PXResult) ((IQueryable<PXResult<FSContractPostDoc>>) source).FirstOrDefault<PXResult<FSContractPostDoc>>()).GetItem<FSContractPostDoc>().ContractPostDocID;
    int? serviceContractId = ((PXResult) ((IQueryable<PXResult<FSContractPostDoc>>) source).FirstOrDefault<PXResult<FSContractPostDoc>>()).GetItem<FSContractPostDoc>().ServiceContractID;
    int? contractPeriodId = ((PXResult) ((IQueryable<PXResult<FSContractPostDoc>>) source).FirstOrDefault<PXResult<FSContractPostDoc>>()).GetItem<FSContractPostDoc>().ContractPeriodID;
    PXDatabase.Delete<FSContractPostRegister>(new PXDataFieldRestrict[4]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostRegister.contractPostBatchID>((object) contractPostBatchId),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostRegister.postedTO>((object) str3),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostRegister.postDocType>((object) str1),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostRegister.postRefNbr>((object) str2)
    });
    PXDatabase.Delete<FSContractPostDoc>(new PXDataFieldRestrict[4]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostDoc.contractPostDocID>((object) contractPostDocId),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostDoc.postedTO>((object) str3),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostDoc.postDocType>((object) str1),
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostDoc.postRefNbr>((object) str2)
    });
    PXDatabase.Delete<FSContractPostDet>(new PXDataFieldRestrict[1]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSContractPostDet.contractPostDocID>((object) contractPostDocId)
    });
    PXUpdate<Set<FSContractPeriod.invoiced, False, Set<FSContractPeriod.status, ListField_Status_ContractPeriod.Pending>>, FSContractPeriod, Where<FSContractPeriod.serviceContractID, Equal<Required<FSContractPeriod.serviceContractID>>, And<FSContractPeriod.contractPeriodID, Equal<Required<FSContractPeriod.contractPeriodID>>>>>.Update(pxGraph, new object[2]
    {
      (object) serviceContractId,
      (object) contractPeriodId
    });
    ContractPostBatchMaint instance = PXGraph.CreateInstance<ContractPostBatchMaint>();
    ((PXSelectBase<FSContractPostBatch>) instance.ContractBatchRecords).Current = PXResultset<FSContractPostBatch>.op_Implicit(((PXSelectBase<FSContractPostBatch>) instance.ContractBatchRecords).Search<FSContractPostDoc.contractPostBatchID>((object) contractPostBatchId, Array.Empty<object>()));
    if (((IQueryable<PXResult<ContractPostBatchDetail>>) ((PXSelectBase<ContractPostBatchDetail>) instance.ContractPostDocRecords).Select(Array.Empty<object>())).Count<PXResult<ContractPostBatchDetail>>() == 0)
      ((PXSelectBase<FSContractPostBatch>) instance.ContractBatchRecords).Delete(((PXSelectBase<FSContractPostBatch>) instance.ContractBatchRecords).Current);
    ((PXAction) instance.Save).Press();
  }

  public virtual void CreateBillHistoryRowsForDocument(
    PXGraph graph,
    string childEntityType,
    string childDocType,
    string childRefNbr,
    string parentEntityType,
    string parentDocType,
    string parentRefNbr)
  {
    PXCache cacheARTran = graph.Caches[typeof (PX.Objects.AR.ARTran)];
    IEnumerable<IGrouping<string, \u003C\u003Ef__AnonymousType4<PX.Objects.AR.ARTran, FSxARTran>>> source1 = GraphHelper.RowCast<PX.Objects.AR.ARTran>((IEnumerable) PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) childDocType,
      (object) childRefNbr
    })).Select(x => new
    {
      arTran = x,
      fsxARTran = cacheARTran.GetExtension<FSxARTran>((object) x)
    }).GroupBy(x => x.fsxARTran.ServiceContractRefNbr);
    if (source1.Count<IGrouping<string, \u003C\u003Ef__AnonymousType4<PX.Objects.AR.ARTran, FSxARTran>>>() <= 0)
      return;
    PXCache cach = graph.Caches[typeof (FSBillHistory)];
    foreach (IGrouping<string, \u003C\u003Ef__AnonymousType4<PX.Objects.AR.ARTran, FSxARTran>> source2 in source1)
    {
      var data1 = source2.First();
      if (data1.fsxARTran.ServiceContractRefNbr != null)
      {
        PX.Objects.AR.ARTran copy = (PX.Objects.AR.ARTran) cacheARTran.CreateCopy((object) data1.arTran);
        FSxARTran extension1 = cacheARTran.GetExtension<FSxARTran>((object) copy);
        extension1.SrvOrdType = (string) null;
        extension1.ServiceOrderRefNbr = (string) null;
        extension1.AppointmentRefNbr = (string) null;
        this.CreateBillHistoryRowsFromARTran(cach, copy, extension1, childEntityType, childDocType, childRefNbr, parentEntityType, parentDocType, parentRefNbr, false);
        if (parentEntityType != null && parentDocType != null && parentRefNbr != null)
        {
          PXResultset<FSBillHistory> pxResultset = PXSelectBase<FSBillHistory, PXSelect<FSBillHistory, Where<FSBillHistory.childEntityType, Equal<Required<FSBillHistory.childEntityType>>, And<FSBillHistory.childDocType, Equal<Required<FSBillHistory.childDocType>>, And<FSBillHistory.childRefNbr, Equal<Required<FSBillHistory.childRefNbr>>, And<FSBillHistory.srvOrdType, IsNotNull>>>>>.Config>.Select(graph, new object[3]
          {
            (object) parentEntityType,
            (object) parentDocType,
            (object) parentRefNbr
          });
          PX.Objects.AR.ARTran arTran = new PX.Objects.AR.ARTran();
          FSxARTran extension2 = cacheARTran.GetExtension<FSxARTran>((object) arTran);
          extension2.ServiceContractRefNbr = extension1.ServiceContractRefNbr;
          foreach (PXResult<FSBillHistory> pxResult in pxResultset)
          {
            FSBillHistory fsBillHistory = PXResult<FSBillHistory>.op_Implicit(pxResult);
            extension2.SrvOrdType = fsBillHistory.SrvOrdType;
            extension2.ServiceOrderRefNbr = fsBillHistory.ServiceOrderRefNbr;
            extension2.AppointmentRefNbr = fsBillHistory.AppointmentRefNbr;
            this.CreateBillHistoryRowsFromARTran(cach, arTran, extension2, childEntityType, childDocType, childRefNbr, parentEntityType, parentDocType, parentRefNbr, false);
          }
        }
      }
      else
      {
        foreach (var data2 in EnumerableExtensions.Distinct(source2, x => (x.fsxARTran.SrvOrdType, x.fsxARTran.ServiceOrderRefNbr, x.fsxARTran.AppointmentRefNbr)))
          this.CreateBillHistoryRowsFromARTran(cach, data2.arTran, data2.fsxARTran, childEntityType, childDocType, childRefNbr, parentEntityType, parentDocType, parentRefNbr, true);
      }
    }
    cach.Persist((PXDBOperation) 2);
  }

  public virtual FSBillHistory CreateBillHistoryRowsFromARTran(
    PXCache billHistoryCache,
    PX.Objects.AR.ARTran arTran,
    FSxARTran fsxARTranRow,
    string childEntityType,
    string childDocType,
    string childRefNbr,
    string parentEntityType,
    string parentDocType,
    string parentRefNbr,
    bool verifyIfExists)
  {
    if (parentEntityType == "PXSO" && (arTran.SOOrderType == null || arTran.SOOrderNbr == null))
      return (FSBillHistory) null;
    FSBillHistory fsBillHistory = new FSBillHistory()
    {
      SrvOrdType = fsxARTranRow.SrvOrdType,
      ServiceOrderRefNbr = fsxARTranRow.ServiceOrderRefNbr,
      AppointmentRefNbr = fsxARTranRow.AppointmentRefNbr,
      ServiceContractRefNbr = fsxARTranRow.ServiceContractRefNbr,
      ParentEntityType = parentEntityType,
      ParentDocType = !(parentEntityType == "PXSO") || parentDocType != null ? parentDocType : arTran.SOOrderType,
      ParentRefNbr = !(parentEntityType == "PXSO") || parentRefNbr != null ? parentRefNbr : arTran.SOOrderNbr,
      ChildEntityType = childEntityType,
      ChildDocType = childDocType,
      ChildRefNbr = childRefNbr
    };
    if (verifyIfExists)
    {
      FSBillHistory dirty = FSBillHistory.UK.FindDirty(billHistoryCache.Graph, fsBillHistory.SrvOrdType, fsBillHistory.ServiceOrderRefNbr, fsBillHistory.AppointmentRefNbr, fsBillHistory.ParentEntityType, fsBillHistory.ParentDocType, fsBillHistory.ParentRefNbr, fsBillHistory.ChildEntityType, fsBillHistory.ChildDocType, fsBillHistory.ChildRefNbr);
      if (dirty != null)
        return dirty;
    }
    return (FSBillHistory) billHistoryCache.Insert((object) fsBillHistory);
  }

  public virtual void CleanPostingInfoFromSOCreditMemo(PXGraph graph, PX.Objects.SO.SOInvoice crmSOInvoiceRow)
  {
    if (crmSOInvoiceRow.DocType != "CRM")
      return;
    foreach (PX.Objects.AR.ARTran arTran1 in GraphHelper.RowCast<PX.Objects.AR.ARTran>((IEnumerable) PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) crmSOInvoiceRow.DocType,
      (object) crmSOInvoiceRow.RefNbr
    })).GroupBy<PX.Objects.AR.ARTran, (string, string)>((Func<PX.Objects.AR.ARTran, (string, string)>) (x => (x.OrigInvoiceType, x.OrigInvoiceNbr))).Select<IGrouping<(string, string), PX.Objects.AR.ARTran>, PX.Objects.AR.ARTran>((Func<IGrouping<(string, string), PX.Objects.AR.ARTran>, PX.Objects.AR.ARTran>) (y => y.OrderByDescending<PX.Objects.AR.ARTran, string>((Func<PX.Objects.AR.ARTran, string>) (z => z.RefNbr)).First<PX.Objects.AR.ARTran>())).ToList<PX.Objects.AR.ARTran>())
    {
      PX.Objects.AR.ARTran arTran2 = GraphHelper.RowCast<PX.Objects.AR.ARTran>((IEnumerable) PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>.Config>.Select(graph, new object[2]
      {
        (object) arTran1.OrigInvoiceType,
        (object) arTran1.OrigInvoiceNbr
      })).Where<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (_ => _.SOOrderType != null && _.SOOrderNbr != null)).GroupBy<PX.Objects.AR.ARTran, (string, string)>((Func<PX.Objects.AR.ARTran, (string, string)>) (x => (x.SOOrderType, x.SOOrderNbr))).Select<IGrouping<(string, string), PX.Objects.AR.ARTran>, PX.Objects.AR.ARTran>((Func<IGrouping<(string, string), PX.Objects.AR.ARTran>, PX.Objects.AR.ARTran>) (y => y.OrderByDescending<PX.Objects.AR.ARTran, string>((Func<PX.Objects.AR.ARTran, string>) (z => z.SOOrderNbr)).First<PX.Objects.AR.ARTran>())).FirstOrDefault<PX.Objects.AR.ARTran>();
      if (arTran2 != null && !string.IsNullOrEmpty(arTran2.SOOrderNbr))
      {
        object obj = (object) PXResult<PX.Objects.SO.SOOrder>.op_Implicit(((IQueryable<PXResult<PX.Objects.SO.SOOrder>>) PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select(graph, new object[2]
        {
          (object) arTran2.SOOrderType,
          (object) arTran2.SOOrderNbr
        })).FirstOrDefault<PXResult<PX.Objects.SO.SOOrder>>());
        SM_SOOrderEntry extension = ((PXGraph) PXGraph.CreateInstance<SOOrderEntry>()).GetExtension<SM_SOOrderEntry>();
        extension.CleanPostingInfoLinkedToDoc(obj);
        extension.CleanContractPostingInfoLinkedToDoc(obj);
      }
      else
        this.CleanPostingInfoLinkedToDoc((object) PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(PXSelectBase<PX.Objects.SO.SOInvoice, PXSelect<PX.Objects.SO.SOInvoice, Where<PX.Objects.SO.SOInvoice.docType, Equal<Required<PX.Objects.SO.SOInvoice.docType>>, And<PX.Objects.SO.SOInvoice.refNbr, Equal<Required<PX.Objects.SO.SOInvoice.refNbr>>>>>.Config>.Select(graph, new object[2]
        {
          (object) arTran1.OrigInvoiceType,
          (object) arTran1.OrigInvoiceNbr
        })));
    }
  }

  public virtual void SetExtensionVisibleInvisible<DAC>(
    PXCache cache,
    PXRowSelectedEventArgs e,
    bool isVisible,
    bool isGrid)
    where DAC : PXCacheExtension
  {
    foreach (string str in this.GetFieldsName(typeof (DAC)))
      PXUIFieldAttribute.SetVisible(cache, (object) null, str, isVisible);
  }

  public virtual List<string> GetFieldsName(System.Type dacType)
  {
    List<string> fieldsName = new List<string>();
    foreach (PropertyInfo property in dacType.GetProperties())
    {
      if (((IEnumerable<object>) property.GetCustomAttributes(true)).Where<object>((Func<object, bool>) (atr => atr is SkipSetExtensionVisibleInvisibleAttribute)).Count<object>() == 0)
        fieldsName.Add(property.Name);
    }
    return fieldsName;
  }

  public class ContactAddressSource
  {
    public string BillingSource;
    public string ShippingSource;
  }
}
