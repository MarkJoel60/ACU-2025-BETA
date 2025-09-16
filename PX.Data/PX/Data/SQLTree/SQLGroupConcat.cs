// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLGroupConcat
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLGroupConcat : SQLExpression
{
  public SQLGroupConcat(List<SQLExpression> arguments, List<OrderSegment> orderBy = null)
    : base()
  {
    this.Arguments = arguments ?? throw new ArgumentNullException(nameof (arguments));
    this.OrderBy = orderBy ?? new List<OrderSegment>();
  }

  protected SQLGroupConcat(SQLGroupConcat other)
    : base((SQLExpression) other)
  {
    this.Distinct = other.Distinct;
    this.Arguments = other.Arguments;
    this.OrderBy = other.OrderBy;
    this.Separator = other.Separator;
  }

  public bool Distinct { get; set; }

  public List<SQLExpression> Arguments { get; }

  public List<OrderSegment> OrderBy { get; }

  public string Separator { get; set; }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLGroupConcat(this);

  public override bool Equals(SQLExpression other)
  {
    bool flag = base.Equals(other);
    if (!flag)
      return flag;
    SQLGroupConcat sqlGroupConcat = (SQLGroupConcat) other;
    return this.Distinct == sqlGroupConcat.Distinct && !(this.Separator != sqlGroupConcat.Separator) && this.Arguments.SequenceEqual<SQLExpression>((IEnumerable<SQLExpression>) sqlGroupConcat.Arguments) && this.OrderBy.SequenceEqual<OrderSegment>((IEnumerable<OrderSegment>) sqlGroupConcat.OrderBy);
  }

  internal override PXDbType GetDBType()
  {
    return SQLExpression.GetMaxByPrecedence(this.Arguments.Select<SQLExpression, PXDbType?>((Func<SQLExpression, PXDbType?>) (a => new PXDbType?(a.GetDBType()))).ToArray<PXDbType?>());
  }

  public override string ToString() => this.SQLQuery((Connection) new MSSQLConnection()).ToString();

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
