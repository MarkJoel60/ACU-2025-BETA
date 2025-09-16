// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.MultiFactorServiceHttpRequestConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using PX.AspNetCore;
using System;
using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.Data.MultiFactorAuth;

internal sealed class MultiFactorServiceHttpRequestConverter : IMultiFactorService
{
  private readonly IMultiFactorService _impl;

  public MultiFactorServiceHttpRequestConverter(IMultiFactorService impl) => this._impl = impl;

  bool IMultiFactorService.IsAccessCodeValid(
    string login,
    string password,
    string oneTimePassword,
    object request,
    out Tuple<int, Guid, bool> user,
    out ErrorReason? errorReason)
  {
    return !(request is HttpRequest httpRequest) ? this._impl.IsAccessCodeValid(login, password, oneTimePassword, request, out user, out errorReason) : this._impl.IsAccessCodeValid(login, password, oneTimePassword, (object) httpRequest.RequestContext.HttpContext.GetCoreHttpContext().Request, out user, out errorReason);
  }

  IReadOnlyDictionary<string, ITwoFactorSender> IMultiFactorService.GetTwoFactorSenders()
  {
    return this._impl.GetTwoFactorSenders();
  }

  StartTwoFactorPipelineResult IMultiFactorService.SendAcceptRequest(
    IEnumerable<(int companyId, Guid userId, int twoFactorLevel, bool isPasswordChanging)> users,
    string innerCorrelation,
    Dictionary<string, string> customData,
    string secondFactorType)
  {
    return this._impl.SendAcceptRequest(users, innerCorrelation, customData, secondFactorType);
  }

  string IMultiFactorService.GetCookieName(string login) => this._impl.GetCookieName(login);

  bool IMultiFactorService.IsAccessCodeValid(int companyId, Guid userId, string oneTimePassword)
  {
    return this._impl.IsAccessCodeValid(companyId, userId, oneTimePassword);
  }

  IEnumerable<string> IMultiFactorService.GeneratePersistentCodes(
    Guid userId,
    int count,
    int lifetime)
  {
    return this._impl.GeneratePersistentCodes(userId, count, lifetime);
  }

  IReadOnlyList<(int companyId, Guid userId, int twoFactorLevel, bool isPasswordChange)> IMultiFactorService.GetUserIdsWithTwoFactorType(
    string login,
    string password,
    out bool isMultiFactorEnabled,
    out string[] multiFactorProviders,
    out bool isPasswordChanging)
  {
    return this._impl.GetUserIdsWithTwoFactorType(login, password, out isMultiFactorEnabled, out multiFactorProviders, out isPasswordChanging);
  }

  OtpConfiguration IMultiFactorService.GetConfiguration() => this._impl.GetConfiguration();

  void IMultiFactorService.RememberDevice(string login, string password, HttpContext context)
  {
    this._impl.RememberDevice(login, password, context);
  }

  bool IMultiFactorService.CheckPersistentCode(string oneTimePassword)
  {
    return this._impl.CheckPersistentCode(oneTimePassword);
  }

  void IMultiFactorService.SendRegistrationPersistentCode()
  {
    this._impl.SendRegistrationPersistentCode();
  }
}
