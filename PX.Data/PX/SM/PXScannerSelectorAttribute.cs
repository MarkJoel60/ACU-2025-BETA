// Decompiled with JetBrains decompiler
// Type: PX.SM.PXScannerSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

/// <summary>Searches for scanners available for the current user</summary>
[PXDBGuid(false)]
[PXUIField(DisplayName = "Scanner", Visibility = PXUIVisibility.SelectorVisible, FieldClass = "DeviceHub")]
[PXRestrictor(typeof (Where<SMScanner.isActive, Equal<PX.Data.True>>), "Scanner is inactive.", new System.Type[] {})]
[PXSelector(typeof (Search<SMScanner.scannerID>), DescriptionField = typeof (SMScanner.scannerName), SelectorMode = PXSelectorMode.DisplayModeText)]
public class PXScannerSelectorAttribute : PXAggregateAttribute
{
  public PXDBGuidAttribute DataType => this.GetAttribute<PXDBGuidAttribute>();

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
