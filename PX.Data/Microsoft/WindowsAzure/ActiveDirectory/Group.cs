// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.Group
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

[DataServiceKey("objectId")]
public class Group : DirectoryObject
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _description;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _dirSyncEnabled;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _displayName;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DateTime? _lastDirSyncTime;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _mail;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _mailNickname;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _mailEnabled;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<string> _proxyAddresses = new Collection<string>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _securityEnabled;

  /// <summary>Create a new Group object.</summary>
  /// <param name="objectId">Initial value of objectId.</param>
  /// <param name="provisioningErrors">Initial value of provisioningErrors.</param>
  /// <param name="proxyAddresses">Initial value of proxyAddresses.</param>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public static Group CreateGroup(
    string objectId,
    Collection<ProvisioningError> provisioningErrors,
    Collection<string> proxyAddresses)
  {
    Group group = new Group();
    group.objectId = objectId;
    group.provisioningErrors = provisioningErrors != null ? provisioningErrors : throw new ArgumentNullException(nameof (provisioningErrors));
    group.proxyAddresses = proxyAddresses != null ? proxyAddresses : throw new ArgumentNullException(nameof (proxyAddresses));
    return group;
  }

  /// <summary>
  /// There are no comments for Property description in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string description
  {
    get => this._description;
    set => this._description = value;
  }

  /// <summary>
  /// There are no comments for Property dirSyncEnabled in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? dirSyncEnabled
  {
    get => this._dirSyncEnabled;
    set => this._dirSyncEnabled = value;
  }

  /// <summary>
  /// There are no comments for Property displayName in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string displayName
  {
    get => this._displayName;
    set => this._displayName = value;
  }

  /// <summary>
  /// There are no comments for Property lastDirSyncTime in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DateTime? lastDirSyncTime
  {
    get => this._lastDirSyncTime;
    set => this._lastDirSyncTime = value;
  }

  /// <summary>
  /// There are no comments for Property mail in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string mail
  {
    get => this._mail;
    set => this._mail = value;
  }

  /// <summary>
  /// There are no comments for Property mailNickname in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string mailNickname
  {
    get => this._mailNickname;
    set => this._mailNickname = value;
  }

  /// <summary>
  /// There are no comments for Property mailEnabled in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? mailEnabled
  {
    get => this._mailEnabled;
    set => this._mailEnabled = value;
  }

  /// <summary>
  /// There are no comments for Property provisioningErrors in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<ProvisioningError> provisioningErrors
  {
    get => this._provisioningErrors;
    set => this._provisioningErrors = value;
  }

  /// <summary>
  /// There are no comments for Property proxyAddresses in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<string> proxyAddresses
  {
    get => this._proxyAddresses;
    set => this._proxyAddresses = value;
  }

  /// <summary>
  /// There are no comments for Property securityEnabled in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? securityEnabled
  {
    get => this._securityEnabled;
    set => this._securityEnabled = value;
  }
}
