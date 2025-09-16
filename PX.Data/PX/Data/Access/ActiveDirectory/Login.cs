// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.Login
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
[Serializable]
public sealed class Login
{
  private readonly string _name;
  private readonly string _domain;
  private string _toString;

  public Login(string name, string domain)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (domain == null)
      throw new ArgumentNullException(nameof (domain));
    this._name = name;
    this._domain = domain;
  }

  public string Name => this._name;

  public string Domain => this._domain;

  public override string ToString()
  {
    if (this._toString == null)
      this._toString = string.IsNullOrEmpty(this._domain) ? this._name : $"{this._domain}\\{this._name}";
    return this._toString;
  }
}
