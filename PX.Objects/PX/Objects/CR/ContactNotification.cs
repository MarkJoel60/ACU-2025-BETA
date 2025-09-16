// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactNotification
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXProjection(typeof (Select2<NotificationRecipient, InnerJoin<NotificationSource, On<NotificationSource.sourceID, Equal<NotificationRecipient.sourceID>>>>), Persistent = true)]
[PXCacheName("Contact Notification")]
[PXBreakInheritance]
[Serializable]
public class ContactNotification : NotificationRecipient
{
  protected 
  #nullable disable
  string _SourceClassID;
  protected string _ReportID;
  protected int? _TemplateID;

  [PXDBGuid(false)]
  [PXSelector(typeof (Search<NotificationSetup.setupID>), SubstituteKey = typeof (NotificationSetup.notificationCD))]
  [PXUIField(DisplayName = "Notification ID", Enabled = false)]
  public override Guid? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  [PXFormula(typeof (PX.Objects.CR.EntityDescription<NotificationRecipient.refNoteID>))]
  public virtual string EntityDescription { get; set; }

  [PXDBString(BqlField = typeof (NotificationRecipient.classID))]
  public virtual string SourceClassID
  {
    get => this._SourceClassID;
    set => this._SourceClassID = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Class ID", Enabled = false)]
  public override string ClassID
  {
    [PXDependsOnFields(new System.Type[] {typeof (NotificationRecipient.refNoteID), typeof (ContactNotification.sourceClassID)})] get
    {
      return !this.RefNoteID.HasValue ? this._SourceClassID : (string) null;
    }
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC", BqlField = typeof (NotificationSource.reportID))]
  [PXUIField(DisplayName = "Report", Enabled = false)]
  [PXFormula(typeof (Default<NotificationSource.setupID>))]
  public virtual string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  [PXDBInt(BqlField = typeof (NotificationSource.notificationID))]
  [PXUIField]
  [PXSelector(typeof (Search<Notification.notificationID>), SubstituteKey = typeof (Notification.name), DescriptionField = typeof (Notification.name))]
  public virtual int? TemplateID
  {
    get => this._TemplateID;
    set => this._TemplateID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact")]
  [PXParent(typeof (Select<Contact, Where<Contact.contactID, Equal<Current<ContactNotification.contactID>>>>))]
  public override int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Format")]
  [NotificationFormat.List]
  [PXNotificationFormat(typeof (ContactNotification.reportID), typeof (ContactNotification.notificationID))]
  public override string Format
  {
    get => this._Format;
    set => this._Format = value;
  }

  [PXDBInt(BqlField = typeof (NotificationSource.sourceID))]
  [PXExtraKey]
  public virtual int? KeySourceID
  {
    get => new int?();
    set
    {
    }
  }

  [PXDBGuid(false, BqlField = typeof (NotificationSource.setupID))]
  [PXExtraKey]
  public virtual Guid? KeySetupID
  {
    get => new Guid?();
    set
    {
    }
  }

  public new abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContactNotification.setupID>
  {
  }

  public abstract class entityDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactNotification.entityDescription>
  {
  }

  public abstract class sourceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactNotification.sourceClassID>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactNotification.classID>
  {
  }

  public abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactNotification.reportID>
  {
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactNotification.templateID>
  {
  }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactNotification.contactID>
  {
  }

  public new abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactNotification.format>
  {
  }

  public abstract class keySourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactNotification.keySourceID>
  {
  }

  public abstract class keySetupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContactNotification.keySetupID>
  {
  }

  public new abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContactNotification.notificationID>
  {
  }
}
