// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.PersonaType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class PersonaType
{
  private ItemIdType personaIdField;
  private string personaType1Field;
  private string personaObjectStatusField;
  private System.DateTime creationTimeField;
  private bool creationTimeFieldSpecified;
  private BodyContentAttributedValueType[] bodiesField;
  private string displayNameFirstLastSortKeyField;
  private string displayNameLastFirstSortKeyField;
  private string companyNameSortKeyField;
  private string homeCitySortKeyField;
  private string workCitySortKeyField;
  private string displayNameFirstLastHeaderField;
  private string displayNameLastFirstHeaderField;
  private string displayNameField;
  private string displayNameFirstLastField;
  private string displayNameLastFirstField;
  private string fileAsField;
  private string fileAsIdField;
  private string displayNamePrefixField;
  private string givenNameField;
  private string middleNameField;
  private string surnameField;
  private string generationField;
  private string nicknameField;
  private string yomiCompanyNameField;
  private string yomiFirstNameField;
  private string yomiLastNameField;
  private string titleField;
  private string departmentField;
  private string companyNameField;
  private string locationField;
  private EmailAddressType emailAddressField;
  private EmailAddressType[] emailAddressesField;
  private PersonaPhoneNumberType phoneNumberField;
  private string imAddressField;
  private string homeCityField;
  private string workCityField;
  private int relevanceScoreField;
  private bool relevanceScoreFieldSpecified;
  private FolderIdType[] folderIdsField;
  private PersonaAttributionType[] attributionsField;
  private StringAttributedValueType[] displayNamesField;
  private StringAttributedValueType[] fileAsesField;
  private StringAttributedValueType[] fileAsIdsField;
  private StringAttributedValueType[] displayNamePrefixesField;
  private StringAttributedValueType[] givenNamesField;
  private StringAttributedValueType[] middleNamesField;
  private StringAttributedValueType[] surnamesField;
  private StringAttributedValueType[] generationsField;
  private StringAttributedValueType[] nicknamesField;
  private StringAttributedValueType[] initialsField;
  private StringAttributedValueType[] yomiCompanyNamesField;
  private StringAttributedValueType[] yomiFirstNamesField;
  private StringAttributedValueType[] yomiLastNamesField;
  private PhoneNumberAttributedValueType[] businessPhoneNumbersField;
  private PhoneNumberAttributedValueType[] businessPhoneNumbers2Field;
  private PhoneNumberAttributedValueType[] homePhonesField;
  private PhoneNumberAttributedValueType[] homePhones2Field;
  private PhoneNumberAttributedValueType[] mobilePhonesField;
  private PhoneNumberAttributedValueType[] mobilePhones2Field;
  private PhoneNumberAttributedValueType[] assistantPhoneNumbersField;
  private PhoneNumberAttributedValueType[] callbackPhonesField;
  private PhoneNumberAttributedValueType[] carPhonesField;
  private PhoneNumberAttributedValueType[] homeFaxesField;
  private PhoneNumberAttributedValueType[] organizationMainPhonesField;
  private PhoneNumberAttributedValueType[] otherFaxesField;
  private PhoneNumberAttributedValueType[] otherTelephonesField;
  private PhoneNumberAttributedValueType[] otherPhones2Field;
  private PhoneNumberAttributedValueType[] pagersField;
  private PhoneNumberAttributedValueType[] radioPhonesField;
  private PhoneNumberAttributedValueType[] telexNumbersField;
  private PhoneNumberAttributedValueType[] tTYTDDPhoneNumbersField;
  private PhoneNumberAttributedValueType[] workFaxesField;
  private EmailAddressAttributedValueType[] emails1Field;
  private EmailAddressAttributedValueType[] emails2Field;
  private EmailAddressAttributedValueType[] emails3Field;
  private StringAttributedValueType[] businessHomePagesField;
  private StringAttributedValueType[] personalHomePagesField;
  private StringAttributedValueType[] officeLocationsField;
  private StringAttributedValueType[] imAddressesField;
  private StringAttributedValueType[] imAddresses2Field;
  private StringAttributedValueType[] imAddresses3Field;
  private PostalAddressAttributedValueType[] businessAddressesField;
  private PostalAddressAttributedValueType[] homeAddressesField;
  private PostalAddressAttributedValueType[] otherAddressesField;
  private StringAttributedValueType[] titlesField;
  private StringAttributedValueType[] departmentsField;
  private StringAttributedValueType[] companyNamesField;
  private StringAttributedValueType[] managersField;
  private StringAttributedValueType[] assistantNamesField;
  private StringAttributedValueType[] professionsField;
  private StringAttributedValueType[] spouseNamesField;
  private StringArrayAttributedValueType[] childrenField;
  private StringAttributedValueType[] schoolsField;
  private StringAttributedValueType[] hobbiesField;
  private StringAttributedValueType[] weddingAnniversariesField;
  private StringAttributedValueType[] birthdaysField;
  private StringAttributedValueType[] locationsField;
  private ExtendedPropertyAttributedValueType[] extendedPropertiesField;

  /// <remarks />
  public ItemIdType PersonaId
  {
    get => this.personaIdField;
    set => this.personaIdField = value;
  }

  /// <remarks />
  [XmlElement("PersonaType")]
  public string PersonaType1
  {
    get => this.personaType1Field;
    set => this.personaType1Field = value;
  }

  /// <remarks />
  public string PersonaObjectStatus
  {
    get => this.personaObjectStatusField;
    set => this.personaObjectStatusField = value;
  }

  /// <remarks />
  public System.DateTime CreationTime
  {
    get => this.creationTimeField;
    set => this.creationTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CreationTimeSpecified
  {
    get => this.creationTimeFieldSpecified;
    set => this.creationTimeFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("BodyContentAttributedValue", IsNullable = false)]
  public BodyContentAttributedValueType[] Bodies
  {
    get => this.bodiesField;
    set => this.bodiesField = value;
  }

  /// <remarks />
  public string DisplayNameFirstLastSortKey
  {
    get => this.displayNameFirstLastSortKeyField;
    set => this.displayNameFirstLastSortKeyField = value;
  }

  /// <remarks />
  public string DisplayNameLastFirstSortKey
  {
    get => this.displayNameLastFirstSortKeyField;
    set => this.displayNameLastFirstSortKeyField = value;
  }

  /// <remarks />
  public string CompanyNameSortKey
  {
    get => this.companyNameSortKeyField;
    set => this.companyNameSortKeyField = value;
  }

  /// <remarks />
  public string HomeCitySortKey
  {
    get => this.homeCitySortKeyField;
    set => this.homeCitySortKeyField = value;
  }

  /// <remarks />
  public string WorkCitySortKey
  {
    get => this.workCitySortKeyField;
    set => this.workCitySortKeyField = value;
  }

  /// <remarks />
  public string DisplayNameFirstLastHeader
  {
    get => this.displayNameFirstLastHeaderField;
    set => this.displayNameFirstLastHeaderField = value;
  }

  /// <remarks />
  public string DisplayNameLastFirstHeader
  {
    get => this.displayNameLastFirstHeaderField;
    set => this.displayNameLastFirstHeaderField = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public string DisplayNameFirstLast
  {
    get => this.displayNameFirstLastField;
    set => this.displayNameFirstLastField = value;
  }

  /// <remarks />
  public string DisplayNameLastFirst
  {
    get => this.displayNameLastFirstField;
    set => this.displayNameLastFirstField = value;
  }

  /// <remarks />
  public string FileAs
  {
    get => this.fileAsField;
    set => this.fileAsField = value;
  }

  /// <remarks />
  public string FileAsId
  {
    get => this.fileAsIdField;
    set => this.fileAsIdField = value;
  }

  /// <remarks />
  public string DisplayNamePrefix
  {
    get => this.displayNamePrefixField;
    set => this.displayNamePrefixField = value;
  }

  /// <remarks />
  public string GivenName
  {
    get => this.givenNameField;
    set => this.givenNameField = value;
  }

  /// <remarks />
  public string MiddleName
  {
    get => this.middleNameField;
    set => this.middleNameField = value;
  }

  /// <remarks />
  public string Surname
  {
    get => this.surnameField;
    set => this.surnameField = value;
  }

  /// <remarks />
  public string Generation
  {
    get => this.generationField;
    set => this.generationField = value;
  }

  /// <remarks />
  public string Nickname
  {
    get => this.nicknameField;
    set => this.nicknameField = value;
  }

  /// <remarks />
  public string YomiCompanyName
  {
    get => this.yomiCompanyNameField;
    set => this.yomiCompanyNameField = value;
  }

  /// <remarks />
  public string YomiFirstName
  {
    get => this.yomiFirstNameField;
    set => this.yomiFirstNameField = value;
  }

  /// <remarks />
  public string YomiLastName
  {
    get => this.yomiLastNameField;
    set => this.yomiLastNameField = value;
  }

  /// <remarks />
  public string Title
  {
    get => this.titleField;
    set => this.titleField = value;
  }

  /// <remarks />
  public string Department
  {
    get => this.departmentField;
    set => this.departmentField = value;
  }

  /// <remarks />
  public string CompanyName
  {
    get => this.companyNameField;
    set => this.companyNameField = value;
  }

  /// <remarks />
  public string Location
  {
    get => this.locationField;
    set => this.locationField = value;
  }

  /// <remarks />
  public EmailAddressType EmailAddress
  {
    get => this.emailAddressField;
    set => this.emailAddressField = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public EmailAddressType[] EmailAddresses
  {
    get => this.emailAddressesField;
    set => this.emailAddressesField = value;
  }

  /// <remarks />
  public PersonaPhoneNumberType PhoneNumber
  {
    get => this.phoneNumberField;
    set => this.phoneNumberField = value;
  }

  /// <remarks />
  public string ImAddress
  {
    get => this.imAddressField;
    set => this.imAddressField = value;
  }

  /// <remarks />
  public string HomeCity
  {
    get => this.homeCityField;
    set => this.homeCityField = value;
  }

  /// <remarks />
  public string WorkCity
  {
    get => this.workCityField;
    set => this.workCityField = value;
  }

  /// <remarks />
  public int RelevanceScore
  {
    get => this.relevanceScoreField;
    set => this.relevanceScoreField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool RelevanceScoreSpecified
  {
    get => this.relevanceScoreFieldSpecified;
    set => this.relevanceScoreFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("FolderId", IsNullable = false)]
  public FolderIdType[] FolderIds
  {
    get => this.folderIdsField;
    set => this.folderIdsField = value;
  }

  /// <remarks />
  [XmlArrayItem("Attribution", IsNullable = false)]
  public PersonaAttributionType[] Attributions
  {
    get => this.attributionsField;
    set => this.attributionsField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] DisplayNames
  {
    get => this.displayNamesField;
    set => this.displayNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] FileAses
  {
    get => this.fileAsesField;
    set => this.fileAsesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] FileAsIds
  {
    get => this.fileAsIdsField;
    set => this.fileAsIdsField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] DisplayNamePrefixes
  {
    get => this.displayNamePrefixesField;
    set => this.displayNamePrefixesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] GivenNames
  {
    get => this.givenNamesField;
    set => this.givenNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] MiddleNames
  {
    get => this.middleNamesField;
    set => this.middleNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Surnames
  {
    get => this.surnamesField;
    set => this.surnamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Generations
  {
    get => this.generationsField;
    set => this.generationsField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Nicknames
  {
    get => this.nicknamesField;
    set => this.nicknamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Initials
  {
    get => this.initialsField;
    set => this.initialsField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] YomiCompanyNames
  {
    get => this.yomiCompanyNamesField;
    set => this.yomiCompanyNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] YomiFirstNames
  {
    get => this.yomiFirstNamesField;
    set => this.yomiFirstNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] YomiLastNames
  {
    get => this.yomiLastNamesField;
    set => this.yomiLastNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] BusinessPhoneNumbers
  {
    get => this.businessPhoneNumbersField;
    set => this.businessPhoneNumbersField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] BusinessPhoneNumbers2
  {
    get => this.businessPhoneNumbers2Field;
    set => this.businessPhoneNumbers2Field = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] HomePhones
  {
    get => this.homePhonesField;
    set => this.homePhonesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] HomePhones2
  {
    get => this.homePhones2Field;
    set => this.homePhones2Field = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] MobilePhones
  {
    get => this.mobilePhonesField;
    set => this.mobilePhonesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] MobilePhones2
  {
    get => this.mobilePhones2Field;
    set => this.mobilePhones2Field = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] AssistantPhoneNumbers
  {
    get => this.assistantPhoneNumbersField;
    set => this.assistantPhoneNumbersField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] CallbackPhones
  {
    get => this.callbackPhonesField;
    set => this.callbackPhonesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] CarPhones
  {
    get => this.carPhonesField;
    set => this.carPhonesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] HomeFaxes
  {
    get => this.homeFaxesField;
    set => this.homeFaxesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] OrganizationMainPhones
  {
    get => this.organizationMainPhonesField;
    set => this.organizationMainPhonesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] OtherFaxes
  {
    get => this.otherFaxesField;
    set => this.otherFaxesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] OtherTelephones
  {
    get => this.otherTelephonesField;
    set => this.otherTelephonesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] OtherPhones2
  {
    get => this.otherPhones2Field;
    set => this.otherPhones2Field = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] Pagers
  {
    get => this.pagersField;
    set => this.pagersField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] RadioPhones
  {
    get => this.radioPhonesField;
    set => this.radioPhonesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] TelexNumbers
  {
    get => this.telexNumbersField;
    set => this.telexNumbersField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] TTYTDDPhoneNumbers
  {
    get => this.tTYTDDPhoneNumbersField;
    set => this.tTYTDDPhoneNumbersField = value;
  }

  /// <remarks />
  [XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
  public PhoneNumberAttributedValueType[] WorkFaxes
  {
    get => this.workFaxesField;
    set => this.workFaxesField = value;
  }

  /// <remarks />
  [XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
  public EmailAddressAttributedValueType[] Emails1
  {
    get => this.emails1Field;
    set => this.emails1Field = value;
  }

  /// <remarks />
  [XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
  public EmailAddressAttributedValueType[] Emails2
  {
    get => this.emails2Field;
    set => this.emails2Field = value;
  }

  /// <remarks />
  [XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
  public EmailAddressAttributedValueType[] Emails3
  {
    get => this.emails3Field;
    set => this.emails3Field = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] BusinessHomePages
  {
    get => this.businessHomePagesField;
    set => this.businessHomePagesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] PersonalHomePages
  {
    get => this.personalHomePagesField;
    set => this.personalHomePagesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] OfficeLocations
  {
    get => this.officeLocationsField;
    set => this.officeLocationsField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] ImAddresses
  {
    get => this.imAddressesField;
    set => this.imAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] ImAddresses2
  {
    get => this.imAddresses2Field;
    set => this.imAddresses2Field = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] ImAddresses3
  {
    get => this.imAddresses3Field;
    set => this.imAddresses3Field = value;
  }

  /// <remarks />
  [XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
  public PostalAddressAttributedValueType[] BusinessAddresses
  {
    get => this.businessAddressesField;
    set => this.businessAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
  public PostalAddressAttributedValueType[] HomeAddresses
  {
    get => this.homeAddressesField;
    set => this.homeAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
  public PostalAddressAttributedValueType[] OtherAddresses
  {
    get => this.otherAddressesField;
    set => this.otherAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Titles
  {
    get => this.titlesField;
    set => this.titlesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Departments
  {
    get => this.departmentsField;
    set => this.departmentsField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] CompanyNames
  {
    get => this.companyNamesField;
    set => this.companyNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Managers
  {
    get => this.managersField;
    set => this.managersField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] AssistantNames
  {
    get => this.assistantNamesField;
    set => this.assistantNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Professions
  {
    get => this.professionsField;
    set => this.professionsField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] SpouseNames
  {
    get => this.spouseNamesField;
    set => this.spouseNamesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringArrayAttributedValue", IsNullable = false)]
  public StringArrayAttributedValueType[] Children
  {
    get => this.childrenField;
    set => this.childrenField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Schools
  {
    get => this.schoolsField;
    set => this.schoolsField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Hobbies
  {
    get => this.hobbiesField;
    set => this.hobbiesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] WeddingAnniversaries
  {
    get => this.weddingAnniversariesField;
    set => this.weddingAnniversariesField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Birthdays
  {
    get => this.birthdaysField;
    set => this.birthdaysField = value;
  }

  /// <remarks />
  [XmlArrayItem("StringAttributedValue", IsNullable = false)]
  public StringAttributedValueType[] Locations
  {
    get => this.locationsField;
    set => this.locationsField = value;
  }

  /// <remarks />
  [XmlArrayItem("ExtendedPropertyAttributedValue", IsNullable = false)]
  public ExtendedPropertyAttributedValueType[] ExtendedProperties
  {
    get => this.extendedPropertiesField;
    set => this.extendedPropertiesField = value;
  }
}
