// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.BaseProfileProcessingWrapper
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

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public class BaseProfileProcessingWrapper
{
  public static IBaseProfileProcessingWrapper GetBaseProfileProcessingWrapper(
    object pluginObject,
    ICardProcessingReadersProvider provider)
  {
    IBaseProfileProcessingWrapper processingWrapper = BaseProfileProcessingWrapper.GetBaseProfileProcessingWrapper(pluginObject);
    if (!(processingWrapper is ISetCardProcessingReadersProvider processingReadersProvider))
      throw new PXException("Could not set CardProcessingReadersProvider");
    processingReadersProvider.SetProvider(provider);
    return processingWrapper;
  }

  private static IBaseProfileProcessingWrapper GetBaseProfileProcessingWrapper(object pluginObject)
  {
    CCProcessingHelper.CheckHttpsConnection();
    return (IBaseProfileProcessingWrapper) new BaseProfileProcessingWrapper.V2BaseProfileProcessor((!CCProcessingHelper.IsV1ProcessingInterface(pluginObject.GetType()) ? CCProcessingHelper.IsV2ProcessingInterface(pluginObject) : throw new PXException("The operation cannot be completed. The {0} processing center uses an unsupported plug-in.")) ?? throw new PXException("The type of the card processing plugin is unknown: {0}. Check the card processing settings.", new object[1]
    {
      (object) pluginObject.GetType().Name
    }));
  }

  private class V2BaseProfileProcessor : V2ProcessorBase, IBaseProfileProcessingWrapper
  {
    private ICCProcessingPlugin _plugin;

    public V2BaseProfileProcessor(ICCProcessingPlugin v2Plugin) => this._plugin = v2Plugin;

    private ICCProfileProcessor GetProcessor()
    {
      return this._plugin.CreateProcessor<ICCProfileProcessor>(new V2SettingsGenerator(this.GetProvider()).GetSettings()) ?? throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} feature is not supported by processing", new object[1]
      {
        (object) CCProcessingFeature.ProfileManagement
      }));
    }

    public string CreateCustomerProfile()
    {
      ICardProcessingReadersProvider provider = this.GetProvider();
      CustomerData customerData = V2ProcessingInputGenerator.GetCustomerData(provider.GetCustomerDataReader());
      customerData.AddressData = V2ProcessingInputGenerator.GetAddressData(provider.GetCustomerDataReader());
      return V2PluginErrorHandler.ExecuteAndHandleError<string>((Func<string>) (() => this.GetProcessor().CreateCustomerProfile(customerData)));
    }

    public string CreatePaymentProfile()
    {
      ICardProcessingReadersProvider provider = this.GetProvider();
      string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(provider.GetCustomerDataReader()).CustomerProfileID;
      CreditCardData cardData = V2ProcessingInputGenerator.GetCardData(provider.GetCardDataReader(), provider.GetExpirationDateConverter());
      cardData.AddressData = V2ProcessingInputGenerator.GetAddressData(provider.GetCustomerDataReader());
      return V2PluginErrorHandler.ExecuteAndHandleError<string>((Func<string>) (() => this.GetProcessor().CreatePaymentProfile(customerProfileId, cardData)));
    }

    public void DeleteCustomerProfile()
    {
      string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(this.GetProvider().GetCustomerDataReader()).CustomerProfileID;
      V2PluginErrorHandler.ExecuteAndHandleError((Action) (() => this.GetProcessor().DeleteCustomerProfile(customerProfileId)));
    }

    public void DeletePaymentProfile()
    {
      string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader()).CustomerProfileID;
      string paymentProfileId = V2ProcessingInputGenerator.GetCardData(this._provider.GetCardDataReader()).PaymentProfileID;
      V2PluginErrorHandler.ExecuteAndHandleError((Action) (() => this.GetProcessor().DeletePaymentProfile(customerProfileId, paymentProfileId)));
    }

    public CreditCardData GetPaymentProfile()
    {
      string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader()).CustomerProfileID;
      string paymentProfileId = V2ProcessingInputGenerator.GetCardData(this._provider.GetCardDataReader()).PaymentProfileID;
      return V2PluginErrorHandler.ExecuteAndHandleError<CreditCardData>((Func<CreditCardData>) (() => this.GetProcessor().GetPaymentProfile(customerProfileId, paymentProfileId)));
    }

    public CustomerData GetCustomerProfile(string customerProfileId)
    {
      return V2PluginErrorHandler.ExecuteAndHandleError<CustomerData>((Func<CustomerData>) (() => this.GetProcessor().GetCustomerProfile(customerProfileId)));
    }
  }
}
