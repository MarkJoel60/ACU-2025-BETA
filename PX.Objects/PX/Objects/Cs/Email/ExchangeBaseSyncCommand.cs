// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.ExchangeBaseSyncCommand
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Update;
using PX.Data.Update.ExchangeService;
using PX.Data.Update.WebServices;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CS.Email;

public abstract class ExchangeBaseSyncCommand : IDisposable
{
  protected string OperationCode;
  protected MicrosoftExchangeSyncProvider Provider;
  protected Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

  public EMailSyncServer Account => this.Provider.Account;

  public EMailSyncPolicy Policy => this.Provider.Policy;

  public PXSyncCache Cache => this.Provider.Cache;

  public PXExchangeServer Gate => this.Provider.Gate;

  protected ExchangeBaseSyncCommand(MicrosoftExchangeSyncProvider provider, string operationCode)
  {
    this.Provider = provider;
    this.OperationCode = operationCode;
  }

  public virtual void Dispose()
  {
  }

  public abstract void ProcessSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes);

  protected EMailSyncAccount GetSyncAccount(PXGraph graph, int? accID)
  {
    return PXResultset<EMailSyncAccount>.op_Implicit(PXSelectBase<EMailSyncAccount, PXSelectReadonly<EMailSyncAccount, Where<EMailSyncAccount.emailAccountID, Equal<Required<EMailSyncAccount.emailAccountID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) accID
    }));
  }

  protected void SetSyncAccount(
    PXGraph graph,
    int? accID,
    string operation,
    PXEmailSyncDirection.Directions direction,
    DateTime? date,
    string folderID,
    bool? hasErrors)
  {
    if (!accID.HasValue)
      return;
    EMailSyncAccount syncAccount = this.GetSyncAccount(graph, accID);
    if (syncAccount == null)
      return;
    PXCache cach = graph.Caches[typeof (EMailSyncAccount)];
    if (direction == 3)
    {
      if (date.HasValue)
      {
        PXCache pxCache1 = cach;
        EMailSyncAccount emailSyncAccount1 = syncAccount;
        string str1 = $"{operation}{((PXEmailSyncDirection.Directions) 2).ToString()}Date";
        DateTime? nullable = date;
        // ISSUE: variable of a boxed type
        __Boxed<DateTime> local1 = (ValueType) (nullable ?? PXTimeZoneInfo.Now);
        pxCache1.SetValue((object) emailSyncAccount1, str1, (object) local1);
        PXCache pxCache2 = cach;
        EMailSyncAccount emailSyncAccount2 = syncAccount;
        string str2 = $"{operation}{((PXEmailSyncDirection.Directions) 1).ToString()}Date";
        nullable = date;
        // ISSUE: variable of a boxed type
        __Boxed<DateTime> local2 = (ValueType) (nullable ?? PXTimeZoneInfo.Now);
        pxCache2.SetValue((object) emailSyncAccount2, str2, (object) local2);
      }
      if (folderID != null)
      {
        cach.SetValue((object) syncAccount, $"{operation}{((PXEmailSyncDirection.Directions) 2).ToString()}Folder", (object) folderID);
        cach.SetValue((object) syncAccount, $"{operation}{((PXEmailSyncDirection.Directions) 1).ToString()}Folder", (object) folderID);
      }
    }
    else
    {
      if (date.HasValue)
        cach.SetValue((object) syncAccount, $"{operation}{direction.ToString()}Date", (object) date);
      if (folderID != null)
        cach.SetValue((object) syncAccount, $"{operation}{direction.ToString()}Folder", (object) folderID);
    }
    if (hasErrors.GetValueOrDefault())
      syncAccount.HasErrors = new bool?(true);
    syncAccount.ToReinitialize = new bool?(false);
    syncAccount.IsReset = new bool?(false);
    cach.Update((object) syncAccount);
    cach.Persist((PXDBOperation) 2);
    cach.Persist((PXDBOperation) 1);
    graph.Clear();
  }

  protected DateTime? GetDateTime(PXEmailSyncDirection.Directions direction, PXSyncMailbox mailbox)
  {
    DateTime? dateTime1;
    if (direction != 1)
    {
      if (direction != 2)
        throw new Exception("The sync direction is incorrect.");
      dateTime1 = mailbox.ExportPreset.Date;
      if (!dateTime1.HasValue)
        dateTime1 = new DateTime?(new DateTime(1901, 1, 1));
    }
    else
      dateTime1 = mailbox.ImportPreset.Date;
    if (dateTime1.HasValue)
    {
      DateTime? nullable = dateTime1;
      DateTime dateTime2 = new DateTime(1901, 1, 1);
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) != 0)
        dateTime1 = new DateTime?(new DateTime(1901, 1, 1));
      dateTime1 = new DateTime?(PXTimeZoneInfo.ConvertTimeToUtc(dateTime1.Value, LocaleInfo.GetTimeZone()));
    }
    return dateTime1;
  }

  protected DateTime? GetDateTime(
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    DateTime? dateTime1;
    if (direction != 1)
    {
      if (direction != 2)
        throw new Exception("The sync direction is incorrect.");
      dateTime1 = mailboxes.Any<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => !m.ExportPreset.Date.HasValue)) ? new DateTime?() : mailboxes.Min<PXSyncMailbox, DateTime?>((Func<PXSyncMailbox, DateTime?>) (m => m.ExportPreset.Date));
      if (!dateTime1.HasValue)
        dateTime1 = new DateTime?(new DateTime(1901, 1, 1));
    }
    else
      dateTime1 = mailboxes.Any<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => !m.ImportPreset.Date.HasValue)) ? new DateTime?() : mailboxes.Min<PXSyncMailbox, DateTime?>((Func<PXSyncMailbox, DateTime?>) (m => m.ImportPreset.Date));
    if (dateTime1.HasValue)
    {
      DateTime? nullable = dateTime1;
      DateTime dateTime2 = new DateTime(1901, 1, 1);
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) != 0)
        dateTime1 = new DateTime?(new DateTime(1901, 1, 1));
      dateTime1 = new DateTime?(PXTimeZoneInfo.ConvertTimeToUtc(dateTime1.Value, LocaleInfo.GetTimeZone()));
    }
    return dateTime1;
  }

  protected DateTime ConvertDateTime(DateTime? acuDate)
  {
    return !acuDate.HasValue ? new DateTime(1950, 1, 1) : PXTimeZoneInfo.ConvertTimeToUtc(acuDate.Value, LocaleInfo.GetTimeZone());
  }

  protected void PostSyncHandling(
    PXGraph graph,
    string operationCode,
    PXEmailSyncDirection.Directions direction,
    DateTime date,
    IEnumerable<PXSyncMailbox> mailboxes,
    IEnumerable<PXSyncResult> processed)
  {
    int num1 = 0;
    int num2 = 0;
    foreach (PXSyncResult pxSyncResult in processed)
    {
      ++num1;
      if (!pxSyncResult.Success)
      {
        ++num2;
        string errorMessage = this.Provider.CreateErrorMessage(false, PXMessages.LocalizeFormatNoPrefix("An error has occured during {0} sync. {1} of item '{2}' failed.", new object[3]
        {
          (object) operationCode,
          (object) direction.ToString(),
          (object) pxSyncResult.DisplayKey
        }), pxSyncResult.Message, pxSyncResult.Error, pxSyncResult.Details);
        this.StoreError(pxSyncResult.Address, errorMessage);
      }
    }
    foreach (PXSyncMailbox mailbox in mailboxes)
    {
      bool flag = this.HasErrors(mailbox.Address);
      if (this.Policy.SkipError.GetValueOrDefault() || !flag)
        this.SetSyncAccount(graph, mailbox.EmailAccountID, operationCode, direction, new DateTime?(date), (string) null, new bool?(flag));
    }
    this.Provider.LogInfo((string) null, "{0} {1} for {2} mailboxes have been completed. Total {3} have been processed, including {4} failed.", (object) direction.ToString(), (object) operationCode, (object) mailboxes.Count<PXSyncMailbox>(), (object) num1, (object) num2);
  }

  protected PXSyncResult SafeOperation(
    string mailbox,
    string id,
    Guid? note,
    string key,
    string title,
    PXSyncItemStatus status,
    Action action,
    bool logOperation)
  {
    return this.SafeOperation((PXEmailSyncDirection.Directions) 1, mailbox, id, note, key, title, status, action, logOperation);
  }

  protected PXSyncResult SafeOperation(
    PXEmailSyncDirection.Directions direction,
    string mailbox,
    string id,
    Guid? note,
    string key,
    string title,
    PXSyncItemStatus status,
    Action action,
    bool logOperation)
  {
    PXSyncResult result;
    try
    {
      PXTimeTagAttribute.SyncScope syncScope = (PXTimeTagAttribute.SyncScope) null;
      try
      {
        syncScope = new PXTimeTagAttribute.SyncScope();
        action();
      }
      finally
      {
        syncScope?.Dispose();
      }
      result = new PXSyncResult(this.OperationCode, direction, mailbox, id, note, key)
      {
        ActionTitle = title,
        ItemStatus = status
      };
    }
    catch (Exception ex)
    {
      result = new PXSyncResult(this.OperationCode, direction, mailbox, id, note, key, (string) null, ex, (string[]) null)
      {
        ActionTitle = title,
        ItemStatus = status
      };
    }
    if (logOperation)
      this.Provider.LogResult(result);
    return result;
  }

  protected PXSyncResult SafeOperation<ExchangeType>(
    PXExchangeResponce<ExchangeType> item,
    string id,
    Guid? note,
    string key,
    string title,
    PXSyncItemStatus status,
    Action success,
    Func<bool> failed = null)
    where ExchangeType : ItemType, new()
  {
    if ((item != null ? ((PXExchangeItem<ExchangeType>) item).Item?.ItemId : (ItemIdType) null) != null)
      id = ((PXExchangeItem<ExchangeType>) item).Item.ItemId.Id;
    if (item.Success)
      return this.SafeOperation((PXEmailSyncDirection.Directions) 2, ((PXExchangeItemBase) item).Address, id, note, key, title, status, success, true);
    PXSyncResult pxSyncResult1 = (PXSyncResult) null;
    bool bypassUnsuccessfull = false;
    if (failed != null)
      pxSyncResult1 = this.SafeOperation((PXEmailSyncDirection.Directions) 2, ((PXExchangeItemBase) item).Address, id, note, key, title, status, (Action) (() => bypassUnsuccessfull = failed()), false);
    PXSyncResult pxSyncResult2;
    if (!bypassUnsuccessfull)
      pxSyncResult2 = new PXSyncResult(this.OperationCode, (PXEmailSyncDirection.Directions) 2, ((PXExchangeItemBase) item).Address, id, note, key, item.Message, (Exception) null, item.Details)
      {
        ActionTitle = title,
        ItemStatus = status
      };
    else
      pxSyncResult2 = pxSyncResult1;
    PXSyncResult result = pxSyncResult2;
    if (!bypassUnsuccessfull)
      this.Provider.LogResult(result);
    return result;
  }

  protected string ColumnByLambda<T, V>(Expression<Func<T, V>> exp)
  {
    string str = string.Empty;
    if (exp.Body is MemberExpression)
      str = ((MemberExpression) exp.Body).Member.Name;
    if (exp.Body is UnaryExpression)
      str = ((MemberExpression) ((UnaryExpression) exp.Body).Operand).Member.Name;
    return str;
  }

  protected void MergeErrors(Dictionary<string, List<string>> dictionary)
  {
    foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
    {
      List<string> stringList;
      if (!this.Errors.TryGetValue(keyValuePair.Key, out stringList))
        this.Errors[keyValuePair.Key] = stringList = new List<string>();
      stringList.AddRange((IEnumerable<string>) keyValuePair.Value);
    }
  }

  protected void StoreError(string address, Exception error)
  {
    this.StoreError(address, error.ToString());
  }

  protected void StoreError(string address, string error)
  {
    List<string> stringList;
    if (!this.Errors.TryGetValue(address, out stringList))
      this.Errors[address] = stringList = new List<string>();
    stringList.Add(error);
  }

  protected bool HasErrors(string address) => this.Errors.ContainsKey(address);

  protected virtual void ExportInsertedItemProperty<T>(
    Expression<Func<T, object>> exp,
    T item,
    object value,
    string exchTimezone = null,
    bool isAllDay = false)
    where T : ItemType, new()
  {
    if (value == null)
      return;
    Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo> gang = this.Cache.FieldsCache(typeof (T), this.ColumnByLambda<T, object>(exp));
    if (value is DateTime)
    {
      PXTimeZoneInfo systemTimeZoneById = PXTimeZoneInfo.FindSystemTimeZoneById(exchTimezone);
      if (isAllDay)
        value = (object) PXTimeZoneInfo.ConvertTimeToUtc((DateTime) value, systemTimeZoneById ?? LocaleInfo.GetTimeZone());
      if (((Gang<PropertyInfo, PropertyInfo, PropertyInfo>) gang).Item3 != (PropertyInfo) null)
      {
        TimeZoneDefinitionType timeZone = systemTimeZoneById == null ? (TimeZoneDefinitionType) null : this.Gate.GetTimeZone(systemTimeZoneById.RegionId);
        if (timeZone != null)
        {
          value = (object) PXTimeZoneInfo.ConvertTimeFromUtc((DateTime) value, systemTimeZoneById);
          ((Gang<PropertyInfo, PropertyInfo, PropertyInfo>) gang).Item3.SetValue((object) item, (object) timeZone);
        }
      }
    }
    if (((Gang<PropertyInfo>) gang).Item1 != (PropertyInfo) null)
      ((Gang<PropertyInfo>) gang).Item1.SetValue((object) item, value);
    if (!(((Gang<PropertyInfo, PropertyInfo>) gang).Item2 != (PropertyInfo) null))
      return;
    ((Gang<PropertyInfo, PropertyInfo>) gang).Item2.SetValue((object) item, (object) true);
  }

  protected virtual void ExportInsertedItemPropertyConditional<T>(
    Expression<Func<T, object>> exp,
    T item,
    object value,
    object condition)
    where T : ItemType, new()
  {
    if (condition == null)
      return;
    this.ExportInsertedItemProperty<T>(exp, item, value);
  }

  protected virtual IEnumerable<SetItemFieldType> ExportUpdatedItemProperty<T>(
    Expression<Func<T, object>> exp,
    UnindexedFieldURIType uri,
    object value,
    string exchTimezone = null,
    bool isAllDay = false)
    where T : ItemType, new()
  {
    Expression<Func<T, object>> exp1 = exp;
    PathToUnindexedFieldType uriPath = new PathToUnindexedFieldType();
    uriPath.FieldURI = uri;
    object obj = value;
    string exchTimezone1 = exchTimezone;
    int num = isAllDay ? 1 : 0;
    return this.ExportUpdatedItemProperty<T>(exp1, (BasePathToElementType) uriPath, obj, exchTimezone1, num != 0);
  }

  protected virtual IEnumerable<SetItemFieldType> ExportUpdatedItemPropertyConditional<T>(
    Expression<Func<T, object>> exp,
    UnindexedFieldURIType uri,
    object value,
    object condition)
    where T : ItemType, new()
  {
    if (condition == null)
      return (IEnumerable<SetItemFieldType>) null;
    Expression<Func<T, object>> exp1 = exp;
    PathToUnindexedFieldType uriPath = new PathToUnindexedFieldType();
    uriPath.FieldURI = uri;
    object obj = value;
    return this.ExportUpdatedItemProperty<T>(exp1, (BasePathToElementType) uriPath, obj);
  }

  protected virtual IEnumerable<SetItemFieldType> ExportUpdatedItemProperty<T>(
    Expression<Func<T, object>> exp,
    DictionaryURIType uri,
    string fieldIndex,
    object value,
    object condition)
    where T : ItemType, new()
  {
    if (condition == null)
      return (IEnumerable<SetItemFieldType>) null;
    Expression<Func<T, object>> exp1 = exp;
    PathToIndexedFieldType uriPath = new PathToIndexedFieldType();
    uriPath.FieldURI = uri;
    uriPath.FieldIndex = fieldIndex;
    object obj = value;
    return this.ExportUpdatedItemProperty<T>(exp1, (BasePathToElementType) uriPath, obj);
  }

  protected virtual IEnumerable<SetItemFieldType> ExportUpdatedItemProperty<T>(
    Expression<Func<T, object>> exp,
    string tag,
    MapiPropertyTypeType type,
    object value,
    object condition)
    where T : ItemType, new()
  {
    if (condition == null)
      return (IEnumerable<SetItemFieldType>) null;
    Expression<Func<T, object>> exp1 = exp;
    PathToExtendedFieldType uriPath = new PathToExtendedFieldType();
    uriPath.PropertyTag = tag;
    uriPath.PropertyType = type;
    object obj = value;
    return this.ExportUpdatedItemProperty<T>(exp1, (BasePathToElementType) uriPath, obj);
  }

  protected virtual IEnumerable<SetItemFieldType> ExportUpdatedItemProperty<T>(
    Expression<Func<T, object>> exp,
    string tag,
    MapiPropertyTypeType type,
    object value)
    where T : ItemType, new()
  {
    if (value == null)
      return (IEnumerable<SetItemFieldType>) null;
    Expression<Func<T, object>> exp1 = exp;
    PathToExtendedFieldType uriPath = new PathToExtendedFieldType();
    uriPath.PropertyTag = tag;
    uriPath.PropertyType = type;
    ExtendedPropertyType[] extendedProperties = PXExchangeConversionHelper.GetExtendedProperties(Tuple.Create<string, MapiPropertyTypeType, object>(tag, type, value));
    return this.ExportUpdatedItemProperty<T>(exp1, (BasePathToElementType) uriPath, (object) extendedProperties);
  }

  protected virtual IEnumerable<SetItemFieldType> ExportUpdatedItemProperty<T>(
    Expression<Func<T, object>> exp,
    BasePathToElementType uriPath,
    object value,
    string exchTimezone = null,
    bool isAllDay = false)
    where T : ItemType, new()
  {
    if (value != null)
    {
      T item = new T();
      Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo> fieldInfo = this.Cache.FieldsCache(typeof (T), this.ColumnByLambda<T, object>(exp));
      if (value is DateTime)
      {
        PXTimeZoneInfo systemTimeZoneById = PXTimeZoneInfo.FindSystemTimeZoneById(exchTimezone);
        if (isAllDay)
          value = (object) PXTimeZoneInfo.ConvertTimeToUtc((DateTime) value, systemTimeZoneById ?? LocaleInfo.GetTimeZone());
        if (((Gang<PropertyInfo, PropertyInfo, PropertyInfo>) fieldInfo).Item3 != (PropertyInfo) null)
        {
          TimeZoneDefinitionType timeZone = systemTimeZoneById == null ? (TimeZoneDefinitionType) null : this.Gate.GetTimeZone(systemTimeZoneById.RegionId);
          if (timeZone != null)
          {
            value = (object) PXTimeZoneInfo.ConvertTimeFromUtc((DateTime) value, systemTimeZoneById);
            UnindexedFieldURIType result;
            if (Enum.TryParse<UnindexedFieldURIType>((uriPath as PathToUnindexedFieldType).FieldURI.ToString() + "TimeZone", out result))
            {
              T obj = new T();
              ((Gang<PropertyInfo, PropertyInfo, PropertyInfo>) fieldInfo).Item3.SetValue((object) obj, (object) timeZone);
              SetItemFieldType setItemFieldType = new SetItemFieldType();
              ((ChangeDescriptionType) setItemFieldType).Item = (BasePathToElementType) new PathToUnindexedFieldType()
              {
                FieldURI = result
              };
              setItemFieldType.Item1 = (ItemType) obj;
              yield return setItemFieldType;
            }
          }
        }
      }
      if (((Gang<PropertyInfo>) fieldInfo).Item1 != (PropertyInfo) null)
        ((Gang<PropertyInfo>) fieldInfo).Item1.SetValue((object) item, value);
      if (((Gang<PropertyInfo, PropertyInfo>) fieldInfo).Item2 != (PropertyInfo) null)
        ((Gang<PropertyInfo, PropertyInfo>) fieldInfo).Item2.SetValue((object) item, (object) true);
      SetItemFieldType setItemFieldType1 = new SetItemFieldType();
      ((ChangeDescriptionType) setItemFieldType1).Item = uriPath;
      setItemFieldType1.Item1 = (ItemType) item;
      yield return setItemFieldType1;
    }
  }

  protected virtual void ImportItemProperty<T, V>(
    Expression<Func<T, V>> exp,
    T item,
    Action<V> setter,
    bool merge = false,
    string exchTimezone = null,
    bool isAllDay = false)
    where T : ItemType, new()
  {
    Gang<PropertyInfo, PropertyInfo, PropertyInfo, PropertyInfo> gang = this.Cache.FieldsCache(typeof (T), this.ColumnByLambda<T, V>(exp));
    if (((Gang<PropertyInfo, PropertyInfo>) gang).Item2 != (PropertyInfo) null && !(bool) ((Gang<PropertyInfo, PropertyInfo>) gang).Item2.GetValue((object) item))
      return;
    object obj = ((Gang<PropertyInfo>) gang).Item1?.GetValue((object) item);
    if (obj == null & merge)
      return;
    if (obj is DateTime & isAllDay)
    {
      PXTimeZoneInfo systemTimeZoneById = PXTimeZoneInfo.FindSystemTimeZoneById(exchTimezone);
      obj = (object) PXTimeZoneInfo.ConvertTimeFromUtc((DateTime) obj, systemTimeZoneById ?? LocaleInfo.GetTimeZone());
    }
    setter((V) obj);
  }

  protected virtual IEnumerable<DeleteItemFieldType> DeleteItemProperty<T>(
    Expression<Func<T, object>> exp,
    UnindexedFieldURIType uri)
    where T : ItemType, new()
  {
    return this.DeleteItemProperty<T>(exp, (BasePathToElementType) new PathToUnindexedFieldType()
    {
      FieldURI = uri
    });
  }

  protected virtual IEnumerable<DeleteItemFieldType> DeleteItemProperty<T>(
    Expression<Func<T, object>> exp,
    DictionaryURIType uri,
    string fieldIndex)
    where T : ItemType, new()
  {
    return this.DeleteItemProperty<T>(exp, (BasePathToElementType) new PathToIndexedFieldType()
    {
      FieldURI = uri,
      FieldIndex = fieldIndex
    });
  }

  protected virtual IEnumerable<DeleteItemFieldType> DeleteItemProperty<T>(
    Expression<Func<T, object>> exp,
    BasePathToElementType uriPath)
    where T : ItemType, new()
  {
    DeleteItemFieldType deleteItemFieldType = new DeleteItemFieldType();
    ((ChangeDescriptionType) deleteItemFieldType).Item = uriPath;
    yield return deleteItemFieldType;
  }
}
