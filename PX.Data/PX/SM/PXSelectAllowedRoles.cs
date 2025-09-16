// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectAllowedRoles
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Data;
using PX.Data.Access.ActiveDirectory;
using PX.Data.Services;
using PX.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class PXSelectAllowedRoles : PXSelectBase<EPLoginTypeAllowsRole>
{
  private bool isLoginTypeUpdated;

  [InjectDependency]
  protected IUserService userService { get; set; }

  public PXSelectAllowedRoles(PXGraph graph)
  {
    this._Graph = graph;
    this.View = new PXView(this._Graph, false, (BqlCommand) new PX.Data.Select<EPLoginTypeAllowsRole, Where<EPLoginTypeAllowsRole.loginTypeID, Equal<PX.Data.Current<Users.loginTypeID>>>>(), (Delegate) new PXSelectDelegate(this.ViewDelegate));
    this._Graph.FieldVerifying.AddHandler(typeof (EPLoginTypeAllowsRole), typeof (EPLoginTypeAllowsRole.selected).Name, new PXFieldVerifying(this.SelectedFieldVerifying));
    this._Graph.FieldUpdated.AddHandler(typeof (EPLoginTypeAllowsRole), typeof (EPLoginTypeAllowsRole.selected).Name, new PXFieldUpdated(this.SelectedFieldUpdated));
    this._Graph.FieldUpdated.AddHandler(typeof (Users), typeof (Users.loginTypeID).Name, new PXFieldUpdated(this.UsersLoginTypeIDFieldUpdated));
    this._Graph.RowPersisting.AddHandler(typeof (EPLoginTypeAllowsRole), new PXRowPersisting(this.RowPersisting));
    this._Graph.RowPersisted.AddHandler(typeof (Users), new PXRowPersisted(this.UsersRowPersisted));
    this._Graph.RowSelected.AddHandler(typeof (Users), new PXRowSelected(this.UsersRowSelected));
  }

  protected virtual IEnumerable ViewDelegate()
  {
    PXSelectAllowedRoles selectAllowedRoles = this;
    PXCache cach = selectAllowedRoles._Graph.Caches[typeof (Users)];
    Users current = (Users) cach.Current;
    if (current != null && current.Username != null)
    {
      int? nullable1 = current.Source;
      int num = 1;
      bool? nullable2;
      if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      {
        nullable2 = current.OverrideADRoles;
        bool flag = true;
        if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
        {
          string[] strArray = ServiceLocator.Current.GetInstance<IActiveDirectoryProvider>().GetADMappedRolesBySID(current.ExtRef) ?? new string[0];
          for (int index = 0; index < strArray.Length; ++index)
          {
            string str = strArray[index];
            yield return (object) new EPLoginTypeAllowsRole()
            {
              Rolename = str,
              Selected = new bool?(true)
            };
          }
          strArray = (string[]) null;
          yield break;
        }
      }
      bool IsUserInserted = cach.GetStatus((object) current) == PXEntryStatus.Inserted;
      Dictionary<string, UsersInRoles> assigned = PXSelectBase<UsersInRoles, PXSelect<UsersInRoles, Where<UsersInRoles.username, Equal<PX.Data.Current<Users.username>>>>.Config>.Select(selectAllowedRoles._Graph).RowCast<UsersInRoles>().ToDictionary<UsersInRoles, string>((Func<UsersInRoles, string>) (ur => ur.Rolename));
      Dictionary<string, EPLoginTypeAllowsRole> allowed = new Dictionary<string, EPLoginTypeAllowsRole>();
      nullable1 = current.LoginTypeID;
      if (!nullable1.HasValue)
      {
        foreach (EPLoginTypeAllowsRole loginTypeAllowsRole in PXSelectBase<Roles, PXSelect<Roles, Where<Roles.guest, Equal<PX.Data.Current<Users.guest>>, Or<PX.Data.Current<Users.guest>, Equal<False>>>>.Config>.Select(selectAllowedRoles._Graph).RowCast<Roles>().Select<Roles, EPLoginTypeAllowsRole>((Func<Roles, EPLoginTypeAllowsRole>) (r => new EPLoginTypeAllowsRole()
        {
          Rolename = r.Rolename,
          IsDefault = new bool?(false)
        })))
        {
          allowed.Add(loginTypeAllowsRole.Rolename, loginTypeAllowsRole);
          selectAllowedRoles.Insert(loginTypeAllowsRole);
          selectAllowedRoles.Cache.IsDirty = false;
        }
      }
      else
        allowed = PXSelectBase<EPLoginTypeAllowsRole, PXSelectJoin<EPLoginTypeAllowsRole, InnerJoin<Roles, On<EPLoginTypeAllowsRole.rolename, Equal<Roles.rolename>>>, Where<EPLoginTypeAllowsRole.loginTypeID, Equal<PX.Data.Current<Users.loginTypeID>>>>.Config>.Select(selectAllowedRoles._Graph).RowCast<EPLoginTypeAllowsRole>().ToDictionary<EPLoginTypeAllowsRole, string>((Func<EPLoginTypeAllowsRole, string>) (ar => ar.Rolename));
      HashSet<string> allRoles = new HashSet<string>((IEnumerable<string>) assigned.Keys);
      allRoles.UnionWith((IEnumerable<string>) allowed.Keys);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      foreach (string key in selectAllowedRoles.userService.FilterRoles((IEnumerable<string>) allRoles).Where<string>(PXSelectAllowedRoles.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled ?? (PXSelectAllowedRoles.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled = new Func<string, bool>(PXAccess.IsRoleEnabled))))
      {
        EPLoginTypeAllowsRole loginTypeAllowsRole;
        allowed.TryGetValue(key, out loginTypeAllowsRole);
        UsersInRoles usersInRoles;
        assigned.TryGetValue(key, out usersInRoles);
        if (usersInRoles != null && loginTypeAllowsRole != null)
        {
          loginTypeAllowsRole.Selected = new bool?(true);
          yield return (object) loginTypeAllowsRole;
        }
        else
        {
          if (usersInRoles == null && loginTypeAllowsRole != null)
          {
            nullable2 = loginTypeAllowsRole.IsDefault;
            bool flag = true;
            if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue && selectAllowedRoles.isLoginTypeUpdated | IsUserInserted)
            {
              selectAllowedRoles._Graph.Caches[typeof (UsersInRoles)].Insert((object) new UsersInRoles()
              {
                Rolename = loginTypeAllowsRole.Rolename
              });
              loginTypeAllowsRole.Selected = new bool?(true);
              yield return (object) loginTypeAllowsRole;
              continue;
            }
          }
          if (usersInRoles != null)
            selectAllowedRoles._Graph.Caches[typeof (UsersInRoles)].Delete((object) usersInRoles);
          else if (loginTypeAllowsRole != null)
          {
            loginTypeAllowsRole.Selected = new bool?(false);
            yield return (object) loginTypeAllowsRole;
          }
        }
      }
      assigned = (Dictionary<string, UsersInRoles>) null;
      allowed = (Dictionary<string, EPLoginTypeAllowsRole>) null;
    }
  }

  protected virtual void SelectedFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if ((bool) e.NewValue || PXAccess.GetUserName() != ((Users) this._Graph.Caches[typeof (Users)].Current).Username)
      return;
    EPLoginTypeAllowsRole row = (EPLoginTypeAllowsRole) e.Row;
    if (!PX.SM.Access.WillSelfLock(sender, row.Rolename) || this.Ask("Warning", "These changes will prevent you from accessing this form. Are you sure you want to continue?", MessageButtons.YesNo, MessageIcon.Warning) == WebDialogResult.Yes)
      return;
    e.NewValue = (object) true;
    e.Cancel = true;
  }

  protected virtual void SelectedFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    EPLoginTypeAllowsRole row = (EPLoginTypeAllowsRole) e.Row;
    UsersInRoles usersInRoles = (UsersInRoles) PXSelectBase<UsersInRoles, PXSelect<UsersInRoles, Where<UsersInRoles.rolename, Equal<Required<UsersInRoles.rolename>>, And<UsersInRoles.username, Equal<Required<UsersInRoles.username>>>>>.Config>.Select(sender.Graph, (object) row.Rolename, (object) ((Users) this._Graph.Caches[typeof (Users)].Current).Username);
    bool? selected1 = row.Selected;
    bool flag1 = true;
    if (selected1.GetValueOrDefault() == flag1 & selected1.HasValue && usersInRoles == null)
      sender.Graph.Caches[typeof (UsersInRoles)].Insert((object) new UsersInRoles()
      {
        Rolename = row.Rolename
      });
    bool? selected2 = row.Selected;
    bool flag2 = true;
    if (selected2.GetValueOrDefault() == flag2 & selected2.HasValue || usersInRoles == null)
      return;
    sender.Graph.Caches[typeof (UsersInRoles)].Delete((object) usersInRoles);
  }

  protected virtual void UsersLoginTypeIDFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.isLoginTypeUpdated = true;
  }

  protected virtual void UsersRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    Users row = (Users) e.Row;
    if (row == null)
      return;
    PXCache cach = this._Graph.Caches[typeof (EPLoginTypeAllowsRole)];
    int? source = row.Source;
    int num1 = 0;
    int num2;
    if (!(source.GetValueOrDefault() == num1 & source.HasValue))
    {
      bool? overrideAdRoles = row.OverrideADRoles;
      bool flag = true;
      num2 = overrideAdRoles.GetValueOrDefault() == flag & overrideAdRoles.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    PXUIFieldAttribute.SetEnabled<EPLoginTypeAllowsRole.selected>(cach, (object) null, num2 != 0);
    PXUIFieldAttribute.SetEnabled<EPLoginTypeAllowsRole.rolename>(this._Graph.Caches[typeof (EPLoginTypeAllowsRole)], (object) null, false);
  }

  protected virtual void UsersRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    this.isLoginTypeUpdated = false;
  }

  protected virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }
}
