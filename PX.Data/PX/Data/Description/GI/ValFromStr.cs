// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.ValFromStr
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Description.GI;

internal static class ValFromStr
{
  public static RelationType GetRelation(string rel)
  {
    switch (rel)
    {
      case "L":
        return RelationType.Left;
      case "R":
        return RelationType.Right;
      case "I":
        return RelationType.Inner;
      case "F":
        return RelationType.Full;
      case "C":
        return RelationType.Cross;
      default:
        return RelationType.Inner;
    }
  }

  public static string GetSql(RelationType type)
  {
    switch (type)
    {
      case RelationType.Inner:
        return "INNER JOIN";
      case RelationType.Left:
        return "LEFT JOIN";
      case RelationType.Right:
        return "RIGHT JOIN";
      case RelationType.Full:
        return "FULL JOIN";
      case RelationType.Cross:
        return "CROSS JOIN";
      default:
        return "INNER JOIN";
    }
  }

  public static Joiner.JoinType GetSqlJoinType(RelationType type)
  {
    switch (type)
    {
      case RelationType.Inner:
        return Joiner.JoinType.INNER_JOIN;
      case RelationType.Left:
        return Joiner.JoinType.LEFT_JOIN;
      case RelationType.Right:
        return Joiner.JoinType.RIGHT_JOIN;
      case RelationType.Full:
        return Joiner.JoinType.FULL_JOIN;
      case RelationType.Cross:
        return Joiner.JoinType.CROSS_JOIN;
      default:
        return Joiner.JoinType.INNER_JOIN;
    }
  }

  public static PXCondition GetCondition(Condition cond)
  {
    switch (cond)
    {
      case Condition.Equals:
        return PXCondition.EQ;
      case Condition.NotEqual:
        return PXCondition.NE;
      case Condition.Greater:
        return PXCondition.GT;
      case Condition.GreaterOrEqual:
        return PXCondition.GE;
      case Condition.Less:
        return PXCondition.LT;
      case Condition.LessOrEqual:
        return PXCondition.LE;
      case Condition.Between:
        return PXCondition.BETWEEN;
      case Condition.Contains:
        return PXCondition.LIKE;
      case Condition.NotContains:
        return PXCondition.NOTLIKE;
      case Condition.EndsWith:
        return PXCondition.LLIKE;
      case Condition.StartsWith:
        return PXCondition.RLIKE;
      case Condition.IsNull:
        return PXCondition.ISNULL;
      case Condition.NotNull:
        return PXCondition.ISNOTNULL;
      case Condition.In:
        return PXCondition.IN;
      case Condition.NotIn:
        return PXCondition.NI;
      default:
        throw new PXException("Invalid condition: '{0}'", new object[1]
        {
          (object) cond
        });
    }
  }

  public static Condition GetCondition(string cond)
  {
    string str = !string.IsNullOrEmpty(cond) ? cond.Trim() : throw new PXArgumentException(cond);
    if (str != null)
    {
      switch (str.Length)
      {
        case 1:
          switch (str[0])
          {
            case 'B':
              return Condition.Between;
            case 'E':
              return Condition.Equals;
            case 'G':
              return Condition.Greater;
            case 'L':
              return Condition.Less;
          }
          break;
        case 2:
          switch (str[0])
          {
            case 'G':
              if (str == "GE")
                return Condition.GreaterOrEqual;
              break;
            case 'I':
              if (str == "IN")
                return Condition.In;
              break;
            case 'L':
              switch (str)
              {
                case "LE":
                  return Condition.LessOrEqual;
                case "LI":
                  return Condition.Contains;
                case "LL":
                  return Condition.EndsWith;
              }
              break;
            case 'N':
              switch (str)
              {
                case "NE":
                  return Condition.NotEqual;
                case "NL":
                  return Condition.NotContains;
                case "NU":
                  return Condition.IsNull;
                case "NN":
                  return Condition.NotNull;
                case "NI":
                  return Condition.NotIn;
              }
              break;
            case 'R':
              if (str == "RL")
                return Condition.StartsWith;
              break;
          }
          break;
      }
    }
    throw new PXException("Invalid condition: '{0}'", new object[1]
    {
      (object) cond
    });
  }

  public static Condition GetCondition(PXCondition cond)
  {
    switch (cond)
    {
      case PXCondition.EQ:
        return Condition.Equals;
      case PXCondition.NE:
        return Condition.NotEqual;
      case PXCondition.GT:
        return Condition.Greater;
      case PXCondition.GE:
        return Condition.GreaterOrEqual;
      case PXCondition.LT:
        return Condition.Less;
      case PXCondition.LE:
        return Condition.LessOrEqual;
      case PXCondition.LIKE:
        return Condition.Contains;
      case PXCondition.RLIKE:
        return Condition.StartsWith;
      case PXCondition.LLIKE:
        return Condition.EndsWith;
      case PXCondition.NOTLIKE:
        return Condition.NotContains;
      case PXCondition.BETWEEN:
        return Condition.Between;
      case PXCondition.ISNULL:
        return Condition.IsNull;
      case PXCondition.ISNOTNULL:
        return Condition.NotNull;
      case PXCondition.IN:
        return Condition.In;
      case PXCondition.NI:
        return Condition.NotIn;
      default:
        throw new PXException("Invalid condition: '{0}'", new object[1]
        {
          (object) cond.ToString()
        });
    }
  }

  public static PXCondition GetPXCondition(string cond)
  {
    return ValFromStr.GetCondition(ValFromStr.GetCondition(cond));
  }

  public static SQLExpression GetSqlExpression(
    Condition cond,
    SQLExpression left,
    SQLExpression right)
  {
    switch (cond)
    {
      case Condition.Equals:
        return SQLExpressionExt.EQ(left, right);
      case Condition.NotEqual:
        return SQLExpressionExt.NE(left, right);
      case Condition.Greater:
        return left.GT(right);
      case Condition.GreaterOrEqual:
        return left.GE(right);
      case Condition.Less:
        return left.LT(right);
      case Condition.LessOrEqual:
        return left.LE(right);
      case Condition.Between:
        return left.Between(right);
      case Condition.Contains:
        return left.Like(ValFromStr.WildCard().Concat(right).Concat(ValFromStr.WildCard()));
      case Condition.NotContains:
        return left.NotLike(ValFromStr.WildCard().Concat(right).Concat(ValFromStr.WildCard()));
      case Condition.EndsWith:
        return left.Like(ValFromStr.WildCard().Concat(right));
      case Condition.StartsWith:
        return left.Like(right.Concat(ValFromStr.WildCard()));
      case Condition.IsNull:
        return left.IsNull();
      case Condition.NotNull:
        return left.IsNotNull();
      case Condition.In:
        return left.In(right);
      case Condition.NotIn:
        return left.NotIn(right);
      default:
        return left;
    }
  }

  private static SQLExpression WildCard() => (SQLExpression) new SQLConst((object) "%");

  public static void ApplyFilter(PXGraph graph, PXWhereCond where, PXFilterRow filter)
  {
    ExceptionExtensions.ThrowOnNull<PXGraph>(graph, nameof (graph), (string) null);
    where.OpenBrackets = filter.OpenBrackets;
    where.CloseBrackets = filter.CloseBrackets;
    where.Op = filter.OrOperator ? Operation.Or : Operation.And;
    where.UseExt = filter.UseExt;
    where.Value1 = filter.Description != null ? (IPXValue) new PXSimpleValue(filter.Description.DataValue) : (IPXValue) new PXSimpleValue(filter.Value);
    where.Value2 = filter.Description2 != null ? (IPXValue) new PXSimpleValue(filter.Description2.DataValue) : (IPXValue) new PXSimpleValue(filter.Value2);
    switch (filter.Condition)
    {
      case PXCondition.EQ:
        where.Cond = Condition.Equals;
        break;
      case PXCondition.NE:
        where.Cond = Condition.NotEqual;
        break;
      case PXCondition.GT:
        where.Cond = Condition.Greater;
        break;
      case PXCondition.GE:
        where.Cond = Condition.GreaterOrEqual;
        break;
      case PXCondition.LT:
        where.Cond = Condition.Less;
        break;
      case PXCondition.LE:
        where.Cond = Condition.LessOrEqual;
        break;
      case PXCondition.LIKE:
        where.Cond = Condition.Contains;
        break;
      case PXCondition.RLIKE:
        where.Cond = Condition.StartsWith;
        break;
      case PXCondition.LLIKE:
        where.Cond = Condition.EndsWith;
        break;
      case PXCondition.NOTLIKE:
        where.Cond = Condition.NotContains;
        break;
      case PXCondition.BETWEEN:
        where.Cond = Condition.Between;
        break;
      case PXCondition.ISNULL:
        where.Cond = Condition.IsNull;
        break;
      case PXCondition.ISNOTNULL:
        where.Cond = Condition.NotNull;
        break;
      case PXCondition.TODAY:
      case PXCondition.OVERDUE:
      case PXCondition.TODAY_OVERDUE:
      case PXCondition.TOMMOROW:
      case PXCondition.THIS_WEEK:
      case PXCondition.NEXT_WEEK:
      case PXCondition.THIS_MONTH:
      case PXCondition.NEXT_MONTH:
        System.DateTime?[] dateRange = PXView.DateTimeFactory.GetDateRange(filter.Condition, graph.Accessinfo.BusinessDate);
        if (dateRange[0].HasValue)
          where.Value1 = (IPXValue) new PXSimpleValue((object) dateRange[0]);
        if (dateRange[1].HasValue)
          where.Value2 = (IPXValue) new PXSimpleValue((object) dateRange[1]);
        if (where.Value1 != null && where.Value2 == null)
          where.Cond = Condition.GreaterOrEqual;
        if (where.Value1 == null && where.Value2 != null)
        {
          where.Cond = Condition.LessOrEqual;
          where.Value1 = where.Value2;
          where.Value2 = (IPXValue) null;
        }
        if (where.Value1 == null || where.Value2 == null)
          break;
        where.Cond = Condition.Between;
        break;
      case PXCondition.IN:
        break;
      case PXCondition.NI:
        break;
      default:
        throw new PXException("Invalid condition: '{0}'", new object[1]
        {
          (object) filter.Condition.ToString()
        });
    }
  }

  public static Operation GetOperation(string op) => !(op == "A") ? Operation.Or : Operation.And;

  public static SQLExpression GetSqlExpression(
    Operation op,
    SQLExpression left,
    SQLExpression right)
  {
    return (op == Operation.And ? left.And(right) : left.Or(right)).Unembrace();
  }

  public static SQLExpression GetSqlExpression(Operation op, IEnumerable<SQLExpression> args)
  {
    return (op == Operation.And ? SQLExpression.And(args) : SQLExpression.Or(args)).Unembrace();
  }

  public static SortOrder GetSort(string order) => !(order == "A") ? SortOrder.Desc : SortOrder.Asc;

  public static string GetSql(SortOrder order) => order != SortOrder.Asc ? "DESC" : "ASC";

  public static AggregateFunction GetAggregate(string str)
  {
    AggregateFunction aggregate;
    switch (str)
    {
      case "AVG":
        aggregate = AggregateFunction.Avg;
        break;
      case "COUNT":
        aggregate = AggregateFunction.Count;
        break;
      case "MAX":
        aggregate = AggregateFunction.Max;
        break;
      case "MIN":
        aggregate = AggregateFunction.Min;
        break;
      case "SUM":
        aggregate = AggregateFunction.Sum;
        break;
      case "STRINGAGG":
        aggregate = AggregateFunction.StringAgg;
        break;
      default:
        aggregate = AggregateFunction.Max;
        break;
    }
    return aggregate;
  }

  public static SQLExpression GetSqlExpression(AggregateFunction? type, SQLExpression operand)
  {
    SQLExpression sqlExpression;
    if (type.HasValue)
    {
      switch (type.GetValueOrDefault())
      {
        case AggregateFunction.Avg:
          sqlExpression = operand.Avg();
          break;
        case AggregateFunction.Count:
          sqlExpression = SQLExpression.CountDistinct(operand);
          break;
        case AggregateFunction.Max:
          sqlExpression = operand.Max();
          break;
        case AggregateFunction.Min:
          sqlExpression = operand.Min();
          break;
        case AggregateFunction.Sum:
          sqlExpression = operand.Sum();
          break;
        case AggregateFunction.StringAgg:
          sqlExpression = operand.StringAgg();
          break;
        default:
          sqlExpression = operand.Max();
          break;
      }
    }
    else
      sqlExpression = (SQLExpression) null;
    return sqlExpression;
  }
}
