// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXWhereCond
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Description.GI;

public class PXWhereCond : IEquatable<PXWhereCond>, ICloneable
{
  public int OpenBrackets;
  public PXTable Table;
  public IPXValue DataField;
  public Condition Cond;
  public IPXValue Value1;
  public IPXValue Value2;
  public int CloseBrackets;
  public Operation Op;
  public bool UseExt;

  public bool Equals(PXWhereCond other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    if (this.OpenBrackets != other.OpenBrackets || this.CloseBrackets != other.CloseBrackets || this.Cond != other.Cond || this.Op != other.Op || this.UseExt != other.UseExt || this.Table != other.Table && (this.Table == null || !this.Table.Equals(other.Table)) || this.DataField != other.DataField && (this.DataField == null || !this.DataField.Equals(other.DataField)) || this.Value1 != other.Value1 && (this.Value1 == null || !this.Value1.Equals(other.Value1)))
      return false;
    if (this.Value2 == other.Value2)
      return true;
    return this.Value2 != null && this.Value2.Equals(other.Value2);
  }

  public object Clone()
  {
    return (object) new PXWhereCond()
    {
      OpenBrackets = this.OpenBrackets,
      Table = (this.Table == null ? (PXTable) null : (PXTable) this.Table.Clone()),
      DataField = this.DataField,
      Cond = this.Cond,
      Value1 = this.Value1,
      Value2 = this.Value2,
      CloseBrackets = this.CloseBrackets,
      Op = this.Op,
      UseExt = this.UseExt
    };
  }
}
