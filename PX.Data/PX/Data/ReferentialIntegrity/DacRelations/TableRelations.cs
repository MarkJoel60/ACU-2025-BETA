// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.DacRelations.TableRelations
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Collections.Immutable;

#nullable disable
namespace PX.Data.ReferentialIntegrity.DacRelations;

[PXInternalUseOnly]
public class TableRelations
{
  public System.Type Table { get; }

  public ImmutableList<TableRelations.Relation> OutRelations { get; }

  public ImmutableList<TableRelations.Relation> InRelations { get; }

  public TableRelations(
    System.Type table,
    IEnumerable<TableRelations.Relation> outRelations,
    IEnumerable<TableRelations.Relation> inRelations)
  {
    this.Table = table;
    this.OutRelations = ImmutableList.ToImmutableList<TableRelations.Relation>(outRelations);
    this.InRelations = ImmutableList.ToImmutableList<TableRelations.Relation>(inRelations);
  }

  [PXInternalUseOnly]
  public class Relation
  {
    public System.Type FromTable { get; }

    public System.Type ToTable { get; }

    public FieldRelations FieldRelations { get; }

    public Relation(System.Type fromTable, System.Type toTable, FieldRelations fieldRelations)
    {
      this.FromTable = fromTable;
      this.ToTable = toTable;
      this.FieldRelations = fieldRelations;
    }
  }
}
