// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.CardTransactionProcessingWrapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public class CardTransactionProcessingWrapper
{
  public static ICardTransactionProcessingWrapper GetTransactionProcessingWrapper(
    object pluginObject,
    ICardProcessingReadersProvider provider)
  {
    ICardTransactionProcessingWrapper processingWrapper = CardTransactionProcessingWrapper.GetTransactionProcessingWrapper(pluginObject);
    if (!(processingWrapper is ISetCardProcessingReadersProvider processingReadersProvider))
      throw new PXException("Could not set CardProcessingReadersProvider");
    processingReadersProvider.SetProvider(provider);
    return processingWrapper;
  }

  private static ICardTransactionProcessingWrapper GetTransactionProcessingWrapper(
    object pluginObject)
  {
    return (ICardTransactionProcessingWrapper) new CardTransactionProcessingWrapper.V2CardTransactionProcessor((!CCProcessingHelper.IsV1ProcessingInterface(pluginObject.GetType()) ? CCProcessingHelper.IsV2ProcessingInterface(pluginObject) : throw new PXException("The operation cannot be completed. The {0} processing center uses an unsupported plug-in.")) ?? throw new PXException("The type of the card processing plugin is unknown: {0}. Check the card processing settings.", new object[1]
    {
      (object) pluginObject.GetType().Name
    }));
  }

  protected class V2CardTransactionProcessor : 
    V2ProcessorBase,
    ICardTransactionProcessingWrapper,
    ISetCardProcessingReadersProvider
  {
    private readonly ICCProcessingPlugin _plugin;

    public V2CardTransactionProcessor(ICCProcessingPlugin v2Plugin) => this._plugin = v2Plugin;

    public TranProcessingResult DoTransaction(CCTranType aTranType, TranProcessingInput inputData)
    {
      ProcessingResult processingResult1 = this.GetProcessor<ICCTransactionProcessor>(CCProcessingFeature.Base).DoTransaction(new V2ProcessingInputGenerator(this._provider).GetProcessingInput(aTranType, inputData));
      TranProcessingResult processingResult2 = V2Converter.ConvertTranProcessingResult(processingResult1);
      ICCTranStatusGetter processor = this._plugin.CreateProcessor<ICCTranStatusGetter>((IEnumerable<SettingsValue>) null);
      if (processor != null)
      {
        CCTranStatus tranStatus = processor.GetTranStatus(processingResult1);
        processingResult2.TranStatus = V2Converter.ConvertTranStatus(tranStatus);
      }
      return processingResult2;
    }

    public TransactionData GetTransaction(string transactionId)
    {
      ICCTransactionGetter processor = this.GetProcessor<ICCTransactionGetter>(CCProcessingFeature.TransactionGetter);
      return V2PluginErrorHandler.ExecuteAndHandleError<TransactionData>((Func<TransactionData>) (() => processor.GetTransaction(transactionId)));
    }

    public IEnumerable<TransactionData> GetTransactionsByBatch(string batchId)
    {
      ICCBatchTransactionGetter processor = this.GetProcessor<ICCBatchTransactionGetter>(CCProcessingFeature.TransactionGetter);
      return V2PluginErrorHandler.ExecuteAndHandleError<IEnumerable<TransactionData>>((Func<IEnumerable<TransactionData>>) (() => processor.GetTransactionsByBatch(batchId, (TransactionSearchParams) null)));
    }

    public IEnumerable<TransactionData> GetTransactionsByTypedBatch(
      string batchId,
      BatchType batchType)
    {
      ICCBatchTransactionGetter processor = this.GetProcessor<ICCBatchTransactionGetter>(CCProcessingFeature.TransactionGetter);
      TransactionSearchParams transactionSearchParams = new TransactionSearchParams()
      {
        BatchType = new BatchType?(batchType)
      };
      return V2PluginErrorHandler.ExecuteAndHandleError<IEnumerable<TransactionData>>((Func<IEnumerable<TransactionData>>) (() => processor.GetTransactionsByBatch(batchId, transactionSearchParams)));
    }

    public IEnumerable<TransactionData> GetTransactionsByCustomer(
      string customerProfileId,
      TransactionSearchParams searchParams = null)
    {
      ICCTransactionGetter processor = this.GetProcessor<ICCTransactionGetter>(CCProcessingFeature.TransactionGetter);
      return V2PluginErrorHandler.ExecuteAndHandleError<IEnumerable<TransactionData>>((Func<IEnumerable<TransactionData>>) (() => processor.GetTransactionsByCustomer(customerProfileId, searchParams)));
    }

    public TransactionData FindTransaction(TransactionSearchParams searchParams)
    {
      ICCTransactionFinder finder = this.GetProcessor<ICCTransactionFinder>(CCProcessingFeature.TransactionFinder);
      return V2PluginErrorHandler.ExecuteAndHandleError<TransactionData>((Func<TransactionData>) (() => finder.FindTransaction(searchParams)));
    }

    public IEnumerable<TransactionData> GetUnsettledTransactions(
      TransactionSearchParams searchParams = null)
    {
      ICCTransactionGetter processor = this.GetProcessor<ICCTransactionGetter>(CCProcessingFeature.TransactionGetter);
      return V2PluginErrorHandler.ExecuteAndHandleError<IEnumerable<TransactionData>>((Func<IEnumerable<TransactionData>>) (() => processor.GetUnsettledTransactions(searchParams)));
    }

    public IEnumerable<BatchData> GetSettledBatches(BatchSearchParams batchSearchParams)
    {
      ICCSettledBatchGetter processor = this.GetProcessor<ICCSettledBatchGetter>(CCProcessingFeature.TransactionGetter);
      return V2PluginErrorHandler.ExecuteAndHandleError<IEnumerable<BatchData>>((Func<IEnumerable<BatchData>>) (() => processor.GetSettledBatches(batchSearchParams)));
    }

    private T GetProcessor<T>(CCProcessingFeature feature) where T : class
    {
      return this._plugin.CreateProcessor<T>(new V2SettingsGenerator(this._provider).GetSettings()) ?? throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} feature is not supported by processing", new object[1]
      {
        (object) feature
      }));
    }

    public void ExportSettings(IList<PluginSettingDetail> aSettings)
    {
      foreach (SettingsDetail exportSetting in this._plugin.ExportSettings())
        aSettings.Add(V2Converter.ConvertSettingsDetail(exportSetting));
    }

    public void TestCredentials(APIResponse apiResponse)
    {
      V2SettingsGenerator settingsGenerator = new V2SettingsGenerator(this.GetProvider());
      try
      {
        this._plugin.TestCredentials(settingsGenerator.GetSettings());
        apiResponse.isSucess = true;
        apiResponse.ErrorSource = CCError.CCErrorSource.None;
        apiResponse.Messages = (Dictionary<string, string>) null;
      }
      catch (CCProcessingException ex)
      {
        apiResponse.ErrorSource = CCError.CCErrorSource.ProcessingCenter;
        apiResponse.isSucess = false;
        if (apiResponse.Messages.Keys.Contains<string>("Exception"))
          apiResponse.Messages["Exception"] = ((Exception) ex).Message;
        else
          apiResponse.Messages.Add("Exception", ((Exception) ex).Message);
      }
    }

    public CCError ValidateSettings(PluginSettingDetail setting)
    {
      string str = this._plugin.ValidateSettings(V2Converter.ConvertSettingDetailToV2(setting));
      return new CCError()
      {
        source = string.IsNullOrEmpty(str) ? CCError.CCErrorSource.None : CCError.CCErrorSource.Internal,
        ErrorMessage = str
      };
    }
  }
}
