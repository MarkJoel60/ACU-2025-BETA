// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ClientIntentType
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
public class ClientIntentType
{
  private ItemIdType itemIdField;
  private int intentField;
  private int itemVersionField;
  private bool wouldRepairField;
  private ClientIntentMeetingInquiryActionType predictedActionField;
  private bool predictedActionFieldSpecified;

  /// <remarks />
  public ItemIdType ItemId
  {
    get => this.itemIdField;
    set => this.itemIdField = value;
  }

  /// <remarks />
  public int Intent
  {
    get => this.intentField;
    set => this.intentField = value;
  }

  /// <remarks />
  public int ItemVersion
  {
    get => this.itemVersionField;
    set => this.itemVersionField = value;
  }

  /// <remarks />
  public bool WouldRepair
  {
    get => this.wouldRepairField;
    set => this.wouldRepairField = value;
  }

  /// <remarks />
  public ClientIntentMeetingInquiryActionType PredictedAction
  {
    get => this.predictedActionField;
    set => this.predictedActionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PredictedActionSpecified
  {
    get => this.predictedActionFieldSpecified;
    set => this.predictedActionFieldSpecified = value;
  }
}
