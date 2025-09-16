// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ValidateVisitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

internal class ValidateVisitor : ISQLQueryVisitor<bool>
{
  private readonly EvaluateVisitor _expressionVisitor;

  public ValidateVisitor(PXCache cache, object item, object[] pars)
  {
    this._expressionVisitor = new EvaluateVisitor(cache, item, pars);
  }

  public bool Visit(Table table) => true;

  public bool Visit(SimpleTable table) => true;

  public bool Visit(Query table)
  {
    bool flag1 = true;
    foreach (Joiner joiner in table.GetFrom())
    {
      flag1 &= joiner.Accept<bool>((ISQLQueryVisitor<bool>) this);
      if (!flag1)
        return false;
    }
    if (table.GetWhere() != null)
    {
      bool? nullable = table.GetWhere().Accept<object>((ISQLExpressionVisitor<object>) this._expressionVisitor) as bool?;
      bool flag2 = true;
      if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) && !this._expressionVisitor.UnknownValue)
        return false;
    }
    return true;
  }

  public bool Visit(JoinedAttrQuery table) => this.Visit((Query) table);

  public bool Visit(XMLPathQuery table) => this.Visit((Query) table);

  public bool Visit(Joiner joiner)
  {
    if (joiner.getOn() != null)
    {
      bool? nullable = joiner.getOn().Accept<object>((ISQLExpressionVisitor<object>) this._expressionVisitor) as bool?;
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) && !this._expressionVisitor.UnknownValue)
        return false;
    }
    return true;
  }

  public bool Visit(Unioner unioner) => true;
}
