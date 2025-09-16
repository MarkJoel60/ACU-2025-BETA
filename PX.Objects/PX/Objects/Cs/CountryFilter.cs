// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CountryFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;

#nullable enable
namespace PX.Objects.CS;

/// <summary>
/// A DAC that is used as a filter in the dialog box for adding a country or state.
/// </summary>
[PXCacheName("Country Filter")]
public class CountryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "Country ID")]
  [Country(DescriptionField = typeof (Country.description))]
  public 
  #nullable disable
  string CountryID { get; set; }

  /// <inheritdoc cref="T:PX.Objects.CS.Country.description" />
  [PXString]
  [PXUIField(DisplayName = "Country Name", Enabled = false)]
  [PXFormula(typeof (Selector<CountryFilter.countryID, Country.description>))]
  public string CountryName { get; set; }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CountryFilter.countryID>
  {
  }

  public abstract class countryName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CountryFilter.countryName>
  {
  }
}
