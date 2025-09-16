// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry
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
/// Represents a set of <see cref="T:PX.Data.PXCache" /> and at least one <see cref="T:PX.Data.IBqlTable" /> that belongs to this cache.
/// </summary>
public sealed class EntityEntry
{
  public EntityEntry(System.Type entityType, PXCache cache, IEnumerable<IBqlTable> item)
  {
    this.Cache = cache ?? throw new ArgumentNullException(nameof (cache));
    this.Items = item ?? throw new ArgumentNullException(nameof (item));
    this.EntityType = entityType ?? throw new ArgumentNullException(nameof (entityType));
  }

  public EntityEntry(System.Type entityType, PXCache cache, params IBqlTable[] item)
    : this(entityType, cache, item != null ? ((IEnumerable<IBqlTable>) item).AsEnumerable<IBqlTable>() : (IEnumerable<IBqlTable>) null)
  {
  }

  public EntityEntry(PXCache cache, IEnumerable<IBqlTable> items)
    : this(cache?.GetItemType(), cache, items)
  {
  }

  public EntityEntry(PXCache cache, params IBqlTable[] items)
    : this(cache, items != null ? ((IEnumerable<IBqlTable>) items).AsEnumerable<IBqlTable>() : (IEnumerable<IBqlTable>) null)
  {
  }

  /// <summary>
  /// The type of <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry.Items" />.
  /// </summary>
  /// <remarks>
  /// The value can differ from the value retuned by <see cref="M:PX.Data.PXCache.GetItemType" /> if the item is treated as a parent.
  /// For instance, you are working with <see cref="T:PX.Objects.CR.CRLead" /> with the cache of <see cref="T:PX.Objects.CR.Contact" />.
  /// </remarks>
  public System.Type EntityType { get; }

  /// <summary>
  /// The cache of <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry.Items" />.
  /// </summary>
  public PXCache Cache { get; }

  /// <summary>
  /// The list of <see cref="T:PX.Data.IBqlTable" />s.
  /// </summary>
  public IEnumerable<IBqlTable> Items { get; }

  /// <summary>
  /// Returns an entity of the <typeparamref name="T" /> type.
  /// </summary>
  /// <typeparam name="T">The item type.</typeparam>
  /// <returns>The DAC.</returns>
  /// <exception cref="T:System.InvalidCastException">
  /// The <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry.Items" /> list contains more than one element
  /// or the item cannot be converted to <typeparamref name="T" />.
  /// </exception>
  public T Single<T>() where T : IBqlTable => this.Items.Cast<T>().Single<T>();

  /// <summary>
  /// Returns the list of entities of the <typeparamref name="T" /> type.
  /// </summary>
  /// <typeparam name="T">The item type.</typeparam>
  /// <returns>The DAC.</returns>
  /// <exception cref="T:System.InvalidCastException">
  /// At least one item in <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.EntityEntry.Items" /> cannot be converted to <typeparamref name="T" />.
  /// </exception>
  /// <returns>The list of DACs.</returns>
  public IEnumerable<T> Multiple<T>() where T : IBqlTable => this.Items.Cast<T>();
}
