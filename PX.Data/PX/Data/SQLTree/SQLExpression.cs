// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLExpression
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

[DebuggerDisplay("{oper_}")]
public class SQLExpression : IEquatable<SQLExpression>
{
  protected SQLExpression lexpr_;
  protected SQLExpression rexpr_;
  protected SQLExpression.Operation oper_;
  private SQLExpression.Braces forceBraces_;
  private string alias_;
  private System.Type binding_;
  protected static readonly IList<PXDbType> _dbTypesLatin = (IList<PXDbType>) new PXDbType[3]
  {
    PXDbType.Text,
    PXDbType.VarChar,
    PXDbType.Char
  };
  /// <summary>
  /// Used MS-SQL data type precedence
  /// https://docs.microsoft.com/en-us/sql/t-sql/data-types/data-type-precedence-transact-sql?view=sql-server-2017
  /// </summary>
  private static readonly IList<PXDbType> _dbTypesPrecedences = (IList<PXDbType>) new PXDbType[28]
  {
    PXDbType.Udt,
    PXDbType.Unspecified,
    PXDbType.Variant,
    PXDbType.DirectExpression,
    PXDbType.Xml,
    PXDbType.DateTime,
    PXDbType.SmallDateTime,
    PXDbType.Float,
    PXDbType.Real,
    PXDbType.Decimal,
    PXDbType.Money,
    PXDbType.SmallMoney,
    PXDbType.BigInt,
    PXDbType.Int,
    PXDbType.SmallInt,
    PXDbType.TinyInt,
    PXDbType.Bit,
    PXDbType.NText,
    PXDbType.Text,
    PXDbType.Image,
    PXDbType.Timestamp,
    PXDbType.UniqueIdentifier,
    PXDbType.NVarChar,
    PXDbType.NChar,
    PXDbType.VarChar,
    PXDbType.Char,
    PXDbType.VarBinary,
    PXDbType.Binary
  };

  protected SQLExpression(SQLExpression other)
  {
    this.oper_ = other.oper_;
    this.lexpr_ = other.lexpr_?.Duplicate();
    this.rexpr_ = other.rexpr_?.Duplicate();
    this.forceBraces_ = other.forceBraces_;
    this.alias_ = other.alias_;
    this.binding_ = other.binding_;
  }

  protected SQLExpression(SQLExpression.Operation oper = SQLExpression.Operation.NONE)
  {
    this.oper_ = oper;
  }

  public virtual SQLExpression Duplicate() => new SQLExpression(this);

  public string Alias() => this.alias_;

  public virtual string AliasOrName() => this.Alias();

  internal virtual string getSurrogateAlias() => this.Alias();

  public SQLExpression SetAlias(string a) => this.As(a);

  public SQLExpression As(string a)
  {
    this.alias_ = a;
    return this;
  }

  internal SQLExpression SetColumnTableAliases(System.Type dac, string alias)
  {
    List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
    this.FillExpressionsOfType((Predicate<SQLExpression>) (e => e is Column column1 && column1.Table().AliasOrName() == dac.Name), sqlExpressionList);
    foreach (Column column2 in sqlExpressionList.Cast<Column>())
      column2.Table().Alias = alias;
    return this;
  }

  public SQLExpression BindTo(System.Type t)
  {
    this.binding_ = t;
    return this;
  }

  public SQLExpression.Operation Oper() => this.oper_;

  public SQLExpression LExpr() => this.lexpr_;

  public SQLExpression RExpr() => this.rexpr_;

  public SQLExpression Unembrace()
  {
    this.forceBraces_ = SQLExpression.Braces.CLEAR;
    return this;
  }

  public SQLExpression Embrace()
  {
    this.forceBraces_ = SQLExpression.Braces.FORCE;
    return this;
  }

  public bool IsEmbraced() => this.forceBraces_ == SQLExpression.Braces.FORCE;

  public bool IsUnembraced() => this.forceBraces_ == SQLExpression.Braces.CLEAR;

  internal static SQLExpression Empty() => new SQLExpression(SQLExpression.Operation.EMPTY);

  public static SQLExpression Null() => new SQLExpression(SQLExpression.Operation.NULL);

  public static SQLExpression GetFirstInSequence(SQLExpression exp)
  {
    if (exp.LExpr() != null)
      return SQLExpression.GetFirstInSequence(exp.LExpr());
    exp.SetRight((SQLExpression) null);
    return exp;
  }

  public override int GetHashCode()
  {
    int hashCode = this.oper_.GetHashCode();
    if (this.alias_ != null)
      hashCode ^= this.alias_.GetHashCode();
    return hashCode;
  }

  public override bool Equals(object other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    return !(this.GetType() != other.GetType()) && this.Equals(other as SQLExpression);
  }

  public virtual bool Equals(SQLExpression other)
  {
    return other != null && !(this.GetType() != other.GetType()) && (this == other || this.oper_ == other.oper_ && object.Equals((object) this.lexpr_, (object) other.lexpr_) && object.Equals((object) this.rexpr_, (object) other.rexpr_) && this.alias_ == other.alias_);
  }

  internal virtual SQLExpression substituteConstant(string from, string to)
  {
    if (this.lexpr_ != null)
      this.lexpr_.substituteConstant(from, to);
    if (this.rexpr_ != null)
      this.rexpr_.substituteConstant(from, to);
    return this;
  }

  internal virtual SQLExpression substituteTableName(string from, string to)
  {
    this.lexpr_?.substituteTableName(from, to);
    this.rexpr_?.substituteTableName(from, to);
    return this;
  }

  internal virtual SQLExpression substituteNode(SQLExpression from, SQLExpression to)
  {
    if (this.Equals(from))
      return to;
    if (this.lexpr_ != null)
    {
      if (this.lexpr_.Equals(from))
        this.lexpr_ = to;
      else
        this.lexpr_.substituteNode(from, to);
    }
    if (this.rexpr_ != null)
    {
      if (this.rexpr_.Equals(from))
        this.rexpr_ = to;
      else
        this.rexpr_.substituteNode(from, to);
    }
    return this;
  }

  internal virtual bool CanBeFlattened(
    Dictionary<string, SQRenamer> aliases,
    bool insideAggregate = false,
    bool isMaxAggregate = false,
    Func<Column, bool> isInsideGroupBy = null)
  {
    if (insideAggregate && this.IsFormula())
      return this.ItCouldBeFlattenedSubQuery(aliases) || this.ItCouldBeFlattenedFormula(aliases);
    SQLExpression lexpr = this.lexpr_;
    if ((lexpr != null ? (lexpr.CanBeFlattened(aliases, insideAggregate, isMaxAggregate, isInsideGroupBy) ? 1 : 0) : 1) == 0)
      return false;
    SQLExpression rexpr = this.rexpr_;
    return rexpr == null || rexpr.CanBeFlattened(aliases, insideAggregate, isMaxAggregate, isInsideGroupBy);
  }

  private bool ItCouldBeFlattenedFormula(Dictionary<string, SQRenamer> aliases)
  {
    if (this.lexpr_ is Column && this.rexpr_ is SQLConst)
      return this.lexpr_.CanBeFlattened(aliases, true);
    return this.lexpr_ is SQLConst && this.rexpr_ is Column && this.rexpr_.CanBeFlattened(aliases, true);
  }

  private bool ItCouldBeFlattenedSubQuery(Dictionary<string, SQRenamer> aliases)
  {
    return this.lexpr_ is SubQuery || this.rexpr_ is SubQuery || IsSubQuery(this.lexpr_ as Column) || IsSubQuery(this.rexpr_ as Column);

    bool IsSubQuery(Column column)
    {
      if (column == null)
        return false;
      string key = column.Table()?.AliasOrName();
      SQRenamer sqRenamer;
      SQLExpression sqlExpression;
      return key != null && aliases.TryGetValue(key, out sqRenamer) && sqRenamer.extCol2Int.TryGetValue(column.Name, out sqlExpression) && sqlExpression is SubQuery;
    }
  }

  internal virtual bool TrySplitByAnd(List<SQLExpression> list)
  {
    if (this.oper_ != SQLExpression.Operation.AND)
      return InternalTrySplitByAnd();
    SQLExpression lexpr = this.lexpr_;
    if ((lexpr != null ? (lexpr.TrySplitByAnd(list) ? 1 : 0) : 1) == 0)
      return false;
    SQLExpression rexpr = this.rexpr_;
    return rexpr == null || rexpr.TrySplitByAnd(list);

    bool InternalTrySplitByAnd()
    {
      if (!SQLExpression.OperIsSimpleCondition(this.oper_))
        return false;
      SQLExpression lexpr = this.lexpr_;
      if ((lexpr != null ? (lexpr.oper_ == SQLExpression.Operation.AND ? 1 : 0) : 0) != 0)
        return AddAndSplitNext(this.lexpr_.rexpr_, this.rexpr_, this.lexpr_.lexpr_);
      SQLExpression rexpr = this.rexpr_;
      if ((rexpr != null ? (rexpr.oper_ == SQLExpression.Operation.AND ? 1 : 0) : 0) != 0)
        return AddAndSplitNext(this.lexpr_, this.rexpr_.lexpr_, this.rexpr_.rexpr_);
      list.Add(this);
      return true;
    }

    bool AddAndSplitNext(SQLExpression left, SQLExpression right, SQLExpression next)
    {
      list.Add(new SQLExpression(this.oper_)
      {
        lexpr_ = left,
        rexpr_ = right
      });
      return next.TrySplitByAnd(list);
    }
  }

  private static bool OperIsSimpleCondition(SQLExpression.Operation oper)
  {
    return EnumerableExtensions.IsIn<SQLExpression.Operation>(oper, SQLExpression.Operation.EQ, SQLExpression.Operation.GT, SQLExpression.Operation.GE, SQLExpression.Operation.LT, SQLExpression.Operation.LE, new SQLExpression.Operation[1]
    {
      SQLExpression.Operation.NE
    });
  }

  /// <summary>Changes table prefix of columns.</summary>
  internal virtual SQLExpression substituteColumnAliases(Dictionary<string, string> dict)
  {
    this.lexpr_?.substituteColumnAliases(dict);
    this.rexpr_?.substituteColumnAliases(dict);
    return this;
  }

  /// <summary>Substitutes column with whole new expression</summary>
  internal virtual SQLExpression substituteExternalColumnAliases(
    Dictionary<string, SQRenamer> aliases,
    QueryPart queryPart,
    bool insideAggregate = false,
    HashSet<string> excludes = null,
    bool isMaxAggregate = false,
    Func<Column, bool> isInsideGroupBy = null)
  {
    if (this.IsAggregate())
    {
      if (insideAggregate)
        throw new PXCannotPutScalarIntoAggregateException();
      SQLExpression sqlExpression = this.oper_ != SQLExpression.Operation.AGG_CONCAT ? (this.rexpr_ = this.rexpr_?.substituteExternalColumnAliases(aliases, queryPart, true, excludes, this.oper_ == SQLExpression.Operation.MAX, isInsideGroupBy)) : (this.lexpr_ = this.lexpr_?.substituteExternalColumnAliases(aliases, queryPart, true, excludes, this.oper_ == SQLExpression.Operation.MAX, isInsideGroupBy));
      return sqlExpression != null && sqlExpression.oper_ == SQLExpression.Operation.NULL || sqlExpression != null && sqlExpression.oper_ == SQLExpression.Operation.SUB_QUERY ? sqlExpression : this;
    }
    this.lexpr_ = this.lexpr_?.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy);
    this.rexpr_ = this.rexpr_?.substituteExternalColumnAliases(aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy);
    return this;
  }

  public StringBuilder SQLQuery(Connection conn)
  {
    return this.Accept<StringBuilder>((ISQLExpressionVisitor<StringBuilder>) conn.CreateSQLWriter());
  }

  public override string ToString()
  {
    string str1 = this.lexpr_ == null ? "" : this.lexpr_.ToString();
    string str2 = this.rexpr_ == null ? "" : this.rexpr_.ToString();
    return $"{(this.IsEmbraced() ? "(" : "")}{str1} {this.oper_.ToString()} {str2}{(this.IsEmbraced() ? ")" : "")}";
  }

  internal SQLExpression SetLeft(SQLExpression e)
  {
    this.lexpr_ = e;
    return this;
  }

  internal SQLExpression SetRight(SQLExpression e)
  {
    this.rexpr_ = e;
    return this;
  }

  internal SQLExpression SetOper(SQLExpression.Operation o)
  {
    this.oper_ = o;
    return this;
  }

  internal virtual bool IsBoolType()
  {
    switch (this.oper_)
    {
      case SQLExpression.Operation.EQ:
      case SQLExpression.Operation.NE:
      case SQLExpression.Operation.GE:
      case SQLExpression.Operation.GT:
      case SQLExpression.Operation.LT:
      case SQLExpression.Operation.LE:
      case SQLExpression.Operation.AND:
      case SQLExpression.Operation.OR:
      case SQLExpression.Operation.NOT:
      case SQLExpression.Operation.IN:
      case SQLExpression.Operation.NOT_IN:
      case SQLExpression.Operation.LIKE:
      case SQLExpression.Operation.NOT_LIKE:
      case SQLExpression.Operation.IS_NOT_NULL:
      case SQLExpression.Operation.IS_NULL:
      case SQLExpression.Operation.EXISTS:
      case SQLExpression.Operation.NOT_EXISTS:
      case SQLExpression.Operation.BETWEEN:
      case SQLExpression.Operation.NOT_BETWEEN:
      case SQLExpression.Operation.CONTAINS:
      case SQLExpression.Operation.FREETEXT:
        return true;
      default:
        return false;
    }
  }

  internal bool IsLatin()
  {
    if (!SQLExpression._dbTypesLatin.Contains(this.GetDBType()))
    {
      SQLExpression rexpr = this.rexpr_;
      if ((rexpr != null ? (rexpr.IsLatin() ? 1 : 0) : 0) == 0)
      {
        SQLExpression lexpr = this.lexpr_;
        return lexpr != null && lexpr.IsLatin();
      }
    }
    return true;
  }

  internal virtual PXDbType GetDBType() => PXDatabase.Provider.SqlDialect.GetDBType(this);

  protected internal static PXDbType GetMaxByPrecedence(params PXDbType?[] types)
  {
    return SQLExpression.GetMaxByPrecedence(((IEnumerable<PXDbType?>) types).Where<PXDbType?>((Func<PXDbType?, bool>) (t => t.HasValue)).Cast<PXDbType>().ToArray<PXDbType>());
  }

  protected internal static PXDbType GetMaxByPrecedence(params PXDbType[] types)
  {
    if (!((IEnumerable<PXDbType>) types).Any<PXDbType>())
      return PXDbType.Unspecified;
    int index = ((IEnumerable<PXDbType>) types).Min<PXDbType>((Func<PXDbType, int>) (t => SQLExpression._dbTypesPrecedences.IndexOf(t)));
    return SQLExpression._dbTypesPrecedences[index];
  }

  public bool IsAggregate() => SQLExpression.IsAggregate(this.oper_);

  internal static bool IsAggregate(SQLExpression.Operation oper)
  {
    switch (oper)
    {
      case SQLExpression.Operation.MAX:
      case SQLExpression.Operation.MIN:
      case SQLExpression.Operation.AVG:
      case SQLExpression.Operation.SUM:
      case SQLExpression.Operation.COUNT:
      case SQLExpression.Operation.COUNT_DISTINCT:
      case SQLExpression.Operation.AGG_CONCAT:
        return true;
      default:
        return false;
    }
  }

  protected bool IsFormula()
  {
    switch (this.oper_)
    {
      case SQLExpression.Operation.PLUS:
      case SQLExpression.Operation.CONCAT:
      case SQLExpression.Operation.MUL:
      case SQLExpression.Operation.DIV:
      case SQLExpression.Operation.MINUS:
      case SQLExpression.Operation.SEQ:
      case SQLExpression.Operation.BIT_AND:
      case SQLExpression.Operation.BIT_OR:
      case SQLExpression.Operation.BIT_NOT:
        return true;
      default:
        return false;
    }
  }

  internal bool DoesContainAggregate()
  {
    List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
    this.FillExpressionsOfType((Predicate<SQLExpression>) (e => e.IsAggregate()), sqlExpressionList);
    return sqlExpressionList.Any<SQLExpression>();
  }

  internal static SQLExpression GetRowSharingExpression(
    companySetting setting,
    Column companyColumn,
    Column companyMaskColumn,
    int companyId)
  {
    return SQLExpression.GetRowSharingExpression(setting, companyColumn, companyMaskColumn, false, companyId, true, out bool _);
  }

  internal static SQLExpression GetRowSharingExpression(
    companySetting setting,
    Column companyColumn,
    Column companyMaskColumn,
    bool isKvExt,
    int companyId,
    bool rowSharingAllowed,
    out bool needKvExtOrdering)
  {
    needKvExtOrdering = false;
    int[] selectables;
    if (!(PXDatabase.Provider is PXDatabaseProviderBase provider) || setting.Flag == companySetting.companyFlag.Separate || !rowSharingAllowed || !provider.tryGetSelectableCompanies(companyId, out selectables))
      return SQLExpressionExt.EQ(companyColumn, (SQLExpression) new SQLConst((object) companyId)).Embrace();
    SQLExpression sqlExpression1 = SQLExpression.None();
    foreach (int v in selectables)
      sqlExpression1 = sqlExpression1.Seq((SQLExpression) new SQLConst((object) v));
    SQLExpression exp = sqlExpression1.Seq((SQLExpression) new SQLConst((object) companyId));
    SQLExpression sqlExpression2 = companyColumn.In(exp);
    if (!isKvExt)
      sqlExpression2 = sqlExpression2.And(SQLExpression.BinaryMaskTestExpr((SQLExpression) companyMaskColumn, companyId, 2));
    SQLExpression sharingExpression = sqlExpression2.Embrace();
    needKvExtOrdering = true;
    return sharingExpression;
  }

  public static SQLExpression None() => new SQLExpression();

  public static SQLExpression Count() => new SQLExpression(SQLExpression.Operation.COUNT);

  public static SQLExpression Count(SQLExpression expr)
  {
    return SQLExpression.Aggregate(SQLExpression.Operation.COUNT, expr);
  }

  public static SQLExpression CountDistinct(SQLExpression column)
  {
    return SQLExpression.Aggregate(SQLExpression.Operation.COUNT_DISTINCT, column);
  }

  internal static SQLExpression Aggregate(SQLExpression.Operation op, SQLExpression expr)
  {
    if (expr != null && (expr.oper_ == SQLExpression.Operation.NONE || expr.oper_ == SQLExpression.Operation.NULL) && expr.GetType() == typeof (SQLExpression))
      return expr;
    return new SQLExpression(op) { rexpr_ = expr };
  }

  internal SQLExpression UnwrapAggregate() => !this.IsAggregate() ? this : this.rexpr_;

  public static SQLExpression Now() => new SQLExpression(SQLExpression.Operation.DATE_NOW);

  public static SQLExpression NowUtc() => new SQLExpression(SQLExpression.Operation.DATE_UTC_NOW);

  public static SQLExpression Today() => new SQLExpression(SQLExpression.Operation.TODAY);

  public static SQLExpression TodayUtc() => new SQLExpression(SQLExpression.Operation.TODAY_UTC);

  public SQLExpression GetTime() => this.UnaryOperation(SQLExpression.Operation.GET_TIME);

  private SQLExpression BinaryOper(SQLExpression.Operation op, SQLExpression r)
  {
    if (r == null)
      return this;
    if (this.oper_ == SQLExpression.Operation.NONE && this.GetType() == typeof (SQLExpression))
      return r;
    return r.oper_ == SQLExpression.Operation.NONE && r.GetType() == typeof (SQLExpression) ? this : this.BinaryFunc(op, r);
  }

  private SQLExpression BinaryFunc(SQLExpression.Operation op, SQLExpression r)
  {
    return SQLExpression.BinaryFunc(op, this, r);
  }

  private static SQLExpression BinaryFunc(
    SQLExpression.Operation op,
    SQLExpression l,
    SQLExpression r)
  {
    return new SQLExpression(op) { lexpr_ = l, rexpr_ = r };
  }

  public static SQLExpression IsTrue(bool value)
  {
    return SQLExpressionExt.EQ(new SQLConst((object) 1), value ? (SQLExpression) new SQLConst((object) 1) : (SQLExpression) new SQLConst((object) 0));
  }

  public static SQLExpression Not(SQLExpression r)
  {
    return SQLExpression.UnaryOperation(SQLExpression.Operation.NOT, r);
  }

  public static SQLExpression Negate(SQLExpression r)
  {
    return SQLExpression.UnaryOperation(SQLExpression.Operation.UMINUS, r?.Embrace());
  }

  public static SQLExpression ConcatSequence(SQLExpression seq)
  {
    return (SQLExpression) new SQLConcat(seq);
  }

  public SQLExpression BitAnd(SQLExpression r)
  {
    return this.BinaryOper(SQLExpression.Operation.BIT_AND, r);
  }

  public SQLExpression BitOr(SQLExpression r) => this.BinaryOper(SQLExpression.Operation.BIT_OR, r);

  public SQLExpression BitNot() => this.UnaryOperation(SQLExpression.Operation.BIT_NOT);

  public SQLExpression LT(SQLExpression r) => this.BinaryOper(SQLExpression.Operation.LT, r);

  public SQLExpression LE(SQLExpression r) => this.BinaryOper(SQLExpression.Operation.LE, r);

  public SQLExpression GT(SQLExpression r) => this.BinaryOper(SQLExpression.Operation.GT, r);

  public SQLExpression GE(SQLExpression r) => this.BinaryOper(SQLExpression.Operation.GE, r);

  internal SQLExpression Between(SQLExpression args)
  {
    return this.BinaryOper(SQLExpression.Operation.BETWEEN, args);
  }

  internal static SQLExpression BetweenArgs(SQLExpression min, SQLExpression max)
  {
    return new SQLExpression(SQLExpression.Operation.BETWEEN_ARGS)
    {
      lexpr_ = min,
      rexpr_ = max
    };
  }

  public SQLExpression BinaryLength() => this.UnaryOperation(SQLExpression.Operation.BINARY_LEN);

  public SQLExpression Length() => this.UnaryOperation(SQLExpression.Operation.LEN);

  public SQLExpression IsNumeric() => this.UnaryOperation(SQLExpression.Operation.IS_NUMERIC);

  public SQLExpression Ascii() => this.UnaryOperation(SQLExpression.Operation.ASCII);

  public SQLExpression ToChar() => this.UnaryOperation(SQLExpression.Operation.CHAR);

  public SQLExpression ToBase64() => this.UnaryOperation(SQLExpression.Operation.TO_BASE64);

  public SQLExpression Max() => SQLExpression.Aggregate(SQLExpression.Operation.MAX, this);

  public SQLExpression Min() => SQLExpression.Aggregate(SQLExpression.Operation.MIN, this);

  public SQLExpression Avg() => SQLExpression.Aggregate(SQLExpression.Operation.AVG, this);

  public SQLExpression Sum() => SQLExpression.Aggregate(SQLExpression.Operation.SUM, this);

  public SQLExpression StringAgg(SQLConst separator = null)
  {
    return separator != null ? (SQLExpression) new SQLAggConcat(separator, this) : (SQLExpression) new SQLAggConcat((IConstant<string>) new CommaSpace(), this);
  }

  public SQLExpression Abs() => this.UnaryOperation(SQLExpression.Operation.ABS);

  public SQLExpression Ceiling() => this.UnaryOperation(SQLExpression.Operation.CEILING);

  public SQLExpression Floor() => this.UnaryOperation(SQLExpression.Operation.FLOOR);

  public SQLExpression CharIndex(SQLExpression expressionToFind)
  {
    return this.BinaryOper(SQLExpression.Operation.CHARINDEX, expressionToFind);
  }

  public SQLExpression Reverse() => this.UnaryOperation(SQLExpression.Operation.REVERSE);

  public SQLExpression LowerCase() => this.UnaryOperation(SQLExpression.Operation.LOWER);

  public SQLExpression UpperCase() => this.UnaryOperation(SQLExpression.Operation.UPPER);

  public SQLExpression Left(SQLExpression count)
  {
    return this.BinaryOper(SQLExpression.Operation.LEFT, count);
  }

  public SQLExpression Right(SQLExpression count)
  {
    return this.BinaryOper(SQLExpression.Operation.RIGHT, count);
  }

  public SQLExpression LTrim() => this.UnaryOperation(SQLExpression.Operation.LTRIM);

  public SQLExpression RTrim() => this.UnaryOperation(SQLExpression.Operation.RTRIM);

  public SQLExpression Trim() => this.UnaryOperation(SQLExpression.Operation.TRIM);

  public SQLExpression Repeat(SQLExpression count)
  {
    return this.BinaryOper(SQLExpression.Operation.REPEAT, count);
  }

  public SQLExpression Power(SQLExpression count)
  {
    return this.BinaryOper(SQLExpression.Operation.POWER, count);
  }

  public SQLExpression Replace(SQLExpression toReplace, SQLExpression replaceWith)
  {
    return this.BinaryOper(SQLExpression.Operation.REPLACE, new SQLExpression(SQLExpression.Operation.EMPTY)
    {
      lexpr_ = toReplace,
      rexpr_ = replaceWith
    });
  }

  public SQLExpression Round(SQLExpression length)
  {
    return this.BinaryOper(SQLExpression.Operation.ROUND, length);
  }

  public SQLExpression Like(SQLExpression pattern)
  {
    return this.BinaryOper(SQLExpression.Operation.LIKE, pattern);
  }

  public SQLExpression NotLike(SQLExpression pattern)
  {
    return this.BinaryOper(SQLExpression.Operation.NOT_LIKE, pattern);
  }

  public SQLExpression Equal(SQLExpression pattern, StringComparison comparison = StringComparison.Ordinal)
  {
    switch (comparison)
    {
      case StringComparison.CurrentCulture:
        return this.BinaryOper(SQLExpression.Operation.EQUAL_CC_CS, pattern);
      case StringComparison.CurrentCultureIgnoreCase:
        return this.BinaryOper(SQLExpression.Operation.EQUAL_CC_CI, pattern);
      case StringComparison.InvariantCulture:
        return this.BinaryOper(SQLExpression.Operation.EQUAL_I_CS, pattern);
      case StringComparison.InvariantCultureIgnoreCase:
        return this.BinaryOper(SQLExpression.Operation.EQUAL_I_CI, pattern);
      case StringComparison.Ordinal:
        return this.BinaryOper(SQLExpression.Operation.EQUAL_O_CS, pattern);
      case StringComparison.OrdinalIgnoreCase:
        return this.BinaryOper(SQLExpression.Operation.EQUAL_O_CI, pattern);
      default:
        throw new PXNotSupportedException($"Method String.Equals is not supported for {comparison} comparer");
    }
  }

  public SQLExpression Compare(SQLExpression expression, bool ignoreCase, CultureInfo ci)
  {
    return this.BinaryOper(SQLExpression.Operation.COMPARE_CULTUREINFO, new SQLExpression(SQLExpression.Operation.EMPTY)
    {
      lexpr_ = expression,
      rexpr_ = new SQLExpression()
      {
        lexpr_ = (SQLExpression) new SQLConst((object) ignoreCase),
        rexpr_ = (SQLExpression) new SQLConst((object) ci.Name)
      }
    });
  }

  public SQLExpression Compare(SQLExpression pattern, StringComparison? comparison = null)
  {
    if (!comparison.HasValue)
      comparison = new StringComparison?(StringComparison.InvariantCulture);
    switch (comparison.Value)
    {
      case StringComparison.CurrentCulture:
        return this.BinaryOper(SQLExpression.Operation.COMPARE_CC_CS, pattern);
      case StringComparison.CurrentCultureIgnoreCase:
        return this.BinaryOper(SQLExpression.Operation.COMPARE_CC_CI, pattern);
      case StringComparison.InvariantCulture:
        return this.BinaryOper(SQLExpression.Operation.COMPARE_I_CS, pattern);
      case StringComparison.InvariantCultureIgnoreCase:
        return this.BinaryOper(SQLExpression.Operation.COMPARE_I_CI, pattern);
      case StringComparison.Ordinal:
        return this.BinaryOper(SQLExpression.Operation.COMPARE_O_CS, pattern);
      case StringComparison.OrdinalIgnoreCase:
        return this.BinaryOper(SQLExpression.Operation.COMPARE_O_CI, pattern);
      default:
        throw new PXNotSupportedException($"Method String.Equals is not supported for {comparison} comparer");
    }
  }

  protected SQLExpression UnaryOperation(SQLExpression.Operation op)
  {
    return SQLExpression.UnaryOperation(op, this);
  }

  protected static SQLExpression UnaryOperation(SQLExpression.Operation op, SQLExpression operand)
  {
    return new SQLExpression()
    {
      oper_ = op,
      rexpr_ = operand
    };
  }

  public static SQLExpression operator +(SQLExpression left, SQLExpression right)
  {
    return new SQLExpression(SQLExpression.Operation.PLUS)
    {
      lexpr_ = left,
      rexpr_ = right
    };
  }

  public static SQLExpression operator -(SQLExpression left, SQLExpression right)
  {
    return new SQLExpression(SQLExpression.Operation.MINUS)
    {
      lexpr_ = left,
      rexpr_ = right
    };
  }

  public static SQLExpression operator -(SQLExpression exp)
  {
    return new SQLExpression(SQLExpression.Operation.UMINUS)
    {
      rexpr_ = exp
    };
  }

  public static SQLExpression operator /(SQLExpression left, SQLExpression right)
  {
    return new SQLExpression(SQLExpression.Operation.DIV)
    {
      lexpr_ = left,
      rexpr_ = right
    };
  }

  public static SQLExpression operator %(SQLExpression left, SQLExpression right)
  {
    return new SQLExpression(SQLExpression.Operation.MOD)
    {
      lexpr_ = left,
      rexpr_ = right
    };
  }

  public static SQLExpression operator *(SQLExpression left, SQLExpression right)
  {
    return new SQLExpression(SQLExpression.Operation.MUL)
    {
      lexpr_ = left,
      rexpr_ = right
    };
  }

  public SQLExpression CastAsInteger()
  {
    return new SQLExpression(SQLExpression.Operation.CAST_AS_INTEGER)
    {
      lexpr_ = this
    };
  }

  public SQLExpression CastAsDecimal(int precision, int scale)
  {
    return this.BinaryFunc(SQLExpression.Operation.CAST_AS_DECIMAL, new SQLExpression()
    {
      lexpr_ = (SQLExpression) new SQLConst((object) precision),
      rexpr_ = (SQLExpression) new SQLConst((object) scale)
    });
  }

  public SQLExpression CastAsVarBinary()
  {
    return new SQLExpression(SQLExpression.Operation.CAST_AS_VARBINARY)
    {
      lexpr_ = this
    };
  }

  public SQLExpression TrimEnd() => this.UnaryOperation(SQLExpression.Operation.RTRIM);

  public SQLExpression TrimStart() => this.UnaryOperation(SQLExpression.Operation.LTRIM);

  public SQLExpression Round(int precision)
  {
    return this.BinaryFunc(SQLExpression.Operation.ROUND, (SQLExpression) new SQLConst((object) precision));
  }

  public SQLExpression Between(SQLExpression min, SQLExpression max)
  {
    return this.Between(SQLExpression.BetweenArgs(min, max));
  }

  public SQLExpression NotBetween(SQLExpression min, SQLExpression max)
  {
    return this.BinaryFunc(SQLExpression.Operation.NOT_BETWEEN, SQLExpression.BetweenArgs(min, max));
  }

  public SQLExpression Substr(SQLExpression pos, SQLExpression len)
  {
    return this.BinaryFunc(SQLExpression.Operation.SUBSTR, new SQLExpression()
    {
      lexpr_ = pos,
      rexpr_ = len ?? (SQLExpression) new SQLConst((object) int.MaxValue)
    });
  }

  public SQLExpression Substr(SQLExpression pos, uint len)
  {
    return this.Substr(pos, (SQLExpression) new SQLConst((object) len));
  }

  public SQLExpression Substr(uint pos, uint len)
  {
    return this.Substr((SQLExpression) new SQLConst((object) pos), (SQLExpression) new SQLConst((object) len));
  }

  public SQLExpression NullIf(SQLExpression p)
  {
    return this.BinaryFunc(SQLExpression.Operation.NULL_IF, p);
  }

  public SQLExpression Coalesce(SQLExpression other)
  {
    return this.BinaryFunc(SQLExpression.Operation.ISNULL_FUNC, other);
  }

  public SQLExpression ConvertBinToInt(uint pos, uint len)
  {
    return this.BinaryFunc(SQLExpression.Operation.CONVERT_BIN_TO_INT, new SQLExpression()
    {
      lexpr_ = (SQLExpression) new SQLConst((object) pos),
      rexpr_ = (SQLExpression) new SQLConst((object) len)
    });
  }

  public SQLExpression Seq(object c)
  {
    return this.BinaryOper(SQLExpression.Operation.SEQ, (SQLExpression) new SQLConst(c));
  }

  public SQLExpression Seq(SQLExpression r)
  {
    if (r == null || r.oper_ != SQLExpression.Operation.SEQ)
      return this.BinaryOper(SQLExpression.Operation.SEQ, r);
    if (this.oper_ == SQLExpression.Operation.NONE && this.GetType() == typeof (SQLExpression))
      return r;
    SQLExpression sqlExpression1 = r;
    while (sqlExpression1.lexpr_ != null && sqlExpression1.lexpr_.oper_ == SQLExpression.Operation.SEQ)
      sqlExpression1 = sqlExpression1.lexpr_;
    if (sqlExpression1.lexpr_ == null)
    {
      sqlExpression1.lexpr_ = this;
    }
    else
    {
      SQLExpression sqlExpression2 = this.BinaryOper(SQLExpression.Operation.SEQ, sqlExpression1.lexpr_);
      sqlExpression1.lexpr_ = sqlExpression2;
    }
    return r;
  }

  public SQLExpression In(SQLExpression exp)
  {
    if (exp is SubQuery subQuery)
    {
      Query query = subQuery.Query();
      if (query.GetLimit() == 0)
        query.Limit(-1);
    }
    return this.BinaryFunc(SQLExpression.Operation.IN, exp);
  }

  public SQLExpression In(IEnumerable<SQLExpression> items)
  {
    SQLExpression exp = (SQLExpression) null;
    foreach (SQLExpression r in items)
      exp = exp != null ? exp.Seq(r) : r;
    return this.In(exp);
  }

  public SQLExpression In(Query q)
  {
    if (q.GetLimit() == 0)
      q.Limit(-1);
    return this.BinaryFunc(SQLExpression.Operation.IN, (SQLExpression) new SubQuery(q));
  }

  public SQLExpression NotIn(SQLExpression exp)
  {
    if (exp is SubQuery subQuery)
    {
      Query query = subQuery.Query();
      if (query.GetLimit() == 0)
        query.Limit(-1);
    }
    return this.BinaryOper(SQLExpression.Operation.NOT_IN, exp);
  }

  public SQLExpression NotIn(IEnumerable<SQLExpression> items)
  {
    SQLExpression exp = (SQLExpression) null;
    foreach (SQLExpression r in items)
      exp = exp != null ? exp.Seq(r) : r;
    return this.NotIn(exp);
  }

  public SQLExpression NotIn(Query q)
  {
    if (q.GetLimit() == 0)
      q.Limit(-1);
    return this.NotIn((SQLExpression) new SubQuery(q));
  }

  public SQLExpression InsideBranch()
  {
    return this.BinaryFunc(SQLExpression.Operation.INSIDE_BRANCH, (SQLExpression) null);
  }

  public SQLExpression RestrictByUserBranches()
  {
    return this.BinaryFunc(SQLExpression.Operation.RESTRICT_BYUSERBRANCHES, (SQLExpression) null);
  }

  public SQLExpression IsNull()
  {
    return new SQLExpression(SQLExpression.Operation.IS_NULL)
    {
      lexpr_ = this
    };
  }

  public SQLExpression IsNotNull()
  {
    return new SQLExpression(SQLExpression.Operation.IS_NOT_NULL)
    {
      lexpr_ = this
    };
  }

  public SQLExpression And(SQLExpression r) => this.BinaryOper(SQLExpression.Operation.AND, r);

  public SQLExpression Or(SQLExpression r) => this.BinaryOper(SQLExpression.Operation.OR, r);

  public static SQLExpression And(IEnumerable<SQLExpression> items)
  {
    return (SQLExpression) SQLMultiOperation.CreateAnd(items.ToList<SQLExpression>());
  }

  public static SQLExpression Or(IEnumerable<SQLExpression> items)
  {
    return (SQLExpression) SQLMultiOperation.CreateOr(items.ToList<SQLExpression>());
  }

  public bool IsNullExpression() => this.oper_ == SQLExpression.Operation.NULL;

  public static SQLExpression EQ(SQLExpression l, SQLExpression r)
  {
    return SQLExpression.BinaryFunc(SQLExpression.Operation.EQ, l, r);
  }

  public static SQLExpression EQ(System.Type leftField, System.Type rightField)
  {
    return SQLExpression.BinaryFunc(SQLExpression.Operation.EQ, (SQLExpression) new Column(leftField), (SQLExpression) new Column(rightField));
  }

  public static SQLExpression NE(SQLExpression l, SQLExpression r)
  {
    return SQLExpression.BinaryFunc(SQLExpression.Operation.NE, l, r);
  }

  public static SQLExpression NE(System.Type leftField, System.Type rightField)
  {
    return SQLExpression.BinaryFunc(SQLExpression.Operation.NE, (SQLExpression) new Column(leftField), (SQLExpression) new Column(rightField));
  }

  public static SQLExpression BinaryMaskTestExpr(SQLExpression col, int company, int rights)
  {
    int pos = (company + 3) / 4;
    int num = 2 * ((company + 3) % 4);
    int v = rights << num;
    return SQLExpressionExt.EQ(new SQLConst((object) v), col.Substr((uint) pos, 1U).Ascii().BitAnd((SQLExpression) new SQLConst((object) v)));
  }

  public SQLExpression DatePart(IConstant<string> uom)
  {
    return (SQLExpression) new SQLDatePart(uom, this);
  }

  public static SQLExpression DatePartByTimeZone(
    IConstant<string> datePart,
    SQLExpression dataField,
    PXTimeZoneInfo destinationTimeZone)
  {
    return (SQLExpression) new SQLDatePart(datePart, (SQLExpression) new SQLDateByTimeZone(dataField, destinationTimeZone));
  }

  internal List<SQLExpression> GetExpressionsOfType(SQLExpression.Operation oper)
  {
    List<SQLExpression> res = new List<SQLExpression>();
    this.FillExpressionsOfType((Predicate<SQLExpression>) (e => e.oper_ == oper), res);
    return res;
  }

  internal List<T> GetExpressionsOfType<T>() where T : SQLExpression
  {
    List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
    this.FillExpressionsOfType((Predicate<SQLExpression>) (e => e is T), sqlExpressionList);
    return sqlExpressionList.Cast<T>().ToList<T>();
  }

  internal virtual void FillExpressionsOfType(
    Predicate<SQLExpression> predicate,
    List<SQLExpression> res)
  {
    this.lexpr_?.FillExpressionsOfType(predicate, res);
    if (predicate(this))
      res.Add(this);
    this.rexpr_?.FillExpressionsOfType(predicate, res);
  }

  internal virtual SQLExpression addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    this.lexpr_?.addExternalAliasToTableNames(alias, aliases, excludes);
    this.rexpr_?.addExternalAliasToTableNames(alias, aliases, excludes);
    return this;
  }

  internal virtual SQLExpression unembraceAnds()
  {
    if (this.lexpr_ != null)
      this.lexpr_.unembraceAnds();
    if (this.oper_ == SQLExpression.Operation.AND && this.lexpr_ != null && this.rexpr_ != null)
    {
      SQLExpression lexpr = this.lexpr_;
      if ((lexpr != null ? (lexpr.oper_ == SQLExpression.Operation.AND ? 1 : 0) : 0) != 0)
        this.lexpr_.forceBraces_ = SQLExpression.Braces.NATURAL;
      SQLExpression rexpr = this.rexpr_;
      if ((rexpr != null ? (rexpr.oper_ == SQLExpression.Operation.AND ? 1 : 0) : 0) != 0)
        this.rexpr_.forceBraces_ = SQLExpression.Braces.NATURAL;
    }
    if (this.rexpr_ != null)
      this.rexpr_.unembraceAnds();
    return this;
  }

  internal virtual SQLExpression unembraceOrs()
  {
    if (this.oper_ == SQLExpression.Operation.OR)
    {
      SQLExpression lexpr = this.lexpr_;
      if ((lexpr != null ? (lexpr.oper_ == SQLExpression.Operation.OR ? 1 : 0) : 0) != 0 && this.lexpr_.forceBraces_ == SQLExpression.Braces.NATURAL)
        this.lexpr_.forceBraces_ = SQLExpression.Braces.CLEAR;
      SQLExpression rexpr = this.rexpr_;
      if ((rexpr != null ? (rexpr.oper_ == SQLExpression.Operation.OR ? 1 : 0) : 0) != 0 && this.rexpr_.forceBraces_ == SQLExpression.Braces.NATURAL)
        this.rexpr_.forceBraces_ = SQLExpression.Braces.CLEAR;
    }
    if (this.lexpr_ != null)
      this.lexpr_.unembraceOrs();
    if (this.rexpr_ != null)
      this.rexpr_.unembraceOrs();
    return this;
  }

  protected internal virtual SQLExpression.CLType isConstantOrLiteral()
  {
    if (this.oper_ == SQLExpression.Operation.IS_NULL || this.oper_ == SQLExpression.Operation.IS_NOT_NULL)
    {
      SQLExpression lexpr = this.lexpr_;
      return lexpr == null ? SQLExpression.CLType.OTHER : lexpr.isConstantOrLiteral();
    }
    if (this.oper_ == SQLExpression.Operation.NONE)
      return SQLExpression.CLType.OTHER;
    SQLExpression lexpr1 = this.lexpr_;
    SQLExpression.CLType clType1 = lexpr1 != null ? lexpr1.isConstantOrLiteral() : SQLExpression.CLType.OTHER;
    SQLExpression rexpr = this.rexpr_;
    SQLExpression.CLType clType2 = rexpr != null ? rexpr.isConstantOrLiteral() : SQLExpression.CLType.OTHER;
    return clType2 <= clType1 ? clType1 : clType2;
  }

  internal virtual object evaluateConstant()
  {
    return this.evaluateConstant(this.lexpr_?.evaluateConstant(), this.rexpr_?.evaluateConstant());
  }

  private protected object evaluateConstant(object l, object r)
  {
    string str = l as string;
    switch (this.oper_)
    {
      case SQLExpression.Operation.NULL:
        return (object) null;
      case SQLExpression.Operation.EQ:
        return (object) object.Equals(l, r);
      case SQLExpression.Operation.NE:
        return (object) !object.Equals(l, r);
      case SQLExpression.Operation.GE:
        return str != null ? (object) (str.CompareTo(r as string) >= 0) : (object) null;
      case SQLExpression.Operation.GT:
        return str != null ? (object) (str.CompareTo(r as string) > 0) : (object) null;
      case SQLExpression.Operation.LT:
        return str != null ? (object) (str.CompareTo(r as string) < 0) : (object) null;
      case SQLExpression.Operation.LE:
        return str != null ? (object) (str.CompareTo(r as string) <= 0) : (object) null;
      case SQLExpression.Operation.AND:
        return (object) (bool) (!(l as bool? ?? true) ? 0 : (r as bool? ?? true ? 1 : 0));
      case SQLExpression.Operation.OR:
        return (object) (bool) (l as bool? ?? true ? 1 : (r as bool? ?? true ? 1 : 0));
      case SQLExpression.Operation.IS_NOT_NULL:
        return (object) (l != null);
      case SQLExpression.Operation.IS_NULL:
        return (object) (l == null);
      default:
        return (object) null;
    }
  }

  internal void getConstantFilterList(ref List<SQLExpression> filter)
  {
    if (this.oper_ == SQLExpression.Operation.OR)
    {
      SQLExpression lexpr = this.lexpr_;
      bool? constant;
      if ((lexpr != null ? (lexpr.isConstantOrLiteral() == SQLExpression.CLType.CONSTANT ? 1 : 0) : 0) != 0)
      {
        constant = this.lexpr_.evaluateConstant() as bool?;
        bool flag = false;
        if (constant.GetValueOrDefault() == flag & constant.HasValue)
        {
          this.rexpr_?.getConstantFilterList(ref filter);
          return;
        }
      }
      SQLExpression rexpr = this.rexpr_;
      if ((rexpr != null ? (rexpr.isConstantOrLiteral() == SQLExpression.CLType.CONSTANT ? 1 : 0) : 0) == 0)
        return;
      constant = this.rexpr_.evaluateConstant() as bool?;
      bool flag1 = false;
      if (!(constant.GetValueOrDefault() == flag1 & constant.HasValue))
        return;
      this.lexpr_?.getConstantFilterList(ref filter);
    }
    else if (this.oper_ == SQLExpression.Operation.AND)
    {
      this.lexpr_?.getConstantFilterList(ref filter);
      this.rexpr_?.getConstantFilterList(ref filter);
    }
    else
    {
      if (this.lexpr_ is Column)
      {
        SQLExpression rexpr = this.rexpr_;
        if ((rexpr != null ? (rexpr.isConstantOrLiteral() != SQLExpression.CLType.OTHER ? 1 : 0) : 1) != 0)
          goto label_22;
      }
      if (!(this.rexpr_ is Column))
        return;
      SQLExpression lexpr = this.lexpr_;
      if ((lexpr != null ? (lexpr.isConstantOrLiteral() != SQLExpression.CLType.OTHER ? 1 : 0) : 1) == 0)
        return;
label_22:
      filter.Add(this);
    }
  }

  internal virtual T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);

  public enum Operation
  {
    NONE,
    NULL,
    EMPTY,
    SUB_QUERY,
    PLUS,
    CONCAT,
    MUL,
    DIV,
    MOD,
    MINUS,
    UMINUS,
    EQ,
    NE,
    GE,
    GT,
    LT,
    LE,
    AND,
    OR,
    NOT,
    ASCII,
    IN,
    NOT_IN,
    LIKE,
    NOT_LIKE,
    MAX,
    MIN,
    AVG,
    SUM,
    COUNT,
    COUNT_DISTINCT,
    SEQ,
    IS_NOT_NULL,
    IS_NULL,
    ISNULL_FUNC,
    SUBSTR,
    CONVERT_BIN_TO_INT,
    CAST_AS_DECIMAL,
    CAST_AS_INTEGER,
    BINARY_LEN,
    REPLACE,
    NULL_IF,
    LEN,
    ABS,
    RTRIM,
    LTRIM,
    ROUND,
    DATE_DIFF,
    DATE_PART,
    DATE_NOW,
    DATE_UTC_NOW,
    BIT_AND,
    BIT_OR,
    EXISTS,
    NOT_EXISTS,
    BETWEEN,
    NOT_BETWEEN,
    BETWEEN_ARGS,
    CONTAINS,
    FREETEXT,
    CEILING,
    FLOOR,
    CHARINDEX,
    REVERSE,
    LOWER,
    UPPER,
    LEFT,
    RIGHT,
    REPEAT,
    POWER,
    TODAY,
    TODAY_UTC,
    TRIM,
    BIT_NOT,
    GET_TIME,
    CAST_AS_VARBINARY,
    INSIDE_BRANCH,
    RESTRICT_BYUSERBRANCHES,
    IS_NUMERIC,
    EQUAL_O_CI,
    EQUAL_O_CS,
    EQUAL_I_CS,
    EQUAL_I_CI,
    EQUAL_CC_CS,
    EQUAL_CC_CI,
    COMPARE_O_CI,
    COMPARE_O_CS,
    COMPARE_I_CS,
    COMPARE_I_CI,
    COMPARE_CC_CS,
    COMPARE_CC_CI,
    COMPARE_CULTUREINFO,
    CHAR,
    TO_BASE64,
    MD5,
    AGG_CONCAT,
  }

  private enum Braces
  {
    CLEAR = -1, // 0xFFFFFFFF
    NATURAL = 0,
    FORCE = 1,
  }

  protected internal enum CLType
  {
    CONSTANT,
    LITERAL,
    OTHER,
  }
}
