// Decompiled with JetBrains decompiler
// Type: PX.Data.LEPMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class LEPMaint : PXGraph<LEPMaint>, ICanAlterSiteMap
{
  public PXSave<ListEntryPoint> Save;
  public PXCancel<ListEntryPoint> Cancel;
  public PXSelect<ListEntryPoint> Items;

  public IEnumerable items()
  {
    return (IEnumerable) PXSelectBase<ListEntryPoint, PXSelect<ListEntryPoint>.Config>.Select((PXGraph) this).AsEnumerable<PXResult<ListEntryPoint>>().Select<PXResult<ListEntryPoint>, ListEntryPoint>((Func<PXResult<ListEntryPoint>, ListEntryPoint>) (locale => (ListEntryPoint) locale)).Where<ListEntryPoint>((Func<ListEntryPoint, bool>) (item => !this.IsScreenHidden(item.ListScreenID) && !this.IsScreenHidden(item.EntryScreenID)));
  }

  public LEPMaint()
  {
    PXSiteMapNodeSelectorAttribute.SetRestriction<ListEntryPoint.entryScreenID>(this.Items.Cache, (object) null, (Func<PX.SM.SiteMap, bool>) (s => PXSiteMap.IsDashboard(s.Url)));
  }

  public bool IsSiteMapAltered { get; protected set; }

  public override void Persist()
  {
    this.IsSiteMapAltered |= this.Items.Cache.IsDirty;
    base.Persist();
  }

  public virtual void ListEntryPoint_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ListEntryPoint row))
      return;
    if (row.ListScreenID != null && PXSiteMap.Provider.FindSiteMapNodeByScreenID(row.ListScreenID) == null)
      cache.RaiseExceptionHandling<ListEntryPoint.listScreenID>((object) row, (object) row.ListScreenID, (Exception) new PXSetPropertyException("This form was removed from the site map. Select another form or delete the row.", PXErrorLevel.Warning));
    if (row.EntryScreenID != null && PXSiteMap.Provider.FindSiteMapNodeByScreenID(row.EntryScreenID) == null)
      cache.RaiseExceptionHandling<ListEntryPoint.entryScreenID>((object) row, (object) row.EntryScreenID, (Exception) new PXSetPropertyException("This form was removed from the site map. Select another form or delete the row.", PXErrorLevel.Warning));
    if (!string.Equals(row.EntryScreenID, row.ListScreenID, StringComparison.OrdinalIgnoreCase))
      return;
    cache.RaiseExceptionHandling<ListEntryPoint.entryScreenID>((object) row, (object) row.EntryScreenID, (Exception) new PXSetPropertyException("The entry screen and list screen must differ (Screen ID: {0}).", new object[1]
    {
      (object) row.ListScreenID
    }));
  }

  private bool IsScreenHidden(string screenId)
  {
    return PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId) != null && PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId) == null;
  }
}
