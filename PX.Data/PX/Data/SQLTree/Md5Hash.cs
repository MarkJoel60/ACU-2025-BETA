// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Md5Hash
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

public class Md5Hash : SQLExpression
{
  public Md5Hash(params Column[] columns)
    : base()
  {
    this.oper_ = SQLExpression.Operation.MD5;
    foreach (Column column in columns)
    {
      SQLExpression lexpr = this.lexpr_;
      this.lexpr_ = (lexpr != null ? lexpr.Concat((SQLExpression) new SQLSmartConvert(typeof (string), (SQLExpression) column)) : (SQLExpression) null) ?? (SQLExpression) new SQLSmartConvert(typeof (string), (SQLExpression) column);
    }
  }

  protected Md5Hash(Md5Hash other)
    : base((SQLExpression) other)
  {
  }

  public override SQLExpression Duplicate() => (SQLExpression) new Md5Hash(this);

  internal override PXDbType GetDBType() => PXDbType.Binary;

  public override string ToString() => $"HASHBYTES('MD5', {this.lexpr_})";

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
