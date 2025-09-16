// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.RichContact
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.RichTextEdit;

/// <exclude />
public class RichContact : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Contact ID", Visibility = PXUIVisibility.Invisible)]
  public virtual int? ContactID { get; set; }

  [PXDBEmail]
  [PXUIField(DisplayName = "Email", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual 
  #nullable disable
  string EMail { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "First Name")]
  public virtual string FirstName { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Middle Name")]
  public virtual string MidName { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Name")]
  public virtual string LastName { get; set; }

  /// <exclude />
  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RichContact.contactID>
  {
  }

  /// <exclude />
  public abstract class eMail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RichContact.eMail>
  {
  }

  /// <exclude />
  public abstract class firstName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RichContact.firstName>
  {
  }

  public abstract class midName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RichContact.midName>
  {
  }

  /// <exclude />
  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RichContact.lastName>
  {
  }
}
