// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateOpportunityAllAction`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
public class CRCreateOpportunityAllAction<TGraph, TMaster, TOpportunityExt, TAccountExt, TContactExt> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TMaster : class, IBqlTable, new()
  where TOpportunityExt : CRCreateOpportunityAction<TGraph, TMaster>
  where TAccountExt : CRCreateAccountAction<TGraph, TMaster>
  where TContactExt : CRCreateContactAction<TGraph, TMaster>
{
  public PXAction<TMaster> ConvertToOpportunityAll;

  public TOpportunityExt OpportunityExt { get; private set; }

  public TAccountExt AccountExt { get; private set; }

  public TContactExt ContactExt { get; private set; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.OpportunityExt = this.Base.GetExtension<TOpportunityExt>() ?? throw new PXException("The graph does not have defined extension: {0}.", new object[1]
    {
      (object) typeof (TOpportunityExt).Name
    });
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
  [PXButton]
  public virtual IEnumerable convertToOpportunityAll(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRCreateOpportunityAllAction<TGraph, TMaster, TOpportunityExt, TAccountExt, TContactExt>.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new CRCreateOpportunityAllAction<TGraph, TMaster, TOpportunityExt, TAccountExt, TContactExt>.\u003C\u003Ec__DisplayClass14_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.\u003C\u003E4__this = this;
    if (this.AccountExt.NeedToUse)
    {
      Contact contact = ((PXSelectBase<Contact>) this.ContactExt.ExistingContact).SelectSingle(Array.Empty<object>());
      BAccount baccount = ((PXSelectBase<BAccount>) this.AccountExt.ExistingAccount).SelectSingle(Array.Empty<object>());
      if (contact != null && contact.BAccountID.HasValue && baccount == null)
      {
        ((PXSelectBase) this.AccountExt.Documents).Cache.SetValue<Document.bAccountID>((object) ((PXSelectBase<Document>) this.AccountExt.Documents).Current, (object) contact.BAccountID);
        GraphHelper.Caches<TMaster>((PXGraph) this.Base).Update(this.AccountExt.GetMainCurrent());
      }
    }
    List<CRPopupValidator> crPopupValidatorList = new List<CRPopupValidator>();
    if (this.ContactExt.NeedToUse)
      crPopupValidatorList.Add((CRPopupValidator) this.ContactExt.PopupValidator);
    if (this.AccountExt.NeedToUse)
      crPopupValidatorList.Add((CRPopupValidator) this.AccountExt.PopupValidator);
    // ISSUE: reference to a compiler-generated field
    if (this.OpportunityExt.AskExtConvert(out cDisplayClass140.redirect, crPopupValidatorList.ToArray()))
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass140.processingGraph = this.Base.CloneGraphState<TGraph>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass140, __methodptr(\u003CconvertToOpportunityAll\u003Eb__0)));
    }
    return adapter.Get();
  }

  public virtual void DoConvert(bool redirect)
  {
    ConversionResult<BAccount> conversionResult = (ConversionResult<BAccount>) null;
    ConversionResult<CROpportunity> result;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      bool? valueOriginal = ((PXSelectBase) this.OpportunityExt.Documents).Cache.GetValueOriginal<Document.overrideRefContact>((object) ((PXSelectBase<Document>) this.OpportunityExt.Documents).Current) as bool?;
      if (this.AccountExt.NeedToUse)
      {
        // ISSUE: variable of a boxed type
        __Boxed<TAccountExt> accountExt = (object) this.AccountExt;
        AccountConversionOptions options = new AccountConversionOptions();
        options.PreserveCachedRecordsFilters.Add((ICRPreserveCachedRecordsFilter) this.ContactExt.PopupValidator);
        options.PreserveCachedRecordsFilters.Add((ICRPreserveCachedRecordsFilter) this.OpportunityExt.PopupValidator);
        conversionResult = accountExt.Convert(options);
      }
      if (this.ContactExt.NeedToUse)
      {
        // ISSUE: variable of a boxed type
        __Boxed<TContactExt> contactExt = (object) this.ContactExt;
        ContactConversionOptions options = new ContactConversionOptions();
        options.GraphWithRelation = conversionResult?.Graph;
        options.PreserveCachedRecordsFilters.Add((ICRPreserveCachedRecordsFilter) this.OpportunityExt.PopupValidator);
        contactExt.Convert(options);
      }
      result = this.OpportunityExt.Convert(new OpportunityConversionOptions()
      {
        ForceOverrideContact = valueOriginal
      });
      if (!((PXSelectBase<Document>) this.OpportunityExt.Documents).Current.RefContactID.HasValue)
        throw new PXException("The opportunity cannot be created because the lead was changed improperly. Please refresh the page and try again.");
      transactionScope.Complete();
    }
    if (!redirect)
      return;
    this.OpportunityExt.Redirect(result);
  }
}
