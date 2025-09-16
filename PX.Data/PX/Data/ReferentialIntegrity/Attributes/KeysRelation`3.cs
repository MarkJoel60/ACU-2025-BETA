// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.KeysRelation`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Inspecting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable enable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// A base class for a strongly typed relation between two tables.
/// </summary>
/// <typeparam name="TSelf">The type of a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.KeysRelation`3" /> descendant.</typeparam>
/// <typeparam name="TParentTable">The type of the parent table of the relation.</typeparam>
/// <typeparam name="TChildTable">The type of the child table of the relation.</typeparam>
public abstract class KeysRelation<TSelf, TParentTable, TChildTable> : 
  CustomPredicate,
  IForeignKeyBetween<
  #nullable disable
  TChildTable, TParentTable>,
  IForeignKey,
  IForeignKeyFrom<TChildTable>,
  IForeignKeyTo<TParentTable>
  where TSelf : KeysRelation<TSelf, TParentTable, TChildTable>, ITypeArrayOf<IFieldsRelation>, TypeArray.IsNotEmpty, new()
  where TParentTable : class, IBqlTable, new()
  where TChildTable : class, IBqlTable, new()
{
  private protected static readonly Reference Ref = KeysRelation<TSelf, TParentTable, TChildTable>.CreateReference();
  private static readonly System.Type Predicate = ((IEnumerable<IFieldsRelation>) TypeArrayOf<IFieldsRelation>.CheckAndExtractInstances(typeof (TSelf), (string) null)).ToWhere(autoParameters: new bool?());

  /// <see cref="M:PX.Data.PXCache.CollectForeignKeys(System.Type,System.Collections.Generic.IEnumerable{System.Type})" />
  internal static void CollectReference()
  {
    if (ServiceLocator.IsLocationProviderSet)
    {
      ITableReferenceCollector referenceCollector;
      if (ServiceLocatorExtensions.TryGetInstance<ITableReferenceCollector>(ServiceLocator.Current, ref referenceCollector))
      {
        if (referenceCollector.AllReferencesAreCollected.IsCompleted)
          return;
        referenceCollector.TryCollectReference(KeysRelation<TSelf, TParentTable, TChildTable>.Ref);
      }
      else
        PXTrace.WriteWarning("Reference collection for {ReferenceOrigins} references is turned off because ITableReferenceCollector is not registered or there are errors during its resolution", (object) new ReferenceOrigin[1]);
    }
    else
      PXTrace.WriteWarning("Reference collection for {ReferenceOrigins} references is turned off because ServiceLocator is not set", (object) new ReferenceOrigin[1]);
  }

  private static Reference CreateReference()
  {
    try
    {
      return Reference.FromFieldsRelations(typeof (TSelf), ReferenceOrigin.ForeignKeyApi);
    }
    catch (PXException ex) when (ExceptionExtensions.Rethrow<PXException>(ex).Case<PXInvalidFieldsRelationException>((System.Action<PXInvalidFieldsRelationException>) (e => PXTrace.WriteError((Exception) e, "An invalid set of {Relations} with {ParentTables} and {ChildTables} was found in the FK declaration inside {DacType} to {ParentDacType} (for {ReferenceOrigin} reference type). Please make sure that you haven't used BQL fields from the base DAC in the relation inside the derived DAC.", (object) e.Relations, (object) e.ParentTables, (object) e.ChildTables, (object) typeof (TChildTable), (object) typeof (TParentTable), (object) e.ReferenceOrigin))).Default((System.Action<PXException>) (e => PXTrace.WriteError((Exception) e, "An invalid reference was found for in the FK declaration inside {DacType} to {ParentDacType} (for {ReferenceOrigin} reference type).", (object) typeof (TChildTable), (object) typeof (TParentTable), (object) ReferenceOrigin.ForeignKeyApi))))
    {
      throw;
    }
  }

  protected KeysRelation()
    : base((IBqlUnary) Activator.CreateInstance(KeysRelation<TSelf, TParentTable, TChildTable>.Predicate))
  {
  }

  /// <summary>Finds the parent entity that the child entity refers.</summary>
  public static TParentTable FindParent(PXGraph graph, TChildTable child, PKFindOptions options = PKFindOptions.None)
  {
    if ((object) child == null)
      return default (TParentTable);
    PXCache childCache = graph.Caches[KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child.Table];
    TableWithKeys child1 = KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child;
    if (child1.KeyFields.Count == 1)
    {
      PXCache pxCache = childCache;
      // ISSUE: variable of a boxed type
      __Boxed<TChildTable> data = (object) child;
      child1 = KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child;
      string name = child1.KeyFields.First<System.Type>().Name;
      object key = pxCache.GetValue((object) data, name);
      return PrimaryKeyOf<TParentTable>.FindImpl(graph, options, (Func<PXGraph, object, bool, TParentTable>) ((g, k, ro) => (TParentTable) KeysRelation<TSelf, TParentTable, TChildTable>.Ref.SelectParentImpl(g, (ro ? 1 : 0) != 0, k)), key);
    }
    child1 = KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child;
    object[] array = child1.KeyFields.Select<System.Type, object>((Func<System.Type, object>) (k => childCache.GetValue((object) child, k.Name))).ToArray<object>();
    return PrimaryKeyOf<TParentTable>.FindImpl(graph, options, (Func<PXGraph, object[], bool, TParentTable>) ((g, ks, ro) => (TParentTable) KeysRelation<TSelf, TParentTable, TChildTable>.Ref.SelectParentImpl(g, ro, ks)), array);
  }

  /// <summary>
  /// Selects the children entities that refer the parent entity.
  /// </summary>
  public static IEnumerable<TChildTable> SelectChildren(PXGraph graph, TParentTable parent)
  {
    return KeysRelation<TSelf, TParentTable, TChildTable>.Ref.SelectChildren(graph, (object) parent, true).Cast<TChildTable>();
  }

  /// <summary>
  /// Indicates that two entities of the reference are match to each other.
  /// </summary>
  public static bool Match(PXGraph graph, TParentTable parent, TChildTable child)
  {
    return KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Match(graph, (object) parent, (object) child);
  }

  public ReadOnlyDictionary<System.Type, System.Type> FieldsMapping
  {
    get => KeysRelation<TSelf, TParentTable, TChildTable>.Ref.FieldMap;
  }

  public System.Type ParentTable => typeof (TParentTable);

  public System.Type ChildTable => typeof (TChildTable);

  IBqlTable IForeignKey.FindParent(PXGraph graph, IBqlTable child, PKFindOptions options)
  {
    return (IBqlTable) KeysRelation<TSelf, TParentTable, TChildTable>.FindParent(graph, (TChildTable) child, options);
  }

  IEnumerable<IBqlTable> IForeignKey.SelectChildren(PXGraph graph, IBqlTable parent)
  {
    return (IEnumerable<IBqlTable>) KeysRelation<TSelf, TParentTable, TChildTable>.SelectChildren(graph, (TParentTable) parent);
  }

  IBqlTable IForeignKeyFrom<TChildTable>.FindParent(
    PXGraph graph,
    TChildTable child,
    PKFindOptions options)
  {
    return (IBqlTable) KeysRelation<TSelf, TParentTable, TChildTable>.FindParent(graph, child, options);
  }

  IEnumerable<TChildTable> IForeignKeyFrom<TChildTable>.SelectChildren(
    PXGraph graph,
    IBqlTable parent)
  {
    return KeysRelation<TSelf, TParentTable, TChildTable>.SelectChildren(graph, (TParentTable) parent);
  }

  TParentTable IForeignKeyTo<TParentTable>.FindParent(
    PXGraph graph,
    IBqlTable child,
    PKFindOptions options)
  {
    return KeysRelation<TSelf, TParentTable, TChildTable>.FindParent(graph, (TChildTable) child, options);
  }

  IEnumerable<IBqlTable> IForeignKeyTo<TParentTable>.SelectChildren(
    PXGraph graph,
    TParentTable parent)
  {
    return (IEnumerable<IBqlTable>) KeysRelation<TSelf, TParentTable, TChildTable>.SelectChildren(graph, parent);
  }

  TParentTable IForeignKeyBetween<TChildTable, TParentTable>.FindParent(
    PXGraph graph,
    TChildTable child,
    PKFindOptions options)
  {
    return KeysRelation<TSelf, TParentTable, TChildTable>.FindParent(graph, child, options);
  }

  IEnumerable<TChildTable> IForeignKeyBetween<TChildTable, TParentTable>.SelectChildren(
    PXGraph graph,
    TParentTable parent)
  {
    return KeysRelation<TSelf, TParentTable, TChildTable>.SelectChildren(graph, parent);
  }

  public static class Dirty
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.KeysRelation`3.FindParent(PX.Data.PXGraph,`2,PX.Data.PKFindOptions)" />
    /// <remarks>Forced to include dirty records.</remarks>
    public static TParentTable FindParent(PXGraph graph, TChildTable child, PKFindOptions options = PKFindOptions.None)
    {
      if ((object) child == null)
        return default (TParentTable);
      PXCache childCache = graph.Caches[KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child.Table];
      TableWithKeys child1 = KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child;
      if (child1.KeyFields.Count == 1)
      {
        PXCache pxCache = childCache;
        // ISSUE: variable of a boxed type
        __Boxed<TChildTable> data = (object) child;
        child1 = KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child;
        string name = child1.KeyFields.First<System.Type>().Name;
        object key = pxCache.GetValue((object) data, name);
        return PrimaryKeyOf<TParentTable>.FindImpl(graph, options | PKFindOptions.IncludeDirty, (Func<PXGraph, object, bool, TParentTable>) ((g, k, ro) => (TParentTable) KeysRelation<TSelf, TParentTable, TChildTable>.Ref.SelectParentImpl(g, (ro ? 1 : 0) != 0, k)), key);
      }
      child1 = KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child;
      object[] array = child1.KeyFields.Select<System.Type, object>((Func<System.Type, object>) (k => childCache.GetValue((object) child, k.Name))).ToArray<object>();
      return PrimaryKeyOf<TParentTable>.FindImpl(graph, options | PKFindOptions.IncludeDirty, (Func<PXGraph, object[], bool, TParentTable>) ((g, ks, ro) => (TParentTable) KeysRelation<TSelf, TParentTable, TChildTable>.Ref.SelectParentImpl(g, ro, ks)), array);
    }

    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.KeysRelation`3.SelectChildren(PX.Data.PXGraph,`1)" />
    /// <remarks>Forced to include dirty records.</remarks>
    public static IEnumerable<TChildTable> SelectChildren(PXGraph graph, TParentTable parent)
    {
      return KeysRelation<TSelf, TParentTable, TChildTable>.Ref.SelectChildren(graph, (object) parent, false).Cast<TChildTable>();
    }
  }

  /// <summary>
  /// Represents a predicate for the <typeparamref name="TChildTable" /> that is parametrized
  /// with corresponding key values of a current (<see cref="T:PX.Data.Current`1" />) <typeparamref name="TParentTable" /> row.
  /// </summary>
  public class SameAsCurrent : 
    CustomPredicate,
    IParameterizedForeignKeyBetween<TChildTable, TParentTable>,
    IParameterizedForeignKey,
    IBqlUnary,
    IBqlCreator,
    IBqlVerifier,
    IParameterizedForeignKeyFrom<TChildTable>,
    IParameterizedForeignKeyTo<TParentTable>
  {
    private static readonly Lazy<IBqlUnary> Predicate = Lazy.By<IBqlUnary>(new Func<IBqlUnary>(KeysRelation<TSelf, TParentTable, TChildTable>.SameAsCurrent.CreatePredicate));

    public SameAsCurrent()
      : base(KeysRelation<TSelf, TParentTable, TChildTable>.SameAsCurrent.Predicate.Value)
    {
    }

    private static IBqlUnary CreatePredicate()
    {
      TableWithKeys tableWithKeys = KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Child;
      IReadOnlyCollection<System.Type> keyFields1 = tableWithKeys.KeyFields;
      tableWithKeys = KeysRelation<TSelf, TParentTable, TChildTable>.Ref.Parent;
      IReadOnlyCollection<System.Type> keyFields2 = tableWithKeys.KeyFields;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return (IBqlUnary) EnumerableExtensions.Zip<System.Type, System.Type>((IEnumerable<System.Type>) keyFields1, (IEnumerable<System.Type>) keyFields2).Select<Tuple<System.Type, System.Type>, FieldAndParameter>((Func<Tuple<System.Type, System.Type>, FieldAndParameter>) (p => new FieldAndParameter(p.Item1, p.Item2, typeof (Current<>)))).ToArray<FieldAndParameter>().ToWhere().With<System.Type, object>(KeysRelation<TSelf, TParentTable, TChildTable>.SameAsCurrent.\u003C\u003EO.\u003C0\u003E__CreateInstance ?? (KeysRelation<TSelf, TParentTable, TChildTable>.SameAsCurrent.\u003C\u003EO.\u003C0\u003E__CreateInstance = new Func<System.Type, object>(Activator.CreateInstance)));
    }

    /// <inheritdoc cref="T:PX.Data.And`1" />
    public sealed class And<TCondition> : 
      BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<KeysRelation<TSelf, TParentTable, TChildTable>.SameAsCurrent>, PX.Data.And<TCondition>>>
      where TCondition : IBqlUnary, new()
    {
    }

    /// <inheritdoc cref="T:PX.Data.Or`1" />
    public sealed class Or<TCondition> : 
      BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<KeysRelation<TSelf, TParentTable, TChildTable>.SameAsCurrent>, PX.Data.Or<TCondition>>>
      where TCondition : IBqlUnary, new()
    {
    }
  }

  /// <inheritdoc cref="T:PX.Data.And`1" />
  public sealed class And<TCondition> : 
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<TSelf>, PX.Data.And<TCondition>>>
    where TCondition : IBqlUnary, new()
  {
  }

  /// <inheritdoc cref="T:PX.Data.Or`1" />
  public sealed class Or<TCondition> : 
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<TSelf>, PX.Data.Or<TCondition>>>
    where TCondition : IBqlUnary, new()
  {
  }
}
