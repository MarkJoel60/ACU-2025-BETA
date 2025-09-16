// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.PgSQLWriterVisitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.PgSql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal class PgSQLWriterVisitor : BaseSQLWriterVisitor
{
  private Dictionary<string, List<(StringBuilder joinText, bool wasUsed)>> _ftsJoins;

  internal int PadSpacedLength => this.sqlDialect.DefaultPadSpaceLength;

  public PgSQLWriterVisitor()
  {
    this.kwPrefix_ = "\"";
    this.kwSuffix_ = "\"";
  }

  /// <inheritdoc />
  public override StringBuilder Visit(XMLPathQuery q)
  {
    this._res.AppendLine();
    string str1 = q.RootName ?? "root";
    string str2 = "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"";
    if (q.HasRoot)
    {
      this._res.Append("CONCAT(");
      this._res.Append($"'<{str1} {str2}>',");
    }
    List<SQLExpression> arguments = new List<SQLExpression>();
    bool flag = q.ElementName != string.Empty;
    string str3 = q.ElementName ?? "row";
    if (flag)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<" + str3);
      if (!q.HasRoot)
        stringBuilder.Append(" " + str2);
      stringBuilder.Append(">");
      arguments.Add((SQLExpression) new SQLConst((object) stringBuilder.ToString()));
    }
    List<SQLExpression> selection = q.GetSelection();
    // ISSUE: explicit non-virtual call
    if (selection != null && __nonvirtual (selection.Count) > 0)
    {
      int num = 1;
      foreach (SQLExpression exp in selection)
      {
        string str4 = exp.AliasOrName();
        if (str4 == null)
        {
          str4 = "UnknownColumn" + num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          ++num;
        }
        SQLExpression sqlExpression1 = exp;
        PXDbType dbType = exp.GetDBType();
        if (PgSQLWriterVisitor.IsStringDbType(dbType) || PgSQLWriterVisitor.IsStringFieldQuery(exp))
          sqlExpression1 = sqlExpression1.Replace((SQLExpression) new SQLConst((object) "&"), (SQLExpression) new SQLConst((object) "&amp;")).Replace((SQLExpression) new SQLConst((object) "<"), (SQLExpression) new SQLConst((object) "&lt;")).Replace((SQLExpression) new SQLConst((object) ">"), (SQLExpression) new SQLConst((object) "&gt;")).Replace((SQLExpression) new SQLConst((object) "\""), (SQLExpression) new SQLConst((object) "&quot;")).Replace((SQLExpression) new SQLConst((object) "'"), (SQLExpression) new SQLConst((object) "&apos;"));
        else if (q.HasBinaryBase64 && (dbType == PXDbType.Binary || dbType == PXDbType.VarBinary))
        {
          SQLConst sqlConst = new SQLConst((object) "base64");
          sqlConst.SetDBType(PXDbType.VarChar);
          sqlExpression1 = (SQLExpression) new SQLScalarFunction("encode", new SQLExpression[2]
          {
            sqlExpression1,
            (SQLExpression) sqlConst
          });
        }
        else if (dbType == PXDbType.UniqueIdentifier)
          sqlExpression1 = (SQLExpression) new SQLConvert(typeof (string), sqlExpression1);
        SQLExpression sqlExpression2 = SQLExpression.ConcatSequence(new SQLConst((object) $"<{str4}>").Seq(sqlExpression1).Seq((SQLExpression) new SQLConst((object) $"</{str4}>")));
        if (q.HasXsinil)
          sqlExpression2 = sqlExpression2.Coalesce((SQLExpression) new SQLConst((object) $"<{str4} xsi:nil=\"true\"/>"));
        arguments.Add(sqlExpression2);
      }
    }
    if (flag)
      arguments.Add((SQLExpression) new SQLConst((object) $"</{str3}>"));
    SQLGroupConcat sqlGroupConcat = new SQLGroupConcat(arguments, q.GetOrder())
    {
      Distinct = true,
      Separator = string.Empty
    };
    Query q1 = (Query) q.Duplicate();
    q1.ClearSelection();
    q1.Field(sqlGroupConcat.Coalesce((SQLExpression) new SQLConst((object) string.Empty)));
    q1.GetOrder()?.Clear();
    if (q1.HasPaging() && q1.Projection() is ProjectionForXmlReference projectionForXmlReference)
    {
      projectionForXmlReference.Skip = q1.GetOffset();
      projectionForXmlReference.Top = q1.GetLimit();
      q1.Offset(0).Limit(0);
    }
    this._res.Append("(");
    this.Visit(q1);
    this._res.AppendLine();
    this._res.Append(")");
    if (q.HasRoot)
    {
      this._res.AppendLine();
      this._res.Append($",'</{str1}>')");
    }
    return this._res;
  }

  /// <inheritdoc />
  public override StringBuilder Visit(Joiner j)
  {
    j.Table().Alias = this.sqlDialect.MakeCorrectDbIdentifier(j.Table().Alias);
    return base.Visit(j);
  }

  private static bool IsStringDbType(PXDbType dbType)
  {
    return dbType == PXDbType.NVarChar || dbType == PXDbType.VarChar || dbType == PXDbType.NChar || dbType == PXDbType.Char || dbType == PXDbType.NText || dbType == PXDbType.Text;
  }

  private static bool IsStringFieldQuery(SQLExpression exp)
  {
    if (!(exp is SubQuery subQuery))
      return false;
    List<SQLExpression> selection = subQuery.Query()?.GetSelection();
    return selection != null && selection.Count == 1 && selection.Single<SQLExpression>().GetDBType().IsString();
  }

  /// <inheritdoc />
  public override StringBuilder Visit(JoinedAttrQuery q)
  {
    this._res.Append("SELECT (STRING_AGG(CONCAT(QUOTE_NULLABLE(");
    if (q.valCol_ == "AttributeID")
      this._res.Append(this.sqlDialect.quoteDbIdentifier("AttributeID"));
    else
      this._res.Append(this.sqlDialect.quoteTableAndColumn(q.srcTable_, q.valCol_));
    this._res.Append("), '->', QUOTE_NULLABLE(");
    if (q.keyCol_ != null)
      this._res.Append("\"").Append(q.keyCol_).Append("\"");
    else
      this._res.Append("COALESCE(CAST(ValueNumeric AS CHAR(30)), CAST(ValueDate AS CHAR(23)), ValueString, ValueText, N'')");
    this._res.Append(")), '\\r\\n')) AS attrs FROM ");
    foreach (Joiner joiner in q.GetFrom())
      joiner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    this._res.Append(" WHERE ");
    q.GetWhere()?.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" GROUP BY ").Append(q.kvExtCol_);
    if (q.GetOrder() != null)
    {
      foreach (OrderSegment orderSegment in q.GetOrder())
      {
        if (orderSegment.expr_ is Column expr)
        {
          this._res.Append(", ");
          expr.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        }
      }
    }
    this.AppendOrderBy((Query) q);
    this._res.Append(" LIMIT 1");
    return this._res;
  }

  private void AppendOrderBy(Query q)
  {
    if (q.GetOrder() == null || !q.GetOrder().Any<OrderSegment>())
      return;
    this._res.Append(Environment.NewLine + "ORDER BY ");
    bool flag = true;
    foreach (OrderSegment os in q.GetOrder())
    {
      if (os.expr_ != null)
      {
        if (flag)
          flag = false;
        else
          this._res.Append(", ");
        this.Visit(os);
        this._res.Append(" NULLS ");
        this._res.Append(os.ascending_ ? "FIRST" : "LAST");
      }
    }
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SimpleTable t)
  {
    t.Name = this.sqlDialect.MakeCorrectDbIdentifier(t.Name);
    t.Alias = this.sqlDialect.MakeCorrectDbIdentifier(t.Alias);
    return base.Visit(t);
  }

  /// <inheritdoc />
  public override StringBuilder SQLTableName(SimpleTable t)
  {
    t.Name = this.sqlDialect.MakeCorrectDbIdentifier(t.Name);
    t.Alias = this.sqlDialect.MakeCorrectDbIdentifier(t.Alias);
    return base.SQLTableName(t);
  }

  /// <inheritdoc />
  public override StringBuilder Visit(Query q)
  {
    List<SQLExpression> selection = q.GetSelection();
    // ISSUE: explicit non-virtual call
    if (selection != null && __nonvirtual (selection.Count) > 0)
    {
      this._res.Append("SELECT ");
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
          this._res.Append($" AS {this.kwPrefix_}{this.sqlDialect.MakeCorrectDbIdentifier(sqlExpression.Alias())}{this.kwSuffix_}");
      }
    }
    SQLExpression where1 = q.GetWhere();
    List<Joiner> from = q.GetFrom();
    bool flag1 = from != null && from.Any<Joiner>();
    if (flag1)
    {
      // ISSUE: explicit non-virtual call
      if (selection != null && __nonvirtual (selection.Count) > 0)
        this._res.Append(Environment.NewLine + "FROM ");
      foreach (Joiner joiner in from)
        joiner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    }
    List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
    if (q.GetSelection() != null)
      sqlExpressionList.AddRange((IEnumerable<SQLExpression>) q.GetSelection());
    if (q.GetWhere() != null)
      sqlExpressionList.Add(q.GetWhere());
    if (q.GetGroupBy() != null)
      sqlExpressionList.AddRange((IEnumerable<SQLExpression>) q.GetGroupBy());
    if (q.GetOrder() != null)
      sqlExpressionList.AddRange(q.GetOrder().Select<OrderSegment, SQLExpression>((Func<OrderSegment, SQLExpression>) (os => os.expr_)));
    List<SQLExpression> sqlExpressions = new List<SQLExpression>();
    sqlExpressionList.ForEach((System.Action<SQLExpression>) (e => e?.FillExpressionsOfType((Predicate<SQLExpression>) (expr => expr is SQLRank), sqlExpressions)));
    List<SQLRank> list1 = sqlExpressions.Cast<SQLRank>().ToList<SQLRank>();
    if (q.GetWhere() != null && list1.Any<SQLRank>())
    {
      SQLExpression where2 = q.GetWhere();
      List<SQLFullTextSearch> list2 = where2.GetExpressionsOfType(SQLExpression.Operation.CONTAINS).Cast<SQLFullTextSearch>().ToList<SQLFullTextSearch>();
      list2.AddRange(where2.GetExpressionsOfType(SQLExpression.Operation.FREETEXT).Cast<SQLFullTextSearch>());
      foreach (SQLRank sqlRank in list1)
      {
        Column column = sqlRank.Field as Column;
        if (column != null)
        {
          SQLFullTextSearch fts = list2.FirstOrDefault<SQLFullTextSearch>((Func<SQLFullTextSearch, bool>) (ftp => ftp.SearchField().Name.OrdinalEquals(column.Name)));
          sqlRank.SetFTS(fts);
        }
      }
    }
    if (where1 != null)
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
      where1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
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
    this.AppendOrderBy(q);
    int num = q.GetLimit();
    int offset = q.GetOffset();
    if (num == 0 && offset > 0)
      num = 2147483646;
    if (num > 0)
    {
      this._res.Append(Environment.NewLine + "LIMIT ");
      this._res.Append(num);
      if (offset > 0)
      {
        this._res.Append(Environment.NewLine + "OFFSET ");
        this._res.Append(offset);
      }
    }
    foreach (Unioner unioner in q.GetUnion())
      unioner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    return this._res;
  }

  /// <inheritdoc />
  protected override bool NeedBrackets(Query sq) => base.NeedBrackets(sq) && sq.Alias != null;

  /// <inheritdoc />
  public override StringBuilder SQLAlias(SimpleTable t)
  {
    t.Name = this.sqlDialect.MakeCorrectDbIdentifier(t.Name);
    t.Alias = this.sqlDialect.MakeCorrectDbIdentifier(t.Alias);
    return base.SQLAlias(t);
  }

  public override StringBuilder SQLAlias(Table t)
  {
    t.Alias = this.sqlDialect.MakeCorrectDbIdentifier(t.Alias);
    return base.SQLAlias(t);
  }

  /// <inheritdoc />
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
    this._res.Append(str ?? "");
    if (e.RExpr() != null)
      e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    else if (e.Oper() == SQLExpression.Operation.COUNT)
      this._res.Append("*");
    this._res.Append(")");
    return this._res;
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SQLDateDiff e)
  {
    this._res.Append("floor(date_part('epoch', ");
    to_timestamp(e.RExpr());
    this._res.Append(" - ");
    to_timestamp(e.LExpr());
    this._res.Append(")");
    this._res.Append(GetDatePart(e.UOM()));
    this._res.Append(")");
    if (PgSQLWriterVisitor.IsIntExtractResult(e.UOM()))
      this._res.Append("::int");
    return this._res;

    void to_timestamp(SQLExpression exp)
    {
      int num = !(exp is Column column) ? 0 : (column.GetDBType() == PXDbType.Int ? 1 : 0);
      if (num != 0)
        this._res.Append("to_timestamp(");
      exp.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      if (exp is SQLConst sqlConst && sqlConst.GetValue() is string s && System.DateTime.TryParse(s, out System.DateTime _))
        this._res.Append("::timestamp");
      if (num == 0)
        return;
      this._res.Append("*60*60*24)");
    }

    static string GetDatePart(string msSqlDatePart)
    {
      if (msSqlDatePart != null)
      {
        switch (msSqlDatePart.Length)
        {
          case 2:
            switch (msSqlDatePart[1])
            {
              case 'd':
                if (msSqlDatePart == "dd")
                  return "/24/60/60";
                break;
              case 'h':
                if (msSqlDatePart == "hh")
                  return "/60/60";
                break;
              case 'i':
                if (msSqlDatePart == "mi")
                  return "/60";
                break;
              case 'm':
                if (msSqlDatePart == "mm")
                  return "/30.44/24/60/60";
                break;
              case 'q':
                if (msSqlDatePart == "qq")
                  return "/30.44/3/24/60/60";
                break;
              case 's':
                switch (msSqlDatePart)
                {
                  case "ss":
                    return "";
                  case "ms":
                    return "*1000";
                }
                break;
              case 'w':
                if (msSqlDatePart == "ww")
                  return "/7/24/60/60";
                break;
            }
            break;
          case 4:
            if (msSqlDatePart == "yyyy")
              return "/365/24/60/60";
            break;
        }
      }
      return "/24/60/60";
    }
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SQLDatePart e)
  {
    switch (e.UOM())
    {
      case "ss":
        return this.VisitDatePartSeconds(e);
      case "ms":
        return this.VisitDatePartMilliseconds(e);
      default:
        return this.VisitDatePart(e);
    }
  }

  private StringBuilder VisitDatePart(SQLDatePart e)
  {
    this._res.Append($"EXTRACT({PgSQLWriterVisitor.ConvertToPgSqlDateDiff(e.UOM(), "DAY")} FROM (");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(")::timestamp)");
    if (PgSQLWriterVisitor.IsIntExtractResult(e.UOM()))
      this._res.Append("::int");
    return this._res;
  }

  private StringBuilder VisitDatePartMilliseconds(SQLDatePart e)
  {
    this._res.Append("TRUNC(");
    this.VisitDatePart(e);
    this._res.Append("-TRUNC(");
    this.VisitDatePart(new SQLDatePart((IConstant<string>) new DatePart.second(), e.LExpr().Duplicate()));
    this._res.Append(")*1000)");
    return this._res;
  }

  private StringBuilder VisitDatePartSeconds(SQLDatePart e)
  {
    this._res.Append("TRUNC(");
    this.VisitDatePart(e);
    this._res.Append(")");
    return this._res;
  }

  private static bool IsIntExtractResult(string msSqlDatePart)
  {
    if (msSqlDatePart != null)
    {
      switch (msSqlDatePart.Length)
      {
        case 2:
          switch (msSqlDatePart[1])
          {
            case 'd':
              if (msSqlDatePart == "dd")
                break;
              goto label_12;
            case 'h':
              if (msSqlDatePart == "hh")
                break;
              goto label_12;
            case 'i':
              if (msSqlDatePart == "mi")
                break;
              goto label_12;
            case 'm':
              if (msSqlDatePart == "mm")
                break;
              goto label_12;
            case 'q':
              if (msSqlDatePart == "qq")
                break;
              goto label_12;
            case 'w':
              if (msSqlDatePart == "dw" || msSqlDatePart == "ww")
                break;
              goto label_12;
            case 'y':
              if (msSqlDatePart == "dy")
                break;
              goto label_12;
            default:
              goto label_12;
          }
          break;
        case 4:
          if (!(msSqlDatePart == "yyyy"))
            goto label_12;
          break;
        default:
          goto label_12;
      }
      return true;
    }
label_12:
    return false;
  }

  private static string ConvertToPgSqlDateDiff(string msSqlDatePart, string defaultValue)
  {
    if (msSqlDatePart != null)
    {
      switch (msSqlDatePart.Length)
      {
        case 2:
          switch (msSqlDatePart[1])
          {
            case 'd':
              if (msSqlDatePart == "dd")
                return "DAY";
              break;
            case 'h':
              if (msSqlDatePart == "hh")
                return "HOUR";
              break;
            case 'i':
              if (msSqlDatePart == "mi")
                return "MINUTE";
              break;
            case 'm':
              if (msSqlDatePart == "mm")
                return "MONTH";
              break;
            case 'q':
              if (msSqlDatePart == "qq")
                return "QUARTER";
              break;
            case 's':
              switch (msSqlDatePart)
              {
                case "ss":
                  return "SECOND";
                case "ms":
                  return "MILLISECONDS";
              }
              break;
            case 'w':
              switch (msSqlDatePart)
              {
                case "dw":
                  return "ISODOW";
                case "ww":
                  return "WEEK";
              }
              break;
            case 'y':
              if (msSqlDatePart == "dy")
                return "DOY";
              break;
          }
          break;
        case 4:
          if (msSqlDatePart == "yyyy")
            return "YEAR";
          break;
      }
    }
    return defaultValue;
  }

  private static string ConvertToPgSqlDatePartForInterval(string msSqlDatePart, string defaultValue)
  {
    if (msSqlDatePart != null)
    {
      switch (msSqlDatePart.Length)
      {
        case 2:
          switch (msSqlDatePart[1])
          {
            case 'd':
              if (msSqlDatePart == "dd")
                return "days";
              break;
            case 'h':
              if (msSqlDatePart == "hh")
                return "hours";
              break;
            case 'i':
              if (msSqlDatePart == "mi")
                return "mins";
              break;
            case 'm':
              if (msSqlDatePart == "mm")
                return "months";
              break;
            case 's':
              if (msSqlDatePart == "ss")
                return "secs";
              break;
            case 'w':
              if (msSqlDatePart == "ww")
                return "weeks";
              break;
          }
          break;
        case 4:
          if (msSqlDatePart == "yyyy")
            return "years";
          break;
      }
    }
    return defaultValue;
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SQLFullTextSearch e)
  {
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" @@ ");
    this.To_TsQuery(e);
    return this._res;
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SQLRank e)
  {
    if (e.FTS != null)
    {
      this._res.Append("ts_rank_cd(to_tsvector('english'::regconfig, ");
      e.FTS.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append("), ");
      this.To_TsQuery(e.FTS);
      this._res.Append(")");
    }
    return this._res;
  }

  public override StringBuilder Visit(SQLExpression e)
  {
    if (e.IsAggregate())
      return this.SQLAggregateExpression(e);
    this.EnsureConstTypes(e);
    switch (e.Oper())
    {
      case SQLExpression.Operation.EQ:
        this.EnsurePadSpaced(e);
        if (e.IsStringColumnLiteralCompare() || e.IsRightLeftCompare(new Func<SQLExpression, SQLExpression, bool>(this.IsPadSpacedCharConvertAndStringConstCompare)))
          return this.VisitEqual(e, StringComparison.InvariantCultureIgnoreCase);
        break;
      case SQLExpression.Operation.NE:
        this.EnsurePadSpaced(e);
        if (e.LExpr().IsStringColumnLiteral() && e.RExpr().IsConstLiteral() || e.IsRightLeftCompare(new Func<SQLExpression, SQLExpression, bool>(this.IsPadSpacedCharConvertAndStringConstCompare)))
          return this.VisitNotEqual(e, StringComparison.InvariantCultureIgnoreCase);
        break;
      case SQLExpression.Operation.REPLACE:
        SQLExpression sqlExpression = e.RExpr();
        this.PgFormatFunctionWithCollate("REPLACE", e.LExpr(), sqlExpression.LExpr(), sqlExpression.RExpr());
        return this._res;
      case SQLExpression.Operation.RTRIM:
        this._res.Append("RTrim( ");
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append("::text)");
        return this._res;
      case SQLExpression.Operation.LTRIM:
        this._res.Append("LTrim( ");
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append("::text)");
        return this._res;
      case SQLExpression.Operation.LOWER:
        this._res.Append("LOWER( ");
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append("::text)");
        return this._res;
      case SQLExpression.Operation.UPPER:
        this._res.Append("UPPER( ");
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append("::text)");
        return this._res;
      case SQLExpression.Operation.LEFT:
        this._res.Append("LEFT( ");
        e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append("::text, ");
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append("::int)");
        return this._res;
      case SQLExpression.Operation.RIGHT:
        this._res.Append("RIGHT( ");
        e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append("::text, ");
        e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append("::int)");
        return this._res;
    }
    return base.Visit(e);
  }

  private void EnsurePadSpaced(SQLExpression exp)
  {
    if (exp.LExpr().IsConstLiteral())
    {
      if (exp.RExpr().IsPadSpacedStringColumn())
        exp.SetLeft((SQLExpression) new SQLConvert(typeof (char), exp.LExpr(), this.PadSpacedLength));
      else if (exp.RExpr().IsNotPadSpacedStringColumn())
      {
        exp.SetLeft((SQLExpression) new SQLConvert(typeof (string), exp.LExpr()));
      }
      else
      {
        if (!(exp.RExpr() is Literal literal) || !PgSQLWriterVisitor.IsStringFieldQuery(literal.LExpr()))
          return;
        exp.RExpr().SetLeft((SQLExpression) new SQLConvert(typeof (char), exp.RExpr().LExpr(), this.PadSpacedLength));
      }
    }
    else if (exp.RExpr().IsConstLiteral())
    {
      if (exp.LExpr().IsPadSpacedStringColumn() || exp.LExpr().IsPadSpacedStringColumnLiteral())
        exp.SetRight((SQLExpression) new SQLConvert(typeof (char), exp.RExpr(), this.PadSpacedLength));
      else if (exp.LExpr().IsNotPadSpacedStringColumn())
      {
        exp.SetRight((SQLExpression) new SQLConvert(typeof (string), exp.RExpr()));
      }
      else
      {
        if (!(exp.LExpr() is Literal literal) || !PgSQLWriterVisitor.IsStringFieldQuery(literal.LExpr()))
          return;
        exp.LExpr().SetLeft((SQLExpression) new SQLConvert(typeof (char), exp.LExpr().LExpr().Duplicate(), this.PadSpacedLength));
      }
    }
    else if (exp.RExpr().IsConstOrConstLiteral() && PgSQLWriterVisitor.IsStringFieldQuery(exp.LExpr()))
    {
      exp.SetLeft((SQLExpression) new SQLConvert(typeof (char), exp.LExpr(), this.PadSpacedLength));
      if (!(exp.RExpr() is SQLConst sqlConst))
        return;
      sqlConst.SetDBType(PXDbType.Char);
    }
    else if (exp.LExpr().IsConstOrConstLiteral() && PgSQLWriterVisitor.IsStringFieldQuery(exp.RExpr()))
    {
      exp.SetRight((SQLExpression) new SQLConvert(typeof (char), exp.RExpr(), this.PadSpacedLength));
      if (!(exp.LExpr() is SQLConst sqlConst))
        return;
      sqlConst.SetDBType(PXDbType.Char);
    }
    else if (exp.RExpr() is SQLConst source1 && exp.LExpr().IsPadSpacedStringColumn())
    {
      exp.SetRight((SQLExpression) new SQLConvert(typeof (char), (SQLExpression) source1, this.PadSpacedLength));
    }
    else
    {
      if (!(exp.LExpr() is SQLConst source) || !exp.RExpr().IsPadSpacedStringColumn())
        return;
      exp.SetLeft((SQLExpression) new SQLConvert(typeof (char), (SQLExpression) source, this.PadSpacedLength));
    }
  }

  private bool IsPadSpacedCharConvert(SQLExpression expression)
  {
    return expression is SQLConvert sqlConvert && sqlConvert.GetDBType().IsChar() && sqlConvert.TargetTypeLength == this.PadSpacedLength;
  }

  private bool IsPadSpacedCharConvertAndStringConstCompare(SQLExpression left, SQLExpression right)
  {
    return this.IsPadSpacedCharConvert(left) && right.IsConstOrConstLiteral() && right.GetDBType().IsString();
  }

  private void EnsureConstTypes(SQLExpression exp)
  {
    if (EnsureConstTypesInternal(exp.LExpr(), exp.RExpr()))
      return;
    EnsureConstTypesInternal(exp.RExpr(), exp.LExpr());

    bool EnsureConstTypesInternal(SQLExpression constLiteral, SQLExpression otherExp)
    {
      if (!constLiteral.IsConstOrConstLiteral() || otherExp.IsConstOrConstLiteral())
        return false;
      PXDbType dbTypeOrDefault1 = otherExp.GetDBTypeOrDefault();
      PXDbType dbTypeOrDefault2 = constLiteral.GetDBTypeOrDefault();
      if (dbTypeOrDefault2 == PXDbType.Bit && dbTypeOrDefault1 == PXDbType.Decimal)
      {
        ((ISQLDBTypedExpression) constLiteral).SetDBType(PXDbType.Int);
        return true;
      }
      if (dbTypeOrDefault2 == dbTypeOrDefault1 || dbTypeOrDefault1 == PXDbType.Unspecified || dbTypeOrDefault2 != PXDbType.Unspecified)
        return false;
      ((ISQLDBTypedExpression) constLiteral).SetDBType(this.GetOperationArgumentsDBType(exp.Oper(), dbTypeOrDefault1));
      return true;
    }
  }

  private PXDbType GetMaxByPrecedence(SQLExpression left, SQLExpression right)
  {
    if (left is SQLConst && left.GetDBTypeOrDefault() != PXDbType.Bit && !(right is SQLConst) && right.GetDBTypeOrDefault().IsString())
      return right.GetDBTypeOrDefault();
    if (right is SQLConst && right.GetDBTypeOrDefault() != PXDbType.Bit && !(left is SQLConst) && left.GetDBTypeOrDefault().IsString())
      return left.GetDBTypeOrDefault();
    return SQLExpression.GetMaxByPrecedence(((IEnumerable<PXDbType>) new PXDbType[2]
    {
      left.GetDBTypeOrDefault(),
      right.GetDBTypeOrDefault()
    }).Where<PXDbType>((Func<PXDbType, bool>) (t => t != PXDbType.Unspecified)).ToArray<PXDbType>());
  }

  private void EnsureEqualSidesTypesForCoalese(SQLExpression exp)
  {
    PXDbType maxByPrecedence = this.GetMaxByPrecedence(exp.LExpr(), exp.RExpr());
    if (exp.LExpr().GetDBTypeOrDefault() != maxByPrecedence)
    {
      if (exp.LExpr() is ISQLDBTypedExpression sqldbTypedExpression1)
      {
        sqldbTypedExpression1.SetDBType(maxByPrecedence);
      }
      else
      {
        System.Type convertType = this.GetConvertType(maxByPrecedence);
        if (convertType != (System.Type) null && this.NeedTypeCast(maxByPrecedence, exp.LExpr().GetDBTypeOrDefault()))
        {
          SQLConvert e = new SQLConvert(convertType, exp.LExpr());
          exp.SetLeft((SQLExpression) e);
        }
      }
    }
    if (exp.RExpr().GetDBTypeOrDefault() == maxByPrecedence)
      return;
    if (exp.RExpr() is ISQLDBTypedExpression sqldbTypedExpression2)
    {
      sqldbTypedExpression2.SetDBType(maxByPrecedence);
    }
    else
    {
      System.Type convertType = this.GetConvertType(maxByPrecedence);
      if (!(convertType != (System.Type) null) || !this.NeedTypeCast(maxByPrecedence, exp.RExpr().GetDBTypeOrDefault()))
        return;
      SQLConvert e = new SQLConvert(convertType, exp.RExpr());
      exp.SetRight((SQLExpression) e);
    }
  }

  private PXDbType GetOperationArgumentsDBType(
    SQLExpression.Operation oper,
    PXDbType otherArgDBType)
  {
    switch (oper)
    {
      case SQLExpression.Operation.BIT_AND:
      case SQLExpression.Operation.BIT_OR:
      case SQLExpression.Operation.BIT_NOT:
        return otherArgDBType.IsString() ? PXDbType.Text : PXDbType.Int;
      default:
        return otherArgDBType;
    }
  }

  private void To_TsQuery(SQLFullTextSearch e)
  {
    if (e.Oper() == SQLExpression.Operation.FREETEXT && e.SearchValue is SQLConst searchValue)
    {
      this._res.Append("to_tsquery(");
      this._res.Append(((SqlScripterBase) PgSqlScripter.Instance).quoteLiteral(string.Join(" | ", ((IEnumerable<string>) searchValue.GetValue().ToString().Split(' ')).Select<string, string>((Func<string, string>) (s => s.Trim()))), new bool?()));
    }
    else
    {
      this._res.Append("to_tsquery(");
      e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
    this._res.Append(")");
  }

  /// <inheritdoc />
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
      this._res.Append(" || ");
    if (e.RExpr() == null)
      return;
    if (e.RExpr().Oper() == SQLExpression.Operation.SEQ)
      this.seqAsPlus(e.RExpr());
    else
      e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
  }

  /// <inheritdoc />
  public override StringBuilder VisitConvertBinToInt(SQLExpression e)
  {
    SQLExpression sqlExpression = e.RExpr();
    return this.FormatFunction("SUBSTRING", e.LExpr(), sqlExpression.LExpr(), sqlExpression.RExpr());
  }

  /// <inheritdoc />
  public override StringBuilder VisitCastAsInteger(SQLExpression e)
  {
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("::int");
  }

  /// <inheritdoc />
  public override StringBuilder VisitCastAsVarBinary(SQLExpression e)
  {
    this._res.Append("(");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(")::bytea");
  }

  /// <inheritdoc />
  public override StringBuilder VisitBinaryLen(SQLExpression e)
  {
    this.FormatFunction("OCTET_LENGTH", e.RExpr());
    return e.RExpr().GetDBType() == PXDbType.BigInt ? this._res.Append("::bigint") : this._res;
  }

  /// <inheritdoc />
  public override StringBuilder VisitLen(SQLExpression e)
  {
    this.FormatFunction("LENGTH", e.RExpr());
    return e.RExpr().GetDBType() == PXDbType.BigInt ? this._res.Append("::bigint") : this._res;
  }

  /// <inheritdoc />
  public override StringBuilder VisitIsNumeric(SQLExpression e)
  {
    return this.FormatFunction("isnumeric", e.RExpr());
  }

  /// <inheritdoc />
  public override StringBuilder VisitTrim(SQLExpression e)
  {
    this._res.Append("LTRIM(RTRIM( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("))");
  }

  /// <inheritdoc />
  public override StringBuilder VisitIsNullFunc(SQLExpression e)
  {
    this.EnsureEqualSidesTypesForCoalese(e);
    return this.FormatFunction("COALESCE", e.LExpr(), e.RExpr());
  }

  /// <inheritdoc />
  public override StringBuilder VisitDateNow(SQLExpression e)
  {
    return this._res.Append(this.sqlDialect.GetDate);
  }

  /// <inheritdoc />
  public override StringBuilder VisitDateUtcNow(SQLExpression e)
  {
    return this._res.Append(this.sqlDialect.GetUtcDate);
  }

  /// <inheritdoc />
  public override StringBuilder VisitToday(SQLExpression e) => this._res.Append("current_date");

  /// <inheritdoc />
  public override StringBuilder VisitTodayUtc(SQLExpression e)
  {
    return this._res.Append($"({this.sqlDialect.GetUtcDate})::date");
  }

  /// <inheritdoc />
  public override StringBuilder VisitGetTime(SQLExpression e)
  {
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("::time");
  }

  /// <inheritdoc />
  public override StringBuilder VisitCharIndex(SQLExpression e)
  {
    return this.PgFormatFunctionWithCollate("strpos", e.LExpr(), e.RExpr());
  }

  /// <inheritdoc />
  public override StringBuilder VisitRepeat(SQLExpression e)
  {
    return this.FormatFunction("repeat", e.LExpr(), e.RExpr());
  }

  /// <inheritdoc />
  public override StringBuilder VisitPower(SQLExpression e)
  {
    return this.FormatFunction("POWER", e.LExpr(), e.RExpr());
  }

  /// <inheritdoc />
  public override StringBuilder VisitMinusDate(SQLExpression e)
  {
    this._res.Append("(");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" - INTERVAL '");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("' DAY)");
  }

  /// <inheritdoc />
  public override StringBuilder VisitASCII(SQLExpression e)
  {
    if (e.IsEmbraced())
      this._res.Append("( ");
    this._res.Append(" get_byte(");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", 0)");
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  public override StringBuilder VisitChar(SQLExpression e)
  {
    if (e.RExpr() is SQLConst sqlConst && sqlConst.GetValue() is 0)
      return this._res.Append("''");
    return this.FormatFunction("chr", e.RExpr());
  }

  /// <inheritdoc />
  public override StringBuilder VisitLike(SQLExpression e, bool negative = false)
  {
    if (e.IsEmbraced())
      this._res.Append("( ");
    AsUnaccent((System.Action) (() =>
    {
      e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      if (PgSQLWriterVisitor.IsStringDbType(e.LExpr().GetDBType()))
        return;
      this._res.Append("::" + this.GetTypeNameForConvert(typeof (string)));
    }));
    if (negative)
      this._res.Append(" NOT");
    this._res.Append($" {((SqlScripterBase) PgSqlScripter.Instance).CaseInsensitiveLike} REPLACE(");
    AsUnaccent((System.Action) (() => e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this)));
    this._res.Append(" collate \"default\"");
    this._res.Append(", '\\', '\\\\')");
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;

    static void AsUnaccent(System.Action action) => action();
  }

  /// <inheritdoc />
  public override StringBuilder VisitEqual(SQLExpression e, StringComparison comparison)
  {
    return this.VisitEquality(e, comparison, " = ");
  }

  public StringBuilder VisitNotEqual(SQLExpression e, StringComparison comparison)
  {
    return this.VisitEquality(e, comparison, " <> ");
  }

  private StringBuilder VisitEquality(
    SQLExpression e,
    StringComparison comparison,
    string operation)
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
    this._res.Append(operation);
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
    this._res.Append(this.CollateCollation(collation));
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  /// <summary>
  /// Here we using a custom collations, similar to SQL Server
  /// latin1_general_ci_ai -&gt; 'und-u-ks-level1-kc-false'
  /// latin1_general_cs_ai -&gt; 'und-u-ks-level1-kc-true'
  /// latin1_general_ci_as -&gt; 'und-u-ks-level2'
  /// latin1_general_cs_as -&gt; 'und-u-ks-level2-kc-true'
  /// </summary>
  /// 
  ///             Collations in Postgresql:
  ///             <see cref="!:https://www.postgresql.org/docs/current/collation.html" />
  /// 
  ///             CLDR repository BCP47:
  ///             <see cref="!:https://github.com/unicode-org/cldr/blob/main/common/bcp47/collation.xml" />
  /// 
  ///             RFC BCP47:
  ///             <see cref="!:https://www.rfc-editor.org/info/bcp47" />
  /// 
  ///             ICU documentation:
  ///             <see cref="!:https://unicode-org.github.io/icu" />
  /// <param name="comparison"></param>
  /// <returns></returns>
  private string GetCollation(StringComparison comparison)
  {
    switch (comparison)
    {
      case StringComparison.CurrentCulture:
        return PXLocalesProvider.CollationProvider.Collation.CS ?? "latin1_general_cs_ai";
      case StringComparison.CurrentCultureIgnoreCase:
        return PXLocalesProvider.CollationProvider.Collation.CI ?? "latin1_general_ci_ai";
      case StringComparison.InvariantCulture:
        return "latin1_general_cs_ai";
      case StringComparison.InvariantCultureIgnoreCase:
        return "latin1_general_ci_ai";
      case StringComparison.Ordinal:
        return "C.utf8";
      case StringComparison.OrdinalIgnoreCase:
        return "default";
      default:
        return (string) null;
    }
  }

  private string GetCollation(string ci, bool? ignoreCase)
  {
    (string CS, string CI, string CSL, string CIL) tuple = PXLocalesProvider.CollationProvider.CollationByCulture(ci);
    bool? nullable = ignoreCase;
    bool flag = true;
    return !(nullable.GetValueOrDefault() == flag & nullable.HasValue) ? tuple.CS : tuple.CI ?? "default";
  }

  /// <inheritdoc />
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
    StringExpressionAppend(stringExpression1, true);
    this._res.Append(" = ");
    StringExpressionAppend(stringExpression2);
    if (!string.IsNullOrEmpty(collation))
      this._res.Append($" COLLATE \"{collation}\"");
    this._res.Append(" THEN 0 WHEN ");
    StringExpressionAppend(stringExpression1);
    this._res.Append(" < ");
    StringExpressionAppend(stringExpression2);
    if (!string.IsNullOrEmpty(collation))
      this._res.Append($" COLLATE \"{collation}\"");
    this._res.Append(" THEN -1 WHEN ");
    StringExpressionAppend(stringExpression1);
    this._res.Append(" > ");
    StringExpressionAppend(stringExpression2);
    if (!string.IsNullOrEmpty(collation))
      this._res.Append($" COLLATE \"{collation}\"");
    this._res.Append(" THEN 1 ELSE NULL END)");
    return this._res;

    void StringExpressionAppend(SQLExpression stringExpression, bool castAsString = false)
    {
      if (needCI)
      {
        this._res.Append("LOWER(");
        stringExpression.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append(")");
      }
      else
        stringExpression.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      if (!castAsString || !(stringExpression is SQLConst sqlConst3) || sqlConst3.GetDBType() != PXDbType.Unspecified && !PgSQLWriterVisitor.IsStringDbType(sqlConst3.GetDBType()))
        return;
      this._res.Append("::text");
    }
  }

  /// <inheritdoc />
  public override StringBuilder VisitCompareWithCulture(SQLExpression e)
  {
    SQLExpression sqlExpression1 = e.LExpr();
    SQLExpression sqlExpression2 = e.RExpr().LExpr();
    bool? ignoreCase = (bool?) (e.RExpr().RExpr().LExpr() is SQLConst sqlConst1 ? sqlConst1.GetValue() : (object) null);
    object obj = e.RExpr().RExpr().RExpr() is SQLConst sqlConst2 ? sqlConst2.GetValue() : (object) null;
    string str;
    if (obj == null || !(obj is string ci))
    {
      bool? nullable = ignoreCase;
      bool flag = true;
      str = nullable.GetValueOrDefault() == flag & nullable.HasValue ? this.GetCollation(StringComparison.CurrentCultureIgnoreCase) : this.GetCollation(StringComparison.CurrentCulture);
    }
    else
      str = this.GetCollation(ci, ignoreCase);
    string collation = str;
    this._res.Append("(CASE WHEN ");
    sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" = ");
    sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (!string.IsNullOrEmpty(collation))
      this._res.Append(this.CollateCollation(collation));
    this._res.Append(" THEN 0 WHEN ");
    sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" < ");
    sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (!string.IsNullOrEmpty(collation))
      this._res.Append(this.CollateCollation(collation));
    this._res.Append(" THEN -1 ELSE 1 END)");
    return this._res;
  }

  private string CollateCollation(string collation) => $" COLLATE \"{collation}\"";

  /// <inheritdoc />
  public override bool TryPlusSpecialCase(SQLExpression e)
  {
    SQLExpression sqlExpression1 = e.LExpr();
    SQLExpression sqlExpression2 = e.RExpr();
    PXDbType dbTypeOrDefault1 = sqlExpression1.GetDBTypeOrDefault();
    PXDbType dbTypeOrDefault2 = sqlExpression2.GetDBTypeOrDefault();
    if (dbTypeOrDefault1.IsDateTime())
    {
      this._res.Append("(");
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" + make_interval(DAYS => ");
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" ))");
      return true;
    }
    if (dbTypeOrDefault2.IsDateTime())
    {
      this._res.Append("(");
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" + make_interval(DAYS => ");
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" ))");
      return true;
    }
    if (sqlExpression1.IsConcatable() && (sqlExpression2.IsConcatable() || dbTypeOrDefault2 == PXDbType.Unspecified) || sqlExpression2.IsConcatable() && (sqlExpression1.IsConcatable() || dbTypeOrDefault1 == PXDbType.Unspecified))
    {
      this.FormatFunction("CONCAT", sqlExpression1, sqlExpression2);
      return true;
    }
    this.SetExpressionDbType(e);
    return false;
  }

  private void SetExpressionDbType(SQLExpression e)
  {
    if (e == null)
      return;
    if (e is SQLConst sqlConst1 && sqlConst1.GetDBType() == PXDbType.Unspecified && sqlConst1.GetValue() is string)
      sqlConst1.SetDBType(PXDbType.NVarChar);
    if (e is SQLSwitch sqlSwitch)
    {
      sqlSwitch.GetCases().ForEach((System.Action<Tuple<SQLExpression, SQLExpression>>) (c =>
      {
        if (!(c.Item2 is SQLConst sqlConst3) || sqlConst3.GetDBType() != PXDbType.Unspecified || !(sqlConst3.GetValue() is string))
          return;
        sqlConst3.SetDBType(PXDbType.NVarChar);
      }));
      if (sqlSwitch.GetDefault() is SQLConst sqlConst4 && sqlConst4.GetDBType() == PXDbType.Unspecified && sqlConst4.GetValue() is string)
        sqlConst4.SetDBType(PXDbType.NVarChar);
    }
    this.SetExpressionDbType(e.LExpr());
    this.SetExpressionDbType(e.RExpr());
  }

  /// <inheritdoc />
  public override StringBuilder VisitToBase64(SQLExpression e)
  {
    this._res.Append("encode(");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("::bytea, 'base64')");
  }

  public override StringBuilder VisitCastAsDecimal(SQLExpression e)
  {
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("::decimal(19,6)");
  }

  /// <inheritdoc />
  public override string GetAddSign(SQLExpression e)
  {
    return !e.GetDBTypeOrDefault().IsString() ? "+" : "||";
  }

  protected override StringBuilder FormatRoundFunction(SQLExpression source, SQLExpression n)
  {
    this._res.Append("ROUND").Append("(");
    source.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", ");
    n.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("::int)");
  }

  protected StringBuilder PgFormatFunctionWithCollate(string name, params SQLExpression[] args)
  {
    this._res.Append(name).Append("( ");
    for (int index = 0; index < args.Length; ++index)
    {
      if (index > 0)
        this._res.Append(", ");
      args[index].Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      if (index == 0)
        this._res.Append(" collate \"default\"");
    }
    return this._res.Append(")");
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SQLConvert e)
  {
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append("::" + this.GetTypeNameForConvert(e.TargetType, e.TargetTypeLength));
  }

  private string GetTypeNameForConvert(System.Type type, int length)
  {
    return this.GetTypeNameForConvert(type) + (length > 0 ? $"({length})" : "");
  }

  /// <inheritdoc />
  protected override string GetTypeNameForConvert(System.Type type)
  {
    if (type == typeof (bool))
      return "bool";
    if (type == typeof (System.DateTime))
      return "Timestamp";
    if (type == typeof (string))
      return "text";
    if (type == typeof (float))
      return "real";
    if (type == typeof (Decimal))
      return "numeric";
    if (type == typeof (int))
      return "integer";
    if (type == typeof (short))
      return "smallint";
    if (type == typeof (long))
      return "bigint";
    if (type == typeof (double))
      return "float";
    if (type == typeof (char))
      return "char";
    throw new NotSupportedException($"Unsupported convert target type '{type.Name}'");
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SQLScalarFunction e)
  {
    return this.FormatFunction(e.Name ?? "", e.LExpr());
  }

  public override StringBuilder Visit(SQLSwitch c)
  {
    PXDbType dbType = c.GetDBType();
    System.Type convertType = this.GetConvertType(dbType);
    if (c.IsEmbraced())
      this._res.Append("(");
    this._res.Append("CASE ");
    foreach (Tuple<SQLExpression, SQLExpression> tuple in c.GetCases())
    {
      this._res.Append("WHEN ");
      tuple.Item1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" THEN ");
      SQLExpression source = tuple.Item2;
      if (source.GetDBType() != dbType && convertType != (System.Type) null)
        source = (SQLExpression) new SQLConvert(convertType, source);
      source.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" ");
    }
    if (c.RExpr() != null)
    {
      this._res.Append("ELSE ");
      SQLExpression source = c.RExpr();
      if (convertType != (System.Type) null && this.NeedTypeCast(dbType, source.GetDBType()))
        source = (SQLExpression) new SQLConvert(convertType, source);
      source.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" ");
    }
    this._res.Append("END");
    if (c.IsEmbraced())
      this._res.Append(")");
    return this._res;
  }

  private bool NeedTypeCast(PXDbType mainDBType, PXDbType exprType)
  {
    return (mainDBType != PXDbType.Char && mainDBType != PXDbType.VarChar || exprType != PXDbType.Text) && mainDBType != exprType;
  }

  private System.Type GetConvertType(PXDbType dbType)
  {
    switch (dbType)
    {
      case PXDbType.BigInt:
        return typeof (long);
      case PXDbType.Bit:
        return typeof (bool);
      case PXDbType.Char:
      case PXDbType.NChar:
        return typeof (char);
      case PXDbType.DateTime:
      case PXDbType.SmallDateTime:
        return typeof (System.DateTime);
      case PXDbType.Int:
      case PXDbType.SmallInt:
      case PXDbType.TinyInt:
        return typeof (int);
      case PXDbType.NVarChar:
      case PXDbType.VarChar:
        return typeof (string);
      default:
        return (System.Type) null;
    }
  }

  /// <inheritdoc />
  public override StringBuilder Visit(Column c)
  {
    c.Name = c.Name.ToLower();
    if (c.Alias() != null)
      c.As(c.Alias().ToLower());
    return base.Visit(c);
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SQLGroupConcat exp)
  {
    this._res.AppendLine();
    this._res.Append("string_agg(");
    bool flag = true;
    foreach (SQLExpression sqlExpression in exp.Arguments)
    {
      if (flag)
        flag = false;
      else
        this._res.Append(" || ");
      sqlExpression.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
    this._res.AppendLine();
    this._res.Append($", '{exp.Separator}' ");
    this.AppendOrderBy(exp.OrderBy);
    this._res.Append(")");
    return this._res;
  }

  public override StringBuilder Visit(Md5Hash exp)
  {
    this._res.Append("md5").Append("(");
    exp.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(")");
  }

  private void AppendOrderBy(List<OrderSegment> orderBy)
  {
    if (orderBy == null || !orderBy.Any<OrderSegment>())
      return;
    this._res.Append(Environment.NewLine + "ORDER BY ");
    bool flag = true;
    foreach (OrderSegment os in orderBy)
    {
      if (flag)
        flag = false;
      else
        this._res.Append(", ");
      this.Visit(os);
    }
  }

  /// <inheritdoc />
  public override StringBuilder VisitConstValue(object value, PXDbType type)
  {
    if (value == null)
      return this._res.Append("NULL");
    System.Type type1 = value.GetType();
    if (type1 == typeof (byte[]))
    {
      if (type == PXDbType.Timestamp)
        return this._res.Append(((SqlScripterBase) PgSqlScripter.Instance).quoteLiteral(DbAdapterPgSql.TimeStampToString((byte[]) value), new bool?()));
      byte[] source = (byte[]) value;
      return this._res.Append("'").Append("\\x").Append(((IEnumerable<byte>) source).Any<byte>() ? BitConverter.ToString(source).Replace("-", "") : "0").Append("'");
    }
    if (type1 == typeof (PXFieldName))
      return this._res.Append(((PXDataValue) value).Value);
    if (!(value is ICollection collection))
      return this._res.Append(this.sqlDialect.enquoteValue(value, type));
    bool flag = true;
    foreach (object obj in (IEnumerable) collection)
    {
      if (flag)
        flag = false;
      else
        this._res.Append(", ");
      this.VisitConstValue(obj, PXDbType.Unspecified);
    }
    if (flag)
      this._res.Append("NULL");
    return this._res;
  }

  /// <inheritdoc />
  public override StringBuilder Visit(SQLDateAdd e)
  {
    string str = !(e.DatePart == "dw") ? PgSQLWriterVisitor.ConvertToPgSqlDatePartForInterval(e.DatePart, "") : throw new PXException("Cannot add weekday unit.");
    if (str == "")
      throw new PXException("Unknown interval unit.");
    this._res.Append("(");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append($" + make_interval({str} => ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append("::int))");
    return this._res;
  }

  public override StringBuilder Visit(SQLAggConcat exp)
  {
    this._res.Append("STRING_AGG( ");
    exp.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append($"::text, '{exp.GetSeparator()}' )");
  }
}
