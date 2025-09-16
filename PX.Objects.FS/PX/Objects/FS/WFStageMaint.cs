// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.WFStageMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.FS;

public class WFStageMaint : PXGraph<
#nullable disable
WFStageMaint>
{
  private const int RootNodeID = 0;
  private FSWFStage fsWFStageRow_Current;
  public PXCancel<WFStageMaint.WFStageFilter> Cancel;
  public PXSave<WFStageMaint.WFStageFilter> Save;
  public PXFilter<WFStageMaint.WFStageFilter> Filter;
  public PXFilter<WFStageMaint.SelectedNode> NodeFilter;
  public PXSelect<FSWFStage, Where<FSWFStage.wFID, Equal<Current<WFStageMaint.SelectedNode.wFID>>>> CurrentItem;
  public PXSelect<FSWFStage, Where<FSWFStage.parentWFStageID, Equal<Argument<int?>>>, OrderBy<Asc<FSWFStage.sortOrder>>> Nodes;
  public PXSelect<FSWFStage, Where<FSWFStage.parentWFStageID, Equal<Argument<int?>>>, OrderBy<Asc<FSWFStage.sortOrder>>> Items;
  public PXAction<WFStageMaint.WFStageFilter> up;
  public PXAction<WFStageMaint.WFStageFilter> down;

  protected virtual IEnumerable nodes([PXInt] int? parent)
  {
    List<FSWFStage> fswfStageList = new List<FSWFStage>();
    if (!parent.HasValue)
    {
      FSWFStage fswfStage = new FSWFStage();
      fswfStage.WFStageID = new int?(0);
      fswfStage.WFID = ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.WFID;
      FSSrvOrdType fsSrvOrdType = FSSrvOrdType.UK.Find((PXGraph) this, fswfStage.WFID);
      if (fsSrvOrdType != null)
      {
        fswfStage.WFStageCD = fsSrvOrdType.SrvOrdType;
        fswfStageList.Add(fswfStage);
      }
    }
    else
    {
      foreach (PXResult<FSWFStage> pxResult in PXSelectBase<FSWFStage, PXSelect<FSWFStage, Where<FSWFStage.wFID, Equal<Current<WFStageMaint.SelectedNode.wFID>>, And<FSWFStage.parentWFStageID, Equal<Required<FSWFStage.parentWFStageID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) parent
      }))
      {
        FSWFStage fswfStage = PXResult<FSWFStage>.op_Implicit(pxResult);
        fswfStageList.Add(fswfStage);
      }
    }
    return (IEnumerable) fswfStageList;
  }

  protected virtual IEnumerable items([PXInt] int? parent)
  {
    ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.ParentWFStageID = !parent.HasValue ? new int?(0) : parent;
    PXResultset<FSWFStage> pxResultset;
    if (parent.HasValue)
    {
      int? nullable = parent;
      int num = 0;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        pxResultset = PXSelectBase<FSWFStage, PXSelect<FSWFStage, Where<FSWFStage.wFID, Equal<Current<WFStageMaint.SelectedNode.wFID>>, And<FSWFStage.parentWFStageID, Equal<Required<FSWFStage.parentWFStageID>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) parent
        });
        goto label_4;
      }
    }
    pxResultset = PXSelectBase<FSWFStage, PXSelect<FSWFStage, Where<FSWFStage.wFID, Equal<Current<WFStageMaint.SelectedNode.wFID>>, And<FSWFStage.parentWFStageID, Equal<Current<WFStageMaint.SelectedNode.parentWFStageID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
label_4:
    return (IEnumerable) pxResultset;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Up(PXAdapter adapter)
  {
    IList<FSWFStage> sortedItems = this.GetSortedItems();
    int? wfStageId = ((PXSelectBase<FSWFStage>) this.CurrentItem).Current.WFStageID;
    int index1 = 0;
    int? nullable1;
    int? nullable2;
    for (int index2 = 0; index2 < sortedItems.Count; ++index2)
    {
      nullable1 = sortedItems[index2].WFStageID;
      nullable2 = wfStageId;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        index1 = index2;
      sortedItems[index2].SortOrder = new int?(index2 + 1);
      ((PXSelectBase<FSWFStage>) this.Items).Update(sortedItems[index2]);
    }
    if (index1 > 0)
    {
      FSWFStage fswfStage1 = sortedItems[index1];
      nullable2 = fswfStage1.SortOrder;
      nullable1 = nullable2;
      fswfStage1.SortOrder = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?();
      FSWFStage fswfStage2 = sortedItems[index1 - 1];
      nullable2 = fswfStage2.SortOrder;
      nullable1 = nullable2;
      fswfStage2.SortOrder = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
      ((PXSelectBase<FSWFStage>) this.Items).Update(sortedItems[index1]);
      ((PXSelectBase<FSWFStage>) this.Items).Update(sortedItems[index1 - 1]);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Down(PXAdapter adapter)
  {
    IList<FSWFStage> sortedItems = this.GetSortedItems();
    int? wfStageId = ((PXSelectBase<FSWFStage>) this.CurrentItem).Current.WFStageID;
    int index1 = 0;
    int? nullable1;
    int? nullable2;
    for (int index2 = 0; index2 < sortedItems.Count; ++index2)
    {
      nullable1 = sortedItems[index2].WFStageID;
      nullable2 = wfStageId;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        index1 = index2;
      sortedItems[index2].SortOrder = new int?(index2 + 1);
      ((PXSelectBase<FSWFStage>) this.Items).Update(sortedItems[index2]);
    }
    if (index1 < sortedItems.Count - 1)
    {
      FSWFStage fswfStage1 = sortedItems[index1];
      nullable2 = fswfStage1.SortOrder;
      nullable1 = nullable2;
      fswfStage1.SortOrder = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
      FSWFStage fswfStage2 = sortedItems[index1 + 1];
      nullable2 = fswfStage2.SortOrder;
      nullable1 = nullable2;
      fswfStage2.SortOrder = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?();
      ((PXSelectBase<FSWFStage>) this.Items).Update(sortedItems[index1]);
      ((PXSelectBase<FSWFStage>) this.Items).Update(sortedItems[index1 + 1]);
    }
    return adapter.Get();
  }

  protected virtual void _(Events.RowSelecting<FSWFStage> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSWFStage> e)
  {
  }

  protected virtual void _(Events.RowInserting<FSWFStage> e)
  {
    if (e.Row == null)
      return;
    FSWFStage row = e.Row;
    if (row == null || ((PXSelectBase<WFStageMaint.WFStageFilter>) this.Filter).Current == null)
      return;
    IEnumerable enumerable = this.items(((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.ParentWFStageID);
    int num = 0;
    foreach (PXResult<FSWFStage> pxResult in enumerable)
    {
      FSWFStage fswfStage = PXResult<FSWFStage>.op_Implicit(pxResult);
      int? sortOrder = fswfStage.SortOrder;
      if (sortOrder.Value > num)
      {
        sortOrder = fswfStage.SortOrder;
        num = sortOrder.Value;
      }
    }
    row.SortOrder = new int?(num + 1);
    row.ParentWFStageID = ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.ParentWFStageID;
    row.WFStageCD = row.WFStageCD.Trim();
    row.WFID = ((PXSelectBase<WFStageMaint.WFStageFilter>) this.Filter).Current.WFID;
    string str = this.ValidateItemName(row.WFStageCD, row.ParentWFStageID);
    if (!(str != string.Empty))
      return;
    ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<FSWFStage>>) e).Cache.RaiseExceptionHandling<FSWFStage.wFStageCD>((object) e.Row, (object) row.WFStageCD, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 4));
    e.Cancel = true;
  }

  protected virtual void _(Events.RowInserted<FSWFStage> e)
  {
    if (e.Row == null)
      return;
    FSWFStage row = e.Row;
    if (row == null || ((PXSelectBase<WFStageMaint.WFStageFilter>) this.Filter).Current == null)
      return;
    ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.ParentWFStageID = row.ParentWFStageID;
  }

  protected virtual void _(Events.RowUpdating<FSWFStage> e)
  {
    if (e.Row == null)
      return;
    FSWFStage newRow = e.NewRow;
    FSWFStage row = e.Row;
    PXCache cache = ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<FSWFStage>>) e).Cache;
    int? sortOrder1 = newRow.SortOrder;
    int? sortOrder2 = row.SortOrder;
    if (sortOrder1.GetValueOrDefault() == sortOrder2.GetValueOrDefault() & sortOrder1.HasValue == sortOrder2.HasValue || !e.ExternalCall)
      return;
    int? nullable = row.SortOrder;
    sortOrder1 = newRow.SortOrder;
    if (nullable.GetValueOrDefault() < sortOrder1.GetValueOrDefault() & nullable.HasValue & sortOrder1.HasValue)
    {
      PXCache sender = cache;
      FSWFStage fsWFStageRow_Route = newRow;
      sortOrder1 = row.SortOrder;
      int? from;
      if (!sortOrder1.HasValue)
      {
        nullable = new int?();
        from = nullable;
      }
      else
        from = new int?(sortOrder1.GetValueOrDefault() + 1);
      int? sortOrder3 = newRow.SortOrder;
      this.UpdateSequence(sender, fsWFStageRow_Route, from, sortOrder3, -1);
    }
    else
      this.UpdateSequence(cache, newRow, newRow.SortOrder, row.SortOrder, 1);
    ((PXSelectBase) this.Filter).View.RequestRefresh();
  }

  protected virtual void _(Events.RowUpdated<FSWFStage> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSWFStage> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSWFStage> e)
  {
    if (e.Row == null)
      return;
    FSWFStage row = e.Row;
    if (row == null || !row.WFStageID.HasValue)
      return;
    this.deleteRecurring(row);
  }

  protected virtual void _(Events.RowPersisting<FSWFStage> e)
  {
    if (e.Row == null)
      return;
    FSWFStage row = e.Row;
    if (row == null || e.Operation != 2)
      return;
    int? nullable = ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.WFID;
    int? wfid = row.WFID;
    if (!(nullable.GetValueOrDefault() == wfid.GetValueOrDefault() & nullable.HasValue == wfid.HasValue))
      return;
    int? parentWfStageId = ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.ParentWFStageID;
    nullable = row.WFStageID;
    if (!(parentWfStageId.GetValueOrDefault() == nullable.GetValueOrDefault() & parentWfStageId.HasValue == nullable.HasValue))
    {
      nullable = ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.ParentWFStageID;
      if (nullable.HasValue)
        return;
    }
    this.fsWFStageRow_Current = row;
  }

  protected virtual void _(Events.RowPersisted<FSWFStage> e)
  {
    if (e.Row == null)
      return;
    FSWFStage row = e.Row;
    if (row != this.fsWFStageRow_Current || e.TranStatus != 1)
      return;
    ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.WFID = row.WFID;
    ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.WFStageID = row.WFStageID;
  }

  protected virtual void _(
    Events.FieldUpdating<WFStageMaint.WFStageFilter, WFStageMaint.WFStageFilter.wFID> e)
  {
    if (((Events.FieldUpdatingBase<Events.FieldUpdating<WFStageMaint.WFStageFilter, WFStageMaint.WFStageFilter.wFID>>) e).NewValue == null)
      return;
    FSSrvOrdType fsSrvOrdType = PXResultset<FSSrvOrdType>.op_Implicit(PXSelectBase<FSSrvOrdType, PXSelect<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Required<FSSrvOrdType.srvOrdType>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      ((Events.FieldUpdatingBase<Events.FieldUpdating<WFStageMaint.WFStageFilter, WFStageMaint.WFStageFilter.wFID>>) e).NewValue
    }));
    if (fsSrvOrdType == null)
      return;
    ((PXSelectBase<WFStageMaint.WFStageFilter>) this.Filter).Current.Descr = fsSrvOrdType.Descr;
  }

  protected virtual void _(
    Events.FieldUpdated<WFStageMaint.WFStageFilter, WFStageMaint.WFStageFilter.wFID> e)
  {
    WFStageMaint.WFStageFilter row = e.Row;
    if (row == null)
      return;
    ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.WFID = row.WFID;
  }

  protected virtual void _(Events.RowSelecting<WFStageMaint.WFStageFilter> e)
  {
  }

  protected virtual void _(Events.RowSelected<WFStageMaint.WFStageFilter> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase) this.Items).AllowInsert = e.Row.WFID.HasValue;
  }

  protected virtual void _(Events.RowInserting<WFStageMaint.WFStageFilter> e)
  {
  }

  protected virtual void _(Events.RowInserted<WFStageMaint.WFStageFilter> e)
  {
  }

  protected virtual void _(Events.RowUpdating<WFStageMaint.WFStageFilter> e)
  {
  }

  protected virtual void _(Events.RowUpdated<WFStageMaint.WFStageFilter> e)
  {
  }

  protected virtual void _(Events.RowDeleting<WFStageMaint.WFStageFilter> e)
  {
  }

  protected virtual void _(Events.RowDeleted<WFStageMaint.WFStageFilter> e)
  {
  }

  protected virtual void _(Events.RowPersisting<WFStageMaint.WFStageFilter> e)
  {
  }

  protected virtual void _(Events.RowPersisted<WFStageMaint.WFStageFilter> e)
  {
  }

  public virtual int Comparison(FSWFStage fsWFStageRow_X, FSWFStage fsWFStageRow_Y)
  {
    return fsWFStageRow_X.SortOrder.Value.CompareTo(fsWFStageRow_Y.SortOrder.Value);
  }

  public virtual IList<FSWFStage> GetSortedItems()
  {
    List<FSWFStage> sortedItems = new List<FSWFStage>();
    foreach (PXResult<FSWFStage> pxResult in ((PXSelectBase<FSWFStage>) this.Items).Select(new object[1]
    {
      (object) ((PXSelectBase<WFStageMaint.SelectedNode>) this.NodeFilter).Current.ParentWFStageID
    }))
    {
      FSWFStage fswfStage = PXResult<FSWFStage>.op_Implicit(pxResult);
      sortedItems.Add(fswfStage);
    }
    sortedItems.Sort(new System.Comparison<FSWFStage>(this.Comparison));
    return (IList<FSWFStage>) sortedItems;
  }

  public virtual string ValidateItemName(string name, int? parentSSID)
  {
    if (PXResultset<FSWFStage>.op_Implicit(PXSelectBase<FSWFStage, PXSelect<FSWFStage, Where<FSWFStage.wFID, Equal<Current<WFStageMaint.WFStageFilter.wFID>>, And<FSWFStage.wFStageCD, Equal<Required<FSWFStage.wFStageCD>>, And<FSWFStage.parentWFStageID, Equal<Required<FSWFStage.parentWFStageID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) name,
      (object) parentSSID
    })) != null)
      return "This ID is already in use for the current level.";
    return PXResultset<FSWFStage>.op_Implicit(PXSelectBase<FSWFStage, PXSelect<FSWFStage, Where<FSWFStage.wFID, Equal<Current<WFStageMaint.WFStageFilter.wFID>>, And<FSWFStage.wFStageCD, Equal<Required<FSWFStage.wFStageCD>>, And<FSWFStage.wFStageID, Equal<Required<FSWFStage.parentWFStageID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) name,
      (object) parentSSID
    })) != null ? "This ID is already in use for the parent level." : string.Empty;
  }

  public virtual void deleteRecurring(FSWFStage fsWFStageRow)
  {
    if (fsWFStageRow == null)
      return;
    foreach (PXResult<FSWFStage> pxResult in PXSelectBase<FSWFStage, PXSelect<FSWFStage, Where<FSWFStage.wFID, Equal<Current<WFStageMaint.WFStageFilter.wFID>>, And<FSWFStage.parentWFStageID, Equal<Required<FSWFStage.wFStageID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsWFStageRow.WFStageID
    }))
      this.deleteRecurring(PXResult<FSWFStage>.op_Implicit(pxResult));
    ((PXSelectBase) this.Items).Cache.Delete((object) fsWFStageRow);
  }

  public virtual void UpdateSequence(
    PXCache sender,
    FSWFStage fsWFStageRow_Route,
    int? from,
    int? to,
    int step)
  {
    foreach (PXResult<FSWFStage> pxResult in PXSelectBase<FSWFStage, PXSelect<FSWFStage, Where<FSWFStage.wFID, Equal<Required<FSWFStage.wFID>>, And<FSWFStage.wFStageID, NotEqual<Required<FSWFStage.wFStageID>>, And<FSWFStage.sortOrder, Between<Required<FSWFStage.sortOrder>, Required<FSWFStage.sortOrder>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) fsWFStageRow_Route.WFID,
      (object) fsWFStageRow_Route.WFStageID,
      (object) from,
      (object) to
    }))
    {
      FSWFStage fswfStage = PXResult<FSWFStage>.op_Implicit(pxResult);
      int? sortOrder = fswfStage.SortOrder;
      int num = step;
      fswfStage.SortOrder = sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + num) : new int?();
    }
  }

  [Serializable]
  public class WFStageFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Service Order Type")]
    [FSSelectorWorkflow]
    public virtual int? WFID { get; set; }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField]
    public virtual string Descr { get; set; }

    public abstract class wFID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WFStageMaint.WFStageFilter.wFID>
    {
    }

    public abstract class descr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WFStageMaint.WFStageFilter.descr>
    {
    }
  }

  [DebuggerDisplay("WFID={WFID} ParentWFStageID={ParentWFStageID} WFStageID={WFStageID}")]
  [Serializable]
  public class SelectedNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    public virtual int? WFID { get; set; }

    [PXInt]
    public virtual int? ParentWFStageID { get; set; }

    [PXInt]
    public virtual int? WFStageID { get; set; }

    public abstract class wFID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WFStageMaint.SelectedNode.wFID>
    {
    }

    public abstract class parentWFStageID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WFStageMaint.SelectedNode.parentWFStageID>
    {
    }

    public abstract class wFStageID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WFStageMaint.SelectedNode.wFStageID>
    {
    }
  }
}
