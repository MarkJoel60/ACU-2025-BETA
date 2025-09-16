// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAccumulatorRestriction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class PXAccumulatorRestriction
{
  public readonly string FieldName;
  public readonly PXComp Comp;
  public readonly object Value;

  public PXAccumulatorRestriction(string fieldName, PXComp comp, object value)
  {
    this.FieldName = fieldName;
    this.Comp = comp;
    this.Value = value;
  }
}
