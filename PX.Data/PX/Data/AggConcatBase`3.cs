// Decompiled with JetBrains decompiler
// Type: PX.Data.AggConcatBase`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public abstract class AggConcatBase<Field, Separator, NextAggregate> : 
  AggregatedFnBase<Field, NextAggregate>,
  IAggConcat
  where Field : IBqlField
  where Separator : IConstant<string>, IBqlOperand, new()
  where NextAggregate : IBqlFunction, new()
{
  private const int SeparatorMaxLength = 10;

  public IConstant<string> GetSeparator()
  {
    Separator separator = new Separator();
    if (separator.Value.Length > 10)
      throw new ArgumentOutOfRangeException(nameof (Separator), (object) separator.Value, $"The separator for the AggConcat function must not be longer than {10} characters.");
    return (IConstant<string>) separator;
  }

  /// <exclude />
  public override string GetFunction() => "CONCAT";
}
