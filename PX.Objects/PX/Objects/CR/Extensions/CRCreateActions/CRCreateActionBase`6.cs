// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateActionBase`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.MassProcess;
using PX.Objects.CR.Wizard;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
[PXInternalUseOnly]
public abstract class CRCreateActionBase<TGraph, TMain, TTargetGraph, TTarget, TFilter, TConversionOptions> : 
  CRCreateActionBaseInit<TGraph, TMain>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
  where TTargetGraph : PXGraph, new()
  where TTarget : class, IBqlTable, INotable, new()
  where TFilter : class, IBqlTable, IClassIdFilter, new()
  where TConversionOptions : ConversionOptions<TTargetGraph, TTarget>
{
  private CRPopupValidator.Generic<TFilter> _popupValidator;

  protected virtual string TargetType => "PX.Objects.CR.Contact";

  public virtual CRPopupValidator.Generic<TFilter> PopupValidator
  {
    get
    {
      return this._popupValidator ?? (this._popupValidator = CRPopupValidator.Create<TFilter>(this.FilterInfo, this.AdditionalFilters));
    }
  }

  protected virtual ICRValidationFilter[] AdditionalFilters => (ICRValidationFilter[]) null;

  public bool NeedToUse { get; set; }

  public virtual void ClearAnswers(bool clearCurrent = false)
  {
    ((PXSelectBase) this.FilterInfo).ClearAnswers(clearCurrent);
  }

  public virtual void Initialize()
  {
    this.NeedToUse = true;
    ((PXGraphExtension) this).Initialize();
  }

  protected abstract CRValidationFilter<TFilter> FilterInfo { get; }

  protected virtual PXSelectBase<CRPMTimeActivity> Activities
  {
    get => (PXSelectBase<CRPMTimeActivity>) null;
  }

  public virtual void _(
    Events.FieldVerifying<PopupAttributes, FieldValue.displayName> e)
  {
    if (e.Row?.Value == null)
      throw new PXSetPropertyException("Values should be specified for all required attributes.", (PXErrorLevel) 4);
  }

  public virtual IEnumerable<PopupAttributes> GetFilledAttributes()
  {
    CRCreateActionBase<TGraph, TMain, TTargetGraph, TTarget, TFilter, TConversionOptions> createActionBase = this;
    PXCache cache = createActionBase.Base.Caches[typeof (PopupAttributes)];
    foreach (PopupAttributes preparedAttribute in createActionBase.GetPreparedAttributes())
    {
      PopupAttributes popupAttributes = (PopupAttributes) cache.Locate((object) preparedAttribute);
      if (popupAttributes == null)
        GraphHelper.Hold(cache, (object) preparedAttribute);
      yield return popupAttributes ?? preparedAttribute;
    }
  }

  protected virtual IEnumerable<PopupAttributes> GetPreparedAttributes()
  {
    List<CSAnswers> master = this.GetAttributesForMasterEntity().Where<CSAnswers>((Func<CSAnswers, bool>) (a => a.Value != null)).ToList<CSAnswers>();
    return ((IEnumerable<CRAttribute.AttributeExt>) CRAttribute.EntityAttributes(typeof (TTarget), ((PXSelectBase<TFilter>) this.FilterInfo).Current?.ClassID)).Where<CRAttribute.AttributeExt>((Func<CRAttribute.AttributeExt, bool>) (a => a.Required)).Select<CRAttribute.AttributeExt, (CRAttribute.AttributeExt, CSAnswers)>((Func<CRAttribute.AttributeExt, (CRAttribute.AttributeExt, CSAnswers)>) (a => (a, master.FirstOrDefault<CSAnswers>((Func<CSAnswers, bool>) (_a => _a.AttributeID == a.ID))))).Select<(CRAttribute.AttributeExt, CSAnswers), PopupAttributes>((Func<(CRAttribute.AttributeExt, CSAnswers), int, PopupAttributes>) ((a, i) =>
    {
      PopupAttributes preparedAttributes = new PopupAttributes();
      preparedAttributes.Selected = new bool?(false);
      ((FieldValue) preparedAttributes).CacheName = typeof (TTarget).FullName;
      preparedAttributes.Name = a.entity.ID + "_Attributes";
      preparedAttributes.DisplayName = a.entity.Description;
      preparedAttributes.AttributeID = a.entity.ID;
      preparedAttributes.Value = a.master?.Value ?? a.entity.DefaultValue;
      preparedAttributes.Order = new int?(i);
      preparedAttributes.Required = new bool?(true);
      return preparedAttributes;
    }));
  }

  protected virtual IEnumerable<CSAnswers> GetAttributesForMasterEntity()
  {
    return GraphHelper.RowCast<CSAnswers>((IEnumerable) this.Base.Views["Answers"].SelectMulti(Array.Empty<object>()));
  }

  protected virtual object GetMasterEntity() => (object) null;

  public virtual void _(
    Events.FieldSelecting<PopupAttributes, FieldValue.value> e)
  {
    PopupAttributes row = e.Row;
    if (row == null || !typeof (TTarget).FullName.Equals(((FieldValue) row).CacheName))
      return;
    PXDBAttributeAttribute.Activate(this.Base.Caches[typeof (TTarget)]);
    ((Events.FieldSelectingBase<Events.FieldSelecting<PopupAttributes, FieldValue.value>>) e).ReturnState = (object) PXMassProcessHelper.InitValueFieldState(this.Base.Caches[typeof (TTarget)], (FieldValue) e.Row);
    ((Events.FieldSelectingBase<Events.FieldSelecting<PopupAttributes, FieldValue.value>>) e).Cancel = true;
  }

  public virtual void _(
    Events.FieldVerifying<PopupUDFAttributes, PopupUDFAttributes.displayName> e)
  {
    if (e.Row?.Value == null || string.IsNullOrEmpty(e.Row?.Value.ToString()))
      throw new PXSetPropertyException("Values must be specified for all required user-defined fields.", (PXErrorLevel) 4);
  }

  public virtual void _(
    Events.FieldSelecting<PopupUDFAttributes, PopupUDFAttributes.value> e)
  {
    PopupUDFAttributes row = e.Row;
    string screenId = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (TTargetGraph))?.ScreenID;
    if (row == null || screenId == null || !screenId.Equals(row.ScreenID))
      return;
    PXDBAttributeAttribute.Activate(this.Base.Caches[typeof (TTarget)]);
    PXFieldState graphUdfFieldState = UDFHelper.GetGraphUDFFieldState(typeof (TTargetGraph), row.AttributeID);
    if (graphUdfFieldState == null)
      return;
    graphUdfFieldState.Required = new bool?(true);
    if (!string.IsNullOrEmpty(row.Value))
      graphUdfFieldState.Value = (object) row.Value;
    ((Events.FieldSelectingBase<Events.FieldSelecting<PopupUDFAttributes, PopupUDFAttributes.value>>) e).ReturnState = (object) graphUdfFieldState;
    ((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<PopupUDFAttributes, PopupUDFAttributes.value>>) e).Cache.IsDirty = false;
  }

  public virtual void ValidateForImport(params CRPopupValidator[] validators)
  {
    if (!this.Base.IsContractBasedAPI && this.Base.IsDirty)
      this.Base.Actions.PressSave();
    this.AdjustFilterForContactBasedAPI(((PXSelectBase<TFilter>) this.FilterInfo).Current);
    this.PopupValidator.Validate(validators);
  }

  public virtual void CheckWizardState()
  {
    if (!WizardScope.IsScoped)
      return;
    WebDialogResult answer = ((PXSelectBase) this.PopupValidator.Filter).View.Answer;
    if (answer != 3)
    {
      if (answer == 72)
      {
        this.ClearAnswers();
        throw new CRWizardBackException();
      }
    }
    else
    {
      this.ClearAnswers();
      throw new CRWizardAbortException();
    }
  }

  public virtual WebDialogResult AskExt(params CRPopupValidator[] validators)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRCreateActionBase<TGraph, TMain, TTargetGraph, TTarget, TFilter, TConversionOptions>.\u003C\u003Ec__DisplayClass27_0 cDisplayClass270 = new CRCreateActionBase<TGraph, TMain, TTargetGraph, TTarget, TFilter, TConversionOptions>.\u003C\u003Ec__DisplayClass27_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.validators = validators;
    try
    {
      // ISSUE: method pointer
      return this.PopupValidator.AskExt(new PXView.InitializePanel((object) cDisplayClass270, __methodptr(\u003CAskExt\u003Eb__0)), !WizardScope.IsScoped);
    }
    catch (PXSetPropertyException ex)
    {
      return (WebDialogResult) 3;
    }
  }

  public virtual bool AskExtConvert(out bool redirect, params CRPopupValidator[] validators)
  {
    return this.AskExtConvert(true, out redirect, validators);
  }

  public virtual bool AskExtConvert(
    bool throwOnException,
    out bool redirect,
    params CRPopupValidator[] validators)
  {
    if (this.Base.IsContractBasedAPI || this.Base.IsImport)
    {
      this.ValidateForImport(validators);
      redirect = false;
      return true;
    }
    this.CheckWizardState();
    if (this.AskWasAborted())
      return redirect = false;
    WebDialogResult webDialogResult = this.AskExt(validators);
    bool flag = this.PopupValidator.TryValidate(validators);
    if (webDialogResult != 3 & throwOnException && !flag)
      throw new PXActionInterruptException("Validation is failed for one or more fields.");
    redirect = webDialogResult == 6;
    return flag && WebDialogResultExtension.IsPositive(webDialogResult);
  }

  public bool AskWasAborted() => ((PXSelectBase) this.PopupValidator.Filter).View.Answer == 3;

  internal virtual void AdjustFilterForContactBasedAPI(TFilter filter)
  {
  }

  public TMain GetMain(Document doc)
  {
    return (TMain) ((PXSelectBase) this.Documents).Cache.GetMain<Document>(doc);
  }

  public TMain GetMainCurrent() => this.GetMain(((PXSelectBase<Document>) this.Documents).Current);

  public virtual ConversionResult<TTarget> Convert(TConversionOptions options = null)
  {
    return this.TryConvert(options).ThrowIfHasException<ConversionResult<TTarget>>();
  }

  public virtual ConversionResult<TTarget> TryConvert(TConversionOptions options = null)
  {
    TTargetGraph graph = default (TTargetGraph);
    TTarget entity = default (TTarget);
    Exception exception = (Exception) null;
    try
    {
      graph = this.CreateTargetGraph();
      entity = this.CreateMaster(graph, options);
      this.ReverseDocumentUpdate(graph, entity);
      this.OnBeforePersist(graph, entity);
      // ISSUE: variable of a boxed type
      __Boxed<TConversionOptions> local1 = (object) options;
      if ((local1 != null ? (!local1.DoNotPersistAfterConvert ? 1 : 0) : 1) != 0)
        graph.Actions.PressSave();
      // ISSUE: variable of a boxed type
      __Boxed<TConversionOptions> local2 = (object) options;
      if ((local2 != null ? (!local2.DoNotCancelAfterConvert ? 1 : 0) : 1) != 0)
      {
        using (options.PreserveCachedRecords<TTargetGraph, TTarget>())
        {
          this.Base.Actions.PressCancel();
          ((PXSelectBase) this.Documents).View.Clear();
          ((PXSelectBase<Document>) this.Documents).Current = PXResultset<Document>.op_Implicit(((PXSelectBase<Document>) this.Documents).Search<Document.noteID>((object) ((PXSelectBase<Document>) this.Documents).Current.NoteID, Array.Empty<object>()));
        }
      }
    }
    catch (Exception ex)
    {
      exception = ex;
    }
    ConversionResult<TTarget> conversionResult = new ConversionResult<TTarget>();
    conversionResult.Graph = (PXGraph) graph;
    conversionResult.Entity = entity;
    conversionResult.Exception = exception;
    return conversionResult;
  }

  protected virtual TTargetGraph CreateTargetGraph()
  {
    TTargetGraph instance = PXGraph.CreateInstance<TTargetGraph>();
    ((PXCache) GraphHelper.Caches<TMain>((PXGraph) instance)).Current = (object) this.GetMainCurrent();
    return instance;
  }

  protected abstract TTarget CreateMaster(TTargetGraph graph, TConversionOptions options);

  public void Redirect(ConversionResult<TTarget> result)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
    PXGraph pxGraph = result.Graph ?? (PXGraph) this.CreateTargetGraph();
    ((PXCache) GraphHelper.Caches<TTarget>(pxGraph)).Current = (object) result.Entity;
    if (result.Converted)
      pxGraph.Actions.PressCancel();
    throw new PXRedirectRequiredException(pxGraph, typeof (TTarget).Name);
  }

  protected virtual void FillAttributes(CRAttributeList<TTarget> answers, TTarget entity)
  {
    answers.CopyAllAttributes((object) entity, (object) this.GetMainCurrent());
    IEnumerable<PopupAttributes> filledAttributes = this.GetFilledAttributes();
    foreach (PopupAttributes popupAttributes in (filledAttributes != null ? filledAttributes.Where<PopupAttributes>((Func<PopupAttributes, bool>) (a => a.Value != null)) : (IEnumerable<PopupAttributes>) null) ?? Enumerable.Empty<PopupAttributes>())
      answers.Update(new CSAnswers()
      {
        AttributeID = popupAttributes.AttributeID,
        Value = popupAttributes.Value,
        RefNoteID = entity.NoteID,
        IsActive = new bool?(true)
      });
  }

  protected virtual void FillUDF(
    PXCache sourceUDFPopupCache,
    object src_row,
    PXCache dst_cache,
    TTarget dst_row,
    string classID)
  {
    UDFHelper.CopyAttributes((PXCache) GraphHelper.Caches<TMain>((PXGraph) this.Base), src_row, dst_cache, (object) dst_row, classID);
    UDFHelper.FillfromPopupUDF((PXCache) GraphHelper.Caches<TTarget>((PXGraph) this.Base), sourceUDFPopupCache, typeof (TTargetGraph), (object) dst_row);
  }

  protected virtual void FillNotesAndAttachments(
    PXGraph graph,
    object src_row,
    PXCache dst_cache,
    TTarget dst_row)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.customerModule>();
    CRSetup crSetup = PXResultset<CRSetup>.op_Implicit(PXSelectBase<CRSetup, PXSelectReadonly<CRSetup>.Config>.Select(graph, Array.Empty<object>()));
    PXNoteAttribute.CopyNoteAndFiles((PXCache) GraphHelper.Caches<TMain>(graph), src_row, dst_cache, (object) dst_row, new bool?(((crSetup != null ? (crSetup.CopyNotes.GetValueOrDefault() ? 1 : 0) : 0) & (flag ? 1 : 0)) != 0), new bool?(((crSetup != null ? (crSetup.CopyFiles.GetValueOrDefault() ? 1 : 0) : 0) & (flag ? 1 : 0)) != 0));
  }

  protected virtual void FillRelations(PXGraph graph, TTarget target)
  {
    Document current = ((PXSelectBase<Document>) this.Documents).Current;
    PXCache cach = graph.Caches[typeof (CRRelation)];
    CRRelation instance = (CRRelation) cach.CreateInstance();
    instance.RefNoteID = target.NoteID;
    instance.RefEntityType = target.GetType().FullName;
    instance.Role = "SR";
    instance.TargetType = this.TargetType;
    instance.TargetNoteID = current.NoteID;
    instance.ContactID = current.RefContactID;
    PXDBDefaultAttribute.SetDefaultForInsert<CRRelation.refNoteID>(cach, (object) instance, false);
    cach.Insert((object) instance);
  }

  protected virtual void ReverseDocumentUpdate(TTargetGraph graph, TTarget entity)
  {
  }

  protected virtual void OnBeforePersist(TTargetGraph graph, TTarget entity)
  {
  }

  protected virtual TTarget MapFromDocument(Document source, TTarget target) => target;

  public virtual IEnumerable<PopupUDFAttributes> GetRequiredUDFFields()
  {
    PXCache<TMain> sourceCache = GraphHelper.Caches<TMain>((PXGraph) this.Base);
    object masterEntity = this.GetMasterEntity();
    System.Type targetGraph = typeof (TTargetGraph);
    CRValidationFilter<TFilter> filterInfo = this.FilterInfo;
    string classId = filterInfo != null ? ((PXSelectBase<TFilter>) filterInfo).Current?.ClassID : (string) null;
    return UDFHelper.GetRequiredUDFFields((PXCache) sourceCache, masterEntity, targetGraph, classId);
  }
}
