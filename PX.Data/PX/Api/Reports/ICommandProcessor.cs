// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ICommandProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;

#nullable disable
namespace PX.Api.Reports;

internal interface ICommandProcessor
{
  bool CanExecute(Command cmd);

  void Execute(Command cmd);
}
