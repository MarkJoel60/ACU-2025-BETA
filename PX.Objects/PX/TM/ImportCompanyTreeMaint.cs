// Decompiled with JetBrains decompiler
// Type: PX.TM.ImportCompanyTreeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections;
using System.ComponentModel;

#nullable enable
namespace PX.TM;

[Serializable]
public class ImportCompanyTreeMaint : PXGraph<
#nullable disable
ImportCompanyTreeMaint>
{
  public PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Argument<int?>>>, OrderBy<Asc<EPCompanyTreeMaster.sortOrder>>> Folders;
  public PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Argument<int?>>>, OrderBy<Asc<EPCompanyTreeMaster.sortOrder>>> Items;
  public PXSelectJoin<EPCompanyTreeMember, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<PX.Objects.EP.EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>, Where<EPCompanyTreeMember.workGroupID, Equal<Current<ImportCompanyTreeMaint.SelectedNode.workGroupID>>>, OrderBy<Asc<PX.Objects.EP.EPEmployee.acctCD>>> Members;
  public PXSave<EPCompanyTreeMaster> Save;
  public PXCancel<EPCompanyTreeMaster> Cancel;
  public PXAction<EPCompanyTreeMaster> UpdateTree;
  public PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.workGroupID, Equal<Required<EPCompanyTreeMaster.workGroupID>>>> Item;
  public PXAction<EPCompanyTreeMaster> down;
  public PXAction<EPCompanyTreeMaster> up;
  public PXAction<EPCompanyTreeMaster> left;
  public PXAction<EPCompanyTreeMaster> right;
  public PXAction<EPCompanyTreeMaster> viewEmployee;
  public PXSelect<EPCompanyTreeH> treeH;
  public PXSelect<EPCompanyTreeH, Where<EPCompanyTreeH.workGroupID, Equal<Required<EPCompanyTreeH.workGroupID>>>, OrderBy<Desc<EPCompanyTreeH.parentWGLevel>>> parents;
  public PXSelect<EPCompanyTreeH, Where<EPCompanyTreeH.parentWGID, Equal<Required<EPCompanyTreeH.parentWGID>>>, OrderBy<Asc<EPCompanyTreeH.workGroupLevel>>> childrens;

  protected virtual IEnumerable folders([PXInt] int? WorkGroupID)
  {
    ImportCompanyTreeMaint companyTreeMaint1 = this;
    if (!WorkGroupID.HasValue)
    {
      EPCompanyTreeMaster companyTreeMaster = new EPCompanyTreeMaster();
      ((EPCompanyTree) companyTreeMaster).WorkGroupID = new int?(0);
      ((EPCompanyTree) companyTreeMaster).Description = PXSiteMap.RootNode.Title;
      yield return (object) companyTreeMaster;
    }
    else
    {
      ImportCompanyTreeMaint companyTreeMaint2 = companyTreeMaint1;
      object[] objArray = new object[1]
      {
        (object) WorkGroupID
      };
      foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.workGroupID>>>>.Config>.Select((PXGraph) companyTreeMaint2, objArray))
      {
        EPCompanyTreeMaster companyTreeMaster = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(((EPCompanyTree) companyTreeMaster).Description))
          yield return (object) companyTreeMaster;
      }
    }
  }

  protected virtual IEnumerable items([PXInt] int? WorkGroupID)
  {
    if (!WorkGroupID.HasValue)
      WorkGroupID = ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current != null ? ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current).WorkGroupID : new int?(0);
    this.CurrentSelected.FolderID = WorkGroupID;
    return (IEnumerable) PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.workGroupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) WorkGroupID
    });
  }

  protected virtual void EPCompanyTreeMaster_SortOrder_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    EPCompanyTreeMaster row = (EPCompanyTreeMaster) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) 1;
    ((CancelEventArgs) e).Cancel = true;
    PXResultset<EPCompanyTreeMaster> pxResultset = ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Select(new object[1]
    {
      (object) ((EPCompanyTree) row).ParentWGID
    });
    if (pxResultset.Count <= 0)
      return;
    EPCompanyTreeMaster companyTreeMaster = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResultset[pxResultset.Count - 1]);
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    int? sortOrder = ((EPCompanyTree) companyTreeMaster).SortOrder;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) (sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + 1) : new int?());
    defaultingEventArgs.NewValue = (object) local;
  }

  protected virtual void EPCompanyTreeMaster_ParentWGID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMaster))
      return;
    e.NewValue = (object) this.CurrentSelected.FolderID.GetValueOrDefault();
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPCompanyTreeMaster_BypassEscalation_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMaster row) || !((EPCompanyTree) row).BypassEscalation.GetValueOrDefault())
      return;
    ((EPCompanyTree) row).WaitTime = new int?(0);
  }

  protected virtual void EPCompanyTreeMaster_Description_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXResultset<EPCompanyTreeMaster>.op_Implicit(PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.description, Equal<Required<EPCompanyTreeMaster.description>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      e.NewValue
    })) != null)
      throw new PXSetPropertyException("An attempt was made to add a duplicate entry.", new object[1]
      {
        (object) "Description"
      });
  }

  protected virtual void EPCompanyTreeMaster_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMaster row))
      return;
    PXCache pxCache1 = sender;
    EPCompanyTreeMaster companyTreeMaster1 = row;
    int? parentWgid;
    int num1;
    if (!((EPCompanyTree) row).BypassEscalation.GetValueOrDefault())
    {
      parentWgid = ((EPCompanyTree) row).ParentWGID;
      int num2 = 0;
      num1 = !(parentWgid.GetValueOrDefault() == num2 & parentWgid.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<EPCompanyTree.waitTime>(pxCache1, (object) companyTreeMaster1, num1 != 0);
    PXCache pxCache2 = sender;
    EPCompanyTreeMaster companyTreeMaster2 = row;
    parentWgid = ((EPCompanyTree) row).ParentWGID;
    int num3 = 0;
    int num4 = !(parentWgid.GetValueOrDefault() == num3 & parentWgid.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EPCompanyTree.bypassEscalation>(pxCache2, (object) companyTreeMaster2, num4 != 0);
  }

  protected virtual void EPCompanyTreeMaster_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMaster row))
      return;
    this.insertHRecords(row);
  }

  protected virtual void EPCompanyTreeMaster_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    EPCompanyTreeMaster row = e.Row as EPCompanyTreeMaster;
    EPCompanyTreeMaster oldRow = e.OldRow as EPCompanyTreeMaster;
    if (row == null || oldRow == null)
      return;
    int? nullable = ((EPCompanyTree) row).ParentWGID;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue || ((EPCompanyTree) row).BypassEscalation.GetValueOrDefault())
      ((EPCompanyTree) row).WaitTime = new int?(0);
    nullable = ((EPCompanyTree) row).ParentWGID;
    int? parentWgid = ((EPCompanyTree) oldRow).ParentWGID;
    if (!(nullable.GetValueOrDefault() == parentWgid.GetValueOrDefault() & nullable.HasValue == parentWgid.HasValue))
    {
      this.updateHRecordParent(row);
    }
    else
    {
      int? waitTime = ((EPCompanyTree) row).WaitTime;
      nullable = ((EPCompanyTree) oldRow).WaitTime;
      if (waitTime.GetValueOrDefault() == nullable.GetValueOrDefault() & waitTime.HasValue == nullable.HasValue)
        return;
      int? workGroupId = ((EPCompanyTree) row).WorkGroupID;
      nullable = ((EPCompanyTree) row).WaitTime;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = ((EPCompanyTree) oldRow).WaitTime;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int delta = valueOrDefault1 - valueOrDefault2;
      this.updateHRecordTime(workGroupId, delta);
    }
  }

  protected virtual void EPCompanyTreeMaster_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    this.CurrentSelected.WorkGroupID = new int?();
    if (!(e.Row is EPCompanyTreeMaster row) || !((EPCompanyTree) row).WorkGroupID.HasValue)
      return;
    this.deleteHRecords(((EPCompanyTree) row).WorkGroupID);
    this.deleteRecurring(row);
  }

  public virtual void EPCompanyTreeMaster_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    EPCompanyTreeMaster row = (EPCompanyTreeMaster) e.Row;
    if (row == null)
      return;
    EPAssignmentRoute epAssignmentRoute = PXResultset<EPAssignmentRoute>.op_Implicit(PXSelectBase<EPAssignmentRoute, PXSelect<EPAssignmentRoute, Where<EPAssignmentRoute.workgroupID, Equal<Required<EPCompanyTreeMaster.workGroupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((EPCompanyTree) row).WorkGroupID
    }));
    if (epAssignmentRoute == null)
      return;
    EPAssignmentMap epAssignmentMap = PXResultset<EPAssignmentMap>.op_Implicit(PXSelectBase<EPAssignmentMap, PXSelect<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentRoute.assignmentMapID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) epAssignmentRoute.AssignmentMapID
    }));
    if (epAssignmentMap == null)
      throw new PXException("Workgroup is in use and cannot be deleted.");
    throw new PXException("The workgroup '{0}' is used in the '{1}' map '{2}'.", new object[1]
    {
      (object) epAssignmentMap.Name
    });
  }

  protected virtual void EPCompanyTreeMember_UserID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMember))
      return;
    Guid result = new Guid();
    if (!Guid.TryParse(e.NewValue.ToString(), out result))
      throw new PXSetPropertyException("User cannot be found");
  }

  private void deleteRecurring(EPCompanyTreeMaster map)
  {
    if (map == null)
      return;
    foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.workGroupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((EPCompanyTree) map).WorkGroupID
    }))
      this.deleteRecurring(PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult));
    ((PXSelectBase) this.Items).Cache.Delete((object) map);
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable updateTree(PXAdapter adapter)
  {
    foreach (PXResult<EPCompanyTreeH> pxResult in ((PXSelectBase<EPCompanyTreeH>) this.treeH).Select(Array.Empty<object>()))
      ((PXSelectBase<EPCompanyTreeH>) this.treeH).Delete(PXResult<EPCompanyTreeH>.op_Implicit(pxResult));
    this.AddRecursive(new int?(0));
    return adapter.Get();
  }

  private void AddRecursive(int? parentID)
  {
    if (!parentID.HasValue)
      return;
    foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) parentID
    }))
    {
      EPCompanyTreeMaster record = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult);
      this.insertHRecords(record);
      this.AddRecursive(((EPCompanyTree) record).WorkGroupID);
    }
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowDown")]
  public virtual IEnumerable Down(PXAdapter adapter)
  {
    int currentIndex;
    PXResultset<EPCompanyTreeMaster> pxResultset = this.SelectSiblings(this.CurrentSelected.FolderID, this.CurrentSelected.WorkGroupID, out currentIndex);
    if (currentIndex >= 0 && currentIndex < pxResultset.Count - 1)
    {
      EPCompanyTreeMaster companyTreeMaster1 = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResultset[currentIndex]);
      EPCompanyTreeMaster companyTreeMaster2 = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResultset[currentIndex + 1]);
      EPCompanyTreeMaster companyTreeMaster3 = companyTreeMaster1;
      int? sortOrder = ((EPCompanyTree) companyTreeMaster3).SortOrder;
      ((EPCompanyTree) companyTreeMaster3).SortOrder = sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + 1) : new int?();
      EPCompanyTreeMaster companyTreeMaster4 = companyTreeMaster2;
      sortOrder = ((EPCompanyTree) companyTreeMaster4).SortOrder;
      ((EPCompanyTree) companyTreeMaster4).SortOrder = sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() - 1) : new int?();
      ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Update(companyTreeMaster1);
      ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Update(companyTreeMaster2);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowUp")]
  public virtual IEnumerable Up(PXAdapter adapter)
  {
    int currentIndex;
    PXResultset<EPCompanyTreeMaster> pxResultset = this.SelectSiblings(this.CurrentSelected.FolderID, this.CurrentSelected.WorkGroupID, out currentIndex);
    if (currentIndex > 0)
    {
      EPCompanyTreeMaster companyTreeMaster1 = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResultset[currentIndex]);
      EPCompanyTreeMaster companyTreeMaster2 = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResultset[currentIndex - 1]);
      EPCompanyTreeMaster companyTreeMaster3 = companyTreeMaster1;
      int? sortOrder = ((EPCompanyTree) companyTreeMaster3).SortOrder;
      ((EPCompanyTree) companyTreeMaster3).SortOrder = sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() - 1) : new int?();
      EPCompanyTreeMaster companyTreeMaster4 = companyTreeMaster2;
      sortOrder = ((EPCompanyTree) companyTreeMaster4).SortOrder;
      ((EPCompanyTree) companyTreeMaster4).SortOrder = sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + 1) : new int?();
      ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Update(companyTreeMaster1);
      ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Update(companyTreeMaster2);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowLeft")]
  public virtual IEnumerable Left(PXAdapter adapter)
  {
    EPCompanyTreeMaster companyTreeMaster1 = PXResultset<EPCompanyTreeMaster>.op_Implicit(((PXSelectBase<EPCompanyTreeMaster>) this.Item).SelectWindowed(0, 1, new object[1]
    {
      (object) this.CurrentSelected.FolderID
    }));
    if (companyTreeMaster1 != null)
    {
      int? nullable1 = ((EPCompanyTree) companyTreeMaster1).ParentWGID;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        EPCompanyTreeMaster companyTreeMaster2 = PXResultset<EPCompanyTreeMaster>.op_Implicit(((PXSelectBase<EPCompanyTreeMaster>) this.Item).SelectWindowed(0, 1, new object[1]
        {
          (object) ((EPCompanyTree) companyTreeMaster1).ParentWGID
        }));
        if (companyTreeMaster2 != null)
        {
          int currentIndex;
          PXResultset<EPCompanyTreeMaster> pxResultset = this.SelectSiblings(((EPCompanyTree) companyTreeMaster2).ParentWGID, ((EPCompanyTree) companyTreeMaster2).WorkGroupID, out currentIndex);
          if (currentIndex >= 0)
          {
            EPCompanyTreeMaster companyTreeMaster3 = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResultset[pxResultset.Count - 1]);
            EPCompanyTreeMaster copy = (EPCompanyTreeMaster) ((PXSelectBase) this.Items).Cache.CreateCopy((object) companyTreeMaster1);
            ((EPCompanyTree) copy).ParentWGID = ((EPCompanyTree) companyTreeMaster2).ParentWGID;
            EPCompanyTreeMaster companyTreeMaster4 = copy;
            nullable1 = ((EPCompanyTree) companyTreeMaster3).SortOrder;
            int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
            ((EPCompanyTree) companyTreeMaster4).SortOrder = nullable2;
            ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Update(copy);
          }
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowRight")]
  public virtual IEnumerable Right(PXAdapter adapter)
  {
    EPCompanyTreeMaster companyTreeMaster1 = PXResultset<EPCompanyTreeMaster>.op_Implicit(((PXSelectBase<EPCompanyTreeMaster>) this.Item).SelectWindowed(0, 1, new object[1]
    {
      (object) this.CurrentSelected.FolderID
    }));
    if (companyTreeMaster1 != null)
    {
      int currentIndex;
      PXResultset<EPCompanyTreeMaster> pxResultset1 = this.SelectSiblings(((EPCompanyTree) companyTreeMaster1).ParentWGID, ((EPCompanyTree) companyTreeMaster1).WorkGroupID, out currentIndex);
      if (currentIndex > 0)
      {
        EPCompanyTreeMaster companyTreeMaster2 = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResultset1[currentIndex - 1]);
        PXResultset<EPCompanyTreeMaster> pxResultset2 = this.SelectSiblings(((EPCompanyTree) companyTreeMaster2).WorkGroupID);
        int num = 1;
        if (pxResultset2.Count > 0)
          num = ((EPCompanyTree) PXResult<EPCompanyTreeMaster>.op_Implicit(pxResultset2[pxResultset2.Count - 1])).SortOrder.GetValueOrDefault() + 1;
        EPCompanyTreeMaster copy = (EPCompanyTreeMaster) ((PXSelectBase) this.Items).Cache.CreateCopy((object) companyTreeMaster1);
        ((EPCompanyTree) copy).ParentWGID = ((EPCompanyTree) companyTreeMaster2).WorkGroupID;
        ((EPCompanyTree) copy).SortOrder = new int?(num);
        ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Update(copy);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewEmployee()
  {
    EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
    ((PXSelectBase<PX.Objects.EP.EPEmployee>) instance.Employee).Current = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(((PXSelectBase<PX.Objects.EP.EPEmployee>) instance.Employee).Search<PX.Objects.EP.EPEmployee.defContactID>((object) ((PXSelectBase<EPCompanyTreeMember>) this.Members).Current.ContactID, Array.Empty<object>()));
    throw new PXRedirectRequiredException((PXGraph) instance, nameof (ViewEmployee));
  }

  private PXResultset<EPCompanyTreeMaster> SelectSiblings(int? patentWGID)
  {
    return this.SelectSiblings(patentWGID, new int?(0), out int _);
  }

  private PXResultset<EPCompanyTreeMaster> SelectSiblings(
    int? parentWGID,
    int? workGroupID,
    out int currentIndex)
  {
    currentIndex = -1;
    if (!parentWGID.HasValue)
      return (PXResultset<EPCompanyTreeMaster>) null;
    PXResultset<EPCompanyTreeMaster> pxResultset = ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Select(new object[1]
    {
      (object) parentWGID
    });
    int num = 0;
    foreach (PXResult<EPCompanyTreeMaster> pxResult in pxResultset)
    {
      EPCompanyTreeMaster companyTreeMaster = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult);
      int? workGroupId = ((EPCompanyTree) companyTreeMaster).WorkGroupID;
      int? nullable = workGroupID;
      if (workGroupId.GetValueOrDefault() == nullable.GetValueOrDefault() & workGroupId.HasValue == nullable.HasValue)
        currentIndex = num;
      ((EPCompanyTree) companyTreeMaster).SortOrder = new int?(num + 1);
      ((PXSelectBase<EPCompanyTreeMaster>) this.Items).Update(companyTreeMaster);
      ++num;
    }
    return pxResultset;
  }

  protected virtual IEnumerable members([PXInt] int? WorkGroupID)
  {
    ImportCompanyTreeMaint companyTreeMaint1 = this;
    if (!WorkGroupID.HasValue && ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current != null)
      WorkGroupID = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).WorkGroupID;
    ((PXSelectBase) companyTreeMaint1.Members).Cache.AllowInsert = WorkGroupID.HasValue;
    companyTreeMaint1.CurrentSelected.WorkGroupID = WorkGroupID;
    ImportCompanyTreeMaint companyTreeMaint2 = companyTreeMaint1;
    object[] objArray = new object[1]
    {
      (object) WorkGroupID
    };
    foreach (PXResult<EPCompanyTreeMember, PX.Objects.EP.EPEmployee, EPEmployeePosition, Users> pxResult in PXSelectBase<EPCompanyTreeMember, PXSelectJoin<EPCompanyTreeMember, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<PX.Objects.EP.EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>, LeftJoin<Users, On<Users.pKID, Equal<PX.Objects.EP.EPEmployee.userID>>>>>, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>>>.Config>.Select((PXGraph) companyTreeMaint2, objArray))
    {
      EPCompanyTreeMember companyTreeMember = PXResult<EPCompanyTreeMember, PX.Objects.EP.EPEmployee, EPEmployeePosition, Users>.op_Implicit(pxResult);
      PX.Objects.EP.EPEmployee epEmployee = PXResult<EPCompanyTreeMember, PX.Objects.EP.EPEmployee, EPEmployeePosition, Users>.op_Implicit(pxResult);
      EPEmployeePosition employeePosition = PXResult<EPCompanyTreeMember, PX.Objects.EP.EPEmployee, EPEmployeePosition, Users>.op_Implicit(pxResult);
      Users users = PXResult<EPCompanyTreeMember, PX.Objects.EP.EPEmployee, EPEmployeePosition, Users>.op_Implicit(pxResult);
      if (string.IsNullOrEmpty(epEmployee.AcctCD))
        ((PXSelectBase) companyTreeMaint1.Members).Cache.RaiseExceptionHandling<EPCompanyTreeMember.contactID>((object) companyTreeMember, (object) null, (Exception) new PXSetPropertyException("The user '{0}' is not associated with an employee.", (PXErrorLevel) 3, new object[1]
        {
          (object) users.Username
        }));
      yield return (object) new PXResult<EPCompanyTreeMember, PX.Objects.EP.EPEmployee, EPEmployeePosition>(companyTreeMember, epEmployee, employeePosition);
    }
  }

  protected virtual void EPCompanyTreeMember_WorkGroupID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMember))
      return;
    e.NewValue = (object) this.CurrentSelected.WorkGroupID.GetValueOrDefault();
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPCompanyTreeMember_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.UpdateOwner(e.Row);
  }

  protected virtual void EPCompanyTreeMember_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.UpdateOwner(e.Row);
  }

  protected virtual void EPCompanyTreeMember_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMember row) || !row.WorkGroupID.HasValue)
      return;
    ((PXSelectBase) this.Items).Cache.IsDirty = true;
  }

  private void UpdateOwner(object row)
  {
    EPCompanyTreeMember companyTreeMember1 = (EPCompanyTreeMember) row;
    bool flag = false;
    if (companyTreeMember1.IsOwner.GetValueOrDefault())
    {
      foreach (PXResult<EPCompanyTreeMember> pxResult in ((PXSelectBase<EPCompanyTreeMember>) this.Members).Select(new object[1]
      {
        (object) companyTreeMember1.WorkGroupID
      }))
      {
        EPCompanyTreeMember companyTreeMember2 = PXResult<EPCompanyTreeMember>.op_Implicit(pxResult);
        if (companyTreeMember2.IsOwner.GetValueOrDefault())
        {
          int? contactId1 = companyTreeMember2.ContactID;
          int? contactId2 = companyTreeMember1.ContactID;
          if (!(contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue))
          {
            companyTreeMember2.IsOwner = new bool?(false);
            ((PXSelectBase) this.Members).Cache.Update((object) companyTreeMember2);
            flag = true;
          }
        }
      }
    }
    if (!flag)
      return;
    ((PXSelectBase) this.Members).View.RequestRefresh();
  }

  private void insertHRecords(EPCompanyTreeMaster record)
  {
    int num = -1;
    foreach (PXResult<EPCompanyTreeH> pxResult in ((PXSelectBase<EPCompanyTreeH>) this.parents).Select(new object[1]
    {
      (object) ((EPCompanyTree) record).ParentWGID
    }))
    {
      EPCompanyTreeH epCompanyTreeH1 = PXResult<EPCompanyTreeH>.op_Implicit(pxResult);
      int? nullable;
      if (num == -1)
      {
        nullable = epCompanyTreeH1.WorkGroupLevel;
        num = nullable.GetValueOrDefault() + 1;
      }
      EPCompanyTreeH copy = (EPCompanyTreeH) ((PXSelectBase) this.treeH).Cache.CreateCopy((object) epCompanyTreeH1);
      copy.WorkGroupID = ((EPCompanyTree) record).WorkGroupID;
      copy.WorkGroupLevel = new int?(num);
      EPCompanyTreeH epCompanyTreeH2 = copy;
      nullable = epCompanyTreeH2.WaitTime;
      int? waitTime = ((EPCompanyTree) record).WaitTime;
      epCompanyTreeH2.WaitTime = nullable.HasValue & waitTime.HasValue ? new int?(nullable.GetValueOrDefault() + waitTime.GetValueOrDefault()) : new int?();
      ((PXSelectBase) this.treeH).Cache.Insert((object) copy);
    }
    ((PXSelectBase) this.treeH).Cache.Insert((object) new EPCompanyTreeH()
    {
      WorkGroupID = ((EPCompanyTree) record).WorkGroupID,
      WorkGroupLevel = new int?(num),
      ParentWGID = ((EPCompanyTree) record).WorkGroupID,
      ParentWGLevel = new int?(num),
      WaitTime = new int?(0)
    });
  }

  private void deleteHRecords(int? workGroupID)
  {
    foreach (PXResult<EPCompanyTreeH> pxResult in ((PXSelectBase<EPCompanyTreeH>) this.parents).Select(new object[1]
    {
      (object) workGroupID
    }))
      ((PXSelectBase) this.treeH).Cache.Delete((object) PXResult<EPCompanyTreeH>.op_Implicit(pxResult));
  }

  private void updateHRecordTime(int? workGroupID, int delta)
  {
    foreach (PXResult<EPCompanyTreeH> pxResult1 in ((PXSelectBase<EPCompanyTreeH>) this.childrens).Select(new object[1]
    {
      (object) workGroupID
    }))
    {
      EPCompanyTreeH epCompanyTreeH1 = PXResult<EPCompanyTreeH>.op_Implicit(pxResult1);
      foreach (PXResult<EPCompanyTreeH> pxResult2 in ((PXSelectBase<EPCompanyTreeH>) this.parents).Select(new object[1]
      {
        (object) epCompanyTreeH1.WorkGroupID
      }))
      {
        EPCompanyTreeH epCompanyTreeH2 = PXResult<EPCompanyTreeH>.op_Implicit(pxResult2);
        int? workGroupId = epCompanyTreeH2.WorkGroupID;
        int? nullable1 = epCompanyTreeH2.ParentWGID;
        if (!(workGroupId.GetValueOrDefault() == nullable1.GetValueOrDefault() & workGroupId.HasValue == nullable1.HasValue))
        {
          nullable1 = epCompanyTreeH2.ParentWGLevel;
          int? parentWgLevel = epCompanyTreeH1.ParentWGLevel;
          if (nullable1.GetValueOrDefault() < parentWgLevel.GetValueOrDefault() & nullable1.HasValue & parentWgLevel.HasValue)
          {
            EPCompanyTreeH epCompanyTreeH3 = epCompanyTreeH2;
            int? waitTime = epCompanyTreeH3.WaitTime;
            int num = delta;
            int? nullable2;
            if (!waitTime.HasValue)
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = new int?(waitTime.GetValueOrDefault() + num);
            epCompanyTreeH3.WaitTime = nullable2;
            ((PXSelectBase) this.treeH).Cache.Update((object) epCompanyTreeH2);
          }
        }
      }
    }
  }

  private void updateHRecordParent(EPCompanyTreeMaster item)
  {
    PXResultset<EPCompanyTreeH> pxResultset = ((PXSelectBase<EPCompanyTreeH>) this.parents).Select(new object[1]
    {
      (object) ((EPCompanyTree) item).ParentWGID
    });
    int num1 = -1;
    if (pxResultset.Count > 0)
      num1 = PXResult<EPCompanyTreeH>.op_Implicit(pxResultset[pxResultset.Count - 1]).WorkGroupLevel.GetValueOrDefault();
    int num2 = 0;
    EPCompanyTreeH epCompanyTreeH1 = (EPCompanyTreeH) null;
    foreach (PXResult<EPCompanyTreeH> pxResult1 in ((PXSelectBase<EPCompanyTreeH>) this.childrens).Select(new object[1]
    {
      (object) ((EPCompanyTree) item).WorkGroupID
    }))
    {
      EPCompanyTreeH epCompanyTreeH2 = PXResult<EPCompanyTreeH>.op_Implicit(pxResult1);
      int? nullable1;
      if (epCompanyTreeH1 == null)
      {
        epCompanyTreeH1 = (EPCompanyTreeH) ((PXSelectBase) this.treeH).Cache.CreateCopy((object) epCompanyTreeH2);
        int num3 = num1 + 1;
        nullable1 = epCompanyTreeH2.WorkGroupLevel;
        int valueOrDefault = nullable1.GetValueOrDefault();
        num2 = num3 - valueOrDefault;
      }
      foreach (PXResult<EPCompanyTreeH> pxResult2 in ((PXSelectBase<EPCompanyTreeH>) this.parents).Select(new object[1]
      {
        (object) epCompanyTreeH2.WorkGroupID
      }))
      {
        EPCompanyTreeH epCompanyTreeH3 = PXResult<EPCompanyTreeH>.op_Implicit(pxResult2);
        nullable1 = epCompanyTreeH3.WorkGroupID;
        int? nullable2 = epCompanyTreeH3.ParentWGID;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          nullable2 = epCompanyTreeH3.ParentWGLevel;
          nullable1 = epCompanyTreeH1.WorkGroupLevel;
          if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
          {
            ((PXSelectBase) this.treeH).Cache.Delete((object) epCompanyTreeH3);
            continue;
          }
        }
        EPCompanyTreeH copy = (EPCompanyTreeH) ((PXSelectBase) this.treeH).Cache.CreateCopy((object) epCompanyTreeH3);
        EPCompanyTreeH epCompanyTreeH4 = copy;
        nullable1 = epCompanyTreeH4.WorkGroupLevel;
        int num4 = num2;
        int? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new int?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new int?(nullable1.GetValueOrDefault() + num4);
        epCompanyTreeH4.WorkGroupLevel = nullable3;
        EPCompanyTreeH epCompanyTreeH5 = copy;
        nullable1 = epCompanyTreeH5.ParentWGLevel;
        int num5 = num2;
        int? nullable4;
        if (!nullable1.HasValue)
        {
          nullable2 = new int?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new int?(nullable1.GetValueOrDefault() + num5);
        epCompanyTreeH5.ParentWGLevel = nullable4;
        ((PXSelectBase) this.treeH).Cache.Update((object) copy);
      }
      foreach (PXResult<EPCompanyTreeH> pxResult3 in pxResultset)
      {
        EPCompanyTreeH copy = (EPCompanyTreeH) ((PXSelectBase) this.treeH).Cache.CreateCopy((object) PXResult<EPCompanyTreeH>.op_Implicit(pxResult3));
        copy.WorkGroupID = epCompanyTreeH2.WorkGroupID;
        copy.WorkGroupLevel = epCompanyTreeH2.WorkGroupLevel;
        EPCompanyTreeH epCompanyTreeH6 = copy;
        int? waitTime1 = epCompanyTreeH6.WaitTime;
        int? waitTime2 = epCompanyTreeH2.WaitTime;
        int? nullable5 = ((EPCompanyTree) item).WaitTime;
        int? nullable6 = waitTime2.HasValue & nullable5.HasValue ? new int?(waitTime2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new int?();
        int? nullable7;
        if (!(waitTime1.HasValue & nullable6.HasValue))
        {
          nullable5 = new int?();
          nullable7 = nullable5;
        }
        else
          nullable7 = new int?(waitTime1.GetValueOrDefault() + nullable6.GetValueOrDefault());
        epCompanyTreeH6.WaitTime = nullable7;
        ((PXSelectBase) this.treeH).Cache.Insert((object) copy);
      }
    }
  }

  private ImportCompanyTreeMaint.SelectedNode CurrentSelected
  {
    get
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (ImportCompanyTreeMaint.SelectedNode)];
      if (cach.Current == null)
      {
        cach.Insert();
        cach.IsDirty = false;
      }
      return (ImportCompanyTreeMaint.SelectedNode) cach.Current;
    }
  }

  [Serializable]
  public class SelectedNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _FolderID;
    protected int? _WorkGroupID;

    [PXDBInt(IsKey = true)]
    public virtual int? FolderID
    {
      get => this._FolderID;
      set => this._FolderID = value;
    }

    [PXDBInt(IsKey = true)]
    public virtual int? WorkGroupID
    {
      get => this._WorkGroupID;
      set => this._WorkGroupID = value;
    }

    public abstract class folderID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ImportCompanyTreeMaint.SelectedNode.folderID>
    {
    }

    public abstract class workGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ImportCompanyTreeMaint.SelectedNode.workGroupID>
    {
    }
  }
}
