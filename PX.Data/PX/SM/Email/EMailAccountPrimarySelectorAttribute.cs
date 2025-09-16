// Decompiled with JetBrains decompiler
// Type: PX.SM.Email.EMailAccountPrimarySelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM.Email;

public class EMailAccountPrimarySelectorAttribute : PXSelectorAttribute
{
  public EMailAccountPrimarySelectorAttribute(System.Type where)
    : base(EMailAccountPrimarySelectorAttribute.GetCommand(where), typeof (EMailAccount.address), typeof (EMailAccount.description), typeof (EMailAccount.emailAccountType), typeof (EMailAccount.replyAddress))
  {
    this.DescriptionField = typeof (EMailAccount.description);
  }

  public EMailAccountPrimarySelectorAttribute()
    : this((System.Type) null)
  {
  }

  private static System.Type GetCommand(System.Type additional)
  {
    System.Type type1 = typeof (LeftJoin<PreferencesEmail, On<PreferencesEmail.defaultEMailAccountID, Equal<EMailAccount.emailAccountID>>, LeftJoin<EMailSyncAccount, On<EMailSyncAccount.emailAccountID, Equal<EMailAccount.emailAccountID>>, LeftJoin<EMailSyncServer, On<EMailSyncServer.accountID, Equal<EMailSyncAccount.serverID>>>>>);
    System.Type type2 = typeof (Where2<PX.Data.Match<Current<AccessInfo.userName>>, And<Where<EMailAccount.emailAccountType, NotEqual<EmailAccountTypesAttribute.exchange>, Or<EMailSyncServer.isActive, Equal<PX.Data.True>>>>>);
    System.Type type3 = typeof (OrderBy<Asc<EMailAccount.description>>);
    if (additional != (System.Type) null)
      type2 = BqlCommand.Compose(typeof (Where2<,>), type2, typeof (And<>), additional);
    return BqlCommand.Compose(typeof (Search2<,,,>), typeof (EMailAccount.emailAccountID), type1, type2, type3);
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, e.NewValue) != null)
      e.Cancel = true;
    else
      base.FieldVerifying(sender, e);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.SelectorMode = PXSelectorMode.DisplayModeText;
  }
}
