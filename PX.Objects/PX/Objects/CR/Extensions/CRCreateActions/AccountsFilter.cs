// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.AccountsFilter
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
public class AccountsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IClassIdFilter
{
  [PXDefault]
  [PXDBString]
  [PXDimension("BIZACCT")]
  [PXUIField(DisplayName = "Business Account ID", Required = true)]
  public virtual 
  #nullable disable
  string BAccountID { get; set; }

  [PXDefault]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Business Account Name", Required = true)]
  public virtual string AccountName { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Business Account Class")]
  [PXSelector(typeof (CRCustomerClass.cRCustomerClassID), DescriptionField = typeof (CRCustomerClass.description))]
  public virtual string AccountClass { get; set; }

  string IClassIdFilter.ClassID => this.AccountClass;

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Link Contact to Account")]
  public virtual bool? LinkContactToAccount { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  public virtual bool? NeedToUse { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountsFilter.bAccountID>
  {
  }

  public abstract class accountName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountsFilter.accountName>
  {
  }

  public abstract class accountClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountsFilter.accountClass>
  {
  }

  public abstract class linkContactToAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountsFilter.linkContactToAccount>
  {
  }

  public abstract class needToUse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AccountsFilter.needToUse>
  {
  }
}
