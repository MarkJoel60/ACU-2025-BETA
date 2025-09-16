// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRouteSetup
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Route Management Preferences")]
[PXPrimaryGraph(typeof (RouteSetupMaint))]
[Serializable]
public class FSRouteSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string RouteManagementFieldClass = "ROUTEMANAGEMENT";

  [PXDBString(10)]
  [PXDefault]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Route Numbering Sequence")]
  public virtual string RouteNumberingID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Calculate Route Statistics Automatically")]
  public virtual bool? AutoCalculateRouteStats { get; set; }

  [PXDBString(4, IsUnicode = false)]
  [PXUIField(DisplayName = "Default Service Order Type")]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new Type[] {})]
  [FSSelectorSrvOrdTypeRoute]
  public virtual string DfltSrvOrdType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Group Inventory Documents by Posting Process")]
  public virtual bool? GroupINDocumentsByPostingProcess { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Track Start and Complete Location of Route")]
  public virtual bool? TrackRouteLocation { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Set Appointments Created Manually as First in Route")]
  public virtual bool? SetFirstManualAppointment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Seasons in Schedule Contracts")]
  public virtual bool? EnableSeasonScheduleContract { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public static class FK
  {
    public class RouteNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<FSRouteSetup>.By<FSRouteSetup.routeNumberingID>
    {
    }

    public class DfltServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSRouteSetup>.By<FSRouteSetup.dfltSrvOrdType>
    {
    }
  }

  public abstract class routeNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteSetup.routeNumberingID>
  {
  }

  public abstract class autoCalculateRouteStats : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSRouteSetup.autoCalculateRouteStats>
  {
  }

  public abstract class dfltSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteSetup.dfltSrvOrdType>
  {
  }

  public abstract class groupINDocumentsByPostingProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSRouteSetup.groupINDocumentsByPostingProcess>
  {
  }

  public abstract class trackRouteLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSRouteSetup.trackRouteLocation>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRouteSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSRouteSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteSetup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSRouteSetup.Tstamp>
  {
  }

  public abstract class setFirstManualAppointment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSRouteSetup.setFirstManualAppointment>
  {
  }

  public abstract class enableSeasonScheduleContract : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSRouteSetup.enableSeasonScheduleContract>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRouteSetup.noteID>
  {
  }
}
