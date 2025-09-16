// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.CustomerDataReader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.CA;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public class CustomerDataReader : ICustomerDataReader
{
  private PXGraph _graph;
  private int? _customerID;
  private string _customerCD;
  private int? _pminstanceID;
  private string _prefixForCustomerCD;
  private CCProcessingCenter _processingCenter;
  private const string key_CustomerCD = "CustomerCD";
  private const string key_CustomerName = "CustomerName";
  private const string key_Customer_FirstName = "CustomerFirstName";
  private const string key_Customer_LastName = "CustomerLastName";
  private const string key_Country = "CountryID";
  private const string key_State = "State";
  private const string key_City = "City";
  private const string key_Address = "Address";
  private const string key_AddressLine1 = "AddressLine1";
  private const string key_AddressLine2 = "AddressLine2";
  private const string key_AddressLine3 = "AddressLine3";
  private const string key_PostalCode = "PostalCode";
  private const string key_Phone = "Phone";
  private const string key_Fax = "Fax";
  private const string key_Email = "Email";
  private const string Key_Customer_CCProcessingID = "CCProcessingID";

  public CustomerDataReader(CCProcessingContext context)
  {
    PXGraph callerGraph = context.callerGraph;
    int? aCustomerId = context.aCustomerID;
    string aCustomerCd = context.aCustomerCD;
    int? aPmInstanceId = context.aPMInstanceID;
    if (callerGraph == null)
      throw new ArgumentNullException("graph");
    if (!aCustomerId.HasValue && string.IsNullOrEmpty(aCustomerCd))
      throw new ArgumentNullException("customerId", "Either customerId or customerCD must be not null");
    this._graph = callerGraph;
    this._customerID = aCustomerId;
    this._customerCD = aCustomerCd;
    this._pminstanceID = aPmInstanceId;
    this._processingCenter = context.processingCenter;
    this._prefixForCustomerCD = context.PrefixForCustomerCD;
  }

  void ICustomerDataReader.ReadData(Dictionary<string, string> aData)
  {
    PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Address, PX.Objects.CR.Contact> pxResult1 = (PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Address, PX.Objects.CR.Contact>) null;
    PXResult<PX.Objects.AR.CustomerPaymentMethod> pxResult2 = (PXResult<PX.Objects.AR.CustomerPaymentMethod>) null;
    int? customerId = this._customerID;
    int num = 0;
    if (!(customerId.GetValueOrDefault() == num & customerId.HasValue))
      pxResult1 = (PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Address, PX.Objects.CR.Contact>) PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelectJoin<PX.Objects.AR.Customer, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defBillAddressID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) this._customerID
      }));
    else if (!string.IsNullOrEmpty(this._customerCD))
      pxResult1 = (PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Address, PX.Objects.CR.Contact>) PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelectJoin<PX.Objects.AR.Customer, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defBillAddressID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>>, Where<PX.Objects.AR.Customer.acctCD, Equal<Required<PX.Objects.AR.Customer.acctCD>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) this._customerCD
      }));
    if (this._pminstanceID.HasValue)
      pxResult2 = PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) this._pminstanceID
      }));
    if (pxResult1 == null)
      return;
    PX.Objects.AR.Customer customer = PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Address, PX.Objects.CR.Contact>.op_Implicit(pxResult1);
    PX.Objects.CR.Address aAddress = PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Address, PX.Objects.CR.Contact>.op_Implicit(pxResult1);
    PX.Objects.CR.Contact contact1 = PXResult<PX.Objects.AR.Customer, PX.Objects.CR.Address, PX.Objects.CR.Contact>.op_Implicit(pxResult1);
    if (pxResult2 != null)
    {
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PXResult<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(pxResult2);
      PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) customerPaymentMethod.BillAddressID
      }));
      if (address != null)
        aAddress = address;
      PX.Objects.CR.Contact contact2 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) customerPaymentMethod.BillContactID
      }));
      if (contact2 != null)
        contact1 = contact2;
      if (pxResult2 != null && !string.IsNullOrEmpty(PXResult<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(pxResult2).CustomerCCPID))
        aData["CCProcessingID"] = PXResult<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(pxResult2).CustomerCCPID;
    }
    else if (this._processingCenter != null)
    {
      string customerCcpid = PXResultset<CustomerProcessingCenterID>.op_Implicit(PXSelectBase<CustomerProcessingCenterID, PXSelect<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.bAccountID, Equal<Required<CustomerProcessingCenterID.bAccountID>>, And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<CustomerProcessingCenterID.cCProcessingCenterID>>>>>.Config>.Select(this._graph, new object[2]
      {
        (object) this._customerID,
        (object) this._processingCenter.ProcessingCenterID
      }))?.CustomerCCPID;
      if (!string.IsNullOrEmpty(customerCcpid))
        aData["CCProcessingID"] = customerCcpid;
    }
    string str = customer.AcctCD;
    if (str != null)
      str = str.Trim();
    aData["CustomerCD"] = (this._prefixForCustomerCD ?? string.Empty) + (str ?? string.Empty);
    aData["CustomerName"] = customer.AcctName;
    aData["CountryID"] = aAddress?.CountryID;
    aData["State"] = aAddress?.State;
    aData["City"] = aAddress?.City;
    aData["Address"] = CCProcessingHelper.ExtractStreetAddress((IAddressBase) aAddress);
    aData["AddressLine1"] = aAddress.AddressLine1;
    aData["AddressLine2"] = aAddress.AddressLine2;
    aData["AddressLine3"] = aAddress.AddressLine3;
    aData["PostalCode"] = aAddress?.PostalCode;
    aData["CustomerFirstName"] = contact1?.FirstName;
    aData["CustomerLastName"] = contact1?.LastName;
    aData["Phone"] = contact1?.Phone1;
    aData["Fax"] = contact1?.Fax;
    aData["Email"] = contact1?.EMail;
  }

  string ICustomerDataReader.Key_CustomerName => "CustomerName";

  string ICustomerDataReader.Key_CustomerCD => "CustomerCD";

  string ICustomerDataReader.Key_Customer_FirstName => "CustomerFirstName";

  string ICustomerDataReader.Key_Customer_LastName => "CustomerLastName";

  string ICustomerDataReader.Key_BillAddr_Country => "CountryID";

  string ICustomerDataReader.Key_BillAddr_State => "State";

  string ICustomerDataReader.Key_BillAddr_City => "City";

  string ICustomerDataReader.Key_BillAddr_Address => "Address";

  string ICustomerDataReader.Key_BillAddr_AddressLine1 => "AddressLine1";

  string ICustomerDataReader.Key_BillAddr_AddressLine2 => "AddressLine2";

  string ICustomerDataReader.Key_BillAddr_AddressLine3 => "AddressLine3";

  string ICustomerDataReader.Key_BillAddr_PostalCode => "PostalCode";

  string ICustomerDataReader.Key_BillContact_Phone => "Phone";

  string ICustomerDataReader.Key_BillContact_Fax => "Fax";

  string ICustomerDataReader.Key_BillContact_Email => "Email";

  string ICustomerDataReader.Key_Customer_CCProcessingID => "CCProcessingID";
}
