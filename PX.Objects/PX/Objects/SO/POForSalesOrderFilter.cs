// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.POForSalesOrderFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Intercompany Purchase Orders Filter")]
public class POForSalesOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true)]
  [PXDefault("PO")]
  [POCrossCompanyDocType.List]
  [PXUIField]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string PODocType { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Sales Order Type", Required = false)]
  [PXDefault(typeof (Search2<SOOrderType.orderType, InnerJoin<SOSetup, On<Where2<Where<SOSetup.dfltIntercompanyOrderType, Equal<SOOrderType.orderType>, And<Current<POForSalesOrderFilter.pODocType>, Equal<POCrossCompanyDocType.purchaseOrder>>>, Or<Where<SOSetup.dfltIntercompanyRMAType, Equal<SOOrderType.orderType>, And<Current<POForSalesOrderFilter.pODocType>, Equal<POCrossCompanyDocType.purchaseReturn>>>>>>>>))]
  [PXSelector(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.orderType, Equal<SOOrderType.orderType>, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>, And<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>>>>>, Where2<Where<Current<POForSalesOrderFilter.pODocType>, Equal<POCrossCompanyDocType.purchaseOrder>, And<SOOrderType.behavior, In3<SOBehavior.sO, SOBehavior.iN>>>, Or<Where<Current<POForSalesOrderFilter.pODocType>, Equal<POCrossCompanyDocType.purchaseReturn>, And<SOOrderType.behavior, In3<SOBehavior.rM, SOBehavior.cM>>>>>>), DescriptionField = typeof (SOOrderType.descr))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), "Order Type is inactive.", new System.Type[] {typeof (SOOrderType.orderType)})]
  public virtual string IntercompanyOrderType { get; set; }

  [VendorActive]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>), "This vendor does not belong to your organization. Select another vendor.", new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
  public virtual int? SellingCompany { get; set; }

  [CustomerActive]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>), "This customer does not belong to your organization. Select another customer.", new System.Type[] {typeof (PX.Objects.AR.Customer.acctCD)})]
  public virtual int? PurchasingCompany { get; set; }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? CopyProjectDetails { get; set; }

  public abstract class pODocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POForSalesOrderFilter.pODocType>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POForSalesOrderFilter.docDate>
  {
  }

  public abstract class intercompanyOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POForSalesOrderFilter.intercompanyOrderType>
  {
  }

  public abstract class sellingCompany : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POForSalesOrderFilter.sellingCompany>
  {
  }

  public abstract class purchasingCompany : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POForSalesOrderFilter.purchasingCompany>
  {
  }

  public abstract class copyProjectDetails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POForSalesOrderFilter.copyProjectDetails>
  {
  }
}
