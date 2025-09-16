// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ValidatePMDocumentAddressProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CT;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ValidatePMDocumentAddressProcess : 
  ValidateDocumentAddressGraph<ValidatePMDocumentAddressProcess>
{
  [PXMergeAttributes]
  [DocumentTypeField.List(DocumentTypeField.SetOfValues.PMRelatedDocumentTypes)]
  public virtual void _(
    PX.Data.Events.CacheAttached<ValidateDocumentAddressFilter.documentType> e)
  {
  }

  protected override IEnumerable GetAddressRecords(ValidateDocumentAddressFilter filter)
  {
    IEnumerable addressRecords = (IEnumerable) new List<UnvalidatedAddress>();
    switch (filter.DocumentType.Trim())
    {
      case "PR":
        addressRecords = this.AddPMProjectAddresses(filter);
        break;
      case "PQ":
        addressRecords = this.AddPMQuoteAddresses(filter);
        break;
      case "PFI":
        addressRecords = this.AddProFormaInvoiceAddresses(filter);
        break;
    }
    return addressRecords;
  }

  protected virtual IEnumerable AddPMProjectAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidatePMDocumentAddressProcess graph = this;
    List<object> objectList1 = new List<object>();
    List<object> projectAddresses = new List<object>();
    object[] objArray1 = (object[]) new string[2]
    {
      "D",
      "A"
    };
    object[] objArray2 = new object[1]{ (object) objArray1 };
    PXSelectBase<PMProject> pxSelectBase = (PXSelectBase<PMProject>) new PXSelectJoin<PMProject, InnerJoin<PMAddress, On<PMProject.billAddressID, Equal<PMAddress.addressID>>>, Where<PMAddress.isDefaultBillAddress, Equal<False>, And<PMAddress.isValidated, Equal<False>, And<PMProject.status, In<Required<PMProject.status>>, And<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>>>>>((PXGraph) graph);
    FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMSiteAddress>.On<BqlOperand<PMProject.siteAddressID, IBqlInt>.IsEqual<PMSiteAddress.addressID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMSiteAddress.isValidated, Equal<False>>>>, And<BqlOperand<PMSiteAddress.isDefaultBillAddress, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PMProject.status, IBqlString>.IsIn<P.AsString>>>, And<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>>>.And<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<False>>>, PMProject>.View view = new FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMSiteAddress>.On<BqlOperand<PMProject.siteAddressID, IBqlInt>.IsEqual<PMSiteAddress.addressID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMSiteAddress.isValidated, Equal<False>>>>, And<BqlOperand<PMSiteAddress.isDefaultBillAddress, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PMProject.status, IBqlString>.IsIn<P.AsString>>>, And<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>>>.And<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<False>>>, PMProject>.View((PXGraph) graph);
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray2 = new object[2]
      {
        (object) objArray1,
        (object) filter.Country
      };
      pxSelectBase.WhereAnd<Where<PMAddress.countryID, Equal<Required<PMAddress.countryID>>>>();
      ((PXSelectBase<PMProject>) view).WhereAnd<Where<PMSiteAddress.countryID, Equal<Required<PMSiteAddress.countryID>>>>();
    }
    List<object> objectList2 = ((PXSelectBase) pxSelectBase).View.SelectMulti(objArray2);
    projectAddresses = ((PXSelectBase) view).View.SelectMulti(objArray2);
    foreach (PXResult<PMProject, PMAddress> pxResult in objectList2)
    {
      PMAddress address = PXResult<PMProject, PMAddress>.op_Implicit(pxResult);
      PMProject document = PXResult<PMProject, PMAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = graph.ConvertToUnvalidatedAddress<PMAddress>(address, (IBqlTable) document, document.ContractCD, "Projects", new ProjectStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) graph.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<PMProject, PMSiteAddress> pxResult in projectAddresses)
    {
      PMSiteAddress address = PXResult<PMProject, PMSiteAddress>.op_Implicit(pxResult);
      PMProject document = PXResult<PMProject, PMSiteAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = graph.ConvertToUnvalidatedAddress<PMSiteAddress>(address, (IBqlTable) document, document.ContractCD, "Projects", new ProjectStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) graph.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }

  protected virtual IEnumerable AddPMQuoteAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidatePMDocumentAddressProcess documentAddressProcess = this;
    List<object> objectList1 = new List<object>();
    List<object> shipAddresses = new List<object>();
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
    shipAddresses = ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray);
    foreach (PXResult<PMQuote, CRAddress> pxResult in objectList2)
    {
      CRAddress address = PXResult<PMQuote, CRAddress>.op_Implicit(pxResult);
      PMQuote document = PXResult<PMQuote, CRAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRAddress>(address, (IBqlTable) document, document.QuoteNbr, "Project Quotes", new PMQuoteStatusAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<PMQuote, CRShippingAddress> pxResult in shipAddresses)
    {
      CRShippingAddress address = PXResult<PMQuote, CRShippingAddress>.op_Implicit(pxResult);
      PMQuote document = PXResult<PMQuote, CRShippingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<CRShippingAddress>(address, (IBqlTable) document, document.QuoteNbr, "Project Quotes", new PMQuoteStatusAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }

  protected virtual IEnumerable AddProFormaInvoiceAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidatePMDocumentAddressProcess graph = this;
    object[] objArray1 = (object[]) new string[2]
    {
      "H",
      "O"
    };
    FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAddress>.On<BqlOperand<PMProforma.billAddressID, IBqlInt>.IsEqual<PMAddress.addressID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAddress.isValidated, Equal<False>>>>, And<BqlOperand<PMAddress.isDefaultBillAddress, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PMProforma.status, IBqlString>.IsIn<P.AsString>>>, PMProforma>.View view1 = new FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAddress>.On<BqlOperand<PMProforma.billAddressID, IBqlInt>.IsEqual<PMAddress.addressID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAddress.isValidated, Equal<False>>>>, And<BqlOperand<PMAddress.isDefaultBillAddress, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PMProforma.status, IBqlString>.IsIn<P.AsString>>>, PMProforma>.View((PXGraph) graph);
    FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMShippingAddress>.On<BqlOperand<PMProforma.shipAddressID, IBqlInt>.IsEqual<PMShippingAddress.addressID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMShippingAddress.isValidated, Equal<False>>>>, And<BqlOperand<PMShippingAddress.isDefaultBillAddress, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PMProforma.status, IBqlString>.IsIn<P.AsString>>>, PMProforma>.View view2 = new FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMShippingAddress>.On<BqlOperand<PMProforma.shipAddressID, IBqlInt>.IsEqual<PMShippingAddress.addressID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMShippingAddress.isValidated, Equal<False>>>>, And<BqlOperand<PMShippingAddress.isDefaultBillAddress, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PMProforma.status, IBqlString>.IsIn<P.AsString>>>, PMProforma>.View((PXGraph) graph);
    object[] objArray2 = new object[1]{ (object) objArray1 };
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray2 = new object[2]
      {
        (object) objArray1,
        (object) filter.Country
      };
      ((PXSelectBase<PMProforma>) view1).WhereAnd<Where<BqlOperand<PMAddress.countryID, IBqlString>.IsEqual<P.AsString>>>();
      ((PXSelectBase<PMProforma>) view2).WhereAnd<Where<BqlOperand<PMShippingAddress.countryID, IBqlString>.IsEqual<P.AsString>>>();
    }
    List<object> objectList = ((PXSelectBase) view1).View.SelectMulti(objArray2);
    List<object> shippAddresses = ((PXSelectBase) view2).View.SelectMulti(objArray2);
    foreach (PXResult<PMProforma, PMAddress> pxResult in objectList)
    {
      PMAddress address = PXResult<PMProforma, PMAddress>.op_Implicit(pxResult);
      PMProforma document = PXResult<PMProforma, PMAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = graph.ConvertToUnvalidatedAddress<PMAddress>(address, (IBqlTable) document, document.RefNbr, "Pro Forma Invoices", new ProformaStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) graph.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<PMProforma, PMShippingAddress> pxResult in shippAddresses)
    {
      PMShippingAddress address = PXResult<PMProforma, PMShippingAddress>.op_Implicit(pxResult);
      PMProforma document = PXResult<PMProforma, PMShippingAddress>.op_Implicit(pxResult);
      UnvalidatedAddress unvalidatedAddress = graph.ConvertToUnvalidatedAddress<PMShippingAddress>(address, (IBqlTable) document, document.RefNbr, "Pro Forma Invoices", new ProformaStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) graph.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }
}
