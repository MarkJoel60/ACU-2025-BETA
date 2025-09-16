// Decompiled with JetBrains decompiler
// Type: PX.Data.In`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Checks if the preceding operand matches any value in the array returned by the operand that should be the Required or Optional BQL parameter with a specified field name.
/// The condition is true if the preceding operand is equal to a value from the array. Equivalent to the SQL operator IN.
/// The In operator is used to replace multiple OR conditions in a BQL statement.</summary>
public class In<Operand> : InBase<Operand> where Operand : IBqlParameter
{
  protected override bool IsNegative => false;

  [Obsolete]
  protected override string SqlOperator => " IN ";
}
