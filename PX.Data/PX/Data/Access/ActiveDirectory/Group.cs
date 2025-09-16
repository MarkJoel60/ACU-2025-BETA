// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.Group
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
public sealed class Group
{
  private readonly string _sid;
  private readonly string _cn;
  private readonly string _dc;
  private readonly string _displayName;
  private readonly string _description;
  private string _rid;

  public Group(string sid, string cn, string dc, string name, string description)
  {
    this._sid = sid;
    this._cn = cn;
    this._dc = dc;
    this._displayName = name;
    this._description = description;
  }

  public string SID => this._sid;

  public string RID => this._rid ?? (this._rid = Group.GetRID(this._sid));

  public static string GetRID(string sid)
  {
    string rid = string.Empty;
    int num = sid.LastIndexOf('-');
    if (num > -1 && num < sid.Length - 1)
      rid = sid.Substring(num + 1);
    return rid;
  }

  public string CN => this._cn;

  public string DC => this._dc;

  public string DisplayName => this._displayName;

  public string Description => this._description;
}
