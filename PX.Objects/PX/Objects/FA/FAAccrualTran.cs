// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAAccrualTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (Select2<PX.Objects.GL.GLTran, LeftJoin<FAAccrualTran, On<PX.Objects.GL.GLTran.tranID, Equal<FAAccrualTran.tranID>>>, Where<PX.Objects.GL.GLTran.module, NotEqual<BatchModule.moduleFA>, And<PX.Objects.GL.GLTran.released, Equal<True>, And<Where<PX.Objects.GL.GLTran.reclassified, Equal<False>, Or<PX.Objects.GL.GLTran.reclassified, Equal<True>, And<PX.Objects.GL.GLTran.reclassRemainingAmt, Greater<Zero>>>>>>>>), new System.Type[] {typeof (FAAccrualTran)})]
[PXCacheName("FA Accrual Transaction")]
[Serializable]
public class FAAccrualTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IFALocation
{
  protected bool? _Selected = new bool?(false);
  protected int? _TranID;
  protected int? _GLTranAccountID;
  protected int? _GLTranSubID;
  protected Decimal? _GLTranQty;
  protected Decimal? _GLTranAmt;
  protected Decimal? _SelectedQty;
  protected Decimal? _SelectedAmt;
  protected Decimal? _OpenQty;
  protected Decimal? _OpenAmt;
  protected Decimal? _ClosedAmt;
  protected Decimal? _ClosedQty;
  protected Decimal? _UnitCost;
  protected int? _ClassID;
  protected int? _BranchID;
  protected int? _EmployeeID;
  protected 
  #nullable disable
  string _Department;
  protected bool? _Component = new bool?(false);
  protected Decimal? _GLTranDebitAmt;
  protected Decimal? _GLTranCreditAmt;
  protected Decimal? _GLTranOrigQty;
  protected int? _GLTranInventoryID;
  protected string _GLTranModule;
  protected string _GLTranBatchNbr;
  protected string _GLTranUOM;
  protected DateTime? _GLTranDate;
  protected string _GLTranRefNbr;
  protected int? _GLTranReferenceID;
  protected string _GLTranDesc;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXExtraKey]
  public virtual int? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.GLTran.tranID), IsKey = true)]
  [PXDependsOnFields(new System.Type[] {typeof (FAAccrualTran.tranID)})]
  public virtual int? GLTranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.GLTran.accountID))]
  public virtual int? GLTranAccountID
  {
    get => this._GLTranAccountID;
    set => this._GLTranAccountID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.GLTran.subID))]
  public virtual int? GLTranSubID
  {
    get => this._GLTranSubID;
    set => this._GLTranSubID = value;
  }

  [PXDBQuantity]
  [PXDefault(typeof (FAAccrualTran.gLTranOrigQty))]
  [PXUIField(DisplayName = "Orig. Quantity", Enabled = false)]
  public virtual Decimal? GLTranQty
  {
    get => this._GLTranQty;
    set => this._GLTranQty = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(typeof (FAAccrualTran.gLTranDebitAmt))]
  [PXUIField(DisplayName = "Orig. Amount", Enabled = false)]
  public virtual Decimal? GLTranAmt
  {
    get => this._GLTranAmt;
    set => this._GLTranAmt = value;
  }

  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Selected Quantity")]
  public virtual Decimal? SelectedQty
  {
    get => this._SelectedQty;
    set => this._SelectedQty = value;
  }

  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Selected Amount")]
  public virtual Decimal? SelectedAmt
  {
    get => this._SelectedAmt;
    set => this._SelectedAmt = value;
  }

  [PXDBQuantity]
  [PXFormula(typeof (Sub<FAAccrualTran.gLTranQty, Add<FAAccrualTran.selectedQty, FAAccrualTran.closedQty>>))]
  [PXUIField(DisplayName = "Open Quantity", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBBaseCury(null, null)]
  [PXFormula(typeof (Sub<FAAccrualTran.gLTranAmt, Add<FAAccrualTran.selectedAmt, FAAccrualTran.closedAmt>>))]
  [PXUIField(DisplayName = "Open Amount", Enabled = false)]
  public virtual Decimal? OpenAmt
  {
    get => this._OpenAmt;
    set => this._OpenAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClosedAmt
  {
    get => this._ClosedAmt;
    set => this._ClosedAmt = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClosedQty
  {
    get => this._ClosedQty;
    set => this._ClosedQty = value;
  }

  [PXDBBaseCury(null, null)]
  [PXFormula(typeof (Switch<Case<Where<FAAccrualTran.gLTranQty, LessEqual<decimal0>>, FAAccrualTran.gLTranAmt>, Div<FAAccrualTran.gLTranAmt, FAAccrualTran.gLTranQty>>))]
  [PXUIField(DisplayName = "Unit Cost", Enabled = false)]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Reconciled")]
  [PXDefault(false)]
  public bool? Reconciled { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (IIf<BqlOperand<FAAccrualTran.tranID, IBqlInt>.IsNotNull, FAAccrualTran.gLTranQty, PX.Objects.GL.GLTran.qty>), typeof (Decimal))]
  public virtual Decimal? GLTranQtyCalc { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (IIf<BqlOperand<FAAccrualTran.tranID, IBqlInt>.IsNotNull, FAAccrualTran.gLTranAmt, BqlOperand<PX.Objects.GL.GLTran.debitAmt, IBqlDecimal>.Add<PX.Objects.GL.GLTran.creditAmt>>), typeof (Decimal))]
  public virtual Decimal? GLTranAmtCalc { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXInt]
  [PXSelector(typeof (Search2<FAClass.assetID, LeftJoin<FABookSettings, On<FAClass.assetID, Equal<FABookSettings.assetID>>, LeftJoin<FABook, On<FABookSettings.bookID, Equal<FABook.bookID>>>>, Where<FAClass.recordType, Equal<FARecordType.classType>, And<FABook.updateGL, Equal<True>>>>), SubstituteKey = typeof (FAClass.assetCD), DescriptionField = typeof (FAClass.description))]
  [PXUIField(DisplayName = "Asset Class")]
  public virtual int? ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  [Branch(null, null, true, true, false)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? GLTranBranchID { get; set; }

  [PXInt]
  [PXSelector(typeof (EPEmployee.bAccountID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXUIField(DisplayName = "Custodian")]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXString(10, IsUnicode = true)]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField(DisplayName = "Department")]
  public virtual string Department
  {
    get => this._Department;
    set => this._Department = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Component")]
  public virtual bool? Component
  {
    get => this._Component;
    set => this._Component = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.GL.GLTran.debitAmt))]
  public virtual Decimal? GLTranDebitAmt
  {
    get => this._GLTranDebitAmt;
    set => this._GLTranDebitAmt = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.GL.GLTran.creditAmt))]
  public virtual Decimal? GLTranCreditAmt
  {
    get => this._GLTranCreditAmt;
    set => this._GLTranCreditAmt = value;
  }

  [PXDBQuantity(BqlField = typeof (PX.Objects.GL.GLTran.qty))]
  public virtual Decimal? GLTranOrigQty
  {
    get => this._GLTranOrigQty;
    set => this._GLTranOrigQty = value;
  }

  [Inventory(Enabled = false, BqlField = typeof (PX.Objects.GL.GLTran.inventoryID))]
  public virtual int? GLTranInventoryID
  {
    get => this._GLTranInventoryID;
    set => this._GLTranInventoryID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.GL.GLTran.module))]
  [PXUIField(DisplayName = "Module", Enabled = false)]
  public virtual string GLTranModule
  {
    get => this._GLTranModule;
    set => this._GLTranModule = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.GL.GLTran.batchNbr))]
  [PXUIField(DisplayName = "Batch Number", Enabled = false)]
  [PXSelector(typeof (Search<Batch.batchNbr>))]
  public virtual string GLTranBatchNbr
  {
    get => this._GLTranBatchNbr;
    set => this._GLTranBatchNbr = value;
  }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa", BqlField = typeof (PX.Objects.GL.GLTran.uOM))]
  [PXUIField(DisplayName = "UOM", Enabled = false)]
  public virtual string GLTranUOM
  {
    get => this._GLTranUOM;
    set => this._GLTranUOM = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.GL.GLTran.tranDate))]
  [PXUIField(DisplayName = "Transaction Date", Enabled = false)]
  public virtual DateTime? GLTranDate
  {
    get => this._GLTranDate;
    set => this._GLTranDate = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.GL.GLTran.refNbr))]
  [PXUIField(DisplayName = "Ref. Number", Enabled = false)]
  public virtual string GLTranRefNbr
  {
    get => this._GLTranRefNbr;
    set => this._GLTranRefNbr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.GLTran.referenceID))]
  [PXSelector(typeof (Search<BAccountR.bAccountID>), SubstituteKey = typeof (BAccountR.acctCD), CacheGlobal = true)]
  [CustomerVendorRestrictor]
  [PXUIField(DisplayName = "Customer/Vendor", Enabled = false)]
  public virtual int? GLTranReferenceID
  {
    get => this._GLTranReferenceID;
    set => this._GLTranReferenceID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.GL.GLTran.tranDesc))]
  [PXUIField(DisplayName = "Transaction Description", Enabled = false)]
  public virtual string GLTranDesc
  {
    get => this._GLTranDesc;
    set => this._GLTranDesc = value;
  }

  /// <inheritdoc cref="P:PX.Objects.GL.GLTran.Reclassified" />
  [PXDBBool(BqlField = typeof (PX.Objects.GL.GLTran.reclassified))]
  public virtual bool? GLReclassified { get; set; }

  /// <inheritdoc cref="P:PX.Objects.GL.GLTran.ReclassRemainingAmt" />
  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.GL.GLTran.reclassRemainingAmt))]
  public virtual Decimal? GLReclassRemainingAmt { get; set; }

  [Inventory(Enabled = false, BqlField = typeof (PX.Objects.GL.GLTran.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [INUnit(typeof (PX.Objects.GL.GLTran.inventoryID), Enabled = false, BqlField = typeof (PX.Objects.GL.GLTran.uOM))]
  public virtual string UOM { get; set; }

  public class PK : PrimaryKeyOf<FAAccrualTran>.By<FAAccrualTran.tranID>
  {
    public static FAAccrualTran Find(PXGraph graph, int? tranID, PKFindOptions options = 0)
    {
      return (FAAccrualTran) PrimaryKeyOf<FAAccrualTran>.By<FAAccrualTran.tranID>.FindBy(graph, (object) tranID, options);
    }
  }

  public static class FK
  {
    public class GLTran : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.gLTranID>
    {
    }

    public class GLTranAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.gLTranAccountID>
    {
    }

    public class GLTranSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.gLTranSubID>
    {
    }

    public class AssetClass : 
      PrimaryKeyOf<FAClass>.By<FAClass.assetID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.classID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.branchID>
    {
    }

    public class GLTranBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.gLTranBranchID>
    {
    }

    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.employeeID>
    {
    }

    public class Department : 
      PrimaryKeyOf<EPDepartment>.By<EPDepartment.departmentID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.department>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FAAccrualTran>.By<FAAccrualTran.inventoryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAAccrualTran.selected>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.tranID>
  {
  }

  public abstract class gLTranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.gLTranID>
  {
  }

  public abstract class gLTranAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.gLTranAccountID>
  {
  }

  public abstract class gLTranSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.gLTranSubID>
  {
  }

  public abstract class gLTranQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.gLTranQty>
  {
  }

  public abstract class gLTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.gLTranAmt>
  {
  }

  public abstract class selectedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.selectedQty>
  {
  }

  public abstract class selectedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.selectedAmt>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.openQty>
  {
  }

  public abstract class openAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.openAmt>
  {
  }

  public abstract class closedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.closedAmt>
  {
  }

  public abstract class closedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.closedQty>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAAccrualTran.unitCost>
  {
  }

  public abstract class reconciled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAAccrualTran.reconciled>
  {
  }

  public abstract class gLTranQtyCalc : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAAccrualTran.gLTranQtyCalc>
  {
  }

  public abstract class gLTranAmtCalc : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAAccrualTran.gLTranAmtCalc>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAAccrualTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAAccrualTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAAccrualTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FAAccrualTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAAccrualTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAAccrualTran.lastModifiedDateTime>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.classID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.branchID>
  {
  }

  public abstract class gLTranBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.gLTranBranchID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.employeeID>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAAccrualTran.department>
  {
  }

  public abstract class component : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAAccrualTran.component>
  {
  }

  public abstract class gLTranDebitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAAccrualTran.gLTranDebitAmt>
  {
  }

  public abstract class gLTranCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAAccrualTran.gLTranCreditAmt>
  {
  }

  public abstract class gLTranOrigQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAAccrualTran.gLTranOrigQty>
  {
  }

  public abstract class gLTranInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FAAccrualTran.gLTranInventoryID>
  {
  }

  public abstract class gLTranModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAAccrualTran.gLTranModule>
  {
  }

  public abstract class gLTranBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAAccrualTran.gLTranBatchNbr>
  {
  }

  public abstract class gLTranUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAAccrualTran.gLTranUOM>
  {
  }

  public abstract class gLTranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FAAccrualTran.gLTranDate>
  {
  }

  public abstract class gLTranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAAccrualTran.gLTranRefNbr>
  {
  }

  public abstract class gLTranReferenceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FAAccrualTran.gLTranReferenceID>
  {
  }

  public abstract class gLTranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAAccrualTran.gLTranDesc>
  {
  }

  public abstract class gLReclassified : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAAccrualTran.gLReclassified>
  {
  }

  public abstract class gLReclassRemainingAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAAccrualTran.gLReclassRemainingAmt>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAAccrualTran.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAAccrualTran.uOM>
  {
  }
}
