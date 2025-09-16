// Decompiled with JetBrains decompiler
// Type: PX.SM.PXADUsersSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Access.ActiveDirectory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class PXADUsersSelectorAttribute : PXCustomSelectorAttribute
{
  [InjectDependencyOnTypeLevel]
  private IActiveDirectoryProvider _activeDirectoryProvider { get; set; }

  public PXADUsersSelectorAttribute()
    : base(typeof (Users.username), typeof (Users.username), typeof (Users.displayName), typeof (Users.guest), typeof (Users.state))
  {
    base.DescriptionField = typeof (Users.fullName);
  }

  public override System.Type DescriptionField
  {
    get => base.DescriptionField;
    set
    {
    }
  }

  public bool UseCached { get; set; } = true;

  public virtual IEnumerable GetRecords()
  {
    PXADUsersSelectorAttribute selectorAttribute = this;
    List<Users> list = PXSelectBase<Users, PXSelect<Users, Where<Users.source, Equal<PXUsersSourceListAttribute.activeDirectory>>>.Config>.Select(selectorAttribute._Graph).RowCast<Users>().ToList<Users>();
    HashSet<string> logins = new HashSet<string>();
    foreach (Users users in list)
      logins.Add(users.ExtRef);
    foreach (PX.Data.Access.ActiveDirectory.User user in selectorAttribute._activeDirectoryProvider.GetUsers(selectorAttribute.UseCached))
    {
      Users instance = (Users) selectorAttribute._Graph.Caches[typeof (Users)].CreateInstance();
      instance.Fill(user);
      if (!logins.Contains(instance.ExtRef))
        yield return (object) instance;
    }
  }
}
