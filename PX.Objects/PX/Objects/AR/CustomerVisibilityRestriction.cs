// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.AR;

public sealed class CustomerVisibilityRestriction : PXCacheExtension<
#nullable disable
Customer>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [CustomerRaw(typeof (Search2<Customer.acctCD, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<Customer.defAddressID>>>>>, Where<Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<Match<Current<AccessInfo.userName>>>>>), IsKey = true)]
  public string AcctCD { get; set; }

  [PXMergeAttributes]
  [PXDefault(typeof (Search2<ARSetup.dfltCustomerClassID, InnerJoin<CustomerClass, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>, Where<CustomerClass.orgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>))]
  [PXSelector(typeof (Search<CustomerClass.customerClassID, Where<CustomerClass.orgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<MatchUser>>>), CacheGlobal = true, DescriptionField = typeof (CustomerClass.descr))]
  public string CustomerClassID { get; set; }

  [PXMergeAttributes]
  [PXDefault(0, typeof (Search<CustomerClass.orgBAccountID, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public int? COrgBAccountID { get; set; }

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches(typeof (BAccountR.cOrgBAccountID))]
  [RestrictVendorByUserBranches(typeof (BAccountR.vOrgBAccountID))]
  public int? ParentBAccountID { get; set; }

  public abstract class cOrgBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerVisibilityRestriction.cOrgBAccountID>
  {
  }

  public abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerVisibilityRestriction.parentBAccountID>
  {
  }
}
