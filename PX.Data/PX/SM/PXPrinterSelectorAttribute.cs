// Decompiled with JetBrains decompiler
// Type: PX.SM.PXPrinterSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

/// <summary>Searches for printers available for the current user</summary>
[PXDBGuid(false)]
[PXUIField(DisplayName = "Printer", FieldClass = "DeviceHub")]
[PXRestrictor(typeof (Where<SMPrinter.isActive, Equal<PX.Data.True>>), "Printer is inactive.", new System.Type[] {})]
[PXSelector(typeof (Search<SMPrinter.printerID, Where<PX.Data.Match<Current<AccessInfo.userName>>>>), DescriptionField = typeof (SMPrinter.printerName), SelectorMode = PXSelectorMode.DisplayModeText)]
public class PXPrinterSelectorAttribute : PXAggregateAttribute
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
