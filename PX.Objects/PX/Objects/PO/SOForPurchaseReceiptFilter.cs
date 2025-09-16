// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.SOForPurchaseReceiptFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Intercompany Sales Orders Filter")]
public class SOForPurchaseReceiptFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [CustomerActive]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>), "This customer does not belong to your organization. Select another customer.", new System.Type[] {typeof (Customer.acctCD)})]
  public virtual int? PurchasingCompany { get; set; }

  [VendorActive]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>), "This vendor does not belong to your organization. Select another vendor.", new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
  public virtual int? SellingCompany { get; set; }

  [PXBool]
  [PXUIField]
  [PXDefault(true)]
  public virtual bool? PutReceiptsOnHold { get; set; }

  public abstract class docDate : 
    BqlType<IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOForPurchaseReceiptFilter.docDate>
  {
  }

  public abstract class purchasingCompany : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOForPurchaseReceiptFilter.purchasingCompany>
  {
  }

  public abstract class sellingCompany : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOForPurchaseReceiptFilter.sellingCompany>
  {
  }

  public abstract class putReceiptsOnHold : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOForPurchaseReceiptFilter.putReceiptsOnHold>
  {
  }
}
