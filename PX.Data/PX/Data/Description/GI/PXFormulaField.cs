// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXFormulaField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXFormulaField : PXExtField
{
  public IPXValue Value;

  public override object Clone()
  {
    PXFormulaField pxFormulaField = new PXFormulaField();
    pxFormulaField.Name = this.Name;
    pxFormulaField.Table = this.Table == null ? (PXTable) null : (PXTable) this.Table.Clone();
    pxFormulaField.Function = this.Function;
    pxFormulaField.Value = this.Value;
    return (object) pxFormulaField;
  }
}
