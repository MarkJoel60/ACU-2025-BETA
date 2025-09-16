// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.FullTextIndexRebuildExt.InventorySearchIndexExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt;
using PX.Objects.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.FullTextIndexRebuildExt;

public class InventorySearchIndexExt : PXGraphExtension<
#nullable disable
FullTextIndexRebuildProc>
{
  private const string Separator = " ";

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>();

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SM.FullTextIndexRebuildProc.BuildIndex(PX.Objects.SM.FullTextIndexRebuildProc.RecordType)" />
  /// </summary>
  [PXOverride]
  public virtual void BuildIndex(
    FullTextIndexRebuildProc.RecordType item,
    Action<FullTextIndexRebuildProc.RecordType> baseMethod)
  {
    if (!string.Equals(item.Entity, typeof (PX.Objects.IN.InventoryItem).FullName, StringComparison.OrdinalIgnoreCase))
    {
      baseMethod(item);
    }
    else
    {
      this.RebuildInventoryItemFullTextSearchIndex(item);
      baseMethod(item);
    }
  }

  protected virtual void RebuildInventoryItemFullTextSearchIndex(
    FullTextIndexRebuildProc.RecordType item)
  {
    Stopwatch stopwatch = new Stopwatch();
    ((Dictionary<Type, PXCache>) ((PXGraph) this.Base).Caches).Clear();
    ((PXGraph) this.Base).Clear((PXClearOption) 3);
    PXProcessing<FullTextIndexRebuildProc.RecordType>.SetCurrentItem((object) item);
    PXView view = ((PXSelectBase) new PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<InventorySearchIndex>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<InventorySearchIndex.inventoryID>>>, FbqlJoins.Left<INItemXRef>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<INItemXRef.inventoryID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion>>>.Aggregate<To<GroupBy<PX.Objects.IN.InventoryItem.inventoryID>, GroupBy<InventorySearchIndex.contentIDDesc>, GroupBy<InventorySearchIndex.contentAlternateID>, AggConcat<INItemXRef.alternateID, InventorySearchIndexExt.separator>>>.Order<By<BqlField<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.Asc>>>.ReadOnly((PXGraph) this.Base)).View;
    List<Type> typeList = new List<Type>()
    {
      typeof (PX.Objects.IN.InventoryItem.inventoryID),
      typeof (PX.Objects.IN.InventoryItem.inventoryCD),
      typeof (PX.Objects.IN.InventoryItem.descr),
      typeof (PX.Objects.IN.InventoryItem.noteID),
      typeof (InventorySearchIndex.inventoryID),
      typeof (InventorySearchIndex.contentIDDesc),
      typeof (InventorySearchIndex.contentAlternateID),
      typeof (INItemXRef.inventoryID),
      typeof (INItemXRef.alternateID)
    };
    stopwatch.Start();
    int num1 = 0;
    InventorySearchIndexExtension extension = ((PXGraph) PXGraph.CreateInstance<InventoryItemMaint>()).GetExtension<InventorySearchIndexExtension>();
    List<object> objectList;
    do
    {
      using (new PXFieldScope(view, (IEnumerable<Type>) typeList, true))
        objectList = view.SelectWindowed((object[]) null, (object[]) null, (string[]) null, (bool[]) null, num1, 50000);
      stopwatch.Stop();
      stopwatch.Reset();
      stopwatch.Start();
      num1 += 50000;
      int count = objectList.Count;
      int num2 = 0;
      try
      {
        Dictionary<Guid, InventorySearchIndex> dictionary = new Dictionary<Guid, InventorySearchIndex>(objectList.Count);
        foreach (PXResult pxResult in objectList)
        {
          ++num2;
          PX.Objects.IN.InventoryItem row = (PX.Objects.IN.InventoryItem) pxResult[typeof (PX.Objects.IN.InventoryItem)];
          InventorySearchIndex inventorySearchIndex = (InventorySearchIndex) pxResult[typeof (InventorySearchIndex)];
          INItemXRef inItemXref = (INItemXRef) pxResult[typeof (INItemXRef)];
          string str = extension.BuildContentIDDescr(row);
          InventorySearchIndex record = new InventorySearchIndex()
          {
            IndexID = new Guid?(Guid.NewGuid()),
            InventoryID = row.InventoryID,
            ContentIDDesc = str,
            ContentAlternateID = string.IsNullOrWhiteSpace(inItemXref.AlternateID) ? str : $"{str} {inItemXref.AlternateID}".Trim()
          };
          if (record != null && !string.IsNullOrWhiteSpace(record.ContentIDDesc))
          {
            if (!inventorySearchIndex.InventoryID.HasValue)
            {
              if (!dictionary.ContainsKey(row.NoteID.Value))
                dictionary.Add(row.NoteID.Value, record);
            }
            else if (record.ContentIDDesc != inventorySearchIndex.ContentIDDesc || record.ContentAlternateID != inventorySearchIndex.ContentAlternateID)
              extension.Update(record);
          }
        }
        stopwatch.Stop();
        stopwatch.Reset();
        stopwatch.Start();
        PXBulkInsert<InventorySearchIndex>.Insert((PXGraph) this.Base, (IEnumerable) dictionary.Values);
        stopwatch.Stop();
      }
      catch (Exception ex)
      {
        throw new Exception($"The system processed {num2} out of {count}. {num2} are searchable. Error: {ex.Message}.", ex);
      }
    }
    while (objectList.Count > 0);
  }

  private class separator : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventorySearchIndexExt.separator>
  {
    public separator()
      : base(" ")
    {
    }
  }
}
