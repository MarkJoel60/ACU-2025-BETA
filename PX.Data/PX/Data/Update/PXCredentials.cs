// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXCredentials
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Extensions;
using System;

#nullable disable
namespace PX.Data.Update;

public class PXCredentials
{
  public string UserName { get; set; }

  public string Domain { get; set; }

  public string Login
  {
    get
    {
      return this.UserName + (string.IsNullOrWhiteSpace(this.Domain) ? string.Empty : "@" + this.Domain);
    }
  }

  public string Password { get; set; }

  public PXCredentials(string login, string password)
    : this(PXCredentials.GetUser(login), PXCredentials.GetDomain(login), password)
  {
  }

  public PXCredentials(string userName, string domain, string password)
  {
    this.UserName = !string.IsNullOrWhiteSpace(userName) ? userName : throw new ArgumentNullException(nameof (userName));
    this.Domain = domain;
    this.Password = password;
  }

  public static string GetUser(string login)
  {
    if (string.IsNullOrWhiteSpace(login))
      return (string) null;
    if (login.Contains("@"))
      return StringExtensions.Segment(login, '@', (ushort) 0);
    if (login.Contains("/"))
      return StringExtensions.Segment(login, '/', (ushort) 1);
    return login.Contains("\\") ? StringExtensions.Segment(login, '\\', (ushort) 1) : login;
  }

  public static string GetDomain(string login)
  {
    if (string.IsNullOrWhiteSpace(login))
      return (string) null;
    if (login.Contains("@"))
      return StringExtensions.Segment(login, '@', (ushort) 1);
    if (login.Contains("/"))
      return StringExtensions.Segment(login, '/', (ushort) 0);
    return login.Contains("\\") ? StringExtensions.Segment(login, '\\', (ushort) 0) : (string) null;
  }
}
