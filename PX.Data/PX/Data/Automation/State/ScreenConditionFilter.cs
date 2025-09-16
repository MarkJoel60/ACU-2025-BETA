// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.ScreenConditionFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class ScreenConditionFilter
{
  public readonly string FieldName;
  public readonly int Condition;
  public readonly string Value;
  public readonly string Value2;
  public readonly int OpenBrackets;
  public readonly int CloseBrackets;
  public readonly int Operator;
  public readonly bool IsExpression;

  public ScreenConditionFilter(
    string fieldName,
    int condition,
    string value,
    string value2,
    int openBrackets,
    int closeBrackets,
    int oper,
    bool isExpression = false)
  {
    this.FieldName = fieldName;
    this.Condition = condition;
    this.Value = value;
    this.Value2 = value2;
    this.OpenBrackets = openBrackets;
    this.CloseBrackets = closeBrackets;
    this.Operator = oper;
    this.IsExpression = isExpression;
  }
}
