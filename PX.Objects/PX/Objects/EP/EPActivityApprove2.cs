// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPActivityApprove2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Required to duplicate EPActivityApprove cache for Bulk Time Entry popup.
/// </summary>
[PXHidden]
public class EPActivityApprove2 : EPActivityApprove
{
  [PXDBGuid(true, IsKey = true)]
  public override Guid? NoteID { get; set; }

  [EPActivityProjectDefault(typeof (PMTimeActivity.isBillable))]
  [EPTimeCardProject]
  [PXFormula(typeof (Switch<Case<Where<Not<FeatureInstalled<FeaturesSet.projectAccounting>>>, DefaultValue<EPActivityApprove2.projectID>, Case<Where<PMTimeActivity.isBillable, Equal<True>, And<Current2<EPActivityApprove2.projectID>, Equal<NonProject>>>, Null, Case<Where<PMTimeActivity.isBillable, Equal<False>, And<Current2<EPActivityApprove2.projectID>, IsNull>>, DefaultValue<EPActivityApprove2.projectID>>>>, EPActivityApprove2.projectID>))]
  [PXDefault(typeof (Search<PMProject.contractID, Where<PMProject.nonProject, Equal<True>>>))]
  public override int? ProjectID { get; set; }

  [ProjectTask(typeof (EPActivityApprove2.projectID), "TA", DisplayName = "Project Task")]
  public override int? ProjectTaskID { get; set; }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override 
  #nullable disable
  string Summary { get; set; }

  [PXDBDateAndTime(BqlField = typeof (PMTimeActivity.date), DisplayNameDate = "Date", DisplayNameTime = "Time")]
  [PXUIField(DisplayName = "Date")]
  public override DateTime? Date { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.weekID))]
  [PXUIField(DisplayName = "Time Card Week", Enabled = false)]
  [PXWeekSelector2]
  [PXFormula(typeof (Default<EPActivityApprove2.date, EPActivityApprove.trackTime>))]
  [PXDefault(typeof (EPWeeklyCrewTimeActivity.week))]
  public override int? WeekID { get; set; }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove2.workgroupID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPActivityApprove2.noteID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove2.projectID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPActivityApprove2.projectTaskID>
  {
  }

  public new abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPActivityApprove2.summary>
  {
  }

  public new abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPActivityApprove2.date>
  {
  }

  public new abstract class weekID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove2.weekID>
  {
  }
}
