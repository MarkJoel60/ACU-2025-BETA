// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.EMailAccountMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Mail.OAuth.Extensions.Graphs;
using PX.Objects.Common;
using PX.SM;
using PX.SM.Email;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SM;

public class EMailAccountMaint_Workflow : PXGraphExtension<EMailAccountMaint>
{
  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    EMailAccountMaint_Workflow.Configure(configuration.GetScreenConfigurationContext<EMailAccountMaint, EMailAccount>());
  }

  protected static void Configure(
    WorkflowContext<EMailAccountMaint, EMailAccount> context)
  {
    BoundedTo<EMailAccountMaint, EMailAccount>.ActionCategory.IConfigured authenticationCategory = CommonActionCategories.Get<EMailAccountMaint, EMailAccount>(context).Authentication;
    context.AddScreenConfigurationFor((Func<BoundedTo<EMailAccountMaint, EMailAccount>.ScreenConfiguration.IStartConfigScreen, BoundedTo<EMailAccountMaint, EMailAccount>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<EMailAccountMaint, EMailAccount>.ScreenConfiguration.IConfigured) ((BoundedTo<EMailAccountMaint, EMailAccount>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.AddNew("EmailSummarySidePanel", (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) a.DisplayName("Summary").IsSidePanelScreen((Func<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<EMailAccountMaint, EMailAccount>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen("SM2040DB").WithIcon("pie_chart").WithAssignments((Action<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.IContainerFillerNavigationActionParameters>) (fields => fields.Add("EmailAccount", (Func<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.INeedRightOperand, BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.IConfigured>) (f => f.SetFromField<EMailAccount.emailAccountID>()))))))));
      actions.AddNew("EmailTrackingSidePanel", (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) a.DisplayName("Account Emails").IsSidePanelScreen((Func<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<EMailAccountMaint, EMailAccount>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen("SM2040SP").WithIcon("email_outline").WithAssignments((Action<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.IContainerFillerNavigationActionParameters>) (fields => fields.Add("EMailAccount_emailAccountID", (Func<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.INeedRightOperand, BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.IConfigured>) (f => f.SetFromField<EMailAccount.emailAccountID>()))))))));
      actions.AddNew("EmailLogSidePanel", (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) a.DisplayName("Email Log").IsSidePanelScreen((Func<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<EMailAccountMaint, EMailAccount>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen("SM4041SP").WithIcon("receipt").WithAssignments((Action<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.IContainerFillerNavigationActionParameters>) (fields => fields.Add("EmailLog_emailAccountID", (Func<BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.INeedRightOperand, BoundedTo<EMailAccountMaint, EMailAccount>.NavigationParameter.IConfigured>) (f => f.SetFromField<EMailAccount.emailAccountID>()))))))));
      actions.Add((Expression<Func<EMailAccountMaint, PXAction<EMailAccount>>>) (graph => graph.SendAll), (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (c => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 0).MassProcessingScreen<EmailSendReceiveMaint>()));
      actions.Add((Expression<Func<EMailAccountMaint, PXAction<EMailAccount>>>) (graph => graph.ReceiveAll), (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (c => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 0).MassProcessingScreen<EmailSendReceiveMaint>()));
      actions.Add((Expression<Func<EMailAccountMaint, PXAction<EMailAccount>>>) (graph => graph.SendReceiveAll), (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (c => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 0).MassProcessingScreen<EmailSendReceiveMaint>()));
      actions.Add<EMailAccountMaint_CheckEmailAccountGraphExt>((Expression<Func<EMailAccountMaint_CheckEmailAccountGraphExt, PXAction<EMailAccount>>>) (graph => graph.CheckEmailAccount), (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (c => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 0).WithDisplayOnToolbar((DisplayOnToolBar) 2)));
      actions.Add<EMailAccountMaint_MailingOAuthExt>((Expression<Func<EMailAccountMaint_MailingOAuthExt, PXAction<EMailAccount>>>) (graph => graph.EmailAccountSignIn), (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (c => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) c.WithCategory(authenticationCategory).WithDisplayOnToolbar((DisplayOnToolBar) 2)));
      actions.Add<EMailAccountMaint_MailingOAuthExt>((Expression<Func<EMailAccountMaint_MailingOAuthExt, PXAction<EMailAccount>>>) (graph => graph.EmailAccountSignOut), (Func<BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured>) (c => (BoundedTo<EMailAccountMaint, EMailAccount>.ActionDefinition.IConfigured) c.WithCategory(authenticationCategory)));
    })).WithCategories((Action<BoundedTo<EMailAccountMaint, EMailAccount>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(authenticationCategory)))));
  }
}
