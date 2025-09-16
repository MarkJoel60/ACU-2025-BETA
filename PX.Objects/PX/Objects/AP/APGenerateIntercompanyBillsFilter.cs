// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APGenerateIntercompanyBillsFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
[Serializable]
public class APGenerateIntercompanyBillsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDate]
  [PXUnboundDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? Date { get; set; }

  [VendorActive(DisplayName = "Seller", Visibility = PXUIVisibility.SelectorVisible, Required = false, DescriptionField = typeof (Vendor.acctName), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>), "It is not Seller", new System.Type[] {typeof (Vendor.acctCD)})]
  public virtual int? VendorID { get; set; }

  [CustomerActive(DisplayName = "Purchaser", Visibility = PXUIVisibility.SelectorVisible, Required = false, DescriptionField = typeof (PX.Objects.AR.Customer.acctName), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>), "It is not Purchaser", new System.Type[] {typeof (PX.Objects.AR.Customer.acctCD)})]
  [PXDefault(typeof (SearchFor<PX.Objects.AR.Customer.bAccountID>.In<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.branchID, Equal<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.AR.Customer.status, PX.Data.IsNull>>>, PX.Data.Or<BqlOperand<PX.Objects.AR.Customer.status, IBqlString>.IsEqual<CustomerStatus.active>>>>.Or<BqlOperand<PX.Objects.AR.Customer.status, IBqlString>.IsEqual<CustomerStatus.oneTime>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? CustomerID { get; set; }

  [PXBool]
  [PXUnboundDefault(true)]
  [PXUIField(DisplayName = "Create AP Documents on Hold", Visibility = PXUIVisibility.Visible)]
  public virtual bool? CreateOnHold { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Create AP Documents in Specific Period", Visibility = PXUIVisibility.Visible)]
  public virtual bool? CreateInSpecificPeriod { get; set; }

  [PXDBInt]
  [PXFormula(typeof (PurchaserOrganizationID<APGenerateIntercompanyBillsFilter.customerID>))]
  public int? OrganizationID { get; set; }

  [APOpenPeriod(null, null, null, typeof (APGenerateIntercompanyBillsFilter.organizationID), null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
  [PXUIField(DisplayName = "Fin. Period", Visibility = PXUIVisibility.Visible, Required = false)]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Project Information to AP Document", Visibility = PXUIVisibility.Visible)]
  public virtual bool? CopyProjectInformation { get; set; }

  [Project]
  public virtual int? ProjectID { get; set; }

  public abstract class date : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.date>
  {
  }

  public abstract class vendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.vendorID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.customerID>
  {
  }

  public abstract class createOnHold : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.createOnHold>
  {
  }

  public abstract class createInSpecificPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.createInSpecificPeriod>
  {
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.organizationID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.finPeriodID>
  {
  }

  public abstract class copyProjectInformationto : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.copyProjectInformationto>
  {
  }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APGenerateIntercompanyBillsFilter.projectID>
  {
  }
}
