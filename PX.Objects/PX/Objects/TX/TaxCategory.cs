// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxCategory
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
namespace PX.Objects.TX;

/// <summary>
/// Represents a tax category. The head of a master-detail aggregate of a set of taxes.
/// The class is used to define a set of taxes that can be applied to a document on data entry pages.
/// </summary>
[PXPrimaryGraph(typeof (TaxCategoryMaint))]
[PXCacheName("Tax Category")]
[Serializable]
public class TaxCategory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxCategoryID;
  protected string _Descr;
  protected bool? _TaxCatFlag;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The tax category ID. This is the key field, which can be specified by the user.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<TaxCategory.taxCategoryID>), CacheGlobal = true)]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  /// <summary>
  /// The description of the tax category, which can be specified by the user.
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
  /// "Exclude Listed Taxes" flag. Specifies how the taxes that are included in the category should be applied to the document line.
  /// <value>
  /// <c>false</c>: Only the taxes of the category that are intersected with the taxes of the tax zone should be applied to the document line.
  /// <c>true</c>: All taxes of the tax zone except the taxes of the category should be applied to the document line.
  /// </value>
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exclude Listed Taxes")]
  public virtual bool? TaxCatFlag
  {
    get => this._TaxCatFlag;
    set => this._TaxCatFlag = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exempt", FieldClass = "ExemptedTaxReporting")]
  public virtual bool? Exempt { get; set; }

  [PXNote(DescriptionField = typeof (TaxCategory.taxCategoryID), Selector = typeof (Search<TaxCategory.taxCategoryID>))]
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

  public class PK : PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>
  {
    public static TaxCategory Find(PXGraph graph, string taxCategoryID, PKFindOptions options = 0)
    {
      return (TaxCategory) PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>.FindBy(graph, (object) taxCategoryID, options);
    }
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxCategory.taxCategoryID>
  {
    /// <summary>15</summary>
    public const int Length = 15;
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxCategory.descr>
  {
  }

  public abstract class taxCatFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxCategory.taxCatFlag>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxCategory.active>
  {
  }

  public abstract class exempt : IBqlField, IBqlOperand
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxCategory.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxCategory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxCategory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxCategory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxCategory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxCategory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxCategory.lastModifiedDateTime>
  {
  }
}
