// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.ReportParameters.CustomerReportParametersVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.Common.DAC.ReportParameters;

public sealed class CustomerReportParametersVisibilityRestriction : 
  PXCacheExtension<
  #nullable disable
  CustomerReportParameters>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerClassByUserBranches]
  public string CustomerClassID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches]
  public int? CustomerID { get; set; }

  [PXMergeAttributes]
  [PXDimensionSelector("BIZACCT", typeof (Search<Customer.bAccountID, Where<Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And2<Where<Customer.customerClassID, Equal<Optional<CustomerReportParameters.customerClassID>>, Or<Optional<CustomerReportParameters.customerClassID>, IsNull>>, And<Match<Customer, BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>>), typeof (BAccountR.acctCD), new System.Type[] {typeof (BAccountR.acctCD), typeof (Customer.acctName), typeof (Customer.customerClassID), typeof (Customer.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)})]
  public int? CustomerIDByCustomerClass { get; set; }

  public abstract class customerIDByCustomerClass : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerReportParametersVisibilityRestriction.customerIDByCustomerClass>
  {
  }
}
