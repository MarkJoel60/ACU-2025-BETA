// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.DAC.APPaymentProcessorUser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.DAC;

/// <summary>AP external payment processor user</summary>
[PXCacheName("Payment Processor Users")]
public class APPaymentProcessorUser : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>External payment processor id</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APExternalPaymentProcessor.externalPaymentProcessorID))]
  [PXForeignReference(typeof (APPaymentProcessorUser.FK.ExternalPaymentProcessorKey))]
  public virtual string? ExternalPaymentProcessorID { get; set; }

  /// <summary>Organization id</summary>
  [PXDBDefault(typeof (APPaymentProcessorOrganization.organizationID))]
  [Organization(true, IsKey = true)]
  [PXForeignReference(typeof (APPaymentProcessorUser.FK.OrganizationKey))]
  [PXUIField(DisplayName = "Company ID", Enabled = false, Visible = false)]
  [PXParent(typeof (APPaymentProcessorUser.FK.PaymentProcessorOrganizationKey))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>The identifier of the user.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.SM.Users.PKID">Users.PKID</see> field.
  /// </value>
  [PXDBGuid(false, IsKey = true)]
  [PXForeignReference(typeof (APPaymentProcessorUser.FK.UserKey))]
  [PXSelector(typeof (Search<Users.pKID>), SubstituteKey = typeof (Users.username))]
  [PXUIField(DisplayName = "Username")]
  public virtual Guid? UserID { get; set; }

  /// <summary>Status</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("R")]
  [UserStatus.List]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string? Status { get; set; }

  /// <summary>External User id</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "External User ID", Enabled = false, Visible = false)]
  public virtual string? ExternalUserID { get; set; }

  /// <summary>Can be onboarded state (for Onboard action state)</summary>
  [PXBool]
  [PXUIField(DisplayName = "Can Be Onboarded", Visible = false)]
  public bool? CanBeOnboarded => new bool?(this.Status == "R");

  /// <summary>Can be enabled state (for Enable action state)</summary>
  [PXBool]
  [PXUIField(DisplayName = "Can Be Enabled", Visible = false)]
  public bool? CanBeEnabled => new bool?(this.Status == "D");

  /// <summary>Can be disabled state (for Disable action state)</summary>
  [PXBool]
  [PXUIField(Visible = false)]
  public bool? CanBeDisabled => new bool?(this.Status == "O");

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
  [PXUIField(DisplayName = "NoteID")]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[]? Tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<APPaymentProcessorUser>.By<APPaymentProcessorUser.externalPaymentProcessorID, APPaymentProcessorUser.organizationID, APPaymentProcessorUser.userID>
  {
    public static APPaymentProcessorUser Find(
      PXGraph graph,
      string externalPaymentProcessorID,
      int? organizationID,
      Guid? userID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPaymentProcessorUser>.By<APPaymentProcessorUser.externalPaymentProcessorID, APPaymentProcessorUser.organizationID, APPaymentProcessorUser.userID>.FindBy(graph, (object) externalPaymentProcessorID, (object) organizationID, (object) userID, options);
    }
  }

  public static class FK
  {
    public class ExternalPaymentProcessorKey : 
      PrimaryKeyOf<APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>.ForeignKeyOf<APPaymentProcessorUser>.By<APPaymentProcessorUser.externalPaymentProcessorID>
    {
    }

    public class UserKey : 
      PrimaryKeyOf<
      #nullable disable
      Users>.By<Users.pKID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorUser>.By<APPaymentProcessorUser.userID>
    {
    }

    public class OrganizationKey : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorUser>.By<APPaymentProcessorUser.organizationID>
    {
    }

    public class PaymentProcessorOrganizationKey : 
      PrimaryKeyOf<
      #nullable disable
      APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.externalPaymentProcessorID, APPaymentProcessorOrganization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorUser>.By<APPaymentProcessorUser.externalPaymentProcessorID, APPaymentProcessorUser.organizationID>
    {
    }
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorUser.externalPaymentProcessorID>
  {
  }

  public abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<APPaymentProcessorUser.organizationID>
  {
  }

  public abstract class userID : BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorUser.userID>
  {
  }

  public abstract class status : BqlType<IBqlString, string>.Field<APPaymentProcessorUser.status>
  {
  }

  public abstract class externalUserID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorUser.externalUserID>
  {
  }

  public abstract class canBeOnboarded : 
    BqlType<IBqlBool, bool>.Field<APPaymentProcessorUser.canBeOnboarded>
  {
  }

  public abstract class canBeEnabled : 
    BqlType<IBqlBool, bool>.Field<APPaymentProcessorUser.canBeEnabled>
  {
  }

  public abstract class canBeDisabled : 
    BqlType<IBqlBool, bool>.Field<APPaymentProcessorUser.canBeDisabled>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorUser.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorUser.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorUser.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorUser.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorUser.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorUser.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorUser.noteID>
  {
  }

  public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<APPaymentProcessorUser.tstamp>
  {
  }
}
