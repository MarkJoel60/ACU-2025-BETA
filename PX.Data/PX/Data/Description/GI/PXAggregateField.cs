// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXAggregateField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXAggregateField : PXExtField, IEquatable<PXAggregateField>
{
  public string Alias;

  public override object Clone()
  {
    PXAggregateField pxAggregateField = new PXAggregateField();
    pxAggregateField.Name = this.Name;
    pxAggregateField.Table = (PXTable) this.Table?.Clone();
    pxAggregateField.Function = this.Function;
    pxAggregateField.Alias = this.Alias;
    return (object) pxAggregateField;
  }

  public bool Equals(PXAggregateField other)
  {
    return this.Equals((PXExtField) other) && string.Equals(this.Alias, other?.Alias, StringComparison.OrdinalIgnoreCase);
  }
}
