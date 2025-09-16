// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BusinessAccountMaint_Extensions.BusinessAccountMaint_CRRelationDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.BusinessAccountMaint_Extensions;

/// <inheritdoc />
public class BusinessAccountMaint_CRRelationDetailsExt : 
  CRRelationDetailsExt<BusinessAccountMaint, BAccount, BAccount.noteID>
{
  [PXDBChildIdentity(typeof (BAccount.bAccountID))]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRRelation.entityID> e)
  {
  }

  protected virtual void _(Events.RowSelected<BAccount> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase) this.Relations).Cache.AllowInsert = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<BAccount>>) e).Cache.GetStatus((object) e.Row) != 2;
  }
}
