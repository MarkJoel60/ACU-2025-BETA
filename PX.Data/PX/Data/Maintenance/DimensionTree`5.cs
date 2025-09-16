// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.DimensionTree`5
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.Maintenance;

/// <summary>
/// Provides methods to travel through the elements of <typeparamref name="TEntity" /> type in tree structure based on some <see cref="T:PX.Data.Maintenance.DimensionTree`1.Dimension" />.
/// </summary>
/// <typeparam name="TEntity">The type of the elements of a tree.</typeparam>
/// <typeparam name="TDimension">The dimension that defines structure of a tree.</typeparam>
/// <typeparam name="TNaturalKeyField">The <see cref="T:PX.Data.IBqlField" /> of the natural key (CD) of the elements of a tree.</typeparam>
/// <typeparam name="TSurrogateKeyField">The <see cref="T:PX.Data.IBqlField" /> of the surrogate key (ID) of the elements of a tree.</typeparam>
[ImmutableObject(true)]
public class DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField> : 
  DimensionTree<TDimension>,
  IPrefetchable,
  IPXCompanyDependent
  where TTree : DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>, new()
  where TEntity : class, IBqlTable, new()
  where TDimension : IConstant<string>, IBqlOperand, new()
  where TNaturalKeyField : IBqlField
  where TSurrogateKeyField : IBqlField
{
  private readonly PXCache<TEntity> _cache = (PXCache<TEntity>) new DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.PXNoEventsCache<TEntity>(new PXGraph());
  private readonly DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode _root = new DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode();
  private readonly Dictionary<string, DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode> _shortcutsByNatural = new Dictionary<string, DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode>();
  private readonly Dictionary<int, DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode> _shortcutsBySurrogate = new Dictionary<int, DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode>();

  public static IEnumerable<TEntity> EnrollNodes(int? parentID)
  {
    TTree instance = DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.Instance;
    return parentID.HasValue ? instance.GetNearestChildrenOf(parentID.Value) : instance.GetRoots();
  }

  public static TTree Instance
  {
    get
    {
      return PXContext.GetSlot<TTree>() ?? PXContext.SetSlot<TTree>(DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.GetFromDBSlot());
    }
  }

  private static TTree GetFromDBSlot()
  {
    return PXDatabase.GetLocalizableSlot<TTree>(typeof (TTree).FullName, DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.GetTablesToWatch());
  }

  private new static System.Type[] GetTablesToWatch()
  {
    return DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.EnrollUnderlyingTables(typeof (TEntity)).Concat<System.Type>((IEnumerable<System.Type>) DimensionTree<TDimension>.GetTablesToWatch()).ToArray<System.Type>();
  }

  private static IEnumerable<System.Type> EnrollUnderlyingTables(System.Type table)
  {
    List<System.Type> source = new List<System.Type>() { table };
    if (table.BaseType != typeof (object))
      source.AddRange(DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.EnrollUnderlyingTables(table.BaseType));
    if (table.IsDefined(typeof (PXProjectionAttribute), false))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      source.AddRange(((IEnumerable<System.Type>) table.GetCustomAttribute<PXProjectionAttribute>(false).GetTables()).SelectMany<System.Type, System.Type>(DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.\u003C\u003EO.\u003C0\u003E__EnrollUnderlyingTables ?? (DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.\u003C\u003EO.\u003C0\u003E__EnrollUnderlyingTables = new Func<System.Type, IEnumerable<System.Type>>(DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.EnrollUnderlyingTables))));
    }
    return source.Distinct<System.Type>();
  }

  public string MakeWildcard(int key)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return this.GetNodeByID(key).With<TEntity, string>(new Func<TEntity, string>(this.GetNaturalKey)).With<string, string>(DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.\u003C\u003EO.\u003C1\u003E__MakeWildcard ?? (DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.\u003C\u003EO.\u003C1\u003E__MakeWildcard = new Func<string, string>(DimensionTree<TDimension>.MakeWildcard)));
  }

  public TEntity GetNodeByID(int surrogateKey)
  {
    return !this._shortcutsBySurrogate.ContainsKey(surrogateKey) ? default (TEntity) : DimensionTree<TDimension>.Clone<TEntity>(this._shortcutsBySurrogate[surrogateKey].Value);
  }

  public TEntity GetNodeByCD(string naturalKey)
  {
    return !this._shortcutsByNatural.ContainsKey(naturalKey) ? default (TEntity) : DimensionTree<TDimension>.Clone<TEntity>(this._shortcutsByNatural[naturalKey].Value);
  }

  public IEnumerable<TEntity> GetRoots()
  {
    return DimensionTree<TDimension>.CloneSequence<TEntity>(this._root.Children.Select<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>((Func<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>) (c => c.Value)));
  }

  public IEnumerable<TEntity> GetNearestChildrenOf(TEntity parent)
  {
    return this.GetNearestChildrenOf(this.GetSurrogateKey(parent));
  }

  public IEnumerable<TEntity> GetAllChildrenOf(TEntity parent)
  {
    return this.GetAllChildrenOf(this.GetSurrogateKey(parent));
  }

  public IEnumerable<TEntity> GetParentsOf(TEntity child)
  {
    return this.GetParentsOf(this.GetSurrogateKey(child));
  }

  public IEnumerable<TEntity> GetNearestChildrenOf(int parentKey)
  {
    return !this._shortcutsBySurrogate.ContainsKey(parentKey) ? Enumerable.Empty<TEntity>() : DimensionTree<TDimension>.CloneSequence<TEntity>(this._shortcutsBySurrogate[parentKey].Children.Select<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>((Func<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>) (r => r.Value)));
  }

  public IEnumerable<TEntity> GetAllChildrenOf(int parentKey)
  {
    if (!this._shortcutsBySurrogate.ContainsKey(parentKey))
      return Enumerable.Empty<TEntity>();
    DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode treeNode = this._shortcutsBySurrogate[parentKey];
    return DimensionTree<TDimension>.CloneSequence<TEntity>(treeNode.Children.Select<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>((Func<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>) (r => r.Value)).Concat<TEntity>(treeNode.Children.SelectMany<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>((Func<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, IEnumerable<TEntity>>) (c => this.GetAllChildrenOf(c.Value)))));
  }

  public IEnumerable<TEntity> GetParentsOf(int childKey)
  {
    return !this._shortcutsBySurrogate.ContainsKey(childKey) ? Enumerable.Empty<TEntity>() : DimensionTree<TDimension>.CloneSequence<TEntity>(this.EnrollParents(this._shortcutsBySurrogate[childKey]));
  }

  public IEnumerable<TEntity> GetNearestChildrenOf(string parentKey)
  {
    return !this._shortcutsByNatural.ContainsKey(parentKey) ? Enumerable.Empty<TEntity>() : DimensionTree<TDimension>.CloneSequence<TEntity>(this._shortcutsByNatural[parentKey].Children.Select<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>((Func<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>) (r => r.Value)));
  }

  public IEnumerable<TEntity> GetAllChildrenOf(string parentKey)
  {
    if (!this._shortcutsByNatural.ContainsKey(parentKey))
      return Enumerable.Empty<TEntity>();
    DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode treeNode = this._shortcutsByNatural[parentKey];
    return DimensionTree<TDimension>.CloneSequence<TEntity>(treeNode.Children.Select<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>((Func<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>) (r => r.Value)).Concat<TEntity>(treeNode.Children.SelectMany<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, TEntity>((Func<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, IEnumerable<TEntity>>) (c => this.GetAllChildrenOf(c.Value)))));
  }

  public IEnumerable<TEntity> GetParentsOf(string childKey)
  {
    return !this._shortcutsByNatural.ContainsKey(childKey) ? Enumerable.Empty<TEntity>() : DimensionTree<TDimension>.CloneSequence<TEntity>(this.EnrollParents(this._shortcutsByNatural[childKey]));
  }

  public void Prefetch()
  {
    try
    {
      this.BuildTree(this.GetElements(), DimensionTree<TDimension>.Segments);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      throw;
    }
  }

  protected virtual void PrepareElement(TEntity original)
  {
  }

  protected virtual TEntity[] GetElements()
  {
    return PXDatabase.SelectRecords<TEntity>().ToArray<TEntity>();
  }

  private IEnumerable<TEntity> EnrollParents(
    DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode node)
  {
    for (DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode parent = node.Parent; parent != null && parent != this._root; parent = parent.Parent)
      yield return parent.Value;
  }

  private void BuildTree(TEntity[] flattenElements, DimensionTree<TDimension>.Segment[] segments)
  {
    ILookup<\u003C\u003Ef__AnonymousType44<int>, \u003C\u003Ef__AnonymousType43<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode, string, int, int>> lookup = ((IEnumerable<TEntity>) flattenElements).Select(element => new
    {
      element = element,
      naturalKey = this.GetNaturalKey(element).TrimEnd(' ')
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier0 = _param1,
      surrogateKey = this.GetSurrogateKey(_param1.element)
    }).Select(_param1 =>
    {
      var data = _param1;
      string naturalKey = _param1.\u003C\u003Eh__TransparentIdentifier0.naturalKey;
      int length = naturalKey != null ? naturalKey.Length : 0;
      return new
      {
        \u003C\u003Eh__TransparentIdentifier1 = data,
        keyLength = length
      };
    }).Where(_param1 => _param1.keyLength > 0).Select(_param1 => EnumerableExtensions.Aggregate((IEnumerable<DimensionTree<TDimension>.Segment>) segments, new
    {
      ConsumedLength = 0,
      FilledSegmentsCount = 0,
      Break = false
    }, (accum, segment) => new
    {
      ConsumedLength = accum.ConsumedLength + (int) segment.Length.GetValueOrDefault(),
      FilledSegmentsCount = accum.FilledSegmentsCount + 1,
      Break = _param1.keyLength <= accum.ConsumedLength + (int) segment.Length.GetValueOrDefault()
    }, accum => new
    {
      Node = new DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode()
      {
        Value = _param1.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.element
      },
      NaturalKey = DimensionTree<TDimension>.PadKey(_param1.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.naturalKey, accum.ConsumedLength),
      SurrogateKey = _param1.\u003C\u003Eh__TransparentIdentifier1.surrogateKey,
      FilledSegmentsCount = accum.FilledSegmentsCount
    }, accum => accum.Break)).ToLookup(e => new
    {
      FilledSegmentsCount = e.FilledSegmentsCount
    });
    \u003C\u003Ef__AnonymousType44<int>[] array = Enumerable.Range(0, segments.Length + 1).Select(i => new
    {
      FilledSegmentsCount = i
    }).ToArray();
    for (int index1 = array.LastIndex(); index1 >= 0; --index1)
    {
      foreach (var data1 in lookup[array[index1]])
      {
        var child = data1;
        for (int index2 = index1 - 1; index2 >= 0; --index2)
        {
          var data2 = lookup[array[index2]].FirstOrDefault(potentialParent => child.NaturalKey.StartsWith(potentialParent.NaturalKey, true, CultureInfo.InvariantCulture));
          if (data2?.Node != null)
          {
            child.Node.Parent = data2.Node;
            break;
          }
        }
        if (child.Node.Parent == null)
          child.Node.Parent = this._root;
        this.PrepareElement(child.Node.Value);
        this._shortcutsBySurrogate.Add(child.SurrogateKey, child.Node);
        this._shortcutsByNatural.Add(child.NaturalKey, child.Node);
        string key = child.NaturalKey.TrimEnd();
        if (key != child.NaturalKey)
          this._shortcutsByNatural.Add(key, child.Node);
        string naturalKey = this.GetNaturalKey(child.Node.Value);
        if (EnumerableExtensions.IsNotIn<string>(naturalKey, key, child.NaturalKey))
          this._shortcutsByNatural.Add(naturalKey, child.Node);
      }
    }
  }

  private string GetNaturalKey(TEntity entity)
  {
    return this._cache.GetValue<TNaturalKeyField>((object) entity).ToString();
  }

  private int GetSurrogateKey(TEntity entity)
  {
    return (int) this._cache.GetValue<TSurrogateKeyField>((object) entity);
  }

  private class TreeNode
  {
    private readonly List<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode> _children = new List<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode>();
    private DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode _parent;

    public TEntity Value { get; set; }

    public DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode Parent
    {
      get => this._parent;
      set
      {
        if (object.Equals((object) this._parent, (object) value))
          return;
        this._parent?._children.Remove(this);
        this._parent = value;
        this._parent?._children.Add(this);
      }
    }

    public IEnumerable<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode> Children
    {
      get
      {
        return (IEnumerable<DimensionTree<TTree, TEntity, TDimension, TNaturalKeyField, TSurrogateKeyField>.TreeNode>) this._children;
      }
    }
  }

  private class PXNoEventsCache<TNode> : PXCache<TNode> where TNode : class, IBqlTable, new()
  {
    public PXNoEventsCache(PXGraph graph)
      : base(graph.Caches[typeof (TNode)].Graph)
    {
      this._EventsRowAttr.RowSelecting = (IPXRowSelectingSubscriber[]) null;
      this._EventsRowAttr.RowSelected = (IPXRowSelectedSubscriber[]) null;
      this._EventsRowAttr.RowInserting = (IPXRowInsertingSubscriber[]) null;
      this._EventsRowAttr.RowInserted = (IPXRowInsertedSubscriber[]) null;
      this._EventsRowAttr.RowUpdating = (IPXRowUpdatingSubscriber[]) null;
      this._EventsRowAttr.RowUpdated = (IPXRowUpdatedSubscriber[]) null;
      this._EventsRowAttr.RowDeleting = (IPXRowDeletingSubscriber[]) null;
      this._EventsRowAttr.RowDeleted = (IPXRowDeletedSubscriber[]) null;
      this._EventsRowAttr.RowPersisting = (IPXRowPersistingSubscriber[]) null;
      this._EventsRowAttr.RowPersisted = (IPXRowPersistedSubscriber[]) null;
    }
  }
}
