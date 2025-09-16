// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.DAC.APPaymentProcessorBill
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

/// <summary>AP external payment processor Bill information</summary>
[PXCacheName("Payment Processor Bills")]
public class APPaymentProcessorBill : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>External payment processor id</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APExternalPaymentProcessor.externalPaymentProcessorID))]
  [PXForeignReference(typeof (APPaymentProcessorBill.FK.ExternalPaymentProcessorKey))]
  public virtual string? ExternalPaymentProcessorID { get; set; }

  /// <summary>Organization id</summary>
  [PXDefault]
  [Organization(true, IsKey = true)]
  [PXForeignReference(typeof (APPaymentProcessorBill.FK.OrganizationKey))]
  [PXUIField(DisplayName = "Organization", Required = true)]
  [PXParent(typeof (APPaymentProcessorBill.FK.PaymentProcessorOrganization))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>Document type</summary>
  [PXDBString(3, IsKey = true)]
  [PXDBDefault(typeof (APRegister.docType))]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, TabOrder = 0)]
  public virtual string? DocType { get; set; }

  /// <summary>Document Reference nnumber</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APRegister.refNbr))]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<APPaymentProcessorBill.docType>>, And<APRegister.refNbr, Equal<Current<APPaymentProcessorBill.refNbr>>>>>))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string? RefNbr { get; set; }

  /// <summary>External payment processor bill id</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "External Bill ID")]
  public virtual string? ExternalBillID { get; set; }

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
    PrimaryKeyOf<APPaymentProcessorBill>.By<APPaymentProcessorBill.externalPaymentProcessorID, APPaymentProcessorBill.organizationID, APPaymentProcessorBill.docType, APPaymentProcessorBill.refNbr>
  {
    public static APPaymentProcessorBill Find(
      PXGraph graph,
      string externalPaymentProcessorID,
      int? organizationID,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPaymentProcessorBill>.By<APPaymentProcessorBill.externalPaymentProcessorID, APPaymentProcessorBill.organizationID, APPaymentProcessorBill.docType, APPaymentProcessorBill.refNbr>.FindBy(graph, (object) externalPaymentProcessorID, (object) organizationID, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class ExternalPaymentProcessorKey : 
      PrimaryKeyOf<APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>.ForeignKeyOf<APPaymentProcessorBill>.By<APPaymentProcessorBill.externalPaymentProcessorID>
    {
    }

    public class OrganizationKey : 
      PrimaryKeyOf<
      #nullable disable
      PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorBill>.By<APPaymentProcessorBill.organizationID>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<
      #nullable disable
      APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorBill>.By<APPaymentProcessorBill.docType, APPaymentProcessorBill.refNbr>
    {
    }

    public class PaymentProcessorOrganization : 
      PrimaryKeyOf<
      #nullable disable
      APPaymentProcessorOrganization>.By<APPaymentProcessorOrganization.externalPaymentProcessorID, APPaymentProcessorOrganization.organizationID>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorBill>.By<APPaymentProcessorBill.externalPaymentProcessorID, APPaymentProcessorBill.organizationID>
    {
    }
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorBill.externalPaymentProcessorID>
  {
  }

  public abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<APPaymentProcessorBill.organizationID>
  {
  }

  public abstract class docType : BqlType<IBqlString, string>.Field<APPaymentProcessorBill.docType>
  {
  }

  public abstract class refNbr : BqlType<IBqlString, string>.Field<APPaymentProcessorBill.refNbr>
  {
  }

  public abstract class externalBillID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorBill.externalBillID>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorBill.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorBill.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorBill.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorBill.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorBill.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorBill.lastModifiedDateTime>
  {
  }

  public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<APPaymentProcessorBill.tstamp>
  {
  }
}
