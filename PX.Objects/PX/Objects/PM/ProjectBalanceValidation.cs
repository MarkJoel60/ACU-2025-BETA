// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectBalanceValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CT;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class ProjectBalanceValidation : PXGraph<ProjectBalanceValidation>
{
  public PXCancel<PMValidationFilter> Cancel;
  public PXFilter<PMValidationFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<PMProject, PMValidationFilter, Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>, And2<Match<PMProject, Current<AccessInfo.userName>>, And<Where<PMProject.isActive, Equal<True>, Or<PMProject.isCompleted, Equal<True>>>>>>>> Items;
  public PXSetup<PMSetup> Setup;
  [PXViewName("Project")]
  public PXSelect<PMProject> Project;
  public PXAction<PMValidationFilter> viewProject;

  public ProjectBalanceValidation()
  {
    ((PXProcessingBase<PMProject>) this.Items).SetSelected<Contract.selected>();
    ((PXProcessing<PMProject>) this.Items).SetProcessCaption("Process");
    ((PXProcessing<PMProject>) this.Items).SetProcessAllCaption("Process All");
    ((PXProcessing<PMProject>) this.Items).SetProcessTooltip("Recalculate project balances, including actual, committed, and change order buckets");
    ((PXProcessing<PMProject>) this.Items).SetProcessAllTooltip("Recalculate project balances, including actual, committed, and change order buckets");
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewProject(PXAdapter adapter)
  {
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    ((PXSelectBase<PMProject>) instance.Project).Current = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractCD, Equal<Current<PMProject.contractCD>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Project");
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMValidationFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<PMProject>) this.Items).SetProcessDelegate<ProjectBalanceValidationProcess>(new PXProcessingBase<PMProject>.ProcessItemDelegate<ProjectBalanceValidationProcess>((object) new ProjectBalanceValidation.\u003C\u003Ec__DisplayClass8_0()
    {
      filter = ((PXSelectBase<PMValidationFilter>) this.Filter).Current
    }, __methodptr(\u003C_\u003Eb__0)));
    PXUIFieldAttribute.SetVisible<PMValidationFilter.rebuildCommitments>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXSelectBase<PMSetup>) this.Setup).Current == null || ((PXSelectBase<PMSetup>) this.Setup).Current.CostCommitmentTracking.GetValueOrDefault());
  }
}
