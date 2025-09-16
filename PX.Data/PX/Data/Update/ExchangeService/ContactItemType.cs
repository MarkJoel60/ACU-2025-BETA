// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ContactItemType
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
public class ContactItemType : ItemType
{
  private string fileAsField;
  private FileAsMappingType fileAsMappingField;
  private bool fileAsMappingFieldSpecified;
  private string displayNameField;
  private string givenNameField;
  private string initialsField;
  private string middleNameField;
  private string nicknameField;
  private CompleteNameType completeNameField;
  private string companyNameField;
  private EmailAddressDictionaryEntryType[] emailAddressesField;
  private PhysicalAddressDictionaryEntryType[] physicalAddressesField;
  private PhoneNumberDictionaryEntryType[] phoneNumbersField;
  private string assistantNameField;
  private System.DateTime birthdayField;
  private bool birthdayFieldSpecified;
  private string businessHomePageField;
  private string[] childrenField;
  private string[] companiesField;
  private ContactSourceType contactSourceField;
  private bool contactSourceFieldSpecified;
  private string departmentField;
  private string generationField;
  private ImAddressDictionaryEntryType[] imAddressesField;
  private string jobTitleField;
  private string managerField;
  private string mileageField;
  private string officeLocationField;
  private PhysicalAddressIndexType postalAddressIndexField;
  private bool postalAddressIndexFieldSpecified;
  private string professionField;
  private string spouseNameField;
  private string surnameField;
  private System.DateTime weddingAnniversaryField;
  private bool weddingAnniversaryFieldSpecified;
  private bool hasPictureField;
  private bool hasPictureFieldSpecified;
  private string phoneticFullNameField;
  private string phoneticFirstNameField;
  private string phoneticLastNameField;
  private string aliasField;
  private string notesField;
  private byte[] photoField;
  private byte[][] userSMIMECertificateField;
  private byte[][] mSExchangeCertificateField;
  private string directoryIdField;
  private SingleRecipientType managerMailboxField;
  private EmailAddressType[] directReportsField;

  /// <remarks />
  public string FileAs
  {
    get => this.fileAsField;
    set => this.fileAsField = value;
  }

  /// <remarks />
  public FileAsMappingType FileAsMapping
  {
    get => this.fileAsMappingField;
    set => this.fileAsMappingField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool FileAsMappingSpecified
  {
    get => this.fileAsMappingFieldSpecified;
    set => this.fileAsMappingFieldSpecified = value;
  }

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public string GivenName
  {
    get => this.givenNameField;
    set => this.givenNameField = value;
  }

  /// <remarks />
  public string Initials
  {
    get => this.initialsField;
    set => this.initialsField = value;
  }

  /// <remarks />
  public string MiddleName
  {
    get => this.middleNameField;
    set => this.middleNameField = value;
  }

  /// <remarks />
  public string Nickname
  {
    get => this.nicknameField;
    set => this.nicknameField = value;
  }

  /// <remarks />
  public CompleteNameType CompleteName
  {
    get => this.completeNameField;
    set => this.completeNameField = value;
  }

  /// <remarks />
  public string CompanyName
  {
    get => this.companyNameField;
    set => this.companyNameField = value;
  }

  /// <remarks />
  [XmlArrayItem("Entry", IsNullable = false)]
  public EmailAddressDictionaryEntryType[] EmailAddresses
  {
    get => this.emailAddressesField;
    set => this.emailAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Entry", IsNullable = false)]
  public PhysicalAddressDictionaryEntryType[] PhysicalAddresses
  {
    get => this.physicalAddressesField;
    set => this.physicalAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Entry", IsNullable = false)]
  public PhoneNumberDictionaryEntryType[] PhoneNumbers
  {
    get => this.phoneNumbersField;
    set => this.phoneNumbersField = value;
  }

  /// <remarks />
  public string AssistantName
  {
    get => this.assistantNameField;
    set => this.assistantNameField = value;
  }

  /// <remarks />
  public System.DateTime Birthday
  {
    get => this.birthdayField;
    set => this.birthdayField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool BirthdaySpecified
  {
    get => this.birthdayFieldSpecified;
    set => this.birthdayFieldSpecified = value;
  }

  /// <remarks />
  [XmlElement(DataType = "anyURI")]
  public string BusinessHomePage
  {
    get => this.businessHomePageField;
    set => this.businessHomePageField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] Children
  {
    get => this.childrenField;
    set => this.childrenField = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] Companies
  {
    get => this.companiesField;
    set => this.companiesField = value;
  }

  /// <remarks />
  public ContactSourceType ContactSource
  {
    get => this.contactSourceField;
    set => this.contactSourceField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ContactSourceSpecified
  {
    get => this.contactSourceFieldSpecified;
    set => this.contactSourceFieldSpecified = value;
  }

  /// <remarks />
  public string Department
  {
    get => this.departmentField;
    set => this.departmentField = value;
  }

  /// <remarks />
  public string Generation
  {
    get => this.generationField;
    set => this.generationField = value;
  }

  /// <remarks />
  [XmlArrayItem("Entry", IsNullable = false)]
  public ImAddressDictionaryEntryType[] ImAddresses
  {
    get => this.imAddressesField;
    set => this.imAddressesField = value;
  }

  /// <remarks />
  public string JobTitle
  {
    get => this.jobTitleField;
    set => this.jobTitleField = value;
  }

  /// <remarks />
  public string Manager
  {
    get => this.managerField;
    set => this.managerField = value;
  }

  /// <remarks />
  public string Mileage
  {
    get => this.mileageField;
    set => this.mileageField = value;
  }

  /// <remarks />
  public string OfficeLocation
  {
    get => this.officeLocationField;
    set => this.officeLocationField = value;
  }

  /// <remarks />
  public PhysicalAddressIndexType PostalAddressIndex
  {
    get => this.postalAddressIndexField;
    set => this.postalAddressIndexField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PostalAddressIndexSpecified
  {
    get => this.postalAddressIndexFieldSpecified;
    set => this.postalAddressIndexFieldSpecified = value;
  }

  /// <remarks />
  public string Profession
  {
    get => this.professionField;
    set => this.professionField = value;
  }

  /// <remarks />
  public string SpouseName
  {
    get => this.spouseNameField;
    set => this.spouseNameField = value;
  }

  /// <remarks />
  public string Surname
  {
    get => this.surnameField;
    set => this.surnameField = value;
  }

  /// <remarks />
  public System.DateTime WeddingAnniversary
  {
    get => this.weddingAnniversaryField;
    set => this.weddingAnniversaryField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool WeddingAnniversarySpecified
  {
    get => this.weddingAnniversaryFieldSpecified;
    set => this.weddingAnniversaryFieldSpecified = value;
  }

  /// <remarks />
  public bool HasPicture
  {
    get => this.hasPictureField;
    set => this.hasPictureField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasPictureSpecified
  {
    get => this.hasPictureFieldSpecified;
    set => this.hasPictureFieldSpecified = value;
  }

  /// <remarks />
  public string PhoneticFullName
  {
    get => this.phoneticFullNameField;
    set => this.phoneticFullNameField = value;
  }

  /// <remarks />
  public string PhoneticFirstName
  {
    get => this.phoneticFirstNameField;
    set => this.phoneticFirstNameField = value;
  }

  /// <remarks />
  public string PhoneticLastName
  {
    get => this.phoneticLastNameField;
    set => this.phoneticLastNameField = value;
  }

  /// <remarks />
  public string Alias
  {
    get => this.aliasField;
    set => this.aliasField = value;
  }

  /// <remarks />
  public string Notes
  {
    get => this.notesField;
    set => this.notesField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] Photo
  {
    get => this.photoField;
    set => this.photoField = value;
  }

  /// <remarks />
  [XmlArrayItem("Base64Binary", DataType = "base64Binary", IsNullable = false)]
  public byte[][] UserSMIMECertificate
  {
    get => this.userSMIMECertificateField;
    set => this.userSMIMECertificateField = value;
  }

  /// <remarks />
  [XmlArrayItem("Base64Binary", DataType = "base64Binary", IsNullable = false)]
  public byte[][] MSExchangeCertificate
  {
    get => this.mSExchangeCertificateField;
    set => this.mSExchangeCertificateField = value;
  }

  /// <remarks />
  public string DirectoryId
  {
    get => this.directoryIdField;
    set => this.directoryIdField = value;
  }

  /// <remarks />
  public SingleRecipientType ManagerMailbox
  {
    get => this.managerMailboxField;
    set => this.managerMailboxField = value;
  }

  /// <remarks />
  [XmlArrayItem("Mailbox", IsNullable = false)]
  public EmailAddressType[] DirectReports
  {
    get => this.directReportsField;
    set => this.directReportsField = value;
  }
}
