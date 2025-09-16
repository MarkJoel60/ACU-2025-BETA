// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Helpers.CCPluginTypeHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Licensing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.CA;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Helpers;

/// <summary>A class that contains auxiliary methods for working with plug-in types.</summary>
public static class CCPluginTypeHelper
{
  /// <summary> The list of plug-in types that have been removed from the Acumatica ERP code in Version 2019 R2.</summary>
  public static string[] NotSupportedPluginTypeNames
  {
    get
    {
      return new string[2]
      {
        "PX.CCProcessing.AuthorizeNetProcessing",
        "PX.CCProcessing.AuthorizeNetTokenizedProcessing"
      };
    }
  }

  public static Type GetPluginType(string typeName)
  {
    return CCPluginTypeHelper.GetPluginType(typeName, true);
  }

  public static Type GetPluginType(string typeName, bool throwOnError)
  {
    return PXBuildManager.GetType(typeName, throwOnError);
  }

  public static object CreatePluginInstance(CCProcessingCenter procCenter)
  {
    return Activator.CreateInstance(CCPluginTypeHelper.GetPluginTypeWithCheck(procCenter));
  }

  /// <summary>
  /// Gets plug-in type by name and performs its validation.
  /// </summary>
  /// <param name="typeName">Name of the plug-in type to get.</param>
  /// <param name="pluginType">Contains type if found; otherwise, null/.</param>
  /// <returns>Result of plug-in type search and validation.</returns>
  public static CCPluginCheckResult TryGetPluginTypeWithCheck(string typeName, out Type pluginType)
  {
    pluginType = (Type) null;
    if (string.IsNullOrEmpty(typeName))
      return CCPluginCheckResult.Empty;
    if (CCPluginTypeHelper.InUnsupportedList(typeName))
      return CCPluginCheckResult.Unsupported;
    pluginType = CCPluginTypeHelper.GetPluginType(typeName, false);
    if (pluginType == (Type) null)
      return CCPluginCheckResult.Missing;
    return CCProcessingHelper.IsV1ProcessingInterface(pluginType) ? CCPluginCheckResult.Unsupported : CCPluginCheckResult.Ok;
  }

  /// <summary>
  /// Checks whether the payment plug-in type that is configured for the processing center is supported by the processing center
  /// and returns the Type object that corresponds to this plug-in type.
  /// </summary>
  public static Type GetPluginTypeWithCheck(CCProcessingCenter procCenter)
  {
    if (procCenter == null)
      throw new ArgumentNullException(nameof (procCenter));
    Type pluginType;
    switch (CCPluginTypeHelper.TryGetPluginTypeWithCheck(procCenter.ProcessingTypeName, out pluginType))
    {
      case CCPluginCheckResult.Empty:
        throw new PXException("Plug-in type is not selected for the {0} processing center.", new object[1]
        {
          (object) procCenter.ProcessingCenterID
        });
      case CCPluginCheckResult.Missing:
        throw new PXException("The {0} processing center references a missing plug-in.", new object[1]
        {
          (object) procCenter.ProcessingCenterID
        });
      case CCPluginCheckResult.Unsupported:
        throw new PXException("The {0} processing center uses an unsupported plug-in.", new object[1]
        {
          (object) procCenter.ProcessingCenterID
        });
      default:
        return pluginType;
    }
  }

  /// <summary>
  /// Performs validation of the plug-in referenced by Processing Center.
  /// </summary>
  /// <param name="graph">A graph to perform Db queries</param>
  /// <param name="procCenterId">Procesing Center Id</param>
  /// <returns>Result of plug-in validation.</returns>
  public static CCPluginCheckResult CheckProcessingCenterPlugin(PXGraph graph, string procCenterId)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (procCenterId == null)
      throw new ArgumentNullException(nameof (procCenterId));
    return CCPluginTypeHelper.TryGetPluginTypeWithCheck(PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select(graph, new object[1]
    {
      (object) procCenterId
    }))?.ProcessingTypeName, out Type _);
  }

  public static bool InUnsupportedList(string typeStr)
  {
    return typeStr != null && ((IEnumerable<string>) CCPluginTypeHelper.NotSupportedPluginTypeNames).Any<string>((Func<string, bool>) (i => i == typeStr));
  }

  /// <summary>Checks whether the plug-in type is inherited from the parent class.</summary>
  /// <param name="pluginType">The Type object that should be checked.</param>
  /// <param name="checkTypeName">The full name of the parent class.</param>
  /// <param name="currLevel">The current level of recursion iteration.</param>
  /// <param name="maxLevel">The maximum level of recursion iteration.</param>
  public static bool CheckParentClass(
    Type pluginType,
    string checkTypeName,
    int currLevel,
    int maxLevel)
  {
    if (pluginType == (Type) null || maxLevel == currLevel)
      return false;
    return pluginType.FullName == checkTypeName || CCPluginTypeHelper.CheckParentClass(pluginType.BaseType, checkTypeName, currLevel + 1, maxLevel);
  }

  /// <summary>Checks whether the plug-in type implements the interface.</summary>
  /// <param name="pluginType">The Type object that should be checked.</param>
  /// <param name="interfaceTypeName">The full name of the interface.</param>
  public static bool CheckImplementInterface(Type pluginType, string interfaceTypeName)
  {
    TypeFilter filter = (TypeFilter) ((t, o) => t.FullName == o.ToString());
    return pluginType.FindInterfaces(filter, (object) interfaceTypeName).Length != 0;
  }

  public static bool IsProcCenterFeatureDisabled(string typeName)
  {
    return CCPluginTypeHelper.IsProcCenterFeatureDisabled(typeName, false);
  }

  public static void ThrowIfProcCenterFeatureDisabled(string typeName)
  {
    CCPluginTypeHelper.IsProcCenterFeatureDisabled(typeName, true);
  }

  private static bool IsProcCenterFeatureDisabled(string typeName, bool throwOnError)
  {
    switch (typeName)
    {
      case null:
        return false;
      case "PX.CCProcessing.Fortis.V2.FortisProcessingPlugin":
      case "PX.CCProcessing.AcumaticaPayments.V2.AcumaticaPaymentsProcessingPlugin":
        if (!PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>() || !CCPluginTypeHelper.VerifyAssemblyCodeSign(typeName))
        {
          if (!throwOnError)
            return true;
          throw new PXSetPropertyException("The Acumatica Payments feature is disabled on the Enable/Disable Features (CS100000) form.");
        }
        break;
      case "PX.CCProcessing.V2.AuthnetProcessingPlugin":
        if (!PXAccess.FeatureInstalled<FeaturesSet.authorizeNetIntegration>() || !CCPluginTypeHelper.VerifyAssemblyCodeSign(typeName))
        {
          if (!throwOnError)
            return true;
          throw new PXSetPropertyException("The Authorize.Net Integration feature is disabled on the Enable/Disable Features (CS100000) form.");
        }
        break;
      case "PX.CCProcessing.V2.AuthnetUnlocker.AuthnetProcessingPlugin":
        if (!PXAccess.FeatureInstalled<FeaturesSet.authorizeNetIntegration>())
        {
          if (!throwOnError)
            return true;
          throw new PXSetPropertyException("The Authorize.Net Integration feature is disabled on the Enable/Disable Features (CS100000) form.");
        }
        break;
      case "PX.CCProcessing.Stripe.V2.StripeProcessingPlugin":
        if (!PXAccess.FeatureInstalled<FeaturesSet.stripeIntegration>() || !CCPluginTypeHelper.VerifyAssemblyCodeSign(typeName))
        {
          if (!throwOnError)
            return true;
          throw new PXSetPropertyException("The Stripe Integration feature is disabled on the Enable/Disable Features (CS100000) form.");
        }
        break;
      case "PX.Commerce.Shopify.ShopifyPayments.ShopifyPaymentsProcessingPlugin":
        if (!PXAccess.FeatureInstalled<FeaturesSet.shopifyIntegration>())
          return true;
        break;
      default:
        if (!PXAccess.FeatureInstalled<FeaturesSet.customCCIntegration>())
        {
          if (!throwOnError)
            return true;
          throw new PXSetPropertyException("The Custom Card Processing Integration feature is disabled on the Enable/Disable Features (CS100000) form.");
        }
        break;
    }
    return false;
  }

  private static bool VerifyAssemblyCodeSign(string typeName)
  {
    if (!(((IServiceProvider) ServiceLocator.Current).GetService(typeof (ICodeSigningManager)) is ICodeSigningManager service))
      return true;
    Type pluginType = CCPluginTypeHelper.GetPluginType(typeName, false);
    return service.VerifyAssemblyCodeSign(pluginType.Assembly);
  }
}
