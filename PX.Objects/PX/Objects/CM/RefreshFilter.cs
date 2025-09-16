// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RefreshFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CM;

[Serializable]
public class RefreshFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CuryID;
  protected DateTime? _CuryEffDate;
  protected string _CuryRateTypeID;

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXUIField(DisplayName = "To Currency")]
  [PXSelector(typeof (Search<Currency.curyID>))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBDate(MinValue = "01/01/2000")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? CuryEffDate
  {
    get => this._CuryEffDate;
    set => this._CuryEffDate = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXUIField(DisplayName = "Rate Type")]
  [PXSelector(typeof (Search<CurrencyRateType.curyRateTypeID>))]
  public virtual string CuryRateTypeID
  {
    get => this._CuryRateTypeID;
    set => this._CuryRateTypeID = value;
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshFilter.curyID>
  {
  }

  public abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RefreshFilter.curyEffDate>
  {
  }

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RefreshFilter.curyRateTypeID>
  {
  }
}
