// Decompiled with JetBrains decompiler
// Type: PX.EP.EPLoginTypeMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.Services;
using PX.Licensing;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.EP;

/// <exclude />
public class EPLoginTypeMaint : 
  PXGraph<
  #nullable disable
  EPLoginTypeMaint, EPLoginType, EPLoginType.loginTypeName>,
  ICaptionable
{
  public PXSelect<EPLoginType> LoginType;
  public PXSelect<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>>> CurrentLoginType;
  public PXSelectJoin<EPLoginTypeAllowsRole, InnerJoin<PX.SM.Roles, On<PX.SM.Roles.rolename, Equal<EPLoginTypeAllowsRole.rolename>>>, Where<EPLoginTypeAllowsRole.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>>> AllowedRoles;
  public PXSelect<PX.SM.Users, Where<PX.SM.Users.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>>> Users;
  public PXSelectJoin<EPManagedLoginType, InnerJoin<EPLoginType, On<EPLoginType.loginTypeID, Equal<EPManagedLoginType.loginTypeID>>>, Where<EPManagedLoginType.parentLoginTypeID, Equal<Current<EPLoginType.loginTypeID>>>> ManagedLoginTypes;
  public PXSelectUsersInRoles UserRoles;
  public PXSelectJoin<UsersInRoles, LeftJoin<PX.SM.Users, On<UsersInRoles.username, Equal<PX.SM.Users.username>>>, Where<PX.SM.Users.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>, And<UsersInRoles.rolename, In<Required<UsersInRoles.rolename>>>>> assignedRoles;
  public PXSelectJoin<EPLoginTypeAllowsRole, InnerJoin<PX.SM.Roles, On<PX.SM.Roles.rolename, Equal<EPLoginTypeAllowsRole.rolename>>>, Where<EPLoginTypeAllowsRole.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>, And<EPLoginTypeAllowsRole.isDefault, Equal<PX.Data.True>>>> DefaultAllowedRoles;
  public PXSelectJoin<EPLoginTypeAllowsRole, InnerJoin<PX.SM.Roles, On<PX.SM.Roles.rolename, Equal<EPLoginTypeAllowsRole.rolename>>>, Where<EPLoginTypeAllowsRole.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>, And<EPLoginTypeAllowsRole.isDefault, Equal<False>>>> NonDefaultAllowedRoles;
  public PXSelect<PX.SM.Users, Where<PX.SM.Users.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>>> defaultedUsers;
  public PXAction<EPLoginType> UpdateUsers;

  [InjectDependency]
  private ILicensing _licensing { get; set; }

  public string Caption()
  {
    EPLoginType current = this.LoginType.Current;
    return current == null || current.Description == null ? "" : current.Description;
  }

  [PXButton]
  [PXUIField(DisplayName = "Overwrite User Roles")]
  protected virtual void updateUsers()
  {
    IEnumerable<PX.SM.Roles> defaultRoles = this.DefaultAllowedRoles.Select().RowCast<PX.SM.Roles>();
    IEnumerable<PX.SM.Roles> nonDefaultRoles = this.NonDefaultAllowedRoles.Select().RowCast<PX.SM.Roles>();
    if ((defaultRoles == null || !defaultRoles.Any<PX.SM.Roles>()) && (nonDefaultRoles == null || !nonDefaultRoles.Any<PX.SM.Roles>()))
      return;
    this.Persist();
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
    {
      EPLoginTypeMaint instance = PXGraph.CreateInstance<EPLoginTypeMaint>();
      instance.LoginType.Current = this.LoginType.Current;
      IEnumerable<PXResult<PX.SM.Users>> source = instance.defaultedUsers.Select().AsEnumerable<PXResult<PX.SM.Users>>();
      if (defaultRoles != null && defaultRoles.Any<PX.SM.Roles>())
      {
        string[] array = defaultRoles.ToList<PX.SM.Roles>().Select<PX.SM.Roles, string>((Func<PX.SM.Roles, string>) (x => x.Rolename)).ToArray<string>();
        Dictionary<string, List<UsersInRoles>> dictionary = instance.assignedRoles.Select((object) array).RowCast<UsersInRoles>().GroupBy<UsersInRoles, string>((Func<UsersInRoles, string>) (x => x.Rolename)).ToDictionary<IGrouping<string, UsersInRoles>, string, List<UsersInRoles>>((Func<IGrouping<string, UsersInRoles>, string>) (x => x.Key), (Func<IGrouping<string, UsersInRoles>, List<UsersInRoles>>) (v => v.ToList<UsersInRoles>()));
        foreach (PX.SM.Roles roles in defaultRoles)
        {
          List<UsersInRoles> userRoles = dictionary.ContainsKey(roles.Rolename) ? dictionary[roles.Rolename] : new List<UsersInRoles>();
          foreach (PXResult<PX.SM.Users> pxResult in source.Where<PXResult<PX.SM.Users>>((Func<PXResult<PX.SM.Users>, bool>) (x => !userRoles.Select<UsersInRoles, string>((Func<UsersInRoles, string>) (u => u.Username.ToUpper())).Contains<string>(((PX.SM.Users) x).Username.ToUpper()))))
          {
            PX.SM.Users users = (PX.SM.Users) pxResult;
            instance.Caches[typeof (UsersInRoles)].Insert((object) new UsersInRoles()
            {
              Rolename = roles.Rolename,
              Username = users.Username,
              ApplicationName = "/"
            });
          }
        }
      }
      if (nonDefaultRoles != null && nonDefaultRoles.Any<PX.SM.Roles>())
      {
        string[] array = nonDefaultRoles.ToList<PX.SM.Roles>().Select<PX.SM.Roles, string>((Func<PX.SM.Roles, string>) (x => x.Rolename)).ToArray<string>();
        Dictionary<string, List<UsersInRoles>> dictionary = instance.assignedRoles.Select((object) array).RowCast<UsersInRoles>().GroupBy<UsersInRoles, string>((Func<UsersInRoles, string>) (x => x.Rolename)).ToDictionary<IGrouping<string, UsersInRoles>, string, List<UsersInRoles>>((Func<IGrouping<string, UsersInRoles>, string>) (x => x.Key), (Func<IGrouping<string, UsersInRoles>, List<UsersInRoles>>) (v => v.ToList<UsersInRoles>()));
        foreach (PX.SM.Roles roles in nonDefaultRoles)
        {
          List<UsersInRoles> userRoles = dictionary.ContainsKey(roles.Rolename) ? dictionary[roles.Rolename] : new List<UsersInRoles>();
          foreach (PXResult<PX.SM.Users> pxResult in source.Where<PXResult<PX.SM.Users>>((Func<PXResult<PX.SM.Users>, bool>) (x => userRoles.Select<UsersInRoles, string>((Func<UsersInRoles, string>) (u => u.Username.ToUpper())).Contains<string>(((PX.SM.Users) x).Username.ToUpper()))))
          {
            PX.SM.Users users = (PX.SM.Users) pxResult;
            UsersInRoles usersInRoles = PXSelectBase<UsersInRoles, PXSelect<UsersInRoles, Where<UsersInRoles.username, Equal<Required<UsersInRoles.username>>, And<UsersInRoles.rolename, Equal<Required<UsersInRoles.rolename>>, And<UsersInRoles.applicationName, Equal<Required<UsersInRoles.applicationName>>>>>>.Config>.Select((PXGraph) instance, (object) users.Username, (object) roles.Rolename, (object) "/").RowCast<UsersInRoles>().FirstOrDefault<UsersInRoles>();
            if (usersInRoles != null)
              instance.Caches[typeof (UsersInRoles)].Delete((object) usersInRoles);
          }
        }
      }
      instance.Persist();
    }));
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXSelector(typeof (Search<PX.SM.Users.username>), DescriptionField = typeof (PX.SM.Users.comment), DirtyRead = true)]
  protected virtual void UsersInRoles_Username_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(Events.RowSelected<EPLoginType> e)
  {
    EPLoginType row = e.Row;
    this.ManagedLoginTypes.Cache.AllowInsert = row != null && row.Entity == "C";
    this.ManagedLoginTypes.Cache.AllowUpdate = row != null && row.Entity == "C";
    PXUIFieldAttribute.SetVisible<EPLoginType.allowThisTypeForContacts>(this.LoginType.Cache, (object) row, row.Entity == "C");
    PXUIFieldAttribute.SetVisible<EPLoginType.allowThisTypeForEmployees>(this.LoginType.Cache, (object) row, row.Entity == "E");
  }

  protected virtual void _(Events.RowDeleting<EPLoginType> e)
  {
    EPLoginType row = e.Row;
    if (row == null)
      return;
    if ((PX.SM.Users) PXSelectBase<PX.SM.Users, PXSelect<PX.SM.Users, Where<PX.SM.Users.loginTypeID, Equal<Required<EPLoginType.loginTypeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.LoginTypeID) != null)
      throw new PXException("This record is referenced and cannot be deleted.");
  }

  protected virtual void _(
    Events.FieldVerifying<EPLoginType, EPLoginType.entity> e)
  {
    string newValue = (string) e.NewValue;
    EPLoginTypeAllowsRole loginTypeAllowsRole = (EPLoginTypeAllowsRole) PXSelectBase<EPLoginTypeAllowsRole, PXSelectJoin<EPLoginTypeAllowsRole, LeftJoin<PX.SM.Roles, On<EPLoginTypeAllowsRole.rolename, Equal<PX.SM.Roles.rolename>>>, Where<PX.SM.Roles.guest, NotEqual<PX.Data.True>, And<EPLoginTypeAllowsRole.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>>>>.Config>.Select((PXGraph) this);
    if (newValue == "C" && loginTypeAllowsRole != null)
      throw new PXSetPropertyException("Cannot change entity to Contact. Delete all non-guest Allowed Roles.");
  }

  protected virtual void _(
    Events.FieldVerifying<EPLoginTypeAllowsRole, EPLoginTypeAllowsRole.rolename> e)
  {
    PX.SM.Roles roles = PX.SM.Roles.UK.Find((PXGraph) this, e.NewValue as string);
    if (roles == null)
      return;
    e.NewValue = (object) roles.Rolename;
  }

  protected virtual void _(Events.RowPersisting<EPLoginType> e)
  {
    if (e.Row == null)
      return;
    int? allowedSessions = e.Row.AllowedSessions;
    string allowedLoginType = e.Row.AllowedLoginType;
    EPLoginTypeMaint.CheckAllowedSessionsValue(e.Row, e.Cache, allowedLoginType, allowedSessions, this._licensing.License);
  }

  private static void CheckAllowedSessionsValue(
    EPLoginType row,
    PXCache cache,
    string allowedLoginType,
    int? sessionsValue,
    PXLicense license)
  {
    int num1 = 1;
    int num2 = int.MaxValue;
    bool flag = true;
    switch (allowedLoginType)
    {
      case "U":
        num1 = 1;
        num2 = System.Math.Min(WebConfig.MaximumAllowedSessionsCount ?? int.MaxValue, license.UsersAllowed);
        flag = false;
        break;
      case "A":
        num1 = 1;
        num2 = System.Math.Min(WebConfig.MaximumAllowedSessionsCount ?? int.MaxValue, license.MaxApiUsersAllowed);
        flag = false;
        break;
      case "R":
        num1 = 1;
        num2 = System.Math.Min(WebConfig.MaximumAllowedSessionsCount ?? int.MaxValue, System.Math.Max(license.UsersAllowed, license.MaxApiUsersAllowed));
        break;
      default:
        if (!string.IsNullOrEmpty(allowedLoginType))
          break;
        goto case "R";
    }
    if (num2 < 3)
      num2 = 3;
    if (!flag && !sessionsValue.HasValue)
    {
      cache.RaiseExceptionHandling<EPLoginType.allowedSessions>((object) row, (object) null, (Exception) new PXSetPropertyException("The value in the Max. Number of Concurrent Logins box should be between {1} and {0}.", new object[2]
      {
        (object) num2,
        (object) num1
      }));
      throw new PXRowPersistingException("allowedSessions", (object) null, "The value in the Max. Number of Concurrent Logins box should be between {1} and {0}.", new object[2]
      {
        (object) num2,
        (object) num1
      });
    }
    if (sessionsValue.HasValue)
    {
      int? nullable1 = sessionsValue;
      int num3 = num1;
      if (!(nullable1.GetValueOrDefault() < num3 & nullable1.HasValue))
      {
        int? nullable2 = sessionsValue;
        int num4 = num2;
        if (!(nullable2.GetValueOrDefault() > num4 & nullable2.HasValue))
          return;
      }
      cache.RaiseExceptionHandling<EPLoginType.allowedSessions>((object) row, (object) null, (Exception) new PXSetPropertyException("The value in the Max. Number of Concurrent Logins box should be between {1} and {0}.", new object[2]
      {
        (object) num2,
        (object) num1
      }));
      throw new PXRowPersistingException("allowedSessions", (object) sessionsValue, "The value in the Max. Number of Concurrent Logins box should be between {1} and {0}.", new object[2]
      {
        (object) num2,
        (object) num1
      });
    }
  }

  protected virtual void _(
    Events.FieldSelecting<EPLoginType, EPLoginType.disableTwoFactorAuth> e)
  {
    if (e.Row == null || !(e.Row.AllowedLoginType == "A"))
      return;
    e.ReturnValue = (object) true;
  }

  protected virtual void _(
    Events.FieldSelecting<EPLoginType, EPLoginType.allowedLoginType> e)
  {
    if (e.Row == null || e.Row.AllowedLoginType != null)
      return;
    e.ReturnValue = (object) "R";
  }

  public override void Persist()
  {
    List<EPLoginTypeAllowsRole> allowed = new List<EPLoginTypeAllowsRole>(this.AllowedRoles.Select().RowCast<EPLoginTypeAllowsRole>());
    List<UsersInRoles> source = new List<UsersInRoles>(PXSelectBase<UsersInRoles, PXSelectJoin<UsersInRoles, LeftJoin<PX.SM.Users, On<UsersInRoles.username, Equal<PX.SM.Users.username>>>, Where<PX.SM.Users.loginTypeID, Equal<Current<EPLoginType.loginTypeID>>>>.Config>.Select((PXGraph) this).RowCast<UsersInRoles>());
    source.RemoveAll((Predicate<UsersInRoles>) (ur => allowed.Exists((Predicate<EPLoginTypeAllowsRole>) (ar => ar.Rolename == ur.Rolename))));
    List<string> list = source.Select<UsersInRoles, string>((Func<UsersInRoles, string>) (a => a.Rolename)).ToList<string>();
    IEnumerable<string> allowedRoles = ServiceLocator.Current.GetInstance<IUserService>().FilterRoles((IEnumerable<string>) list);
    source.RemoveAll((Predicate<UsersInRoles>) (a => !allowedRoles.Contains<string>(a.Rolename)));
    if (source.Count > 0 && this.AllowedRoles.View.Ask((string) null, (object) null, string.Empty, "Any not allowed roles for all corresponding users will be deleted.", MessageButtons.YesNo, MessageIcon.Warning) != WebDialogResult.Yes)
      return;
    foreach (UsersInRoles usersInRoles in source)
      this.UserRoles.Delete(usersInRoles);
    base.Persist();
  }
}
