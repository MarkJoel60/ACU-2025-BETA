// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Inspecting.GraphViewReferenceAnalyzer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Metadata;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Data.ReferentialIntegrity.Inspecting;

internal class GraphViewReferenceAnalyzer
{
  private readonly 
  #nullable disable
  GraphViewReferenceAnalyzerOptions _options;
  private readonly ITableReferenceCollector _tableReferenceCollector;
  private readonly IGraphRegistry _graphRegistry;
  private readonly ILogger _logger;
  private readonly TaskCompletionSource<bool> _taskCompletionSource = new TaskCompletionSource<bool>();

  public Task AllGraphsAreInspected => (Task) this._taskCompletionSource.Task;

  public GraphViewReferenceAnalyzer(
    IOptions<GraphViewReferenceAnalyzerOptions> options,
    ITableReferenceCollector tableReferenceCollector,
    IGraphRegistry graphRegistry,
    ILogger logger)
  {
    this._options = options.Value;
    this._tableReferenceCollector = tableReferenceCollector;
    this._graphRegistry = graphRegistry;
    this._logger = logger;
    if (options.Value.CollectReferencesFromDataViews)
      return;
    this._taskCompletionSource.SetCanceled();
    logger.Warning("Data views-based DAC reference collection is turned off (CollectReferencesFromDataViews flag is set to false).");
  }

  public Task CollectReferencesAsync(CancellationToken cancellationToken = default (CancellationToken))
  {
    if (this._options.CollectReferencesFromDataViews)
    {
      using (Operation operation = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 1, new LogEventLevel?()).Begin("Collecting DAC references from data views", Array.Empty<object>()))
      {
        foreach (System.Type graphType in this._graphRegistry.All)
        {
          cancellationToken.ThrowIfCancellationRequested();
          this.CollectGraphViews(graphType);
        }
        this._taskCompletionSource.SetResult(true);
        operation.Complete();
      }
    }
    return (Task) this._taskCompletionSource.Task;
  }

  private void CollectGraphViews(System.Type graphType)
  {
    EnumerableExtensions.ForEach<System.Reflection.FieldInfo>(Enumerable.Repeat<System.Type>(graphType, 1).Union<System.Type>((IEnumerable<System.Type>) PXExtensionManager.GetExtensions(graphType, false)).SelectMany<System.Type, System.Reflection.FieldInfo>((Func<System.Type, IEnumerable<System.Reflection.FieldInfo>>) (t => (IEnumerable<System.Reflection.FieldInfo>) t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField))).Where<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (fi => fi.FieldType.IsSubclassOf(typeof (PXSelectBase)))), (System.Action<System.Reflection.FieldInfo>) (fi => this.Collect(graphType, fi.FieldType)));
  }

  private void Collect(System.Type graphType, System.Type viewType)
  {
    if (!viewType.IsGenericType)
      return;
    System.Type type = (System.Type) null;
    for (; viewType != typeof (object); viewType = viewType.BaseType)
    {
      type = viewType.GetNestedType("Config", BindingFlags.Instance | BindingFlags.Public);
      if (type != (System.Type) null)
      {
        type = type.MakeGenericType(viewType.GetGenericArguments());
        break;
      }
    }
    if (type == (System.Type) null)
      return;
    System.Type selectType = ((IViewConfigBase) Activator.CreateInstance(type)).GetCommand().GetSelectType();
    HashSet<System.Type> tables = new HashSet<System.Type>();
    System.Type where = (System.Type) null;
    System.Type join = (System.Type) null;
    foreach (System.Type genericTypeArgument in selectType.GenericTypeArguments)
    {
      if (typeof (IBqlTable).IsAssignableFrom(genericTypeArgument))
        tables.Add(genericTypeArgument);
      else if (typeof (IBqlWhere).IsAssignableFrom(genericTypeArgument))
        where = genericTypeArgument;
      else if (typeof (IBqlJoin).IsAssignableFrom(genericTypeArgument))
        join = genericTypeArgument;
    }
    if (join != (System.Type) null)
    {
      IEnumerable<Tuple<System.Type, System.Type>> relations = GraphViewReferenceAnalyzer.ProcessJoin(tables, join);
      this.TryCollectViewReference(graphType, viewType, relations, ReferenceOrigin.JoinInCustomSelect);
    }
    if (!(where != (System.Type) null))
      return;
    IEnumerable<Tuple<System.Type, System.Type>> relations1 = GraphViewReferenceAnalyzer.ProcessWhere((IReadOnlyCollection<System.Type>) tables, where);
    this.TryCollectViewReference(graphType, viewType, relations1, ReferenceOrigin.WhereInCustomSelect);
  }

  private void TryCollectViewReference(
    System.Type graphType,
    System.Type viewType,
    IEnumerable<Tuple<System.Type, System.Type>> relations,
    ReferenceOrigin origin)
  {
    try
    {
      this.CollectViewReference(relations, origin);
    }
    catch (Exception ex) when (ExceptionExtensions.Rethrow<Exception>(ex).Case<PXInvalidFieldsRelationException>((System.Action<PXInvalidFieldsRelationException>) (e => this._logger.Error((Exception) e, "An invalid set of {Relations} with {ParentTables} and {ChildTables} was found in the view declaration of type {ViewType} inside {GraphType} (for {ReferenceOrigin} reference type). Please make sure that you haven't used BQL fields from the base DAC in the relation inside the derived DAC.", new object[6]
    {
      (object) e.Relations,
      (object) e.ParentTables,
      (object) e.ChildTables,
      (object) viewType,
      (object) graphType,
      (object) origin
    }))).Default((System.Action<Exception>) (e => this._logger.Error<System.Type, System.Type, ReferenceOrigin>(e, "An invalid reference was found in the view declaration of type {ViewType} inside {GraphType} (for {ReferenceOrigin} reference type).", viewType, graphType, origin))))
    {
      throw;
    }
  }

  private void CollectViewReference(
    IEnumerable<Tuple<System.Type, System.Type>> relations,
    ReferenceOrigin origin)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    EnumerableExtensions.ForEach<Reference>(relations.GroupBy<Tuple<System.Type, System.Type>, Tuple<System.Type, System.Type>>((Func<Tuple<System.Type, System.Type>, Tuple<System.Type, System.Type>>) (pair => Tuple.Create<System.Type, System.Type>(BqlCommand.GetItemType(pair.Item1), BqlCommand.GetItemType(pair.Item2)))).Select<IGrouping<Tuple<System.Type, System.Type>, Tuple<System.Type, System.Type>>, System.Type[]>((Func<IGrouping<Tuple<System.Type, System.Type>, Tuple<System.Type, System.Type>>, System.Type[]>) (g => g.Select<Tuple<System.Type, System.Type>, System.Type>((Func<Tuple<System.Type, System.Type>, System.Type>) (pair => typeof (PX.Data.ReferentialIntegrity.Attributes.Field<>.IsRelatedTo<>).MakeGenericType(pair.Item1, pair.Item2))).ToArray<System.Type>())).Select<System.Type[], System.Type>(GraphViewReferenceAnalyzer.\u003C\u003EO.\u003C0\u003E__Construct ?? (GraphViewReferenceAnalyzer.\u003C\u003EO.\u003C0\u003E__Construct = new Func<System.Type[], System.Type>(TypeArrayOf<IFieldsRelation>.Construct))).Select<System.Type, Reference>((Func<System.Type, Reference>) (relation => Reference.FromFieldsRelations(relation, origin))), (System.Action<Reference>) (t => this._tableReferenceCollector.TryCollectReference(t)));
  }

  private static IEnumerable<Tuple<System.Type, System.Type>> ProcessWhere(
    IReadOnlyCollection<System.Type> tables,
    System.Type where)
  {
    return GraphViewReferenceAnalyzer.ProcessChain(tables, true, where);
  }

  private static IEnumerable<Tuple<System.Type, System.Type>> ProcessJoin(
    HashSet<System.Type> tables,
    System.Type join)
  {
    while (join.IsGenericType && join.GetGenericTypeDefinition() != typeof (JoinBase<,,>))
      join = join.BaseType;
    if (!join.IsGenericType)
      return Enumerable.Empty<Tuple<System.Type, System.Type>>();
    List<Tuple<System.Type, System.Type>> tupleList = new List<Tuple<System.Type, System.Type>>();
    System.Type[] genericTypeArguments = join.GenericTypeArguments;
    System.Type c = genericTypeArguments[0];
    System.Type type1 = genericTypeArguments[1];
    System.Type type2 = genericTypeArguments[2];
    if (typeof (IBqlTable).IsAssignableFrom(c) && typeof (IBqlOn).IsAssignableFrom(type1))
    {
      tables.Add(c);
      tupleList.AddRange(GraphViewReferenceAnalyzer.ProcessChain((IReadOnlyCollection<System.Type>) tables, false, type1));
    }
    if (typeof (IBqlJoin).IsAssignableFrom(type2))
      tupleList.AddRange(GraphViewReferenceAnalyzer.ProcessJoin(tables, type2));
    return (IEnumerable<Tuple<System.Type, System.Type>>) tupleList;
  }

  private static IEnumerable<Tuple<System.Type, System.Type>> ProcessChain(
    IReadOnlyCollection<System.Type> tables,
    bool directed,
    System.Type chain)
  {
    System.Type[] genericTypeArguments = chain.GenericTypeArguments;
    List<Tuple<System.Type, System.Type>> tupleList = new List<Tuple<System.Type, System.Type>>();
    for (int index = 0; index < genericTypeArguments.Length; ++index)
    {
      if (typeof (IBqlOperand).IsAssignableFrom(genericTypeArguments[index]))
      {
        System.Type operand = genericTypeArguments[index];
        ++index;
        if (index < genericTypeArguments.Length && typeof (IBqlComparison).IsAssignableFrom(genericTypeArguments[index]))
        {
          System.Type comparison = genericTypeArguments[index];
          tupleList.AddRange(GraphViewReferenceAnalyzer.ProcessOperand(tables, directed, operand, comparison));
        }
      }
      else if (typeof (IBqlCreator).IsAssignableFrom(genericTypeArguments[index]))
        tupleList.AddRange(GraphViewReferenceAnalyzer.ProcessChain(tables, directed, genericTypeArguments[index]));
    }
    return (IEnumerable<Tuple<System.Type, System.Type>>) tupleList;
  }

  private static IEnumerable<Tuple<System.Type, System.Type>> ProcessOperand(
    IReadOnlyCollection<System.Type> tables,
    bool directed,
    System.Type operand,
    System.Type comparison)
  {
    if (typeof (IBqlField).IsAssignableFrom(operand))
    {
      System.Type fieldA = operand;
      System.Type itemType1 = BqlCommand.GetItemType(operand);
      System.Type fieldB = comparison;
      while (fieldB.IsGenericType)
        fieldB = ((IEnumerable<System.Type>) fieldB.GenericTypeArguments).First<System.Type>();
      if (typeof (IBqlField).IsAssignableFrom(fieldB) && fieldB != fieldA)
      {
        System.Type itemType2 = BqlCommand.GetItemType(fieldB);
        if (directed)
        {
          if (tables.Contains<System.Type>(itemType1))
            yield return Tuple.Create<System.Type, System.Type>(fieldA, fieldB);
          else if (tables.Contains<System.Type>(itemType2))
            yield return Tuple.Create<System.Type, System.Type>(fieldB, fieldA);
        }
        else
        {
          bool flag1 = GraphViewReferenceAnalyzer.IsKeyField(itemType1, fieldA);
          bool flag2 = GraphViewReferenceAnalyzer.IsKeyField(itemType2, fieldB);
          if (flag1 & flag2 || !flag1 && !flag2)
          {
            yield return Tuple.Create<System.Type, System.Type>(fieldA, fieldB);
            yield return Tuple.Create<System.Type, System.Type>(fieldB, fieldA);
          }
          else if (flag1)
            yield return Tuple.Create<System.Type, System.Type>(fieldB, fieldA);
          else
            yield return Tuple.Create<System.Type, System.Type>(fieldA, fieldB);
        }
      }
      fieldA = (System.Type) null;
      fieldB = (System.Type) null;
    }
  }

  private static bool IsKeyField(System.Type table, System.Type field)
  {
    PropertyInfo property = table.GetProperty(field.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
    return (object) property != null && property.GetCustomAttributes(typeof (PXDBFieldAttribute), true).OfType<PXDBFieldAttribute>().Count<PXDBFieldAttribute>((Func<PXDBFieldAttribute, bool>) (attr => attr.IsKey)) > 0;
  }

  private class NoDac : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }
}
