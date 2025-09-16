// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ValidateARDocumentAddressProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.Standalone;
using PX.Objects.CR;
using PX.Objects.GL;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class ValidateARDocumentAddressProcess : 
  ValidateDocumentAddressGraph<ValidateARDocumentAddressProcess>
{
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
  [DocumentTypeField.List(DocumentTypeField.SetOfValues.ARRelatedDocumentTypes)]
  public virtual void _(
    PX.Data.Events.CacheAttached<ValidateDocumentAddressFilter.documentType> e)
  {
  }

  protected override IEnumerable GetAddressRecords(ValidateDocumentAddressFilter filter)
  {
    IEnumerable addressRecords = (IEnumerable) new List<UnvalidatedAddress>();
    switch (filter.DocumentType.Trim())
    {
      case "IM":
        addressRecords = this.AddARInvoiceAddresses(filter);
        break;
      case "CS":
        addressRecords = this.AddCashSalesAddresses(filter);
        break;
    }
    return addressRecords;
  }

  protected virtual IEnumerable AddARInvoiceAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateARDocumentAddressProcess documentAddressProcess = this;
    List<object> objectList1 = new List<object>();
    List<object> billAddresses = new List<object>();
    object[] objArray1 = (object[]) new string[10]
    {
      "H",
      "R",
      "P",
      "E",
      "W",
      "B",
      "S",
      "N",
      "U",
      "C"
    };
    object[] objArray2 = (object[]) new string[2]
    {
      "CSL",
      "RCS"
    };
    object[] objArray3 = new object[2]
    {
      (object) objArray1,
      (object) objArray2
    };
    PXSelectBase<ARInvoice> pxSelectBase1 = (PXSelectBase<ARInvoice>) new PXSelectJoin<ARInvoice, InnerJoin<Customer, On<ARInvoice.customerID, Equal<Customer.bAccountID>>, InnerJoin<ARShippingAddress, On<ARInvoice.shipAddressID, Equal<ARShippingAddress.addressID>>>>, Where<ARShippingAddress.isDefaultBillAddress, Equal<False>, And<ARShippingAddress.isValidated, Equal<False>, And<ARInvoice.released, Equal<False>, And<ARInvoice.status, In<Required<ARInvoice.status>>, And<ARInvoice.docType, NotIn<Required<ARInvoice.docType>>, And<ARInvoice.origModule, NotEqual<BatchModule.moduleSO>>>>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<ARInvoice> pxSelectBase2 = (PXSelectBase<ARInvoice>) new PXSelectJoin<ARInvoice, InnerJoin<Customer, On<ARInvoice.customerID, Equal<Customer.bAccountID>>, InnerJoin<ARAddress, On<ARInvoice.billAddressID, Equal<ARAddress.addressID>>>>, Where<ARAddress.isDefaultBillAddress, Equal<False>, And<ARAddress.isValidated, Equal<False>, And<ARInvoice.released, Equal<False>, And<ARInvoice.status, In<Required<ARInvoice.status>>, And<ARInvoice.docType, NotIn<Required<ARInvoice.docType>>, And<ARInvoice.origModule, NotEqual<BatchModule.moduleSO>>>>>>>>((PXGraph) documentAddressProcess);
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray3 = new object[3]
      {
        (object) objArray1,
        (object) objArray2,
        (object) filter.Country
      };
      pxSelectBase1.WhereAnd<Where<ARShippingAddress.countryID, Equal<Required<ARShippingAddress.countryID>>>>();
      pxSelectBase2.WhereAnd<Where<ARAddress.countryID, Equal<Required<ARAddress.countryID>>>>();
    }
    List<object> objectList2 = ((PXSelectBase) pxSelectBase1).View.SelectMulti(objArray3);
    billAddresses = ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray3);
    foreach (PXResult<ARInvoice, Customer, ARShippingAddress> pxResult in objectList2)
    {
      ARShippingAddress address = PXResult<ARInvoice, Customer, ARShippingAddress>.op_Implicit(pxResult);
      ARInvoice document = PXResult<ARInvoice, Customer, ARShippingAddress>.op_Implicit(pxResult);
      string str = new ARInvoiceType.ListAttribute().ValueLabelDic[document.DocType];
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<ARShippingAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.RefNbr : $"{str}, {document.RefNbr}", "Invoices and Memos", new ARDocStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<ARInvoice, Customer, ARAddress> pxResult in billAddresses)
    {
      ARAddress address = PXResult<ARInvoice, Customer, ARAddress>.op_Implicit(pxResult);
      ARInvoice document = PXResult<ARInvoice, Customer, ARAddress>.op_Implicit(pxResult);
      string str = new ARInvoiceType.ListAttribute().ValueLabelDic[document.DocType];
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<ARAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.RefNbr : $"{str}, {document.RefNbr}", "Invoices and Memos", new ARDocStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }

  protected virtual IEnumerable AddCashSalesAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateARDocumentAddressProcess documentAddressProcess = this;
    List<object> shipAddresses = new List<object>();
    List<object> objectList1 = new List<object>();
    PXSelectBase<ARCashSale> pxSelectBase1 = (PXSelectBase<ARCashSale>) new PXSelectJoin<ARCashSale, InnerJoin<ARAddress, On<ARCashSale.billAddressID, Equal<ARAddress.addressID>>>, Where<ARAddress.isDefaultBillAddress, Equal<False>, And<ARAddress.isValidated, Equal<False>, And<ARCashSale.released, Equal<False>, And<ARRegister.origModule, NotEqual<BatchModule.moduleSO>>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<ARCashSale> pxSelectBase2 = (PXSelectBase<ARCashSale>) new PXSelectJoin<ARCashSale, InnerJoin<ARShippingAddress, On<ARCashSale.shipAddressID, Equal<ARShippingAddress.addressID>>>, Where<ARShippingAddress.isDefaultBillAddress, Equal<False>, And<ARShippingAddress.isValidated, Equal<False>, And<ARCashSale.released, Equal<False>, And<ARRegister.origModule, NotEqual<BatchModule.moduleSO>>>>>>((PXGraph) documentAddressProcess);
    object[] objArray = new object[0];
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray = new object[1]{ (object) filter.Country };
      pxSelectBase1.WhereAnd<Where<ARAddress.countryID, Equal<Required<ARAddress.countryID>>>>();
      pxSelectBase2.WhereAnd<Where<ARShippingAddress.countryID, Equal<Required<ARShippingAddress.countryID>>>>();
    }
    List<object> objectList2 = ((PXSelectBase) pxSelectBase1).View.SelectMulti(objArray);
    shipAddresses = ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray);
    foreach (PXResult<ARCashSale, ARAddress> pxResult in objectList2)
    {
      ARAddress address = PXResult<ARCashSale, ARAddress>.op_Implicit(pxResult);
      ARCashSale document = PXResult<ARCashSale, ARAddress>.op_Implicit(pxResult);
      string str = new ARCashSaleType.ListAttribute().ValueLabelDic[document.DocType];
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<ARAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.RefNbr : $"{str}, {document.RefNbr}", "Cash Sales", new ARDocStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<ARCashSale, ARShippingAddress> pxResult in shipAddresses)
    {
      ARShippingAddress address = PXResult<ARCashSale, ARShippingAddress>.op_Implicit(pxResult);
      ARCashSale document = PXResult<ARCashSale, ARShippingAddress>.op_Implicit(pxResult);
      string str = new ARCashSaleType.ListAttribute().ValueLabelDic[document.DocType];
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<ARShippingAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.RefNbr : $"{str}, {document.RefNbr}", "Cash Sales", new ARDocStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }
}
