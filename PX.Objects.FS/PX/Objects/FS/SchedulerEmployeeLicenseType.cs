// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerEmployeeLicenseType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (SelectFrom<FSLicenseType>))]
[PXCacheName("Employee License Type")]
[Serializable]
public class SchedulerEmployeeLicenseType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (FSLicenseType.licenseTypeID))]
  [PXSelector(typeof (FSLicenseType.licenseTypeID), SubstituteKey = typeof (FSLicenseType.licenseTypeCD))]
  [PXUIField(DisplayName = "Employee License Type ID")]
  public virtual int? LicenseTypeID { get; set; }

  [PXDBString(BqlField = typeof (FSLicenseType.licenseTypeCD))]
  public virtual 
  #nullable disable
  string LicenseTypeCD { get; set; }

  [PXDBString(BqlField = typeof (FSLicenseType.descr))]
  [PXUIField]
  public virtual string Descr { get; set; }

  public abstract class licenseTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerEmployeeLicenseType.licenseTypeID>
  {
  }

  public abstract class licenseTypeCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerEmployeeLicenseType.licenseTypeCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerEmployeeLicenseType.descr>
  {
  }
}
