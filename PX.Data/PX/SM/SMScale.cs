// Decompiled with JetBrains decompiler
// Type: PX.SM.SMScale
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Scale")]
public class SMScale : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DeviceHubID;

  [PXDBGuid(true)]
  [PXReferentialIntegrityCheck]
  public virtual Guid? ScaleDeviceID { get; set; }

  [PXDBString(30, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "DeviceHub ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string DeviceHubID { get; set; }

  [PXDBString(10, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Scale ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScaleID { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Descr { get; set; }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  [PXUIField(DisplayName = "Scale UOM", Visible = false)]
  public virtual string UOM { get; set; }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Scale Last Weight", Visible = false)]
  public virtual Decimal? LastWeight { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Updated", Enabled = false)]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<SMScale>.By<SMScale.scaleDeviceID>
  {
    public static SMScale Find(PXGraph graph, Guid? scaleDeviceID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMScale>.By<SMScale.scaleDeviceID>.FindBy(graph, (object) scaleDeviceID, options);
    }
  }

  public abstract class scaleDeviceID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScale.scaleDeviceID>
  {
  }

  public abstract class deviceHubID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScale.deviceHubID>
  {
  }

  public abstract class scaleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScale.scaleID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScale.descr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScale.uOM>
  {
  }

  public abstract class lastWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SMScale.lastWeight>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScale.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScale.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMScale.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScale.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScale.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMScale.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMScale.Tstamp>
  {
  }
}
