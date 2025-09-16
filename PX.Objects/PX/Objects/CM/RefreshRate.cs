// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RefreshRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// A non-mapped temporary DAC used to implement the Refresh Currency Rates form (CM507000) and process (<see cref="T:PX.Objects.CM.RefreshCurrencyRates" />).
/// The records of this type are generated in the <see cref="M:PX.Objects.CM.RefreshCurrencyRates.currencyRateList" /> view based on
/// the <see cref="T:PX.Objects.CM.CurrencyList" /> and <see cref="T:PX.Objects.CM.CurrencyRateType" /> records. When processing is invoked,
/// the <see cref="!:RefreshCurrencyRates.RefreshRates(RefreshFilter, List&lt;RefreshRate&gt;, string)" /> action uses the data in these records
/// (currency code, rate type and adjustment rate) along with the rates received from the external service to create new currency rates
/// in the system (<see cref="T:PX.Objects.CM.CurrencyRate" />).
/// </summary>
[PXProjection(typeof (Select<CurrencyRate>))]
[Serializable]
public class RefreshRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _FromCuryID;
  protected string _CuryRateType;
  private Decimal? _OnlineRateAdjustment;
  protected Decimal? _CuryRate;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(5, IsKey = true, IsUnicode = true, BqlTable = typeof (CurrencyRate))]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>>>))]
  public virtual string FromCuryID
  {
    get => this._FromCuryID;
    set => this._FromCuryID = value;
  }

  [PXDBString(6, IsKey = true, IsUnicode = true, BqlTable = typeof (CurrencyRate))]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  public virtual string CuryRateType
  {
    get => this._CuryRateType;
    set => this._CuryRateType = value;
  }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Online Rate Adjustment (%)")]
  public virtual Decimal? OnlineRateAdjustment
  {
    get => this._OnlineRateAdjustment;
    set => this._OnlineRateAdjustment = value;
  }

  [PXDBDecimal(8, MinValue = 0.0, BqlTable = typeof (CurrencyRate))]
  [PXDefault]
  [PXUIField]
  public virtual Decimal? CuryRate
  {
    get => this._CuryRate;
    set => this._CuryRate = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(BqlTable = typeof (CurrencyRate))]
  public virtual Guid? NoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RefreshRate.selected>
  {
  }

  public abstract class fromCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshRate.fromCuryID>
  {
  }

  public abstract class curyRateType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshRate.curyRateType>
  {
  }

  public abstract class onlineRateAdjustment : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RefreshRate.onlineRateAdjustment>
  {
  }

  public abstract class curyRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RefreshRate.curyRate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RefreshRate.noteID>
  {
  }
}
