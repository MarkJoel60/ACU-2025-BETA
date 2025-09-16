// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxCategoryDet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>
/// The detail of <see cref="T:PX.Objects.TX.TaxCategory" />. Implements the many-to-many relationship between <see cref="T:PX.Objects.TX.Tax" /> and <see cref="T:PX.Objects.TX.TaxCategory" />.
/// </summary>
[PXCacheName("Tax Category Detail")]
[Serializable]
public class TaxCategoryDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ITaxDetail
{
  protected 
  #nullable disable
  string _TaxCategoryID;
  protected string _TaxID;
  protected bool? _IsImported;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.TX.TaxCategory" />.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXDefault(typeof (TaxCategory.taxCategoryID))]
  [PXUIField(DisplayName = "Tax Category", Visible = false)]
  [PXParent(typeof (Select<TaxCategory, Where<TaxCategory.taxCategoryID, Equal<Current<TaxCategoryDet.taxCategoryID>>>>))]
  [PXSelector(typeof (TaxCategory.taxCategoryID))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.TX.Tax" />.
  /// </summary>
  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (Search<Tax.taxID, Where<Tax.isExternal, Equal<False>>>), DescriptionField = typeof (Tax.descr))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
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

  public class PK : 
    PrimaryKeyOf<TaxCategoryDet>.By<TaxCategoryDet.taxCategoryID, TaxCategoryDet.taxID>
  {
    public static TaxCategoryDet Find(
      PXGraph graph,
      string taxCategoryID,
      string taxID,
      PKFindOptions options = 0)
    {
      return (TaxCategoryDet) PrimaryKeyOf<TaxCategoryDet>.By<TaxCategoryDet.taxCategoryID, TaxCategoryDet.taxID>.FindBy(graph, (object) taxCategoryID, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class TaxCategory : 
      PrimaryKeyOf<TaxCategory>.By<TaxCategory.taxCategoryID>.ForeignKeyOf<TaxCategoryDet>.By<TaxCategoryDet.taxCategoryID>
    {
    }
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxCategoryDet.taxCategoryID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxCategoryDet.taxID>
  {
  }

  public abstract class isImported : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxCategoryDet.isImported>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxCategoryDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxCategoryDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxCategoryDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxCategoryDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxCategoryDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxCategoryDet.lastModifiedDateTime>
  {
  }
}
