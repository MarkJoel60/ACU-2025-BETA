// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FixedAssetChecksExtensionBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.Objects.GL.Exceptions;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FA;

public abstract class FixedAssetChecksExtensionBase<TGraph> : PXGraphExtension<
#nullable disable
TGraph> where TGraph : PXGraph
{
  public FbqlSelect<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FABook.bookID, 
  #nullable disable
  Equal<FATran.bookID>>>>>.And<BqlOperand<
  #nullable enable
  FABook.updateGL, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FATran.assetID, 
  #nullable disable
  Equal<P.AsInt>>>>>.And<BqlOperand<
  #nullable enable
  FATran.released, IBqlBool>.IsEqual<
  #nullable disable
  True>>>.Order<By<Desc<FATran.tranPeriodID>>>, FATran>.View recentTransactions;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  public virtual bool UnreleasedTransactionsExistsForAsset(int? assetID)
  {
    return FixedAssetChecksExtensionBase<TGraph>.HashView<FixedAssetChecksExtensionBase<TGraph>.UnreleasedView, int?>.GetView((PXGraph) this.Base).Contains(assetID);
  }

  public virtual bool AssetBookNotAcquired(int? assetID, int? bookId)
  {
    return FixedAssetChecksExtensionBase<TGraph>.HashView<FixedAssetChecksExtensionBase<TGraph>.BookNotAcquiredView, FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance>.GetView((PXGraph) this.Base).Contains(new FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance(assetID, bookId));
  }

  public virtual void CheckIfAssetIsNotAcquired(int? assetID, int? bookId)
  {
    if (!this.AssetBookNotAcquired(assetID, bookId))
      return;
    FixedAsset fixedAsset = FixedAsset.PK.Find((PXGraph) this.Base, assetID);
    if (fixedAsset != null)
      throw new PXException("The {0} fixed asset does not have a released purchasing transaction. To continue processing, make sure that the purchasing transaction exists and is released.", new object[1]
      {
        (object) fixedAsset.AssetCD
      });
  }

  public virtual void CheckUnreleasedTransactions(int? assetID)
  {
    if (!this.UnreleasedTransactionsExistsForAsset(assetID))
      return;
    FixedAsset fixedAsset = FixedAsset.PK.Find((PXGraph) this.Base, assetID);
    if (fixedAsset != null)
      throw new PXException("The {0} fixed asset contains unreleased transactions. Release them to continue processing the asset.", new object[1]
      {
        (object) fixedAsset.AssetCD
      });
  }

  public virtual void CheckIfAssetIsUnderConstruction(int? assetID)
  {
    FixedAsset fixedAsset = FixedAsset.PK.Find((PXGraph) this.Base, assetID);
    if (fixedAsset != null && fixedAsset.UnderConstruction.GetValueOrDefault())
      throw new AssetIsUnderConstructionException();
  }

  public virtual void CheckUnreleasedTransactions(FixedAsset asset)
  {
    if (FixedAssetChecksExtensionBase<TGraph>.HashView<FixedAssetChecksExtensionBase<TGraph>.UnreleasedView, int?>.GetView((PXGraph) this.Base).Contains(asset.AssetID))
      throw new PXException("The {0} fixed asset contains unreleased transactions. Release them to continue processing the asset.", new object[1]
      {
        (object) asset.AssetCD
      });
  }

  public virtual string GetPeriodIDFromDate(FixedAsset asset, DateTime? depreciateFromDate)
  {
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(asset.BranchID);
    return this.FinPeriodRepository.GetPeriodIDFromDate(depreciateFromDate, parentOrganizationId);
  }

  public virtual string GetLastTransactionPeriodID(FixedAsset asset)
  {
    return ((PXSelectBase<FATran>) this.recentTransactions).SelectSingle(new object[1]
    {
      (object) asset.AssetID
    })?.TranPeriodID;
  }

  public virtual (bool checkPassed, bool cannotFindPeriodByDate, string comparedPeriodID, string lastTransactionPeriodID) CheckGivenPeriodOrDateNotEarlierThanTheLastTransactionPeriod(
    FixedAsset asset,
    DateTime? date)
  {
    return this.CheckGivenPeriodOrDateNotEarlierThanTheLastTransactionPeriod(asset, date, (string) null);
  }

  public virtual (bool checkPassed, bool cannotFindPeriodByDate, string comparedPeriodID, string lastTransactionPeriodID) CheckGivenPeriodOrDateNotEarlierThanTheLastTransactionPeriod(
    FixedAsset asset,
    DateTime? date,
    string periodID)
  {
    if (periodID == null)
    {
      try
      {
        if (periodID == null)
          periodID = this.GetPeriodIDFromDate(asset, date);
      }
      catch (FinancialPeriodNotDefinedForDateException ex)
      {
        return (false, true, (string) null, (string) null);
      }
    }
    string transactionPeriodId = this.GetLastTransactionPeriodID(asset);
    return (string.CompareOrdinal(periodID, transactionPeriodId) >= 0, false, periodID, transactionPeriodId);
  }

  public virtual void CheckDepreciationPeriodNotEarlierThanTheLastTransactionPeriod(
    FixedAsset asset,
    DateTime? depreciateFromDate)
  {
    if (!depreciateFromDate.HasValue)
      return;
    (bool checkPassed, bool cannotFindPeriodByDate, string str1, string str2) = this.CheckGivenPeriodOrDateNotEarlierThanTheLastTransactionPeriod(asset, depreciateFromDate);
    if (cannotFindPeriodByDate)
      throw new FinancialPeriodNotDefinedForDateException(depreciateFromDate);
    if (!checkPassed)
      throw new PXException("The period of the placed-in service date ({0}) cannot be earlier than the period of the latest transaction ({1}).", new object[2]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(str1),
        (object) FinPeriodIDFormattingAttribute.FormatForError(str2)
      });
  }

  public virtual void CheckIfAssetCanBeDisposed(
    FixedAsset asset,
    FADetails det,
    FASetup fasetup,
    DateTime disposalDate,
    string disposalPeriodID,
    bool deprBeforeDisposal)
  {
    if (det.ReceiptDate.HasValue && DateTime.Compare(det.ReceiptDate.Value, disposalDate) > 0)
      throw new PXException("Disposal date must be greater than acquisition date.");
    bool? nullable = det.IsReconciled;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      nullable = fasetup.ReconcileBeforeDisposal;
      if (nullable.GetValueOrDefault())
        throw new PXException("Unreconciled asset can not be disposed");
    }
    (bool checkPassed, bool cannotFindPeriodByDate, string str1, string str2) = this.CheckGivenPeriodOrDateNotEarlierThanTheLastTransactionPeriod(asset, new DateTime?(disposalDate), disposalPeriodID);
    if (cannotFindPeriodByDate)
      throw new PXException("Book Period cannot be found in the system.");
    disposalPeriodID = str1;
    if (!checkPassed)
      throw new PXException("The disposal period ({0}) cannot be earlier than the period of the most recent transaction ({1}).", new object[2]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(str1),
        (object) FinPeriodIDFormattingAttribute.FormatForError(str2)
      });
    foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXSelectReadonly<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FABookBalance.assetID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) asset.AssetID
    }))
    {
      FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
      nullable = faBookBalance.UpdateGL;
      string str3 = nullable.GetValueOrDefault() ? disposalPeriodID : this.FABookPeriodRepository.GetFABookPeriodIDOfDate(new DateTime?(disposalDate), faBookBalance.BookID, faBookBalance.AssetID);
      this.CheckUnreleasedTransactions(faBookBalance.AssetID);
      int num1;
      if (faBookBalance.LastDeprPeriod != faBookBalance.DeprToPeriod)
      {
        nullable = faBookBalance.Depreciate;
        if (nullable.GetValueOrDefault())
        {
          num1 = string.CompareOrdinal(str3, faBookBalance.CurrDeprPeriod) > 0 ? 1 : 0;
          goto label_15;
        }
      }
      num1 = 0;
label_15:
      int num2 = deprBeforeDisposal ? 1 : 0;
      if ((num1 & num2) != 0)
      {
        nullable = fasetup.AutoReleaseDepr;
        if (!nullable.GetValueOrDefault())
          throw new PXException("Fixed asset should be depreciated to period '{0}' if \"Automatically Release Depreciation Transactions\" is not set.", new object[1]
          {
            (object) PeriodIDAttribute.FormatForError($"{Math.Min(int.Parse(str3), int.Parse(faBookBalance.DeprToPeriod))}")
          });
      }
      if (string.CompareOrdinal(faBookBalance.LastDeprPeriod, str3) > 0)
        throw new PXException("The asset cannot be disposed of in past periods.");
    }
  }

  public virtual void CheckIfAssetCanBeTransferred(
    TransferProcess.TransferFilter filter,
    FixedAsset asset,
    FADetailsTransfer det)
  {
    PXAccess.MasterCollection.Branch branch1 = PXAccess.GetBranch(asset.BranchID);
    PXAccess.MasterCollection.Branch branch2 = PXAccess.GetBranch(filter.BranchTo);
    if (branch1 != null && branch2 != null && branch1.BaseCuryID != branch2.BaseCuryID)
      throw new PXSetPropertyException("The {1} fixed asset cannot be transferred to the {3} branch because the base currency of the fixed asset's branch ({0}) differs from the base currency of the {3} destination branch ({2}).", (PXErrorLevel) 3, new object[4]
      {
        (object) branch1.BaseCuryID,
        (object) asset.AssetCD,
        (object) branch2.BaseCuryID,
        (object) branch2.BranchCD
      });
    this.CheckUnreleasedTransactions(asset);
    this.CheckAssetTransferInformation(asset, (FADetails) det, filter.PeriodID);
    if (!string.IsNullOrEmpty(det.TransferPeriodID))
      return;
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelectReadonly<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FABookBalance.assetID>>>, OrderBy<Desc<FABookBalance.updateGL>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) asset.AssetID
    }));
    if (faBookBalance != null && !string.IsNullOrEmpty(faBookBalance.LastDeprPeriod) && faBookBalance.UpdateGL.GetValueOrDefault())
      throw new PXException("There are no open Financial Periods defined in the system.");
  }

  public virtual void CheckAssetTransferInformation(
    FixedAsset asset,
    FADetails det,
    string transferPeriodId = null)
  {
    if (det == null || asset == null)
      return;
    if (det.Status == "H" || det.Status == "S")
      throw new PXException("A transfer document cannot be created for a fixed asset in the Suspended or On Hold status. To proceed, unsuspend the asset or remove it from hold on the Fixed Assets (FA303000) form.");
    bool? nullable = asset.UnderConstruction;
    if (nullable.GetValueOrDefault())
      return;
    nullable = asset.Depreciable;
    if (!nullable.GetValueOrDefault() || transferPeriodId == null)
      return;
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.updateGL, Equal<True>>>>>.And<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) asset.AssetID
    }));
    if (faBookBalance != null && faBookBalance.DeprToPeriod != null && faBookBalance.Status != "F" && faBookBalance.Status == "A" && string.Compare(faBookBalance.DeprToPeriod, transferPeriodId) < 0)
      throw new PXException("A fixed asset in the Active status cannot be transferred in a period later than its Depr. to Period. To make a transfer, use an earlier period or fully depreciate the asset before transferring it.");
  }

  public abstract class HashView<TView, T> : PXView where TView : PXView
  {
    protected HashSet<T> _set;

    public static TView GetView(PXGraph graph)
    {
      string key = $"_{typeof (TView).Name}_";
      PXView view;
      if (!((Dictionary<string, PXView>) graph.Views).TryGetValue(key, out view))
      {
        PXViewCollection views = graph.Views;
        string str = key;
        Type type = typeof (TView);
        object[] objArray = new object[1]{ (object) graph };
        PXView instance;
        view = instance = (PXView) Activator.CreateInstance(type, objArray);
        views[str] = instance;
      }
      return (TView) view;
    }

    protected HashView(PXGraph graph, BqlCommand select)
      : base(graph, true, select)
    {
      this.Initialize(graph.TypedViews.GetView(this.BqlSelect, true).SelectMulti(Array.Empty<object>()).ToArray());
    }

    protected abstract void Initialize(object[] list);

    public virtual bool Contains(T item) => this._set.Contains(item);

    public virtual void Clear()
    {
    }
  }

  public class UnreleasedView(PXGraph graph) : 
    FixedAssetChecksExtensionBase<TGraph>.HashView<FixedAssetChecksExtensionBase<TGraph>.UnreleasedView, int?>(graph, (BqlCommand) new Select4<FATran, Where<FATran.released, Equal<False>>, Aggregate<GroupBy<FATran.assetID, GroupBy<FATran.bookID>>>>())
  {
    protected override void Initialize(object[] list)
    {
      this._set = new HashSet<int?>((IEnumerable<int?>) Array.ConvertAll<object, int?>(list, (Converter<object, int?>) (a => ((FATran) a).AssetID)));
    }
  }

  public class BookNotAcquiredView(PXGraph graph) : 
    FixedAssetChecksExtensionBase<TGraph>.HashView<FixedAssetChecksExtensionBase<TGraph>.BookNotAcquiredView, FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance>(graph, (BqlCommand) new Select<FABookBalance, Where<FABookBalance.isAcquired, NotEqual<True>>>())
  {
    protected override void Initialize(object[] list)
    {
      this._set = new HashSet<FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance>((IEnumerable<FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance>) Array.ConvertAll<object, FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance>(list, (Converter<object, FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance>) (a => new FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance(((FABookBalance) a).AssetID, ((FABookBalance) a).BookID))));
    }
  }

  public struct HashCodedFABookBalance : 
    IEquatable<FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance>
  {
    public int? AssetId { get; }

    public int? BookId { get; }

    public HashCodedFABookBalance(int? assetId, int? bookId)
    {
      int? nullable1 = assetId;
      int? nullable2 = bookId;
      this.AssetId = nullable1;
      this.BookId = nullable2;
    }

    public override bool Equals(object obj)
    {
      return obj is FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance other && this.Equals(other);
    }

    public bool Equals(
      FixedAssetChecksExtensionBase<TGraph>.HashCodedFABookBalance other)
    {
      int? assetId1 = this.AssetId;
      int? assetId2 = other.AssetId;
      if (!(assetId1.GetValueOrDefault() == assetId2.GetValueOrDefault() & assetId1.HasValue == assetId2.HasValue))
        return false;
      int? bookId1 = this.BookId;
      int? bookId2 = other.BookId;
      return bookId1.GetValueOrDefault() == bookId2.GetValueOrDefault() & bookId1.HasValue == bookId2.HasValue;
    }

    public override int GetHashCode()
    {
      return (17 * 31 /*0x1F*/ + this.AssetId.GetHashCode()) * 31 /*0x1F*/ + this.BookId.GetHashCode();
    }
  }
}
