// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.CreditCardDataReader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.Data;
using PX.Objects.CA;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public class CreditCardDataReader : ICreditCardDataReader
{
  private PXGraph _graph;
  private int? _pminstanceID;

  public CreditCardDataReader(PXGraph graph, int? pminstanceID)
  {
    this._graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
    this._pminstanceID = pminstanceID;
  }

  void ICreditCardDataReader.ReadData(Dictionary<string, string> aData)
  {
    PXResultset<CustomerPaymentMethodDetail> pxResultset = PXSelectBase<CustomerPaymentMethodDetail, PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>>>>, Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<CustomerPaymentMethodDetail.pMInstanceID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) this._pminstanceID
    });
    if (pxResultset == null)
      return;
    foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> pxResult in pxResultset)
    {
      PaymentMethodDetail paymentMethodDetail1 = PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail>.op_Implicit(pxResult);
      CustomerPaymentMethodDetail paymentMethodDetail2 = PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail>.op_Implicit(pxResult);
      bool? nullable = paymentMethodDetail1.IsCCProcessingID;
      if (nullable.GetValueOrDefault())
        aData["CCPID"] = paymentMethodDetail2.Value;
      nullable = paymentMethodDetail1.IsIdentifier;
      if (nullable.GetValueOrDefault())
        aData["CCDNUM"] = paymentMethodDetail2.Value;
      nullable = paymentMethodDetail1.IsExpirationDate;
      if (nullable.GetValueOrDefault())
        aData["EXPDATE"] = paymentMethodDetail2.Value;
      nullable = paymentMethodDetail1.IsCVV;
      if (nullable.GetValueOrDefault())
        aData["CVV"] = paymentMethodDetail2.Value;
      nullable = paymentMethodDetail1.IsOwnerName;
      if (nullable.GetValueOrDefault())
        aData["NAMEONCC"] = paymentMethodDetail2.Value;
    }
  }

  string ICreditCardDataReader.Key_CardNumber => "CCDNUM";

  string ICreditCardDataReader.Key_CardExpiryDate => "EXPDATE";

  string ICreditCardDataReader.Key_CardCVV => "CVV";

  string ICreditCardDataReader.Key_PMCCProcessingID => "CCPID";
}
