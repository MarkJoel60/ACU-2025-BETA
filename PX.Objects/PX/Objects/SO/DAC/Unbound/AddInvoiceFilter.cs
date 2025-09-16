// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Unbound.AddInvoiceFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Unbound;

/// <exclude />
[PXCacheName("Add Invoice Filter")]
public class AddInvoiceFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(3, IsFixed = true)]
  [PXDefault("INV")]
  [PXStringList(new string[] {"INV", "CSL", "DRM", "CRM"}, new string[] {"Invoice", "Cash Sale", "Debit Memo", "Credit Memo"})]
  [PXUIField(DisplayName = "AR Doc. Type")]
  public virtual 
  #nullable disable
  string ARDocType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  [ARInvoiceType.RefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoinSingleTable<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>>, Where<ARRegisterAlias.docType, Equal<Optional<AddInvoiceFilter.aRDocType>>, And<ARRegisterAlias.released, Equal<boolTrue>, And<ARRegisterAlias.origModule, Equal<BatchModule.moduleSO>, And<ARRegisterAlias.customerID, Equal<Current<PX.Objects.SO.SOOrder.customerID>>>>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>), Filterable = true)]
  [PXRestrictor(typeof (Where<ARRegisterAlias.canceled, NotEqual<True>>), "The invoice {0} cannot be selected because it is canceled.", new Type[] {typeof (ARRegisterAlias.refNbr)})]
  [PXRestrictor(typeof (Where<ARRegisterAlias.isUnderCorrection, NotEqual<True>>), "The invoice {0} cannot be selected because the correction invoice exists for this invoice.", new Type[] {typeof (ARRegisterAlias.refNbr)})]
  [PXFormula(typeof (Default<AddInvoiceFilter.aRDocType>))]
  public virtual string ARRefNbr { get; set; }

  [PXString(2, IsFixed = true, InputMask = ">aa")]
  [PXSelector(typeof (Search2<PX.Objects.SO.SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On2<SOOrderTypeOperation.FK.OrderType, And<SOOrderTypeOperation.operation, Equal<PX.Objects.SO.SOOrderType.defaultOperation>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrderType.behavior, NotIn3<SOBehavior.qT, SOBehavior.bL, SOBehavior.tR>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrderType.behavior, NotEqual<SOBehavior.rM>>>>>.Or<BqlOperand<PX.Objects.SO.SOOrderType.aRDocType, IBqlString>.IsNotEqual<PX.Objects.AR.ARDocType.noUpdate>>>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.SO.SOOrderType.requireAllocation, NotEqual<True>, Or<AllocationAllowed>>), "'{0}' cannot be found in the system.", new Type[] {typeof (PX.Objects.SO.SOOrderType.orderType)})]
  [PXRestrictor(typeof (Where<PX.Objects.SO.SOOrderType.active, Equal<True>>), null, new Type[] {})]
  [PXUIField]
  public virtual string OrderType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PX.Objects.SO.SO.RefNbr(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional2<AddInvoiceFilter.orderType>>, And<PX.Objects.SO.SOOrder.customerID, Equal<Current<PX.Objects.SO.SOOrder.customerID>>>>, OrderBy<Desc<PX.Objects.SO.SOOrder.orderNbr>>>), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.SO.SOOrder.releasedCntr, Greater<int0>>), null, new Type[] {})]
  public virtual string OrderNbr { get; set; }

  [Inventory]
  public virtual int? InventoryID { get; set; }

  [PXString]
  [PXSelector(typeof (Search<INItemLotSerial.lotSerialNbr, Where<INItemLotSerial.inventoryID, Equal<Optional2<AddInvoiceFilter.inventoryID>>>>))]
  [PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial")]
  public virtual string LotSerialNbr { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Non-Stock Kits by Components")]
  public virtual bool? Expand { get; set; }

  public abstract class aRDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddInvoiceFilter.aRDocType>
  {
  }

  public abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddInvoiceFilter.aRRefNbr>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddInvoiceFilter.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddInvoiceFilter.orderNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddInvoiceFilter.inventoryID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddInvoiceFilter.lotSerialNbr>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AddInvoiceFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AddInvoiceFilter.endDate>
  {
  }

  public abstract class expand : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AddInvoiceFilter.expand>
  {
  }
}
