// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.SplitParams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class SplitParams : FixedAsset
{
  protected int? _SplitID;
  protected 
  #nullable disable
  string _SplittedAssetCD;
  protected Decimal? _Cost;
  protected Decimal? _SplittedQty;
  protected Decimal? _Ratio;

  [PXInt(IsKey = true)]
  [PXLineNbr(typeof (SplitProcess.SplitFilter))]
  public virtual int? SplitID
  {
    get => this._SplitID;
    set => this._SplitID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public override string AssetCD { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Asset ID")]
  public virtual string SplittedAssetCD
  {
    get => this._SplittedAssetCD;
    set => this._SplittedAssetCD = value;
  }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost")]
  public virtual Decimal? Cost
  {
    get => this._Cost;
    set => this._Cost = value;
  }

  [PXQuantity(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? SplittedQty
  {
    get => this._SplittedQty;
    set => this._SplittedQty = value;
  }

  [PXDecimal(18, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ratio")]
  public virtual Decimal? Ratio
  {
    get => this._Ratio;
    set => this._Ratio = value;
  }

  public abstract class splitID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SplitParams.splitID>
  {
  }

  public abstract class splittedAssetCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SplitParams.splittedAssetCD>
  {
  }

  public abstract class cost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SplitParams.cost>
  {
  }

  public abstract class splittedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SplitParams.splittedQty>
  {
  }

  public abstract class ratio : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SplitParams.ratio>
  {
  }
}
