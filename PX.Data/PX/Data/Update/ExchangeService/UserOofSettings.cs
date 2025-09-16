// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UserOofSettings
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
public class UserOofSettings
{
  private OofState oofStateField;
  private ExternalAudience externalAudienceField;
  private Duration durationField;
  private ReplyBody internalReplyField;
  private ReplyBody externalReplyField;

  /// <remarks />
  public OofState OofState
  {
    get => this.oofStateField;
    set => this.oofStateField = value;
  }

  /// <remarks />
  public ExternalAudience ExternalAudience
  {
    get => this.externalAudienceField;
    set => this.externalAudienceField = value;
  }

  /// <remarks />
  public Duration Duration
  {
    get => this.durationField;
    set => this.durationField = value;
  }

  /// <remarks />
  public ReplyBody InternalReply
  {
    get => this.internalReplyField;
    set => this.internalReplyField = value;
  }

  /// <remarks />
  public ReplyBody ExternalReply
  {
    get => this.externalReplyField;
    set => this.externalReplyField = value;
  }
}
