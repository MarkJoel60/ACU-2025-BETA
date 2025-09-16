// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityMaint_Extensions.OpportunityMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.OpportunityMaint_Extensions;

public class OpportunityMaint_ActivityDetailsExt : 
  ActivityDetailsExt<OpportunityMaint, CROpportunity, CROpportunity.noteID>
{
  public override System.Type GetLinkConditionClause()
  {
    return typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.refNoteID, In<P.AsGuid>>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CROpportunityClass.showContactActivities>, Equal<True>>>>>.And<BqlOperand<CRPMTimeActivity.refNoteID, IBqlGuid>.IsEqual<BqlField<CROpportunity.leadID, IBqlGuid>.FromCurrent>>>>>);
  }

  public virtual IEnumerable activities()
  {
    OpportunityMaint opportunityMaint = this.Base;
    if ((opportunityMaint != null ? (!((Guid?) ((PXSelectBase<CROpportunity>) opportunityMaint.OpportunityCurrent)?.Current?.NoteID).HasValue ? 1 : 0) : 1) != 0)
      return (IEnumerable) Enumerable.Empty<CRPMTimeActivity>();
    List<object> objectList = new List<object>()
    {
      (object) ((PXSelectBase<CROpportunity>) this.Base.OpportunityCurrent).Current.NoteID
    };
    foreach (PXResult<CRQuote> pxResult in ((PXSelectBase<CRQuote>) this.Base.Quotes).Select(Array.Empty<object>()))
    {
      CRQuote crQuote = PXResult<CRQuote>.op_Implicit(pxResult);
      objectList.Add((object) crQuote.QuoteID);
    }
    return GraphHelper.QuickSelect(((PXSelectBase) this.Activities).View, new object[1]
    {
      (object) objectList.ToArray()
    });
  }

  public override System.Type GetBAccountIDCommand() => typeof (CROpportunity.bAccountID);

  public override System.Type GetContactIDCommand() => typeof (CROpportunity.contactID);

  public override string GetCustomMailTo()
  {
    CROpportunity current = ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current;
    if (current == null)
      return (string) null;
    CRContact crContact = current.OpportunityContactID.With<int?, CRContact>((Func<int?, CRContact>) (_ => CRContact.PK.Find((PXGraph) this.Base, new int?(_.Value))));
    return string.IsNullOrWhiteSpace(crContact?.Email) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(crContact.Email, crContact.DisplayName);
  }

  protected virtual void _(Events.RowSelected<CROpportunity> e)
  {
    if (e.Row == null)
      return;
    CROpportunityClass parent = (CROpportunityClass) PrimaryKeyOf<CROpportunityClass>.By<CROpportunityClass.cROpportunityClassID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.classID>.FindParent((PXGraph) this.Base, (CROpportunity.classID) e.Row, (PKFindOptions) 0);
    if (parent == null)
      return;
    this.DefaultEmailAccountID = parent.DefaultEMailAccountID;
  }
}
