// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProformaProgressLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Represents a pro forma invoice line with the <see cref="F:PX.Objects.PM.PMProformaLineType.Progressive">Progressive</see> type. The records of this type are edited through the <strong>Progress
/// Billing</strong> tab of the Pro Forma Invoices (PM307000) form. The DAC is based on the <see cref="T:PX.Objects.PM.PMProformaLine" /> DAC and extends it with the fields relevant to the
/// lines of this type.</summary>
[PXCacheName("Pro Forma Line")]
[PXBreakInheritance]
public class PMProformaProgressLine : PMProformaLine
{
  /// <summary>The reference number of the parent <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see>.</summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.PM.PMProforma.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<PMProforma.refNbr>), Filterable = true)]
  [PXUIField]
  [PXDBDefault(typeof (PMProforma.refNbr))]
  [PXFormula(null, typeof (CountCalc<PMProforma.numberOfLines>))]
  [PXParent(typeof (Select<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMProformaProgressLine.refNbr>>, And<PMProforma.revisionID, Equal<Current<PMProformaProgressLine.revisionID>>, And<Current<PMProformaProgressLine.type>, Equal<PMProformaLineType.progressive>>>>>))]
  public override 
  #nullable disable
  string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>The type of the pro forma invoice line.</summary>
  /// <value>
  /// Defaults to the <see cref="F:PX.Objects.PM.PMProformaLineType.Progressive">Progressive</see> type.
  /// </value>
  [PXDBString(1)]
  [PXDefault("P")]
  public override string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the pro forma invoice line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMProformaLine.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (PMProformaLine.projectID), "AR", DisplayName = "Project Task", AllowCompleted = true, Enabled = false, Required = true)]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>), "Task Type is not valid", new System.Type[] {typeof (PMTask.taskCD)})]
  public override int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  /// <inheritdoc />
  [PXDefault(typeof (Search<PX.Objects.GL.Account.accountGroupID, Where<PX.Objects.GL.Account.accountID, Equal<Current<PMProformaProgressLine.accountID>>>>))]
  [AccountGroup]
  [PXForeignReference(typeof (Field<PMProformaProgressLine.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public override int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <inheritdoc />
  [Account(typeof (PMProformaProgressLine.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where<PX.Objects.GL.Account.accountGroupID, IsNotNull>>), DisplayName = "Sales Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  [PXDefault]
  [PXForeignReference(typeof (Field<PMProformaProgressLine.accountID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public override int? AccountID { get; set; }

  /// <inheritdoc />
  [SubAccount(typeof (PMProformaProgressLine.accountID), typeof (PMProformaProgressLine.branchID), true)]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.TX.TaxCategory">tax category</see> associated with the pro forma invoice line.</summary>
  /// <value>
  /// Defaults to the tax category of the corresponding revenue budget line.
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PMTax(typeof (PMProforma), typeof (PMTax), typeof (PMTaxTran))]
  [PMRetainedTax(typeof (PMProforma), typeof (PMTax), typeof (PMTaxTran))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXDefault(typeof (Search<PMBudget.taxCategoryID, Where<PMBudget.projectID, Equal<Current<PMProformaLine.projectID>>, And<PMBudget.projectTaskID, Equal<Current<PMProformaProgressLine.taskID>>, And<PMBudget.accountGroupID, Equal<Current<PMProformaProgressLine.accountGroupID>>, And<PMBudget.inventoryID, Equal<Current<PMProformaProgressLine.inventoryID>>, And<PMBudget.costCodeID, Equal<Current<PMProformaLine.costCodeID>>>>>>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public override string TaxCategoryID { get; set; }

  /// <inheritdoc />
  [PXFormula(typeof (Sub<Add<Add<PMProformaLine.curyAmount, PMProformaLine.curyMaterialStoredAmount>, PMProformaLine.curyTimeMaterialAmount>, PMProformaLine.curyPrepaidAmount>), typeof (SumCalc<PMProforma.curyProgressiveTotal>))]
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount to Invoice")]
  public override Decimal? CuryLineTotal { get; set; }

  /// <inheritdoc />
  [PXFormula(typeof (Mult<PMProformaProgressLine.curyLineTotal, Div<PMProformaLine.retainagePct, decimal100>>), typeof (SumCalc<PMProforma.curyRetainageDetailTotal>))]
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProformaLine.retainage))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Amount", FieldClass = "Retainage")]
  public override Decimal? CuryRetainage { get; set; }

  [PMUnit(typeof (PMProformaProgressLine.inventoryID), Enabled = false)]
  public override string UOM { get; set; }

  /// <inheritdoc />
  [PXDBString]
  [PXDefault(typeof (Search<PMTask.progressBillingBase, Where<BqlOperand<PMTask.taskID, IBqlInt>.IsEqual<BqlField<PMProformaProgressLine.taskID, IBqlInt>.FromCurrent>>>))]
  [PXUIField(DisplayName = "Progress Billing Basis", Required = true)]
  [PX.Objects.PM.ProgressBillingBase.List]
  public override string ProgressBillingBase { get; set; }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaProgressLine.refNbr>
  {
    public const int Length = 15;
  }

  public new abstract class revisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaProgressLine.revisionID>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaProgressLine.lineNbr>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaProgressLine.type>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaProgressLine.inventoryID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaProgressLine.branchID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaProgressLine.taskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProformaProgressLine.accountGroupID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaProgressLine.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaProgressLine.subID>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaProgressLine.taxCategoryID>
  {
  }

  public new abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaProgressLine.curyLineTotal>
  {
  }

  public new abstract class curyRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaProgressLine.curyRetainage>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaProgressLine.uOM>
  {
  }

  public new abstract class isPrepayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProformaProgressLine.isPrepayment>
  {
  }

  public new abstract class progressBillingBase : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProformaProgressLine.progressBillingBase>
  {
  }

  public new abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaProgressLine.curyUnitPrice>
  {
  }
}
