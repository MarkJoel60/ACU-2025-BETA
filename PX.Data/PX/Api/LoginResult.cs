// Decompiled with JetBrains decompiler
// Type: PX.Api.LoginResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api;

/// <summary>The result of login operation</summary>
public class LoginResult
{
  public LoginResult.ErrorCode Code;
  public string Message;
  public string Session;

  public enum ErrorCode
  {
    OK,
    INVALID_CREDENTIALS,
    INTERNAL_ERROR,
    INVALID_API_VERSION,
  }
}
