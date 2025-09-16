// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.PluginSettingDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

public class PluginSettingDetail
{
  public string DetailID { get; set; }

  public string Descr { get; set; }

  /// <summary>The value selected by the user for the key.</summary>
  public string Value { get; set; }

  /// <summary>The type of the control.</summary>
  public int? ControlType { get; set; }

  /// <summary>Indicates (if set to <tt>true</tt>) that encryption of the key value is required.</summary>
  public bool? IsEncryptionRequired { get; set; }

  /// <summary>Sets/Retrieves the values in the combo box if the <see cref="P:PX.Objects.AR.CCPaymentProcessing.Common.PluginSettingDetail.ControlType" /> is <see cref="!:SettingsControlType.Combo" />.</summary>
  public ICollection<KeyValuePair<string, string>> ComboValuesCollection { get; set; }

  public PluginSettingDetail(string detailID, string descr, string value)
    : this(detailID, descr, value, new int?(1))
  {
  }

  public PluginSettingDetail(string detailID, string descr, string value, int? controlType)
  {
    this.DetailID = detailID;
    this.Descr = descr;
    this.Value = value;
    this.ControlType = controlType;
    this.ComboValuesCollection = (ICollection<KeyValuePair<string, string>>) new Dictionary<string, string>();
  }

  public PluginSettingDetail()
  {
  }
}
