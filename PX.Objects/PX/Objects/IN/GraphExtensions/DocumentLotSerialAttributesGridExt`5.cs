// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.DocumentLotSerialAttributesGridExt`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions;

public abstract class DocumentLotSerialAttributesGridExt<TGraph, TDocument, TLine, TSplit, TAttributesHeader> : 
  LotSerialAttributesGridExt<
  #nullable disable
  TGraph, TAttributesHeader>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, ILotSerialTrackableDocument, new()
  where TLine : class, IBqlTable, ILotSerialTrackableLine, new()
  where TSplit : class, IBqlTable, ILotSerialTrackableLineSplit, new()
  where TAttributesHeader : class, ILotSerialAttributesHeader, IBqlTable, new()
{
  public PXAction<TDocument> useAttributesFromFirstLine;

  public IEnumerable LotSerialAttributes()
  {
    PXView internalView = new PXView((PXGraph) this.Base, true, PXView.View.BqlSelect);
    int startRow = PXView.StartRow;
    int maximumRows = PXView.MaximumRows;
    int num = 0;
    foreach (INItemLotSerialAttribute lotSerialAttribute in internalView.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, maximumRows, ref num))
    {
      if (!string.IsNullOrEmpty(lotSerialAttribute.LotSerialNbr))
      {
        internalView.Cache.SetStatus((object) lotSerialAttribute, (PXEntryStatus) 5);
        yield return (object) lotSerialAttribute;
      }
    }
    PXView.StartRow = 0;
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField]
  protected virtual IEnumerable UseAttributesFromFirstLine(PXAdapter adapter)
  {
    IEnumerable<TSplit> lineOrderedSplits = this.GetCurrentDocumentLineOrderedSplits();
    TSplit split1 = default (TSplit);
    TAttributesHeader attributesHeader1 = default (TAttributesHeader);
    foreach (TSplit split2 in lineOrderedSplits)
    {
      if ((object) split1 == null)
      {
        split1 = split2;
        attributesHeader1 = this.GetAttributesHeader(split2.InventoryID, split2.LotSerialNbr, false);
      }
      else
      {
        TAttributesHeader attributesHeader2 = this.GetAttributesHeader(split2.InventoryID, split2.LotSerialNbr, true);
        foreach (string attributeIdentifier in this.GetActiveAttributeIdentifiers(split2.InventoryID))
        {
          object obj = this.GetValue<TAttributesHeader>(attributesHeader1, attributeIdentifier);
          attributesHeader2 = this.SetValue<TAttributesHeader>(attributesHeader2, attributeIdentifier, obj);
        }
      }
    }
    return adapter.Get();
  }

  public override void Initialize()
  {
    base.Initialize();
    ((FieldSelectingEvents) this.Base.FieldSelecting).AddAbstractHandler<TSplit>("MfgLotSerialNbr", (Action<AbstractEvents.IFieldSelecting<TSplit, IBqlField>>) (e => this.MfgLotSerialNbrFieldSelecting("MfgLotSerialNbr", e)));
    ((FieldUpdatingEvents) this.Base.FieldUpdating).AddAbstractHandler<TSplit>("MfgLotSerialNbr", (Action<AbstractEvents.IFieldUpdating<TSplit, IBqlField>>) (e => this.MfgLotSerialNbrFieldUpdating("MfgLotSerialNbr", e)));
  }

  protected virtual void MfgLotSerialNbrFieldSelecting(
    string fieldName,
    AbstractEvents.IFieldSelecting<TSplit, IBqlField> e)
  {
    TSplit row = e.Row;
    if (!(((PXSelectBase) this.GetAttributesHeaderView()).Cache.GetStateExt((object) this.GetAttributesHeader((int?) row?.InventoryID, row?.LotSerialNbr, false), fieldName) is PXFieldState stateExt))
      return;
    if (!PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>())
    {
      stateExt.Visibility = (PXUIVisibility) 1;
      stateExt.Visible = false;
    }
    stateExt.Enabled = this.LotSerialGenerated(row) && !this.IsAttributeReadonlyForDocument(GraphHelper.Caches<TDocument>((PXGraph) this.Base).Rows.Current);
    stateExt.SetFieldName(fieldName);
    e.ReturnState = (object) stateExt;
    e.ReturnValue = stateExt.Value;
  }

  protected virtual void MfgLotSerialNbrFieldUpdating(
    string fieldName,
    AbstractEvents.IFieldUpdating<TSplit, IBqlField> e)
  {
    TSplit row = e.Row;
    // ISSUE: variable of a boxed type
    __Boxed<TSplit> local = (object) row;
    if ((local != null ? (!local.InventoryID.HasValue ? 1 : 0) : 1) != 0 || row.LotSerialNbr == null)
      return;
    TAttributesHeader attributesHeader = this.GetAttributesHeader(row.InventoryID, row.LotSerialNbr, true);
    PXCache cache = ((PXSelectBase) this.GetAttributesHeaderView()).Cache;
    cache.SetValueExt((object) attributesHeader, fieldName, e.NewValue);
    cache.Update((object) attributesHeader);
  }

  protected virtual void _(Events.RowSelected<TDocument> e)
  {
    if ((object) e.Row == null)
      return;
    ((PXAction) this.useAttributesFromFirstLine).SetEnabled(!this.IsAttributeReadonlyForDocument(e.Row));
    ((PXCache) GraphHelper.Caches<INItemLotSerialAttribute>((PXGraph) this.Base)).AllowUpdate = !this.IsAttributeReadonlyForDocument(e.Row);
  }

  protected virtual void _(Events.RowSelected<TSplit> e)
  {
    TLine current = GraphHelper.Caches<TLine>((PXGraph) this.Base).Rows.Current;
    IEnumerable<string> attributeIdentifiers = this.GetActiveAttributeIdentifiers((int?) ((int?) e.Row?.InventoryID ?? current?.InventoryID));
    bool flag = attributeIdentifiers != null && attributeIdentifiers.Any<string>();
    ((PXAction) this.useAttributesFromFirstLine).SetVisible(flag);
    ((PXCache) GraphHelper.Caches<INItemLotSerialAttribute>((PXGraph) this.Base)).AllowSelect = flag;
  }

  protected virtual void _(Events.RowUpdated<TSplit> e)
  {
    if (!(e.OldRow.LotSerialNbr != e.Row.LotSerialNbr) || !this.LotSerialGenerated(e.Row))
      return;
    this.CopyLotSerialAttributeValues(e.Row);
  }

  protected virtual void _(Events.RowInserted<TSplit> e)
  {
    if (!this.LotSerialGenerated(e.Row))
      return;
    this.CopyLotSerialAttributeValues(e.Row);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Data.PXGraph.PrePersist" />
  /// </summary>
  [PXOverride]
  public bool PrePersist(Func<bool> base_PrePersist)
  {
    PXCache<TDocument> pxCache = GraphHelper.Caches<TDocument>((PXGraph) this.Base);
    TDocument current = pxCache.Rows.Current;
    if ((object) current != null && EnumerableExtensions.IsIn<PXEntryStatus>(pxCache.GetStatus(current), (PXEntryStatus) 2, (PXEntryStatus) 1))
    {
      if (!((PXCache) GraphHelper.Caches<TSplit>((PXGraph) this.Base)).IsDirty && !((PXCache) GraphHelper.Caches<TAttributesHeader>((PXGraph) this.Base)).IsDirty)
      {
        bool? hold = current.Hold;
        if (!hold.GetValueOrDefault())
        {
          // ISSUE: variable of a boxed type
          __Boxed<TDocument> original = (object) pxCache.GetOriginal(current);
          int num;
          if (original == null)
          {
            num = 0;
          }
          else
          {
            hold = original.Hold;
            num = hold.GetValueOrDefault() ? 1 : 0;
          }
          if (num == 0)
            goto label_15;
        }
        else
          goto label_15;
      }
      foreach (KeyValuePair<(int? inventoryID, string lotSerialNbr), TAttributesHeader> lotSerialAttribute in this.DeleteOrphanedLotSerialAttributes())
      {
        if (!string.IsNullOrEmpty(lotSerialAttribute.Key.lotSerialNbr))
        {
          TAttributesHeader header = lotSerialAttribute.Value;
          if ((object) header == null)
          {
            TAttributesHeader attributesHeader = new TAttributesHeader();
            attributesHeader.InventoryID = lotSerialAttribute.Key.inventoryID;
            attributesHeader.LotSerialNbr = lotSerialAttribute.Key.lotSerialNbr;
            header = attributesHeader;
          }
          this.VerifyRequiredAttributes(header);
        }
      }
    }
label_15:
    return base_PrePersist();
  }

  protected override bool AllowEmptyRequiredAttribute()
  {
    // ISSUE: variable of a boxed type
    __Boxed<TDocument> current = (object) GraphHelper.Caches<TDocument>((PXGraph) this.Base).Rows.Current;
    return current != null && current.Hold.GetValueOrDefault();
  }

  protected override bool ShowErrorOnAttributeField() => false;

  protected override bool GetAttributeEnabled(INItemLotSerialAttribute attribute)
  {
    return this.LotSerialGenerated(GraphHelper.Caches<TSplit>((PXGraph) this.Base).Rows.Current);
  }

  protected abstract bool IsAttributeReadonlyForDocument(TDocument document);

  protected abstract IEnumerable<TSplit> GetAllCurrentDocumentSplits();

  protected abstract IEnumerable<TAttributesHeader> GetAllCurrentDocumentLotSerialAttributesHeaders();

  protected abstract IEnumerable<TSplit> GetCurrentDocumentLineOrderedSplits();

  protected virtual TAttributesHeader FillAttributesHeaderKeys(TSplit split)
  {
    TAttributesHeader attributesHeader = new TAttributesHeader();
    attributesHeader.InventoryID = split.InventoryID;
    attributesHeader.LotSerialNbr = split.LotSerialNbr;
    return attributesHeader;
  }

  protected virtual bool LotSerialGenerated(TSplit split)
  {
    if (string.IsNullOrEmpty(split?.LotSerialNbr))
      return false;
    return string.IsNullOrEmpty(split.AssignedNbr) || !INLotSerialNbrAttribute.StringsEqual(split.AssignedNbr, split.LotSerialNbr);
  }

  protected virtual IEnumerable<string> GetActiveAttributeIdentifiers(int? inventoryID)
  {
    return this.GetAttributes(inventoryID).Where<LotSerialGraphExtBase<TGraph>.AttributeInformation>((Func<LotSerialGraphExtBase<TGraph>.AttributeInformation, bool>) (a => a.IsActive)).Select<LotSerialGraphExtBase<TGraph>.AttributeInformation, string>((Func<LotSerialGraphExtBase<TGraph>.AttributeInformation, string>) (a => a.AttributeID));
  }

  private Dictionary<(int? inventoryID, string lotSerialNbr), TAttributesHeader> DeleteOrphanedLotSerialAttributes()
  {
    Dictionary<(int?, string), TAttributesHeader> dictionary = this.GetAllCurrentDocumentSplits().GroupBy<TSplit, (int?, string)>((Func<TSplit, (int?, string)>) (s => (s.InventoryID, s.LotSerialNbr.ToUpper()))).ToDictionary<IGrouping<(int?, string), TSplit>, (int?, string), TAttributesHeader>((Func<IGrouping<(int?, string), TSplit>, (int?, string)>) (s => s.Key), (Func<IGrouping<(int?, string), TSplit>, TAttributesHeader>) (s => default (TAttributesHeader)));
    foreach (TAttributesHeader attributesHeader in this.GetAllCurrentDocumentLotSerialAttributesHeaders())
    {
      (int?, string) key = (attributesHeader.InventoryID, attributesHeader.LotSerialNbr?.ToUpper());
      if (!dictionary.ContainsKey(key))
        this.GetAttributesHeaderView().Delete(attributesHeader);
      else
        dictionary[key] = attributesHeader;
    }
    return dictionary;
  }

  protected virtual TAttributesHeader CopyLotSerialAttributeValues(TSplit split)
  {
    TAttributesHeader attributesHeader1 = this.GetAttributesHeader(split.InventoryID, split.LotSerialNbr, false);
    if ((object) attributesHeader1 != null)
      return attributesHeader1;
    INItemLotSerialAttributesHeader attributesHeader2 = INItemLotSerialAttributesHeader.PK.Find((PXGraph) this.Base, split.InventoryID, split.LotSerialNbr);
    if (attributesHeader2 == null)
      return default (TAttributesHeader);
    PXSelectBase<TAttributesHeader> attributesHeaderView = this.GetAttributesHeaderView();
    TAttributesHeader attributesHeader3 = attributesHeaderView.Insert(this.FillAttributesHeaderKeys(split));
    attributesHeader3.MfgLotSerialNbr = attributesHeader2.MfgLotSerialNbr;
    TAttributesHeader attributesHeader4 = attributesHeaderView.Update(attributesHeader3);
    foreach (string attributeIdentifier in this.GetActiveAttributeIdentifiers(split.InventoryID))
    {
      object obj = this.GetValue<INItemLotSerialAttributesHeader>(attributesHeader2, attributeIdentifier);
      if (obj != null)
        this.SetValue<TAttributesHeader>(attributesHeader4, attributeIdentifier, obj);
    }
    return attributesHeader4;
  }
}
