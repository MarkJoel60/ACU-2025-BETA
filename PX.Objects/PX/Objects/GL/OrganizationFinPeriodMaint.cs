// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.OrganizationFinPeriodMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.FA;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.GraphBaseExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class OrganizationFinPeriodMaint : 
  PXGraph<OrganizationFinPeriodMaint>,
  IFinPeriodMaintenanceGraph
{
  public PXSelect<OrganizationFinYear, Where<OrganizationFinYear.organizationID, Equal<Optional<OrganizationFinYear.organizationID>>, And<MatchWithOrganization<OrganizationFinYear.organizationID>>>> OrgFinYear;
  public PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.organizationID, Equal<Optional<OrganizationFinYear.organizationID>>, And<OrganizationFinPeriod.finYear, Equal<Optional<OrganizationFinYear.year>>>>, OrderBy<Asc<OrganizationFinPeriod.periodNbr>>> OrgFinPeriods;
  public PXSelectJoin<OrganizationFinYear, InnerJoin<PX.Objects.GL.DAC.Organization, On<OrganizationFinYear.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>, Where<PX.Objects.GL.DAC.Organization.organizationCD, Equal<Required<PX.Objects.GL.DAC.Organization.organizationCD>>>, OrderBy<Desc<OrganizationFinYear.year>>> LastOrganizationYear;
  public PXFilter<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters> NewCalendarParams;
  public PXSetup<FinYearSetup> YearSetup;
  public PXFilter<OrganizationFinPeriodMaint.FinYearKey> StoredYearKey;
  public PXAction<OrganizationFinYear> cancel;
  public PXDelete<OrganizationFinYear> Delete;
  public PXFirst<OrganizationFinYear> First;
  public PXPrevious<OrganizationFinYear> Previous;
  public PXNext<OrganizationFinYear> Next;
  public PXLast<OrganizationFinYear> Last;
  private BqlCommand referencingByTranQuery;
  private BqlCommand referencingByBatchQuery;
  private MasterFinPeriodMaint masterCalendarGraph;

  public OrganizationFinPeriodMaint()
  {
    ((PXSelectBase) this.OrgFinYear).Cache.AllowInsert = false;
    ((PXSelectBase) this.OrgFinPeriods).Cache.AllowInsert = ((PXSelectBase) this.OrgFinPeriods).Cache.AllowUpdate = ((PXSelectBase) this.OrgFinPeriods).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetVisible<OrganizationFinPeriod.iNClosed>(((PXSelectBase) this.OrgFinPeriods).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.inventory>());
    PXUIFieldAttribute.SetVisible<OrganizationFinPeriod.fAClosed>(((PXSelectBase) this.OrgFinPeriods).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.fixedAsset>());
    FinYearSetup current = ((PXSelectBase<FinYearSetup>) this.YearSetup).Current;
    ((PXAction) this.Delete).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() || PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>());
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    OrganizationFinPeriodMaint organizationFinPeriodMaint = this;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    OrganizationFinPeriodMaint.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250 = new OrganizationFinPeriodMaint.\u003C\u003Ec__DisplayClass25_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.\u003C\u003E4__this = this;
    string str;
    if (((PXSelectBase) organizationFinPeriodMaint.NewCalendarParams).View.Answer == null)
    {
      OrganizationFinPeriodMaint.FinYearKey finYearKey = new OrganizationFinPeriodMaint.FinYearKey()
      {
        OrganizationID = (int?) ((PXSelectBase<OrganizationFinYear>) organizationFinPeriodMaint.OrgFinYear).Current?.OrganizationID,
        Year = ((PXSelectBase<OrganizationFinYear>) organizationFinPeriodMaint.OrgFinYear).Current?.Year
      };
      ((PXGraph) organizationFinPeriodMaint).Clear();
      ((PXGraph) organizationFinPeriodMaint).SelectTimeStamp();
      ((PXSelectBase) organizationFinPeriodMaint.StoredYearKey).Cache.Clear();
      ((PXSelectBase<OrganizationFinPeriodMaint.FinYearKey>) organizationFinPeriodMaint.StoredYearKey).Insert(finYearKey);
      ((PXSelectBase) organizationFinPeriodMaint.StoredYearKey).Cache.IsDirty = false;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass250.organizationCD = (string) a.Searches.GetSearchValueByPosition(0);
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass250.organizationCD == null)
        yield break;
      str = (string) a.Searches.GetSearchValueByPosition(1);
    }
    else
    {
      object valueExt = ((PXSelectBase) organizationFinPeriodMaint.NewCalendarParams).Cache.GetValueExt<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters.organizationID>((object) ((PXSelectBase<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters>) organizationFinPeriodMaint.NewCalendarParams).Current);
      PXFieldState pxFieldState = valueExt as PXFieldState;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass250.organizationCD = pxFieldState != null ? (string) pxFieldState.Value : (string) valueExt;
      str = ((PXSelectBase<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters>) organizationFinPeriodMaint.NewCalendarParams).Current.StartYear;
    }
    // ISSUE: reference to a compiler-generated field
    OrganizationFinYear organizationFinYear1 = PXResultset<OrganizationFinYear>.op_Implicit(PXSelectBase<OrganizationFinYear, PXSelectJoin<OrganizationFinYear, InnerJoin<PX.Objects.GL.DAC.Organization, On<OrganizationFinYear.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>, Where<PX.Objects.GL.DAC.Organization.organizationCD, Equal<Required<PX.Objects.GL.DAC.Organization.organizationCD>>, And<OrganizationFinYear.year, Equal<Required<OrganizationFinYear.year>>>>>.Config>.SelectSingleBound((PXGraph) organizationFinPeriodMaint, new object[0], new object[2]
    {
      (object) cDisplayClass250.organizationCD,
      (object) str
    }));
    OrganizationFinYear organizationFinYear2;
    if (organizationFinYear1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OrganizationFinYear organizationFinYear3 = ((PXSelectBase<OrganizationFinYear>) organizationFinPeriodMaint.LastOrganizationYear).SelectSingle(new object[1]
      {
        (object) cDisplayClass250.organizationCD
      });
      if (organizationFinYear3 == null)
      {
        // ISSUE: method pointer
        if (organizationFinPeriodMaint.NewCalendarParams.AskExtFullyValid(new PXView.InitializePanel((object) cDisplayClass250, __methodptr(\u003CCancel\u003Eb__0)), (DialogAnswerType) 1, true))
        {
          OrganizationFinYear organizationFinYear4;
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            organizationFinYear4 = organizationFinPeriodMaint.GenerateSingleOrganizationFinYear(((PXSelectBase<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters>) organizationFinPeriodMaint.NewCalendarParams).Current.OrganizationID.Value, ((PXSelectBase<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters>) organizationFinPeriodMaint.NewCalendarParams).Current.StartYear, ((PXSelectBase<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters>) organizationFinPeriodMaint.NewCalendarParams).Current.StartMasterFinPeriodID);
            ((PXGraph) organizationFinPeriodMaint).Actions.PressSave();
            transactionScope.Complete();
          }
          organizationFinYear2 = organizationFinYear4;
        }
        else
          organizationFinYear2 = organizationFinPeriodMaint.FinPeriodRepository.FindOrganizationFinYearByID((int?) ((PXSelectBase<OrganizationFinPeriodMaint.FinYearKey>) organizationFinPeriodMaint.StoredYearKey).Current?.OrganizationID, ((PXSelectBase<OrganizationFinPeriodMaint.FinYearKey>) organizationFinPeriodMaint.StoredYearKey).Current?.Year);
      }
      else
        organizationFinYear2 = organizationFinYear3;
    }
    else
      organizationFinYear2 = organizationFinYear1;
    if (organizationFinYear2 != null)
      yield return (object) organizationFinYear2;
  }

  protected virtual void NewOrganizationCalendarParameters_StartMasterFinPeriodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    OrganizationFinPeriodMaint.NewOrganizationCalendarParameters row = (OrganizationFinPeriodMaint.NewOrganizationCalendarParameters) e.Row;
    row.StartDate = (DateTime?) this.FinPeriodRepository.FindByID(new int?(0), row.StartMasterFinPeriodID)?.StartDate;
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return string.Compare(viewName, "OrgFinYear") != 0 ? ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters) : 0;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<OrganizationFinYear> e)
  {
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<OrganizationFinYear>>) e).Cache.IsDirty = false;
  }

  protected virtual void _(PX.Data.Events.RowDeleting<OrganizationFinYear> e)
  {
    this.VerifyOrganizationFinYearForDelete(e.Row);
  }

  protected virtual void VerifyOrganizationFinYearForDelete(OrganizationFinYear organizationFinYear)
  {
    if (!this.IsFirstOrLastOrganizationFinYear(organizationFinYear))
      throw new PXException("Only first or last financial year can be deleted.");
    if (this.IsOrganizationFinYearUsed(organizationFinYear))
      throw new PXException("You cannot delete a Financial Period which has already been used.");
    using (new PXConnectionScope())
    {
      OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXViewOf<OrganizationFinPeriod>.BasedOn<SelectFromBase<OrganizationFinPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FATran>.On<BqlOperand<FATran.finPeriodID, IBqlString>.IsEqual<OrganizationFinPeriod.finPeriodID>>>, FbqlJoins.Inner<Branch>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.branchID, Equal<FATran.branchID>>>>>.Or<BqlOperand<Branch.branchID, IBqlInt>.IsEqual<FATran.srcBranchID>>>>>, FbqlJoins.Inner<FABook>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABook.bookID, Equal<FATran.bookID>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<OrganizationFinPeriod.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<OrganizationFinPeriod.finYear, IBqlString>.IsEqual<P.AsString>>>.Order<PX.Data.BQL.Fluent.By<Asc<OrganizationFinPeriod.finPeriodID>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
      {
        (object) organizationFinYear.OrganizationID,
        (object) organizationFinYear.Year
      }));
      if (organizationFinPeriod != null)
        throw new PXException("The {0} financial period cannot be deleted because at least one fixed asset transaction exists for this period in the posting book.", new object[1]
        {
          (object) PeriodIDAttribute.FormatForError(organizationFinPeriod.FinPeriodID)
        });
    }
  }

  private bool IsFirstOrLastOrganizationFinYear(OrganizationFinYear organizationFinYear)
  {
    PX.Objects.GL.FinPeriods.TableDefinition.FinYear firstYear = this.FinPeriodRepository.FindFirstYear(organizationFinYear.OrganizationID, true);
    PX.Objects.GL.FinPeriods.TableDefinition.FinYear lastYear = this.FinPeriodRepository.FindLastYear(organizationFinYear.OrganizationID, true);
    return organizationFinYear.Year == firstYear.Year || organizationFinYear.Year == lastYear.Year;
  }

  protected bool IsOrganizationFinYearUsed(OrganizationFinYear organizationFinYear)
  {
    return GraphHelper.RowCast<OrganizationFinPeriod>((IEnumerable) ((PXSelectBase<OrganizationFinPeriod>) this.OrgFinPeriods).Select(new object[2]
    {
      (object) organizationFinYear.OrganizationID,
      (object) organizationFinYear.Year
    })).Any<OrganizationFinPeriod>((Func<OrganizationFinPeriod, bool>) (period => period.Status == "Closed" || period.Status == "Locked" || period.DateLocked.GetValueOrDefault())) || this.IsOrganizationFinYearReferenced(organizationFinYear);
  }

  protected BqlCommand ReferencingByTranQuery
  {
    get
    {
      return this.referencingByTranQuery = this.referencingByTranQuery ?? PXSelectBase<GLTran, PXSelectJoin<GLTran, InnerJoin<Branch, On<GLTran.branchID, Equal<Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>, And<GLTran.finPeriodID, Equal<OrganizationFinPeriod.finPeriodID>>>>>>.Config>.GetCommand();
    }
  }

  protected BqlCommand ReferencingByBatchQuery
  {
    get
    {
      return this.referencingByBatchQuery = this.referencingByBatchQuery ?? PXSelectBase<Batch, PXSelectJoin<Batch, InnerJoin<Branch, On<Batch.branchID, Equal<Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>, And<Batch.finPeriodID, Equal<OrganizationFinPeriod.finPeriodID>>>>>>.Config>.GetCommand();
    }
  }

  private bool IsOrganizationFinYearReferenced(OrganizationFinYear organizationFinYear)
  {
    using (new PXConnectionScope())
    {
      GLTran glTran = this.ReferencingByTranQuery.WhereNew<Where<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinYear.organizationID>>, And<OrganizationFinPeriod.finYear, Equal<Required<OrganizationFinYear.year>>>>>().SelectSingleReadonly<GLTran>((PXGraph) this, (object) organizationFinYear.OrganizationID, (object) organizationFinYear.Year);
      return this.ReferencingByBatchQuery.WhereNew<Where<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinYear.organizationID>>, And<OrganizationFinPeriod.finYear, Equal<Required<OrganizationFinYear.year>>>>>().SelectSingleReadonly<Batch>((PXGraph) this, (object) organizationFinYear.OrganizationID, (object) organizationFinYear.Year) != null || glTran != null;
    }
  }

  protected virtual OrganizationFinYear GenerateSingleOrganizationFinYear(
    int organizationID,
    string startYearNumber,
    string startMasterFinPeriodID)
  {
    MasterFinYear masterFinYearById = this.FinPeriodRepository.FindMasterFinYearByID(startYearNumber);
    MasterFinPeriod masterFinPeriodById = this.FinPeriodRepository.FindMasterFinPeriodByID(startMasterFinPeriodID);
    return this.GenerateSingleOrganizationFinYear(organizationID, masterFinYearById, masterFinPeriodById);
  }

  /// <summary>
  /// TODO: Share function in <see cref="M:PX.Objects.CS.OrganizationMaint.CreateOrganizationCalendar(PX.Objects.GL.DAC.Organization,PX.Data.PXEntryStatus)" /> function
  /// </summary>
  /// <param name="organizationID"></param>
  /// <param name="masterFinPeriod"></param>
  /// <returns></returns>
  protected virtual OrganizationFinPeriod CopyOrganizationFinPeriodFromMaster(
    int organizationID,
    MasterFinPeriod masterFinPeriod,
    FinPeriod orgFinPeriodStatusSource,
    string yearNumber = null,
    string periodNumber = null)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>();
    string str = this.FinPeriodUtils.ComposeFinPeriodID(yearNumber, periodNumber) ?? masterFinPeriod.FinPeriodID;
    OrganizationFinPeriod organizationFinPeriod = new OrganizationFinPeriod()
    {
      OrganizationID = new int?(organizationID),
      FinPeriodID = str,
      MasterFinPeriodID = masterFinPeriod.FinPeriodID,
      FinYear = yearNumber ?? masterFinPeriod.FinYear,
      PeriodNbr = periodNumber ?? masterFinPeriod.PeriodNbr,
      Custom = masterFinPeriod.Custom,
      DateLocked = masterFinPeriod.DateLocked,
      StartDate = masterFinPeriod.StartDate,
      EndDate = masterFinPeriod.EndDate,
      Status = flag ? masterFinPeriod.Status : (orgFinPeriodStatusSource != null ? orgFinPeriodStatusSource.Status : "Inactive"),
      ARClosed = flag ? masterFinPeriod.ARClosed : (orgFinPeriodStatusSource != null ? orgFinPeriodStatusSource.ARClosed : new bool?(false)),
      APClosed = flag ? masterFinPeriod.APClosed : (orgFinPeriodStatusSource != null ? orgFinPeriodStatusSource.APClosed : new bool?(false)),
      FAClosed = flag ? masterFinPeriod.FAClosed : (orgFinPeriodStatusSource != null ? orgFinPeriodStatusSource.FAClosed : new bool?(false)),
      CAClosed = flag ? masterFinPeriod.CAClosed : (orgFinPeriodStatusSource != null ? orgFinPeriodStatusSource.CAClosed : new bool?(false)),
      INClosed = flag ? masterFinPeriod.INClosed : (orgFinPeriodStatusSource != null ? orgFinPeriodStatusSource.INClosed : new bool?(false)),
      Descr = masterFinPeriod.Descr
    };
    PXDBLocalizableStringAttribute.CopyTranslations<MasterFinPeriod.descr, OrganizationFinPeriod.descr>((PXGraph) this, (object) masterFinPeriod, (object) organizationFinPeriod);
    return organizationFinPeriod;
  }

  protected virtual OrganizationFinPeriod GenerateAdjustmentOrganizationFinPeriod(
    int organizationID,
    OrganizationFinPeriod prevFinPeriod)
  {
    string yearNumber1 = this.FinPeriodUtils.ParseFinPeriodID(prevFinPeriod.FinPeriodID).yearNumber;
    MasterFinYear masterFinYearById = this.FinPeriodRepository.FindMasterFinYearByID(yearNumber1, true);
    string finPeriodID = $"{yearNumber1:0000}{masterFinYearById.FinPeriods:00}";
    (string yearNumber, string periodNumber) finPeriodId = this.FinPeriodUtils.ParseFinPeriodID(prevFinPeriod.FinPeriodID);
    string yearNumber2 = finPeriodId.yearNumber;
    string periodNumber = $"{int.Parse(finPeriodId.periodNumber) + 1:00}";
    OrganizationFinPeriod organizationFinPeriod = new OrganizationFinPeriod()
    {
      OrganizationID = new int?(organizationID),
      FinPeriodID = this.FinPeriodUtils.ComposeFinPeriodID(yearNumber2, periodNumber),
      MasterFinPeriodID = finPeriodID,
      FinYear = yearNumber2,
      PeriodNbr = periodNumber,
      Custom = prevFinPeriod.Custom,
      DateLocked = prevFinPeriod.DateLocked,
      StartDate = prevFinPeriod.EndDate,
      EndDate = prevFinPeriod.EndDate,
      Status = prevFinPeriod.Status,
      ARClosed = prevFinPeriod.ARClosed,
      APClosed = prevFinPeriod.APClosed,
      FAClosed = prevFinPeriod.FAClosed,
      CAClosed = prevFinPeriod.CAClosed,
      INClosed = prevFinPeriod.INClosed,
      Descr = "Adjustment Period"
    };
    PXDBLocalizableStringAttribute.CopyTranslations<MasterFinPeriod.descr, OrganizationFinPeriod.descr>((PXGraph) this, (object) this.FinPeriodRepository.FindMasterFinPeriodByID(finPeriodID), (object) organizationFinPeriod);
    return organizationFinPeriod;
  }

  private MasterFinPeriodMaint MasterCalendarGraph
  {
    get
    {
      return this.masterCalendarGraph ?? (this.masterCalendarGraph = PXGraph.CreateInstance<MasterFinPeriodMaint>());
    }
  }

  protected void ClearMasterCalendarGraph()
  {
    Dictionary<string, WebDialogResult> dictionary = new Dictionary<string, WebDialogResult>();
    foreach (string key in ((Dictionary<string, PXView>) ((PXGraph) this.MasterCalendarGraph).Views).Keys.ToList<string>())
    {
      WebDialogResult answer = ((PXGraph) this.MasterCalendarGraph).Views[key].Answer;
      if (answer != null)
        dictionary.Add(key, answer);
    }
    ((PXGraph) this.MasterCalendarGraph).Clear();
    foreach (KeyValuePair<string, WebDialogResult> keyValuePair in dictionary)
    {
      PXView pxView;
      if (((Dictionary<string, PXView>) ((PXGraph) this.MasterCalendarGraph).Views).TryGetValue(keyValuePair.Key, out pxView))
        pxView.Answer = keyValuePair.Value;
    }
  }

  protected virtual OrganizationFinYear GenerateSingleOrganizationFinYear(
    int organizationID,
    MasterFinYear startMasterYear,
    MasterFinPeriod startMasterFinPeriod)
  {
    if (startMasterYear == null)
      throw new ArgumentNullException(nameof (startMasterYear));
    if (startMasterFinPeriod == null)
      throw new ArgumentNullException(nameof (startMasterFinPeriod));
    OrganizationFinYear organizationFinYear = GraphHelper.Caches<OrganizationFinYear>((PXGraph) this).Insert(new OrganizationFinYear()
    {
      OrganizationID = new int?(organizationID),
      Year = startMasterYear.Year,
      FinPeriods = startMasterYear.FinPeriods,
      StartMasterFinPeriodID = startMasterFinPeriod.FinPeriodID,
      StartDate = startMasterFinPeriod.StartDate
    });
    short num1 = 1;
    MasterFinPeriod masterFinPeriod = startMasterFinPeriod;
    int num2 = (int) organizationFinYear.FinPeriods.Value;
    bool? nullable = startMasterYear.CustomPeriods;
    bool flag1 = nullable.GetValueOrDefault() || ((PXSelectBase<FinYearSetup>) this.YearSetup).Current.PeriodType == "CN";
    int num3;
    if (!flag1)
    {
      nullable = ((PXSelectBase<FinYearSetup>) this.YearSetup).Current.HasAdjustmentPeriod;
      if (nullable.GetValueOrDefault())
      {
        num3 = 1;
        goto label_8;
      }
    }
    num3 = !flag1 ? 0 : (this.FinPeriodRepository.GetAdjustmentFinPeriods(startMasterYear.Year, new int?(0)).Any<FinPeriod>() ? 1 : 0);
label_8:
    bool flag2 = num3 != 0;
    if (flag2)
      --num2;
    OrganizationFinPeriod prevFinPeriod = (OrganizationFinPeriod) null;
    FinPeriod firstPeriod = this.FinPeriodRepository.FindFirstPeriod(new int?(organizationID), true);
    for (; (int) num1 <= num2; ++num1)
    {
      prevFinPeriod = GraphHelper.Caches<OrganizationFinPeriod>((PXGraph) this).Insert(this.CopyOrganizationFinPeriodFromMaster(organizationID, masterFinPeriod, firstPeriod == null || Convert.ToInt32(firstPeriod.FinYear) <= Convert.ToInt32(organizationFinYear.Year) ? (FinPeriod) null : firstPeriod, organizationFinYear.Year, $"{num1:00}"));
      if ((int) num1 < num2)
      {
        string finPeriodId = masterFinPeriod.FinPeriodID;
        while ((masterFinPeriod = this.FinPeriodRepository.FindNextNonAdjustmentMasterFinPeriod(finPeriodId, true)) == null)
        {
          this.ClearMasterCalendarGraph();
          this.MasterCalendarGraph.GenerateNextMasterFinYear();
        }
      }
    }
    organizationFinYear.EndDate = prevFinPeriod.EndDate;
    if (flag2)
      GraphHelper.Caches<OrganizationFinPeriod>((PXGraph) this).Insert(this.GenerateAdjustmentOrganizationFinPeriod(organizationID, prevFinPeriod));
    return organizationFinYear;
  }

  protected virtual OrganizationFinYear GenerateNextOrganizationFinYear(OrganizationFinYear year)
  {
    string str = $"{int.Parse(year.Year) + 1:0000}";
    MasterFinYear masterFinYearById;
    while ((masterFinYearById = this.FinPeriodRepository.FindMasterFinYearByID(str, true)) == null)
    {
      this.ClearMasterCalendarGraph();
      this.MasterCalendarGraph.GenerateCalendar(new int?(0), int.Parse(this.FinPeriodRepository.FindLastYear(new int?(0), true).Year), int.Parse(str));
    }
    short num1 = masterFinYearById.FinPeriods.Value;
    bool? nullable = masterFinYearById.CustomPeriods;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      nullable = ((PXSelectBase<FinYearSetup>) this.YearSetup).Current.HasAdjustmentPeriod;
      if (nullable.GetValueOrDefault())
        goto label_6;
    }
    nullable = masterFinYearById.CustomPeriods;
    if (!nullable.GetValueOrDefault() || !this.FinPeriodRepository.GetAdjustmentFinPeriods(masterFinYearById.Year, new int?(0)).Any<FinPeriod>())
      goto label_7;
label_6:
    --num1;
label_7:
    OrganizationFinPeriod organizationFinPeriodOfYear = this.FinPeriodRepository.FindLastNonAdjustmentOrganizationFinPeriodOfYear(year.OrganizationID, year.Year, true);
    int toYear = int.Parse(organizationFinPeriodOfYear.FinYear);
    PXSelectBase<MasterFinPeriod> pxSelectBase1 = (PXSelectBase<MasterFinPeriod>) new PXSelectReadonly<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Greater<Required<MasterFinPeriod.finPeriodID>>, And<MasterFinPeriod.startDate, NotEqual<MasterFinPeriod.endDate>>>, OrderBy<Asc<MasterFinPeriod.finPeriodID>>>((PXGraph) this);
    ((PXSelectBase) pxSelectBase1).View.Clear();
    List<MasterFinPeriod> list;
    while (true)
    {
      PXSelectBase<MasterFinPeriod> pxSelectBase2 = pxSelectBase1;
      int num2 = (int) num1;
      object[] objArray = new object[1]
      {
        (object) organizationFinPeriodOfYear.MasterFinPeriodID
      };
      if ((list = GraphHelper.RowCast<MasterFinPeriod>((IEnumerable) pxSelectBase2.SelectWindowed(0, num2, objArray)).ToList<MasterFinPeriod>()).Count < (int) num1)
      {
        ++toYear;
        this.ClearMasterCalendarGraph();
        this.MasterCalendarGraph.GenerateCalendar(new int?(0), int.Parse(this.FinPeriodRepository.FindLastYear(new int?(0), true).Year), toYear);
        ((PXSelectBase) pxSelectBase1).View.Clear();
      }
      else
        break;
    }
    MasterFinPeriod startMasterFinPeriod = list.First<MasterFinPeriod>();
    return this.GenerateSingleOrganizationFinYear(year.OrganizationID.Value, masterFinYearById, startMasterFinPeriod);
  }

  protected virtual OrganizationFinYear GeneratePreviousOrganizationFinYear(OrganizationFinYear year)
  {
    string str = $"{int.Parse(year.Year) - 1:0000}";
    MasterFinYear masterFinYearById;
    while ((masterFinYearById = this.FinPeriodRepository.FindMasterFinYearByID(str, true)) == null)
    {
      this.ClearMasterCalendarGraph();
      this.MasterCalendarGraph.GenerateCalendar(new int?(0), int.Parse(str), int.Parse(this.FinPeriodRepository.FindFirstYear(new int?(0), true).Year));
    }
    short num1 = masterFinYearById.FinPeriods.Value;
    bool? nullable = masterFinYearById.CustomPeriods;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      nullable = ((PXSelectBase<FinYearSetup>) this.YearSetup).Current.HasAdjustmentPeriod;
      if (nullable.GetValueOrDefault())
        goto label_6;
    }
    nullable = masterFinYearById.CustomPeriods;
    if (!nullable.GetValueOrDefault() || !this.FinPeriodRepository.GetAdjustmentFinPeriods(masterFinYearById.Year, new int?(0)).Any<FinPeriod>())
      goto label_7;
label_6:
    --num1;
label_7:
    int fromYear = int.Parse(PX.Objects.GL.FinPeriods.FinPeriodUtils.FiscalYear(year.StartMasterFinPeriodID));
    PXSelectBase<MasterFinPeriod> pxSelectBase1 = (PXSelectBase<MasterFinPeriod>) new PXSelectReadonly<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Less<Required<MasterFinPeriod.finPeriodID>>, And<MasterFinPeriod.startDate, NotEqual<MasterFinPeriod.endDate>>>, OrderBy<Desc<MasterFinPeriod.finPeriodID>>>((PXGraph) this);
    ((PXSelectBase) pxSelectBase1).View.Clear();
    List<MasterFinPeriod> list;
    while (true)
    {
      PXSelectBase<MasterFinPeriod> pxSelectBase2 = pxSelectBase1;
      int num2 = (int) num1;
      object[] objArray = new object[1]
      {
        (object) year.StartMasterFinPeriodID
      };
      if ((list = GraphHelper.RowCast<MasterFinPeriod>((IEnumerable) pxSelectBase2.SelectWindowed(0, num2, objArray)).ToList<MasterFinPeriod>()).Count < (int) num1)
      {
        --fromYear;
        this.ClearMasterCalendarGraph();
        this.MasterCalendarGraph.GenerateCalendar(new int?(0), fromYear, int.Parse(this.FinPeriodRepository.FindFirstYear(new int?(0), true).Year));
        ((PXSelectBase) pxSelectBase1).View.Clear();
      }
      else
        break;
    }
    MasterFinPeriod startMasterFinPeriod = list.Last<MasterFinPeriod>();
    return this.GenerateSingleOrganizationFinYear(year.OrganizationID.Value, masterFinYearById, startMasterFinPeriod);
  }

  public virtual void GenerateCalendar(int? organizationID, int fromYear, int toYear)
  {
    (int firstYear, int lastYear) = this.FinPeriodUtils.GetFirstLastYearForGeneration(organizationID, fromYear, toYear, true);
    OrganizationFinYear organizationFinYearById1 = this.FinPeriodRepository.FindOrganizationFinYearByID(organizationID, $"{firstYear:0000}", true);
    OrganizationFinYear organizationFinYearById2 = this.FinPeriodRepository.FindOrganizationFinYearByID(organizationID, $"{lastYear:0000}", true);
    OrganizationFinYear year1 = organizationFinYearById1;
    if (WebDialogResultExtension.IsNegative(((PXSelectBase) this.OrgFinPeriods).View.Answer))
      return;
    ((PXSelectBase) this.MasterCalendarGraph.Periods).View.Answer = ((PXSelectBase) this.OrgFinPeriods).View.Answer;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        if (fromYear < firstYear)
        {
          do
          {
            year1 = this.GeneratePreviousOrganizationFinYear(year1);
            ((PXGraph) this).Actions.PressSave();
          }
          while (year1 != null && string.CompareOrdinal(year1.Year, $"{fromYear:0000}") > 0);
        }
        OrganizationFinYear year2 = organizationFinYearById2;
        if (toYear > lastYear)
        {
          do
          {
            year2 = this.GenerateNextOrganizationFinYear(year2);
            ((PXGraph) this).Actions.PressSave();
            if (year2 == null)
              break;
          }
          while (string.CompareOrdinal(year2.Year, $"{toYear:0000}") < 0);
        }
      }
      catch (PXDialogRequiredException ex)
      {
        if (ex.Graph is MasterFinPeriodMaint && !string.IsNullOrEmpty(((Exception) ex).Message) && ex.ViewName == "Periods")
          ((PXSelectBase<OrganizationFinPeriod>) this.OrgFinPeriods).Ask(ex.Header, ((Exception) ex).Message, ex.Buttons, ex.Icon);
        throw;
      }
      transactionScope.Complete();
    }
  }

  [Serializable]
  public class NewOrganizationCalendarParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXUIField(DisplayName = "Organization", Enabled = false)]
    [Organization(true)]
    public virtual int? OrganizationID { get; set; }

    /// <summary>The number of the first financial year.</summary>
    [PXString(4, IsFixed = true)]
    [PXDefault(typeof (Search3<MasterFinYear.year, OrderBy<Asc<MasterFinYear.year>>>))]
    [PXUIField(DisplayName = "First Financial Year")]
    [PXSelector(typeof (Search<MasterFinYear.year>), new Type[] {typeof (MasterFinYear.year)})]
    public virtual string StartYear { get; set; }

    /// <summary>The start master period ID of the first year.</summary>
    [PXString]
    [PXUIField(DisplayName = "Master Period ID", Required = true)]
    [PXDefault]
    [FinPeriodSelector(typeof (Search<FinPeriod.finPeriodID, Where<FinPeriod.finYear, GreaterEqual<Sub<Current<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters.startYear>, int1>>, And<FinPeriod.finYear, LessEqual<Current<OrganizationFinPeriodMaint.NewOrganizationCalendarParameters.startYear>>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>>>>>), null, null, null, null, null, null, true, false, false, false, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, new Type[] {typeof (FinPeriod.startDate), typeof (FinPeriod.finPeriodID)}, null, true)]
    public virtual string StartMasterFinPeriodID { get; set; }

    /// <summary>The start date of master period ID of the first year.</summary>
    [PXDate]
    [PXDefault]
    [PXUIField(DisplayName = "Start Date", Enabled = false, Required = true)]
    public virtual DateTime? StartDate { get; set; }

    public abstract class organizationID : IBqlField, IBqlOperand
    {
    }

    public abstract class startYear : IBqlField, IBqlOperand
    {
    }

    public abstract class startMasterFinPeriodID : IBqlField, IBqlOperand
    {
    }

    public abstract class startDate : IBqlField, IBqlOperand
    {
    }
  }

  [Serializable]
  public class FinYearKey : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    public virtual int? OrganizationID { get; set; }

    [PXString]
    public virtual string Year { get; set; }

    public abstract class organizationID : IBqlField, IBqlOperand
    {
    }

    public abstract class year : IBqlField, IBqlOperand
    {
    }
  }

  public class OrganizationFinPeriodStatusActionsGraphExtension : 
    FinPeriodStatusActionsGraphBaseExtension<OrganizationFinPeriodMaint, OrganizationFinYear>
  {
  }

  public class GenerateOrganizationCalendarExtension : 
    GenerateCalendarExtensionBase<OrganizationFinPeriodMaint, OrganizationFinYear>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>();
    }
  }
}
