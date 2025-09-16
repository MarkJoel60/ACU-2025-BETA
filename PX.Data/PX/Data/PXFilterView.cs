// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilterView
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public class PXFilterView : PXView
{
  private const string _PERSISTFILTER_ACTION = "__PersistFilter__";
  private const string _UNDOFILTER_ACTION = "__UndoFilter__";
  internal string _ViewName;
  private string _ScreenID;

  public PXFilterView(PXGraph graph, string screenID, string viewName)
    : base(graph, true, (BqlCommand) new PX.Data.Select<FilterHeader, Where<FilterHeader.screenID, Equal<Required<FilterHeader.screenID>>, And<FilterHeader.isHidden, NotEqual<True>, And<FilterHeader.viewName, Equal<Required<FilterHeader.viewName>>, And<Where<FilterHeader.isShared, Equal<True>, Or<FilterHeader.userName, Equal<Current<AccessInfo.userName>>, Or<FilterHeader.isSystem, Equal<True>>>>>>>>, OrderBy<Asc<FilterHeader.filterOrder>>>())
  {
    this._ViewName = viewName;
    this._ScreenID = string.IsNullOrEmpty(screenID) ? screenID : screenID.Replace(".", "");
    this._IsReadOnly = false;
    if (!graph.Caches.ContainsKey(typeof (FilterHeader)))
    {
      PXCache pxCache = (PXCache) new PXFilterCache(graph);
      graph.Caches[typeof (FilterHeader)] = pxCache;
    }
    if (!graph.Actions.Contains((object) "__PersistFilter__"))
    {
      PXFilterView.PXFilterAction pxFilterAction = new PXFilterView.PXFilterAction(graph, "Save", new PXButtonDelegate(this.SaveFilterDelegate));
      pxFilterAction.SetVisible(false);
      graph.Actions["__PersistFilter__"] = (PXAction) pxFilterAction;
    }
    if (!graph.Actions.Contains((object) "__UndoFilter__"))
    {
      PXFilterView.PXFilterAction pxFilterAction = new PXFilterView.PXFilterAction(graph, "Undo", new PXButtonDelegate(this.UndoFilterDelegate));
      pxFilterAction.SetVisible(false);
      graph.Actions["__UndoFilter__"] = (PXAction) pxFilterAction;
    }
    graph.RowPersisting.AddHandler<FilterHeader>(new PXRowPersisting(this.RowPersistingHandler));
  }

  private void RowPersistingHandler(PXCache sender, PXRowPersistingEventArgs e)
  {
    FilterHeader row = e.Row as FilterHeader;
    bool flag = PXSiteMap.Provider.FindSiteMapNodeByScreenID("CS209010") != null;
    if (!(row == null | flag) && (Convert.ToBoolean(sender.GetValueOriginal<FilterHeader.isShared>((object) row)) || row.IsShared.GetValueOrDefault()))
      throw new PXException("You are not allowed to edit shared filters. If you need to edit this filter contact your system administrator.");
  }

  public override List<object> Select(
    object[] currents,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    List<object> list = ((IEnumerable<object>) FilterHeader.Definition.Get().Where<FilterHeader>((Func<FilterHeader, bool>) (f =>
    {
      if (!(f.ScreenID == this._ScreenID) || !(f.ViewName == this._ViewName) || f.IsHidden.GetValueOrDefault())
        return false;
      return f.IsShared.GetValueOrDefault() || f.IsSystem.GetValueOrDefault() || f.UserName == this._Graph.Accessinfo.UserName;
    }))).ToList<object>();
    List<FilterHeader> hidden = new List<FilterHeader>();
    foreach (FilterHeader filterHeader in list)
    {
      FilterHeader header = filterHeader;
      if (FilterHeader.Definition.Get().Where<FilterHeader>((Func<FilterHeader, bool>) (f =>
      {
        if (f.FilterName == header.FilterName && f.UserName == this._Graph.Accessinfo.UserName && f.ViewName == this._ViewName)
        {
          Guid? filterId1 = f.FilterID;
          Guid? filterId2 = header.FilterID;
          if ((filterId1.HasValue == filterId2.HasValue ? (filterId1.HasValue ? (filterId1.GetValueOrDefault() != filterId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && f.IsShared.GetValueOrDefault())
            return f.IsHidden.GetValueOrDefault();
        }
        return false;
      })).Any<FilterHeader>())
      {
        bool? isShared = header.IsShared;
        bool flag = true;
        if (isShared.GetValueOrDefault() == flag & isShared.HasValue)
        {
          hidden.Add(header);
          continue;
        }
      }
      header.IsOwned = new bool?(string.Compare(header.UserName, this._Graph.Accessinfo.UserName, true) == 0);
    }
    list.RemoveAll((Predicate<object>) (i => hidden.Contains((FilterHeader) i)));
    return list;
  }

  public override PXCache Cache
  {
    get
    {
      PXFilterCache cache = base.Cache as PXFilterCache;
      cache.ViewName = this._ViewName;
      cache.ScreenID = this._ScreenID;
      return (PXCache) cache;
    }
  }

  internal string ViewName => this._ViewName;

  [PXButton]
  private IEnumerable UndoFilterDelegate(PXAdapter adapter)
  {
    this._Graph.Caches[typeof (FilterHeader)].Clear();
    this._Graph.Caches[typeof (FilterRow)].Clear();
    return adapter.Get();
  }

  [PXButton]
  private IEnumerable SaveFilterDelegate(PXAdapter adapter)
  {
    if (adapter.Parameters.Length != 0)
    {
      Guid parameter = (Guid) adapter.Parameters[0];
      FilterHeader header1 = (FilterHeader) PXSelectBase<FilterHeader, PXSelect<FilterHeader, Where<FilterHeader.filterID, Equal<Required<FilterHeader.filterID>>>>.Config>.Select(this._Graph, (object) parameter);
      if (header1 == null)
      {
        foreach (FilterHeader filterHeader in this._Graph.Caches[typeof (FilterHeader)].Deleted)
        {
          Guid? filterId = filterHeader.FilterID;
          Guid guid = parameter;
          if ((filterId.HasValue ? (filterId.HasValue ? (filterId.GetValueOrDefault() == guid ? 1 : 0) : 1) : 0) != 0)
          {
            header1 = filterHeader;
            break;
          }
        }
      }
      if (header1 != null)
      {
        bool? nullable = header1.IsSystem;
        bool flag1 = true;
        if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
        {
          string b = string.IsNullOrEmpty(header1.UserName) ? PXAccess.GetUserName() : header1.UserName;
          PXCache cach1 = this._Graph.Caches[typeof (FilterHeader)];
          PXCache cach2 = this._Graph.Caches[typeof (FilterRow)];
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            List<FilterHeader> filterHeaderList = new List<FilterHeader>();
            if (string.Equals(header1.UserName, b))
            {
              if (cach1.GetStatus((object) header1) == PXEntryStatus.Deleted)
                filterHeaderList.Add(header1);
              else
                PXFilterView.PersistHeader(cach1, header1);
            }
            nullable = header1.IsDefault;
            bool flag2 = true;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
            {
              foreach (FilterHeader header2 in cach1.Cached)
              {
                nullable = header2.IsDefault;
                bool flag3 = true;
                if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue) && header2.ScreenID == header1.ScreenID && header2.ViewName == header1.ViewName && header2.UserName == header1.UserName)
                {
                  if (cach1.GetStatus((object) header2) == PXEntryStatus.Deleted)
                    filterHeaderList.Add(header2);
                  else
                    PXFilterView.PersistHeader(cach1, header2);
                }
              }
            }
            foreach (FilterRow row in cach2.Cached)
            {
              Guid? filterId = row.FilterID;
              Guid guid = parameter;
              if ((filterId.HasValue ? (filterId.HasValue ? (filterId.GetValueOrDefault() == guid ? 1 : 0) : 1) : 0) != 0)
              {
                switch (cach2.GetStatus((object) row))
                {
                  case PXEntryStatus.Updated:
                    cach2.PersistUpdated((object) row);
                    continue;
                  case PXEntryStatus.Inserted:
                    row.FilterID = header1.FilterID;
                    cach2.PersistInserted((object) row);
                    continue;
                  case PXEntryStatus.Deleted:
                    cach2.PersistDeleted((object) row);
                    continue;
                  default:
                    continue;
                }
              }
            }
            foreach (FilterHeader header3 in filterHeaderList)
            {
              foreach (PXResult<FilterRow> pxResult in PXSelectBase<FilterRow, PXSelect<FilterRow, Where<FilterRow.filterID, Equal<Required<FilterRow.filterID>>>>.Config>.Select(this._Graph, (object) header3.FilterID))
              {
                FilterRow row = (FilterRow) pxResult;
                cach2.PersistDeleted((object) row);
              }
              foreach (FilterRow row in cach2.Deleted)
              {
                Guid? filterId1 = row.FilterID;
                Guid? filterId2 = header3.FilterID;
                if ((filterId1.HasValue == filterId2.HasValue ? (filterId1.HasValue ? (filterId1.GetValueOrDefault() == filterId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
                  cach2.PersistDeleted((object) row);
              }
              PXFilterView.PersistHeader(cach1, header3);
            }
            transactionScope.Complete(this._Graph);
          }
          cach1.Normalize();
          cach1.Persisted(false);
          cach2.Normalize();
          cach2.Persisted(false);
          adapter.Parameters[0] = (object) header1.FilterID;
          PXDatabase.SelectTimeStamp();
        }
      }
    }
    return adapter.Get();
  }

  private static void PersistHeader(PXCache headerCache, FilterHeader header)
  {
    switch (headerCache.GetStatus((object) header))
    {
      case PXEntryStatus.Updated:
        headerCache.PersistUpdated((object) header);
        break;
      case PXEntryStatus.Inserted:
        headerCache.PersistInserted((object) header);
        break;
      case PXEntryStatus.Deleted:
        headerCache.PersistDeleted((object) header);
        break;
    }
  }

  public sealed class PXFilterAction : PXAction
  {
    private readonly PXButtonState _state;
    private readonly PXButtonDelegate _handler;

    public PXFilterAction(PXGraph graph, string displayName, PXButtonDelegate handler)
      : base(graph)
    {
      this._state = PXButtonState.CreateInstance((object) null, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (FilterHeader));
      this._handler = handler;
      this._Attributes = new PXEventSubscriberAttribute[1]
      {
        (PXEventSubscriberAttribute) new PXUIFieldAttribute()
        {
          DisplayName = displayName
        }
      };
    }

    public override IEnumerable Press(PXAdapter adapter) => this._handler(adapter);

    public override object GetState(object row) => (object) this._state;

    public override void SetMenu(ButtonMenu[] menus)
    {
    }

    public override void AddMenuAction(PXAction action, string prevAction, bool insertAfter)
    {
    }

    internal override void RemoveMenuAction(PXAction action)
    {
    }

    public override System.Type GetRowType() => (System.Type) null;

    public override void Press()
    {
    }
  }
}
