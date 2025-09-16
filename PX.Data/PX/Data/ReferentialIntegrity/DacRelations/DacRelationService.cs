// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.DacRelations.DacRelationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Merging;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.ReferentialIntegrity.DacRelations;

internal class DacRelationService : IDacRelationService
{
  private readonly IReferenceMerger _referenceMerger;
  private readonly Lazy<IDictionary<System.Type, PX.Data.ReferentialIntegrity.DacRelations.TableRelations>> TableRelations;

  public PX.Data.ReferentialIntegrity.DacRelations.TableRelations GetTableRelations(System.Type table)
  {
    if (table.IsDefined(typeof (PXProjectionAttribute)))
      return this.GetProjectionRelations(table);
    PX.Data.ReferentialIntegrity.DacRelations.TableRelations tableRelations;
    return this.TableRelations.Value.TryGetValue(table, out tableRelations) || this.TableRelations.Value.TryGetValue(this._referenceMerger.GetSuggestedType(table), out tableRelations) ? tableRelations : (PX.Data.ReferentialIntegrity.DacRelations.TableRelations) null;
  }

  /// <summary>
  /// If table is a projection, we need to collect it's references, references from base classes
  /// and references from DAC's which are defined in Select of <see cref="T:PX.Data.PXProjectionAttribute" />
  /// if fields from their references exist in this projection.
  /// </summary>
  /// <param name="table">The projection table.</param>
  /// <returns>Relations of projection table.</returns>
  private PX.Data.ReferentialIntegrity.DacRelations.TableRelations GetProjectionRelations(System.Type table)
  {
    PX.Data.ReferentialIntegrity.DacRelations.TableRelations tableRelations1;
    this.TableRelations.Value.TryGetValue(table, out tableRelations1);
    if (tableRelations1 == null)
      tableRelations1 = new PX.Data.ReferentialIntegrity.DacRelations.TableRelations(table, Enumerable.Empty<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>(), Enumerable.Empty<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>());
    List<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation> list1 = ((IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>) tableRelations1.InRelations).ToList<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>();
    List<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation> list2 = ((IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>) tableRelations1.OutRelations).ToList<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>();
    IEnumerable<System.Type> types = ((IEnumerable<System.Type>) table.GetCustomAttribute<PXProjectionAttribute>().GetTables()).Union<System.Type>(table.GetInheritanceChain().TakeWhile<System.Type>((Func<System.Type, bool>) (baseTable => typeof (IBqlTable).IsAssignableFrom(baseTable)))).Where<System.Type>((Func<System.Type, bool>) (t => t != table));
    List<(string, System.Type)> fields = this.GetFieldsFromAnotherTableForProjection(table);
    foreach (System.Type table1 in types)
    {
      PX.Data.ReferentialIntegrity.DacRelations.TableRelations tableRelations2 = this.GetTableRelations(table1);
      if (tableRelations2 != null)
      {
        list1.AddRange(((IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>) tableRelations2.InRelations).Where<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>((Func<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation, bool>) (rel => this.IsReferenceAllowed(rel, fields, false))));
        list2.AddRange(((IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>) tableRelations2.OutRelations).Where<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>((Func<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation, bool>) (rel => this.IsReferenceAllowed(rel, fields, true))));
      }
    }
    return !list1.Any<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>() && !list2.Any<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>() ? (PX.Data.ReferentialIntegrity.DacRelations.TableRelations) null : new PX.Data.ReferentialIntegrity.DacRelations.TableRelations(table, (IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>) list2, (IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>) list1);
  }

  private bool IsReferenceAllowed(
    PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation relation,
    List<(string FieldName, System.Type BqlTable)> allowedFields,
    bool isOutgoing)
  {
    System.Type typeInRelation = isOutgoing ? relation.FromTable : relation.ToTable;
    HashSet<string> allowedFieldsForTable = allowedFields.Where<(string, System.Type)>((Func<(string, System.Type), bool>) (f => f.BqlTable == typeInRelation)).Select<(string, System.Type), string>((Func<(string, System.Type), string>) (f => f.FieldName)).ToHashSet<string>();
    if (allowedFieldsForTable.Count<string>() == 0)
      return false;
    foreach (ImmutableList<FieldRelations.Relation> immutableList in relation.FieldRelations.Relations.Values)
    {
      foreach (FieldRelations.Relation relation1 in immutableList)
      {
        if ((isOutgoing ? ((IEnumerable<Tuple<System.Type, System.Type>>) relation1.FieldPairs).Select<Tuple<System.Type, System.Type>, string>((Func<Tuple<System.Type, System.Type>, string>) (f => f.Item1.Name)) : ((IEnumerable<Tuple<System.Type, System.Type>>) relation1.FieldPairs).Select<Tuple<System.Type, System.Type>, string>((Func<Tuple<System.Type, System.Type>, string>) (f => f.Item2.Name))).All<string>((Func<string, bool>) (linkedField => allowedFieldsForTable.Any<string>((Func<string, bool>) (allowedField => allowedField.Equals(linkedField, StringComparison.OrdinalIgnoreCase))))))
          return true;
      }
    }
    return false;
  }

  private List<(string FieldName, System.Type BqlTable)> GetFieldsFromAnotherTableForProjection(
    System.Type projectionTable)
  {
    return ((IEnumerable<PropertyInfo>) projectionTable.GetProperties()).Select<PropertyInfo, (string, System.Type)>((Func<PropertyInfo, (string, System.Type)>) (prop => this.GetFieldFromAnotherTable(prop, projectionTable))).Where<(string, System.Type)>((Func<(string, System.Type), bool>) (field => field.FieldName != null)).ToList<(string, System.Type)>();
  }

  private (string FieldName, System.Type BqlTable) GetFieldFromAnotherTable(
    PropertyInfo prop,
    System.Type projectionTable)
  {
    PXDBFieldAttribute customAttribute = prop.GetCustomAttribute<PXDBFieldAttribute>(false);
    if (customAttribute != null && customAttribute.BqlTable != (System.Type) null && customAttribute.BqlTable != projectionTable)
      return (prop.Name, customAttribute.BqlTable);
    return prop.DeclaringType != projectionTable ? (prop.Name, prop.DeclaringType) : ((string) null, (System.Type) null);
  }

  public DacRelationService(
    ITableMergedReferencesInspector mergedReferences,
    IReferenceMerger referenceMerger,
    ILogger logger)
  {
    this._referenceMerger = referenceMerger;
    this.TableRelations = new Lazy<IDictionary<System.Type, PX.Data.ReferentialIntegrity.DacRelations.TableRelations>>((Func<IDictionary<System.Type, PX.Data.ReferentialIntegrity.DacRelations.TableRelations>>) (() =>
    {
      using (Operation operation = LoggerOperationExtensions.OperationAt(logger, (LogEventLevel) 1, new LogEventLevel?()).Begin("Building DAC relations map from DAC references", Array.Empty<object>()))
      {
        Dictionary<System.Type, PX.Data.ReferentialIntegrity.DacRelations.TableRelations> dictionary = mergedReferences.GetMergedReferencesOfAllTables().Select<KeyValuePair<System.Type, MergedReferencesInspectionResult>, (System.Type, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>)>((Func<KeyValuePair<System.Type, MergedReferencesInspectionResult>, (System.Type, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>)>) (kv => (kv.Key, DacRelationService.ReferencesToRelations((IEnumerable<MergedReference>) kv.Value.OutgoingMergedReferences), DacRelationService.ReferencesToRelations((IEnumerable<MergedReference>) kv.Value.IncomingMergedReferences)))).Where<(System.Type, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>)>((Func<(System.Type, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>), bool>) (kv => kv.outgoingRelations.Any<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>() || kv.incomingRelations.Any<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>())).ToDictionary<(System.Type, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>), System.Type, PX.Data.ReferentialIntegrity.DacRelations.TableRelations>((Func<(System.Type, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>), System.Type>) (kv => kv.key), (Func<(System.Type, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>, IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>), PX.Data.ReferentialIntegrity.DacRelations.TableRelations>) (kv => new PX.Data.ReferentialIntegrity.DacRelations.TableRelations(kv.key, kv.outgoingRelations, kv.incomingRelations)));
        operation.Complete();
        return (IDictionary<System.Type, PX.Data.ReferentialIntegrity.DacRelations.TableRelations>) dictionary;
      }
    }));
  }

  private static RelationType OriginToRelation(ReferenceOrigin origin)
  {
    switch (origin)
    {
      case ReferenceOrigin.ForeignKeyApi:
        return RelationType.Key;
      case ReferenceOrigin.ParentAttribute:
      case ReferenceOrigin.DeclareReferenceAttribute:
      case ReferenceOrigin.SelectorAttribute:
        return RelationType.Direct;
      default:
        return RelationType.Indirect;
    }
  }

  private static FieldRelations.Relation ReferenceToFieldRelation(MergedReference reference)
  {
    TableWithKeys child = reference.Reference.Child;
    TableWithKeys parent = reference.Reference.Parent;
    System.Type table1 = child.Table;
    System.Type table2 = parent.Table;
    IEnumerable<(System.Type, System.Type)> source = child.KeyFields.Zip<System.Type, System.Type, (System.Type, System.Type)>((IEnumerable<System.Type>) parent.KeyFields, (Func<System.Type, System.Type, (System.Type, System.Type)>) ((f, t) => (f, t)));
    System.Type type = table2;
    if (table1 == type)
      source = source.Where<(System.Type, System.Type)>((Func<(System.Type, System.Type), bool>) (tuple => tuple.fromField.Name != tuple.toField.Name)).Select<(System.Type, System.Type), (System.Type, System.Type)>((Func<(System.Type, System.Type), (System.Type, System.Type)>) (tuple => string.Compare(tuple.fromField.Name, tuple.toField.Name) <= 0 ? (tuple.toField, tuple.fromField) : tuple));
    return new FieldRelations.Relation(source.Select<(System.Type, System.Type), Tuple<System.Type, System.Type>>((Func<(System.Type, System.Type), Tuple<System.Type, System.Type>>) (t => t.ToTuple<System.Type, System.Type>())).Distinct<Tuple<System.Type, System.Type>>());
  }

  private static IEnumerable<PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation> ReferencesToRelations(
    IEnumerable<MergedReference> references)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return references.GroupBy<MergedReference, (System.Type, System.Type)>((Func<MergedReference, (System.Type, System.Type)>) (r =>
    {
      TableWithKeys tableWithKeys = r.Reference.Child;
      System.Type table1 = tableWithKeys.Table;
      tableWithKeys = r.Reference.Parent;
      System.Type table2 = tableWithKeys.Table;
      return (table1, table2);
    })).Select<IGrouping<(System.Type, System.Type), MergedReference>, ((System.Type, System.Type), Dictionary<RelationType, IEnumerable<FieldRelations.Relation>>)>((Func<IGrouping<(System.Type, System.Type), MergedReference>, ((System.Type, System.Type), Dictionary<RelationType, IEnumerable<FieldRelations.Relation>>)>) (referencesByTables => ((referencesByTables.Key.child, referencesByTables.Key.parent), referencesByTables.GroupBy<MergedReference, RelationType>((Func<MergedReference, RelationType>) (r => DacRelationService.OriginToRelation(r.Reference.ReferenceOrigin))).Select<IGrouping<RelationType, MergedReference>, (RelationType, IEnumerable<FieldRelations.Relation>)>((Func<IGrouping<RelationType, MergedReference>, (RelationType, IEnumerable<FieldRelations.Relation>)>) (referencesByType => (referencesByType.Key, referencesByType.Select<MergedReference, FieldRelations.Relation>(DacRelationService.\u003C\u003EO.\u003C0\u003E__ReferenceToFieldRelation ?? (DacRelationService.\u003C\u003EO.\u003C0\u003E__ReferenceToFieldRelation = new Func<MergedReference, FieldRelations.Relation>(DacRelationService.ReferenceToFieldRelation))).Where<FieldRelations.Relation>((Func<FieldRelations.Relation, bool>) (r => r.FieldPairs.Count > 0)).Distinct<FieldRelations.Relation>((IEqualityComparer<FieldRelations.Relation>) new DacRelationService.RelationComparer())))).Where<(RelationType, IEnumerable<FieldRelations.Relation>)>((Func<(RelationType, IEnumerable<FieldRelations.Relation>), bool>) (c => c.relations.Any<FieldRelations.Relation>())).ToDictionary<(RelationType, IEnumerable<FieldRelations.Relation>), RelationType, IEnumerable<FieldRelations.Relation>>((Func<(RelationType, IEnumerable<FieldRelations.Relation>), RelationType>) (c => c.relationType), (Func<(RelationType, IEnumerable<FieldRelations.Relation>), IEnumerable<FieldRelations.Relation>>) (c => c.relations))))).Where<((System.Type, System.Type), Dictionary<RelationType, IEnumerable<FieldRelations.Relation>>)>((Func<((System.Type, System.Type), Dictionary<RelationType, IEnumerable<FieldRelations.Relation>>), bool>) (c => c.fieldRelations.Any<KeyValuePair<RelationType, IEnumerable<FieldRelations.Relation>>>())).Select<((System.Type, System.Type), Dictionary<RelationType, IEnumerable<FieldRelations.Relation>>), PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>((Func<((System.Type, System.Type), Dictionary<RelationType, IEnumerable<FieldRelations.Relation>>), PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation>) (c => new PX.Data.ReferentialIntegrity.DacRelations.TableRelations.Relation(c.tables.from, c.tables.to, new FieldRelations((IDictionary<RelationType, IEnumerable<FieldRelations.Relation>>) c.fieldRelations))));
  }

  private class RelationComparer : IEqualityComparer<FieldRelations.Relation>
  {
    private static int GetFieldPairsHash(IEnumerable<Tuple<System.Type, System.Type>> pairs)
    {
      int fieldPairsHash = 19;
      foreach (Tuple<System.Type, System.Type> pair in pairs)
        fieldPairsHash = fieldPairsHash * 31 /*0x1F*/ + pair.GetHashCode();
      return fieldPairsHash;
    }

    public bool Equals(FieldRelations.Relation a, FieldRelations.Relation b)
    {
      if (a == b)
        return true;
      return a != null && b != null && a.FieldPairs.Count == b.FieldPairs.Count && this.GetHashCode(a) == this.GetHashCode(b);
    }

    public int GetHashCode(FieldRelations.Relation relation)
    {
      return DacRelationService.RelationComparer.GetFieldPairsHash((IEnumerable<Tuple<System.Type, System.Type>>) relation.FieldPairs);
    }
  }
}
