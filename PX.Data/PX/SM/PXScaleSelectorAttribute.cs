// Decompiled with JetBrains decompiler
// Type: PX.SM.PXScaleSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

/// <summary>Searches for scales available for the current user</summary>
[PXDBGuid(false)]
[PXUIField(DisplayName = "Scales", FieldClass = "DeviceHub")]
[PXSelector(typeof (SMScale.scaleDeviceID), DescriptionField = typeof (SMScale.scaleID), SelectorMode = PXSelectorMode.DisplayModeText)]
public class PXScaleSelectorAttribute : PXAggregateAttribute
{
  public PXDBStringAttribute DataType => this.GetAttribute<PXDBStringAttribute>();

  public PXUIFieldAttribute UIField => this.GetAttribute<PXUIFieldAttribute>();

  public PXSelectorAttribute Selector => this.GetAttribute<PXSelectorAttribute>();

  public System.Type BqlField
  {
    get => this.DataType.BqlField;
    set
    {
      this.DataType.BqlField = value;
      this.BqlTable = this.DataType.BqlTable;
    }
  }

  public PXUIVisibility Visibility
  {
    get => this.UIField.Visibility;
    set => this.UIField.Visibility = value;
  }

  public string DisplayName
  {
    get => this.UIField.DisplayName;
    set => this.UIField.DisplayName = value;
  }
}
