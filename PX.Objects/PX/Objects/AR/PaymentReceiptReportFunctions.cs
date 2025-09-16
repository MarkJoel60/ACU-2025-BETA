// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PaymentReceiptReportFunctions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using System;

#nullable disable
namespace PX.Objects.AR;

public class PaymentReceiptReportFunctions
{
  public string GetPaymentType(string paymentMethodType, string docType, string tranType)
  {
    string paymentType = string.Empty;
    if (string.IsNullOrEmpty(paymentMethodType))
      return paymentType;
    ARDocType.PrintListAttribute printListAttribute = new ARDocType.PrintListAttribute();
    if (printListAttribute.ValueLabelDic.ContainsKey(docType))
      paymentType = printListAttribute.ValueLabelDic[docType];
    if (docType == "CSL" && EnumerableExtensions.IsIn<string>(paymentMethodType, "CCD", "POS", "EFT"))
      paymentType = printListAttribute.ValueLabelDic["PMT"];
    if (EnumerableExtensions.IsIn<string>(docType, "RPM", "RCS") && tranType == "CDT")
      paymentType = printListAttribute.ValueLabelDic["REF"];
    if (EnumerableExtensions.IsIn<string>(docType, "CSL", "RCS") && EnumerableExtensions.IsIn<string>(tranType, "VDG", "REJ"))
      paymentType = printListAttribute.ValueLabelDic["RPM"];
    if (paymentType != string.Empty)
      paymentType = PXMessages.LocalizeNoPrefix(paymentType);
    return paymentType;
  }

  public PaymentReceiptReportFunctions.TransactionData GetTransactionData(int? actualTranId)
  {
    PaymentReceiptReportFunctions.TransactionData transactionData = new PaymentReceiptReportFunctions.TransactionData();
    if (actualTranId.HasValue)
    {
      PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing pmtProcessing = new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing();
      PXGraph graph = pmtProcessing.Repository.Graph;
      ExternalTransaction extTran = PXResultset<ExternalTransaction>.op_Implicit(PXSelectBase<ExternalTransaction, PXSelect<ExternalTransaction, Where<ExternalTransaction.transactionID, Equal<Required<ExternalTransaction.transactionID>>>>.Config>.Select(graph, new object[1]
      {
        (object) actualTranId
      }));
      if (extTran != null)
      {
        CCProcTran successfulCcProcTran = this.GetSuccessfulCCProcTran(pmtProcessing, extTran);
        if (successfulCcProcTran == null && extTran.ParentTranID.HasValue)
        {
          extTran = PXResultset<ExternalTransaction>.op_Implicit(PXSelectBase<ExternalTransaction, PXSelect<ExternalTransaction, Where<ExternalTransaction.transactionID, Equal<Required<ExternalTransaction.transactionID>>>>.Config>.Select(graph, new object[1]
          {
            (object) extTran.ParentTranID
          }));
          if (extTran != null && extTran.ProcStatus == "VDS")
            successfulCcProcTran = this.GetSuccessfulCCProcTran(pmtProcessing, extTran);
        }
        if (successfulCcProcTran != null)
        {
          CCTranStatusCode.ListAttribute listAttribute = new CCTranStatusCode.ListAttribute();
          transactionData.CardNumber = !string.IsNullOrEmpty(extTran.CardType) ? graph.GetService<ICCDisplayMaskService>().UseDefaultMaskForCardNumber(extTran.LastDigits ?? string.Empty, CardType.GetDisplayName(extTran.CardType), new MeansOfPayment?(CardType.GetMeansOfPayment(extTran.CardType))) : string.Empty;
          transactionData.AuthNumber = extTran.AuthNumber;
          transactionData.CuryID = successfulCcProcTran.CuryID;
          transactionData.StartTime = successfulCcProcTran.StartTime;
          transactionData.TranType = successfulCcProcTran.TranType;
          transactionData.Amount = successfulCcProcTran.Amount;
          transactionData.TranStatus = PXMessages.LocalizeFormatNoPrefix("{0}", new object[1]
          {
            (object) listAttribute.ValueLabelDic[successfulCcProcTran.TranStatus]
          });
        }
      }
    }
    return transactionData;
  }

  public string GetPaymentMethodTypeString(string paymentType)
  {
    string methodTypeString = string.Empty;
    if (!string.IsNullOrEmpty(paymentType))
      methodTypeString = PXMessages.LocalizeFormatNoPrefix("{0}", new object[1]
      {
        (object) new PaymentMethodType.ListAttribute().ValueLabelDic[paymentType]
      });
    return methodTypeString;
  }

  private CCProcTran GetSuccessfulCCProcTran(
    PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing pmtProcessing,
    ExternalTransaction extTran)
  {
    CCProcTran successfulCcProcTran = (CCProcTran) null;
    try
    {
      successfulCcProcTran = pmtProcessing.GetCCProcTran(extTran);
    }
    catch
    {
    }
    return successfulCcProcTran;
  }

  public class TransactionData
  {
    public DateTime? StartTime { get; set; }

    public string AuthNumber { get; set; }

    public Decimal? Amount { get; set; }

    public string TranType { get; set; }

    public string TranStatus { get; set; }

    public string CuryID { get; set; }

    public string CardNumber { get; set; }
  }
}
