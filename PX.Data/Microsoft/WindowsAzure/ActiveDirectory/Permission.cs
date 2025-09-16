// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.Permission
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

[DataServiceKey("objectId")]
public class Permission
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _clientId;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _consentType;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DateTime? _expiryTime;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _objectId;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _principalId;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _resourceId;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _scope;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DateTime? _startTime;

  /// <summary>Create a new Permission object.</summary>
  /// <param name="objectId">Initial value of objectId.</param>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public static Permission CreatePermission(string objectId)
  {
    return new Permission() { objectId = objectId };
  }

  /// <summary>
  /// There are no comments for Property clientId in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string clientId
  {
    get => this._clientId;
    set => this._clientId = value;
  }

  /// <summary>
  /// There are no comments for Property consentType in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string consentType
  {
    get => this._consentType;
    set => this._consentType = value;
  }

  /// <summary>
  /// There are no comments for Property expiryTime in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DateTime? expiryTime
  {
    get => this._expiryTime;
    set => this._expiryTime = value;
  }

  /// <summary>
  /// There are no comments for Property objectId in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string objectId
  {
    get => this._objectId;
    set => this._objectId = value;
  }

  /// <summary>
  /// There are no comments for Property principalId in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string principalId
  {
    get => this._principalId;
    set => this._principalId = value;
  }

  /// <summary>
  /// There are no comments for Property resourceId in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string resourceId
  {
    get => this._resourceId;
    set => this._resourceId = value;
  }

  /// <summary>
  /// There are no comments for Property scope in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string scope
  {
    get => this._scope;
    set => this._scope = value;
  }

  /// <summary>
  /// There are no comments for Property startTime in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DateTime? startTime
  {
    get => this._startTime;
    set => this._startTime = value;
  }
}
