// Decompiled with JetBrains decompiler
// Type: PX.Data.companySetting
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[Obsolete("Will be removed from public API in 2022 R2")]
public class companySetting
{
  internal readonly companySetting.companyFlag Flag;
  internal readonly string Identity;
  internal readonly string WebAppType;
  internal readonly string Deleted;
  internal readonly string RecordStatus;
  internal readonly string Branch;
  internal readonly string Modified;
  internal readonly string ModifiedBy;
  internal readonly string TimeTag;
  internal readonly bool TableNotFound;

  [Obsolete("Will be removed from public API in 2022 R2")]
  public companySetting(
    companySetting.companyFlag flag,
    string identity,
    string deleted,
    string branch,
    string modified,
    string modifiedBy,
    string timetag)
    : this(flag, identity, deleted, branch, modified, modifiedBy, timetag, nameof (WebAppType), false, (string) null)
  {
  }

  internal companySetting(companySetting.companyFlag flag)
    : this(flag, (string) null, (string) null, (string) null, (string) null, (string) null, (string) null, (string) null, false, (string) null)
  {
  }

  internal companySetting(companySetting.companyFlag flag, bool tableNotFound)
    : this(flag, (string) null, (string) null, (string) null, (string) null, (string) null, (string) null, (string) null, tableNotFound, (string) null)
  {
  }

  internal companySetting(
    companySetting.companyFlag flag,
    string identity,
    string deleted,
    string branch,
    string modified,
    string modifiedBy,
    string timetag,
    string webAppType,
    string recordStatus)
    : this(flag, identity, deleted, branch, modified, modifiedBy, timetag, webAppType, false, recordStatus)
  {
  }

  internal companySetting(
    companySetting.companyFlag flag,
    string identity,
    string deleted,
    string branch,
    string modified,
    string modifiedBy,
    string timetag,
    string webAppType,
    bool tableNotFound,
    string recordStatus)
  {
    this.Flag = flag;
    this.Identity = identity;
    this.WebAppType = webAppType;
    this.Deleted = deleted;
    this.Branch = branch;
    this.Modified = modified;
    this.ModifiedBy = modifiedBy;
    this.TimeTag = timetag;
    this.TableNotFound = tableNotFound;
    this.RecordStatus = recordStatus;
  }

  [Obsolete("Will be removed from public API in 2022 R2")]
  public companySetting copyAsDedicated()
  {
    return new companySetting(companySetting.companyFlag.Dedicated, this.Identity, this.Deleted, this.Branch, this.Modified, this.ModifiedBy, this.TimeTag, this.WebAppType, this.TableNotFound, this.RecordStatus);
  }

  [Obsolete("Will be removed from public API in 2022 R2")]
  public static bool NeedRestrict(companySetting setting)
  {
    if (setting == null)
      return false;
    return setting.Flag == companySetting.companyFlag.UserGlobal || setting.Flag == companySetting.companyFlag.Dedicated;
  }

  /// <exclude />
  [Obsolete("Will be removed from public API in 2022 R2")]
  public enum companyFlag
  {
    Separate,
    Shared,
    Global,
    UserGlobal,
    Dedicated,
  }
}
