// Decompiled with JetBrains decompiler
// Type: PX.SM.SMScanner
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

[PXCacheName("Scanners")]
public class SMScanner : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DeviceHubID;

  [PXDBGuid(true)]
  [PXReferentialIntegrityCheck]
  public virtual Guid? ScannerID { get; set; }

  [PXDBString(30, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "DeviceHub ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string DeviceHubID { get; set; }

  [PXDBString(20, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Scanner ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScannerName { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active", IsReadOnly = true)]
  public virtual bool? IsActive { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {}, new string[] {})]
  [PXDefault]
  [PXUIField(DisplayName = "Paper Source (Default)", IsReadOnly = true)]
  public virtual int? PaperSourceDefValue { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Paper Sources", Visibility = PXUIVisibility.Invisible, Visible = false)]
  public virtual string PaperSourceComboValues { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {}, new string[] {})]
  [PXDefault]
  [PXUIField(DisplayName = "Color Mode (Default)", IsReadOnly = true)]
  public virtual int? PixelTypeDefValue { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Color Modes", Visibility = PXUIVisibility.Invisible, Visible = false)]
  public virtual string PixelTypeComboValues { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {}, new string[] {})]
  [PXDefault]
  [PXUIField(DisplayName = "Resolution (Default)", IsReadOnly = true)]
  public virtual int? ResolutionDefValue { get; set; }

  [PXDBString(4000, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Resolutions", Visibility = PXUIVisibility.Invisible, Visible = false)]
  public virtual string ResolutionComboValues { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {}, new string[] {})]
  [PXDefault]
  [PXUIField(DisplayName = "File Type (Default)", IsReadOnly = true)]
  public virtual int? FileTypeDefValue { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "File Types", Visibility = PXUIVisibility.Invisible, Visible = false)]
  public virtual string FileTypeComboValues { get; set; }

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

  public class PK : PrimaryKeyOf<SMScanner>.By<SMScanner.scannerID>
  {
    public static SMScanner Find(PXGraph graph, Guid? scannerID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMScanner>.By<SMScanner.scannerID>.FindBy(graph, (object) scannerID, options);
    }
  }

  public abstract class scannerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScanner.scannerID>
  {
  }

  public abstract class deviceHubID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanner.deviceHubID>
  {
  }

  public abstract class scannerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanner.scannerName>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanner.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMScanner.isActive>
  {
  }

  public abstract class paperSourceDefValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMScanner.paperSourceDefValue>
  {
  }

  public abstract class paperSourceComboValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanner.paperSourceComboValues>
  {
  }

  public abstract class pixelTypeDefValue : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanner.pixelTypeDefValue>
  {
  }

  public abstract class pixelTypeComboValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanner.pixelTypeComboValues>
  {
  }

  public abstract class resolutionDefValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMScanner.resolutionDefValue>
  {
  }

  public abstract class resolutionComboValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanner.resolutionComboValues>
  {
  }

  public abstract class fileTypeDefValue : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanner.fileTypeDefValue>
  {
  }

  public abstract class fileTypeComboValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanner.fileTypeComboValues>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScanner.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanner.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMScanner.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMScanner.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanner.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMScanner.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SMScanner.Tstamp>
  {
  }
}
