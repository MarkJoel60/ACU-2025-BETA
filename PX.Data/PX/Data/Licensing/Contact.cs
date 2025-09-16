// Decompiled with JetBrains decompiler
// Type: PX.Data.Licensing.Contact
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Licensing;

[PXHidden]
[Serializable]
public class Contact : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  public virtual int? BAccountID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? UserID { get; set; }

  [PXDBInt]
  public virtual int? ContactID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  public virtual 
  #nullable disable
  string FullName { get; set; }

  [PXDBEmail]
  public virtual string EMail { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string WebSite { get; set; }

  [PXDBString(50)]
  public virtual string Phone1 { get; set; }

  [PXDBString(50)]
  public virtual string Phone2 { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.bAccountID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contact.userID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.contactID>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.fullName>
  {
  }

  public abstract class eMail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.eMail>
  {
  }

  public abstract class webSite : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.webSite>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.phone1>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.phone2>
  {
  }
}
