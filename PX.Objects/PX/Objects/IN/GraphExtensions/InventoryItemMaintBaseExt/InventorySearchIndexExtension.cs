// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt.InventorySearchIndexExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt;

public class InventorySearchIndexExtension : PXGraphExtension<
#nullable disable
InventoryItemMaintBase>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>();

  [PXOverride]
  public void PerformPersist(
    PXGraph.IPersistPerformer persister,
    Action<PXGraph.IPersistPerformer> base_PerformPersist)
  {
    base_PerformPersist(persister);
    PXCache<PX.Objects.IN.InventoryItem> inventoryCache = GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base);
    GraphHelper.Caches<InventorySearchIndex>((PXGraph) this.Base);
    string[] inactiveStatuses = new string[2]{ "IN", "DE" };
    foreach (PX.Objects.IN.InventoryItem row in GraphHelper.RowCast<PX.Objects.IN.InventoryItem>(((PXCache) inventoryCache).Cached).Where<PX.Objects.IN.InventoryItem>((Func<PX.Objects.IN.InventoryItem, bool>) (item =>
    {
      PXEntryStatus status = inventoryCache.GetStatus(item);
      bool? isTemplate = item.IsTemplate;
      bool flag = false;
      return isTemplate.GetValueOrDefault() == flag & isTemplate.HasValue && (status == 2 || status == 1) && EnumerableExtensions.IsNotIn<string>(item.ItemStatus, (IEnumerable<string>) inactiveStatuses);
    })).ToList<PX.Objects.IN.InventoryItem>())
    {
      InventorySearchIndex record = this.BuildSearchIndex((PXCache) inventoryCache, row);
      if (PXResultset<InventorySearchIndex>.op_Implicit(PXSelectBase<InventorySearchIndex, PXViewOf<InventorySearchIndex>.BasedOn<SelectFromBase<InventorySearchIndex, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventorySearchIndex.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<InventorySearchIndex.contentIDDesc, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<InventorySearchIndex.contentAlternateID, IBqlString>.IsEqual<P.AsString>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[3]
      {
        (object) record.InventoryID,
        (object) record.ContentIDDesc,
        (object) record.ContentAlternateID
      })) == null && !this.Update(record))
        this.Insert(record);
    }
    foreach (PX.Objects.IN.InventoryItem inventoryItem in GraphHelper.RowCast<PX.Objects.IN.InventoryItem>(((PXCache) inventoryCache).Cached).Where<PX.Objects.IN.InventoryItem>((Func<PX.Objects.IN.InventoryItem, bool>) (item =>
    {
      PXEntryStatus status = inventoryCache.GetStatus(item);
      bool? isTemplate = item.IsTemplate;
      bool flag = false;
      if (!(isTemplate.GetValueOrDefault() == flag & isTemplate.HasValue))
        return false;
      return status == 3 || EnumerableExtensions.IsIn<string>(item.ItemStatus, (IEnumerable<string>) inactiveStatuses);
    })).ToList<PX.Objects.IN.InventoryItem>())
    {
      InventorySearchIndex record = PXResultset<InventorySearchIndex>.op_Implicit(PXSelectBase<InventorySearchIndex, PXViewOf<InventorySearchIndex>.BasedOn<SelectFromBase<InventorySearchIndex, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventorySearchIndex.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) inventoryItem.InventoryID
      }));
      if (record != null)
        this.Delete(record);
    }
  }

  protected virtual InventorySearchIndex BuildSearchIndex(PXCache sender, PX.Objects.IN.InventoryItem row)
  {
    string contentIDDesc = this.BuildContentIDDescr(row);
    return new InventorySearchIndex()
    {
      IndexID = new Guid?(Guid.NewGuid()),
      InventoryID = row.InventoryID,
      ContentIDDesc = contentIDDesc,
      ContentAlternateID = this.BuildContentAlternateID(sender, row, contentIDDesc)
    };
  }

  [PXInternalUseOnly]
  public virtual string BuildContentIDDescr(PX.Objects.IN.InventoryItem row)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string a = row.InventoryCD.Trim();
    string b1 = ((PXSelectBase) this.Base.Item).Cache.GetFormatedMaskField<PX.Objects.IN.InventoryItem.inventoryCD>((IBqlTable) row).Trim();
    string b2 = string.Empty;
    if (((PXSelectBase) this.Base.Item).Cache.GetStateExt<PX.Objects.IN.InventoryItem.inventoryCD>((object) row) is PXStringState stateExt)
      b2 = string.Join(" ", Mask.GetSegments(stateExt?.InputMask ?? string.Empty, row.InventoryCD, ' '));
    stringBuilder.Append(a);
    if (!string.Equals(a, b1))
    {
      stringBuilder.Append(" ");
      stringBuilder.Append(b1);
    }
    if (!string.Equals(a, b2))
    {
      stringBuilder.Append(" ");
      stringBuilder.Append(b2);
    }
    stringBuilder.Append(" ");
    stringBuilder.Append(!Str.IsNullOrWhiteSpace(row.Descr) ? row.Descr?.Trim() : "");
    return stringBuilder.ToString().Trim();
  }

  protected virtual string BuildContentAlternateID(
    PXCache sender,
    PX.Objects.IN.InventoryItem row,
    string contentIDDesc)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(contentIDDesc);
    IEnumerable<string> strings = GraphHelper.RowCast<INItemXRef>((IEnumerable) PXSelectBase<INItemXRef, PXViewOf<INItemXRef>.BasedOn<SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemXRef.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<PX.Objects.IN.InventoryItem, INItemXRef>, PX.Objects.IN.InventoryItem, INItemXRef>.SameAsCurrent>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) row
    })).Select<INItemXRef, string>((Func<INItemXRef, string>) (x => x.AlternateID));
    if (strings != null && strings.Any<string>())
    {
      stringBuilder.Append(" ");
      stringBuilder.Append(string.Join(" ", strings));
    }
    return stringBuilder.ToString().Trim();
  }

  [PXInternalUseOnly]
  protected virtual bool Insert(InventorySearchIndex record)
  {
    return PXDatabase.Insert(typeof (InventorySearchIndex), new PXDataFieldAssign[4]
    {
      new PXDataFieldAssign(typeof (InventorySearchIndex.inventoryID).Name, (PXDbType) 8, (object) record.InventoryID),
      new PXDataFieldAssign(typeof (InventorySearchIndex.indexID).Name, (PXDbType) 14, (object) record.IndexID),
      new PXDataFieldAssign(typeof (InventorySearchIndex.contentIDDesc).Name, (PXDbType) 11, (object) record.ContentIDDesc),
      new PXDataFieldAssign(typeof (InventorySearchIndex.contentAlternateID).Name, (PXDbType) 11, (object) record.ContentAlternateID)
    });
  }

  [PXInternalUseOnly]
  public virtual bool Update(InventorySearchIndex record)
  {
    return PXDatabase.Update(typeof (InventorySearchIndex), new PXDataFieldParam[3]
    {
      (PXDataFieldParam) new PXDataFieldRestrict(typeof (InventorySearchIndex.inventoryID).Name, (PXDbType) 8, (object) record.InventoryID),
      (PXDataFieldParam) new PXDataFieldAssign(typeof (InventorySearchIndex.contentIDDesc).Name, (PXDbType) 11, (object) record.ContentIDDesc),
      (PXDataFieldParam) new PXDataFieldAssign(typeof (InventorySearchIndex.contentAlternateID).Name, (PXDbType) 11, (object) record.ContentAlternateID)
    });
  }

  [PXInternalUseOnly]
  protected virtual bool Delete(InventorySearchIndex record)
  {
    return PXDatabase.Delete(typeof (InventorySearchIndex), new PXDataFieldRestrict[1]
    {
      new PXDataFieldRestrict(typeof (InventorySearchIndex.inventoryID).Name, (PXDbType) 8, (object) record.InventoryID)
    });
  }
}
