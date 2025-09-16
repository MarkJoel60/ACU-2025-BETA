// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Stores currency and exchange rate information for a particular document or transaction.
/// Usually, there is an individual record of this type for each document or transaction that involves monetary amounts and supports multi-currency.
/// The documents store a link to their instance of CurrencyInfo in the CuryInfoID field, such as <see cref="P:PX.Objects.GL.GLTran.CuryInfoID" />.
/// The exchange rate data for objects of this type is either entered by user or obtained from the <see cref="T:PX.Objects.CM.CurrencyRate" /> records.
/// Records of this type are either created automatically by the <see cref="T:PX.Objects.CM.CurrencyInfoAttribute" /> or
/// explicitly inserted by the application code (such as in <see cref="T:PX.Objects.AR.ARReleaseProcess" />).
/// User must not be aware of existence of these records.
/// </summary>
[PXCacheName("Currency Info")]
[Serializable]
public class CurrencyInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected long? _CuryInfoID;
  protected 
  #nullable disable
  string _ModuleCode;
  protected bool? _IsReadOnly;
  protected bool? _BaseCalc;
  protected string _BaseCuryID;
  protected string _CuryID;
  protected string _CuryRateTypeID;
  protected DateTime? _CuryEffDate;
  protected string _CuryMultDiv;
  protected Decimal? _CuryRate;
  protected Decimal? _RecipRate;
  protected short? _CuryPrecision;
  protected short? _BasePrecision;
  protected byte[] _tstamp;

  private CMSetup getCMSetup(PXCache cache)
  {
    return PXResultset<CMSetup>.op_Implicit(PXSetup<CMSetup>.Select(cache.Graph, Array.Empty<object>()));
  }

  private CurrencyRate getCuryRate(PXCache cache)
  {
    if (string.Equals(this.CuryID, this.BaseCuryID, StringComparison.OrdinalIgnoreCase))
      return (CurrencyRate) null;
    return PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyInfo.baseCuryID>>, And<CurrencyRate.fromCuryID, Equal<Required<CurrencyInfo.curyID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyInfo.curyRateTypeID>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyInfo.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.SelectWindowed(cache.Graph, 0, 1, new object[4]
    {
      (object) this.BaseCuryID,
      (object) this.CuryID,
      (object) this.CuryRateTypeID,
      (object) this.CuryEffDate
    }));
  }

  public virtual void SetCuryEffDate(PXCache sender, object value)
  {
    sender.SetValue<CurrencyInfo.curyEffDate>((object) this, value);
    this.defaultCuryRate(sender, false);
  }

  public virtual bool AllowUpdate(PXCache sender)
  {
    return (sender == null || sender.AllowUpdate || sender.AllowDelete) && !this.IsReadOnly.GetValueOrDefault() && !(this.CuryID == this.BaseCuryID);
  }

  private void SetDefaultEffDate(PXCache cache)
  {
    object obj;
    if (cache.RaiseFieldDefaulting<CurrencyInfo.curyEffDate>((object) this, ref obj))
      cache.RaiseFieldUpdating<CurrencyInfo.curyEffDate>((object) this, ref obj);
    this.CuryEffDate = (DateTime?) obj;
  }

  private void defaultCuryRate(PXCache cache) => this.defaultCuryRate(cache, true);

  private void defaultCuryRate(PXCache cache, bool ForceDefault)
  {
    CurrencyRate curyRate = this.getCuryRate(cache);
    if (curyRate != null)
    {
      DateTime? curyEffDate1 = this.CuryEffDate;
      this.CuryEffDate = curyRate.CuryEffDate;
      this.CuryRate = new Decimal?(Math.Round(curyRate.CuryRate.Value, 8));
      this.CuryMultDiv = curyRate.CuryMultDiv;
      this.RecipRate = new Decimal?(Math.Round(curyRate.RateReciprocal.Value, 8));
      DateTime? curyEffDate2 = curyRate.CuryEffDate;
      DateTime? nullable1 = curyEffDate1;
      if ((curyEffDate2.HasValue & nullable1.HasValue ? (curyEffDate2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
      CurrencyRateType currencyRateType = (CurrencyRateType) PXSelectorAttribute.Select<CurrencyInfo.curyRateTypeID>(cache, (object) this);
      if (currencyRateType == null)
        return;
      short? rateEffDays = currencyRateType.RateEffDays;
      int? nullable2 = rateEffDays.HasValue ? new int?((int) rateEffDays.GetValueOrDefault()) : new int?();
      int num = 0;
      if (!(nullable2.GetValueOrDefault() > num & nullable2.HasValue))
        return;
      DateTime? nullable3 = curyEffDate1;
      curyEffDate2 = curyRate.CuryEffDate;
      int days = (nullable3.HasValue & curyEffDate2.HasValue ? new TimeSpan?(nullable3.GetValueOrDefault() - curyEffDate2.GetValueOrDefault()) : new TimeSpan?()).Value.Days;
      rateEffDays = currencyRateType.RateEffDays;
      nullable2 = rateEffDays.HasValue ? new int?((int) rateEffDays.GetValueOrDefault()) : new int?();
      int valueOrDefault = nullable2.GetValueOrDefault();
      if (days >= valueOrDefault & nullable2.HasValue)
        throw new PXRateIsNotDefinedForThisDateException(curyRate.CuryRateType, curyRate.FromCuryID, curyRate.ToCuryID, curyEffDate1.Value);
    }
    else
    {
      if (!ForceDefault)
        return;
      if (string.Equals(this.CuryID, this.BaseCuryID, StringComparison.OrdinalIgnoreCase))
      {
        bool isDirty = cache.IsDirty;
        CurrencyInfo currencyInfo = new CurrencyInfo();
        cache.SetDefaultExt<CurrencyInfo.curyRate>((object) currencyInfo);
        cache.SetDefaultExt<CurrencyInfo.curyMultDiv>((object) currencyInfo);
        cache.SetDefaultExt<CurrencyInfo.recipRate>((object) currencyInfo);
        this.CuryRate = new Decimal?(Math.Round(currencyInfo.CuryRate.Value, 8));
        this.CuryMultDiv = currencyInfo.CuryMultDiv;
        this.RecipRate = new Decimal?(Math.Round(currencyInfo.RecipRate.Value, 8));
        cache.IsDirty = isDirty;
      }
      else if (this._CuryRateTypeID == null || !this._CuryEffDate.HasValue)
      {
        this.CuryRate = new Decimal?();
        this.RecipRate = new Decimal?();
        this.CuryMultDiv = "M";
      }
      else
      {
        this.CuryRate = new Decimal?();
        this.RecipRate = new Decimal?();
        this.CuryMultDiv = "M";
        throw new PXSetPropertyException("Currency Rate is not defined.", (PXErrorLevel) 2);
      }
    }
  }

  public virtual bool CheckRateVariance(PXCache cache)
  {
    CMSetup cmSetup = this.getCMSetup(cache);
    if (cmSetup != null && cmSetup.RateVarianceWarn.GetValueOrDefault())
    {
      Decimal? nullable = cmSetup.RateVariance;
      Decimal num1 = 0M;
      if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
      {
        CurrencyRate curyRate = this.getCuryRate(cache);
        if (curyRate != null)
        {
          nullable = curyRate.CuryRate;
          if (nullable.HasValue)
          {
            nullable = curyRate.CuryRate;
            Decimal num2 = 0M;
            if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
            {
              Decimal num3;
              if (!(curyRate.CuryMultDiv == this.CuryMultDiv))
              {
                nullable = this.CuryRate;
                Decimal num4 = 0M;
                if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
                {
                  nullable = this.CuryRate;
                  Decimal num5 = 1M / nullable.Value;
                  nullable = curyRate.CuryRate;
                  Decimal num6 = nullable.Value;
                  Decimal num7 = 100M * (num5 - num6);
                  nullable = curyRate.CuryRate;
                  Decimal num8 = nullable.Value;
                  num3 = num7 / num8;
                  goto label_9;
                }
              }
              nullable = this.CuryRate;
              Decimal num9 = nullable.Value;
              nullable = curyRate.CuryRate;
              Decimal num10 = nullable.Value;
              Decimal num11 = 100M * (num9 - num10);
              nullable = curyRate.CuryRate;
              Decimal num12 = nullable.Value;
              num3 = num11 / num12;
label_9:
              Decimal num13 = Math.Abs(num3);
              nullable = cmSetup.RateVariance;
              Decimal valueOrDefault = nullable.GetValueOrDefault();
              if (num13 > valueOrDefault & nullable.HasValue)
                return true;
            }
          }
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Key field. Database identity.
  /// Unique identifier of the Currency Info object.
  /// </summary>
  [PXDBLongIdentity(IsKey = true)]
  [PXUIField(Visible = false)]
  [PXDependsOnFields(new Type[] {typeof (CurrencyInfo.curyID), typeof (CurrencyInfo.baseCuryID), typeof (CurrencyInfo.sampleCuryRate)})]
  [CurrencyInfo.CuryInfoID]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// Identifier of the module, to which the Currency Info object belongs.
  /// The value of this field affects the choice of the default <see cref="P:PX.Objects.CM.CurrencyInfo.CuryRateTypeID">Rate Type</see>:
  /// for <c>"CA"</c> the Rate Type is taken from the <see cref="P:PX.Objects.CM.CMSetup.CARateTypeDflt" />,
  /// for <c>"AP"</c> the Rate Type is taken from the <see cref="P:PX.Objects.CM.CMSetup.APRateTypeDflt" />,
  /// for <c>"AR"</c> the Rate Type is taken from the <see cref="P:PX.Objects.CM.CMSetup.ARRateTypeDflt" />,
  /// for <c>"GL"</c> the Rate Type is taken from the <see cref="P:PX.Objects.CM.CMSetup.GLRateTypeDflt" />.
  /// </summary>
  public virtual string ModuleCode
  {
    get => this._ModuleCode;
    set => this._ModuleCode = value;
  }

  /// <summary>
  /// When set to <c>true</c>, the system won't allow user to change the fields of this object.
  /// </summary>
  public virtual bool? IsReadOnly
  {
    get => this._IsReadOnly;
    set => this._IsReadOnly = value;
  }

  /// <summary>
  /// When <c>true</c>, indicates that the system must calculate the amounts in the
  /// <see cref="P:PX.Objects.CM.CurrencyInfo.BaseCuryID">base currency</see> for the related document or transaction.
  /// Otherwise the changes in the amounts expressed in the <see cref="P:PX.Objects.CM.CurrencyInfo.CuryID">currency of the document</see>
  /// won't result in an update to the amounts in base currency.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? BaseCalc
  {
    get => this._BaseCalc;
    set => this._BaseCalc = value;
  }

  /// <summary>
  /// Identifier of the base <see cref="T:PX.Objects.CM.Currency" />.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault(typeof (AccessInfo.baseCuryID))]
  [PXUIField(DisplayName = "Base Currency ID")]
  [CurrencyInfo.BaseCuryID]
  public virtual string BaseCuryID
  {
    get => this._BaseCuryID;
    set => this._BaseCuryID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Currency" /> of this Currency Info object.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault(typeof (AccessInfo.baseCuryID))]
  [PXUIField]
  [PXSelector(typeof (Currency.curyID))]
  [CurrencyInfo.CuryID]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// The read-only property providing the <see cref="P:PX.Objects.CM.CurrencyInfo.CuryID">Currency</see> for display in the User Interface.
  /// </summary>
  [PXString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string DisplayCuryID
  {
    get => this._CuryID;
    set
    {
    }
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> associated with this object.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [CurrencyInfo.CuryRateTypeID]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID))]
  [PXForeignReference(typeof (Field<CurrencyInfo.curyRateTypeID>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Curr. Rate Type ID")]
  public virtual string CuryRateTypeID
  {
    get => this._CuryRateTypeID;
    set => this._CuryRateTypeID = value;
  }

  /// <summary>
  /// The date, starting from which the specified <see cref="P:PX.Objects.CM.CurrencyInfo.CuryRate">rate</see> is considered current.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Data.AccessInfo.BusinessDate">current business date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Effective Date")]
  [CurrencyInfo.CuryEffDate]
  public virtual DateTime? CuryEffDate
  {
    get => this._CuryEffDate;
    set => this._CuryEffDate = value;
  }

  /// <summary>
  /// The operation required for currency conversion: Divide or Multiply.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"M"</c> - Multiply,
  /// <c>"D"</c> - Divide.
  /// Defaults to <c>"M"</c>.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Mult Div")]
  public virtual string CuryMultDiv
  {
    get => this._CuryMultDiv;
    set => this._CuryMultDiv = value;
  }

  /// <summary>
  /// The currency rate. For the purposes of conversion the value of this field is used
  /// together with the operation selected in the <see cref="P:PX.Objects.CM.CurrencyInfo.CuryMultDiv" /> field.
  /// </summary>
  /// <value>
  /// Defaults to <c>1.0</c>.
  /// </value>
  [PXDBDecimal(8)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? CuryRate
  {
    get => this._CuryRate;
    set => this._CuryRate = value;
  }

  /// <summary>
  /// The inverse of the <see cref="P:PX.Objects.CM.CurrencyInfo.CuryRate">exchange rate</see>, which is calculated automatically.
  /// </summary>
  [PXDBDecimal(8)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? RecipRate
  {
    get => this._RecipRate;
    set => this._RecipRate = value;
  }

  /// <summary>
  /// The exchange rate used for calculations and determined by the values of
  /// the <see cref="P:PX.Objects.CM.CurrencyInfo.CuryMultDiv" />, <see cref="P:PX.Objects.CM.CurrencyInfo.CuryRate" /> and <see cref="P:PX.Objects.CM.CurrencyInfo.RecipRate" /> fields.
  /// </summary>
  [PXDecimal(8)]
  [PXUIField(DisplayName = "Curr. Rate")]
  [PXDefault]
  [CurrencyInfo.CuryRate]
  public virtual Decimal? SampleCuryRate
  {
    [PXDependsOnFields(new Type[] {typeof (CurrencyInfo.curyMultDiv), typeof (CurrencyInfo.curyRate), typeof (CurrencyInfo.recipRate)})] get
    {
      return !(this._CuryMultDiv == "M") ? this._RecipRate : this._CuryRate;
    }
    set
    {
      if (this.CuryMultDiv == "M")
        this._CuryRate = value;
      else
        this._RecipRate = value;
    }
  }

  /// <summary>
  /// The inverse of the <see cref="P:PX.Objects.CM.CurrencyInfo.SampleCuryRate" />. This value is also determined by the values of
  /// the <see cref="P:PX.Objects.CM.CurrencyInfo.CuryMultDiv" />, <see cref="P:PX.Objects.CM.CurrencyInfo.CuryRate" /> and <see cref="P:PX.Objects.CM.CurrencyInfo.RecipRate" /> fields.
  /// </summary>
  [PXDecimal(8)]
  [PXUIField(DisplayName = "Reciprocal Rate")]
  [CurrencyInfo.CuryRecipRate]
  public virtual Decimal? SampleRecipRate
  {
    [PXDependsOnFields(new Type[] {typeof (CurrencyInfo.curyMultDiv), typeof (CurrencyInfo.recipRate), typeof (CurrencyInfo.curyRate)})] get
    {
      return !(this._CuryMultDiv == "M") ? this._CuryRate : this._RecipRate;
    }
    set
    {
      if (this.CuryMultDiv == "M")
        this._RecipRate = value;
      else
        this._CuryRate = value;
    }
  }

  /// <summary>
  /// The number of digits after the decimal point used in operations with the amounts
  /// associated with this Currency Info object and expressed in its <see cref="P:PX.Objects.CM.CurrencyInfo.CuryID">currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field is taken from the <see cref="P:PX.Objects.CM.CurrencyList.DecimalPlaces" /> field of the record associated with the
  /// <see cref="P:PX.Objects.CM.CurrencyInfo.CuryID">currency</see> of this object.
  /// </value>
  [PXShort]
  [CurrencyPrecision(2, typeof (CurrencyInfo.curyID))]
  public virtual short? CuryPrecision
  {
    get => this._CuryPrecision;
    set => this._CuryPrecision = value;
  }

  /// <summary>
  /// The number of digits after the decimal point used in operations with the amounts
  /// associated with this Currency Info object and expressed in its <see cref="P:PX.Objects.CM.CurrencyInfo.BaseCuryID">base currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field is taken from the <see cref="P:PX.Objects.CM.CurrencyList.DecimalPlaces" /> field of the record associated with the
  /// <see cref="P:PX.Objects.CM.CurrencyInfo.BaseCuryID">base currency</see> of this object.
  /// </value>
  [PXShort]
  [CurrencyPrecision(2, typeof (CurrencyInfo.baseCuryID))]
  public virtual short? BasePrecision
  {
    get => this._BasePrecision;
    set => this._BasePrecision = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<CurrencyInfo>.By<CurrencyInfo.curyInfoID>
  {
    public static CurrencyInfo Find(PXGraph graph, long? curyInfoID, PKFindOptions options = 0)
    {
      return (CurrencyInfo) PrimaryKeyOf<CurrencyInfo>.By<CurrencyInfo.curyInfoID>.FindBy(graph, (object) curyInfoID, options);
    }
  }

  public static class FK
  {
    public class Currency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyInfo>.By<CurrencyInfo.curyID>
    {
    }

    public class BaseCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyInfo>.By<CurrencyInfo.baseCuryID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<CurrencyRateType>.By<CurrencyRateType.curyRateTypeID>.ForeignKeyOf<CurrencyInfo>.By<CurrencyInfo.curyRateTypeID>
    {
    }
  }

  private sealed class CuryInfoIDAttribute : PXEventSubscriberAttribute
  {
    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      // ISSUE: method pointer
      sender.Graph.CommandPreparing.AddHandler(sender.GetItemType(), this._FieldName, new PXCommandPreparing((object) this, __methodptr(Parameter_CommandPreparing)));
    }

    public void Parameter_CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
    {
      long? nullable1;
      if ((e.Operation & 3) != null || (e.Operation & 124) == 16 /*0x10*/ || (e.Operation & 124) == 64 /*0x40*/ || e.Row != null || !(nullable1 = e.Value as long?).HasValue)
        return;
      long? nullable2 = nullable1;
      long num = 0;
      if (!(nullable2.GetValueOrDefault() < num & nullable2.HasValue))
        return;
      e.DataValue = (object) null;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  private sealed class CuryRateTypeIDAttribute : 
    PXEventSubscriberAttribute,
    IPXFieldDefaultingSubscriber,
    IPXFieldVerifyingSubscriber,
    IPXFieldUpdatedSubscriber
  {
    public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row) || string.IsNullOrEmpty(row.ModuleCode))
        return;
      CMSetup cmSetup = (CMSetup) null;
      if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        cmSetup = row.getCMSetup(sender);
      if (cmSetup == null)
        return;
      string str;
      switch (row.ModuleCode)
      {
        case "PM":
          str = cmSetup.PMRateTypeDflt;
          break;
        case "CA":
          str = cmSetup.CARateTypeDflt;
          break;
        case "AP":
          str = cmSetup.APRateTypeDflt;
          break;
        case "AR":
          str = cmSetup.ARRateTypeDflt;
          break;
        case "GL":
          str = cmSetup.GLRateTypeDflt;
          break;
        case "SO":
          str = cmSetup.SOFreightRateTypeDflt;
          break;
        default:
          str = (string) null;
          break;
      }
      if (string.IsNullOrEmpty(str))
        return;
      e.NewValue = (object) str;
    }

    public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        return;
      ((CancelEventArgs) e).Cancel = true;
    }

    public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row))
        return;
      row.SetDefaultEffDate(sender);
      try
      {
        row.defaultCuryRate(sender);
      }
      catch (PXSetPropertyException ex)
      {
        if (!e.ExternalCall)
          return;
        sender.RaiseExceptionHandling(this._FieldName, e.Row, sender.GetValue(e.Row, this._FieldOrdinal), (Exception) ex);
      }
    }
  }

  private sealed class CuryEffDateAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
  {
    public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row))
        return;
      if (row.CuryRateTypeID == null)
      {
        Decimal? nullable = row.CuryRate;
        if (nullable.HasValue)
        {
          nullable = row.RecipRate;
          if (nullable.HasValue)
            return;
        }
      }
      try
      {
        row.defaultCuryRate(sender);
      }
      catch (PXSetPropertyException ex)
      {
        sender.RaiseExceptionHandling(this._FieldName, e.Row, sender.GetValue(e.Row, this._FieldOrdinal), (Exception) ex);
      }
    }
  }

  public sealed class CuryIDAttribute : 
    PXEventSubscriberAttribute,
    IPXFieldUpdatedSubscriber,
    IPXRowSelectedSubscriber,
    IPXRowUpdatingSubscriber,
    IPXRowUpdatedSubscriber,
    IPXFieldVerifyingSubscriber
  {
    private bool? currencyInfoDirty;

    public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row))
        return;
      row.SetDefaultEffDate(sender);
      try
      {
        row.defaultCuryRate(sender);
      }
      catch (PXSetPropertyException ex)
      {
        sender.RaiseExceptionHandling(this._FieldName, e.Row, sender.GetValue(e.Row, this._FieldOrdinal), (Exception) ex);
      }
      row.CuryPrecision = new short?();
    }

    public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row))
        return;
      bool flag = row.IsReadOnly.GetValueOrDefault() || row.CuryID == row.BaseCuryID;
      PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyMultDiv>(sender, (object) row, !flag);
      PXUIFieldAttribute.SetEnabled<CurrencyInfo.sampleCuryRate>(sender, (object) row, !flag);
      PXUIFieldAttribute.SetEnabled<CurrencyInfo.sampleRecipRate>(sender, (object) row, !flag);
      PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyRateTypeID>(sender, (object) row, !flag);
      PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyEffDate>(sender, (object) row, !flag);
      PXUIFieldAttribute.SetEnabled<CurrencyInfo.baseCuryID>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<CurrencyInfo.displayCuryID>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyID>(sender, (object) row, true);
    }

    public void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
    {
      if (e.Row is CurrencyInfo row && row._IsReadOnly.GetValueOrDefault())
        ((CancelEventArgs) e).Cancel = true;
      else
        this.currencyInfoDirty = new bool?(sender.IsDirty);
    }

    public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row))
        return;
      if (e.OldRow is CurrencyInfo oldRow && (string.IsNullOrEmpty(row.CuryID) || string.IsNullOrEmpty(row.BaseCuryID)))
      {
        row.BaseCuryID = oldRow.BaseCuryID;
        row.CuryID = oldRow.CuryID;
      }
      bool? currencyInfoDirty = this.currencyInfoDirty;
      bool flag = false;
      if (!(currencyInfoDirty.GetValueOrDefault() == flag & currencyInfoDirty.HasValue) || !(row.CuryID == oldRow.CuryID) || !(row.CuryRateTypeID == oldRow.CuryRateTypeID))
        return;
      DateTime? curyEffDate1 = row.CuryEffDate;
      DateTime? curyEffDate2 = oldRow.CuryEffDate;
      if ((curyEffDate1.HasValue == curyEffDate2.HasValue ? (curyEffDate1.HasValue ? (curyEffDate1.GetValueOrDefault() == curyEffDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || !(row.CuryMultDiv == oldRow.CuryMultDiv))
        return;
      Decimal? curyRate1 = row.CuryRate;
      Decimal? curyRate2 = oldRow.CuryRate;
      if (!(curyRate1.GetValueOrDefault() == curyRate2.GetValueOrDefault() & curyRate1.HasValue == curyRate2.HasValue))
        return;
      sender.IsDirty = false;
      this.currencyInfoDirty = new bool?();
    }

    public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row))
        return;
      row.getCMSetup(sender);
    }
  }

  public sealed class BaseCuryIDAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
  {
    public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row))
        return;
      short? nullable = new short?();
      row.BasePrecision = nullable;
    }
  }

  private sealed class CuryRateAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
  {
    public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row) || !e.ExternalCall)
        return;
      Decimal num = Math.Round(row.SampleCuryRate.Value, 8);
      bool flag = false;
      if (num == 0M)
      {
        try
        {
          row.defaultCuryRate(sender);
          flag = true;
        }
        catch (PXSetPropertyException ex)
        {
          num = 1M;
        }
      }
      if (!flag)
      {
        row.CuryRate = new Decimal?(num);
        row.RecipRate = new Decimal?(Math.Round(1M / num, 8));
        row.CuryMultDiv = "M";
      }
      if (!row.CheckRateVariance(sender))
        return;
      PXUIFieldAttribute.SetWarning<CurrencyInfo.sampleCuryRate>(sender, e.Row, "Rate variance exceeds the limit specified on the Currency Management Preferences form.");
    }
  }

  private sealed class CuryRecipRateAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
  {
    public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row) || !e.ExternalCall)
        return;
      Decimal num = Math.Round(row.SampleRecipRate.Value, 8);
      if (num == 0M)
        num = 1M;
      row.CuryRate = new Decimal?(num);
      row.RecipRate = new Decimal?(Math.Round(1M / num, 8));
      row.CuryMultDiv = "D";
      if (!row.CheckRateVariance(sender))
        return;
      PXUIFieldAttribute.SetWarning(sender, e.Row, "SampleRecipRate", "Rate variance exceeds the limit specified on the Currency Management Preferences form.");
    }
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CurrencyInfo.curyInfoID>
  {
  }

  public abstract class baseCalc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyInfo.baseCalc>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.baseCuryID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.curyID>
  {
  }

  public abstract class displayCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.displayCuryID>
  {
  }

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfo.curyRateTypeID>
  {
  }

  public abstract class curyEffDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CurrencyInfo.curyEffDate>
  {
  }

  public abstract class curyMultDiv : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.curyMultDiv>
  {
  }

  public abstract class curyRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CurrencyInfo.curyRate>
  {
  }

  public abstract class recipRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CurrencyInfo.recipRate>
  {
  }

  public abstract class sampleCuryRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfo.sampleCuryRate>
  {
  }

  public abstract class sampleRecipRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfo.sampleRecipRate>
  {
  }

  public abstract class curyPrecision : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CurrencyInfo.curyPrecision>
  {
    public const short Default = 2;
  }

  public abstract class basePrecision : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CurrencyInfo.basePrecision>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CurrencyInfo.Tstamp>
  {
  }
}
