// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGIFormulaProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common.Parser;
using PX.Data.Description.GI;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class PXGIFormulaProcessor
{
  private readonly Dictionary<string, ExpressionNode> Cache = new Dictionary<string, ExpressionNode>();

  public bool ContainsFields(string formula)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PXGIFormulaProcessor.\u003C\u003Ec__DisplayClass3_0 cDisplayClass30 = new PXGIFormulaProcessor.\u003C\u003Ec__DisplayClass3_0();
    if (string.IsNullOrEmpty(formula) || formula[0] != '=')
      return false;
    ExpressionNode compiled = this.GetCompiled(formula);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.result = false;
    // ISSUE: method pointer
    ExpressionNode.ConvertMethod convertMethod = new ExpressionNode.ConvertMethod((object) cDisplayClass30, __methodptr(\u003CContainsFields\u003Eb__0));
    compiled.ToText(convertMethod);
    // ISSUE: reference to a compiler-generated field
    return cDisplayClass30.result;
  }

  public object Evaluate(string formula, SyFormulaFinalDelegate getter)
  {
    return string.IsNullOrEmpty(formula) || formula[0] != '=' ? (object) null : this.GetCompiled(formula).Eval((object) getter);
  }

  public SQLExpression TransformToExpression(string formula, SyFormulaFinalDelegate getter)
  {
    return string.IsNullOrEmpty(formula) || formula[0] != '=' ? (SQLExpression) null : this.GetCompiled(formula).Accept<SQLExpression>((INodeVisitor<SQLExpression>) new PXGIFormulaProcessor.SqlExpressionNodeVisitor(this, getter));
  }

  private ExpressionNode GetCompiled(string formula)
  {
    ExpressionNode compiled;
    if (!this.Cache.TryGetValue(formula, out compiled))
    {
      compiled = PXGIFormulaProcessor.Compile(formula);
      this.Cache[formula] = compiled;
    }
    return compiled;
  }

  private static ExpressionNode Compile(string formula)
  {
    formula = formula.Substring(1);
    return SyExpressionParser.Parse(formula);
  }

  private SQLExpression Func2sqlExpr(FunctionNode node, SQLExpression[] args)
  {
    if (PXGIFormulaProcessor.NodeIs(node, "Abs"))
      return args[0].Abs();
    if (PXGIFormulaProcessor.NodeIs(node, "CBool"))
      return (SQLExpression) new SQLConvert(typeof (bool), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "CDate"))
      return (SQLExpression) new SQLConvert(typeof (System.DateTime), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "CDbl"))
      return (SQLExpression) new SQLConvert(typeof (double), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "CDec"))
      return (SQLExpression) new SQLConvert(typeof (Decimal), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "Ceiling"))
      return args[0].Ceiling();
    if (PXGIFormulaProcessor.NodeIs(node, "CInt"))
      return (SQLExpression) new SQLConvert(typeof (int), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "CLong"))
      return (SQLExpression) new SQLConvert(typeof (long), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "Concat"))
    {
      SQLExpression left = (SQLExpression) null;
      foreach (SQLExpression right in args)
        left = (left != null ? left.Concat(right) : (SQLExpression) null) ?? right;
      return left;
    }
    if (PXGIFormulaProcessor.NodeIs(node, "CShort"))
      return (SQLExpression) new SQLConvert(typeof (short), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "CSng"))
      return (SQLExpression) new SQLConvert(typeof (float), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "CStr"))
      return (SQLExpression) new SQLConvert(typeof (string), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "DateAdd"))
      return (SQLExpression) new SQLDateAdd(PXGIFormulaProcessor.GetDatePart((string) ((SQLConst) args[1]).GetValue()) ?? throw new PXException("Time interval is not correct for the {0} function.", new object[1]
      {
        (object) node.Name
      }), args[2], args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "DateDiff"))
      return (SQLExpression) new SQLDateDiff(PXGIFormulaProcessor.GetDateDiff((string) ((SQLConst) args[0]).GetValue()) ?? throw new PXException("Time interval is not correct for the {0} function.", new object[1]
      {
        (object) node.Name
      }), args[1], args[2]);
    if (PXGIFormulaProcessor.NodeIs(node, "Day"))
      return (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.day(), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "DayOfWeek"))
      return (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.weekDay(), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "DayOfYear"))
      return (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.dayOfYear(), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "Floor"))
      return args[0].Floor();
    if (PXGIFormulaProcessor.NodeIs(node, "Format"))
      throw new NotImplementedException();
    if (PXGIFormulaProcessor.NodeIs(node, "Hour"))
      return (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.hour(), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "IIf"))
      return (SQLExpression) new SQLSwitch().Case(args[0], args[1]).Default(args[2]);
    if (PXGIFormulaProcessor.NodeIs(node, "InStr"))
      return args[0].CharIndex(args[1]);
    if (PXGIFormulaProcessor.NodeIs(node, "InStrRev"))
      return (SQLExpression) new SQLSwitch().Case(args[0].Duplicate().CharIndex(args[1]).GE((SQLExpression) new SQLConst((object) 1)), args[0].Length() - args[0].Duplicate().Reverse().CharIndex(args[1].Reverse()) + (SQLExpression) new SQLConst((object) 1)).Default((SQLExpression) new SQLConst((object) 0));
    if (PXGIFormulaProcessor.NodeIs(node, "IsNull"))
      return args[0].Coalesce(args[1]);
    if (PXGIFormulaProcessor.NodeIs(node, "LCase"))
      return args[0].LowerCase();
    if (PXGIFormulaProcessor.NodeIs(node, "Left"))
      return args[0].Left(args[1]);
    if (PXGIFormulaProcessor.NodeIs(node, "Len"))
      return args[0].Length();
    if (PXGIFormulaProcessor.NodeIs(node, "LTrim"))
      return args[0].LTrim();
    if (PXGIFormulaProcessor.NodeIs(node, "Max"))
      return args.Length < 2 ? args[0].Max() : (SQLExpression) new SQLSwitch().Case(args[0].GE(args[1]), args[0].Duplicate()).Default(args[1].Duplicate());
    if (PXGIFormulaProcessor.NodeIs(node, "Min"))
      return args.Length < 2 ? args[0].Min() : (SQLExpression) new SQLSwitch().Case(args[0].LE(args[1]), args[0].Duplicate()).Default(args[1].Duplicate());
    if (PXGIFormulaProcessor.NodeIs(node, "Minute"))
      return (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.minute(), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "Month"))
      return (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.month(), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "MonthName"))
    {
      SQLSwitch sqlSwitch = new SQLSwitch();
      SQLDatePart sqlDatePart = new SQLDatePart((IConstant<string>) new DatePart.month(), args[0]);
      string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
      for (int index = 0; index < monthNames.Length; ++index)
      {
        if (!string.IsNullOrEmpty(monthNames[index]))
        {
          SQLExpression when = sqlDatePart.Duplicate().EQ((object) (index + 1));
          SQLConst then = new SQLConst((object) monthNames[index]);
          sqlSwitch.Case(when, (SQLExpression) then);
        }
      }
      return (SQLExpression) sqlSwitch;
    }
    if (PXGIFormulaProcessor.NodeIs(node, "Now"))
      return SQLExpression.Now();
    if (PXGIFormulaProcessor.NodeIs(node, "NowUTC"))
      return SQLExpression.NowUtc();
    if (PXGIFormulaProcessor.NodeIs(node, "NullIf"))
      return args[0].NullIf(args[1]);
    if (PXGIFormulaProcessor.NodeIs(node, "PadLeft"))
      return args[2].Repeat(args[1] - args[0].Length()).Concat(args[0].Duplicate());
    if (PXGIFormulaProcessor.NodeIs(node, "PadRight"))
      return args[0].Concat(args[2].Repeat(args[1] - args[0].Duplicate().Length()));
    if (PXGIFormulaProcessor.NodeIs(node, "Pow"))
      return args[0].Power(args[1]);
    if (PXGIFormulaProcessor.NodeIs(node, "Replace"))
      return args[0].Replace(args[1], args[2]);
    if (PXGIFormulaProcessor.NodeIs(node, "Right"))
      return args[0].Right(args[1]);
    if (PXGIFormulaProcessor.NodeIs(node, "Round"))
      return args[0].Round(args[1]);
    if (PXGIFormulaProcessor.NodeIs(node, "RTrim"))
      return args[0].RTrim();
    if (PXGIFormulaProcessor.NodeIs(node, "Second"))
      return (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.second(), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "Substring"))
      return args[0].Substr(args[1], args[2]);
    if (PXGIFormulaProcessor.NodeIs(node, "Switch"))
    {
      SQLSwitch sqlSwitch = new SQLSwitch();
      for (int index = 0; index < args.Length - 1; index += 2)
        sqlSwitch.Case(args[index], args[index + 1]);
      if (args.Length % 2 == 1)
        sqlSwitch.Default(args[args.Length - 1]);
      return (SQLExpression) sqlSwitch;
    }
    if (PXGIFormulaProcessor.NodeIs(node, "Today"))
      return SQLExpression.Today();
    if (PXGIFormulaProcessor.NodeIs(node, "TodayUTC"))
      return SQLExpression.TodayUtc();
    if (PXGIFormulaProcessor.NodeIs(node, "Trim"))
      return args[0].Trim();
    if (PXGIFormulaProcessor.NodeIs(node, "UCase"))
      return args[0].UpperCase();
    if (PXGIFormulaProcessor.NodeIs(node, "Year"))
      return (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.year(), args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "COUNTDISTINCT"))
      return SQLExpression.CountDistinct(args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "COUNT"))
      return !((IEnumerable<SQLExpression>) args).Any<SQLExpression>() ? SQLExpression.Count() : SQLExpression.Count(args[0]);
    if (PXGIFormulaProcessor.NodeIs(node, "SUM"))
      return args[0].Sum();
    if (PXGIFormulaProcessor.NodeIs(node, "AVG"))
      return args[0].Avg();
    if (PXGIFormulaProcessor.NodeIs(node, "STRINGAGG"))
      return args[0].StringAgg(args[1] as SQLConst);
    throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Unsupported formula operator '{0}.'", (object) (node.Name ?? string.Empty)));
  }

  private static bool NodeIs(FunctionNode node, string funcName)
  {
    return string.Equals(node.Name, funcName, StringComparison.OrdinalIgnoreCase);
  }

  private static IConstant<string> GetDatePart(string step)
  {
    string str1;
    if (step != null)
      str1 = step.Trim('\'').ToLower();
    else
      str1 = string.Empty;
    string str2 = str1;
    if (str2 != null)
    {
      switch (str2.Length)
      {
        case 1:
          switch (str2[0])
          {
            case 'd':
              goto label_29;
            case 'h':
              goto label_30;
            case 'm':
              goto label_28;
            case 'n':
              goto label_31;
            case 'q':
              goto label_34;
            case 's':
              goto label_32;
            case 'w':
              goto label_33;
            case 'y':
              break;
            default:
              goto label_35;
          }
          break;
        case 2:
          switch (str2[1])
          {
            case 'd':
              if (str2 == "dd")
                goto label_29;
              goto label_35;
            case 'h':
              if (str2 == "hh")
                goto label_30;
              goto label_35;
            case 'i':
              if (str2 == "mi")
                goto label_31;
              goto label_35;
            case 'k':
              if (str2 == "wk")
                goto label_33;
              goto label_35;
            case 'm':
              if (str2 == "mm")
                goto label_28;
              goto label_35;
            case 'q':
              if (str2 == "qq")
                goto label_34;
              goto label_35;
            case 's':
              if (str2 == "ss")
                goto label_32;
              goto label_35;
            case 'w':
              if (str2 == "ww")
                goto label_33;
              goto label_35;
            case 'y':
              if (str2 == "yy")
                break;
              goto label_35;
            default:
              goto label_35;
          }
          break;
        case 3:
          if (str2 == "day")
            goto label_29;
          goto label_35;
        case 4:
          switch (str2[2])
          {
            case 'a':
              if (str2 == "year")
                break;
              goto label_35;
            case 'e':
              if (str2 == "week")
                goto label_33;
              goto label_35;
            case 'u':
              if (str2 == "hour")
                goto label_30;
              goto label_35;
            case 'y':
              if (str2 == "yyyy")
                break;
              goto label_35;
            default:
              goto label_35;
          }
          break;
        case 5:
          if (str2 == "month")
            goto label_28;
          goto label_35;
        case 6:
          switch (str2[0])
          {
            case 'm':
              if (str2 == "minute")
                goto label_31;
              goto label_35;
            case 's':
              if (str2 == "second")
                goto label_32;
              goto label_35;
            default:
              goto label_35;
          }
        case 7:
          if (str2 == "quarter")
            goto label_34;
          goto label_35;
        default:
          goto label_35;
      }
      return (IConstant<string>) new DatePart.year();
label_28:
      return (IConstant<string>) new DatePart.month();
label_29:
      return (IConstant<string>) new DatePart.day();
label_30:
      return (IConstant<string>) new DatePart.hour();
label_31:
      return (IConstant<string>) new DatePart.minute();
label_32:
      return (IConstant<string>) new DatePart.second();
label_33:
      return (IConstant<string>) new DatePart.week();
label_34:
      return (IConstant<string>) new DatePart.quarter();
    }
label_35:
    return (IConstant<string>) null;
  }

  private static IConstant<string> GetDateDiff(string step)
  {
    string str1;
    if (step != null)
      str1 = step.Trim('\'').ToLower();
    else
      str1 = string.Empty;
    string str2 = str1;
    if (str2 != null)
    {
      switch (str2.Length)
      {
        case 1:
          switch (str2[0])
          {
            case 'd':
              goto label_30;
            case 'h':
              goto label_31;
            case 'm':
              goto label_29;
            case 'n':
              goto label_32;
            case 'q':
              goto label_35;
            case 's':
              goto label_33;
            case 'w':
              goto label_34;
            case 'y':
              break;
            default:
              goto label_36;
          }
          break;
        case 2:
          switch (str2[1])
          {
            case 'd':
              if (str2 == "dd")
                goto label_30;
              goto label_36;
            case 'h':
              if (str2 == "hh")
                goto label_31;
              goto label_36;
            case 'i':
              if (str2 == "mi")
                goto label_32;
              goto label_36;
            case 'k':
              if (str2 == "wk")
                goto label_34;
              goto label_36;
            case 'm':
              if (str2 == "mm")
                goto label_29;
              goto label_36;
            case 'q':
              if (str2 == "qq")
                goto label_35;
              goto label_36;
            case 's':
              if (str2 == "ss")
                goto label_33;
              goto label_36;
            case 't':
              switch (str2)
              {
                case "dt":
                  goto label_30;
                case "ht":
                  goto label_31;
                case "nt":
                  goto label_32;
                case "st":
                  goto label_33;
                default:
                  goto label_36;
              }
            case 'w':
              if (str2 == "ww")
                goto label_34;
              goto label_36;
            case 'y':
              if (str2 == "yy")
                break;
              goto label_36;
            default:
              goto label_36;
          }
          break;
        case 3:
          if (str2 == "day")
            goto label_30;
          goto label_36;
        case 4:
          switch (str2[2])
          {
            case 'a':
              if (str2 == "year")
                break;
              goto label_36;
            case 'e':
              if (str2 == "week")
                goto label_34;
              goto label_36;
            case 'u':
              if (str2 == "hour")
                goto label_31;
              goto label_36;
            case 'y':
              if (str2 == "yyyy")
                break;
              goto label_36;
            default:
              goto label_36;
          }
          break;
        case 5:
          if (str2 == "month")
            goto label_29;
          goto label_36;
        case 6:
          switch (str2[0])
          {
            case 'm':
              if (str2 == "minute")
                goto label_32;
              goto label_36;
            case 's':
              if (str2 == "second")
                goto label_33;
              goto label_36;
            default:
              goto label_36;
          }
        case 7:
          if (str2 == "quarter")
            goto label_35;
          goto label_36;
        default:
          goto label_36;
      }
      return (IConstant<string>) new DateDiff.year();
label_29:
      return (IConstant<string>) new DateDiff.month();
label_30:
      return (IConstant<string>) new DateDiff.day();
label_31:
      return (IConstant<string>) new DateDiff.hour();
label_32:
      return (IConstant<string>) new DateDiff.minute();
label_33:
      return (IConstant<string>) new DateDiff.second();
label_34:
      return (IConstant<string>) new DateDiff.week();
label_35:
      return (IConstant<string>) new DateDiff.quarter();
    }
label_36:
    return (IConstant<string>) null;
  }

  private class SqlExpressionNodeVisitor : 
    INodeVisitorExt<SQLExpression>,
    INodeVisitor<SQLExpression>
  {
    public SqlExpressionNodeVisitor(PXGIFormulaProcessor processor, SyFormulaFinalDelegate getter)
    {
      this.Processor = processor;
      this.Getter = getter;
    }

    public PXGIFormulaProcessor Processor { get; }

    public SyFormulaFinalDelegate Getter { get; }

    public SQLExpression Visit(FunctionNode node)
    {
      PXGIFormulaProcessor processor = this.Processor;
      FunctionNode node1 = node;
      List<ExpressionNode> arguments = node.Arguments;
      SQLExpression[] args = (arguments != null ? arguments.Select<ExpressionNode, SQLExpression>((Func<ExpressionNode, SQLExpression>) (a => a.Accept<SQLExpression>((INodeVisitor<SQLExpression>) this))).ToArray<SQLExpression>() : (SQLExpression[]) null) ?? new SQLExpression[0];
      return processor.Func2sqlExpr(node1, args);
    }

    public SQLExpression Visit(ConstantNode node) => (SQLExpression) new SQLConst(node.Value);

    public SQLExpression Visit(NameNode node) => (SQLExpression) this.Getter(node.Name);

    public SQLExpression Visit(AggregateNode node)
    {
      SQLExpression operand = node.Argument != null ? node.Argument.Accept<SQLExpression>((INodeVisitor<SQLExpression>) this) : SQLExpression.None();
      return ValFromStr.GetSqlExpression(new AggregateFunction?(ValFromStr.GetAggregate(node.Name)), operand);
    }

    public SQLExpression Visit(ZeroOpNode node)
    {
      if (node.Op == Operator.Null)
        return SQLExpression.Null();
      if (node.Op == Operator.False)
        return (SQLExpression) new SQLConst((object) 0);
      if (node.Op == Operator.True)
        return (SQLExpression) new SQLConst((object) 1);
      throw new PXException("Unsupported node type '{0}'.", new object[1]
      {
        (object) node.Op.Name
      });
    }

    public SQLExpression Visit(UnaryOpNode node)
    {
      SQLExpression r = node.Right.Accept<SQLExpression>((INodeVisitor<SQLExpression>) this).Embrace();
      switch ((node.Op.Name ?? string.Empty).ToLower())
      {
        case "not":
          return r.BitNot();
        case "-":
          return SQLExpression.Negate(r);
        case "+":
        case "":
          return r;
        default:
          throw new PXException("Unsupported formula operator '{0}.'", new object[1]
          {
            (object) node.Op.Name
          });
      }
    }

    public SQLExpression Visit(BinaryOpNode node)
    {
      SQLExpression l = node.Left.Accept<SQLExpression>((INodeVisitor<SQLExpression>) this);
      SQLExpression sqlExpression = node.Right.Accept<SQLExpression>((INodeVisitor<SQLExpression>) this).Embrace();
      string lower = (node.Op.Name ?? string.Empty).ToLower();
      if (lower != null)
      {
        switch (lower.Length)
        {
          case 1:
            switch (lower[0])
            {
              case '*':
                return l * sqlExpression;
              case '+':
                return l + sqlExpression;
              case '-':
                return l - sqlExpression;
              case '/':
                return l / sqlExpression;
              case '<':
                return l.LT(sqlExpression);
              case '=':
                return (node.Right is ZeroOpNode right ? right.Op : (Operator) null) == Operator.Null ? l.IsNull() : SQLExpressionExt.EQ(l, sqlExpression);
              case '>':
                return l.GT(sqlExpression);
              default:
                goto label_30;
            }
          case 2:
            switch (lower[1])
            {
              case '=':
                switch (lower)
                {
                  case ">=":
                    return l.GE(sqlExpression);
                  case "<=":
                    return l.LE(sqlExpression);
                  default:
                    goto label_30;
                }
              case '>':
                if (lower == "<>")
                  return SQLExpressionExt.NE(l, sqlExpression);
                goto label_30;
              case 'n':
                if (lower == "in")
                  return l.In(sqlExpression);
                goto label_30;
              case 'r':
                if (lower == "or")
                  return l.Or(sqlExpression);
                goto label_30;
              case 's':
                if (lower == "is")
                  break;
                goto label_30;
              default:
                goto label_30;
            }
            break;
          case 3:
            switch (lower[0])
            {
              case 'a':
                if (lower == "and")
                  return l.And(sqlExpression);
                goto label_30;
              case 'm':
                if (lower == "mod")
                  return l % sqlExpression;
                goto label_30;
              default:
                goto label_30;
            }
          case 5:
            if (lower == "isnot")
              break;
            goto label_30;
          default:
            goto label_30;
        }
        throw new NotImplementedException();
      }
label_30:
      throw new PXException("Unsupported formula operator '{0}.'", new object[1]
      {
        (object) node.Op.Name
      });
    }

    public SQLExpression Visit(LikeNode node)
    {
      return ((BinaryOpNode) node).Left.Accept<SQLExpression>((INodeVisitor<SQLExpression>) this).Like(((BinaryOpNode) node).Right.Accept<SQLExpression>((INodeVisitor<SQLExpression>) this));
    }
  }
}
