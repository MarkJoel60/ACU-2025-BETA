// Decompiled with JetBrains decompiler
// Type: PX.SM.EmailNotificationAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

[PXDBInt]
[PXSelector(typeof (Search<Notification.notificationID>), DescriptionField = typeof (Notification.name), SelectorMode = PXSelectorMode.DisplayModeText)]
[PXUIField(DisplayName = "Email Template")]
public class EmailNotificationAttribute : PXEntityAttribute
{
}
