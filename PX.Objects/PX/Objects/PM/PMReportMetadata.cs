// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMReportMetadata
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// This is a virtual DAC. The fields/selector of this dac is used in reports.
/// </summary>
[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMReportMetadata : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectID;
  protected int? _ProjectTaskID;

  [PXDefault]
  [Project(WarnIfCompleted = false)]
  [PXRestrictor(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, NotEqual<True>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new Type[] {typeof (PMProject.contractCD)})]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault]
  [Project(false, WarnIfCompleted = false)]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlOperand<PMProject.nonProject, IBqlBool>.IsNotEqual<True>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new Type[] {typeof (PMProject.contractCD)})]
  public virtual int? AllProjectID { get; set; }

  [PXDefault]
  [Project(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.status, Equal<ProjectStatus.active>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.status, Equal<ProjectStatus.suspended>>>>>.Or<BqlOperand<PMProject.status, IBqlString>.IsEqual<ProjectStatus.completed>>>>>>), WarnIfCompleted = false)]
  [PXRestrictor(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, NotEqual<True>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new Type[] {typeof (PMProject.contractCD)})]
  public virtual int? ActiveProjectID { get; set; }

  [PXDefault]
  [Project(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.status, Equal<ProjectStatus.active>>>>>.Or<BqlOperand<PMProject.status, IBqlString>.IsEqual<ProjectStatus.completed>>>>>), WarnIfCompleted = false)]
  [PXRestrictor(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, NotEqual<True>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new Type[] {typeof (PMProject.contractCD)})]
  public virtual int? ActiveOrCompletedProjectID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the progress worksheet report.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.planned>, And<PMProject.nonProject, Equal<False>>>), "The {0} project is not active.", new Type[] {typeof (PMProject.contractCD)})]
  [PXDefault]
  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.status, NotEqual<ProjectStatus.planned>, And<PMProject.nonProject, Equal<False>>>>))]
  public virtual int? ProjectIDForProgressWorksheetReport { get; set; }

  [ActiveProject(IsDBField = false)]
  public virtual int? FeaturedProjectID { get; set; }

  [BaseProjectTask(typeof (PMReportMetadata.projectID))]
  public virtual int? ProjectTaskID
  {
    get => this._ProjectTaskID;
    set => this._ProjectTaskID = value;
  }

  /// <summary>
  /// Field schema (in PXSelector) for Task field in reports (e.g. Project Budget Forecast by Month).
  /// Shows only tasks for project with ID <see cref="T:PX.Objects.PM.PMReportMetadata.projectID">projectID</see>.
  /// Row-level security is taken into account, i.e. tasks from inaccessible projects are not shown.
  /// </summary>
  [PXInt]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMTask.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.projectID, Equal<BqlField<PMReportMetadata.projectID, IBqlInt>.AsOptional>>>>>.And<MatchUserFor<PMProject>>>, PMTask>.SearchFor<PMTask.taskID>), new Type[] {typeof (PMTask.taskCD), typeof (PMTask.description), typeof (PMTask.status)}, SubstituteKey = typeof (PMTask.taskCD), DescriptionField = typeof (PMTask.description))]
  public virtual int? ValidTaskID { get; set; }

  [PXString]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.projectID, Equal<BqlField<PMReportMetadata.projectID, IBqlInt>.AsOptional>>>>, And<BqlOperand<PMProforma.corrected, IBqlBool>.IsNotEqual<True>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.status, Equal<ProformaStatus.open>>>>>.Or<BqlOperand<PMProforma.status, IBqlString>.IsEqual<ProformaStatus.closed>>>>>, PMProforma>.SearchFor<PMProforma.refNbr>), DescriptionField = typeof (PMProforma.description))]
  public virtual 
  #nullable disable
  string OpenOrClosedProforma { get; set; }

  [PXString]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.projectID, Equal<BqlField<PMReportMetadata.projectID, IBqlInt>.AsOptional>>>>>.And<BqlOperand<PMProforma.corrected, IBqlBool>.IsNotEqual<True>>>, PMProforma>.SearchFor<PMProforma.refNbr>), DescriptionField = typeof (PMProforma.description))]
  public virtual string AnyProforma { get; set; }

  [PXString]
  [PX.Objects.PM.WipDetailLevel.StringList]
  [PXDefault(typeof (BqlOperand<PX.Objects.PM.WipDetailLevel.taskAndCostCode, IBqlString>.When<Where<FeatureInstalled<FeaturesSet.costCodes>>>.Else<PX.Objects.PM.WipDetailLevel.projectTaskOnly>))]
  public virtual string WipDetailLevel { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMReportMetadata.projectID>
  {
  }

  public abstract class allProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMReportMetadata.allProjectID>
  {
  }

  public abstract class activeProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMReportMetadata.activeProjectID>
  {
  }

  public abstract class activeOrCompletedProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMReportMetadata.activeOrCompletedProjectID>
  {
  }

  public abstract class projectIDForProgressWorksheetReport : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMReportMetadata.projectIDForProgressWorksheetReport>
  {
  }

  public abstract class featuredProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMReportMetadata.featuredProjectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMReportMetadata.projectTaskID>
  {
  }

  public abstract class validTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMReportMetadata.validTaskID>
  {
  }

  public abstract class openOrClosedProforma : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMReportMetadata.openOrClosedProforma>
  {
  }

  public abstract class anyProforma : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMReportMetadata.anyProforma>
  {
  }

  public abstract class wipDetailLevel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMReportMetadata.wipDetailLevel>
  {
  }
}
