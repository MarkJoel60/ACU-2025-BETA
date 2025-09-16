// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.Role
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.CodeDom.Compiler;
using System.Data.Services.Common;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

[DataServiceKey("objectId")]
public class Role : DirectoryObject
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _description;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _displayName;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _isSystem;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _roleDisabled;

  /// <summary>Create a new Role object.</summary>
  /// <param name="objectId">Initial value of objectId.</param>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public static Role CreateRole(string objectId)
  {
    Role role = new Role();
    role.objectId = objectId;
    return role;
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
  /// There are no comments for Property displayName in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string displayName
  {
    get => this._displayName;
    set => this._displayName = value;
  }

  /// <summary>
  /// There are no comments for Property isSystem in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? isSystem
  {
    get => this._isSystem;
    set => this._isSystem = value;
  }

  /// <summary>
  /// There are no comments for Property roleDisabled in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? roleDisabled
  {
    get => this._roleDisabled;
    set => this._roleDisabled = value;
  }
}
