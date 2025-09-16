// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.PushSubscriptionRequestType
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
public class PushSubscriptionRequestType : BaseSubscriptionRequestType
{
  private int statusFrequencyField;
  private string uRLField;
  private string callerDataField;

  /// <remarks />
  public int StatusFrequency
  {
    get => this.statusFrequencyField;
    set => this.statusFrequencyField = value;
  }

  /// <remarks />
  public string URL
  {
    get => this.uRLField;
    set => this.uRLField = value;
  }

  /// <remarks />
  public string CallerData
  {
    get => this.callerDataField;
    set => this.callerDataField = value;
  }
}
