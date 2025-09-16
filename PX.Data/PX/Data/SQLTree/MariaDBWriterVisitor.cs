// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.MariaDBWriterVisitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal class MariaDBWriterVisitor : MySQLWriterVisitor
{
  public override StringBuilder VisitBinaryLen(SQLExpression e)
  {
    this._res.Append("CAST( ");
    this.FormatFunction("LENGTH", e.RExpr());
    return this._res.Append(" AS SIGNED INTEGER)");
  }
}
