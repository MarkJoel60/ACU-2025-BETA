// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.ScheduledJobHandlerProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac.Features.Indexed;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable
namespace PX.Data.Process;

internal class ScheduledJobHandlerProvider(
  IEnumerable<IScheduledJobHandler> handlers,
  IIndex<string, IScheduledJobHandler> index) : IScheduledJobHandlerProvider
{
  public bool TryGetHandler(string jobType, [NotNullWhen(true)] out IScheduledJobHandler? jobHandler)
  {
    return index.TryGetValue(jobType, ref jobHandler);
  }

  public IEnumerable<IScheduledJobHandler> GetActiveHandlers()
  {
    return (IEnumerable<IScheduledJobHandler>) handlers.Where<IScheduledJobHandler>((Func<IScheduledJobHandler, bool>) (handler => handler.IsActive)).OrderBy<IScheduledJobHandler, string>((Func<IScheduledJobHandler, string>) (h => h.Description));
  }
}
