// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWipAdjustment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Contains the main properties of a project WIP adjustment transaction.
/// The records of this type are created and edited on the WIP Adjustment (PM305600) form.
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectWipAdjustmentEntry" /> graph).
/// </summary>
[PXCacheName("Project WIP Adjustment")]
[PXPrimaryGraph(typeof (ProjectWipAdjustmentEntry))]
[Serializable]
public class PMWipAdjustment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign, INotable
{
  /// <summary>The transaction identifier.</summary>
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMWipAdjustment, TypeArrayOf<IFbqlJoin>.Empty>, PMWipAdjustment>.SearchFor<PMWipAdjustment.refNbr>), new System.Type[] {typeof (PMWipAdjustment.refNbr), typeof (PMWipAdjustment.status), typeof (PMWipAdjustment.date), typeof (PMWipAdjustment.projectionDate)}, DescriptionField = typeof (PMWipAdjustment.description))]
  [AutoNumber(typeof (Search<PMSetup.wipAdjustmentNumbering>), typeof (AccessInfo.businessDate))]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [PXDefault(typeof (Current<AccessInfo.branchID>))]
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Extensions.Currency" /> of the transaction.
  /// </summary>
  /// <value>
  /// It is set to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see> by default.
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXUIField(DisplayName = "Currency")]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<True>>>), new System.Type[] {typeof (CurrencyList.curyID), typeof (CurrencyList.description)}, CacheGlobal = true)]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The default <see cref="T:PX.Objects.CM.Extensions.CurrencyRateType">rate type</see> for the currency rate that is used for the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Extensions.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (PX.Objects.CM.Extensions.CurrencyRateType.curyRateTypeID), DescriptionField = typeof (PX.Objects.CM.Extensions.CurrencyRateType.descr))]
  [PXUIField(DisplayName = "Currency Rate Type")]
  public virtual string RateTypeID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> record associated with the transaction.
  /// </summary>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The status of the transaction.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.ProjectWipAdjustmentStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ProjectWipAdjustmentStatus.List]
  [PXDefault("H")]
  [PXUIField]
  public virtual string Status { get; set; }

  /// <summary>The date when the transaction was created.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? Date { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the transaction.
  /// </summary>
  /// <value>
  /// Determined by the <see cref="P:PX.Objects.PM.PMWipAdjustment.ProjectionDate">projection date</see> of the transaction. Unlike <see cref="P:PX.Objects.PM.PMWipAdjustment.FinPeriodID" />,
  /// the value of this field can't be overridden by the user.
  /// </value>
  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the transaction.
  /// </summary>
  /// <value>
  /// By default, it is set to the period to which the <see cref="P:PX.Objects.PM.PMWipAdjustment.ProjectionDate" /> belongs but can be overridden by the user.
  /// </value>
  [AROpenPeriod(typeof (PMWipAdjustment.projectionDate), typeof (PMWipAdjustment.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (PMWipAdjustment.tranPeriodID), IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  /// <summary>The date that the transaction was projected to.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Projection Date")]
  public virtual DateTime? ProjectionDate { get; set; }

  /// <summary>The counter of the transaction lines.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the transaction is on hold.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the transaction is approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the transaction is rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? Rejected { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the transaction is released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>
  /// The status of projects for which the transaction has been generated.
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.ProjectStatus.ListAttribute" />.
  /// </summary>
  [PXDBString(10)]
  [ProjectStatuses.ProjectStatusList]
  [PXDefault("A")]
  [PXUIField]
  public virtual string ProjectStatus { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.EP.EPEmployee">employee</see> responsible for the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [Owner(typeof (PMWipAdjustment.workgroupID))]
  [PXFormula(typeof (Default<PMWipAdjustment.workgroupID>))]
  [PXDefault(typeof (Coalesce<Search<EPCompanyTreeMember.contactID, Where<EPCompanyTreeMember.workGroupID, Equal<Current<PMWipAdjustment.workgroupID>>, And<EPCompanyTreeMember.contactID, Equal<Current<AccessInfo.contactID>>>>>, Search<CREmployee.defContactID, Where<CREmployee.userID, Equal<Current<AccessInfo.userID>>, And<Current<PMWipAdjustment.workgroupID>, IsNull>>>>))]
  public virtual int? OwnerID { get; set; }

  /// <summary>
  /// The workgroup that is responsible for the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// The number of the <see cref="T:PX.Objects.GL.Batch" /> that is created when the transaction is released.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<Batch.batchNbr>))]
  public virtual string BatchNbr { get; set; }

  /// <summary>
  /// The posting option of the Overbilling/Underbilling amounts.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PostingOptions.OverbillingUnderbillingOptionsListAttribute" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PostingOptions.OverbillingUnderbillingOptionsList]
  [PXDefault(typeof (BqlField<PMSetup.wipAdjustmentOverbillingUnderbillingOption, IBqlString>.FromSetup))]
  [PXUIField(DisplayName = "Overbilling/Underbilling posting option")]
  public virtual string OverbillingUnderbillingOption { get; set; }

  /// <summary>The default overbilling account.</summary>
  [Account(typeof (PMWipAdjustment.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.type, Equal<AccountType.liability>, And<PX.Objects.GL.Account.isCashAccount, NotEqual<True>>>>>>))]
  [PXDefault(typeof (BqlField<PMSetup.wipAdjustmentOverbillingAccountID, IBqlInt>.FromSetup))]
  public virtual int? OverbillingAccountID { get; set; }

  /// <summary>The default overbilling subaccount.</summary>
  [SubAccount(typeof (PMWipAdjustment.overbillingAccountID), typeof (PMWipAdjustment.branchID), true)]
  [PXDefault(typeof (BqlField<PMSetup.wipAdjustmentOverbillingSubID, IBqlInt>.FromSetup))]
  public virtual int? OverbillingSubID { get; set; }

  /// <summary>The default underbilling account.</summary>
  [Account(typeof (PMWipAdjustment.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.type, Equal<AccountType.asset>, And<PX.Objects.GL.Account.isCashAccount, NotEqual<True>>>>>>))]
  [PXDefault(typeof (BqlField<PMSetup.wipAdjustmentUnderbillingAccountID, IBqlInt>.FromSetup))]
  public virtual int? UnderbillingAccountID { get; set; }

  /// <summary>The default underbilling subaccount.</summary>
  [SubAccount(typeof (PMWipAdjustment.underbillingAccountID), typeof (PMWipAdjustment.branchID), true)]
  [PXDefault(typeof (BqlField<PMSetup.wipAdjustmentUnderbillingSubID, IBqlInt>.FromSetup))]
  public virtual int? UnderbillingSubID { get; set; }

  /// <summary>The posting option of the Revenue amounts.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PostingOptions.RevenueOptionsListAttribute" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PostingOptions.RevenueOptionsList]
  [PXDefault(typeof (BqlField<PMSetup.wipAdjustmentRevenueOption, IBqlString>.FromSetup))]
  [PXUIField(DisplayName = "Revenue Posting Option")]
  public virtual string RevenueOption { get; set; }

  /// <summary>The default revenue subaccount.</summary>
  [Account(typeof (PMWipAdjustment.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.type, Equal<AccountType.income>, And<PX.Objects.GL.Account.isCashAccount, NotEqual<True>>>>>>))]
  [PXDefault(typeof (BqlField<PMSetup.wipAdjustmentRevenueAccountID, IBqlInt>.FromSetup))]
  public virtual int? RevenueAccountID { get; set; }

  /// <summary>The revenue default subaccount.</summary>
  [SubAccount(typeof (PMWipAdjustment.revenueAccountID), typeof (PMWipAdjustment.branchID), true)]
  [PXDefault(typeof (BqlField<PMSetup.wipAdjustmentRevenueSubID, IBqlInt>.FromSetup))]
  public virtual int? RevenueSubID { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryOverbillingAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustment.overbillingAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overbilling Amount", Enabled = false)]
  public virtual Decimal? CuryOverbillingAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.OverbillingAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OverbillingAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryUnderbillingAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustment.underbillingAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Underbilling Amount", Enabled = false)]
  public virtual Decimal? CuryUnderbillingAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.UnderbillingAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnderbillingAmount { get; set; }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryOverbillingAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryUnderbillingAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustment.totalAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Amount", Enabled = false)]
  public virtual Decimal? CuryTotalAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMWipAdjustment.curyOverbillingAmount), typeof (PMWipAdjustment.curyUnderbillingAmount)})] get
    {
      Decimal? nullable = this.CuryOverbillingAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryUnderbillingAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.OverbillingAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.UnderbillingAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMWipAdjustment.overbillingAmount), typeof (PMWipAdjustment.underbillingAmount)})] get
    {
      Decimal? nullable = this.OverbillingAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.UnderbillingAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryOverbillingAdjustmentAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustment.overbillingAdjustmentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overbilling Adjustment", Enabled = false)]
  public virtual Decimal? CuryOverbillingAdjustmentAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.OverbillingAdjustmentAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OverbillingAdjustmentAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryUnderbillingAdjustmentAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustment.underbillingAdjustmentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Underbilling Adjustment", Enabled = false)]
  public virtual Decimal? CuryUnderbillingAdjustmentAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.UnderbillingAdjustmentAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnderbillingAdjustmentAmount { get; set; }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryOverbillingAdjustmentAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryUnderbillingAdjustmentAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustment.totalAdjustmentAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Adjustment", Enabled = false)]
  public virtual Decimal? CuryTotalAdjustmentAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMWipAdjustment.curyOverbillingAdjustmentAmount), typeof (PMWipAdjustment.curyUnderbillingAdjustmentAmount)})] get
    {
      Decimal? adjustmentAmount = this.CuryOverbillingAdjustmentAmount;
      Decimal valueOrDefault1 = adjustmentAmount.GetValueOrDefault();
      adjustmentAmount = this.CuryUnderbillingAdjustmentAmount;
      Decimal valueOrDefault2 = adjustmentAmount.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.OverbillingAdjustmentAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.UnderbillingAdjustmentAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalAdjustmentAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMWipAdjustment.overbillingAdjustmentAmount), typeof (PMWipAdjustment.underbillingAdjustmentAmount)})] get
    {
      Decimal? adjustmentAmount = this.OverbillingAdjustmentAmount;
      Decimal valueOrDefault1 = adjustmentAmount.GetValueOrDefault();
      adjustmentAmount = this.UnderbillingAdjustmentAmount;
      Decimal valueOrDefault2 = adjustmentAmount.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that change orders with the Pending Approval status are included in calculations.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Pending CO in Calculations")]
  public virtual bool? IncludePendingChangeOrders { get; set; }

  /// <exclude />
  [PXNote(DescriptionField = typeof (PMWipAdjustment.description))]
  public virtual Guid? NoteID { get; set; }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <exclude />
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  /// <exclude />
  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  /// <exclude />
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  /// <exclude />
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  /// <exclude />
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  /// <exclude />
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<PMWipAdjustment>.By<PMWipAdjustment.refNbr>
  {
    public static PMWipAdjustment Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (PMWipAdjustment) PrimaryKeyOf<PMWipAdjustment>.By<PMWipAdjustment.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipAdjustment.refNbr>
  {
    public const int Length = 30;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipAdjustment.branchID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipAdjustment.curyID>
  {
  }

  public abstract class rateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipAdjustment.rateTypeID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMWipAdjustment.curyInfoID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipAdjustment.status>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMWipAdjustment.date>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustment.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipAdjustment.finPeriodID>
  {
  }

  public abstract class projectionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWipAdjustment.projectionDate>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipAdjustment.lineCntr>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipAdjustment.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipAdjustment.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipAdjustment.rejected>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipAdjustment.released>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipAdjustment.description>
  {
  }

  public abstract class projectStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustment.projectStatus>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipAdjustment.ownerID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipAdjustment.workgroupID>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipAdjustment.batchNbr>
  {
  }

  public abstract class overbillingUnderbillingOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustment.overbillingUnderbillingOption>
  {
  }

  public abstract class overbillingAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustment.overbillingAccountID>
  {
  }

  public abstract class overbillingSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustment.overbillingSubID>
  {
  }

  public abstract class underbillingAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustment.underbillingAccountID>
  {
  }

  public abstract class underbillingSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustment.underbillingSubID>
  {
  }

  public abstract class revenueOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustment.revenueOption>
  {
  }

  public abstract class revenueAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustment.revenueAccountID>
  {
  }

  public abstract class revenueSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipAdjustment.revenueSubID>
  {
  }

  public abstract class curyOverbillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.curyOverbillingAmount>
  {
  }

  public abstract class overbillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.overbillingAmount>
  {
  }

  public abstract class curyUnderbillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.curyUnderbillingAmount>
  {
  }

  public abstract class underbillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.underbillingAmount>
  {
  }

  public abstract class curyTotalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.curyTotalAmount>
  {
  }

  public abstract class totalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.totalAmount>
  {
  }

  public abstract class curyOverbillingAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.curyOverbillingAdjustmentAmount>
  {
  }

  public abstract class overbillingAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.overbillingAdjustmentAmount>
  {
  }

  public abstract class curyUnderbillingAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.curyUnderbillingAdjustmentAmount>
  {
  }

  public abstract class underbillingAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.underbillingAdjustmentAmount>
  {
  }

  public abstract class curyTotalAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.curyTotalAdjustmentAmount>
  {
  }

  public abstract class totalAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustment.totalAdjustmentAmount>
  {
  }

  public abstract class includePendingChangeOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMWipAdjustment.includePendingChangeOrders>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMWipAdjustment.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMWipAdjustment.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMWipAdjustment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWipAdjustment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMWipAdjustment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWipAdjustment.lastModifiedDateTime>
  {
  }
}
