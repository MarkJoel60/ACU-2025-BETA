// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.DBRankAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class DBRankAttribute : PXDBIntAttribute
{
  protected virtual void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.Expr = (SQLExpression) new Column("Rank");
    e.ExcludeFromInsertUpdate();
  }
}
