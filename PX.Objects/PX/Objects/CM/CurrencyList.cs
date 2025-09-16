// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Contains general properties of the currencies that are stored in the system and are used during registration of documents across all modules.
/// The DAC provides such information as code, description, and precision, which are required for amounts given in a particular currency.
/// Financial settings associated with the currency (such as various accounts and subaccounts) are stored separately
/// in the records of the <see cref="T:PX.Objects.CM.Currency" /> type.
/// Records of this type are edited on the Currencies (CM202000) form (which corresponds to the <see cref="T:PX.Objects.CM.CurrencyMaint" /> graph).
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (CurrencyMaint)}, new Type[] {typeof (Select<CurrencyList, Where<CurrencyList.curyID, Equal<Current<CurrencyList.curyID>>>>)})]
[PXCacheName("Currency")]
[Serializable]
public class CurrencyList : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CuryID;
  protected string _Description;
  protected string _CurySymbol;
  protected string _CuryCaption;
  protected short? _DecimalPlaces;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  private bool? _IsActive;
  private bool? _IsFinancial;

  [PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXFieldDescription]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Currency Symbol")]
  public virtual string CurySymbol
  {
    get => this._CurySymbol;
    set => this._CurySymbol = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Currency Caption")]
  public virtual string CuryCaption
  {
    get => this._CuryCaption;
    set => this._CuryCaption = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 4)]
  [PXDefault(2)]
  [PXUIField(DisplayName = "Decimal Precision")]
  public virtual short? DecimalPlaces
  {
    get => this._DecimalPlaces;
    set => this._DecimalPlaces = value;
  }

  /// <summary>
  /// Decimal places according ISO 4217 (not editable by user)
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 4)]
  public virtual short? ISODecimalPlaces { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIEnabled(typeof (Where<CurrencyList.isFinancial, NotEqual<True>>))]
  [PXUIField]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (CurrencyList.isActive))]
  [PXUIField]
  public virtual bool? IsFinancial
  {
    get => this._IsFinancial;
    set => this._IsFinancial = value;
  }

  public class PK : PrimaryKeyOf<CurrencyList>.By<CurrencyList.curyID>
  {
    public static CurrencyList Find(PXGraph graph, string curyID, PKFindOptions options = 0)
    {
      return (CurrencyList) PrimaryKeyOf<CurrencyList>.By<CurrencyList.curyID>.FindBy(graph, (object) curyID, options);
    }
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyList.curyID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyList.description>
  {
  }

  public abstract class curySymbol : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyList.curySymbol>
  {
  }

  public abstract class curyCaption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyList.curyCaption>
  {
  }

  public abstract class decimalPlaces : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CurrencyList.decimalPlaces>
  {
  }

  public abstract class isoDecimalPlaces : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CurrencyList.isoDecimalPlaces>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CurrencyList.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyList.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyList.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CurrencyList.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyList.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyList.lastModifiedDateTime>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyList.isActive>
  {
  }

  public abstract class isFinancial : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyList.isFinancial>
  {
  }
}
