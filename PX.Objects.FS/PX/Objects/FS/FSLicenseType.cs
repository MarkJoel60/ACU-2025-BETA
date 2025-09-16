// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLicenseType
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

[PXCacheName("License Type")]
[PXPrimaryGraph(typeof (LicenseTypeMaint))]
[Serializable]
public class FSLicenseType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? LicenseTypeID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXUIField]
  [PXSelector(typeof (Search<FSLicenseType.licenseTypeCD>))]
  [PXDefault]
  [NormalizeWhiteSpace]
  public virtual 
  #nullable disable
  string LicenseTypeCD { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<FSLicenseType>.By<FSLicenseType.licenseTypeID>
  {
    public static FSLicenseType Find(PXGraph graph, int? licenseTypeID, PKFindOptions options = 0)
    {
      return (FSLicenseType) PrimaryKeyOf<FSLicenseType>.By<FSLicenseType.licenseTypeID>.FindBy(graph, (object) licenseTypeID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSLicenseType>.By<FSLicenseType.licenseTypeCD>
  {
    public static FSLicenseType Find(PXGraph graph, string licenseTypeCD, PKFindOptions options = 0)
    {
      return (FSLicenseType) PrimaryKeyOf<FSLicenseType>.By<FSLicenseType.licenseTypeCD>.FindBy(graph, (object) licenseTypeCD, options);
    }
  }

  public abstract class licenseTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLicenseType.licenseTypeID>
  {
  }

  public abstract class licenseTypeCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLicenseType.licenseTypeCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLicenseType.descr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSLicenseType.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSLicenseType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLicenseType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSLicenseType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSLicenseType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLicenseType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSLicenseType.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSLicenseType.Tstamp>
  {
  }
}
