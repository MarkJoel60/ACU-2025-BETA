// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.ParameterMap
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic;

internal class ParameterMap
{
  private readonly BqlBuilder _builder;
  private readonly Dictionary<Type, List<object>> _map = new Dictionary<Type, List<object>>();

  internal ParameterMap(BqlBuilder builder) => this._builder = builder;

  public IEnumerable<object> All
  {
    get
    {
      return this.ParametersWithAddress.Select<Tuple<object, Type, int>, object>((Func<Tuple<object, Type, int>, object>) (p => p.Item1));
    }
  }

  public IEnumerable<object> this[Type table] => (IEnumerable<object>) this._map[table];

  public object this[int index]
  {
    get
    {
      Tuple<Type, int> relative = this.AbsoluteIndexToRelative(index);
      if (relative == null)
        throw new IndexOutOfRangeException();
      return this._map[relative.Item1][relative.Item2];
    }
    set
    {
      Tuple<Type, int> relative = this.AbsoluteIndexToRelative(index);
      if (relative == null)
        throw new IndexOutOfRangeException();
      this._map[relative.Item1][relative.Item2] = value;
    }
  }

  public object this[Type table, int index]
  {
    get => this._map[table][index];
    set => this._map[table][index] = value;
  }

  internal void SetSection(Type table, object[] parameters)
  {
    Dictionary<Type, List<object>> map = this._map;
    Type key = table;
    if ((object) key == null)
      key = this._builder.Select.Table;
    List<object> objectList = (parameters != null ? ((IEnumerable<object>) parameters).ToList<object>() : (List<object>) null) ?? new List<object>();
    map[key] = objectList;
  }

  internal void RemoveSection(Type table)
  {
    Dictionary<Type, List<object>> map = this._map;
    Type key = table;
    if ((object) key == null)
      key = this._builder.Select.Table;
    map.Remove(key);
  }

  internal void AppendToSection(Type table, object[] parameters)
  {
    Dictionary<Type, List<object>> map = this._map;
    Type key = table;
    if ((object) key == null)
      key = this._builder.Select.Table;
    map[key].Add((object) ((IEnumerable<object>) parameters ?? Enumerable.Empty<object>()));
  }

  private Tuple<Type, int> AbsoluteIndexToRelative(int index)
  {
    return this.ParametersWithAddress.Skip<Tuple<object, Type, int>>(index).FirstOrDefault<Tuple<object, Type, int>>().With<Tuple<object, Type, int>, Tuple<Type, int>>((Func<Tuple<object, Type, int>, Tuple<Type, int>>) (p => Tuple.Create<Type, int>(p.Item2, p.Item3)));
  }

  private IEnumerable<Tuple<object, Type, int>> ParametersWithAddress
  {
    get
    {
      return this._builder.Tables.Where<Type>((Func<Type, bool>) (t => this._map.ContainsKey(t))).SelectMany<Type, Tuple<object, Type, int>>((Func<Type, IEnumerable<Tuple<object, Type, int>>>) (t => this._map[t].Select<object, Tuple<object, Type, int>>((Func<object, int, Tuple<object, Type, int>>) ((p, i) => Tuple.Create<object, Type, int>(p, t, i)))));
    }
  }
}
