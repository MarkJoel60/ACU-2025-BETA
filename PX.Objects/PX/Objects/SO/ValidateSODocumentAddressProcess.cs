// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ValidateSODocumentAddressProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.GL;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class ValidateSODocumentAddressProcess : 
  ValidateDocumentAddressGraph<ValidateSODocumentAddressProcess>
{
  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  public virtual void _(
    PX.Data.Events.CacheAttached<SOShippingAddress.customerID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  public virtual void _(
    PX.Data.Events.CacheAttached<SOBillingAddress.customerID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  public virtual void _(
    PX.Data.Events.CacheAttached<SOShipmentAddress.customerID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  public virtual void _(
    PX.Data.Events.CacheAttached<ARShippingAddress.customerID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<ARAddress.customerID> e)
  {
  }

  [PXMergeAttributes]
  [DocumentTypeField.List(DocumentTypeField.SetOfValues.SORelatedDocumentTypes)]
  public virtual void _(
    PX.Data.Events.CacheAttached<ValidateDocumentAddressFilter.documentType> e)
  {
  }

  protected override IEnumerable GetAddressRecords(ValidateDocumentAddressFilter filter)
  {
    IEnumerable addressRecords = (IEnumerable) new List<UnvalidatedAddress>();
    switch (filter.DocumentType.Trim())
    {
      case "SO":
        addressRecords = this.AddSalesOrderAddresses(filter);
        break;
      case "SH":
        addressRecords = this.AddShipmentAddresses(filter);
        break;
      case "IN":
        addressRecords = this.AddSOInvoiceAddresses(filter);
        break;
    }
    return addressRecords;
  }

  protected virtual IEnumerable AddSalesOrderAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateSODocumentAddressProcess documentAddressProcess = this;
    object[] objArray1 = (object[]) new string[4]
    {
      "H",
      "N",
      "C",
      "L"
    };
    object[] objArray2 = (object[]) new string[5]
    {
      "H",
      "R",
      "N",
      "C",
      "L"
    };
    object[] objArray3 = (object[]) new string[4]
    {
      "H",
      "N",
      "C",
      "L"
    };
    object[] objArray4 = (object[]) new string[7]
    {
      "H",
      "R",
      "E",
      "A",
      "N",
      "C",
      "L"
    };
    object[] objArray5 = (object[]) new string[8]
    {
      "H",
      "R",
      "E",
      "A",
      "N",
      "B",
      "C",
      "L"
    };
    object[] objArray6 = (object[]) new string[5]
    {
      "H",
      "N",
      "B",
      "C",
      "L"
    };
    Dictionary<string, object[]> dictionary = new Dictionary<string, object[]>();
    dictionary.Add("CM", objArray1);
    dictionary.Add("IN", objArray2);
    dictionary.Add("MO", objArray2);
    dictionary.Add("QT", objArray3);
    dictionary.Add("RM", objArray4);
    dictionary.Add("SO", objArray5);
    dictionary.Add("TR", objArray6);
    List<object> objectList = new List<object>();
    List<object> soBillAddresses = new List<object>();
    PXSelectBase<SOOrder> pxSelectBase1 = (PXSelectBase<SOOrder>) new PXSelectJoin<SOOrder, InnerJoin<SOShippingAddress, On<SOOrder.shipAddressID, Equal<SOShippingAddress.addressID>>>, Where<SOShippingAddress.isDefaultAddress, Equal<False>, And<SOShippingAddress.isValidated, Equal<False>, And<SOOrder.completed, Equal<False>, And<SOOrder.cancelled, Equal<False>, And<SOOrder.behavior, Equal<Required<SOOrder.behavior>>, And<SOOrder.status, In<Required<SOOrder.status>>>>>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<SOOrder> pxSelectBase2 = (PXSelectBase<SOOrder>) new PXSelectJoin<SOOrder, InnerJoin<SOBillingAddress, On<SOOrder.billAddressID, Equal<SOBillingAddress.addressID>>>, Where<SOBillingAddress.isDefaultAddress, Equal<False>, And<SOBillingAddress.isValidated, Equal<False>, And<SOOrder.completed, Equal<False>, And<SOOrder.cancelled, Equal<False>, And<SOOrder.behavior, Equal<Required<SOOrder.behavior>>, And<SOOrder.status, In<Required<SOOrder.status>>>>>>>>>((PXGraph) documentAddressProcess);
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      pxSelectBase1.WhereAnd<Where<SOShippingAddress.countryID, Equal<Required<SOShippingAddress.countryID>>>>();
      pxSelectBase2.WhereAnd<Where<SOBillingAddress.countryID, Equal<Required<SOBillingAddress.countryID>>>>();
    }
    foreach (KeyValuePair<string, object[]> keyValuePair in dictionary)
    {
      object[] objArray7;
      if (!string.IsNullOrEmpty(filter?.Country))
        objArray7 = new object[3]
        {
          (object) keyValuePair.Key,
          (object) keyValuePair.Value,
          (object) filter.Country
        };
      else
        objArray7 = new object[2]
        {
          (object) keyValuePair.Key,
          (object) keyValuePair.Value
        };
      object[] objArray8 = objArray7;
      objectList.AddRange((IEnumerable<object>) ((PXSelectBase) pxSelectBase1).View.SelectMulti(objArray8));
      soBillAddresses.AddRange((IEnumerable<object>) ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray8));
    }
    foreach (PXResult<SOOrder, SOShippingAddress> pxResult in objectList)
    {
      SOShippingAddress address = PXResult<SOOrder, SOShippingAddress>.op_Implicit(pxResult);
      SOOrder document = PXResult<SOOrder, SOShippingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<SOShippingAddress>(address, (IBqlTable) document, $"{document.OrderType}, {document.OrderNbr}", "Sales Orders", new SOOrderStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<SOOrder, SOBillingAddress> pxResult in soBillAddresses)
    {
      SOBillingAddress address = PXResult<SOOrder, SOBillingAddress>.op_Implicit(pxResult);
      SOOrder document = PXResult<SOOrder, SOBillingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<SOBillingAddress>(address, (IBqlTable) document, $"{document.OrderType}, {document.OrderNbr}", "Sales Orders", new SOOrderStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }

  protected virtual IEnumerable AddShipmentAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateSODocumentAddressProcess documentAddressProcess = this;
    object[] objArray1 = (object[]) new string[5]
    {
      "H",
      "N",
      "F",
      "Y",
      "I"
    };
    List<object> objectList = new List<object>();
    PXSelectBase<SOShipment> pxSelectBase = (PXSelectBase<SOShipment>) new PXSelectJoin<SOShipment, InnerJoin<SOShipmentAddress, On<SOShipment.shipAddressID, Equal<SOShipmentAddress.addressID>>>, Where<SOShipmentAddress.isDefaultAddress, Equal<False>, And<SOShipmentAddress.isValidated, Equal<False>, And<SOShipment.confirmed, Equal<False>, And<SOShipment.status, In<Required<SOShipment.status>>>>>>>((PXGraph) documentAddressProcess);
    object[] objArray2 = new object[1]{ (object) objArray1 };
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray2 = new object[2]
      {
        (object) objArray1,
        (object) filter.Country
      };
      pxSelectBase.WhereAnd<Where<SOShipmentAddress.countryID, Equal<Required<SOShipmentAddress.countryID>>>>();
    }
    foreach (PXResult<SOShipment, SOShipmentAddress> pxResult in ((PXSelectBase) pxSelectBase).View.SelectMulti(objArray2))
    {
      SOShipmentAddress address = PXResult<SOShipment, SOShipmentAddress>.op_Implicit(pxResult);
      SOShipment document = PXResult<SOShipment, SOShipmentAddress>.op_Implicit(pxResult);
      string str = new SOShipmentType.ListAttribute().ValueLabelDic[document.ShipmentType];
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<SOShipmentAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.ShipmentNbr : $"{str}, {document.ShipmentNbr}", "Shipments", new SOShipmentStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }

  protected virtual IEnumerable AddSOInvoiceAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateSODocumentAddressProcess documentAddressProcess = this;
    object[] objArray1 = (object[]) new string[8]
    {
      "H",
      "W",
      "R",
      "P",
      "E",
      "B",
      "N",
      "C"
    };
    List<object> objectList1 = new List<object>();
    List<object> billAddresses = new List<object>();
    PXSelectBase<PX.Objects.AR.ARInvoice> pxSelectBase1 = (PXSelectBase<PX.Objects.AR.ARInvoice>) new PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<SOInvoice, On<SOInvoice.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<SOInvoice.docType, Equal<PX.Objects.AR.ARInvoice.docType>>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.ARInvoice.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, InnerJoin<ARShippingAddress, On<PX.Objects.AR.ARInvoice.shipAddressID, Equal<ARShippingAddress.addressID>>>>>, Where<ARShippingAddress.isDefaultBillAddress, Equal<False>, And<ARShippingAddress.isValidated, Equal<False>, And<SOInvoice.released, Equal<False>, And<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>, And<SOInvoice.status, In<Required<SOInvoice.status>>>>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<PX.Objects.AR.ARInvoice> pxSelectBase2 = (PXSelectBase<PX.Objects.AR.ARInvoice>) new PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<SOInvoice, On<SOInvoice.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<SOInvoice.docType, Equal<PX.Objects.AR.ARInvoice.docType>>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.ARInvoice.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, InnerJoin<ARAddress, On<PX.Objects.AR.ARInvoice.billAddressID, Equal<ARAddress.addressID>>>>>, Where<ARAddress.isDefaultBillAddress, Equal<False>, And<ARAddress.isValidated, Equal<False>, And<SOInvoice.released, Equal<False>, And<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>, And<SOInvoice.status, In<Required<SOInvoice.status>>>>>>>>((PXGraph) documentAddressProcess);
    object[] objArray2 = new object[1]{ (object) objArray1 };
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray2 = new object[2]
      {
        (object) objArray1,
        (object) filter.Country
      };
      pxSelectBase1.WhereAnd<Where<ARShippingAddress.countryID, Equal<Required<ARShippingAddress.countryID>>>>();
      pxSelectBase2.WhereAnd<Where<ARAddress.countryID, Equal<Required<ARAddress.countryID>>>>();
    }
    List<object> objectList2 = ((PXSelectBase) pxSelectBase1).View.SelectMulti(objArray2);
    billAddresses = ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray2);
    foreach (PXResult<PX.Objects.AR.ARInvoice, SOInvoice, PX.Objects.AR.Customer, ARShippingAddress> pxResult in objectList2)
    {
      ARShippingAddress address = PXResult<PX.Objects.AR.ARInvoice, SOInvoice, PX.Objects.AR.Customer, ARShippingAddress>.op_Implicit(pxResult);
      PX.Objects.AR.ARInvoice document = PXResult<PX.Objects.AR.ARInvoice, SOInvoice, PX.Objects.AR.Customer, ARShippingAddress>.op_Implicit(pxResult);
      string str = new ARDocType.ListAttribute().ValueLabelDic[document.DocType];
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<ARShippingAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.RefNbr : $"{str}, {document.RefNbr}", "SO Invoices", new ARDocStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<PX.Objects.AR.ARInvoice, SOInvoice, PX.Objects.AR.Customer, ARAddress> pxResult in billAddresses)
    {
      ARAddress address = PXResult<PX.Objects.AR.ARInvoice, SOInvoice, PX.Objects.AR.Customer, ARAddress>.op_Implicit(pxResult);
      PX.Objects.AR.ARInvoice document = PXResult<PX.Objects.AR.ARInvoice, SOInvoice, PX.Objects.AR.Customer, ARAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress1 = new UnvalidatedAddress();
      string str = new ARDocType.ListAttribute().ValueLabelDic[document.DocType];
      UnvalidatedAddress unvalidatedAddress2 = documentAddressProcess.ConvertToUnvalidatedAddress<ARAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.RefNbr : $"{str}, {document.RefNbr}", "SO Invoices", new ARDocStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress2);
    }
  }
}
