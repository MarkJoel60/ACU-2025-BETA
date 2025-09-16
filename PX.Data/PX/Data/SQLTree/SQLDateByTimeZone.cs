// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLDateByTimeZone
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

public class SQLDateByTimeZone : SQLExpression
{
  private readonly SQLExpression _dataField;
  private readonly PXTimeZoneInfo _localInfo;

  public SQLDateByTimeZone(SQLExpression dataField, PXTimeZoneInfo localInfo)
    : base()
  {
    this._dataField = dataField;
    this._localInfo = localInfo;
    this.Construct();
  }

  public SQLExpression Value => this.lexpr_;

  protected SQLDateByTimeZone(SQLDateByTimeZone other)
    : base((SQLExpression) other)
  {
    this._dataField = other._dataField.Duplicate();
    this._localInfo = other._localInfo;
    this.Construct();
  }

  public override SQLExpression Duplicate() => (SQLExpression) new SQLDateByTimeZone(this);

  internal override PXDbType GetDBType() => this.lexpr_.GetDBType();

  public override string ToString() => $"SQLDateByTimeZone({this.lexpr_})";

  private void Construct()
  {
    if (this._localInfo == null)
    {
      this.lexpr_ = this.AddDelta(TimeSpan.Zero);
    }
    else
    {
      TimeZoneInfo.AdjustmentRule[] adjustmentRules = this._localInfo.GetAdjustmentRules();
      if (adjustmentRules == null || adjustmentRules.Length == 0)
      {
        this.lexpr_ = this.AddDelta(this._localInfo.BaseUtcOffset);
      }
      else
      {
        SQLSwitch caseNode = new SQLSwitch();
        EnumerableExtensions.ForEach<TimeZoneInfo.AdjustmentRule>((IEnumerable<TimeZoneInfo.AdjustmentRule>) adjustmentRules, (System.Action<TimeZoneInfo.AdjustmentRule>) (r =>
        {
          Tuple<SQLExpression, SQLExpression> tuple = this.ConstructRule(r);
          caseNode.Case(tuple.Item1, tuple.Item2);
        }));
        caseNode.Default(this.AddDelta(this._localInfo.BaseUtcOffset));
        this.lexpr_ = (SQLExpression) caseNode;
      }
    }
  }

  private Tuple<SQLExpression, SQLExpression> ConstructRule(TimeZoneInfo.AdjustmentRule rule)
  {
    return Tuple.Create<SQLExpression, SQLExpression>(this.GetYearConditional(rule.DateStart, rule.DateEnd), (SQLExpression) this.GetInnerCase(rule));
  }

  private SQLSwitch GetInnerCase(TimeZoneInfo.AdjustmentRule rule)
  {
    SQLSwitch innerCase = new SQLSwitch();
    innerCase.Case(this.GetDateConditional(rule.DaylightTransitionStart, rule.DaylightTransitionEnd, rule), this.AddDelta(this._localInfo.BaseUtcOffset + rule.DaylightDelta + AdjustmentRuleExtensions.GetBaseUftOffsetDelta(rule)));
    innerCase.Default(this.AddDelta(this._localInfo.BaseUtcOffset + AdjustmentRuleExtensions.GetBaseUftOffsetDelta(rule)));
    return innerCase;
  }

  private SQLExpression GetYearConditional(System.DateTime start, System.DateTime end)
  {
    return start.Year == end.Year ? this.GetYearFunction().EQ((object) start.Year) : this.GetYearFunction().GE((SQLExpression) new SQLConst((object) start.Year)).And(this.GetYearFunction().LE((SQLExpression) new SQLConst((object) end.Year)));
  }

  private SQLExpression GetDateConditional(
    TimeZoneInfo.TransitionTime start,
    TimeZoneInfo.TransitionTime end,
    TimeZoneInfo.AdjustmentRule rule)
  {
    SQLExpression sqlExpression = this.AddDelta(this._localInfo.BaseUtcOffset + AdjustmentRuleExtensions.GetBaseUftOffsetDelta(rule)).GE(this.GetDateFunction(start));
    SQLExpression r = this.AddDelta(this._localInfo.BaseUtcOffset + rule.DaylightDelta + AdjustmentRuleExtensions.GetBaseUftOffsetDelta(rule)).LT(this.GetDateFunction(end));
    return this.IsGreater(end, start) ? sqlExpression.And(r) : sqlExpression.Or(r);
  }

  private bool IsGreater(TimeZoneInfo.TransitionTime first, TimeZoneInfo.TransitionTime second)
  {
    if (first.Month != second.Month)
      return first.Month > second.Month;
    if (first.Week != second.Week)
      return first.Week > second.Week;
    return first.DayOfWeek != second.DayOfWeek ? first.DayOfWeek > second.DayOfWeek : first.TimeOfDay > second.TimeOfDay;
  }

  private int GetDayOfWeek(DayOfWeek dayOfWeek)
  {
    return dayOfWeek == DayOfWeek.Sunday ? 7 : (int) dayOfWeek;
  }

  private SQLExpression GetDateFunction(TimeZoneInfo.TransitionTime time)
  {
    string v = time.TimeOfDay.ToString("HH:mm");
    return (SQLExpression) new SQLScalarFunction("GetDateByWeek", new SQLExpression[5]
    {
      this.GetYearFunction(),
      (SQLExpression) new SQLConst((object) time.Month),
      (SQLExpression) new SQLConst((object) time.Week),
      (SQLExpression) new SQLConst((object) this.GetDayOfWeek(time.DayOfWeek)),
      (SQLExpression) new SQLConst((object) v)
    });
  }

  private SQLExpression AddDelta(TimeSpan delta)
  {
    SQLExpression date = this._dataField;
    if (delta.Hours != 0)
      date = (SQLExpression) new SQLDateAdd((IConstant<string>) new PX.Data.DatePart.hour(), (SQLExpression) new SQLConst((object) delta.Hours), date);
    if (delta.Minutes != 0)
      date = (SQLExpression) new SQLDateAdd((IConstant<string>) new PX.Data.DatePart.minute(), (SQLExpression) new SQLConst((object) delta.Minutes), date);
    return date;
  }

  private SQLExpression GetYearFunction()
  {
    return (SQLExpression) new SQLDatePart((IConstant<string>) new PX.Data.DatePart.year(), this._dataField);
  }

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
