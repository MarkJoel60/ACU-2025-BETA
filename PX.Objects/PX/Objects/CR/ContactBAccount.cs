// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactBAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[Obsolete("This class is not used anymore")]
[PXProjection(typeof (Select<PX.Objects.CR.Contact>), Persistent = false)]
public class ContactBAccount : PX.Objects.CR.Contact
{
  [PXDBInt(IsKey = true, BqlTable = typeof (PX.Objects.CR.Contact))]
  [PXUIField]
  public override int? ContactID { get; set; }

  [PXString]
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.fullName)})]
  public virtual 
  #nullable disable
  string Contact => !string.IsNullOrEmpty(this.DisplayName) ? this.DisplayName : this.FullName;

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactBAccount.contactID>
  {
  }

  public abstract class contact : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactBAccount.contact>
  {
  }

  public new abstract class contactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactBAccount.contactType>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactBAccount.bAccountID>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContactBAccount.parentBAccountID>
  {
  }
}
