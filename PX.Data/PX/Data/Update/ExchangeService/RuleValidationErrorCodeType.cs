// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RuleValidationErrorCodeType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum RuleValidationErrorCodeType
{
  /// <remarks />
  ADOperationFailure,
  /// <remarks />
  ConnectedAccountNotFound,
  /// <remarks />
  CreateWithRuleId,
  /// <remarks />
  EmptyValueFound,
  /// <remarks />
  DuplicatedPriority,
  /// <remarks />
  DuplicatedOperationOnTheSameRule,
  /// <remarks />
  FolderDoesNotExist,
  /// <remarks />
  InvalidAddress,
  /// <remarks />
  InvalidDateRange,
  /// <remarks />
  InvalidFolderId,
  /// <remarks />
  InvalidSizeRange,
  /// <remarks />
  InvalidValue,
  /// <remarks />
  MessageClassificationNotFound,
  /// <remarks />
  MissingAction,
  /// <remarks />
  MissingParameter,
  /// <remarks />
  MissingRangeValue,
  /// <remarks />
  NotSettable,
  /// <remarks />
  RecipientDoesNotExist,
  /// <remarks />
  RuleNotFound,
  /// <remarks />
  SizeLessThanZero,
  /// <remarks />
  StringValueTooBig,
  /// <remarks />
  UnsupportedAddress,
  /// <remarks />
  UnexpectedError,
  /// <remarks />
  UnsupportedRule,
}
