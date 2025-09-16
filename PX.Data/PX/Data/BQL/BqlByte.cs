// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlByte
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable enable
namespace PX.Data.BQL;

/// <summary>
/// Allows to define a BqlField, a BqlConstant, or a BqlOperand and also use an Argument of <see cref="T:PX.Data.BQL.IBqlByte" /> type, that corresponds to SQL TINYINT.
/// </summary>
public abstract class BqlByte : BqlType<IBqlByte, byte>
{
  private BqlByte()
  {
  }
}
