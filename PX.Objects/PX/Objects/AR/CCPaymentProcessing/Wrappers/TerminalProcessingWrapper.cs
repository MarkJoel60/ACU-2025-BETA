// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.TerminalProcessingWrapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers.Interfaces;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public class TerminalProcessingWrapper
{
  public static ITerminalProcessingWrapper GetTerminalProcessingWrapper(
    object pluginObject,
    ICardProcessingReadersProvider provider)
  {
    ITerminalProcessingWrapper processingWrapper = TerminalProcessingWrapper.GetTerminalProcessingWrapper(pluginObject);
    if (!(processingWrapper is ISetCardProcessingReadersProvider processingReadersProvider))
      throw new ApplicationException("Could not set CardProcessingReadersProvider");
    processingReadersProvider.SetProvider(provider);
    return processingWrapper;
  }

  private static ITerminalProcessingWrapper GetTerminalProcessingWrapper(object pluginObject)
  {
    return (ITerminalProcessingWrapper) new TerminalProcessingWrapper.V2TerminalProcessor((!CCProcessingHelper.IsV1ProcessingInterface(pluginObject.GetType()) ? CCProcessingHelper.IsV2ProcessingInterface(pluginObject) : throw new PXException("The operation cannot be completed. The {0} processing center uses an unsupported plug-in.")) ?? throw new PXException("The type of the card processing plugin is unknown: {0}. Check the card processing settings.", new object[1]
    {
      (object) pluginObject.GetType().Name
    }));
  }

  protected class V2TerminalProcessor : 
    V2ProcessorBase,
    ITerminalProcessingWrapper,
    ISetCardProcessingReadersProvider
  {
    private readonly ICCProcessingPlugin _plugin;

    public V2TerminalProcessor(ICCProcessingPlugin v2Plugin) => this._plugin = v2Plugin;

    public POSTerminalData GetTerminal(string terminalID)
    {
      return this.GetProcessor<ICCTerminalGetter>(CCProcessingFeature.TerminalGetter).GetTerminal(terminalID);
    }

    public IEnumerable<POSTerminalData> GetTerminals()
    {
      return this.GetProcessor<ICCTerminalGetter>(CCProcessingFeature.TerminalGetter).GetTerminals();
    }

    private T GetProcessor<T>(CCProcessingFeature feature) where T : class
    {
      return this._plugin.CreateProcessor<T>(new V2SettingsGenerator(this._provider).GetSettings()) ?? throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} feature is not supported by processing", new object[1]
      {
        (object) feature
      }));
    }
  }
}
