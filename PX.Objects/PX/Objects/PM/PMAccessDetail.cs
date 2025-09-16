// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccessDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.CT;
using PX.SM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.PM;

public class PMAccessDetail : UserAccess
{
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.baseType, 
  #nullable disable
  Equal<CTPRType.project>>>>>.And<BqlOperand<
  #nullable enable
  PMProject.nonProject, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, PMProject>.View Project;
  public PXSave<PMProject> Save;
  public PXCancel<PMProject> Cancel;
  public PXFirst<PMProject> First;
  public PXPrevious<PMProject> Prev;
  public PXNext<PMProject> Next;
  public PXLast<PMProject> Last;

  protected virtual IEnumerable groups()
  {
    PMAccessDetail pmAccessDetail = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) pmAccessDetail, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PMProject).Namespace || UserAccess.IsIncluded(((UserAccess) pmAccessDetail).getMask(), relationGroup))
      {
        ((PXSelectBase<RelationGroup>) pmAccessDetail.Groups).Current = relationGroup;
        yield return (object) relationGroup;
      }
    }
  }

  public PMAccessDetail()
  {
    ((PXSelectBase) this.Project).Cache.AllowDelete = false;
    ((PXSelectBase) this.Project).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetRequired(((PXSelectBase) this.Project).Cache, (string) null, false);
    ((PXGraph) this).Views.Caches.Remove(((PXSelectBase<RelationGroup>) this.Groups).GetItemType());
    ((PXGraph) this).Views.Caches.Add(((PXSelectBase<RelationGroup>) this.Groups).GetItemType());
  }

  protected virtual byte[] getMask()
  {
    byte[] mask = (byte[]) null;
    if (((PXSelectBase<Users>) this.User).Current != null)
      mask = ((PXSelectBase<Users>) this.User).Current.GroupMask;
    else if (((PXSelectBase<PMProject>) this.Project).Current != null)
      mask = ((PXSelectBase<PMProject>) this.Project).Current.GroupMask;
    return mask;
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<Users>) this.User).Current != null)
    {
      UserAccess.PopulateNeighbours<Users>((PXSelectBase<Users>) this.User, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<Users>();
      base.Persist();
    }
    else
    {
      if (((PXSelectBase<PMProject>) this.Project).Current == null)
        return;
      UserAccess.PopulateNeighbours<PMProject>((PXSelectBase<PMProject>) this.Project, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<PMProject>();
      base.Persist();
    }
  }

  [PXDimensionSelector("PROJECT", typeof (Search<PMProject.contractCD, Where<PMProject.baseType, Equal<CTPRType.project>>>), typeof (PMProject.contractCD), new Type[] {typeof (PMProject.contractCD), typeof (PMProject.customerID), typeof (PMProject.description), typeof (PMProject.status)}, DescriptionField = typeof (PMProject.description), Filterable = true)]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public void _(PX.Data.Events.CacheAttached<PMProject.contractCD> e)
  {
  }
}
