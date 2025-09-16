// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.IN;

public class INAccess : BaseAccess
{
  public PXSetup<PX.Objects.IN.INSetup> INSetup;
  public PXSelect<INSite, Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>> Site;

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (Search<INSite.siteCD, Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>>), typeof (INSite.siteCD))]
  protected virtual void INSite_SiteCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [INAccess.INRelationGroupWarehouseSelector(typeof (RelationGroup.groupName), Filterable = true)]
  protected virtual void RelationGroup_GroupName_CacheAttached(PXCache sender)
  {
  }

  public INAccess()
  {
    PX.Objects.IN.INSetup current = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current;
    ((PXSelectBase) this.Site).Cache.AllowDelete = false;
    ((PXSelectBase) this.Site).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Site).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<INSite.included>(((PXSelectBase) this.Site).Cache, (object) null);
  }

  public int? INTransitSiteID
  {
    get
    {
      if (!((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current.TransitSiteID.HasValue)
        throw new PXException("Please fill transite site id in inventory preferences.");
      return ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current.TransitSiteID;
    }
  }

  protected virtual IEnumerable site()
  {
    INAccess inAccess = this;
    if (((PXSelectBase<RelationGroup>) inAccess.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) inAccess.Group).Current.GroupName))
    {
      foreach (PXResult<INSite> pxResult in PXSelectBase<INSite, PXSelect<INSite>.Config>.Select((PXGraph) inAccess, Array.Empty<object>()))
      {
        INSite inSite = PXResult<INSite>.op_Implicit(pxResult);
        int? siteId = inSite.SiteID;
        int? inTransitSiteId = inAccess.INTransitSiteID;
        if (!(siteId.GetValueOrDefault() == inTransitSiteId.GetValueOrDefault() & siteId.HasValue == inTransitSiteId.HasValue))
        {
          ((PXSelectBase<INSite>) inAccess.Site).Current = inSite;
          yield return (object) inSite;
        }
      }
    }
  }

  public static IEnumerable GroupDelegate(PXGraph graph, bool inclInserted)
  {
    PXResultset<Neighbour> set = PXSelectBase<Neighbour, PXSelectGroupBy<Neighbour, Where<Neighbour.leftEntityType, Equal<warehouseType>>, Aggregate<GroupBy<Neighbour.coverageMask, GroupBy<Neighbour.inverseMask, GroupBy<Neighbour.winCoverageMask, GroupBy<Neighbour.winInverseMask>>>>>>.Config>.Select(graph, Array.Empty<object>());
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select(graph, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(relationGroup.GroupName) | inclInserted && (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (InventoryItem).Namespace) && (relationGroup.SpecificType == null || relationGroup.SpecificType == typeof (INSite).FullName) || UserAccess.InNeighbours(set, relationGroup))
        yield return (object) relationGroup;
    }
  }

  protected virtual IEnumerable group() => INAccess.GroupDelegate((PXGraph) this, true);

  protected virtual void RelationGroup_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    base.RelationGroup_RowInserted(cache, e);
    RelationGroup row = (RelationGroup) e.Row;
    row.SpecificModule = typeof (inventoryModule).Namespace;
    row.SpecificType = typeof (INSite).FullName;
  }

  protected virtual void RelationGroup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RelationGroup row))
      return;
    if (string.IsNullOrEmpty(row.GroupName))
      ((PXAction) this.Save).SetEnabled(false);
    else
      ((PXAction) this.Save).SetEnabled(true);
  }

  protected virtual void INSite_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INSite row = e.Row as INSite;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null || sender.GetStatus((object) row) != null)
      return;
    row.Included = new bool?(false);
    for (int index = 0; index < row.GroupMask.Length && index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0 && ((int) row.GroupMask[index] & (int) current.GroupMask[index]) == (int) current.GroupMask[index])
        row.Included = new bool?(true);
    }
  }

  protected virtual void INSite_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    INSite row = e.Row as INSite;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  public virtual void Persist()
  {
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    this.populateNeighbours<INSite>((PXSelectBase<INSite>) this.Site);
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    base.Persist();
    PXSelectorAttribute.ClearGlobalCache<Users>();
    PXSelectorAttribute.ClearGlobalCache<INSite>();
    PXDimensionAttribute.Clear();
  }

  [Account]
  protected virtual void INSite_SalesAcctID_CacheAttached(PXCache sender)
  {
  }

  [Account]
  protected virtual void INSite_InvtAcctID_CacheAttached(PXCache sender)
  {
  }

  [Account]
  protected virtual void INSite_COGSAcctID_CacheAttached(PXCache sender)
  {
  }

  [Account]
  protected virtual void INSite_StdCstRevAcctID_CacheAttached(PXCache sender)
  {
  }

  [Account]
  protected virtual void INSite_StdCstVarAcctID_CacheAttached(PXCache sender)
  {
  }

  [Account]
  protected virtual void INSite_PPVAcctID_CacheAttached(PXCache sender)
  {
  }

  [Account]
  protected virtual void INSite_POAccrualAcctID_CacheAttached(PXCache sender)
  {
  }

  [Account]
  protected virtual void INSite_LCVarianceAcctID_CacheAttached(PXCache sender)
  {
  }

  public class INRelationGroupWarehouseSelectorAttribute(Type type) : PXCustomSelectorAttribute(type)
  {
    public virtual IEnumerable GetRecords() => INAccess.GroupDelegate(this._Graph, false);
  }
}
