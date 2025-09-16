// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.DAC.APPaymentProcessorAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.DAC;

/// <summary>AP payment processor account</summary>
[PXCacheName("Payment Processor Accounts")]
public class APPaymentProcessorAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>External payment processor id</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APExternalPaymentProcessor.externalPaymentProcessorID))]
  [PXForeignReference(typeof (APPaymentProcessorAccount.FK.ExternalPaymentProcessorKey))]
  public virtual string? ExternalPaymentProcessorID { get; set; }

  /// <summary>Organization id</summary>
  [PXDBDefault(typeof (APPaymentProcessorOrganization.organizationID))]
  [Organization(true, IsKey = true)]
  [PXForeignReference(typeof (APPaymentProcessorAccount.FK.OrganizationKey))]
  [PXUIField(DisplayName = "Company ID", Enabled = false, Visible = false)]
  [PXParent(typeof (APPaymentProcessorAccount.FK.PaymentProcessorOrganizationKey))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>External Account id</summary>
  [PXDBString(50, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "External Account ID", Enabled = false, Visible = false)]
  public virtual string? ExternalAccountID { get; set; }

  /// <summary>External Account Bank Name</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Bank", Enabled = false)]
  public virtual string? ExternalAccountBank { get; set; }

  /// <summary>External Account Bank Name</summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Type", Enabled = false)]
  public virtual string? ExternalAccountType { get; set; }

  /// <summary>External Account Name</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Holder", Enabled = false)]
  public virtual string? ExternalAccountName { get; set; }

  /// <summary>External Account Routing Number</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Routing Number", Enabled = false)]
  public virtual string? ExternalAccountRoutingNumber { get; set; }

  /// <summary>External Account Number</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Number", Enabled = false)]
  public virtual string? ExternalAccountNumber { get; set; }

  /// <summary>
  /// The Acumatica specific cash account id which is linked to a external funding account.
  /// </summary>
  [CashAccount(typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.CA.CashAccount.branchID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.active, Equal<True>, And<PX.Objects.GL.Branch.organizationID, Equal<Current<APPaymentProcessorAccount.organizationID>>>>>>), DisplayName = "Cash Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.CA.CashAccount.descr))]
  public virtual int? CashAccountID { get; set; }

  /// <summary>Status</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [AccountStatus.List]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string? Status { get; set; }

  /// <summary>Is active or not</summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active", Visible = false, Enabled = false)]
  public virtual bool? IsActive { get; set; }

  /// <summary>Can be disabled state (for Disable action state)</summary>
  [PXBool]
  [PXUIField(DisplayName = "Can be disabled", Visible = false)]
  public bool? CanBeDisabled => this.IsActive;

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
    PrimaryKeyOf<APPaymentProcessorAccount>.By<APPaymentProcessorAccount.externalPaymentProcessorID, APPaymentProcessorAccount.organizationID, APPaymentProcessorAccount.externalAccountID>
  {
    public static APPaymentProcessorAccount Find(
      PXGraph graph,
      string externalPaymentProcessorID,
      int organizationID,
      string externalAccountID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPaymentProcessorAccount>.By<APPaymentProcessorAccount.externalPaymentProcessorID, APPaymentProcessorAccount.organizationID, APPaymentProcessorAccount.externalAccountID>.FindBy(graph, (object) externalPaymentProcessorID, (object) organizationID, (object) externalAccountID, options);
    }
  }

  public static class FK
  {
    public class ExternalPaymentProcessorKey : 
      PrimaryKeyOf<APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>.ForeignKeyOf<APPaymentProcessorAccount>.By<APPaymentProcessorAccount.externalPaymentProcessorID>
    {
    }

    public class CashAccountKey : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorAccount>.By<APPaymentProcessorAccount.cashAccountID>
    {
    }

    public class OrganizationKey : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorAccount>.By<APPaymentProcessorAccount.organizationID>
    {
    }

    public class PaymentProcessorOrganizationKey : 
      PrimaryKeyOf<
      #nullable disable
      APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.externalPaymentProcessorID, APPaymentProcessorOrganization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorAccount>.By<APPaymentProcessorAccount.externalPaymentProcessorID, APPaymentProcessorAccount.organizationID>
    {
    }
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.externalPaymentProcessorID>
  {
  }

  public abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<APPaymentProcessorAccount.organizationID>
  {
  }

  public abstract class externalAccountID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.externalAccountID>
  {
  }

  public abstract class externalAccountBank : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.externalAccountBank>
  {
  }

  public abstract class externalAccountType : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.externalAccountType>
  {
  }

  public abstract class externalAccountName : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.externalAccountName>
  {
  }

  public abstract class externalAccountRoutingNumber : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.externalAccountRoutingNumber>
  {
  }

  public abstract class externalAccountNumber : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.externalAccountNumber>
  {
  }

  public abstract class cashAccountID : 
    BqlType<IBqlInt, int>.Field<APPaymentProcessorAccount.cashAccountID>
  {
  }

  public abstract class status : BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.status>
  {
  }

  public abstract class isActive : BqlType<IBqlBool, bool>.Field<APPaymentProcessorAccount.isActive>
  {
  }

  public abstract class canBeDisabled : 
    BqlType<IBqlBool, bool>.Field<APPaymentProcessorAccount.canBeDisabled>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorAccount.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorAccount.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorAccount.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorAccount.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorAccount.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorAccount.noteid>
  {
  }

  public abstract class tstamp : 
    BqlType<IBqlByteArray, byte[]>.Field<APPaymentProcessorAccount.tstamp>
  {
  }
}
