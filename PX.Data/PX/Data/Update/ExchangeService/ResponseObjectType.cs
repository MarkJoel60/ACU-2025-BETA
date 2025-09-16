// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ResponseObjectType
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
[XmlInclude(typeof (PostReplyItemBaseType))]
[XmlInclude(typeof (PostReplyItemType))]
[XmlInclude(typeof (AddItemToMyCalendarType))]
[XmlInclude(typeof (RemoveItemType))]
[XmlInclude(typeof (ProposeNewTimeType))]
[XmlInclude(typeof (ReferenceItemResponseType))]
[XmlInclude(typeof (AcceptSharingInvitationType))]
[XmlInclude(typeof (SuppressReadReceiptType))]
[XmlInclude(typeof (SmartResponseBaseType))]
[XmlInclude(typeof (SmartResponseType))]
[XmlInclude(typeof (CancelCalendarItemType))]
[XmlInclude(typeof (ForwardItemType))]
[XmlInclude(typeof (ReplyAllToItemType))]
[XmlInclude(typeof (ReplyToItemType))]
[XmlInclude(typeof (WellKnownResponseObjectType))]
[XmlInclude(typeof (MeetingRegistrationResponseObjectType))]
[XmlInclude(typeof (DeclineItemType))]
[XmlInclude(typeof (TentativelyAcceptItemType))]
[XmlInclude(typeof (AcceptItemType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public abstract class ResponseObjectType : ResponseObjectCoreType
{
  private string objectNameField;

  /// <remarks />
  [XmlAttribute]
  public string ObjectName
  {
    get => this.objectNameField;
    set => this.objectNameField = value;
  }
}
