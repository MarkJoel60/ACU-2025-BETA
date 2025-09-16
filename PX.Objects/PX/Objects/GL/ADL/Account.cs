// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ADL.Account
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.GL.ADL;

public class Account : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IIncludable, IRestricted
{
  protected int? _AccountID;
  protected 
  #nullable disable
  string _Type;
  protected string _CuryID;
  protected byte[] _GroupMask;
  protected bool? _Included;
  public const string Default = "0";

  [PXDBIdentity]
  [PXUIField]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [AccountType.List]
  [PXUIField]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  [PXBool]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public virtual bool? TransactionsForGivenCurrencyExists { get; set; }

  [PXUnboundDefault(false)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID { get; set; }

  public class PK : PrimaryKeyOf<Account>.By<Account.accountID>
  {
    public static Account Find(PXGraph graph, int? accountID, PKFindOptions options = 0)
    {
      return (Account) PrimaryKeyOf<Account>.By<Account.accountID>.FindBy(graph, (object) accountID, options);
    }
  }

  public static class FK
  {
    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Account>.By<Account.curyID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<Account>.By<Account.taxCategoryID>
    {
    }
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Account.accountID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.type>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.curyID>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Account.groupMask>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class transactionsForGivenCurrencyExists : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Account.transactionsForGivenCurrencyExists>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Account.included>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.taxCategoryID>
  {
  }
}
