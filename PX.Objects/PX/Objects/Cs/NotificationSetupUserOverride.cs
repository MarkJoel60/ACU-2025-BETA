// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NotificationSetupUserOverride
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("User's Notification Setup")]
public class NotificationSetupUserOverride : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (Search<Users.pKID, Where<Users.pKID, Equal<Current<AccessInfo.userID>>>>))]
  public virtual Guid? UserID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXSelector(typeof (NotificationSetup.setupID), SubstituteKey = typeof (NotificationSetup.notificationCD))]
  [PXUIField(DisplayName = "Mailing ID")]
  public virtual Guid? SetupID { get; set; }

  [PXString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField]
  [PXFormula(typeof (Selector<NotificationSetupUserOverride.setupID, NotificationSetup.reportID>))]
  public virtual 
  #nullable disable
  string ReportID { get; set; }

  [PXDefault]
  [PXPrinterSelector]
  [PXForeignReference(typeof (Field<NotificationSetupUserOverride.defaultPrinterID>.IsRelatedTo<SMPrinter.printerID>))]
  public virtual Guid? DefaultPrinterID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Ship Via")]
  [PXUIEnabled(typeof (Where<Selector<NotificationSetupUserOverride.setupID, NotificationSetup.module>, Equal<PXModule.so>>))]
  [PXDefault(typeof (Search<NotificationSetup.shipVia, Where<BqlOperand<NotificationSetup.setupID, IBqlGuid>.IsEqual<BqlField<NotificationSetupUserOverride.setupID, IBqlGuid>.FromCurrent>>>))]
  [PXSelector(typeof (Search<Carrier.carrierID>), DescriptionField = typeof (Carrier.description), CacheGlobal = true)]
  public virtual string ShipVia { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public class PK : 
    PrimaryKeyOf<NotificationSetupUserOverride>.By<NotificationSetupUserOverride.userID, NotificationSetupUserOverride.setupID>
  {
    public static NotificationSetupUserOverride Find(
      PXGraph graph,
      Guid? userID,
      Guid? setupID,
      PKFindOptions options = 0)
    {
      return (NotificationSetupUserOverride) PrimaryKeyOf<NotificationSetupUserOverride>.By<NotificationSetupUserOverride.userID, NotificationSetupUserOverride.setupID>.FindBy(graph, (object) userID, (object) setupID, options);
    }
  }

  public static class FK
  {
    public class DefaultSetup : 
      PrimaryKeyOf<NotificationSetup>.By<NotificationSetup.setupID>.ForeignKeyOf<NotificationSetupUserOverride>.By<NotificationSetupUserOverride.setupID>
    {
    }

    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<NotificationSetupUserOverride>.By<NotificationSetupUserOverride.userID>
    {
    }
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationSetupUserOverride.userID>
  {
  }

  public abstract class setupID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetupUserOverride.setupID>
  {
  }

  public abstract class reportID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetupUserOverride.reportID>
  {
  }

  public abstract class defaultPrinterID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetupUserOverride.defaultPrinterID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationSetupUserOverride.active>
  {
  }

  public abstract class shipVia : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetupUserOverride.shipVia>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    NotificationSetupUserOverride.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetupUserOverride.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetupUserOverride.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationSetupUserOverride.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationSetupUserOverride.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationSetupUserOverride.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    NotificationSetupUserOverride.lastModifiedDateTime>
  {
  }
}
