// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxZoneDet
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
/// The detail of <see cref="T:PX.Objects.TX.TaxZone" />. Implements the many-to-many relationship between the <see cref="T:PX.Objects.TX.Tax" /> and <see cref="T:PX.Objects.TX.TaxZone" />.
/// </summary>
[PXCacheName("Tax Zone Detail")]
[Serializable]
public class TaxZoneDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ITaxDetail
{
  protected 
  #nullable disable
  string _TaxZoneID;
  protected string _TaxID;
  protected bool? _IsImported;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.TX.TaxZone" />.
  /// </summary>
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault(typeof (TaxZone.taxZoneID))]
  [PXUIField(DisplayName = "Tax Zone ID", Visible = false)]
  [PXParent(typeof (Select<TaxZone, Where<TaxZone.taxZoneID, Equal<Current<TaxZoneDet.taxZoneID>>>>))]
  [PXSelector(typeof (TaxZone.taxZoneID))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
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

  public class PK : PrimaryKeyOf<TaxZoneDet>.By<TaxZoneDet.taxZoneID, TaxZoneDet.taxID>
  {
    public static TaxZoneDet Find(
      PXGraph graph,
      string taxZoneID,
      string taxID,
      PKFindOptions options = 0)
    {
      return (TaxZoneDet) PrimaryKeyOf<TaxZoneDet>.By<TaxZoneDet.taxZoneID, TaxZoneDet.taxID>.FindBy(graph, (object) taxZoneID, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class TaxZone : 
      PrimaryKeyOf<TaxZone>.By<TaxZone.taxZoneID>.ForeignKeyOf<TaxZoneDet>.By<TaxZoneDet.taxZoneID>
    {
    }

    public class Tax : PrimaryKeyOf<Tax>.By<Tax.taxID>.ForeignKeyOf<TaxZoneDet>.By<TaxZoneDet.taxID>
    {
    }
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZoneDet.taxZoneID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZoneDet.taxID>
  {
  }

  public abstract class isImported : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxZoneDet.isImported>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxZoneDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxZoneDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxZoneDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxZoneDet.lastModifiedDateTime>
  {
  }
}
