// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExchangeEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.SM;

#nullable disable
namespace PX.Objects.EP;

public class ExchangeEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    CRSMEmail message = package.Message;
    EMailAccount account = package.Account;
    PXGraph graph = package.Graph;
    if (account.EmailAccountType != "E")
      return false;
    foreach (PXResult<EPEmployee> pxResult in PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, InnerJoin<EMailSyncAccount, On<EPEmployee.bAccountID, Equal<EMailSyncAccount.employeeID>>>, Where<EMailSyncAccount.emailAccountID, Equal<Required<EMailSyncAccount.emailAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) account.EmailAccountID
    }))
    {
      EPEmployee epEmployee = PXResult<EPEmployee>.op_Implicit(pxResult);
      if (epEmployee.UserID.HasValue)
      {
        message.OwnerID = epEmployee.DefContactID;
        return true;
      }
    }
    return false;
  }
}
