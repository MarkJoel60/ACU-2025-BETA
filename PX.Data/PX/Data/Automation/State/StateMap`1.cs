// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.StateMap`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class StateMap<State>
{
  private readonly Dictionary<string, State> Content = new Dictionary<string, State>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
  private readonly List<string> Order = new List<string>();

  public void Add(string key, State value)
  {
    if (this.Content.ContainsKey(key))
    {
      this.Content.Remove(key);
      this.Order.Remove(key);
    }
    this.Content[key] = value;
    this.Order.Add(key);
  }

  public StateMap<State>.NamedState[] GetList()
  {
    return this.Order.Select<string, StateMap<State>.NamedState>((Func<string, StateMap<State>.NamedState>) (_ => new StateMap<State>.NamedState(_, this.Content[_]))).ToArray<StateMap<State>.NamedState>();
  }

  public Dictionary<string, State> GetDictionary()
  {
    return EnumerableExtensions.ToDictionary<string, State>((IEnumerable<KeyValuePair<string, State>>) this.Content);
  }

  public State GetValue(string key)
  {
    State state;
    this.Content.TryGetValue(key, out state);
    return state;
  }

  public bool TryGetValue(string key, out State value) => this.Content.TryGetValue(key, out value);

  public void Rename(string oldKey, string newKey)
  {
    State state;
    if (!this.Content.TryGetValue(oldKey, out state))
      return;
    this.Content.Remove(oldKey);
    this.Content[newKey] = state;
    int index = this.Order.IndexOf(oldKey);
    if (index >= 0)
      this.Order[index] = newKey;
    else
      this.Order.Add(newKey);
  }

  public bool IsEmpty() => !this.Content.Any<KeyValuePair<string, State>>();

  public sealed class NamedState
  {
    public readonly string Name;
    public readonly State Value;

    public NamedState(string name, State value)
    {
      this.Name = name;
      this.Value = value;
    }
  }
}
