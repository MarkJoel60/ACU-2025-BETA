// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.V2SettingsGenerator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public class V2SettingsGenerator
{
  private ICardProcessingReadersProvider _provider;

  public V2SettingsGenerator(ICardProcessingReadersProvider provider) => this._provider = provider;

  public IEnumerable<SettingsValue> GetSettings()
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    this._provider.GetProcessingCenterSettingsStorage().ReadSettings(dictionary);
    List<SettingsValue> settings = new List<SettingsValue>();
    foreach (KeyValuePair<string, string> keyValuePair in dictionary)
    {
      SettingsValue settingsValue = new SettingsValue()
      {
        DetailID = keyValuePair.Key,
        Value = keyValuePair.Value
      };
      settings.Add(settingsValue);
    }
    return (IEnumerable<SettingsValue>) settings;
  }
}
