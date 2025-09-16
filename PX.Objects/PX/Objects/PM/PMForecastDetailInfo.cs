// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMForecastDetailInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// The projection DAC that represents a <see cref="T:PX.Objects.PM.PMForecastDetail">project budget forecast line</see> connected with the associated <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see>.
/// </summary>
[PXHidden]
[PXBreakInheritance]
[PXProjection(typeof (Select2<PMForecastDetail, InnerJoin<PMAccountGroup, On<PMForecastDetail.accountGroupID, Equal<PMAccountGroup.groupID>>>>), new Type[] {typeof (PMForecastDetail)})]
public class PMForecastDetailInfo : PMForecastDetail
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.ProjectID" />
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXDBInt(IsKey = true)]
  public override int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.RevisionID" />
  [PXParent(typeof (Select<PMForecast, Where<PMForecast.projectID, Equal<Current<PMForecastDetailInfo.projectID>>, And<PMForecast.revisionID, Equal<Current<PMForecastDetailInfo.revisionID>>>>>))]
  [PXDBString(15, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Revision")]
  public override 
  #nullable disable
  string RevisionID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.ProjectTaskID" />
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMForecastDetailInfo.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [BaseProjectTask(typeof (PMForecastDetailInfo.projectID), IsKey = true, Enabled = false, AllowCompleted = true, AllowCanceled = true)]
  public override int? ProjectTaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.Type" />
  [PXDBString(1, BqlField = typeof (PMAccountGroup.type))]
  public virtual string AccountGroupType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.IsExpense" />
  [PXDBBool(BqlField = typeof (PMAccountGroup.isExpense))]
  public virtual bool? IsExpense { get; set; }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastDetailInfo.projectID>
  {
  }

  public new abstract class revisionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecastDetailInfo.revisionID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMForecastDetailInfo.projectTaskID>
  {
  }

  public abstract class accountGroupType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecastDetailInfo.accountGroupType>
  {
  }

  public abstract class isExpense : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMForecastDetailInfo.isExpense>
  {
  }
}
