// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.ValidatePODocumentAddressProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class ValidatePODocumentAddressProcess : 
  ValidateDocumentAddressGraph<ValidatePODocumentAddressProcess>
{
  [PXMergeAttributes]
  [DocumentTypeField.List(DocumentTypeField.SetOfValues.PORelatedDocumentTypes)]
  public virtual void _(
    PX.Data.Events.CacheAttached<ValidateDocumentAddressFilter.documentType> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<POShipAddress.bAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<PORemitAddress.bAccountID> e)
  {
  }

  protected override IEnumerable GetAddressRecords(ValidateDocumentAddressFilter filter)
  {
    IEnumerable addressRecords = (IEnumerable) new List<UnvalidatedAddress>();
    if (filter.DocumentType.Trim().Equals("PO"))
      addressRecords = this.AddPurchaseOrderAddresses(filter);
    return addressRecords;
  }

  protected virtual IEnumerable AddPurchaseOrderAddresses(ValidateDocumentAddressFilter filter)
  {
    ValidatePODocumentAddressProcess documentAddressProcess = this;
    List<object> objectList = new List<object>();
    List<object> poShipAddresses = new List<object>();
    object[] objArray1 = (object[]) new string[9]
    {
      "H",
      "D",
      "E",
      "B",
      "V",
      "N",
      "M",
      "L",
      "C"
    };
    object[] objArray2 = (object[]) new string[9]
    {
      "H",
      "D",
      "E",
      "B",
      "V",
      "A",
      "N",
      "L",
      "C"
    };
    object[] objArray3 = (object[]) new string[9]
    {
      "H",
      "D",
      "E",
      "B",
      "V",
      "N",
      "M",
      "L",
      "C"
    };
    object[] objArray4 = (object[]) new string[5]
    {
      "H",
      "B",
      "V",
      "N",
      "L"
    };
    object[] objArray5 = (object[]) new string[6]
    {
      "H",
      "B",
      "V",
      "M",
      "L",
      "C"
    };
    Dictionary<string, object[]> dictionary = new Dictionary<string, object[]>();
    dictionary.Add("RO", objArray1);
    dictionary.Add("DP", objArray2);
    dictionary.Add("PD", objArray3);
    dictionary.Add("SB", objArray4);
    dictionary.Add("BL", objArray5);
    PXSelectBase<POOrder> pxSelectBase1 = (PXSelectBase<POOrder>) new PXSelectJoin<POOrder, InnerJoin<PORemitAddress, On<POOrder.remitAddressID, Equal<PORemitAddress.addressID>>>, Where<PORemitAddress.isDefaultAddress, Equal<False>, And<PORemitAddress.isValidated, Equal<False>, And<POOrder.cancelled, Equal<False>, And<POOrder.orderType, Equal<Required<POOrder.orderType>>, And<POOrder.status, In<Required<POOrder.status>>>>>>>>((PXGraph) documentAddressProcess);
    PXSelectBase<POOrder> pxSelectBase2 = (PXSelectBase<POOrder>) new PXSelectJoin<POOrder, InnerJoin<POShipAddress, On<POOrder.shipAddressID, Equal<POShipAddress.addressID>>>, Where<POShipAddress.isDefaultAddress, Equal<False>, And<POShipAddress.isValidated, Equal<False>, And<POOrder.cancelled, Equal<False>, And<POOrder.orderType, Equal<Required<POOrder.orderType>>, And<POOrder.status, In<Required<POOrder.status>>>>>>>>((PXGraph) documentAddressProcess);
    if (!string.IsNullOrEmpty(filter?.Country))
    {
      pxSelectBase1.WhereAnd<Where<PORemitAddress.countryID, Equal<Required<PORemitAddress.countryID>>>>();
      pxSelectBase2.WhereAnd<Where<POShipAddress.countryID, Equal<Required<POShipAddress.countryID>>>>();
    }
    foreach (KeyValuePair<string, object[]> keyValuePair in dictionary)
    {
      object[] objArray6;
      if (!string.IsNullOrEmpty(filter?.Country))
        objArray6 = new object[3]
        {
          (object) keyValuePair.Key,
          (object) keyValuePair.Value,
          (object) filter.Country
        };
      else
        objArray6 = new object[2]
        {
          (object) keyValuePair.Key,
          (object) keyValuePair.Value
        };
      object[] objArray7 = objArray6;
      objectList.AddRange((IEnumerable<object>) ((PXSelectBase) pxSelectBase1).View.SelectMulti(objArray7));
      poShipAddresses.AddRange((IEnumerable<object>) ((PXSelectBase) pxSelectBase2).View.SelectMulti(objArray7));
    }
    foreach (PXResult<POOrder, PORemitAddress> pxResult in objectList)
    {
      PORemitAddress address = PXResult<POOrder, PORemitAddress>.op_Implicit(pxResult);
      POOrder document = PXResult<POOrder, PORemitAddress>.op_Implicit(pxResult);
      string str = new POOrderType.ListAttribute().ValueLabelDic[document.OrderType];
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<PORemitAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.OrderNbr : $"{str}, {document.OrderNbr}", "Purchase Orders", new POOrderStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
    foreach (PXResult<POOrder, POShipAddress> pxResult in poShipAddresses)
    {
      POShipAddress address = PXResult<POOrder, POShipAddress>.op_Implicit(pxResult);
      POOrder document = PXResult<POOrder, POShipAddress>.op_Implicit(pxResult);
      string str = new POOrderType.ListAttribute().ValueLabelDic[document.OrderType];
      UnvalidatedAddress unvalidatedAddress = documentAddressProcess.ConvertToUnvalidatedAddress<POShipAddress>(address, (IBqlTable) document, string.IsNullOrEmpty(str) ? document.OrderNbr : $"{str}, {document.OrderNbr}", "Purchase Orders", new POOrderStatus.ListAttribute().ValueLabelDic[document.Status]);
      yield return (object) ((PXSelectBase<UnvalidatedAddress>) documentAddressProcess.DocumentAddresses).Insert(unvalidatedAddress);
    }
  }
}
