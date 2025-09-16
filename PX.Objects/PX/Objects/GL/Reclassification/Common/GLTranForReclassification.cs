// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Reclassification.Common.GLTranForReclassification
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
using PX.Objects.GL.Attributes;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.GL.Reclassification.Common;

[PXBreakInheritance]
public class GLTranForReclassification : GLTran
{
  protected int? _NewBranchID;
  protected int? _NewAccountID;
  protected int? _NewSubID;
  protected DateTime? _NewTranDate;
  protected 
  #nullable disable
  string _NewFinPeriodID;
  protected string _NewTranDesc;
  protected string _CuryID;

  [PXUIField(DisplayName = "", IsReadOnly = true, Visible = false)]
  [PXImage]
  public virtual string SplittedIcon { get; set; }

  [PXDBBaseCury(typeof (GLTran.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DebitAmt
  {
    get => this._DebitAmt;
    set => this._DebitAmt = value;
  }

  [PXDBBaseCury(typeof (GLTran.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CreditAmt
  {
    get => this._CreditAmt;
    set => this._CreditAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXDBCurrency(typeof (GLTran.curyInfoID), typeof (GLTran.debitAmt))]
  public override Decimal? CuryDebitAmt
  {
    get => this._CuryDebitAmt;
    set => this._CuryDebitAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXDBCurrency(typeof (GLTran.curyInfoID), typeof (GLTran.creditAmt))]
  public override Decimal? CuryCreditAmt
  {
    get => this._CuryCreditAmt;
    set => this._CuryCreditAmt = value;
  }

  [PXDBInt]
  [PXDefault]
  public override int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<BAccountR.bAccountID>), new System.Type[] {typeof (BAccountR.bAccountID), typeof (BAccountR.acctName), typeof (BAccountR.type)}, SubstituteKey = typeof (BAccountR.acctCD))]
  [CustomerVendorRestrictor]
  [PXUIField(DisplayName = "Customer/Vendor", Enabled = false, Visible = false)]
  public override int? ReferenceID
  {
    get => this._ReferenceID;
    set => this._ReferenceID = value;
  }

  [Branch(typeof (GLTranForReclassification.branchID), null, true, true, false, DisplayName = "To Branch", IsDBField = false)]
  public virtual int? NewBranchID
  {
    get => this._NewBranchID;
    set => this._NewBranchID = value;
  }

  [Account(typeof (GLTranForReclassification.newBranchID), LedgerID = typeof (GLTran.ledgerID), DescriptionField = typeof (PX.Objects.GL.Account.description), DisplayName = "To Account", IsDBField = false, AvoidControlAccounts = true)]
  public virtual int? NewAccountID
  {
    get => this._NewAccountID;
    set => this._NewAccountID = value;
  }

  [SubAccount(typeof (GLTranForReclassification.newAccountID), typeof (GLTranForReclassification.newBranchID), false, DisplayName = "To Subaccount", IsDBField = false)]
  public virtual int? NewSubID
  {
    get => this._NewSubID;
    set => this._NewSubID = value;
  }

  public virtual string NewSubCD { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "New Tran. Date")]
  public virtual DateTime? NewTranDate
  {
    get => this._NewTranDate;
    set => this._NewTranDate = value;
  }

  [OpenPeriod(null, typeof (GLTranForReclassification.newTranDate), typeof (GLTranForReclassification.newBranchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, true, IsDBField = false, RedefaultOrRevalidateOnOrganizationSourceUpdated = false, RedefaultOnDateChanged = false)]
  public virtual string NewFinPeriodID
  {
    get => this._NewFinPeriodID;
    set => this._NewFinPeriodID = value;
  }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "New Transaction Description")]
  public virtual string NewTranDesc
  {
    get => this._NewTranDesc;
    set => this._NewTranDesc = value;
  }

  [GLProjectDefault(typeof (GLTran.ledgerID))]
  [ActiveProject(AccountFieldType = typeof (GLTranForReclassification.newAccountID), IsDBField = false, DisplayName = "To Project")]
  public virtual int? NewProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<GLTranForReclassification.newProjectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [BaseProjectTask(typeof (GLTranForReclassification.newProjectID), "GL", IsDBField = false, DisplayName = "To Project Task", AllowInactive = false)]
  [PXUIEnabled(typeof (Where<FeatureInstalled<FeaturesSet.projectAccounting>>))]
  [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.projectAccounting>>))]
  public virtual int? NewTaskID { get; set; }

  [PXForeignReference(typeof (Field<GLTran.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [CostCode(typeof (GLTranForReclassification.newAccountID), typeof (GLTranForReclassification.newTaskID))]
  [PXUIEnabled(typeof (Where<FeatureInstalled<FeaturesSet.costCodes>>))]
  [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.costCodes>>))]
  public virtual int? NewCostCodeID { get; set; }

  [PXDefault]
  [PXCurrency(typeof (GLTran.curyInfoID), typeof (GLTranForReclassification.newAmt))]
  [PXUIField(DisplayName = "New Amount", Visible = false)]
  public virtual Decimal? CuryNewAmt { get; set; }

  [PXBaseCury(typeof (GLTran.ledgerID))]
  public virtual Decimal? NewAmt { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXInt]
  [PXDefault]
  public virtual int? SortOrder { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SourceCuryDebitAmt { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SourceCuryCreditAmt { get; set; }

  public virtual ReclassRowTypes ReclassRowType { get; set; }

  public virtual int? EditingPairReclassifyingLineNbr { get; set; }

  public virtual GLTranKey ParentKey { get; set; }

  public bool IsSplitting => this.ParentKey != null;

  /// <summary>This field is used for UI only.</summary>
  public bool IsSplitted { get; set; }

  public new abstract class branchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranForReclassification.branchID>
  {
  }

  public abstract class splittedIcon : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranForReclassification.splittedIcon>
  {
  }

  public new abstract class referenceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranForReclassification.referenceID>
  {
  }

  public abstract class newBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranForReclassification.newBranchID>
  {
  }

  public abstract class newAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranForReclassification.newAccountID>
  {
  }

  public abstract class newSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranForReclassification.newSubID>
  {
  }

  public abstract class newTranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTranForReclassification.newTranDate>
  {
  }

  public abstract class newFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranForReclassification.newFinPeriodID>
  {
  }

  public abstract class newTranDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranForReclassification.newTranDesc>
  {
  }

  public abstract class newProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranForReclassification.newProjectID>
  {
  }

  public abstract class newTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranForReclassification.newTaskID>
  {
  }

  public abstract class newCostCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranForReclassification.newCostCodeID>
  {
  }

  public abstract class curyNewAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranForReclassification.curyNewAmt>
  {
  }

  public abstract class newAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranForReclassification.newAmt>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranForReclassification.curyID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranForReclassification.sortOrder>
  {
  }

  public class ExceptionAndErrorValuesTriple
  {
    public Exception Error;
    public object ErrorValue;
    public object ErrorUIValue;
  }
}
