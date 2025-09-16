// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPlugin
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.TaxProvider;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXCacheName("Tax Plug-in")]
[PXPrimaryGraph(typeof (TaxPluginMaint))]
[Serializable]
public class TaxPlugin : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxPluginID;
  protected string _Description;
  protected string _PluginTypeName;
  protected bool? _IsActive;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<TaxPlugin.taxPluginID>), CacheGlobal = true)]
  public virtual string TaxPluginID
  {
    get => this._TaxPluginID;
    set => this._TaxPluginID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXProviderTypeSelector(new Type[] {typeof (ITaxProvider)})]
  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Plug-In (Type)")]
  public virtual string PluginTypeName
  {
    get => this._PluginTypeName;
    set => this._PluginTypeName = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  /// <summary>
  /// The tax calculation mode of the external tax provider.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.TX.TaxCalculationMode.ExternalTaxProviderTaxCalcMode" /> class.
  /// The default value is <see cref="F:PX.Objects.TX.TaxCalculationMode.Net" />
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [TaxCalculationMode.ExternalTaxProviderTaxCalcMode]
  [PXUIField(DisplayName = "Default Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<TaxPlugin>.By<TaxPlugin.taxPluginID>
  {
    public static TaxPlugin Find(PXGraph graph, string taxPluginID, PKFindOptions options = 0)
    {
      return (TaxPlugin) PrimaryKeyOf<TaxPlugin>.By<TaxPlugin.taxPluginID>.FindBy(graph, (object) taxPluginID, options);
    }
  }

  public abstract class taxPluginID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPlugin.taxPluginID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPlugin.description>
  {
  }

  public abstract class pluginTypeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPlugin.pluginTypeName>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxPlugin.isActive>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.TX.TaxPlugin.TaxCalcMode" />
  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPlugin.taxCalcMode>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxPlugin.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxPlugin.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxPlugin.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPlugin.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxPlugin.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxPlugin.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPlugin.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxPlugin.lastModifiedDateTime>
  {
  }
}
