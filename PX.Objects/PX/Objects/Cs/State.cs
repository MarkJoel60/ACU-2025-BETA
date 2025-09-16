// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.State
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("State")]
[PXPrimaryGraph(new Type[] {typeof (CountryMaint)}, new Type[] {typeof (Select<State, Where<State.countryID, Equal<Current<State.countryID>>, And<State.stateID, Equal<Current<State.stateID>>>>>)})]
[Serializable]
public class State : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CountryID;
  protected string _StateID;
  protected string _Name;
  protected string _LocationCode;
  protected bool? _IsTaxRegistrationRequired;
  protected string _TaxRegistrationMask;
  protected string _TaxRegistrationRegexp;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>Indicates whether the record is selected or not.</summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">??")]
  [PXDefault(typeof (Country.countryID))]
  [PXUIField(DisplayName = "Country", Visible = false)]
  [PXSelector(typeof (Country.countryID), DirtyRead = true)]
  [PXParent(typeof (Select<Country, Where<Country.countryID, Equal<Current<State.countryID>>>>))]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual string StateID
  {
    get => this._StateID;
    set => this._StateID = value;
  }

  [PXDBLocalizableString(30, IsUnicode = true)]
  [PXUIField]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Validation Regexp")]
  public virtual string StateRegexp { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Location Code", Visible = false, FieldClass = "PayrollModule")]
  public virtual string LocationCode
  {
    get => this._LocationCode;
    set => this._LocationCode = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Registration Required")]
  public virtual bool? IsTaxRegistrationRequired
  {
    get => this._IsTaxRegistrationRequired;
    set => this._IsTaxRegistrationRequired = value;
  }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Tax Registration Mask")]
  public virtual string TaxRegistrationMask
  {
    get => this._TaxRegistrationMask;
    set => this._TaxRegistrationMask = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Tax Registration Reg. Exp.")]
  public virtual string TaxRegistrationRegexp
  {
    get => this._TaxRegistrationRegexp;
    set => this._TaxRegistrationRegexp = value;
  }

  /// <summary>
  /// Get or set NonTaxable that mark current state does not impose sales taxes.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Non-Taxable")]
  [PXDefault(false)]
  public virtual bool? NonTaxable { get; set; }

  /// <summary>
  /// The reference to <see cref="T:PX.Objects.CS.SalesTerritory.salesTerritoryID" /> to that country belongs to.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<SalesTerritory.salesTerritoryID, Where<SalesTerritory.salesTerritoryType, Equal<SalesTerritoryTypeAttribute.byState>, And<SalesTerritory.countryID, Equal<Current<State.countryID>>>>>), new Type[] {typeof (SalesTerritory.salesTerritoryID), typeof (SalesTerritory.name)}, DescriptionField = typeof (SalesTerritory.name))]
  [PXRestrictor(typeof (Where<SalesTerritory.isActive, Equal<True>>), "The sales territory is inactive.", new Type[] {}, ShowWarning = true)]
  [PXForeignReference]
  public virtual string SalesTerritoryID { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public class PK : PrimaryKeyOf<State>.By<State.countryID, State.stateID>
  {
    public static State Find(
      PXGraph graph,
      string countryID,
      string stateID,
      PKFindOptions options = 0)
    {
      return (State) PrimaryKeyOf<State>.By<State.countryID, State.stateID>.FindBy(graph, (object) countryID, (object) stateID, options);
    }
  }

  public static class FK
  {
    public class SalesTerritory : 
      PrimaryKeyOf<SalesTerritory>.By<SalesTerritory.salesTerritoryID>.ForeignKeyOf<State>.By<State.salesTerritoryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  State.selected>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  State.countryID>
  {
  }

  public abstract class stateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  State.stateID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  State.name>
  {
  }

  public abstract class stateRegexp : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  State.stateRegexp>
  {
  }

  public abstract class locationCode : IBqlField, IBqlOperand
  {
  }

  public abstract class isTaxRegistrationRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    State.isTaxRegistrationRequired>
  {
  }

  public abstract class taxRegistrationMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    State.taxRegistrationMask>
  {
  }

  public abstract class taxRegistrationRegexp : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    State.taxRegistrationRegexp>
  {
  }

  public abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  State.nonTaxable>
  {
  }

  public abstract class salesTerritoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  State.salesTerritoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  State.noteID>
  {
  }

  /// <exclude />
  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  State.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  State.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    State.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    State.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  State.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    State.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    State.lastModifiedDateTime>
  {
  }
}
