// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateBothContactAndAccountAction`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
public abstract class CRCreateBothContactAndAccountAction<TGraph, TMaster, TAccountExt, TContactExt> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TMaster : class, IBqlTable, new()
  where TAccountExt : CRCreateAccountAction<TGraph, TMaster>
  where TContactExt : CRCreateContactAction<TGraph, TMaster>
{
  public PXAction<TMaster> CreateBothContactAndAccount;

  public TAccountExt AccountExt { get; private set; }

  public TContactExt ContactExt { get; private set; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.AccountExt = this.Base.GetExtension<TAccountExt>() ?? throw new PXException("The graph does not have defined extension: {0}.", new object[1]
    {
      (object) typeof (TAccountExt).Name
    });
    this.ContactExt = this.Base.GetExtension<TContactExt>() ?? throw new PXException("The graph does not have defined extension: {0}.", new object[1]
    {
      (object) typeof (TContactExt).Name
    });
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable createBothContactAndAccount(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRCreateBothContactAndAccountAction<TGraph, TMaster, TAccountExt, TContactExt>.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new CRCreateBothContactAndAccountAction<TGraph, TMaster, TAccountExt, TContactExt>.\u003C\u003Ec__DisplayClass10_0();
    Contact contact = ((PXSelectBase<Contact>) this.ContactExt.ExistingContact).SelectSingle(Array.Empty<object>());
    BAccount baccount = ((PXSelectBase<BAccount>) this.AccountExt.ExistingAccount).SelectSingle(Array.Empty<object>());
    if (contact != null && contact.BAccountID.HasValue && baccount == null)
    {
      ((PXSelectBase) this.AccountExt.Documents).Cache.SetValue<Document.bAccountID>((object) ((PXSelectBase<Document>) this.AccountExt.Documents).Current, (object) contact.BAccountID);
      GraphHelper.Caches<TMaster>((PXGraph) this.Base).Update(this.AccountExt.GetMainCurrent());
      PXGraph.ThrowWithoutRollback((Exception) new PXSetPropertyException("A new business account cannot be created because the specified contact already belongs to a business account. Would you like to link the lead with the business account of the contact?"));
    }
    // ISSUE: reference to a compiler-generated field
    if (this.AccountExt.AskExtConvert(out cDisplayClass100.redirect, (CRPopupValidator) this.ContactExt.PopupValidator))
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.processingGraph = this.Base.CloneGraphState<TGraph>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass100, __methodptr(\u003CcreateBothContactAndAccount\u003Eb__0)));
    }
    return adapter.Get();
  }

  public virtual void DoConvert(bool redirect)
  {
    ConversionResult<BAccount> result;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      // ISSUE: variable of a boxed type
      __Boxed<TAccountExt> accountExt = (object) this.AccountExt;
      AccountConversionOptions options = new AccountConversionOptions();
      options.PreserveCachedRecordsFilters.Add((ICRPreserveCachedRecordsFilter) this.ContactExt.PopupValidator);
      result = accountExt.Convert(options);
      this.ContactExt.Convert(new ContactConversionOptions()
      {
        GraphWithRelation = result.Graph
      });
      if (!((PXSelectBase<Document>) this.AccountExt.Documents).Current.RefContactID.HasValue)
        throw new PXException("The account cannot be created because the lead was changed improperly. Please refresh the page and try again.");
      transactionScope.Complete();
    }
    if (!redirect)
      return;
    this.AccountExt.Redirect(result);
  }

  public virtual void _(Events.RowSelected<TMaster> e)
  {
    if (this.AccountExt?.CreateBAccount == null)
      return;
    ((PXAction) this.CreateBothContactAndAccount).SetEnabled(((PXAction) this.AccountExt.CreateBAccount).GetEnabled());
  }

  public virtual void _(Events.RowSelected<ContactFilter> e)
  {
    if (e.Row == null)
      return;
    bool flag = ((PXSelectBase<BAccount>) this.AccountExt.ExistingAccount).SelectSingle(Array.Empty<object>()) == null == (((PXSelectBase<Contact>) this.ContactExt.ExistingContact).SelectSingle(Array.Empty<object>()) == null);
    PXUIFieldAttribute.SetEnabled<ContactFilter.fullName>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ContactFilter>>) e).Cache, (object) e.Row, flag);
  }

  public virtual void _(Events.FieldUpdated<AccountsFilter.accountName> e)
  {
    ContactFilter contactFilter = ((PXSelectBase<ContactFilter>) this.ContactExt.ContactInfo).SelectSingle(Array.Empty<object>());
    if (contactFilter == null)
      return;
    ((PXSelectBase) this.ContactExt.ContactInfo).Cache.SetValueExt<ContactFilter.fullName>((object) contactFilter, e.NewValue);
  }
}
