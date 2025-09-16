// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountsPreloadFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class AccountsPreloadFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXDimensionSelector("ACCOUNT", typeof (Search<Account.accountID, Where<Account.accountCD, Like<Current<SelectedNode.accountMaskWildcard>>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>>>, OrderBy<Asc<Account.accountCD>>>), typeof (Account.accountCD), DescriptionField = typeof (Account.description))]
  [PXUIField]
  public virtual int? FromAccount { get; set; }

  [PXInt]
  [PXDimensionSelector("ACCOUNT", typeof (Search<Account.accountID, Where<Account.accountCD, Like<Current<SelectedNode.accountMaskWildcard>>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>>>, OrderBy<Asc<Account.accountCD>>>), typeof (Account.accountCD), DescriptionField = typeof (Account.description))]
  [PXUIField]
  public virtual int? ToAccount { get; set; }

  [SubAccountRaw(DisplayName = "Subaccount Mask")]
  public virtual 
  #nullable disable
  string SubIDFilter { get; set; }

  [PXString(30, IsUnicode = true)]
  public virtual string SubCDWildcard
  {
    get => SubCDUtils.CreateSubCDWildcard(this.SubIDFilter, "SUBACCOUNT");
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string AccountCDWildcard { get; set; }

  public abstract class fromAccount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountsPreloadFilter.fromAccount>
  {
  }

  public abstract class toAccount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountsPreloadFilter.toAccount>
  {
  }

  public abstract class subIDFilter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountsPreloadFilter.subIDFilter>
  {
  }

  public abstract class subCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountsPreloadFilter.subCDWildcard>
  {
  }

  public abstract class accountCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountsPreloadFilter.accountCDWildcard>
  {
  }
}
