// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAccessDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.IN;

public class INAccessDetail : UserAccess
{
  public PXSetup<PX.Objects.IN.INSetup> INSetup;
  public PXSelect<INSite, Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>> Site;
  public PXSave<INSite> Save;
  public PXCancel<INSite> Cancel;
  public PXFirst<INSite> First;
  public PXPrevious<INSite> Prev;
  public PXNext<INSite> Next;
  public PXLast<INSite> Last;

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (Search<INSite.siteCD, Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>>), typeof (INSite.siteCD))]
  protected virtual void INSite_SiteCD_CacheAttached(PXCache sender)
  {
  }

  public INAccessDetail()
  {
    PX.Objects.IN.INSetup current = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current;
    ((PXSelectBase) this.Site).Cache.AllowDelete = false;
    ((PXSelectBase) this.Site).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Site).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<INSite.siteCD>(((PXSelectBase) this.Site).Cache, (object) null);
    ((PXGraph) this).Views.Caches.Remove(typeof (RelationGroup));
    ((PXGraph) this).Views.Caches.Add(typeof (RelationGroup));
  }

  protected virtual IEnumerable groups()
  {
    INAccessDetail inAccessDetail = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) inAccessDetail, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if ((relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (InventoryItem).Namespace) && (relationGroup.SpecificType == null || relationGroup.SpecificType == typeof (INSite).FullName) || UserAccess.IsIncluded(((UserAccess) inAccessDetail).getMask(), relationGroup))
      {
        ((PXSelectBase<RelationGroup>) inAccessDetail.Groups).Current = relationGroup;
        yield return (object) relationGroup;
      }
    }
  }

  protected virtual byte[] getMask()
  {
    byte[] mask = (byte[]) null;
    if (((PXSelectBase<INSite>) this.Site).Current != null)
      mask = ((PXSelectBase<INSite>) this.Site).Current.GroupMask;
    return mask;
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<INSite>) this.Site).Current == null)
      return;
    UserAccess.PopulateNeighbours<INSite>((PXSelectBase<INSite>) this.Site, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
    PXSelectorAttribute.ClearGlobalCache<PX.Objects.GL.Account>();
    base.Persist();
  }
}
