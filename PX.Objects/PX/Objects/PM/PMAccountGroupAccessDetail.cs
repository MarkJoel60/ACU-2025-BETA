// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccountGroupAccessDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class PMAccountGroupAccessDetail : UserAccess
{
  public PXSelect<PMAccountGroup> AccountGroup;
  public PXSave<PMAccountGroup> Save;
  public PXCancel<PMAccountGroup> Cancel;
  public PXFirst<PMAccountGroup> First;
  public PXPrevious<PMAccountGroup> Prev;
  public PXNext<PMAccountGroup> Next;
  public PXLast<PMAccountGroup> Last;

  public PMAccountGroupAccessDetail()
  {
    ((PXSelectBase) this.AccountGroup).Cache.AllowDelete = false;
    ((PXSelectBase) this.AccountGroup).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.AccountGroup).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PMAccountGroup.groupCD>(((PXSelectBase) this.AccountGroup).Cache, (object) null);
    ((PXGraph) this).Views.Caches.Remove(((PXSelectBase<RelationGroup>) this.Groups).GetItemType());
    ((PXGraph) this).Views.Caches.Add(((PXSelectBase<RelationGroup>) this.Groups).GetItemType());
  }

  protected virtual IEnumerable groups()
  {
    PMAccountGroupAccessDetail groupAccessDetail = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) groupAccessDetail, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if ((relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PMAccountGroup).Namespace) && (relationGroup.SpecificType == null || relationGroup.SpecificType == typeof (PMAccountGroup).FullName) || UserAccess.IsIncluded(((UserAccess) groupAccessDetail).getMask(), relationGroup))
      {
        ((PXSelectBase<RelationGroup>) groupAccessDetail.Groups).Current = relationGroup;
        yield return (object) relationGroup;
      }
    }
  }

  protected virtual byte[] getMask()
  {
    byte[] mask = (byte[]) null;
    if (((PXSelectBase<Users>) this.User).Current != null)
      mask = ((PXSelectBase<Users>) this.User).Current.GroupMask;
    else if (((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current != null)
      mask = ((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current.GroupMask;
    return mask;
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<Users>) this.User).Current != null)
    {
      UserAccess.PopulateNeighbours<Users>((PXSelectBase<Users>) this.User, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<Users>();
    }
    else
    {
      if (((PXSelectBase<PMAccountGroup>) this.AccountGroup).Current == null)
        return;
      UserAccess.PopulateNeighbours<PMAccountGroup>((PXSelectBase<PMAccountGroup>) this.AccountGroup, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<PMAccountGroup>();
    }
    base.Persist();
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(Events.CacheAttached<PMAccountGroup.description> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(Events.CacheAttached<PMAccountGroup.type> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(Events.CacheAttached<RelationGroup.groupName> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(Events.CacheAttached<RelationGroup.description> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Restriction Group Type")]
  protected virtual void _(Events.CacheAttached<RelationGroup.groupType> e)
  {
  }
}
