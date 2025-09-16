// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UnvalidatedAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// An unbound DAC used for the grid on the screen for validating addresses in documents. Each record represents an invalid address used in the document
/// for which the <see cref="P:PX.Objects.CS.IValidatedAddress.IsValidated" /> flag is set to <see langword="false" />.
/// </summary>
/// <remarks>
/// The DAC is used for grid on:
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
public class UnvalidatedAddress : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// A boolean value that indicates (if set to <see langword="true" />) that the record is marked as selected.
  /// If the value is set to <see langword="false" />, the record is marked as unselected.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <summary>
  /// The Document Nbr, which is a comma separated value of DocType/OrderType and RefNbr/OrderNbr fields.
  /// If Document Type is not present for a specific document, this field just holds the RefNbr/OrderNbr.
  /// </summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Nbr.")]
  public virtual 
  #nullable disable
  string DocumentNbr { get; set; }

  /// <summary>The Address ID for the address.</summary>
  [PXInt(IsKey = true)]
  public virtual int? AddressID { get; set; }

  /// <summary>
  /// The DocumentType field that depicts the document type to which the address belongs.
  /// </summary>
  [PXString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Type")]
  public virtual string DocumentType { get; set; }

  /// <summary>The status of the document.</summary>
  [PXString(20, IsUnicode = true)]
  public virtual string Status { get; set; }

  /// <summary>The first line of the address.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 1")]
  public virtual string AddressLine1 { get; set; }

  /// <summary>The second line of the address.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 2")]
  public virtual string AddressLine2 { get; set; }

  /// <summary>The city of the address.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "City")]
  public virtual string City { get; set; }

  /// <summary>The state of the address.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  public virtual string State { get; set; }

  /// <summary>The postal code of the address.</summary>
  [PXString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  public virtual string PostalCode { get; set; }

  /// <summary>The country of the address.</summary>
  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Country")]
  public virtual string CountryID { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// A field of type <see cref="T:PX.Objects.CS.IAddress" />, which is used to save the address record.
  /// </summary>
  public IAddress Address { get; set; }

  /// <summary>
  /// A field of type <see cref="T:PX.Data.IBqlTable" />, which is used to save the document.
  /// </summary>
  public IBqlTable Document { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UnvalidatedAddress.selected>
  {
  }

  public abstract class documentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UnvalidatedAddress.documentNbr>
  {
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UnvalidatedAddress.addressID>
  {
  }

  public abstract class documentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UnvalidatedAddress.documentType>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UnvalidatedAddress.status>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UnvalidatedAddress.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UnvalidatedAddress.addressLine2>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UnvalidatedAddress.city>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UnvalidatedAddress.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UnvalidatedAddress.postalCode>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UnvalidatedAddress.countryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UnvalidatedAddress.noteID>
  {
  }
}
