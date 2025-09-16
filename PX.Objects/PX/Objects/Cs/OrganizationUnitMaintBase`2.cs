// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.OrganizationUnitMaintBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

public abstract class OrganizationUnitMaintBase<TOrgUnit, WhereClause> : 
  PXGraph<OrganizationUnitMaintBase<TOrgUnit, WhereClause>, TOrgUnit>,
  IActionsMenuGraph
  where TOrgUnit : PX.Objects.CR.BAccount, new()
  where WhereClause : class, IBqlWhere, new()
{
  [PXViewName("Customer")]
  public PXSelect<TOrgUnit, WhereClause> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Standalone.Location> BaseLocations;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Address> AddressDummy;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Contact> ContactDummy;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> VendorDummy;
  public PXSelect<BAccountItself, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Optional<PX.Objects.CR.BAccount.bAccountID>>>> CurrentBAccountItself;
  public PXSetup<PX.Objects.GL.Company> cmpany;
  public PXSelect<UploadFile> Files;
  public PXSelect<GLSetup> glsetup;
  public PXSelect<PX.Objects.GL.Company> Company;
  public PXMenuAction<TOrgUnit> ActionsMenu;
  public PXAction<TOrgUnit> newContact;

  [InjectDependency]
  private protected IUserBranchSlotControl UserBranchSlotControl { get; set; }

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  public virtual PXSelectBase<TOrgUnit> BAccountAccessor => (PXSelectBase<TOrgUnit>) this.BAccount;

  public abstract PXSelectBase<PX.Objects.EP.EPEmployee> EmployeesAccessor { get; }

  public PXAction ActionsMenuItem => (PXAction) this.ActionsMenu;

  protected OrganizationUnitMaintBase()
  {
    ((PXSelectBase) this.EmployeesAccessor).Cache.AllowInsert = false;
    ((PXSelectBase) this.EmployeesAccessor).Cache.AllowDelete = false;
    ((PXSelectBase) this.EmployeesAccessor).Cache.AllowUpdate = false;
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.salutation>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], "Attention");
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.fullName>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], (object) null);
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multiCompany>();
    ((PXAction) this.Next).SetVisible(flag);
    ((PXAction) this.Previous).SetVisible(flag);
    ((PXAction) this.Last).SetVisible(flag);
    ((PXAction) this.First).SetVisible(flag);
    ((PXAction) this.Insert).SetVisible(flag);
  }

  [PXSuppressActionValidation]
  [PXUIField]
  [PXButton]
  public IEnumerable ViewContact(PXAdapter adapter)
  {
    if (this.EmployeesAccessor.Current == null || ((PXSelectBase) this.BAccountAccessor).Cache.GetStatus((object) this.BAccountAccessor.Current) == 2)
      return adapter.Get();
    PX.Objects.EP.EPEmployee current = this.EmployeesAccessor.Current;
    EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
    if ((((PXSelectBase<PX.Objects.EP.EPEmployee>) instance.Employee).Current = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(((PXSelectBase<PX.Objects.EP.EPEmployee>) instance.Employee).Search<PX.Objects.EP.EPEmployee.bAccountID>((object) current.BAccountID, Array.Empty<object>()))) == null)
      throw new PXSetPropertyException("You do not have sufficient access rights to view or modify an employee for the {0} {1}.", new object[2]
      {
        (object) PXUIFieldAttribute.GetItemName(((PXSelectBase) this.BAccount).Cache),
        (object) this.BAccountAccessor.Current.AcctCD
      });
    throw new PXRedirectRequiredException((PXGraph) instance, "Contact Maintenance");
  }

  [PXUIField(DisplayName = "New Employee")]
  [PXButton]
  public IEnumerable NewContact(PXAdapter adapter)
  {
    if (((PXSelectBase) this.BAccountAccessor).Cache.GetStatus((object) this.BAccountAccessor.Current) != 2)
    {
      EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
      try
      {
        int? nullable = this.BaccountIDForNewEmployee();
        PXSelectJoin<PX.Objects.EP.EPEmployee, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<PX.Objects.EP.EPEmployee.parentBAccountID>>>, Where<PX.Objects.EP.EPEmployee.parentBAccountID, IsNull, Or<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>> employee = instance.Employee;
        PX.Objects.EP.EPEmployee epEmployee = new PX.Objects.EP.EPEmployee();
        epEmployee.RouteEmails = new bool?(true);
        epEmployee.ParentBAccountID = nullable;
        ((PXSelectBase<PX.Objects.EP.EPEmployee>) employee).Insert(epEmployee);
        ((PXSelectBase) instance.Employee).Cache.IsDirty = false;
        GraphHelper.Caches<RedirectEmployeeParameters>((PXGraph) instance).Insert(new RedirectEmployeeParameters()
        {
          RouteEmails = new bool?(true),
          ParentBAccountID = nullable
        });
      }
      catch (PXFieldProcessingException ex)
      {
        if (((PXSelectBase) instance.Employee).Cache.GetBqlField(ex.FieldName) == typeof (PX.Objects.EP.EPEmployee.parentBAccountID))
          throw new PXSetPropertyException("You do not have sufficient access rights to view or modify an employee for the {0} {1}.", new object[2]
          {
            (object) PXUIFieldAttribute.GetItemName(((PXSelectBase) this.BAccount).Cache),
            (object) this.BAccountAccessor.Current.AcctCD
          });
        throw;
      }
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField]
  [ContactDisplayName(typeof (PX.Objects.CR.Contact.lastName), typeof (PX.Objects.CR.Contact.firstName), typeof (PX.Objects.CR.Contact.midName), typeof (PX.Objects.CR.Contact.title), true)]
  protected virtual void Contact_DisplayName_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<TOrgUnit> e)
  {
    if (e.TranStatus != null || (e.Operation & 3) != 3)
      return;
    PXUpdate<Set<BAccountR.cOrgBAccountID, Zero>, BAccountR, Where<BAccountR.cOrgBAccountID, Equal<Required<BAccountR.cOrgBAccountID>>>>.Update((PXGraph) this, new object[1]
    {
      (object) e.Row.BAccountID
    });
    PXUpdate<Set<BAccountR.vOrgBAccountID, Zero>, BAccountR, Where<BAccountR.vOrgBAccountID, Equal<Required<BAccountR.vOrgBAccountID>>>>.Update((PXGraph) this, new object[1]
    {
      (object) e.Row.BAccountID
    });
    PXUpdate<Set<CustomerClass.orgBAccountID, Zero>, CustomerClass, Where<CustomerClass.orgBAccountID, Equal<Required<PX.Objects.CR.BAccount.cOrgBAccountID>>>>.Update((PXGraph) this, new object[1]
    {
      (object) e.Row.BAccountID
    });
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.baseCuryID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.baseCuryID>, PX.Objects.CR.BAccount, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.baseCuryID>>) e).Cancel = true;
  }

  protected abstract int? BaccountIDForNewEmployee();

  protected void ClearRoleNameInBranches()
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).Caches[typeof (PX.Objects.GL.Branch)].Clear();
      bool flag = true;
      foreach (PXResult<PX.Objects.GL.Branch> pxResult in PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      {
        if (PXResult<PX.Objects.GL.Branch>.op_Implicit(pxResult).RoleName != null)
          flag = false;
      }
      using (new PXReadDeletedScope(false))
      {
        if (flag)
          PXDatabase.Update<PX.Objects.GL.Branch>(new PXDataFieldParam[1]
          {
            (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.GL.Branch.roleName>((object) null)
          });
      }
      transactionScope.Complete();
    }
  }

  protected void RefreshBranch()
  {
    int? currentBranchId = PXContext.GetBranchID();
    this.UserBranchSlotControl.Reset();
    if (this._currentUserInformationProvider.GetActiveBranches().Where<BranchInfo>((Func<BranchInfo, bool>) (b =>
    {
      int id = b.Id;
      int? nullable = currentBranchId;
      int valueOrDefault = nullable.GetValueOrDefault();
      return id == valueOrDefault & nullable.HasValue;
    })).Any<BranchInfo>())
      return;
    PXLogin.SetBranchID(PXGraph.CreateInstance<SMAccessPersonalMaint>().GetDefaultBranchId());
  }
}
