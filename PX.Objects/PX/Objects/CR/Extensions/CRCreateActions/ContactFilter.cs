// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.ContactFilter
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
[Serializable]
public class ContactFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IClassIdFilter
{
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "First Name")]
  public virtual 
  #nullable disable
  string FirstName { get; set; }

  [PXDefault]
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Name", Required = true)]
  public virtual string LastName { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string FullName { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Salutation { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PhoneValidation]
  public virtual string Phone1 { get; set; }

  [PXDBString(3)]
  [PXDefault("B1")]
  [PXUIField(DisplayName = "Phone 1 Type")]
  [PhoneTypes]
  public virtual string Phone1Type { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PhoneValidation]
  public virtual string Phone2 { get; set; }

  [PXDBString(3)]
  [PXDefault("C")]
  [PXUIField(DisplayName = "Phone 2 Type")]
  [PhoneTypes]
  public virtual string Phone2Type { get; set; }

  [PXDBEmail]
  [PXUIField]
  [PXDefault]
  public virtual string Email { get; set; }

  [PXDefault(typeof (CRSetup.defaultContactClassID))]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Contact Class")]
  [PXSelector(typeof (CRContactClass.classID), DescriptionField = typeof (CRContactClass.description))]
  public virtual string ContactClass { get; set; }

  string IClassIdFilter.ClassID => this.ContactClass;

  [PXBool]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  public virtual bool? NeedToUse { get; set; }

  public abstract class firstName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.firstName>
  {
  }

  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.lastName>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.fullName>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.salutation>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.phone1>
  {
  }

  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.phone1Type>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.phone2>
  {
  }

  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.phone2Type>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.email>
  {
  }

  public abstract class contactClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactFilter.contactClass>
  {
  }

  public abstract class needToUse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContactFilter.needToUse>
  {
  }
}
