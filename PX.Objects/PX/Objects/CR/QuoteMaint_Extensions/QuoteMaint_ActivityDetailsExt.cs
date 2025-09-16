// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.QuoteMaint_Extensions.QuoteMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions;
using System;

#nullable disable
namespace PX.Objects.CR.QuoteMaint_Extensions;

public class QuoteMaint_ActivityDetailsExt : ActivityDetailsExt<QuoteMaint, CRQuote, CRQuote.noteID>
{
  public override System.Type GetBAccountIDCommand() => typeof (CRQuote.bAccountID);

  public override System.Type GetContactIDCommand() => typeof (CRQuote.contactID);

  public override System.Type GetEmailMessageTarget()
  {
    return typeof (Select<CRContact, Where<CRContact.contactID, Equal<Current<CRQuote.opportunityContactID>>>>);
  }

  public override string GetCustomMailTo()
  {
    CRQuote current = ((PXSelectBase<CRQuote>) this.Base.Quote).Current;
    if (current == null)
      return (string) null;
    CRContact crContact = current.OpportunityContactID.With<int?, CRContact>((Func<int?, CRContact>) (_ => CRContact.PK.Find((PXGraph) this.Base, new int?(_.Value))));
    return string.IsNullOrWhiteSpace(crContact?.Email) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(crContact.Email, crContact.DisplayName);
  }

  protected virtual void _(Events.RowPersisted<CRQuote> e)
  {
    if (e.Row == null || e.TranStatus != null || e.Operation != 3)
      return;
    Guid? noteId = e.Row.NoteID;
    if (!noteId.HasValue)
      return;
    Guid valueOrDefault = noteId.GetValueOrDefault();
    foreach (PXResult<CRActivity> pxResult in PXSelectBase<CRActivity, PXViewOf<CRActivity>.BasedOn<SelectFromBase<CRActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRActivity.refNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) valueOrDefault
    }))
    {
      CRActivity crActivity = PXResult<CRActivity>.op_Implicit(pxResult);
      crActivity.RefNoteIDType = (string) null;
      crActivity.RefNoteID = new Guid?();
      ((PXGraph) this.Base).Caches[typeof (CRActivity)].Update((object) crActivity);
    }
    ((PXGraph) this.Base).Caches[typeof (CRActivity)].Persist((PXDBOperation) 1);
  }
}
