// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.NoteIdExpression
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

internal class NoteIdExpression : SQLExpression
{
  internal readonly SQLExpression MainExpr;

  public NoteIdExpression(SQLExpression mainExpr, bool popupTextEnabled)
    : this(mainExpr, popupTextEnabled ? 4 : 3)
  {
  }

  public NoteIdExpression(SQLExpression mainExpr, int nullsCount)
    : base()
  {
    this.MainExpr = mainExpr;
    this.lexpr_ = mainExpr;
    for (int index = 0; index < nullsCount; ++index)
      this.lexpr_ = this.lexpr_.Seq(SQLExpression.Null());
  }

  protected NoteIdExpression(NoteIdExpression other)
    : base((SQLExpression) other)
  {
    this.IgnoreNulls = other.IgnoreNulls;
    this.MainExpr = other.MainExpr;
  }

  public bool IgnoreNulls { get; set; }

  public override SQLExpression Duplicate() => (SQLExpression) new NoteIdExpression(this);

  public override bool Equals(SQLExpression other)
  {
    bool flag = base.Equals(other);
    if (!flag)
      return flag;
    NoteIdExpression noteIdExpression = (NoteIdExpression) other;
    return this.IgnoreNulls == noteIdExpression.IgnoreNulls && this.MainExpr.Equals(noteIdExpression.MainExpr);
  }

  internal override PXDbType GetDBType()
  {
    return !this.IgnoreNulls ? PXDbType.Unspecified : this.MainExpr.GetDBType();
  }

  public override string ToString()
  {
    return !this.IgnoreNulls ? this.lexpr_.ToString() : this.MainExpr.ToString();
  }

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
