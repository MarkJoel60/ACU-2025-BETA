// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MailTipTypes
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
[Flags]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum MailTipTypes
{
  /// <remarks />
  All = 1,
  /// <remarks />
  OutOfOfficeMessage = 2,
  /// <remarks />
  MailboxFullStatus = 4,
  /// <remarks />
  CustomMailTip = 8,
  /// <remarks />
  ExternalMemberCount = 16, // 0x00000010
  /// <remarks />
  TotalMemberCount = 32, // 0x00000020
  /// <remarks />
  MaxMessageSize = 64, // 0x00000040
  /// <remarks />
  DeliveryRestriction = 128, // 0x00000080
  /// <remarks />
  ModerationStatus = 256, // 0x00000100
  /// <remarks />
  InvalidRecipient = 512, // 0x00000200
  /// <remarks />
  Scope = 1024, // 0x00000400
}
