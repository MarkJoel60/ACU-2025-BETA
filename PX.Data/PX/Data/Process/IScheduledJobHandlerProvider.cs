// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.IScheduledJobHandlerProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Data.Process;

internal interface IScheduledJobHandlerProvider
{
  bool TryGetHandler(string jobType, [NotNullWhen(true)] out IScheduledJobHandler? jobHandler);

  IEnumerable<IScheduledJobHandler> GetActiveHandlers();
}
