// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlInt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable enable
namespace PX.Data.BQL;

/// <summary>
/// Allows to define a BqlField, a BqlConstant, or a BqlOperand and also use an Argument of <see cref="T:PX.Data.BQL.IBqlInt" /> type, that corresponds to SQL INT.
/// </summary>
public abstract class BqlInt : BqlType<IBqlInt, int>
{
  private BqlInt()
  {
  }
}
