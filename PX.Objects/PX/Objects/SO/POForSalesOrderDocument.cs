// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.POForSalesOrderDocument
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Intercompany Purchase Orders Documents")]
public class POForSalesOrderDocument : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<POForSalesOrderDocument.excluded, Equal<True>>, True>, Current<POForSalesOrderDocument.selected>>))]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [POVendor]
  public virtual int? VendorID { get; set; }

  [Branch(null, null, true, true, true, DisplayName = "Purchasing Company")]
  public virtual int? BranchID { get; set; }

  [PXString(2, IsFixed = true, IsKey = true)]
  [PODocType.List]
  [PXUIField]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string DocNbr { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [FinPeriodSelector]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Promised Date")]
  public virtual DateTime? ExpectedDate { get; set; }

  [PXDecimal]
  [PXUIField]
  public virtual Decimal? CuryDocTotal { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Document Discount", Visible = false)]
  public virtual Decimal? CuryDiscTot { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Tax Total", Visible = false)]
  public virtual Decimal? CuryTaxTotal { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Total Qty.")]
  public virtual Decimal? DocQty { get; set; }

  [PXDBInt]
  [PXSubordinateSelector]
  [PXUIField]
  public virtual int? EmployeeID { get; set; }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID { get; set; }

  [Owner(typeof (POForSalesOrderDocument.workgroupID))]
  public virtual int? OwnerID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string DocDesc { get; set; }

  [PXString(2)]
  public virtual string OrderType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string OrderNbr { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Excluded")]
  public virtual bool? Excluded { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POForSalesOrderDocument.selected>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POForSalesOrderDocument.vendorID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POForSalesOrderDocument.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POForSalesOrderDocument.docType>
  {
  }

  public abstract class docNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POForSalesOrderDocument.docNbr>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POForSalesOrderDocument.docDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POForSalesOrderDocument.finPeriodID>
  {
  }

  public abstract class expectedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POForSalesOrderDocument.expectedDate>
  {
  }

  public abstract class curyDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POForSalesOrderDocument.curyDocTotal>
  {
  }

  public abstract class curyDiscTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POForSalesOrderDocument.curyDiscTot>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POForSalesOrderDocument.curyTaxTotal>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POForSalesOrderDocument.curyID>
  {
  }

  public abstract class docQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POForSalesOrderDocument.docQty>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POForSalesOrderDocument.employeeID>
  {
  }

  public abstract class workgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POForSalesOrderDocument.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POForSalesOrderDocument.ownerID>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POForSalesOrderDocument.docDesc>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POForSalesOrderDocument.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POForSalesOrderDocument.orderNbr>
  {
  }

  public abstract class excluded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POForSalesOrderDocument.excluded>
  {
  }
}
