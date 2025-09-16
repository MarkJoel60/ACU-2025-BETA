// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectUsers`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Services;
using PX.Data.SQLTree;
using PX.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.SM;

public class PXSelectUsers<TPrimary> : PXSelectBase<Users> where TPrimary : class, IBqlTable, new()
{
  public PXAction<TPrimary> ResetPassword;
  public PXAction<TPrimary> ResetPasswordOK;
  public PXAction<TPrimary> ActivateLogin;
  public PXAction<TPrimary> DisableLogin;
  public PXAction<TPrimary> EnableLogin;
  public PXAction<TPrimary> UnlockLogin;
  public PXAction<TPrimary> SendLoginLink;

  [InjectDependency]
  private IUserService _userService { get; set; }

  [InjectDependency]
  private IUserManagementService _userManagementService { get; set; }

  public PXSelectUsers(PXGraph graph, Delegate handler)
    : this(graph)
  {
    this.View = new PXView(this._Graph, false, this.View.BqlSelect, handler);
  }

  public PXSelectUsers(PXGraph graph)
  {
    this._Graph = graph;
    this.View = new PXView(this._Graph, false, (BqlCommand) new PX.Data.Select<Users, Where<Users.isHidden, Equal<False>>>());
    System.Type table = typeof (Users);
    this._Graph.FieldDefaulting.AddHandler(table, typeof (Users.pKID).Name, new PXFieldDefaulting(this.PKIDFieldDefaulting));
    this._Graph.FieldUpdated.AddHandler(table, typeof (Users.passwordChangeable).Name, new PXFieldUpdated(this.PasswordChangeableFieldUpdated));
    this._Graph.FieldUpdated.AddHandler(table, typeof (Users.guest).Name, new PXFieldUpdated(this.GuestFieldUpdated));
    this._Graph.FieldVerifying.AddHandler(table, typeof (Users.guest).Name, new PXFieldVerifying(this.GuestFieldVerifying));
    this._Graph.FieldVerifying.AddHandler(table, typeof (Users.username).Name, new PXFieldVerifying(this.UsernameFieldVerifying));
    this._Graph.CommandPreparing.AddHandler(table, typeof (Users.password).Name, new PXCommandPreparing(this.PasswordCommandPreparing));
    this._Graph.RowPersisting.AddHandler(table, new PXRowPersisting(this.RowPersisting));
    this._Graph.RowPersisted.AddHandler(table, new PXRowPersisted(this.RowPersisted));
    this._Graph.RowUpdated.AddHandler(table, new PXRowUpdated(this.RowUpdated));
    this._Graph.RowSelected.AddHandler(table, new PXRowSelected(this.RowSelected));
    this._Graph.RowInserting.AddHandler(table, new PXRowInserting(this.RowInserting));
    this._Graph.RowInserted.AddHandler(table, new PXRowInserted(this.RowInserted));
    this._Graph.RowUpdating.AddHandler(table, new PXRowUpdating(this.RowUpdating));
    this.ResetPassword = PXNamedAction<TPrimary>.AddAction(graph, nameof (ResetPassword), "Reset Password", new PXButtonDelegate(this.resetPassword));
    this.ResetPasswordOK = PXNamedAction<TPrimary>.AddAction(graph, nameof (ResetPasswordOK), "OK", new PXButtonDelegate(this.resetPasswordOK));
    this.ActivateLogin = PXNamedAction<TPrimary>.AddAction(graph, nameof (ActivateLogin), "Activate User", new PXButtonDelegate(this.activateLogin));
    this.EnableLogin = PXNamedAction<TPrimary>.AddAction(graph, nameof (EnableLogin), "Enable User", new PXButtonDelegate(this.enableLogin));
    this.EnableLogin.IsMass = true;
    this.DisableLogin = PXNamedAction<TPrimary>.AddAction(graph, nameof (DisableLogin), "Disable User", new PXButtonDelegate(this.disableLogin));
    this.DisableLogin.IsMass = true;
    this.UnlockLogin = PXNamedAction<TPrimary>.AddAction(graph, nameof (UnlockLogin), "Unlock User", new PXButtonDelegate(this.unlockLogin));
    this.SendLoginLink = PXNamedAction<TPrimary>.AddAction(graph, nameof (SendLoginLink), "Send Login Link", new PXButtonDelegate(this.sendLoginLink));
  }

  [PXUIField(DisplayName = "Reset Password", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable resetPassword(PXAdapter adapter)
  {
    this.Current.NewPassword = (string) null;
    this.Current.ConfirmPassword = (string) null;
    int num = (int) this.AskExt();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "OK", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable resetPasswordOK(PXAdapter adapter)
  {
    this._Graph.Actions.PressSave();
    if (string.IsNullOrWhiteSpace(this.Current.NewPassword))
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[newPassword]"
      });
    if (string.IsNullOrWhiteSpace(this.Current.ConfirmPassword))
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[confirmPassword]"
      });
    Access.CheckPasswords((IUserValidationService) this._userManagementService, false, this.Current);
    this.Current.Password = this.Current.NewPassword;
    Access.SetPassword(this._userManagementService, false, false, this.Current);
    try
    {
      PXSelectUsers<TPrimary>.SendPasswordInfo(this.Current);
    }
    finally
    {
      if (HttpContext.Current != null && this.Current.Username == PXAccess.GetUserName())
        throw new PXRefreshException();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Activate User", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable activateLogin(PXAdapter adapter)
  {
    this.Current.IsPendingActivation = new bool?(false);
    this.Update(this.Current);
    this.SavePress();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Enable User", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable enableLogin(PXAdapter adapter)
  {
    this.Current.IsApproved = new bool?(true);
    this.Current.IsPendingActivation = new bool?(false);
    this.Update(this.Current);
    this.SavePress();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Disable User", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable disableLogin(PXAdapter adapter)
  {
    Guid userId = this._Graph.Accessinfo.UserID;
    Guid? pkid = this.Current.PKID;
    if ((pkid.HasValue ? (userId == pkid.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXException("You cannot disable your own user account.");
    bool? isOnLine = this.Current.IsOnLine;
    bool flag = true;
    if (isOnLine.GetValueOrDefault() == flag & isOnLine.HasValue && this.Ask("This user is currently online. The user will be signed out from the system and disabled for further sessions. Do you want to continue?", MessageButtons.YesNo) != WebDialogResult.Yes)
      return adapter.Get();
    this.Current.IsApproved = new bool?(false);
    this.Current.IsOnLine = new bool?(false);
    this.Update(this.Current);
    this.SavePress();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Unlock User", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable unlockLogin(PXAdapter adapter)
  {
    this.Current.IsLockedOut = new bool?(false);
    this.Current.LastLockedOutDate = this.Current.LockedOutDate;
    this.Current.LockedOutDate = new System.DateTime?();
    this.Update(this.Current);
    this.SavePress();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Send Login Link", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable sendLoginLink(PXAdapter adapter)
  {
    PXSelectUsers<TPrimary>.SendLoginInfo(this.Current);
    return adapter.Get();
  }

  protected virtual void SavePress()
  {
    if (!this._Graph.Actions.Contains((object) "Save"))
      return;
    this._Graph.Actions["Save"].Press();
  }

  protected virtual void PKIDFieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected virtual void PasswordCommandPreparing(PXCache cache, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || PXTransactionScope.GetSharedInsert() <= 0)
      return;
    e.DataType = PXDbType.NVarChar;
    e.DataLength = new int?(512 /*0x0200*/);
    e.DataValue = (object) ((Users) e.Row).Password;
    e.BqlTable = this.Cache.BqlTable;
    PXCommandPreparingEventArgs preparingEventArgs = e;
    System.Type dac = e.Table;
    if ((object) dac == null)
      dac = e.BqlTable;
    Column column = new Column("Password", (Table) new SimpleTable(dac), e.DataType);
    preparingEventArgs.Expr = (SQLExpression) column;
  }

  protected virtual void PasswordChangeableFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Users row1 = (Users) e.Row;
    int? source = row1.Source;
    int num1 = 0;
    bool flag1 = source.GetValueOrDefault() == num1 & source.HasValue;
    PXCache cache = sender;
    object row2 = e.Row;
    int num2;
    if (flag1)
    {
      bool? passwordChangeable = row1.PasswordChangeable;
      bool flag2 = true;
      num2 = passwordChangeable.GetValueOrDefault() == flag2 & passwordChangeable.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<Users.passwordChangeOnNextLogin>(cache, row2, num2 != 0);
  }

  protected virtual void GuestFieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void GuestFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Users row))
      return;
    bool? oldValue = (bool?) e.OldValue;
    bool? guest = row.Guest;
    if (oldValue.GetValueOrDefault() == guest.GetValueOrDefault() & oldValue.HasValue == guest.HasValue)
      return;
    ((Users) e.Row).ContactID = new int?();
  }

  protected virtual void UsernameFieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  public void ToggleActions(PXCache sender, Users user)
  {
    this.ActivateLogin.SetEnabled(user != null && user.State == "P");
    this.EnableLogin.SetEnabled(user != null && user.State == "D");
    this.DisableLogin.SetEnabled(user != null && (user.State == "A" || user.State == "O"));
    this.UnlockLogin.SetEnabled(user != null && user.State == "L");
    this.ActivateLogin.SetVisible(user != null && user.State == "P");
    this.EnableLogin.SetVisible(user != null && user.State == "D");
    this.DisableLogin.SetVisible(user != null && (user.State == "A" || user.State == "O"));
    this.UnlockLogin.SetVisible(user != null && user.State == "L");
    bool flag1 = sender.GetStatus((object) user) == PXEntryStatus.Inserted;
    int num1;
    if (user == null)
    {
      num1 = 0;
    }
    else
    {
      bool? loginWithPassword = user.ForbidLoginWithPassword;
      bool flag2 = true;
      num1 = loginWithPassword.GetValueOrDefault() == flag2 & loginWithPassword.HasValue ? 1 : 0;
    }
    bool flag3 = num1 != 0;
    this.ResetPassword.SetVisible(!flag1 && user != null);
    PXAction<TPrimary> resetPassword = this.ResetPassword;
    int num2;
    if (!flag1 && user != null)
    {
      int? source = user.Source;
      int num3 = 0;
      if (source.GetValueOrDefault() == num3 & source.HasValue)
      {
        num2 = !flag3 ? 1 : 0;
        goto label_7;
      }
    }
    num2 = 0;
label_7:
    resetPassword.SetEnabled(num2 != 0);
    this.SendLoginLink.SetVisible(false);
    this.SendLoginLink.SetEnabled(false);
  }

  public static void SendLoginInfo(Users row)
  {
    if (row == null || string.IsNullOrEmpty(row.Email))
      return;
    int? source = row.Source;
    int num = 0;
    if (!(source.GetValueOrDefault() == num & source.HasValue))
      return;
    Access instance = PXGraph.CreateInstance<Access>();
    instance.UserList.Current = row;
    instance.SendUserWelcome.Press();
  }

  public static void SendLoginInfo(Users row, PXGraph originalGraph)
  {
    if (row == null || string.IsNullOrEmpty(row.Email))
      return;
    int? source = row.Source;
    int num = 0;
    if (!(source.GetValueOrDefault() == num & source.HasValue))
      return;
    Access instance = PXGraph.CreateInstance<Access>();
    foreach (IBqlTable current in originalGraph.Caches.Currents)
    {
      instance.EnsureCachePersistence(current.GetType());
      instance.Caches[current.GetType()].Current = (object) current;
    }
    foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) originalGraph.Views)
    {
      if (!instance.Views.ContainsKey(view.Key))
        instance.Views.Add(view.Key, view.Value);
    }
    instance.UserList.Current = row;
    instance.SendUserWelcome.Press();
  }

  public static void SendPasswordInfo(Users row)
  {
    if (row == null || string.IsNullOrEmpty(row.Email))
      return;
    int? source = row.Source;
    int num = 0;
    if (!(source.GetValueOrDefault() == num & source.HasValue))
      return;
    Access instance = PXGraph.CreateInstance<Access>();
    instance.UserList.Current = row;
    instance.SendChangedPassword.Press();
  }

  [PXInternalUseOnly]
  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    Users row = (Users) e.Row;
    this.ToggleActions(sender, row);
    if (row == null)
      return;
    bool flag1 = sender.GetStatus((object) row) == PXEntryStatus.Inserted;
    int? source1 = row.Source;
    int num1 = 0;
    bool isEnabled = source1.GetValueOrDefault() == num1 & source1.HasValue;
    PXUIFieldAttribute.SetEnabled<Users.loginTypeID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetVisible<Users.password>(sender, (object) row, flag1 & isEnabled);
    PXUIFieldAttribute.SetVisible<Users.generatePassword>(sender, (object) row, flag1 & isEnabled);
    PXCache cache1 = sender;
    Users data1 = row;
    bool? nullable;
    int num2;
    if (flag1 & isEnabled)
    {
      nullable = row.GeneratePassword;
      bool flag2 = true;
      num2 = !(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<Users.password>(cache1, (object) data1, num2 != 0);
    PXUIFieldAttribute.SetEnabled<Users.firstName>(sender, (object) row, !row.ContactID.HasValue & isEnabled);
    PXUIFieldAttribute.SetEnabled<Users.lastName>(sender, (object) row, !row.ContactID.HasValue & isEnabled);
    PXUIFieldAttribute.SetEnabled<Users.email>(sender, (object) row, !row.ContactID.HasValue & isEnabled);
    PXUIFieldAttribute.SetEnabled<Users.allowPasswordRecovery>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<Users.passwordChangeable>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<Users.passwordNeverExpires>(sender, (object) row, isEnabled);
    PXCache cache2 = sender;
    Users data2 = row;
    int num3;
    if (isEnabled)
    {
      nullable = row.PasswordChangeable;
      bool flag3 = true;
      num3 = nullable.GetValueOrDefault() == flag3 & nullable.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<Users.passwordChangeOnNextLogin>(cache2, (object) data2, num3 != 0);
    PXUIFieldAttribute.SetEnabled<Users.guest>(sender, (object) row, false);
    PXCache cache3 = sender;
    Users data3 = row;
    nullable = row.Guest;
    bool flag4 = true;
    int check = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 0 : 2;
    PXDefaultAttribute.SetPersistingCheck<Users.loginTypeID>(cache3, (object) data3, (PXPersistingCheck) check);
    PXCache cache4 = sender;
    Users data4 = row;
    int? source2 = row.Source;
    int num4 = 0;
    int num5 = !(source2.GetValueOrDefault() == num4 & source2.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetVisible<Users.overrideADRoles>(cache4, (object) data4, num5 != 0);
    string error1 = PXUIFieldAttribute.GetError<Users.username>(sender, (object) row);
    if (!string.IsNullOrEmpty(error1) && !(error1 == "A username containing '\\' can hide the Active Directory login"))
      return;
    PXCache cache5 = sender;
    Users data5 = row;
    string error2;
    if (row.Username != null && row.Username.Contains<char>('\\'))
    {
      int? source3 = row.Source;
      int num6 = 0;
      if (source3.GetValueOrDefault() == num6 & source3.HasValue)
      {
        error2 = "A username containing '\\' can hide the Active Directory login";
        goto label_13;
      }
    }
    error2 = (string) null;
label_13:
    PXUIFieldAttribute.SetWarning<Users.username>(cache5, (object) data5, error2);
  }

  protected virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (e.NewRow == null || ((Users) e.NewRow).PKID.HasValue)
      return;
    e.Cancel = true;
  }

  protected virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    Users row = (Users) e.Row;
    if (row == null || row.PKID.HasValue)
      return;
    e.Cancel = true;
  }

  protected virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    PXNoteAttribute.GetNoteID(sender, e.Row, typeof (Users.noteID).Name);
    sender.SetDefaultExt<Users.generatePassword>(e.Row);
    PXSelectUsers<TPrimary>.GeneratePassword((Users) e.Row);
  }

  protected virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is Users row))
      return;
    bool? nullable1 = row.IsLockedOut;
    bool flag1 = true;
    System.DateTime? nullable2;
    System.DateTime dateTime;
    if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
    {
      nullable2 = row.LockedOutDate;
      System.DateTime accountLockoutTime = SitePolicy.AccountLockoutTime;
      if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() > accountLockoutTime ? 1 : 0) : 0) == 0)
      {
        Users users1 = row;
        dateTime = PXTimeZoneInfo.Now;
        System.DateTime? nullable3 = new System.DateTime?(dateTime.ToUniversalTime());
        users1.LockedOutDate = nullable3;
        Users users2 = row;
        dateTime = PXTimeZoneInfo.Now;
        System.DateTime? nullable4 = new System.DateTime?(dateTime.ToUniversalTime());
        users2.LastLockedOutDate = nullable4;
      }
    }
    nullable1 = row.IsLockedOut;
    bool flag2 = false;
    if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
    {
      nullable2 = row.LockedOutDate;
      dateTime = SitePolicy.AccountLockoutTime;
      if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() > dateTime ? 1 : 0) : 0) != 0)
      {
        Users users = row;
        nullable2 = new System.DateTime?();
        System.DateTime? nullable5 = nullable2;
        users.LockedOutDate = nullable5;
      }
    }
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Update)
    {
      nullable1 = row.IsLockedOut;
      bool flag3 = true;
      if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
      {
        Guid userId = this._Graph.Accessinfo.UserID;
        Guid? pkid = row.PKID;
        if ((pkid.HasValue ? (userId == pkid.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          sender.RaiseExceptionHandling<Users.isLockedOut>(e.Row, (object) row.IsLockedOut, (Exception) new PXSetPropertyException("You cannot lock yourself out."));
      }
    }
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert)
    {
      if (!string.IsNullOrEmpty(row.Username))
      {
        Users users = (Users) PXSelectBase<Users, PXSelectReadonly<Users>.Config>.Search<Users.username>(this._Graph, (object) row.Username);
        if (users != null)
        {
          Guid? pkid1 = users.PKID;
          if (pkid1.HasValue)
          {
            pkid1 = users.PKID;
            Guid? pkid2 = row.PKID;
            if ((pkid1.HasValue == pkid2.HasValue ? (pkid1.HasValue ? (pkid1.GetValueOrDefault() != pkid2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
            {
              PXSetPropertyException propertyException = (PXSetPropertyException) new PXSetPropertyException<Users.username>("The entered login already exists.");
              sender.RaiseExceptionHandling<Users.username>(e.Row, (object) row.Username, (Exception) propertyException);
              throw propertyException;
            }
          }
        }
      }
      if (!string.IsNullOrEmpty(row.ConfirmPassword) && !(row.Password == row.ConfirmPassword) && !(row.NewPassword == row.ConfirmPassword))
        sender.RaiseExceptionHandling<Users.password>(e.Row, (object) row.Password, (Exception) new PXSetPropertyException("The entered password doesn't match the confirmation."));
      row.CreationDate = new System.DateTime?(PXTimeZoneInfo.Now);
      row.NewPassword = row.Password;
      row.ConfirmPassword = row.Password;
    }
    nullable1 = row.PasswordChangeable;
    bool flag4 = false;
    if (!(nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue))
      return;
    row.PasswordChangeOnNextLogin = new bool?(false);
  }

  protected virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    Users row = (Users) e.Row;
    if (e.TranStatus != PXTranStatus.Open)
      return;
    Guid? guidFromDeletedUser = Access.GetGuidFromDeletedUser(((Users) e.Row).Username);
    if (guidFromDeletedUser.HasValue)
      ((Users) e.Row).PKID = guidFromDeletedUser;
    bool flag1 = false;
    bool flag2 = (e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert;
    int? source = row.Source;
    int num = 0;
    if (source.GetValueOrDefault() == num & source.HasValue)
    {
      bool flag3 = flag2;
      if (!flag3 && row.Password != null && (e.Operation & PXDBOperation.Delete) == PXDBOperation.Update)
        flag3 = ((Users) PXSelectBase<Users, PXSelectReadonly<Users, Where<Users.pKID, Equal<Required<Users.pKID>>>>.Config>.SelectWindowed(this._Graph, 0, 1, (object) row.PKID)).Password == null;
      if (flag3)
      {
        row.NewPassword = row.Password;
        row.ConfirmPassword = row.Password;
        Access.SetPassword(this._userManagementService, false, row);
        PXSelectUsers<TPrimary>.SendLoginInfo(row, this._Graph);
        flag1 = true;
      }
    }
    if (!flag1 & flag2)
    {
      bool? loginWithPassword = row.ForbidLoginWithPassword;
      bool flag4 = true;
      if (loginWithPassword.GetValueOrDefault() == flag4 & loginWithPassword.HasValue)
        PXSelectUsers<TPrimary>.SendLoginInfo(row, this._Graph);
    }
    PXUIFieldAttribute.SetVisible<Users.password>(sender, e.Row, false);
    PXUIFieldAttribute.SetVisible<Users.newPassword>(sender, e.Row, true);
  }

  protected virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    bool flag1 = false;
    foreach (string field in (List<string>) sender.Fields)
    {
      if (!(field == "NewPassword") && !(field == "OldPassword") && !(field == "ConfirmPassword") && !(field == "LastLoginDate") && !(field == "lastPasswordChangedDate") && !object.Equals(sender.GetValue(e.Row, field), sender.GetValue(e.OldRow, field)))
      {
        flag1 = true;
        break;
      }
    }
    sender.IsDirty |= flag1;
    Users oldRow = (Users) e.OldRow;
    Users row = (Users) e.Row;
    bool? guest = row.Guest;
    bool flag2 = true;
    bool? nullable;
    if (guest.GetValueOrDefault() == flag2 & guest.HasValue)
    {
      nullable = oldRow.Guest;
      bool flag3 = true;
      if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
      {
        foreach (PXResult<UsersInRoles, Roles> pxResult in PXSelectBase<UsersInRoles, PXSelectJoin<UsersInRoles, LeftJoin<Roles, On<Roles.rolename, Equal<UsersInRoles.rolename>>>, Where<UsersInRoles.applicationName, Equal<PX.Data.Current<Users.applicationName>>, And<UsersInRoles.username, Equal<PX.Data.Current<Users.username>>>>>.Config>.SelectMultiBound(this._Graph, new object[1]
        {
          e.Row
        }))
        {
          UsersInRoles usersInRoles = (UsersInRoles) pxResult;
          if (this._userService.AllowDeleteRolesOnGuestChange(usersInRoles.Rolename))
          {
            Roles roles = (Roles) pxResult;
            if (roles != null)
            {
              nullable = roles.Guest;
              bool flag4 = true;
              if (!(nullable.GetValueOrDefault() == flag4 & nullable.HasValue))
                this._Graph.Caches[typeof (UsersInRoles)].Delete((object) usersInRoles);
            }
          }
        }
      }
    }
    if (sender.GetStatus((object) row) != PXEntryStatus.Inserted)
      return;
    nullable = oldRow.GeneratePassword;
    bool? generatePassword = row.GeneratePassword;
    if (nullable.GetValueOrDefault() == generatePassword.GetValueOrDefault() & nullable.HasValue == generatePassword.HasValue)
      return;
    PXSelectUsers<TPrimary>.GeneratePassword(row);
  }

  private static void GeneratePassword(Users user)
  {
    Users users = user;
    bool? generatePassword = user.GeneratePassword;
    bool flag = true;
    string str = generatePassword.GetValueOrDefault() == flag & generatePassword.HasValue ? Membership.GeneratePassword(System.Math.Max(SitePolicy.PasswordMinLength, 8), SitePolicy.PasswordComplexity ? 1 : 0) : user.Password;
    users.Password = str;
  }
}
