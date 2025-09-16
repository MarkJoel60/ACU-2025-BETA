// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateActionBaseInit`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GDPR;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
[PXInternalUseOnly]
public abstract class CRCreateActionBaseInit<TGraph, TMain> : PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  public PXSelectExtension<Document> Documents;
  public PXSelectExtension<DocumentContact> Contacts;
  public PXSelectExtension<DocumentAddress> Addresses;

  protected virtual CRCreateActionBaseInit<TGraph, TMain>.DocumentMapping GetDocumentMapping()
  {
    return new CRCreateActionBaseInit<TGraph, TMain>.DocumentMapping(typeof (TMain));
  }

  protected abstract CRCreateActionBaseInit<TGraph, TMain>.DocumentContactMapping GetDocumentContactMapping();

  protected abstract CRCreateActionBaseInit<TGraph, TMain>.DocumentAddressMapping GetDocumentAddressMapping();

  protected virtual IPersonalContact MapContact(DocumentContact source, IPersonalContact target)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    target.FullName = source.FullName;
    target.Title = source.Title;
    target.FirstName = source.FirstName;
    target.LastName = source.LastName;
    target.Salutation = source.Salutation;
    target.Attention = source.Attention;
    target.Email = source.Email;
    target.WebSite = source.WebSite;
    target.Phone1 = source.Phone1;
    target.Phone1Type = source.Phone1Type;
    target.Phone2 = source.Phone2;
    target.Phone2Type = source.Phone2Type;
    target.Phone3 = source.Phone3;
    target.Phone3Type = source.Phone3Type;
    target.Fax = source.Fax;
    target.FaxType = source.FaxType;
    return target;
  }

  protected virtual IConsentable MapConsentable(DocumentContact source, IConsentable target)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    target.ConsentAgreement = source.ConsentAgreement;
    target.ConsentDate = source.ConsentDate;
    target.ConsentExpirationDate = source.ConsentExpirationDate;
    return target;
  }

  protected virtual IAddressBase MapAddress(DocumentAddress source, IAddressBase target)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    target.AddressLine1 = source.AddressLine1;
    target.AddressLine2 = source.AddressLine2;
    target.City = source.City;
    target.CountryID = source.CountryID;
    target.State = source.State;
    target.PostalCode = source.PostalCode;
    if (target is IAddressISO20022 iaddressIsO20022)
    {
      iaddressIsO20022.Department = source.Department;
      iaddressIsO20022.SubDepartment = source.SubDepartment;
      iaddressIsO20022.StreetName = source.StreetName;
      iaddressIsO20022.BuildingNumber = source.BuildingNumber;
      iaddressIsO20022.BuildingName = source.BuildingName;
      iaddressIsO20022.Floor = source.Floor;
      iaddressIsO20022.UnitNumber = source.UnitNumber;
      iaddressIsO20022.PostBox = source.PostBox;
      iaddressIsO20022.Room = source.Room;
      iaddressIsO20022.TownLocationName = source.TownLocationName;
      iaddressIsO20022.DistrictName = source.DistrictName;
    }
    if (target is IAddressLocation iaddressLocation)
    {
      iaddressLocation.Latitude = source.Latitude;
      iaddressLocation.Longitude = source.Longitude;
    }
    if (target is IValidatedAddress validatedAddress && source.IsValidated.HasValue)
      validatedAddress.IsValidated = source.IsValidated;
    return target;
  }

  protected class DocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type ParentBAccountID = typeof (Document.parentBAccountID);
    public System.Type WorkgroupID = typeof (Document.workgroupID);
    public System.Type OverrideSalesTerritory = typeof (Document.overrideSalesTerritory);
    public System.Type SalesTerritoryID = typeof (Document.salesTerritoryID);
    public System.Type OwnerID = typeof (Document.ownerID);
    public System.Type BAccountID = typeof (Document.bAccountID);
    public System.Type ContactID = typeof (Document.contactID);
    public System.Type RefContactID = typeof (Document.refContactID);
    public System.Type ClassID = typeof (Document.classID);
    public System.Type NoteID = typeof (Document.noteID);
    public System.Type Source = typeof (Document.source);
    public System.Type CampaignID = typeof (Document.campaignID);
    public System.Type OverrideRefContact = typeof (Document.overrideRefContact);
    public System.Type Description = typeof (Document.description);
    public System.Type Location = typeof (Document.locationID);
    public System.Type TaxZoneID = typeof (Document.taxZoneID);
    public System.Type QualificationDate = typeof (Document.qualificationDate);
    public System.Type IsActive = typeof (Document.isActive);

    public System.Type Extension => typeof (Document);

    public System.Type Table => this._table;

    public DocumentMapping(System.Type table) => this._table = table;
  }

  protected class DocumentContactMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type FullName = typeof (DocumentContact.fullName);
    public System.Type Title = typeof (DocumentContact.title);
    public System.Type FirstName = typeof (DocumentContact.firstName);
    public System.Type LastName = typeof (DocumentContact.lastName);
    public System.Type Salutation = typeof (DocumentContact.salutation);
    public System.Type Attention = typeof (DocumentContact.attention);
    public System.Type Email = typeof (DocumentContact.email);
    public System.Type Phone1 = typeof (DocumentContact.phone1);
    public System.Type Phone1Type = typeof (DocumentContact.phone1Type);
    public System.Type Phone2 = typeof (DocumentContact.phone2);
    public System.Type Phone2Type = typeof (DocumentContact.phone2Type);
    public System.Type Phone3 = typeof (DocumentContact.phone3);
    public System.Type Phone3Type = typeof (DocumentContact.phone3Type);
    public System.Type Fax = typeof (DocumentContact.fax);
    public System.Type FaxType = typeof (DocumentContact.faxType);
    public System.Type OverrideContact = typeof (DocumentContact.overrideContact);
    public System.Type ConsentAgreement = typeof (DocumentContact.consentAgreement);
    public System.Type ConsentDate = typeof (DocumentContact.consentDate);
    public System.Type ConsentExpirationDate = typeof (DocumentContact.consentExpirationDate);

    public System.Type Extension => typeof (DocumentContact);

    public System.Type Table => this._table;

    public DocumentContactMapping(System.Type table) => this._table = table;
  }

  protected class DocumentAddressMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type OverrideAddress = typeof (DocumentAddress.overrideAddress);
    public System.Type AddressLine1 = typeof (DocumentAddress.addressLine1);
    public System.Type AddressLine2 = typeof (DocumentAddress.addressLine2);
    public System.Type AddressLine3 = typeof (DocumentAddress.addressLine3);
    public System.Type City = typeof (DocumentAddress.city);
    public System.Type CountryID = typeof (DocumentAddress.countryID);
    public System.Type State = typeof (DocumentAddress.state);
    public System.Type PostalCode = typeof (DocumentAddress.postalCode);
    public System.Type IsValidated = typeof (DocumentAddress.isValidated);
    public System.Type Department = typeof (DocumentAddress.department);
    public System.Type SubDepartment = typeof (DocumentAddress.subDepartment);
    public System.Type StreetName = typeof (DocumentAddress.streetName);
    public System.Type BuildingNumber = typeof (DocumentAddress.buildingNumber);
    public System.Type BuildingName = typeof (DocumentAddress.buildingName);
    public System.Type Floor = typeof (DocumentAddress.floor);
    public System.Type UnitNumber = typeof (DocumentAddress.unitNumber);
    public System.Type PostBox = typeof (DocumentAddress.postBox);
    public System.Type Room = typeof (DocumentAddress.room);
    public System.Type TownLocationName = typeof (DocumentAddress.townLocationName);
    public System.Type DistrictName = typeof (DocumentAddress.districtName);
    public System.Type Latitude = typeof (DocumentAddress.latitude);
    public System.Type Longitude = typeof (DocumentAddress.longitude);

    public System.Type Extension => typeof (DocumentAddress);

    public System.Type Table => this._table;

    public DocumentAddressMapping(System.Type table) => this._table = table;
  }
}
