// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Helpers.CCProcessingFeatureHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.CA;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Helpers;

public class CCProcessingFeatureHelper
{
  public static bool IsFeatureSupported(Type pluginType, CCProcessingFeature feature)
  {
    bool flag = false;
    if (typeof (ICCProcessingPlugin).IsAssignableFrom(pluginType))
    {
      ICCProcessingPlugin plugin = (ICCProcessingPlugin) Activator.CreateInstance(pluginType);
      Func<object>[] source = (Func<object>[]) null;
      switch (feature)
      {
        case CCProcessingFeature.ProfileManagement:
          source = new Func<object>[1]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCProfileProcessor>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.ExtendedProfileManagement:
          source = new Func<object>[1]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCProfileProcessor>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.HostedForm:
          source = new Func<object>[1]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCHostedFormProcessor>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.PaymentHostedForm:
          source = new Func<object>[4]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCHostedPaymentFormProcessor>((IEnumerable<SettingsValue>) null)),
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCTransactionGetter>((IEnumerable<SettingsValue>) null)),
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCProfileCreator>((IEnumerable<SettingsValue>) null)),
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCHostedPaymentFormResponseParser>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.WebhookManagement:
          source = new Func<object>[2]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCWebhookProcessor>((IEnumerable<SettingsValue>) null)),
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCWebhookResolver>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.TransactionGetter:
          source = new Func<object>[1]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCTransactionGetter>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.PaymentForm:
          source = new Func<object>[3]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCPaymentFormProcessor>((IEnumerable<SettingsValue>) null)),
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCTransactionGetter>((IEnumerable<SettingsValue>) null)),
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCProfileCreator>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.CapturePreauthorization:
          return CCProcessingFeatureHelper.IsFeatureSupportedByPlugin(plugin, (PluginFeature) 0) ?? true;
        case CCProcessingFeature.ProfileEditForm:
          return CCProcessingFeatureHelper.IsFeatureSupportedByPlugin(plugin, (PluginFeature) 1) ?? true;
        case CCProcessingFeature.ProfileForm:
          source = new Func<object>[1]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCProfileFormProcessor>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.TransactionFinder:
          source = new Func<object>[1]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCTransactionFinder>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.EFTSupport:
          return CCProcessingFeatureHelper.IsFeatureSupportedByPlugin(plugin, (PluginFeature) 2).GetValueOrDefault();
        case CCProcessingFeature.PayLink:
          source = new Func<object>[1]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCPayLinkProcessor>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.Level3:
          return CCProcessingFeatureHelper.IsFeatureSupportedByPlugin(plugin, (PluginFeature) 3).GetValueOrDefault();
        case CCProcessingFeature.TerminalGetter:
          source = new Func<object>[1]
          {
            (Func<object>) (() => (object) plugin.CreateProcessor<ICCTerminalGetter>((IEnumerable<SettingsValue>) null))
          };
          break;
        case CCProcessingFeature.AuthorizeIncrement:
          return CCProcessingFeatureHelper.IsFeatureSupportedByPlugin(plugin, (PluginFeature) 4).GetValueOrDefault();
        case CCProcessingFeature.AuthorizeBasedOnPrevious:
          return CCProcessingFeatureHelper.IsFeatureSupportedByPlugin(plugin, (PluginFeature) 5).GetValueOrDefault();
      }
      if (source != null)
        flag = ((IEnumerable<Func<object>>) source).All<Func<object>>((Func<Func<object>, bool>) (f => CCProcessingFeatureHelper.CheckV2TypeWrapper(f)));
    }
    return flag;
  }

  private static bool CheckV2TypeWrapper(Func<object> check)
  {
    bool flag = true;
    try
    {
      flag = check() != null;
    }
    catch
    {
    }
    return flag;
  }

  public static bool IsFeatureSupported(
    CCProcessingCenter ccProcessingCenter,
    CCProcessingFeature feature)
  {
    return CCProcessingFeatureHelper.IsFeatureSupported(ccProcessingCenter, feature, false);
  }

  public static bool IsFeatureSupported(
    CCProcessingCenter ccProcessingCenter,
    CCProcessingFeature feature,
    bool throwOnError)
  {
    if (string.IsNullOrEmpty(ccProcessingCenter?.ProcessingTypeName))
    {
      if (throwOnError)
        throw new PXException("Plug-in type is not selected for the {0} processing center.", new object[1]
        {
          (object) ccProcessingCenter?.ProcessingCenterID
        });
      return false;
    }
    try
    {
      return CCProcessingFeatureHelper.IsFeatureSupported(CCPluginTypeHelper.GetPluginTypeWithCheck(ccProcessingCenter), feature);
    }
    catch
    {
      if (!throwOnError)
        return false;
      throw;
    }
  }

  public static void CheckProcessing(
    CCProcessingCenter processingCenter,
    CCProcessingFeature feature,
    CCProcessingContext newContext)
  {
    CCProcessingFeatureHelper.CheckProcessingCenter(processingCenter);
    newContext.processingCenter = processingCenter;
    if (feature != CCProcessingFeature.Base && !CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, feature, true))
      throw new PXException("{0} feature is not supported by processing", new object[1]
      {
        (object) feature.ToString()
      });
  }

  public static bool IsPaymentHostedFormSupported(CCProcessingCenter procCenter)
  {
    return CCProcessingFeatureHelper.IsFeatureSupported(procCenter, CCProcessingFeature.PaymentHostedForm, false) || CCProcessingFeatureHelper.IsFeatureSupported(procCenter, CCProcessingFeature.PaymentForm, false);
  }

  private static void CheckProcessingCenter(CCProcessingCenter processingCenter)
  {
    if (processingCenter == null)
      throw new PXException("Processing center can't be found");
    if (!processingCenter.IsActive.GetValueOrDefault())
      throw new PXException("Processing center {0} is inactive", new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      });
    if (string.IsNullOrEmpty(processingCenter.ProcessingTypeName))
      throw new PXException("Processing center for this card type is not configured properly.");
  }

  private static bool? IsFeatureSupportedByPlugin(
    ICCProcessingPlugin plugin,
    PluginFeature pluginFeature)
  {
    return plugin.CreateProcessor<ICCFeatureManager>((IEnumerable<SettingsValue>) null)?.IsFeatureSupported(pluginFeature);
  }
}
