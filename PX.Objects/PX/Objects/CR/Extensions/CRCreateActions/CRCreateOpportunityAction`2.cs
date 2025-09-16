// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateOpportunityAction`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GDPR;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
public abstract class CRCreateOpportunityAction<TGraph, TMain> : 
  CRCreateActionBase<TGraph, TMain, OpportunityMaint, CROpportunity, OpportunityFilter, OpportunityConversionOptions>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<OpportunityFilter> OpportunityInfo;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupAttributes> OpportunityInfoAttributes;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupUDFAttributes> OpportunityInfoUDF;
  public PXAction<TMain> ConvertToOpportunity;
  public PXAction<TMain> ConvertToOpportunityRedirect;

  protected override ICRValidationFilter[] AdditionalFilters
  {
    get
    {
      return new ICRValidationFilter[2]
      {
        (ICRValidationFilter) this.OpportunityInfoAttributes,
        (ICRValidationFilter) this.OpportunityInfoUDF
      };
    }
  }

  protected override CRValidationFilter<OpportunityFilter> FilterInfo => this.OpportunityInfo;

  protected virtual IEnumerable opportunityInfoAttributes()
  {
    return (IEnumerable) this.GetFilledAttributes();
  }

  protected virtual IEnumerable<PopupUDFAttributes> opportunityInfoUDF()
  {
    return this.GetRequiredUDFFields();
  }

  public virtual void _(
    Events.FieldDefaulting<OpportunityFilter, OpportunityFilter.subject> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<OpportunityFilter, OpportunityFilter.subject>, OpportunityFilter, object>) e).NewValue = (object) ((PXSelectBase<Document>) this.Documents).Current?.Description?.Replace("\r\n", " ")?.Replace("\n", " ");
  }

  public virtual void _(
    Events.FieldUpdated<OpportunityFilter, OpportunityFilter.opportunityClass> e)
  {
    ((PXCache) GraphHelper.Caches<PopupAttributes>((PXGraph) this.Base)).Clear();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable convertToOpportunity(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRCreateOpportunityAction<TGraph, TMain>.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new CRCreateOpportunityAction<TGraph, TMain>.\u003C\u003Ec__DisplayClass12_0();
    // ISSUE: reference to a compiler-generated field
    if (this.AskExtConvert(out cDisplayClass120.redirect))
    {
      if (this.Base.IsDirty)
        this.Base.Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.processingGraph = this.Base.CloneGraphState<TGraph>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass120, __methodptr(\u003CconvertToOpportunity\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable convertToOpportunityRedirect(PXAdapter adapter)
  {
    OpportunityMaint targetGraph = this.CreateTargetGraph();
    CROpportunity master = this.CreateMaster(targetGraph, (OpportunityConversionOptions) null);
    ConversionResult<CROpportunity> result = new ConversionResult<CROpportunity>();
    result.Graph = (PXGraph) targetGraph;
    result.Entity = master;
    result.Converted = false;
    this.Redirect(result);
    return adapter.Get();
  }

  internal override void AdjustFilterForContactBasedAPI(OpportunityFilter filter)
  {
    base.AdjustFilterForContactBasedAPI(filter);
    if (filter.Subject != null)
      return;
    filter.Subject = ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>())?.FullName;
  }

  protected override CROpportunity CreateMaster(
    OpportunityMaint graph,
    OpportunityConversionOptions options)
  {
    OpportunityFilter current1 = ((PXSelectBase<OpportunityFilter>) this.OpportunityInfo).Current;
    Document current2 = ((PXSelectBase<Document>) this.Documents).Current;
    DocumentContact source1 = ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>());
    DocumentAddress source2 = ((PXSelectBase<DocumentAddress>) this.Addresses).SelectSingle(Array.Empty<object>());
    CROpportunity crOpportunity = ((PXSelectBase<CROpportunity>) graph.Opportunity).Insert(new CROpportunity()
    {
      Subject = current1.Subject,
      CloseDate = current1.CloseDate,
      ClassID = current1.OpportunityClass,
      LeadID = current2.NoteID,
      Source = current2.Source,
      CampaignSourceID = current2.CampaignID,
      OverrideSalesTerritory = current2.OverrideSalesTerritory
    });
    bool? nullable = crOpportunity.OverrideSalesTerritory;
    if (nullable.HasValue && nullable.GetValueOrDefault())
      crOpportunity.SalesTerritoryID = current2.SalesTerritoryID;
    crOpportunity.ContactID = current2.RefContactID;
    crOpportunity.BAccountID = current2.BAccountID;
    if (current2 != null && current2.BAccountID.HasValue)
    {
      BAccount baccount = BAccount.PK.Find((PXGraph) graph, current2.BAccountID);
      crOpportunity.CuryID = baccount.CuryID ?? baccount.BaseCuryID;
    }
    if (((PXSelectBase<CROpportunityClass>) graph.OpportunityClass).SelectSingle(Array.Empty<object>())?.DefaultOwner == "S")
    {
      crOpportunity.OwnerID = current2.OwnerID;
      crOpportunity.WorkgroupID = current2.WorkgroupID;
    }
    CROpportunity master = ((PXSelectBase<CROpportunity>) graph.Opportunity).Update(crOpportunity);
    nullable = (bool?) options?.ForceOverrideContact;
    bool valueOrDefault;
    int num1;
    if (nullable.HasValue)
    {
      valueOrDefault = nullable.GetValueOrDefault();
      num1 = 1;
    }
    else
      num1 = 0;
    int num2 = valueOrDefault ? 1 : 0;
    if ((num1 & num2) != 0)
      ((PXSelectBase) graph.Opportunity).Cache.SetValueExt<CROpportunity.allowOverrideContactAddress>((object) master, (object) true);
    nullable = master.AllowOverrideContactAddress;
    if (nullable.GetValueOrDefault())
    {
      CRAddress target1 = ((PXSelectBase<CRAddress>) graph.Opportunity_Address).SelectSingle(Array.Empty<object>());
      this.MapAddress(source2, (IAddressBase) target1);
      ((PXSelectBase<CRAddress>) graph.Opportunity_Address).Update(target1);
      CRContact target2 = ((PXSelectBase<CRContact>) graph.Opportunity_Contact).SelectSingle(Array.Empty<object>());
      this.MapContact(source1, (IPersonalContact) target2);
      this.MapConsentable(source1, (IConsentable) target2);
      ((PXSelectBase<CRContact>) graph.Opportunity_Contact).Update(target2);
      CRShippingAddress target3 = ((PXSelectBase<CRShippingAddress>) graph.Shipping_Address).SelectSingle(Array.Empty<object>());
      this.MapAddress(source2, (IAddressBase) target3);
      ((PXSelectBase<CRShippingAddress>) graph.Shipping_Address).Update(target3);
      CRShippingContact target4 = ((PXSelectBase<CRShippingContact>) graph.Shipping_Contact).SelectSingle(Array.Empty<object>());
      this.MapContact(source1, (IPersonalContact) target4);
      this.MapConsentable(source1, (IConsentable) target4);
      ((PXSelectBase<CRShippingContact>) graph.Shipping_Contact).Update(target4);
    }
    this.FillAttributes((CRAttributeList<CROpportunity>) graph.Answers, master);
    this.FillUDF(((PXSelectBase) this.OpportunityInfoUDF).Cache, ((PXSelectBase) this.Documents).Cache.GetMain<Document>(current2), ((PXSelectBase) graph.Opportunity).Cache, master, master.ClassID);
    this.FillRelations((PXGraph) graph, master);
    this.FillNotesAndAttachments((PXGraph) graph, ((PXSelectBase) this.Documents).Cache.GetMain<Document>(current2), ((PXSelectBase) graph.Opportunity).Cache, master);
    return master;
  }

  protected override void ReverseDocumentUpdate(OpportunityMaint graph, CROpportunity entity)
  {
    Document current = ((PXSelectBase<Document>) this.Documents).Current;
    ((PXSelectBase) this.Documents).Cache.SetValue<Document.description>((object) current, (object) entity.Subject);
    ((PXSelectBase) this.Documents).Cache.SetValue<Document.qualificationDate>((object) current, (object) PXTimeZoneInfo.Now);
    ((PXSelectBase) this.Documents).Cache.SetValue<Document.convertedBy>((object) current, (object) PXAccess.GetUserID());
    GraphHelper.Caches<TMain>((PXGraph) graph).Update(this.GetMain(current));
  }
}
