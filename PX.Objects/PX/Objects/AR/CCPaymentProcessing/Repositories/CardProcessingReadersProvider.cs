// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.CardProcessingReadersProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public class CardProcessingReadersProvider : ICardProcessingReadersProvider
{
  private CCProcessingContext _context;
  private String2DateConverterFunc _string2DateConverter;

  public CardProcessingReadersProvider(CCProcessingContext context)
  {
    this._context = context ?? throw new ArgumentNullException(nameof (context));
    this._string2DateConverter = this._context.expirationDateConverter;
  }

  public ICreditCardDataReader GetCardDataReader()
  {
    return (ICreditCardDataReader) new CreditCardDataReader(this._context.callerGraph, this._context.aPMInstanceID);
  }

  public IEnumerable<ICreditCardDataReader> GetCustomerCardsDataReaders()
  {
    return new CustomerCardsDataReader(this._context.callerGraph, this._context.aCustomerID, this._context.processingCenter.ProcessingCenterID, (Func<PXGraph, int?, ICreditCardDataReader>) ((graph, insID) => (ICreditCardDataReader) new CreditCardDataReader(graph, insID))).GetCardReaders();
  }

  public ICustomerDataReader GetCustomerDataReader()
  {
    return (ICustomerDataReader) new CustomerDataReader(this._context);
  }

  public IDocDetailsDataReader GetDocDetailsDataReader()
  {
    return (IDocDetailsDataReader) new DocDetailsDataReader(this._context.callerGraph, this._context.aDocType, this._context.aRefNbr);
  }

  public IProcessingCenterSettingsStorage GetProcessingCenterSettingsStorage()
  {
    return (IProcessingCenterSettingsStorage) new ProcessingCenterSettingsStorage(this._context.callerGraph, this._context.processingCenter.ProcessingCenterID);
  }

  public String2DateConverterFunc GetExpirationDateConverter() => this._string2DateConverter;

  public IPaymentMethodDataReader GetPaymentMethodDataReader()
  {
    return (IPaymentMethodDataReader) new PaymentMethodDataReader(this._context.callerGraph, this._context.PaymentMethodID);
  }
}
