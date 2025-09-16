// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkEntitiesExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.Cache;
using PX.Objects.CR.Wizard;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.SideBySideComparison.Link;

/// <summary>
/// The extension that provides ability to link two sets of entities after performing comparison of their fields
/// and selecting values from left or right entity sets.
/// </summary>
/// <typeparam name="TGraph">The entry <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TMain">The primary DAC (a <see cref="T:PX.Data.IBqlTable" /> type) of the <typeparam name="TGraph">graph</typeparam>.</typeparam>
/// <typeparam name="TFilter">The type of <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkFilter" /> that is used by the current extension.</typeparam>
public abstract class LinkEntitiesExt<TGraph, TMain, TFilter> : 
  CompareEntitiesExt<TGraph, TMain, LinkComparisonRow>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
  where TFilter : LinkFilter, new()
{
  protected override string ViewPrefix => "Link_";

  public PXSelectBase SelectEntityForLink { get; protected set; }

  public PXFilter<TFilter> Filter { get; protected set; }

  public override void Initialize()
  {
    base.Initialize();
    this.Filter = this.Base.GetOrCreateSelectFromView<PXFilter<TFilter>>(this.ViewPrefix + "Filter");
    this.SelectEntityForLink = (PXSelectBase) this.Base.GetOrCreateSelectFromView<PXSelectBase<TFilter>>(this.ViewPrefix + "SelectEntityForLink");
  }

  /// <summary>
  /// Shows(and processes if called for the second time) the smart panel that shows entities to be linked.
  /// </summary>
  /// <remarks>
  /// After the method execution, the result is processed with <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkEntitiesExt`3.AskLinkWithEntityByID(System.Object)" />.
  /// </remarks>
  /// <param name="entityID">Optional ID of the entity with which the current entity should be linked.</param>
  /// <exception cref="T:PX.Data.PXDialogRequiredException">Is raised to render the smart panel for user interaction.</exception>
  /// <exception cref="T:PX.Objects.CR.Wizard.CRWizardBackException">Is raised when <see cref="F:PX.Objects.CR.Wizard.WizardResult.Back" /> is clicked.</exception>
  /// <exception cref="T:PX.Objects.CR.Wizard.CRWizardAbortException">Is raised when <see cref="F:PX.Objects.CR.Wizard.WizardResult.Abort" /> is clicked.</exception>
  public virtual void SelectEntityForLinkAsk(object entityID = null)
  {
    object entityID1 = entityID ?? this.GetSelectedEntityID();
    WebDialogResult webDialogResult = this.AskSelectEntity();
    switch ((int) webDialogResult)
    {
      case 0:
        this.AskSelectEntity();
        break;
      case 1:
      case 6:
        try
        {
          this.LinkAsk(entityID1);
          break;
        }
        catch (Exception ex)
        {
          if (!(ex is CRWizardBackException))
            throw;
          goto case 0;
        }
      case 2:
        break;
      case 3:
        this.ClearAnswers();
        throw new CRWizardAbortException();
      case 4:
        break;
      case 5:
        break;
      default:
        if (webDialogResult != 72)
          break;
        this.ClearAnswers();
        throw new CRWizardBackException();
    }
  }

  /// <summary>
  /// Shows (and processes if called for the second time) the smart panel
  /// on which a user selects the fields that remains in both records after linking.
  /// </summary>
  /// <remarks>
  /// After the method execution, the result is processed with <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkEntitiesExt`3.AskLinkWithEntityByID(System.Object)" />.
  /// </remarks>
  /// <param name="entityID">Optional ID of the entity with which the current entity is linked.</param>
  /// <exception cref="T:PX.Data.PXDialogRequiredException">Is raised to render the smart panel for user interaction.</exception>
  /// <exception cref="T:PX.Objects.CR.Wizard.CRWizardBackException">Is raised when <see cref="F:PX.Objects.CR.Wizard.WizardResult.Back" /> is clicked.</exception>
  /// <exception cref="T:PX.Objects.CR.Wizard.CRWizardAbortException">Is raised when <see cref="F:PX.Objects.CR.Wizard.WizardResult.Abort" /> is clicked.</exception>
  public virtual void LinkAsk(object entityID = null)
  {
    WebDialogResult webDialogResult = this.AskLinkWithEntityByID(entityID ?? this.GetSelectedEntityID());
    if (webDialogResult <= 3)
    {
      if (webDialogResult != 1)
      {
        if (webDialogResult != 3)
          return;
        this.ClearAnswers();
        throw new CRWizardAbortException();
      }
    }
    else if (webDialogResult != 6)
    {
      if (webDialogResult != 72)
        return;
      this.ClearAnswers();
      throw new CRWizardBackException();
    }
    this.ProcessLink();
  }

  public virtual void ProcessLink()
  {
    PreventRecursionCall.Execute((Action) (() =>
    {
      bool? processLink = ((PXSelectBase<TFilter>) this.Filter).Current.ProcessLink;
      if (processLink.HasValue && processLink.GetValueOrDefault())
        this.ProcessComparisonResult();
      this.UpdateMainAfterProcess();
    }), nameof (ProcessLink), "C:\\build\\code_repo\\WebSites\\Pure\\PX.Objects\\CR\\GraphExtensions\\SideBySideComparison\\Link\\LinkEntitiesExt.cs", 130);
    this.ClearAnswers();
  }

  protected virtual void ClearAnswers()
  {
    if (this.SelectEntityForLink != null)
    {
      this.SelectEntityForLink.Cache.Current = (object) null;
      this.SelectEntityForLink.View.Answer = (WebDialogResult) 0;
    }
    ((PXSelectBase) this.Filter).View.Answer = (WebDialogResult) 0;
  }

  protected abstract object GetSelectedEntityID();

  public override IEnumerable<LinkComparisonRow> FilterComparisons(
    IEnumerable<LinkComparisonRow> comparisons)
  {
    foreach (LinkComparisonRow filterComparison in base.FilterComparisons(comparisons))
    {
      if (!filterComparison.LeftFieldState.IsReadOnly && filterComparison.LeftFieldState.Enabled)
        yield return filterComparison;
    }
  }

  public override IEnumerable<LinkComparisonRow> UpdateComparisons(
    IEnumerable<LinkComparisonRow> comparisons)
  {
    foreach (LinkComparisonRow updateComparison in base.UpdateComparisons(comparisons))
    {
      if (!string.IsNullOrEmpty(updateComparison.RightValue))
        updateComparison.Selection = ComparisonSelection.Right;
      if (string.IsNullOrEmpty(updateComparison.LeftValue) || updateComparison.RightFieldState.IsReadOnly || !updateComparison.RightFieldState.Enabled)
        updateComparison.Hidden = new bool?(true);
      yield return updateComparison;
    }
  }

  public override IEnumerable<string> GetFieldsForComparison(
    System.Type itemType,
    PXCache leftCache,
    PXCache rightCache)
  {
    return leftCache.GetFields_ContactInfo().Union<string>(rightCache.GetFields_ContactInfo());
  }

  public virtual void UpdateMainAfterProcess()
  {
  }

  /// <summary>
  /// Shows the smart panel that displays entities to be linked.
  /// </summary>
  /// <exception cref="T:PX.Data.PXDialogRequiredException">Is raised to render the smart panel for user interaction.</exception>
  public virtual WebDialogResult AskSelectEntity()
  {
    PXSelectBase selectEntityForLink = this.SelectEntityForLink;
    WebDialogResult? nullable;
    if (selectEntityForLink == null)
    {
      nullable = new WebDialogResult?();
    }
    else
    {
      PXView view1 = selectEntityForLink.View;
      if (view1 == null)
      {
        nullable = new WebDialogResult?();
      }
      else
      {
        PXView view2 = view1.WithAnswerForImport((WebDialogResult) 6);
        if (view2 == null)
        {
          nullable = new WebDialogResult?();
        }
        else
        {
          PXView view3 = view2.WithAnswerForMobile((WebDialogResult) 6);
          nullable = view3 != null ? view3.WithAnswerForCbApi((WebDialogResult) 6)?.AskExt() : new WebDialogResult?();
        }
      }
    }
    return nullable ?? (WebDialogResult) 6;
  }

  /// <summary>
  /// Shows the smart panel on which a user selects fields that remains in both records after linking.
  /// </summary>
  /// <param name="entityID">The ID of the entity with which the current entity should be linked.</param>
  /// <exception cref="T:PX.Data.PXDialogRequiredException">Is raised to render the smart panel for user interaction.</exception>
  public virtual WebDialogResult AskLinkWithEntityByID(object entityID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    LinkEntitiesExt<TGraph, TMain, TFilter>.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new LinkEntitiesExt<TGraph, TMain, TFilter>.\u003C\u003Ec__DisplayClass21_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.entityID = entityID;
    // ISSUE: reference to a compiler-generated method
    // ISSUE: method pointer
    return ((PXSelectBase<TFilter>) this.Filter.WithActionIfNoAnswerFor<PXFilter<TFilter>>(this.Base.IsImport || this.Base.IsMobile || this.Base.IsContractBasedAPI, new Action(cDisplayClass210.\u003CAskLinkWithEntityByID\u003Eb__0)).WithAnswerForImport<PXFilter<TFilter>>((WebDialogResult) 6).WithAnswerForMobile<PXFilter<TFilter>>((WebDialogResult) 6).WithAnswerForCbApi<PXFilter<TFilter>>((WebDialogResult) 6)).AskExt((string) null, new PXView.InitializePanel((object) cDisplayClass210, __methodptr(\u003CAskLinkWithEntityByID\u003Eb__1)));
  }

  protected virtual void _(Events.RowSelected<LinkComparisonRow> e)
  {
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<LinkComparisonRow>>) e).Cache, (object) e.Row).For<ComparisonRow.leftValueSelected>((Action<PXUIFieldAttribute>) (ui =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = ui;
      bool? processLink = ((PXSelectBase<TFilter>) this.Filter).Current.ProcessLink;
      int num = !processLink.HasValue ? 0 : (processLink.GetValueOrDefault() ? 1 : 0);
      pxuiFieldAttribute.Enabled = num != 0;
    })).SameFor<ComparisonRow.rightValueSelected>();
  }

  protected virtual void _(Events.RowSelected<TFilter> e)
  {
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TFilter>>) e).Cache, (object) e.Row).For<LinkFilter.caption>((Action<PXUIFieldAttribute>) (ui => ui.Visible = ((IEnumerable<PXResult<LinkComparisonRow>>) this.VisibleComparisonRows.Select(Array.Empty<object>())).ToList<PXResult<LinkComparisonRow>>().Any<PXResult<LinkComparisonRow>>()));
  }

  protected virtual void _(Events.FieldSelecting<LinkFilter.caption> e)
  {
    if (e == null || e.Row == null)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<LinkFilter.caption>>) e).ReturnValue = (object) PXMessages.Localize("Select the field values that you want to use for the contact.");
  }
}
