// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.PasswordProfile
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.CodeDom.Compiler;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

public class PasswordProfile
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _password;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _forceChangePasswordNextLogin;

  /// <summary>
  /// There are no comments for Property password in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string password
  {
    get => this._password;
    set => this._password = value;
  }

  /// <summary>
  /// There are no comments for Property forceChangePasswordNextLogin in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? forceChangePasswordNextLogin
  {
    get => this._forceChangePasswordNextLogin;
    set => this._forceChangePasswordNextLogin = value;
  }
}
