// Decompiled with JetBrains decompiler
// Type: PX.Data.MembershipUserExt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Web.Security;

#nullable disable
namespace PX.Data;

/// <exclude />
public class MembershipUserExt : MembershipUser
{
  public int Source { get; private set; }

  public string FirstName { get; private set; }

  public string LastName { get; private set; }

  public string DisplayName { get; private set; }

  public bool Guest { get; private set; }

  public bool IsPendingActivation { get; private set; }

  public MembershipUserExt(
    string providerName,
    string name,
    object providerUserKey,
    string email,
    string passwordQuestion,
    string comment,
    bool isApproved,
    bool isLockedOut,
    bool isPendingActivation,
    System.DateTime creationDate,
    System.DateTime lastLoginDate,
    System.DateTime lastActivityDate,
    System.DateTime lastPasswordChangedDate,
    System.DateTime lastLockoutDate,
    int source,
    string firstName,
    string lastName,
    string displayName,
    bool guest)
    : base(providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
  {
    this.Source = source;
    this.FirstName = firstName;
    this.LastName = lastName;
    this.DisplayName = string.IsNullOrEmpty(displayName) ? name : displayName;
    this.Guest = guest;
    this.IsPendingActivation = isPendingActivation;
  }
}
