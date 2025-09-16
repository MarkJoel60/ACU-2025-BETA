// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldName
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXFieldName : PXDataValue
{
  public SQLExpression FieldExpr;

  public PXFieldName(string field, SQLExpression expr = null)
    : base(PXDbType.DirectExpression, (object) field)
  {
    this.FieldExpr = expr;
  }

  public static string Placeholder => typeof (FieldNameParam).FullName;
}
