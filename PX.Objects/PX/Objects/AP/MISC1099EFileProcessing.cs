// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.MISC1099EFileProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.Overrides.APDocumentRelease;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.AP;

public class MISC1099EFileProcessing : PXGraph<MISC1099EFileProcessing>
{
  public PXCancel<MISC1099EFileFilter> Cancel;
  public PXFilter<MISC1099EFileFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingOrderBy<MISC1099EFileProcessingInfo, MISC1099EFileFilter, PX.Data.OrderBy<Asc<MISC1099EFileProcessingInfo.payerOrganizationID, Asc<MISC1099EFileProcessingInfo.payerBranchID, Asc<MISC1099EFileProcessingInfo.vendorID>>>>> Records;
  protected int RecordCounter;
  public Dictionary<string, string> combinedFederalOrStateCodes;
  public PXAction<MISC1099EFileFilter> View1099Summary;

  protected PX.Objects.GL.DAC.Organization TransmitterOrganization
  {
    get
    {
      PX.Objects.GL.DAC.Organization transmitterOrganization;
      this.OrganizationSlot.All1099.TryGetValue(this.Filter.Current.OrganizationID.GetValueOrDefault(), out transmitterOrganization);
      return transmitterOrganization;
    }
  }

  protected PX.Objects.GL.Branch TransmitterBranch
  {
    get
    {
      PX.Objects.GL.Branch transmitterBranch;
      this.AvailableBranches.TryGetValue(this.Filter.Current.BranchID.GetValueOrDefault(), out transmitterBranch);
      return transmitterBranch;
    }
  }

  protected YearFormat CalculateYearFormat(string year)
  {
    return string.Compare(year, "2021") >= 0 ? YearFormat.F2021 : YearFormat.F2020;
  }

  protected MISC1099EFileProcessing.AP1099OrganizationDefinition OrganizationSlot
  {
    get
    {
      return PXDatabase.GetSlot<MISC1099EFileProcessing.AP1099OrganizationDefinition, PXFilter<MISC1099EFileFilter>>(typeof (MISC1099EFileProcessing.AP1099OrganizationDefinition).FullName, this.Filter, typeof (PX.Objects.GL.DAC.Organization));
    }
  }

  protected virtual IDictionary<int, PX.Objects.GL.DAC.Organization> AvailableOrganizations
  {
    get => (IDictionary<int, PX.Objects.GL.DAC.Organization>) this.OrganizationSlot.ForReporting;
  }

  protected virtual int?[] MarkedOrganizationIDs
  {
    get
    {
      if (this.Filter.Current != null)
      {
        int? nullable1 = this.Filter.Current.OrganizationID;
        if (nullable1.HasValue)
        {
          if (this.Filter.Current.Include == "A")
            return (int?[]) null;
          int?[] markedOrganizationIds = new int?[1];
          PX.Objects.GL.DAC.Organization transmitterOrganization = this.TransmitterOrganization;
          int? nullable2;
          if (transmitterOrganization == null)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = transmitterOrganization.OrganizationID;
          markedOrganizationIds[0] = nullable2;
          return markedOrganizationIds;
        }
      }
      return new int?[0];
    }
  }

  protected virtual IDictionary<int, PX.Objects.GL.Branch> AvailableBranches
  {
    get
    {
      return (IDictionary<int, PX.Objects.GL.Branch>) PXDatabase.GetSlot<MISC1099EFileProcessing.AP1099BranchDefinition, PXFilter<MISC1099EFileFilter>>(typeof (MISC1099EFileProcessing.AP1099BranchDefinition).FullName, this.Filter, typeof (PX.Objects.GL.DAC.Organization), typeof (PX.Objects.GL.Branch)).Available;
    }
  }

  protected virtual int?[] MarkedBranchIDs
  {
    get
    {
      if (this.Filter.Current != null)
      {
        int? nullable1 = this.Filter.Current.OrganizationID;
        if (nullable1.HasValue)
        {
          List<int?> source1;
          if (this.Filter.Current.Include == "A")
          {
            HashSet<int> source2 = new HashSet<int>(this.AvailableBranches.Values.Select<PX.Objects.GL.Branch, int>((Func<PX.Objects.GL.Branch, int>) (b => b.BranchID.Value)));
            EnumerableExtensions.AddRange<int>((ISet<int>) source2, this.AvailableOrganizations.Keys.Select<int, int[]>((Func<int, int[]>) (orgID => PXAccess.GetChildBranchIDs(new int?(orgID), false))).SelectMany<int[], int>((Func<int[], IEnumerable<int>>) (b => (IEnumerable<int>) b)));
            source1 = source2.Cast<int?>().ToList<int?>();
          }
          else
          {
            List<int?> nullableList = new List<int?>();
            PX.Objects.GL.Branch transmitterBranch = this.TransmitterBranch;
            int? nullable2;
            if (transmitterBranch == null)
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = transmitterBranch.BranchID;
            nullableList.Add(nullable2);
            source1 = nullableList;
          }
          return !source1.Any<int?>((Func<int?, bool>) (id => id.HasValue)) ? (int?[]) null : source1.ToArray();
        }
      }
      return (int?[]) null;
    }
  }

  protected virtual MISC1099EFileProcessingInfoRaw AdjustOrganizationBranch(
    MISC1099EFileProcessingInfoRaw info)
  {
    int? payerOrganizationId = info.PayerOrganizationID;
    int? payerBranchId = info.PayerBranchID;
    I1099Settings obj = this.AdjustOrganizationBranch(ref payerOrganizationId, ref payerBranchId);
    info.PayerOrganizationID = payerOrganizationId;
    info.PayerBranchID = payerBranchId;
    info.PayerBAccountID = obj.BAccountID;
    return info;
  }

  protected virtual I1099Settings AdjustOrganizationBranch(
    ref int? organizationID,
    ref int? branchID)
  {
    PX.Objects.GL.DAC.Organization organization;
    this.AvailableOrganizations.TryGetValue(organizationID.GetValueOrDefault(), out organization);
    PX.Objects.GL.Branch branch;
    this.AvailableBranches.TryGetValue(branchID.GetValueOrDefault(), out branch);
    organizationID = new int?(((int?) organization?.OrganizationID).GetValueOrDefault());
    branchID = new int?(((int?) branch?.BranchID).GetValueOrDefault());
    if (!organizationID.HasValue && !branchID.HasValue)
      throw new PXException("'{0}' cannot be empty.");
    return (I1099Settings) organization ?? (I1099Settings) branch;
  }

  public IEnumerable records()
  {
    MISC1099EFileProcessing graph = this;
    graph.Caches<MISC1099EFileProcessingInfoRaw>().Clear();
    graph.Caches<MISC1099EFileProcessingInfoRaw>().ClearQueryCache();
    MISC1099EFileFilter current1 = graph.Filter.Current;
    int? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = current1.OrganizationID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 == 0)
    {
      PX.Objects.GL.DAC.Organization transmitterOrganization = graph.TransmitterOrganization;
      if ((transmitterOrganization != null ? (transmitterOrganization.Reporting1099ByBranches.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        MISC1099EFileFilter current2 = graph.Filter.Current;
        int num2;
        if (current2 == null)
        {
          num2 = 1;
        }
        else
        {
          nullable = current2.BranchID;
          num2 = !nullable.HasValue ? 1 : 0;
        }
        if (num2 != 0)
          yield break;
      }
      using (new PXReadBranchRestrictedScope(graph.MarkedOrganizationIDs, graph.MarkedBranchIDs))
      {
        // ISSUE: reference to a compiler-generated method
        foreach (MISC1099EFileProcessingInfo efileProcessingInfo in (IEnumerable<MISC1099EFileProcessingInfo>) PXSelectBase<MISC1099EFileProcessingInfoRaw, PXSelect<MISC1099EFileProcessingInfoRaw, Where<MISC1099EFileProcessingInfoRaw.finYear, Equal<Current<MISC1099EFileFilter.finYear>>, And<Current<MISC1099EFileFilter.organizationID>, PX.Data.IsNotNull>>>.Config>.Select((PXGraph) graph).RowCast<MISC1099EFileProcessingInfoRaw>().Select<MISC1099EFileProcessingInfoRaw, MISC1099EFileProcessingInfoRaw>(new Func<MISC1099EFileProcessingInfoRaw, MISC1099EFileProcessingInfoRaw>(graph.\u003Crecords\u003Eb__22_0)).GroupBy(rawInfo => new
        {
          PayerOrganizationID = rawInfo.PayerOrganizationID,
          PayerBranchID = rawInfo.PayerBranchID,
          VendorID = rawInfo.VendorID
        }).Select<IGrouping<\u003C\u003Ef__AnonymousType4<int?, int?, int?>, MISC1099EFileProcessingInfoRaw>, MISC1099EFileProcessingInfo>(group =>
        {
          MISC1099EFileProcessingInfo efileProcessingInfo = new MISC1099EFileProcessingInfo();
          efileProcessingInfo.PayerOrganizationID = group.Key.PayerOrganizationID;
          efileProcessingInfo.PayerBranchID = group.Key.PayerBranchID;
          efileProcessingInfo.PayerBAccountID = group.First<MISC1099EFileProcessingInfoRaw>().PayerBAccountID;
          int? payerOrganizationId = group.Key.PayerOrganizationID;
          int num3 = 0;
          efileProcessingInfo.DisplayOrganizationID = payerOrganizationId.GetValueOrDefault() > num3 & payerOrganizationId.HasValue ? group.Key.PayerOrganizationID : new int?();
          int? payerBranchId = group.Key.PayerBranchID;
          int num4 = 0;
          efileProcessingInfo.DisplayBranchID = payerBranchId.GetValueOrDefault() > num4 & payerBranchId.HasValue ? group.Key.PayerBranchID : new int?();
          efileProcessingInfo.BoxNbr = group.First<MISC1099EFileProcessingInfoRaw>().BoxNbr;
          efileProcessingInfo.FinYear = group.First<MISC1099EFileProcessingInfoRaw>().FinYear;
          efileProcessingInfo.VendorID = group.Key.VendorID;
          efileProcessingInfo.VAcctCD = group.First<MISC1099EFileProcessingInfoRaw>().VAcctCD;
          efileProcessingInfo.VAcctName = group.First<MISC1099EFileProcessingInfoRaw>().VAcctName;
          efileProcessingInfo.LTaxRegistrationID = group.First<MISC1099EFileProcessingInfoRaw>().LTaxRegistrationID;
          efileProcessingInfo.HistAmt = group.Sum<MISC1099EFileProcessingInfoRaw>((Func<MISC1099EFileProcessingInfoRaw, Decimal?>) (h => h.HistAmt));
          efileProcessingInfo.CountryID = group.First<MISC1099EFileProcessingInfoRaw>().CountryID;
          efileProcessingInfo.State = group.First<MISC1099EFileProcessingInfoRaw>().State;
          return efileProcessingInfo;
        }).ToArray<MISC1099EFileProcessingInfo>())
        {
          graph.Filter.Current.CountryID = efileProcessingInfo.CountryID;
          yield return (object) (graph.Records.Cache.Locate((object) efileProcessingInfo) as MISC1099EFileProcessingInfo) ?? graph.Records.Cache.Insert((object) efileProcessingInfo);
        }
      }
    }
  }

  public MISC1099EFileProcessing()
  {
    this.Records.SetProcessTooltip("Create e-file for selected vendors");
    this.Records.SetProcessAllTooltip("Create e-file for all vendors");
  }

  protected virtual void MISC1099EFileFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    MISC1099EFileFilter oldRow = (MISC1099EFileFilter) e.OldRow;
    MISC1099EFileFilter row = (MISC1099EFileFilter) e.Row;
    if (!(oldRow.FinYear != row.FinYear))
    {
      int? organizationId1 = oldRow.OrganizationID;
      int? organizationId2 = row.OrganizationID;
      if (organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue && !(oldRow.Box7 != row.Box7))
        return;
    }
    this.Records.Cache.Clear();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<MISC1099EFileFilter, MISC1099EFileFilter.finYear> e)
  {
    if (e.Row.FinYear == null || string.Compare(e.Row.FinYear, "2020") >= 0 || !(e.Row.FileFormat == "N"))
      return;
    e.Cache.SetDefaultExt<MISC1099EFileFilter.fileFormat>((object) e.Row);
  }

  protected virtual void MISC1099EFileFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    MISC1099EFileFilter rowfilter = e.Row as MISC1099EFileFilter;
    if (rowfilter == null)
      return;
    this.Records.SetProcessDelegate((PXProcessingBase<MISC1099EFileProcessingInfo>.ProcessListDelegate) (list => PXGraph.CreateInstance<MISC1099EFileProcessing>().Process(list, rowfilter)));
    if (rowfilter.Include == "A")
    {
      bool HaveBranches = false;
      string str = PXSelectBase<PX.Objects.GL.DAC.Organization, PXViewOf<PX.Objects.GL.DAC.Organization>.BasedOn<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.organizationID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.reporting1099, NotEqual<PX.Data.True>>>>>.Or<BqlOperand<PX.Objects.GL.DAC.Organization.reporting1099, IBqlBool>.IsNull>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.Branch.reporting1099, NotEqual<PX.Data.True>>>>>.Or<BqlOperand<PX.Objects.GL.Branch.reporting1099, IBqlBool>.IsNull>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>.Order<By<Desc<NullIf<PX.Objects.GL.DAC.Organization.organizationType, P.AsString>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 10, (object) "WithoutBranches").AsEnumerable<PXResult<PX.Objects.GL.DAC.Organization>>().Cast<PXResult<PX.Objects.GL.DAC.Organization, PX.Objects.GL.Branch>>().Select<PXResult<PX.Objects.GL.DAC.Organization, PX.Objects.GL.Branch>, string>((Func<PXResult<PX.Objects.GL.DAC.Organization, PX.Objects.GL.Branch>, string>) (row =>
      {
        PX.Objects.GL.DAC.Organization organization = (PX.Objects.GL.DAC.Organization) row;
        PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) row;
        HaveBranches |= organization.OrganizationType != "WithoutBranches";
        return branch.BranchCD;
      })).JoinToString<string>(", ");
      if (HaveBranches)
      {
        sender.RaiseExceptionHandling<MISC1099EFileFilter.include>((object) rowfilter, (object) rowfilter.Include, (Exception) new PXSetPropertyException("For the following branches or companies (or both), a 1099 history exists, but the 1099-MISC Reporting Entity check box is cleared on the Branches (CS102000) or Companies (CS101500) form: {0}. Information on these organizational entities cannot be shown in the 1099-MISC e-file.", PXErrorLevel.Warning, new object[1]
        {
          (object) str
        }));
      }
      else
      {
        PXCache pxCache = sender;
        MISC1099EFileFilter row = rowfilter;
        string include = rowfilter.Include;
        PXSetPropertyException propertyException;
        if (string.IsNullOrEmpty(str))
          propertyException = (PXSetPropertyException) null;
        else
          propertyException = new PXSetPropertyException("For the following company or companies, a 1099 history exists, but the 1099-MISC Reporting Entity check box is cleared on the Companies (CS101500) form: {0}. Information on these organizational entities cannot be shown in the 1099-MISC e-file.", PXErrorLevel.Warning, new object[1]
          {
            (object) str
          });
        pxCache.RaiseExceptionHandling<MISC1099EFileFilter.include>((object) row, (object) include, (Exception) propertyException);
      }
    }
    else
      sender.RaiseExceptionHandling<MISC1099EFileFilter.include>((object) rowfilter, (object) rowfilter.Include, (Exception) null);
    if (string.Compare(rowfilter.FinYear, "2020") < 0)
      PXStringListAttribute.SetList<MISC1099EFileFilter.fileFormat>(sender, (object) rowfilter, ("M", "MISC"));
    else
      PXStringListAttribute.SetList<MISC1099EFileFilter.fileFormat>(sender, (object) rowfilter, ("M", "MISC"), ("N", "NEC"));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<MISC1099EFileFilter, MISC1099EFileFilter.organizationID> e)
  {
    e.Row.BranchID = new int?();
  }

  protected virtual MISC1099EFileProcessing.Reporting1099Entity GetReportingEntity(
    int? organizationID,
    int? branchID)
  {
    MISC1099EFileProcessing.Reporting1099Entity reportingEntity = new MISC1099EFileProcessing.Reporting1099Entity()
    {
      Settings = this.AdjustOrganizationBranch(ref organizationID, ref branchID)
    };
    foreach (PXResult<PX.Objects.CR.BAccount, PX.Objects.CR.Contact, PX.Objects.CR.Address, LocationExtAddress> pxResult in PXSelectBase<PX.Objects.CR.BAccount, PXViewOf<PX.Objects.CR.BAccount>.BasedOn<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Contact>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.BAccount.defContactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>>, FbqlJoins.Left<PX.Objects.CR.Address>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.BAccount.defAddressID, IBqlInt>.IsEqual<PX.Objects.CR.Address.addressID>>>>, FbqlJoins.Left<LocationExtAddress>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.BAccount.bAccountID, Equal<LocationExtAddress.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.BAccount.defLocationID, IBqlInt>.IsEqual<LocationExtAddress.locationID>>>>>.Where<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], (object) reportingEntity.Settings.BAccountID))
    {
      reportingEntity.BAccount = (PX.Objects.CR.BAccount) pxResult;
      reportingEntity.Address = (PX.Objects.CR.Address) pxResult;
      reportingEntity.Contact = (PX.Objects.CR.Contact) pxResult;
      reportingEntity.Location = (LocationExtAddress) pxResult;
    }
    return reportingEntity;
  }

  public void Process(List<MISC1099EFileProcessingInfo> records, MISC1099EFileFilter filter)
  {
    if (filter.FileFormat == "M" || this.CalculateYearFormat(filter.FinYear) == YearFormat.F2021)
      this.FillCombinedFederalOrStateCodes();
    using (new PXReadBranchRestrictedScope(this.MarkedOrganizationIDs, this.MarkedBranchIDs, requireAccessForAllSpecified: true))
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (StreamWriter writer = new StreamWriter((Stream) memoryStream, Encoding.Unicode))
        {
          TransmitterTRecord transmitterRecord = this.CreateTransmitterRecord(this.GetReportingEntity(filter.OrganizationID, filter.BranchID), filter, 0);
          List<object> objectList = new List<object>()
          {
            (object) transmitterRecord
          };
          int totalPayeeB = 0;
          List<IGrouping<(int?, int?), MISC1099EFileProcessingInfo>> list1 = records.GroupBy<MISC1099EFileProcessingInfo, (int?, int?)>((Func<MISC1099EFileProcessingInfo, (int?, int?)>) (rec => (rec.PayerOrganizationID, rec.PayerBranchID))).ToList<IGrouping<(int?, int?), MISC1099EFileProcessingInfo>>();
          foreach (IGrouping<(int?, int?), MISC1099EFileProcessingInfo> source in list1)
          {
            MISC1099EFileProcessing.Reporting1099Entity reportingEntity = this.GetReportingEntity(source.Key.Item1, source.Key.Item2);
            PX.Objects.CR.Contact rowShipContact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.BAccount.defLocationID>>>>>.Config>.Select((PXGraph) this, (object) reportingEntity.BAccountID, (object) ((LocationExtAddress) reportingEntity).LocationID);
            PayerRecordA payerArecord = this.CreatePayerARecord(reportingEntity, rowShipContact, filter);
            if (payerArecord.CombinedFederalORStateFiler == "1" && source.All<MISC1099EFileProcessingInfo>((Func<MISC1099EFileProcessingInfo, bool>) (info => this.GetCombinedFederalOrStateCode(info.State) == string.Empty)))
              payerArecord.CombinedFederalORStateFiler = string.Empty;
            objectList.Add((object) payerArecord);
            List<PayeeRecordB> payeeRecordBList = new List<PayeeRecordB>();
            foreach (MISC1099EFileProcessingInfo efileProcessingInfo in (IEnumerable<MISC1099EFileProcessingInfo>) source)
            {
              PXProcessing<MISC1099EFileProcessingInfo>.SetCurrentItem((object) efileProcessingInfo);
              payeeRecordBList.Add(this.CreatePayeeBRecord((I1099Settings) reportingEntity, efileProcessingInfo, filter));
              PXProcessing<MISC1099EFileProcessingInfo>.SetProcessed();
            }
            List<PayeeRecordB> list2 = EnumerableExtensions.WhereNotNull<PayeeRecordB>((IEnumerable<PayeeRecordB>) payeeRecordBList).ToList<PayeeRecordB>();
            totalPayeeB += list2.Count;
            transmitterRecord.TotalNumberofPayees = list2.Count.ToString();
            objectList.AddRange((IEnumerable<object>) list2);
            objectList.Add((object) this.CreateEndOfPayerRecordC(list2));
            if (reportingEntity.CFSFiler.GetValueOrDefault() && (filter.FileFormat == "M" || this.CalculateYearFormat(filter.FinYear) == YearFormat.F2021))
              objectList.AddRange((IEnumerable<object>) list2.Where<PayeeRecordB>((Func<PayeeRecordB, bool>) (x => !string.IsNullOrWhiteSpace(x.PayeeState))).GroupBy<PayeeRecordB, string>((Func<PayeeRecordB, string>) (x => x.PayeeState.Trim()), (IEqualityComparer<string>) StringComparer.CurrentCultureIgnoreCase).Select<IGrouping<string, PayeeRecordB>, StateTotalsRecordK>((Func<IGrouping<string, PayeeRecordB>, StateTotalsRecordK>) (y => this.CreateStateTotalsRecordK(y.ToList<PayeeRecordB>()))).Where<StateTotalsRecordK>((Func<StateTotalsRecordK, bool>) (kRecord => kRecord != null)));
          }
          objectList.Add((object) this.CreateEndOfTransmissionRecordF(list1.Count, totalPayeeB));
          FixedLengthFile fixedLengthFile = new FixedLengthFile();
          foreach (I1099Record obj in objectList)
            obj.WriteToFile(writer, this.CalculateYearFormat(filter.FinYear));
          writer.Flush();
          throw new PXRedirectToFileException(new PX.SM.FileInfo($"1099-{PXStringListAttribute.GetLocalizedLabel<MISC1099EFileFilter.fileFormat>((PXCache) this.Caches<MISC1099EFileFilter>(), (object) filter)}.txt", (string) null, memoryStream.ToArray()), true);
        }
      }
    }
  }

  public virtual TransmitterTRecord CreateTransmitterRecord(
    MISC1099EFileProcessing.Reporting1099Entity entity,
    MISC1099EFileFilter filter,
    int totalPayeeB)
  {
    return this.CreateTransmitterRecord((I1099Settings) entity, (PX.Objects.CR.BAccount) entity, (PX.Objects.CR.Contact) entity, (PX.Objects.CR.Address) entity, filter, totalPayeeB);
  }

  protected TransmitterTRecord CreateTransmitterRecord(
    I1099Settings settings1099,
    PX.Objects.CR.BAccount bAccount,
    PX.Objects.CR.Contact rowMainContact,
    PX.Objects.CR.Address rowMainAddress,
    MISC1099EFileFilter filter,
    int totalPayeeB)
  {
    return new TransmitterTRecord()
    {
      RecordType = "T",
      PaymentYear = filter.FinYear,
      PriorYearDataIndicator = filter.IsPriorYear.GetValueOrDefault() ? "P" : string.Empty,
      TransmitterTIN = bAccount.TaxRegistrationID,
      TransmitterControlCode = settings1099.TCC,
      TestFileIndicator = filter.IsTestMode.GetValueOrDefault() ? "T" : string.Empty,
      ForeignEntityIndicator = settings1099.ForeignEntity.GetValueOrDefault() ? "1" : string.Empty,
      TransmitterName = bAccount.LegalName.Trim(),
      CompanyName = bAccount.LegalName.Trim(),
      CompanyMailingAddress = rowMainAddress.AddressLine1 + rowMainAddress.AddressLine2,
      CompanyCity = rowMainAddress.City,
      CompanyState = rowMainAddress.State,
      CompanyZipCode = rowMainAddress.PostalCode,
      TotalNumberofPayees = totalPayeeB.ToString(),
      ContactName = settings1099.ContactName,
      ContactTelephoneAndExt = settings1099.CTelNumber,
      ContactEmailAddress = settings1099.CEmail,
      RecordSequenceNumber = (++this.RecordCounter).ToString(),
      VendorIndicator = "V",
      VendorName = "Project X, Inc. (Acumatica)",
      VendorMailingAddress = "011235 SE 6th St.Suite 140",
      VendorCity = "Bellevue",
      VendorState = "WA",
      VendorZipCode = "98004",
      VendorContactName = "Acumatica Support",
      VendorContactTelephoneAndExt = "7038737570",
      VendorForeignEntityIndicator = ""
    };
  }

  public virtual PayerRecordA CreatePayerARecord(
    MISC1099EFileProcessing.Reporting1099Entity entity,
    PX.Objects.CR.Contact rowShipContact,
    MISC1099EFileFilter filter)
  {
    return this.CreatePayerARecord((I1099Settings) entity, (PX.Objects.CR.BAccount) entity, (PX.Objects.CR.Contact) entity, (PX.Objects.CR.Address) entity, (LocationExtAddress) entity, rowShipContact, filter);
  }

  public PayerRecordA CreatePayerARecord(
    I1099Settings settings1099,
    PX.Objects.CR.BAccount bAccount,
    PX.Objects.CR.Contact rowMainContact,
    PX.Objects.CR.Address rowMainAddress,
    LocationExtAddress rowShipInfo,
    PX.Objects.CR.Contact rowShipContact,
    MISC1099EFileFilter filter)
  {
    string str1 = bAccount.LegalName.Trim();
    string str2 = string.Empty;
    if (str1.Length > 40)
    {
      str2 = str1.Substring(40);
      str1 = str1.Substring(0, 40);
    }
    string str3;
    bool? nullable;
    string str4;
    switch (filter.FileFormat)
    {
      case "M":
        str3 = "A";
        if (string.Compare(filter.FinYear, "2021") < 0)
        {
          nullable = filter.ReportingDirectSalesOnly;
          str4 = nullable.GetValueOrDefault() ? "1" : "1234568ABCDE";
          if (string.Compare(filter.FinYear, "2020") < 0)
          {
            str4 += "G";
            break;
          }
          break;
        }
        nullable = filter.ReportingDirectSalesOnly;
        str4 = nullable.GetValueOrDefault() ? "1" : "1234568ABCDEF";
        break;
      case "N":
        str3 = "NE";
        nullable = filter.ReportingDirectSalesOnly;
        str4 = nullable.GetValueOrDefault() ? "1" : "14";
        break;
      default:
        throw new PXException("The 1099 e-file format {0} is not supported.", new object[1]
        {
          (object) PXStringListAttribute.GetLocalizedLabel<MISC1099EFileFilter.fileFormat>((PXCache) this.Caches<MISC1099EFileFilter>(), (object) filter)
        });
    }
    PayerRecordA payerArecord = new PayerRecordA();
    payerArecord.RecordType = "A";
    payerArecord.PaymentYear = filter.FinYear;
    nullable = settings1099.CFSFiler;
    payerArecord.CombinedFederalORStateFiler = !nullable.GetValueOrDefault() || this.CalculateYearFormat(filter.FinYear) != YearFormat.F2021 && !(filter.FileFormat == "M") ? string.Empty : "1";
    payerArecord.PayerTaxpayerIdentificationNumberTIN = bAccount.TaxRegistrationID;
    payerArecord.PayerNameControl = settings1099.NameControl;
    nullable = filter.IsLastFiling;
    payerArecord.LastFilingIndicator = nullable.GetValueOrDefault() ? "1" : string.Empty;
    payerArecord.TypeofReturn = str3;
    payerArecord.AmountCodes = str4;
    nullable = settings1099.ForeignEntity;
    payerArecord.ForeignEntityIndicator = nullable.GetValueOrDefault() ? "1" : string.Empty;
    payerArecord.FirstPayerNameLine = str1;
    payerArecord.SecondPayerNameLine = str2;
    payerArecord.TransferAgentIndicator = "0";
    payerArecord.PayerShippingAddress = rowShipInfo.AddressLine1 + rowShipInfo.AddressLine2;
    payerArecord.PayerCity = rowShipInfo.City;
    payerArecord.PayerState = rowShipInfo.State;
    payerArecord.PayerZipCode = rowShipInfo.PostalCode;
    payerArecord.PayerTelephoneAndExt = rowShipContact.Phone1;
    payerArecord.RecordSequenceNumber = (++this.RecordCounter).ToString();
    return payerArecord;
  }

  public PayeeRecordB CreatePayeeBRecord(
    I1099Settings settings1099,
    MISC1099EFileProcessingInfo record1099,
    MISC1099EFileFilter filter)
  {
    this.Caches<AP1099History>().ClearQueryCache();
    int? nullable1 = record1099.DisplayOrganizationID;
    ref int? local1 = ref nullable1;
    int?[] array1 = local1.HasValue ? local1.GetValueOrDefault().SingleToArray<int>().Cast<int?>().ToArray<int?>() : (int?[]) null;
    nullable1 = record1099.DisplayBranchID;
    ref int? local2 = ref nullable1;
    int?[] array2 = local2.HasValue ? local2.GetValueOrDefault().SingleToArray<int>().Cast<int?>().ToArray<int?>() : (int?[]) null;
    using (new PXReadBranchRestrictedScope(array1, array2, requireAccessForAllSpecified: true))
    {
      VendorR vendorR = (VendorR) PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Required<VendorR.bAccountID>>>>.Config>.Select((PXGraph) this, (object) record1099.VendorID);
      PX.Objects.CR.Address address = (PX.Objects.CR.Address) PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.BAccount.defAddressID>>>>>.Config>.Select((PXGraph) this, (object) vendorR.BAccountID, (object) vendorR.DefAddressID);
      LocationExtAddress locationExtAddress = (LocationExtAddress) PXSelectBase<LocationExtAddress, PXSelect<LocationExtAddress, Where<LocationExtAddress.locationBAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>, And<LocationExtAddress.locationID, Equal<Required<PX.Objects.CR.BAccount.defLocationID>>>>>.Config>.Select((PXGraph) this, (object) vendorR.BAccountID, (object) vendorR.DefLocationID);
      List<AP1099History> list = PXSelectBase<AP1099History, PXSelectJoinGroupBy<AP1099History, InnerJoin<AP1099Box, On<AP1099History.boxNbr, Equal<AP1099Box.boxNbr>>>, Where<AP1099History.vendorID, Equal<Required<AP1099History.vendorID>>, And<AP1099History.finYear, Equal<Required<AP1099History.finYear>>, PX.Data.And<Where<Required<MISC1099EFileFilter.box7>, Equal<MISC1099EFileFilter.box7.box7All>, Or<Required<MISC1099EFileFilter.box7>, Equal<MISC1099EFileFilter.box7.box7Equal>, And<AP1099History.boxNbr, Equal<MISC1099EFileFilter.box7.box7Nbr>, Or<Required<MISC1099EFileFilter.box7>, Equal<MISC1099EFileFilter.box7.box7NotEqual>, And<AP1099History.boxNbr, NotEqual<MISC1099EFileFilter.box7.box7Nbr>>>>>>>>>, PX.Data.Aggregate<GroupBy<AP1099History.boxNbr, Sum<AP1099History.histAmt>>>>.Config>.Select((PXGraph) this, (object) record1099.VendorID, (object) filter.FinYear, (object) filter.Box7, (object) filter.Box7, (object) filter.Box7).AsEnumerable<PXResult<AP1099History>>().Where<PXResult<AP1099History>>((Func<PXResult<AP1099History>, bool>) (res =>
      {
        Decimal? histAmt = res.GetItem<AP1099History>().HistAmt;
        Decimal? minReportAmt = res.GetItem<AP1099Box>().MinReportAmt;
        return histAmt.GetValueOrDefault() >= minReportAmt.GetValueOrDefault() & histAmt.HasValue & minReportAmt.HasValue;
      })).RowCast<AP1099History>().ToList<AP1099History>();
      APSetup apSetup = (APSetup) PXSetup<APSetup>.Select((PXGraph) this, Array.Empty<object>());
      if (list.Sum<AP1099History>((Func<AP1099History, Decimal?>) (hist => hist.HistAmt)).GetValueOrDefault() == 0M)
        return (PayeeRecordB) null;
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      Decimal num3 = 0M;
      Decimal num4 = 0M;
      Decimal num5 = 0M;
      Decimal num6 = 0M;
      Decimal num7 = 0M;
      Decimal num8 = 0M;
      Decimal num9 = 0M;
      Decimal num10 = 0M;
      Decimal num11 = 0M;
      Decimal num12 = 0M;
      Decimal num13 = 0M;
      bool flag1 = list.Where<AP1099History>((Func<AP1099History, bool>) (hist =>
      {
        short? boxNbr1 = hist.BoxNbr;
        if ((boxNbr1.HasValue ? new int?((int) boxNbr1.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 4)
          return true;
        short? boxNbr2 = hist.BoxNbr;
        return (boxNbr2.HasValue ? new int?((int) boxNbr2.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 7;
      })).Sum<AP1099History>((Func<AP1099History, Decimal?>) (hist => hist.HistAmt)).GetValueOrDefault() > 0M;
      bool flag2 = list.Where<AP1099History>((Func<AP1099History, bool>) (hist =>
      {
        short? boxNbr3 = hist.BoxNbr;
        if ((boxNbr3.HasValue ? new int?((int) boxNbr3.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 1)
        {
          short? boxNbr4 = hist.BoxNbr;
          if ((boxNbr4.HasValue ? new int?((int) boxNbr4.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 2)
          {
            short? boxNbr5 = hist.BoxNbr;
            if ((boxNbr5.HasValue ? new int?((int) boxNbr5.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 3)
            {
              short? boxNbr6 = hist.BoxNbr;
              if ((boxNbr6.HasValue ? new int?((int) boxNbr6.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 5)
              {
                short? boxNbr7 = hist.BoxNbr;
                if ((boxNbr7.HasValue ? new int?((int) boxNbr7.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 6)
                {
                  if (this.CalculateYearFormat(filter.FinYear) == YearFormat.F2020)
                  {
                    short? boxNbr8 = hist.BoxNbr;
                    if ((boxNbr8.HasValue ? new int?((int) boxNbr8.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 7)
                      goto label_12;
                  }
                  short? boxNbr9 = hist.BoxNbr;
                  if ((boxNbr9.HasValue ? new int?((int) boxNbr9.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 8)
                  {
                    short? boxNbr10 = hist.BoxNbr;
                    if ((boxNbr10.HasValue ? new int?((int) boxNbr10.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 10)
                    {
                      short? boxNbr11 = hist.BoxNbr;
                      if ((boxNbr11.HasValue ? new int?((int) boxNbr11.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 11)
                      {
                        short? boxNbr12 = hist.BoxNbr;
                        if ((boxNbr12.HasValue ? new int?((int) boxNbr12.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 13)
                        {
                          short? boxNbr13 = hist.BoxNbr;
                          return (boxNbr13.HasValue ? new int?((int) boxNbr13.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 14;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
label_12:
        return true;
      })).Sum<AP1099History>((Func<AP1099History, Decimal?>) (hist => hist.HistAmt)).GetValueOrDefault() > 0M;
      bool flag3 = apSetup.PrintDirectSalesOn == "MA" || apSetup.PrintDirectSalesOn == "MF" && (flag2 || !flag1) || apSetup.PrintDirectSalesOn == "NF" & flag2 && !flag1;
      bool flag4 = !flag3;
      bool? nullable2;
      Decimal num14;
      Decimal num15;
      Decimal num16;
      switch (filter.FileFormat)
      {
        case "M":
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num17;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1;
            }));
            if (ap1099History == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History = (AP1099History) ap1099Hist;
            }
            num17 = System.Math.Round(ap1099History.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num17 = 0M;
          num14 = num17;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num18;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 2;
            }));
            if (ap1099History == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History = (AP1099History) ap1099Hist;
            }
            num18 = System.Math.Round(ap1099History.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num18 = 0M;
          num1 = num18;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num19;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 3;
            }));
            if (ap1099History == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History = (AP1099History) ap1099Hist;
            }
            num19 = System.Math.Round(ap1099History.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num19 = 0M;
          num2 = num19;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num20;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 4;
            }));
            if (ap1099History == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History = (AP1099History) ap1099Hist;
            }
            num20 = System.Math.Round(ap1099History.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num20 = 0M;
          num15 = num20;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num21;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 5;
            }));
            if (ap1099History == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History = (AP1099History) ap1099Hist;
            }
            num21 = System.Math.Round(ap1099History.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num21 = 0M;
          num3 = num21;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num22;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 6;
            }));
            if (ap1099History == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History = (AP1099History) ap1099Hist;
            }
            num22 = System.Math.Round(ap1099History.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num22 = 0M;
          num4 = num22;
          num5 = 0M;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num23;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 8;
            }));
            if (ap1099History == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History = (AP1099History) ap1099Hist;
            }
            num23 = System.Math.Round(ap1099History.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num23 = 0M;
          num6 = num23;
          AP1099History ap1099History1 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
          {
            if (v == null)
              return false;
            short? boxNbr = v.BoxNbr;
            return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 9;
          }));
          if (ap1099History1 == null)
          {
            AP1099Hist ap1099Hist = new AP1099Hist();
            ap1099Hist.HistAmt = new Decimal?(0M);
            ap1099History1 = (AP1099History) ap1099Hist;
          }
          Decimal num24 = System.Math.Round(ap1099History1.HistAmt.GetValueOrDefault(), 2);
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num25;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History2 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 10;
            }));
            if (ap1099History2 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History2 = (AP1099History) ap1099Hist;
            }
            num25 = System.Math.Round(ap1099History2.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num25 = 0M;
          num7 = num25;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num26;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History3 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 13;
            }));
            if (ap1099History3 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History3 = (AP1099History) ap1099Hist;
            }
            num26 = System.Math.Round(ap1099History3.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num26 = 0M;
          num8 = num26;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num27;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History4 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 14;
            }));
            if (ap1099History4 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History4 = (AP1099History) ap1099Hist;
            }
            num27 = System.Math.Round(ap1099History4.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num27 = 0M;
          num9 = num27;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num28;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History5 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 151;
            }));
            if (ap1099History5 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History5 = (AP1099History) ap1099Hist;
            }
            num28 = System.Math.Round(ap1099History5.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num28 = 0M;
          num10 = num28;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num29;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History6 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 152;
            }));
            if (ap1099History6 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History6 = (AP1099History) ap1099Hist;
            }
            num29 = System.Math.Round(ap1099History6.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num29 = 0M;
          num11 = num29;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num30;
          if (!nullable2.GetValueOrDefault() && this.CalculateYearFormat(filter.FinYear) != YearFormat.F2020)
          {
            AP1099History ap1099History7 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 11;
            }));
            if (ap1099History7 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History7 = (AP1099History) ap1099Hist;
            }
            num30 = System.Math.Round(ap1099History7.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num30 = 0M;
          num12 = num30;
          Decimal num31;
          if (string.Compare(filter.FinYear, "2020") >= 0)
          {
            num31 = 0M;
          }
          else
          {
            nullable2 = filter.ReportingDirectSalesOnly;
            if (!nullable2.GetValueOrDefault())
            {
              AP1099History ap1099History8 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
              {
                if (v == null)
                  return false;
                short? boxNbr = v.BoxNbr;
                return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 7;
              }));
              if (ap1099History8 == null)
              {
                AP1099Hist ap1099Hist = new AP1099Hist();
                ap1099Hist.HistAmt = new Decimal?(0M);
                ap1099History8 = (AP1099History) ap1099Hist;
              }
              num31 = System.Math.Round(ap1099History8.HistAmt.GetValueOrDefault(), 2);
            }
            else
              num31 = 0M;
          }
          num13 = num31;
          Decimal num32 = num14 + num1 + num2 + num15 + num3 + num4 + num6 + (flag3 ? num24 : 0M) + num7 + num8 + num9 + num10 + num11 + num12 + num13;
          if (string.Compare(filter.FinYear, "2020") >= 0 && num32 == 0M)
            return (PayeeRecordB) null;
          num16 = 0M;
          break;
        case "N":
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num33;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History9 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 7;
            }));
            if (ap1099History9 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History9 = (AP1099History) ap1099Hist;
            }
            num33 = System.Math.Round(ap1099History9.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num33 = 0M;
          num14 = num33;
          nullable2 = filter.ReportingDirectSalesOnly;
          Decimal num34;
          if (!nullable2.GetValueOrDefault())
          {
            AP1099History ap1099History10 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 4;
            }));
            if (ap1099History10 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History10 = (AP1099History) ap1099Hist;
            }
            num34 = System.Math.Round(ap1099History10.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num34 = 0M;
          num15 = num34;
          Decimal num35;
          if (this.CalculateYearFormat(filter.FinYear) != YearFormat.F2020)
          {
            AP1099History ap1099History11 = list.FirstOrDefault<AP1099History>((Func<AP1099History, bool>) (v =>
            {
              if (v == null)
                return false;
              short? boxNbr = v.BoxNbr;
              return (boxNbr.HasValue ? new int?((int) boxNbr.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 9;
            }));
            if (ap1099History11 == null)
            {
              AP1099Hist ap1099Hist = new AP1099Hist();
              ap1099Hist.HistAmt = new Decimal?(0M);
              ap1099History11 = (AP1099History) ap1099Hist;
            }
            num35 = System.Math.Round(ap1099History11.HistAmt.GetValueOrDefault(), 2);
          }
          else
            num35 = 0M;
          Decimal num36 = num35;
          if (num14 + num15 + (flag4 ? num36 : 0M) == 0M)
            return (PayeeRecordB) null;
          num16 = 0M;
          break;
        default:
          throw new PXException("The 1099 e-file format {0} is not supported.", new object[1]
          {
            (object) PXStringListAttribute.GetLocalizedLabel<MISC1099EFileFilter.fileFormat>((PXCache) this.Caches<MISC1099EFileFilter>(), (object) filter)
          });
      }
      string str1 = string.Empty;
      switch (vendorR.TinType)
      {
        case "E":
          str1 = "1";
          break;
        case "S":
        case "I":
        case "A":
          str1 = "2";
          break;
      }
      PayeeRecordB payeeBrecord = new PayeeRecordB();
      payeeBrecord.RecordType = "B";
      payeeBrecord.PaymentYear = filter.FinYear;
      nullable2 = filter.IsCorrectionReturn;
      payeeBrecord.CorrectedReturnIndicator = nullable2.GetValueOrDefault() ? "G" : string.Empty;
      payeeBrecord.NameControl = string.Empty;
      payeeBrecord.TypeOfTIN = str1;
      payeeBrecord.PayerTaxpayerIdentificationNumberTIN = locationExtAddress.TaxRegistrationID;
      payeeBrecord.PayerAccountNumberForPayee = vendorR.AcctCD;
      payeeBrecord.PayerOfficeCode = string.Empty;
      payeeBrecord.PaymentAmount1 = num14;
      payeeBrecord.PaymentAmount2 = num1;
      payeeBrecord.PaymentAmount3 = num2;
      payeeBrecord.PaymentAmount4 = num15;
      payeeBrecord.PaymentAmount5 = num3;
      payeeBrecord.PaymentAmount6 = num4;
      payeeBrecord.PaymentAmount7 = num5;
      payeeBrecord.PaymentAmount8 = num6;
      payeeBrecord.PaymentAmount9 = num16;
      payeeBrecord.PaymentAmountA = num7;
      payeeBrecord.PaymentAmountB = num8;
      payeeBrecord.PaymentAmountC = num9;
      payeeBrecord.Payment = num10;
      payeeBrecord.PaymentAmountE = num11;
      payeeBrecord.PaymentAmountF = num12;
      payeeBrecord.PaymentAmountG = num13;
      nullable2 = vendorR.ForeignEntity;
      payeeBrecord.ForeignCountryIndicator = nullable2.GetValueOrDefault() ? "1" : string.Empty;
      payeeBrecord.PayeeNameLine = vendorR.LegalName;
      payeeBrecord.PayeeMailingAddress = address.AddressLine1 + address.AddressLine2;
      payeeBrecord.PayeeCity = address.City;
      payeeBrecord.PayeeState = address.State;
      payeeBrecord.PayeeZipCode = address.PostalCode;
      payeeBrecord.RecordSequenceNumber = (++this.RecordCounter).ToString();
      payeeBrecord.SecondTINNotice = string.Empty;
      string str2;
      if ((this.CalculateYearFormat(filter.FinYear) != YearFormat.F2020 || !string.Equals(filter.FileFormat, "M")) && ((this.CalculateYearFormat(filter.FinYear) != YearFormat.F2021 ? 0 : (string.Equals(filter.FileFormat, "M") ? 1 : 0)) & (flag3 ? 1 : 0)) == 0 && ((this.CalculateYearFormat(filter.FinYear) != YearFormat.F2021 ? 0 : (string.Equals(filter.FileFormat, "N") ? 1 : 0)) & (flag4 ? 1 : 0)) == 0)
      {
        str2 = string.Empty;
      }
      else
      {
        nullable1 = record1099.VendorID;
        str2 = this.GetDirectSaleIndicator(nullable1.Value, filter.FinYear);
      }
      payeeBrecord.DirectSalesIndicator = str2;
      nullable2 = vendorR.FATCA;
      payeeBrecord.FATCA = !nullable2.GetValueOrDefault() || this.CalculateYearFormat(filter.FinYear) != YearFormat.F2020 && !string.Equals(filter.FileFormat, "M") ? string.Empty : "1";
      payeeBrecord.SpecialDataEntries = string.Empty;
      payeeBrecord.StateIncomeTaxWithheld = string.Empty;
      payeeBrecord.LocalIncomeTaxWithheld = string.Empty;
      nullable2 = settings1099.CFSFiler;
      payeeBrecord.CombineFederalOrStateCode = !nullable2.GetValueOrDefault() || !(filter.FileFormat == "M") && this.CalculateYearFormat(filter.FinYear) != YearFormat.F2021 ? string.Empty : this.GetCombinedFederalOrStateCode(address.State);
      return payeeBrecord;
    }
  }

  public EndOfPayerRecordC CreateEndOfPayerRecordC(List<PayeeRecordB> listPayeeB)
  {
    return new EndOfPayerRecordC()
    {
      RecordType = "C",
      NumberOfPayees = Convert.ToString(listPayeeB.Count),
      ControlTotal1 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount1)), 2),
      ControlTotal2 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount2)), 2),
      ControlTotal3 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount3)), 2),
      ControlTotal4 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount4)), 2),
      ControlTotal5 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount5)), 2),
      ControlTotal6 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount6)), 2),
      ControlTotal7 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount7)), 2),
      ControlTotal8 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount8)), 2),
      ControlTotal9 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount9)), 2),
      ControlTotalA = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountA)), 2),
      ControlTotalB = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountB)), 2),
      ControlTotalC = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountC)), 2),
      ControlTotalD = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.Payment)), 2),
      ControlTotalE = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountE)), 2),
      ControlTotalF = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountF)), 2),
      ControlTotalG = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountG)), 2),
      RecordSequenceNumber = (++this.RecordCounter).ToString()
    };
  }

  public StateTotalsRecordK CreateStateTotalsRecordK(List<PayeeRecordB> listPayeeB)
  {
    if (listPayeeB == null)
      return (StateTotalsRecordK) null;
    string federalOrStateCode = this.GetCombinedFederalOrStateCode((listPayeeB.FirstOrDefault<PayeeRecordB>() ?? new PayeeRecordB()).PayeeState);
    if (string.IsNullOrEmpty(federalOrStateCode))
      return (StateTotalsRecordK) null;
    return new StateTotalsRecordK()
    {
      RecordType = "K",
      NumberOfPayees = Convert.ToString(listPayeeB.Count),
      ControlTotal1 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount1)), 2),
      ControlTotal2 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount2)), 2),
      ControlTotal3 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount3)), 2),
      ControlTotal4 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount4)), 2),
      ControlTotal5 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount5)), 2),
      ControlTotal6 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount6)), 2),
      ControlTotal7 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount7)), 2),
      ControlTotal8 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount8)), 2),
      ControlTotal9 = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmount9)), 2),
      ControlTotalA = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountA)), 2),
      ControlTotalB = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountB)), 2),
      ControlTotalC = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountC)), 2),
      ControlTotalD = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.Payment)), 2),
      ControlTotalE = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountE)), 2),
      ControlTotalF = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountF)), 2),
      ControlTotalG = System.Math.Round(listPayeeB.Sum<PayeeRecordB>((Func<PayeeRecordB, Decimal>) (brec => brec.PaymentAmountG)), 2),
      RecordSequenceNumber = (++this.RecordCounter).ToString(),
      StateIncomeTaxWithheldTotal = 0M,
      LocalIncomeTaxWithheldTotal = 0M,
      CombinedFederalOrStateCode = federalOrStateCode
    };
  }

  public EndOfTransmissionRecordF CreateEndOfTransmissionRecordF(int totalPayerA, int totalPayeeB)
  {
    return new EndOfTransmissionRecordF()
    {
      RecordType = "F",
      NumberOfARecords = totalPayerA.ToString(),
      TotalNumberOfPayees = totalPayeeB.ToString(),
      RecordSequenceNumber = (++this.RecordCounter).ToString()
    };
  }

  [PXUIField(DisplayName = "View 1099 Vendor History", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual IEnumerable view1099Summary(PXAdapter adapter)
  {
    if (this.Records.Current != null)
    {
      AP1099DetailEnq instance = PXGraph.CreateInstance<AP1099DetailEnq>();
      instance.YearVendorHeader.Current.FinYear = this.Records.Current.FinYear;
      instance.YearVendorHeader.Current.VendorID = this.Records.Current.VendorID;
      PXFieldState valueExt = this.Records.Cache.GetValueExt<MISC1099EFileProcessingInfo.payerBAccountID>(this.Records.Cache.Current) as PXFieldState;
      instance.YearVendorHeader.Cache.SetValueExt<AP1099YearMaster.orgBAccountID>((object) instance.YearVendorHeader.Current, valueExt.Value);
      throw new PXRedirectRequiredException((PXGraph) instance, true, "1099 Year Vendor History");
    }
    return adapter.Get();
  }

  protected virtual string GetCombinedFederalOrStateCode(string stateAbbrCode)
  {
    stateAbbrCode = stateAbbrCode ?? string.Empty;
    string empty;
    if (!this.combinedFederalOrStateCodes.TryGetValue(stateAbbrCode.Trim().ToUpper(), out empty))
      empty = string.Empty;
    return empty;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  protected virtual void FillCombinedFederalOrStateCodes(string finYear)
  {
    this.FillCombinedFederalOrStateCodes();
  }

  protected virtual void FillCombinedFederalOrStateCodes()
  {
    this.combinedFederalOrStateCodes = new Dictionary<string, string>();
    this.combinedFederalOrStateCodes.Add("AL", "01");
    this.combinedFederalOrStateCodes.Add("AZ", "04");
    this.combinedFederalOrStateCodes.Add("AR", "05");
    this.combinedFederalOrStateCodes.Add("CA", "06");
    this.combinedFederalOrStateCodes.Add("CO", "07");
    this.combinedFederalOrStateCodes.Add("CT", "08");
    this.combinedFederalOrStateCodes.Add("DE", "10");
    this.combinedFederalOrStateCodes.Add("DC", "11");
    this.combinedFederalOrStateCodes.Add("GA", "13");
    this.combinedFederalOrStateCodes.Add("HI", "15");
    this.combinedFederalOrStateCodes.Add("ID", "16");
    this.combinedFederalOrStateCodes.Add("IN", "18");
    this.combinedFederalOrStateCodes.Add("KS", "20");
    this.combinedFederalOrStateCodes.Add("LA", "22");
    this.combinedFederalOrStateCodes.Add("ME", "23");
    this.combinedFederalOrStateCodes.Add("MD", "24");
    this.combinedFederalOrStateCodes.Add("MA", "25");
    this.combinedFederalOrStateCodes.Add("MI", "26");
    this.combinedFederalOrStateCodes.Add("MN", "27");
    this.combinedFederalOrStateCodes.Add("MS", "28");
    this.combinedFederalOrStateCodes.Add("MO", "29");
    this.combinedFederalOrStateCodes.Add("MT", "30");
    this.combinedFederalOrStateCodes.Add("NE", "31");
    this.combinedFederalOrStateCodes.Add("NJ", "34");
    this.combinedFederalOrStateCodes.Add("NM", "35");
    this.combinedFederalOrStateCodes.Add("NC", "37");
    this.combinedFederalOrStateCodes.Add("ND", "38");
    this.combinedFederalOrStateCodes.Add("OH", "39");
    this.combinedFederalOrStateCodes.Add("OK", "40");
    this.combinedFederalOrStateCodes.Add("PA", "42");
    this.combinedFederalOrStateCodes.Add("RI", "44");
    this.combinedFederalOrStateCodes.Add("SC", "45");
    this.combinedFederalOrStateCodes.Add("WI", "55");
  }

  protected virtual string GetDirectSaleIndicator(int VendorID, string FinYear)
  {
    using (new PXReadBranchRestrictedScope(this.MarkedOrganizationIDs, this.MarkedBranchIDs, requireAccessForAllSpecified: true))
    {
      using (IEnumerator<PXResult<AP1099History>> enumerator = PXSelectBase<AP1099History, PXSelectJoinGroupBy<AP1099History, InnerJoin<AP1099Box, On<AP1099Box.boxNbr, Equal<AP1099History.boxNbr>>>, Where<AP1099History.vendorID, Equal<Required<AP1099History.vendorID>>, And<AP1099History.boxNbr, Equal<Required<AP1099History.boxNbr>>, And<AP1099History.finYear, Equal<Required<AP1099History.finYear>>>>>, PX.Data.Aggregate<GroupBy<AP1099History.boxNbr, Sum<AP1099History.histAmt>>>>.Config>.Select((PXGraph) this, (object) VendorID, (object) 9, (object) FinYear).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<AP1099History, AP1099Box> current = (PXResult<AP1099History, AP1099Box>) enumerator.Current;
          Decimal? histAmt = ((AP1099History) current).HistAmt;
          Decimal? minReportAmt = ((AP1099Box) current).MinReportAmt;
          return histAmt.GetValueOrDefault() >= minReportAmt.GetValueOrDefault() & histAmt.HasValue & minReportAmt.HasValue ? "1" : string.Empty;
        }
      }
    }
    return string.Empty;
  }

  public class Reporting1099Entity : I1099Settings
  {
    public I1099Settings Settings;
    public PX.Objects.CR.BAccount BAccount;
    public PX.Objects.CR.Contact Contact;
    public PX.Objects.CR.Address Address;
    public LocationExtAddress Location;

    public int? BAccountID
    {
      get => this.Settings.BAccountID;
      set => this.Settings.BAccountID = value;
    }

    public string TCC
    {
      get => this.Settings.TCC;
      set => this.Settings.TCC = value;
    }

    public bool? ForeignEntity
    {
      get => this.Settings.ForeignEntity;
      set => this.Settings.ForeignEntity = value;
    }

    public bool? CFSFiler
    {
      get => this.Settings.CFSFiler;
      set => this.Settings.CFSFiler = value;
    }

    public string ContactName
    {
      get => this.Settings.ContactName;
      set => this.Settings.ContactName = value;
    }

    public string CTelNumber
    {
      get => this.Settings.CTelNumber;
      set => this.Settings.CTelNumber = value;
    }

    public string CEmail
    {
      get => this.Settings.CEmail;
      set => this.Settings.CEmail = value;
    }

    public string NameControl
    {
      get => this.Settings.NameControl;
      set => this.Settings.NameControl = value;
    }

    public static implicit operator PX.Objects.CR.BAccount(
      MISC1099EFileProcessing.Reporting1099Entity entity)
    {
      return entity.BAccount;
    }

    public static implicit operator PX.Objects.CR.Contact(
      MISC1099EFileProcessing.Reporting1099Entity entity)
    {
      return entity.Contact;
    }

    public static implicit operator PX.Objects.CR.Address(
      MISC1099EFileProcessing.Reporting1099Entity entity)
    {
      return entity.Address;
    }

    public static implicit operator LocationExtAddress(
      MISC1099EFileProcessing.Reporting1099Entity entity)
    {
      return entity.Location;
    }
  }

  protected class AP1099OrganizationDefinition : 
    IPrefetchable<PXFilter<MISC1099EFileFilter>>,
    IPXCompanyDependent
  {
    public Dictionary<int, PX.Objects.GL.DAC.Organization> All1099;
    public Dictionary<int, PX.Objects.GL.DAC.Organization> ForReporting;

    public void Prefetch(PXFilter<MISC1099EFileFilter> filter)
    {
      List<PX.Objects.GL.DAC.Organization> list = PXSelectorAttribute.SelectAll<MISC1099EFileFilter.organizationID>(filter.Cache, (object) filter.Current).RowCast<PX.Objects.GL.DAC.Organization>().ToList<PX.Objects.GL.DAC.Organization>();
      this.All1099 = list.ToDictionary<PX.Objects.GL.DAC.Organization, int>((Func<PX.Objects.GL.DAC.Organization, int>) (o => o.OrganizationID.Value));
      this.ForReporting = list.Where<PX.Objects.GL.DAC.Organization>((Func<PX.Objects.GL.DAC.Organization, bool>) (o => o.Reporting1099.GetValueOrDefault())).ToDictionary<PX.Objects.GL.DAC.Organization, int>((Func<PX.Objects.GL.DAC.Organization, int>) (o => o.OrganizationID.Value));
    }
  }

  protected class AP1099BranchDefinition : 
    IPrefetchable<PXFilter<MISC1099EFileFilter>>,
    IPXCompanyDependent
  {
    public Dictionary<int, PX.Objects.GL.Branch> Available;

    public void Prefetch(PXFilter<MISC1099EFileFilter> filter)
    {
      this.Available = PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.Branch.organizationID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.organizationID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.GL.DAC.Organization.reporting1099ByBranches, Equal<PX.Data.True>>>>, PX.Data.And<BqlOperand<PX.Objects.GL.Branch.reporting1099, IBqlBool>.IsEqual<PX.Data.True>>>, PX.Data.And<BqlOperand<PX.Objects.GL.DAC.Organization.active, IBqlBool>.IsEqual<PX.Data.True>>>, PX.Data.And<BqlOperand<PX.Objects.GL.Branch.active, IBqlBool>.IsEqual<PX.Data.True>>>>.And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>.Config>.Select(filter.Cache.Graph).RowCast<PX.Objects.GL.Branch>().ToDictionary<PX.Objects.GL.Branch, int>((Func<PX.Objects.GL.Branch, int>) (b => b.BranchID.Value));
    }
  }
}
