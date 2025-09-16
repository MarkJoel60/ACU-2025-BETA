// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.NonIndexableItemDetailType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class NonIndexableItemDetailType
{
  private ItemIdType itemIdField;
  private ItemIndexErrorType errorCodeField;
  private string errorDescriptionField;
  private bool isPartiallyIndexedField;
  private bool isPermanentFailureField;
  private string sortValueField;
  private int attemptCountField;
  private System.DateTime lastAttemptTimeField;
  private bool lastAttemptTimeFieldSpecified;
  private string additionalInfoField;

  /// <remarks />
  public ItemIdType ItemId
  {
    get => this.itemIdField;
    set => this.itemIdField = value;
  }

  /// <remarks />
  public ItemIndexErrorType ErrorCode
  {
    get => this.errorCodeField;
    set => this.errorCodeField = value;
  }

  /// <remarks />
  public string ErrorDescription
  {
    get => this.errorDescriptionField;
    set => this.errorDescriptionField = value;
  }

  /// <remarks />
  public bool IsPartiallyIndexed
  {
    get => this.isPartiallyIndexedField;
    set => this.isPartiallyIndexedField = value;
  }

  /// <remarks />
  public bool IsPermanentFailure
  {
    get => this.isPermanentFailureField;
    set => this.isPermanentFailureField = value;
  }

  /// <remarks />
  public string SortValue
  {
    get => this.sortValueField;
    set => this.sortValueField = value;
  }

  /// <remarks />
  public int AttemptCount
  {
    get => this.attemptCountField;
    set => this.attemptCountField = value;
  }

  /// <remarks />
  public System.DateTime LastAttemptTime
  {
    get => this.lastAttemptTimeField;
    set => this.lastAttemptTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool LastAttemptTimeSpecified
  {
    get => this.lastAttemptTimeFieldSpecified;
    set => this.lastAttemptTimeFieldSpecified = value;
  }

  /// <remarks />
  public string AdditionalInfo
  {
    get => this.additionalInfoField;
    set => this.additionalInfoField = value;
  }
}
