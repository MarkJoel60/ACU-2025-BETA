// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxCustomer
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS;

public class FSxCustomer : PXCacheExtension<
#nullable disable
PX.Objects.AR.Customer>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBInt]
  [PXDefault(typeof (Search<FSxCustomerClass.defaultBillingCycleID, Where<PX.Objects.AR.CustomerClass.customerClassID, Equal<Current<PX.Objects.AR.Customer.customerClassID>>>>))]
  [PXUIField(DisplayName = "Billing Cycle")]
  [PXSelector(typeof (FSBillingCycle.billingCycleID), SubstituteKey = typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  public virtual int? BillingCycleID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("BT")]
  [ListField_Send_Invoices_To.ListAtrribute]
  [PXUIField(DisplayName = "Bill-To Address")]
  public virtual string SendInvoicesTo { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("SO")]
  [ListField_Ship_To.ListAtrribute]
  [PXUIField(DisplayName = "Ship-To Address")]
  public virtual string BillShipmentSource { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("SO")]
  [ListField_Default_Billing_Customer_Source.ListAtrribute]
  [PXUIField(DisplayName = "Default Billing Customer")]
  public virtual string DefaultBillingCustomerSource { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Billing Customer")]
  [FSSelectorCustomer]
  public virtual int? BillCustomerID { get; set; }

  [PXDefault]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSxCustomer.billCustomerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Billing Location")]
  public virtual int? BillLocationID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Customer Signature on Mobile App")]
  public virtual bool? RequireCustomerSignature { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? chkServiceManagement => new bool?(true);

  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsExtendingToCustomer { get; set; }

  public abstract class billingCycleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxCustomer.billingCycleID>
  {
  }

  public abstract class sendInvoicesTo : ListField_Send_Invoices_To
  {
  }

  public abstract class billShipmentSource : ListField_Ship_To
  {
  }

  public abstract class defaultBillingCustomerSource : ListField_Default_Billing_Customer_Source
  {
  }

  public abstract class billCustomerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxCustomer.billCustomerID>
  {
  }

  public abstract class billLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxCustomer.billLocationID>
  {
  }

  public abstract class requireCustomerSignature : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxCustomer.requireCustomerSignature>
  {
  }

  public abstract class ChkServiceManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxCustomer.ChkServiceManagement>
  {
  }

  public abstract class isExtendingToCustomer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxCustomer.isExtendingToCustomer>
  {
  }
}
