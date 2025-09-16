// Decompiled with JetBrains decompiler
// Type: PX.Data.ILoginUiService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Web;

#nullable disable
namespace PX.Data;

/// <remarks> This service is intended to be used only from the UI context, such as login/error pages.</remarks>
/// <exclude />
public interface ILoginUiService
{
  string[] GetCompanies(string userName, string password);

  bool LoginUser(ref string userName, string password);

  void LoginUser(ref string userName, string oldPassword, string newPassword);

  void InitUserEnvironment(string userName, string localeName);

  void LogoutCurrentUser();

  void LogoutUser(string userName);

  string FindUserByHash(string hash, string login);

  string FindQuestionByUsername(string username, string login);

  bool ValidateAnswer(string userName, string answer);

  /// <param name="company">Company where to look for the user by <paramref name="userEmail" />.</param>
  void SendUserLogin(string company, string userEmail, string linkToSend);

  /// <param name="paramName">Name of the parameter which will be used in query string and will contain unique recovery number.</param>
  void SendUserPassword(string userLogin, string linkToSend, string paramName);

  bool EulaRequired(string userName);

  void AgreeToEula(string userName);

  string GetCompanyIdFromCookie(HttpRequest request);

  void ClearCompanyIdInCookie(HttpResponse response);

  string GetPathWithoutCompanyId(HttpRequest request);
}
