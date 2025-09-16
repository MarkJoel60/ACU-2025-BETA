// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.IconIndexType
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
public enum IconIndexType
{
  /// <remarks />
  Default,
  /// <remarks />
  PostItem,
  /// <remarks />
  MailRead,
  /// <remarks />
  MailUnread,
  /// <remarks />
  MailReplied,
  /// <remarks />
  MailForwarded,
  /// <remarks />
  MailEncrypted,
  /// <remarks />
  MailSmimeSigned,
  /// <remarks />
  MailEncryptedReplied,
  /// <remarks />
  MailSmimeSignedReplied,
  /// <remarks />
  MailEncryptedForwarded,
  /// <remarks />
  MailSmimeSignedForwarded,
  /// <remarks />
  MailEncryptedRead,
  /// <remarks />
  MailSmimeSignedRead,
  /// <remarks />
  MailIrm,
  /// <remarks />
  MailIrmForwarded,
  /// <remarks />
  MailIrmReplied,
  /// <remarks />
  SmsSubmitted,
  /// <remarks />
  SmsRoutedToDeliveryPoint,
  /// <remarks />
  SmsRoutedToExternalMessagingSystem,
  /// <remarks />
  SmsDelivered,
  /// <remarks />
  OutlookDefaultForContacts,
  /// <remarks />
  AppointmentItem,
  /// <remarks />
  AppointmentRecur,
  /// <remarks />
  AppointmentMeet,
  /// <remarks />
  AppointmentMeetRecur,
  /// <remarks />
  AppointmentMeetNY,
  /// <remarks />
  AppointmentMeetYes,
  /// <remarks />
  AppointmentMeetNo,
  /// <remarks />
  AppointmentMeetMaybe,
  /// <remarks />
  AppointmentMeetCancel,
  /// <remarks />
  AppointmentMeetInfo,
  /// <remarks />
  TaskItem,
  /// <remarks />
  TaskRecur,
  /// <remarks />
  TaskOwned,
  /// <remarks />
  TaskDelegated,
}
