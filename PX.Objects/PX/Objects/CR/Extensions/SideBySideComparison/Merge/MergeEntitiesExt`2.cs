// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.Cache;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.SideBySideComparison.Merge;

/// <summary>
/// The extension that can be used to merge two sets of entities after performing comparision of their fields
/// and selecting values from left or right entity sets.
/// </summary>
/// <typeparam name="TGraph">The entry <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TMain">The primary DAC (a <see cref="T:PX.Data.IBqlTable" /> type) of the <typeparamref name="TGraph">graph</typeparamref>.</typeparam>
public abstract class MergeEntitiesExt<TGraph, TMain> : 
  CompareEntitiesExt<TGraph, TMain, MergeComparisonRow>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, INotable, new()
{
  protected override string ViewPrefix => "Merge_";

  public CRValidationFilter<MergeEntitiesFilter> Filter { get; protected set; }

  public override string LeftValueDescription => "Current Record";

  public override string RightValueDescription => "Duplicate Record";

  public override void Initialize()
  {
    base.Initialize();
    PXGraph.RowSelectedEvents rowSelected = this.Base.RowSelected;
    System.Type primaryItemType = this.Base.PrimaryItemType;
    MergeEntitiesExt<TGraph, TMain> mergeEntitiesExt = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) mergeEntitiesExt, __vmethodptr(mergeEntitiesExt, PrimaryRowSelected));
    rowSelected.AddHandler(primaryItemType, pxRowSelected);
    this.Filter = this.Base.GetOrCreateSelectFromView<CRValidationFilter<MergeEntitiesFilter>>(this.ViewPrefix + "Filter");
  }

  /// <summary>
  /// Performs the update of the related to target and duplicate entities
  /// that are included neither in the left <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext" /> nor in the right <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext" />.
  /// </summary>
  /// <param name="targetEntity">The target entity.</param>
  /// <param name="duplicateEntity">The duplicate entity.</param>
  public virtual void MergeRelatedDocuments(TMain targetEntity, TMain duplicateEntity)
  {
    PXCache cach = this.Base.Caches[typeof (TMain)];
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(cach, (object) duplicateEntity);
    PXNoteAttribute.SetFileNotes(cach, (object) targetEntity, fileNotes);
    string note1 = PXNoteAttribute.GetNote(cach, (object) targetEntity);
    string note2 = PXNoteAttribute.GetNote(cach, (object) duplicateEntity);
    if (string.IsNullOrWhiteSpace(note2))
      return;
    if (!string.IsNullOrWhiteSpace(note1))
      note1 += "\r\n\r\n";
    string str = note1 + note2;
    PXNoteAttribute.SetNote(cach, (object) targetEntity, str);
  }

  public override IEnumerable<MergeComparisonRow> UpdateComparisons(
    IEnumerable<MergeComparisonRow> comparisons)
  {
    foreach (MergeComparisonRow updateComparison in base.UpdateComparisons(comparisons))
    {
      MergeComparisonRow mergeComparisonRow1 = updateComparison;
      int? targetRecord;
      int num1;
      if (!string.IsNullOrEmpty(updateComparison.RightValue))
      {
        targetRecord = ((PXSelectBase<MergeEntitiesFilter>) this.Filter).Current.TargetRecord;
        if (targetRecord.HasValue && targetRecord.GetValueOrDefault() == 1 || string.IsNullOrEmpty(updateComparison.LeftValue))
        {
          num1 = 2;
          goto label_5;
        }
      }
      num1 = 1;
label_5:
      mergeComparisonRow1.Selection = (ComparisonSelection) num1;
      if (updateComparison.LeftFieldState.IsReadOnly || !updateComparison.LeftFieldState.Enabled || updateComparison.RightFieldState.IsReadOnly || !updateComparison.RightFieldState.Enabled)
      {
        updateComparison.Hidden = new bool?(true);
        MergeComparisonRow mergeComparisonRow2 = updateComparison;
        targetRecord = ((PXSelectBase<MergeEntitiesFilter>) this.Filter).Current.TargetRecord;
        int num2 = !targetRecord.HasValue || targetRecord.GetValueOrDefault() != 1 ? 1 : 2;
        mergeComparisonRow2.Selection = (ComparisonSelection) num2;
      }
      yield return updateComparison;
    }
  }

  /// <summary>
  /// Shows the smart panel on which a user can select fields that will remain in the target record after merge.
  /// </summary>
  /// <returns>
  /// The web dialog result for smart panel of <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesExt`2.Filter" />.
  /// If the smart panel validation failed it returns <see cref="F:PX.Data.WebDialogResult.None" />.
  /// </returns>
  /// <param name="mergeEntityID">The ID of the entity with which the current entity should be merged.</param>
  /// <exception cref="T:PX.Data.PXDialogRequiredException">Is raised to render the smart panel for user interaction.</exception>
  public virtual WebDialogResult AskMerge(object mergeEntityID)
  {
    WebDialogResult webDialogResult;
    if (((PXSelectBase) this.Filter).View.Answer != null)
    {
      webDialogResult = ((PXSelectBase) this.Filter).View.Answer;
    }
    else
    {
      ((PXSelectBase) this.Filter).Cache.Clear();
      ((PXSelectBase<MergeEntitiesFilter>) this.Filter).Current = this.CreateNewFilter(mergeEntityID);
      // ISSUE: method pointer
      webDialogResult = ((PXSelectBase<MergeEntitiesFilter>) this.Filter).AskExt((string) null, new PXView.InitializePanel((object) this, __methodptr(\u003CAskMerge\u003Eb__13_0)));
    }
    return WebDialogResultExtension.IsPositive(webDialogResult) && !this.Filter.TryValidate() ? (WebDialogResult) 0 : webDialogResult;
  }

  public virtual MergeEntitiesFilter CreateNewFilter(object mergeEntityID)
  {
    return ((PXSelectBase<MergeEntitiesFilter>) this.Filter).Insert(new MergeEntitiesFilter()
    {
      MergeEntityID = mergeEntityID.ToString()
    });
  }

  /// <summary>
  /// Processes all comparison rows (see <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesExt`2.ProcessComparisons(System.Collections.Generic.IReadOnlyCollection{PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeComparisonRow})" />)
  /// and merges related documents (see <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesExt`2.MergeRelatedDocuments(`1,`1)" />).
  /// </summary>
  /// <returns>The resulting contexts for the target and duplicate sets of entities.</returns>
  public virtual (TMain Target, TMain Duplicate) ProcessMerge()
  {
    (EntitiesContext LeftContext, EntitiesContext RightContext) tuple = this.ProcessComparisonResult();
    (EntitiesContext target, EntitiesContext duplicate) = this.DefineTargetAndDuplicateContexts(tuple.LeftContext, tuple.RightContext);
    TMain targetEntity = target.MainEntry.Single<TMain>();
    TMain duplicateEntity = duplicate.MainEntry.Single<TMain>();
    this.MergeRelatedDocuments(targetEntity, duplicateEntity);
    this.Base.Caches[typeof (TMain)].Current = (object) targetEntity;
    return (targetEntity, duplicateEntity);
  }

  public override (EntitiesContext LeftContext, EntitiesContext RightContext) ProcessComparisons(
    IReadOnlyCollection<MergeComparisonRow> comparisons)
  {
    EntitiesContext leftEntitiesContext = this.GetLeftEntitiesContext();
    EntitiesContext rightEntitiesContext = this.GetRightEntitiesContext();
    int? targetRecord = ((PXSelectBase<MergeEntitiesFilter>) this.Filter).Current.TargetRecord;
    int num = 0;
    if (targetRecord.GetValueOrDefault() == num & targetRecord.HasValue)
      this.UpdateLeftEntitiesContext(leftEntitiesContext, (IEnumerable<MergeComparisonRow>) comparisons);
    else
      this.UpdateRightEntitiesContext(rightEntitiesContext, (IEnumerable<MergeComparisonRow>) comparisons);
    return (leftEntitiesContext, rightEntitiesContext);
  }

  public override IEnumerable<string> GetFieldsForComparison(
    System.Type itemType,
    PXCache leftCache,
    PXCache rightCache)
  {
    if (!(itemType == typeof (CSAnswers)))
      return base.GetFieldsForComparison(itemType, leftCache, rightCache).Concat<string>(this.GetUdfFieldsForComparison(itemType, leftCache, rightCache));
    return (IEnumerable<string>) new string[1]
    {
      this.GetAttributeField()
    };
  }

  public virtual IEnumerable<string> GetUdfFieldsForComparison(
    System.Type itemType,
    PXCache leftCache,
    PXCache rightCache)
  {
    return leftCache.GetFields_Udf().Union<string>(rightCache.GetFields_Udf());
  }

  public virtual string GetAttributeField() => "Value";

  public override string GetStringValue(
    PXCache cache,
    IBqlTable item,
    string fieldName,
    PXFieldState state)
  {
    if (!fieldName.StartsWith("Attribute"))
      return base.GetStringValue(cache, item, fieldName, state);
    return state.Value?.ToString();
  }

  public override void SetStringValue(
    PXCache cache,
    IBqlTable item,
    string fieldName,
    string stringValue)
  {
    if (cache.GetItemType() == typeof (CSAnswers))
    {
      if (!"value".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
        return;
      cache.SetValue<CSAnswers.value>((object) item, (object) stringValue);
    }
    else if (fieldName.StartsWith("Attribute"))
      cache.SetValueExt((object) item, fieldName, cache.ValueFromString(fieldName, stringValue));
    else
      base.SetStringValue(cache, item, fieldName, stringValue);
  }

  public override MergeComparisonRow CreateComparisonRow(
    string fieldName,
    System.Type itemType,
    ref int order,
    (PXCache Cache, IBqlTable Item, string Value, PXFieldState State) left,
    (PXCache Cache, IBqlTable Item, string Value, PXFieldState State) right)
  {
    MergeComparisonRow comparisonRow = base.CreateComparisonRow(fieldName, itemType, ref order, left, right);
    comparisonRow.EnableNoneSelection = new bool?(true);
    if (left.Cache.GetItemType() == typeof (CSAnswers))
    {
      (PXCache, IBqlTable, string, PXFieldState) valueTuple = left.Item is CSAnswers ? left : right;
      comparisonRow.FieldDisplayName = left.Cache.GetValueExt((object) valueTuple.Item2, "AttributeID_description") as string;
    }
    return comparisonRow;
  }

  public override IEnumerable<(IBqlTable LeftItem, IBqlTable RightItem)> MapEntries(
    EntityEntry leftEntry,
    EntityEntry rightEntry,
    EntitiesContext leftContext,
    EntitiesContext rightContext)
  {
    if (!(leftEntry.EntityType == typeof (CSAnswers)))
      return base.MapEntries(leftEntry, rightEntry, leftContext, rightContext);
    IEnumerable<CSAnswers> source1 = leftEntry.Multiple<CSAnswers>();
    IEnumerable<CSAnswers> source2 = rightEntry.Multiple<CSAnswers>();
    IEnumerable<string> strings = source1.Select<CSAnswers, string>((Func<CSAnswers, string>) (e => e.AttributeID.ToUpper())).Union<string>(source2.Select<CSAnswers, string>((Func<CSAnswers, string>) (e => e.AttributeID.ToUpper()))).Distinct<string>();
    List<(IBqlTable, IBqlTable)> valueTupleList = new List<(IBqlTable, IBqlTable)>();
    foreach (string str in strings)
    {
      string attributeId = str;
      CSAnswers csAnswers1 = source1.Where<CSAnswers>((Func<CSAnswers, bool>) (e => e.AttributeID == attributeId)).FirstOrDefault<CSAnswers>();
      CSAnswers csAnswers2 = source2.Where<CSAnswers>((Func<CSAnswers, bool>) (e => e.AttributeID == attributeId)).FirstOrDefault<CSAnswers>();
      valueTupleList.Add(((IBqlTable) this.CoalesceCSAnswers(csAnswers1, csAnswers2, leftContext.MainEntry), (IBqlTable) this.CoalesceCSAnswers(csAnswers2, csAnswers1, rightContext.MainEntry)));
    }
    return (IEnumerable<(IBqlTable, IBqlTable)>) valueTupleList;
  }

  /// <summary>
  /// Returns an existing answer (<paramref name="leftAnswer" />) or creates one that would be inserted in the database
  /// if <paramref name="leftAnswer" /> is <see langword="null" />.
  /// </summary>
  /// <param name="leftAnswer">The answer to check.</param>
  /// <param name="rightAnswer">The answer whose values are copied if <paramref name="leftAnswer" /> is <see langword="null" />.</param>
  /// <param name="mainEntry">The entry of a single item whose <see cref="P:PX.Data.INotable.NoteID" />
  /// is used for the new answer if <paramref name="leftAnswer" /> is <see langword="null" />.</param>
  /// <returns></returns>
  protected virtual CSAnswers CoalesceCSAnswers(
    CSAnswers leftAnswer,
    CSAnswers rightAnswer,
    EntityEntry mainEntry)
  {
    if (leftAnswer != null)
      return leftAnswer;
    CSAnswers csAnswers = new CSAnswers()
    {
      AttributeID = rightAnswer.AttributeID,
      AttributeCategory = rightAnswer.AttributeCategory,
      IsRequired = rightAnswer.IsRequired,
      RefNoteID = PXNoteAttribute.GetNoteID(mainEntry.Cache, (object) mainEntry.Single<IBqlTable>(), (string) null)
    };
    this.Base.Caches[typeof (CSAnswers)].Insert((object) csAnswers);
    return csAnswers;
  }

  public virtual (EntitiesContext target, EntitiesContext duplicate) DefineTargetAndDuplicateContexts(
    EntitiesContext leftContext,
    EntitiesContext rightContext)
  {
    return ((PXSelectBase<MergeEntitiesFilter>) this.Filter).Current.TargetRecord.GetValueOrDefault() != 1 ? (leftContext, rightContext) : (rightContext, leftContext);
  }

  public override IEnumerable<MergeComparisonRow> FilterComparisons(
    IEnumerable<MergeComparisonRow> comparisons)
  {
    foreach (MergeComparisonRow comparison in comparisons)
    {
      if (comparison.LeftFieldState.Visible || comparison.RightFieldState.Visible)
        yield return comparison;
    }
  }

  protected virtual void _(Events.RowSelected<MergeEntitiesFilter> e)
  {
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<MergeEntitiesFilter>>) e).Cache, (object) e.Row).For<MergeEntitiesFilter.caption>((Action<PXUIFieldAttribute>) (ui => ui.Visible = ((IEnumerable<PXResult<MergeComparisonRow>>) this.VisibleComparisonRows.Select(Array.Empty<object>())).ToList<PXResult<MergeComparisonRow>>().Any<PXResult<MergeComparisonRow>>()));
  }

  protected virtual void _(
    Events.FieldUpdated<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord> e)
  {
    if (object.Equals(e.NewValue, ((Events.FieldUpdatedBase<Events.FieldUpdated<MergeEntitiesFilter, MergeEntitiesFilter.targetRecord>, MergeEntitiesFilter, object>) e).OldValue))
      return;
    this.ReprepareComparisonsInCache();
  }

  protected virtual void _(
    Events.FieldSelecting<MergeEntitiesFilter.caption> e)
  {
    if (e == null || e.Row == null)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<MergeEntitiesFilter.caption>>) e).ReturnValue = (object) PXMessages.Localize("Select the field values that you want to keep in the target record.");
  }
}
