// Decompiled with JetBrains decompiler
// Type: PX.SM.ToReinitializeAccountAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

[Serializable]
public class ToReinitializeAccountAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
{
  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EMailSyncPolicy row))
      return;
    foreach (PXResult<EMailSyncAccount> pxResult in PXSelectBase<EMailSyncAccount, PXSelect<EMailSyncAccount, Where<EMailSyncAccount.policyName, Equal<Required<EMailSyncPolicy.policyName>>>>.Config>.Select(sender.Graph, (object) row.PolicyName))
    {
      EMailSyncAccount emailSyncAccount = (EMailSyncAccount) pxResult;
      emailSyncAccount.ToReinitialize = new bool?(true);
      sender.Graph.Caches[typeof (EMailSyncAccount)].Update((object) emailSyncAccount);
    }
    foreach (PXResult<EMailSyncServer, EMailSyncAccount> pxResult in PXSelectBase<EMailSyncServer, PXSelectJoin<EMailSyncServer, LeftJoin<EMailSyncAccount, On<EMailSyncAccount.serverID, Equal<EMailSyncServer.accountID>>>, Where<EMailSyncServer.defaultPolicyName, Equal<Required<EMailSyncPolicy.policyName>>>>.Config>.Select(sender.Graph, (object) row.PolicyName))
    {
      EMailSyncAccount emailSyncAccount = (EMailSyncAccount) pxResult;
      emailSyncAccount.ToReinitialize = new bool?(true);
      sender.Graph.Caches[typeof (EMailSyncAccount)].Update((object) emailSyncAccount);
    }
  }
}
