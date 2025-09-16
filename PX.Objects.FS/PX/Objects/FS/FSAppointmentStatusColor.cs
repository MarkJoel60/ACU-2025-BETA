// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentStatusColor
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Appointment Status Color")]
[Serializable]
public class FSAppointmentStatusColor : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1, IsKey = true, IsFixed = true, InputMask = "")]
  [PXUIField(DisplayName = "ID")]
  public virtual 
  #nullable disable
  string StatusID { get; set; }

  [PXDBString(60, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Status")]
  public virtual string StatusLabel { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Visible")]
  public virtual bool? IsVisible { get; set; }

  [PXDBString(7, IsUnicode = true, InputMask = "C<AAAAAA")]
  [PXDefault("#000000")]
  [PXUIField(DisplayName = "Background Color")]
  public virtual string BackgroundColor { get; set; }

  [PXDBString(7, IsUnicode = true, InputMask = "C<AAAAAA")]
  [PXDefault("#000000")]
  [PXUIField(DisplayName = "Text Color")]
  public virtual string TextColor { get; set; }

  /// <summary>Color of the appointment's status band</summary>
  [PXDBString(7, IsUnicode = true, InputMask = "C<AAAAAA")]
  [PXDefault("#000000")]
  [PXUIField(DisplayName = "Bar Color")]
  public virtual string BandColor { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "System Record")]
  public virtual bool? SystemRecord { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[] Tstamp { get; set; }

  public class PK : PrimaryKeyOf<FSAppointmentStatusColor>.By<FSAppointmentStatusColor.statusID>
  {
    public static FSAppointmentStatusColor Find(
      PXGraph graph,
      string statusID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentStatusColor) PrimaryKeyOf<FSAppointmentStatusColor>.By<FSAppointmentStatusColor.statusID>.FindBy(graph, (object) statusID, options);
    }
  }

  public abstract class statusID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStatusColor.statusID>
  {
  }

  public abstract class statusLabel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStatusColor.statusLabel>
  {
  }

  public abstract class isVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentStatusColor.isVisible>
  {
  }

  public abstract class backgroundColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStatusColor.backgroundColor>
  {
  }

  public abstract class textColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStatusColor.textColor>
  {
  }

  public abstract class bandColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStatusColor.bandColor>
  {
  }

  public abstract class systemRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentStatusColor.systemRecord>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentStatusColor.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStatusColor.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentStatusColor.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentStatusColor.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentStatusColor.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentStatusColor.lastModifiedDateTime>
  {
  }

  public abstract class tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FSAppointmentStatusColor.tstamp>
  {
  }
}
