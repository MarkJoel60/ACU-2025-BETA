// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.BaseSQLWriterVisitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal abstract class BaseSQLWriterVisitor : 
  ISQLQueryVisitor<StringBuilder>,
  ISQLExpressionVisitor<StringBuilder>
{
  private readonly Lazy<IUserOrganizationService> _userBranchService = new Lazy<IUserOrganizationService>((Func<IUserOrganizationService>) (() => ServiceLocator.Current.GetInstance<IUserOrganizationService>()));
  protected StringBuilder _res = new StringBuilder();
  private readonly Lazy<PXDatabaseProvider> _provider = new Lazy<PXDatabaseProvider>((Func<PXDatabaseProvider>) (() => PXDatabase.Provider));
  protected string kwPrefix_;
  protected string kwSuffix_;

  public virtual StringBuilder Visit(SQLMultiOperation e)
  {
    int length1 = this._res.Length;
    bool flag1 = false;
    if (e.Oper() == SQLExpression.Operation.OR && !e.IsEmbraced() && !e.IsUnembraced())
    {
      flag1 = true;
      this._res.Append(" ( ");
    }
    if (e.IsEmbraced())
    {
      flag1 = true;
      this._res.Append("( ");
    }
    int length2 = this._res.Length;
    bool flag2 = false;
    foreach (SQLExpression sqlExpression in e.Arguments)
    {
      if (flag2)
        this._res.Append(e.SqlSeparator);
      int length3 = this._res.Length;
      sqlExpression?.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      if (this._res.Length - length3 == 0 & flag2)
        this._res.Remove(this._res.Length - e.SqlSeparator.Length, e.SqlSeparator.Length);
      else
        flag2 = true;
    }
    if (flag1)
    {
      if (length2 == this._res.Length)
        this._res.Remove(length1, this._res.Length - length1);
      else
        this._res.Append(')');
    }
    return this._res;
  }

  private protected PXDatabaseProvider provider => this._provider.Value;

  private protected ISqlDialect sqlDialect => this.provider.SqlDialect;

  public abstract StringBuilder Visit(XMLPathQuery q);

  public abstract StringBuilder Visit(JoinedAttrQuery q);

  public abstract StringBuilder Visit(Query q);

  public virtual StringBuilder Visit(OrderSegment os)
  {
    os.expr_.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    if (!os.ascending_)
      this._res.Append(" DESC");
    return this._res;
  }

  protected virtual bool NeedBrackets(Query sq)
  {
    if (sq.GetFrom().Count != 1)
      return true;
    return sq.GetSelection() != null && sq.GetSelection().Count != 0;
  }

  public virtual StringBuilder Visit(Joiner j)
  {
    this.AppendJoinType(j.Join(), j.Hint());
    if (j.Table() is Query)
    {
      int num = this.NeedBrackets(j.Table() as Query) ? 1 : 0;
      if (num != 0)
        this._res.Append("(" + Environment.NewLine);
      j.Table().Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
      if (num != 0)
        this._res.Append(Environment.NewLine + ") ");
      string alias = j.Table().Alias;
      if ((alias != null ? (alias.Length > 0 ? 1 : 0) : 0) != 0)
        this._res.Append(this.kwPrefix_ + this.sqlDialect.MakeCorrectDbIdentifier(j.Table().Alias) + this.kwSuffix_);
    }
    else
      j.Table().Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    if (j.Condition() != null)
    {
      this._res.Append(" ON ");
      j.Condition().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
    return this._res;
  }

  public StringBuilder Visit(Unioner u)
  {
    this.AppendUnionType(u.Union());
    if (u.Table() is Query)
    {
      Query query = u.Table() as Query;
      bool flag = true;
      if (query.GetFrom().Count == 1 && (query.GetSelection() == null || query.GetSelection().Count == 0))
        flag = false;
      if (flag)
        this._res.Append("(" + Environment.NewLine);
      u.Table().Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
      if (flag)
        this._res.Append(Environment.NewLine + ") ");
      string alias = u.Table().Alias;
      if ((alias != null ? (alias.Length > 0 ? 1 : 0) : 0) != 0)
        this._res.Append(this.kwPrefix_ + this.sqlDialect.MakeCorrectDbIdentifier(u.Table().Alias) + this.kwSuffix_);
    }
    else
      u.Table().Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    return this._res;
  }

  protected virtual void AppendJoinType(Joiner.JoinType jt, Joiner.JoinHint hint)
  {
    switch (jt)
    {
      case Joiner.JoinType.CROSS_JOIN:
        this._res.Append(" CROSS JOIN ");
        break;
      case Joiner.JoinType.INNER_JOIN:
        this._res.Append(Environment.NewLine + "INNER JOIN ");
        break;
      case Joiner.JoinType.LEFT_JOIN:
        this._res.Append(Environment.NewLine + "LEFT JOIN ");
        break;
      case Joiner.JoinType.RIGHT_JOIN:
        this._res.Append(Environment.NewLine + "RIGHT JOIN ");
        break;
      case Joiner.JoinType.FULL_JOIN:
        this._res.Append(Environment.NewLine + "FULL JOIN ");
        break;
    }
  }

  protected virtual void AppendUnionType(Unioner.UnionType ut)
  {
    if (ut != Unioner.UnionType.UNION)
    {
      if (ut != Unioner.UnionType.UNIONALL)
        return;
      this._res.Append($"{Environment.NewLine}UNION ALL{Environment.NewLine}");
    }
    else
      this._res.Append($"{Environment.NewLine}UNION{Environment.NewLine}");
  }

  public virtual StringBuilder Visit(Table table) => this._res.Append("<TABLE>");

  public virtual StringBuilder Visit(SimpleTable t)
  {
    this._res.Append(this.kwPrefix_);
    this._res.Append(t.Name);
    this._res.Append(this.kwSuffix_ + " ");
    if (t.Alias != null && t.Alias.Any<char>())
    {
      this._res.Append(this.kwPrefix_);
      this._res.Append(this.sqlDialect.MakeCorrectDbIdentifier(t.Alias));
      this._res.Append(this.kwSuffix_);
    }
    else
    {
      this._res.Append(this.kwPrefix_);
      this._res.Append(this.sqlDialect.MakeCorrectDbIdentifier(t.Name));
      this._res.Append(this.kwSuffix_);
    }
    return this._res;
  }

  public virtual StringBuilder SQLTableName(SimpleTable t)
  {
    this._res.Append(this.kwPrefix_);
    this._res.Append(this.sqlDialect.MakeCorrectDbIdentifier(t.Name));
    this._res.Append(this.kwSuffix_);
    return this._res;
  }

  public virtual StringBuilder SQLAlias(SimpleTable t)
  {
    if (!string.IsNullOrEmpty(t.Alias))
    {
      this._res.Append(this.kwPrefix_);
      this._res.Append(this.sqlDialect.MakeCorrectDbIdentifier(t.Alias));
      this._res.Append(this.kwSuffix_);
    }
    else
    {
      this._res.Append(this.kwPrefix_);
      this._res.Append(this.sqlDialect.MakeCorrectDbIdentifier(t.Name));
      this._res.Append(this.kwSuffix_);
    }
    return this._res;
  }

  public virtual StringBuilder SQLAlias(Table t)
  {
    this._res.Append(this.sqlDialect.MakeCorrectDbIdentifier(t.Alias));
    return this._res;
  }

  public virtual StringBuilder Visit(SubQuery sq)
  {
    this._res.Append("(");
    sq.Query().Accept<StringBuilder>((ISQLQueryVisitor<StringBuilder>) this);
    this._res.Append(")");
    return this._res;
  }

  protected abstract StringBuilder SQLAggregateExpression(SQLExpression e);

  public virtual StringBuilder Visit(SQLDateByTimeZone exp)
  {
    return exp.Value.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
  }

  public abstract StringBuilder Visit(SQLDateDiff e);

  public abstract StringBuilder Visit(SQLDatePart e);

  public virtual StringBuilder Visit(SQLSmartConvert exp)
  {
    return exp.GetDBType() != exp.RExpr().GetDBType() ? this.Visit((SQLConvert) exp) : exp.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
  }

  public abstract StringBuilder Visit(SQLDateAdd e);

  public virtual StringBuilder Visit(NoteIdExpression exp)
  {
    return !exp.IgnoreNulls ? exp.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this) : exp.MainExpr.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
  }

  public abstract StringBuilder Visit(SQLConvert e);

  protected virtual string GetTypeNameForConvert(System.Type type)
  {
    if (type == typeof (bool))
      return "BIT";
    if (type == typeof (System.DateTime))
      return "DATETIME";
    if (type == typeof (string))
      return "NVARCHAR(Max)";
    if (type == typeof (float))
      return "REAL";
    if (type == typeof (Decimal))
      return "DECIMAL(38,19)";
    if (type == typeof (int))
      return "INT";
    if (type == typeof (short))
      return "SMALLINT";
    if (type == typeof (long))
      return "BIGINT";
    if (type == typeof (double))
      return "FLOAT(53)";
    if (type == typeof (char))
      return "CHAR";
    throw new NotSupportedException($"Unsupported convert target type '{type.Name}'");
  }

  public abstract StringBuilder Visit(SQLScalarFunction e);

  public abstract StringBuilder Visit(SQLFullTextSearch e);

  public abstract StringBuilder Visit(SQLRank e);

  public virtual StringBuilder Visit(SQLExpression e)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    if (e.IsAggregate())
      return this.SQLAggregateExpression(e);
    bool flag = false;
    int length1 = this._res.Length;
    switch (e.Oper())
    {
      case SQLExpression.Operation.PLUS:
        if (this.TryPlusSpecialCase(e))
          return this._res;
        break;
      case SQLExpression.Operation.CONCAT:
        return this.VisitConcat(e);
      case SQLExpression.Operation.MINUS:
        switch (e.LExpr().GetDBTypeOrDefault())
        {
          case PXDbType.DateTime:
          case PXDbType.SmallDateTime:
            return this.VisitMinusDate(e);
        }
        break;
      case SQLExpression.Operation.OR:
        if (!e.IsEmbraced() && !e.IsUnembraced())
        {
          this._res.Append(" ( ");
          flag = true;
          break;
        }
        break;
      case SQLExpression.Operation.ASCII:
        return this.VisitASCII(e);
      case SQLExpression.Operation.LIKE:
        return this.VisitLike(e);
      case SQLExpression.Operation.NOT_LIKE:
        return this.VisitLike(e, true);
      case SQLExpression.Operation.SEQ:
        List<SQLExpression> sqlExpressionList = e.DecomposeSequence();
        for (int index = 0; index < sqlExpressionList.Count; ++index)
        {
          if (index > 0)
            this._res.Append(", ");
          sqlExpressionList[index].Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
        }
        return this._res;
      case SQLExpression.Operation.ISNULL_FUNC:
        return this.VisitIsNullFunc(e);
      case SQLExpression.Operation.SUBSTR:
        SQLExpression sqlExpression1 = e.RExpr();
        this.FormatFunction("SUBSTRING", e.LExpr(), sqlExpression1.LExpr(), sqlExpression1.RExpr());
        return this._res;
      case SQLExpression.Operation.CONVERT_BIN_TO_INT:
        return this.VisitConvertBinToInt(e);
      case SQLExpression.Operation.CAST_AS_DECIMAL:
        return this.VisitCastAsDecimal(e);
      case SQLExpression.Operation.CAST_AS_INTEGER:
        return this.VisitCastAsInteger(e);
      case SQLExpression.Operation.BINARY_LEN:
        return this.VisitBinaryLen(e);
      case SQLExpression.Operation.REPLACE:
        SQLExpression sqlExpression2 = e.RExpr();
        this.FormatFunction("REPLACE", e.LExpr(), sqlExpression2.LExpr(), sqlExpression2.RExpr());
        return this._res;
      case SQLExpression.Operation.NULL_IF:
        this.FormatFunction("NULLIF", e.LExpr(), e.RExpr());
        return this._res;
      case SQLExpression.Operation.LEN:
        return this.VisitLen(e);
      case SQLExpression.Operation.ABS:
        this.FormatFunction("ABS", e.RExpr());
        return this._res;
      case SQLExpression.Operation.RTRIM:
        this.FormatFunction("RTrim", e.RExpr());
        return this._res;
      case SQLExpression.Operation.LTRIM:
        this.FormatFunction("LTrim", e.RExpr());
        return this._res;
      case SQLExpression.Operation.ROUND:
        this.FormatRoundFunction(e.LExpr(), e.RExpr());
        return this._res;
      case SQLExpression.Operation.DATE_NOW:
        return this.VisitDateNow(e);
      case SQLExpression.Operation.DATE_UTC_NOW:
        return this.VisitDateUtcNow(e);
      case SQLExpression.Operation.CEILING:
        this.FormatFunction("CEILING", e.RExpr());
        return this._res;
      case SQLExpression.Operation.FLOOR:
        this.FormatFunction("FLOOR", e.RExpr());
        return this._res;
      case SQLExpression.Operation.CHARINDEX:
        return this.VisitCharIndex(e);
      case SQLExpression.Operation.REVERSE:
        this.FormatFunction("REVERSE", e.RExpr());
        return this._res;
      case SQLExpression.Operation.LOWER:
        this.FormatFunction("LOWER", e.RExpr());
        return this._res;
      case SQLExpression.Operation.UPPER:
        this.FormatFunction("UPPER", e.RExpr());
        return this._res;
      case SQLExpression.Operation.LEFT:
        this.FormatFunction("LEFT", e.LExpr(), e.RExpr());
        return this._res;
      case SQLExpression.Operation.RIGHT:
        this.FormatFunction("RIGHT", e.LExpr(), e.RExpr());
        return this._res;
      case SQLExpression.Operation.REPEAT:
        return this.VisitRepeat(e);
      case SQLExpression.Operation.POWER:
        return this.VisitPower(e);
      case SQLExpression.Operation.TODAY:
        return this.VisitToday(e);
      case SQLExpression.Operation.TODAY_UTC:
        return this.VisitTodayUtc(e);
      case SQLExpression.Operation.TRIM:
        return this.VisitTrim(e);
      case SQLExpression.Operation.GET_TIME:
        return this.VisitGetTime(e);
      case SQLExpression.Operation.CAST_AS_VARBINARY:
        return this.VisitCastAsVarBinary(e);
      case SQLExpression.Operation.INSIDE_BRANCH:
        SQLExpression r = e.LExpr();
        switch (r)
        {
          case SQLConst _:
label_7:
            this._res.Append(string.Join<int>(", ", BaseSQLWriterVisitor.GetInsideBranchParameterExpression((int?) e.LExpr().evaluateConstant())));
            return this._res;
          case Literal _:
            if (r.Oper() != SQLExpression.Operation.EMPTY)
              break;
            goto label_7;
        }
        Query q1 = new Query();
        SimpleTable t1 = new SimpleTable("Branch");
        SimpleTable t2 = new SimpleTable("Organization");
        Query q2 = new Query();
        q2.Field((SQLExpression) new Column(typeof (PXAccess.Organization.organizationID), (Table) t2)).From((Table) t2).Where(SQLExpressionExt.EQ(new Column(typeof (PXAccess.Organization.bAccountID), (Table) t2), r));
        q1.Field((SQLExpression) new Column(typeof (PXAccess.Branch.branchID), (Table) t1)).From((Table) t1).Where(SQLExpressionExt.EQ(new Column(typeof (PXAccess.Branch.bAccountID), (Table) t1), r).Or(new Column(typeof (PXAccess.Branch.organizationID), (Table) t1).In(q2)));
        return this.Visit(new SubQuery(q1));
      case SQLExpression.Operation.RESTRICT_BYUSERBRANCHES:
        SQLExpression sqlExpression3 = e.LExpr();
        List<int?> values = new List<int?>() { new int?(0) };
        switch (sqlExpression3)
        {
          case SQLConst _:
label_11:
            string constant = (string) e.LExpr().evaluateConstant();
            List<int?> nullableList = new List<int?>();
            using (IEnumerator<int> enumerator = this._userBranchService.Value.GetBranches(constant).Select<BranchInfo, int>((Func<BranchInfo, int>) (b => b.Id)).Distinct<int>().GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(new int?(enumerator.Current));
                values.Add(new int?(branch.BAccountID));
                if (!nullableList.Contains(branch.Organization.BAccountID))
                {
                  nullableList.Add(branch.Organization.BAccountID);
                  values.Add(branch.Organization.BAccountID);
                  if (branch.Organization.Parents.Count > 0)
                  {
                    foreach (PXAccess.MasterCollection.Organization parent in branch.Organization.Parents)
                    {
                      if (!nullableList.Contains(parent.BAccountID))
                        nullableList.Add(parent.BAccountID);
                      if (!values.Contains(parent.BAccountID))
                        values.Add(parent.BAccountID);
                    }
                  }
                }
              }
              break;
            }
          case Literal _:
            if (sqlExpression3.Oper() != SQLExpression.Operation.EMPTY)
              break;
            goto label_11;
        }
        this._res.Append(string.Join<int?>(", ", (IEnumerable<int?>) values));
        return this._res;
      case SQLExpression.Operation.IS_NUMERIC:
        return this.VisitIsNumeric(e);
      case SQLExpression.Operation.EQUAL_O_CI:
        return this.VisitEqual(e, StringComparison.OrdinalIgnoreCase);
      case SQLExpression.Operation.EQUAL_O_CS:
        return this.VisitEqual(e, StringComparison.Ordinal);
      case SQLExpression.Operation.EQUAL_I_CS:
        return this.VisitEqual(e, StringComparison.InvariantCulture);
      case SQLExpression.Operation.EQUAL_I_CI:
        return this.VisitEqual(e, StringComparison.InvariantCultureIgnoreCase);
      case SQLExpression.Operation.EQUAL_CC_CS:
        return this.VisitEqual(e, StringComparison.CurrentCulture);
      case SQLExpression.Operation.EQUAL_CC_CI:
        return this.VisitEqual(e, StringComparison.CurrentCultureIgnoreCase);
      case SQLExpression.Operation.COMPARE_O_CI:
        return this.VisitCompare(e, StringComparison.OrdinalIgnoreCase);
      case SQLExpression.Operation.COMPARE_O_CS:
        return this.VisitCompare(e, StringComparison.Ordinal);
      case SQLExpression.Operation.COMPARE_I_CS:
        return this.VisitCompare(e, StringComparison.InvariantCulture);
      case SQLExpression.Operation.COMPARE_I_CI:
        return this.VisitCompare(e, StringComparison.InvariantCultureIgnoreCase);
      case SQLExpression.Operation.COMPARE_CC_CS:
        return this.VisitCompare(e, StringComparison.CurrentCulture);
      case SQLExpression.Operation.COMPARE_CC_CI:
        return this.VisitCompare(e, StringComparison.CurrentCultureIgnoreCase);
      case SQLExpression.Operation.COMPARE_CULTUREINFO:
        return this.VisitCompareWithCulture(e);
      case SQLExpression.Operation.CHAR:
        return this.VisitChar(e);
      case SQLExpression.Operation.TO_BASE64:
        return this.VisitToBase64(e);
    }
    if (e.IsEmbraced())
      this._res.Append("( ");
    int num1 = 0;
    if (e.LExpr() != null)
    {
      int length2 = this._res.Length;
      e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      num1 = this._res.Length - length2;
    }
    switch (e.Oper())
    {
      case SQLExpression.Operation.NONE:
      case SQLExpression.Operation.NULL:
        this._res.Append("NULL");
        break;
      case SQLExpression.Operation.PLUS:
        this._res.Append($" {this.GetAddSign(e)} ");
        break;
      case SQLExpression.Operation.MUL:
        this._res.Append(" * ");
        break;
      case SQLExpression.Operation.DIV:
        this._res.Append(" / ");
        break;
      case SQLExpression.Operation.MOD:
        this._res.Append(" % ");
        break;
      case SQLExpression.Operation.MINUS:
        this._res.Append(" - ");
        break;
      case SQLExpression.Operation.UMINUS:
        this._res.Append(" (- ");
        flag = true;
        break;
      case SQLExpression.Operation.EQ:
        this._res.Append(" = ");
        break;
      case SQLExpression.Operation.NE:
        this._res.Append(" <> ");
        break;
      case SQLExpression.Operation.GE:
        this._res.Append(" >= ");
        break;
      case SQLExpression.Operation.GT:
        this._res.Append(" > ");
        break;
      case SQLExpression.Operation.LT:
        this._res.Append(" < ");
        break;
      case SQLExpression.Operation.LE:
        this._res.Append(" <= ");
        break;
      case SQLExpression.Operation.AND:
        if (num1 > 0)
        {
          this._res.Append(" AND ");
          break;
        }
        break;
      case SQLExpression.Operation.OR:
        if (num1 > 0)
        {
          this._res.Append(" OR ");
          break;
        }
        break;
      case SQLExpression.Operation.NOT:
        this._res.Append(" NOT ( ");
        flag = true;
        break;
      case SQLExpression.Operation.IN:
        this._res.Append(" IN ");
        if (!(e.RExpr() is SubQuery))
        {
          this._res.Append("( ");
          flag = true;
          break;
        }
        break;
      case SQLExpression.Operation.NOT_IN:
        this._res.Append(" NOT IN ");
        if (!(e.RExpr() is SubQuery))
        {
          this._res.Append("( ");
          flag = true;
          break;
        }
        break;
      case SQLExpression.Operation.MIN:
        this._res.Append("MIN( ");
        flag = true;
        break;
      case SQLExpression.Operation.COUNT:
        this._res.Append("COUNT( ");
        flag = true;
        break;
      case SQLExpression.Operation.COUNT_DISTINCT:
        this._res.Append("COUNT(DISTINCT ");
        flag = true;
        break;
      case SQLExpression.Operation.IS_NOT_NULL:
        this._res.Append(" IS NOT NULL ");
        break;
      case SQLExpression.Operation.IS_NULL:
        this._res.Append(" IS NULL ");
        break;
      case SQLExpression.Operation.BIT_AND:
        this._res.Append(" & ");
        break;
      case SQLExpression.Operation.BIT_OR:
        this._res.Append(" | ");
        break;
      case SQLExpression.Operation.EXISTS:
        this._res.Append(" EXISTS(");
        flag = true;
        break;
      case SQLExpression.Operation.NOT_EXISTS:
        this._res.Append(" NOT EXISTS(");
        flag = true;
        break;
      case SQLExpression.Operation.BETWEEN:
        this._res.Append(" BETWEEN ");
        break;
      case SQLExpression.Operation.NOT_BETWEEN:
        this._res.Append(" NOT BETWEEN ");
        break;
      case SQLExpression.Operation.BETWEEN_ARGS:
        this._res.Append(" AND ");
        break;
      case SQLExpression.Operation.BIT_NOT:
        this._res.Append(" (~ ");
        flag = true;
        break;
    }
    int num2 = 0;
    if (e.RExpr() != null)
    {
      int length3 = this._res.Length;
      e.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      num2 = this._res.Length - length3;
    }
    if (e.Oper() == SQLExpression.Operation.AND || e.Oper() == SQLExpression.Operation.OR)
    {
      if (num1 == 0 && num2 == 0)
        this._res.Remove(length1, this._res.Length - length1);
      else if (num2 == 0)
      {
        int length4 = e.Oper() == SQLExpression.Operation.AND ? 5 : 4;
        this._res.Remove(this._res.Length - length4, length4);
      }
    }
    if (flag)
      this._res.Append(')');
    if (e.IsEmbraced())
      this._res.Append(')');
    return this._res;
  }

  private static IEnumerable<int> GetInsideBranchParameterExpression(int? BAccountID)
  {
    int[] array = PXAccess.MasterBranches.AllBranchesByID.Where<KeyValuePair<int, PXAccess.MasterCollection.Branch>>((Func<KeyValuePair<int, PXAccess.MasterCollection.Branch>, bool>) (b =>
    {
      if (b.Value.DeletedDatabaseRecord)
        return false;
      int? nullable1 = BAccountID;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        int baccountId1 = b.Value.BAccountID;
        nullable1 = BAccountID;
        int valueOrDefault = nullable1.GetValueOrDefault();
        if (!(baccountId1 == valueOrDefault & nullable1.HasValue))
        {
          nullable1 = b.Value.Organization.BAccountID;
          int? nullable2 = BAccountID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            return b.Value.Organization.Parents.Any<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (p =>
            {
              int? baccountId2 = p.BAccountID;
              int? nullable3 = BAccountID;
              return baccountId2.GetValueOrDefault() == nullable3.GetValueOrDefault() & baccountId2.HasValue == nullable3.HasValue;
            }));
        }
      }
      return true;
    })).Select<KeyValuePair<int, PXAccess.MasterCollection.Branch>, int>((Func<KeyValuePair<int, PXAccess.MasterCollection.Branch>, int>) (b => b.Value.BranchID)).ToArray<int>();
    if (array != null && array.Length != 0)
      return (IEnumerable<int>) array;
    if (!BAccountID.HasValue)
    {
      IEnumerable<int> source = CurrentUserInformationProvider.Instance.GetAllBranches().Select<BranchInfo, int>((Func<BranchInfo, int>) (_ => _.Id));
      return source.Any<int>() ? source : (IEnumerable<int>) new int[1];
    }
    return (IEnumerable<int>) new int[1]{ BAccountID.Value };
  }

  public abstract StringBuilder VisitConcat(SQLExpression e);

  public abstract StringBuilder VisitConvertBinToInt(SQLExpression e);

  public abstract StringBuilder VisitCastAsInteger(SQLExpression e);

  public abstract StringBuilder VisitCastAsVarBinary(SQLExpression e);

  public abstract StringBuilder VisitBinaryLen(SQLExpression e);

  public abstract StringBuilder VisitLen(SQLExpression e);

  public abstract StringBuilder VisitIsNumeric(SQLExpression e);

  public abstract StringBuilder VisitTrim(SQLExpression e);

  public abstract StringBuilder VisitIsNullFunc(SQLExpression e);

  public abstract StringBuilder VisitDateNow(SQLExpression e);

  public abstract StringBuilder VisitDateUtcNow(SQLExpression e);

  public abstract StringBuilder VisitToday(SQLExpression e);

  public abstract StringBuilder VisitTodayUtc(SQLExpression e);

  public abstract StringBuilder VisitGetTime(SQLExpression e);

  public abstract StringBuilder VisitCharIndex(SQLExpression e);

  public abstract StringBuilder VisitRepeat(SQLExpression e);

  public abstract StringBuilder VisitPower(SQLExpression e);

  public abstract StringBuilder VisitMinusDate(SQLExpression e);

  public abstract StringBuilder VisitASCII(SQLExpression e);

  public virtual StringBuilder VisitChar(SQLExpression e) => this.FormatFunction("char", e.RExpr());

  public abstract StringBuilder VisitLike(SQLExpression e, bool negative = false);

  public abstract StringBuilder VisitEqual(SQLExpression e, StringComparison comparison);

  public abstract StringBuilder VisitCompare(SQLExpression e, StringComparison comparison);

  public abstract StringBuilder VisitCompareWithCulture(SQLExpression e);

  public abstract bool TryPlusSpecialCase(SQLExpression e);

  public abstract StringBuilder VisitToBase64(SQLExpression e);

  public virtual StringBuilder VisitCastAsDecimal(SQLExpression e)
  {
    SQLExpression sqlExpression = e.RExpr();
    this._res.Append("CAST( ");
    e.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(" AS DECIMAL(");
    sqlExpression.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append(", ");
    sqlExpression.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append("))");
    return this._res;
  }

  public virtual string GetAddSign(SQLExpression e) => "+";

  protected virtual StringBuilder FormatRoundFunction(SQLExpression source, SQLExpression n)
  {
    return this.FormatFunction("ROUND", source, n);
  }

  protected virtual StringBuilder FormatFunction(string name, params SQLExpression[] args)
  {
    this._res.Append(name).Append("( ");
    for (int index = 0; index < args.Length; ++index)
    {
      if (index > 0)
        this._res.Append(", ");
      args[index].Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    }
    return this._res.Append(")");
  }

  public virtual StringBuilder Visit(SQLSwitch c)
  {
    if (c.IsEmbraced())
      this._res.Append("(");
    this._res.Append("CASE ");
    foreach (Tuple<SQLExpression, SQLExpression> tuple in c.GetCases())
    {
      this._res.Append("WHEN ");
      tuple.Item1.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" THEN ");
      tuple.Item2.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" ");
    }
    if (c.RExpr() != null)
    {
      this._res.Append("ELSE ");
      c.RExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
      this._res.Append(" ");
    }
    this._res.Append("END");
    if (c.IsEmbraced())
      this._res.Append(")");
    return this._res;
  }

  public virtual StringBuilder Visit(Asterisk a) => this._res.Append("*");

  public virtual StringBuilder Visit(Column c)
  {
    if (c.Table() != null && !c.DoNotEnquoteWithTable)
    {
      int length = this._res.Length;
      c.Table().SQLAlias(this);
      if (this._res.Length == length)
        c.Table().SQLTableName(this);
      this._res.Append(".");
    }
    if (c.DoNotEnquote)
      this._res.Append(c.Name);
    else
      this._res.Append(this.kwPrefix_ + c.Name + this.kwSuffix_);
    return this._res;
  }

  public virtual StringBuilder Visit(Literal l)
  {
    if (l.Oper() == SQLExpression.Operation.EMPTY)
      return l.LExpr().Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) this);
    this._res.Append("@P");
    if (l.LExpr() == null)
      this._res.Append("0");
    else
      this._res.Append(l.LExpr().ToString());
    return this._res;
  }

  public StringBuilder Visit(SQLConst c)
  {
    object effectiveValue = c.GetValue();
    PXDbType dbType = c.GetDBType();
    return this.VisitConstValue(this.provider.CorrectConstValue(dbType, effectiveValue), dbType);
  }

  public abstract StringBuilder Visit(SQLGroupConcat exp);

  public abstract StringBuilder Visit(Md5Hash exp);

  public abstract StringBuilder Visit(SQLAggConcat exp);

  public virtual StringBuilder VisitConstValue(object value, PXDbType type)
  {
    if (!(value is IEnumerable enumerable))
      return this._res.Append(value.ToString());
    bool flag = true;
    foreach (object obj in enumerable)
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
}
