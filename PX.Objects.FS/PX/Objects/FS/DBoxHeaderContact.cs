// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DBoxHeaderContact
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

#nullable disable
namespace PX.Objects.FS;

public class DBoxHeaderContact
{
  public virtual string Title { get; set; }

  public virtual string Attention { get; set; }

  public virtual string FullName { get; set; }

  public virtual string Email { get; set; }

  public virtual string Phone1 { get; set; }

  public virtual string Phone2 { get; set; }

  public virtual string Phone3 { get; set; }

  public virtual string Fax { get; set; }

  public static implicit operator DBoxHeaderContact(PX.Objects.CR.CRContact contact)
  {
    if (contact == null)
      return (DBoxHeaderContact) null;
    return new DBoxHeaderContact()
    {
      Title = contact.Title,
      Attention = contact.Attention,
      FullName = contact.FullName,
      Email = contact.Email,
      Phone1 = contact.Phone1,
      Phone2 = contact.Phone2,
      Phone3 = contact.Phone3,
      Fax = contact.Fax
    };
  }
}
