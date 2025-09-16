// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlFieldMapper`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL;

public class BqlFieldMapper<MapFrom, TMapTo> : IBqlFieldMapper, IBqlFieldMappingResolver
  where MapFrom : class, IBqlTable, new()
  where TMapTo : class, IBqlTable, new()
{
  private readonly Dictionary<string, SQLExpression> _map = new Dictionary<string, SQLExpression>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private readonly Dictionary<System.Type, IFbqlSet> _overrides = new Dictionary<System.Type, IFbqlSet>();

  public System.Type MappedFrom { get; } = typeof (MapFrom);

  public System.Type MappedTo { get; } = typeof (TMapTo);

  public Dictionary<string, SQLExpression> GetMapping(PXGraph graph) => this.EnsureMap(graph);

  public IBqlFieldMapper Map<TFbqlSet>() where TFbqlSet : IFbqlSet
  {
    this._overrides.Add(typeof (TFbqlSet), (IFbqlSet) Activator.CreateInstance<TFbqlSet>());
    return (IBqlFieldMapper) this;
  }

  private Dictionary<string, SQLExpression> EnsureMap(PXGraph graph)
  {
    if (this._map.Any<KeyValuePair<string, SQLExpression>>())
      return this._map;
    PXCache cach1 = graph.Caches[typeof (TMapTo)];
    foreach (KeyValuePair<System.Type, IFbqlSet> keyValuePair1 in this._overrides)
    {
      List<KeyValuePair<SQLExpression, SQLExpression>> assignments = new List<KeyValuePair<SQLExpression, SQLExpression>>();
      keyValuePair1.Value.AppendExpression(graph, new BqlCommandInfo(), assignments);
      foreach (KeyValuePair<SQLExpression, SQLExpression> keyValuePair2 in assignments)
      {
        SQLExpression sqlExpression1;
        SQLExpression sqlExpression2;
        EnumerableExtensions.Deconstruct<SQLExpression, SQLExpression>(keyValuePair2, ref sqlExpression1, ref sqlExpression2);
        SQLExpression sqlExpression3 = sqlExpression1;
        SQLExpression sqlExpression4 = sqlExpression2;
        if (sqlExpression3 is Column column && this.EqualMapTo(column.Table().AliasOrName()))
          this._map[column.Name] = sqlExpression4;
        else
          throw new PXException("A field from the UNION query cannot be matched to any field of the {0} DAC.", new object[1]
          {
            (object) typeof (TMapTo).Name
          });
      }
    }
    PXCache cach2 = graph.Caches[typeof (MapFrom)];
    foreach (string field in (List<string>) cach1.Fields)
    {
      string bqlFieldTo = field;
      if (!this._map.ContainsKey(bqlFieldTo))
      {
        int index = cach2.Fields.FindIndex((Predicate<string>) (f => f.OrdinalEquals(bqlFieldTo)));
        this._map[bqlFieldTo] = index >= 0 ? (SQLExpression) new Column(cach2.Fields[index], cach2.GetItemType()) : SQLExpression.Null();
      }
    }
    return this._map;
  }

  private bool EqualMapTo(string table)
  {
    for (System.Type type = typeof (TMapTo); type != typeof (PXBqlTable) && type != typeof (object) && type != (System.Type) null; type = type.BaseType)
    {
      if (table.OrdinalEquals(type.Name))
        return true;
    }
    return false;
  }
}
