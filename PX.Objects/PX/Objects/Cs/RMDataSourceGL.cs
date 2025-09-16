// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMDataSourceGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class RMDataSourceGL : PXCacheExtension<
#nullable disable
RMDataSource>
{
  protected int? _LedgerID;
  protected string _StartAccount;
  protected string _StartSub;
  protected string _StartPeriod;
  protected string _EndPeriod;
  protected string _AccountClassID;
  protected string _EndAccount;
  protected string _EndSub;
  protected short? _StartPeriodYearOffset;
  protected short? _StartPeriodOffset;
  protected short? _EndPeriodYearOffset;
  protected short? _EndPeriodOffset;

  public static bool IsActive() => true;

  [Organization(false, null)]
  public virtual int? OrganizationID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? UseMasterCalendar { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Ledger")]
  [PXSelector(typeof (PX.Objects.GL.Ledger.ledgerID))]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Start Account")]
  [PXSelector(typeof (PX.Objects.GL.Account.accountCD))]
  public virtual string StartAccount
  {
    get => this._StartAccount;
    set => this._StartAccount = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Start Sub.")]
  [PXSelector(typeof (PX.Objects.GL.Sub.subCD))]
  public virtual string StartSub
  {
    get => this._StartSub;
    set => this._StartSub = value;
  }

  [BranchCDOfOrganization(typeof (RMDataSourceGL.organizationID), false, null)]
  public virtual string StartBranch { get; set; }

  [FinPeriodSelector(typeof (Search<OrganizationFinPeriod.finPeriodID, Where<Where2<Where<FinPeriod.organizationID, Equal<IsNull<Optional2<RMDataSourceGL.organizationID>, FinPeriod.organizationID.masterValue>>, And<IsNull<Optional2<RMDataSourceGL.useMasterCalendar>, False>, NotEqual<True>>>, Or<Where<FinPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<Optional2<RMDataSourceGL.useMasterCalendar>, Equal<True>>>>>>>), null, null, null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  [PXUIField(DisplayName = "Start Period")]
  public virtual string StartPeriod
  {
    get => this._StartPeriod;
    set => this._StartPeriod = value;
  }

  [FinPeriodSelector(typeof (Search<OrganizationFinPeriod.finPeriodID, Where<Where2<Where<FinPeriod.organizationID, Equal<IsNull<Optional2<RMDataSourceGL.organizationID>, FinPeriod.organizationID.masterValue>>, And<IsNull<Optional2<RMDataSourceGL.useMasterCalendar>, False>, NotEqual<True>>>, Or<Where<FinPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<Optional2<RMDataSourceGL.useMasterCalendar>, Equal<True>>>>>>>), null, null, null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  [PXUIField(DisplayName = "End Period")]
  public virtual string EndPeriod
  {
    get => this._EndPeriod;
    set => this._EndPeriod = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Class")]
  [PXSelector(typeof (AccountClass.accountClassID))]
  public virtual string AccountClassID
  {
    get => this._AccountClassID;
    set => this._AccountClassID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "End Account")]
  [PXSelector(typeof (PX.Objects.GL.Account.accountCD))]
  public virtual string EndAccount
  {
    get => this._EndAccount;
    set => this._EndAccount = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "End Sub.")]
  [PXSelector(typeof (PX.Objects.GL.Sub.subCD))]
  public virtual string EndSub
  {
    get => this._EndSub;
    set => this._EndSub = value;
  }

  [BranchCDOfOrganization(typeof (RMDataSourceGL.organizationID), false, null)]
  public virtual string EndBranch { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Offset (Year, Period)")]
  public virtual short? StartPeriodYearOffset
  {
    get => this._StartPeriodYearOffset;
    set => this._StartPeriodYearOffset = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "")]
  public virtual short? StartPeriodOffset
  {
    get => this._StartPeriodOffset;
    set => this._StartPeriodOffset = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Offset (Year, Period)")]
  public virtual short? EndPeriodYearOffset
  {
    get => this._EndPeriodYearOffset;
    set => this._EndPeriodYearOffset = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "")]
  public virtual short? EndPeriodOffset
  {
    get => this._EndPeriodOffset;
    set => this._EndPeriodOffset = value;
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMDataSourceGL.organizationID>
  {
  }

  public abstract class useMasterCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMDataSourceGL.useMasterCalendar>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMDataSourceGL.ledgerID>
  {
  }

  public abstract class startAccount : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourceGL.startAccount>
  {
  }

  public abstract class startSub : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourceGL.startSub>
  {
  }

  public abstract class startBranch : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourceGL.startBranch>
  {
  }

  public abstract class startPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourceGL.startPeriod>
  {
  }

  public abstract class endPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourceGL.endPeriod>
  {
  }

  public abstract class accountClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMDataSourceGL.accountClassID>
  {
  }

  public abstract class endAccount : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourceGL.endAccount>
  {
  }

  public abstract class endSub : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourceGL.endSub>
  {
  }

  public abstract class endBranch : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSourceGL.endBranch>
  {
  }

  public abstract class startPeriodYearOffset : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    RMDataSourceGL.startPeriodYearOffset>
  {
  }

  public abstract class startPeriodOffset : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    RMDataSourceGL.startPeriodOffset>
  {
  }

  public abstract class endPeriodYearOffset : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    RMDataSourceGL.endPeriodYearOffset>
  {
  }

  public abstract class endPeriodOffset : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    RMDataSourceGL.endPeriodOffset>
  {
  }
}
