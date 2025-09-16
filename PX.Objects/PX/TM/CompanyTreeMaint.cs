// Decompiled with JetBrains decompiler
// Type: PX.TM.CompanyTreeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections;
using System.ComponentModel;
using System.Text;

#nullable enable
namespace PX.TM;

[Serializable]
public class CompanyTreeMaint : PXGraph<
#nullable disable
CompanyTreeMaint>
{
  private readonly EntityHelper _entityHelper;
  public PXFilter<CompanyTreeMaint.SelectedNode> SelectedFolders;
  public PXSelectOrderBy<EPCompanyTreeMaster, OrderBy<Asc<EPCompanyTreeMaster.sortOrder>>> Folders;
  public PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.workGroupID, Equal<Current<EPCompanyTreeMaster.workGroupID>>>> CurrentWorkGroup;
  public PXSelectJoin<EPCompanyTreeMember, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<PX.Objects.EP.EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>, Where<EPCompanyTreeMember.workGroupID, Equal<Current<EPCompanyTreeMaster.workGroupID>>>, OrderBy<Asc<PX.Objects.EP.EPEmployee.acctCD>>> Members;
  public PXSelectOrderBy<EPCompanyTreeMaster, OrderBy<Asc<EPCompanyTreeMaster.sortOrder>>> ParentFolders;
  public PXFilter<CompanyTreeMaint.SelectedParentNode> SelectedParentFolders;
  public PXSave<CompanyTreeMaint.SelectedNode> Save;
  public PXCancel<CompanyTreeMaint.SelectedNode> Cancel;
  public PXAction<CompanyTreeMaint.SelectedNode> AddWorkGroup;
  public PXAction<CompanyTreeMaint.SelectedNode> DeleteWorkGroup;
  public PXAction<CompanyTreeMaint.SelectedNode> down;
  public PXAction<CompanyTreeMaint.SelectedNode> up;
  public PXAction<CompanyTreeMaint.SelectedNode> viewEmployee;
  public PXAction<CompanyTreeMaint.SelectedNode> MoveWorkGroup;
  public PXSelect<EPCompanyTreeH> treeH;
  public PXSelect<EPCompanyTreeH, Where<EPCompanyTreeH.workGroupID, Equal<Required<EPCompanyTreeH.workGroupID>>>, OrderBy<Desc<EPCompanyTreeH.parentWGLevel>>> parents;
  public PXSelect<EPCompanyTreeH, Where<EPCompanyTreeH.parentWGID, Equal<Required<EPCompanyTreeH.parentWGID>>>, OrderBy<Asc<EPCompanyTreeH.workGroupLevel>>> childrens;

  public CompanyTreeMaint()
  {
    this._entityHelper = new EntityHelper((PXGraph) this);
    PXUIFieldAttribute.SetEnabled<PX.Objects.EP.EPEmployee.acctCD>(((PXGraph) this).Caches[typeof (PX.Objects.EP.EPEmployee)], (object) null, false);
    PXUIFieldAttribute.SetDisplayName<EPCompanyTree.parentWGID>(((PXGraph) this).Caches[typeof (EPCompanyTree)], "Move to");
    PXUIFieldAttribute.SetDisplayName<EPCompanyTreeMember.contactID>(((PXGraph) this).Caches[typeof (EPCompanyTreeMember)], "Employee Name");
    PXUIFieldAttribute.SetDisplayName<EPEmployeePosition.positionID>(((PXGraph) this).Caches[typeof (EPEmployeePosition)], "Job Title");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.fullName>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], PXAccess.FeatureInstalled<FeaturesSet.branch>() ? "Branch" : "Company");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.displayName>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], "Employee Name");
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.EP.EPEmployee.acctName> e)
  {
  }

  protected virtual IEnumerable folders([PXInt] int? workGroupID)
  {
    CompanyTreeMaint companyTreeMaint1 = this;
    if (!workGroupID.HasValue)
    {
      EPCompanyTreeMaster companyTreeMaster = new EPCompanyTreeMaster();
      ((EPCompanyTree) companyTreeMaster).WorkGroupID = new int?(0);
      ((EPCompanyTree) companyTreeMaster).Description = PXSiteMap.RootNode.Title;
      yield return (object) companyTreeMaster;
    }
    else
    {
      CompanyTreeMaint companyTreeMaint2 = companyTreeMaint1;
      object[] objArray = new object[1]
      {
        (object) workGroupID
      };
      foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>, OrderBy<Asc<EPCompanyTreeMaster.sortOrder>>>.Config>.Select((PXGraph) companyTreeMaint2, objArray))
      {
        EPCompanyTreeMaster companyTreeMaster = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(((EPCompanyTree) companyTreeMaster).Description))
          yield return (object) companyTreeMaster;
      }
    }
  }

  protected virtual IEnumerable currentWorkGroup()
  {
    CompanyTreeMaint companyTreeMaint1 = this;
    if (((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current != null)
    {
      PXUIFieldAttribute.SetEnabled<EPCompanyTreeMaster.description>(((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMaster)], (object) null, ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID.HasValue);
      PXUIFieldAttribute.SetEnabled<EPCompanyTreeMaster.parentWGID>(((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMaster)], (object) null, ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID.HasValue);
      ((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMaster)].AllowInsert = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID.HasValue;
      ((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMaster)].AllowDelete = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID.HasValue;
      ((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMaster)].AllowUpdate = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID.HasValue;
      PXAction action1 = ((PXGraph) companyTreeMaint1).Actions["MoveWorkGroup"];
      int? parentWgid = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID;
      int num1 = parentWgid.HasValue ? 1 : 0;
      action1.SetEnabled(num1 != 0);
      PXAction action2 = ((PXGraph) companyTreeMaint1).Actions["Up"];
      parentWgid = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID;
      int num2 = parentWgid.HasValue ? 1 : 0;
      action2.SetEnabled(num2 != 0);
      PXAction action3 = ((PXGraph) companyTreeMaint1).Actions["Down"];
      parentWgid = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID;
      int num3 = parentWgid.HasValue ? 1 : 0;
      action3.SetEnabled(num3 != 0);
      PXAction action4 = ((PXGraph) companyTreeMaint1).Actions["DeleteWorkGroup"];
      parentWgid = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID;
      int num4 = parentWgid.HasValue ? 1 : 0;
      action4.SetEnabled(num4 != 0);
      CompanyTreeMaint companyTreeMaint2 = companyTreeMaint1;
      object[] objArray = new object[1]
      {
        (object) ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).WorkGroupID
      };
      foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.workGroupID, Equal<Required<EPCompanyTreeMaster.workGroupID>>>>.Config>.Select((PXGraph) companyTreeMaint2, objArray))
        yield return (object) PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult);
    }
  }

  protected virtual IEnumerable members()
  {
    CompanyTreeMaint companyTreeMaint1 = this;
    PXCache cach1 = ((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMember)];
    int? nullable;
    int num1;
    if (((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current != null)
    {
      nullable = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID;
      num1 = nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    cach1.AllowInsert = num1 != 0;
    PXCache cach2 = ((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMember)];
    int num2;
    if (((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current != null)
    {
      nullable = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID;
      num2 = nullable.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    cach2.AllowDelete = num2 != 0;
    PXCache cach3 = ((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMember)];
    int num3;
    if (((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current != null)
    {
      nullable = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).ParentWGID;
      num3 = nullable.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    cach3.AllowUpdate = num3 != 0;
    if (((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current != null)
    {
      CompanyTreeMaint companyTreeMaint2 = companyTreeMaint1;
      object[] objArray = new object[1]
      {
        (object) ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) companyTreeMaint1.Folders).Current).WorkGroupID
      };
      foreach (PXResult<EPCompanyTreeMember> pxResult in PXSelectBase<EPCompanyTreeMember, PXSelectJoin<EPCompanyTreeMember, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<PX.Objects.EP.EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>, LeftJoin<Users, On<Users.pKID, Equal<PX.Objects.EP.EPEmployee.userID>>>>>, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>>>.Config>.Select((PXGraph) companyTreeMaint2, objArray))
      {
        EPCompanyTreeMember companyTreeMember = ((PXResult) pxResult)[typeof (EPCompanyTreeMember)] as EPCompanyTreeMember;
        PX.Objects.EP.EPEmployee epEmployee = ((PXResult) pxResult)[typeof (PX.Objects.EP.EPEmployee)] as PX.Objects.EP.EPEmployee;
        Users users = ((PXResult) pxResult)[typeof (Users)] as Users;
        EPEmployeePosition employeePosition = ((PXResult) pxResult)[typeof (EPEmployeePosition)] as EPEmployeePosition;
        nullable = companyTreeMember.ContactID;
        if (nullable.HasValue)
        {
          if (users == null)
            ((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMember)].RaiseExceptionHandling<EPCompanyTreeMember.contactID>((object) companyTreeMember, (object) null, (Exception) new PXSetPropertyException("The record has been deleted", (PXErrorLevel) 2));
          else if (users.State != "A" && users.State != "O")
            ((PXGraph) companyTreeMaint1).Caches[typeof (EPCompanyTreeMember)].RaiseExceptionHandling<EPCompanyTreeMember.contactID>((object) companyTreeMember, (object) null, (Exception) new PXSetPropertyException("The user is not active.", (PXErrorLevel) 2));
          if (epEmployee == null)
            ((PXGraph) companyTreeMaint1).Caches[typeof (PX.Objects.EP.EPEmployee)].RaiseExceptionHandling<PX.Objects.EP.EPEmployee.acctCD>((object) epEmployee, (object) null, (Exception) new PXSetPropertyException("The record has been deleted.", (PXErrorLevel) 2));
          else if (epEmployee.VStatus == null)
            ((PXGraph) companyTreeMaint1).Caches[typeof (PX.Objects.EP.EPEmployee)].RaiseExceptionHandling<PX.Objects.EP.EPEmployee.acctCD>((object) epEmployee, (object) null, (Exception) new PXSetPropertyException("The user is not associated with an employee record.", (PXErrorLevel) 2));
          else if (epEmployee.VStatus != "A")
            ((PXGraph) companyTreeMaint1).Caches[typeof (PX.Objects.EP.EPEmployee)].RaiseExceptionHandling<PX.Objects.EP.EPEmployee.acctCD>((object) epEmployee, (object) null, (Exception) new PXSetPropertyException("The employee is not active.", (PXErrorLevel) 2));
        }
        yield return (object) new PXResult<EPCompanyTreeMember, PX.Objects.EP.EPEmployee, EPEmployeePosition>(companyTreeMember, epEmployee, employeePosition);
      }
    }
  }

  protected virtual IEnumerable parentFolders([PXInt] int? workGroupID)
  {
    CompanyTreeMaint companyTreeMaint1 = this;
    if (!workGroupID.HasValue)
    {
      EPCompanyTreeMaster companyTreeMaster = new EPCompanyTreeMaster();
      ((EPCompanyTree) companyTreeMaster).WorkGroupID = new int?(0);
      ((EPCompanyTree) companyTreeMaster).Description = PXSiteMap.RootNode.Title;
      yield return (object) companyTreeMaster;
    }
    else
    {
      CompanyTreeMaint companyTreeMaint2 = companyTreeMaint1;
      object[] objArray = new object[1]
      {
        (object) workGroupID
      };
      foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>, OrderBy<Asc<EPCompanyTreeMaster.sortOrder>>>.Config>.Select((PXGraph) companyTreeMaint2, objArray))
      {
        EPCompanyTreeMaster companyTreeMaster = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(((EPCompanyTree) companyTreeMaster).Description))
        {
          int? workGroupId1 = ((EPCompanyTree) companyTreeMaster).WorkGroupID;
          int? workGroupId2 = ((PXSelectBase<CompanyTreeMaint.SelectedNode>) companyTreeMaint1.SelectedFolders).Current.WorkGroupID;
          if (!(workGroupId1.GetValueOrDefault() == workGroupId2.GetValueOrDefault() & workGroupId1.HasValue == workGroupId2.HasValue))
            yield return (object) companyTreeMaster;
        }
      }
    }
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew")]
  public virtual IEnumerable addWorkGroup(PXAdapter adapter)
  {
    EPCompanyTreeMaster companyTreeMaster1 = ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current ?? PXResultset<EPCompanyTreeMaster>.op_Implicit(((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Select(Array.Empty<object>()));
    if (companyTreeMaster1 != null)
    {
      int? nullable = ((EPCompanyTree) companyTreeMaster1).WorkGroupID;
      int num1 = nullable.Value;
      EPCompanyTreeMaster instance = (EPCompanyTreeMaster) ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].CreateInstance();
      ((EPCompanyTree) instance).Description = "<NEW>";
      ((EPCompanyTree) instance).ParentWGID = new int?(num1);
      if (((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Insert((object) instance) is EPCompanyTreeMaster companyTreeMaster3)
      {
        companyTreeMaster3.TempChildID = ((EPCompanyTree) companyTreeMaster3).WorkGroupID;
        companyTreeMaster3.TempParentID = new int?(num1);
        EPCompanyTreeMaster companyTreeMaster2 = PXResultset<EPCompanyTreeMaster>.op_Implicit(PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>, OrderBy<Desc<EPCompanyTreeMaster.sortOrder>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) num1
        }));
        nullable = ((EPCompanyTree) companyTreeMaster2).SortOrder;
        int num2 = nullable.Value + 1;
        ((EPCompanyTree) companyTreeMaster3).SortOrder = new int?(companyTreeMaster2 != null ? num2 : 1);
        ((PXSelectBase) this.Folders).Cache.ActiveRow = (IBqlTable) companyTreeMaster3;
        ((PXSelectBase) this.Folders).View.RequestRefresh();
        PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>, OrderBy<Desc<EPCompanyTreeMaster.sortOrder>>>.Config>.Clear((PXGraph) this);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "RecordDel")]
  public virtual IEnumerable deleteWorkGroup(PXAdapter adapter)
  {
    if (((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current != null)
    {
      this.VerifyRecurringBeforeDelete(((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current);
      ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Delete((object) ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowDown")]
  public virtual IEnumerable Down(PXAdapter adapter)
  {
    EPCompanyTreeMaster current = ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current;
    EPCompanyTreeMaster companyTreeMaster = PXResultset<EPCompanyTreeMaster>.op_Implicit(PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>, And<EPCompanyTreeMaster.sortOrder, Greater<Required<EPCompanyTreeMaster.parentWGID>>>>, OrderBy<Asc<EPCompanyTreeMaster.sortOrder>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current).ParentWGID,
      (object) ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current).SortOrder
    }));
    if (companyTreeMaster != null && current != null)
    {
      int num = ((EPCompanyTree) current).SortOrder.Value;
      ((EPCompanyTree) current).SortOrder = ((EPCompanyTree) companyTreeMaster).SortOrder;
      ((EPCompanyTree) companyTreeMaster).SortOrder = new int?(num);
      ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Update((object) companyTreeMaster);
      ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Update((object) current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowUp")]
  public virtual IEnumerable Up(PXAdapter adapter)
  {
    EPCompanyTreeMaster current = ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current;
    EPCompanyTreeMaster companyTreeMaster = PXResultset<EPCompanyTreeMaster>.op_Implicit(PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>, And<EPCompanyTreeMaster.sortOrder, Less<Required<EPCompanyTreeMaster.parentWGID>>>>, OrderBy<Desc<EPCompanyTreeMaster.sortOrder>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current).ParentWGID,
      (object) ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current).SortOrder
    }));
    if (companyTreeMaster != null && current != null)
    {
      int num = ((EPCompanyTree) current).SortOrder.Value;
      ((EPCompanyTree) current).SortOrder = ((EPCompanyTree) companyTreeMaster).SortOrder;
      ((EPCompanyTree) companyTreeMaster).SortOrder = new int?(num);
      ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Update((object) companyTreeMaster);
      ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Update((object) current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewEmployee()
  {
    if (!((PXSelectBase<EPCompanyTreeMember>) this.Members).Current.ContactID.HasValue)
      return;
    EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
    ((PXSelectBase<PX.Objects.EP.EPEmployee>) instance.Employee).Current = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(((PXSelectBase<PX.Objects.EP.EPEmployee>) instance.Employee).Search<PX.Objects.EP.EPEmployee.defContactID>((object) ((PXSelectBase<EPCompanyTreeMember>) this.Members).Current.ContactID, Array.Empty<object>()));
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable moveWorkGroup(PXAdapter adapter)
  {
    EPCompanyTreeMaster current = ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Current as EPCompanyTreeMaster;
    ((PXSelectBase<CompanyTreeMaint.SelectedNode>) this.SelectedFolders).Current.WorkGroupID = ((EPCompanyTree) ((PXSelectBase<EPCompanyTreeMaster>) this.Folders).Current).WorkGroupID;
    if (current != null)
    {
      if (((PXSelectBase) this.SelectedParentFolders).View.Answer == null)
        ((PXSelectBase<CompanyTreeMaint.SelectedParentNode>) this.SelectedParentFolders).Current.WorkGroupID = ((EPCompanyTree) current).ParentWGID;
      if (((PXSelectBase<CompanyTreeMaint.SelectedParentNode>) this.SelectedParentFolders).AskExt() == 1)
      {
        ((EPCompanyTree) current).ParentWGID = ((PXSelectBase<CompanyTreeMaint.SelectedParentNode>) this.SelectedParentFolders).Current.WorkGroupID;
        current.TempParentID = ((PXSelectBase<CompanyTreeMaint.SelectedParentNode>) this.SelectedParentFolders).Current.WorkGroupID;
        EPCompanyTreeMaster companyTreeMaster1 = PXResultset<EPCompanyTreeMaster>.op_Implicit(PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>, OrderBy<Desc<EPCompanyTreeMaster.sortOrder>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) ((PXSelectBase<CompanyTreeMaint.SelectedParentNode>) this.SelectedParentFolders).Current.WorkGroupID
        }));
        if (companyTreeMaster1 != null)
        {
          int num = ((EPCompanyTree) companyTreeMaster1).SortOrder.Value;
          ((EPCompanyTree) current).SortOrder = new int?(num + 1);
        }
        else
          ((EPCompanyTree) current).SortOrder = new int?(1);
        EPCompanyTreeMaster companyTreeMaster2 = ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Update((object) current) as EPCompanyTreeMaster;
        PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>, OrderBy<Asc<EPCompanyTreeMaster.sortOrder>>>.Config>.Clear((PXGraph) this);
        ((PXSelectBase) this.Folders).Cache.ActiveRow = (IBqlTable) companyTreeMaster2;
        ((PXSelectBase) this.Folders).View.RequestRefresh();
      }
    }
    return adapter.Get();
  }

  protected virtual void EPCompanyTreeMaster_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMaster row))
      return;
    this.insertHRecords(row);
  }

  protected virtual void EPCompanyTreeMaster_Description_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!string.IsNullOrEmpty((string) e.NewValue))
      return;
    e.NewValue = (object) "<No Name>";
  }

  public virtual void _(PX.Data.Events.RowPersisting<EPCompanyTreeMaster> e)
  {
    EPCompanyTreeMaster row = e.Row;
    if (row == null || (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<EPCompanyTreeMaster>>) e).Cache.GetValueExt<EPCompanyTreeMaster.description>((object) row) as PXFieldState).ErrorLevel == 4)
      return;
    if (((EPCompanyTree) row).Description == "<NEW>" || ((EPCompanyTree) row).Description == "<No Name>")
      throw new PXSetPropertyException("Please specify workgroup description first.", new object[1]
      {
        (object) "Description"
      });
    foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXViewOf<EPCompanyTreeMaster>.BasedOn<SelectFromBase<EPCompanyTreeMaster, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPCompanyTreeMaster.description, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((EPCompanyTree) row).Description
    }))
    {
      EPCompanyTreeMaster companyTreeMaster = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult);
      if (companyTreeMaster != null)
      {
        int? workGroupId1 = ((EPCompanyTree) companyTreeMaster).WorkGroupID;
        int? workGroupId2 = ((EPCompanyTree) row).WorkGroupID;
        if (!(workGroupId1.GetValueOrDefault() == workGroupId2.GetValueOrDefault() & workGroupId1.HasValue == workGroupId2.HasValue) && ((EPCompanyTree) companyTreeMaster).Description.ToLower() == ((EPCompanyTree) row).Description)
          throw new PXSetPropertyException("An attempt was made to add a duplicate entry.", new object[1]
          {
            (object) "Description"
          });
      }
    }
  }

  protected virtual void EPCompanyTreeMaster_Description_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMaster row))
      return;
    foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.description, Equal<Required<EPCompanyTreeMaster.description>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }))
    {
      EPCompanyTreeMaster companyTreeMaster = PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult);
      if (companyTreeMaster != null && ((EPCompanyTree) companyTreeMaster).Description == e.NewValue.ToString() && ((PXGraph) this).IsImport)
      {
        sender.SetStatus((object) row, (PXEntryStatus) 5);
        this.deleteHRecords(((EPCompanyTree) row).WorkGroupID);
      }
    }
  }

  protected virtual void EPCompanyTreeMaster_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is EPCompanyTreeMaster row) || !((EPCompanyTree) row).WorkGroupID.HasValue)
      return;
    this.deleteHRecords(((EPCompanyTree) row).WorkGroupID);
    this.deleteRecurring(row);
  }

  protected virtual void EPCompanyTreeMaster_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    EPCompanyTreeMaster row = e.Row as EPCompanyTreeMaster;
    EPCompanyTreeMaster oldRow = e.OldRow as EPCompanyTreeMaster;
    if (row == null || oldRow == null)
      return;
    int? parentWgid1 = ((EPCompanyTree) row).ParentWGID;
    int? parentWgid2 = ((EPCompanyTree) oldRow).ParentWGID;
    if (parentWgid1.GetValueOrDefault() == parentWgid2.GetValueOrDefault() & parentWgid1.HasValue == parentWgid2.HasValue)
      return;
    this.updateHRecordParent(row);
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    foreach (EPCompanyTreeMaster companyTreeMaster1 in ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Cached)
    {
      int? nullable = companyTreeMaster1.TempParentID;
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      {
        foreach (EPCompanyTreeMaster companyTreeMaster2 in ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Cached)
        {
          nullable = companyTreeMaster2.TempChildID;
          int? tempParentId = companyTreeMaster1.TempParentID;
          if (nullable.GetValueOrDefault() == tempParentId.GetValueOrDefault() & nullable.HasValue == tempParentId.HasValue)
          {
            ((EPCompanyTree) companyTreeMaster1).ParentWGID = ((EPCompanyTree) companyTreeMaster2).WorkGroupID;
            companyTreeMaster1.TempParentID = ((EPCompanyTree) companyTreeMaster2).WorkGroupID;
            ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].SetStatus((object) companyTreeMaster1, (PXEntryStatus) 1);
          }
        }
      }
    }
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.Members).View.RequestRefresh();
  }

  public virtual void EPCompanyTreeMaster_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    EPCompanyTreeMaster row = (EPCompanyTreeMaster) e.Row;
    if (row == null)
      return;
    EPWeeklyCrewTimeActivity crewTimeActivity = PXResultset<EPWeeklyCrewTimeActivity>.op_Implicit(PXSelectBase<EPWeeklyCrewTimeActivity, PXSelect<EPWeeklyCrewTimeActivity, Where<EPWeeklyCrewTimeActivity.workgroupID, Equal<Required<EPCompanyTreeMaster.workGroupID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((EPCompanyTree) row).WorkGroupID
    }));
    if (crewTimeActivity != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      object[] objArray = new object[2]
      {
        (object) ((EPCompanyTree) row).Description,
        null
      };
      int? week = crewTimeActivity.Week;
      ref int? local = ref week;
      objArray[1] = (object) (local.HasValue ? local.GetValueOrDefault().ToInvariantString() : (string) null);
      throw new PXException("The {0} workgroup cannot be deleted because it has been used in at least one weekly crew time entry: {1}", objArray);
    }
  }

  protected virtual void EPCompanyTreeMember_Active_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    EPCompanyTreeMember row = (EPCompanyTreeMember) e.Row;
    if (row == null)
      return;
    string str1 = "";
    foreach (PXResult<EPAssignmentMap> pxResult in PXSelectBase<EPAssignmentMap, PXSelectJoin<EPAssignmentMap, InnerJoin<EPAssignmentRoute, On<EPAssignmentRoute.assignmentMapID, Equal<EPAssignmentMap.assignmentMapID>>>, Where<EPAssignmentRoute.ownerID, Equal<Required<EPAssignmentRoute.ownerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContactID
    }))
    {
      EPAssignmentMap epAssignmentMap = PXResult<EPAssignmentMap>.op_Implicit(pxResult);
      if (epAssignmentMap == null)
        throw new PXException("Workgroup is in use and cannot be deleted.");
      string str2 = epAssignmentMap.MapType.GetValueOrDefault() != 1 ? "Approval" : "Assignment";
      PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<PX.Objects.EP.EPEmployee.defContactID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.ContactID
      }));
      if (epEmployee != null)
        str1 = $"{str1}{$"Employee '{epEmployee.AcctName}' and this workgroup are used in the '{str2}' map '{epAssignmentMap.Name}'."}\n";
    }
    if (string.IsNullOrEmpty(str1))
      return;
    sender.RaiseExceptionHandling<EPCompanyTreeMember.contactID>((object) row, (object) row.ContactID, (Exception) new PXSetPropertyException(str1 + "\nDeactivation of this employee may cause delays", (PXErrorLevel) 3));
  }

  protected virtual void EPCompanyTreeMember_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.UpdateOwner(e.Row);
  }

  protected virtual void EPCompanyTreeMember_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.UpdateOwner(e.Row);
  }

  private void UpdateOwner(object row)
  {
    EPCompanyTreeMember companyTreeMember1 = (EPCompanyTreeMember) row;
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
          }
        }
      }
    }
    ((PXSelectBase) this.Members).View.RequestRefresh();
  }

  private void VerifyRecurringBeforeDelete(EPCompanyTreeMaster map)
  {
    if (map != null)
    {
      foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((EPCompanyTree) map).WorkGroupID
      }))
        this.VerifyRecurringBeforeDelete(PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult));
    }
    this.VerifyForUsing(map);
  }

  private void VerifyForUsing(EPCompanyTreeMaster row)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string str1 = PXMessages.LocalizeNoPrefix("Assignment");
    string str2 = PXMessages.LocalizeNoPrefix("Approval");
    foreach (PXResult<EPAssignmentMap> pxResult in PXSelectBase<EPAssignmentMap, PXSelectJoin<EPAssignmentMap, InnerJoin<EPRule, On<EPRule.assignmentMapID, Equal<EPAssignmentMap.assignmentMapID>>>, Where<EPRule.workgroupID, Equal<Required<EPRule.workgroupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((EPCompanyTree) row).WorkGroupID
    }))
    {
      EPAssignmentMap epAssignmentMap = PXResult<EPAssignmentMap>.op_Implicit(pxResult);
      if (epAssignmentMap == null)
        throw new PXException("Workgroup is in use and cannot be deleted.");
      string str3 = epAssignmentMap.MapType.GetValueOrDefault() == 1 ? str1 : str2;
      stringBuilder.AppendLine(PXMessages.LocalizeFormatNoPrefix("The workgroup '{0}' is used in the '{1}' map '{2}'.", new object[3]
      {
        (object) ((EPCompanyTree) row).Description,
        (object) str3,
        (object) epAssignmentMap.Name
      }));
    }
    string str4 = stringBuilder.ToString();
    if (!string.IsNullOrWhiteSpace(str4))
      throw new PXException(str4);
    stringBuilder.Clear();
    foreach (PXResult<EPApproval> pxResult in PXSelectBase<EPApproval, PXViewOf<EPApproval>.BasedOn<SelectFromBase<EPApproval, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPApproval.workgroupID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((EPCompanyTree) row).WorkGroupID
    }))
    {
      EPApproval epApproval = PXResult<EPApproval>.op_Implicit(pxResult);
      stringBuilder.AppendLine(PXMessages.LocalizeFormatNoPrefix("The {0} workgroup is assigned to approve a record of the {1} type. {1} reference number: {2}. {1} description: {3}.", new object[4]
      {
        (object) ((EPCompanyTree) row).Description,
        (object) this._entityHelper.GetFriendlyEntityName(epApproval.RefNoteID),
        (object) this._entityHelper.GetEntityRowID(epApproval.RefNoteID, ", "),
        (object) epApproval.Descr
      }));
    }
    string str5 = stringBuilder.ToString();
    if (!string.IsNullOrWhiteSpace(str5))
      throw new PXException(str5);
  }

  private void deleteRecurring(EPCompanyTreeMaster map)
  {
    if (map == null)
      return;
    foreach (PXResult<EPCompanyTreeMaster> pxResult in PXSelectBase<EPCompanyTreeMaster, PXSelect<EPCompanyTreeMaster, Where<EPCompanyTreeMaster.parentWGID, Equal<Required<EPCompanyTreeMaster.parentWGID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((EPCompanyTree) map).WorkGroupID
    }))
      this.deleteRecurring(PXResult<EPCompanyTreeMaster>.op_Implicit(pxResult));
    ((PXGraph) this).Caches[typeof (EPCompanyTreeMaster)].Delete((object) map);
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

  [PXHidden]
  [Serializable]
  public class SelectedNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _WorkGroupID;

    [PXInt]
    [PXUIField(Visible = false)]
    public virtual int? WorkGroupID
    {
      get => this._WorkGroupID;
      set => this._WorkGroupID = value;
    }

    public abstract class workGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CompanyTreeMaint.SelectedNode.workGroupID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class SelectedParentNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _WorkGroupID;

    [PXInt]
    [PXUIField(DisplayName = "Move to")]
    public virtual int? WorkGroupID
    {
      get => this._WorkGroupID;
      set => this._WorkGroupID = value;
    }

    public abstract class workGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CompanyTreeMaint.SelectedParentNode.workGroupID>
    {
    }
  }
}
