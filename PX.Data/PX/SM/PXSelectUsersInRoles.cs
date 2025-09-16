// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectUsersInRoles
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

public class PXSelectUsersInRoles : PXSelectBase<UsersInRoles>
{
  public PXSelectUsersInRoles(PXGraph graph, Delegate handler)
    : this(graph)
  {
    this.View = new PXView(this._Graph, false, this.View.BqlSelect, handler);
  }

  public PXSelectUsersInRoles(PXGraph graph)
  {
    this._Graph = graph;
    this.View = new PXView(this._Graph, false, (BqlCommand) new PX.Data.Select<UsersInRoles, Where<UsersInRoles.applicationName, Equal<PX.Data.Current<Users.applicationName>>, And<UsersInRoles.username, Equal<PX.Data.Current<Users.username>>>>>());
    this._Graph.RowPersisting.AddHandler(typeof (UsersInRoles), new PXRowPersisting(this.RowPersisting));
    this._Graph.FieldVerifying.AddHandler(typeof (UsersInRoles), typeof (UsersInRoles.rolename).Name, new PXFieldVerifying(this.RolenameFieldVerifying));
  }

  protected virtual void RolenameFieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    Users users = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Select(cache.Graph, (object) ((UsersInRoles) e.Row).Username);
    if (users == null)
      return;
    bool? guest1 = users.Guest;
    bool flag1 = true;
    if (!(guest1.GetValueOrDefault() == flag1 & guest1.HasValue))
      return;
    Roles roles = (Roles) PXSelectorAttribute.Select(cache, e.Row, typeof (UsersInRoles.rolename).Name, e.NewValue);
    if (roles == null)
      return;
    bool? guest2 = roles.Guest;
    bool flag2 = true;
    if (guest2.GetValueOrDefault() == flag2 & guest2.HasValue)
      return;
    cache.RaiseExceptionHandling(typeof (UsersInRoles.rolename).Name, e.Row, e.NewValue, new Exception("Only guest roles are allowed for a guest user."));
  }

  internal virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is UsersInRoles row))
      return;
    Users current = (Users) this._Graph.Caches[typeof (Users)].Current;
    if (row.Username != null || e.Operation != PXDBOperation.Insert || current == null)
      return;
    row.Username = current.Username;
  }
}
