// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.EntityToGuidBindService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Api.Services;

public class EntityToGuidBindService
{
  private readonly Dictionary<EntityToGuidBindService.EntityDescriptor, Guid> _guids = new Dictionary<EntityToGuidBindService.EntityDescriptor, Guid>((IEqualityComparer<EntityToGuidBindService.EntityDescriptor>) new EntityToGuidBindService.EntityDescriptor.Comparer());
  private readonly Dictionary<(Guid, string), EntityToGuidBindService.EntityDescriptor> _descriptors = new Dictionary<(Guid, string), EntityToGuidBindService.EntityDescriptor>();
  private readonly Dictionary<(Guid, string), EntityToGuidBindService.EntityDescriptor> _obsoletes = new Dictionary<(Guid, string), EntityToGuidBindService.EntityDescriptor>();
  private const string SessionKey = "EntityToGuidBindService";
  internal const string NoteIdFieldName = "NoteID";
  internal const char NonKeySeparator = '_';
  public const char KeySeparator = '|';

  internal static IDisposable SetupContextInstance()
  {
    if (EntityToGuidBindService.GetContextInstance() != null)
      return Disposable.Empty;
    PXContext.SetSlot<EntityToGuidBindService>(new EntityToGuidBindService());
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return Disposable.Create(EntityToGuidBindService.\u003C\u003EO.\u003C0\u003E__ClearSlot ?? (EntityToGuidBindService.\u003C\u003EO.\u003C0\u003E__ClearSlot = new System.Action(PXContext.ClearSlot<EntityToGuidBindService>)));
  }

  private static EntityToGuidBindService GetContextInstance()
  {
    return PXContext.GetSlot<EntityToGuidBindService>();
  }

  public static EntityToGuidBindService Instance
  {
    get
    {
      EntityToGuidBindService contextInstance = EntityToGuidBindService.GetContextInstance();
      if (contextInstance != null)
        return contextInstance;
      if (!(PXContext.Session[nameof (EntityToGuidBindService)] is EntityToGuidBindService instance))
      {
        instance = new EntityToGuidBindService();
        PXContext.Session[nameof (EntityToGuidBindService)] = (object) instance;
      }
      return instance;
    }
  }

  public Guid GetGuid(string graphName, string[] keys, string[] values, string source = "undefined")
  {
    return this.GetGuid(new EntityToGuidBindService.EntityDescriptor(graphName, keys, values, source));
  }

  public Guid GetGuid(EntityToGuidBindService.EntityDescriptor key)
  {
    Guid guid;
    if (!this._guids.TryGetValue(key, out guid))
    {
      guid = Guid.NewGuid();
      this._guids.Add(key, guid);
      this._descriptors.Add((guid, key.Source), key);
    }
    return guid;
  }

  public EntityToGuidBindService.EntityDescriptor GetDescriptor(Guid guid, string source)
  {
    EntityToGuidBindService.EntityDescriptor descriptor;
    if (this.TryGetDescriptor(guid, source, out descriptor))
      return descriptor;
    throw new EntityToGuidBindService.EntityDescriptorNotFoundException(guid, source);
  }

  public void MarkObsolete(Guid guid, string source)
  {
    (Guid, string) key = (guid, source);
    EntityToGuidBindService.EntityDescriptor entityDescriptor;
    if (!this._descriptors.TryGetValue(key, out entityDescriptor) || this._obsoletes.ContainsKey(key))
      return;
    this._obsoletes.Add(key, entityDescriptor);
  }

  public bool TryToRemove(Guid guid, string source)
  {
    (Guid, string) key1 = (guid, source);
    EntityToGuidBindService.EntityDescriptor key2;
    if (!this._obsoletes.TryGetValue(key1, out key2))
      return true;
    this._guids.Remove(key2);
    this._descriptors.Remove(key1);
    this._obsoletes.Remove(key1);
    return false;
  }

  public void SubstituteGuid(Guid oldGuid, Guid newGuid, string source)
  {
    (Guid, string) key1 = (oldGuid, source);
    (Guid, string) key2 = (newGuid, source);
    EntityToGuidBindService.EntityDescriptor key3;
    if (!this._descriptors.TryGetValue(key1, out key3))
      return;
    this._descriptors.Remove(key1);
    this._guids.Remove(key3);
    this._descriptors.Add(key2, key3);
    this._guids.Add(key3, newGuid);
  }

  public bool TryGetDescriptor(
    Guid guid,
    string source,
    out EntityToGuidBindService.EntityDescriptor descriptor)
  {
    if (this._descriptors.TryGetValue((guid, source), out descriptor))
      return true;
    descriptor = new EntityToGuidBindService.EntityDescriptor();
    return false;
  }

  public static string FormatKey(string objectName, string field)
  {
    return $"{objectName}{(ValueType) '|'}{field}";
  }

  public static string FormatNonKeyField(string objectName, string field)
  {
    return $"{objectName}{(ValueType) '_'}{field}";
  }

  public static KeyValuePair<string, string> ParseKey(string key)
  {
    int length = key.LastIndexOf('|');
    return new KeyValuePair<string, string>(key.Substring(0, length), key.Substring(length + 1));
  }

  public static string[] FormatKeys(Command[] keys)
  {
    return ((IEnumerable<Command>) keys).Select<Command, string>((Func<Command, string>) (c => EntityToGuidBindService.FormatKey(c.ObjectName, c.FieldName ?? c.Value))).Distinct<string>().ToArray<string>();
  }

  public void BindGuid(
    Guid guid,
    string graphName,
    string[] keys,
    string[] values,
    string source = "undefined")
  {
    this.BindGuid(guid, new EntityToGuidBindService.EntityDescriptor(graphName, keys, values, source));
  }

  internal void BindGuid(
    Guid guid,
    EntityToGuidBindService.EntityDescriptor descriptor)
  {
    (Guid, string) key = (guid, descriptor.Source);
    if (this._descriptors.ContainsKey(key) && this._guids.ContainsKey(descriptor))
      return;
    this._guids[descriptor] = guid;
    this._descriptors[key] = descriptor;
  }

  public struct EntityDescriptor
  {
    private readonly string _descriptorKey;
    public readonly KeyValuePair<string, string>[] Items;
    public Guid? NoteId;
    public string NoteView;
    public readonly string GraphName;
    public readonly string Source;
    public bool IsArchived;

    public EntityDescriptor(string graphName, string[] keys, string[] values, string source)
    {
      this.Items = ((IEnumerable<string>) keys).Zip<string, string, KeyValuePair<string, string>>((IEnumerable<string>) values, (Func<string, string, KeyValuePair<string, string>>) ((key, value) => new KeyValuePair<string, string>(key, value))).OrderBy<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (p => p.Key)).ToArray<KeyValuePair<string, string>>();
      this.NoteId = new Guid?();
      this.NoteView = (string) null;
      this.GraphName = graphName;
      this.Source = source;
      this._descriptorKey = $"{graphName}&{source}&{((IEnumerable<KeyValuePair<string, string>>) this.Items).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (p => $"{p.Key}={p.Value ?? ""}")).JoinToString<string>("&")}";
      this.IsArchived = PXDatabase.ReadOnlyArchived;
    }

    public EntityDescriptor(string graphName, string detailView, Guid noteId, string source)
    {
      this.Items = (KeyValuePair<string, string>[]) null;
      this.NoteId = new Guid?(noteId);
      this.NoteView = detailView;
      this.GraphName = graphName;
      this.Source = source;
      this._descriptorKey = $"{graphName}&{source}&NoteID&{noteId.ToString()}";
      this.IsArchived = PXDatabase.ReadOnlyArchived;
    }

    public override string ToString() => this._descriptorKey;

    public class Comparer : IEqualityComparer<EntityToGuidBindService.EntityDescriptor>
    {
      public bool Equals(
        EntityToGuidBindService.EntityDescriptor x,
        EntityToGuidBindService.EntityDescriptor y)
      {
        return x.ToString().OrdinalEquals(y.ToString());
      }

      public int GetHashCode(EntityToGuidBindService.EntityDescriptor obj)
      {
        return obj.ToString().GetHashCode();
      }
    }
  }

  internal class EntityDescriptorNotFoundException : Exception
  {
    public EntityDescriptorNotFoundException(Guid guid, string source)
      : base($"{guid.ToString()}, {source}")
    {
    }

    public EntityDescriptorNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      ReflectionSerializer.RestoreObjectProps<EntityToGuidBindService.EntityDescriptorNotFoundException>(this, info);
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      ReflectionSerializer.GetObjectData<EntityToGuidBindService.EntityDescriptorNotFoundException>(this, info);
      base.GetObjectData(info, context);
    }
  }
}
