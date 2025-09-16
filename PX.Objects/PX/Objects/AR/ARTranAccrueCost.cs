// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranAccrueCost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select<ARTran, Where<ARTran.accrueCost, Equal<True>>>), Persistent = false)]
[Serializable]
public class ARTranAccrueCost : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected string _UOM;
  protected Decimal? _BaseQty;
  protected bool? _AccrueCost;
  protected string _CostBasis;
  protected Decimal? _CuryAccruedCost;
  protected Decimal? _AccruedCost;
  protected int? _ExpenseAccrualAccountID;
  protected int? _ExpenseAccrualSubID;
  protected int? _ExpenseAccountID;
  protected int? _ExpenseSubID;

  [PXDBInt(BqlField = typeof (ARTran.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARTran.tranType))]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (ARTran.refNbr))]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARTranAccrueCost.tranType>>, And<ARRegister.refNbr, Equal<Current<ARTranAccrueCost.refNbr>>>>>))]
  [PXParent(typeof (Select<PX.Objects.SO.SOInvoice, Where<PX.Objects.SO.SOInvoice.docType, Equal<Current<ARTranAccrueCost.tranType>>, And<PX.Objects.SO.SOInvoice.refNbr, Equal<Current<ARTranAccrueCost.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (ARTran.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [ARTranInventoryItem(Filterable = true, BqlField = typeof (ARTran.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXBool]
  [PXFormula(typeof (Selector<ARTranAccrueCost.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  public virtual bool? IsStockItem { get; set; }

  [INUnit(typeof (ARTranAccrueCost.inventoryID), BqlField = typeof (ARTran.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(BqlField = typeof (ARTran.qty))]
  public virtual Decimal? Qty { get; set; }

  [PXDBQuantity(BqlField = typeof (ARTran.baseQty))]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [PXDBBool(BqlField = typeof (ARTran.accrueCost))]
  public virtual bool? AccrueCost
  {
    get => this._AccrueCost;
    set => this._AccrueCost = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (ARTran.costBasis))]
  public virtual string CostBasis
  {
    get => this._CostBasis;
    set => this._CostBasis = value;
  }

  [PXDBDecimal(BqlField = typeof (ARTran.curyAccruedCost))]
  public virtual Decimal? CuryAccruedCost
  {
    get => this._CuryAccruedCost;
    set => this._CuryAccruedCost = value;
  }

  [PXDBDecimal(BqlField = typeof (ARTran.accruedCost))]
  public virtual Decimal? AccruedCost
  {
    get => this._AccruedCost;
    set => this._AccruedCost = value;
  }

  [Account(typeof (ARTranAccrueCost.branchID), BqlField = typeof (ARTran.expenseAccrualAccountID))]
  public virtual int? ExpenseAccrualAccountID
  {
    get => this._ExpenseAccrualAccountID;
    set => this._ExpenseAccrualAccountID = value;
  }

  [SubAccount(typeof (ARTranAccrueCost.expenseAccrualAccountID), typeof (ARTranAccrueCost.branchID), true, BqlField = typeof (ARTran.expenseAccrualSubID))]
  public virtual int? ExpenseAccrualSubID
  {
    get => this._ExpenseAccrualSubID;
    set => this._ExpenseAccrualSubID = value;
  }

  [Account(typeof (ARTranAccrueCost.branchID), BqlField = typeof (ARTran.expenseAccountID))]
  public virtual int? ExpenseAccountID
  {
    get => this._ExpenseAccountID;
    set => this._ExpenseAccountID = value;
  }

  [SubAccount(typeof (ARTranAccrueCost.expenseAccountID), typeof (ARTranAccrueCost.branchID), true, BqlField = typeof (ARTran.expenseSubID))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  public class PK : 
    PrimaryKeyOf<ARTranAccrueCost>.By<ARTranAccrueCost.tranType, ARTranAccrueCost.refNbr, ARTranAccrueCost.lineNbr>
  {
    public static ARTranAccrueCost Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ARTranAccrueCost) PrimaryKeyOf<ARTranAccrueCost>.By<ARTranAccrueCost.tranType, ARTranAccrueCost.refNbr, ARTranAccrueCost.lineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAccrueCost.branchID>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAccrueCost.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAccrueCost.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAccrueCost.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAccrueCost.inventoryID>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranAccrueCost.isStockItem>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAccrueCost.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranAccrueCost.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranAccrueCost.baseQty>
  {
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranAccrueCost.accrueCost>
  {
  }

  public abstract class costBasis : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranAccrueCost.costBasis>
  {
  }

  public abstract class curyAccruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranAccrueCost.curyAccruedCost>
  {
  }

  public abstract class accruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranAccrueCost.accruedCost>
  {
  }

  public abstract class expenseAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARTranAccrueCost.expenseAccrualAccountID>
  {
  }

  public abstract class expenseAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARTranAccrueCost.expenseAccrualSubID>
  {
  }

  public abstract class expenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARTranAccrueCost.expenseAccountID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranAccrueCost.expenseSubID>
  {
  }
}
