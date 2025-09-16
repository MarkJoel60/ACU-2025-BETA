// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.DBCombinedSearchStringAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common.Scopes;
using PX.Objects.IN.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class DBCombinedSearchStringAttribute(Type operand) : PXDBCalcedAttribute(operand, typeof (string))
{
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    base.CommandPreparing(sender, e);
    if (!FlaggedModeScopeBase<InventoryFullTextSearchSelectScope>.IsActive)
      e.Expr = (SQLExpression) new SQLConst((object) null);
    e.ExcludeFromInsertUpdate();
  }
}
