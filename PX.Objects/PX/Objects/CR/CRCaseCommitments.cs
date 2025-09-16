// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseCommitments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Represents a commitment for a <see cref="T:PX.Objects.CR.CRCase">case</see>.
/// </summary>
/// <remarks>
/// The class is used only if the <see cref="P:PX.Objects.CS.FeaturesSet.CaseCommitmentsTracking">Case Commitments</see> feature is turned on.
/// </remarks>
[PXCacheName("Case Commitments")]
public class CRCaseCommitments : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The user-friendly unique identifier of the <see cref="T:PX.Objects.CR.CRCase">case</see> that is associated with this case commitments.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (CRCase.caseCD))]
  [PXUIField]
  [PXParent(typeof (CRCaseCommitments.FK.Case))]
  public virtual 
  #nullable disable
  string CaseCD { get; set; }

  /// <summary>
  /// The original value of the <see cref="P:PX.Objects.CR.CRCaseCommitments.ResponseDueDateTime" /> field. The value will remain unchanged if the value of the related <see cref="P:PX.Objects.CR.CRCaseCommitments.ResponseDueDateTime" /> field assigned to <see langword="null" />.
  /// </summary>
  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXFormula(typeof (BqlOperand<CRCaseCommitments.initialResponseDueDateTime, IBqlDateTime>.IfNullThen<CRCaseCommitments.originalInitialResponseDueDateTime>))]
  [PXUIField(DisplayName = "Initial Response Due", Enabled = false)]
  public virtual DateTime? OriginalInitialResponseDueDateTime { get; set; }

  /// <summary>
  /// The due date and time of the first outgoing activity of the case.
  /// </summary>
  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXUIField(DisplayName = "Initial Response Due", Enabled = false)]
  public virtual DateTime? InitialResponseDueDateTime { get; set; }

  /// <summary>
  /// Dummy field to show <see cref="P:PX.Objects.CR.CRCaseCommitments.InitialResponseDueDateTime" /> in the <see cref="T:PX.Objects.CR.CRCase">case</see> summary.
  /// </summary>
  [PXDate(DisplayMask = "g", InputMask = "g")]
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (CRCaseCommitments.initialResponseDueDateTime)})]
  public virtual DateTime? HeaderInitialResponseDueDateTime => this.InitialResponseDueDateTime;

  /// <summary>
  /// The original value of the <see cref="P:PX.Objects.CR.CRCaseCommitments.ResolutionDueDateTime" /> field. The value will remain unchanged if the value of the related <see cref="P:PX.Objects.CR.CRCaseCommitments.ResolutionDueDateTime" /> field assigned to <see langword="null" />.
  /// </summary>
  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXFormula(typeof (BqlOperand<CRCaseCommitments.resolutionDueDateTime, IBqlDateTime>.IfNullThen<CRCaseCommitments.originalResolutionDueDateTime>))]
  [PXUIField(DisplayName = "Resolution Due", Enabled = false)]
  public virtual DateTime? OriginalResolutionDueDateTime { get; set; }

  /// <summary>The due date and time of the resolution for the case.</summary>
  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXUIField(DisplayName = "Resolution Due", Enabled = false)]
  public virtual DateTime? ResolutionDueDateTime { get; set; }

  /// <summary>
  /// The dummy field that is used to display the <see cref="P:PX.Objects.CR.CRCaseCommitments.ResolutionDueDateTime" /> value in the <see cref="T:PX.Objects.CR.CRCase">case</see> summary.
  /// </summary>
  [PXDate(DisplayMask = "g", InputMask = "g")]
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (CRCaseCommitments.resolutionDueDateTime)})]
  public virtual DateTime? HeaderResolutionDueDateTime => this.ResolutionDueDateTime;

  /// <summary>
  /// The due date and time to reply after the first unanswered incoming activity of the case.
  /// </summary>
  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXUIField(DisplayName = "Response Due", Enabled = false)]
  public virtual DateTime? ResponseDueDateTime { get; set; }

  /// <summary>
  /// The dummy field that is used to display the <see cref="P:PX.Objects.CR.CRCaseCommitments.ResponseDueDateTime" /> value in the <see cref="T:PX.Objects.CR.CRCase">case</see> summary.
  /// </summary>
  [PXDate(DisplayMask = "g", InputMask = "g")]
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (CRCaseCommitments.responseDueDateTime)})]
  public virtual DateTime? HeaderResponseDueDateTime => this.ResponseDueDateTime;

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<CRCaseCommitments>.By<CRCaseCommitments.caseCD>
  {
    public static CRCaseCommitments Find(PXGraph graph, string caseCD, PKFindOptions options = 0)
    {
      return (CRCaseCommitments) PrimaryKeyOf<CRCaseCommitments>.By<CRCaseCommitments.caseCD>.FindBy(graph, (object) caseCD, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Case class</summary>
    public class Case : 
      PrimaryKeyOf<CRCase>.By<CRCase.caseCD>.ForeignKeyOf<CRCaseCommitments>.By<CRCaseCommitments.caseCD>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCaseCommitments.CaseCD" />
  public abstract class caseCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCaseCommitments.caseCD>
  {
  }

  public abstract class originalInitialResponseDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseCommitments.originalInitialResponseDueDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCaseCommitments.InitialResponseDueDateTime" />
  public abstract class initialResponseDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseCommitments.initialResponseDueDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCaseCommitments.HeaderInitialResponseDueDateTime" />
  public abstract class headerInitialResponseDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseCommitments.headerInitialResponseDueDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCaseCommitments.OriginalResolutionDueDateTime" />
  public abstract class originalResolutionDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseCommitments.originalResolutionDueDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCaseCommitments.ResolutionDueDateTime" />
  public abstract class resolutionDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseCommitments.resolutionDueDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCaseCommitments.HeaderResolutionDueDateTime" />
  public abstract class headerResolutionDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseCommitments.headerResolutionDueDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCaseCommitments.ResponseDueDateTime" />
  public abstract class responseDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseCommitments.responseDueDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRCaseCommitments.HeaderResponseDueDateTime" />
  public abstract class headerResponseDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseCommitments.headerResponseDueDateTime>
  {
  }
}
