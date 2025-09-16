// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FATran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("Fixed Asset Transaction")]
[Serializable]
public class FATran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IFALocation
{
  protected 
  #nullable disable
  string _RefNbr;
  protected int? _LineNbr;
  protected int? _AssetID;
  protected int? _BookID;
  protected DateTime? _ReceiptDate;
  protected DateTime? _DeprFromDate;
  protected DateTime? _TranDate;
  protected string _FinPeriodID;
  protected string _TranPeriodID;
  protected string _TranType;
  protected int? _DebitAccountID;
  protected int? _DebitSubID;
  protected int? _CreditAccountID;
  protected int? _CreditSubID;
  protected Decimal? _TranAmt;
  protected Decimal? _RGOLAmt;
  protected string _MethodDesc;
  protected string _TranDesc;
  protected string _BatchNbr;
  protected bool? _Released;
  protected bool? _Posted;
  protected string _Origin;
  protected int? _ClassID;
  protected int? _TargetAssetID;
  protected int? _BranchID;
  protected int? _EmployeeID;
  protected string _Department;
  protected bool? _NewAsset = new bool?(true);
  protected bool? _Component = new bool?(false);
  protected int? _GLTranID;
  protected int? _TranID;
  protected Guid? _NoteID;
  protected int? _SrcBranchID;
  protected Decimal? _Qty;
  protected string _AssetCD;
  protected bool? _ReclassificationOnDebitProhibited;
  protected bool? _ReclassificationOnCreditProhibited;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// Indicates whether the record is selected for mass processing or not.
  /// </summary>
  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (FARegister.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FARegister, Where<FARegister.refNbr, Equal<Current<FATran.refNbr>>>>))]
  [PXUIField]
  [PXSelector(typeof (FARegister.refNbr))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (FARegister.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (FixedAsset.assetID))]
  [PXSelector(typeof (Search2<FixedAsset.assetID, LeftJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>, LeftJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FADetails.assetID>, And<FALocationHistory.revisionID, Equal<FADetails.locationRevID>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<FALocationHistory.locationID>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FALocationHistory.employeeID>>>>>>, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>>), new Type[] {typeof (FixedAsset.assetCD), typeof (FixedAsset.description), typeof (FixedAsset.classID), typeof (FixedAsset.usefulLife), typeof (FixedAsset.assetTypeID), typeof (FADetails.status), typeof (PX.Objects.GL.Branch.branchCD), typeof (EPEmployee.acctName), typeof (FALocationHistory.department)}, Filterable = true, SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXSelector(typeof (Search2<FABook.bookID, InnerJoin<FABookBalance, On<FABookBalance.bookID, Equal<FABook.bookID>>>, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Receipt Date")]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  [PXDate]
  [PXDefault(typeof (Switch<Case<Where<Selector<FATran.classID, FixedAsset.underConstruction>, NotEqual<True>>, FATran.receiptDate>, Null>))]
  [PXUIField(DisplayName = "Placed-in-Service Date")]
  public virtual DateTime? DeprFromDate
  {
    get => this._DeprFromDate;
    set => this._DeprFromDate = value;
  }

  [PXDBDate]
  [PXDefault(typeof (FARegister.docDate))]
  [PXUIField(DisplayName = "Tran. Date")]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXUIField(DisplayName = "Tran. Period")]
  [FABookPeriodSelector(null, null, null, typeof (FATran.bookID), true, typeof (FATran.assetID), typeof (FATran.tranDate), null, null, null, null, ReportParametersFlag.None)]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [FABookPeriodID(typeof (FATran.bookID), null, true, typeof (FATran.assetID), null, null, null, null, null)]
  [PXFormula(typeof (RowExt<FATran.finPeriodID>))]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [FATran.tranType.List]
  [PXUIField]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [Account]
  public virtual int? DebitAccountID
  {
    get => this._DebitAccountID;
    set => this._DebitAccountID = value;
  }

  [SubAccount(typeof (FATran.debitAccountID))]
  public virtual int? DebitSubID
  {
    get => this._DebitSubID;
    set => this._DebitSubID = value;
  }

  [Account]
  public virtual int? CreditAccountID
  {
    get => this._CreditAccountID;
    set => this._CreditAccountID = value;
  }

  [SubAccount(typeof (FATran.creditAccountID))]
  public virtual int? CreditSubID
  {
    get => this._CreditSubID;
    set => this._CreditSubID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Transaction Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (AddCalc<FAAccrualTran.closedAmt>))]
  [PXFormula(null, typeof (SumCalc<FARegister.tranAmt>))]
  public virtual Decimal? TranAmt
  {
    get => this._TranAmt;
    set => this._TranAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "RGOL Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Method", Enabled = false)]
  public virtual string MethodDesc
  {
    get => this._MethodDesc;
    set => this._MethodDesc = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Transaction Description")]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleFA>>>))]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Posted
  {
    get => this._Posted;
    set => this._Posted = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (FARegister.origin))]
  [PXUIField(Visible = false)]
  public virtual string Origin
  {
    get => this._Origin;
    set => this._Origin = value;
  }

  [PXInt]
  public virtual int? ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  [PXInt]
  public virtual int? TargetAssetID
  {
    get => this._TargetAssetID;
    set => this._TargetAssetID = value;
  }

  [Branch(typeof (Search<FixedAsset.branchID, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>), null, true, true, true, IsDetail = false)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXInt]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXString(10, IsUnicode = true)]
  public virtual string Department
  {
    get => this._Department;
    set => this._Department = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Create Asset")]
  public virtual bool? NewAsset
  {
    get => this._NewAsset;
    set => this._NewAsset = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Component")]
  public virtual bool? Component
  {
    get => this._Component;
    set => this._Component = value;
  }

  [PXDBInt]
  [PXParent(typeof (Select<FAAccrualTran, Where<FAAccrualTran.tranID, Equal<Current<FATran.gLtranID>>, And<Current<FATran.origin>, NotEqual<FARegister.origin.reversal>>>>))]
  public virtual int? GLTranID
  {
    get => this._GLTranID;
    set => this._GLTranID = value;
  }

  [PXDBIdentity]
  public virtual int? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [Branch(null, null, false, true, true)]
  [PXUIField(DisplayName = "From Branch")]
  public virtual int? SrcBranchID
  {
    get => this._SrcBranchID;
    set => this._SrcBranchID = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Quantity")]
  [PXFormula(null, typeof (AddCalc<FAAccrualTran.closedQty>))]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Asset ID")]
  public virtual string AssetCD
  {
    get => this._AssetCD;
    set => this._AssetCD = value;
  }

  [PXBool]
  public virtual bool? Depreciable { get; set; }

  public virtual bool IsOriginReversal => this.Origin == "V";

  public virtual bool IsTransfer => this.TranType == "TD" || this.TranType == "TP";

  [PXDBBool]
  [PXUIField]
  public virtual bool? ReclassificationOnDebitProhibited
  {
    get => this._ReclassificationOnDebitProhibited;
    set => this._ReclassificationOnDebitProhibited = value;
  }

  [PXDBBool]
  [PXUIField]
  public virtual bool? ReclassificationOnCreditProhibited
  {
    get => this._ReclassificationOnCreditProhibited;
    set => this._ReclassificationOnCreditProhibited = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public string GetKeyImage()
  {
    return $"{typeof (FATran.refNbr).Name}:{this.RefNbr}, {typeof (FATran.lineNbr).Name}:{this.LineNbr}";
  }

  public virtual string ToString()
  {
    return $"{EntityHelper.GetFriendlyEntityName(typeof (FATran))}[{this.GetKeyImage()}]";
  }

  public class PK : PrimaryKeyOf<FATran>.By<FATran.refNbr, FATran.lineNbr>
  {
    public static FATran Find(PXGraph graph, string refNbr, int? lineNbr, PKFindOptions options = 0)
    {
      return (FATran) PrimaryKeyOf<FATran>.By<FATran.refNbr, FATran.lineNbr>.FindBy(graph, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.ForeignKeyOf<FATran>.By<FATran.assetID>
    {
    }

    public class Book : PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FATran>.By<FATran.bookID>
    {
    }

    public class DebitAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FATran>.By<FATran.debitAccountID>
    {
    }

    public class DebitSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<FATran>.By<FATran.debitSubID>
    {
    }

    public class CreditAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FATran>.By<FATran.creditAccountID>
    {
    }

    public class CreditSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<FATran>.By<FATran.creditSubID>
    {
    }

    public class AssetClass : 
      PrimaryKeyOf<FAClass>.By<FAClass.assetID>.ForeignKeyOf<FATran>.By<FATran.classID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FATran>.By<FATran.branchID>
    {
    }

    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FATran>.By<FATran.employeeID>
    {
    }

    public class Department : 
      PrimaryKeyOf<EPDepartment>.By<EPDepartment.departmentID>.ForeignKeyOf<FATran>.By<FATran.department>
    {
    }

    public class FAAccrualTransaction : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FAAccrualTran>.By<FATran.gLtranID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FATran.selected>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.lineNbr>
  {
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.bookID>
  {
  }

  public abstract class receiptDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FATran.receiptDate>
  {
  }

  public abstract class deprFromDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FATran.deprFromDate>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FATran.tranDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.tranPeriodID>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.tranType>
  {
    public const string PurchasingPlus = "P+";
    public const string PurchasingMinus = "P-";
    public const string DepreciationPlus = "D+";
    public const string DepreciationMinus = "D-";
    public const string CalculatedPlus = "C+";
    public const string CalculatedMinus = "C-";
    public const string SalePlus = "S+";
    public const string SaleMinus = "S-";
    public const string TransferPurchasing = "TP";
    public const string TransferDepreciation = "TD";
    public const string ReconciliationPlus = "R+";
    public const string ReconciliationMinus = "R-";
    public const string PurchasingDisposal = "PD";
    public const string PurchasingReversal = "PR";
    public const string AdjustingDeprPlus = "A+";
    public const string AdjustingDeprMinus = "A-";

    public class CustomListAttribute(string[] AllowedValues, string[] AllowedLabels) : 
      PXStringListAttribute(AllowedValues, AllowedLabels)
    {
      public string[] AllowedValues => this._AllowedValues;

      public string[] AllowedLabels => this._AllowedLabels;
    }

    public class ListAttribute : FATran.tranType.CustomListAttribute
    {
      public ListAttribute()
        : base(new string[16 /*0x10*/]
        {
          "P+",
          "P-",
          "D+",
          "D-",
          "C+",
          "C-",
          "S+",
          "S-",
          "TP",
          "TD",
          "R+",
          "R-",
          "PD",
          "PR",
          "A+",
          "A-"
        }, new string[16 /*0x10*/]
        {
          "Purchasing+",
          "Purchasing-",
          "Depreciation+",
          "Depreciation-",
          "Calculated+",
          "Calculated-",
          "Sale/Dispose+",
          "Sale/Dispose-",
          "Transfer Purchasing",
          "Transfer Depreciation",
          "Reconciliation+",
          "Reconciliation-",
          "Purchasing Disposal",
          "Purchasing Reversal",
          "Depreciation Adjusting+",
          "Depreciation Adjusting-"
        })
      {
      }
    }

    public class AdjustmentListAttribute : FATran.tranType.CustomListAttribute
    {
      public AdjustmentListAttribute()
        : base(new string[2]{ "P+", "D+" }, new string[2]
        {
          "Purchasing+",
          "Depreciation+"
        })
      {
      }
    }

    public class NonDepreciableListAttribute : FATran.tranType.CustomListAttribute
    {
      public NonDepreciableListAttribute()
        : base(new string[1]{ "P+" }, new string[1]
        {
          "Purchasing+"
        })
      {
      }
    }

    public class purchasingPlus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.purchasingPlus>
    {
      public purchasingPlus()
        : base("P+")
      {
      }
    }

    public class purchasingMinus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.purchasingMinus>
    {
      public purchasingMinus()
        : base("P-")
      {
      }
    }

    public class depreciationPlus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.depreciationPlus>
    {
      public depreciationPlus()
        : base("D+")
      {
      }
    }

    public class depreciationMinus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.depreciationMinus>
    {
      public depreciationMinus()
        : base("D-")
      {
      }
    }

    public class calculatedPlus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.calculatedPlus>
    {
      public calculatedPlus()
        : base("C+")
      {
      }
    }

    public class calculatedMinus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.calculatedMinus>
    {
      public calculatedMinus()
        : base("C-")
      {
      }
    }

    public class salePlus : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FATran.tranType.salePlus>
    {
      public salePlus()
        : base("S+")
      {
      }
    }

    public class saleMinus : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FATran.tranType.saleMinus>
    {
      public saleMinus()
        : base("S-")
      {
      }
    }

    public class transferPurchasing : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.transferPurchasing>
    {
      public transferPurchasing()
        : base("TP")
      {
      }
    }

    public class transferDepreciation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.transferDepreciation>
    {
      public transferDepreciation()
        : base("TD")
      {
      }
    }

    public class reconcilliationPlus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.reconcilliationPlus>
    {
      public reconcilliationPlus()
        : base("R+")
      {
      }
    }

    public class reconcilliationMinus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.reconcilliationMinus>
    {
      public reconcilliationMinus()
        : base("R-")
      {
      }
    }

    public class purchasingDisposal : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.purchasingDisposal>
    {
      public purchasingDisposal()
        : base("PD")
      {
      }
    }

    public class purchasingReversal : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.purchasingReversal>
    {
      public purchasingReversal()
        : base("PR")
      {
      }
    }

    public class adjustingDeprPlus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.adjustingDeprPlus>
    {
      public adjustingDeprPlus()
        : base("A+")
      {
      }
    }

    public class adjustingDeprMinus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FATran.tranType.adjustingDeprMinus>
    {
      public adjustingDeprMinus()
        : base("A-")
      {
      }
    }
  }

  public abstract class debitAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.debitAccountID>
  {
  }

  public abstract class debitSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.debitSubID>
  {
  }

  public abstract class creditAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.creditAccountID>
  {
  }

  public abstract class creditSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.creditSubID>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FATran.tranAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FATran.rGOLAmt>
  {
  }

  public abstract class methodDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.methodDesc>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.tranDesc>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.batchNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FATran.released>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FATran.posted>
  {
  }

  public abstract class origin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.origin>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.classID>
  {
  }

  public abstract class targetAssetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.targetAssetID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.branchID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.employeeID>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.department>
  {
  }

  public abstract class newAsset : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FATran.newAsset>
  {
  }

  public abstract class component : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FATran.component>
  {
  }

  public abstract class gLtranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.gLtranID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.tranID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FATran.noteID>
  {
  }

  public abstract class srcBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FATran.srcBranchID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FATran.qty>
  {
  }

  public abstract class assetCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FATran.assetCD>
  {
  }

  public abstract class depreciable : IBqlField, IBqlOperand
  {
  }

  public abstract class reclassificationOnDebitProhibited : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FATran.reclassificationOnDebitProhibited>
  {
  }

  public abstract class reclassificationOnCreditProhibited : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FATran.reclassificationOnCreditProhibited>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FATran.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FATran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FATran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FATran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FATran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FATran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FATran.lastModifiedDateTime>
  {
  }
}
