// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPrinter
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

[PXCacheName("Printers")]
public class SMPrinter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IIncludable, IRestricted
{
  protected Guid? _PrinterID;
  protected 
  #nullable disable
  string _DeviceHubID;
  protected string _PrinterName;
  protected string _Description;
  protected int? _DefaultNumberOfCopies;
  protected bool? _IsActive;
  protected bool? _Included;

  [PXDBGuid(true)]
  [PXReferentialIntegrityCheck]
  public virtual Guid? PrinterID
  {
    get => this._PrinterID;
    set => this._PrinterID = value;
  }

  [PXDBString(30, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "DeviceHub ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string DeviceHubID
  {
    get => this._DeviceHubID;
    set => this._DeviceHubID = value;
  }

  [PXDBString(20, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Printer", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string PrinterName
  {
    get => this._PrinterName;
    set => this._PrinterName = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBInt(MinValue = 1)]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Default Number of Copies", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual int? DefaultNumberOfCopies
  {
    get => this._DefaultNumberOfCopies;
    set => this._DefaultNumberOfCopies = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active", IsReadOnly = true)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

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
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBGroupMask]
  public virtual byte[] GroupMask { get; set; }

  /// <summary>
  /// An unbound field that is used in the user interface to include the budget tree node into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// Also see <see cref="!:GLBudgetTree.GroupMask" />.
  /// </summary>
  [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  public class PK : PrimaryKeyOf<SMPrinter>.By<SMPrinter.printerID>
  {
    public static SMPrinter Find(PXGraph graph, Guid? printerID)
    {
      return SMPrinter.PK.Find(graph, printerID);
    }
  }

  public class UK : PrimaryKeyOf<SMPrinter>.By<SMPrinter.deviceHubID, SMPrinter.printerName>
  {
    public static SMPrinter Find(PXGraph graph, string deviceHubID, string printerName)
    {
      return SMPrinter.UK.Find(graph, deviceHubID, printerName);
    }
  }

  public abstract class printerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPrinter.printerID>
  {
  }

  public abstract class deviceHubID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPrinter.deviceHubID>
  {
  }

  public abstract class printerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPrinter.printerName>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPrinter.description>
  {
  }

  public abstract class defaultNumberOfCopies : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPrinter.defaultNumberOfCopies>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMPrinter.isActive>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPrinter.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPrinter.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMPrinter.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPrinter.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPrinter.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMPrinter.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMPrinter.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPrinter.noteID>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMPrinter.groupMask>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMPrinter.included>
  {
  }
}
