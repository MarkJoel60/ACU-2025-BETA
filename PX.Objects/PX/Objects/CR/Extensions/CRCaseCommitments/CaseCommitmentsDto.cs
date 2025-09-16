// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCaseCommitments.CaseCommitmentsDto
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable enable
namespace PX.Objects.CR.Extensions.CRCaseCommitments;

/// <summary>
/// The class that represents a set of objects which are required to calculate case commitments.
/// </summary>
public class CaseCommitmentsDto
{
  public CRCase Case { get; set; }

  public PX.Objects.CR.CRCaseCommitments Commitments { get; set; }

  public CRCaseClass Class { get; set; }

  public CRActivityStatistics? ActivityStatistics { get; set; }

  public CRClassSeverityTime? SeverityTime { get; set; }

  public CRPMTimeActivity? FirstUnansweredActivity { get; set; }

  public CRCase? OriginalCase { get; set; }

  public PX.Objects.CR.CRCaseCommitments? OriginalCommitments { get; set; }

  public void Deconstruct(
    out CRCase @case,
    out PX.Objects.CR.CRCaseCommitments commitments,
    out CRCaseClass @class,
    out CRActivityStatistics? activityStatistics,
    out CRClassSeverityTime? targetConfigRecord,
    out CRPMTimeActivity? firstUnansweredActivity)
  {
    @case = this.Case;
    commitments = this.Commitments;
    @class = this.Class;
    activityStatistics = this.ActivityStatistics;
    targetConfigRecord = this.SeverityTime;
    firstUnansweredActivity = this.FirstUnansweredActivity;
  }
}
