// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.CampaignMaintWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;

#nullable disable
namespace PX.Objects.CR.Workflows;

public class CampaignMaintWorkflow : PXGraphExtension<CampaignMaint>
{
  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    CampaignMaintWorkflow.Configure(configuration.GetScreenConfigurationContext<CampaignMaint, CRCampaign>());
  }

  protected static void Configure(WorkflowContext<CampaignMaint, CRCampaign> context)
  {
    context.AddScreenConfigurationFor((Func<BoundedTo<CampaignMaint, CRCampaign>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CampaignMaint, CRCampaign>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CampaignMaint, CRCampaign>.ScreenConfiguration.IConfigured) ((BoundedTo<CampaignMaint, CRCampaign>.ScreenConfiguration.IAllowOptionalConfig) screen).WithFieldStates((Action<BoundedTo<CampaignMaint, CRCampaign>.DynamicFieldState.IContainerFillerFields>) (fields => fields.Add<CRCampaign.status>((Func<BoundedTo<CampaignMaint, CRCampaign>.DynamicFieldState.INeedAnyConfigField, BoundedTo<CampaignMaint, CRCampaign>.DynamicFieldState.IConfigured>) (field => (BoundedTo<CampaignMaint, CRCampaign>.DynamicFieldState.IConfigured) ((BoundedTo<CampaignMaint, CRCampaign>.DynamicFieldState.INeedAnyConfigField) field.SetComboValues(new (string, string)[4]
    {
      ("A", "Canceled"),
      ("O", "Completed"),
      ("P", "Planning"),
      ("X", "Execution")
    })).WithDefaultValue((object) "P")))))));
  }

  public static class States
  {
    public const string Canceled = "A";
    public const string Completed = "O";
    public const string Planning = "P";
    public const string Execution = "X";
  }

  public static class StateNames
  {
    public const string Canceled = "Canceled";
    public const string Completed = "Completed";
    public const string Planning = "Planning";
    public const string Execution = "Execution";
  }
}
