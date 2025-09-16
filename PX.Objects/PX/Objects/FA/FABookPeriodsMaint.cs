// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodsMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.SQLTree;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.FA;

public class FABookPeriodsMaint : PXGraph<
#nullable disable
FABookPeriodsMaint>
{
  public PXFilter<FABookYear> BookYear;
  [PXFilterable(new Type[] {})]
  public PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FABookPeriod.bookID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  FABookYear.bookID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  FABookPeriod.organizationID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  FABookYear.organizationID, IBqlInt>.FromCurrent>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  FABookPeriod.finYear, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  FABookYear.year, IBqlString>.FromCurrent>>>.Order<
  #nullable disable
  By<Asc<FABookPeriod.periodNbr>>>>.ReadOnly BookPeriod;
  public PXAction<FABookYear> First;
  public PXAction<FABookYear> Previous;
  public PXAction<FABookYear> Next;
  public PXAction<FABookYear> Last;
  public PXAction<FABookYear> SynchronizeCalendarWithGL;

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXDBIntAttribute), "IsKey", false)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Required", true)]
  protected void FABookYear_BookID_CacheAttached(PXCache sender)
  {
  }

  [Organization(true, IsKey = false, ValidateValue = false)]
  protected void FABookYear_OrganizationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
  [PXSelector(typeof (Search<FABookYear.year, Where<FABookYear.organizationID, Equal<Current<FABookYear.organizationID>>, And<FABookYear.bookID, Equal<Current<FABookYear.bookID>>>>, OrderBy<Desc<FABookYear.year>>>))]
  protected void FABookYear_Year_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXFirstButton]
  protected virtual IEnumerable first(PXAdapter adapter)
  {
    if (this.IsValidCurrent())
    {
      FABookYear faBookYear = this.SelectSingleBookYear(FABookPeriodsMaint.LastFirstYear.First);
      ((PXSelectBase) this.BookYear).Cache.Clear();
      yield return (object) faBookYear;
    }
  }

  [PXUIField]
  [PXPreviousButton]
  protected virtual IEnumerable previous(PXAdapter adapter)
  {
    if (this.IsValidCurrent())
    {
      FABookYear faBookYear = this.SelectSingleBookYear(FABookPeriodsMaint.PrevNextYear.Previous, (((PXSelectBase) this.BookYear).Cache.Current as FABookYear).Year) ?? this.SelectSingleBookYear(FABookPeriodsMaint.LastFirstYear.Last);
      ((PXSelectBase) this.BookYear).Cache.Clear();
      yield return (object) faBookYear;
    }
  }

  [PXUIField]
  [PXNextButton]
  protected virtual IEnumerable next(PXAdapter adapter)
  {
    if (this.IsValidCurrent())
    {
      FABookYear faBookYear = this.SelectSingleBookYear(FABookPeriodsMaint.PrevNextYear.Next, (((PXSelectBase) this.BookYear).Cache.Current as FABookYear).Year) ?? this.SelectSingleBookYear(FABookPeriodsMaint.LastFirstYear.First);
      ((PXSelectBase) this.BookYear).Cache.Clear();
      yield return (object) faBookYear;
    }
  }

  [PXUIField]
  [PXLastButton]
  protected virtual IEnumerable last(PXAdapter adapter)
  {
    if (this.IsValidCurrent())
    {
      FABookYear faBookYear = this.SelectSingleBookYear(FABookPeriodsMaint.LastFirstYear.Last);
      ((PXSelectBase) this.BookYear).Cache.Clear();
      yield return (object) faBookYear;
    }
  }

  [PXUIField]
  [PXProcessButton(Category = "Processing")]
  protected virtual IEnumerable synchronizeCalendarWithGL(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, FABookPeriodsMaint.\u003C\u003Ec.\u003C\u003E9__20_0 ?? (FABookPeriodsMaint.\u003C\u003Ec.\u003C\u003E9__20_0 = new PXToggleAsyncDelegate((object) FABookPeriodsMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CsynchronizeCalendarWithGL\u003Eb__20_0))));
    return adapter.Get();
  }

  protected virtual void SyncronizeCalendars()
  {
    IFABookPeriodUtils service = ((PXGraph) this).GetService<IFABookPeriodUtils>();
    ((PXGraph) this).GetService<IFABookPeriodRepository>();
    Dictionary<int, FABookPeriod> dictionary = GraphHelper.RowCast<FABookPeriod>((IEnumerable) PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<FABook.bookID>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Inner<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.startDate, NotEqual<FinPeriod.startDate>>>>>.Or<BqlOperand<FABookPeriod.endDate, IBqlDateTime>.IsNotEqual<FinPeriod.endDate>>>.Aggregate<To<GroupBy<FABookPeriod.organizationID>, GroupBy<FABookPeriod.bookID>, Min<FABookPeriod.finYear>, Min<FABookPeriod.finPeriodID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<FABookPeriod, int>((Func<FABookPeriod, int>) (period => period.OrganizationID.Value));
    FABookYear faBookYear = PXResultset<FABookYear>.op_Implicit(PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookYear.bookID, Equal<P.AsInt>>>>>.And<BqlOperand<FABookYear.organizationID, IBqlInt>.IsIn<P.AsInt>>>.Aggregate<To<Max<FABookYear.year>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) (int?) dictionary.Values.FirstOrDefault<FABookPeriod>()?.BookID,
      (object) dictionary.Keys
    }));
    List<(string, int[])> valueTupleList1 = new List<(string, int[])>();
    List<int> source = new List<int>();
    List<(int, int, string, string)> valueTupleList2 = new List<(int, int, string, string)>();
    List<Exception> exceptions = new List<Exception>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (KeyValuePair<int, FABookPeriod> keyValuePair in dictionary)
      {
        int key = keyValuePair.Key;
        int postingBookID = keyValuePair.Value.BookID.Value;
        string finPeriodId = keyValuePair.Value.FinPeriodID;
        FABookPeriod faBookPeriod1 = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<P.AsInt>>>>, And<BqlOperand<FABookPeriod.organizationID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FABookPeriod.startDate, IBqlDateTime>.IsEqual<FinPeriod.startDate>>>, And<BqlOperand<FABookPeriod.endDate, IBqlDateTime>.IsEqual<FinPeriod.endDate>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsLess<P.AsString>>>.Aggregate<To<Max<FABookPeriod.finPeriodID>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) postingBookID,
          (object) key,
          (object) finPeriodId
        }));
        string aFiscalPeriod;
        if (faBookPeriod1.FinPeriodID == null)
        {
          FABookPeriod faBookPeriod2 = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<P.AsInt>>>>>.And<BqlOperand<FABookPeriod.organizationID, IBqlInt>.IsEqual<P.AsInt>>>.Order<By<BqlField<FABookPeriod.finPeriodID, IBqlString>.Asc>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) postingBookID,
            (object) key
          }));
          if (string.CompareOrdinal(faBookPeriod2.FinPeriodID, keyValuePair.Value.FinPeriodID) < 0)
          {
            if (key == 0)
            {
              if (!this.IsPossibleToCreatePreviousFABookYear(key, postingBookID, keyValuePair.Value.FinYear))
                throw new PXException("The FA calendar cannot be synchronized with the GL because the financial year's configuration has changed. Please contact your Acumatica support provider.");
            }
            else
            {
              if (!this.IsPossibleToCreatePreviousFABookYear(key, postingBookID, keyValuePair.Value.FinYear))
              {
                exceptions.Add((Exception) new PXException("The FA calendar for the {0} book of the {1} company cannot be synchronized with the GL because the financial year's configuration has changed. Please contact your Acumatica support provider.", new object[2]
                {
                  (object) BookMaint.FindByID((PXGraph) this, new int?(postingBookID)).BookCode,
                  (object) PXAccess.GetOrganizationCD(new int?(key))
                }));
                continue;
              }
              string previousYearId = FinPeriodUtils.GetPreviousYearID(keyValuePair.Value.FinYear);
              valueTupleList2.Add((key, postingBookID, faBookPeriod2.FinYear, previousYearId == null ? faBookPeriod2.FinYear : previousYearId));
            }
          }
          aFiscalPeriod = faBookPeriod2.FinPeriodID;
        }
        else
          aFiscalPeriod = service.GetNextFABookPeriodID(faBookPeriod1.FinPeriodID, new int?(postingBookID), key);
        string str = FinPeriodUtils.FiscalYear(aFiscalPeriod);
        int[] childBranchIds = PXAccess.GetChildBranchIDs(new int?(key), true);
        if (key != 0)
        {
          valueTupleList1.Add((aFiscalPeriod, childBranchIds));
          source.Add(key);
        }
        PXDatabase.Delete<FABookPeriod>(new PXDataFieldRestrict[3]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookPeriod.organizationID>((object) key),
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookPeriod.bookID>((object) postingBookID),
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookPeriod.finPeriodID>((PXDbType) 100, new int?(), (object) aFiscalPeriod, (PXComp) 3)
        });
        PXDatabase.Delete<FABookYear>(new PXDataFieldRestrict[3]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookYear.organizationID>((object) key),
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookYear.bookID>((object) postingBookID),
          (PXDataFieldRestrict) new PXDataFieldRestrict<FABookYear.year>((PXDbType) 100, new int?(), (object) str, (PXComp) 3)
        });
        foreach (PXResult<FixedAsset> pxResult in PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FABookBalance>.On<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<FABookBalance.assetID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.updateGL, Equal<True>>>>, And<BqlOperand<FABookBalance.depreciate, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FABookBalance.status, IBqlString>.IsIn<FixedAssetStatus.active, FixedAssetStatus.suspended>>>, And<BqlOperand<FABookBalance.deprToPeriod, IBqlString>.IsGreaterEqual<P.AsString>>>>.And<BqlOperand<FixedAsset.branchID, IBqlInt>.IsIn<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) finPeriodId,
          (object) PXAccess.GetChildBranchIDs(new int?(key), true)
        }))
          PXDatabase.Delete<FABookHistory>(new PXDataFieldRestrict[3]
          {
            (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.assetID>((object) PXResult<FixedAsset>.op_Implicit(pxResult).AssetID),
            (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.bookID>((object) postingBookID),
            (PXDataFieldRestrict) new PXDataFieldRestrict<FABookHistory.finPeriodID>((PXDbType) 100, new int?(), (object) aFiscalPeriod, (PXComp) 3)
          });
        foreach (PXResult<PX.Objects.GL.FinPeriods.TableDefinition.FinYear> pxResult in PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXViewOf<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.BasedOn<SelectFromBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FABookYear>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookYear.year, Equal<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>>>>, And<BqlOperand<FABookYear.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID>>>>.And<BqlOperand<FABookYear.bookID, IBqlInt>.IsEqual<P.AsInt>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<FABookYear.year, IBqlString>.IsNull>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) postingBookID,
          (object) key
        }))
        {
          PX.Objects.GL.FinPeriods.TableDefinition.FinYear finYear = PXResult<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.op_Implicit(pxResult);
          PXDatabase.Insert<FABookYear>(new PXDataFieldAssign[13]
          {
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.bookID>((object) postingBookID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.organizationID>((object) finYear.OrganizationID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.year>((object) finYear.Year),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.startMasterFinPeriodID>((object) finYear.StartMasterFinPeriodID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.finPeriods>((object) finYear.FinPeriods),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.startDate>((object) finYear.StartDate),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.endDate>((object) finYear.EndDate),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.createdByID>((object) finYear.CreatedByID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.createdByScreenID>((object) finYear.CreatedByScreenID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.createdDateTime>((object) finYear.CreatedDateTime),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.lastModifiedByID>((object) finYear.LastModifiedByID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.lastModifiedByScreenID>((object) finYear.LastModifiedByScreenID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookYear.lastModifiedDateTime>((object) finYear.LastModifiedDateTime)
          });
        }
        foreach (PXResult<FinPeriod> pxResult in PXSelectBase<FinPeriod, PXViewOf<FinPeriod>.BasedOn<SelectFromBase<FinPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FABookPeriod>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.finPeriodID, Equal<FinPeriod.finPeriodID>>>>, And<BqlOperand<FABookPeriod.organizationID, IBqlInt>.IsEqual<FinPeriod.organizationID>>>>.And<BqlOperand<FABookPeriod.bookID, IBqlInt>.IsEqual<P.AsInt>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FinPeriod.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsNull>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) postingBookID,
          (object) key
        }))
        {
          FinPeriod finPeriod = PXResult<FinPeriod>.op_Implicit(pxResult);
          PXDatabase.Insert<FABookPeriod>(new PXDataFieldAssign[17]
          {
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.bookID>((object) postingBookID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.organizationID>((object) finPeriod.OrganizationID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.finYear>((object) finPeriod.FinYear),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.finPeriodID>((object) finPeriod.FinPeriodID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.masterFinPeriodID>((object) finPeriod.MasterFinPeriodID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.startDate>((object) finPeriod.StartDate),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.endDate>((object) finPeriod.EndDate),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.descr>((object) finPeriod.Descr),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.dateLocked>((object) finPeriod.DateLocked),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.periodNbr>((object) finPeriod.PeriodNbr),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.closed>((object) (bool) (finPeriod.Status == "Closed" ? 1 : (finPeriod.Status == "Locked" ? 1 : 0))),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.createdByID>((object) finPeriod.CreatedByID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.createdByScreenID>((object) finPeriod.CreatedByScreenID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.createdDateTime>((object) finPeriod.CreatedDateTime),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.lastModifiedByID>((object) finPeriod.LastModifiedByID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.lastModifiedByScreenID>((object) finPeriod.LastModifiedByScreenID),
            (PXDataFieldAssign) new PXDataFieldAssign<FABookPeriod.lastModifiedDateTime>((object) finPeriod.LastModifiedDateTime)
          });
        }
      }
      transactionScope.Complete();
    }
    if (source.Count<int>() > 0)
    {
      string nextYearId1 = FinPeriodUtils.GetNextYearID(faBookYear.Year);
      List<FAOrganizationBook> list = GraphHelper.RowCast<FAOrganizationBook>((IEnumerable) PXSelectBase<FAOrganizationBook, PXViewOf<FAOrganizationBook>.BasedOn<SelectFromBase<FAOrganizationBook, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FAOrganizationBook.updateGL, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FAOrganizationBook.rawOrganizationID, IsNull>>>>.Or<BqlOperand<FAOrganizationBook.rawOrganizationID, IBqlInt>.IsIn<P.AsInt>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) source
      })).ToList<FAOrganizationBook>();
      string nextYearId2 = FinPeriodUtils.GetNextYearID(list.Min<FAOrganizationBook, string>((Func<FAOrganizationBook, string>) (b => b.LastCalendarYear)));
      GenerationPeriods.GeneratePeriods(new BoundaryYears()
      {
        FromYear = nextYearId2,
        ToYear = nextYearId1
      }, list, ref exceptions);
    }
    foreach ((int, int, string, string) valueTuple in valueTupleList2)
      GenerationPeriods.GeneratePeriods(new BoundaryYears()
      {
        FromYear = valueTuple.Item3,
        ToYear = valueTuple.Item4
      }, GraphHelper.RowCast<FAOrganizationBook>((IEnumerable) PXSelectBase<FAOrganizationBook, PXViewOf<FAOrganizationBook>.BasedOn<SelectFromBase<FAOrganizationBook, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FAOrganizationBook.bookID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FAOrganizationBook.rawOrganizationID, IsNull>>>>.Or<BqlOperand<FAOrganizationBook.rawOrganizationID, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) valueTuple.Item2,
        (object) valueTuple.Item1
      })).ToList<FAOrganizationBook>(), ref exceptions);
    foreach ((string str, int[] numArray) in valueTupleList1)
    {
      foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FixedAsset>.On<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<FixedAsset.assetID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.updateGL, Equal<True>>>>, And<BqlOperand<FABookBalance.depreciate, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FABookBalance.status, IBqlString>.IsIn<FixedAssetStatus.active, FixedAssetStatus.suspended>>>, And<BqlOperand<FABookBalance.deprToPeriod, IBqlString>.IsGreaterEqual<P.AsString>>>>.And<BqlOperand<FixedAsset.branchID, IBqlInt>.IsIn<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
      {
        (object) str,
        (object) numArray
      }))
      {
        FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
        string deprToPeriod = faBookBalance.DeprToPeriod;
        string strA = (string) PXFormulaAttribute.Evaluate<FABookBalance.deprToPeriod>((PXCache) GraphHelper.Caches<FABookBalance>((PXGraph) this), (object) faBookBalance);
        if (string.Compare(strA, deprToPeriod) != 0)
          PXDatabase.Update<FABookBalance>(new PXDataFieldParam[3]
          {
            (PXDataFieldParam) new PXDataFieldAssign<FABookBalance.deprToPeriod>((object) strA),
            (PXDataFieldParam) new PXDataFieldRestrict<FABookBalance.assetID>((object) faBookBalance.AssetID),
            (PXDataFieldParam) new PXDataFieldRestrict<FABookBalance.bookID>((object) faBookBalance.BookID)
          });
      }
      foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FixedAsset>.On<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<FixedAsset.assetID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.updateGL, Equal<True>>>>, And<BqlOperand<FABookBalance.depreciate, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FABookBalance.status, IBqlString>.IsIn<FixedAssetStatus.active, FixedAssetStatus.suspended>>>, And<BqlOperand<FABookBalance.lastDeprPeriod, IBqlString>.IsNotNull>>, And<BqlOperand<FABookBalance.currDeprPeriod, IBqlString>.IsGreaterEqual<P.AsString>>>>.And<BqlOperand<FixedAsset.branchID, IBqlInt>.IsIn<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
      {
        (object) str,
        (object) numArray
      }))
      {
        FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
        string currDeprPeriod = faBookBalance.CurrDeprPeriod;
        string nextFaBookPeriodId = service.GetNextFABookPeriodID(faBookBalance.LastDeprPeriod, faBookBalance.BookID, faBookBalance.AssetID);
        if (string.Compare(nextFaBookPeriodId, currDeprPeriod) != 0)
          PXDatabase.Update<FABookBalance>(new PXDataFieldParam[3]
          {
            (PXDataFieldParam) new PXDataFieldAssign<FABookBalance.currDeprPeriod>((object) nextFaBookPeriodId),
            (PXDataFieldParam) new PXDataFieldRestrict<FABookBalance.assetID>((object) faBookBalance.AssetID),
            (PXDataFieldParam) new PXDataFieldRestrict<FABookBalance.bookID>((object) faBookBalance.BookID)
          });
      }
      foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FixedAsset>.On<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<FixedAsset.assetID>>>, FbqlJoins.Left<FABookHistory>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<FABookBalance.assetID>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<FABookBalance.bookID>>>>.And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsEqual<FABookBalance.currDeprPeriod>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.updateGL, Equal<True>>>>, And<BqlOperand<FABookBalance.depreciate, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FABookBalance.status, IBqlString>.IsIn<FixedAssetStatus.active, FixedAssetStatus.suspended>>>, And<BqlOperand<FABookBalance.deprToPeriod, IBqlString>.IsGreaterEqual<P.AsString>>>, And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsNull>>>.And<BqlOperand<FixedAsset.branchID, IBqlInt>.IsIn<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
      {
        (object) str,
        (object) numArray
      }))
      {
        FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
        AssetProcess.CalculateAsset((IEnumerable<FABookBalance>) faBookBalance.SingleToListOrNull<FABookBalance>(), faBookBalance.CurrDeprPeriod);
      }
    }
    if (exceptions.Count > 0)
    {
      foreach (Exception exception in exceptions)
        PXTrace.WriteError(exception);
      throw new PXException("One or more FA calendars could not be synchronized with the GL. See the trace for details.");
    }
  }

  protected virtual void CheckSyncNecessity()
  {
    if (PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<FABook.bookID>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Inner<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.startDate, NotEqual<FinPeriod.startDate>>>>>.Or<BqlOperand<FABookPeriod.endDate, IBqlDateTime>.IsNotEqual<FinPeriod.endDate>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())) == null)
      throw new PXException("The periods in the posting book match the financial periods in the general ledger.");
  }

  protected virtual void CheckReleasedTransactionsExistence()
  {
    if (PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<FABook.bookID>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Inner<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>, FbqlJoins.Inner<FATran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<FATran.bookID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.finPeriodID, Equal<FATran.finPeriodID>>>>>.And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.startDate, NotEqual<FinPeriod.startDate>>>>>.Or<BqlOperand<FABookPeriod.endDate, IBqlDateTime>.IsNotEqual<FinPeriod.endDate>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
      throw new PXException("The periods in the posting book cannot be synchronized with the financial periods in the general ledger, because there are released transactions in the periods. Please contact the support service for further assistance.");
  }

  protected virtual void CheckUnreleasedTransactionsExistence()
  {
    if (PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<FABook.bookID>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Inner<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>, FbqlJoins.Inner<FATran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.bookID, Equal<FATran.bookID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.finPeriodID, Equal<FATran.finPeriodID>>>>>.And<BqlOperand<FATran.released, IBqlBool>.IsNotEqual<True>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.startDate, NotEqual<FinPeriod.startDate>>>>>.Or<BqlOperand<FABookPeriod.endDate, IBqlDateTime>.IsNotEqual<FinPeriod.endDate>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
      throw new PXException("The periods in the posting book cannot be synchronized with the financial periods in the general ledger, because there are unreleased transactions in the periods. Use the Delete Unreleased Transactions (FA508000) form to delete the transactions.");
  }

  protected virtual bool IsPossibleToCreatePreviousFABookYear(
    int organizationID,
    int postingBookID,
    string firstYearID)
  {
    IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(new int?(postingBookID), true);
    FABookYear firstYear = PXResultset<FABookYear>.op_Implicit(PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookYear.organizationID, Equal<P.AsInt>>>>, And<BqlOperand<FABookYear.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookYear.year, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) organizationID,
      (object) postingBookID,
      (object) firstYearID
    }));
    FABookYear newYear = new FABookYear();
    FiscalYearCreator<FABookYear, FABookPeriod>.CreatePrevYear(faBookYearSetup, firstYear, newYear);
    PX.Objects.GL.FinPeriods.TableDefinition.FinYear finYear = PXResultset<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.op_Implicit(PXSelectBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, PXViewOf<PX.Objects.GL.FinPeriods.TableDefinition.FinYear>.BasedOn<SelectFromBase<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) organizationID,
      (object) firstYearID
    }));
    if (newYear == null)
      return false;
    DateTime? endDate = newYear.EndDate;
    DateTime? startDate = finYear.StartDate;
    if (endDate.HasValue != startDate.HasValue)
      return false;
    return !endDate.HasValue || endDate.GetValueOrDefault() == startDate.GetValueOrDefault();
  }

  protected bool IsValidCurrent()
  {
    if (((PXSelectBase) this.BookYear).Cache.InternalCurrent == null)
      return false;
    FABookYear current = ((PXSelectBase) this.BookYear).Cache.Current as FABookYear;
    int? nullable = current.BookID;
    if (nullable.HasValue)
    {
      nullable = current.OrganizationID;
      if (nullable.HasValue)
        return PXResultset<FABookYear>.op_Implicit(PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookYear.bookID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) current.BookID
        })) != null;
    }
    return false;
  }

  protected FABookYear SelectSingleBookYear(FABookPeriodsMaint.LastFirstYear lastFirstYear)
  {
    ((PXGraph) this).Caches[typeof (FABookYear)].ClearQueryCache();
    PXResultset<FABookYear> source = PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookYear.bookID, Equal<BqlField<FABookYear.bookID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<FABookYear.organizationID, IBqlInt>.IsEqual<BqlField<FABookYear.organizationID, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>());
    if (lastFirstYear == FABookPeriodsMaint.LastFirstYear.First)
      return PXResult<FABookYear>.op_Implicit(((IQueryable<PXResult<FABookYear>>) source).OrderBy<PXResult<FABookYear>, string>((Expression<Func<PXResult<FABookYear>, string>>) (row => ((FABookYear) row).Year)).First<PXResult<FABookYear>>());
    return PXResult<FABookYear>.op_Implicit(((IQueryable<PXResult<FABookYear>>) source).OrderByDescending<PXResult<FABookYear>, string>((Expression<Func<PXResult<FABookYear>, string>>) (row => ((FABookYear) row).Year)).First<PXResult<FABookYear>>());
  }

  protected FABookYear SelectSingleBookYear(FABookPeriodsMaint.PrevNextYear direction, string year)
  {
    ((PXGraph) this).Caches[typeof (FABookYear)].ClearQueryCache();
    PXResultset<FABookYear> source = PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookYear.bookID, Equal<BqlField<FABookYear.bookID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<FABookYear.organizationID, IBqlInt>.IsEqual<BqlField<FABookYear.organizationID, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>());
    if (direction == FABookPeriodsMaint.PrevNextYear.Next)
      return PXResult<FABookYear>.op_Implicit(SQLinqExtensions.ReadOnly<PXResult<FABookYear>>(((IQueryable<PXResult<FABookYear>>) source).OrderBy<PXResult<FABookYear>, string>((Expression<Func<PXResult<FABookYear>, string>>) (row => ((FABookYear) row).Year)).Where<PXResult<FABookYear>>((Expression<Func<PXResult<FABookYear>, bool>>) (row => string.Compare(((FABookYear) row).Year, year) > 0))).FirstOrDefault<PXResult<FABookYear>>());
    if (direction == FABookPeriodsMaint.PrevNextYear.Previous)
      return PXResult<FABookYear>.op_Implicit(SQLinqExtensions.ReadOnly<PXResult<FABookYear>>(((IQueryable<PXResult<FABookYear>>) source).OrderByDescending<PXResult<FABookYear>, string>((Expression<Func<PXResult<FABookYear>, string>>) (row => ((FABookYear) row).Year)).Where<PXResult<FABookYear>>((Expression<Func<PXResult<FABookYear>, bool>>) (row => string.Compare(((FABookYear) row).Year, year) < 0))).FirstOrDefault<PXResult<FABookYear>>());
    return PXResult<FABookYear>.op_Implicit(SQLinqExtensions.ReadOnly<PXResult<FABookYear>>(((IQueryable<PXResult<FABookYear>>) source).Where<PXResult<FABookYear>>((Expression<Func<PXResult<FABookYear>, bool>>) (row => string.Compare(((FABookYear) row).Year, year) == 0))).FirstOrDefault<PXResult<FABookYear>>());
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FABookYear> e)
  {
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FABookYear>>) e).Cache.IsDirty = false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<FABookYear> e)
  {
    FABook faBook = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXViewOf<FABook>.BasedOn<SelectFromBase<FABook, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABook.bookID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) e.Row.BookID
    }));
    PXUIFieldAttribute.SetVisible<FABookYear.organizationID>(((PXSelectBase) this.BookYear).Cache, (object) null, faBook != null && faBook.UpdateGL.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>());
  }

  protected IEnumerable bookYear()
  {
    if (((PXSelectBase) this.BookYear).Cache.InternalCurrent == null)
      return (IEnumerable) new object[1]
      {
        (object) PXResultset<FABookYear>.op_Implicit(PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlOperand<FABookYear.bookID, IBqlInt>.IsEqual<FABook.bookID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookYear.organizationID, Equal<P.AsInt>>>>, And<BqlOperand<FABookYear.startDate, IBqlDateTime>.IsLess<P.AsDateTime>>>>.And<BqlOperand<FABookYear.endDate, IBqlDateTime>.IsGreater<P.AsDateTime>>>.Order<By<Desc<FABook.updateGL>, Asc<FABookYear.year>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
        {
          (object) PXAccess.GetParentOrganizationID(((PXGraph) this).Accessinfo.BranchID),
          (object) ((PXGraph) this).Accessinfo.BusinessDate,
          (object) ((PXGraph) this).Accessinfo.BusinessDate
        }))
      };
    FABookYear current = ((PXSelectBase) this.BookYear).Cache.Current as FABookYear;
    FABook faBook = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXViewOf<FABook>.BasedOn<SelectFromBase<FABook, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABook.bookID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) current.BookID
    }));
    if (faBook == null)
      return (IEnumerable) null;
    if (faBook.UpdateGL.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
    {
      int? organizationId = current.OrganizationID;
      int num = 0;
      if (organizationId.GetValueOrDefault() == num & organizationId.HasValue)
        current.OrganizationID = PXAccess.GetParentOrganizationID(((PXGraph) this).Accessinfo.BranchID);
    }
    else
      current.OrganizationID = new int?(0);
    if (PXResultset<FABookYear>.op_Implicit(PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookYear.bookID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) faBook.BookID
    })) == null)
    {
      ((PXSelectBase) this.BookYear).Cache.RaiseExceptionHandling<FABookYear.year>((object) current, (object) null, (Exception) new PXSetPropertyException("There is no calendar generated for the book in the current financial year. Create a calendar on the Generate Book Calendars (FA501000) form.", (PXErrorLevel) 2));
      current.Year = (string) null;
      return (IEnumerable) null;
    }
    if (string.IsNullOrWhiteSpace(current.Year))
    {
      DateTime dateTime = ((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now;
      current.Year = dateTime.Year.ToString();
    }
    FABookYear faBookYear = this.SelectSingleBookYear(FABookPeriodsMaint.PrevNextYear.Equal, current.Year);
    if (faBookYear == null)
    {
      ((PXSelectBase) this.BookYear).Cache.RaiseExceptionHandling<FABookYear.year>((object) current, (object) null, (Exception) new PXSetPropertyException("There is no calendar generated for the book in the current financial year. Create a calendar on the Generate Book Calendars (FA501000) form.", (PXErrorLevel) 2));
      current.Year = (string) null;
      return (IEnumerable) null;
    }
    return (IEnumerable) new object[1]
    {
      (object) faBookYear
    };
  }

  public enum LastFirstYear
  {
    Last,
    First,
  }

  public enum PrevNextYear
  {
    Previous,
    Next,
    Equal,
  }
}
