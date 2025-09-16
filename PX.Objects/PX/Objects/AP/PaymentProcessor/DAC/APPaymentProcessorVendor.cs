// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.DAC.APPaymentProcessorVendor
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

/// <summary>AP external payment processor vendor information</summary>
[PXCacheName("Payment Processor Vendors")]
public class APPaymentProcessorVendor : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>External payment processor id</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APExternalPaymentProcessor.externalPaymentProcessorID))]
  [PXForeignReference(typeof (APPaymentProcessorVendor.FK.ExternalPaymentProcessorKey))]
  public virtual string? ExternalPaymentProcessorID { get; set; }

  /// <summary>Organization id</summary>
  [PXDefault]
  [Organization(true, IsKey = true)]
  [PXForeignReference(typeof (APPaymentProcessorVendor.FK.OrganizationKey))]
  [PXUIField(DisplayName = "Organization", Required = true)]
  [PXParent(typeof (APPaymentProcessorVendor.FK.PaymentProcessorOrganization))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>Vendor ID</summary>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.Location.bAccountID))]
  [PXUIField(DisplayName = "BAccountID", Visible = false, Enabled = false)]
  [PXParent(typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<APPaymentProcessorVendor.bAccountID>>>>))]
  public virtual int? BAccountID { get; set; }

  /// <summary>Remittance Adress Changed</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Remittance Address Changed", Visible = true, Enabled = false)]
  public virtual bool? IsRemittanceAddressChanged { get; set; }

  /// <summary>Bank Details Changed</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bank Details Changed", Visible = true, Enabled = false)]
  public virtual bool? IsBankDetailsChanged { get; set; }

  /// <summary>Vendor location ID</summary>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.Location.locationID))]
  [PXUIField(Visible = false, Enabled = false, Visibility = PXUIVisibility.Invisible)]
  [PXParent(typeof (Select<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APPaymentProcessorVendor.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APPaymentProcessorVendor.locationID>>>>>))]
  public virtual int? LocationID { get; set; }

  /// <summary>External payment processor vendor id</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "External Vendor ID")]
  public virtual string? ExternalVendorID { get; set; }

  /// <summary>Network status of vendor</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Bill.com Status", IsReadOnly = true)]
  public virtual string? NetworkStatus { get; set; }

  /// <summary>Payment type</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Preferred Disbursement Method", IsReadOnly = true)]
  public virtual string? PayByType { get; set; }

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

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[]? Tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<APPaymentProcessorVendor>.By<APPaymentProcessorVendor.externalPaymentProcessorID, APPaymentProcessorVendor.organizationID, APPaymentProcessorVendor.bAccountID, APPaymentProcessorVendor.locationID>
  {
    public static APPaymentProcessorVendor Find(
      PXGraph graph,
      string externalPaymentProcessorID,
      int? organizationID,
      int? bAccountID,
      int? locationID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPaymentProcessorVendor>.By<APPaymentProcessorVendor.externalPaymentProcessorID, APPaymentProcessorVendor.organizationID, APPaymentProcessorVendor.bAccountID, APPaymentProcessorVendor.locationID>.FindBy(graph, (object) externalPaymentProcessorID, (object) organizationID, (object) bAccountID, (object) locationID, options);
    }
  }

  public static class FK
  {
    public class ExternalPaymentProcessorKey : 
      PrimaryKeyOf<APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>.ForeignKeyOf<APPaymentProcessorVendor>.By<APPaymentProcessorVendor.externalPaymentProcessorID>
    {
    }

    public class OrganizationKey : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorVendor>.By<APPaymentProcessorVendor.organizationID>
    {
    }

    public class BAccount : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<
      #nullable enable
      APExternalPaymentProcessor>.By<APPaymentProcessorVendor.bAccountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<
      #nullable enable
      APExternalPaymentProcessor>.By<APPaymentProcessorVendor.bAccountID, APPaymentProcessorVendor.locationID>
    {
    }

    public class PaymentProcessorOrganization : 
      PrimaryKeyOf<
      #nullable disable
      APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.externalPaymentProcessorID, APPaymentProcessorOrganization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorVendor>.By<APPaymentProcessorVendor.externalPaymentProcessorID, APPaymentProcessorVendor.organizationID>
    {
    }
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorVendor.externalPaymentProcessorID>
  {
  }

  public abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<APPaymentProcessorVendor.organizationID>
  {
  }

  public abstract class bAccountID : BqlType<IBqlInt, int>.Field<APPaymentProcessorVendor.bAccountID>
  {
  }

  public abstract class isRemittanceAddressChanged : 
    BqlType<IBqlInt, int>.Field<APPaymentProcessorVendor.isRemittanceAddressChanged>
  {
  }

  public abstract class isBankDetailsChanged : 
    BqlType<IBqlInt, int>.Field<APPaymentProcessorVendor.isBankDetailsChanged>
  {
  }

  public abstract class locationID : BqlType<IBqlInt, int>.Field<APPaymentProcessorVendor.locationID>
  {
  }

  public abstract class externalVendorID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorVendor.externalVendorID>
  {
  }

  public abstract class networkStatus : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorVendor.networkStatus>
  {
  }

  public abstract class payByType : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorVendor.payByType>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorVendor.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorVendor.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorVendor.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorVendor.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorVendor.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorVendor.lastModifiedDateTime>
  {
  }

  public abstract class tstamp : 
    BqlType<IBqlByteArray, byte[]>.Field<APPaymentProcessorVendor.tstamp>
  {
  }
}
