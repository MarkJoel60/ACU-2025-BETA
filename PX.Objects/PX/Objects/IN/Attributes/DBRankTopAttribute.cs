// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.DBRankTopAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common.Scopes;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class DBRankTopAttribute : PXDBIntAttribute
{
  protected virtual void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    if (!FlaggedModeScopeBase<InventoryFullTextSearchSelectScope>.IsActive)
      e.Expr = (SQLExpression) new SQLConst((object) null);
    e.ExcludeFromInsertUpdate();
  }
}
