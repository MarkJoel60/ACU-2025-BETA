// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenEventSubscriberProperty
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUScreenEventSubscriberProperty
{
  public const 
  #nullable disable
  string SubscriberType = "SubscriberType";
  public const string NotificationTemplate = "NotificationTemplate";
  public const string ActivityTemplate = "ActivityTemplate";
  public const string Action = "Action";
  public const string Active = "Active";
  public const string Entity = "Entity";

  public class subscriberType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUScreenEventSubscriberProperty.subscriberType>
  {
    public subscriberType()
      : base("SubscriberType")
    {
    }
  }

  public class notificationTemplate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUScreenEventSubscriberProperty.notificationTemplate>
  {
    public notificationTemplate()
      : base("NotificationTemplate")
    {
    }
  }

  public class activityTemplate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUScreenEventSubscriberProperty.activityTemplate>
  {
    public activityTemplate()
      : base("ActivityTemplate")
    {
    }
  }

  public class action : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenEventSubscriberProperty.action>
  {
    public action()
      : base("Action")
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenEventSubscriberProperty.active>
  {
    public active()
      : base("Active")
    {
    }
  }

  public class entity : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenEventSubscriberProperty.entity>
  {
    public entity()
      : base("Entity")
    {
    }
  }
}
