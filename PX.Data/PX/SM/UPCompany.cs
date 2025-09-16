// Decompiled with JetBrains decompiler
// Type: PX.SM.UPCompany
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.Update;
using PX.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.SM;

[PXCacheName("Tenant")]
[PXPrimaryGraph(typeof (CompanyMaint))]
public class UPCompany : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CompanyID;
  protected 
  #nullable disable
  string _CompanyCD;
  protected int? _ParentID;
  protected string _dataType;
  protected bool? _Active;
  protected string _LoginName;
  protected bool? _Hidden;
  protected int? _Sequence;
  protected bool? _Current;

  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Tenant ID", Visibility = PXUIVisibility.SelectorVisible)]
  [CompanySelector]
  public virtual int? CompanyID
  {
    get => this._CompanyID;
    set => this._CompanyID = value;
  }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Tenant Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string CompanyCD
  {
    get => this._CompanyCD;
    set => this._CompanyCD = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Parent ID")]
  [ParentCompanySelector]
  [PXDefault(1, PersistingCheck = PXPersistingCheck.Null)]
  public virtual int? ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
  }

  [PXUIField(DisplayName = "Data Type", Enabled = false)]
  [PXString(128 /*0x80*/)]
  [DataTypesList]
  [PXDefault("Custom")]
  public virtual string DataType { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Active", Enabled = false, Visible = false)]
  [PXDefault(true)]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXUIField(DisplayName = "Login Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXString(1024 /*0x0400*/)]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  public virtual string LoginName
  {
    get => this._LoginName;
    set => this._LoginName = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Hidden", Visible = false)]
  public virtual bool? Hidden
  {
    get => this._Hidden;
    set => this._Hidden = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Sequence")]
  public virtual int? Sequence
  {
    get => this._Sequence;
    set => this._Sequence = value;
  }

  [PXUIField(DisplayName = "Size in DB", Enabled = false)]
  [PXDBLong]
  public virtual long? Size { get; set; }

  [PXUIField(DisplayName = "Size in DB (MB)", Enabled = false)]
  [PXDecimal(2)]
  public virtual Decimal? SizeMB
  {
    get => new Decimal?(this.Size.HasValue ? Convert.ToDecimal(this.Size.Value) / 1048576M : 0M);
    set
    {
    }
  }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Company Description", Enabled = false)]
  public virtual string Description
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPCompany.companyID), typeof (UPCompany.companyCD)})] get
    {
      string str = (string) null;
      if (!string.IsNullOrEmpty(this.CompanyCD) && !int.TryParse(this.CompanyCD, out int _))
        str = this.CompanyCD;
      if (string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(this.LoginName))
        str = this.LoginName;
      return !string.IsNullOrEmpty(str) ? $"{str} ({this.CompanyID.ToString()})" : this.CompanyID.ToString();
    }
  }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [PXStringList(new string[] {"Active", "Trial", "Unlicensed"}, new string[] {"Active", "Test Tenant", "Unlicensed"})]
  public virtual string Status
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPCompany.loginName)})] get
    {
      PXLicense license = LicensingManager.Instance.License;
      if (!license.Licensed || license.Trials != null && license.Trials.Contains(this.LoginName))
        return CompanyLicenseStatus.Trial.ToString();
      return ((IEnumerable<string>) PXDatabase.AvailableCompanies).Contains<string>(this.LoginName) || PXDatabase.Companies == null || PXDatabase.Companies.Length == 0 ? CompanyLicenseStatus.Active.ToString() : CompanyLicenseStatus.Unlicensed.ToString();
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Is System", Enabled = false)]
  public virtual bool? System
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPCompany.dataType), typeof (UPCompany.companyID)})] get
    {
      int num1;
      if (!this.Hidden.GetValueOrDefault())
      {
        int? companyId = this.CompanyID;
        int num2 = 1;
        num1 = companyId.GetValueOrDefault() == num2 & companyId.HasValue ? 1 : 0;
      }
      else
        num1 = 1;
      return new bool?(num1 != 0);
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Current", Enabled = false)]
  public virtual bool? Current
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPCompany.companyID)})] get
    {
      int? companyId = this.CompanyID;
      int currentCompany = PXInstanceHelper.CurrentCompany;
      return new bool?(companyId.GetValueOrDefault() == currentCompany & companyId.HasValue);
    }
  }

  [PXBool]
  public virtual bool? Altered { get; set; }

  public class PK : PrimaryKeyOf<UPCompany>.By<UPCompany.companyID>
  {
    public static UPCompany Find(PXGraph graph, int? companyID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<UPCompany>.By<UPCompany.companyID>.FindBy(graph, (object) companyID, options);
    }
  }

  public static class FK
  {
    public class Company : 
      PrimaryKeyOf<UPCompany>.By<UPCompany.companyID>.ForeignKeyOf<UPCompany>.By<UPCompany.parentID>
    {
    }
  }

  public abstract class companyID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPCompany.companyID>
  {
  }

  public abstract class companyCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPCompany.companyCD>
  {
  }

  public abstract class parentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPCompany.parentID>
  {
  }

  public abstract class dataType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPCompany.dataType>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPCompany.active>
  {
  }

  public abstract class loginName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPCompany.loginName>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPCompany.hidden>
  {
  }

  public abstract class sequence : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPCompany.sequence>
  {
  }

  public abstract class size : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  UPCompany.size>
  {
  }

  public abstract class sizeMB : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UPCompany.sizeMB>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPCompany.description>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPCompany.status>
  {
  }

  public abstract class system : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPCompany.system>
  {
  }

  public abstract class current : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPCompany.current>
  {
  }

  public abstract class altered : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPCompany.altered>
  {
  }
}
