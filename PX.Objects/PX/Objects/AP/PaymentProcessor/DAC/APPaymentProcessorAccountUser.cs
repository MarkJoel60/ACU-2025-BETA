// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.DAC.APPaymentProcessorAccountUser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.DAC;

/// <summary>AP payment processor account user.</summary>
[PXCacheName("Payment Processor Account User")]
public class APPaymentProcessorAccountUser : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>External payment processor id.</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APExternalPaymentProcessor.externalPaymentProcessorID))]
  [PXForeignReference(typeof (APPaymentProcessorAccountUser.FK.ExternalPaymentProcessorKey))]
  public virtual string? ExternalPaymentProcessorID { get; set; }

  /// <summary>Organization id</summary>
  [PXDBDefault(typeof (APPaymentProcessorOrganization.organizationID))]
  [Organization(true, IsKey = true)]
  [PXForeignReference(typeof (APPaymentProcessorAccountUser.FK.OrganizationKey))]
  [PXUIField(DisplayName = "Company ID", Enabled = false, Visible = false)]
  [PXParent(typeof (APPaymentProcessorAccountUser.FK.PaymentProcessorOrganizationKey))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>External ID of the bank account user.</summary>
  [PXDBString(50, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "External ID", Enabled = false, Visible = false)]
  public virtual string? ExternalID { get; set; }

  /// <summary>External Account id</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXSelector(typeof (APPaymentProcessorAccount.externalAccountID))]
  [PXUIField(DisplayName = "External Account ID", Enabled = false, Visible = false)]
  public virtual string? ExternalAccountID { get; set; }

  /// <summary>External account status</summary>
  [PXString(1, IsFixed = true)]
  [AccountStatus.List]
  [PXFormula(typeof (Selector<APPaymentProcessorAccountUser.externalAccountID, APPaymentProcessorAccount.status>))]
  [PXUIField(DisplayName = "Account Status", Enabled = false)]
  public virtual string? ExternalAccountStatus { get; set; }

  /// <summary>External User id</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "External User ID", Enabled = false, Visible = false)]
  public virtual string? ExternalUserID { get; set; }

  /// <summary>Verification Status</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("U")]
  [PX.Objects.AP.PaymentProcessor.VerificationStatus.List]
  [PXUIField(DisplayName = "Verification status", Enabled = false, Visible = false)]
  public virtual string? VerificationStatus { get; set; }

  /// <summary>Status</summary>
  [PXString(1, IsFixed = true)]
  [AccountUserStatus.List]
  [PXUIField(DisplayName = "Verification Status", Enabled = false)]
  public virtual string? Status
  {
    [PXDependsOnFields(new System.Type[] {typeof (APPaymentProcessorAccountUser.verificationStatus), typeof (APPaymentProcessorAccountUser.externalAccountStatus)})] get
    {
      if (this.VerificationStatus == "U" && this.ExternalAccountStatus == "P")
        return "P";
      if (this.VerificationStatus == "N" && this.ExternalAccountStatus == "A")
        return "N";
      return this.VerificationStatus == "V" && this.ExternalAccountStatus == "A" ? "V" : "D";
    }
    set
    {
    }
  }

  /// <summary>Can be disabled (for Disable action state)</summary>
  [PXBool]
  [PXUIField(Visible = false)]
  public bool? CanBeDisabled
  {
    get => new bool?(this.VerificationStatus == "V" || this.VerificationStatus == "N");
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string? CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created Date Time")]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string? LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  [PXUIField(DisplayName = "Noteid")]
  public virtual Guid? Noteid { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[]? Tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<APPaymentProcessorAccountUser>.By<APPaymentProcessorAccountUser.externalPaymentProcessorID, APPaymentProcessorAccountUser.organizationID, APPaymentProcessorAccountUser.externalID>
  {
    public static APPaymentProcessorAccountUser Find(
      PXGraph graph,
      string externalPaymentProcessorID,
      int? organizationID,
      string externalID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPaymentProcessorAccountUser>.By<APPaymentProcessorAccountUser.externalPaymentProcessorID, APPaymentProcessorAccountUser.organizationID, APPaymentProcessorAccountUser.externalID>.FindBy(graph, (object) externalPaymentProcessorID, (object) organizationID, (object) externalID, options);
    }
  }

  public static class FK
  {
    public class ExternalPaymentProcessorKey : 
      PrimaryKeyOf<APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>.ForeignKeyOf<APPaymentProcessorAccountUser>.By<APPaymentProcessorAccountUser.externalPaymentProcessorID>
    {
    }

    public class OrganizationKey : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorAccountUser>.By<APPaymentProcessorAccountUser.organizationID>
    {
    }

    public class PaymentProcessorOrganizationKey : 
      PrimaryKeyOf<
      #nullable disable
      APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.externalPaymentProcessorID, APPaymentProcessorOrganization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorAccountUser>.By<APPaymentProcessorAccountUser.externalPaymentProcessorID, APPaymentProcessorAccountUser.organizationID>
    {
    }
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.externalPaymentProcessorID>
  {
  }

  public abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<APPaymentProcessorAccountUser.organizationID>
  {
  }

  public abstract class externalID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.externalID>
  {
  }

  public abstract class externalAccountID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.externalAccountID>
  {
  }

  public abstract class externalAccountStatus : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.externalAccountStatus>
  {
  }

  public abstract class externalUserID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.externalUserID>
  {
  }

  public abstract class verificationStatus : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.verificationStatus>
  {
  }

  public abstract class status : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.status>
  {
  }

  public abstract class canBeDisabled : 
    BqlType<IBqlBool, bool>.Field<APPaymentProcessorAccountUser.canBeDisabled>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorAccountUser.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorAccountUser.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorAccountUser.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccountUser.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorAccountUser.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorAccountUser.noteid>
  {
  }

  public abstract class tstamp : 
    BqlType<IBqlByteArray, byte[]>.Field<APPaymentProcessorAccountUser.tstamp>
  {
  }
}
