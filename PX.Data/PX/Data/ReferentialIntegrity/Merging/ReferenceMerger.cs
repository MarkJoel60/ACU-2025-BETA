// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Merging.ReferenceMerger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.ReferentialIntegrity.Inspecting;
using PX.Metadata;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Merging;

/// <summary>
/// Performs merging of similar <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s by weighting
/// their corresponding <see cref="T:PX.Data.IBqlTable" />s and deciding which of them is more primary.
/// </summary>
internal class ReferenceMerger : IReferenceMerger
{
  private readonly Lazy<IReadOnlyList<System.Type>> _allBqlTables;
  private readonly Lazy<IReadOnlyDictionary<System.Type, ReferenceMerger.RemappingDecision>> _inheritanceMap;
  private readonly Lazy<IReadOnlyDictionary<System.Type, ReferenceMerger.RemappingDecision>> _mergeMap;

  public ReferenceMerger(ITableReferenceInspector tableReferenceInspector, IDacRegistry dacRegistry)
  {
    ReferenceMerger referenceMerger = this;
    if (tableReferenceInspector == null)
      throw new ArgumentNullException(nameof (tableReferenceInspector));
    if (dacRegistry == null)
      throw new ArgumentNullException(nameof (dacRegistry));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this._allBqlTables = new Lazy<IReadOnlyList<System.Type>>((Func<IReadOnlyList<System.Type>>) (() => (IReadOnlyList<System.Type>) dacRegistry.All.Where<System.Type>(ReferenceMerger.\u003C\u003EO.\u003C0\u003E__IsSupportedTable ?? (ReferenceMerger.\u003C\u003EO.\u003C0\u003E__IsSupportedTable = new Func<System.Type, bool>(ReferenceMerger.IsSupportedTable))).ToArray<System.Type>()));
    this._inheritanceMap = new Lazy<IReadOnlyDictionary<System.Type, ReferenceMerger.RemappingDecision>>((Func<IReadOnlyDictionary<System.Type, ReferenceMerger.RemappingDecision>>) (() => (IReadOnlyDictionary<System.Type, ReferenceMerger.RemappingDecision>) EnumerableExtensions.AsReadOnly<System.Type, ReferenceMerger.RemappingDecision>((IDictionary<System.Type, ReferenceMerger.RemappingDecision>) tableReferenceInspector.GetReferencesOfAllDacs().Keys.Union<System.Type>(referenceMerger.AllBqlTables).Select(type => new
    {
      type = type,
      baseType = !type.BaseType.IsBqlTable() || type.IgnoreInheritanceChain() ? type : type.BaseType
    }).Select(_param1 => new
    {
      Type = _param1.type,
      Decision = ReferenceMerger.RemappingDecision.Decide(_param1.type, _param1.baseType)
    }).ToDictionary(t => t.Type, t => t.Decision))));
    this._mergeMap = new Lazy<IReadOnlyDictionary<System.Type, ReferenceMerger.RemappingDecision>>((Func<IReadOnlyDictionary<System.Type, ReferenceMerger.RemappingDecision>>) (() =>
    {
      Dictionary<System.Type, ReferenceMerger.RemappingDecision> dictionary = new Dictionary<System.Type, ReferenceMerger.RemappingDecision>();
      EnumerableExtensions.AddRange<System.Type, ReferenceMerger.RemappingDecision>((IDictionary<System.Type, ReferenceMerger.RemappingDecision>) dictionary, (IEnumerable<KeyValuePair<System.Type, ReferenceMerger.RemappingDecision>>) referenceMerger._inheritanceMap.Value);
      foreach (IEnumerable<System.Type> source in referenceMerger.AllBqlTables.GroupBy<System.Type, string>((Func<System.Type, string>) (t => t.Name)).Where<IGrouping<string, System.Type>>((Func<IGrouping<string, System.Type>, bool>) (g => g.Count<System.Type>() > 1)))
      {
        System.Type[] array = source.OrderBy<System.Type, int>((Func<System.Type, int>) (t => new ReferenceMerger.WeightedType(t).FullWeight)).ToArray<System.Type>();
        ReferenceMerger.RemappingDecision remappingDecision = (ReferenceMerger.RemappingDecision) null;
        System.Type descendantType = ((IEnumerable<System.Type>) array).First<System.Type>();
        foreach (System.Type baseType in ((IEnumerable<System.Type>) array).Skip<System.Type>(1))
        {
          remappingDecision = ReferenceMerger.RemappingDecision.Decide(descendantType, baseType);
          descendantType = baseType;
        }
        if (remappingDecision != null)
        {
          foreach (System.Type key in array)
            dictionary[key] = remappingDecision;
        }
      }
      return (IReadOnlyDictionary<System.Type, ReferenceMerger.RemappingDecision>) EnumerableExtensions.AsReadOnly<System.Type, ReferenceMerger.RemappingDecision>((IDictionary<System.Type, ReferenceMerger.RemappingDecision>) dictionary);
    }));
  }

  private static bool IsSupportedTable(System.Type table) => table.TableSupportsReferences();

  private IEnumerable<System.Type> AllBqlTables => (IEnumerable<System.Type>) this._allBqlTables.Value;

  public IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult> MergeReferences(
    IReadOnlyDictionary<System.Type, ReferencesInspectionResult> collectedReferences)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    System.Type[] array1 = collectedReferences.Keys.Where<System.Type>(ReferenceMerger.\u003C\u003EO.\u003C0\u003E__IsSupportedTable ?? (ReferenceMerger.\u003C\u003EO.\u003C0\u003E__IsSupportedTable = new Func<System.Type, bool>(ReferenceMerger.IsSupportedTable))).Union<System.Type>(this.AllBqlTables).ToArray<System.Type>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    Func<System.Type, IEnumerable<MergedReference>> getReferencesFor = (Func<System.Type, IEnumerable<MergedReference>>) (t => !collectedReferences.ContainsKey(t) ? Enumerable.Empty<MergedReference>() : collectedReferences[t].OutgoingReferences.Select<Reference, MergedReference>(ReferenceMerger.\u003C\u003EO.\u003C1\u003E__FromReference ?? (ReferenceMerger.\u003C\u003EO.\u003C1\u003E__FromReference = new Func<Reference, MergedReference>(MergedReference.FromReference))));
    ILookup<System.Type, MergedReference> incomingReferencesMap = ((IEnumerable<MergedReference>) this.MergeReferences((IEnumerable<System.Type>) array1, getReferencesFor, true)).ToLookup<MergedReference, System.Type>((Func<MergedReference, System.Type>) (r => r.Reference.Parent.Table));
    MergedReference[] source = this.MergeReferences((IEnumerable<System.Type>) array1, (Func<System.Type, IEnumerable<MergedReference>>) (t => incomingReferencesMap[t]), false);
    System.Type[] array2 = ((IEnumerable<MergedReference>) source).SelectMany<MergedReference, System.Type>((Func<MergedReference, IEnumerable<System.Type>>) (t => ((IEnumerable<System.Type>) t.ApplicableParents).Concat<System.Type>((IEnumerable<System.Type>) t.ApplicableChildren))).Distinct<System.Type>().ToArray<System.Type>();
    ILookup<System.Type, MergedReference> outgoing = ((IEnumerable<MergedReference>) source).ToLookup<MergedReference, System.Type>((Func<MergedReference, System.Type>) (r => r.Reference.Child.Table));
    ILookup<System.Type, MergedReference> incoming = ((IEnumerable<MergedReference>) source).ToLookup<MergedReference, System.Type>((Func<MergedReference, System.Type>) (r => r.Reference.Parent.Table));
    Func<System.Type, bool> predicate = (Func<System.Type, bool>) (t => outgoing[t].Any<MergedReference>() || incoming[t].Any<MergedReference>());
    ReadOnlyDictionary<System.Type, MergedReferencesInspectionResult> readOnlyDictionary = EnumerableExtensions.AsReadOnly<System.Type, MergedReferencesInspectionResult>((IDictionary<System.Type, MergedReferencesInspectionResult>) ((IEnumerable<System.Type>) array2).Where<System.Type>(predicate).Select<System.Type, MergedReferencesInspectionResult>((Func<System.Type, MergedReferencesInspectionResult>) (t => new MergedReferencesInspectionResult(t, outgoing[t], incoming[t]))).ToDictionary<MergedReferencesInspectionResult, System.Type>((Func<MergedReferencesInspectionResult, System.Type>) (t => t.InspectingTable)));
    ReferenceMerger.RemappingDecision.ClearCaches();
    return (IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult>) readOnlyDictionary;
  }

  public System.Type GetSuggestedType(System.Type originalType)
  {
    ReferenceMerger.RemappingDecision remappingDecision;
    return !this._mergeMap.Value.TryGetValue(originalType, out remappingDecision) ? originalType : remappingDecision.SuggestedType;
  }

  private MergedReference[] MergeReferences(
    IEnumerable<System.Type> mergingTypes,
    Func<System.Type, IEnumerable<MergedReference>> getReferencesFor,
    bool childrenSubstitution)
  {
    Dictionary<System.Type, MergedReference[]> dictionary = new Dictionary<System.Type, MergedReference[]>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    foreach (System.Type key in (IEnumerable<System.Type>) mergingTypes.OrderByDescending<System.Type, int>(ReferenceMerger.\u003C\u003EO.\u003C2\u003E__GetInheritanceDepth ?? (ReferenceMerger.\u003C\u003EO.\u003C2\u003E__GetInheritanceDepth = new Func<System.Type, int>(TableReferencesHelper.GetInheritanceDepth))))
      this.RelinkReferences(this._inheritanceMap.Value[key], getReferencesFor, childrenSubstitution, dictionary);
    foreach (IEnumerable<System.Type> types in ((IEnumerable<System.Type>) dictionary.Keys.ToArray<System.Type>()).GroupBy<System.Type, string>((Func<System.Type, string>) (t => t.Name)).Where<IGrouping<string, System.Type>>((Func<IGrouping<string, System.Type>, bool>) (g => g.Count<System.Type>() > 1)))
    {
      foreach (System.Type key in types)
        this.RelinkReferences(this._mergeMap.Value[key], getReferencesFor, childrenSubstitution, dictionary);
    }
    return dictionary.SelectMany<KeyValuePair<System.Type, MergedReference[]>, MergedReference>((Func<KeyValuePair<System.Type, MergedReference[]>, IEnumerable<MergedReference>>) (t => (IEnumerable<MergedReference>) t.Value)).ToArray<MergedReference>();
  }

  private void RelinkReferences(
    ReferenceMerger.RemappingDecision decision,
    Func<System.Type, IEnumerable<MergedReference>> getReferencesFor,
    bool childrenSubstitution,
    Dictionary<System.Type, MergedReference[]> mergedReferences)
  {
    Func<MergedReference[], MergedReference[], MergedReference[]> func = (Func<MergedReference[], MergedReference[], MergedReference[]>) ((left, right) => ((IEnumerable<MergedReference>) left).Select<MergedReference, Reference>((Func<MergedReference, Reference>) (t => t.Reference)).Union<Reference>(((IEnumerable<MergedReference>) right).Select<MergedReference, Reference>((Func<MergedReference, Reference>) (t => t.Reference))).GroupJoin((IEnumerable<MergedReference>) left, (Func<Reference, Reference>) (r => r), (Func<MergedReference, Reference>) (oldRef => oldRef.Reference), (r, oldRefJoin) => new
    {
      r = r,
      oldRefJoin = oldRefJoin
    }).SelectMany(_param1 => _param1.oldRefJoin.DefaultIfEmpty<MergedReference>(), (_param1, oldRef) => new
    {
      \u003C\u003Eh__TransparentIdentifier0 = _param1,
      oldRef = oldRef
    }).GroupJoin((IEnumerable<MergedReference>) right, _param1 => _param1.\u003C\u003Eh__TransparentIdentifier0.r, (Func<MergedReference, Reference>) (newRef => newRef.Reference), (_param1, newRefJoin) => new
    {
      \u003C\u003Eh__TransparentIdentifier1 = _param1,
      newRefJoin = newRefJoin
    }).SelectMany(_param1 => _param1.newRefJoin.DefaultIfEmpty<MergedReference>(), (_param1, newRef) => new
    {
      \u003C\u003Eh__TransparentIdentifier2 = _param1,
      newRef = newRef
    }).Where(_param1 => _param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.oldRef != null || _param1.newRef != null).Select(_param1 => _param1.newRef == null || _param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.oldRef == null ? (_param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.oldRef ?? _param1.newRef).Copy() : _param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.oldRef.AppendApplicableTables(_param1.newRef)).ToArray<MergedReference>());
    System.Type type = decision.Descendant.Type;
    System.Type suggestedType = decision.SuggestedType;
    MergedReference[] array1 = getReferencesFor(type).ToArray<MergedReference>();
    int num = suggestedType != type ? 1 : 0;
    mergedReferences.Ensure<System.Type, MergedReference[]>(type, (Func<MergedReference[]>) (() => new MergedReference[0]));
    if (num != 0)
    {
      MergedReference[] mergedReference1 = mergedReferences[type];
      MergedReference[] source1 = func(mergedReference1, array1);
      MergedReference[] array2 = ((IEnumerable<MergedReference>) source1).Where<MergedReference>((Func<MergedReference, bool>) (r => decision.CanSubstituteDescendantWithBaseType(r.Reference.ParentSelect))).ToArray<MergedReference>();
      List<MergedReference> list = EnumerableExtensions.ExceptBy<MergedReference, Reference>((IEnumerable<MergedReference>) source1, (IEnumerable<MergedReference>) array2, (Func<MergedReference, Reference>) (t => t.Reference), (IEqualityComparer<Reference>) null).ToList<MergedReference>();
      List<MergedReference> source2 = new List<MergedReference>();
      foreach (MergedReference mergedReference2 in array2)
      {
        try
        {
          MergedReference mergedReference3 = MergedReference.FromReference(Reference.FromParentSelect(BqlCommand.CreateInstance(this.SubstituteTable(mergedReference2.Reference.ParentSelect, type, suggestedType)), mergedReference2.Reference.ReferenceOrigin, mergedReference2.Reference.ReferenceBehavior, childrenSubstitution ? suggestedType : mergedReference2.Reference.Child.Table));
          if (!mergedReference3.Reference.TableReferencesToItself)
          {
            MergedReference mergedReference4 = childrenSubstitution ? mergedReference3.AppendApplicableChildren(((IEnumerable<System.Type>) mergedReference2.ApplicableChildren).Append<System.Type>(type)).AppendApplicableParents((IEnumerable<System.Type>) mergedReference2.ApplicableParents) : mergedReference3.AppendApplicableParents(((IEnumerable<System.Type>) mergedReference2.ApplicableParents).Append<System.Type>(type)).AppendApplicableChildren((IEnumerable<System.Type>) mergedReference2.ApplicableChildren);
            source2.Add(mergedReference4);
          }
        }
        catch (PXArgumentException ex)
        {
          list.Add(mergedReference2);
        }
      }
      if (source2.Any<MergedReference>())
      {
        mergedReferences.Ensure<System.Type, MergedReference[]>(suggestedType, (Func<MergedReference[]>) (() => new MergedReference[0]));
        mergedReferences[suggestedType] = func(mergedReferences[suggestedType], source2.ToArray());
      }
      if (list.Any<MergedReference>())
        mergedReferences[type] = list.ToArray();
      else
        mergedReferences.Remove(type);
    }
    else
      mergedReferences[type] = func(mergedReferences[type], array1);
  }

  private System.Type SubstituteTable(System.Type selectStatement, System.Type oldTable, System.Type newTable)
  {
    List<System.Type> typeList = new List<System.Type>();
    foreach (System.Type type in BqlCommand.Decompose(selectStatement))
    {
      if (typeof (IBqlTable).IsAssignableFrom(type) && type == oldTable)
        typeList.Add(newTable);
      else if (typeof (IBqlField).IsAssignableFrom(type) && BqlCommand.GetItemType(type) == oldTable)
      {
        System.Type nestedType = newTable.GetNestedType(type.Name);
        if (nestedType == (System.Type) null)
        {
          foreach (System.Type extension in PXCache._GetExtensions(newTable, false))
          {
            nestedType = extension.GetNestedType(type.Name);
            if (nestedType != (System.Type) null)
              break;
          }
        }
        typeList.Add(nestedType);
      }
      else
        typeList.Add(type);
    }
    return BqlCommand.Compose(typeList.ToArray());
  }

  [ImmutableObject(true)]
  [DebuggerDisplay("{Type} : {FullWeight} : {Weight}")]
  private class WeightedType
  {
    private static IEnumerable<string> DbTables { get; } = PXDatabase.Tables;

    public WeightedType(System.Type type)
    {
      this.Type = type;
      this.Weight = ReferenceMerger.WeightedType.CalculateTypeWeight(type);
      this.NamespaceDepth = ReferenceMerger.WeightedType.GetNamespaceDepth(type);
      this.FullWeight = 100 * (int) this.Weight - this.NamespaceDepth;
    }

    public System.Type Type { get; }

    public ReferenceMerger.WeightedType.TypeWeight Weight { get; }

    public int NamespaceDepth { get; }

    public int FullWeight { get; }

    private static ReferenceMerger.WeightedType.TypeWeight CalculateTypeWeight(System.Type type)
    {
      int num = !type.BaseType.IsBqlTable() ? 0 : (PXReferentialIntegrityCheckAttribute.HasReferentialIntegrity(type, false) ? 1 : 0);
      bool flag1 = type.IsDefined(typeof (PXTableAttribute), false);
      bool flag2 = ReferenceMerger.WeightedType.DbTables.Contains<string>(type.Name);
      bool flag3 = type.IsDefined(typeof (PXCacheNameAttribute), false);
      bool flag4 = !type.IsDefined(typeof (PXHiddenAttribute), false);
      bool flag5 = !type.IsNested;
      return (ReferenceMerger.WeightedType.TypeWeight) ((num != 0 ? 32 /*0x20*/ : 0) | (flag1 ? 16 /*0x10*/ : 0) | (flag2 ? 8 : 0) | (flag3 ? 4 : 0) | (flag4 ? 2 : 0) | (flag5 ? 1 : 0));
    }

    private static int GetNamespaceDepth(System.Type type)
    {
      if (type == (System.Type) null)
        throw new ArgumentNullException(nameof (type));
      int namespaceDepth = 0;
      if (type.FullName != null)
      {
        foreach (char ch in type.FullName)
        {
          switch (ch)
          {
            case '+':
            case '.':
              ++namespaceDepth;
              break;
            case '[':
              goto label_8;
          }
        }
      }
label_8:
      return namespaceDepth;
    }

    [Flags]
    public enum TypeWeight
    {
      HasReferentialIntegrity = 32, // 0x00000020
      IsPXTable = 16, // 0x00000010
      MappedDirectly = 8,
      HasCacheName = 4,
      NotHidden = 2,
      NotNested = 1,
      None = 0,
    }
  }

  [ImmutableObject(true)]
  [DebuggerDisplay("{SuggestedType}")]
  private class RemappingDecision
  {
    private static readonly ConcurrentDictionary<Tuple<System.Type, System.Type>, IEnumerable<System.Type>> FieldsDiffCache = new ConcurrentDictionary<Tuple<System.Type, System.Type>, IEnumerable<System.Type>>();
    private static readonly ConcurrentDictionary<System.Type, IEnumerable<System.Type>> FieldsCache = new ConcurrentDictionary<System.Type, IEnumerable<System.Type>>();

    public static ReferenceMerger.RemappingDecision Decide(System.Type descendantType, System.Type baseType)
    {
      if (descendantType == (System.Type) null)
        throw new ArgumentNullException(nameof (descendantType));
      return !(baseType == (System.Type) null) ? new ReferenceMerger.RemappingDecision(descendantType, baseType) : throw new ArgumentNullException(nameof (baseType));
    }

    private RemappingDecision(System.Type descendantType, System.Type baseType)
    {
      this.Descendant = new ReferenceMerger.WeightedType(descendantType);
      this.BaseType = new ReferenceMerger.WeightedType(baseType);
      this.FieldsIntroducedByDescendant = ReferenceMerger.RemappingDecision.GetFieldsIntroducedByDescendant(descendantType, baseType);
      this.SuggestedType = this.BaseType.FullWeight >= this.Descendant.FullWeight ? baseType : descendantType;
    }

    public ReferenceMerger.WeightedType Descendant { get; }

    public ReferenceMerger.WeightedType BaseType { get; }

    public IEnumerable<System.Type> FieldsIntroducedByDescendant { get; }

    public System.Type SuggestedType { get; }

    /// <summary>
    /// Indicates whether <see cref="P:PX.Data.ReferentialIntegrity.Merging.ReferenceMerger.RemappingDecision.Descendant" /> can be substituted by its <see cref="P:PX.Data.ReferentialIntegrity.Merging.ReferenceMerger.RemappingDecision.BaseType" /> in provided <paramref name="selectStatement" />
    /// </summary>
    public bool CanSubstituteDescendantWithBaseType(System.Type selectStatement)
    {
      if (selectStatement == (System.Type) null)
        throw new ArgumentNullException(nameof (selectStatement));
      return ((IEnumerable<System.Type>) BqlCommand.Decompose(selectStatement)).Where<System.Type>((Func<System.Type, bool>) (t => typeof (IBqlField).IsAssignableFrom(t) && BqlCommand.GetItemType(t) == this.Descendant.Type)).All<System.Type>((Func<System.Type, bool>) (t => !this.FieldsIntroducedByDescendant.Contains<System.Type>(t)));
    }

    private static IEnumerable<System.Type> GetFieldsIntroducedByDescendant(
      System.Type type,
      System.Type baseType)
    {
      return ReferenceMerger.RemappingDecision.FieldsDiffCache.GetOrAdd(Tuple.Create<System.Type, System.Type>(type, baseType), (Func<Tuple<System.Type, System.Type>, IEnumerable<System.Type>>) (tuple =>
      {
        System.Type[] array = EnumerableExtensions.ExceptBy<System.Type, string>(ReferenceMerger.RemappingDecision.FieldsCache.GetOrAdd(tuple.Item1, new Func<System.Type, IEnumerable<System.Type>>(GetNestedTypes)), ReferenceMerger.RemappingDecision.FieldsCache.GetOrAdd(tuple.Item2, new Func<System.Type, IEnumerable<System.Type>>(GetNestedTypes)), (Func<System.Type, string>) (f => f.Name), (IEqualityComparer<string>) null).ToArray<System.Type>();
        return array.Length != 0 ? (IEnumerable<System.Type>) array : Enumerable.Empty<System.Type>();
      }));

      static IEnumerable<System.Type> GetNestedTypes(System.Type t)
      {
        return (IEnumerable<System.Type>) ((IEnumerable<System.Type>) t.GetNestedTypes()).Concat<System.Type>(PXCache._GetExtensions(t, false).SelectMany<System.Type, System.Type>((Func<System.Type, IEnumerable<System.Type>>) (ext => (IEnumerable<System.Type>) ext.GetNestedTypes()))).ToArray<System.Type>();
      }
    }

    internal static void ClearCaches()
    {
      ReferenceMerger.RemappingDecision.FieldsCache.Clear();
      ReferenceMerger.RemappingDecision.FieldsDiffCache.Clear();
    }
  }
}
