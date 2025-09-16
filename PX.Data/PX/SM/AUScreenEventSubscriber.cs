// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenEventSubscriber
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select<AUScreenItem, Where<AUScreenItem.itemType, Equal<AUScreenItemType.eventSubscriber>>>), Persistent = true)]
[Serializable]
public class AUScreenEventSubscriber : AUScreenItem
{
  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Subscriber ID")]
  public override 
  #nullable disable
  string ItemCD
  {
    get => this._ItemCD;
    set => this._ItemCD = value;
  }

  [PXDBString(2, IsUnicode = false, InputMask = "", IsKey = false)]
  [PXDefault("ES")]
  public override string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true, InputMask = "", IsKey = false)]
  [PXDefault("")]
  public override string ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public override string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Customized")]
  public override bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXString(50)]
  [PXUIField(DisplayName = "Type")]
  public virtual string SubscriberType { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Notification Template")]
  public virtual int? NotificationTemplate { get; set; }

  [PXString(50)]
  [PXUIField(DisplayName = "Activity Template")]
  public virtual string ActivityTemplate { get; set; }

  [PXString(50)]
  [PXUIField(DisplayName = "Action")]
  public virtual string Action { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXString(50)]
  [PXUIField(DisplayName = "Entity")]
  public virtual string Entity { get; set; }

  public new abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriber.screenID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenEventSubscriber.projectID>
  {
  }

  public new class itemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEventSubscriber.itemCD>
  {
  }

  public new abstract class itemType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriber.itemType>
  {
  }

  public new abstract class itemID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriber.itemID>
  {
  }

  public new abstract class parentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriber.parentID>
  {
  }

  public new abstract class childRowCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenEventSubscriber.childRowCntr>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriber.description>
  {
  }

  public new abstract class isActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenEventSubscriber.isActive>
  {
  }

  public abstract class subscriberType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriber.subscriberType>
  {
  }

  public abstract class notificationTemplate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenEventSubscriber.notificationTemplate>
  {
  }

  public abstract class activityTemplate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenEventSubscriber.activityTemplate>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEventSubscriber.action>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenEventSubscriber.active>
  {
  }

  public abstract class entity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenEventSubscriber.entity>
  {
  }
}
