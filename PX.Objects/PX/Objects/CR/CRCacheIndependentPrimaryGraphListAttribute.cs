// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCacheIndependentPrimaryGraphListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

public class CRCacheIndependentPrimaryGraphListAttribute : PXPrimaryGraphBaseAttribute, IEnumerable
{
  private readonly IList<CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph> _items;

  public CRCacheIndependentPrimaryGraphListAttribute()
  {
    this._items = (IList<CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph>) new List<CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph>();
  }

  public CRCacheIndependentPrimaryGraphListAttribute(System.Type[] graphTypes, System.Type[] conditions)
    : this()
  {
    if (graphTypes == null)
      throw new ArgumentNullException(nameof (graphTypes));
    if (conditions == null)
      throw new ArgumentNullException(nameof (conditions));
    if (graphTypes.Length != conditions.Length)
      throw new ArgumentException("The length of 'graphTypes' must be equal to the length of 'conditions'");
    for (int index = 0; index < conditions.Length; ++index)
      this.Add(graphTypes[index], conditions[index]);
  }

  public virtual void Add(System.Type graphType, System.Type condition)
  {
    this._items.Add(new CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph(graphType, condition));
  }

  private Dictionary<System.Type, HashSet<System.Type>> getPossibleDACs(System.Type itemType)
  {
    Dictionary<System.Type, HashSet<System.Type>> possibleDaCs = new Dictionary<System.Type, HashSet<System.Type>>();
    foreach (CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph primaryGraph in (IEnumerable<CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph>) this._items)
    {
      System.Type type;
      if (typeof (IBqlWhere).IsAssignableFrom(primaryGraph.Condition))
        type = itemType;
      else
        type = BqlCommand.CreateInstance(new System.Type[1]
        {
          primaryGraph.Condition
        }).GetTables()[0];
      if (type != (System.Type) null)
      {
        if (!possibleDaCs.ContainsKey(primaryGraph.GraphType))
          possibleDaCs[primaryGraph.GraphType] = new HashSet<System.Type>();
        possibleDaCs[primaryGraph.GraphType].Add(type);
      }
    }
    return possibleDaCs;
  }

  private bool typeIsPossible(System.Type testType, HashSet<System.Type> possibleTypes)
  {
    foreach (System.Type possibleType in possibleTypes)
    {
      if (possibleType.IsAssignableFrom(testType))
        return true;
    }
    return false;
  }

  public virtual System.Type GetGraphType(
    PXCache cache,
    ref object row,
    bool checkRights,
    System.Type preferedType)
  {
    PXGraph pxGraph = (PXGraph) null;
    Dictionary<System.Type, HashSet<System.Type>> dictionary = (Dictionary<System.Type, HashSet<System.Type>>) null;
    if (preferedType != (System.Type) null)
      dictionary = this.getPossibleDACs(cache.GetItemType());
    System.Type graphType = (System.Type) null;
    bool flag = false;
    foreach (CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph primaryGraph in (IEnumerable<CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph>) this._items)
    {
      System.Type itemType = cache.GetItemType();
      if (pxGraph == null)
      {
        pxGraph = PXGraph.CreateInstance<PXGraph>();
        pxGraph.Caches[typeof (AccessInfo)].Current = (object) pxGraph.Accessinfo;
        if (row != null)
        {
          PXEntryStatus status = cache.GetStatus(row);
          pxGraph.Caches[itemType].SetStatus(row, status);
        }
      }
      if (typeof (IBqlWhere).IsAssignableFrom(primaryGraph.Condition))
      {
        IBqlWhere instance = (IBqlWhere) Activator.CreateInstance(primaryGraph.Condition);
        bool? nullable = new bool?();
        object obj = (object) null;
        BqlFormula.Verify(cache, row, (IBqlCreator) instance, ref nullable, ref obj);
        flag = flag || nullable.GetValueOrDefault();
        if (nullable.GetValueOrDefault() && (preferedType == (System.Type) null || dictionary.ContainsKey(preferedType) && this.typeIsPossible(itemType, dictionary[preferedType])))
        {
          graphType = primaryGraph.GraphType;
          if (!checkRights || PXAccess.VerifyRights(primaryGraph.GraphType))
            return primaryGraph.GraphType;
        }
      }
      else if (row != null)
      {
        BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
        {
          primaryGraph.Condition
        });
        PXView pxView = new PXView(pxGraph, false, instance);
        object obj = pxView.SelectSingleBound(new object[1]
        {
          row
        }, Array.Empty<object>());
        flag = flag || obj != null;
        if (obj != null && (preferedType == (System.Type) null || dictionary.ContainsKey(preferedType) && this.typeIsPossible(pxView.GetItemType(), dictionary[preferedType])))
        {
          graphType = primaryGraph.GraphType;
          if (!checkRights || PXAccess.VerifyRights(primaryGraph.GraphType))
          {
            row = obj;
            if (row is PXResult)
              row = ((PXResult) row)[0];
            return primaryGraph.GraphType;
          }
        }
      }
    }
    if (!flag && row != null)
      this.OnNoItemFound(graphType);
    if (graphType != (System.Type) null)
      this.OnAccessDenied(graphType);
    return (System.Type) null;
  }

  protected virtual void OnNoItemFound(System.Type graphType)
  {
  }

  protected virtual void OnAccessDenied(System.Type graphType)
  {
  }

  public virtual IEnumerator GetEnumerator() => (IEnumerator) this._items.GetEnumerator();

  public virtual IEnumerable<System.Type> GetAllGraphTypes()
  {
    return this._items.Select<CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph, System.Type>((Func<CRCacheIndependentPrimaryGraphListAttribute.PrimaryGraph, System.Type>) (i => i.GraphType));
  }

  private sealed class PrimaryGraph
  {
    private readonly System.Type _graphType;
    private readonly System.Type _condition;

    public PrimaryGraph(System.Type graphType, System.Type condition)
    {
      if (graphType == (System.Type) null)
        throw new ArgumentNullException(nameof (graphType));
      if (!typeof (PXGraph).IsAssignableFrom(graphType))
        throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("Type '{0}' must inherit 'PX.Data.PXGraph' type", new object[1]
        {
          (object) MainTools.GetLongName(graphType)
        }));
      if (condition == (System.Type) null)
        throw new ArgumentNullException(nameof (condition));
      if (!typeof (BqlCommand).IsAssignableFrom(condition) && !typeof (IBqlWhere).IsAssignableFrom(condition))
        throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("Type '{0}' must inherit 'PX.Data.BqlCommand' type", new object[1]
        {
          (object) MainTools.GetLongName(condition)
        }));
      this._graphType = graphType;
      this._condition = condition;
    }

    public System.Type GraphType => this._graphType;

    public System.Type Condition => this._condition;
  }
}
