// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentAndApprovalMapEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EP;

public class EPAssignmentAndApprovalMapEnq : PXGraph<EPAssignmentAndApprovalMapEnq>
{
  public PXSelect<EPAssignmentMap, Where<EPAssignmentMap.mapType, Equal<EPMapType.assignment>, Or<FeatureInstalled<FeaturesSet.approvalWorkflow>>>, OrderBy<Desc<EPAssignmentMap.createdDateTime>>> Maps;
  public PXSelect<EPAssignmentMap, Where2<Where<EPAssignmentMap.mapType, Equal<EPMapType.assignment>, Or<FeatureInstalled<FeaturesSet.approvalWorkflow>>>, And<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>>> MapsForRedirect;
  public PXCancel<EPAssignmentMap> Cancel;
  public PXAction<EPAssignmentMap> ViewDetails;
  public PXAction<EPAssignmentMap> AddApprovalNew;
  public PXAction<EPAssignmentMap> AddAssignmentNew;

  [PXUIField]
  [PXButton(ImageKey = "RecordEdit", Tooltip = "Navigate to the selected map")]
  public virtual void viewDetails()
  {
    if (((PXSelectBase<EPAssignmentMap>) this.Maps).Current == null)
      return;
    int? assignmentMapId = ((PXSelectBase<EPAssignmentMap>) this.Maps).Current.AssignmentMapID;
    ((PXSelectBase) this.Maps).Cache.Clear();
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) ((PXSelectBase<EPAssignmentMap>) this.MapsForRedirect).SelectSingle(new object[1]
    {
      (object) assignmentMapId
    }), (PXRedirectHelper.WindowMode) 4);
  }

  [PXUIField(DisplayName = "Add Approval Map")]
  [PXInsertButton(Tooltip = "Add New Approval Map", CommitChanges = true, ImageKey = "")]
  public void addApprovalNew()
  {
    EPApprovalMapMaint instance = PXGraph.CreateInstance<EPApprovalMapMaint>();
    ((PXSelectBase<EPAssignmentMap>) instance.AssigmentMap).Current = ((PXSelectBase<EPAssignmentMap>) instance.AssigmentMap).Insert();
    ((PXSelectBase) instance.AssigmentMap).Cache.IsDirty = false;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
  }

  [PXUIField(DisplayName = "Add Assignment Map")]
  [PXInsertButton(Tooltip = "Add New Assignment Map", CommitChanges = true, ImageKey = "")]
  protected void addAssignmentNew()
  {
    EPAssignmentMapMaint instance = PXGraph.CreateInstance<EPAssignmentMapMaint>();
    ((PXSelectBase<EPAssignmentMap>) instance.AssigmentMap).Current = ((PXSelectBase<EPAssignmentMap>) instance.AssigmentMap).Insert();
    ((PXSelectBase) instance.AssigmentMap).Cache.IsDirty = false;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
  }

  protected virtual void EPAssignmentMap_EntityType_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is EPAssignmentMap row))
      return;
    e.ReturnState = (object) this.CreateFieldStateForEntity(e.ReturnValue, row.EntityType, row.GraphType);
  }

  private PXFieldState CreateFieldStateForEntity(
    object returnState,
    string entityType,
    string graphType)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    Type type1 = (Type) null;
    if (graphType != null)
      type1 = GraphHelper.GetType(graphType);
    else if (entityType != null)
    {
      Type type2 = PXBuildManager.GetType(entityType, false);
      type1 = type2 == (Type) null ? (Type) null : EntityHelper.GetPrimaryGraphType((PXGraph) this, type2);
    }
    if (type1 != (Type) null)
    {
      PXSiteMapNode siteMapNodeUnsecure = PXSiteMapProviderExtensions.FindSiteMapNodeUnsecure(PXSiteMap.Provider, type1);
      if (siteMapNodeUnsecure != null)
      {
        stringList1.Add(entityType);
        stringList2.Add(siteMapNodeUnsecure.Title);
      }
    }
    return PXStringState.CreateInstance(returnState, new int?(60), new bool?(), "Entity", new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), (string) null, (string[]) null);
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Entity Type")]
  protected virtual void EPAssignmentMap_EntityType_CacheAttached(PXCache sender)
  {
  }
}
