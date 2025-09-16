// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.AssignmentEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.SM;

#nullable disable
namespace PX.Objects.EP;

public class AssignmentEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    CRSMEmail message = package.Message;
    EMailAccount account = package.Account;
    if (!message.IsIncome.GetValueOrDefault() || message.OwnerID.HasValue || message.WorkgroupID.HasValue || message.ClassID.GetValueOrDefault() == -2)
      return false;
    bool flag = false;
    if (account.DefaultEmailAssignmentMapID.HasValue)
      flag = PXGraph.CreateInstance<EPAssignmentProcessor<CRSMEmail>>().Assign(message, account.DefaultEmailAssignmentMapID);
    if (!flag)
    {
      package.Graph.Caches[typeof (CRSMEmail)].SetValue<CRSMEmail.ownerID>((object) message, (object) account.DefaultOwnerID);
      package.Graph.Caches[typeof (CRSMEmail)].SetValue<CRSMEmail.workgroupID>((object) message, (object) account.DefaultWorkgroupID);
    }
    return true;
  }
}
