// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.NameInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
[Serializable]
public sealed class NameInfo
{
  private readonly string _name;
  private readonly ReadOnlyCollection<Login> _logins;
  private readonly string _domainLogin;
  private string _distinguishedName;

  private NameInfo(string name)
  {
    this._name = name;
    int length = name.IndexOf('@');
    if (length < 0)
      return;
    this._name = name.Substring(0, length);
    this._logins = new ReadOnlyCollection<Login>((IEnumerable<Login>) new Login[1]
    {
      new Login(this._name, name.Substring(length + 1))
    });
    this._domainLogin = ActiveDirectoryProvider.GetDomainLogin((IEnumerable<Login>) this._logins);
  }

  public NameInfo(string name, IEnumerable<Login> logins)
  {
    this._name = name;
    this._logins = new ReadOnlyCollection<Login>(logins);
    this._domainLogin = ActiveDirectoryProvider.GetDomainLogin((IEnumerable<Login>) this._logins);
  }

  public NameInfo(string name, string domainLogin, IEnumerable<Login> logins)
  {
    this._name = name;
    this._logins = new ReadOnlyCollection<Login>(logins);
    this._domainLogin = domainLogin;
  }

  public string Name => this._name;

  public string DomainLogin => this._domainLogin;

  public string DistinguishedName
  {
    get
    {
      if (this._distinguishedName == null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Login login in this._logins)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append($"{login.Domain}\\{login.Name}");
        }
        this._distinguishedName = stringBuilder.ToString();
      }
      return this._distinguishedName;
    }
  }

  public ReadOnlyCollection<Login> Logins => this._logins;

  public static string AtToSlash(string name)
  {
    if (name.Contains<char>('\\') || !name.Contains<char>('@'))
      return name;
    string[] strArray = name.Split('@');
    return string.Format("{1}\\{0}", (object) strArray[0], (object) strArray[1]);
  }
}
