// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.RequestECMCertificateFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An unbound DAC that is used for the dialog box that is displayed when a certificate is requested on the Customers (AR303000) form.
/// </summary>
[PXHidden]
[Serializable]
public class RequestECMCertificateFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The company code for which the exemption certificate is requested.
  /// </summary>
  [PXDBString]
  [PXUIField]
  [PXSelector(typeof (Search5<TaxPluginMapping.companyCode, InnerJoin<TaxPlugin, On<TaxPluginMapping.taxPluginID, Equal<TaxPlugin.taxPluginID>>, InnerJoin<TXSetup, On<TaxPlugin.taxPluginID, Equal<TXSetup.eCMProvider>>>>, Aggregate<GroupBy<TaxPluginMapping.companyCode>>>))]
  [PXDefault(typeof (Search2<TaxPluginMapping.companyCode, InnerJoin<TaxPlugin, On<TaxPluginMapping.taxPluginID, Equal<TaxPlugin.taxPluginID>>, InnerJoin<TXSetup, On<TaxPlugin.taxPluginID, Equal<TXSetup.eCMProvider>>>>, Where<TaxPluginMapping.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  public virtual 
  #nullable disable
  string CompanyCode { get; set; }

  /// <summary>The email ID of the recipient.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Email")]
  [PXDefault(typeof (Search<PX.Objects.CR.Contact.eMail, Where<PX.Objects.CR.Contact.contactID, Equal<Current<Customer.primaryContactID>>, And<PX.Objects.CR.Contact.bAccountID, Equal<Current<Customer.bAccountID>>>>>))]
  public virtual string EmailId { get; set; }

  /// <summary>The email template for a certificate request.</summary>
  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Certificate Request Template")]
  [ECMCertificateTemplateSelector(typeof (CertificateTemplate.templateName), ValidateValue = false)]
  [PXDefault]
  public virtual string Template { get; set; }

  /// <summary>The country for which the certificate is requested.</summary>
  [PXDBString(2, IsUnicode = true)]
  [PXUIField(DisplayName = "Country")]
  [PXSelector(typeof (PX.Objects.CS.Country.countryID), DescriptionField = typeof (PX.Objects.CS.Country.description))]
  [PXDefault(typeof (Search<PX.Objects.CS.Country.countryID, Where<PX.Objects.CS.Country.countryID, Equal<RequestECMCertificateFilter.ECMDefaultCountry.uSCountry>>>))]
  public virtual string CountryID { get; set; }

  /// <summary>
  /// The state of the country for which the certificate is requested.
  /// </summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (RequestECMCertificateFilter.countryID))]
  [PXDefault]
  public virtual string State { get; set; }

  /// <summary>The reason for the requested exemption certificate.</summary>
  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Reason for Exemption")]
  [ECMExemptionReasonSelector(DescriptionField = typeof (CertificateReason.reasonName))]
  public virtual string ExemptReason { get; set; }

  public abstract class companyCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RequestECMCertificateFilter.companyCode>
  {
  }

  public abstract class emailId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RequestECMCertificateFilter.emailId>
  {
  }

  public abstract class template : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RequestECMCertificateFilter.template>
  {
  }

  public class ECMDefaultCountry
  {
    public const string US = "US";

    public class uSCountry : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      RequestECMCertificateFilter.ECMDefaultCountry.uSCountry>
    {
      public uSCountry()
        : base("US")
      {
      }
    }
  }

  public abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RequestECMCertificateFilter.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RequestECMCertificateFilter.state>
  {
  }

  public abstract class exemptReason : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RequestECMCertificateFilter.exemptReason>
  {
  }
}
