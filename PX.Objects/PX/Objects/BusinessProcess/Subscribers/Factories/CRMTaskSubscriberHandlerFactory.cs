// Decompiled with JetBrains decompiler
// Type: PX.Objects.BusinessProcess.Subscribers.Factories.CRMTaskSubscriberHandlerFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BusinessProcess.DAC;
using PX.BusinessProcess.Event;
using PX.BusinessProcess.Subscribers.ActionHandlers;
using PX.BusinessProcess.Subscribers.Factories;
using PX.BusinessProcess.UI;
using PX.Data;
using PX.Data.Automation;
using PX.Data.PushNotifications;
using PX.Objects.BusinessProcess.Subscribers.ActionHandlers;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.BusinessProcess.Subscribers.Factories;

internal class CRMTaskSubscriberHandlerFactory : 
  CreateSubscriberBase,
  IBPSubscriberActionHandlerFactoryWithCreateAction,
  IBPSubscriberActionHandlerFactory
{
  private readonly IPushNotificationDefinitionProvider _pushDefinitionsProvider;
  private readonly IPXPageIndexingService _pageIndexingService;
  private readonly IWorkflowFieldsService _workflowFieldsService;

  public CRMTaskSubscriberHandlerFactory(
    IPushNotificationDefinitionProvider pushDefinitionsProvider,
    IPXPageIndexingService pageIndexingService,
    IWorkflowFieldsService workflowFieldsService)
  {
    this._pushDefinitionsProvider = pushDefinitionsProvider;
    this._pageIndexingService = pageIndexingService;
    this._workflowFieldsService = workflowFieldsService;
  }

  public IEventAction CreateActionHandler(
    Guid handlerId,
    bool stopOnError,
    IEventDefinitionsProvider eventDefinitionsProvider)
  {
    TaskTemplate taskTemplate = PXResult<TaskTemplate>.op_Implicit(((IEnumerable<PXResult<TaskTemplate>>) PXSelectBase<TaskTemplate, PXSelect<TaskTemplate, Where<TaskTemplate.noteID, Equal<Required<TaskTemplate.noteID>>>>.Config>.Select(PXGraph.CreateInstance<PXGraph>(), new object[1]
    {
      (object) handlerId
    })).AsEnumerable<PXResult<TaskTemplate>>().SingleOrDefault<PXResult<TaskTemplate>>());
    return (IEventAction) new CRMTaskAction(handlerId, taskTemplate?.Name, this._pushDefinitionsProvider, eventDefinitionsProvider, this._pageIndexingService, this._workflowFieldsService);
  }

  public Tuple<PXButtonDelegate, PXEventSubscriberAttribute[]> getCreateActionDelegate(
    BusinessProcessEventMaint maintGraph)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    return Tuple.Create<PXButtonDelegate, PXEventSubscriberAttribute[]>(new PXButtonDelegate((object) new CRMTaskSubscriberHandlerFactory.\u003C\u003Ec__DisplayClass5_0()
    {
      maintGraph = maintGraph,
      \u003C\u003E4__this = this
    }, __methodptr(\u003CgetCreateActionDelegate\u003Eb__0)), new PXEventSubscriberAttribute[1]
    {
      (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        OnClosingPopup = (PXSpecialButtonType) 4
      }
    });
  }

  public IEnumerable<BPHandler> GetHandlers(PXGraph graph)
  {
    return PXSelectBase<TaskTemplate, PXSelect<TaskTemplate, Where<TaskTemplate.screenID, Equal<Current<BPEvent.screenID>>, Or<Current<BPEvent.screenID>, IsNull>>>.Config>.Select(graph, Array.Empty<object>()).FirstTableItems.Where<TaskTemplate>((Func<TaskTemplate, bool>) (t => t != null)).Select<TaskTemplate, BPHandler>((Func<TaskTemplate, BPHandler>) (t => new BPHandler()
    {
      Id = t.NoteID,
      Name = t.Name,
      Type = this.TypeName
    }));
  }

  public void RedirectToHandler(Guid? handlerId)
  {
    PXRedirectHelper.TryRedirect((PXGraph) CRMTaskAction.CreateGraphWithTaskTemplate(handlerId), (PXRedirectHelper.WindowMode) 1);
  }

  public string Type => "TASK";

  public string TypeName => "Task";

  public string CreateActionName => "NewCRMTask";

  public string CreateActionLabel => "Task";
}
