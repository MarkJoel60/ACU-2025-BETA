// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ActivitiesEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.PM;
using PX.SM;
using PX.TM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.EP;

[DashboardType(new int[] {0, 20})]
public class ActivitiesEnq : PXGraph<ActivitiesEnq>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> _baccount;
  [PXHidden]
  public PXSelect<PX.Objects.AR.Customer> _customer;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> _vendor;
  [PXViewName("Activities")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (OwnedFilter))]
  public PXSelectReadonly<CRPMTimeActivity, Where<CRPMTimeActivity.classID, Equal<CRActivityClass.activity>, And<Where<CRPMTimeActivity.workgroupID, IsWorkgroupOrSubgroupOfContact<CurrentValue<AccessInfo.contactID>>, Or2<Where<CRPMTimeActivity.workgroupID, IsNull, And<CRPMTimeActivity.ownerID, IsSubordinateOfContact<CurrentValue<AccessInfo.contactID>>>>, Or<CRPMTimeActivity.ownerID, Equal<Current<AccessInfo.contactID>>, Or<CRActivity.createdByID, Equal<Current<AccessInfo.userID>>>>>>>>, OrderBy<Desc<CRPMTimeActivity.endDate, Desc<CRPMTimeActivity.priority, Desc<CRPMTimeActivity.startDate>>>>> Activities;
  public PXSetup<EPSetup> epsetup;
  public PXAction<CRPMTimeActivity> ViewOwner;
  public PXAction<CRPMTimeActivity> ViewEntity;
  public PXAction<CRPMTimeActivity> ViewActivity;

  public ActivitiesEnq()
  {
    PXUIFieldAttribute.SetDisplayName<CRPMTimeActivity.startDate>(((PXSelectBase) this.Activities).Cache, "Date");
    PXUIFieldAttribute.SetDisplayName<CRPMTimeActivity.endDate>(((PXSelectBase) this.Activities).Cache, "Completed At");
    PXCache cach = ((PXGraph) this).Caches[typeof (CRPMTimeActivity)];
    PXUIFieldAttribute.SetVisible(cach, (string) null, false);
    PXUIFieldAttribute.SetVisible<CRPMTimeActivity.subject>(cach, (object) null);
    PXUIFieldAttribute.SetVisible<CRPMTimeActivity.uistatus>(cach, (object) null);
    PXUIFieldAttribute.SetVisible<CRPMTimeActivity.startDate>(cach, (object) null);
    PXUIFieldAttribute.SetVisible<CRPMTimeActivity.timeSpent>(cach, (object) null);
    PXUIFieldAttribute.SetVisible<CRPMTimeActivity.overtimeSpent>(cach, (object) null);
    PXUIFieldAttribute.SetVisible<CRPMTimeActivity.source>(cach, (object) null);
    bool flag = ProjectAttribute.IsPMVisible("TA");
    PXUIFieldAttribute.SetVisible<CRPMTimeActivity.projectID>(((PXSelectBase) this.Activities).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CRPMTimeActivity.projectTaskID>(((PXSelectBase) this.Activities).Cache, (object) null, flag);
  }

  [PXUIField(DisplayName = "View Owner", Visible = false)]
  [PXLookupButton(Tooltip = "Shows current owner")]
  protected virtual IEnumerable viewOwner(PXAdapter adapter)
  {
    CRPMTimeActivity current = ((PXSelectBase<CRPMTimeActivity>) this.Activities).Current;
    if (current != null && current.OwnerID.HasValue)
    {
      EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.defContactID, Equal<Required<CRPMTimeActivity.ownerID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.OwnerID
      }));
      if (epEmployee != null)
        PXRedirectHelper.TryRedirect((PXGraph) this, (object) epEmployee, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Entity", Visible = false)]
  [PXLookupButton]
  protected virtual void viewEntity()
  {
    CRPMTimeActivity current = ((PXSelectBase<CRPMTimeActivity>) this.Activities).Current;
    if (current == null)
      return;
    new EntityHelper((PXGraph) this).NavigateToRow(current.RefNoteID, (PXRedirectHelper.WindowMode) 1);
  }

  [PXUIField(DisplayName = "View Entity", Visible = false)]
  protected virtual IEnumerable viewActivity(PXAdapter adapter)
  {
    CRPMTimeActivity current = ((PXSelectBase<CRPMTimeActivity>) this.Activities).Current;
    if (current != null)
    {
      CRActivityMaint instance = PXGraph.CreateInstance<CRActivityMaint>();
      ((PXSelectBase<CRActivity>) instance.Activities).Current = PXResultset<CRActivity>.op_Implicit(((PXSelectBase<CRActivity>) instance.Activities).Search<CRPMTimeActivity.noteID>((object) current.NoteID, Array.Empty<object>()));
      if (((PXSelectBase<CRActivity>) instance.Activities).Current != null)
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Class", Visible = false)]
  [PXMergeAttributes]
  protected virtual void CRPMTimeActivity_ClassID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Type", Required = true, Visible = false)]
  [PXMergeAttributes]
  protected virtual void CRPMTimeActivity_Type_CacheAttached(PXCache sender)
  {
  }

  [Project(FieldClass = "PROJECT", BqlField = typeof (PMTimeActivity.projectID))]
  [PXMergeAttributes]
  public virtual void CRPMTimeActivity_ProjectID_CacheAttached(PXCache sender)
  {
  }
}
