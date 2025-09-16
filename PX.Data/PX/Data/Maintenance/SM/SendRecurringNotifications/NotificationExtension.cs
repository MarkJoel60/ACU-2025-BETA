// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SendRecurringNotifications.NotificationExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;

#nullable enable
namespace PX.Data.Maintenance.SM.SendRecurringNotifications;

public sealed class NotificationExtension : PXCacheExtension<
#nullable disable
Notification>
{
  [PXBool]
  public bool? CreatedFromReport { get; set; }

  public abstract class createdFromReport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    NotificationExtension.createdFromReport>
  {
  }
}
