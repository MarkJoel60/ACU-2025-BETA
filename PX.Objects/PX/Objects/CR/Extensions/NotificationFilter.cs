// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.NotificationFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions;

[PXHidden]
[Serializable]
public class NotificationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXSelector(typeof (Search2<Notification.name, LeftJoin<SiteMap, On<SiteMap.screenID, Equal<Notification.screenID>>, LeftJoin<EPActivityType, On<EPActivityType.type, Equal<Notification.type>>>>, Where<EPActivityType.application, NotEqual<PXActivityApplicationAttribute.system>>>), new System.Type[] {typeof (Notification.name), typeof (SiteMap.title), typeof (Notification.screenID), typeof (Notification.subject)}, Headers = new string[] {"Description", "Screen Name", "Screen ID", "Subject"}, DescriptionField = typeof (Notification.name))]
  [PXString(255 /*0xFF*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Template")]
  [PXDefault]
  public virtual 
  #nullable disable
  string NotificationName { get; set; }

  [PXInt]
  [PXDefault(2)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Replacing Email Body", "After Email Body", "Before Email Body"})]
  [PXUIField(DisplayName = "Insert Template Text")]
  public virtual int? InsertTemplateText { get; set; }

  public abstract class notificationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationFilter.notificationName>
  {
  }

  public abstract class insertTemplateText : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationFilter.insertTemplateText>
  {
    public const int ReplacingEmailBody = 0;
    public const int AfterEmailBody = 1;
    public const int BeforeEmailBody = 2;

    public class replacingEmailBody : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      NotificationFilter.insertTemplateText.replacingEmailBody>
    {
      public replacingEmailBody()
        : base(0)
      {
      }
    }

    public class afterEmailBody : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      NotificationFilter.insertTemplateText.afterEmailBody>
    {
      public afterEmailBody()
        : base(1)
      {
      }
    }

    public class beforeEmailBody : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      NotificationFilter.insertTemplateText.beforeEmailBody>
    {
      public beforeEmailBody()
        : base(2)
      {
      }
    }
  }
}
