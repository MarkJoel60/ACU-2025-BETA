// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.UnassignedEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.SM;

#nullable disable
namespace PX.Objects.EP;

public class UnassignedEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    if (package.IsProcessed || !account.IncomingProcessing.GetValueOrDefault() || !account.ProcessUnassigned.GetValueOrDefault() || !account.ResponseNotificationID.HasValue)
      return false;
    int num = account.ResponseNotificationID.Value;
    TemplateNotificationGenerator notificationGenerator = TemplateNotificationGenerator.Create((object) package.Message, new int?(num));
    notificationGenerator.LinkToEntity = false;
    notificationGenerator.MailAccountId = account.EmailAccountID;
    notificationGenerator.Send();
    return true;
  }
}
