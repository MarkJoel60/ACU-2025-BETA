// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RuleValidationErrorType
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
public class RuleValidationErrorType
{
  private RuleFieldURIType fieldURIField;
  private RuleValidationErrorCodeType errorCodeField;
  private string errorMessageField;
  private string fieldValueField;

  /// <remarks />
  public RuleFieldURIType FieldURI
  {
    get => this.fieldURIField;
    set => this.fieldURIField = value;
  }

  /// <remarks />
  public RuleValidationErrorCodeType ErrorCode
  {
    get => this.errorCodeField;
    set => this.errorCodeField = value;
  }

  /// <remarks />
  public string ErrorMessage
  {
    get => this.errorMessageField;
    set => this.errorMessageField = value;
  }

  /// <remarks />
  public string FieldValue
  {
    get => this.fieldValueField;
    set => this.fieldValueField = value;
  }
}
