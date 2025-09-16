// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRetainageFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class APRetainageFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false)]
  public int? OrganizationID { get; set; }

  [Branch(null, null, true, true, true, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? BranchID { get; set; }

  [OrganizationTree(typeof (APRetainageFilter.organizationID), typeof (APRetainageFilter.branchID), null, false)]
  [PXUIRequired(typeof (Where<FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>>))]
  public int? OrgBAccountID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DocDate { get; set; }

  [APOpenPeriod(typeof (APRetainageFilter.docDate), null, null, typeof (APRetainageFilter.organizationID), null, null, true, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, ValidatePeriod = PeriodValidation.DefaultSelectUpdate, IsFilterMode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [VendorActive(Visibility = PXUIVisibility.SelectorVisible, Required = false, DescriptionField = typeof (Vendor.acctName))]
  public virtual int? VendorID { get; set; }

  [APActiveProject]
  public virtual int? ProjectID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [APInvoiceType.RefNbr(typeof (Search5<APRegisterAlias.refNbr, InnerJoin<APInvoice, On<APInvoice.docType, Equal<APRegisterAlias.docType>, And<APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoinSingleTable<Vendor, On<APRegisterAlias.vendorID, Equal<Vendor.bAccountID>>, LeftJoin<APTran, On<APRegisterAlias.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.curyRetainageBal, NotEqual<decimal0>, And<APTran.curyRetainageAmt, NotEqual<decimal0>>>>>>>>>, Where<APRegisterAlias.retainageApply, Equal<True>, And<APRegisterAlias.released, Equal<True>, And2<Where<APRegisterAlias.paymentsByLinesAllowed, NotEqual<True>, And<APRegisterAlias.curyRetainageUnreleasedAmt, Greater<decimal0>, And<APRegisterAlias.curyRetainageTotal, Greater<decimal0>>>>, Or<APRegisterAlias.paymentsByLinesAllowed, Equal<True>, And<APTran.refNbr, PX.Data.IsNotNull>>>>>, Aggregate<GroupBy<APRegisterAlias.docType, GroupBy<APRegisterAlias.refNbr>>>, OrderBy<Desc<APRegisterAlias.refNbr>>>))]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible)]
  public virtual string RefNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Lines with Open Balance", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowBillsWithOpenBalance { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRetainageFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRetainageFilter.branchID>
  {
  }

  public abstract class orgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRetainageFilter.orgBAccountID>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APRetainageFilter.docDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRetainageFilter.finPeriodID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRetainageFilter.vendorID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRetainageFilter.projectID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRetainageFilter.refNbr>
  {
  }

  public abstract class showBillsWithOpenBalance : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRetainageFilter.showBillsWithOpenBalance>
  {
  }
}
