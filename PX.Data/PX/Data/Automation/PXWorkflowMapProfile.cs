// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.PXWorkflowMapProfile
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using AutoMapper;
using PX.Data.Automation.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.Automation;

internal class PXWorkflowMapProfile : Profile
{
  public PXWorkflowMapProfile()
  {
    this.CreateMap<ScreenConditionFilter, PXFilterRow>().ForMember<string>((Expression<Func<PXFilterRow, string>>) (d => d.DataField), (System.Action<IMemberConfigurationExpression<ScreenConditionFilter, PXFilterRow, string>>) (m => m.MapFrom<string>((Expression<Func<ScreenConditionFilter, string>>) (s => s.FieldName)))).ForMember<PXCondition>((Expression<Func<PXFilterRow, PXCondition>>) (d => d.Condition), (System.Action<IMemberConfigurationExpression<ScreenConditionFilter, PXFilterRow, PXCondition>>) (m => m.MapFrom<PXCondition>((Expression<Func<ScreenConditionFilter, PXCondition>>) (s => (PXCondition) (s.Condition - 1))))).ForMember<bool>((Expression<Func<PXFilterRow, bool>>) (d => d.OrOperator), (System.Action<IMemberConfigurationExpression<ScreenConditionFilter, PXFilterRow, bool>>) (m => m.MapFrom<bool>((Expression<Func<ScreenConditionFilter, bool>>) (s => s.Operator == 1))));
    this.CreateMap<ScreenCondition, IEnumerable<PXFilterRow>>().ConvertUsing((Func<ScreenCondition, IEnumerable<PXFilterRow>, ResolutionContext, IEnumerable<PXFilterRow>>) ((s, r, ctx) => s.Filters.Select<ScreenConditionFilter, PXFilterRow>((Func<ScreenConditionFilter, PXFilterRow>) (item => ctx.Mapper.Map<PXFilterRow>((object) item)))));
  }
}
