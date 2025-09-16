// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommissionPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents a period of a <see cref="T:PX.Objects.AR.ARSPCommissionYear">commission year</see>,
/// for which salesperson commissions are calculated by the Calculate Commissions
/// (AR505500) process. The number of periods in a commission is regulated by the
/// <see cref="P:PX.Objects.AR.ARSetup.SPCommnPeriodType">commission period type</see>, which is specified
/// on the Accounts Receivable Preferences (AR101000) form. The entities of this type are
/// created during the Calculate Commissions (AR505500) process, which corresponds
/// to the <see cref="T:PX.Objects.AR.ARSPCommissionProcess" /> graph. Commission periods are closed
/// on the Close Commission Period (AR506500) form, which corresponds to the
/// <see cref="T:PX.Objects.AR.ARSPCommissionReview" /> graph.
/// </summary>
[PXCacheName("AR Salesperson Commission Period")]
[Serializable]
public class ARSPCommissionPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CommnPeriodID;
  protected string _Year;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected string _Status;
  protected bool? _Filed;
  protected byte[] _tstamp;

  [PXDefault]
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (ARSPCommissionPeriod.commnPeriodID))]
  public virtual string CommnPeriodID
  {
    get => this._CommnPeriodID;
    set => this._CommnPeriodID = value;
  }

  [PXDBString(4, IsFixed = true)]
  [PXDefault]
  public virtual string Year
  {
    get => this._Year;
    set => this._Year = value;
  }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? StartDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (ARSPCommissionPeriod.startDate), typeof (ARSPCommissionPeriod.endDate)})] get
    {
      if (this._StartDate.HasValue && this._EndDate.HasValue)
      {
        DateTime? startDate = this._StartDate;
        DateTime? endDate = this._EndDate;
        if ((startDate.HasValue == endDate.HasValue ? (startDate.HasValue ? (startDate.GetValueOrDefault() == endDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          return new DateTime?(this._StartDate.Value.AddDays(-1.0));
      }
      return this._StartDate;
    }
    set
    {
      DateTime? nullable1;
      if (value.HasValue && this._EndDate.HasValue)
      {
        DateTime? nullable2 = value;
        DateTime? endDateUi = this.EndDateUI;
        if ((nullable2.HasValue == endDateUi.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == endDateUi.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          nullable1 = new DateTime?(value.Value.AddDays(1.0));
          goto label_4;
        }
      }
      nullable1 = value;
label_4:
      this._StartDate = nullable1;
    }
  }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [ARSPCommissionPeriodStatus.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Filed
  {
    get => this._Filed;
    set => this._Filed = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateUI
  {
    [PXDependsOnFields(new Type[] {typeof (ARSPCommissionPeriod.endDate)})] get
    {
      ref DateTime? local = ref this._EndDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(-1.0));
    }
    set => this._EndDate = value?.AddDays(1.0);
  }

  /// <exclude />
  public class PK : PrimaryKeyOf<ARSPCommissionPeriod>.By<ARSPCommissionPeriod.commnPeriodID>
  {
    public static ARSPCommissionPeriod Find(
      PXGraph graph,
      string commnPeriodID,
      PKFindOptions options = 0)
    {
      return (ARSPCommissionPeriod) PrimaryKeyOf<ARSPCommissionPeriod>.By<ARSPCommissionPeriod.commnPeriodID>.FindBy(graph, (object) commnPeriodID, options);
    }
  }

  public abstract class commnPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSPCommissionPeriod.commnPeriodID>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommissionPeriod.year>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSPCommissionPeriod.startDate>
  {
  }

  public abstract class startDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSPCommissionPeriod.startDateUI>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARSPCommissionPeriod.endDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommissionPeriod.status>
  {
  }

  public abstract class filed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSPCommissionPeriod.filed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARSPCommissionPeriod.Tstamp>
  {
  }

  public abstract class endDateUI : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSPCommissionPeriod.endDateUI>
  {
  }
}
