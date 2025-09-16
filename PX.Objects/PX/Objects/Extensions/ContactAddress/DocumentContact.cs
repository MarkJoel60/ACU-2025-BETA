// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.ContactAddress.DocumentContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Extensions.ContactAddress;

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

  public virtual string EMail { get; set; }

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
}
