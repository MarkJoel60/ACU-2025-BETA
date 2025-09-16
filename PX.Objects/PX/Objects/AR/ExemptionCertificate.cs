// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ExemptionCertificate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An unbound DAC that is used to collect the information that is related to the exemption certificate.
/// </summary>
[PXHidden]
public class ExemptionCertificate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The exemption ceritifcate ID.</summary>
  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Certificate ID")]
  public virtual 
  #nullable disable
  string CertificateID { get; set; }

  /// <summary>The company ID in the exemption certificate.</summary>
  [PXString]
  public virtual string ECMCompanyID { get; set; }

  /// <summary>The state or region of the exemption certificate.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string State { get; set; }

  /// <summary>The reason of the exemption certificate.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string ExemptionReason { get; set; }

  /// <summary>The effective date of the exemption certificate.</summary>
  [PXDate]
  [PXUIField(DisplayName = "Effective Date", Enabled = false)]
  public virtual DateTime? EffectiveDate { get; set; }

  /// <summary>The expiration date of the exemption certificate.</summary>
  [PXDate]
  [PXUIField(DisplayName = "Expiration Date", Enabled = false)]
  public virtual DateTime? ExpirationDate { get; set; }

  /// <summary>The status of the exemption certificate.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string Status { get; set; }

  /// <summary>The company code of the exemption certificate.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string CompanyCode { get; set; }

  public abstract class certificateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ExemptionCertificate.certificateID>
  {
  }

  public abstract class eCMCompanyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExemptionCertificate.eCMCompanyID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptionCertificate.state>
  {
  }

  public abstract class exemptionReason : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExemptionCertificate.exemptionReason>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ExemptionCertificate.effectiveDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ExemptionCertificate.expirationDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptionCertificate.status>
  {
  }

  public abstract class companyCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExemptionCertificate.companyCode>
  {
  }
}
