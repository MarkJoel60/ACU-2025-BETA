// Decompiled with JetBrains decompiler
// Type: PX.Objects.BusinessProcess.Subscribers.ActionHandlers.CRMTaskAction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.BusinessProcess.Event;
using PX.BusinessProcess.Subscribers.ActionHandlers;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.Automation;
using PX.Data.BusinessProcess;
using PX.Data.PushNotifications;
using PX.Data.Wiki.Parser;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Objects.BusinessProcess.Subscribers.ActionHandlers;

internal class CRMTaskAction : TemplateMessageAction
{
  private readonly SyFormulaProcessor _formulaProcessor = new SyFormulaProcessor();

  public CRMTaskAction(
    Guid id,
    string name,
    IPushNotificationDefinitionProvider graphProvider,
    IEventDefinitionsProvider eventDefinitionProvider,
    IPXPageIndexingService pageIndexingService,
    IWorkflowFieldsService workflowFieldsService)
    : base(id, graphProvider, eventDefinitionProvider, pageIndexingService, workflowFieldsService)
  {
    ((EventActionBase) this).Id = id;
    ((EventActionBase) this).Name = name;
  }

  public virtual void Process(MatchedRow[] eventRows, CancellationToken cancellation)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRMTaskAction.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new CRMTaskAction.\u003C\u003Ec__DisplayClass2_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.eventRows = eventRows;
    TaskTemplateMaint withTaskTemplate = CRMTaskAction.CreateGraphWithTaskTemplate(new Guid?(((EventActionBase) this).Id));
    TaskTemplate current1 = ((PXSelectBase<TaskTemplate>) withTaskTemplate.TaskTemplates).Current;
    IEnumerable<TaskTemplateSetting> taskTemplateSettings = GraphHelper.RowCast<TaskTemplateSetting>((IEnumerable) ((PXSelectBase<TaskTemplateSetting>) withTaskTemplate.TaskTemplateSettings).Select(Array.Empty<object>()));
    using (new PXLocaleScope(current1.LocaleName ?? PXLocalesProvider.GetCurrentLocale()))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRMTaskAction.\u003C\u003Ec__DisplayClass2_1 cDisplayClass21 = new CRMTaskAction.\u003C\u003Ec__DisplayClass2_1();
      CRTaskMaint instance = PXGraph.CreateInstance<CRTaskMaint>();
      CRActivity crActivity1 = GraphHelper.InitNewRow<CRActivity>(((PXSelectBase) instance.Tasks).Cache, (CRActivity) null);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass21.parameters = EventActionBase.GetParameters(cDisplayClass20.eventRows);
      string str1;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      (cDisplayClass21.entityGraph, str1) = this.GetDefinitionGraph(cDisplayClass20.eventRows[0], current1.ScreenID);
      Dictionary<string, AUWorkflowFormField[]> formFields = this.GetFormFields(current1.ScreenID);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this.SetFormValues(cDisplayClass20.eventRows[0], cDisplayClass21.entityGraph, current1.ScreenID, formFields);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      crActivity1.Subject = PXTemplateContentParser.Instance.Process(current1.Summary, cDisplayClass21.parameters, cDisplayClass21.entityGraph, str1, formFields);
      SyFormulaFinalDelegate formulaFinalDelegate;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass21.entityGraph is PXGenericInqGrph)
      {
        Guid result;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (Guid.TryParse(PXTemplateContentParser.Instance.Process(current1.RefNoteID, cDisplayClass21.parameters, cDisplayClass21.entityGraph, str1, formFields), out result))
          crActivity1.RefNoteID = new Guid?(result);
        // ISSUE: method pointer
        formulaFinalDelegate = new SyFormulaFinalDelegate((object) cDisplayClass20, __methodptr(\u003CProcess\u003Eb__1));
      }
      else
      {
        if (current1.AttachActivity.GetValueOrDefault())
        {
          // ISSUE: reference to a compiler-generated field
          PXCache cache = cDisplayClass21.entityGraph.Views[str1].Cache;
          crActivity1.RefNoteID = cache.GetValue(cache.Current, "NoteID") as Guid?;
        }
        // ISSUE: method pointer
        formulaFinalDelegate = new SyFormulaFinalDelegate((object) cDisplayClass21, __methodptr(\u003CProcess\u003Eb__0));
      }
      CRActivity crActivity2 = (CRActivity) ((PXSelectBase) instance.Tasks).Cache.Insert((object) crActivity1);
      foreach (TaskTemplateSetting taskTemplateSetting in taskTemplateSettings)
      {
        if (taskTemplateSetting.Value != null)
        {
          bool? nullable = taskTemplateSetting.IsActive;
          string str2;
          string str3;
          if (nullable.GetValueOrDefault() && PXFieldNamesListAttribute.SplitNames(taskTemplateSetting.FieldName, ref str2, ref str3))
          {
            PXCache cach = ((PXGraph) instance).Caches[str2];
            if (cach != null)
            {
              PXCache pxCache = cach;
              object current2 = cach.Current;
              string str4 = str3;
              nullable = taskTemplateSetting.FromSchema;
              object obj = nullable.GetValueOrDefault() ? (object) taskTemplateSetting.Value : this._formulaProcessor.Evaluate(taskTemplateSetting.Value, formulaFinalDelegate);
              pxCache.SetValueExt(current2, str4, obj);
            }
          }
        }
      }
      if (!string.IsNullOrEmpty(current1.OwnerName))
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        string s = PXTemplateContentParser.NullableInstance.Process(StringExtensions.LastSegment(current1.OwnerName, '|'), cDisplayClass21.parameters, cDisplayClass21.entityGraph, str1, formFields);
        if (s != string.Empty)
          crActivity2.OwnerID = new int?(int.Parse(s));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      string str5 = PXTemplateContentParser.ScriptInstance.Process(current1.Body, cDisplayClass21.parameters, cDisplayClass21.entityGraph, str1, formFields).Replace("data-field=\"yes\"", " ");
      crActivity2.Body = str5;
      ((PXSelectBase<CRActivity>) instance.Tasks).Update(crActivity2);
      ((PXGraph) instance).Actions.PressSave();
    }
  }

  internal static TaskTemplateMaint CreateGraphWithTaskTemplate(Guid? noteID)
  {
    TaskTemplateMaint instance = PXGraph.CreateInstance<TaskTemplateMaint>();
    ((PXSelectBase<TaskTemplate>) instance.TaskTemplates).Current = PXResultset<TaskTemplate>.op_Implicit(((PXSelectBase<TaskTemplate>) instance.TaskTemplates).Search<TaskTemplate.noteID>((object) noteID, Array.Empty<object>()));
    return instance;
  }
}
