// Decompiled with JetBrains decompiler
// Type: PX.Data.PXScreenToSiteMapAddHelperBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public abstract class PXScreenToSiteMapAddHelperBase<T> : PXScreenToSiteMapAddHelperBase where T : class, IBqlTable, new()
{
  private const string DefaultScreenNumber = "000001";
  private readonly string _nameField;
  private readonly string _screenIdPrefix;
  private readonly PXView _prevScreenID;
  private bool _siteMapWasAltered;
  private bool _isCopyPasteCotext;

  protected string TitleField { get; }

  protected string ScreenIDField { get; }

  protected bool IsScreenFieldGuid { get; }

  protected PXGraph Graph { get; }

  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; }

  protected PXView SiteMap { get; }

  protected PXCache SiteMapCache
  {
    get => this.Graph.Caches[typeof (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal)];
  }

  protected PXCache Cache => this.Graph.Caches[typeof (T)];

  public override bool IsSiteMapAltered => this.SiteMapCache.IsDirty;

  public override IEnumerable<PX.SM.SiteMap> Cached
  {
    get
    {
      return this.SiteMapCache.Cached.Cast<PX.SM.SiteMap>().Select(node => new
      {
        node = node,
        status = this.SiteMapCache.GetStatus((object) node)
      }).Where(_param1 => _param1.status == PXEntryStatus.Inserted || _param1.status == PXEntryStatus.Updated).Select(_param1 => _param1.node);
    }
  }

  protected PXScreenToSiteMapAddHelperBase(
    PXGraph graph,
    IScreenInfoCacheControl screenInfoCacheControl,
    string screenIdPrefix,
    System.Type nameField,
    System.Type titleField,
    System.Type screenIDField)
  {
    if (titleField == (System.Type) null)
      throw new ArgumentNullException(nameof (titleField));
    if (nameField == (System.Type) null)
      throw new ArgumentNullException(nameof (nameField));
    if (screenIdPrefix == null)
      throw new ArgumentNullException(nameof (screenIdPrefix));
    if (string.IsNullOrEmpty(screenIdPrefix) || screenIdPrefix.Length >= 4)
      throw new ArgumentException("The screen ID prefix is invalid. It must be two characters long.");
    this._nameField = nameField.Name;
    this.TitleField = titleField.Name;
    this.ScreenIDField = screenIDField?.Name;
    this._screenIdPrefix = screenIdPrefix;
    this.Graph = graph ?? throw new ArgumentNullException(nameof (graph));
    this.ScreenInfoCacheControl = screenInfoCacheControl ?? throw new ArgumentNullException(nameof (screenInfoCacheControl));
    if (this.Cache.GetFieldType(this._nameField) != typeof (string))
      throw new ArgumentException("The name field must have the String type");
    if (this.ScreenIDField != null)
      this.IsScreenFieldGuid = this.Cache.GetFieldType(this.ScreenIDField) == typeof (Guid);
    this.Cache.RowSelecting += new PXRowSelecting(this.T_RowSelecting);
    this.Cache.RowUpdating += new PXRowUpdating(this.T_RowUpdating);
    this.Cache.RowDeleted += new PXRowDeleted(this.T_RowDeleted);
    this.Cache.RowSelected += new PXRowSelected(this.T_RowSelected);
    this.Cache.RowPersisted += new PXRowPersisted(this.T_RowPersisted);
    graph.FieldUpdated.AddHandler(typeof (T), this._nameField, new PXFieldUpdated(this.T_Name_FieldUpdated));
    graph.FieldUpdated.AddHandler(typeof (T), this.TitleField, new PXFieldUpdated(this.T_SitemapTitle_FieldUpdated));
    if (!this.IsScreenFieldGuid)
      graph.FieldUpdated.AddHandler(typeof (T), this.ScreenIDField, new PXFieldUpdated(this.T_ScreenID_FieldUpdated));
    this.SiteMap = new PXView(this.Graph, false, (BqlCommand) new Select<PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal, Where<PX.SM.SiteMap.nodeID, Equal<Required<PX.SM.SiteMap.nodeID>>>>());
    this._prevScreenID = new PXView(this.Graph, true, (BqlCommand) new Select<PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal, Where<PX.SM.SiteMap.screenID, Like<Required<PX.SM.SiteMap.screenID>>, And<IsNumeric<Substring<PX.SM.SiteMap.screenID, Required<PXScreenToSiteMapAddHelperBase.PrevScreenIdHelper.intValue>, Required<PXScreenToSiteMapAddHelperBase.PrevScreenIdHelper.intValue>>>, Equal<True>>>, OrderBy<Desc<PX.SM.SiteMap.screenID>>>());
    if (!this.Graph.Views.Caches.Contains(typeof (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal)))
      this.Graph.Views.Caches.Insert(this.Graph.Views.Caches.IndexOf(typeof (T)) + 1, typeof (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal));
    this.SiteMapCache.RowPersisting += new PXRowPersisting(this.SiteMap_RowPersisting);
    this.SiteMapCache.RowPersisted += new PXRowPersisted(this.SiteMap_RowPersisted);
    this.SiteMapCache.FieldDefaultingEvents.Add(typeof (PX.SM.SiteMap.nodeID).Name, new PXFieldDefaulting(this.SiteMap_NodeID_FieldDefaulting));
    this.Graph.OnBeforePersist += new System.Action<PXGraph>(this.OnBeforeGraphPersist);
    this.Graph.OnAfterPersist += new System.Action<PXGraph>(this.OnAfterGraphPersist);
  }

  protected virtual string IncrementScreenNumber(string screenID)
  {
    if (screenID == null)
      throw new ArgumentNullException(nameof (screenID));
    char[] chArray = screenID.Length == 8 ? screenID.ToCharArray() : throw new PXException("The length of the last screen ID is invalid. A new valid value cannot be generated.");
    for (int index = chArray.Length - 1; index >= 2; --index)
    {
      if (index == this._screenIdPrefix.Length && chArray[index] == '9')
        throw new PXException("The last screen ID is '99.99.99', which is a maximum value. A new screen cannot be added because incrementing the screen ID will cause an overflow.");
      if (!char.IsDigit(chArray[index]))
        throw new PXException("The last screen ID contains invalid symbols. A new valid value cannot be generated.");
      if (chArray[index] < '9')
      {
        ++chArray[index];
        break;
      }
      chArray[index] = '0';
    }
    return new string(chArray);
  }

  internal virtual string GenerateNewScreenID(PX.SM.SiteMap node)
  {
    PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this._prevScreenID.SelectSingle((object) (this._screenIdPrefix + "%"), (object) (this._screenIdPrefix.Length + 1), (object) (8 - this._screenIdPrefix.Length));
    return siteMapInternal != null ? this.IncrementScreenNumber(siteMapInternal.ScreenID) : (this._screenIdPrefix + "000001").Substring(0, 8);
  }

  protected virtual void SiteMap_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal row) || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || this.Graph.IsImport && !string.IsNullOrEmpty(row.ScreenID))
      return;
    this.SiteMap_InsertedRowPersisting(sender, (PX.SM.SiteMap) row);
  }

  protected virtual void SiteMap_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == PXTranStatus.Completed)
    {
      PX.SM.SiteMap row = (PX.SM.SiteMap) e.Row;
      if (!string.IsNullOrEmpty(row.ScreenID))
        this.ScreenInfoCacheControl.InvalidateCache(row.ScreenID);
    }
    this.RaiseRowPersisted(sender, e);
  }

  protected virtual void SiteMap_InsertedRowPersisting(PXCache sender, PX.SM.SiteMap row)
  {
    if (!string.IsNullOrEmpty(row.ScreenID))
      return;
    sender.SetValue<PX.SM.SiteMap.screenID>((object) row, (object) this.GenerateNewScreenID(row));
  }

  protected void SiteMap_NodeID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected virtual void OnBeforeGraphPersist(PXGraph graph)
  {
    this._siteMapWasAltered = this.IsSiteMapAltered;
  }

  protected virtual void OnAfterGraphPersist(PXGraph graph)
  {
    if (this._siteMapWasAltered)
    {
      graph.SelectTimeStamp();
      PXSiteMap.Provider.ClearThreadSlot();
    }
    this._siteMapWasAltered = this.IsSiteMapAltered;
  }

  internal virtual bool IsVisible(T row) => this.FindNodes(row).Any<PXSiteMapNode>();

  private void T_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    T newRow = e.NewRow as T;
    string newValue = sender.GetValue((object) newRow, this.TitleField)?.ToString();
    if ((object) newRow == null || !string.IsNullOrEmpty(newValue) || this.IsScreenFieldGuid || string.IsNullOrEmpty(this.ScreenIDField) || sender.GetValue((object) newRow, this.ScreenIDField) == null)
      return;
    sender.RaiseExceptionHandling(this.TitleField, (object) newRow, (object) newValue, (Exception) new PXSetPropertyException("Site Map Title cannot be empty."));
  }

  private void T_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (e.Row == null)
      return;
    foreach (PXSiteMapNode node in this.FindNodes((T) e.Row))
    {
      if (!(node.ScreenID == PXSiteMap.RootNode.ScreenID))
      {
        PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal instance = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.CreateInstance();
        instance.NodeID = new Guid?(node.NodeID);
        instance.ScreenID = node.ScreenID;
        this.SiteMapCache.Delete((object) instance);
        this.Graph.Caches[typeof (PX.SM.SiteMap)].RaiseRowDeleted((object) instance);
        if (this.ScreenIDField != null)
          sender.SetValue(e.Row, this.ScreenIDField, (object) null);
      }
    }
  }

  private void T_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    T row = (T) e.Row;
    if ((object) row == null || string.IsNullOrEmpty(this.TitleField))
      return;
    if (this.Graph.IsCopyPasteContext)
      this._isCopyPasteCotext = this.Graph.IsCopyPasteContext;
    bool isEnabled = this.Graph.IsCopyPasteContext || this.Graph.IsImport || this.IsVisible(row) || NonGenericIEnumerableExtensions.Any_(this.SiteMapCache.Inserted) || sender.GetStatus((object) row) == PXEntryStatus.Notchanged && sender.GetValue((object) row, this.TitleField) != null;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, this.TitleField, isEnabled);
    if (string.IsNullOrEmpty(this.ScreenIDField) || !this.Graph.IsImport || this._isCopyPasteCotext)
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, this.ScreenIDField, isEnabled);
  }

  private void T_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed || !(e.Row is T row))
      return;
    foreach (string screenId in this.FindNodes(row).Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (n => n.ScreenID)).Where<string>((Func<string, bool>) (s => !string.IsNullOrEmpty(s))))
      this.ScreenInfoCacheControl.InvalidateCache(screenId);
  }

  private void T_Name_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    string oldValue = e.OldValue as string;
    if (!string.Equals(sender.GetValue(e.Row, this.TitleField) as string, oldValue) || string.IsNullOrEmpty(oldValue))
      return;
    string str = sender.GetValue(e.Row, this._nameField) as string;
    bool flag = !string.IsNullOrEmpty(str) && this.IsVisible((T) e.Row);
    sender.SetValueExt(e.Row, this.TitleField, flag ? (object) str : (object) (string) null);
  }

  private void T_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is T row) || !this.IsVisible(row) || sender.GetValue((object) row, this.TitleField) != null)
      return;
    PXSiteMapNode[] array1 = this.FindNodes(row).ToArray<PXSiteMapNode>();
    if (array1.Length == 0)
      return;
    PXSiteMapNode o = array1[0];
    PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal instance = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.CreateInstance();
    instance.NodeID = new Guid?(o.NodeID);
    switch (this.SiteMapCache.GetStatus((object) instance))
    {
      case PXEntryStatus.Deleted:
      case PXEntryStatus.InsertedDeleted:
        if (this.ScreenIDField == null || array1.Length <= 1)
          break;
        string[] array2 = EnumerableExtensions.Except<string>(((IEnumerable<PXSiteMapNode>) array1).Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (n => n.ScreenID.ToUpperInvariant())), o.ScreenID.ToUpperInvariant()).Distinct<string>().Select<string, string>((Func<string, string>) (s => Mask.Format("CC.CC.CC.CC", s))).ToArray<string>();
        if (array2.Length == 0)
          break;
        sender.RaiseExceptionHandling(this.ScreenIDField, (object) row, (object) o.ScreenID, (Exception) new PXSetPropertyException("There are multiple nodes in the site map for this screen that have different screen IDs: {0}.", PXErrorLevel.Warning, new object[1]
        {
          (object) string.Join(", ", array2)
        }));
        break;
      default:
        sender.SetValue((object) row, this.TitleField, (object) o.With<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (_ => _.Title)));
        if (this.ScreenIDField != null)
        {
          sender.SetValue((object) row, this.ScreenIDField, this.IsScreenFieldGuid ? (object) o.NodeID : (object) o.ScreenID);
          goto case PXEntryStatus.Deleted;
        }
        goto case PXEntryStatus.Deleted;
    }
  }

  protected virtual void T_SitemapTitle_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    T row = (T) e.Row;
    if ((object) row == null)
      return;
    string str = (string) sender.GetValue((object) row, this.TitleField);
    if (string.IsNullOrEmpty(str))
      return;
    List<PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal> siteMapInternalList = new List<PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal>();
    PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal current = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.Current;
    if (current != null)
    {
      siteMapInternalList.Add(current);
    }
    else
    {
      foreach (PXSiteMapNode pxSiteMapNode in this.FindNodes(row).ToArray<PXSiteMapNode>())
      {
        PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal instance = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.CreateInstance();
        instance.NodeID = new Guid?(pxSiteMapNode.NodeID);
        PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal1 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.Locate((object) instance);
        if (siteMapInternal1 == null)
          siteMapInternal1 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMap.SelectSingle((object) pxSiteMapNode.NodeID);
        PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal2 = siteMapInternal1;
        if (siteMapInternal2 != null)
          siteMapInternalList.Add(siteMapInternal2);
      }
    }
    foreach (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal in siteMapInternalList)
    {
      siteMapInternal.Title = str;
      if (this.SiteMapCache.GetStatus((object) siteMapInternal) != PXEntryStatus.Deleted)
        this.SiteMapCache.Update((object) siteMapInternal);
    }
    PXSiteMapNode pxSiteMapNode1 = this.FindNodes(row).FirstOrDefault<PXSiteMapNode>();
    PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal3;
    if (pxSiteMapNode1 == null)
      siteMapInternal3 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) null;
    else
      siteMapInternal3 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMap.SelectSingle((object) pxSiteMapNode1.NodeID);
    PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal4 = siteMapInternal3;
    string b = pxSiteMapNode1?.Url ?? this.BuildUrl(row);
    foreach (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal5 in this.SiteMapCache.Inserted)
    {
      if (string.Equals(siteMapInternal5.Url, b, StringComparison.OrdinalIgnoreCase))
        this.SiteMapCache.Delete((object) siteMapInternal5);
    }
    if (siteMapInternal4 == null && this.IsVisible(row))
      siteMapInternal4 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.Insert();
    if (siteMapInternal4 != null)
    {
      siteMapInternal4.Url = b;
      siteMapInternal4.ParentID = new Guid?(PXSiteMap.RootNode.NodeID);
      siteMapInternal4.Title = (string) sender.GetValue((object) row, this.TitleField);
      PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal6 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.Update((object) siteMapInternal4);
      if (this.ScreenIDField != null)
        sender.SetValue((object) row, this.ScreenIDField, this.IsScreenFieldGuid ? (object) siteMapInternal6.NodeID : (object) siteMapInternal6.ScreenID);
    }
    if (!this.Graph.IsCopyPasteContext && !this.Graph.IsImport)
      return;
    if (!string.IsNullOrEmpty(this.ScreenIDField) && !this.IsScreenFieldGuid)
    {
      sender.SetValueExt((object) row, this.ScreenIDField, (object) this.GenerateNewScreenID((PX.SM.SiteMap) null));
    }
    else
    {
      PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal node = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.Insert();
      node.Url = b;
      node.ParentID = new Guid?(PXSiteMap.RootNode.NodeID);
      node.Title = (string) sender.GetValue(e.Row, this.TitleField);
      node.ScreenID = this.GenerateNewScreenID((PX.SM.SiteMap) node);
      this.SiteMapCache.Update((object) node);
      if (!this.IsScreenFieldGuid)
        return;
      sender.SetValueExt((object) row, this.ScreenIDField, (object) node.NodeID);
    }
  }

  protected void T_ScreenID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    T row = (T) e.Row;
    if ((object) row == null)
      return;
    PXSiteMapNode pxSiteMapNode = this.FindNodes(row).FirstOrDefault<PXSiteMapNode>();
    PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal1 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) null;
    if (pxSiteMapNode != null)
      siteMapInternal1 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMap.SelectSingle((object) pxSiteMapNode.NodeID);
    string b = pxSiteMapNode?.Url ?? this.BuildUrl(row);
    foreach (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal siteMapInternal2 in this.SiteMapCache.Inserted)
    {
      if (string.Equals(siteMapInternal2.Url, b, StringComparison.OrdinalIgnoreCase))
        this.SiteMapCache.Delete((object) siteMapInternal2);
    }
    string str = (string) sender.GetValue((object) row, this.ScreenIDField);
    if (string.IsNullOrEmpty(str))
      return;
    if (siteMapInternal1 == null)
      siteMapInternal1 = (PXScreenToSiteMapAddHelperBase<T>.SiteMapInternal) this.SiteMapCache.Insert();
    siteMapInternal1.Url = b;
    siteMapInternal1.ParentID = new Guid?(PXSiteMap.RootNode.NodeID);
    siteMapInternal1.Title = (string) sender.GetValue(e.Row, this.TitleField);
    siteMapInternal1.ScreenID = str;
    this.SiteMapCache.Update((object) siteMapInternal1);
  }

  public abstract IEnumerable<PXSiteMapNode> FindNodes(T row);

  public abstract string BuildUrl(T row);

  [PXHidden]
  [Serializable]
  public class SiteMapInternal : PXScreenToSiteMapAddHelperBase.PortalMap
  {
    [PXDBString(8, InputMask = ">CC.CC.CC.CC")]
    [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Screen ID")]
    [PXDefault]
    public override string ScreenID
    {
      get => this._ScreenID;
      set => this._ScreenID = value;
    }
  }
}
