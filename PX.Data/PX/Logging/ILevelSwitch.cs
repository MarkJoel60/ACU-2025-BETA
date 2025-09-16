// Decompiled with JetBrains decompiler
// Type: PX.Logging.ILevelSwitch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;

#nullable disable
namespace PX.Logging;

internal interface ILevelSwitch
{
  T Apply<T>(Func<LoggingLevelSwitch, T> func);

  LoggerConfiguration ApplyTo(LoggerMinimumLevelConfiguration configuration);

  LogEventLevel MinimumLevel { get; }
}
