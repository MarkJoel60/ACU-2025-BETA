// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Consolidation.ConsolSourceDataMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.ConsolidationImport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.GL.Consolidation;

[TableAndChartDashboardType]
public class ConsolSourceDataMaint : PXGraph<
#nullable disable
ConsolSourceDataMaint>
{
  public PXFilter<ConsolSourceDataMaint.ConsolRecordsFilter> Filter;
  public PXCancel<ConsolSourceDataMaint.ConsolRecordsFilter> Cancel;
  public PXSelectOrderBy<GLConsolData, OrderBy<Asc<GLConsolData.finPeriodID>>> ConsolRecords;
  protected PXSelect<Segment, Where<Segment.dimensionID, Equal<SubAccountAttribute.dimensionName>>> SubaccountSegmentsView;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  protected virtual IExportSubaccountMapper CreateExportSubaccountMapper()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.subAccount>() ? (IExportSubaccountMapper) new ExportSubaccountMapper((IReadOnlyCollection<Segment>) GraphHelper.RowCast<Segment>((IEnumerable) ((PXSelectBase<Segment>) this.SubaccountSegmentsView).Select(Array.Empty<object>())).ToArray<Segment>(), GraphHelper.RowCast<SegmentValue>((IEnumerable) ((PXSelectBase<SegmentValue>) new PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<SubAccountAttribute.dimensionName>>>((PXGraph) this)).Select(Array.Empty<object>()))) : (IExportSubaccountMapper) new SubOffExportSubaccountMapper();
  }

  protected virtual IEnumerable consolRecords()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<ConsolSourceDataMaint.ConsolRecordsFilter>) this.Filter).Current.LedgerCD))
      return (IEnumerable) new List<GLConsolData>();
    PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.consolAllowed, Equal<True>, And<PX.Objects.GL.Ledger.ledgerCD, Equal<Required<PX.Objects.GL.Ledger.ledgerCD>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<ConsolSourceDataMaint.ConsolRecordsFilter>) this.Filter).Current.LedgerCD
    }));
    if (ledger == null)
      throw new PXException("Cannot find the source ledger '{0}'.", new object[1]
      {
        (object) ((PXSelectBase<ConsolSourceDataMaint.ConsolRecordsFilter>) this.Filter).Current.LedgerCD
      });
    int? nullable1 = new int?();
    PX.Objects.GL.DAC.Organization organization = OrganizationMaint.FindOrganizationByCD((PXGraph) this, ((PXSelectBase<ConsolSourceDataMaint.ConsolRecordsFilter>) this.Filter).Current.BranchCD);
    if (organization != null)
    {
      if (organization.OrganizationType == "WithoutBranches")
        organization = (PX.Objects.GL.DAC.Organization) null;
      else
        nullable1 = organization.OrganizationID;
    }
    if (!nullable1.HasValue)
    {
      PX.Objects.GL.Branch branchByCd = BranchMaint.FindBranchByCD((PXGraph) this, ((PXSelectBase<ConsolSourceDataMaint.ConsolRecordsFilter>) this.Filter).Current.BranchCD);
      if (!string.IsNullOrEmpty(((PXSelectBase<ConsolSourceDataMaint.ConsolRecordsFilter>) this.Filter).Current.BranchCD) && branchByCd == null)
        throw new PXException("Cannot find the source branch '{0}'.", new object[1]
        {
          (object) ((PXSelectBase<ConsolSourceDataMaint.ConsolRecordsFilter>) this.Filter).Current.BranchCD
        });
      nullable1 = (int?) branchByCd?.BranchID;
      if (branchByCd != null && ledger.BalanceType != "R" && OrganizationMaint.FindOrganizationByID((PXGraph) this, branchByCd.OrganizationID).OrganizationType == "NotBalancing")
        throw new PXException("The {0} branch cannot be consolidated separately because its company has type \"With Branches Not Requiring Balancing\". Consolidate data for the whole ledger.", new object[1]
        {
          (object) ((PXSelectBase<ConsolSourceDataMaint.ConsolRecordsFilter>) this.Filter).Current.BranchCD
        });
    }
    IExportSubaccountMapper subaccountMapper = this.CreateExportSubaccountMapper();
    bool flag = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
      flag = GraphHelper.RowCast<Segment>((IEnumerable) ((PXSelectBase<Segment>) this.SubaccountSegmentsView).Select(Array.Empty<object>())).All<Segment>((Func<Segment, bool>) (segment =>
      {
        short? consolNumChar = segment.ConsolNumChar;
        int? nullable2 = consolNumChar.HasValue ? new int?((int) consolNumChar.GetValueOrDefault()) : new int?();
        int num = 0;
        return nullable2.GetValueOrDefault() <= num & nullable2.HasValue;
      }));
    ((PXSelectBase) this.ConsolRecords).Cache.Clear();
    PXSelectBase<GLHistory> pxSelectBase = (PXSelectBase<GLHistory>) new PXSelectJoin<GLHistory, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<GLHistory.accountID>>, LeftJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<GLHistory.subID>>>>, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.accountID, NotEqual<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>>>>, OrderBy<Asc<GLHistory.finPeriodID, Asc<PX.Objects.GL.Account.accountCD, Asc<PX.Objects.GL.Sub.subCD>>>>>((PXGraph) this);
    if (nullable1.HasValue)
    {
      if (organization != null)
      {
        pxSelectBase.Join<LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<GLHistory.branchID>>>>();
        pxSelectBase.WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>();
      }
      else
        pxSelectBase.WhereAnd<Where<GLHistory.branchID, Equal<Required<GLHistory.branchID>>>>();
    }
    foreach (PXResult<GLHistory, PX.Objects.GL.Account, PX.Objects.GL.Sub> pxResult in pxSelectBase.Select(new object[2]
    {
      (object) ledger.LedgerID,
      (object) nullable1
    }))
    {
      GLHistory glHistory = PXResult<GLHistory, PX.Objects.GL.Account, PX.Objects.GL.Sub>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<GLHistory, PX.Objects.GL.Account, PX.Objects.GL.Sub>.op_Implicit(pxResult);
      PX.Objects.GL.Sub subaccount = PXResult<GLHistory, PX.Objects.GL.Account, PX.Objects.GL.Sub>.op_Implicit(pxResult);
      string glConsolAccountCd = account.GLConsolAccountCD;
      string mappedSubaccountCd = subaccountMapper.GetMappedSubaccountCD(subaccount);
      if (glConsolAccountCd != null && glConsolAccountCd.TrimEnd() != "" && ((mappedSubaccountCd == null ? 0 : (mappedSubaccountCd.TrimEnd() != "" ? 1 : 0)) | (flag ? 1 : 0)) != 0)
      {
        GLConsolData glConsolData1 = ((PXSelectBase<GLConsolData>) this.ConsolRecords).Locate(new GLConsolData()
        {
          MappedValue = mappedSubaccountCd,
          AccountCD = glConsolAccountCd,
          FinPeriodID = glHistory.FinPeriodID
        });
        if (glConsolData1 != null)
        {
          GLConsolData glConsolData2 = glConsolData1;
          Decimal? nullable3 = glConsolData2.ConsolAmtDebit;
          Decimal? nullable4 = glHistory.TranPtdDebit;
          glConsolData2.ConsolAmtDebit = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          GLConsolData glConsolData3 = glConsolData1;
          nullable4 = glConsolData3.ConsolAmtCredit;
          nullable3 = glHistory.TranPtdCredit;
          glConsolData3.ConsolAmtCredit = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        }
        else
          ((PXSelectBase<GLConsolData>) this.ConsolRecords).Insert(new GLConsolData()
          {
            MappedValue = mappedSubaccountCd,
            MappedValueLength = new int?(mappedSubaccountCd.Length),
            AccountCD = glConsolAccountCd,
            FinPeriodID = glHistory.FinPeriodID,
            ConsolAmtDebit = glHistory.TranPtdDebit,
            ConsolAmtCredit = glHistory.TranPtdCredit
          });
      }
    }
    return ((PXSelectBase) this.ConsolRecords).Cache.Inserted;
  }

  public ConsolSourceDataMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    ((PXSelectBase) this.ConsolRecords).Cache.AllowDelete = false;
    ((PXSelectBase) this.ConsolRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.ConsolRecords).Cache.AllowUpdate = false;
    this.SubaccountSegmentsView = new PXSelect<Segment, Where<Segment.dimensionID, Equal<SubAccountAttribute.dimensionName>>>((PXGraph) this);
  }

  [PXHidden]
  [Serializable]
  public class ConsolRecordsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    [PXSelector(typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr))]
    [PXUIField]
    public virtual string LedgerCD { get; set; }

    [PXString]
    [PXUIField]
    public virtual string BranchCD { get; set; }

    public abstract class ledgerCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ConsolSourceDataMaint.ConsolRecordsFilter.ledgerCD>
    {
    }

    public abstract class branchCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ConsolSourceDataMaint.ConsolRecordsFilter.branchCD>
    {
    }
  }
}
