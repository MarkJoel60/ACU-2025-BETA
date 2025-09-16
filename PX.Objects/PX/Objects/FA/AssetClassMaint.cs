// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class AssetClassMaint : PXGraph<AssetClassMaint, FixedAsset>
{
  public PXSelect<FixedAsset, Where<FixedAsset.recordType, Equal<FARecordType.classType>>> AssetClass;
  public PXSelect<FixedAsset, Where<FixedAsset.assetCD, Equal<Current<FixedAsset.assetCD>>>> CurrentAssetClass;
  public PXSelectJoin<FABookSettings, LeftJoin<FABook, On<FABook.bookID, Equal<FABookSettings.bookID>>>, Where<FABookSettings.assetID, Equal<Current<FixedAsset.assetID>>>> DepreciationSettings;
  public PXSetup<PX.Objects.FA.FASetup> FASetup;

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [PXUIField]
  [FARecordType.List]
  protected virtual void FixedAsset_RecordType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField]
  public virtual void FixedAsset_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<FixedAsset.assetCD, Where<FixedAsset.recordType, Equal<FARecordType.classType>>>), new Type[] {typeof (FixedAsset.assetCD), typeof (FixedAsset.description), typeof (FixedAsset.depreciable), typeof (FixedAsset.underConstruction), typeof (FixedAsset.assetTypeID), typeof (FixedAsset.usefulLife)}, Filterable = true)]
  protected virtual void FixedAsset_AssetCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXParent(typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FixedAsset.parentAssetID>>>>), UseCurrent = true, LeaveChildren = true)]
  [PXSelector(typeof (Search<FixedAsset.assetID, Where<FixedAsset.assetID, NotEqual<Current<FixedAsset.assetID>>, And<Where<FixedAsset.recordType, Equal<Current<FixedAsset.recordType>>, And<Current<FixedAsset.recordType>, NotEqual<FARecordType.elementType>, Or<Current<FixedAsset.recordType>, Equal<FARecordType.elementType>, And<FixedAsset.recordType, Equal<FARecordType.assetType>>>>>>>>), new Type[] {typeof (FixedAsset.assetCD), typeof (FixedAsset.description), typeof (FixedAsset.assetTypeID), typeof (FixedAsset.usefulLife)}, SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField]
  protected virtual void FixedAsset_ParentAssetID_CacheAttached(PXCache sender)
  {
  }

  [PXString(1, IsFixed = true)]
  protected virtual void FixedAsset_Status_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Fixed Asset Sub. from")]
  protected virtual void FixedAsset_FASubMask_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Accumulated Depreciation Sub. from")]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void FixedAsset_AccumDeprSubMask_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Depreciation Expense Sub. from")]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void FixedAsset_DeprExpenceSubMask_CacheAttached(PXCache sender)
  {
  }

  [SubAccountMask(DisplayName = "Combine Proceeds Sub. from")]
  protected virtual void FixedAsset_ProceedsSubMask_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Gain/Loss Sub. from")]
  protected virtual void FixedAsset_GainLossSubMask_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<FixedAsset.assetTypeID, IsNotNull>, Selector<FixedAsset.assetTypeID, FAType.depreciable>>, True>))]
  protected virtual void FixedAsset_Depreciable_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.underConstruction> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.accumulatedDepreciationAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.accumulatedDepreciationSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.depreciatedExpenseAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.depreciatedExpenseSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookSettings.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookSettings.depreciationMethodID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookSettings.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookSettings.averagingConvention> e)
  {
  }

  public AssetClassMaint()
  {
    PXCache cache = ((PXSelectBase) this.AssetClass).Cache;
    PX.Objects.FA.FASetup current = ((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current;
    PXUIFieldAttribute.SetRequired<FixedAsset.usefulLife>(cache, true);
  }

  [PXCustomizeBaseAttribute]
  protected virtual void _(PX.Data.Events.CacheAttached<FixedAsset.baseCuryID> e)
  {
  }

  protected virtual void FixedAsset_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null || row.AssetCD == null)
      return;
    foreach (FABookSettings faBookSettings in GraphHelper.RowCast<FABook>((IEnumerable) PXSelectBase<FABook, PXSelect<FABook>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<FABook, FABookSettings>((Func<FABook, FABookSettings>) (book => new FABookSettings()
    {
      BookID = book.BookID
    })))
      ((PXSelectBase<FABookSettings>) this.DepreciationSettings).Insert(faBookSettings);
    ((PXSelectBase) this.DepreciationSettings).Cache.IsDirty = false;
  }

  protected virtual void FixedAsset_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    PXCache pxCache = sender;
    FixedAsset fixedAsset = row;
    bool? nullable1 = row.UseFASubMask;
    int num = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FixedAsset.accumDeprSubMask>(pxCache, (object) fixedAsset, num != 0);
    bool? nullable2 = new bool?(GraphHelper.RowCast<FABookSettings>((IEnumerable) ((PXSelectBase<FABookSettings>) this.DepreciationSettings).Select(Array.Empty<object>())).Any<FABookSettings>((Func<FABookSettings, bool>) (books => books.UpdateGL.GetValueOrDefault())));
    PXUIFieldAttribute.SetEnabled<FixedAsset.holdEntry>(sender, e.Row, !nullable2.GetValueOrDefault());
    nullable1 = ((FixedAsset) e.Row).HoldEntry;
    bool flag = false;
    if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue) || !nullable2.GetValueOrDefault())
      return;
    ((FixedAsset) e.Row).HoldEntry = new bool?(true);
    if (sender.GetStatus(e.Row) != null)
      return;
    sender.SetStatus(e.Row, (PXEntryStatus) 1);
    sender.IsDirty = true;
  }

  protected virtual void FixedAsset_RecordType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "C";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FixedAsset_Depreciable_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AssetMaint.UpdateBalances<FABookSettings.depreciate, FixedAsset.depreciable>(sender, e);
  }

  protected virtual void FixedAsset_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    bool? nullable = row.Depreciable;
    if (nullable.GetValueOrDefault() && row.UsefulLife.GetValueOrDefault() == 0M)
      sender.RaiseExceptionHandling<FixedAsset.usefulLife>((object) row, (object) row.UsefulLife, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", (PXErrorLevel) 4, new object[1]
      {
        (object) 0M
      }));
    nullable = row.Active;
    if (!nullable.GetValueOrDefault())
    {
      if (PXResult<FixedAsset, FADetails>.op_Implicit((PXResult<FixedAsset, FADetails>) ((PXSelectBase) new PXSelectJoin<FixedAsset, InnerJoin<FADetails, On<FixedAsset.assetID, Equal<FADetails.assetID>>>, Where<FixedAsset.classID, Equal<Required<FixedAsset.classID>>, And<Where<FADetails.status, Equal<FixedAssetStatus.active>>>>>((PXGraph) this)).View.SelectSingle(new object[1]
      {
        (object) row.AssetID
      })) != null)
        sender.RaiseExceptionHandling<FixedAsset.active>((object) row, (object) row.Active, (Exception) new PXSetPropertyException("The fixed asset class cannot be deactivated. There are some fixed assets with the Active status associated with this class."));
    }
    nullable = row.UnderConstruction;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.Depreciable;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    sender.RaiseExceptionHandling<FixedAsset.underConstruction>((object) row, (object) row.UnderConstruction, (Exception) new PXSetPropertyException("The Under Construction check box can be selected only for depreciable asset classes."));
  }

  protected virtual void FixedAsset_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    if (PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.classID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    })) != null)
      throw new PXSetPropertyException("You cannot delete Fixed Asset Class because this Class used in Fixed Assets.");
    if (PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.parentAssetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    })) != null)
      throw new PXSetPropertyException("You cannot delete Fixed Asset Class because this Class is parent for another class.");
  }

  protected virtual void FixedAsset_UsefulLife_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    foreach (FABookSettings faBookSettings in GraphHelper.RowCast<FABookSettings>((IEnumerable) PXSelectBase<FABookSettings, PXSelect<FABookSettings, Where<FABookSettings.assetID, Equal<Required<FABookSettings.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    })).Select<FABookSettings, FABookSettings>((Func<FABookSettings, FABookSettings>) (set => (FABookSettings) ((PXSelectBase) this.DepreciationSettings).Cache.CreateCopy((object) set))))
    {
      if ((PXSelectorAttribute.Select<FABookSettings.depreciationMethodID>(((PXSelectBase) this.DepreciationSettings).Cache, (object) faBookSettings) is FADepreciationMethod depreciationMethod ? depreciationMethod.DepreciationMethod : (string) null) != "PC" && depreciationMethod?.DepreciationMethod != "ZL" && depreciationMethod?.DepreciationMethod != "LE")
      {
        faBookSettings.UsefulLife = row.UsefulLife;
        ((PXSelectBase<FABookSettings>) this.DepreciationSettings).Update(faBookSettings);
      }
    }
  }

  protected virtual void FixedAsset_FASubMask_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null || !row.UseFASubMask.GetValueOrDefault())
      return;
    row.AccumDeprSubMask = row.FASubMask;
  }

  protected virtual void FixedAsset_UseFASubMask_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.FixedAsset_FASubMask_FieldUpdated(sender, e);
  }

  protected virtual void FixedAsset_ParentAssetID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    PXSelectBase<FixedAsset> pxSelectBase = (PXSelectBase<FixedAsset>) new PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.parentAssetID>>>>((PXGraph) this);
    int? nullable1 = (int?) e.NewValue;
    string str1 = (string) null;
    FixedAsset fixedAsset;
    for (; nullable1.HasValue; nullable1 = fixedAsset.ParentAssetID)
    {
      fixedAsset = PXResultset<FixedAsset>.op_Implicit(pxSelectBase.Select(new object[1]
      {
        (object) nullable1
      }));
      str1 = str1 ?? fixedAsset.AssetCD;
      int? parentAssetId = fixedAsset.ParentAssetID;
      int? nullable2 = row.AssetID;
      if (parentAssetId.GetValueOrDefault() == nullable2.GetValueOrDefault() & parentAssetId.HasValue == nullable2.HasValue)
      {
        PXFieldVerifyingEventArgs verifyingEventArgs = e;
        nullable2 = row.ParentAssetID;
        string str2;
        if (!nullable2.HasValue)
          str2 = (string) null;
        else
          str2 = PXResultset<FixedAsset>.op_Implicit(pxSelectBase.Select(new object[1]
          {
            (object) row.ParentAssetID
          })).AssetCD;
        verifyingEventArgs.NewValue = (object) str2;
        throw new PXSetPropertyException("You cannot reference the child asset '{0}' as the parent asset.", new object[1]
        {
          (object) str1
        });
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FixedAsset.underConstruction> e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    bool? underConstruction = row.UnderConstruction;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset.underConstruction>, object, object>) e).NewValue;
    if (underConstruction.GetValueOrDefault() == newValue.GetValueOrDefault() & underConstruction.HasValue == newValue.HasValue)
      return;
    if (PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.classID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    })) != null)
      throw new PXSetPropertyException("The state of the Under Construction check box cannot be changed because there are assets associated with the {0} class.");
  }

  protected virtual void FABookSettings_DepreciationMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<FABookSettings.averagingConvention>(e.Row);
    sender.SetDefaultExt<FABookSettings.percentPerYear>(e.Row);
  }

  protected virtual void FABookSettings_DepreciationMethodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FABookSettings row = (FABookSettings) e.Row;
    if (row == null || !row.BookID.HasValue)
      return;
    IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(row.BookID);
    FADepreciationMethod depreciationMethod = PXSelectorAttribute.Select<FABookSettings.depreciationMethodID>(((PXSelectBase) this.DepreciationSettings).Cache, (object) row, e.NewValue) as FADepreciationMethod;
    if (faBookYearSetup == null)
    {
      string str = PXMessages.LocalizeFormat("The book calendar does not exist for the {0} book. Use the Book Calendar Setup (FA206000) form to set up calendars for all needed books.", new object[1]
      {
        (object) (PXSelectorAttribute.Select<FABookSettings.bookID>(sender, (object) row) as FABook).BookCode
      });
      sender.RaiseExceptionHandling<FABookSettings.depreciationMethodID>((object) row, (object) depreciationMethod.DepreciationMethod, (Exception) new PXSetPropertyException(str));
    }
    else if ((faBookYearSetup.PeriodType == "WK" || faBookYearSetup.PeriodType == "BW" || faBookYearSetup.PeriodType == "FW") && depreciationMethod != null && depreciationMethod.IsNewZealandMethod)
    {
      e.NewValue = (object) depreciationMethod?.MethodCD;
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("The {0} calculation method cannot be selected for a book that uses a week-based calendar.", new object[1]
      {
        (object) PXStringListAttribute.GetLocalizedLabel<FADepreciationMethod.depreciationMethod>(((PXGraph) this).Caches[typeof (FADepreciationMethod)], (object) depreciationMethod)
      }));
    }
  }

  protected void FABookSettings_AveragingConvention_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null && !((IEnumerable<object>) new FAAveragingConvention.ListAttribute().GetAllowedValues(sender)).Contains<object>(e.NewValue))
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "[averagingConvention]"
      });
  }

  protected virtual void FABookSettings_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FABookSettings row = (FABookSettings) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<FABookSettings.bookID>(sender, (object) row, !row.BookID.HasValue);
    IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(row.BookID);
    FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Current<FABookSettings.depreciationMethodID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    List<KeyValuePair<object, Dictionary<object, string[]>>> keyValuePairList = new List<KeyValuePair<object, Dictionary<object, string[]>>>();
    if (depreciationMethod != null)
      keyValuePairList.Add(depreciationMethod.IsTableMethod.GetValueOrDefault() ? new KeyValuePair<object, Dictionary<object, string[]>>((object) depreciationMethod.RecordType, FAAveragingConvention.RecordTypeDisabledValues) : new KeyValuePair<object, Dictionary<object, string[]>>((object) depreciationMethod.DepreciationMethod, FAAveragingConvention.DeprMethodDisabledValues));
    if (faBookYearSetup != null)
      keyValuePairList.Add(new KeyValuePair<object, Dictionary<object, string[]>>((object) faBookYearSetup.IsFixedLengthPeriod, FAAveragingConvention.FixedLengthPeriodDisabledValues));
    FAAveragingConvention.SetAveragingConventionsList<FADepreciationMethod.averagingConvention>(sender, (object) row, keyValuePairList.ToArray());
  }

  protected virtual void FABookSettings_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.AssetClass).Current;
    FABookSettings row = (FABookSettings) e.Row;
    if (row == null || current == null)
      return;
    bool? nullable = row.Depreciate;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current.UnderConstruction;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    if (row.AveragingConvention == null || string.IsNullOrEmpty(row.AveragingConvention.Trim()))
      sender.RaiseExceptionHandling<FABookSettings.averagingConvention>((object) row, (object) row.AveragingConvention, (Exception) new PXSetPropertyException("Value can not be empty."));
    if (row.DepreciationMethodID.HasValue)
      return;
    sender.RaiseExceptionHandling<FABookSettings.depreciationMethodID>((object) row, (object) row.DepreciationMethodID, (Exception) new PXSetPropertyException("Value can not be empty."));
  }

  public virtual void Persist()
  {
    foreach (FABookSettings faBookSettings in ((PXSelectBase) this.DepreciationSettings).Cache.Inserted.Cast<FABookSettings>().Concat<FABookSettings>(((PXSelectBase) this.DepreciationSettings).Cache.Updated.Cast<FABookSettings>()))
    {
      FABook book = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Required<FABook.bookID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) faBookSettings.BookID
      }));
      IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(book);
      bool? nullable;
      if (faBookYearSetup == null || !faBookYearSetup.IsFixedLengthPeriod)
      {
        if (PXResultset<FABookPeriodSetup>.op_Implicit(PXSelectBase<FABookPeriodSetup, PXSelect<FABookPeriodSetup, Where<FABookPeriodSetup.bookID, Equal<Required<FABookPeriodSetup.bookID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) faBookSettings.BookID
        })) == null)
        {
          nullable = faBookSettings.UpdateGL;
          bool flag = false;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue && book != null)
            ((PXSelectBase) this.DepreciationSettings).Cache.RaiseExceptionHandling<FABookSettings.bookID>((object) faBookSettings, (object) book.BookCode, (Exception) new PXSetPropertyException<FABookSettings.bookID>("Book Calendar cannot be found in the system."));
        }
      }
      FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FABookSettings.depreciationMethodID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) faBookSettings.DepreciationMethodID
      }));
      if (depreciationMethod != null)
      {
        nullable = depreciationMethod.IsTableMethod;
        if (nullable.GetValueOrDefault())
        {
          Decimal? usefulLife1 = depreciationMethod.UsefulLife;
          Decimal? usefulLife2 = faBookSettings.UsefulLife;
          if (!(usefulLife1.GetValueOrDefault() == usefulLife2.GetValueOrDefault() & usefulLife1.HasValue == usefulLife2.HasValue))
            ((PXSelectBase) this.DepreciationSettings).Cache.RaiseExceptionHandling<FABookSettings.usefulLife>((object) faBookSettings, (object) faBookSettings.UsefulLife, (Exception) new PXSetPropertyException<FABookSettings.usefulLife>("Useful Life does not match the recovery period specified for selected Depreciation Method."));
        }
      }
      if (depreciationMethod != null)
      {
        nullable = depreciationMethod.IsTableMethod;
        if (nullable.GetValueOrDefault() && depreciationMethod.AveragingConvention != faBookSettings.AveragingConvention)
          ((PXSelectBase) this.DepreciationSettings).Cache.RaiseExceptionHandling<FABookSettings.averagingConvention>((object) faBookSettings, (object) faBookSettings.AveragingConvention, (Exception) new PXSetPropertyException<FABookSettings.averagingConvention>("The averaging convention does not match the averaging convention specified for the selected depreciation method."));
      }
    }
    ((PXGraph) this).Persist();
  }
}
