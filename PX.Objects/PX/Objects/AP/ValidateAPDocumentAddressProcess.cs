// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.ValidateAPDocumentAddressProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class ValidateAPDocumentAddressProcess : 
  ValidateDocumentAddressGraph<ValidateAPDocumentAddressProcess>
{
  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<APAddress.vendorID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [DocumentTypeField.List(DocumentTypeField.SetOfValues.APRelatedDocumentTypes)]
  public virtual void _(
    PX.Data.Events.CacheAttached<ValidateDocumentAddressFilter.documentType> e)
  {
  }

  protected override IEnumerable GetAddressRecords(ValidateDocumentAddressFilter filter)
  {
    IEnumerable addressRecords = (IEnumerable) new List<UnvalidatedAddress>();
    if (filter.DocumentType.Trim().Equals("CP"))
      addressRecords = this.AddAPPaymentAddresses(filter);
    return addressRecords;
  }

  protected virtual IEnumerable AddAPPaymentAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidateAPDocumentAddressProcess graph = this;
    object[] objArray1 = (object[]) new string[4]
    {
      "H",
      "G",
      "P",
      "B"
    };
    object[] objArray2 = (object[]) new string[6]
    {
      "CHK",
      "PPM",
      "REF",
      "VRF",
      "VCK",
      "ADR"
    };
    object[] objArray3 = new object[2]
    {
      (object) objArray1,
      (object) objArray2
    };
    PXSelectBase<APPayment> pxSelectBase = (PXSelectBase<APPayment>) new PXSelectJoin<APPayment, InnerJoin<APAddress, On<APPayment.remitAddressID, Equal<APAddress.addressID>>>, Where<APAddress.isDefaultAddress, Equal<False>, And<APAddress.isValidated, Equal<False>, And<APPayment.released, Equal<False>, And<APPayment.status, In<Required<APPayment.status>>, And<APPayment.docType, In<Required<APPayment.docType>>>>>>>>((PXGraph) graph);
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      objArray3 = new object[3]
      {
        (object) objArray1,
        (object) objArray2,
        (object) filter.Country
      };
      pxSelectBase.WhereAnd<Where<APAddress.countryID, Equal<Required<APAddress.countryID>>>>();
    }
    foreach (PXResult<APPayment, APAddress> pxResult in pxSelectBase.View.SelectMulti(objArray3))
    {
      APAddress address = (APAddress) pxResult;
      APPayment document = (APPayment) pxResult;
      string str = new APPaymentType.ListAttribute().ValueLabelDic[document.DocType];
      UnvalidatedAddress unvalidatedAddress = graph.ConvertToUnvalidatedAddress<APAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.RefNbr : $"{str}, {document.RefNbr}", "Checks and Payments", new APDocStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) graph.DocumentAddresses.Insert(unvalidatedAddress);
    }
  }
}
