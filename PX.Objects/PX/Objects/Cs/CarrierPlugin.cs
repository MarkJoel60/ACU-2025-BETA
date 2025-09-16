// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierPlugin
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CarrierService;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.IN;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName]
[Serializable]
public class CarrierPlugin : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CarrierPluginID;
  protected string _Description;
  protected string _PluginTypeName;
  protected string _UnitType;
  protected string _UOM;
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
  [PXSelector(typeof (Search<CarrierPlugin.carrierPluginID>), CacheGlobal = true)]
  public virtual string CarrierPluginID
  {
    get => this._CarrierPluginID;
    set => this._CarrierPluginID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? DetailLineCntr { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the carrier is active and can be used in external carrier integrations.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXProviderTypeSelector(new Type[] {typeof (ICarrierService)})]
  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Plug-In")]
  public virtual string PluginTypeName
  {
    get => this._PluginTypeName;
    set => this._PluginTypeName = value;
  }

  [PXDBString(1, IsFixed = true)]
  [CarrierUnitsType.List]
  [PXDefault("S")]
  [PXUIField(DisplayName = "Carrier Units")]
  public virtual string UnitType
  {
    get => this._UnitType;
    set => this._UnitType = value;
  }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  public virtual string LinearUOM { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Email Template for Return Label")]
  [PXSelector(typeof (Search<Notification.notificationID>), DescriptionField = typeof (Notification.name))]
  public virtual int? ReturnLabelNotification { get; set; }

  [Site]
  public virtual int? SiteID { get; set; }

  [PXUnboundDefault(typeof (Switch<Case<Where<CarrierPlugin.unitType, Equal<CarrierUnitsType.si>>, CarrierPlugin.uOM>, Null>))]
  [CarrierUnboundUnit(typeof (CarrierPlugin.uOM), "S", typeof (CommonSetup.weightUOM), DisplayName = "Kilogram")]
  public virtual string KilogramUOM { get; set; }

  [PXUnboundDefault(typeof (Switch<Case<Where<CarrierPlugin.unitType, Equal<CarrierUnitsType.us>>, CarrierPlugin.uOM>, Null>))]
  [CarrierUnboundUnit(typeof (CarrierPlugin.uOM), "U", typeof (CommonSetup.weightUOM), DisplayName = "Pound")]
  public virtual string PoundUOM { get; set; }

  [PXUnboundDefault(typeof (Switch<Case<Where<CarrierPlugin.unitType, Equal<CarrierUnitsType.si>>, CarrierPlugin.linearUOM>, Null>))]
  [CarrierUnboundUnit(typeof (CarrierPlugin.linearUOM), "S", typeof (CommonSetup.linearUOM), DisplayName = "Centimeter")]
  public virtual string CentimeterUOM { get; set; }

  [PXUnboundDefault(typeof (Switch<Case<Where<CarrierPlugin.unitType, Equal<CarrierUnitsType.us>>, CarrierPlugin.linearUOM>, Null>))]
  [CarrierUnboundUnit(typeof (CarrierPlugin.linearUOM), "U", typeof (CommonSetup.linearUOM), DisplayName = "Inch")]
  public virtual string InchUOM { get; set; }

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

  public class PK : PrimaryKeyOf<CarrierPlugin>.By<CarrierPlugin.carrierPluginID>
  {
    public static CarrierPlugin Find(PXGraph graph, string carrierPluginID, PKFindOptions options = 0)
    {
      return (CarrierPlugin) PrimaryKeyOf<CarrierPlugin>.By<CarrierPlugin.carrierPluginID>.FindBy(graph, (object) carrierPluginID, options);
    }
  }

  public abstract class carrierPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPlugin.carrierPluginID>
  {
  }

  public abstract class detailLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CarrierPlugin.detailLineCntr>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPlugin.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CarrierPlugin.isActive>
  {
  }

  public abstract class pluginTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPlugin.pluginTypeName>
  {
  }

  public abstract class unitType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPlugin.unitType>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPlugin.uOM>
  {
  }

  public abstract class linearUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPlugin.linearUOM>
  {
  }

  public abstract class returnLabelNotification : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CarrierPlugin.returnLabelNotification>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CarrierPlugin.siteID>
  {
  }

  public abstract class kilogramUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPlugin.kilogramUOM>
  {
  }

  public abstract class poundUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPlugin.poundUOM>
  {
  }

  public abstract class centimeterUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPlugin.centimeterUOM>
  {
  }

  public abstract class inchUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPlugin.inchUOM>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CarrierPlugin.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CarrierPlugin.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CarrierPlugin.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPlugin.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CarrierPlugin.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CarrierPlugin.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPlugin.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CarrierPlugin.lastModifiedDateTime>
  {
  }
}
