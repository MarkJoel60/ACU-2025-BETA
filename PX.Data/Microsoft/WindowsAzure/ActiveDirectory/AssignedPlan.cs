// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.AssignedPlan
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

public class AssignedPlan
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DateTime? _assignedTimestamp;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _capabilityStatus;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _service;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Guid? _servicePlanId;

  /// <summary>
  /// There are no comments for Property assignedTimestamp in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DateTime? assignedTimestamp
  {
    get => this._assignedTimestamp;
    set => this._assignedTimestamp = value;
  }

  /// <summary>
  /// There are no comments for Property capabilityStatus in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string capabilityStatus
  {
    get => this._capabilityStatus;
    set => this._capabilityStatus = value;
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
  /// There are no comments for Property servicePlanId in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Guid? servicePlanId
  {
    get => this._servicePlanId;
    set => this._servicePlanId = value;
  }
}
