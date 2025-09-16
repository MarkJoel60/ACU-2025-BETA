// Decompiled with JetBrains decompiler
// Type: PX.SM.LicenseInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class LicenseInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string PERP_TYPE = "PERP";

  [PXUIField(DisplayName = "Valid", Enabled = false)]
  [PXBool]
  public virtual bool? Valid { get; set; }

  [PXUIField(DisplayName = "Preview", Enabled = false, Visible = false)]
  [PXBool]
  public virtual bool? Preview => new bool?(true);

  [PXUIField(DisplayName = "Status", Enabled = false)]
  [PXString]
  [PXStringListLicenseStatus]
  public string Status { get; set; }

  public virtual PXLicense License { get; set; }

  [PXUIField(DisplayName = "Valid From", Enabled = false)]
  [PXDateAndTime]
  public System.DateTime? ValidFrom { get; set; }

  [PXUIField(DisplayName = "Valid To", Enabled = false)]
  [PXDateAndTime]
  public System.DateTime? ValidTo { get; set; }

  [PXUIField(DisplayName = "Site Name", Enabled = false, Visible = false)]
  [PXString]
  public string DomainName { get; set; }

  [PXUIField(DisplayName = "Customer Name", Enabled = false, Visible = false)]
  [PXString]
  public string CustomerName { get; set; }

  [PXUIField(DisplayName = "Number of Users", Enabled = false)]
  [PXInt]
  public int? Users { get; set; }

  [PXUIField(DisplayName = "Number of Tenants", Enabled = false)]
  [PXInt]
  public int? Companies { get; set; }

  [PXUIField(DisplayName = "Number of Processors", Enabled = false)]
  [PXInt]
  public int? Processors { get; set; }

  [PXUIField(DisplayName = "Version", Enabled = false)]
  [PXString]
  public string Version { get; set; }

  [PXUIField(DisplayName = "License Type", Enabled = false)]
  [PXString]
  public string LicenseTypeCD { get; set; }

  [PXUIField(Visible = false, Enabled = false)]
  [PXString]
  public string Type { get; set; }

  [PXUIField(DisplayName = "Offline License", Enabled = false)]
  [PXBool]
  [PXDefault(false)]
  public virtual bool? Offline { get; set; }

  public abstract class valid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LicenseInfo.valid>
  {
  }

  public abstract class preview : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LicenseInfo.preview>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseInfo.status>
  {
  }

  public abstract class validFrom : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  LicenseInfo.validFrom>
  {
  }

  public abstract class validTo : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  LicenseInfo.validTo>
  {
  }

  public abstract class domainName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseInfo.domainName>
  {
  }

  public abstract class customerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseInfo.customerName>
  {
  }

  public abstract class users : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LicenseInfo.users>
  {
  }

  public abstract class companies : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LicenseInfo.companies>
  {
  }

  public abstract class processors : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LicenseInfo.processors>
  {
  }

  public abstract class version : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseInfo.version>
  {
  }

  public abstract class licenseTypeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseInfo.licenseTypeCD>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseInfo.type>
  {
  }

  public abstract class offline : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LicenseInfo.offline>
  {
  }
}
