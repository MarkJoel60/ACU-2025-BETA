// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TemplateGlobalTaskListMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class TemplateGlobalTaskListMaint : PXGraph<TemplateGlobalTaskListMaint>
{
  public PXSavePerRow<PMTask> Save;
  public PXCancel<PMTask> Cancel;
  public PXSelectJoin<PMTask, InnerJoin<PMProject, On<PMProject.contractID, Equal<PMTask.projectID>>>, Where<PMProject.nonProject, Equal<True>>> Tasks;
  public PXAction<PMTask> viewTask;

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (Search<PMProject.contractID, Where<PMProject.nonProject, Equal<True>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.projectID> e)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.status> e)
  {
  }

  [Customer]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.customerID> e)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.autoIncludeInPrj> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInGL>))]
  [PXUIField(DisplayName = "GL")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInGL> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInAP>))]
  [PXUIField(DisplayName = "AP")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInAP> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInAR>))]
  [PXUIField(DisplayName = "AR")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInAR> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInCA>))]
  [PXUIField(DisplayName = "CA")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInCA> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInCR>))]
  [PXUIField(DisplayName = "CRM")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInCR> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInTA>))]
  [PXUIField(DisplayName = "Time Entries")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInTA> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInEA>))]
  [PXUIField(DisplayName = "Expenses")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInEA> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInIN>))]
  [PXUIField(DisplayName = "IN")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInIN> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInSO>))]
  [PXUIField(DisplayName = "SO")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInSO> e)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInPO>))]
  [PXUIField(DisplayName = "PO")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.visibleInPO> e)
  {
  }

  [PXUIField]
  [PXButton]
  public IEnumerable ViewTask(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTask>) this.Tasks).Current != null)
    {
      TemplateGlobalTaskMaint instance = PXGraph.CreateInstance<TemplateGlobalTaskMaint>();
      ((PXSelectBase<PMTask>) instance.Task).Current = PMTask.PK.FindDirty((PXGraph) this, ((PXSelectBase<PMTask>) this.Tasks).Current.ProjectID, ((PXSelectBase<PMTask>) this.Tasks).Current.TaskID);
      throw new PXPopupRedirectException((PXGraph) instance, "Project Task Entry - View Task", true);
    }
    return adapter.Get();
  }
}
