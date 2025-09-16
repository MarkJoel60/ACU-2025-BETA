// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SegmentMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

#nullable disable
namespace PX.Objects.CS;

public class SegmentMaint : PXGraph<SegmentMaint>
{
  public PXSelect<PX.Objects.CS.Segment, Where<PX.Objects.CS.Segment.dimensionID, InFieldClassActivated>> Segment;
  [PXImport(typeof (PX.Objects.CS.Segment))]
  public PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<Optional<PX.Objects.CS.Segment.dimensionID>>, And<SegmentValue.segmentID, Equal<Optional<PX.Objects.CS.Segment.segmentID>>>>> Values;
  private SegmentMaint.GroupColumnsCache _groupColumnsCache;
  public PXSave<PX.Objects.CS.Segment> Save;
  public PXCancel<PX.Objects.CS.Segment> Cancel;
  public PXFirst<PX.Objects.CS.Segment> First;
  public PXPrevious<PX.Objects.CS.Segment> Previous;
  public PXNext<PX.Objects.CS.Segment> Next;
  public PXLast<PX.Objects.CS.Segment> Last;

  public SegmentMaint()
  {
    this._groupColumnsCache = new SegmentMaint.GroupColumnsCache();
    ((PXSelectBase) this.Segment).Cache.AllowInsert = false;
    ((PXSelectBase) this.Segment).Cache.AllowDelete = false;
  }

  public virtual void Persist()
  {
    try
    {
      PXDimensionAttribute.Clear();
      ((PXGraph) this).Persist();
      PXDimensionAttribute.Clear();
    }
    catch (PXDatabaseException ex)
    {
      if (ex.ErrorCode == null)
        throw new PXException("Segment '{0}' cannot be deleted as it has one or more segment values defined.", new object[1]
        {
          ex.Keys[1]
        });
      throw;
    }
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (Search5<Dimension.dimensionID, InnerJoin<PX.Objects.CS.Segment, On<PX.Objects.CS.Segment.dimensionID, Equal<Dimension.dimensionID>>>, Where<Dimension.dimensionID, InFieldClassActivated>, Aggregate<GroupBy<Dimension.dimensionID>>>))]
  protected virtual void Segment_DimensionID_CacheAttached(PXCache sender)
  {
  }

  [PXDBShort(IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Segment.segmentID, Where<PX.Objects.CS.Segment.dimensionID, Equal<Current<PX.Objects.CS.Segment.dimensionID>>>>))]
  [PXParent(typeof (Select<Dimension, Where<Dimension.dimensionID, Equal<Current<PX.Objects.CS.Segment.dimensionID>>>>))]
  protected virtual void Segment_SegmentID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Segment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row is PX.Objects.CS.Segment row)
    {
      PXCache cache1 = ((PXSelectBase) this.Values).Cache;
      bool? autoNumber = row.AutoNumber;
      int num1 = !autoNumber.GetValueOrDefault() ? 1 : 0;
      cache1.AllowInsert = num1 != 0;
      PXCache cache2 = ((PXSelectBase) this.Values).Cache;
      autoNumber = row.AutoNumber;
      int num2 = !autoNumber.GetValueOrDefault() ? 1 : 0;
      cache2.AllowUpdate = num2 != 0;
      PXCache cache3 = ((PXSelectBase) this.Values).Cache;
      autoNumber = row.AutoNumber;
      int num3 = !autoNumber.GetValueOrDefault() ? 1 : 0;
      cache3.AllowDelete = num3 != 0;
      this._groupColumnsCache.TryInjectColumns(((PXSelectBase) this.Values).Cache, row.DimensionID);
    }
    else
    {
      ((PXSelectBase) this.Values).Cache.AllowInsert = true;
      ((PXSelectBase) this.Values).Cache.AllowUpdate = true;
      ((PXSelectBase) this.Values).Cache.AllowDelete = true;
    }
  }

  protected virtual void SegmentValue_Value_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row != null && !cache.HasAttributes(e.Row))
      return;
    PX.Objects.CS.Segment current = ((PXSelectBase<PX.Objects.CS.Segment>) this.Segment).Current;
    if (current == null)
      return;
    short? nullable1 = current.SegmentID;
    if (!nullable1.HasValue)
      return;
    StringBuilder stringBuilder1 = new StringBuilder();
    if (!current.AutoNumber.GetValueOrDefault())
    {
      StringBuilder stringBuilder2 = stringBuilder1;
      nullable1 = current.CaseConvert;
      string str1 = (nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1 ? ">" : "";
      stringBuilder2.Append(str1);
      StringBuilder stringBuilder3 = stringBuilder1;
      nullable1 = current.CaseConvert;
      string str2 = (nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 2 ? "<" : "";
      stringBuilder3.Append(str2);
    }
    short num1 = 0;
    bool? nullable2;
    while (true)
    {
      int num2 = (int) num1;
      nullable1 = current.Length;
      int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int valueOrDefault = nullable3.GetValueOrDefault();
      if (num2 < valueOrDefault & nullable3.HasValue)
      {
        nullable2 = current.AutoNumber;
        if (nullable2.GetValueOrDefault())
          stringBuilder1.Append("C");
        else
          stringBuilder1.Append(current.EditMask);
        ++num1;
      }
      else
        break;
    }
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    int? nullable4 = new int?();
    nullable2 = new bool?();
    bool? nullable5 = nullable2;
    nullable2 = new bool?();
    bool? nullable6 = nullable2;
    int? nullable7 = new int?();
    string str = stringBuilder1.ToString();
    nullable2 = new bool?();
    bool? nullable8 = nullable2;
    PXFieldState instance = PXStringState.CreateInstance(returnState, nullable4, nullable5, "Value", nullable6, nullable7, str, (string[]) null, (string[]) null, nullable8, (string) null, (string[]) null);
    selectingEventArgs.ReturnState = (object) instance;
  }

  protected virtual void SegmentValue_IsConsolidatedValue_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    SegmentValue row = e.Row as SegmentValue;
    if (row.IsConsolidatedValue.Value)
    {
      foreach (PXResult<SegmentValue> pxResult in ((PXSelectBase<SegmentValue>) this.Values).Select(new object[2]
      {
        (object) row.DimensionID,
        (object) row.SegmentID
      }))
      {
        SegmentValue segmentValue = PXResult<SegmentValue>.op_Implicit(pxResult);
        if (segmentValue.IsConsolidatedValue.Value && segmentValue.Value != row.Value)
        {
          segmentValue.IsConsolidatedValue = new bool?(false);
          ((PXSelectBase) this.Values).Cache.Update((object) segmentValue);
        }
      }
    }
    ((PXSelectBase) this.Values).View.RequestRefresh();
  }

  protected virtual void SegmentValue_IsConsolidatedValue_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row != null)
      return;
    PX.Objects.CS.Segment current = ((PXSelectBase<PX.Objects.CS.Segment>) this.Segment).Current;
    if (current != null)
    {
      bool flag = current.DimensionID == "INSUBITEM";
      PXUIFieldAttribute.SetVisible<SegmentValue.isConsolidatedValue>(cache, (object) null, flag);
      PXUIFieldAttribute.SetEnabled<SegmentValue.isConsolidatedValue>(cache, (object) null, flag);
    }
    else
      PXUIFieldAttribute.SetVisible<SegmentValue.isConsolidatedValue>(cache, (object) null, false);
  }

  protected virtual void SegmentValue_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    SegmentValue row = (SegmentValue) e.Row;
    if (row == null || !row.SegmentID.HasValue || string.IsNullOrEmpty(row.Value) || !this.IsSegmentValueExist(row))
      return;
    if (row.IsConsolidatedValue.GetValueOrDefault())
      throw new PXException("Segment value already exists.");
    cache.RaiseExceptionHandling<SegmentValue.value>(e.Row, (object) row.Value, (Exception) new PXException("Segment value already exists."));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SegmentValue_MappedSegValue_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row != null)
      return;
    PX.Objects.CS.Segment current = ((PXSelectBase<PX.Objects.CS.Segment>) this.Segment).Current;
    if (current != null)
    {
      if (current.DimensionID == "SUBACCOUNT")
        PXUIFieldAttribute.SetVisible<SegmentValue.mappedSegValue>(cache, (object) null, true);
      else
        PXUIFieldAttribute.SetVisible<SegmentValue.mappedSegValue>(cache, (object) null, false);
    }
    else
      PXUIFieldAttribute.SetVisible<SegmentValue.mappedSegValue>(cache, (object) null, false);
  }

  protected virtual void SegmentValue_MappedSegValue_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (string.IsNullOrEmpty((string) e.NewValue))
      return;
    short? consolNumChar = ((PXSelectBase<PX.Objects.CS.Segment>) this.Segment).Current.ConsolNumChar;
    int? nullable1 = consolNumChar.HasValue ? new int?((int) consolNumChar.GetValueOrDefault()) : new int?();
    int num = 0;
    if (!(nullable1.GetValueOrDefault() > num & nullable1.HasValue))
      return;
    int length = e.NewValue.ToString().Length;
    consolNumChar = ((PXSelectBase<PX.Objects.CS.Segment>) this.Segment).Current.ConsolNumChar;
    int? nullable2 = consolNumChar.HasValue ? new int?((int) consolNumChar.GetValueOrDefault()) : new int?();
    int valueOrDefault = nullable2.GetValueOrDefault();
    if (length > valueOrDefault & nullable2.HasValue)
      throw new PXSetPropertyException("Maximum total mapped segment value length should have {0} symbols.", new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.CS.Segment>) this.Segment).Current.ConsolNumChar
      });
  }

  private bool IsSegmentValueExist(SegmentValue segValue)
  {
    return ((PXSelectBase) new PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<Required<SegmentValue.dimensionID>>, And<SegmentValue.segmentID, Equal<Required<SegmentValue.segmentID>>, And<SegmentValue.value, Equal<Required<SegmentValue.value>>>>>>((PXGraph) this)).View.SelectSingle(new object[3]
    {
      (object) segValue.DimensionID,
      (object) segValue.SegmentID,
      (object) segValue.Value
    }) != null;
  }

  private class GroupColumnsCache
  {
    private List<SegmentMaint.GroupColumnsCache.Box> _items;
    private string _prevDimensionId;
    private string _prevSpecificModule;

    public GroupColumnsCache() => this._items = new List<SegmentMaint.GroupColumnsCache.Box>();

    public void TryInjectColumns(PXCache target, string dimensionId)
    {
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      if (object.Equals((object) dimensionId, (object) this._prevDimensionId))
        return;
      Dimension o = this.ReadDimension(target.Graph, dimensionId);
      string str = o.With<Dimension, string>((Func<Dimension, string>) (_ => _.SpecificModule));
      if (o == null || string.IsNullOrEmpty(str))
        this.RemovePreviousInjection(target);
      else if (!object.Equals((object) str, (object) this._prevSpecificModule))
      {
        this.RemovePreviousInjection(target);
        this.AddNewInjection(target, str);
      }
      this._prevDimensionId = dimensionId;
      this._prevSpecificModule = str;
    }

    private void AddNewInjection(PXCache target, string specificModule)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SegmentMaint.GroupColumnsCache.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new SegmentMaint.GroupColumnsCache.\u003C\u003Ec__DisplayClass6_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.target = target;
      // ISSUE: reference to a compiler-generated field
      PXGraph graph = cDisplayClass60.target.Graph;
      foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup, Where<RelationGroup.specificModule, Equal<Required<Dimension.specificModule>>, And<RelationGroup.specificType, Equal<Required<RelationGroup.specificType>>>>>.Config>.Select(graph, new object[2]
      {
        (object) specificModule,
        (object) typeof (SegmentValue).FullName
      }))
      {
        RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SegmentMaint.GroupColumnsCache.\u003C\u003Ec__DisplayClass6_1 cDisplayClass61 = new SegmentMaint.GroupColumnsCache.\u003C\u003Ec__DisplayClass6_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass61.CS\u0024\u003C\u003E8__locals1 = cDisplayClass60;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass61.name = relationGroup.GroupName;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass61.mask = relationGroup.GroupMask;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (!cDisplayClass61.CS\u0024\u003C\u003E8__locals1.target.Fields.Contains(cDisplayClass61.name) && cDisplayClass61.mask != null)
        {
          // ISSUE: method pointer
          PXFieldSelecting selectingHandler = new PXFieldSelecting((object) cDisplayClass61, __methodptr(\u003CAddNewInjection\u003Eb__0));
          // ISSUE: method pointer
          PXFieldUpdating updatingHandler = new PXFieldUpdating((object) cDisplayClass61, __methodptr(\u003CAddNewInjection\u003Eb__1));
          // ISSUE: reference to a compiler-generated field
          this._items.Add(new SegmentMaint.GroupColumnsCache.Box(cDisplayClass61.name, selectingHandler, updatingHandler));
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          cDisplayClass61.CS\u0024\u003C\u003E8__locals1.target.Fields.Add(cDisplayClass61.name);
          // ISSUE: reference to a compiler-generated field
          graph.FieldSelecting.AddHandler(typeof (SegmentValue), cDisplayClass61.name, selectingHandler);
          // ISSUE: reference to a compiler-generated field
          graph.FieldUpdating.AddHandler(typeof (SegmentValue), cDisplayClass61.name, updatingHandler);
        }
      }
    }

    private Dimension ReadDimension(PXGraph graph, string dimensionId)
    {
      if (string.IsNullOrEmpty(dimensionId))
        return (Dimension) null;
      return PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelect<Dimension, Where<Dimension.dimensionID, Equal<Required<PX.Objects.CS.Segment.dimensionID>>>>.Config>.Select(graph, new object[1]
      {
        (object) dimensionId
      }));
    }

    private void RemovePreviousInjection(PXCache target)
    {
      PXGraph graph = target.Graph;
      foreach (SegmentMaint.GroupColumnsCache.Box box in this._items)
      {
        string name = box.Name;
        target.Fields.Remove(name);
        graph.FieldSelecting.RemoveHandler(typeof (SegmentValue), name, box.SelectingHandler);
        graph.FieldUpdating.RemoveHandler(typeof (SegmentValue), name, box.UpdatingHandler);
      }
      this._items.Clear();
    }

    private class Box
    {
      private readonly string _name;
      private readonly PXFieldSelecting _selectingHandler;
      private readonly PXFieldUpdating _updatingHandler;

      public Box(string name, PXFieldSelecting selectingHandler, PXFieldUpdating updatingHandler)
      {
        this._name = name;
        this._selectingHandler = selectingHandler;
        this._updatingHandler = updatingHandler;
      }

      public string Name => this._name;

      public PXFieldSelecting SelectingHandler => this._selectingHandler;

      public PXFieldUpdating UpdatingHandler => this._updatingHandler;
    }
  }
}
