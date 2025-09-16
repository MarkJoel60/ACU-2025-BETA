// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ARTranAsSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXHidden]
[PXProjection(typeof (Select<PX.Objects.AR.ARTran>), Persistent = false)]
[Serializable]
public class ARTranAsSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSDetail,
  ILSMaster,
  IItemPlanMaster
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARTran.tranType))]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AR.ARTran.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.AR.ARTran.lineNbr))]
  [PXParent(typeof (Select<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<ARTranAsSplit.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<ARTranAsSplit.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Current<ARTranAsSplit.lineNbr>>>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXInt(IsKey = true)]
  [PXFormula(typeof (int1))]
  public virtual int? SplitLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARTran.lineType))]
  public virtual string LineType { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARTran.tranDate))]
  public virtual DateTime? TranDate { get; set; }

  [NonStockNonKitCrossItem(INPrimaryAlternateType.CPN, "A non-stock kit cannot be added to a document manually. Use the Sales Orders (SO301000) form to prepare an invoice for the corresponding sales order.", typeof (PX.Objects.AR.ARTran.sOOrderNbr), typeof (FeaturesSet.advancedSOInvoices), BqlField = typeof (PX.Objects.AR.ARTran.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [SubItem(BqlField = typeof (PX.Objects.AR.ARTran.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXBool]
  [PXFormula(typeof (Selector<ARTranAsSplit.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  public bool? IsStockItem { get; set; }

  [PXShort]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AR.ARTran.qty, Less<decimal0>>, Minus<PX.Objects.AR.ARTran.invtMult>>, PX.Objects.AR.ARTran.invtMult>), typeof (short))]
  public virtual short? InvtMult { get; set; }

  [SiteAvail(typeof (ARTranAsSplit.inventoryID), typeof (ARTranAsSplit.subItemID), typeof (CostCenter.freeStock), BqlField = typeof (PX.Objects.AR.ARTran.siteID), DocumentBranchType = typeof (PX.Objects.AR.ARInvoice.branchID))]
  public virtual int? SiteID { get; set; }

  [Location(typeof (ARTranAsSplit.siteID), BqlField = typeof (PX.Objects.AR.ARTran.locationID))]
  public virtual int? LocationID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARTran.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  [INUnit(typeof (ARTranAsSplit.inventoryID), BqlField = typeof (PX.Objects.AR.ARTran.uOM))]
  public virtual string UOM { get; set; }

  [PXQuantity(typeof (ARTranAsSplit.uOM), typeof (ARTranAsSplit.baseQty))]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AR.ARTran.qty, Less<decimal0>>, Minus<PX.Objects.AR.ARTran.qty>>, PX.Objects.AR.ARTran.qty>), typeof (Decimal))]
  public virtual Decimal? Qty { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AR.ARTran.qty, Less<decimal0>>, Minus<PX.Objects.AR.ARTran.baseQty>>, PX.Objects.AR.ARTran.baseQty>), typeof (Decimal))]
  public virtual Decimal? BaseQty { get; set; }

  [SOInvoiceLineSplittingExtension.ARLotSerialNbr(typeof (ARTranAsSplit.inventoryID), typeof (ARTranAsSplit.subItemID), typeof (ARTranAsSplit.locationID), typeof (ARTranAsSplit.costCenterID))]
  public virtual string LotSerialNbr { get; set; }

  [PXString(10, IsUnicode = true)]
  public virtual string LotSerClassID { get; set; }

  [PXString(30, IsUnicode = true)]
  public virtual string AssignedNbr { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARTran.expireDate))]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARTran.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARTran.taskID))]
  public virtual int? TaskID { get; set; }

  bool? ILSMaster.IsIntercompany => new bool?(false);

  public static ARTranAsSplit FromARTran(PX.Objects.AR.ARTran item)
  {
    return new ARTranAsSplit()
    {
      TranType = item.TranType,
      RefNbr = item.RefNbr,
      LineNbr = item.LineNbr,
      SplitLineNbr = new int?(1),
      LineType = item.LineType,
      TranDate = item.TranDate,
      InventoryID = item.InventoryID,
      SubItemID = item.SubItemID,
      InvtMult = item.InvtMult,
      SiteID = item.SiteID,
      LocationID = item.LocationID,
      CostCenterID = item.CostCenterID,
      UOM = item.UOM,
      Qty = item.Qty,
      BaseQty = item.BaseQty,
      LotSerialNbr = item.LotSerialNbr,
      ExpireDate = item.ExpireDate,
      ProjectID = item.ProjectID,
      TaskID = item.TaskID
    };
  }

  public static PX.Objects.AR.ARTran ToARTran(ARTranAsSplit item)
  {
    return new PX.Objects.AR.ARTran()
    {
      TranType = item.TranType,
      RefNbr = item.RefNbr,
      LineNbr = item.LineNbr,
      LineType = item.LineType,
      TranDate = item.TranDate,
      InventoryID = item.InventoryID,
      SubItemID = item.SubItemID,
      InvtMult = item.InvtMult,
      SiteID = item.SiteID,
      LocationID = item.LocationID,
      UOM = item.UOM,
      Qty = item.Qty,
      BaseQty = item.BaseQty,
      LotSerialNbr = item.LotSerialNbr,
      ExpireDate = item.ExpireDate,
      ProjectID = item.ProjectID,
      TaskID = item.TaskID
    };
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAsSplit.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAsSplit.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.lineNbr>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.splitLineNbr>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAsSplit.lineType>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTranAsSplit.tranDate>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.subItemID>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranAsSplit.isStockItem>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARTranAsSplit.invtMult>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.locationID>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.costCenterID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAsSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranAsSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranAsSplit.baseQty>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAsSplit.lotSerialNbr>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranAsSplit.lotSerClassID>
  {
  }

  public abstract class assignedNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAsSplit.assignedNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTranAsSplit.expireDate>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAsSplit.taskID>
  {
  }
}
