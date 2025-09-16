// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.OrderSegment
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class OrderSegment
{
  public SQLExpression expr_;
  public bool ascending_;

  internal OrderSegment(OrderSegment other)
  {
    this.expr_ = other.expr_?.Duplicate();
    this.ascending_ = other.ascending_;
  }

  public OrderSegment()
  {
    this.expr_ = (SQLExpression) null;
    this.ascending_ = true;
  }

  public override string ToString()
  {
    return this.expr_?.ToString() + (this.ascending_ ? " ASC" : " DESC");
  }

  public OrderSegment(SQLExpression e, bool ascending = true)
  {
    this.expr_ = e;
    this.ascending_ = ascending;
  }

  public virtual OrderSegment Duplicate() => new OrderSegment(this);
}
