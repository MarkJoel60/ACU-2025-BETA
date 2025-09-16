// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.HostedFromProcessingWrapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public class HostedFromProcessingWrapper
{
  private static readonly Regex checkUrl = new Regex("^(https://[\\w-.]+?)(?=\\.\\w+/)([\\w-./?%&=]+)$", RegexOptions.Compiled);

  public static IHostedFromProcessingWrapper GetHostedFormProcessingWrapper(
    object pluginObject,
    ICardProcessingReadersProvider provider)
  {
    IHostedFromProcessingWrapper processingWrapper = HostedFromProcessingWrapper.GetHostedFormProcessingWrapper(pluginObject);
    if (!(processingWrapper is ISetCardProcessingReadersProvider processingReadersProvider))
      throw new PXException("Could not set CardProcessingReadersProvider");
    processingReadersProvider.SetProvider(provider);
    return processingWrapper;
  }

  private static IHostedFromProcessingWrapper GetHostedFormProcessingWrapper(object pluginObject)
  {
    CCProcessingHelper.CheckHttpsConnection();
    return (IHostedFromProcessingWrapper) new HostedFromProcessingWrapper.V2HostedFormProcessor((!CCProcessingHelper.IsV1ProcessingInterface(pluginObject.GetType()) ? CCProcessingHelper.IsV2ProcessingInterface(pluginObject) : throw new PXException("The operation cannot be completed. The {0} processing center uses an unsupported plug-in.")) ?? throw new PXException("The type of the card processing plugin is unknown: {0}. Check the card processing settings.", new object[1]
    {
      (object) pluginObject.GetType().Name
    }));
  }

  public static IHostedPaymentFormProcessingWrapper GetPaymentFormProcessingWrapper(
    object pluginObject,
    ICardProcessingReadersProvider provider,
    CCProcessingContext context)
  {
    CCProcessingHelper.CheckHttpsConnection();
    HostedFromProcessingWrapper.V2HostedFormProcessor processingWrapper = new HostedFromProcessingWrapper.V2HostedFormProcessor((!CCProcessingHelper.IsV1ProcessingInterface(pluginObject.GetType()) ? CCProcessingHelper.IsV2ProcessingInterface(pluginObject) : throw new PXException("The operation cannot be completed. The {0} processing center uses an unsupported plug-in.")) ?? throw new PXException("The type of the card processing plugin is unknown: {0}. Check the card processing settings.", new object[1]
    {
      (object) pluginObject.GetType().Name
    }));
    processingWrapper.ProcessingCenterId = context?.processingCenter.ProcessingCenterID;
    processingWrapper.CompanyName = context?.callerGraph.Accessinfo.CompanyName;
    if (processingWrapper == null)
      throw new PXException("Could not set CardProcessingReadersProvider");
    processingWrapper.SetProvider(provider);
    return (IHostedPaymentFormProcessingWrapper) processingWrapper;
  }

  private static IEnumerable<CreditCardData> GetExistingProfiles(
    ICardProcessingReadersProvider _provider)
  {
    return _provider.GetCustomerCardsDataReaders().Select<ICreditCardDataReader, CreditCardData>((Func<ICreditCardDataReader, CreditCardData>) (reader => V2ProcessingInputGenerator.GetCardData(reader))).Where<CreditCardData>((Func<CreditCardData, bool>) (card => card.PaymentProfileID != null));
  }

  private class V2HostedFormProcessor : 
    V2ProcessorBase,
    IHostedFromProcessingWrapper,
    IHostedPaymentFormProcessingWrapper
  {
    private readonly ICCProcessingPlugin _plugin;

    public string ProcessingCenterId { get; set; }

    public string CompanyName { get; set; }

    public V2HostedFormProcessor(ICCProcessingPlugin v2Plugin) => this._plugin = v2Plugin;

    private T GetProcessor<T>(CCProcessingFeature feature = CCProcessingFeature.HostedForm) where T : class
    {
      return this._plugin.CreateProcessor<T>(new V2SettingsGenerator(this._provider).GetSettings()) ?? throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} feature is not supported by processing", new object[1]
      {
        (object) feature
      }));
    }

    public void GetCreateForm()
    {
      ICCHostedFormProcessor processor = this.GetProcessor<ICCHostedFormProcessor>();
      CustomerData customerData = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader());
      AddressData addressData = V2ProcessingInputGenerator.GetAddressData(this._provider.GetCustomerDataReader());
      HostedFormData hostedFormData = V2PluginErrorHandler.ExecuteAndHandleError<HostedFormData>((Func<HostedFormData>) (() => processor.GetDataForCreateForm(customerData, addressData)));
      throw new PXPaymentRedirectException(hostedFormData.Caption, hostedFormData.Url, hostedFormData.UseGetMethod, hostedFormData.Token, hostedFormData.Parameters)
      {
        DisableTopLevelNavigation = hostedFormData.DisableTopLevelNavigation
      };
    }

    public void GetManageForm()
    {
      ICCHostedFormProcessor processor = this.GetProcessor<ICCHostedFormProcessor>();
      CustomerData customerData = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader());
      CreditCardData cardData = V2ProcessingInputGenerator.GetCardData(this._provider.GetCardDataReader());
      HostedFormData hostedFormData = V2PluginErrorHandler.ExecuteAndHandleError<HostedFormData>((Func<HostedFormData>) (() => processor.GetDataForManageForm(customerData, cardData)));
      throw new PXPaymentRedirectException(hostedFormData.Caption, hostedFormData.Url, hostedFormData.UseGetMethod, hostedFormData.Token, hostedFormData.Parameters)
      {
        DisableTopLevelNavigation = hostedFormData.DisableTopLevelNavigation
      };
    }

    public void PrepareProfileForm()
    {
      ICCProfileFormProcessor processor = this.GetProcessor<ICCProfileFormProcessor>();
      ProfileFormPrepareOptions formPrepareOptions = new ProfileFormPrepareOptions()
      {
        Customer = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader()),
        Address = V2ProcessingInputGenerator.GetAddressData(this._provider.GetCustomerDataReader()),
        MeansOfPayment = this._provider.GetPaymentMethodDataReader().GetPaymentMethod()?.PaymentType == "EFT" ? (MeansOfPayment) 1 : (MeansOfPayment) 0
      };
      throw new PXPluginRedirectException<PXPluginRedirectOptions>(V2PluginErrorHandler.ExecuteAndHandleError<PXPluginRedirectOptions>((Func<PXPluginRedirectOptions>) (() => processor.PrepareProfileForm(formPrepareOptions))));
    }

    public ProfileFormResponseProcessResult ProcessProfileFormResponse(string response)
    {
      ICCProfileFormProcessor processor = this.GetProcessor<ICCProfileFormProcessor>();
      ProfileFormPrepareOptions formPrepareOptions = new ProfileFormPrepareOptions()
      {
        Customer = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader()),
        Address = V2ProcessingInputGenerator.GetAddressData(this._provider.GetCustomerDataReader())
      };
      ProfileFormResponseProcessOptions options = new ProfileFormResponseProcessOptions()
      {
        PrepareOptions = formPrepareOptions,
        Response = response
      };
      return V2PluginErrorHandler.ExecuteAndHandleError<ProfileFormResponseProcessResult>((Func<ProfileFormResponseProcessResult>) (() => processor.ProcessProfileFormResponse(options)));
    }

    public IEnumerable<CreditCardData> GetMissingPaymentProfiles()
    {
      ICCProfileProcessor processor = this.GetProcessor<ICCProfileProcessor>(CCProcessingFeature.ProfileManagement);
      string customerProfileId = V2ProcessingInputGenerator.GetCustomerData(this._provider.GetCustomerDataReader()).CustomerProfileID;
      return V2PluginErrorHandler.ExecuteAndHandleError<IEnumerable<CreditCardData>>((Func<IEnumerable<CreditCardData>>) (() => InterfaceExtensions.GetMissingPaymentProfiles(processor, customerProfileId, HostedFromProcessingWrapper.GetExistingProfiles(this._provider))));
    }

    public void GetPaymentForm(ProcessingInput inputData)
    {
      ICCHostedPaymentFormProcessor formProcessor = this.GetProcessor<ICCHostedPaymentFormProcessor>();
      HostedFormData hostedFormData = V2PluginErrorHandler.ExecuteAndHandleError<HostedFormData>((Func<HostedFormData>) (() =>
      {
        this.CheckWebhook();
        return formProcessor.GetDataForPaymentForm(inputData);
      }));
      PXTrace.WriteInformation("Perform PaymentRedirectException. Url: " + hostedFormData.Url);
      throw new PXPaymentRedirectException(hostedFormData.Caption, hostedFormData.Url, hostedFormData.UseGetMethod, hostedFormData.Token, hostedFormData.Parameters);
    }

    public HostedFormResponse ParsePaymentFormResponse(string response)
    {
      ICCHostedPaymentFormResponseParser parser = this.GetProcessor<ICCHostedPaymentFormResponseParser>();
      return V2Converter.ConvertHostedFormResponse(V2PluginErrorHandler.ExecuteAndHandleError<HostedFormResponse>((Func<HostedFormResponse>) (() => parser.Parse(response))));
    }

    public PXPluginRedirectOptions PreparePaymentForm(PaymentFormPrepareOptions inputData)
    {
      ICCPaymentFormProcessor processor = this.GetProcessor<ICCPaymentFormProcessor>();
      return V2PluginErrorHandler.ExecuteAndHandleError<PXPluginRedirectOptions>((Func<PXPluginRedirectOptions>) (() => processor.PreparePaymentForm(inputData)));
    }

    public PaymentFormResponseProcessResult ProcessPaymentFormResponse(
      PaymentFormPrepareOptions inputData,
      string response)
    {
      ICCPaymentFormProcessor processor = this.GetProcessor<ICCPaymentFormProcessor>();
      PaymentFormResponseProcessOptions options = new PaymentFormResponseProcessOptions()
      {
        PrepareOptions = inputData,
        Response = response
      };
      return V2PluginErrorHandler.ExecuteAndHandleError<PaymentFormResponseProcessResult>((Func<PaymentFormResponseProcessResult>) (() => processor.ProcessPaymentFormResponse(options)));
    }

    private void CheckWebhook()
    {
      V2SettingsGenerator settingsGenerator = new V2SettingsGenerator(this._provider);
      if (!CCProcessingFeatureHelper.IsFeatureSupported(this._plugin.GetType(), CCProcessingFeature.WebhookManagement))
      {
        PXTrace.WriteInformation("Skip check webhook. Plugin doesn't implement this feature.");
      }
      else
      {
        ICCWebhookProcessor processor = this._plugin.CreateProcessor<ICCWebhookProcessor>(settingsGenerator.GetSettings());
        if (!processor.WebhookEnabled)
        {
          PXTrace.WriteInformation("Skip check webhook. This feature is disabled.");
        }
        else
        {
          string url = CCServiceEndpointHelper.GetEndpointUrl(CCServiceEndpointHelper.EncodeUrlSegment(this.CompanyName), CCServiceEndpointHelper.EncodeUrlSegment(this.ProcessingCenterId));
          if (url == null || url.Contains("localhost"))
            PXTrace.WriteInformation("Skip check webhook. Not valid Url: " + url);
          else if (!HostedFromProcessingWrapper.checkUrl.IsMatch(url))
          {
            PXTrace.WriteInformation("Skip check webhook. Not valid Url: " + url);
          }
          else
          {
            if (processor.GetAttachedWebhooks().Where<Webhook>((Func<Webhook, bool>) (i => i.Url == url)).FirstOrDefault<Webhook>() != null)
              return;
            string str = "AcumaticaWebhook";
            PXTrace.WriteInformation($"Webhook not found. Performing add webhook with name = {str}, url = {url}");
            processor.AddWebhook(new Webhook()
            {
              Enable = true,
              Events = new List<WebhookEvent>()
              {
                (WebhookEvent) 7,
                (WebhookEvent) 6
              },
              Name = str,
              Url = url
            });
          }
        }
      }
    }
  }
}
