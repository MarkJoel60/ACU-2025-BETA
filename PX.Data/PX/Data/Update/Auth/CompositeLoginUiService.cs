// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Auth.CompositeLoginUiService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SP;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Data.Update.Auth;

/// <summary>
/// Wraps ERP and Portal implementations of <see cref="T:PX.Data.ILoginUiService" /> and calls the correct one according to current context.
/// </summary>
/// <summary>
/// Wraps ERP and Portal implementations of <see cref="T:PX.Data.ILoginUiService" /> and calls the correct one according to current context.
/// </summary>
internal sealed class CompositeLoginUiService(IEnumerable<ILoginUiService> implementations) : 
  ILoginUiService
{
  public void AgreeToEula(string userName) => this.GetImplementation().AgreeToEula(userName);

  public void ClearCompanyIdInCookie(HttpResponse response)
  {
    this.GetImplementation().ClearCompanyIdInCookie(response);
  }

  public bool EulaRequired(string userName) => this.GetImplementation().EulaRequired(userName);

  public string FindQuestionByUsername(string username, string login)
  {
    return this.GetImplementation().FindQuestionByUsername(username, login);
  }

  public string FindUserByHash(string hash, string login)
  {
    return this.GetImplementation().FindUserByHash(hash, login);
  }

  public string[] GetCompanies(string userName, string password)
  {
    return this.GetImplementation().GetCompanies(userName, password);
  }

  public string GetCompanyIdFromCookie(HttpRequest request)
  {
    return this.GetImplementation().GetCompanyIdFromCookie(request);
  }

  public string GetPathWithoutCompanyId(HttpRequest request)
  {
    return this.GetImplementation().GetPathWithoutCompanyId(request);
  }

  public void InitUserEnvironment(string userName, string localeName)
  {
    this.GetImplementation().InitUserEnvironment(userName, localeName);
  }

  public bool LoginUser(ref string userName, string password)
  {
    return this.GetImplementation().LoginUser(ref userName, password);
  }

  public void LoginUser(ref string userName, string oldPassword, string newPassword)
  {
    this.GetImplementation().LoginUser(ref userName, oldPassword, newPassword);
  }

  public void LogoutCurrentUser() => this.GetImplementation().LogoutCurrentUser();

  public void LogoutUser(string userName) => this.GetImplementation().LogoutUser(userName);

  public void SendUserLogin(string company, string userEmail, string linkToSend)
  {
    this.GetImplementation().SendUserLogin(company, userEmail, linkToSend);
  }

  public void SendUserPassword(string userLogin, string linkToSend, string paramName)
  {
    this.GetImplementation().SendUserPassword(userLogin, linkToSend, paramName);
  }

  public bool ValidateAnswer(string userName, string answer)
  {
    return this.GetImplementation().ValidateAnswer(userName, answer);
  }

  private ILoginUiService GetImplementation()
  {
    return !PortalHelper.IsPortalContext(PortalContexts.Modern) ? (ILoginUiService) implementations.OfType<ErpLoginUiService>().First<ErpLoginUiService>() : (ILoginUiService) implementations.OfType<PortalLoginUiService>().First<PortalLoginUiService>();
  }
}
