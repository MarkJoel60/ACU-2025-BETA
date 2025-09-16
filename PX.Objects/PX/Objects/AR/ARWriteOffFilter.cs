// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARWriteOffFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class ARWriteOffFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _WOType;
  protected DateTime? _WODate;
  protected string _WOFinPeriodID;
  protected Decimal? _WOLimit;
  protected int? _CustomerID;
  protected string _ReasonCode;
  protected Decimal? _SelTotal;

  [Organization(false)]
  public int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (ARWriteOffFilter.organizationID), false, null, null)]
  public int? BranchID { get; set; }

  [OrganizationTree(typeof (ARWriteOffFilter.organizationID), typeof (ARWriteOffFilter.branchID), null, false)]
  [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>))]
  public int? OrgBAccountID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("SMB")]
  [ARWriteOffType.List]
  [PXUIField]
  public virtual string WOType
  {
    get => this._WOType;
    set => this._WOType = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? WODate
  {
    get => this._WODate;
    set => this._WODate = value;
  }

  [AROpenPeriod(typeof (ARWriteOffFilter.wODate), null, null, typeof (ARWriteOffFilter.organizationID), null, null, true, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, ValidatePeriod = PeriodValidation.DefaultSelectUpdate)]
  [PXUIField]
  public virtual string WOFinPeriodID
  {
    get => this._WOFinPeriodID;
    set => this._WOFinPeriodID = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? WOLimit
  {
    get => this._WOLimit;
    set => this._WOLimit = value;
  }

  [Customer]
  [PXRestrictor(typeof (Where<Customer.status, Equal<CustomerStatus.active>, Or<Customer.status, Equal<CustomerStatus.oneTime>, Or<Customer.status, Equal<CustomerStatus.creditHold>>>>), "The customer status is '{0}'.", new Type[] {typeof (Customer.status)})]
  [PXRestrictor(typeof (Where<Customer.smallBalanceAllow, Equal<True>>), "Write-Offs are not allowed for the customer.", new Type[] {})]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDefault]
  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where2<Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.balanceWriteOff>, And<Current<ARWriteOffFilter.woType>, Equal<ARDocType.smallBalanceWO>>>, Or<Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.creditWriteOff>, And<Current<ARWriteOffFilter.woType>, Equal<ARDocType.smallCreditWO>>>>>>))]
  [PXUIField]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? SelTotal
  {
    get => this._SelTotal;
    set => this._SelTotal = value;
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARWriteOffFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARWriteOffFilter.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class woType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARWriteOffFilter.woType>
  {
  }

  public abstract class wODate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARWriteOffFilter.wODate>
  {
  }

  public abstract class wOFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARWriteOffFilter.wOFinPeriodID>
  {
  }

  public abstract class wOLimit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARWriteOffFilter.wOLimit>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARWriteOffFilter.customerID>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARWriteOffFilter.reasonCode>
  {
  }

  public abstract class selTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARWriteOffFilter.selTotal>
  {
  }
}
