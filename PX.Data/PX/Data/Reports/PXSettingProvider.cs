// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.PXSettingProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Reports;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data.Reports;

[Serializable]
public class PXSettingProvider : SettingsProvider
{
  public virtual 
  #nullable disable
  IList<SettingsProvider.Set> Sets
  {
    get
    {
      List<SettingsProvider.Set> sets = new List<SettingsProvider.Set>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXSettingProvider.ReportSettings>(new PXDataField("Name"), (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, (object) PXSiteMap.CurrentScreenID), (PXDataField) new PXDataFieldValue("UserName", PXDbType.VarChar, (object) PXAccess.GetUserName())))
      {
        string str = pxDataRecord.GetString(0);
        sets.Add(new SettingsProvider.Set(str, str));
      }
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXSettingProvider.ReportSettings>(new PXDataField("Name"), (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, (object) PXSiteMap.CurrentScreenID), (PXDataField) new PXDataFieldValue("IsShared", PXDbType.Bit, (object) true)))
      {
        string str = pxDataRecord.GetString(0);
        bool flag = false;
        foreach (SettingsProvider.Set set in sets)
        {
          if (set.Key == str)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          sets.Add(new SettingsProvider.Set(str, str));
      }
      return (IList<SettingsProvider.Set>) sets;
    }
  }

  public virtual IList<SettingsProvider.LocaleModel> Locales
  {
    get
    {
      List<SettingsProvider.LocaleModel> locales = new List<SettingsProvider.LocaleModel>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Locale>(new PXDataField("LocaleName"), new PXDataField("Description"), (PXDataField) new PXDataFieldValue("IsActive", PXDbType.Bit, (object) true)))
        locales.Add(new SettingsProvider.LocaleModel(pxDataRecord.GetString(0), pxDataRecord.GetString(1)));
      return (IList<SettingsProvider.LocaleModel>) locales;
    }
  }

  public virtual PXReportSettings Default
  {
    get
    {
      using (PXDataRecord rec = PXDatabase.SelectSingle<PXSettingProvider.ReportSettings>(new PXDataField("Name"), new PXDataField("IsDefault"), new PXDataField("IsShared"), new PXDataField("CommonValues"), new PXDataField("MailValues"), new PXDataField("ParamValues"), new PXDataField("Sorting"), new PXDataField("Filters"), new PXDataField("MergeReports"), new PXDataField("MergingOrder"), (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, (object) PXSiteMap.CurrentScreenID), (PXDataField) new PXDataFieldValue("UserName", PXDbType.VarChar, (object) PXAccess.GetUserName()), (PXDataField) new PXDataFieldValue("IsDefault", PXDbType.Bit, (object) true)))
        return rec != null ? this.CreateSettings(rec) : (PXReportSettings) null;
    }
  }

  public virtual PXReportSettings Read(Guid? id)
  {
    using (PXDataRecord rec = PXDatabase.SelectSingle<PX.Data.Reports.DAC.ReportSettings>((PXDataField) new PXDataField<PXSettingProvider.ReportSettings.name>(), (PXDataField) new PXDataField<PXSettingProvider.ReportSettings.isDefault>(), (PXDataField) new PXDataField<PXSettingProvider.ReportSettings.isShared>(), (PXDataField) new PXDataField<PXSettingProvider.ReportSettings.commonValues>(), (PXDataField) new PXDataField<PX.Data.Reports.DAC.ReportSettings.mailValues>(), (PXDataField) new PXDataField<PXSettingProvider.ReportSettings.paramValues>(), (PXDataField) new PXDataField<PXSettingProvider.ReportSettings.sorting>(), (PXDataField) new PXDataField<PXSettingProvider.ReportSettings.filters>(), (PXDataField) new PXDataField<PXSettingProvider.ReportSettings.mergeReports>(), (PXDataField) new PXDataField<PXSettingProvider.ReportSettings.mergingOrder>(), (PXDataField) new PXDataField<PX.Data.Reports.DAC.ReportSettings.username>(), (PXDataField) new PXDataFieldValue<PXSettingProvider.ReportSettings.settingsID>(PXDbType.UniqueIdentifier, (object) id)))
      return rec != null ? this.CreateSettings(rec) : (PXReportSettings) null;
  }

  public virtual PXReportSettings Read(string name, string userName, string screenID)
  {
    using (PXDataRecord rec1 = PXDatabase.SelectSingle<PXSettingProvider.ReportSettings>(new PXDataField("Name"), new PXDataField("IsDefault"), new PXDataField("IsShared"), new PXDataField("CommonValues"), new PXDataField("MailValues"), new PXDataField("ParamValues"), new PXDataField("Sorting"), new PXDataField("Filters"), new PXDataField("MergeReports"), new PXDataField("MergingOrder"), (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, (object) screenID), (PXDataField) new PXDataFieldValue("Name", PXDbType.VarChar, (object) name), (PXDataField) new PXDataFieldValue("UserName", PXDbType.VarChar, (object) userName)))
    {
      if (rec1 != null)
        return rec1 != null ? this.CreateSettings(rec1) : (PXReportSettings) null;
      using (PXDataRecord rec2 = PXDatabase.SelectSingle<PXSettingProvider.ReportSettings>(new PXDataField("Name"), new PXDataField("IsDefault"), new PXDataField("IsShared"), new PXDataField("CommonValues"), new PXDataField("MailValues"), new PXDataField("ParamValues"), new PXDataField("Sorting"), new PXDataField("Filters"), new PXDataField("MergeReports"), new PXDataField("MergingOrder"), (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, (object) PXSiteMap.CurrentScreenID), (PXDataField) new PXDataFieldValue("Name", PXDbType.VarChar, (object) name), (PXDataField) new PXDataFieldValue("IsShared", PXDbType.Bit, (object) true)))
        return rec2 != null ? this.CreateSettings(rec2) : (PXReportSettings) null;
    }
  }

  public virtual Guid GetSettingsId(string name, string userName, string screenID)
  {
    using (PXDataRecord pxDataRecord1 = PXDatabase.SelectSingle<PXSettingProvider.ReportSettings>((PXDataField) new PXDataField<PXSettingProvider.ReportSettings.settingsID>(), (PXDataField) new PXDataFieldValue<PXSettingProvider.ReportSettings.screenID>(PXDbType.VarChar, (object) screenID), (PXDataField) new PXDataFieldValue<PXSettingProvider.ReportSettings.name>(PXDbType.VarChar, (object) name), (PXDataField) new PXDataFieldValue("UserName", PXDbType.VarChar, (object) userName)))
    {
      if (pxDataRecord1 != null)
        return pxDataRecord1.GetGuid(0).Value;
      using (PXDataRecord pxDataRecord2 = PXDatabase.SelectSingle<PXSettingProvider.ReportSettings>((PXDataField) new PXDataField<PXSettingProvider.ReportSettings.settingsID>(), (PXDataField) new PXDataFieldValue<PXSettingProvider.ReportSettings.screenID>(PXDbType.VarChar, (object) screenID), (PXDataField) new PXDataFieldValue<PXSettingProvider.ReportSettings.name>(PXDbType.VarChar, (object) name), (PXDataField) new PXDataFieldValue<PXSettingProvider.ReportSettings.isShared>(PXDbType.Bit, (object) true)))
        return pxDataRecord2.GetGuid(0).Value;
    }
  }

  public virtual void Save(PXReportSettings settings, string currentScreenID)
  {
    if (settings.IsShared)
      settings.IsDefault = false;
    if (settings.IsDefault)
      PXDatabase.Update<PXSettingProvider.ReportSettings>((PXDataFieldParam) new PXDataFieldAssign("IsDefault", PXDbType.Bit, (object) false), (PXDataFieldParam) new PXDataFieldRestrict("ScreenID", PXDbType.VarChar, (object) currentScreenID), (PXDataFieldParam) new PXDataFieldRestrict("UserName", PXDbType.VarChar, (object) PXAccess.GetUserName()));
    byte[] numArray1 = (byte[]) null;
    if (!settings.CommonSettings.CheckDefault())
      numArray1 = PXDatabase.Serialize((object[]) new ReportCommonSettings[1]
      {
        settings.CommonSettings
      });
    byte[] numArray2 = (byte[]) null;
    if (!settings.Mail.CheckDefault())
      numArray2 = PXDatabase.Serialize((object[]) new ReportMailSettings[1]
      {
        settings.Mail
      });
    byte[] numArray3 = ((List<ParameterDefault>) settings.ParameterValues).Count > 0 ? PXDatabase.Serialize((object[]) ((List<ParameterDefault>) settings.ParameterValues).ToArray()) : (byte[]) null;
    byte[] numArray4 = ((List<SortExp>) settings.Sorting).Count > 0 ? PXDatabase.Serialize((object[]) ((List<SortExp>) settings.Sorting).ToArray()) : (byte[]) null;
    byte[] numArray5 = ((List<FilterExp>) settings.Filters).Count > 0 ? PXDatabase.Serialize((object[]) ((List<FilterExp>) settings.Filters).ToArray()) : (byte[]) null;
    string userName = PXAccess.GetUserName();
    bool flag;
    using (PXDataRecord pxDataRecord1 = PXDatabase.SelectSingle<PXSettingProvider.ReportSettings>(new PXDataField("Name"), (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, (object) currentScreenID), (PXDataField) new PXDataFieldValue("Name", PXDbType.VarChar, (object) settings.Name), (PXDataField) new PXDataFieldValue("UserName", PXDbType.VarChar, (object) PXAccess.GetUserName())))
    {
      flag = pxDataRecord1 != null;
      if (!flag)
      {
        if (settings.IsShared)
        {
          using (PXDataRecord pxDataRecord2 = PXDatabase.SelectSingle<PXSettingProvider.ReportSettings>(new PXDataField("Name"), new PXDataField("UserName"), (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, (object) currentScreenID), (PXDataField) new PXDataFieldValue("Name", PXDbType.VarChar, (object) settings.Name), (PXDataField) new PXDataFieldValue("IsShared", PXDbType.Bit, (object) true)))
          {
            flag = pxDataRecord2 != null;
            if (flag)
              userName = pxDataRecord2.GetString(1);
          }
        }
      }
    }
    if (!flag)
      PXDatabase.Insert<PXSettingProvider.ReportSettings>(new PXDataFieldAssign("SettingsID", PXDbType.UniqueIdentifier, (object) Guid.NewGuid()), new PXDataFieldAssign("ScreenID", PXDbType.VarChar, (object) currentScreenID), new PXDataFieldAssign("Name", PXDbType.VarChar, (object) settings.Name), new PXDataFieldAssign("UserName", PXDbType.VarChar, (object) PXAccess.GetUserName()), new PXDataFieldAssign("IsShared", PXDbType.Bit, (object) settings.IsShared), new PXDataFieldAssign("IsDefault", PXDbType.Bit, (object) settings.IsDefault), new PXDataFieldAssign("CommonValues", PXDbType.VarBinary, (object) numArray1), new PXDataFieldAssign("MailValues", PXDbType.VarBinary, (object) numArray2), new PXDataFieldAssign("ParamValues", PXDbType.VarBinary, (object) numArray3), new PXDataFieldAssign("Sorting", PXDbType.VarBinary, (object) numArray4), new PXDataFieldAssign("Filters", PXDbType.VarBinary, (object) numArray5));
    else
      PXDatabase.Update<PXSettingProvider.ReportSettings>((PXDataFieldParam) new PXDataFieldRestrict("ScreenID", PXDbType.VarChar, (object) currentScreenID), (PXDataFieldParam) new PXDataFieldRestrict("Name", PXDbType.VarChar, (object) settings.Name), (PXDataFieldParam) new PXDataFieldRestrict("UserName", PXDbType.VarChar, (object) userName), (PXDataFieldParam) new PXDataFieldAssign("IsShared", PXDbType.Bit, (object) settings.IsShared), (PXDataFieldParam) new PXDataFieldAssign("IsDefault", PXDbType.Bit, (object) settings.IsDefault), (PXDataFieldParam) new PXDataFieldAssign("CommonValues", PXDbType.VarBinary, (object) numArray1), (PXDataFieldParam) new PXDataFieldAssign("MailValues", PXDbType.VarBinary, (object) numArray2), (PXDataFieldParam) new PXDataFieldAssign("ParamValues", PXDbType.VarBinary, (object) numArray3), (PXDataFieldParam) new PXDataFieldAssign("Sorting", PXDbType.VarBinary, (object) numArray4), (PXDataFieldParam) new PXDataFieldAssign("Filters", PXDbType.VarBinary, (object) numArray5));
  }

  public virtual void Save(PXReportSettings settings)
  {
    base.Save(settings, PXSiteMap.CurrentScreenID);
  }

  public virtual bool Delete(string name) => base.Delete(name, PXSiteMap.CurrentScreenID);

  public virtual bool Delete(string name, string currentScreenID)
  {
    if (PXDatabase.Delete<PXSettingProvider.ReportSettings>(new PXDataFieldRestrict("ScreenID", PXDbType.VarChar, (object) currentScreenID), new PXDataFieldRestrict("Name", PXDbType.VarChar, (object) name), new PXDataFieldRestrict("UserName", PXDbType.VarChar, (object) PXAccess.GetUserName())))
      return true;
    return PXDatabase.Delete<PXSettingProvider.ReportSettings>(new PXDataFieldRestrict("ScreenID", PXDbType.VarChar, (object) currentScreenID), new PXDataFieldRestrict("Name", PXDbType.VarChar, (object) name), new PXDataFieldRestrict("IsShared", PXDbType.Bit, (object) true));
  }

  public virtual bool Delete(Guid id)
  {
    return PXDatabase.Delete<PXSettingProvider.ReportSettings>((PXDataFieldRestrict) new PXDataFieldRestrict<PXSettingProvider.ReportSettings.settingsID>(PXDbType.UniqueIdentifier, (object) id));
  }

  private PXReportSettings CreateSettings(PXDataRecord rec)
  {
    byte[] input1 = rec.GetValue(3) as byte[];
    ReportCommonSettings reportCommonSettings = (ReportCommonSettings) null;
    if (input1 != null)
    {
      object[] objArray = PXDatabase.Deserialize(input1);
      if (objArray[0] is ReportCommonSettings)
        reportCommonSettings = (ReportCommonSettings) objArray[0];
    }
    ReportMailSettings reportMailSettings = (ReportMailSettings) null;
    if (rec.GetValue(4) is byte[] input2)
    {
      object[] objArray = PXDatabase.Deserialize(input2);
      if (objArray[0] is ReportMailSettings)
        reportMailSettings = (ReportMailSettings) objArray[0];
    }
    PXReportSettings settings = new PXReportSettings(rec.GetString(0), reportCommonSettings, reportMailSettings);
    PXReportSettings pxReportSettings1 = settings;
    bool? boolean1 = rec.GetBoolean(1);
    bool flag1 = true;
    int num1 = boolean1.GetValueOrDefault() == flag1 & boolean1.HasValue ? 1 : 0;
    pxReportSettings1.IsDefault = num1 != 0;
    PXReportSettings pxReportSettings2 = settings;
    bool? boolean2 = rec.GetBoolean(2);
    bool flag2 = true;
    int num2 = boolean2.GetValueOrDefault() == flag2 & boolean2.HasValue ? 1 : 0;
    pxReportSettings2.IsShared = num2 != 0;
    this.Deserialize<ParameterDefault>(rec.GetValue(5) as byte[], (List<ParameterDefault>) settings.ParameterValues);
    this.Deserialize<SortExp>(rec.GetValue(6) as byte[], (List<SortExp>) settings.Sorting);
    this.Deserialize<FilterExp>(rec.GetValue(7) as byte[], (List<FilterExp>) settings.Filters);
    return settings;
  }

  private void Deserialize<T>(byte[] buffer, List<T> collection)
  {
    if (buffer == null)
      return;
    foreach (T obj in PXDatabase.Deserialize(buffer) as T[])
      collection.Add(obj);
  }

  public virtual string Company
  {
    get
    {
      int? branchId = PXAccess.GetBranchID();
      return branchId.HasValue ? this.GetContactByBranchID(branchId.Value)?.FullName ?? string.Empty : base.Company;
    }
    set
    {
    }
  }

  private PXSettingProvider.Contact GetContactByBranchID(int branchID)
  {
    return (PXSettingProvider.Contact) PXSelectBase<PXSettingProvider.Contact, PXSelectJoinGroupBy<PXSettingProvider.Contact, InnerJoin<PXSettingProvider.BAccount, On<PXSettingProvider.Contact.contactID, Equal<PXSettingProvider.BAccount.defContactID>, And<PXSettingProvider.Contact.bAccountID, Equal<PXSettingProvider.BAccount.bAccountID>>>, InnerJoin<PXSettingProvider.Organization, On<PXSettingProvider.Organization.bAccountID, Equal<PXSettingProvider.BAccount.bAccountID>>, InnerJoin<PXSettingProvider.Branch, On<PXSettingProvider.Organization.organizationID, Equal<PXSettingProvider.Branch.organizationID>>>>>, Where<PXSettingProvider.Branch.branchID, Equal<Required<PXSettingProvider.Branch.branchID>>, And<PXSettingProvider.Branch.companyID, GreaterEqual<PX.Data.Zero>, And<PXSettingProvider.BAccount.companyID, GreaterEqual<PX.Data.Zero>, And<PXSettingProvider.Contact.companyID, GreaterEqual<PX.Data.Zero>, And<PXSettingProvider.Organization.companyID, GreaterEqual<PX.Data.Zero>>>>>>, Aggregate<GroupBy<PXSettingProvider.Organization.organizationID>>>.Config>.SelectSingleBound(new PXGraph(), (object[]) null, (object) branchID);
  }

  [Serializable]
  public class ReportSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _ScreenID;
    protected string _Name;
    protected bool? _IsDefault;
    protected bool? _IsShared;
    protected byte[] _CommonValues;
    protected byte[] _ParamValues;
    protected byte[] _Sorting;
    protected byte[] _Filters;
    protected bool? _MergeReports;
    protected int? _MergingOrder;

    [PXDBGuid(true, IsKey = true)]
    public virtual Guid? SettingsID { get; set; }

    [PXDBString(8, InputMask = "CC.CC.CC.CC")]
    [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string ScreenID
    {
      get => this._ScreenID;
      set => this._ScreenID = value;
    }

    [PXDBString(50)]
    [PXUIField(DisplayName = "Report Template", Visibility = PXUIVisibility.SelectorVisible)]
    public string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    [PXDefault(false)]
    [PXDBBool]
    public virtual bool? IsDefault
    {
      get => this._IsDefault;
      set => this._IsDefault = value;
    }

    [PXDefault(false)]
    [PXDBBool]
    public virtual bool? IsShared
    {
      get => this._IsShared;
      set => this._IsShared = value;
    }

    [PXDBBinary(500)]
    public byte[] CommonValues
    {
      get => this._CommonValues;
      set => this._CommonValues = value;
    }

    [PXDBBinary]
    public byte[] ParamValues
    {
      get => this._ParamValues;
      set => this._ParamValues = value;
    }

    [PXDBBinary]
    public byte[] Sorting
    {
      get => this._Sorting;
      set => this._Sorting = value;
    }

    [PXDBBinary]
    public byte[] Filters
    {
      get => this._Filters;
      set => this._Filters = value;
    }

    [PXDBBool]
    public bool? MergeReports
    {
      get => this._MergeReports;
      set => this._MergeReports = value;
    }

    [PXDBInt]
    public int? MergingOrder
    {
      get => this._MergingOrder;
      set => this._MergingOrder = value;
    }

    public abstract class settingsID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.settingsID>
    {
    }

    public abstract class screenID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.screenID>
    {
    }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.name>
    {
    }

    public abstract class isDefault : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.isDefault>
    {
    }

    public abstract class isShared : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.isShared>
    {
    }

    public abstract class commonValues : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.commonValues>
    {
    }

    public abstract class paramValues : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.paramValues>
    {
    }

    public abstract class sorting : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.sorting>
    {
    }

    public abstract class filters : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.filters>
    {
    }

    public abstract class mergeReports : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.mergeReports>
    {
    }

    public abstract class mergingOrder : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.ReportSettings.mergingOrder>
    {
    }
  }

  [Obsolete("Will be removed in future versions of Acumatica")]
  public class CompanyGraph : PXGraph<PXSettingProvider.CompanyGraph>
  {
    public PXSelectJoinGroupBy<PXSettingProvider.Contact, InnerJoin<PXSettingProvider.BAccount, On<PXSettingProvider.Contact.contactID, Equal<PXSettingProvider.BAccount.defContactID>, And<PXSettingProvider.Contact.bAccountID, Equal<PXSettingProvider.BAccount.bAccountID>>>, InnerJoin<PXSettingProvider.Organization, On<PXSettingProvider.Organization.bAccountID, Equal<PXSettingProvider.BAccount.bAccountID>>, InnerJoin<PXSettingProvider.Branch, On<PXSettingProvider.Organization.organizationID, Equal<PXSettingProvider.Branch.organizationID>>>>>, Where<PXSettingProvider.Branch.branchID, Equal<Required<PXSettingProvider.Branch.branchID>>, And<PXSettingProvider.Branch.companyID, GreaterEqual<PX.Data.Zero>, And<PXSettingProvider.BAccount.companyID, GreaterEqual<PX.Data.Zero>, And<PXSettingProvider.Contact.companyID, GreaterEqual<PX.Data.Zero>, And<PXSettingProvider.Organization.companyID, GreaterEqual<PX.Data.Zero>>>>>>, Aggregate<GroupBy<PXSettingProvider.Organization.organizationID>>> OrganizationContacts;
  }

  [PXHidden]
  public class Organization : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public virtual int? OrganizationID { get; set; }

    [PXDBInt]
    public virtual int? BAccountID { get; set; }

    [PXDBInt]
    public virtual int? CompanyID { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.Organization.organizationID>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.Organization.bAccountID>
    {
    }

    public abstract class companyID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.Organization.companyID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class Branch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public virtual int? OrganizationID { get; set; }

    [PXDBInt]
    public virtual int? BranchID { get; set; }

    [PXDBInt]
    public virtual int? BAccountID { get; set; }

    [PXDBInt]
    public virtual int? CompanyID { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.Branch.organizationID>
    {
    }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXSettingProvider.Branch.branchID>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.Branch.bAccountID>
    {
    }

    public abstract class companyID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXSettingProvider.Branch.companyID>
    {
    }
  }

  [Serializable]
  public class Contact : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public virtual int? BAccountID { get; set; }

    [PXDBInt]
    public virtual int? ContactID { get; set; }

    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    public virtual string FullName { get; set; }

    [PXDBInt]
    public virtual int? CompanyID { get; set; }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.Contact.bAccountID>
    {
    }

    public abstract class contactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.Contact.contactID>
    {
    }

    public abstract class fullName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXSettingProvider.Contact.fullName>
    {
    }

    public abstract class companyID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.Contact.companyID>
    {
    }
  }

  [Serializable]
  public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public virtual int? DefContactID { get; set; }

    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXDBInt]
    public virtual int? CompanyID { get; set; }

    public abstract class defContactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.BAccount.defContactID>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.BAccount.bAccountID>
    {
    }

    public abstract class companyID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXSettingProvider.BAccount.companyID>
    {
    }
  }
}
