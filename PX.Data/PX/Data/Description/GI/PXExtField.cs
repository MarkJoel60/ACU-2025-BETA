// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXExtField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXExtField : ICloneable, IEquatable<PXExtField>
{
  public string Name;
  public PXTable Table;
  public AggregateFunction? Function;

  public virtual object Clone()
  {
    return (object) new PXExtField()
    {
      Name = this.Name,
      Table = (this.Table == null ? (PXTable) null : (PXTable) this.Table.Clone()),
      Function = this.Function
    };
  }

  public bool Equals(PXExtField other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    if (string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase))
    {
      AggregateFunction? function1 = this.Function;
      AggregateFunction? function2 = other.Function;
      if (function1.GetValueOrDefault() == function2.GetValueOrDefault() & function1.HasValue == function2.HasValue)
      {
        if (this.Table == other.Table)
          return true;
        return this.Table != null && this.Table.Equals(other.Table);
      }
    }
    return false;
  }
}
