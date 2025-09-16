// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncCache
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Update;
using PX.Data.Update.ExchangeService;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncCache
{
  private object _locker = new object();
  private Dictionary<string, int> _UsersCache;
  private Dictionary<int, int> _EmployeeCache;
  private Dictionary<string, Tuple<string, List<Tuple<string, string>>>> _CountryCache;
  private Dictionary<string, Guid> _ContactsCache;
  private Dictionary<System.Type, Dictionary<string, Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo>>> _FieldsCache;

  public Dictionary<string, int> UsersCache
  {
    get
    {
      if (this._UsersCache == null)
      {
        lock (this._locker)
        {
          this._UsersCache = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
          foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Contact>(new PXDataField[3]
          {
            (PXDataField) new PXDataField<Contact.eMail>(),
            (PXDataField) new PXDataField<Contact.contactID>(),
            (PXDataField) new PXDataFieldValue<Contact.contactType>((object) "EP")
          }))
          {
            if (!string.IsNullOrEmpty(pxDataRecord.GetString(0)))
              this._UsersCache[pxDataRecord.GetString(0)] = pxDataRecord.GetInt32(1).Value;
          }
        }
      }
      return this._UsersCache;
    }
  }

  public Dictionary<int, int> EmployeeCache
  {
    get
    {
      if (this._EmployeeCache == null)
      {
        lock (this._locker)
        {
          this._EmployeeCache = new Dictionary<int, int>();
          foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PX.Objects.CR.BAccount>(new PXDataField[3]
          {
            (PXDataField) new PXDataField<PX.Objects.CR.BAccount.bAccountID>(),
            (PXDataField) new PXDataField<PX.Objects.CR.BAccount.defContactID>(),
            (PXDataField) new PXDataFieldValue<PX.Objects.CR.BAccount.type>((object) "EP")
          }))
          {
            int? int32 = pxDataRecord.GetInt32(0);
            if (int32.HasValue)
            {
              int32 = pxDataRecord.GetInt32(1);
              if (int32.HasValue)
              {
                Dictionary<int, int> employeeCache = this._EmployeeCache;
                int32 = pxDataRecord.GetInt32(0);
                int key = int32.Value;
                int32 = pxDataRecord.GetInt32(1);
                int num = int32.Value;
                employeeCache[key] = num;
              }
            }
          }
        }
      }
      return this._EmployeeCache;
    }
  }

  public Dictionary<string, Tuple<string, List<Tuple<string, string>>>> CountryCache
  {
    get
    {
      if (this._CountryCache == null)
      {
        lock (this._locker)
        {
          this._CountryCache = new Dictionary<string, Tuple<string, List<Tuple<string, string>>>>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
          foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PX.Objects.CS.Country>(new PXDataField[2]
          {
            (PXDataField) new PXDataField<PX.Objects.CS.Country.countryID>(),
            (PXDataField) new PXDataField<PX.Objects.CS.Country.description>()
          }))
          {
            if (pxDataRecord.GetString(0) != null)
              this._CountryCache[pxDataRecord.GetString(0)] = Tuple.Create<string, List<Tuple<string, string>>>(pxDataRecord.GetString(1), new List<Tuple<string, string>>());
          }
          foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PX.Objects.CS.State>(new PXDataField[3]
          {
            (PXDataField) new PXDataField<PX.Objects.CS.State.countryID>(),
            (PXDataField) new PXDataField<PX.Objects.CS.State.stateID>(),
            (PXDataField) new PXDataField<PX.Objects.CS.State.name>()
          }))
          {
            Tuple<string, List<Tuple<string, string>>> tuple;
            if (pxDataRecord.GetString(0) != null && pxDataRecord.GetString(1) != null && this._CountryCache.TryGetValue(pxDataRecord.GetString(0), out tuple))
              tuple.Item2.Add(Tuple.Create<string, string>(pxDataRecord.GetString(1), pxDataRecord.GetString(2)));
          }
        }
      }
      return this._CountryCache;
    }
  }

  public Guid? ContactsCache(string email)
  {
    if (string.IsNullOrEmpty(email))
      return new Guid?();
    lock (this._locker)
    {
      if (this._ContactsCache == null)
        this._ContactsCache = new Dictionary<string, Guid>();
      Guid guid1;
      if (this._ContactsCache.TryGetValue(email, out guid1))
        return new Guid?(guid1);
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Contact>(new PXDataField[3]
      {
        (PXDataField) new PXDataField<Contact.noteID>(),
        (PXDataField) new PXDataFieldValue<Contact.eMail>((object) email),
        (PXDataField) new PXDataFieldValue<Contact.contactType>((object) "PN")
      }))
      {
        if (pxDataRecord != null)
        {
          Guid? guid2 = pxDataRecord.GetGuid(0);
          if (guid2.HasValue)
          {
            Dictionary<string, Guid> contactsCache = this._ContactsCache;
            string key = email;
            guid2 = pxDataRecord.GetGuid(0);
            Guid guid3 = guid2.Value;
            contactsCache[key] = guid3;
            return pxDataRecord.GetGuid(0);
          }
        }
      }
    }
    return new Guid?();
  }

  public Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo> FieldsCache(
    System.Type type,
    string field)
  {
    lock (this._locker)
    {
      if (this._FieldsCache == null)
        this._FieldsCache = new Dictionary<System.Type, Dictionary<string, Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo>>>();
      Dictionary<string, Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo>> dictionary;
      if (!this._FieldsCache.TryGetValue(type, out dictionary))
      {
        this._FieldsCache[type] = dictionary = new Dictionary<string, Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo>>();
        foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
          if (property.CanRead && property.CanWrite)
          {
            bool flag1 = property.Name.EndsWith("Specified") && property.Name.Length > 9 && property.PropertyType == typeof (bool);
            bool flag2 = property.Name.EndsWith("TimeZone") && property.Name.Length > 8 && property.PropertyType == typeof (TimeZoneDefinitionType);
            bool flag3 = property.Name.EndsWith("TimeZoneId") && property.Name.Length > 8 && property.PropertyType == typeof (string);
            string key = property.Name;
            if (flag1)
              key = property.Name.Substring(0, property.Name.Length - 9);
            if (flag2)
              key = property.Name.Substring(0, property.Name.Length - 8);
            if (flag3)
              key = property.Name.Substring(0, property.Name.Length - 10);
            Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo> gang;
            if (!dictionary.TryGetValue(key, out gang))
              dictionary[key] = gang = new Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo>((PropertyInfo) null, (PropertyInfo) null, (PropertyInfo) null, (PropertyInfo) null);
            if (flag1)
              ((Gang<PropertyInfo, PropertyInfo>) gang).Item2 = property;
            else if (flag2)
              ((Gang<PropertyInfo, PropertyInfo, PropertyInfo>) gang).Item3 = property;
            else if (flag3)
              gang.Item4 = property;
            else
              ((Gang<PropertyInfo>) gang).Item1 = property;
          }
        }
      }
      Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo> gang1;
      dictionary.TryGetValue(field, out gang1);
      return gang1;
    }
  }
}
