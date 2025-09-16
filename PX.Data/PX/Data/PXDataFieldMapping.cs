// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldMapping
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataFieldMapping : PXDataField
{
  public readonly string PropertyName;

  public PXDataFieldMapping(SQLExpression fieldName, string propertyName)
    : base(fieldName)
  {
    this.PropertyName = propertyName;
  }

  public PXDataFieldMapping(string fieldName)
    : this((SQLExpression) new Column(fieldName), fieldName)
  {
  }
}
