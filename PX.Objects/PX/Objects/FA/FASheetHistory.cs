// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FASheetHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class FASheetHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected 
  #nullable disable
  string _PeriodNbr;
  public FASheetHistory.PeriodDeprPair[] PtdValues;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXString(2, IsFixed = true, IsKey = true)]
  public virtual string PeriodNbr
  {
    get => this._PeriodNbr;
    set => this._PeriodNbr = value;
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FASheetHistory.assetID>
  {
  }

  public abstract class periodNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FASheetHistory.periodNbr>
  {
  }

  public class PeriodDeprPair
  {
    private readonly string _PeriodID;
    private readonly Decimal? _PtdDepreciated;
    private readonly Decimal? _PtdCalculated;

    public PeriodDeprPair(string periodID, Decimal? depr, Decimal? calc)
    {
      this._PeriodID = periodID;
      this._PtdDepreciated = depr;
      this._PtdCalculated = calc;
    }

    public string PeriodID => this._PeriodID;

    public Decimal? PtdDepreciated => this._PtdDepreciated;

    public Decimal? PtdCalculated => this._PtdCalculated;
  }
}
