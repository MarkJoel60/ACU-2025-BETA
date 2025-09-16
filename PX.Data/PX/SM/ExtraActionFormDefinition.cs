// Decompiled with JetBrains decompiler
// Type: PX.SM.ExtraActionFormDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using System;

#nullable disable
namespace PX.SM;

internal class ExtraActionFormDefinition
{
  public string ScreenId { get; set; }

  public string FormName { get; set; }

  public string DisplayName { get; set; }

  public ExtraActionFormFieldDefinition[] Fields { get; set; }

  public AUWorkflowForm ToAU()
  {
    AUWorkflowForm au = new AUWorkflowForm();
    au.Screen = this.ScreenId;
    au.FormName = this.FormName;
    au.DisplayName = this.DisplayName;
    au.IsActive = new bool?(true);
    au.IsSystem = new bool?(true);
    return au;
  }

  public static ExtraActionFormDefinition CreateFromDefinition(
    string screenId,
    string formName,
    string formDefinition)
  {
    if (string.IsNullOrEmpty(formDefinition))
      return (ExtraActionFormDefinition) null;
    ExtraActionFormDefinition fromDefinition = JsonConvert.DeserializeObject<ExtraActionFormDefinition>(formDefinition);
    fromDefinition.ScreenId = screenId;
    fromDefinition.FormName = formName;
    int num = 0;
    foreach (ExtraActionFormFieldDefinition formFieldDefinition in fromDefinition.Fields ?? Array.Empty<ExtraActionFormFieldDefinition>())
    {
      formFieldDefinition.ScreenId = screenId;
      formFieldDefinition.FormName = formName;
      formFieldDefinition.LineNumber = num++;
    }
    return fromDefinition;
  }
}
