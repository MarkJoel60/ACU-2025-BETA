// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.MSSQLWriterVisitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.MsSql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal class MSSQLWriterVisitor : BaseSQLWriterVisitor
{
  private Dictionary<string, List<(StringBuilder joinText, bool wasUsed)>> _ftsJoins;

  public MSSQLWriterVisitor()
  {
    this.kwPrefix_ = "[";
    this.kwSuffix_ = "]";
  }

  public override StringBuilder Visit(XMLPathQuery q)
  {
    this._res.AppendLine();
    this._res.Append("(");
    this.Visit((Query) q);
    this._res.AppendLine().Append("FOR XML PATH");
    if (q.ElementName != null)
      this._res.Append($"('{q.ElementName}')");
    if (q.HasXsinil)
      this._res.Append(", ELEMENTS XSINIL");
    if (q.HasBinaryBase64)
      this._res.Append(", BINARY BASE64");
    if (q.HasRoot)
    {
      this._res.Append(", ROOT");
      if (q.RootName != null)
        this._res.Append($"('{q.RootName}')");
    }
    this._res.AppendLine();
    this._res.Append(") ");
    return this._res;
  }

  public override StringBuilder Visit(JoinedAttrQuery q)
  {
    this._res.Append("(SELECT ");
    if (q.valCol_ == "AttributeID")
      this._res.Append("[AttributeID]");
    else
      this._res.Append("[").Append(q.srcTable_).Append("].[").Append(q.valCol_).Append("]");
    this._res.Append(" as '@type', ");
    if (q.keyCol_ != null)
      this._res.Append("[").Append(q.keyCol_).Append("]");
    else
      this._res.Append("COALESCE(CONVERT(VARCHAR(30), ValueNumeric), CONVERT(VARCHAR(23), ValueDate, 121), ValueString, ValueText, N'')");
    this._res.Append(" as '*' FROM ");
    foreach (Joiner joiner in q.GetFrom())
      joiner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    this._res.Append(" WHERE ");
    q.GetWhere()?.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (q.GetOrder() != null && q.GetOrder().Any<OrderSegment>())
    {
      this._res.Append(Environment.NewLine + "ORDER BY ");
      bool flag = true;
      foreach (OrderSegment os in q.GetOrder())
      {
        if (flag)
          flag = false;
        else
          this._res.Append(", ");
        this.Visit(os);
      }
    }
    this._res.Append(" FOR XML PATH('v')) ");
    return this._res;
  }

  public override StringBuilder Visit(Query q)
  {
    List<SQLExpression> selection = q.GetSelection();
    // ISSUE: explicit non-virtual call
    if (selection != null && __nonvirtual (selection.Count) > 0)
    {
      this._res.Append("SELECT ");
      if (q.GetLimit() > 0 && q.GetOffset() == 0)
        this._res.Append($"TOP ({q.GetLimit()}) ");
      if (q.IsDistinct)
        this._res.Append("DISTINCT ");
      bool flag = true;
      foreach (SQLExpression sqlExpression in selection)
      {
        if (flag)
          flag = false;
        else
          this._res.Append(", ");
        sqlExpression.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        if (sqlExpression.Alias() != null)
          this._res.Append($" AS {this.kwPrefix_}{sqlExpression.Alias()}{this.kwSuffix_}");
      }
    }
    SQLExpression where = q.GetWhere();
    List<Joiner> from = q.GetFrom();
    bool flag1 = from != null && from.Any<Joiner>();
    if (flag1)
    {
      // ISSUE: explicit non-virtual call
      if (selection != null && __nonvirtual (selection.Count) > 0)
        this._res.Append(Environment.NewLine + "FROM ");
      if (where != null)
      {
        List<SQLExpression> expressionsOfType = where.GetExpressionsOfType(SQLExpression.Operation.CONTAINS);
        expressionsOfType.AddRange((IEnumerable<SQLExpression>) where.GetExpressionsOfType(SQLExpression.Operation.FREETEXT));
        if (this._ftsJoins == null)
          this._ftsJoins = new Dictionary<string, List<(StringBuilder, bool)>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        MSSQLWriterVisitor.GetFTSJoins(expressionsOfType.OfType<SQLFullTextSearch>(), this._ftsJoins);
      }
      foreach (Joiner joiner in from)
        joiner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    }
    if (where != null)
    {
      if (this._ftsJoins != null & flag1)
      {
        foreach ((StringBuilder, bool) tuple in this._ftsJoins.SelectMany<KeyValuePair<string, List<(StringBuilder, bool)>>, (StringBuilder, bool)>((Func<KeyValuePair<string, List<(StringBuilder, bool)>>, IEnumerable<(StringBuilder, bool)>>) (p => (IEnumerable<(StringBuilder, bool)>) p.Value)).Where<(StringBuilder, bool)>((Func<(StringBuilder, bool), bool>) (t => !t.wasUsed)))
          this._res.Append((object) tuple.Item1);
        this._ftsJoins = (Dictionary<string, List<(StringBuilder, bool)>>) null;
      }
      int length1 = this._res.Length;
      this._res.Append(Environment.NewLine + "WHERE ");
      int length2 = this._res.Length;
      where.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      int length3 = this._res.Length;
      if (length2 == length3)
        this._res.Remove(length1, this._res.Length - length1);
    }
    if (q.GetGroupBy() != null && q.GetGroupBy().Any<SQLExpression>())
    {
      this._res.Append(Environment.NewLine + "GROUP BY ");
      bool flag2 = true;
      foreach (SQLExpression sqlExpression in q.GetGroupBy())
      {
        if (flag2)
          flag2 = false;
        else
          this._res.Append(", ");
        sqlExpression.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      }
    }
    if (q.GetHaving() != null)
    {
      int length4 = this._res.Length;
      this._res.Append(Environment.NewLine + "HAVING ");
      int length5 = this._res.Length;
      q.GetHaving().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      int length6 = this._res.Length;
      if (length5 == length6)
        this._res.Remove(length4, this._res.Length - length4);
    }
    if (q.GetOrder() != null && q.GetOrder().Any<OrderSegment>())
    {
      this._res.Append(Environment.NewLine + "ORDER BY ");
      bool flag3 = true;
      foreach (OrderSegment os in q.GetOrder())
      {
        if (os.expr_ != null)
        {
          if (flag3)
            flag3 = false;
          else
            this._res.Append(", ");
          this.Visit(os);
        }
      }
    }
    int offset = q.GetOffset();
    if (offset > 0)
    {
      Func<long, string> func = (Func<long, string>) (i => i != 1L ? "ROWS" : "ROW");
      this._res.Append($"{Environment.NewLine} OFFSET ({offset}) {func((long) offset)}");
      int limit = q.GetLimit();
      if (limit > 0)
        this._res.Append($" FETCH NEXT ({limit}) {func((long) limit)} ONLY");
    }
    QueryHints hints = q.GetHints();
    if (hints != QueryHints.None)
    {
      List<string> values = new List<string>();
      foreach (string str in PXDatabase.Provider.SqlDialect.GetQueryHintsText(hints))
        values.Add(str);
      if (values.Count > 0)
        this._res.Append($" OPTION({string.Join(", ", (IEnumerable<string>) values)})");
    }
    foreach (Unioner unioner in q.GetUnion())
      unioner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    return this._res;
  }

  protected override void AppendJoinType(Joiner.JoinType jt, Joiner.JoinHint hint)
  {
    if (jt == Joiner.JoinType.INNER_JOIN && hint == Joiner.JoinHint.STRAIGHT_FORCED)
      this._res.Append(Environment.NewLine + "INNER LOOP JOIN ");
    else
      base.AppendJoinType(jt, hint);
  }

  private static void GetFTSJoins(
    IEnumerable<SQLFullTextSearch> fullTextPredicates,
    Dictionary<string, List<(StringBuilder joinText, bool wasUsed)>> ftsJoins)
  {
    foreach (SQLFullTextSearch fullTextPredicate in fullTextPredicates)
    {
      if (fullTextPredicate != null)
      {
        MSSQLWriterVisitor visitor = new MSSQLWriterVisitor();
        StringBuilder res = visitor._res;
        res.Append(Environment.NewLine + "INNER JOIN ");
        if (fullTextPredicate.Oper() == SQLExpression.Operation.FREETEXT)
          res.Append("FREETEXTTABLE (");
        else
          res.Append("CONTAINSTABLE (");
        if (fullTextPredicate.LExpr() is Column column)
        {
          string name = (fullTextPredicate.Table ?? (SimpleTable) column.Table()).Name;
          string str = column.Table().AliasOrName();
          res.Append($"{name}, [{column.Name}], ");
          fullTextPredicate.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) visitor);
          if (fullTextPredicate.Limit() != null)
          {
            res.Append(", ");
            fullTextPredicate.Limit().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) visitor);
          }
          res.Append($" ) AS [ftt_{str}] ON [ftt_{str}].[KEY]=");
          fullTextPredicate.KeyField().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) visitor);
          Table table = fullTextPredicate.KeyField().Table();
          if (table != null)
          {
            string key = table.AliasOrName();
            if (!ftsJoins.ContainsKey(key))
              ftsJoins[key] = new List<(StringBuilder, bool)>();
            ftsJoins[key].Add((res, false));
          }
        }
      }
    }
  }

  public override StringBuilder Visit(Joiner j)
  {
    base.Visit(j);
    if (this._ftsJoins != null)
    {
      string key = j.Table().AliasOrName();
      if (string.IsNullOrEmpty(key) || !this._ftsJoins.ContainsKey(key))
        return this._res;
      List<(StringBuilder joinText, bool wasUsed)> ftsJoin = this._ftsJoins[key];
      for (int index = 0; index < ftsJoin.Count; ++index)
      {
        StringBuilder joinText = ftsJoin[index].joinText;
        this._res.Append((object) joinText);
        ftsJoin[index] = (joinText, true);
      }
    }
    return this._res;
  }

  protected override StringBuilder SQLAggregateExpression(SQLExpression e)
  {
    string str = "";
    switch (e.Oper())
    {
      case SQLExpression.Operation.MAX:
        str = "MAX( ";
        break;
      case SQLExpression.Operation.MIN:
        str = "MIN( ";
        break;
      case SQLExpression.Operation.AVG:
        str = "AVG( ";
        break;
      case SQLExpression.Operation.SUM:
        str = "SUM( ";
        break;
      case SQLExpression.Operation.COUNT:
        str = "COUNT( ";
        break;
      case SQLExpression.Operation.COUNT_DISTINCT:
        str = "COUNT(DISTINCT ";
        break;
    }
    if (e.RExpr().GetDBTypeOrDefault() == PXDbType.Bit)
    {
      this._res.Append("CONVERT (BIT, " + str);
      if (e.RExpr() != null)
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append("+0))");
    }
    else
    {
      this._res.Append(str ?? "");
      if (e.RExpr() != null)
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      else if (e.Oper() == SQLExpression.Operation.COUNT)
        this._res.Append("*");
      this._res.Append(")");
    }
    return this._res;
  }

  public override StringBuilder Visit(SQLDateDiff e)
  {
    this._res.Append($"DATEDIFF({e.UOM()}, ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(")");
    return this._res;
  }

  public override StringBuilder Visit(SQLDatePart e)
  {
    this._res.Append($"DATEPART({e.UOM()}, ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(")");
    return this._res;
  }

  public override StringBuilder Visit(SQLDateAdd e)
  {
    this._res.Append($"DATEADD({e.DatePart}, ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(")");
    return this._res;
  }

  public override StringBuilder Visit(SQLConvert e)
  {
    int num = string.IsNullOrEmpty(e.MySqlCharset) ? 1 : 0;
    if (num != 0)
      this._res.Append($"CONVERT({this.GetTypeNameForConvert(e.TargetType)}, ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (num != 0)
      this._res.Append(")");
    return this._res;
  }

  public override StringBuilder Visit(SQLScalarFunction e)
  {
    return this.FormatFunction("dbo." + e.Name, e.LExpr());
  }

  public override StringBuilder Visit(SQLFullTextSearch e) => this._res;

  public override StringBuilder Visit(SQLRank e)
  {
    if (e.Field is Column field)
      this._res.Append($"[ftt_{field.Table()}].[Rank]");
    return this._res;
  }

  public override StringBuilder VisitConcat(SQLExpression e)
  {
    this.seqAsPlus(e);
    return this._res;
  }

  private void seqAsPlus(SQLExpression e)
  {
    if (e.LExpr() != null)
    {
      if (e.LExpr().Oper() == SQLExpression.Operation.SEQ)
        this.seqAsPlus(e.LExpr());
      else
        e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
    if (e.LExpr() != null && e.RExpr() != null)
      this._res.Append(" + ");
    if (e.RExpr() == null)
      return;
    if (e.RExpr().Oper() == SQLExpression.Operation.SEQ)
      this.seqAsPlus(e.RExpr());
    else
      e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
  }

  public override StringBuilder VisitConvertBinToInt(SQLExpression e)
  {
    SQLExpression sqlExpression = e.RExpr();
    return this.FormatFunction("SUBSTRING", e.LExpr(), sqlExpression.LExpr(), sqlExpression.RExpr());
  }

  public override StringBuilder VisitCastAsInteger(SQLExpression e)
  {
    this._res.Append("CAST( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(" AS INTEGER)");
  }

  public override StringBuilder VisitCastAsVarBinary(SQLExpression e)
  {
    this._res.Append("CAST( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(" AS VARBINARY(MAX))");
  }

  public override StringBuilder VisitBinaryLen(SQLExpression e)
  {
    return this.FormatFunction("DATALENGTH", e.RExpr());
  }

  public override StringBuilder VisitLen(SQLExpression e) => this.FormatFunction("LEN", e.RExpr());

  public override StringBuilder VisitIsNumeric(SQLExpression e)
  {
    return this.FormatFunction("ISNUMERIC", e.RExpr());
  }

  public override StringBuilder VisitTrim(SQLExpression e)
  {
    this._res.Append("LTRIM(RTRIM( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("))");
  }

  public override StringBuilder VisitIsNullFunc(SQLExpression e)
  {
    return this.FormatFunction("ISNULL", e.LExpr(), e.RExpr());
  }

  public override StringBuilder VisitDateNow(SQLExpression e) => this._res.Append("GETDATE()");

  public override StringBuilder VisitDateUtcNow(SQLExpression e)
  {
    return this._res.Append("GETUTCDATE()");
  }

  public override StringBuilder VisitToday(SQLExpression e)
  {
    return this._res.Append("CAST(GETDATE() as DATE)");
  }

  public override StringBuilder VisitTodayUtc(SQLExpression e)
  {
    return this._res.Append("CAST(GETUTCDATE() as DATE)");
  }

  public override StringBuilder VisitGetTime(SQLExpression e)
  {
    this._res.Append("CAST( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(" as TIME)");
  }

  public override StringBuilder VisitCharIndex(SQLExpression e)
  {
    return this.FormatFunction("CHARINDEX", e.RExpr(), e.LExpr());
  }

  public override StringBuilder VisitRepeat(SQLExpression e)
  {
    return this.FormatFunction("REPLICATE", e.LExpr(), e.RExpr());
  }

  public override StringBuilder VisitPower(SQLExpression e)
  {
    return this.FormatFunction("POWER", e.LExpr(), e.RExpr());
  }

  public override StringBuilder VisitMinusDate(SQLExpression e)
  {
    this._res.Append("DATEADD(dd, -( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append("), ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(")");
  }

  public override StringBuilder VisitASCII(SQLExpression e)
  {
    if (e.IsEmbraced())
      this._res.Append("( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  public override StringBuilder VisitLike(SQLExpression e, bool negative = false)
  {
    if (e.IsEmbraced())
      this._res.Append("( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (negative)
      this._res.Append(" NOT");
    this._res.Append(" LIKE ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  private string GetCollation(StringComparison comparison)
  {
    switch (comparison)
    {
      case StringComparison.CurrentCulture:
        return PXLocalesProvider.CollationProvider.Collation.CS ?? "Latin1_General_CS_AS";
      case StringComparison.CurrentCultureIgnoreCase:
        return PXLocalesProvider.CollationProvider.Collation.CI ?? "Latin1_General_CI_AS";
      case StringComparison.InvariantCulture:
        return "Latin1_General_CS_AS";
      case StringComparison.InvariantCultureIgnoreCase:
        return "Latin1_General_CI_AS";
      case StringComparison.Ordinal:
        return "Latin1_General_BIN";
      case StringComparison.OrdinalIgnoreCase:
        return "Latin1_General_BIN";
      default:
        return (string) null;
    }
  }

  private string GetCollation(string ci, bool? ignoreCase)
  {
    (string CS, string CI, string CSL, string CIL) tuple = PXLocalesProvider.CollationProvider.CollationByCulture(ci);
    bool? nullable = ignoreCase;
    bool flag = true;
    return !(nullable.GetValueOrDefault() == flag & nullable.HasValue) ? tuple.CS ?? "Latin1_General_CS_AS" : tuple.CI ?? "Latin1_General_CI_AS";
  }

  public override StringBuilder VisitEqual(SQLExpression e, StringComparison comparison)
  {
    SQLExpression sqlExpression1 = e.LExpr();
    SQLExpression sqlExpression2 = e.RExpr();
    int num = comparison == StringComparison.OrdinalIgnoreCase ? 1 : 0;
    if (e.IsEmbraced())
      this._res.Append("( ");
    if (num != 0)
    {
      this._res.Append("LOWER(");
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(")");
    }
    else
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" = ");
    if (num != 0)
    {
      this._res.Append("LOWER(");
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(")");
    }
    else
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    string collation = this.GetCollation(comparison);
    if (string.IsNullOrEmpty(collation))
      throw new PXNotSupportedException($"Method String.Equal is not supported for {comparison} comparer");
    this._res.Append(" COLLATE " + collation);
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  public override StringBuilder VisitCompare(SQLExpression e, StringComparison comparison)
  {
    SQLExpression stringExpression1 = e.LExpr();
    SQLExpression stringExpression2 = e.RExpr();
    bool needCI = comparison == StringComparison.OrdinalIgnoreCase;
    string collation = this.GetCollation(comparison);
    this._res.Append("(CASE WHEN ");
    if (stringExpression1 is Column && stringExpression2 is SQLConst sqlConst1 && sqlConst1.GetValue() == null)
    {
      stringExpression1.IsNull().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" THEN 0 END)");
      return this._res;
    }
    if (stringExpression2 is Column && stringExpression1 is SQLConst sqlConst2 && sqlConst2.GetValue() == null)
    {
      stringExpression2.IsNull().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" THEN 0 END)");
      return this._res;
    }
    StringExpressionAppend(stringExpression1);
    this._res.Append(" = ");
    StringExpressionAppend(stringExpression2);
    if (!string.IsNullOrEmpty(collation))
      this._res.Append(" COLLATE " + collation);
    this._res.Append(" THEN 0 WHEN ");
    StringExpressionAppend(stringExpression1);
    this._res.Append(" < ");
    StringExpressionAppend(stringExpression2);
    if (!string.IsNullOrEmpty(collation))
      this._res.Append(" COLLATE " + collation);
    this._res.Append(" THEN -1 WHEN ");
    StringExpressionAppend(stringExpression1);
    this._res.Append(" > ");
    StringExpressionAppend(stringExpression2);
    if (!string.IsNullOrEmpty(collation))
      this._res.Append(" COLLATE " + collation);
    this._res.Append(" THEN 1 ELSE NULL END)");
    return this._res;

    void StringExpressionAppend(SQLExpression stringExpression)
    {
      if (needCI)
      {
        this._res.Append("LOWER(");
        stringExpression.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append(")");
      }
      else
        stringExpression.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
  }

  public override StringBuilder VisitCompareWithCulture(SQLExpression e)
  {
    SQLExpression sqlExpression1 = e.LExpr();
    SQLExpression sqlExpression2 = e.RExpr().LExpr();
    bool? ignoreCase = (bool?) (e.RExpr().RExpr().LExpr() is SQLConst sqlConst1 ? sqlConst1.GetValue() : (object) null);
    object obj = e.RExpr().RExpr().RExpr() is SQLConst sqlConst2 ? sqlConst2.GetValue() : (object) null;
    string str1;
    if (obj == null || !(obj is string ci))
    {
      bool? nullable = ignoreCase;
      bool flag = true;
      str1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? this.GetCollation(StringComparison.CurrentCultureIgnoreCase) : this.GetCollation(StringComparison.CurrentCulture);
    }
    else
      str1 = this.GetCollation(ci, ignoreCase);
    string str2 = str1;
    this._res.Append("(CASE WHEN ");
    sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" = ");
    sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (!string.IsNullOrEmpty(str2))
      this._res.Append(" COLLATE " + str2);
    this._res.Append(" THEN 0 WHEN ");
    sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" < ");
    sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (!string.IsNullOrEmpty(str2))
      this._res.Append(" COLLATE " + str2);
    this._res.Append(" THEN -1 ELSE 1 END)");
    return this._res;
  }

  public override StringBuilder VisitToBase64(SQLExpression e)
  {
    Query query = new XMLPathQuery()
    {
      ElementName = string.Empty,
      HasBinaryBase64 = true
    }.Select(e.RExpr());
    this._res.Append("cast(");
    query.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    return this._res.Append(" as xml).value('.', 'nvarchar(max)')");
  }

  public override bool TryPlusSpecialCase(SQLExpression e)
  {
    if (e.GetDBTypeOrDefault().IsDateTime())
    {
      if (e.LExpr().GetDBTypeOrDefault().IsDateTime())
      {
        this._res.Append("DATEADD(dd, ");
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append(", ");
        e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append(")");
        return true;
      }
      if (e.RExpr().GetDBTypeOrDefault().IsDateTime())
      {
        this._res.Append("DATEADD(dd, ");
        e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append(", ");
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append(")");
        return true;
      }
    }
    return false;
  }

  public override StringBuilder Visit(Md5Hash exp)
  {
    this._res.Append("CONVERT(VARCHAR(32), HASHBYTES").Append("('MD5', ");
    exp.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("), 2)");
  }

  public override StringBuilder Visit(SQLAggConcat exp)
  {
    this._res.Append("STRING_AGG( CONVERT( NVARCHAR (MAX), ");
    exp.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append($" ), '{exp.GetSeparator()}' )");
  }

  public override StringBuilder VisitConstValue(object value, PXDbType type)
  {
    if (value == null)
      return this._res.Append("NULL");
    System.Type type1 = value.GetType();
    if (type1 == typeof (string))
    {
      string str = (string) value;
      return this._res.Append(((SqlScripterBase) PointMsSqlServer.GenericScripter).quoteLiteral(str, new bool?(MSSQLWriterVisitor.IsUnicode(str))));
    }
    if (type1 == typeof (bool))
      return this._res.Append($"CONVERT (BIT, {((bool) value ? 1 : 0)})");
    if (type1 == typeof (Decimal))
      return this._res.Append(((Decimal) value).ToString(".0########", (IFormatProvider) CultureInfo.InvariantCulture));
    if (type1 == typeof (double))
      return this._res.Append("CONVERT(FLOAT, ").Append(((double) value).ToString(".0########", (IFormatProvider) CultureInfo.InvariantCulture)).Append(")");
    if (type1 == typeof (float))
      return this._res.Append("CONVERT(FLOAT, ").Append(((float) value).ToString(".0########", (IFormatProvider) CultureInfo.InvariantCulture)).Append(")");
    if (type1 == typeof (short))
      return this._res.Append($"CONVERT(SMALLINT, {(short) value:g})");
    if (type1 == typeof (long))
      return this._res.Append($"CONVERT(BIGINT, {(long) value:g})");
    if (type1 == typeof (Guid))
      return this._res.Append($"CONVERT(UNIQUEIDENTIFIER, '{value}')");
    if (type1 == typeof (System.DateTime))
    {
      System.DateTime dateTime = (System.DateTime) value;
      if (dateTime.Year < 1753)
        dateTime = dateTime.AddYears(1899);
      return this._res.AppendFormat("CONVERT(DATETIME, '{0:0000}{1:00}{2:00} {3}:{4}:{5}.{6:000}')", (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute, (object) dateTime.Second, (object) dateTime.Millisecond);
    }
    if (type1 == typeof (byte[]))
    {
      byte[] source = (byte[]) value;
      return this._res.Append("0x").Append(((IEnumerable<byte>) source).Any<byte>() ? BitConverter.ToString(source).Replace("-", "") : "0");
    }
    return type1 == typeof (PXFieldName) ? this._res.Append(((PXDataValue) value).Value) : base.VisitConstValue(value, type);
  }

  public override StringBuilder Visit(SQLGroupConcat exp) => throw new NotSupportedException();

  private static bool IsUnicode(string str)
  {
    return !string.IsNullOrEmpty(str) && str.Any<char>((Func<char, bool>) (c => c > 'ÿ'));
  }
}
