// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.ExtendedProfileProcessingWrapper
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

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public class ExtendedProfileProcessingWrapper
{
  public static IExtendedProfileProcessingWrapper GetExtendedProfileProcessingWrapper(
    object pluginObject,
    ICardProcessingReadersProvider provider)
  {
    IExtendedProfileProcessingWrapper processingWrapper = ExtendedProfileProcessingWrapper.GetExtendedProfileProcessingWrapper(pluginObject);
    if (!(processingWrapper is ISetCardProcessingReadersProvider processingReadersProvider))
      throw new PXException("Could not set CardProcessingReadersProvider");
    processingReadersProvider.SetProvider(provider);
    return processingWrapper;
  }

  private static IExtendedProfileProcessingWrapper GetExtendedProfileProcessingWrapper(
    object pluginObject)
  {
    CCProcessingHelper.CheckHttpsConnection();
    return (IExtendedProfileProcessingWrapper) new ExtendedProfileProcessingWrapper.V2ExtendedProfileProcessor((!CCProcessingHelper.IsV1ProcessingInterface(pluginObject.GetType()) ? CCProcessingHelper.IsV2ProcessingInterface(pluginObject) : throw new PXException("The operation cannot be completed. The {0} processing center uses an unsupported plug-in.")) ?? throw new PXException("The type of the card processing plugin is unknown: {0}. Check the card processing settings.", new object[1]
    {
      (object) pluginObject.GetType().Name
    }));
  }

  private class V2ExtendedProfileProcessor : 
    ISetCardProcessingReadersProvider,
    IExtendedProfileProcessingWrapper
  {
    private ICCProcessingPlugin _plugin;
    private ICardProcessingReadersProvider _provider;

    public V2ExtendedProfileProcessor(ICCProcessingPlugin v2Plugin) => this._plugin = v2Plugin;

    private T GetProcessor<T>() where T : class
    {
      return this._plugin.CreateProcessor<T>(new V2SettingsGenerator(this._provider).GetSettings()) ?? throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} feature is not supported by processing", new object[1]
      {
        (object) CCProcessingFeature.ExtendedProfileManagement
      }));
    }

    public IEnumerable<CustomerData> GetAllCustomerProfiles()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<CreditCardData> GetAllPaymentProfiles()
    {
      ICCProfileProcessor processor = this.GetProcessor<ICCProfileProcessor>();
      string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader()).CustomerProfileID;
      return V2PluginErrorHandler.ExecuteAndHandleError<IEnumerable<CreditCardData>>((Func<IEnumerable<CreditCardData>>) (() => processor.GetAllPaymentProfiles(customerProfileId)));
    }

    public TranProfile GetOrCreatePaymentProfileFromTransaction(
      string transactionId,
      CreateTranPaymentProfileParams cParams)
    {
      ICCProfileCreator processor = this.GetProcessor<ICCProfileCreator>();
      cParams.CustomerData = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader());
      cParams.CustomerData.AddressData = V2ProcessingInputGenerator.GetAddressData(this._provider.GetCustomerDataReader());
      return V2PluginErrorHandler.ExecuteAndHandleError<TranProfile>((Func<TranProfile>) (() => processor.GetOrCreatePaymentProfileFromTransaction(transactionId, cParams)));
    }

    public CustomerData GetCustomerProfile() => throw new NotImplementedException();

    public void UpdateCustomerProfile() => throw new NotImplementedException();

    public void UpdatePaymentProfile() => throw new NotImplementedException();

    public void SetProvider(ICardProcessingReadersProvider provider) => this._provider = provider;

    protected ICardProcessingReadersProvider GetProvider()
    {
      return this._provider != null ? this._provider : throw new PXInvalidOperationException("Could not set CardProcessingReaderProvider");
    }
  }
}
