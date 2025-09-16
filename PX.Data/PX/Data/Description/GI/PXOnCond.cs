// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXOnCond
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXOnCond : ICloneable
{
  public int OpenBrackets;
  public IPXValue FirstField;
  public Condition Cond;
  public IPXValue SecondField;
  public int CloseBrackets;
  public Operation Op;

  public object Clone()
  {
    return (object) new PXOnCond()
    {
      OpenBrackets = this.OpenBrackets,
      FirstField = this.FirstField,
      Cond = this.Cond,
      SecondField = this.SecondField,
      CloseBrackets = this.CloseBrackets,
      Op = this.Op
    };
  }
}
