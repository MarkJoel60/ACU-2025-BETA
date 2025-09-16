// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.DocumentContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
[PXHidden]
public class DocumentContact : PXMappedCacheExtension
{
  public virtual 
  #nullable disable
  string FullName { get; set; }

  public virtual string Title { get; set; }

  public virtual string FirstName { get; set; }

  public virtual string LastName { get; set; }

  public virtual string Salutation { get; set; }

  public virtual string Attention { get; set; }

  public virtual string Email { get; set; }

  public virtual string WebSite { get; set; }

  public virtual string Phone1 { get; set; }

  public virtual string Phone1Type { get; set; }

  public virtual string Phone2 { get; set; }

  public virtual string Phone2Type { get; set; }

  public virtual string Phone3 { get; set; }

  public virtual string Phone3Type { get; set; }

  public virtual string Fax { get; set; }

  public virtual string FaxType { get; set; }

  public virtual bool? IsDefaultContact { get; set; }

  public virtual bool? OverrideContact { get; set; }

  public virtual bool? ConsentAgreement { get; set; }

  public virtual DateTime? ConsentDate { get; set; }

  public virtual DateTime? ConsentExpirationDate { get; set; }

  public virtual int? DefAddressID { get; set; }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.fullName>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.title>
  {
  }

  public abstract class firstName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.firstName>
  {
  }

  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.lastName>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.salutation>
  {
  }

  public abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.attention>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.email>
  {
  }

  public abstract class webSite : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.webSite>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.phone1>
  {
  }

  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.phone1Type>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.phone2>
  {
  }

  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.phone2Type>
  {
  }

  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.phone3>
  {
  }

  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.phone3Type>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.fax>
  {
  }

  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContact.faxType>
  {
  }

  public abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DocumentContact.isDefaultContact>
  {
  }

  public abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DocumentContact.overrideContact>
  {
  }

  public abstract class consentAgreement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DocumentContact.consentAgreement>
  {
  }

  public abstract class consentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DocumentContact.consentDate>
  {
  }

  public abstract class consentExpirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DocumentContact.consentExpirationDate>
  {
  }

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DocumentContact.defAddressID>
  {
  }
}
