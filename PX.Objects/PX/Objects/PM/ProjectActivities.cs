// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectActivities
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.CT;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

public class ProjectActivities : PXGraph<
#nullable disable
ProjectActivities>
{
  public PXFilter<ProjectActivities.ActivitiesFilter> Filter;
  public PXSetup<PMProject>.Where<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ProjectActivities.ActivitiesFilter.projectID, IBqlInt>.FromCurrent>> Project;

  public class ProjectActivitiesExt_Actions : 
    ActivityDetailsExt_Inversed_Actions<
    #nullable disable
    ProjectActivities.ProjectActivitiesExt, ProjectActivities, PMProject>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  public class ProjectActivitiesExt : ActivityDetailsExt_Inversed<ProjectActivities, PMProject>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

    public override Type GetLinkConditionClause()
    {
      return ProjectEntry.ProjectEntry_ActivityDetailsExt.LinkConditionClause;
    }

    public override Type GetBAccountIDCommand()
    {
      return ProjectEntry.ProjectEntry_ActivityDetailsExt.BAccountIDCommand;
    }

    public override string GetCustomMailTo()
    {
      return ProjectEntry.ProjectEntry_ActivityDetailsExt.GetProjectMailTo((PXGraph) this.Base, ((PXSelectBase<PMProject>) this.Base.Project).Current);
    }

    public override void CreateTimeActivity(PXGraph targetGraph, int classID, string activityType)
    {
      base.CreateTimeActivity(targetGraph, classID, activityType);
      ProjectEntry.ProjectEntry_ActivityDetailsExt.CreateProjectTimeActivity((PXGraph) this.Base, targetGraph, classID);
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class ActivitiesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PX.Objects.PM.Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>), WarnIfCompleted = false)]
    public virtual int? ProjectID { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProjectActivities.ActivitiesFilter.projectID>
    {
    }
  }
}
