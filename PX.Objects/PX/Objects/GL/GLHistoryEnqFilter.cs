// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistoryEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class GLHistoryEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _FinPeriodID;
  protected int? _AccountID;
  protected int? _SubID;
  protected string _SubCD;
  protected bool? _ShowCuryDetail;
  protected string _AccountClassID;
  private const string CS_FIRST_PERIOD = "01";

  [Organization(false, Required = false)]
  public int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (GLHistoryEnqFilter.organizationID), false, null, null, Required = false)]
  public int? BranchID { get; set; }

  [OrganizationTree(typeof (GLHistoryEnqFilter.organizationID), typeof (GLHistoryEnqFilter.branchID), null, false)]
  public int? OrgBAccountID { get; set; }

  [PXDefault]
  [FinPeriodSelector(null, typeof (AccessInfo.businessDate), typeof (GLHistoryEnqFilter.branchID), null, typeof (GLHistoryEnqFilter.organizationID), typeof (GLHistoryEnqFilter.useMasterCalendar), null, false, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Use Master Calendar")]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleCalendarsSupport>))]
  public bool? UseMasterCalendar { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Coalesce<Coalesce<Search<PX.Objects.GL.DAC.Organization.actualLedgerID, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current2<GLHistoryEnqFilter.organizationID>>>>, Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current2<GLHistoryEnqFilter.branchID>>>>>, Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>))]
  [PXUIField]
  [PXSelector(typeof (Ledger.ledgerID), DescriptionField = typeof (Ledger.descr), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true)]
  public virtual int? LedgerID { get; set; }

  [AccountAny]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [SubAccountRestrictedRaw(DisplayName = "Subaccount", SuppressValidation = true)]
  public virtual string SubCD
  {
    get => this._SubCD;
    set => this._SubCD = value;
  }

  public virtual string BegFinPeriod
  {
    get
    {
      return this._FinPeriodID != null ? GLHistoryEnqFilter.FirstPeriodOfYear(FinPeriodUtils.FiscalYear(this._FinPeriodID)) : (string) null;
    }
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubCDWildcard
  {
    [PXDependsOnFields(new Type[] {typeof (GLHistoryEnqFilter.subCD)})] get
    {
      return SubCDUtils.CreateSubCDWildcard(this._SubCD, "SUBACCOUNT");
    }
  }

  [PXDBBool]
  [PXDefault]
  [PXUIField]
  public virtual bool? ShowCuryDetail
  {
    get => this._ShowCuryDetail;
    set => this._ShowCuryDetail = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (AccountClass.accountClassID))]
  public virtual string AccountClassID
  {
    get => this._AccountClassID;
    set => this._AccountClassID = value;
  }

  protected static string FirstPeriodOfYear(string year) => year + "01";

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLHistoryEnqFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryEnqFilter.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnqFilter.finPeriodID>
  {
  }

  public abstract class useMasterCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLHistoryEnqFilter.useMasterCalendar>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryEnqFilter.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryEnqFilter.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryEnqFilter.subID>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistoryEnqFilter.subCD>
  {
  }

  public abstract class begFinPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnqFilter.begFinPeriod>
  {
  }

  public abstract class subCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnqFilter.subCDWildcard>
  {
  }

  public abstract class showCuryDetail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLHistoryEnqFilter.showCuryDetail>
  {
  }

  public abstract class accountClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnqFilter.accountClassID>
  {
  }
}
