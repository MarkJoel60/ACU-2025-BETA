// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

public class ProvisioningError
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _errorDetail;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _resolved;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _service;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DateTime? _timestamp;

  /// <summary>
  /// There are no comments for Property errorDetail in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string errorDetail
  {
    get => this._errorDetail;
    set => this._errorDetail = value;
  }

  /// <summary>
  /// There are no comments for Property resolved in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? resolved
  {
    get => this._resolved;
    set => this._resolved = value;
  }

  /// <summary>
  /// There are no comments for Property service in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string service
  {
    get => this._service;
    set => this._service = value;
  }

  /// <summary>
  /// There are no comments for Property timestamp in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DateTime? timestamp
  {
    get => this._timestamp;
    set => this._timestamp = value;
  }
}
