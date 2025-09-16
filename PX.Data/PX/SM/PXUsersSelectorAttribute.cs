// Decompiled with JetBrains decompiler
// Type: PX.SM.PXUsersSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Access.ActiveDirectory;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PXUsersSelectorAttribute : PXCustomSelectorAttribute
{
  public const string _DOMAINS_SEPARATOR = "; ";

  [InjectDependencyOnTypeLevel]
  private IActiveDirectoryProvider _activeDirectoryProvider { get; set; }

  public PXUsersSelectorAttribute()
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

  public virtual IEnumerable GetRecords()
  {
    return (IEnumerable) this._activeDirectoryProvider.GetAllUsers(this._Graph, (BqlCommand) new PX.Data.Select<Users, Where<Users.pKID, Equal<Users.pKID>>>());
  }
}
