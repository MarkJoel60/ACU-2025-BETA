// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.DacRelations.FieldRelations
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

#nullable disable
namespace PX.Data.ReferentialIntegrity.DacRelations;

[PXInternalUseOnly]
public class FieldRelations
{
  public ImmutableDictionary<RelationType, ImmutableList<FieldRelations.Relation>> Relations { get; }

  public FieldRelations(
    IDictionary<RelationType, IEnumerable<FieldRelations.Relation>> relations)
  {
    this.Relations = ImmutableDictionary.ToImmutableDictionary<KeyValuePair<RelationType, IEnumerable<FieldRelations.Relation>>, RelationType, ImmutableList<FieldRelations.Relation>>((IEnumerable<KeyValuePair<RelationType, IEnumerable<FieldRelations.Relation>>>) relations, (Func<KeyValuePair<RelationType, IEnumerable<FieldRelations.Relation>>, RelationType>) (kv => kv.Key), (Func<KeyValuePair<RelationType, IEnumerable<FieldRelations.Relation>>, ImmutableList<FieldRelations.Relation>>) (kv => ImmutableList.ToImmutableList<FieldRelations.Relation>(kv.Value)));
  }

  [PXInternalUseOnly]
  public class Relation
  {
    public ImmutableList<Tuple<System.Type, System.Type>> FieldPairs { get; }

    public Relation(IEnumerable<Tuple<System.Type, System.Type>> fieldPairs)
    {
      this.FieldPairs = ImmutableList.ToImmutableList<Tuple<System.Type, System.Type>>(fieldPairs);
    }
  }
}
