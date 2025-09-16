// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ScheduleRunBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

public class ScheduleRunBase
{
  public static void SetProcessDelegate<ProcessGraph>(
    PXGraph graph,
    ScheduleRun.Parameters filter,
    PXProcessing<Schedule> view)
    where ProcessGraph : PXGraph<ProcessGraph>, IScheduleProcessing, new()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ScheduleRunBase.\u003C\u003Ec__DisplayClass0_0<ProcessGraph> cDisplayClass00 = new ScheduleRunBase.\u003C\u003Ec__DisplayClass0_0<ProcessGraph>();
    if (filter == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.times = filter.LimitTypeSel == "M" ? filter.RunLimit ?? (short) 1 : short.MaxValue;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.executionDate = filter.LimitTypeSel == "D" ? filter.ExecutionDate ?? graph.Accessinfo.BusinessDate.Value : DateTime.MaxValue;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.parametersErrors = PXUIFieldAttribute.GetErrors(graph.Caches[typeof (ScheduleRun.Parameters)], (object) filter, Array.Empty<PXErrorLevel>());
    // ISSUE: method pointer
    ((PXProcessingBase<Schedule>) view).SetProcessDelegate(new PXProcessingBase<Schedule>.ProcessListDelegate((object) cDisplayClass00, __methodptr(\u003CSetProcessDelegate\u003Eb__0)));
  }
}
