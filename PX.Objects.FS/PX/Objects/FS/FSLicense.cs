// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLicense
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("License")]
[PXPrimaryGraph(typeof (LicenseMaint))]
[Serializable]
public class FSLicense : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(DisplayName = "License ID", Enabled = false)]
  public virtual int? LicenseID { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (FSLicense.refNbr), DescriptionField = typeof (FSLicense.descr))]
  [AutoNumber(typeof (Search<FSSetup.licenseNumberingID>), typeof (AccessInfo.businessDate))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Staff Member")]
  [FSSelector_StaffMember_Employee_Only]
  public virtual int? EmployeeID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual DateTime? ExpirationDate { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Issue Date")]
  public virtual DateTime? IssueDate { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "License Type")]
  [PXSelector(typeof (FSLicenseType.licenseTypeID), SubstituteKey = typeof (FSLicenseType.licenseTypeCD), DescriptionField = typeof (FSLicense.descr))]
  public virtual int? LicenseTypeID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Never Expires")]
  public virtual bool? NeverExpires { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

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
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<FSLicense>.By<FSLicense.licenseID>
  {
    public static FSLicense Find(PXGraph graph, int? licenseID, PKFindOptions options = 0)
    {
      return (FSLicense) PrimaryKeyOf<FSLicense>.By<FSLicense.licenseID>.FindBy(graph, (object) licenseID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSLicense>.By<FSLicense.refNbr>
  {
    public static FSLicense Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (FSLicense) PrimaryKeyOf<FSLicense>.By<FSLicense.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FSLicense>.By<FSLicense.employeeID>
    {
    }

    public class LicenseType : 
      PrimaryKeyOf<FSLicenseType>.By<FSLicenseType.licenseTypeID>.ForeignKeyOf<FSLicense>.By<FSLicense.licenseTypeID>
    {
    }
  }

  public abstract class licenseID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLicense.licenseID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLicense.refNbr>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLicense.descr>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLicense.employeeID>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSLicense.expirationDate>
  {
  }

  public abstract class issueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSLicense.issueDate>
  {
  }

  public abstract class licenseTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLicense.licenseTypeID>
  {
  }

  public abstract class neverExpires : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSLicense.neverExpires>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSLicense.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSLicense.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLicense.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSLicense.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSLicense.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSLicense.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSLicense.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSLicense.Tstamp>
  {
  }
}
