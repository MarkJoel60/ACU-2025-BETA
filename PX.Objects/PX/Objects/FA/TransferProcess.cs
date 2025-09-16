// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.TransferProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.FA;

public class TransferProcess : PXGraph<
#nullable disable
TransferProcess>, IAssetTransferInformationCheckable
{
  public PXCancel<TransferProcess.TransferFilter> Cancel;
  public PXFilter<TransferProcess.TransferFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (FixedAsset.assetCD))]
  public PXFilteredProcessingJoin<FixedAsset, TransferProcess.TransferFilter, LeftJoin<FADetailsTransfer, On<FixedAsset.assetID, Equal<FADetailsTransfer.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<PX.Objects.GL.Account, On<FixedAsset.fAAccountID, Equal<PX.Objects.GL.Account.accountID>>, LeftJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FADetailsTransfer.assetID>, And<FALocationHistory.revisionID, Equal<FADetailsTransfer.locationRevID>>>>>>>> Assets;
  public PXSelect<PX.Objects.CR.BAccount> Baccount;
  public PXSelect<PX.Objects.AP.Vendor> Vendor;
  public PXSelect<EPEmployee> Employee;
  public PXSelect<FixedAsset> AssetSelect;
  public PXSelect<FADetails> Details;
  public PXSelect<FABookBalance> BookBalance;
  public PXSelect<FARegister> Register;
  public PXSelect<FATran> FATransactions;
  public PXSelect<FALocationHistory> Lochist;
  public PXSetup<FASetup> fasetup;

  public TransferProcess()
  {
    FASetup current = ((PXSelectBase<FASetup>) this.fasetup).Current;
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault())
      throw new PXSetupNotEnteredException<FASetup>("This operation is not available in initialization mode. To exit the initialization mode, select the '{1}' checkbox on the '{0}' screen.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FASetup.updateGL>(((PXSelectBase) this.fasetup).Cache)
      });
    PXUIFieldAttribute.SetDisplayName<PX.Objects.GL.Account.accountClassID>(((PXGraph) this).Caches[typeof (PX.Objects.GL.Account)], "Fixed Assets Account Class");
    if (((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseTransfer.GetValueOrDefault())
      return;
    ((PXProcessing<FixedAsset>) this.Assets).SetProcessCaption("Prepare");
    ((PXProcessing<FixedAsset>) this.Assets).SetProcessAllCaption("Prepare All");
  }

  public virtual void Clear(PXClearOption option)
  {
    if (((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).ContainsKey(typeof (FADetails)))
      ((PXGraph) this).Caches[typeof (FADetails)].ClearQueryCache();
    ((PXGraph) this).Clear(option);
  }

  protected virtual void FALocationHistory_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    AssetMaint.LiveUpdateMaskedSubs((PXGraph) this, ((PXSelectBase) this.Assets).Cache, (FALocationHistory) e.Row);
  }

  public static void Transfer(TransferProcess.TransferFilter filter, List<FixedAsset> list)
  {
    PXGraph.CreateInstance<TransferProcess>().DoTransfer(filter, list);
  }

  protected virtual void DoTransfer(TransferProcess.TransferFilter filter, List<FixedAsset> list)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TransferProcess.\u003C\u003Ec__DisplayClass18_0 cDisplayClass180 = new TransferProcess.\u003C\u003Ec__DisplayClass18_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.created = new DocumentList<FARegister>((PXGraph) this);
    foreach (FixedAsset asset in list)
    {
      try
      {
        ((PXGraph) this).Caches[typeof (FixedAsset)].Current = (object) asset;
        FADetails faDetails1 = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) asset
        }, Array.Empty<object>()));
        FALocationHistory faLocationHistory1 = PXResultset<FALocationHistory>.op_Implicit(PXSelectBase<FALocationHistory, PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<FADetails.assetID>>, And<FALocationHistory.revisionID, Equal<Current<FADetails.locationRevID>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) faDetails1
        }, Array.Empty<object>()));
        int? nullable1 = filter.ClassTo;
        int? nullable2 = nullable1 ?? asset.ClassID;
        FAClass faClass = PXResultset<FAClass>.op_Implicit(PXSelectBase<FAClass, PXViewOf<FAClass>.BasedOn<SelectFromBase<FAClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FAClass.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) nullable2
        }));
        nullable1 = filter.BranchTo;
        int? nullable3 = nullable1 ?? faLocationHistory1.LocationID;
        string str = string.IsNullOrEmpty(filter.DepartmentTo) ? faLocationHistory1.Department : filter.DepartmentTo;
        nullable1 = faLocationHistory1.LocationID;
        int? nullable4 = nullable3;
        if (nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue && !(faLocationHistory1.Department != str))
        {
          nullable4 = asset.ClassID;
          nullable1 = nullable2;
          if (nullable4.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable4.HasValue == nullable1.HasValue)
            continue;
        }
        FADetails copy1 = (FADetails) ((PXSelectBase) this.Details).Cache.CreateCopy((object) faDetails1);
        FALocationHistory copy2 = (FALocationHistory) ((PXSelectBase) this.Lochist).Cache.CreateCopy((object) faLocationHistory1);
        FALocationHistory faLocationHistory2 = copy2;
        FADetails faDetails2 = copy1;
        nullable4 = faDetails2.LocationRevID;
        nullable1 = nullable4.HasValue ? new int?(nullable4.GetValueOrDefault() + 1) : new int?();
        faDetails2.LocationRevID = nullable1;
        int? nullable5 = nullable1;
        faLocationHistory2.RevisionID = nullable5;
        copy2.ClassID = nullable2;
        nullable1 = copy2.LocationID;
        nullable4 = nullable3;
        if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
        {
          FALocationHistory faLocationHistory3 = copy2;
          nullable4 = new int?();
          int? nullable6 = nullable4;
          faLocationHistory3.BuildingID = nullable6;
        }
        copy2.LocationID = nullable3;
        copy2.Department = str;
        copy2.PeriodID = filter.PeriodID;
        copy2.TransactionDate = filter.TransferDate;
        copy2.Reason = filter.Reason;
        bool? underConstruction = faClass.UnderConstruction;
        if (underConstruction.GetValueOrDefault() && !faDetails1.DepreciateFromDate.HasValue)
          throw new PXException("'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<FADetails.depreciateFromDate>(((PXSelectBase) this.Details).Cache)
          });
        underConstruction = faClass.UnderConstruction;
        if (!underConstruction.GetValueOrDefault())
        {
          underConstruction = asset.UnderConstruction;
          if (!underConstruction.GetValueOrDefault())
            goto label_12;
        }
        ((PXGraph) this).GetExtension<TransferProcess.TransferProcessFixedAssetChecksExtension>().CheckDepreciationPeriodNotEarlierThanTheLastTransactionPeriod(asset, faDetails1.DepreciateFromDate);
label_12:
        // ISSUE: reference to a compiler-generated field
        TransactionEntry.SegregateRegister((PXGraph) this, nullable3.Value, "T", (string) null, filter.TransferDate, "", cDisplayClass180.created);
        ((PXSelectBase<FADetails>) this.Details).Update(copy1);
        FALocationHistory location = ((PXSelectBase<FALocationHistory>) this.Lochist).Insert(copy2);
        nullable4 = asset.ClassID;
        nullable1 = nullable2;
        if (!(nullable4.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable4.HasValue == nullable1.HasValue))
        {
          underConstruction = asset.UnderConstruction;
          int num = underConstruction.GetValueOrDefault() ? 1 : 0;
          asset.ClassID = nullable2;
          asset.UnderConstruction = faClass.UnderConstruction;
          if (num != 0)
          {
            underConstruction = asset.UnderConstruction;
            if (!underConstruction.GetValueOrDefault())
            {
              asset.AccumulatedDepreciationAccountID = faClass.AccumulatedDepreciationAccountID;
              asset.AccumulatedDepreciationSubID = faClass.AccumulatedDepreciationSubID;
              asset.DepreciatedExpenseAccountID = faClass.DepreciatedExpenseAccountID;
              asset.DepreciatedExpenseSubID = faClass.DepreciatedExpenseSubID;
              this.UpdateBookBalances(asset);
            }
          }
          ((PXSelectBase) this.AssetSelect).Cache.Update((object) asset);
        }
        FARegister current = ((PXSelectBase<FARegister>) this.Register).Current;
        AssetProcess.TransferAsset((PXGraph) this, asset, location, ref current);
      }
      catch (Exception ex)
      {
        PXProcessing.SetError(list.IndexOf(asset), ex);
        throw;
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (((PXSelectBase<FARegister>) this.Register).Current != null && cDisplayClass180.created.Find((object) ((PXSelectBase<FARegister>) this.Register).Current) == null)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass180.created.Add(((PXSelectBase<FARegister>) this.Register).Current);
    }
    ((PXGraph) this).Actions.PressSave();
    if (((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseTransfer.GetValueOrDefault())
    {
      ((PXGraph) this).SelectTimeStamp();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass180, __methodptr(\u003CDoTransfer\u003Eb__0)));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass180.created.Count > 0)
      {
        AssetTranRelease instance = PXGraph.CreateInstance<AssetTranRelease>();
        AssetTranRelease.ReleaseFilter copy = (AssetTranRelease.ReleaseFilter) ((PXSelectBase) instance.Filter).Cache.CreateCopy((object) ((PXSelectBase<AssetTranRelease.ReleaseFilter>) instance.Filter).Current);
        copy.Origin = "T";
        ((PXSelectBase<AssetTranRelease.ReleaseFilter>) instance.Filter).Update(copy);
        ((PXGraph) instance).SelectTimeStamp();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        // ISSUE: reference to a compiler-generated field
        for (int index = 0; index < cDisplayClass180.created.Count; ++index)
        {
          // ISSUE: reference to a compiler-generated field
          FARegister faRegister = cDisplayClass180.created[index];
          faRegister.Selected = new bool?(true);
          ((PXSelectBase<FARegister>) instance.FADocumentList).Update(faRegister);
          ((PXSelectBase) instance.FADocumentList).Cache.SetStatus((object) faRegister, (PXEntryStatus) 1);
          ((PXSelectBase) instance.FADocumentList).Cache.IsDirty = false;
          dictionary["FARegister.RefNbr" + index.ToString()] = faRegister.RefNbr;
        }
        PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectReadonly<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) filter.OrganizationID
        }));
        dictionary["OrgBAccountID"] = organization?.OrganizationCD;
        dictionary["PeriodFrom"] = FinPeriodIDFormattingAttribute.FormatForDisplay(filter.PeriodID);
        dictionary["PeriodTo"] = FinPeriodIDFormattingAttribute.FormatForDisplay(filter.PeriodID);
        dictionary["Mode"] = "U";
        PXReportRequiredException requiredException = new PXReportRequiredException(dictionary, "FA642000", "Preview", (CurrentLocalization) null);
        throw new PXRedirectWithReportException((PXGraph) instance, requiredException, "Release FA Transaction");
      }
    }
  }

  private void UpdateBookBalances(FixedAsset asset)
  {
    foreach (PXResult<FABookSettings, FABookBalance> pxResult in PXSelectBase<FABookSettings, PXViewOf<FABookSettings>.BasedOn<SelectFromBase<FABookSettings, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FABookBalance>.On<BqlOperand<FABookBalance.bookID, IBqlInt>.IsEqual<FABookSettings.bookID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookSettings.assetID, Equal<P.AsInt>>>>>.And<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) asset.ClassID,
      (object) asset.AssetID
    }))
    {
      FABookSettings faBookSettings = PXResult<FABookSettings, FABookBalance>.op_Implicit(pxResult);
      FABookBalance balance = PXResult<FABookSettings, FABookBalance>.op_Implicit(pxResult);
      if (balance != null)
      {
        int? nullable = balance.AssetID;
        if (nullable.HasValue)
        {
          nullable = balance.DepreciationMethodID;
          if (!nullable.HasValue && balance.AveragingConvention == null)
          {
            balance.DepreciationMethodID = AssetMaint.GetDeprMethodOfAssetType((PXGraph) this, faBookSettings.DepreciationMethodID, balance);
            balance.AveragingConvention = faBookSettings.AveragingConvention;
            ((PXSelectBase<FABookBalance>) this.BookBalance).Update(balance);
          }
        }
      }
    }
  }

  public virtual IEnumerable assets()
  {
    TransferProcess.TransferFilter current = ((PXSelectBase<TransferProcess.TransferFilter>) this.Filter).Current;
    PXSelectBase<FixedAsset> pxSelectBase = (PXSelectBase<FixedAsset>) new PXSelectJoin<FixedAsset, InnerJoin<FADetailsTransfer, On<FixedAsset.assetID, Equal<FADetailsTransfer.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<PX.Objects.GL.Account, On<FixedAsset.fAAccountID, Equal<PX.Objects.GL.Account.accountID>>, LeftJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FADetailsTransfer.assetID>, And<FALocationHistory.revisionID, Equal<FADetailsTransfer.locationRevID>>>>>>>, Where<FADetailsTransfer.status, NotEqual<FixedAssetStatus.hold>, And<FADetailsTransfer.status, NotEqual<FixedAssetStatus.disposed>, And<FADetailsTransfer.status, NotEqual<FixedAssetStatus.reversed>>>>>((PXGraph) this);
    if (current.PeriodID != null)
      pxSelectBase.WhereAnd<Where<FADetailsTransfer.transferPeriodID, IsNull, Or<FADetailsTransfer.transferPeriodID, LessEqual<Current<TransferProcess.TransferFilter.periodID>>>>>();
    int? nullable;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
    {
      nullable = current.OrganizationID;
      if (!nullable.HasValue)
        goto label_5;
    }
    pxSelectBase.WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Current<TransferProcess.TransferFilter.organizationID>>>>();
label_5:
    nullable = current.BranchFrom;
    if (nullable.HasValue)
      pxSelectBase.WhereAnd<Where<FixedAsset.branchID, Equal<Current<TransferProcess.TransferFilter.branchFrom>>>>();
    if (current.DepartmentFrom != null)
      pxSelectBase.WhereAnd<Where<FALocationHistory.department, Equal<Current<TransferProcess.TransferFilter.departmentFrom>>>>();
    nullable = current.ClassFrom;
    if (nullable.HasValue)
      pxSelectBase.WhereAnd<Where<FixedAsset.classID, Equal<Current<TransferProcess.TransferFilter.classFrom>>>>();
    int startRow = PXView.StartRow;
    int num = 0;
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    foreach (PXFilterRow filter in PXView.Filters)
    {
      if (filter.DataField.ToLower() == "status")
        filter.DataField = "FADetailsTransfer__Status";
      pxFilterRowList.Add(filter);
    }
    List<object> objectList = ((PXSelectBase) pxSelectBase).View.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, pxFilterRowList.ToArray(), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  protected virtual void FixedAsset_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    int? assetId = row.AssetID;
    int num = 0;
    if (assetId.GetValueOrDefault() < num & assetId.HasValue)
      return;
    FADetailsTransfer det = PXResultset<FADetailsTransfer>.op_Implicit(PXSelectBase<FADetailsTransfer, PXSelect<FADetailsTransfer, Where<FADetailsTransfer.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<FixedAsset.selected>(sender, (object) row)))
    {
      PXUIFieldAttribute.SetEnabled<FixedAsset.selected>(sender, (object) row, true);
      sender.RaiseExceptionHandling<FixedAsset.selected>((object) row, (object) null, (Exception) null);
      try
      {
        ((PXGraph) this).GetExtension<TransferProcess.TransferProcessFixedAssetChecksExtension>().CheckIfAssetCanBeTransferred(((PXSelectBase<TransferProcess.TransferFilter>) this.Filter).Current, row, det);
      }
      catch (PXException ex)
      {
        sender.SetValue<FixedAsset.selected>((object) row, (object) false);
        PXUIFieldAttribute.SetEnabled<FixedAsset.selected>(sender, (object) row, false);
        sender.RaiseExceptionHandling<FixedAsset.selected>((object) row, (object) null, (Exception) new PXSetPropertyException(ex.MessageNoNumber, (PXErrorLevel) 3));
      }
    }
    if (!string.IsNullOrEmpty(det.TransferPeriodID) || row.UnderConstruction.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<FixedAsset.selected>(sender, (object) row, false);
    sender.RaiseExceptionHandling<FADetailsTransfer.transferPeriodID>((object) row, (object) null, (Exception) new PXSetPropertyException("Next Period after Last Depr. Period isn't generated."));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TransferProcess.TransferFilter.organizationID> e)
  {
    if (!(e.Row is TransferProcess.TransferFilter row))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<TransferProcess.TransferFilter.organizationID>>) e).Cache.SetDefaultExt<TransferProcess.TransferFilter.branchFrom>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<TransferProcess.TransferFilter.organizationID>>) e).Cache.SetDefaultExt<TransferProcess.TransferFilter.branchTo>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<TransferProcess.TransferFilter.branchFrom> e)
  {
    if (!(e.Row is TransferProcess.TransferFilter row))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<TransferProcess.TransferFilter.branchFrom>, object, object>) e).NewValue = (object) this.GetDefaultBrunchID(row.OrganizationID);
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<TransferProcess.TransferFilter.branchFrom>, object, object>) e).NewValue != null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<TransferProcess.TransferFilter.branchFrom>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<TransferProcess.TransferFilter.branchTo> e)
  {
    if (!(e.Row is TransferProcess.TransferFilter row))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<TransferProcess.TransferFilter.branchTo>, object, object>) e).NewValue = (object) this.GetDefaultBrunchID(row.OrganizationID);
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<TransferProcess.TransferFilter.branchTo>, object, object>) e).NewValue != null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<TransferProcess.TransferFilter.branchTo>>) e).Cancel = true;
  }

  private int? GetDefaultBrunchID(int? OrganizationID)
  {
    int? defaultBrunchId = new int?();
    int[] childBranchIds = PXAccess.GetChildBranchIDs(OrganizationID, false);
    int? branchId = ((PXGraph) this).Accessinfo.BranchID;
    if (childBranchIds.Length == 1)
      defaultBrunchId = new int?(childBranchIds[0]);
    else if (childBranchIds.Length > 1)
    {
      int? nullable = OrganizationID;
      int? parentOrganizationId = PXAccess.GetParentOrganizationID(branchId);
      if (nullable.GetValueOrDefault() == parentOrganizationId.GetValueOrDefault() & nullable.HasValue == parentOrganizationId.HasValue)
        defaultBrunchId = branchId;
    }
    return defaultBrunchId;
  }

  public void TransferFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TransferProcess.\u003C\u003Ec__DisplayClass26_0 cDisplayClass260 = new TransferProcess.\u003C\u003Ec__DisplayClass26_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass260.filter = (TransferProcess.TransferFilter) e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass260.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<FixedAsset>) this.Assets).SetProcessDelegate(new PXProcessingBase<FixedAsset>.ProcessListDelegate((object) cDisplayClass260, __methodptr(\u003CTransferFilter_RowSelected\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    bool flag = !PXUIFieldAttribute.GetErrors(sender, (object) cDisplayClass260.filter, new PXErrorLevel[2]
    {
      (PXErrorLevel) 4,
      (PXErrorLevel) 5
    }).Any<KeyValuePair<string, string>>() && !string.IsNullOrEmpty(cDisplayClass260.filter.PeriodID) && (cDisplayClass260.filter.BranchTo.HasValue || cDisplayClass260.filter.DepartmentTo != null);
    ((PXProcessing<FixedAsset>) this.Assets).SetProcessEnabled(flag);
    ((PXProcessing<FixedAsset>) this.Assets).SetProcessAllEnabled(flag);
  }

  public void TransferFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null || sender.ObjectsEqual<TransferProcess.TransferFilter.branchFrom, TransferProcess.TransferFilter.departmentFrom, TransferProcess.TransferFilter.classFrom>(e.Row, e.OldRow))
      return;
    foreach (object obj in ((PXSelectBase) this.Assets).Cache.Cached.Cast<FixedAsset>().Where<FixedAsset>((Func<FixedAsset, bool>) (_ => _.Selected.GetValueOrDefault())))
      ((PXSelectBase) this.Assets).Cache.SetValue<FixedAsset.selected>(obj, (object) false);
    ((PXCache) GraphHelper.Caches<FixedAsset>((PXGraph) this)).Clear();
    ((PXSelectBase) this.Assets).Cache.Clear();
  }

  public virtual void CheckAssetTransferInformation(
    FixedAsset asset,
    FADetails det,
    string transferPeriodId = null)
  {
    ((PXGraph) this).GetExtension<TransferProcess.TransferProcessFixedAssetChecksExtension>().CheckAssetTransferInformation(asset, det, transferPeriodId);
  }

  public class TransferProcessFixedAssetChecksExtension : 
    FixedAssetChecksExtensionBase<TransferProcess>
  {
  }

  [Serializable]
  public class TransferFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _TransferDate;
    protected string _PeriodID;
    protected int? _ClassFrom;
    protected int? _BranchFrom;
    protected string _DepartmentFrom;
    protected int? _ClassTo;
    protected int? _BranchTo;
    protected string _DepartmentTo;
    protected string _Reason;

    [Organization(true)]
    [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.multipleCalendarsSupport>>))]
    public virtual int? OrganizationID { get; set; }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Transfer Date")]
    public virtual DateTime? TransferDate
    {
      get => this._TransferDate;
      set => this._TransferDate = value;
    }

    [PXUIField(DisplayName = "Transfer Period")]
    [FABookPeriodOpenInGLSelector(null, null, null, false, null, typeof (TransferProcess.TransferFilter.transferDate), null, null, typeof (TransferProcess.TransferFilter.organizationID), null)]
    public virtual string PeriodID
    {
      get => this._PeriodID;
      set => this._PeriodID = value;
    }

    [PXDBInt]
    [PXSelector(typeof (Search<FAClass.assetID, Where<FAClass.recordType, Equal<FARecordType.classType>>>), new System.Type[] {typeof (FAClass.assetCD), typeof (FAClass.assetTypeID), typeof (FAClass.description), typeof (FAClass.usefulLife)}, SubstituteKey = typeof (FAClass.assetCD), DescriptionField = typeof (FAClass.description), CacheGlobal = true)]
    [PXUIField(DisplayName = "Asset Class")]
    public virtual int? ClassFrom
    {
      get => this._ClassFrom;
      set => this._ClassFrom = value;
    }

    [BranchOfOrganization(typeof (TransferProcess.TransferFilter.organizationID), true, null, typeof (FeaturesSet.multipleCalendarsSupport))]
    public virtual int? BranchFrom
    {
      get => this._BranchFrom;
      set => this._BranchFrom = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
    [PXUIField(DisplayName = "Department")]
    public virtual string DepartmentFrom
    {
      get => this._DepartmentFrom;
      set => this._DepartmentFrom = value;
    }

    [PXDBInt]
    [PXSelector(typeof (Search<FAClass.assetID, Where<FAClass.recordType, Equal<FARecordType.classType>>>), new System.Type[] {typeof (FAClass.assetCD), typeof (FAClass.assetTypeID), typeof (FAClass.description), typeof (FAClass.usefulLife)}, SubstituteKey = typeof (FAClass.assetCD), DescriptionField = typeof (FAClass.description), CacheGlobal = true)]
    [PXUIField(DisplayName = "Asset Class")]
    public virtual int? ClassTo
    {
      get => this._ClassTo;
      set => this._ClassTo = value;
    }

    [BranchOfOrganization(typeof (TransferProcess.TransferFilter.organizationID), true, null, typeof (FeaturesSet.multipleCalendarsSupport))]
    public virtual int? BranchTo
    {
      get => this._BranchTo;
      set => this._BranchTo = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
    [PXUIField(DisplayName = "Department")]
    public virtual string DepartmentTo
    {
      get => this._DepartmentTo;
      set => this._DepartmentTo = value;
    }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Reason")]
    public virtual string Reason
    {
      get => this._Reason;
      set => this._Reason = value;
    }

    public abstract class organizationID : IBqlField, IBqlOperand
    {
    }

    public abstract class transferDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      TransferProcess.TransferFilter.transferDate>
    {
    }

    public abstract class periodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TransferProcess.TransferFilter.periodID>
    {
    }

    public abstract class classFrom : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransferProcess.TransferFilter.classFrom>
    {
    }

    public abstract class branchFrom : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransferProcess.TransferFilter.branchFrom>
    {
    }

    public abstract class departmentFrom : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TransferProcess.TransferFilter.departmentFrom>
    {
    }

    public abstract class classTo : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransferProcess.TransferFilter.classTo>
    {
    }

    public abstract class branchTo : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransferProcess.TransferFilter.branchTo>
    {
    }

    public abstract class departmentTo : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TransferProcess.TransferFilter.departmentTo>
    {
    }

    public abstract class reason : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TransferProcess.TransferFilter.reason>
    {
    }
  }
}
