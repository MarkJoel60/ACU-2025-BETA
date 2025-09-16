// Decompiled with JetBrains decompiler
// Type: PX.Data.NotIn`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Checks if the preceding operand does not match any value in the array returned by the operand that should be the Required or Optional BQL parameter with a specified field name. The condition is true if the array does not contain a value that is equal to the preceding operand. Equivalent to SQL operator NOT IN.</summary>
public class NotIn<Operand> : InBase<Operand> where Operand : IBqlParameter
{
  protected override bool IsNegative => true;

  [Obsolete]
  protected override string SqlOperator => " NOT IN ";
}
