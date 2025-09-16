// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.MySQLWriterVisitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.MySql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal class MySQLWriterVisitor : BaseSQLWriterVisitor
{
  private QueryHints _globalQueryHints;
  private int _queryLevel;

  public MySQLWriterVisitor()
  {
    this.kwPrefix_ = "`";
    this.kwSuffix_ = "`";
  }

  private bool isMySql56
  {
    get => this.sqlDialect.DbmsVersion.Major == 5 && this.sqlDialect.DbmsVersion.Minor == 6;
  }

  private bool isMySql8Plus => this.sqlDialect.DbmsVersion.Major >= 8;

  public override StringBuilder Visit(XMLPathQuery q)
  {
    this._res.AppendLine();
    this._globalQueryHints |= QueryHints.MySqlGroupConcatMaxLength;
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
        if (MySQLWriterVisitor.IsStringDbType(dbType) || MySQLWriterVisitor.IsStringFieldQuery(exp))
          sqlExpression1 = sqlExpression1.Replace((SQLExpression) new SQLConst((object) "&"), (SQLExpression) new SQLConst((object) "&amp;")).Replace((SQLExpression) new SQLConst((object) "<"), (SQLExpression) new SQLConst((object) "&lt;")).Replace((SQLExpression) new SQLConst((object) ">"), (SQLExpression) new SQLConst((object) "&gt;")).Replace((SQLExpression) new SQLConst((object) "\""), (SQLExpression) new SQLConst((object) "&quot;")).Replace((SQLExpression) new SQLConst((object) "'"), (SQLExpression) new SQLConst((object) "&apos;"));
        else if (q.HasBinaryBase64 && (dbType == PXDbType.Binary || dbType == PXDbType.VarBinary))
          sqlExpression1 = (SQLExpression) new SQLScalarFunction("to_base64", new SQLExpression[1]
          {
            sqlExpression1
          });
        else if (dbType == PXDbType.UniqueIdentifier)
          sqlExpression1 = (SQLExpression) new SQLConvert(typeof (string), sqlExpression1)
          {
            MySqlCharset = "utf8mb4"
          };
        if (this.isMySql56 && sqlExpression1 is SQLSwitch && sqlExpression1.GetDBType() == PXDbType.Unspecified && sqlExpression1.GetExpressionsOfType<SQLConst>().All<SQLConst>((Func<SQLConst, bool>) (c => c.GetValue() is bool)))
          ((ISQLDBTypedExpression) sqlExpression1).SetDBType(PXDbType.Char);
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

  public override StringBuilder Visit(JoinedAttrQuery q)
  {
    this._globalQueryHints |= QueryHints.MySqlGroupConcatMaxLength;
    this._res.Append("SELECT CONVERT(GROUP_CONCAT(CONCAT(QUOTE(");
    if (q.valCol_ == "AttributeID")
      this._res.Append("`AttributeID`");
    else
      this._res.Append("`").Append(q.srcTable_).Append("`.`").Append(q.valCol_).Append("`");
    this._res.Append("), '->', QUOTE(");
    if (q.keyCol_ != null)
      this._res.Append("`").Append(q.keyCol_).Append("`");
    else
      this._res.Append("COALESCE(CAST(ValueNumeric AS CHAR(30)), CAST(ValueDate AS CHAR(23)), ValueString, ValueText, N'')");
    this._res.Append(")) SEPARATOR '\\r\\n') USING utf8mb4) AS attrs FROM ");
    foreach (Joiner joiner in q.GetFrom())
      joiner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    this._res.Append(" WHERE ");
    q.GetWhere()?.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" GROUP BY ").Append(q.kvExtCol_);
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
    this._res.Append(" LIMIT 1");
    return this._res;
  }

  public override StringBuilder Visit(SQLDateDiff e)
  {
    this._res.Append("TIMESTAMPDIFF(" + this.ConvertToMySqlDateDiff(e.UOM(), "DAY"));
    this._res.Append(", ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(")");
    if (e.UOM() == "ms")
      this._res.Append("/1000");
    return this._res;
  }

  private string ConvertToMySqlDateDiff(string msSqlDatePart, string defaultValue)
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
                  return "MICROSECOND";
              }
              break;
            case 'w':
              if (msSqlDatePart == "ww")
                return "WEEK";
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

  public override StringBuilder Visit(SQLDatePart e)
  {
    string datePartFunction = this.GetMySqlDatePartFunction(e.UOM(), "");
    if (datePartFunction == "")
      throw new PXException("Unknown date unit.");
    this.FormatFunction(datePartFunction, e.LExpr());
    if (e.UOM() == "ms")
      this._res.Append("/1000");
    return this._res;
  }

  public override StringBuilder Visit(SQLDateAdd e)
  {
    string str = !(e.DatePart == "dw") ? this.GetMySqlDatePartFunction(e.DatePart, "") : throw new PXException("Cannot add weekday unit.");
    if (str == "")
      throw new PXException("Unknown date unit.");
    this._res.Append("DATE_ADD( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", INTERVAL ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append($" {str})");
    if (e.DatePart == "ms")
      this._res.Append("/1000");
    return this._res;
  }

  public override StringBuilder Visit(SQLConvert e)
  {
    int num = !string.IsNullOrEmpty(e.MySqlCharset) ? 1 : 0;
    bool flag = this.sqlDialect.DbmsVersion.Major < 8;
    if (num != 0)
      this._res.Append("CONVERT( ");
    else if (((e.TargetType == typeof (double) ? 1 : (e.TargetType == typeof (float) ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      this._res.Append("((");
    else if (e.TargetType == typeof (bool))
      this._res.Append("pp_conv2tinyInt1( ");
    else if (e.TargetType == typeof (int))
      this._res.Append("pp_conv2int( ");
    else if (e.TargetType == typeof (short))
      this._res.Append("pp_conv2smallInt( ");
    else
      this._res.Append("CONVERT( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (num != 0)
      this._res.Append($" USING {e.MySqlCharset})");
    else if (((e.TargetType == typeof (double) ? 1 : (e.TargetType == typeof (float) ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      this._res.Append(") + 0.0)");
    else if (e.TargetType == typeof (bool))
      this._res.Append(")");
    else if (e.TargetType == typeof (int))
      this._res.Append(")");
    else if (e.TargetType == typeof (short))
      this._res.Append(")");
    else
      this._res.Append($", {this.GetTypeNameForConvert(e.TargetType)})");
    return this._res;
  }

  protected override string GetTypeNameForConvert(System.Type type)
  {
    if (type == typeof (bool))
      return "SIGNED";
    if (type == typeof (string))
      return "NCHAR";
    if (type == typeof (float))
      return "FLOAT(23)";
    if (type == typeof (Decimal))
      return "DECIMAL(57,28)";
    if (type == typeof (int) || type == typeof (short) || type == typeof (long))
      return "SIGNED";
    return type == typeof (double) ? "FLOAT(53)" : base.GetTypeNameForConvert(type);
  }

  public override StringBuilder Visit(SQLScalarFunction e)
  {
    return this.FormatFunction(e.Name, e.LExpr());
  }

  private string GetMySqlDatePartFunction(string mySqlDatePart, string defaultValue)
  {
    if (mySqlDatePart != null)
    {
      switch (mySqlDatePart.Length)
      {
        case 2:
          switch (mySqlDatePart[1])
          {
            case 'd':
              if (mySqlDatePart == "dd")
                return "DAY";
              break;
            case 'h':
              if (mySqlDatePart == "hh")
                return "HOUR";
              break;
            case 'i':
              if (mySqlDatePart == "mi")
                return "MINUTE";
              break;
            case 'm':
              if (mySqlDatePart == "mm")
                return "MONTH";
              break;
            case 'q':
              if (mySqlDatePart == "qq")
                return "QUARTER";
              break;
            case 's':
              switch (mySqlDatePart)
              {
                case "ss":
                  return "SECOND";
                case "ms":
                  return "MICROSECOND";
              }
              break;
            case 'w':
              switch (mySqlDatePart)
              {
                case "dw":
                  return "DAYOFWEEK";
                case "ww":
                  return "WEEK";
              }
              break;
            case 'y':
              if (mySqlDatePart == "dy")
                return "DAYOFYEAR";
              break;
          }
          break;
        case 4:
          if (mySqlDatePart == "yyyy")
            return "YEAR";
          break;
      }
    }
    return defaultValue;
  }

  public override StringBuilder Visit(Query q)
  {
    try
    {
      ++this._queryLevel;
      List<Joiner> from1 = q.GetFrom();
      bool? nullable = from1 != null ? new bool?(from1.Any<Joiner>((Func<Joiner, bool>) (j => j.Join() == Joiner.JoinType.FULL_JOIN))) : new bool?();
      if (nullable.HasValue && nullable.Value)
      {
        Query query1 = (Query) q.Duplicate();
        query1.ApplyHints(QueryHints.None);
        query1.GetOrder()?.Clear();
        List<OrderSegment> orderBy = (List<OrderSegment>) null;
        List<OrderSegment> order = q.GetOrder();
        if (order != null)
        {
          orderBy = new List<OrderSegment>();
          for (int index = 0; index < order.Count; ++index)
          {
            OrderSegment orderSegment = order[index];
            string str = "order__" + index.ToString();
            SQLExpression sqlExpression1 = orderSegment.expr_;
            Column orderCol = sqlExpression1 as Column;
            if (orderCol != null && orderCol.Table() == null)
            {
              SQLExpression sqlExpression2 = query1.GetSelection().FirstOrDefault<SQLExpression>((Func<SQLExpression, bool>) (e => string.Equals(e.AliasOrName(), orderCol.AliasOrName(), StringComparison.OrdinalIgnoreCase)));
              if (sqlExpression2 != null)
                sqlExpression1 = sqlExpression2;
            }
            query1.Field(sqlExpression1.Duplicate().As(str));
            orderBy.Add(new OrderSegment((SQLExpression) new Column(str), orderSegment.ascending_));
          }
        }
        List<Query> allVariants = new List<Query>();
        this.GetAllVariantForFullJoin(query1, allVariants);
        bool flag = true;
        foreach (Query query2 in allVariants)
        {
          if (!flag)
            this._res.Append(Environment.NewLine).Append("UNION").Append(Environment.NewLine);
          else
            flag = false;
          this._res.Append("(");
          query2.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
          this._res.Append(")");
        }
        this.AppendOrderBy(orderBy);
        return this._res;
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
      sqlExpressionList.ForEach((System.Action<SQLExpression>) (e => e.FillExpressionsOfType((Predicate<SQLExpression>) (expr => expr is SQLRank), sqlExpressions)));
      List<SQLRank> list1 = sqlExpressions.Cast<SQLRank>().ToList<SQLRank>();
      if (q.GetWhere() != null && list1.Any<SQLRank>())
      {
        SQLExpression where = q.GetWhere();
        List<SQLFullTextSearch> list2 = where.GetExpressionsOfType(SQLExpression.Operation.CONTAINS).Cast<SQLFullTextSearch>().ToList<SQLFullTextSearch>();
        list2.AddRange(where.GetExpressionsOfType(SQLExpression.Operation.FREETEXT).Cast<SQLFullTextSearch>());
        foreach (SQLRank sqlRank in list1)
        {
          Column column = sqlRank.Field as Column;
          if (column != null)
          {
            SQLFullTextSearch fts = list2.FirstOrDefault<SQLFullTextSearch>((Func<SQLFullTextSearch, bool>) (ftp => ftp.SearchField().Name == column.Name));
            sqlRank.SetFTS(fts);
          }
        }
      }
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
            this._res.Append($" AS {this.kwPrefix_}{sqlExpression.Alias()}{this.kwSuffix_}");
        }
      }
      List<Joiner> from2 = q.GetFrom();
      if (from2 == null || !from2.Any<Joiner>())
        return this._res;
      // ISSUE: explicit non-virtual call
      if (selection != null && __nonvirtual (selection.Count) > 0)
        this._res.Append(Environment.NewLine + "FROM ");
      foreach (Joiner joiner in from2)
        joiner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
      SQLExpression where1 = q.GetWhere();
      if (where1 != null)
      {
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
        bool flag = true;
        foreach (SQLExpression sqlExpression in q.GetGroupBy())
        {
          if (flag)
            flag = false;
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
      this.AppendOrderBy(q.GetOrder());
      int num = q.GetLimit();
      int offset = q.GetOffset();
      if (num == 0 && offset > 0)
        num = 2147483646;
      if (num > 0)
      {
        this._res.Append(Environment.NewLine + "LIMIT ");
        if (offset > 0)
          this._res.Append($"{offset},");
        this._res.Append(num);
      }
      foreach (SQLRank sqlRank in list1)
        sqlRank.SetFTS((SQLFullTextSearch) null);
      foreach (Unioner unioner in q.GetUnion())
        unioner.Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
      return this._res;
    }
    finally
    {
      if (--this._queryLevel == 0)
        this.AddGlobalHints(this._res);
    }
  }

  public override StringBuilder Visit(SubQuery sq)
  {
    try
    {
      ++this._queryLevel;
      if (this.isMySql8Plus && sq.Query().GetSelection().Count == 1 && sq.Query().GetSelection().Single<SQLExpression>().Oper() == SQLExpression.Operation.COUNT)
        sq.Query().Limit(0, 0);
      return base.Visit(sq);
    }
    finally
    {
      --this._queryLevel;
    }
  }

  protected override void AppendJoinType(Joiner.JoinType jt, Joiner.JoinHint hint)
  {
    if (jt == Joiner.JoinType.INNER_JOIN && EnumerableExtensions.IsIn<Joiner.JoinHint>(hint, Joiner.JoinHint.STRAIGHT_FOR_TPT, Joiner.JoinHint.STRAIGHT_FORCED))
      this._res.Append(Environment.NewLine + "STRAIGHT_JOIN ");
    else
      base.AppendJoinType(jt, hint);
  }

  private void AddGlobalHints(StringBuilder res)
  {
    string str = PXDatabase.Provider.SqlDialect.GetQueryHintsText(this._globalQueryHints & QueryHints.MySqlGroupConcatMaxLength).SingleOrDefault<string>();
    if (string.IsNullOrEmpty(str))
      return;
    res.Insert(0, str);
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

  private void GetAllVariantForFullJoin(Query query, List<Query> allVariants)
  {
    List<Joiner> from = query.GetFrom();
    bool? nullable = from != null ? new bool?(from.Any<Joiner>((Func<Joiner, bool>) (j => j.Join() == Joiner.JoinType.FULL_JOIN))) : new bool?();
    if (!nullable.HasValue || !nullable.Value)
    {
      allVariants.Add(query);
    }
    else
    {
      Query query1 = (Query) query.Duplicate();
      query1.GetFrom().First<Joiner>((Func<Joiner, bool>) (j => j.Join() == Joiner.JoinType.FULL_JOIN)).SetJoinType(Joiner.JoinType.LEFT_JOIN);
      Query query2 = (Query) query.Duplicate();
      query2.GetFrom().First<Joiner>((Func<Joiner, bool>) (j => j.Join() == Joiner.JoinType.FULL_JOIN)).SetJoinType(Joiner.JoinType.RIGHT_JOIN);
      this.GetAllVariantForFullJoin(query1, allVariants);
      this.GetAllVariantForFullJoin(query2, allVariants);
    }
  }

  public override StringBuilder Visit(SQLFullTextSearch e)
  {
    this._res.Append("MATCH( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(") AGAINST ( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" IN BOOLEAN MODE)");
    return this._res;
  }

  public override StringBuilder Visit(SQLRank e)
  {
    if (e.FTS != null)
      e.FTS.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res;
  }

  public override StringBuilder VisitConcat(SQLExpression e)
  {
    return this.FormatFunction("CONCAT", e.RExpr());
  }

  public override StringBuilder VisitConvertBinToInt(SQLExpression e)
  {
    SQLExpression sqlExpression = e.RExpr();
    this._res.Append("CASE WHEN ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" IS NULL THEN NULL ELSE COALESCE(CONV(HEX(");
    this.FormatFunction("SUBSTRING", e.LExpr(), sqlExpression.LExpr(), sqlExpression.RExpr());
    return this._res.Append("), 16, 10), 0) END");
  }

  public override StringBuilder VisitCastAsInteger(SQLExpression e)
  {
    this._res.Append("CAST( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(" AS SIGNED INTEGER)");
  }

  public override StringBuilder VisitCastAsVarBinary(SQLExpression e)
  {
    this._res.Append("CAST( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(" AS BINARY)");
  }

  public override StringBuilder VisitBinaryLen(SQLExpression e)
  {
    return this.FormatFunction("LENGTH", e.RExpr());
  }

  public override StringBuilder VisitLen(SQLExpression e)
  {
    return this.FormatFunction("CHAR_LENGTH", e.RExpr());
  }

  public override StringBuilder VisitIsNumeric(SQLExpression e)
  {
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(" REGEXP '^[0-9]*[.]?[0-9]+$'");
  }

  public override StringBuilder VisitTrim(SQLExpression e)
  {
    return this.FormatFunction("Trim", e.RExpr());
  }

  public override StringBuilder VisitIsNullFunc(SQLExpression e)
  {
    return this.FormatFunction("COALESCE", e.LExpr(), e.RExpr());
  }

  public override StringBuilder VisitDateNow(SQLExpression e) => this._res.Append("NOW(6)");

  public override StringBuilder VisitDateUtcNow(SQLExpression e)
  {
    return this._res.Append("UTC_TIMESTAMP(6)");
  }

  public override StringBuilder VisitToday(SQLExpression e) => this._res.Append("CURDATE()");

  public override StringBuilder VisitTodayUtc(SQLExpression e) => this._res.Append("UTC_DATE()");

  public override StringBuilder VisitGetTime(SQLExpression e)
  {
    return this.FormatFunction("TIME", e.RExpr());
  }

  public override StringBuilder VisitCharIndex(SQLExpression e)
  {
    return this.FormatFunction("INSTR", e.LExpr(), e.RExpr());
  }

  public override StringBuilder VisitRepeat(SQLExpression e)
  {
    return this.FormatFunction("REPEAT", e.LExpr(), e.RExpr());
  }

  public override StringBuilder VisitPower(SQLExpression e)
  {
    return this.FormatFunction("POW", e.LExpr(), e.RExpr());
  }

  public override StringBuilder VisitMinusDate(SQLExpression e)
  {
    this._res.Append("DATE_SUB( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", INTERVAL ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(" DAY)");
  }

  public override StringBuilder VisitASCII(SQLExpression e)
  {
    if (e.IsEmbraced())
      this._res.Append("( ");
    this._res.Append(" ASCII ( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(")");
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  public override StringBuilder VisitChar(SQLExpression e)
  {
    this._res.Append("char").Append("( ");
    e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(" USING utf8").Append(")");
  }

  public override StringBuilder VisitLike(SQLExpression e, bool negative = false)
  {
    bool flag = this.sqlDialect.DbmsVersion.Major == 5;
    int num = e.LExpr().GetDBType() == PXDbType.VarChar ? 1 : 0;
    if (e.IsEmbraced())
      this._res.Append("( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (negative)
      this._res.Append(" NOT");
    this._res.Append(" LIKE REPLACE(");
    if (num != 0)
    {
      e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(", '\\\\', '\\\\\\\\')");
    }
    else
    {
      if (flag)
        this._res.Append("REPLACE(");
      this._res.Append("CAST( ");
      e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" AS CHAR) ");
      this._res.Append(", '\\\\', '\\\\\\\\')");
      if (flag)
        this._res.Append(", '%\\\\', '%\\\\\\\\')");
      this._res.Append(" COLLATE utf8mb4_unicode_ci");
    }
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  private string GetCollation(StringComparison comparison)
  {
    switch (comparison)
    {
      case StringComparison.CurrentCulture:
        return PXLocalesProvider.CollationProvider.Collation.CS;
      case StringComparison.CurrentCultureIgnoreCase:
        return PXLocalesProvider.CollationProvider.Collation.CI ?? "utf8mb4_unicode_ci";
      case StringComparison.InvariantCulture:
        return (string) null;
      case StringComparison.InvariantCultureIgnoreCase:
        return "utf8mb4_unicode_ci";
      case StringComparison.Ordinal:
        return "utf8mb4_bin";
      case StringComparison.OrdinalIgnoreCase:
        return "utf8mb4_bin";
      default:
        return (string) null;
    }
  }

  private string GetCollation(StringComparison comparison, bool isLatin)
  {
    switch (comparison)
    {
      case StringComparison.CurrentCulture:
        if (!isLatin)
        {
          if (!string.IsNullOrEmpty(PXLocalesProvider.CollationProvider.Collation.CS))
            return PXLocalesProvider.CollationProvider.Collation.CS;
          return this.sqlDialect.DbmsVersion.Major != 8 ? (string) null : "utf8mb4_0900_as_cs";
        }
        return !string.IsNullOrEmpty(PXLocalesProvider.CollationProvider.Collation.CSL) ? PXLocalesProvider.CollationProvider.Collation.CSL : "latin1_general_cs";
      case StringComparison.CurrentCultureIgnoreCase:
        return !isLatin ? (!string.IsNullOrEmpty(PXLocalesProvider.CollationProvider.Collation.CI) ? PXLocalesProvider.CollationProvider.Collation.CI : "utf8mb4_unicode_ci") : (!string.IsNullOrEmpty(PXLocalesProvider.CollationProvider.Collation.CIL) ? PXLocalesProvider.CollationProvider.Collation.CIL : "latin1_general_ci");
      case StringComparison.InvariantCulture:
        if (isLatin)
          return "latin1_general_cs";
        return this.sqlDialect.DbmsVersion.Major != 8 ? this.GetCollation(StringComparison.CurrentCulture, isLatin) : "utf8mb4_0900_as_cs";
      case StringComparison.InvariantCultureIgnoreCase:
        return !isLatin ? "utf8mb4_unicode_ci" : "latin1_general_ci";
      case StringComparison.Ordinal:
        return (string) null;
      case StringComparison.OrdinalIgnoreCase:
        return !isLatin ? "utf8mb4_bin" : "latin1_bin";
      default:
        return (string) null;
    }
  }

  private string GetCollation(string ci, bool? ignoreCase)
  {
    (string CS, string CI, string CSL, string CIL) tuple = PXLocalesProvider.CollationProvider.CollationByCulture(ci);
    bool? nullable = ignoreCase;
    bool flag = true;
    return !(nullable.GetValueOrDefault() == flag & nullable.HasValue) ? tuple.CS : tuple.CI ?? "utf8mb4_unicode_ci";
  }

  private string GetCollation(string ci, bool? ignoreCase, bool isLatin)
  {
    (string CS, string CI, string CSL, string CIL) tuple = PXLocalesProvider.CollationProvider.CollationByCulture(ci);
    bool? nullable = ignoreCase;
    bool flag = true;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return (string) null;
    return !isLatin ? tuple.CI ?? "utf8mb4_unicode_ci" : tuple.CIL ?? "latin1_general_ci";
  }

  public override StringBuilder VisitEqual(SQLExpression e, StringComparison comparison)
  {
    bool flag = this.sqlDialect.DbmsVersion.Major == 8;
    SQLExpression sqlExpression1 = e.LExpr();
    SQLExpression sqlExpression2 = e.RExpr();
    int num = comparison == StringComparison.OrdinalIgnoreCase ? 1 : 0;
    if (e.IsEmbraced())
      this._res.Append("( ");
    if (num != 0)
    {
      this._res.Append("LOWER(");
      if (flag)
      {
        this._res.Append(" CAST( ");
        sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append(" AS CHAR) ");
      }
      else
        sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(") ");
    }
    else if (flag)
    {
      this._res.Append(" CAST( ");
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" AS CHAR) ");
    }
    else
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" = REPLACE( ");
    if (num != 0)
    {
      this._res.Append("LOWER(");
      if (flag)
      {
        this._res.Append(" CAST( ");
        sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        this._res.Append(" AS CHAR) ");
      }
      else
        sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(") ");
    }
    else if (flag)
    {
      this._res.Append(" CAST( ");
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" AS CHAR) ");
    }
    else
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", '\\\\', '\\\\\\\\') ");
    string collation = this.GetCollation(comparison);
    if (string.IsNullOrEmpty(collation))
      throw new PXNotSupportedException($"Method String.Equal is not supported for {comparison} comparer");
    this._res.Append($" COLLATE {collation} ");
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  public override StringBuilder VisitCompare(SQLExpression e, StringComparison comparison)
  {
    bool isLatin = e.IsLatin();
    SQLExpression sqlExpression1 = e.LExpr();
    SQLExpression sqlExpression2 = e.RExpr();
    bool flag = comparison == StringComparison.OrdinalIgnoreCase;
    string collation = this.GetCollation(comparison, isLatin);
    if (string.IsNullOrEmpty(collation))
      throw new PXNotSupportedException($"Method String.Compare is not supported for {comparison} comparer");
    if (sqlExpression1 is Column && sqlExpression2 is SQLConst sqlConst1 && sqlConst1.GetValue() == null)
    {
      this._res.Append("(CASE WHEN ");
      sqlExpression1.IsNull().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" THEN 0 END)");
      return this._res;
    }
    if (sqlExpression2 is Column && sqlExpression1 is SQLConst sqlConst2 && sqlConst2.GetValue() == null)
    {
      this._res.Append("(CASE WHEN ");
      sqlExpression2.IsNull().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" THEN 0 END)");
      return this._res;
    }
    this._res.Append("STRCMP( ");
    if (flag)
    {
      this._res.Append("LOWER(");
      if (sqlExpression1 is ISQLConstantExpression)
        this._res.Append(PXDatabase.Provider.MakeCharsetPrefix(collation));
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(") ");
    }
    else
    {
      if (sqlExpression1 is ISQLConstantExpression)
        this._res.Append(PXDatabase.Provider.MakeCharsetPrefix(collation));
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
    this._res.Append($" COLLATE {collation} ");
    this._res.Append(", ");
    if (flag)
    {
      this._res.Append("LOWER(");
      if (sqlExpression2 is ISQLConstantExpression)
        this._res.Append(PXDatabase.Provider.MakeCharsetPrefix(collation));
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(") ");
    }
    else
    {
      if (sqlExpression2 is ISQLConstantExpression)
        this._res.Append(PXDatabase.Provider.MakeCharsetPrefix(collation));
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
    this._res.Append($" COLLATE {collation} ");
    this._res.Append(')');
    return this._res;
  }

  public override StringBuilder VisitToBase64(SQLExpression e)
  {
    return new SQLScalarFunction("to_base64", new SQLExpression[1]
    {
      e.RExpr()
    }).Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
  }

  public override bool TryPlusSpecialCase(SQLExpression e)
  {
    SQLExpression sqlExpression1 = e.LExpr();
    SQLExpression sqlExpression2 = e.RExpr();
    PXDbType dbTypeOrDefault1 = sqlExpression1.GetDBTypeOrDefault();
    PXDbType dbTypeOrDefault2 = sqlExpression2.GetDBTypeOrDefault();
    if (dbTypeOrDefault1.IsDateTime())
    {
      this._res.Append("DATE_ADD( ");
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(", INTERVAL ");
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" DAY)");
      return true;
    }
    if (dbTypeOrDefault2.IsDateTime())
    {
      this._res.Append("DATE_ADD( ");
      sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(", INTERVAL ");
      sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" DAY)");
      return true;
    }
    if ((!sqlExpression1.IsConcatable() || !sqlExpression2.IsConcatable() && dbTypeOrDefault2 != PXDbType.Unspecified) && (!sqlExpression2.IsConcatable() || !sqlExpression1.IsConcatable() && dbTypeOrDefault1 != PXDbType.Unspecified))
      return false;
    this.FormatFunction("CONCAT", sqlExpression1, sqlExpression2);
    return true;
  }

  public override StringBuilder VisitCompareWithCulture(SQLExpression e)
  {
    bool isLatin = e.IsLatin();
    SQLExpression sqlExpression1 = e.LExpr();
    SQLExpression sqlExpression2 = e.RExpr().LExpr();
    bool? ignoreCase = (bool?) (e.RExpr().RExpr().LExpr() is SQLConst sqlConst1 ? sqlConst1.GetValue() : (object) null);
    object obj = e.RExpr().RExpr().RExpr() is SQLConst sqlConst2 ? sqlConst2.GetValue() : (object) null;
    string str;
    if (obj == null || !(obj is string ci))
    {
      bool? nullable = ignoreCase;
      bool flag = true;
      str = nullable.GetValueOrDefault() == flag & nullable.HasValue ? this.GetCollation(StringComparison.CurrentCultureIgnoreCase, isLatin) : this.GetCollation(StringComparison.CurrentCulture, isLatin);
    }
    else
      str = this.GetCollation(ci, ignoreCase, isLatin);
    string collation = str;
    if (string.IsNullOrEmpty(collation))
      throw new PXNotSupportedException($"Method String.Compare is not supported for {obj}(IgnoreCase: {ignoreCase}) culture");
    this._res.Append("STRCMP( ");
    if (sqlExpression1 is ISQLConstantExpression)
      this._res.Append(PXDatabase.Provider.MakeCharsetPrefix(collation));
    sqlExpression1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append($" COLLATE {collation} ");
    this._res.Append(", ");
    if (sqlExpression2 is ISQLConstantExpression)
      this._res.Append(PXDatabase.Provider.MakeCharsetPrefix(collation));
    sqlExpression2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append($" COLLATE {collation} ");
    this._res.Append(')');
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
    this._res.Append(str ?? "");
    if (e.RExpr() != null)
      e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    else if (e.Oper() == SQLExpression.Operation.COUNT)
      this._res.Append("*");
    this._res.Append(")");
    return this._res;
  }

  public override StringBuilder Visit(Md5Hash exp)
  {
    this._res.Append("MD5").Append("(");
    exp.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append(")");
  }

  public override StringBuilder Visit(SQLAggConcat exp)
  {
    this._res.Append("GROUP_CONCAT( ");
    exp.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    return this._res.Append($" SEPARATOR '{exp.GetSeparator()}' )");
  }

  public override StringBuilder VisitConstValue(object value, PXDbType type)
  {
    if (value == null)
      return this._res.Append("NULL");
    System.Type type1 = value.GetType();
    if (type1 == typeof (byte[]))
    {
      if (type == PXDbType.Timestamp)
        return this._res.Append(((SqlScripterBase) MySqlScripter.Instance).quoteLiteral(DbAdapterMySql.TimeStampToString((byte[]) value), new bool?()));
      byte[] source = (byte[]) value;
      return this._res.Append("0x").Append(((IEnumerable<byte>) source).Any<byte>() ? BitConverter.ToString(source).Replace("-", "") : "0");
    }
    return type1 == typeof (PXFieldName) ? this._res.Append(((PXDataValue) value).Value) : this._res.Append(this.sqlDialect.enquoteValue(value, type));
  }

  public override StringBuilder Visit(SQLGroupConcat exp)
  {
    this._res.AppendLine();
    this._res.Append("group_concat(");
    if (exp.Distinct)
      this._res.Append("DISTINCT ");
    bool flag = true;
    foreach (SQLExpression sqlExpression in exp.Arguments)
    {
      if (flag)
        flag = false;
      else
        this._res.Append(", ");
      sqlExpression.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
    this.AppendOrderBy(exp.OrderBy);
    if (exp.Separator != null)
    {
      this._res.AppendLine();
      this._res.Append($" separator '{exp.Separator}'");
    }
    this._res.Append(")");
    return this._res;
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
}
