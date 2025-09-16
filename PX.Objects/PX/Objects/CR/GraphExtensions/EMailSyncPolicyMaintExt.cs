// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.GraphExtensions.EMailSyncPolicyMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR.GraphExtensions;

public class EMailSyncPolicyMaintExt : PXGraphExtension<EMailSyncPolicyMaint>
{
  protected virtual void _(Events.RowSelected<EMailSyncPolicy> e, PXRowSelected baseHandler)
  {
    baseHandler.Invoke(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EMailSyncPolicy>>) e).Cache, ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EMailSyncPolicy>>) e).Args);
    if (!(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EMailSyncPolicy>>) e).Args.Row is EMailSyncPolicy row))
      return;
    EMailSyncAccount emailSyncAccount = PXResultset<EMailSyncAccount>.op_Implicit(PXSelectBase<EMailSyncAccount, PXViewOf<EMailSyncAccount>.BasedOn<SelectFromBase<EMailSyncAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<EMailSyncServer>.On<BqlOperand<EMailSyncAccount.serverID, IBqlInt>.IsEqual<EMailSyncServer.accountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EMailSyncAccount.contactsExportDate, IsNotNull>>>>.Or<BqlOperand<EMailSyncAccount.contactsImportDate, IBqlDateTime>.IsNotNull>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EMailSyncAccount.policyName, Equal<BqlField<EMailSyncPolicy.policyName, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<EMailSyncServer.defaultPolicyName, IBqlString>.IsEqual<BqlField<EMailSyncPolicy.policyName, IBqlString>.FromCurrent>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new EMailSyncPolicy[1]
    {
      row
    }, Array.Empty<object>()));
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsFilter>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EMailSyncPolicy>>) e).Cache, (object) row, row.ContactsSync.GetValueOrDefault() && emailSyncAccount == null);
  }
}
