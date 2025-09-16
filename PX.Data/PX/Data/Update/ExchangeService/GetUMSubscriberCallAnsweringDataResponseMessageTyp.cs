// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GetUMSubscriberCallAnsweringDataResponseMessageType
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
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[Serializable]
public class GetUMSubscriberCallAnsweringDataResponseMessageType : ResponseMessageType
{
  private bool isOOFField;
  private UMMailboxTranscriptionEnabledSetting isTranscriptionEnabledInMailboxConfigField;
  private bool isMailboxQuotaExceededField;
  private byte[] greetingField;
  private string greetingNameField;
  private bool taskTimedOutField;

  /// <remarks />
  public bool IsOOF
  {
    get => this.isOOFField;
    set => this.isOOFField = value;
  }

  /// <remarks />
  public UMMailboxTranscriptionEnabledSetting IsTranscriptionEnabledInMailboxConfig
  {
    get => this.isTranscriptionEnabledInMailboxConfigField;
    set => this.isTranscriptionEnabledInMailboxConfigField = value;
  }

  /// <remarks />
  public bool IsMailboxQuotaExceeded
  {
    get => this.isMailboxQuotaExceededField;
    set => this.isMailboxQuotaExceededField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] Greeting
  {
    get => this.greetingField;
    set => this.greetingField = value;
  }

  /// <remarks />
  public string GreetingName
  {
    get => this.greetingNameField;
    set => this.greetingNameField = value;
  }

  /// <remarks />
  public bool TaskTimedOut
  {
    get => this.taskTimedOutField;
    set => this.taskTimedOutField = value;
  }
}
