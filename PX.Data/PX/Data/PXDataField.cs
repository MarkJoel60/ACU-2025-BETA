// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataField
{
  public readonly SQLExpression Expression;

  public PXDataField(SQLExpression expression) => this.Expression = expression;

  public PXDataField(string fieldName)
    : this(fieldName, (string) null)
  {
  }

  public PXDataField(string fieldName, string tableAlias)
  {
    if (string.IsNullOrEmpty(fieldName))
      throw new PXArgumentException(nameof (fieldName), "The argument cannot be null.");
    this.Expression = string.IsNullOrEmpty(tableAlias) ? (SQLExpression) new Column(fieldName) : (SQLExpression) new Column(fieldName, tableAlias);
  }

  public override string ToString() => this.Expression.ToString();

  internal virtual PXDataField copyAndRename(string newName)
  {
    return new PXDataField(!(this.Expression is Column expression) || !(expression.Table() is SimpleTable simpleTable) ? (SQLExpression) new Column(newName) : (SQLExpression) new Column(newName, simpleTable.Name));
  }
}
