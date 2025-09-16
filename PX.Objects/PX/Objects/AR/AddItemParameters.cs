// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.AddItemParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class AddItemParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PriceType;
  protected string _PriceCode;
  protected string _CuryID;

  [PXString(1)]
  [PXDefault("P")]
  [PriceTypeList.List]
  [PXUIField]
  public virtual string PriceType
  {
    get => this._PriceType;
    set => this._PriceType = value;
  }

  [PXString(30, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [ARPriceCodeSelector(typeof (AddItemParameters.priceType))]
  public virtual string PriceCode
  {
    get => this._PriceCode;
    set => this._PriceCode = value;
  }

  [PXDBString(5)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [NullableSite]
  public int? SiteID { get; set; }

  /// <summary>
  /// Defines the state of the Skip Line Discounts check box for the added lines.
  /// </summary>
  [PXDefault(typeof (ARPriceWorksheet.skipLineDiscounts))]
  [PXUIField(DisplayName = "Ignore Automatic Line Discounts")]
  [PXBool]
  public virtual bool? SkipLineDiscounts { get; set; }

  public abstract class priceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddItemParameters.priceType>
  {
  }

  public abstract class priceCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddItemParameters.priceCode>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddItemParameters.curyID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddItemParameters.siteID>
  {
  }

  public abstract class skipLineDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AddItemParameters.skipLineDiscounts>
  {
  }
}
