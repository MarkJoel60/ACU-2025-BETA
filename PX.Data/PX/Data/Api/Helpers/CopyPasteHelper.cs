// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Helpers.CopyPasteHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Helpers;

internal static class CopyPasteHelper
{
  public static Command CopyPasteGetCommand(
    List<Command> script,
    string graphViewName,
    string fieldName)
  {
    foreach (Command command in script)
    {
      if (command.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase) && (command.ObjectName.Equals(graphViewName, StringComparison.OrdinalIgnoreCase) || command.ObjectName.StartsWith(graphViewName, StringComparison.OrdinalIgnoreCase) && command.ObjectName.Contains<char>(':') && StringExtensions.FirstSegment(command.ObjectName, ':').Equals(graphViewName, StringComparison.OrdinalIgnoreCase)))
        return command;
    }
    return (Command) null;
  }

  public static void SetFieldCommandToTheTop(
    List<Command> script,
    List<Container> containers,
    string graphViewName,
    string fieldName,
    bool? commit = true)
  {
    Command command = CopyPasteHelper.CopyPasteGetCommand(script, graphViewName, fieldName);
    if (command == null)
      return;
    if (commit.HasValue)
      command.Commit = commit.Value;
    int index = script.IndexOf(command);
    Container container = containers[index];
    containers.Remove(container);
    containers.Insert(0, container);
    script.Remove(command);
    script.Insert(0, command);
  }
}
