// Decompiled with JetBrains decompiler
// Type: PX.Data.In2`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Checks if the preceding operand matches any value in the results of the Search-based statement that is defined by the operand. The condition is true if the preceding operand is equal to a value from the result set. Equivalent to SQL statement <tt>IN(SELECT ... FROM ...)</tt>.</summary>
public class In2<Operand> : InBase<Operand> where Operand : IBqlSearch, IBqlCreator
{
  protected override bool IsNegative => false;

  [Obsolete]
  protected override string SqlOperator => " IN ";
}
