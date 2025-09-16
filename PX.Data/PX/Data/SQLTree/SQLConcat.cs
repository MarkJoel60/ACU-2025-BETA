// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLConcat
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class SQLConcat : SQLExpression, ISQLDBTypedExpression
{
  private PXDbType type_ = PXDbType.Unspecified;

  protected SQLConcat(SQLConcat other)
    : base((SQLExpression) other)
  {
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLConcat(this);

  public SQLConcat(SQLExpression seq)
    : base()
  {
    this.rexpr_ = seq;
    this.oper_ = SQLExpression.Operation.CONCAT;
  }

  internal override PXDbType GetDBType()
  {
    return this.type_ == PXDbType.Unspecified ? base.GetDBType() : this.type_;
  }

  public void SetDBType(PXDbType type) => this.type_ = type;
}
