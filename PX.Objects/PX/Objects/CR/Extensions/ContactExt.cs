// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ContactExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions;

[PXHidden]
[Serializable]
public sealed class ContactExt : PXCacheExtension<
#nullable disable
Contact>
{
  [PXBool]
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.isPrimary), typeof (Contact.isActive)})]
  public bool? CanBeMadePrimary
  {
    get
    {
      bool? isPrimary = this.Base.IsPrimary;
      bool flag = false;
      return new bool?(isPrimary.GetValueOrDefault() == flag & isPrimary.HasValue && this.Base.IsActive.GetValueOrDefault());
    }
  }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.firstName), typeof (Contact.lastName), typeof (Contact.salutation), typeof (Contact.eMail), typeof (Contact.phone1), typeof (Contact.phone2)})]
  [PXDBCalced(typeof (True), typeof (bool))]
  public bool? IsMeaningfull
  {
    get
    {
      return new bool?(this.Base.FirstName != null || this.Base.LastName != null || this.Base.Salutation != null || this.Base.EMail != null || this.Base.Phone1 != null || this.Base.Phone2 != null);
    }
  }

  [PXBool]
  public bool? IsAddedAsExt { get; set; }

  public abstract class canBeMadePrimary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContactExt.canBeMadePrimary>
  {
  }

  public abstract class isMeaningfull : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContactExt.isMeaningfull>
  {
  }

  public abstract class isAddedAsExt : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContactExt.isAddedAsExt>
  {
  }
}
