// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

[DataServiceKey("objectId")]
public class DirectoryObject
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _objectType;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _objectId;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DirectoryObject _createdOnBehalfOf;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<DirectoryObject> _createdObjects = new Collection<DirectoryObject>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DirectoryObject _manager;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<DirectoryObject> _directReports = new Collection<DirectoryObject>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<DirectoryObject> _members = new Collection<DirectoryObject>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<DirectoryObject> _memberOf = new Collection<DirectoryObject>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<DirectoryObject> _owners = new Collection<DirectoryObject>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<DirectoryObject> _ownedObjects = new Collection<DirectoryObject>();

  /// <summary>Create a new DirectoryObject object.</summary>
  /// <param name="objectId">Initial value of objectId.</param>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public static DirectoryObject CreateDirectoryObject(string objectId)
  {
    return new DirectoryObject() { objectId = objectId };
  }

  /// <summary>
  /// There are no comments for Property objectType in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string objectType
  {
    get => this._objectType;
    set => this._objectType = value;
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
  /// There are no comments for createdOnBehalfOf in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DirectoryObject createdOnBehalfOf
  {
    get => this._createdOnBehalfOf;
    set => this._createdOnBehalfOf = value;
  }

  /// <summary>
  /// There are no comments for createdObjects in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<DirectoryObject> createdObjects
  {
    get => this._createdObjects;
    set
    {
      if (value == null)
        return;
      this._createdObjects = value;
    }
  }

  /// <summary>There are no comments for manager in the schema.</summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DirectoryObject manager
  {
    get => this._manager;
    set => this._manager = value;
  }

  /// <summary>
  /// There are no comments for directReports in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<DirectoryObject> directReports
  {
    get => this._directReports;
    set
    {
      if (value == null)
        return;
      this._directReports = value;
    }
  }

  /// <summary>There are no comments for members in the schema.</summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<DirectoryObject> members
  {
    get => this._members;
    set
    {
      if (value == null)
        return;
      this._members = value;
    }
  }

  /// <summary>There are no comments for memberOf in the schema.</summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<DirectoryObject> memberOf
  {
    get => this._memberOf;
    set
    {
      if (value == null)
        return;
      this._memberOf = value;
    }
  }

  /// <summary>There are no comments for owners in the schema.</summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<DirectoryObject> owners
  {
    get => this._owners;
    set
    {
      if (value == null)
        return;
      this._owners = value;
    }
  }

  /// <summary>There are no comments for ownedObjects in the schema.</summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<DirectoryObject> ownedObjects
  {
    get => this._ownedObjects;
    set
    {
      if (value == null)
        return;
      this._ownedObjects = value;
    }
  }
}
