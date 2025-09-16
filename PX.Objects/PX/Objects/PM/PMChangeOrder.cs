// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeOrder
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
using PX.Data.Search;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.TM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Contains the main properties of a change order. The records of this type are created and edited through the Change Orders (PM308000) form (which corresponds to
/// the <see cref="T:PX.Objects.PM.ChangeOrderEntry" /> graph).</summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Change Order")]
[PXPrimaryGraph(typeof (ChangeOrderEntry))]
[Serializable]
public class PMChangeOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign, INotable
{
  public const 
  #nullable disable
  string FieldClass = "CHANGEORDER";
  protected string _Description;
  protected string _Text;
  /// <summary>
  /// A detailed description of the change order as plain text.
  /// </summary>
  private string _plainText;
  protected string _Status;
  protected bool? _Hold;
  protected bool? _Approved;
  protected bool? _Rejected = new bool?(false);
  protected int? _ProjectID;
  protected int? _CustomerID;
  protected string _ExtRefNbr;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected bool? _Released;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>The reference number of the change order.</summary>
  /// <value>The number is generated from the <see cref="T:PX.Objects.CS.Numbering">numbering sequence</see>, which is specified on the <see cref="T:PX.Objects.PM.PMSetup">Projects Preferences</see> (PM101000) form.</value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMChangeOrder.projectID>>>>.Where<MatchUserFor<PMProject>>, PMChangeOrder>.SearchFor<PMChangeOrder.refNbr>), DescriptionField = typeof (PMChangeOrder.description))]
  [AutoNumber(typeof (PX.Data.Search<PMSetup.changeOrderNumbering>), typeof (AccessInfo.businessDate))]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  /// <summary>The change number.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string ProjectNbr { get; set; }

  /// <summary>The identifier of the GL <see cref="T:PX.Objects.PM.PMChangeOrderClass">change order class</see> that provides default settings for the change order.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMChangeOrderClass.ClassID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMChangeOrder.classID>.IsRelatedTo<PMChangeOrderClass.classID>))]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault(typeof (PX.Data.Search<PMSetup.defaultChangeOrderClassID>))]
  [PXUIField]
  [PXSelector(typeof (PX.Data.Search<PMChangeOrderClass.classID, Where<PMChangeOrderClass.isActive, Equal<True>>>), DescriptionField = typeof (PMChangeOrderClass.description))]
  public virtual string ClassID { get; set; }

  /// <summary>The description of the change order.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>A detailed description of the change order.</summary>
  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Details")]
  public virtual string Text
  {
    get => this._Text;
    set
    {
      this._Text = value;
      this._plainText = (string) null;
    }
  }

  [PXString(IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string DescriptionAsPlainText
  {
    get => this._plainText ?? (this._plainText = SearchService.Html2PlainText(this.Text));
  }

  /// <summary>The status of the change order.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"H"</c>: On Hold,
  /// <c>"A"</c>: Pending Approval,
  /// <c>"O"</c>: Open,
  /// <c>"C"</c>: Closed,
  /// <c>"R"</c>: Rejected
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ChangeOrderStatus.List]
  [PXDefault("H")]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is on hold.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Hold")]
  [PXDefault(true)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? Rejected
  {
    get => this._Rejected;
    set => this._Rejected = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the change order.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMProject.contractID" /> field.
  /// </value>
  [PXDefault]
  [PXForeignReference(typeof (Field<PMChangeOrder.projectID>.IsRelatedTo<PMProject.contractID>))]
  [Project(typeof (Where<PX.Objects.CT.Contract.changeOrderWorkflow, Equal<True>, And<PMProject.baseType, Equal<CTPRType.project>>>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The identifier of the customer associated with the project.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXFormula(typeof (Selector<PMChangeOrder.projectID, PMProject.customerID>))]
  [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName), Enabled = false)]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// The date on which the changes made with the change order should be recorded in the project balances.
  /// </summary>
  /// <value>Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.</value>
  [PXDBDate]
  [PMFinPeriodDateValidation]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date { get; set; }

  /// <summary>
  /// The date that has been communicated to the customer as the approval date of the agreed-upon changes.
  /// </summary>
  /// <value>Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.</value>
  [PXDBDate]
  [PMFinPeriodDateValidation]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? CompletionDate { get; set; }

  /// <summary>The external reference number (such as an identifier required by the customer or a number from an external system integrated with Acumatica ERP) entered
  /// manually.</summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr.")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMChangeOrder.RefNbr">reference number</see> of the original change order
  /// whose changes the currently selected change order reverses.
  /// </summary>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Original CO Nbr.")]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>
  /// The list of <see cref="P:PX.Objects.PM.PMChangeOrder.RefNbr">reference number</see> of the change orders
  /// which reverse currently selected change order.
  /// </summary>
  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Reversing CO Nbr.")]
  public virtual string ReversingRefNbr { get; set; }

  /// <summary>The reverse status of the change order.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"N"</c>: None,
  /// <c>"X"</c>: Reversed,
  /// <c>"R"</c>: Reversing
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ChangeOrderReverseStatus.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Reverse Status", Enabled = false)]
  public virtual string ReverseStatus { get; set; }

  /// <summary>
  /// The total <see cref="P:PX.Objects.PM.PMChangeOrderCostBudget.Amount">amount</see> of the
  /// <see cref="T:PX.Objects.PM.PMChangeOrderCostBudget">cost budget lines</see> of the document.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost Budget Change Total")]
  public virtual Decimal? CostTotal { get; set; }

  /// <summary>
  /// The total <see cref="P:PX.Objects.PM.PMChangeOrderRevenueBudget.Amount">amount</see> of the
  /// <see cref="T:PX.Objects.PM.PMChangeOrderRevenueBudget">revenue budget lines</see> of the document.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revenue Budget Change Total")]
  public virtual Decimal? RevenueTotal { get; set; }

  /// <summary>The total <see cref="P:PX.Objects.PM.PMChangeOrderLine.AmountInProjectCury">amount in project currency</see> of the <see cref="T:PX.Objects.PM.PMChangeOrderLine">commitments lines</see> of the document.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commitment Change Total")]
  public virtual Decimal? CommitmentTotal { get; set; }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMChangeOrder.RevenueTotal">Revenue Budget Change Total</see>
  /// and the <see cref="P:PX.Objects.PM.PMChangeOrder.CostTotal">Cost Budget Change Total</see> values.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin Amount")]
  public virtual Decimal? GrossMarginAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMChangeOrder.revenueTotal), typeof (PMChangeOrder.costTotal)})] get
    {
      Decimal? revenueTotal = this.RevenueTotal;
      Decimal? costTotal = this.CostTotal;
      return !(revenueTotal.HasValue & costTotal.HasValue) ? new Decimal?() : new Decimal?(revenueTotal.GetValueOrDefault() - costTotal.GetValueOrDefault());
    }
  }

  /// <summary>The gross margin percent.</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin (%)")]
  public virtual Decimal? GrossMarginPct
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMChangeOrder.revenueTotal), typeof (PMChangeOrder.costTotal)})] get
    {
      Decimal? revenueTotal1 = this.RevenueTotal;
      Decimal num1 = 0M;
      if (revenueTotal1.GetValueOrDefault() == num1 & revenueTotal1.HasValue)
        return new Decimal?(0M);
      Decimal num2 = (Decimal) 100;
      Decimal? revenueTotal2 = this.RevenueTotal;
      Decimal? nullable1 = this.CostTotal;
      Decimal? nullable2 = revenueTotal2.HasValue & nullable1.HasValue ? new Decimal?(revenueTotal2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3;
      if (!nullable2.HasValue)
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(num2 * nullable2.GetValueOrDefault());
      Decimal? nullable4 = nullable3;
      Decimal? revenueTotal3 = this.RevenueTotal;
      return !(nullable4.HasValue & revenueTotal3.HasValue) ? new Decimal?() : new Decimal?(nullable4.GetValueOrDefault() / revenueTotal3.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMChangeRequest.CostTotal">cost total</see> of all the change requests linked to the change order.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Request Cost Total", Enabled = false, FieldClass = "ChangeRequest")]
  public virtual Decimal? ChangeRequestCostTotal { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMChangeRequest.LineTotal">line total</see> of all the change requests linked to the change order.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Request Line Total", Enabled = false, FieldClass = "ChangeRequest")]
  public virtual Decimal? ChangeRequestLineTotal { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMChangeRequest.MarkupTotal">markup total</see> of all the change requests linked to the change order.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Request Markup Total", Enabled = false, FieldClass = "ChangeRequest")]
  public virtual Decimal? ChangeRequestMarkupTotal { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMChangeRequest.PriceTotal">price total</see> of all the change requests linked to the change order.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Request Price Total", Enabled = false, FieldClass = "ChangeRequest")]
  public virtual Decimal? ChangeRequestPriceTotal { get; set; }

  /// <summary>The workgroup that is responsible for the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PX.Objects.CR.BAccount.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  /// <summary>The <see cref="!:Contact">contact</see> responsible for the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="!:Contact.ContactID" /> field.
  /// </value>
  [PXDefault(typeof (PX.Objects.CR.BAccount.ownerID))]
  [Owner(typeof (PMChangeOrder.workgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  /// <summary>A counter of the document lines, which is used internally to assign <see cref="P:PX.Objects.PM.PMChangeOrderLine.LineNbr">numbers</see> to newly created lines. We do not recommend
  /// that you rely on this field to determine the exact number of lines because it might not reflect this number under various conditions.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>A counter of the budget lines, which is used internally to assign <see cref="P:PX.Objects.PM.PMChangeOrderBudget.LineNbr">numbers</see> to newly created lines. We do not recommend
  /// that you rely on this field to determine the exact number of lines because it might not reflect this number under various conditions.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BudgetLineCntr { get; set; }

  /// <summary>
  /// A positive or negative number of days that represents the delay of the contract.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Contract Change (Days)")]
  public virtual int? DelayDays { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Released")]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the <strong>Cost Budget</strong> tab is visible in the change order.</summary>
  [PXBool]
  [PXUIField(DisplayName = "Visible Cost", Enabled = false)]
  [PXUnboundDefault(true)]
  public virtual bool? IsCostVisible { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the <strong>Revenue <span>Budget</span></strong> tab is visible in the change order.</summary>
  [PXBool]
  [PXUIField(DisplayName = "Visible Revenue", Enabled = false)]
  [PXUnboundDefault(true)]
  public virtual bool? IsRevenueVisible { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the <strong>Commitments</strong> tab is visible in the change order.</summary>
  [PXBool]
  [PXUIField(DisplayName = "Visible Details", Enabled = false)]
  [PXUnboundDefault(true)]
  public virtual bool? IsDetailsVisible { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the <strong>Change Requests</strong> tab is visible in the change order.</summary>
  [PXBool]
  [PXUIField(DisplayName = "2-Tier Change Management", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? IsChangeRequestVisible { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<PMChangeOrder.projectID, PMProject.description>))]
  public string FormCaptionDescription { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.PM.PMChangeOrderClass">change order
  /// class</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (PMChangeOrder.classID))]
  public virtual string[] Attributes { get; set; }

  [ChangeOrderSearchable]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (FbqlSelect<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMChangeOrder.projectID>>>>.Where<MatchUserFor<PMProject>>, PMChangeOrder>.SearchFor<PMChangeOrder.refNbr>), DescriptionField = typeof (PMChangeOrder.description))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>Primary Key</summary>
  /// <exclude />
  public class PK : PrimaryKeyOf<PMChangeOrder>.By<PMChangeOrder.refNbr>
  {
    public static PMChangeOrder Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (PMChangeOrder) PrimaryKeyOf<PMChangeOrder>.By<PMChangeOrder.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public static class FK
  {
    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMChangeOrder>.By<PMChangeOrder.projectID>
    {
    }

    /// <summary>Change Order Class</summary>
    /// <exclude />
    public class ChangeOrderClass : 
      PrimaryKeyOf<PMChangeOrderClass>.By<PMChangeOrderClass.classID>.ForeignKeyOf<PMChangeOrder>.By<PMChangeOrder.classID>
    {
    }

    /// <summary>Customer</summary>
    /// <exclude />
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<PMChangeOrder>.By<PMChangeOrder.customerID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrder.refNbr>
  {
    public const int Length = 15;
  }

  public abstract class projectNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrder.projectNbr>
  {
    public const int Length = 15;
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrder.classID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrder.description>
  {
  }

  public abstract class text : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrder.text>
  {
  }

  public abstract class descriptionAsPlainText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrder.descriptionAsPlainText>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrder.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrder.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrder.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrder.rejected>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrder.projectID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrder.customerID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMChangeOrder.date>
  {
  }

  public abstract class completionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrder.completionDate>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrder.extRefNbr>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrder.origRefNbr>
  {
  }

  public abstract class reversingRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrder.reversingRefNbr>
  {
  }

  public abstract class reverseStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrder.reverseStatus>
  {
  }

  public abstract class costTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeOrder.costTotal>
  {
  }

  public abstract class revenueTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrder.revenueTotal>
  {
  }

  public abstract class commitmentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrder.commitmentTotal>
  {
  }

  public abstract class grossMarginAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrder.grossMarginAmount>
  {
  }

  public abstract class grossMarginPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrder.grossMarginPct>
  {
  }

  public abstract class changeRequestCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrder.changeRequestCostTotal>
  {
  }

  public abstract class changeRequestLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrder.changeRequestLineTotal>
  {
  }

  public abstract class changeRequestMarkupTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrder.changeRequestMarkupTotal>
  {
  }

  public abstract class changeRequestPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrder.changeRequestPriceTotal>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrder.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrder.ownerID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrder.lineCntr>
  {
  }

  public abstract class budgetLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrder.budgetLineCntr>
  {
  }

  public abstract class delayDays : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrder.delayDays>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrder.released>
  {
  }

  public abstract class isCostVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrder.isCostVisible>
  {
  }

  public abstract class isRevenueVisible : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeOrder.isRevenueVisible>
  {
  }

  public abstract class isDetailsVisible : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeOrder.isDetailsVisible>
  {
  }

  public abstract class isChangeRequestVisible : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeOrder.isChangeRequestVisible>
  {
  }

  public abstract class attributes : 
    BqlType<IBqlAttributes, string[]>.Field<PMChangeOrder.attributes>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeOrder.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMChangeOrder.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeOrder.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrder.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrder.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMChangeOrder.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrder.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrder.lastModifiedDateTime>
  {
  }
}
