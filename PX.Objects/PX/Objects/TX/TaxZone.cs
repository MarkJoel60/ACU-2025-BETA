// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxZone
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>
/// Represents a tax zone. The head of a master-detail aggregate of a set of taxes.
/// The class is used to define a set of taxes that can be applied to a document on data entry pages.
/// </summary>
[PXCacheName("Tax Zone")]
[PXPrimaryGraph(new Type[] {typeof (TaxZoneMaint)}, new Type[] {typeof (Select<TaxZone, Where<TaxZone.taxZoneID, Equal<Current<TaxZone.taxZoneID>>>>)})]
[Serializable]
public class TaxZone : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxZoneID;
  protected string _Descr;
  protected string _DfltTaxCategoryID;
  protected bool? _IsImported;
  protected bool? _IsExternal;
  protected string _TaxPluginID;
  protected int? _TaxVendorID;
  protected string _TaxID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>A key field, which can be specified by the user.</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search3<TaxZone.taxZoneID, OrderBy<Asc<TaxZone.taxZoneID>>>), CacheGlobal = true)]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  /// <summary>
  /// The description of the tax zone, which can be specified by the user.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// Default <see cref="T:PX.Objects.TX.TaxCategory" />. It is used to set a tax category for document lines if there are no overriding defaults (e.g. by inventory item).
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (TaxCategory.taxCategoryID), DescriptionField = typeof (TaxCategory.descr))]
  public virtual string DfltTaxCategoryID
  {
    get => this._DfltTaxCategoryID;
    set => this._DfltTaxCategoryID = value;
  }

  /// <summary>
  /// The field was used on importing of tax configuration from Avalara files.
  /// </summary>
  [Obsolete("This property is obsolete and will be removed in Acumatica 8.0")]
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsImported
  {
    get => this._IsImported;
    set => this._IsImported = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the tax zone is used for the external tax provider.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "External Tax Provider")]
  public virtual bool? IsExternal
  {
    get => this._IsExternal;
    set => this._IsExternal = value;
  }

  /// <summary>
  /// <see cref="T:PX.Objects.TX.TaxPlugin.taxPluginID" /> of a tax provider which will bu used.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Provider ID")]
  [PXSelector(typeof (TaxPlugin.taxPluginID), DescriptionField = typeof (TaxPlugin.description))]
  [PXDefault]
  public virtual string TaxPluginID
  {
    get => this._TaxPluginID;
    set => this._TaxPluginID = value;
  }

  /// <summary>
  /// <see cref="!:PX.Objects.AP.Vendor.BAccountID" /> of a tax agency to which the tax zone belongs.
  /// </summary>
  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.taxAgency, Equal<True>>>), DisplayName = "Tax Agency ID", DescriptionField = typeof (PX.Objects.AP.Vendor.acctName))]
  [PXDefault]
  public virtual int? TaxVendorID
  {
    get => this._TaxVendorID;
    set => this._TaxVendorID = value;
  }

  /// <summary>
  /// A service field. It is used to hide "Applicable Taxes" tab on TX206000 page.
  /// </summary>
  [PXBool]
  [PXUIField(Visible = false)]
  public virtual bool? ShowTaxTabExpr => new bool?(!this.IsExternal.GetValueOrDefault());

  /// <summary>
  /// The field contains ID of a tax that would be used to create tax transactions in documents.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (Search<Tax.taxID, Where<Tax.isExternal, Equal<False>, And<Tax.taxType, Equal<CSTaxType.vat>>>>), DescriptionField = typeof (Tax.descr))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBString(1)]
  [PXDefault("C")]
  [MappingTypes]
  [PXUIField(DisplayName = "Tax Zone Is Based On")]
  public virtual string MappingType { get; set; }

  [PXUIField(DisplayName = "Country")]
  [PXDBString(2)]
  [PXSelector(typeof (PX.Objects.CS.Country.countryID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Country.description))]
  [PXDefault]
  public virtual string CountryID { get; set; }

  [PXNote(DescriptionField = typeof (TaxZone.taxZoneID), Selector = typeof (Search<TaxZone.taxZoneID>))]
  public virtual Guid? NoteID { get; set; }

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

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [ExternalAPTaxTypes]
  [PXUIField]
  public virtual string ExternalAPTaxType { get; set; }

  public class PK : PrimaryKeyOf<TaxZone>.By<TaxZone.taxZoneID>
  {
    public static TaxZone Find(PXGraph graph, string taxZoneID, PKFindOptions options = 0)
    {
      return (TaxZone) PrimaryKeyOf<TaxZone>.By<TaxZone.taxZoneID>.FindBy(graph, (object) taxZoneID, options);
    }
  }

  public static class FK
  {
    public class DefaultTaxCategory : 
      PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>.ForeignKeyOf<TaxZone>.By<TaxZone.dfltTaxCategoryID>
    {
    }

    public class Provider : 
      PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>.ForeignKeyOf<TaxZone>.By<TaxZone.taxPluginID>
    {
    }

    public class TaxAgency : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxZone>.By<TaxZone.taxVendorID>
    {
    }

    public class Tax : PrimaryKeyOf<Tax>.By<Tax.taxID>.ForeignKeyOf<TaxZone>.By<TaxZone.taxID>
    {
    }
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZone.taxZoneID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZone.descr>
  {
  }

  public abstract class dfltTaxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZone.dfltTaxCategoryID>
  {
  }

  public abstract class isImported : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxZone.isImported>
  {
  }

  public abstract class isExternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxZone.isExternal>
  {
  }

  public abstract class taxPluginID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZone.taxPluginID>
  {
  }

  public abstract class taxVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxZone.taxVendorID>
  {
  }

  public abstract class showTaxTabExpr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxZone.showTaxTabExpr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZone.taxID>
  {
  }

  public abstract class mappingType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZone.mappingType>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZone.countryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxZone.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxZone.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZone.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxZone.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxZone.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZone.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxZone.lastModifiedDateTime>
  {
  }

  public abstract class externalAPTaxType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZone.externalAPTaxType>
  {
  }
}
