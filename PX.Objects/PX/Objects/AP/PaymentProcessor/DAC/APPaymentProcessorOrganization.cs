// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.DAC.APPaymentProcessorOrganization
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Webhooks.DAC;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.DAC;

/// <summary>AP external payment processor organization</summary>
[PXCacheName("Payment Processor Organizations")]
public class APPaymentProcessorOrganization : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>External payment processor id</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APExternalPaymentProcessor.externalPaymentProcessorID))]
  [PXParent(typeof (APPaymentProcessorOrganization.FK.ExternalPaymentProcessorKey))]
  public virtual 
  #nullable disable
  string ExternalPaymentProcessorID { get; set; }

  /// <summary>Organization id</summary>
  [Organization(true, IsKey = true)]
  [PXForeignReference(typeof (APPaymentProcessorOrganization.FK.OrganizationKey))]
  [PXUIField(DisplayName = "Company ID")]
  public virtual int? OrganizationID { get; set; }

  /// <summary>Is onboarded</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Onboarded", Enabled = false)]
  public virtual bool? IsOnboarded { get; set; }

  /// <summary>Can be onboarded state (for Onboard action state)</summary>
  [PXBool]
  [PXUIField(Visible = false)]
  public bool? CanBeOnboarded
  {
    get
    {
      bool? isOnboarded = this.IsOnboarded;
      return !isOnboarded.HasValue ? new bool?() : new bool?(!isOnboarded.GetValueOrDefault());
    }
  }

  /// <summary>External organization id</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "External Org. ID", Visible = false, Enabled = false)]
  public virtual string ExternalOrganizationID { get; set; }

  /// <summary>
  /// Acumatica specidic Webhook Id. Id points to a database record with the webhook handler.
  /// </summary>
  [PXDBGuid(false)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APPaymentProcessorOrganization.webhookID>.IsRelatedTo<WebHook.webHookID>))]
  [PXSelector(typeof (Search<WebHook.webHookID>), SubstituteKey = typeof (WebHook.name), DescriptionField = typeof (WebHook.name))]
  [PXUIField(DisplayName = "Webhook", Enabled = false)]
  public virtual Guid? WebhookID { get; set; }

  /// <summary>
  /// The external system identifier of the webhook. The ID points to an external system record with a webhook handler.
  /// </summary>
  [PXDBString(50)]
  public virtual string ExternalWebhookID { get; set; }

  /// <summary>Can Subscribe (for SubscribeWebhook action state)</summary>
  [PXBool]
  [PXUIField(Visible = false)]
  public bool? CanSubscribe
  {
    get => new bool?(this.IsOnboarded.GetValueOrDefault() && !this.WebhookID.HasValue);
  }

  /// <summary>Can Unsubscribe (for UnSubscribeWebhook action state)</summary>
  [PXBool]
  [PXUIField(Visible = false)]
  public bool? CanUnSubscribe => new bool?(this.WebhookID.HasValue);

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created Date Time")]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  [PXUIField(DisplayName = "Noteid")]
  public virtual Guid? Noteid { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[] Tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.externalPaymentProcessorID, APPaymentProcessorOrganization.organizationID>
  {
    public static APPaymentProcessorOrganization Find(
      PXGraph graph,
      string externalPaymentProcessorID,
      int? organizationID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.externalPaymentProcessorID, APPaymentProcessorOrganization.organizationID>.FindBy(graph, (object) externalPaymentProcessorID, (object) organizationID, options);
    }
  }

  public static class FK
  {
    public class ExternalPaymentProcessorKey : 
      PrimaryKeyOf<
      #nullable enable
      APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>.ForeignKeyOf<
      #nullable disable
      APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.externalPaymentProcessorID>
    {
    }

    public class OrganizationKey : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.organizationID>
    {
    }
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentProcessorOrganization.externalPaymentProcessorID>
  {
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APPaymentProcessorOrganization.organizationID>
  {
  }

  public abstract class isOnboarded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPaymentProcessorOrganization.isOnboarded>
  {
  }

  public abstract class canBeOnboarded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPaymentProcessorOrganization.canBeOnboarded>
  {
  }

  public abstract class externalOrganizationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentProcessorOrganization.externalOrganizationID>
  {
  }

  public abstract class webhookID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPaymentProcessorOrganization.webhookID>
  {
  }

  public abstract class externalWebhookID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPaymentProcessorOrganization.externalWebhookID>
  {
  }

  public abstract class canSubscribe : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPaymentProcessorOrganization.canSubscribe>
  {
  }

  public abstract class canUnSubscribe : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPaymentProcessorOrganization.canUnSubscribe>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPaymentProcessorOrganization.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentProcessorOrganization.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPaymentProcessorOrganization.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPaymentProcessorOrganization.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentProcessorOrganization.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPaymentProcessorOrganization.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APPaymentProcessorOrganization.noteid>
  {
  }

  public abstract class tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    APPaymentProcessorOrganization.tstamp>
  {
  }
}
