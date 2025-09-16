// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.LotSerialAttributeConfigurationBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IN.DAC.Projections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class LotSerialAttributeConfigurationBase<TGraph> : LotSerialGraphExtBase<TGraph> where TGraph : PXGraph
{
  /// <summary>
  /// Overrides <see cref="M:PX.Data.PXGraph.PostPersist" />
  /// </summary>
  [PXOverride]
  public void PostPersist()
  {
    PXSelectorAttribute.ClearGlobalCache<InventoryItemLotSerialAttributes>();
  }

  [PXOverride]
  public void PerformPersist(
    PXGraph.IPersistPerformer persister,
    Action<PXGraph.IPersistPerformer> base_PerformPersist)
  {
    int num = this.IsAttributeCacheDirty() ? 1 : 0;
    this.ExecutePerformPersist(persister, base_PerformPersist);
    if (num == 0)
      return;
    this.UpdateScreenAttributes();
  }

  protected virtual void ExecutePerformPersist(
    PXGraph.IPersistPerformer persister,
    Action<PXGraph.IPersistPerformer> base_PerformPersist)
  {
    base_PerformPersist(persister);
  }

  protected abstract bool IsAttributeCacheDirty();

  private void UpdateScreenAttributes()
  {
    HashSet<string> attributeIDs = GraphHelper.RowCast<INItemLotSerialAttribute>((IEnumerable) PXSelectBase<INItemLotSerialAttribute, PXViewOf<INItemLotSerialAttribute>.BasedOn<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Aggregate<To<GroupBy<INItemLotSerialAttribute.attributeID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Select<INItemLotSerialAttribute, string>((Func<INItemLotSerialAttribute, string>) (s => s.AttributeID)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    HashSet<string> screenAttributeIDs = ((IEnumerable<Tuple<PXFieldState, short, short, string>>) KeyValueHelper.GetAttributeFields("IN209600")).Select<Tuple<PXFieldState, short, short, string>, string>((Func<Tuple<PXFieldState, short, short, string>, string>) (s => this.ConvertFieldNameToAttributeID(s.Item1.Name))).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    Lazy<CSAttributeMaint2> lazy = new Lazy<CSAttributeMaint2>((Func<CSAttributeMaint2>) (() => PXGraph.CreateInstance<CSAttributeMaint2>()));
    foreach (string attributeID in screenAttributeIDs.Where<string>((Func<string, bool>) (id => !attributeIDs.Contains(id))))
      this.DeleteScreenAttribute(lazy.Value, attributeID);
    foreach (string attributeID in attributeIDs.Where<string>((Func<string, bool>) (id => !screenAttributeIDs.Contains(id))))
      this.AddScreenAttribute(lazy.Value, attributeID);
    if (lazy.IsValueCreated)
      ((PXAction) lazy.Value.Save).Press();
    this._attributes = (Dictionary<string, KeyValueHelper.Attribute>) null;
  }

  private void DeleteScreenAttribute(CSAttributeMaint2 graph, string attributeID)
  {
    ((PXSelectBase<AttribParams>) graph.ScreenSettings).Current.ScreenID = "IN209600";
    CSAttributeMaint2 csAttributeMaint2 = graph;
    BqlCommand bqlSelect = ((PXSelectBase) graph.ScreenSettings).View.BqlSelect;
    graph.del(new PXAdapter((PXView) new PXView.Dummy((PXGraph) csAttributeMaint2, bqlSelect, new List<object>()
    {
      (object) ((PXSelectBase<AttribParams>) graph.ScreenSettings).Current
    }))
    {
      CommandArguments = this.ConvertAttributeIDToFieldName(attributeID)
    });
  }

  private void AddScreenAttribute(CSAttributeMaint2 graph, string attributeID)
  {
    ((PXSelectBase<AttribParams>) graph.ScreenSettings).Current.ScreenID = "IN209600";
    ((PXSelectBase) graph.NewAttrib).View.Answer = (WebDialogResult) 1;
    ((PXSelectBase<CSNewAttribute>) graph.NewAttrib).Current.Column = new int?(1);
    ((PXSelectBase<CSNewAttribute>) graph.NewAttrib).Current.Row = new int?(this.GetScreenAttributeMaxRow() + 1);
    ((PXSelectBase<CSNewAttribute>) graph.NewAttrib).Current.AttributeID = attributeID;
    ((PXAction) graph.AddAttrib).Press();
  }

  private int GetScreenAttributeMaxRow()
  {
    Tuple<PXFieldState, short, short, string>[] attributeFields = KeyValueHelper.GetAttributeFields("IN209600");
    int screenAttributeMaxRow = 0;
    foreach (Tuple<PXFieldState, short, short, string> tuple in attributeFields)
    {
      if (screenAttributeMaxRow < (int) tuple.Item3)
        screenAttributeMaxRow = (int) tuple.Item3;
    }
    return screenAttributeMaxRow;
  }
}
