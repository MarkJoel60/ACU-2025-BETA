// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.IContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public interface IContact : INotable
{
  int? ContactID { get; set; }

  int? BAccountID { get; set; }

  int? BAccountContactID { get; set; }

  int? RevisionID { get; set; }

  bool? IsDefaultContact { get; set; }

  string FullName { get; set; }

  string Salutation { get; set; }

  string Attention { get; set; }

  string Title { get; set; }

  string Phone1 { get; set; }

  string Phone1Type { get; set; }

  string Phone2 { get; set; }

  string Phone2Type { get; set; }

  string Phone3 { get; set; }

  string Phone3Type { get; set; }

  string Fax { get; set; }

  string FaxType { get; set; }

  string Email { get; set; }
}
