// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.POLandedCostDetailFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using System;

#nullable enable
namespace PX.Objects.PO.LandedCosts;

[Serializable]
public class POLandedCostDetailFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXSelector(typeof (Search<POLandedCostDoc.refNbr, Where<POLandedCostDoc.released, Equal<True>>>))]
  [PXUIField]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string LandedCostDocRefNbr { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXSelector(typeof (Search<LandedCostCode.landedCostCodeID>))]
  [PXUIField]
  [PXFieldDescription]
  public virtual string LandedCostCodeID { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUnboundDefault("AL")]
  [POReceiptType.ListAttribute.WithAll]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string ReceiptType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXFormula(typeof (Default<POLandedCostDetailFilter.receiptType>))]
  [POReceiptType.RefNbr(typeof (Search2<PX.Objects.PO.POReceipt.receiptNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.PO.POReceipt.vendorID>>>, Where<PX.Objects.PO.POReceipt.receiptType, Equal<BqlField<POLandedCostDetailFilter.receiptType, IBqlString>.FromCurrent>, And<PX.Objects.PO.POReceipt.released, Equal<True>>>, OrderBy<Desc<PX.Objects.PO.POReceipt.receiptNbr>>>), Filterable = true)]
  [PXUIField]
  [PXUIEnabled(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLandedCostDetailFilter.receiptType, IsNotNull>>>>.And<BqlOperand<POLandedCostDetailFilter.receiptType, IBqlString>.IsNotEqual<POReceiptType.all>>))]
  [PXFieldDescription]
  public virtual string ReceiptNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("RO")]
  [POOrderType.RegularDropShipList]
  [PXUIField]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<POLandedCostDetailFilter.orderType>>>>), Filterable = true)]
  public virtual string OrderNbr { get; set; }

  public abstract class landedCostDocRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailFilter.landedCostDocRefNbr>
  {
  }

  public abstract class landedCostCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailFilter.landedCostCodeID>
  {
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailFilter.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailFilter.receiptNbr>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailFilter.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailFilter.orderNbr>
  {
  }
}
