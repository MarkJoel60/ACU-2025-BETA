// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.CommandProcessor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;

#nullable disable
namespace PX.Api.Reports;

internal abstract class CommandProcessor<T> : ICommandProcessor where T : Command
{
  public virtual bool CanExecute(Command cmd) => cmd is T;

  public abstract void Execute(T cmd);

  public void Execute(Command cmd) => this.Execute((T) cmd);
}
