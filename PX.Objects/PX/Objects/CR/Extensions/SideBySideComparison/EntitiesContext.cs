// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.SideBySideComparison;

/// <summary>
/// The set of entities that is used for comparison (see <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3" />).
/// </summary>
public sealed class EntitiesContext
{
  public EntitiesContext(PXGraph graph, EntityEntry mainEntry, IEnumerable<EntityEntry> entries)
  {
    this.Graph = graph ?? throw new ArgumentNullException(nameof (graph));
    this.MainEntryType = mainEntry?.EntityType ?? throw new ArgumentNullException(nameof (mainEntry));
    List<EntityEntry> list = (entries ?? Enumerable.Empty<EntityEntry>()).Prepend<EntityEntry>(mainEntry).ToList<EntityEntry>();
    this.Entries = (IReadOnlyDictionary<System.Type, EntityEntry>) list.ToDictionary<EntityEntry, System.Type, EntityEntry>((Func<EntityEntry, System.Type>) (e => e.EntityType), (Func<EntityEntry, EntityEntry>) (e => e));
    this.Tables = (IReadOnlyCollection<System.Type>) list.Select<EntityEntry, System.Type>((Func<EntityEntry, System.Type>) (e => e.EntityType)).ToList<System.Type>();
  }

  public EntitiesContext(PXGraph graph, EntityEntry mainEntry, params EntityEntry[] entries)
    : this(graph, mainEntry, entries != null ? ((IEnumerable<EntityEntry>) entries).AsEnumerable<EntityEntry>() : (IEnumerable<EntityEntry>) null)
  {
  }

  /// <summary>The graph.</summary>
  public PXGraph Graph { get; }

  /// <summary>The type of the main item.</summary>
  /// <remarks>
  /// Corresponds to <see cref="P:PX.Data.PXGraph.PrimaryItemType" />).
  /// </remarks>
  public System.Type MainEntryType { get; }

  /// <summary>
  /// The entry of <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext.MainEntryType" />.
  /// </summary>
  public EntityEntry MainEntry => this.Entries[this.MainEntryType];

  /// <summary>All entries in the current context.</summary>
  public IReadOnlyDictionary<System.Type, EntityEntry> Entries { get; }

  /// <summary>
  /// All item types that are presented in the current context.
  /// </summary>
  /// <remarks>
  /// Corresponds to the keys of <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext.Entries" />.
  /// </remarks>
  public IReadOnlyCollection<System.Type> Tables { get; }

  /// <summary>
  /// Returns a single entity of the <paramref name="entityType" /> type that is converted to <typeparamref name="T" />.
  /// </summary>
  /// <typeparam name="T">The generic type of requested entity type.</typeparam>
  /// <param name="entityType">The requested entity type.</param>
  /// <returns>The DAC.</returns>
  /// <exception cref="T:System.ArgumentException">
  /// The <paramref name="entityType" /> type is not presented in the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext.Entries" /> collection.
  /// </exception>
  /// <exception cref="T:System.InvalidCastException">
  /// <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry" /> for <paramref name="entityType" /> contains more than one element
  /// or the item cannot be converted to <typeparamref name="T" />.
  /// </exception>
  public T GetEntity<T>(System.Type entityType) where T : IBqlTable => this[entityType].Single<T>();

  /// <summary>
  /// Returns a single entity of the <typeparamref name="T" /> type.
  /// </summary>
  /// <typeparam name="T">The generic type of the requested entity type.</typeparam>
  /// <returns>The DAC.</returns>
  /// <exception cref="T:System.ArgumentException">
  /// The <typeparamref name="T" /> type is not presented in the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext.Entries" /> collection.
  /// </exception>
  /// <exception cref="T:System.InvalidCastException">
  /// <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry" /> for <typeparamref name="T" /> contains more than one element
  /// or the item cannot be converted to <typeparamref name="T" />.
  /// </exception>
  public T GetEntity<T>() where T : IBqlTable => this.GetEntity<T>(typeof (T));

  /// <summary>
  /// Returns the list of entities of the <paramref name="entityType" /> type that are converted to <typeparamref name="T" />.
  /// </summary>
  /// <typeparam name="T">The generic type of the requested entity type.</typeparam>
  /// <param name="entityType">The requested entity type.</param>
  /// <returns>The DAC.</returns>
  /// <exception cref="T:System.ArgumentException">
  /// The <paramref name="entityType" /> type is not presented in the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext.Entries" /> collection.
  /// </exception>
  /// <exception cref="T:System.InvalidCastException">
  /// At least one item inside <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry" /> cannot be converted to <typeparamref name="T" />.
  /// </exception>
  public IEnumerable<T> GetEntities<T>(System.Type entityType) where T : IBqlTable
  {
    return this[entityType].Multiple<T>();
  }

  /// <summary>
  /// Returns the list of entities of the <typeparamref name="T" /> type that are converted to <typeparamref name="T" />.
  /// </summary>
  /// <typeparam name="T">The generic type of the requested entity type.</typeparam>
  /// <returns>The DAC.</returns>
  /// <exception cref="T:System.ArgumentException">
  /// The <typeparamref name="T" /> type is not presented in the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext.Entries" /> collection.
  /// </exception>
  /// <exception cref="T:System.InvalidCastException">
  /// At least one item inside <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry" /> cannot be converted to <typeparamref name="T" />.
  /// </exception>
  public IEnumerable<T> GetEntities<T>() where T : IBqlTable => this.GetEntities<T>(typeof (T));

  /// <summary>Returns the entity entry for the specified item type.</summary>
  /// <param name="itemType">The item type of the DAC (<see cref="T:PX.Data.IBqlTable" />).</param>
  /// <returns>The entity entry.</returns>
  public EntityEntry this[System.Type itemType] => this.Entries[itemType];

  /// <summary>Returns the entity entry for the specified item type.</summary>
  /// <param name="itemType">The full name of the item type of the DAC (<see cref="T:PX.Data.IBqlTable" />).</param>
  /// <returns>The entity entry.</returns>
  public EntityEntry this[string itemType]
  {
    get
    {
      return this[this.Tables.FirstOrDefault<System.Type>((Func<System.Type, bool>) (t => t.FullName == itemType)) ?? throw new ArgumentException("The given key was not present in the dictionary.", nameof (itemType))];
    }
  }

  /// <summary>
  /// Returns the scope that restores all currents (<see cref="P:PX.Data.PXCache.Current" />)
  /// for all caches represented by the current entity context on <see cref="M:System.IDisposable.Dispose" />,
  /// so they can be safely changed and restored to the original values.
  /// </summary>
  /// <returns>The scope</returns>
  public IDisposable PreserveCurrentsScope()
  {
    return (IDisposable) new ReplaceCurrentScope(this.Entries.Values.Select<EntityEntry, KeyValuePair<PXCache, object>>((Func<EntityEntry, KeyValuePair<PXCache, object>>) (e => new KeyValuePair<PXCache, object>(e.Cache, e.Cache.Current))));
  }
}
