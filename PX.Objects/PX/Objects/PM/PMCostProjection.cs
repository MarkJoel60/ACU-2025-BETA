// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCostProjection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.ProjectAccounting;
using PX.Objects.CT;
using PX.Objects.IN;
using PX.TM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Contains the main properties of a cost projection revision.
/// Cost projections are used for tracking the project costs during project completion
/// in comparison to the initially estimated costs.
/// The records of this type are created and edited through the Cost Projection (PM305000) form
/// (which corresponds to the <see cref="T:PX.Objects.CN.ProjectAccounting.CostProjectionEntry" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Cost Projection")]
[PXPrimaryGraph(typeof (CostProjectionEntry))]
[Serializable]
public class PMCostProjection : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
{
  protected bool? _Released;
  protected Guid? _NoteID;

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> for which the cost projection revision is created.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>), IsKey = true, WarnIfCompleted = false)]
  [PXDefault]
  public virtual int? ProjectID { get; set; }

  /// <summary>The revision identifier of the cost projection.</summary>
  [PXSelector(typeof (Search<PMCostProjection.revisionID, Where<PMCostProjection.projectID, Equal<Current<PMCostProjection.projectID>>>, OrderBy<Desc<PMCostProjection.revisionID>>>), DescriptionField = typeof (PMCostProjection.description))]
  [PXDBString(30, IsKey = true, InputMask = ">aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string RevisionID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMCostProjectionClass">cost projection class</see> of the cost projection revision.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostProjectionClass.ClassID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMCostProjection.classID>.IsRelatedTo<PMCostProjectionClass.classID>))]
  [PXDBString(30, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PMCostProjectionClass.classID>), DescriptionField = typeof (PMCostProjectionClass.description))]
  [PXRestrictor(typeof (Where<PMCostProjectionClass.isActive, Equal<True>>), "Class is Inactive", new System.Type[] {})]
  public virtual string ClassID { get; set; }

  /// <summary>The status of the cost projection revision.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.CN.ProjectAccounting.CostProjectionStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [CostProjectionStatus.List]
  [PXDefault("H")]
  [PXUIField]
  public virtual string Status { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the cost projection revision is on hold.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Hold")]
  [PXDefault(true)]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the cost projection revision is approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the cost projection revision is rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? Rejected { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the cost projection revision is released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Released")]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>The description of the cost projection revision.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>
  /// The date when the cost projection revision was created.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date { get; set; }

  /// <summary>
  /// The number of lines in the cost projection revision which is used to set the default value of the <see cref="P:PX.Objects.PM.PMCostProjectionLine.LineNbr" /> field.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.BudgetedQuantity" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Budgeted Quantity", Enabled = false)]
  public virtual Decimal? TotalBudgetedQuantity { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.BudgetedAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted Cost at Completion", Enabled = false)]
  public virtual Decimal? TotalBudgetedAmount { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.ActualQuantity" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Actual Quantity", Enabled = false)]
  public virtual Decimal? TotalActualQuantity { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.ActualAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Actual Amount", Enabled = false)]
  public virtual Decimal? TotalActualAmount { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.UnbilledQuantity" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Unbilled Quantity", Enabled = false)]
  public virtual Decimal? TotalUnbilledQuantity { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.UnbilledAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Unbilled Amount", Enabled = false)]
  public virtual Decimal? TotalUnbilledAmount { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.Quantity" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Projected Quantity to Complete")]
  public virtual Decimal? TotalQuantity { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.Amount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost to Complete")]
  public virtual Decimal? TotalAmount { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.ProjectedQuantity" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Projected Quantity at Completion")]
  public virtual Decimal? TotalProjectedQuantity { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.ProjectedAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost at Completion")]
  public virtual Decimal? TotalProjectedAmount { get; set; }

  /// <summary>The workgroup that is responsible for the document.</summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PMProject.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.EP.EPEmployee">Employee</see> responsible for the document.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDefault(typeof (PMProject.ownerID))]
  [Owner(typeof (PMCostProjection.workgroupID))]
  public virtual int? OwnerID { get; set; }

  /// <summary>The description of the corresponding project.</summary>
  [PXString]
  [PXFormula(typeof (Selector<PMCostProjection.projectID, PMProject.description>))]
  public string FormCaptionDescription { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.QuantityToComplete" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity to Complete", Enabled = false)]
  public virtual Decimal? TotalQuantityToComplete { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.AmountToComplete" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted Cost to Complete", Enabled = false)]
  public virtual Decimal? TotalAmountToComplete { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionLine.VarianceAmount" />.
  /// </summary>
  [PXFormula(typeof (Sub<PMCostProjection.totalProjectedAmount, PMCostProjection.totalBudgetedAmount>))]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost Variance", Enabled = false)]
  public virtual Decimal? TotalVarianceAmount { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : 
    PrimaryKeyOf<PMCostProjection>.By<PMCostProjection.projectID, PMCostProjection.revisionID>
  {
    public static PMCostProjection Find(
      PXGraph graph,
      int? projectID,
      string revisionID,
      PKFindOptions options = 0)
    {
      return (PMCostProjection) PrimaryKeyOf<PMCostProjection>.By<PMCostProjection.projectID, PMCostProjection.revisionID>.FindBy(graph, (object) projectID, (object) revisionID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Class</summary>
    public class CostProjectionClass : 
      PrimaryKeyOf<PMCostProjectionClass>.By<PMCostProjectionClass.classID>.ForeignKeyOf<PMCostProjection>.By<PMCostProjection.classID>
    {
    }

    /// <summary>Owner</summary>
    public class OwnerContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<PMCostProjection>.By<PMCostProjection.ownerID>
    {
    }

    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMRevenueBudget>.By<PMCostProjection.projectID>
    {
    }
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjection.projectID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjection.revisionID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjection.classID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjection.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjection.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjection.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjection.rejected>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjection.released>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjection.description>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMCostProjection.date>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjection.lineCntr>
  {
  }

  public abstract class totalBudgetedQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalBudgetedQuantity>
  {
  }

  public abstract class totalBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalBudgetedAmount>
  {
  }

  public abstract class totalActualQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalActualQuantity>
  {
  }

  public abstract class totalActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalActualAmount>
  {
  }

  public abstract class totalUnbilledQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalUnbilledQuantity>
  {
  }

  public abstract class totalUnbilledAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalUnbilledAmount>
  {
  }

  public abstract class totalQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalQuantity>
  {
  }

  public abstract class totalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalAmount>
  {
  }

  public abstract class totalProjectedQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalProjectedQuantity>
  {
  }

  public abstract class totalProjectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalProjectedAmount>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjection.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjection.ownerID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostProjection.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMCostProjection.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostProjection.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjection.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjection.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCostProjection.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjection.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjection.lastModifiedDateTime>
  {
  }

  public abstract class totalQuantityToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalQuantityToComplete>
  {
  }

  public abstract class totalAmountToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalAmountToComplete>
  {
  }

  public abstract class totalVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjection.totalVarianceAmount>
  {
  }
}
