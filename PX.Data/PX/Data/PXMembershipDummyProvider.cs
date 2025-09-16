// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMembershipDummyProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Web.Security;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class PXMembershipDummyProvider : PXBaseMembershipProvider
{
  public override string Name => nameof (PXMembershipDummyProvider);

  public override MembershipUser CreateUser(
    string username,
    string password,
    string email,
    string passwordQuestion,
    string passwordAnswer,
    bool isApproved,
    object providerUserKey,
    out MembershipCreateStatus status)
  {
    status = MembershipCreateStatus.ProviderError;
    return (MembershipUser) null;
  }

  public override bool ChangePasswordQuestionAndAnswer(
    string username,
    string password,
    string newPasswordQuestion,
    string newPasswordAnswer)
  {
    return false;
  }

  public override string GetPassword(string username, string answer) => (string) null;

  public override bool ChangePassword(string username, string oldPassword, string newPassword)
  {
    return false;
  }

  public override string ResetPassword(string username, string answer) => (string) null;

  public override void UpdateUser(MembershipUser user)
  {
  }

  public override bool ValidateUser(string username, string password) => false;

  public override bool UnlockUser(string userName) => false;

  public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
  {
    return (MembershipUser) null;
  }

  public override MembershipUser GetUser(string username, bool userIsOnline)
  {
    return (MembershipUser) null;
  }

  public override string GetUserNameByEmail(string email) => (string) null;

  public override bool DeleteUser(string username, bool deleteAllRelatedData) => false;

  public override MembershipUserCollection GetAllUsers(
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    totalRecords = 0;
    return new MembershipUserCollection();
  }

  public override int GetNumberOfUsersOnline() => 0;

  public override MembershipUserCollection FindUsersByName(
    string usernameToMatch,
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    totalRecords = 0;
    return new MembershipUserCollection();
  }

  public override MembershipUserCollection FindUsersByEmail(
    string emailToMatch,
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    totalRecords = 0;
    return new MembershipUserCollection();
  }

  protected internal override bool ValidateUserPassword(
    string username,
    string password,
    bool onlyAllowed,
    bool skipForbidLoginWithPasswordCheck = false)
  {
    return true;
  }

  protected internal override void UpdateUserPassword(string username, string password)
  {
  }

  public override bool EnablePasswordRetrieval => false;

  public override bool EnablePasswordReset => false;

  public override bool RequiresQuestionAndAnswer => false;

  public override string ApplicationName
  {
    get => (string) null;
    set
    {
    }
  }

  public override int MaxInvalidPasswordAttempts => 0;

  public override int PasswordAttemptWindow => 0;

  public override bool RequiresUniqueEmail => false;

  public override MembershipPasswordFormat PasswordFormat => MembershipPasswordFormat.Clear;

  public override int MinRequiredPasswordLength => 0;

  public override int MinRequiredNonAlphanumericCharacters => 0;

  public override string PasswordStrengthRegularExpression => (string) null;

  public override bool ConcurrentUserMode => false;
}
