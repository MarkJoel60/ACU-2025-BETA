// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Merging.TableMergedReferencesInspector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.ReferentialIntegrity.Inspecting;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Merging;

/// <summary>
/// Performs inspection of <see cref="T:PX.Data.IBqlTable" /> referential relationship.
/// </summary>
internal class TableMergedReferencesInspector : ITableMergedReferencesInspector
{
  private readonly Task<IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult>> _processAllMergedReferences;
  private readonly Task<ILookup<System.Type, Reference>> _createApplicableIncomingReferencesLookup;
  private readonly Task<ILookup<System.Type, Reference>> _createApplicableOutgoingReferencesLookup;

  public TableMergedReferencesInspector(
    ITableReferenceInspector tableReferenceInspector,
    IReferenceMerger referenceMerger,
    ILogger logger)
  {
    this._processAllMergedReferences = tableReferenceInspector.AllReferencesAreInspected.ContinueWith<IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult>>((Func<Task, IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult>>) (_ =>
    {
      using (Operation operation = LoggerOperationExtensions.OperationAt(logger, (LogEventLevel) 1, new LogEventLevel?()).Begin("Merging all collected DAC references", Array.Empty<object>()))
      {
        IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult> readOnlyDictionary = referenceMerger.MergeReferences(tableReferenceInspector.GetReferencesOfAllDacs());
        operation.Complete();
        return readOnlyDictionary;
      }
    }));
    this._createApplicableIncomingReferencesLookup = this._processAllMergedReferences.ContinueWith<ILookup<System.Type, Reference>>((Func<Task<IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult>>, ILookup<System.Type, Reference>>) (mergeTask => mergeTask.Result.SelectMany(t => t.Value.IncomingMergedReferences.SelectMany(r => ((IEnumerable<System.Type>) r.ApplicableParents).Select(e => new
    {
      Ref = r.Reference,
      Parent = e
    }))).ToLookup(t => t.Parent, t => t.Ref)));
    this._createApplicableOutgoingReferencesLookup = this._processAllMergedReferences.ContinueWith<ILookup<System.Type, Reference>>((Func<Task<IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult>>, ILookup<System.Type, Reference>>) (mergeTask => mergeTask.Result.SelectMany(t => t.Value.OutgoingMergedReferences.SelectMany(r => ((IEnumerable<System.Type>) r.ApplicableChildren).Select(e => new
    {
      Ref = r.Reference,
      Child = e
    }))).ToLookup(t => t.Child, t => t.Ref)));
    this.AllReferencesAreMerged = Task.WhenAll((Task) this._processAllMergedReferences, (Task) this._createApplicableIncomingReferencesLookup, (Task) this._createApplicableOutgoingReferencesLookup);
  }

  /// <summary>
  /// Gets information about referential relationship between all <see cref="T:PX.Data.IBqlTable" />s
  /// that are included in referential integrity check
  /// </summary>
  public Dictionary<System.Type, MergedReferencesInspectionResult> GetMergedReferencesOfAllTables()
  {
    return this._processAllMergedReferences.Result.ToDictionary<KeyValuePair<System.Type, MergedReferencesInspectionResult>, System.Type, MergedReferencesInspectionResult>((Func<KeyValuePair<System.Type, MergedReferencesInspectionResult>, System.Type>) (r => r.Key), (Func<KeyValuePair<System.Type, MergedReferencesInspectionResult>, MergedReferencesInspectionResult>) (r => r.Value));
  }

  /// <summary>
  /// Gets information about referential relationship between the given <see cref="T:PX.Data.IBqlTable" />
  /// that is included in referential integrity check.
  /// </summary>
  /// <param name="bqlTable">Inspecting <see cref="T:PX.Data.IBqlTable" /></param>
  public MergedReferencesInspectionResult GetMergedReferencesOf(System.Type bqlTable)
  {
    TableMergedReferencesInspector.ThrowIfNotBqlTable(bqlTable);
    MergedReferencesInspectionResult inspectionResult;
    return this._processAllMergedReferences.Result.TryGetValue(bqlTable, out inspectionResult) ? inspectionResult : new MergedReferencesInspectionResult(bqlTable, Enumerable.Empty<MergedReference>(), Enumerable.Empty<MergedReference>());
  }

  /// <summary>
  /// Gets information about referential relationship between the given <see cref="T:PX.Data.IBqlTable" />
  /// and other <see cref="T:PX.Data.IBqlTable" />s that are included in referential integrity check.
  /// </summary>
  public MergedReferencesInspectionResult GetMergedReferencesOf<TTable>() where TTable : IBqlTable
  {
    return this.GetMergedReferencesOf(typeof (TTable));
  }

  public IEnumerable<Reference> GetIncomingReferencesApplicableTo(System.Type bqlTable)
  {
    TableMergedReferencesInspector.ThrowIfNotBqlTable(bqlTable);
    return this._createApplicableIncomingReferencesLookup.Result[bqlTable];
  }

  public IEnumerable<Reference> GetOutgoingReferencesApplicableTo(System.Type bqlTable)
  {
    TableMergedReferencesInspector.ThrowIfNotBqlTable(bqlTable);
    return this._createApplicableOutgoingReferencesLookup.Result[bqlTable];
  }

  /// <summary>
  /// Indicates whether the process of merging <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s is already completed
  /// </summary>
  public Task AllReferencesAreMerged { get; }

  private static void ThrowIfNotBqlTable(System.Type bqlTable)
  {
    if (bqlTable == (System.Type) null)
      throw new PXArgumentException(nameof (bqlTable), "The argument cannot be null.");
    if (!typeof (IBqlTable).IsAssignableFrom(bqlTable))
      throw new PXArgumentException(nameof (bqlTable), "The inspected type must implement the IBqlTable interface.");
  }
}
