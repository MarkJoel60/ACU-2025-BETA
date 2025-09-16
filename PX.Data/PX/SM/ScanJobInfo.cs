// Decompiled with JetBrains decompiler
// Type: PX.SM.ScanJobInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Scan Job")]
[Serializable]
public class ScanJobInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault(typeof (UserPreferences.defaultScannerID))]
  [PXScannerSelector]
  public virtual Guid? ScannerID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXIntList(new int[] {}, new string[] {})]
  [PXUIField(DisplayName = "Paper Source")]
  public virtual int? PaperSource { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Paper Sources", Visibility = PXUIVisibility.Invisible, Visible = false, IsReadOnly = true)]
  public virtual 
  #nullable disable
  string PaperSourceList { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXIntList(new int[] {}, new string[] {})]
  [PXUIField(DisplayName = "Color Mode")]
  public virtual int? PixelType { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Color Modes", Visibility = PXUIVisibility.Invisible, Visible = false, IsReadOnly = true)]
  public virtual string PixelTypeList { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXIntList(new int[] {}, new string[] {})]
  [PXUIField(DisplayName = "Resolution")]
  public virtual int? Resolution { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Resolutions", Visibility = PXUIVisibility.Invisible, Visible = false, IsReadOnly = true)]
  public virtual string ResolutionList { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXIntList(new int[] {}, new string[] {})]
  [PXUIField(DisplayName = "File Type")]
  public virtual int? FileType { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "File Types", Visibility = PXUIVisibility.Invisible, Visible = false, IsReadOnly = true)]
  public virtual string FileTypeList { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "File Name")]
  public virtual string FileName { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string ScanScreenID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? ScanEntityNoteID { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string ScanEntityPrimaryViewName { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string ScanViewName { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string ScanEntityPrimaryParameters { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string ScanEntityParameters { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? ProcessStarted { get; set; }

  public abstract class scannerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ScanJobInfo.scannerID>
  {
  }

  public abstract class paperSource : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ScanJobInfo.paperSource>
  {
  }

  public abstract class paperSourceList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ScanJobInfo.paperSourceList>
  {
  }

  public abstract class pixelType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ScanJobInfo.pixelType>
  {
  }

  public abstract class pixelTypeList : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScanJobInfo.pixelTypeList>
  {
  }

  public abstract class resolution : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ScanJobInfo.resolution>
  {
  }

  public abstract class resolutionList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ScanJobInfo.resolutionList>
  {
  }

  public abstract class fileType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ScanJobInfo.fileType>
  {
  }

  public abstract class fileTypeList : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScanJobInfo.fileTypeList>
  {
  }

  public abstract class fileName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScanJobInfo.fileName>
  {
  }

  public abstract class scanScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScanJobInfo.scanScreenID>
  {
  }

  public abstract class scanEntityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ScanJobInfo.scanEntityNoteID>
  {
  }

  public abstract class scanEntityPrimaryViewName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ScanJobInfo.scanEntityPrimaryViewName>
  {
  }

  public abstract class scanViewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScanJobInfo.scanViewName>
  {
  }

  public abstract class scanEntityPrimaryParameters : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ScanJobInfo.scanEntityPrimaryParameters>
  {
  }

  public abstract class scanEntityParameters : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ScanJobInfo.scanEntityParameters>
  {
  }

  public abstract class processStarted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ScanJobInfo.processStarted>
  {
  }
}
