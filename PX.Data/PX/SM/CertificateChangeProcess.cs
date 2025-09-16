// Decompiled with JetBrains decompiler
// Type: PX.SM.CertificateChangeProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Api.Soap.Screen;
using PX.Common;
using PX.Common.Cryptography;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable enable
namespace PX.SM;

[Serializable]
public class CertificateChangeProcess : PXGraph<
#nullable disable
CertificateChangeProcess>
{
  public PXCancel<CertificateChangeProcess.ProcessFilter> Cancel;
  public PXFilter<CertificateChangeProcess.ProcessFilter> Filter;
  public PXFilteredProcessing<CertificateChangeProcess.EncryptedSource, CertificateChangeProcess.ProcessFilter> Process;
  public PXSetup<PreferencesSecurity> setup;

  public CertificateChangeProcess()
  {
    this.Process.SetProcessVisible(false);
    this.Process.SetProcessAllCaption("Replace Certificate");
  }

  protected virtual IEnumerable process()
  {
    if (!this.Process.Cache.Cached.Cast<object>().Any<object>())
    {
      CertificateChangeProcess.EncryptedSourceDefinition slot = PXDatabase.GetSlot<CertificateChangeProcess.EncryptedSourceDefinition>("EncryptedSourceDefinition");
      if (slot != null && slot.EncryptedSourceList != null)
      {
        foreach (CertificateChangeProcess.EncryptedSource encryptedSource in slot.EncryptedSourceList)
        {
          if (this.Process.Locate(encryptedSource) == null)
            this.Process.Insert(encryptedSource);
        }
        this.Process.Cache.IsDirty = false;
      }
    }
    return this.Process.Cache.Cached;
  }

  protected virtual void ProcessFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CertificateChangeProcess.ProcessFilter row = (CertificateChangeProcess.ProcessFilter) e.Row;
    if (row == null)
      return;
    if (this.setup.Current.DBPrevCertificateName == null)
    {
      row.CurrentCertificate = this.setup.Current.DBCertificateName;
    }
    else
    {
      row.PendingCertificate = this.setup.Current.DBCertificateName;
      if (!string.IsNullOrEmpty(this.setup.Current.DBPrevCertificateName))
        row.CurrentCertificate = this.setup.Current.DBPrevCertificateName;
    }
    string certificate = row.PendingCertificate;
    string prevCertificate = row.CurrentCertificate;
    this.Process.SetProcessDelegate((PXProcessingBase<CertificateChangeProcess.EncryptedSource>.ProcessListDelegate) (items => CertificateChangeProcess.ProcessEncryption(items, certificate, prevCertificate)));
  }

  private static void ProcessEncryption(
    List<CertificateChangeProcess.EncryptedSource> list,
    string certificate,
    string prevCertificate)
  {
    PXGraph graph = new PXGraph();
    PreferencesSecurity current = new PXSetup<PreferencesSecurity>(graph).Current;
    if (certificate == prevCertificate)
      throw new PXSetPropertyException("The encryption certificate cannot be changed. The pending certificate is the same as the current one.");
    string certificate1 = SitePolicy.ValidateCertificate(certificate) || certificate == null ? certificate : throw new PXSetPropertyException("An encryption certificate cannot be created. Make sure that the certificate has been configured properly and the valid certificate file has been uploaded to the site.");
    string prevCertificate1 = prevCertificate;
    if (!CertificateChangeProcess.UpdateSecPrefBeforeEncryption(current, certificate1, prevCertificate1))
      throw new PXSetPropertyException("The encryption certificate cannot be changed. The previous certificate changes have not been completed.");
    SitePolicy.Clear();
    CryptoProvider rsaCryptoProvider = SitePolicy.RSACryptoProvider;
    bool flag = false;
    using (new PXReadDeletedScope())
    {
      for (int index = 0; index < list.Count; ++index)
      {
        CertificateChangeProcess.EncryptedSource encryptedSource = list[index];
        try
        {
          System.Type type = GraphHelper.GetType(encryptedSource.EntityType);
          BqlCommand instance = BqlCommand.CreateInstance(typeof (PX.Data.Select<>), type);
          PXView pxView = new PXView(graph, true, instance);
          PXCache cache = graph.Caches[type];
          string[] strArray = encryptedSource.EncryptedFields.Split(',');
          List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
          foreach (object obj in pxView.SelectMulti())
          {
            object row = obj;
            pxDataFieldParamList.AddRange((IEnumerable<PXDataFieldParam>) cache.Keys.Select<string, PXDataFieldRestrict>((Func<string, PXDataFieldRestrict>) (key => new PXDataFieldRestrict(key, cache.GetValue(row, key)))));
            foreach (string str1 in strArray)
            {
              object s = cache.GetValue(row, str1);
              if (!string.IsNullOrEmpty((string) s))
              {
                PXRSACryptStringAttribute cryptStringAttribute = cache.GetAttributesReadonly(str1).OfType<PXRSACryptStringAttribute>().FirstOrDefault<PXRSACryptStringAttribute>();
                if (cryptStringAttribute != null && cryptStringAttribute.EncryptOnCertificateReplacement(cache, row))
                {
                  byte[] bytes = Encoding.Unicode.GetBytes((string) s);
                  string str2 = !string.IsNullOrEmpty(certificate) ? Convert.ToBase64String(rsaCryptoProvider.Encrypt(bytes)) : Convert.ToBase64String(bytes).Replace(PXDatabase.Provider.SqlDialect.WildcardFieldSeparator, string.Empty);
                  pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign(str1, (object) str2));
                }
              }
            }
            if (pxDataFieldParamList.Count > cache.Keys.Count)
              PXDatabase.Update(type, pxDataFieldParamList.ToArray());
            pxDataFieldParamList.Clear();
          }
        }
        catch (Exception ex)
        {
          PXProcessing<CertificateChangeProcess.EncryptedSource>.SetError(index, ex.Message);
          flag = true;
        }
      }
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
    PXDatabase.Update<PreferencesSecurity>((PXDataFieldParam) new PXDataFieldAssign("DBPrevCertificateName", PXDbType.VarChar, (object) null), (PXDataFieldParam) new PXDataFieldRestrict("DBPrevCertificateName", PXDbType.VarChar, (object) (prevCertificate ?? string.Empty)));
  }

  private static bool UpdateSecPrefBeforeEncryption(
    PreferencesSecurity security,
    string certificate,
    string prevCertificate)
  {
    bool flag = false;
    List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PreferencesSecurity>(new PXDataField("PdfCertificateName"), new PXDataField("TraceMonthsKeep"), new PXDataField("TraceOperationMask"), new PXDataField("PasswordDayAge"), new PXDataField("PasswordMinLength"), new PXDataField("PasswordComplexity"), new PXDataField("PasswordRegexCheck"), new PXDataField("PasswordRegexCheckMessage"), new PXDataField("AccountLockoutThreshold"), new PXDataField("AccountLockoutDuration"), new PXDataField("AccountLockoutReset"), new PXDataField("PasswordSecurityType"), new PXDataField("CreatedByID"), new PXDataField("CreatedByScreenID"), new PXDataField("CreatedDateTime"), new PXDataField("LastModifiedByID"), new PXDataField("LastModifiedByScreenID"), new PXDataField("LastModifiedDateTime"), (PXDataField) new PXDataField<PreferencesSecurity.multiFactorAllowedTypes>(), (PXDataField) (security.DBPrevCertificateName != null ? new PXDataFieldValue("DBPrevCertificateName", PXDbType.VarChar, (object) security.DBPrevCertificateName) : (prevCertificate == null ? new PXDataFieldValue("DBCertificateName", PXDbType.VarChar, new int?(0), (object) null, PXComp.ISNULL) : new PXDataFieldValue("DBCertificateName", PXDbType.VarChar, (object) prevCertificate)))))
    {
      if (pxDataRecord != null)
      {
        pxDataFieldAssignList.Add(new PXDataFieldAssign("DBCertificateName", PXDbType.VarChar, (object) certificate));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("DBPrevCertificateName", PXDbType.VarChar, (object) (prevCertificate ?? string.Empty)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PdfCertificateName", PXDbType.VarChar, (object) pxDataRecord.GetString(0)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("TraceMonthsKeep", PXDbType.Int, (object) pxDataRecord.GetInt32(1)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("TraceOperationMask", PXDbType.Int, (object) pxDataRecord.GetInt32(2)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordDayAge", PXDbType.Int, (object) pxDataRecord.GetInt32(3)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordMinLength", PXDbType.Int, (object) pxDataRecord.GetInt32(4)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordComplexity", PXDbType.Bit, (object) pxDataRecord.GetBoolean(5)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordRegexCheck", PXDbType.VarChar, (object) pxDataRecord.GetString(6)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordRegexCheckMessage", PXDbType.VarChar, (object) pxDataRecord.GetString(7)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("AccountLockoutThreshold", PXDbType.Int, (object) pxDataRecord.GetInt32(8)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("AccountLockoutDuration", PXDbType.Int, (object) pxDataRecord.GetInt32(9)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("AccountLockoutReset", PXDbType.Int, (object) pxDataRecord.GetInt32(10)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordSecurityType", PXDbType.SmallInt, (object) pxDataRecord.GetInt16(11)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("CreatedByID", PXDbType.UniqueIdentifier, (object) pxDataRecord.GetGuid(12)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("CreatedByScreenID", PXDbType.VarChar, (object) pxDataRecord.GetString(13)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("CreatedDateTime", PXDbType.DateTime, (object) pxDataRecord.GetDateTime(14)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastModifiedByID", PXDbType.UniqueIdentifier, (object) pxDataRecord.GetGuid(15)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastModifiedByScreenID", PXDbType.VarChar, (object) pxDataRecord.GetString(16 /*0x10*/)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastModifiedDateTime", PXDbType.DateTime, (object) pxDataRecord.GetDateTime(17)));
        pxDataFieldAssignList.Add((PXDataFieldAssign) new PXDataFieldAssign<PreferencesSecurity.multiFactorAllowedTypes>(PXDbType.Int, (object) pxDataRecord.GetInt32(18)));
      }
    }
    PXDataFieldRestrict dataFieldRestrict = security.DBPrevCertificateName != null ? new PXDataFieldRestrict("DBPrevCertificateName", PXDbType.VarChar, (object) security.DBPrevCertificateName) : (prevCertificate == null ? new PXDataFieldRestrict("DBCertificateName", PXDbType.VarChar, new int?(0), (object) null, PXComp.ISNULL) : new PXDataFieldRestrict("DBCertificateName", PXDbType.VarChar, (object) prevCertificate));
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        flag = PXDatabase.Update<PreferencesSecurity>((PXDataFieldParam) new PXDataFieldAssign("DBCertificateName", PXDbType.VarChar, (object) certificate), (PXDataFieldParam) new PXDataFieldAssign("DBPrevCertificateName", PXDbType.VarChar, (object) (prevCertificate ?? string.Empty)), (PXDataFieldParam) dataFieldRestrict, (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
        transactionScope.Complete();
      }
      catch (PXDbOperationSwitchRequiredException ex)
      {
        if (pxDataFieldAssignList.Count > 0)
        {
          flag = PXDatabase.Insert<PreferencesSecurity>(pxDataFieldAssignList.ToArray());
          transactionScope.Complete();
        }
      }
    }
    return flag;
  }

  [Serializable]
  public class ProcessFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _PendingCertificate;
    protected string _CurrentCertificate;

    [PXDBString(30)]
    [PXDefault]
    [PXUIField(DisplayName = "New Certificate")]
    [PXSelector(typeof (Certificate.name))]
    public virtual string PendingCertificate
    {
      get => this._PendingCertificate;
      set => this._PendingCertificate = value;
    }

    [PXDBString(30)]
    [PXUIField(DisplayName = "Current Certificate", Enabled = false)]
    [PXSelector(typeof (Certificate.name))]
    public virtual string CurrentCertificate
    {
      get => this._CurrentCertificate;
      set => this._CurrentCertificate = value;
    }

    public abstract class pendingCertificate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CertificateChangeProcess.ProcessFilter.pendingCertificate>
    {
    }

    public abstract class currentCertificate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CertificateChangeProcess.ProcessFilter.currentCertificate>
    {
    }
  }

  [PXVirtual]
  [Serializable]
  public class EncryptedSource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected string _EntityType;
    protected string _EntityName;
    protected string _EncryptedFields;

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected", Visible = false)]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXDBString(1024 /*0x0400*/, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Entity Type")]
    public virtual string EntityType
    {
      get => this._EntityType;
      set => this._EntityType = value;
    }

    [PXDBString(300)]
    [PXUIField(DisplayName = "Entity Name")]
    public virtual string EntityName
    {
      get => this._EntityName;
      set => this._EntityName = value;
    }

    [PXDBString]
    public virtual string EncryptedFields
    {
      get => this._EncryptedFields;
      set => this._EncryptedFields = value;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CertificateChangeProcess.EncryptedSource.selected>
    {
    }

    public abstract class entityType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CertificateChangeProcess.EncryptedSource.entityType>
    {
    }

    public abstract class entityName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CertificateChangeProcess.EncryptedSource.entityName>
    {
    }

    public abstract class encryptedFields : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CertificateChangeProcess.EncryptedSource.encryptedFields>
    {
    }
  }

  public class EncryptedSourceDefinition : IPrefetchable, IPXCompanyDependent
  {
    public const string SLOT_KEY = "EncryptedSourceDefinition";

    public List<CertificateChangeProcess.EncryptedSource> EncryptedSourceList { get; private set; }

    public void Prefetch()
    {
      Dictionary<string, CertificateChangeProcess.EncryptedSource> dictionary = new Dictionary<string, CertificateChangeProcess.EncryptedSource>();
      foreach (System.Type allTable in ServiceManager.AllTables)
      {
        if (!(PXCache.GetBqlTable(allTable) != allTable))
        {
          PXRSACryptStringAttribute[] array;
          try
          {
            array = ((IEnumerable) typeof (PXCache<>).MakeGenericType(allTable).GetMethod("GetAttributesStatic", BindingFlags.Static | BindingFlags.NonPublic).Invoke((object) null, new object[0])).OfType<PXRSACryptStringAttribute>().ToArray<PXRSACryptStringAttribute>();
          }
          catch
          {
            continue;
          }
          if (array.Length != 0)
          {
            string str = ((IEnumerable<PXRSACryptStringAttribute>) array).Select<PXRSACryptStringAttribute, string>((Func<PXRSACryptStringAttribute, string>) (a => a.FieldName)).JoinToString<string>(",");
            CertificateChangeProcess.EncryptedSource encryptedSource = new CertificateChangeProcess.EncryptedSource()
            {
              EntityType = allTable.FullName,
              EntityName = EntityHelper.GetFriendlyEntityName(allTable),
              EncryptedFields = str
            };
            if (encryptedSource.EntityName == allTable.FullName)
              encryptedSource.EntityName = allTable.Name;
            if (!dictionary.ContainsKey(encryptedSource.EntityType))
              dictionary[encryptedSource.EntityType] = encryptedSource;
          }
        }
      }
      this.EncryptedSourceList = new List<CertificateChangeProcess.EncryptedSource>((IEnumerable<CertificateChangeProcess.EncryptedSource>) dictionary.Values);
    }
  }
}
