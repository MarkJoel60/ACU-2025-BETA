// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldOrder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataFieldOrder : PXDataField
{
  public readonly bool IsDesc;

  public PXDataFieldOrder(SQLExpression fieldName, bool isDesc)
    : base(fieldName)
  {
    this.IsDesc = isDesc;
  }

  public PXDataFieldOrder(SQLExpression fieldName)
    : this(fieldName, false)
  {
  }

  public PXDataFieldOrder(string fieldName, bool isDesc)
    : base(fieldName)
  {
    this.IsDesc = isDesc;
  }

  public PXDataFieldOrder(string fieldName)
    : this(fieldName, false)
  {
  }

  public PXDataFieldOrder(string fieldName, string tableAlias, bool isDesc)
    : base(fieldName, tableAlias)
  {
    this.IsDesc = isDesc;
  }

  public PXDataFieldOrder(string fieldName, string tableAlias)
    : this(fieldName, tableAlias, false)
  {
  }

  internal override PXDataField copyAndRename(string newName)
  {
    return (PXDataField) new PXDataFieldOrder(!(this.Expression is Column expression) || !(expression.Table() is SimpleTable simpleTable) ? (SQLExpression) new Column(newName) : (SQLExpression) new Column(newName, simpleTable.Name), this.IsDesc);
  }
}
