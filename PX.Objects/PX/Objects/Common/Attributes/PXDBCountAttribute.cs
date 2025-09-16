// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.PXDBCountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class PXDBCountAttribute : PXDBIntAttribute
{
  protected Type DistinctByField { get; }

  public PXDBCountAttribute()
  {
  }

  public PXDBCountAttribute(Type distinctByField) => this.DistinctByField = distinctByField;

  protected virtual void PrepareFieldName(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    ((PXDBFieldAttribute) this).PrepareFieldName(dbFieldName, e);
    if (this.DistinctByField == (Type) null)
      e.Expr = SQLExpression.Count();
    else
      e.Expr = SQLExpression.CountDistinct((SQLExpression) new Column(this.DistinctByField.Name, (Table) new SimpleTable(BqlCommand.GetItemType(this.DistinctByField).Name, (string) null), (PXDbType) 100));
  }
}
