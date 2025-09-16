// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectGroupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class PMProjectGroupMaint : PXGraph<PMProjectGroupMaint>
{
  [PXImport(typeof (PMProjectGroup))]
  public FbqlSelect<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>.Where<MatchUser>, PMProjectGroup>.View ProjectGroups;
  public PXSavePerRow<PMProjectGroup> Save;
  public PXCancel<PMProjectGroup> Cancel;
  public PXAction<PMProjectGroup> otherActions;
  public PXAction<PMProjectGroup> updateRestrictions;
  public PXAction<PMProjectGroup> manageRestrictionGroups;

  public IProjectGroupMaskHelper ProjectGroupMaskHelper
  {
    get
    {
      return (IProjectGroupMaskHelper) ((PXGraph) this).GetExtension<PMProjectGroupMaint.ProjectGroupMaskHelperExt>();
    }
  }

  public PMProjectGroupMaint()
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.rowLevelSecurity>();
    if (flag)
    {
      ((PXAction) this.otherActions).AddMenuAction((PXAction) this.updateRestrictions);
      ((PXAction) this.otherActions).AddMenuAction((PXAction) this.manageRestrictionGroups);
    }
    ((PXAction) this.updateRestrictions).SetVisible(flag);
    ((PXAction) this.manageRestrictionGroups).SetVisible(flag);
  }

  [PXUIField(DisplayName = "Other")]
  [PXButton]
  public void OtherActions()
  {
  }

  [PXUIField(DisplayName = "Update Project Restrictions")]
  [PXButton]
  public IEnumerable UpdateRestrictions(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PMProjectGroupMaint.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new PMProjectGroupMaint.\u003C\u003Ec__DisplayClass10_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.projectGroup = ((PXSelectBase<PMProjectGroup>) this.ProjectGroups).Current;
    // ISSUE: reference to a compiler-generated field
    if (string.IsNullOrEmpty(cDisplayClass100.projectGroup?.ProjectGroupID))
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    if (6 != ((PXSelectBase) this.ProjectGroups).Ask("Update Projects", $"The restriction groups will be reset for all projects that belong to the {cDisplayClass100.projectGroup.ProjectGroupID} project group. Do you want to update the settings of the projects?", (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      {
        (WebDialogResult) 6,
        "Update"
      },
      {
        (WebDialogResult) 7,
        "Cancel"
      }
    }))
      return adapter.Get();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass100, __methodptr(\u003CUpdateRestrictions\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Manage Restriction Groups")]
  [PXButton]
  public void ManageRestrictionGroups()
  {
    PMProjectGroup current = ((PXSelectBase<PMProjectGroup>) this.ProjectGroups).Current;
    if (current != null)
    {
      PMAccessByProjectGroupMaint instance = PXGraph.CreateInstance<PMAccessByProjectGroupMaint>();
      ((PXSelectBase<PMProjectGroup>) instance.ProjectGroup).Current = current;
      throw new PXRedirectRequiredException((PXGraph) instance, nameof (ManageRestrictionGroups));
    }
  }

  protected void _(Events.RowUpdating<PMProjectGroup> e)
  {
    string projectGroupId = e.NewRow?.ProjectGroupID;
    if (string.IsNullOrEmpty(projectGroupId))
      return;
    if (PXResultset<PMProjectGroup>.op_Implicit(PXSelectBase<PMProjectGroup, PXViewOf<PMProjectGroup>.BasedOn<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProjectGroup.projectGroupID, Equal<P.AsString>>>>>.And<Not<MatchUser>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectGroupId
    })) == null)
      return;
    ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<PMProjectGroup>>) e).Cache.RaiseExceptionHandling<PMProjectGroup.projectGroupID>((object) e.NewRow, (object) projectGroupId, (Exception) new PXSetPropertyException("You cannot access the {0} project group.", new object[1]
    {
      (object) projectGroupId
    }));
    e.Cancel = true;
  }

  protected void _(
    Events.FieldVerifying<PMProjectGroup, PMProjectGroup.projectGroupID> e)
  {
    if (e.Row == null)
      return;
    string oldValue = (string) e.OldValue;
    if (this.IsNotEmpty(oldValue))
      throw new PXSetPropertyException("The {0} project group cannot be renamed because at least one project or project template is mapped to this project group.", new object[1]
      {
        (object) oldValue
      });
  }

  private bool IsNotEmpty(string groupId)
  {
    if (string.IsNullOrEmpty(groupId))
      return false;
    return PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.projectGroupID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) groupId
    })) != null;
  }

  public sealed class ProjectGroupMaskHelperExt : PX.Objects.PM.ProjectGroupMaskHelper<PMProjectGroupMaint>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }
}
