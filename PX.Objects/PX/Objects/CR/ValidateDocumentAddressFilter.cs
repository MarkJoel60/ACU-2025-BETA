// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ValidateDocumentAddressFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// An Unbound DAC used for the filters on the screen for validating addresses in documents. The DocumentType field is overridden
/// in the graphs of different processing screens to show only documents which would be processed by the respective screen.
/// </summary>
/// <remarks>
/// The DAC is used for filters on:
/// <list type="bullet">
/// <item><description>The <i>Validate Addresses in Sales Documents (SO508000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.ValidateSODocumentAddressProcess" /> graph)</description></item>
/// <item><description>The <i>Validate Addresses in AR Documents (AR509010)</i> form (corresponds to the <see cref="T:PX.Objects.AR.ValidateARDocumentAddressProcess" /> graph)</description></item>
/// <item><description>The <i>Validate Addresses in AP Documents (AP508000)</i> form (corresponds to the <see cref="T:PX.Objects.AP.ValidateAPDocumentAddressProcess" /> graph)</description></item>
/// <item><description>The <i>Validate Addresses in Purchase Documents (PO507000)</i> form (corresponds to the <see cref="T:PX.Objects.PO.ValidatePODocumentAddressProcess" /> graph)</description></item>
/// <item><description>The <i>Validate Addresses in CRM Documents (CR508000)</i> form (corresponds to the <see cref="T:PX.Objects.CR.ValidateCRDocumentAddressProcess" /> graph)</description></item>
/// <item><description>The <i>Validate Addresses in Project Documents (PM507000)</i> form (corresponds to the <see cref="T:PX.Objects.PM.ValidatePMDocumentAddressProcess" /> graph)</description></item>
/// </list>
/// </remarks>
[PXHidden]
public class ValidateDocumentAddressFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// A boolean value that determines (if set to <see langword="true" />) that the incorrect address would be overridden as a result of address verification.
  /// If the value is set to <see langword="false" />, the incorrect address would not be overridden as a result of address verification.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Override Automatically")]
  public virtual bool? IsOverride { get; set; }

  /// <summary>
  /// The filter that loads the records in the grid based on the selected country.
  /// </summary>
  [PXString(2, InputMask = ">??")]
  [PXUIField(DisplayName = "Country")]
  [PXSelector(typeof (Search<PX.Objects.CS.Country.countryID>), new System.Type[] {typeof (PX.Objects.CS.Country.countryID), typeof (PX.Objects.CS.Country.description), typeof (PX.Objects.CS.Country.addressValidatorPluginID)}, DescriptionField = typeof (PX.Objects.CS.Country.description))]
  public virtual 
  #nullable disable
  string Country { get; set; }

  /// <summary>
  /// A Document Type filter. Classes implementing the base graph <see cref="T:PX.Objects.CR.ValidateDocumentAddressGraph`1" />
  /// would override this field with a StringList attribute to show the required Document Types in the drop down for this field.
  /// </summary>
  [PXString(20)]
  [PXUIField(DisplayName = "Creation Form", Required = true)]
  public virtual string DocumentType { get; set; }

  public abstract class isOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ValidateDocumentAddressFilter.isOverride>
  {
  }

  public abstract class country : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ValidateDocumentAddressFilter.country>
  {
  }

  public abstract class documentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ValidateDocumentAddressFilter.documentType>
  {
  }
}
