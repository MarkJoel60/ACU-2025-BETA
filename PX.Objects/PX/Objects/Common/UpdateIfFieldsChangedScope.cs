// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.UpdateIfFieldsChangedScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

[PXInternalUseOnly]
public class UpdateIfFieldsChangedScope : IDisposable
{
  protected bool _disposed;

  public UpdateIfFieldsChangedScope()
  {
    UpdateIfFieldsChangedScope.Changes changes = PXContext.GetSlot<UpdateIfFieldsChangedScope.Changes>();
    if (changes == null)
    {
      changes = new UpdateIfFieldsChangedScope.Changes();
      PXContext.SetSlot<UpdateIfFieldsChangedScope.Changes>(changes);
    }
    ++changes.ReferenceCounter;
  }

  public virtual UpdateIfFieldsChangedScope AppendContext(params Type[] newChanges)
  {
    UpdateIfFieldsChangedScope.Changes slot = PXContext.GetSlot<UpdateIfFieldsChangedScope.Changes>();
    if (slot.SourceOfChange == null)
      slot.SourceOfChange = new HashSet<Type>();
    foreach (Type newChange in newChanges)
    {
      if (!slot.SourceOfChange.Contains(newChange))
        slot.SourceOfChange.Add(newChange);
    }
    return this;
  }

  public virtual UpdateIfFieldsChangedScope AppendContext<Field>() where Field : IBqlField
  {
    return this.AppendContext(typeof (Field));
  }

  public virtual UpdateIfFieldsChangedScope AppendContext<Field1, Field2>()
    where Field1 : IBqlField
    where Field2 : IBqlField
  {
    return this.AppendContext(typeof (Field1), typeof (Field2));
  }

  public void Dispose()
  {
    this._disposed = !this._disposed ? true : throw new PXObjectDisposedException();
    UpdateIfFieldsChangedScope.Changes slot = PXContext.GetSlot<UpdateIfFieldsChangedScope.Changes>();
    --slot.ReferenceCounter;
    if (slot.ReferenceCounter != 0)
      return;
    PXContext.SetSlot<UpdateIfFieldsChangedScope.Changes>((UpdateIfFieldsChangedScope.Changes) null);
  }

  public virtual bool IsUpdateNeeded(params Type[] changes)
  {
    UpdateIfFieldsChangedScope.Changes slot = PXContext.GetSlot<UpdateIfFieldsChangedScope.Changes>();
    if (slot?.SourceOfChange == null)
      return true;
    foreach (Type change in changes)
    {
      if (slot.SourceOfChange.Contains(change))
        return true;
    }
    return false;
  }

  public virtual bool IsUpdateNeeded<Field>() where Field : IBqlField
  {
    return this.IsUpdateNeeded(typeof (Field));
  }

  public virtual bool IsUpdateNeeded<Field1, Field2>()
    where Field1 : IBqlField
    where Field2 : IBqlField
  {
    return this.IsUpdateNeeded(typeof (Field1), typeof (Field2));
  }

  public virtual bool IsUpdatedOnly(params Type[] fields)
  {
    UpdateIfFieldsChangedScope.Changes slot = PXContext.GetSlot<UpdateIfFieldsChangedScope.Changes>();
    if (slot?.SourceOfChange == null)
      return true;
    foreach (Type type in slot.SourceOfChange)
    {
      if (!((IEnumerable<Type>) fields).Contains<Type>(type))
        return false;
    }
    return true;
  }

  public virtual bool IsUpdatedOnly<Field>() where Field : IBqlField
  {
    return this.IsUpdatedOnly(typeof (Field));
  }

  public virtual bool IsUpdatedOnly<Field1, Field2>()
    where Field1 : IBqlField
    where Field2 : IBqlField
  {
    return this.IsUpdatedOnly(typeof (Field1), typeof (Field2));
  }

  public static bool Contains<TField>() where TField : IBqlField
  {
    UpdateIfFieldsChangedScope.Changes slot = PXContext.GetSlot<UpdateIfFieldsChangedScope.Changes>();
    return slot != null && slot.SourceOfChange?.Contains(typeof (TField)).GetValueOrDefault();
  }

  public static bool Any(Func<Type, bool> predicate)
  {
    UpdateIfFieldsChangedScope.Changes slot = PXContext.GetSlot<UpdateIfFieldsChangedScope.Changes>();
    if (slot == null)
      return false;
    HashSet<Type> sourceOfChange = slot.SourceOfChange;
    return (sourceOfChange != null ? new bool?(sourceOfChange.Any<Type>(predicate)) : new bool?()).GetValueOrDefault();
  }

  public static UpdateIfFieldsChangedScope Create(
    PXCache cache,
    object oldRow,
    object newRow,
    params Type[] fields)
  {
    List<Type> source = new List<Type>();
    foreach (Type field in fields)
    {
      if (!object.Equals(cache.GetValue(oldRow, field.Name), cache.GetValue(newRow, field.Name)))
        source.Add(field);
    }
    return !source.Any<Type>() ? (UpdateIfFieldsChangedScope) null : new UpdateIfFieldsChangedScope().AppendContext(source.ToArray());
  }

  public class Changes
  {
    public HashSet<Type> SourceOfChange { get; set; }

    public int ReferenceCounter { get; set; }
  }
}
