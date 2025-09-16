// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.V2ProcessingInputGenerator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public class V2ProcessingInputGenerator
{
  private ICardProcessingReadersProvider _provider;

  public bool FillCardData { get; set; } = true;

  public bool FillCustomerData { get; set; } = true;

  public bool FillAdressData { get; set; }

  public V2ProcessingInputGenerator(ICardProcessingReadersProvider provider)
  {
    this._provider = provider;
  }

  public ProcessingInput GetProcessingInput(CCTranType tranType, ICCPayment pDoc)
  {
    return V2ProcessingInputGenerator.Convert(this.GetPaymentFormPrepareOptions(tranType, pDoc));
  }

  public PaymentFormPrepareOptions GetPaymentFormPrepareOptions(
    CCTranType tranType,
    ICCPayment pDoc)
  {
    if (pDoc == null)
      throw new ArgumentNullException(nameof (pDoc));
    PaymentFormPrepareOptions formPrepareOptions = new PaymentFormPrepareOptions();
    ((ProcessingInput) formPrepareOptions).TranType = tranType;
    ((ProcessingInput) formPrepareOptions).Amount = pDoc.CuryDocBal.Value;
    ((ProcessingInput) formPrepareOptions).CuryID = pDoc.CuryID;
    ((ProcessingInput) formPrepareOptions).OrigTranID = tranType == 3 ? (string) null : pDoc.OrigRefNbr;
    ((ProcessingInput) formPrepareOptions).AuthCode = tranType == 3 ? pDoc.OrigRefNbr : (string) null;
    PaymentFormPrepareOptions processingInput = formPrepareOptions;
    if (this.FillCardData)
    {
      ((ProcessingInput) processingInput).CardData = V2ProcessingInputGenerator.GetCardData(this._provider.GetCardDataReader());
      ((ProcessingInput) processingInput).CardData.AddressData = V2ProcessingInputGenerator.GetAddressData(this._provider.GetCustomerDataReader());
    }
    if (this.FillAdressData && ((ProcessingInput) processingInput).CardData == null)
    {
      ((ProcessingInput) processingInput).CardData = new CreditCardData();
      ((ProcessingInput) processingInput).CardData.AddressData = V2ProcessingInputGenerator.GetAddressData(this._provider.GetCustomerDataReader());
    }
    if (this.FillCustomerData)
      ((ProcessingInput) processingInput).CustomerData = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader());
    ((ProcessingInput) processingInput).DocumentData = new DocumentData();
    ((ProcessingInput) processingInput).DocumentData.DocType = pDoc.DocType;
    ((ProcessingInput) processingInput).DocumentData.DocRefNbr = pDoc.RefNbr;
    this.FillDocumentData(processingInput);
    return processingInput;
  }

  public ProcessingInput GetProcessingInput(CCTranType tranType, TranProcessingInput inputData)
  {
    return V2ProcessingInputGenerator.Convert(this.GetPaymentFormPrepareOptions(tranType, inputData));
  }

  private PaymentFormPrepareOptions GetPaymentFormPrepareOptions(
    CCTranType commonTranType,
    TranProcessingInput inputData)
  {
    if (inputData == null)
      throw new ArgumentNullException(nameof (inputData));
    CCTranType v2 = V2Converter.ConvertTranTypeToV2(commonTranType);
    PaymentFormPrepareOptions formPrepareOptions = new PaymentFormPrepareOptions();
    ((ProcessingInput) formPrepareOptions).TranType = v2;
    ((ProcessingInput) formPrepareOptions).Amount = inputData.Amount;
    ((ProcessingInput) formPrepareOptions).CuryID = inputData.CuryID;
    ((ProcessingInput) formPrepareOptions).OrigTranID = commonTranType == CCTranType.CaptureOnly ? (string) null : inputData.OrigRefNbr;
    ((ProcessingInput) formPrepareOptions).AuthCode = commonTranType == CCTranType.CaptureOnly ? inputData.OrigRefNbr : (string) null;
    ((ProcessingInput) formPrepareOptions).MeansOfPayment = inputData.MeansOfPayment == "EFT" ? (MeansOfPayment) 1 : (MeansOfPayment) 0;
    ((ProcessingInput) formPrepareOptions).SubtotalAmount = inputData.SubtotalAmount;
    ((ProcessingInput) formPrepareOptions).Tax = inputData.Tax;
    ((ProcessingInput) formPrepareOptions).TerminalData = new POSTerminalData()
    {
      TerminalID = inputData.POSTerminalID
    };
    PaymentFormPrepareOptions processingInput = formPrepareOptions;
    if (this.FillCardData)
    {
      ((ProcessingInput) processingInput).CardData = V2ProcessingInputGenerator.GetCardData(this._provider.GetCardDataReader());
      ((ProcessingInput) processingInput).CardData.AddressData = V2ProcessingInputGenerator.GetAddressData(this._provider.GetCustomerDataReader());
    }
    if (this.FillCustomerData)
      ((ProcessingInput) processingInput).CustomerData = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader());
    ((ProcessingInput) processingInput).DocumentData = new DocumentData();
    ((ProcessingInput) processingInput).DocumentData.DocType = inputData.DocType;
    ((ProcessingInput) processingInput).DocumentData.DocRefNbr = inputData.DocRefNbr;
    this.FillDocumentData(processingInput);
    ((ProcessingInput) processingInput).TranUID = inputData.TranUID;
    return processingInput;
  }

  private static ProcessingInput Convert(PaymentFormPrepareOptions options)
  {
    return new ProcessingInput()
    {
      TranType = ((ProcessingInput) options).TranType,
      Amount = ((ProcessingInput) options).Amount,
      CuryID = ((ProcessingInput) options).CuryID,
      OrigTranID = ((ProcessingInput) options).OrigTranID,
      AuthCode = ((ProcessingInput) options).AuthCode,
      DocumentData = ((ProcessingInput) options).DocumentData,
      CustomerData = ((ProcessingInput) options).CustomerData,
      CardData = ((ProcessingInput) options).CardData,
      TranUID = ((ProcessingInput) options).TranUID,
      SaveCard = ((ProcessingInput) options).SaveCard,
      MeansOfPayment = ((ProcessingInput) options).MeansOfPayment,
      SubtotalAmount = ((ProcessingInput) options).SubtotalAmount,
      Tax = ((ProcessingInput) options).Tax,
      TerminalData = ((ProcessingInput) options).TerminalData
    };
  }

  public static CreditCardData GetCardData(
    ICreditCardDataReader cardReader,
    String2DateConverterFunc expirationDateConverter = null)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    cardReader.ReadData(dictionary);
    CreditCardData cardData = new CreditCardData();
    string s;
    if (dictionary.TryGetValue(cardReader.Key_PMCCProcessingID, out s))
      cardData.PaymentProfileID = s;
    if (dictionary.TryGetValue(cardReader.Key_CardNumber, out s))
      cardData.CardNumber = s;
    if (dictionary.TryGetValue(cardReader.Key_CardCVV, out s))
      cardData.CVV = s;
    if (expirationDateConverter != null)
    {
      if (dictionary.TryGetValue(cardReader.Key_CardExpiryDate, out s))
        cardData.CardExpirationDate = expirationDateConverter(s);
    }
    else
      cardData.CardExpirationDate = new DateTime?();
    return cardData;
  }

  public static CustomerData GetCustomerData(ICustomerDataReader customerReader)
  {
    CustomerData customerData = new CustomerData();
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    customerReader.ReadData(dictionary);
    string str;
    if (dictionary.TryGetValue(customerReader.Key_Customer_CCProcessingID, out str))
      customerData.CustomerProfileID = str;
    if (dictionary.TryGetValue(customerReader.Key_CustomerCD, out str))
      customerData.CustomerCD = str;
    if (dictionary.TryGetValue(customerReader.Key_CustomerName, out str))
      customerData.CustomerName = str;
    if (dictionary.TryGetValue(customerReader.Key_BillContact_Email, out str))
      customerData.Email = str;
    return customerData;
  }

  public static AddressData GetAddressData(ICustomerDataReader customerReader)
  {
    AddressData addressData = new AddressData();
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    customerReader.ReadData(dictionary);
    string str;
    if (dictionary.TryGetValue(customerReader.Key_Customer_FirstName, out str))
      addressData.FirstName = str;
    if (dictionary.TryGetValue(customerReader.Key_Customer_LastName, out str))
      addressData.LastName = str;
    if (dictionary.TryGetValue(customerReader.Key_BillAddr_Address, out str))
      addressData.Address = str;
    if (dictionary.TryGetValue(customerReader.Key_BillAddr_AddressLine1, out str))
      addressData.AddressLine1 = str;
    if (dictionary.TryGetValue(customerReader.Key_BillAddr_AddressLine2, out str))
      addressData.AddressLine2 = str;
    if (dictionary.TryGetValue(customerReader.Key_BillAddr_AddressLine3, out str))
      addressData.AddressLine3 = str;
    if (dictionary.TryGetValue(customerReader.Key_BillAddr_City, out str))
      addressData.City = str;
    if (dictionary.TryGetValue(customerReader.Key_BillAddr_Country, out str))
      addressData.Country = str;
    if (dictionary.TryGetValue(customerReader.Key_BillAddr_PostalCode, out str))
      addressData.PostalCode = str;
    if (dictionary.TryGetValue(customerReader.Key_BillAddr_State, out str))
      addressData.State = str;
    if (dictionary.TryGetValue(customerReader.Key_BillContact_Email, out str))
      addressData.Email = str;
    if (dictionary.TryGetValue(customerReader.Key_BillContact_Fax, out str))
      addressData.Fax = str;
    if (dictionary.TryGetValue(customerReader.Key_BillContact_Phone, out str))
      addressData.Phone = str;
    return addressData;
  }

  private void FillDocumentData(PaymentFormPrepareOptions processingInput)
  {
    ((ProcessingInput) processingInput).DocumentData.DocumentDetails = new List<DocumentDetailData>();
    IDocDetailsDataReader detailsDataReader = this._provider.GetDocDetailsDataReader();
    List<DocDetailInfo> docDetailInfoList1 = new List<DocDetailInfo>();
    List<DocDetailInfo> docDetailInfoList2 = docDetailInfoList1;
    detailsDataReader.ReadData(docDetailInfoList2);
    foreach (DocDetailInfo docDetail in docDetailInfoList1)
    {
      DocumentDetailData v2 = V2ProcessingInputGenerator.ToV2(docDetail);
      ((ProcessingInput) processingInput).DocumentData.DocumentDetails.Add(v2);
    }
  }

  public static DocumentDetailData ToV2(DocDetailInfo docDetail)
  {
    return new DocumentDetailData()
    {
      ItemID = docDetail.ItemID,
      ItemDescription = docDetail.ItemDescription,
      ItemName = docDetail.ItemName,
      Price = docDetail.Price,
      Quantity = docDetail.Quantity,
      IsTaxable = docDetail.IsTaxable
    };
  }
}
