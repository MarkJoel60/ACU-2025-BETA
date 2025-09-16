// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Inspecting.TableReferenceProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Inspecting;

/// <summary>
/// Performs collecting and inspection of <see cref="T:PX.Data.IBqlTable" /> referential relationship.
/// </summary>
internal class TableReferenceProcessor : ITableReferenceCollector, ITableReferenceInspector
{
  private readonly ILogger _logger;
  private readonly TableReferenceProcessor.ReferenceStorage _storage = new TableReferenceProcessor.ReferenceStorage();
  private readonly TaskCompletionSource<bool> _allReferencesAreCollectedTcs = new TaskCompletionSource<bool>();
  private readonly Task<IReadOnlyDictionary<System.Type, ReferencesInspectionResult>> _processAllCollectedReferences;

  public TableReferenceProcessor(ILogger logger)
  {
    this._logger = logger;
    this._processAllCollectedReferences = this._allReferencesAreCollectedTcs.Task.ContinueWith<IReadOnlyDictionary<System.Type, ReferencesInspectionResult>>((Func<Task<bool>, IReadOnlyDictionary<System.Type, ReferencesInspectionResult>>) (_ => this.ProcessAllCollectedReferences()));
  }

  /// <summary>
  /// Indicates whether the process of collecting <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s is already completed
  /// </summary>
  public Task AllReferencesAreCollected => (Task) this._allReferencesAreCollectedTcs.Task;

  public bool TryCollectReference(Reference reference)
  {
    if (this.AllReferencesAreCollected.IsCompleted)
      return false;
    this._storage.Collect(reference.Parent.Table, reference);
    return true;
  }

  public void CollectionCompleted() => this._allReferencesAreCollectedTcs.TrySetResult(true);

  public Task AllReferencesAreInspected => (Task) this._processAllCollectedReferences;

  /// <summary>
  /// Gets information about referential relationship between all <see cref="T:PX.Data.IBqlTable" />s
  /// that are included in referential integrity check
  /// </summary>
  public IReadOnlyDictionary<System.Type, ReferencesInspectionResult> GetReferencesOfAllDacs()
  {
    this.WaitUntilReferenceInspectionIsCompleted();
    return (IReadOnlyDictionary<System.Type, ReferencesInspectionResult>) EnumerableExtensions.AsReadOnly<System.Type, ReferencesInspectionResult>((IDictionary<System.Type, ReferencesInspectionResult>) this._processAllCollectedReferences.Result.ToDictionary<KeyValuePair<System.Type, ReferencesInspectionResult>, System.Type, ReferencesInspectionResult>((Func<KeyValuePair<System.Type, ReferencesInspectionResult>, System.Type>) (r => r.Key), (Func<KeyValuePair<System.Type, ReferencesInspectionResult>, ReferencesInspectionResult>) (r => r.Value)));
  }

  /// <summary>
  /// Gets information about referential relationship between the given <see cref="T:PX.Data.IBqlTable" />
  /// that is included in referential integrity check
  /// </summary>
  /// <param name="bqlTable">Inspecting <see cref="T:PX.Data.IBqlTable" /></param>
  public ReferencesInspectionResult GetReferencesOf(System.Type bqlTable)
  {
    if (bqlTable == (System.Type) null)
      throw new PXArgumentException(nameof (bqlTable), "The argument cannot be null.");
    if (!typeof (IBqlTable).IsAssignableFrom(bqlTable))
      throw new PXArgumentException(nameof (bqlTable), "The inspected type must implement the IBqlTable interface.");
    this.WaitUntilReferenceInspectionIsCompleted();
    ReferencesInspectionResult inspectionResult;
    return this._processAllCollectedReferences.Result.TryGetValue(bqlTable, out inspectionResult) ? inspectionResult : new ReferencesInspectionResult(bqlTable, Enumerable.Empty<Reference>(), Enumerable.Empty<Reference>());
  }

  /// <summary>
  /// Gets information about referential relationship between the given <see cref="T:PX.Data.IBqlTable" />
  /// and other <see cref="T:PX.Data.IBqlTable" />s that are included in referential integrity check
  /// </summary>
  public ReferencesInspectionResult GetReferencesOf<TTable>() where TTable : IBqlTable
  {
    return this.GetReferencesOf(typeof (TTable));
  }

  private IReadOnlyDictionary<System.Type, ReferencesInspectionResult> ProcessAllCollectedReferences()
  {
    using (Operation operation = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 1, new LogEventLevel?()).Begin("Processing all collected DAC references", Array.Empty<object>()))
    {
      Reference[] array1 = this._storage.GetCollectedReferences().SelectMany<KeyValuePair<System.Type, ReadOnlyCollection<Reference>>, Reference>((Func<KeyValuePair<System.Type, ReadOnlyCollection<Reference>>, IEnumerable<Reference>>) (t => (IEnumerable<Reference>) t.Value)).ToArray<Reference>();
      System.Type[] array2 = ((IEnumerable<Reference>) array1).Select<Reference, System.Type>((Func<Reference, System.Type>) (t => t.Child.Table)).Concat<System.Type>(((IEnumerable<Reference>) array1).Select<Reference, System.Type>((Func<Reference, System.Type>) (t => t.Parent.Table))).Distinct<System.Type>().ToArray<System.Type>();
      ILookup<System.Type, Reference> outgoing = ((IEnumerable<Reference>) array1).ToLookup<Reference, System.Type>((Func<Reference, System.Type>) (r => r.Child.Table));
      ILookup<System.Type, Reference> incoming = ((IEnumerable<Reference>) array1).ToLookup<Reference, System.Type>((Func<Reference, System.Type>) (r => r.Parent.Table));
      Func<System.Type, ReferencesInspectionResult> selector = (Func<System.Type, ReferencesInspectionResult>) (t => new ReferencesInspectionResult(t, outgoing[t], incoming[t]));
      ReadOnlyDictionary<System.Type, ReferencesInspectionResult> readOnlyDictionary = EnumerableExtensions.AsReadOnly<System.Type, ReferencesInspectionResult>((IDictionary<System.Type, ReferencesInspectionResult>) ((IEnumerable<System.Type>) array2).Select<System.Type, ReferencesInspectionResult>(selector).ToDictionary<ReferencesInspectionResult, System.Type>((Func<ReferencesInspectionResult, System.Type>) (t => t.InspectingTable)));
      this._storage.ClearCollectedReferences();
      operation.Complete();
      return (IReadOnlyDictionary<System.Type, ReferencesInspectionResult>) readOnlyDictionary;
    }
  }

  private void WaitUntilReferenceInspectionIsCompleted()
  {
    this._processAllCollectedReferences.Wait();
  }

  private class ReferenceStorage
  {
    private readonly object _fkLock = new object();
    private readonly Dictionary<System.Type, HashSet<Reference>> _tablesToReferences = new Dictionary<System.Type, HashSet<Reference>>();

    internal void Collect(System.Type parentTable, Reference reference)
    {
      lock (this._fkLock)
        this._tablesToReferences.GetOrAdd<System.Type, HashSet<Reference>>(parentTable, (Func<System.Type, HashSet<Reference>>) (_ => new HashSet<Reference>())).Add(reference);
    }

    public IReadOnlyDictionary<System.Type, ReadOnlyCollection<Reference>> GetCollectedReferences()
    {
      lock (this._fkLock)
        return (IReadOnlyDictionary<System.Type, ReadOnlyCollection<Reference>>) EnumerableExtensions.AsReadOnly<System.Type, ReadOnlyCollection<Reference>>((IDictionary<System.Type, ReadOnlyCollection<Reference>>) this._tablesToReferences.ToDictionary<KeyValuePair<System.Type, HashSet<Reference>>, System.Type, ReadOnlyCollection<Reference>>((Func<KeyValuePair<System.Type, HashSet<Reference>>, System.Type>) (t => t.Key), (Func<KeyValuePair<System.Type, HashSet<Reference>>, ReadOnlyCollection<Reference>>) (t => t.Value.ToList<Reference>().AsReadOnly())));
    }

    public void ClearCollectedReferences()
    {
      lock (this._fkLock)
        this._tablesToReferences.Clear();
    }
  }
}
