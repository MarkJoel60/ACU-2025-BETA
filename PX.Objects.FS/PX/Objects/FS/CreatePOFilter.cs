// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreatePOFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class CreatePOFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Up to Date")]
  public virtual DateTime? UpToDate { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Item Class")]
  [PXSelector(typeof (Search<INItemClass.itemClassID>), SubstituteKey = typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual int? ItemClassID { get; set; }

  [PXInt]
  [PXFormula(typeof (Default<CreatePOFilter.itemClassID>))]
  [PXUIField(DisplayName = "Inventory")]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemClassID, Equal<Current<CreatePOFilter.itemClassID>>, Or<Current<CreatePOFilter.itemClassID>, IsNull>>>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  [Site(DisplayName = "Warehouse", DescriptionField = typeof (INSite.descr))]
  public virtual int? SiteID { get; set; }

  [VendorNonEmployeeActive(DisplayName = "Vendor", DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true, Filterable = true)]
  public virtual int? POVendorID { get; set; }

  [PXInt]
  [PXUIField]
  [FSSelectorBusinessAccount_CU_PR_VC]
  public virtual int? CustomerID { get; set; }

  [PXString(4, IsFixed = true)]
  [PXUIField]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType, Where<FSSrvOrdType.active, Equal<True>>>))]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXString]
  [PXUIField]
  [PXFormula(typeof (Default<CreatePOFilter.srvOrdType>))]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where<FSServiceOrder.srvOrdType, Equal<Current<CreatePOFilter.srvOrdType>>, Or<Current<CreatePOFilter.srvOrdType>, IsNull>>, OrderBy<Desc<FSServiceOrder.refNbr>>>), DescriptionField = typeof (FSServiceOrder.docDesc))]
  public virtual string SORefNbr { get; set; }

  public abstract class upToDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CreatePOFilter.upToDate>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CreatePOFilter.itemClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CreatePOFilter.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CreatePOFilter.siteID>
  {
  }

  public abstract class poVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CreatePOFilter.poVendorID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CreatePOFilter.customerID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CreatePOFilter.srvOrdType>
  {
  }

  public abstract class sORefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CreatePOFilter.sORefNbr>
  {
  }
}
