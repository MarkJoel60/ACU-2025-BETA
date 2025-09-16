// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeRequest
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
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.TM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Contains the main properties of a change request. The records of this type are created and edited through the Change Requests (PM308500) form (which corresponds to the
/// <see cref="!:ChangeRequestEntry" /> graph).</summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Change Request")]
[Serializable]
public class PMChangeRequest : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign, INotable
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _Text;
  private string _plainText;
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

  /// <summary>
  /// Gets or sets whether the task is selected in the grid.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>The reference number of the change request.</summary>
  /// <value>
  /// The number is generated from the <see cref="T:PX.Objects.CS.Numbering">numbering sequence</see>,
  /// which is specified on the <see cref="T:PX.Objects.PM.PMSetup">Projects Preferences</see> (PM101000) form.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMChangeRequest, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMChangeRequest.projectID>>>>.Where<MatchUserFor<PMProject>>, PMChangeRequest>.SearchFor<PMChangeRequest.refNbr>), DescriptionField = typeof (PMChangeRequest.description))]
  [AutoNumber(typeof (PX.Data.Search<PMSetup.changeRequestNumbering>), typeof (AccessInfo.businessDate))]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.PM.PMChangeOrder">change order</see> that is linked to the change request.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMChangeOrder.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Change Order Nbr.", Enabled = false)]
  [PXSelector(typeof (PMChangeOrder.refNbr), DescriptionField = typeof (PMChangeOrder.description), DirtyRead = true)]
  public virtual string ChangeOrderNbr { get; set; }

  /// <summary>
  /// The reference number of the cost <see cref="T:PX.Objects.PM.PMChangeOrder">change order</see> that is linked to the change request.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMChangeOrder.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Cost Change Order Nbr.", Enabled = false)]
  [PXSelector(typeof (PMChangeOrder.refNbr), DescriptionField = typeof (PMChangeOrder.description), DirtyRead = true)]
  public virtual string CostChangeOrderNbr { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the linked cost change order is released.
  /// </summary>
  [PXBool]
  [PXDBScalar(typeof (PX.Data.Search<PMChangeOrder.released, Where<PMChangeOrder.refNbr, Equal<PMChangeRequest.costChangeOrderNbr>>>))]
  public virtual bool? CostChangeOrderReleased { get; set; }

  /// <summary>The change number.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string ProjectNbr { get; set; }

  /// <summary>The description of the change request.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>A detailed description of the change request.</summary>
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

  /// <summary>
  /// A detailed description of the change request as plain text.
  /// </summary>
  [PXString(IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string DescriptionAsPlainText
  {
    get => this._plainText ?? (this._plainText = SearchService.Html2PlainText(this.Text));
  }

  /// <summary>The status of the change request.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"H"</c>: On Hold,
  /// <c>"A"</c>: Pending Approval,
  /// <c>"O"</c>: Open,
  /// <c>"C"</c>: Closed,
  /// <c>"R"</c>: Rejected
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ChangeRequestStatus.List]
  [PXDefault("H")]
  [PXUIField]
  public virtual string Status { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is on hold.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Hold")]
  [PXDefault(true)]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? Rejected { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the change request.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDefault]
  [PXForeignReference(typeof (Field<PMChangeRequest.projectID>.IsRelatedTo<PMProject.contractID>))]
  [Project(typeof (Where<PX.Objects.CT.Contract.changeOrderWorkflow, Equal<True>, And<PMProject.baseType, Equal<CTPRType.project>>>))]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The identifier of the customer associated with the project.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXFormula(typeof (Selector<PMChangeRequest.projectID, PMProject.customerID>))]
  [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName), Enabled = false)]
  public virtual int? CustomerID { get; set; }

  /// <summary>
  /// The date on which the changes made with the change request should be recorded in the project balances.
  /// </summary>
  /// <value>
  /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date { get; set; }

  /// <summary>
  /// The external reference number (such as an identifier required by the customer or
  /// a number from an external system integrated with Acumatica ERP) entered manually.
  /// </summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr.")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>
  /// The total of <see cref="P:PX.Objects.PM.PMChangeRequestLine.ExtCost">Ext. Cost</see> of the document lines.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost Total")]
  public virtual Decimal? CostTotal { get; set; }

  /// <summary>
  /// The total of <see cref="P:PX.Objects.PM.PMChangeRequestLine.LineAmount">Line Amount</see> of the document lines.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Total")]
  public virtual Decimal? LineTotal { get; set; }

  /// <summary>
  /// The total of <see cref="P:PX.Objects.PM.PMChangeRequestMarkup.MarkupAmount">Markup Amount</see> of the document markups.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Markup Total")]
  public virtual Decimal? MarkupTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMChangeRequest.LineTotal">Line Total</see> and <see cref="P:PX.Objects.PM.PMChangeRequest.MarkupTotal">Markup Total</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Price Total")]
  [PXFormula(typeof (Add<PMChangeRequest.lineTotal, PMChangeRequest.markupTotal>))]
  public virtual Decimal? PriceTotal { get; set; }

  /// <summary>The gross margin percent.</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin (%)")]
  public virtual Decimal? GrossMarginPct
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMChangeRequest.priceTotal), typeof (PMChangeRequest.costTotal)})] get
    {
      Decimal? priceTotal1 = this.PriceTotal;
      Decimal num1 = 0M;
      if (priceTotal1.GetValueOrDefault() == num1 & priceTotal1.HasValue)
        return new Decimal?(0M);
      Decimal num2 = (Decimal) 100;
      Decimal? priceTotal2 = this.PriceTotal;
      Decimal? nullable1 = this.CostTotal;
      Decimal? nullable2 = priceTotal2.HasValue & nullable1.HasValue ? new Decimal?(priceTotal2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3;
      if (!nullable2.HasValue)
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(num2 * nullable2.GetValueOrDefault());
      Decimal? nullable4 = nullable3;
      Decimal? priceTotal3 = this.PriceTotal;
      return !(nullable4.HasValue & priceTotal3.HasValue) ? new Decimal?() : new Decimal?(nullable4.GetValueOrDefault() / priceTotal3.GetValueOrDefault());
    }
  }

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

  /// <summary>
  /// The <see cref="!:Contact">Contact</see> responsible for the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Contact.ContactID" /> field.
  /// </value>
  [PXDefault(typeof (PX.Objects.CR.BAccount.ownerID))]
  [Owner(typeof (PMChangeRequest.workgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  /// <summary>
  /// A counter of the document lines, which is used internally to assign <see cref="P:PX.Objects.PM.PMChangeRequestLine.LineNbr">numbers</see> to newly created lines. We do not recommend
  /// that you rely on this field to determine the exact number of lines because it might not reflect this number under various conditions.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// A counter of the document markup lines, which is used internally to assign <see cref="P:PX.Objects.PM.PMChangeRequestMarkup.LineNbr">numbers</see> to newly created lines. We do not recommend
  /// that you rely on this field to determine the exact number of lines because it might not reflect this number under various conditions.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? MarkupLineCntr { get; set; }

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

  [PXString]
  [Obsolete("AC-296244-[ModernUI] UX layout verification for Change Requests (PM308500)")]
  public string FormCaptionDescription { get; set; }

  [ChangeRequestSearchable]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (FbqlSelect<SelectFromBase<PMChangeRequest, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMChangeRequest.projectID>>>>.Where<MatchUserFor<PMProject>>, PMChangeRequest>.SearchFor<PMChangeRequest.refNbr>), DescriptionField = typeof (PMChangeRequest.description))]
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

  public class Events : PXEntityEventBase<PMChangeRequest>.Container<PMChangeRequest.Events>
  {
    public PXEntityEvent<PMChangeRequest> Open;
    public PXEntityEvent<PMChangeRequest> Close;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeRequest.selected>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequest.refNbr>
  {
    public const int Length = 15;
  }

  public abstract class changeOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequest.changeOrderNbr>
  {
  }

  public abstract class costChangeOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequest.costChangeOrderNbr>
  {
  }

  public abstract class costChangeOrderReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeRequest.costChangeOrderReleased>
  {
  }

  public abstract class projectNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequest.projectNbr>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequest.description>
  {
  }

  public abstract class text : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequest.text>
  {
  }

  public abstract class descriptionAsPlainText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequest.descriptionAsPlainText>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequest.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeRequest.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeRequest.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeRequest.rejected>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequest.projectID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequest.customerID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMChangeRequest.date>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequest.extRefNbr>
  {
  }

  public abstract class costTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequest.costTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequest.lineTotal>
  {
  }

  public abstract class markupTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeRequest.markupTotal>
  {
  }

  public abstract class priceTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequest.priceTotal>
  {
  }

  public abstract class grossMarginPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeRequest.grossMarginPct>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequest.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequest.ownerID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequest.lineCntr>
  {
  }

  public abstract class markupLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequest.markupLineCntr>
  {
  }

  public abstract class delayDays : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequest.delayDays>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeRequest.released>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeRequest.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMChangeRequest.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeRequest.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequest.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeRequest.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMChangeRequest.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequest.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeRequest.lastModifiedDateTime>
  {
  }
}
