// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ValidateCRDocumentAddressProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class ValidateCRDocumentAddressProcess : 
  ValidateDocumentAddressGraph<ValidateCRDocumentAddressProcess>
{
  [PXMergeAttributes]
  [DocumentTypeField.List(DocumentTypeField.SetOfValues.CRRelatedDocumentTypes)]
  public virtual void _(
    PX.Data.Events.CacheAttached<ValidateDocumentAddressFilter.documentType> e)
  {
  }

  protected override IEnumerable GetAddressRecords(ValidateDocumentAddressFilter filter)
  {
    IEnumerable addressRecords = (IEnumerable) new List<UnvalidatedAddress>();
    switch (filter.DocumentType.Trim())
    {
      case "SQ":
        addressRecords = this.AddCRQuoteAddresses(filter);
        break;
      case "OP":
        addressRecords = this.AddCROpportunityAddresses(filter);
        break;
      case "PQ":
        addressRecords = this.AddPMQuoteAddresses(filter);
        break;
    }
    return addressRecords;
  }

  protected virtual IEnumerable AddCRQuoteAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateCRDocumentAddressProcess documentAddressProcess = this;
    List<object> objectList1 = new List<object>();
    List<object> crBillAddresses = new List<object>();
    List<object> crQuoteAddresses = new List<object>();
    object[] objArray = new object[0];
    PXSelectBase<CRQuote> pxSelectBase1 = (PXSelectBase<CRQuote>) new PXSelectJoin<CRQuote, InnerJoin<CRShippingAddress, On<CRQuote.shipAddressID, Equal<CRShippingAddress.addressID>>>, Where<CRQuote.quoteType, Equal<CRQuoteTypeAttribute.distribution>, And<CRShippingAddress.isDefaultAddress, Equal<False>, And<CRShippingAddress.isValidated, Equal<False>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<CRQuote> pxSelectBase2 = (PXSelectBase<CRQuote>) new PXSelectJoin<CRQuote, InnerJoin<CRBillingAddress, On<CRQuote.billAddressID, Equal<CRBillingAddress.addressID>>>, Where<CRQuote.quoteType, Equal<CRQuoteTypeAttribute.distribution>, And<CRBillingAddress.isDefaultAddress, Equal<False>, And<CRBillingAddress.isValidated, Equal<False>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<CRQuote> pxSelectBase3 = (PXSelectBase<CRQuote>) new PXSelectJoin<CRQuote, InnerJoin<CRAddress, On<CRQuote.opportunityAddressID, Equal<CRAddress.addressID>>>, Where<CRQuote.quoteType, Equal<CRQuoteTypeAttribute.distribution>, And<CRAddress.isValidated, Equal<False>, And<Where<CRAddress.isDefaultAddress, Equal<False>, Or<CRQuote.bAccountID, IsNull, And<CRQuote.contactID, IsNull>>>>>>>((PXGraph) documentAddressProcess);
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray = new object[1]{ (object) filter.Country };
      pxSelectBase1.WhereAnd<Where<CRShippingAddress.countryID, Equal<Required<CRShippingAddress.countryID>>>>();
      pxSelectBase2.WhereAnd<Where<CRBillingAddress.countryID, Equal<Required<CRBillingAddress.countryID>>>>();
      pxSelectBase3.WhereAnd<Where<CRAddress.countryID, Equal<Required<CRAddress.countryID>>>>();
    }
    List<object> objectList2 = ((PXSelectBase) pxSelectBase1).View.SelectMulti(objArray);
    crBillAddresses = ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray);
    crQuoteAddresses = ((PXSelectBase) pxSelectBase3).View.SelectMulti(objArray);
    foreach (PXResult<CRQuote, CRShippingAddress> pxResult in objectList2)
    {
      CRShippingAddress address = PXResult<CRQuote, CRShippingAddress>.op_Implicit(pxResult);
      CRQuote document = PXResult<CRQuote, CRShippingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRShippingAddress>(address, (IBqlTable) document, document.QuoteNbr, "Sales Quotes", new PMQuoteStatusAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<CRQuote, CRBillingAddress> pxResult in crBillAddresses)
    {
      CRBillingAddress address = PXResult<CRQuote, CRBillingAddress>.op_Implicit(pxResult);
      CRQuote document = PXResult<CRQuote, CRBillingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRBillingAddress>(address, (IBqlTable) document, document.QuoteNbr, "Sales Quotes", new PMQuoteStatusAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<CRQuote, CRAddress> pxResult in crQuoteAddresses)
    {
      CRAddress address = PXResult<CRQuote, CRAddress>.op_Implicit(pxResult);
      CRQuote document = PXResult<CRQuote, CRAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRAddress>(address, (IBqlTable) document, document.QuoteNbr, "Sales Quotes", new PMQuoteStatusAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }

  protected virtual IEnumerable AddCROpportunityAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateCRDocumentAddressProcess documentAddressProcess = this;
    List<object> objectList1 = new List<object>();
    List<object> crShipAddresses = new List<object>();
    List<object> crBillAddresses = new List<object>();
    object[] objArray1 = (object[]) new string[2]
    {
      "N",
      "N"
    };
    object[] objArray2 = new object[1]{ (object) objArray1 };
    PXSelectBase<CROpportunity> pxSelectBase1 = (PXSelectBase<CROpportunity>) new PXSelectJoin<CROpportunity, InnerJoin<CRAddress, On<CROpportunity.opportunityAddressID, Equal<CRAddress.addressID>>>, Where<CRAddress.isValidated, Equal<False>, And<CROpportunity.status, In<Required<CROpportunity.status>>, And<Where<CRAddress.isDefaultAddress, Equal<False>, Or<CROpportunity.bAccountID, IsNull, And<CROpportunity.contactID, IsNull>>>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<CROpportunity> pxSelectBase2 = (PXSelectBase<CROpportunity>) new PXSelectJoin<CROpportunity, InnerJoin<CRShippingAddress, On<CROpportunity.shipAddressID, Equal<CRShippingAddress.addressID>>>, Where<CRShippingAddress.isValidated, Equal<False>, And<CRShippingAddress.isDefaultAddress, Equal<False>, And<CROpportunity.status, In<Required<CROpportunity.status>>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<CROpportunity> pxSelectBase3 = (PXSelectBase<CROpportunity>) new PXSelectJoin<CROpportunity, InnerJoin<CRBillingAddress, On<CROpportunity.billAddressID, Equal<CRBillingAddress.addressID>>>, Where<CRBillingAddress.isValidated, Equal<False>, And<CRBillingAddress.isDefaultAddress, Equal<False>, And<CROpportunity.status, In<Required<CROpportunity.status>>>>>>((PXGraph) documentAddressProcess);
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray2 = new object[2]
      {
        (object) objArray1,
        (object) filter.Country
      };
      pxSelectBase1.WhereAnd<Where<CRAddress.countryID, Equal<Required<CRAddress.countryID>>>>();
      pxSelectBase2.WhereAnd<Where<CRShippingAddress.countryID, Equal<Required<CRShippingAddress.countryID>>>>();
      pxSelectBase3.WhereAnd<Where<CRBillingAddress.countryID, Equal<Required<CRBillingAddress.countryID>>>>();
    }
    List<object> objectList2 = ((PXSelectBase) pxSelectBase1).View.SelectMulti(objArray2);
    crShipAddresses = ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray2);
    crBillAddresses = ((PXSelectBase) pxSelectBase3).View.SelectMulti(objArray2);
    foreach (PXResult<CROpportunity, CRAddress> pxResult in objectList2)
    {
      CRAddress address = PXResult<CROpportunity, CRAddress>.op_Implicit(pxResult);
      CROpportunity document = PXResult<CROpportunity, CRAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRAddress>(address, (IBqlTable) document, document.OpportunityID, "Opportunities", new OpportunityStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<CROpportunity, CRShippingAddress> pxResult in crShipAddresses)
    {
      CRShippingAddress address = PXResult<CROpportunity, CRShippingAddress>.op_Implicit(pxResult);
      CROpportunity document = PXResult<CROpportunity, CRShippingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRShippingAddress>(address, (IBqlTable) document, document.OpportunityID, "Opportunities", new OpportunityStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<CROpportunity, CRBillingAddress> pxResult in crBillAddresses)
    {
      CRBillingAddress address = PXResult<CROpportunity, CRBillingAddress>.op_Implicit(pxResult);
      CROpportunity document = PXResult<CROpportunity, CRBillingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRBillingAddress>(address, (IBqlTable) document, document.OpportunityID, "Opportunities", new OpportunityStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }

  protected virtual IEnumerable AddPMQuoteAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateCRDocumentAddressProcess documentAddressProcess = this;
    List<object> objectList1 = new List<object>();
    List<object> pmProjectQuoteShipAddresses = new List<object>();
    object[] objArray = new object[0];
    PXSelectBase<PMQuote> pxSelectBase1 = (PXSelectBase<PMQuote>) new PXSelectJoin<PMQuote, InnerJoin<CRAddress, On<PMQuote.opportunityAddressID, Equal<CRAddress.addressID>>>, Where<CRAddress.isValidated, Equal<False>, And<PMQuote.quoteType, Equal<CRQuoteTypeAttribute.project>, And<Where<CRAddress.isDefaultAddress, Equal<False>, Or<PMQuote.bAccountID, IsNull, And<PMQuote.contactID, IsNull>>>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<PMQuote> pxSelectBase2 = (PXSelectBase<PMQuote>) new PXSelectJoin<PMQuote, InnerJoin<CRShippingAddress, On<PMQuote.shipAddressID, Equal<CRShippingAddress.addressID>>>, Where<CRShippingAddress.isDefaultAddress, Equal<False>, And<CRShippingAddress.isValidated, Equal<False>, And<PMQuote.quoteType, Equal<CRQuoteTypeAttribute.project>>>>>((PXGraph) documentAddressProcess);
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray = new object[1]{ (object) filter.Country };
      pxSelectBase1.WhereAnd<Where<CRAddress.countryID, Equal<Required<CRAddress.countryID>>>>();
      pxSelectBase2.WhereAnd<Where<CRShippingAddress.countryID, Equal<Required<CRShippingAddress.countryID>>>>();
    }
    List<object> objectList2 = ((PXSelectBase) pxSelectBase1).View.SelectMulti(objArray);
    pmProjectQuoteShipAddresses = ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray);
    foreach (PXResult<PMQuote, CRAddress> pxResult in objectList2)
    {
      CRAddress address = PXResult<PMQuote, CRAddress>.op_Implicit(pxResult);
      PMQuote document = PXResult<PMQuote, CRAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRAddress>(address, (IBqlTable) document, document.QuoteNbr, "Project Quotes", new PMQuoteStatusAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<PMQuote, CRShippingAddress> pxResult in pmProjectQuoteShipAddresses)
    {
      CRShippingAddress address = PXResult<PMQuote, CRShippingAddress>.op_Implicit(pxResult);
      PMQuote document = PXResult<PMQuote, CRShippingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRShippingAddress>(address, (IBqlTable) document, document.QuoteNbr, "Project Quotes", new PMQuoteStatusAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }
}
