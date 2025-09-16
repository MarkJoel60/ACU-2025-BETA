// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Utilities
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public static class Utilities
{
  public static void Swap<T>(ref T first, ref T second)
  {
    T obj = first;
    first = second;
    second = obj;
  }

  public static TDestinationDAC Clone<TSourceDAC, TDestinationDAC>(PXGraph graph, TSourceDAC source)
    where TSourceDAC : class, IBqlTable, new()
    where TDestinationDAC : class, IBqlTable, new()
  {
    PXCache pxCache1 = (PXCache) GraphHelper.Caches<TSourceDAC>(graph);
    PXCache pxCache2 = (PXCache) GraphHelper.Caches<TDestinationDAC>(graph);
    TDestinationDAC instance = (TDestinationDAC) pxCache2.CreateInstance();
    foreach (string field in (List<string>) pxCache2.Fields)
    {
      if (pxCache1.Fields.Contains(field))
        pxCache2.SetValue((object) instance, field, pxCache1.GetValue((object) source, field));
    }
    return instance;
  }

  public static PXResultset<TSourceDAC> ToResultset<TSourceDAC>(TSourceDAC item) where TSourceDAC : class, IBqlTable, new()
  {
    PXResultset<TSourceDAC> resultset = new PXResultset<TSourceDAC>();
    resultset.Add(new PXResult<TSourceDAC>(item));
    return resultset;
  }

  public static void SetDependentFieldsAfterBranch(
    List<Command> script,
    (string name, string viewName) firstField,
    List<(string name, string viewName)> fieldList)
  {
    if (script.FindIndex((Predicate<Command>) (cmd => cmd.FieldName == firstField.name && cmd.ObjectName == firstField.viewName)) < 0)
      return;
    List<Command> commandList = new List<Command>();
    foreach ((_, _) in fieldList)
    {
      (string, string) item;
      Command command = script.Where<Command>((Func<Command, bool>) (cmd => cmd.FieldName == item.Item1 && cmd.ObjectName == item.Item2)).SingleOrDefault<Command>();
      if (command == null)
        return;
      command.ObjectName = firstField.viewName;
      command.Commit = false;
      commandList.Add(command);
    }
    Command[] array = commandList.ToArray();
    array[array.Length - 1].Commit = true;
    foreach (Command command in array)
      script.Remove(command);
    int index = script.FindIndex((Predicate<Command>) (cmd => cmd.FieldName == firstField.name && cmd.ObjectName == firstField.viewName));
    script.InsertRange(index + 1, (IEnumerable<Command>) array);
  }

  public static void SetDependentFieldsAfterBranch<TDocument, TBranchID, TBAccountID, TLocationID>(
    List<Command> script,
    List<Container> containers,
    string branchViewName,
    string baccountViewName,
    params string[] additinalFields)
    where TDocument : IBqlTable
    where TBranchID : IBqlField
    where TBAccountID : IBqlField
    where TLocationID : IBqlField
  {
    string graphViewName = $"_{typeof (TDocument).Name}_CurrencyInfo_";
    Command command1 = Utilities.CopyPasteGetCommand(script, branchViewName, typeof (TBranchID).Name);
    (string, string) firstField = (command1?.FieldName, command1?.ObjectName);
    Command command2 = Utilities.CopyPasteGetCommand(script, baccountViewName, typeof (TBAccountID).Name);
    Command command3 = Utilities.CopyPasteGetCommand(script, baccountViewName, typeof (TLocationID).Name);
    Command command4 = Utilities.CopyPasteGetCommand(script, baccountViewName, "CuryID");
    Command command5 = Utilities.CopyPasteGetCommand(script, graphViewName, "CuryRateTypeID");
    Command command6 = Utilities.CopyPasteGetCommand(script, graphViewName, "CuryEffDate");
    Command command7 = Utilities.CopyPasteGetCommand(script, graphViewName, "SampleCuryRate");
    Command command8 = Utilities.CopyPasteGetCommand(script, graphViewName, "SampleRecipRate");
    List<(string, string, bool)> fieldList = new List<(string, string, bool)>()
    {
      (command2?.FieldName, command2?.ObjectName, false),
      (command3?.FieldName, command3?.ObjectName, true),
      (command4?.FieldName, command4?.ObjectName, true),
      (command5?.FieldName, command5?.ObjectName, command5 != null && command5.Commit),
      (command6?.FieldName, command6?.ObjectName, command6 != null && command6.Commit),
      (command7?.FieldName, command7?.ObjectName, command7 != null && command7.Commit),
      (command8?.FieldName, command8?.ObjectName, command8 != null && command8.Commit)
    };
    foreach (string additinalField in additinalFields)
    {
      Command command9 = Utilities.CopyPasteGetCommand(script, baccountViewName, additinalField) ?? Utilities.CopyPasteGetCommand(script, branchViewName, additinalField);
      if (command9 != null)
        fieldList.Add((command9.FieldName, command9.ObjectName, command9.Commit));
    }
    fieldList.RemoveAll((Predicate<(string, string, bool)>) (item => string.IsNullOrEmpty(item.Name)));
    Utilities.SetDependentFieldsAfterBranch(script, containers, firstField, fieldList);
  }

  public static void SetDependentFieldsAfterBranch(
    List<Command> script,
    List<Container> containers,
    (string name, string viewName) firstField,
    List<(string name, string viewName, bool commit)> fieldList)
  {
    if (script.FindIndex((Predicate<Command>) (cmd => cmd.FieldName.Equals(firstField.name, StringComparison.OrdinalIgnoreCase) && cmd.ObjectName.Equals(firstField.viewName, StringComparison.OrdinalIgnoreCase))) < 0)
      return;
    List<Command> commandList = new List<Command>();
    foreach ((_, _, _) in fieldList)
    {
      (string, string, bool) item;
      Command command = script.Where<Command>((Func<Command, bool>) (cmd => cmd.FieldName.Equals(item.Item1, StringComparison.OrdinalIgnoreCase) && cmd.ObjectName.Equals(item.Item2, StringComparison.OrdinalIgnoreCase))).SingleOrDefault<Command>();
      if (command == null)
        return;
      command.Commit = item.Item3;
      commandList.Add(command);
    }
    Command[] array = commandList.ToArray();
    List<Container> collection = new List<Container>();
    foreach (Command command in array)
    {
      int index = script.IndexOf(command);
      script.RemoveAt(index);
      collection.Add(containers[index]);
      containers.RemoveAt(index);
    }
    int index1 = script.FindIndex((Predicate<Command>) (cmd => cmd.FieldName.Equals(firstField.name, StringComparison.OrdinalIgnoreCase) && cmd.ObjectName.Equals(firstField.viewName, StringComparison.OrdinalIgnoreCase)));
    script.InsertRange(index1 + 1, (IEnumerable<Command>) array);
    containers.InsertRange(index1 + 1, (IEnumerable<Container>) collection);
  }

  public static Command CopyPasteGetCommand(
    List<Command> script,
    string graphViewName,
    string fieldName)
  {
    foreach (Command command in script)
    {
      if (command.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
      {
        string objectName = command.ObjectName;
        if (objectName.Equals(graphViewName, StringComparison.OrdinalIgnoreCase))
          return command;
        if (objectName.StartsWith(graphViewName, StringComparison.OrdinalIgnoreCase) && objectName.Contains<char>(':'))
        {
          if (((IEnumerable<string>) objectName.Split(':')).First<string>().Equals(graphViewName, StringComparison.OrdinalIgnoreCase))
            return command;
        }
      }
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
    Command command = Utilities.CopyPasteGetCommand(script, graphViewName, fieldName);
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

  public static TEntity CreateInstance<TEntity>(
    this PXCache cache,
    string[] sortColumns,
    object[] searches)
    where TEntity : class, IBqlTable, new()
  {
    string[] strArray = sortColumns;
    if ((strArray != null ? strArray.Length : 0) < cache.Keys.Count)
      return default (TEntity);
    TEntity instance = (TEntity) cache.CreateInstance();
    foreach (string key in (IEnumerable<string>) cache.Keys)
    {
      object obj = findValue(key);
      if (obj == null)
        return default (TEntity);
      cache.SetValue((object) instance, key, obj);
    }
    return instance;

    object findValue(string field)
    {
      int index = ((IEnumerable<string>) sortColumns).FindIndex<string>((Predicate<string>) (c => c.Equals(field, StringComparison.InvariantCultureIgnoreCase)));
      return index < 0 || searches.Length <= index ? (object) null : searches[index];
    }
  }
}
