// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.LinkSubcontractLineFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.CN.Subcontracts.AP;

[PXHidden]
public class LinkSubcontractLineFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The subcontract number.</summary>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Subcontract Nbr.")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<POOrderRS, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<LinkLineOrder>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrderRS.orderNbr, Equal<LinkLineOrder.orderNbr>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.orderType, Equal<LinkLineOrder.orderType>>>>>.And<BqlOperand<PX.Objects.PO.POOrder.orderType, IBqlString>.IsEqual<POOrderType.regularSubcontract>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.vendorID, Equal<BqlField<APRecognizedInvoice.vendorID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POOrder.vendorLocationID, IBqlInt>.IsEqual<BqlField<APRecognizedInvoice.vendorLocationID, IBqlInt>.FromCurrent>>>, And<Not<FeatureInstalled<FeaturesSet.vendorRelations>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<FeatureInstalled<FeaturesSet.vendorRelations>>, And<BqlOperand<PX.Objects.PO.POOrder.vendorID, IBqlInt>.IsEqual<BqlField<APRecognizedInvoice.suppliedByVendorID, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.PO.POOrder.vendorLocationID, IBqlInt>.IsEqual<BqlField<APRecognizedInvoice.suppliedByVendorLocationID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POOrder.payToVendorID, IBqlInt>.IsEqual<BqlField<APRecognizedInvoice.vendorID, IBqlInt>.FromCurrent>>>>.Aggregate<To<GroupBy<POOrderRS.orderNbr>, GroupBy<PX.Objects.PO.POOrder.orderType>>>, POOrderRS>.SearchFor<POOrderRS.orderNbr>), new Type[] {typeof (PX.Objects.PO.POOrder.orderNbr), typeof (PX.Objects.PO.POOrder.vendorID), typeof (PX.Objects.PO.POOrder.vendorLocationID), typeof (PX.Objects.PO.POOrder.orderDate), typeof (PX.Objects.PO.POOrder.status), typeof (PX.Objects.PO.POOrder.curyID), typeof (PX.Objects.PO.POOrder.vendorRefNbr), typeof (PX.Objects.PO.POOrder.curyOrderTotal)}, Headers = new string[] {"Subcontract Nbr.", "Vendor", "Location", "Date", "Status", "Currency", "Vendor Ref.", "Subcontract Total"})]
  public virtual 
  #nullable disable
  string POOrderNbr { get; set; }

  /// <summary>The inventory ID.</summary>
  [Inventory(Enabled = false)]
  public virtual int? InventoryID { get; set; }

  /// <summary>The unit of measure.</summary>
  [INUnit(typeof (LinkSubcontractLineFilter.inventoryID), Enabled = false)]
  public virtual string UOM { get; set; }

  public abstract class pOOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LinkSubcontractLineFilter.pOOrderNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkSubcontractLineFilter.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkSubcontractLineFilter.uOM>
  {
  }
}
