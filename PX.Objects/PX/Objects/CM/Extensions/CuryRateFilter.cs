// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.CuryRateFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CM.Extensions;

[Serializable]
public class CuryRateFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(5, IsUnicode = true)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXUIField]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>>>))]
  public virtual 
  #nullable disable
  string ToCurrency { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? EffDate { get; set; }

  public abstract class toCurrency : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryRateFilter.toCurrency>
  {
  }

  public abstract class effDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CuryRateFilter.effDate>
  {
  }
}
