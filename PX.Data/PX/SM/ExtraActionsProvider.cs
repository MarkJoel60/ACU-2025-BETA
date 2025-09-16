// Decompiled with JetBrains decompiler
// Type: PX.SM.ExtraActionsProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.SM;

internal static class ExtraActionsProvider
{
  public static IEnumerable<AUScreenActionState> SelectActions()
  {
    ExtraActionsProvider.ScreenExtraActionsSlot extraActionsSlot = ExtraActionsProvider.ScreenExtraActionsSlot.GetOrCreate();
    return extraActionsSlot == null ? (IEnumerable<AUScreenActionState>) Array.Empty<AUScreenActionState>() : extraActionsSlot.IndexByScreenExtraActions.Select<AUScreenExtraAction, AUScreenActionState>((Func<AUScreenExtraAction, AUScreenActionState>) (item => item.ToAU()));
  }

  public static IEnumerable<AUWorkflowForm> SelectActionForms()
  {
    ExtraActionsProvider.ScreenExtraActionsSlot extraActionsSlot = ExtraActionsProvider.ScreenExtraActionsSlot.GetOrCreate();
    return extraActionsSlot == null ? (IEnumerable<AUWorkflowForm>) Array.Empty<AUWorkflowForm>() : extraActionsSlot.IndexByScreenExtraForms.Select<ExtraActionFormDefinition, AUWorkflowForm>((Func<ExtraActionFormDefinition, AUWorkflowForm>) (item => item.ToAU()));
  }

  public static IEnumerable<AUWorkflowFormField> SelectActionFormFields()
  {
    ExtraActionsProvider.ScreenExtraActionsSlot extraActionsSlot = ExtraActionsProvider.ScreenExtraActionsSlot.GetOrCreate();
    return extraActionsSlot == null ? (IEnumerable<AUWorkflowFormField>) Array.Empty<AUWorkflowFormField>() : extraActionsSlot.IndexByScreenExtraFormFields.Select<ExtraActionFormFieldDefinition, AUWorkflowFormField>((Func<ExtraActionFormFieldDefinition, AUWorkflowFormField>) (item => item.ToAU()));
  }

  private sealed class ScreenExtraActionsSlot : IPrefetchable, IPXCompanyDependent
  {
    private static string SlotKey = "ExtraActions";
    public IEnumerable<AUScreenExtraAction> IndexByScreenExtraActions;
    public IEnumerable<ExtraActionFormDefinition> IndexByScreenExtraForms;
    public IEnumerable<ExtraActionFormFieldDefinition> IndexByScreenExtraFormFields;

    void IPrefetchable.Prefetch()
    {
      this.IndexByScreenExtraActions = (IEnumerable<AUScreenExtraAction>) PXDatabase.Select<AUScreenExtraAction>();
      this.IndexByScreenExtraForms = ((IEnumerable<AUScreenExtraAction>) PXDatabase.Select<AUScreenExtraAction>().Where<AUScreenExtraAction>((Expression<Func<AUScreenExtraAction, bool>>) (extraAction => extraAction.BeforeRunForm != default (string) || extraAction.AfterRunForm != default (string))).ToArray<AUScreenExtraAction>()).SelectMany<AUScreenExtraAction, ExtraActionFormDefinition>((Func<AUScreenExtraAction, IEnumerable<ExtraActionFormDefinition>>) (item =>
      {
        List<ExtraActionFormDefinition> actionFormDefinitionList = new List<ExtraActionFormDefinition>();
        if (!Str.IsNullOrEmpty(item.BeforeRunForm))
          actionFormDefinitionList.Add(ExtraActionFormDefinition.CreateFromDefinition(item.ScreenID, item.GetBeforeRunFormName(), item.BeforeRunForm));
        if (!Str.IsNullOrEmpty(item.AfterRunForm))
          actionFormDefinitionList.Add(ExtraActionFormDefinition.CreateFromDefinition(item.ScreenID, item.GetAfterRunFormName(), item.AfterRunForm));
        return (IEnumerable<ExtraActionFormDefinition>) actionFormDefinitionList;
      }));
      this.IndexByScreenExtraFormFields = ((IEnumerable<AUScreenExtraAction>) PXDatabase.Select<AUScreenExtraAction>().Where<AUScreenExtraAction>((Expression<Func<AUScreenExtraAction, bool>>) (extraAction => extraAction.BeforeRunForm != default (string) || extraAction.AfterRunForm != default (string))).ToArray<AUScreenExtraAction>()).Select(item => new
      {
        BeforeForm = !Str.IsNullOrEmpty(item.BeforeRunForm) ? ExtraActionFormDefinition.CreateFromDefinition(item.ScreenID, item.GetBeforeRunFormName(), item.BeforeRunForm) : (ExtraActionFormDefinition) null,
        AfterForm = !Str.IsNullOrEmpty(item.AfterRunForm) ? ExtraActionFormDefinition.CreateFromDefinition(item.ScreenID, item.GetAfterRunFormName(), item.AfterRunForm) : (ExtraActionFormDefinition) null
      }).Where(formData =>
      {
        ExtraActionFormDefinition beforeForm = formData.BeforeForm;
        if ((beforeForm != null ? (((IEnumerable<ExtraActionFormFieldDefinition>) beforeForm.Fields).Any<ExtraActionFormFieldDefinition>() ? 1 : 0) : 0) != 0)
          return true;
        ExtraActionFormDefinition afterForm = formData.AfterForm;
        return afterForm != null && ((IEnumerable<ExtraActionFormFieldDefinition>) afterForm.Fields).Any<ExtraActionFormFieldDefinition>();
      }).SelectMany(item => ((IEnumerable<ExtraActionFormFieldDefinition>) (item.BeforeForm?.Fields ?? Array.Empty<ExtraActionFormFieldDefinition>())).Union<ExtraActionFormFieldDefinition>((IEnumerable<ExtraActionFormFieldDefinition>) (item.AfterForm?.Fields ?? Array.Empty<ExtraActionFormFieldDefinition>())));
    }

    public static ExtraActionsProvider.ScreenExtraActionsSlot GetOrCreate()
    {
      return PXContext.GetSlot<ExtraActionsProvider.ScreenExtraActionsSlot>(ExtraActionsProvider.ScreenExtraActionsSlot.SlotKey) ?? PXContext.SetSlot<ExtraActionsProvider.ScreenExtraActionsSlot>(ExtraActionsProvider.ScreenExtraActionsSlot.SlotKey, ExtraActionsProvider.ScreenExtraActionsSlot.GetDatabaseSlot());
    }

    private static ExtraActionsProvider.ScreenExtraActionsSlot GetDatabaseSlot()
    {
      return PXDatabase.GetSlot<ExtraActionsProvider.ScreenExtraActionsSlot>(ExtraActionsProvider.ScreenExtraActionsSlot.SlotKey, typeof (AUScreenExtraAction));
    }
  }
}
