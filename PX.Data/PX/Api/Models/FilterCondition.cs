// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.FilterCondition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api.Models;

public enum FilterCondition
{
  Equals,
  NotEqual,
  Greater,
  GreaterOrEqual,
  Less,
  LessOrEqual,
  Contain,
  StartsWith,
  EndsWith,
  NotContain,
  Between,
  IsNull,
  IsNotNull,
  Today,
  Overdue,
  TodayOverdue,
  Tomorrow,
  ThisWeek,
  NextWeek,
  ThisMonth,
  NextMonth,
  In,
  Ni,
}
