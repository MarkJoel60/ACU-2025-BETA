// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.Credential
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
public sealed class Credential
{
  private readonly string _login;
  private readonly string _password;

  public Credential(string login, string password)
  {
    this._login = login;
    this._password = password;
  }

  public string Login => this._login;

  public string Password => this._password;
}
