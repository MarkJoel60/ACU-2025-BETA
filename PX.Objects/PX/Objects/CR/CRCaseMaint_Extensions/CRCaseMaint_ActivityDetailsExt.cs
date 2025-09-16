// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseMaint_Extensions.CRCaseMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.Extensions;
using System;

#nullable disable
namespace PX.Objects.CR.CRCaseMaint_Extensions;

public class CRCaseMaint_ActivityDetailsExt : ActivityDetailsExt<CRCaseMaint, CRCase, CRCase.noteID>
{
  public override System.Type GetBAccountIDCommand() => typeof (CRCase.customerID);

  public override System.Type GetContactIDCommand() => typeof (CRCase.contactID);

  public override string GetCustomMailTo()
  {
    CRCase current = ((PXSelectBase<CRCase>) this.Base.Case).Current;
    if (current == null)
      return (string) null;
    Contact contact1 = current.ContactID.With<int?, Contact>((Func<int?, Contact>) (_ => Contact.PK.Find((PXGraph) this.Base, new int?(_.Value))));
    if (!string.IsNullOrWhiteSpace(contact1?.EMail))
      return PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(contact1.EMail, contact1.DisplayName);
    Contact contact2 = current.CustomerID.With<int?, PXResult<Contact, BAccount>>((Func<int?, PXResult<Contact, BAccount>>) (_ => (PXResult<Contact, BAccount>) PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelectJoin<Contact, InnerJoin<BAccount, On<BAccount.defContactID, Equal<Contact.contactID>>>, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) _.Value
    })))).With<PXResult<Contact, BAccount>, Contact>((Func<PXResult<Contact, BAccount>, Contact>) (_ => PXResult<Contact, BAccount>.op_Implicit(_)));
    return string.IsNullOrWhiteSpace(contact2?.EMail) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(contact2.EMail, contact2.DisplayName);
  }

  protected virtual void _(Events.RowSelected<CRCase> e)
  {
    if (e.Row == null)
      return;
    this.DefaultSubject = PXMessages.LocalizeFormatNoPrefixNLA("[Case #{0}] {1}", new object[2]
    {
      (object) e.Row.CaseCD,
      (object) e.Row.Subject
    });
    CRCaseClass parent = (CRCaseClass) PrimaryKeyOf<CRCaseClass>.By<CRCaseClass.caseClassID>.ForeignKeyOf<CRCase>.By<CRCase.caseClassID>.FindParent((PXGraph) this.Base, (CRCase.caseClassID) e.Row, (PKFindOptions) 0);
    if (parent != null)
      this.DefaultEmailAccountID = parent.DefaultEMailAccountID;
    ((PXSelectBase) this.Activities).Cache.AllowInsert = !e.Row.Released.GetValueOrDefault();
  }

  protected override void _(Events.RowSelected<CRPMTimeActivity> e)
  {
    base._(e);
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRPMTimeActivity>>) e).Cache, (object) e.Row).For<CRActivity.providesCaseSolution>((Action<PXUIFieldAttribute>) (ui => ui.Visible = true));
  }
}
