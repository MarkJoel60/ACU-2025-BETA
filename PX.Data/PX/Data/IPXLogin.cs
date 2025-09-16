// Decompiled with JetBrains decompiler
// Type: PX.Data.IPXLogin
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Security.Claims;
using System.Web;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public interface IPXLogin
{
  ClaimsPrincipal Authenticate(string username, string password);

  bool LoginUser(ref string userName, string password);

  void TrackLogin(string userName);

  void TrackLoginWithPasswordChange(string userName);

  void FinishLogin(
    HttpContextBase httpContext,
    string userName,
    string companyName,
    string branch);

  void TrackAndFinishLogin(
    HttpContextBase httpContext,
    string username,
    string company,
    string branch);

  void LogoutUser(string userName, AspNetSession session);

  void ResetCompanySpecificUserInfo();

  bool SwitchCompany(string companyId, out string redirectUrl);

  void SwitchCulture(string loginName, string companyId);

  void SetBranchId(string userName, string company, string branch);

  void InitUserEnvironment(string userName, string localeName, bool initBranch = true);

  /// <summary>
  /// Gets default branch for username@company (see <see cref="M:PX.SM.SMAccessPersonalMaint.GetDefaultBranchId(System.String,System.String)" />),
  /// then sets it to context and writes to a cookie
  /// </summary>
  void InitBranch(string username, string company);

  bool IsPasswordChangeRequired(
    int? source,
    string password,
    bool shouldChangeOnNextLogin,
    bool isPasswordNeverExpired,
    System.DateTime? lastPassworChangeDate);

  void SessionExpired(HttpApplication application, string userName);
}
