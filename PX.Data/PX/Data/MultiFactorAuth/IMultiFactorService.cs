// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.IMultiFactorService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.MultiFactorAuth;

public interface IMultiFactorService
{
  IReadOnlyDictionary<string, ITwoFactorSender> GetTwoFactorSenders();

  StartTwoFactorPipelineResult SendAcceptRequest(
    IEnumerable<(int companyId, Guid userId, int twoFactorLevel, bool isPasswordChanging)> users,
    string innerCorrelation,
    Dictionary<string, string> customData,
    string secondFactorType);

  /// <summary>Method returns name for remember device cookie</summary>
  /// <param name="login"></param>
  /// <returns></returns>
  string GetCookieName(string login);

  /// <summary>Checks if one time code is valid.</summary>
  /// <returns></returns>
  bool IsAccessCodeValid(int companyId, Guid userId, string oneTimePassword);

  /// <summary>Checks if one time code is valid.</summary>
  /// <returns></returns>
  bool IsAccessCodeValid(
    string login,
    string password,
    string oneTimePassword,
    object request,
    out Tuple<int, Guid, bool> user,
    out ErrorReason? errorReason);

  /// <summary>
  /// Returns generated access codes to be used as one time access tokens. They are valid for lifetime days.
  /// </summary>
  /// <param name="count">count of access codes to be generated</param>
  /// <param name="lifetime">Number of days for codes to be valid</param>
  /// <returns></returns>
  IEnumerable<string> GeneratePersistentCodes(Guid userId, int count, int lifetime);

  /// <summary>
  /// Returns users list by login and password with multifactor preferences
  /// </summary>
  /// <param name="login"></param>
  /// <param name="password"></param>
  /// <param name="isMultiFactorEnabled"></param>
  /// <param name="multiFactorProviders"></param>
  /// <param name="isPasswordChanging"></param>
  /// <returns></returns>
  IReadOnlyList<(int companyId, Guid userId, int twoFactorLevel, bool isPasswordChange)> GetUserIdsWithTwoFactorType(
    string login,
    string password,
    out bool isMultiFactorEnabled,
    out string[] multiFactorProviders,
    out bool isPasswordChanging);

  OtpConfiguration GetConfiguration();

  void RememberDevice(string login, string password, HttpContext context);

  bool CheckPersistentCode(string oneTimePassword);

  void SendRegistrationPersistentCode();
}
