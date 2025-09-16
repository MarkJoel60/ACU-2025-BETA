// Decompiled with JetBrains decompiler
// Type: PX.Data.EntityHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

public class EntityHelper
{
  protected readonly PXGraph graph;

  public PXGraph Graph => this.graph;

  public EntityHelper(PXGraph graph) => this.graph = graph;

  public string GetFriendlyEntityName(IBqlTable entity)
  {
    return entity != null ? EntityHelper.GetFriendlyEntityName(entity.GetType(), (object) entity) : (string) null;
  }

  public string GetFriendlyEntityName(Guid? noteID)
  {
    object entityRow = this.GetEntityRow(noteID);
    return entityRow != null ? EntityHelper.GetFriendlyEntityName(entityRow.GetType(), entityRow) : (string) null;
  }

  public static string GetFriendlyEntityName(System.Type entityType, object row)
  {
    if (entityType == (System.Type) null)
      return (string) null;
    return entityType.IsDefined(typeof (PXCacheNameAttribute), true) ? entityType.GetCustomAttributes<PXCacheNameAttribute>(true).FirstOrDefault<PXCacheNameAttribute>()?.GetName(row) ?? entityType.FullName : entityType.FullName;
  }

  public static string GetFriendlyEntityName(System.Type entityType)
  {
    return EntityHelper.GetFriendlyEntityName(entityType, (object) null);
  }

  public static string GetFriendlyEntityName<TEntity>() where TEntity : IBqlTable
  {
    return EntityHelper.GetFriendlyEntityName(typeof (TEntity));
  }

  public string GetEntityDescription(Guid? noteID, System.Type entityType)
  {
    return this.GetEntityDescription(noteID, entityType, out object _);
  }

  internal string GetEntityDescription(Guid? noteID, System.Type entityType, out object row)
  {
    Note note = this.GetNote(noteID, false);
    if (note != null)
      entityType = PXBuildManager.GetType(note.EntityType, false);
    row = this.GetEntityRow(entityType, noteID);
    return EntityHelper.GetEntityDescription(this.graph, row);
  }

  public static System.Type GetPrimaryGraphType(PXGraph graph, System.Type type)
  {
    EntityHelper entityHelper = new EntityHelper(graph);
    PXCache cach = graph.Caches[type];
    object obj = cach.Current;
    if (obj == null)
    {
      obj = cach.Insert(cach.CreateInstance());
      cach.Clear();
    }
    cach.IsDirty = false;
    System.Type cacheType = type;
    object row = obj;
    return entityHelper.GetPrimaryGraphType(cacheType, row, false);
  }

  public static string GetEntityDescription(PXGraph graph, object data)
  {
    if (data == null)
      return (string) null;
    PXCache cache = graph.Caches[data.GetType()];
    data = EntityHelper.CorrectEntityType(cache, data.GetType(), data);
    List<string> properties = cache.Fields.Where<string>((Func<string, bool>) (field => cache.GetAttributesReadonly(field).OfType<PXFieldDescriptionAttribute>().Any<PXFieldDescriptionAttribute>())).ToList<string>();
    IEnumerable<string> collection = ((IEnumerable<System.Type>) cache.GetExtensionTypes()).SelectMany((Func<System.Type, IEnumerable<PropertyInfo>>) (extension => (IEnumerable<PropertyInfo>) extension.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)), (extension, property) => new
    {
      extension = extension,
      property = property
    }).Where(_param1 => Attribute.IsDefined((MemberInfo) _param1.property, typeof (PXFieldDescriptionAttribute), true) && !properties.Contains(_param1.property.Name)).Select(_param1 => _param1.property.Name);
    properties.AddRange(collection);
    StringBuilder stringBuilder = new StringBuilder(properties.Count * 16 /*0x10*/);
    foreach (string fieldName in properties)
    {
      if (!(cache.GetStateExt(data, fieldName + "_Description") is PXFieldState stateExt))
        stateExt = cache.GetStateExt(data, fieldName) as PXFieldState;
      string str = EntityHelper.GetFieldString(stateExt)?.ToString();
      if (!string.IsNullOrWhiteSpace(str))
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(", ");
        stringBuilder.Append(str);
      }
    }
    return stringBuilder.ToString();
  }

  public object[] GetEntityRowKeys(System.Type entityType, object e)
  {
    PXCache cach = this.graph.Caches[entityType];
    e = EntityHelper.CorrectEntityType(cach, entityType, e);
    List<object> objectList = new List<object>();
    foreach (System.Type bqlKey in cach.BqlKeys)
      objectList.Add(cach.GetValue(e, bqlKey.Name));
    return objectList.ToArray();
  }

  public string GetEntityKeysDescription(IBqlTable entity, string separator = null)
  {
    if (entity == null)
      return string.Empty;
    separator = separator ?? ", ";
    PXCache entityCache = this.Graph.Caches[entity.GetType()];
    IEnumerable<string> values = entityCache.Keys.Select<string, string>((Func<string, string>) (keyField => this.GetKeyFieldDescription(entityCache, entity, keyField, separator))).Where<string>((Func<string, bool>) (description => !string.IsNullOrWhiteSpace(description)));
    return string.Join(separator, values);
  }

  public string GetKeyFieldDescription(
    PXCache entityCache,
    IBqlTable entity,
    string keyField,
    string descriptionSeparator = null)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(keyField, nameof (keyField), (string) null);
    descriptionSeparator = descriptionSeparator ?? " - ";
    System.Type type = ExceptionExtensions.CheckIfNull<IBqlTable>(entity, nameof (entity), (string) null).GetType();
    object obj = ExceptionExtensions.CheckIfNull<PXCache>(entityCache, nameof (entityCache), (string) null).GetValue((object) entity, keyField);
    switch (obj)
    {
      case null:
      case Array _:
        return (string) null;
      default:
        string fieldString = this.GetFieldString((object) entity, type, keyField);
        if (string.IsNullOrWhiteSpace(fieldString))
          return (string) null;
        PXSelectorAttribute selectorAttribute = entityCache.GetAttributesReadonly(keyField, true).OfType<PXSelectorAttribute>().FirstOrDefault<PXSelectorAttribute>();
        if (selectorAttribute == null)
          return !(obj is string) && string.Equals(obj.ToString(), fieldString, StringComparison.OrdinalIgnoreCase) ? (string) null : fieldString;
        string fieldDescription = (string) null;
        if (selectorAttribute.SubstituteKey != (System.Type) null || obj is string || !string.Equals(obj.ToString(), fieldString, StringComparison.OrdinalIgnoreCase))
          fieldDescription = fieldString;
        if (selectorAttribute.DescriptionField == (System.Type) null)
          return fieldDescription;
        string str = (string) null;
        if (type == selectorAttribute.DescriptionField.DeclaringType)
          str = this.GetFieldString((object) entity, type, selectorAttribute.DescriptionField.Name);
        string a = str ?? this.GetFieldString((object) entity, type, keyField, true);
        if (!string.IsNullOrWhiteSpace(a) && !string.Equals(a, fieldString, StringComparison.CurrentCulture))
          fieldDescription = fieldDescription == null ? a : fieldDescription + descriptionSeparator + a;
        return fieldDescription;
    }
  }

  public string GetRowID<TEntity>(TEntity row, string separator = ", ") where TEntity : IBqlTable
  {
    return this.GetEntityRowID(typeof (TEntity), (object) row, separator);
  }

  public string GetEntityRowID(Guid? noteID, string separator = ", ")
  {
    object entityRow = this.GetEntityRow(noteID);
    return entityRow == null ? string.Empty : this.GetEntityRowID(entityRow.GetType(), entityRow, separator);
  }

  public string GetEntityRowID(System.Type entityType, object e, string separator)
  {
    PXCache cach = this.graph.Caches[entityType];
    e = EntityHelper.CorrectEntityType(cach, entityType, e);
    string entityRowId = (string) null;
    if (separator != null)
    {
      foreach (System.Type bqlKey in cach.BqlKeys)
        entityRowId = entityRowId + (entityRowId == null ? string.Empty : separator) + this.GetFieldString(e, entityType, bqlKey.Name);
    }
    else
    {
      System.Type type = cach.BqlKeys.Last<System.Type>();
      entityRowId = this.GetFieldString(e, entityType, type.Name);
    }
    return entityRowId;
  }

  private static object CorrectEntityType(PXCache cache, System.Type entityType, object entity)
  {
    if (entity != null && cache.GetItemType().IsSubclassOf(entityType))
      entity = cache.ToChildEntity(entityType, entity);
    return entity;
  }

  public string GetEntityRowValues(System.Type entityType, object[] key)
  {
    object entityRow = this.GetEntityRow(entityType, key);
    return entityRow == null ? string.Empty : this.DescriptionEntity(entityType, entityRow);
  }

  public string GetEntityRowValues(System.Type entityType, Guid noteID)
  {
    if (string.IsNullOrEmpty(EntityHelper.GetNoteField(entityType)))
      return string.Empty;
    object entityRow = this.GetEntityRow(entityType, new Guid?(noteID));
    return entityRow == null ? string.Empty : this.DescriptionEntity(entityType, entityRow);
  }

  public string GetEntityRowValues(Guid? noteID)
  {
    object entityRow = this.GetEntityRow(noteID);
    return entityRow == null ? string.Empty : this.DescriptionEntity(entityRow.GetType(), entityRow);
  }

  public Guid? GetEntityNoteID(object row) => this.GetEntityNoteID(row, false);

  public Guid? GetEntityNoteID(object row, bool persistRow)
  {
    if (row == null)
      return new Guid?();
    string noteField = EntityHelper.GetNoteField(row.GetType());
    PXCache cach = this.graph.Caches[row.GetType()];
    row = EntityHelper.CorrectEntityType(cach, row.GetType(), row);
    Guid? entityNoteId = persistRow ? PXNoteAttribute.GetNoteID(cach, row, noteField) : (Guid?) cach.GetValue(row, noteField);
    if (!persistRow || cach.GetStatus(row) != PXEntryStatus.Updated)
      return entityNoteId;
    cach.PersistUpdated(row);
    return entityNoteId;
  }

  internal static Guid? GetEntityNoteID(PXCache dacCache, IBqlTable dac)
  {
    ExceptionExtensions.ThrowOnNull<IBqlTable>(dac, nameof (dac), (string) null);
    if (dac is INotable notable)
      return notable.NoteID;
    int fieldOrdinal = ExceptionExtensions.CheckIfNull<PXCache>(dacCache, nameof (dacCache), (string) null).GetFieldOrdinal("NoteID");
    if (fieldOrdinal >= 0)
      return dacCache.GetValue((object) dac, fieldOrdinal) as Guid?;
    PXNoteAttribute pxNoteAttribute = dacCache.GetAttributesOfType<PXNoteAttribute>((object) dac, (string) null).FirstOrDefault<PXNoteAttribute>();
    return pxNoteAttribute == null ? new Guid?() : dacCache.GetValue((object) dac, pxNoteAttribute.FieldOrdinal) as Guid?;
  }

  /// <summary>
  /// Get NoteID for entity (field marked with <see cref="T:PX.Data.PXNoteAttribute" />. If NoteID is not exist in database create it, persist it.
  /// If entity <see cref="T:PX.Data.Note" /> doesn't exist create it and persist it.
  /// </summary>
  /// <returns>NoteID for entity.</returns>
  /// <remarks>
  /// Do the same as <see cref="M:PX.Data.EntityHelper.GetEntityNoteID(System.Object,System.Boolean)" /> with persistRow = <see langword="true" /> but also persist create Note for it.
  /// </remarks>
  public Guid? GetNoteIDAndEnsureNoteExists(object row)
  {
    Guid? noteID = this.GetEntityNoteID(row, true);
    if (!noteID.HasValue)
      return new Guid?();
    PXCache<Note> pxCache = this.graph.Caches<Note>();
    Note row1 = pxCache.Inserted.RowCast<Note>().FirstOrDefault<Note>((Func<Note, bool>) (n =>
    {
      Guid? noteId = n.NoteID;
      Guid? nullable = noteID;
      if (noteId.HasValue != nullable.HasValue)
        return false;
      return !noteId.HasValue || noteId.GetValueOrDefault() == nullable.GetValueOrDefault();
    }));
    if (row1 != null)
      pxCache.PersistInserted((object) row1);
    return noteID;
  }

  public string DescriptionEntity(System.Type entityType, object row)
  {
    if (row == null)
      return string.Empty;
    Set<string> fieldsNames = this.GetFieldsNames(entityType);
    StringBuilder stringBuilder = new StringBuilder();
    if (((KList<string, string>) fieldsNames).Count == 0)
      return string.Empty;
    PXCache cach = this.graph.Caches[entityType];
    row = EntityHelper.CorrectEntityType(cach, entityType, row);
    foreach (string fieldName in (KList<string, string>) fieldsNames)
    {
      try
      {
        object obj = cach.GetStateExt(row, fieldName);
        if (obj is PXFieldState pxFieldState)
        {
          if (pxFieldState.Value != null)
          {
            switch (obj)
            {
              case PXStringState pxStringState when pxStringState.AllowedValues != null:
                int index1 = Array.IndexOf<object>((object[]) pxStringState.AllowedValues, pxFieldState.Value);
                obj = index1 < 0 || pxStringState.AllowedLabels == null || index1 >= pxStringState.AllowedLabels.Length ? pxFieldState.Value : (object) pxStringState.AllowedLabels[index1];
                break;
              case PXIntState pxIntState when pxIntState.AllowedValues != null:
                int index2 = Array.IndexOf((Array) pxIntState.AllowedValues, pxFieldState.Value);
                obj = index2 < 0 || pxIntState.AllowedLabels == null || index2 >= pxIntState.AllowedLabels.Length ? pxFieldState.Value : (object) pxIntState.AllowedLabels[index2];
                break;
              default:
                obj = pxFieldState.Value;
                break;
            }
          }
          else
            obj = (object) null;
        }
        if (obj != null)
          stringBuilder.Append(obj?.ToString() + ", ");
      }
      catch
      {
      }
    }
    if (stringBuilder.Length >= 2)
      stringBuilder.Remove(stringBuilder.Length - 2, 2);
    return stringBuilder.ToString();
  }

  private Note GetNote(Guid? noteID, bool isReadonly)
  {
    return new PXView(this.graph, isReadonly, (BqlCommand) new Select<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>()).SelectSingle((object) noteID) as Note;
  }

  private Note GetNote(Guid? noteID) => this.GetNote(noteID, true);

  public string[] GetFieldList(System.Type entityType)
  {
    Set<string> fieldsNames = this.GetFieldsNames(entityType);
    string[] fieldList = new string[((KList<string, string>) fieldsNames).Count];
    ((KList<string, string>) fieldsNames).CopyTo(fieldList, 0);
    return fieldList;
  }

  private Set<string> GetFieldsNames(System.Type entityType)
  {
    PXCache cach = this.graph.Caches[entityType];
    Set<string> fieldsNames = new Set<string>(true);
    foreach (PXEventSubscriberAttribute attribute in cach.GetAttributes((string) null))
    {
      if (attribute is IPXInterfaceField pxInterfaceField && (pxInterfaceField.Visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible)
        ((KList<string, string>) fieldsNames).Add(attribute.FieldName);
    }
    if (((KList<string, string>) fieldsNames).Count == 0)
    {
      for (int index = 0; index < cach.Keys.Count; ++index)
        ((KList<string, string>) fieldsNames).Add(cach.Keys[index]);
    }
    return fieldsNames;
  }

  public static string GetNoteField(System.Type entityType)
  {
    foreach (PropertyInfo property in entityType.GetProperties())
    {
      if (property.IsDefined(typeof (PXNoteAttribute), true))
      {
        char ch = property.Name[0];
        ch = ch.ToString().ToLower()[0];
        string noteField = ch.ToString();
        if (property.Name.Length > 1)
          noteField += property.Name.Substring(1);
        return noteField;
      }
    }
    return (string) null;
  }

  public static PXNoteAttribute GetNoteAttribute(System.Type entityType, bool isInherit = true)
  {
    foreach (PropertyInfo property in entityType.GetProperties())
    {
      if (property.IsDefined(typeof (PXNoteAttribute), isInherit))
        return Attribute.GetCustomAttribute((MemberInfo) property, typeof (PXNoteAttribute), isInherit) as PXNoteAttribute;
    }
    return (PXNoteAttribute) null;
  }

  public static System.Type GetNoteType(System.Type entityType)
  {
    string noteField = EntityHelper.GetNoteField(entityType);
    if (noteField == null)
      return (System.Type) null;
    for (; entityType != (System.Type) null; entityType = entityType.BaseType)
    {
      System.Type nestedType = entityType.GetNestedType(noteField);
      if (nestedType != (System.Type) null)
        return nestedType;
    }
    return (System.Type) null;
  }

  [Obsolete]
  public static string GetIDField(System.Type entityType)
  {
    foreach (PropertyInfo property in entityType.GetProperties())
    {
      if (property.IsDefined(typeof (PXDBIdentityAttribute), true) || property.Name == "BAccountID")
      {
        char ch = property.Name[0];
        ch = ch.ToString().ToLower()[0];
        string idField = ch.ToString();
        if (property.Name.Length > 1)
          idField += property.Name.Substring(1);
        return idField;
      }
    }
    return (string) null;
  }

  public static string GetIDField(PXCache cache)
  {
    foreach (string field in (List<string>) cache.Fields)
    {
      if (cache.GetAttributesReadonly((object) null, field).OfType<PXDBIdentityAttribute>().Any<PXDBIdentityAttribute>() || field == "BAccountID")
      {
        string idField = field.ToLower()[0].ToString();
        if (field.Length > 1)
          idField += field.Substring(1);
        return idField;
      }
    }
    return (string) null;
  }

  internal static EntityHelper GetHelperForEntity(Guid? noteID, PXGraph cachingGraph)
  {
    if (noteID.HasValue)
    {
      Guid? nullable = noteID;
      Guid empty = Guid.Empty;
      if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) == 0)
      {
        Note note = new EntityHelper(cachingGraph).SelectNote(noteID);
        if (note == null || note.GraphType == null)
          return (EntityHelper) null;
        System.Type type = GraphHelper.GetType(note.GraphType);
        if (type == (System.Type) null)
          return (EntityHelper) null;
        PXGraph instance = PXGraph.CreateInstance(type);
        return instance == null ? (EntityHelper) null : new EntityHelper(instance);
      }
    }
    return (EntityHelper) null;
  }

  public void NavigateToRow(Guid? noteID)
  {
    this.NavigateToRow(noteID, PXRedirectHelper.WindowMode.Popup);
  }

  public void NavigateToRow(Guid? noteID, PXRedirectHelper.WindowMode mode)
  {
    Note note = this.SelectNote(noteID);
    if (note == null || string.IsNullOrEmpty(note.EntityType))
      return;
    this.NavigateToRow(note.EntityType, noteID, mode);
  }

  public void NavigateToRow(string entityType, Guid? noteID)
  {
    this.NavigateToRow(entityType, noteID, PXRedirectHelper.WindowMode.Popup);
  }

  public void NavigateToRow(string entityType, Guid? noteID, PXRedirectHelper.WindowMode mode)
  {
    System.Type type = PXBuildManager.GetType(entityType, false);
    object entityRow1 = this.GetEntityRow(type, noteID);
    if (entityRow1 != null)
    {
      this.NavigateToRow(type, entityRow1, mode);
    }
    else
    {
      PXCache cach = this.graph.Caches[type];
      if (!cach.DisableReadItem || !(cach.Interceptor is PXProjectionAttribute) || string.IsNullOrEmpty(cach._NoteIDName))
        return;
      PXNoteAttribute pxNoteAttribute = cach.GetAttributesReadonly(cach._NoteIDName).OfType<PXNoteAttribute>().FirstOrDefault<PXNoteAttribute>();
      if (!(pxNoteAttribute?.BqlTable != (System.Type) null))
        return;
      object entityRow2 = this.GetEntityRow(pxNoteAttribute.BqlTable, noteID);
      this.NavigateToRow(pxNoteAttribute.BqlTable, entityRow2, mode);
    }
  }

  public void NavigateToRow(string entityType, object[] keys)
  {
    this.NavigateToRow(entityType, keys, PXRedirectHelper.WindowMode.Popup);
  }

  public void NavigateToRow(string entityType, object[] keys, PXRedirectHelper.WindowMode mode)
  {
    System.Type type = PXBuildManager.GetType(entityType, false);
    this.NavigateToRow(type, this.GetEntityRow(type, keys), mode);
  }

  protected virtual void NavigateToRow(
    System.Type cachetype,
    object row,
    PXRedirectHelper.WindowMode mode)
  {
    if (row == null)
      return;
    PXCache cach = this.graph.Caches[cachetype];
    row = EntityHelper.CorrectEntityType(cach, cachetype, row);
    string message = "View Entity";
    using (cach.IsArchived(row) ? new PXReadThroughArchivedScope() : (PXReadThroughArchivedScope) null)
      PXRedirectHelper.TryRedirect(cach, row, message, mode);
  }

  public object GetEntityRow(Guid? noteID) => this.GetEntityRow(noteID, false);

  public System.Type GetEntityRowType(Guid? noteID) => this.GetEntityRowType(noteID, false);

  public System.Type GetEntityRowType(Guid? noteID, bool suspendTypeCalculation)
  {
    Note note = this.SelectNote(noteID);
    if (note == null || string.IsNullOrEmpty(note.EntityType))
      return (System.Type) null;
    System.Type entityType = PXBuildManager.GetType(note.EntityType, false);
    if (!suspendTypeCalculation)
      entityType = EntityHelper.GetNoteFieldDAC(entityType, false);
    return entityType;
  }

  public object GetEntityRow(Guid? noteID, bool suspendTypeCalculation)
  {
    System.Type entityRowType = this.GetEntityRowType(noteID, suspendTypeCalculation);
    return !(entityRowType != (System.Type) null) ? (object) null : this.GetEntityRow(entityRowType, noteID);
  }

  private static System.Type GetNoteFieldDAC(System.Type entityType, bool inhereted)
  {
    System.Type c = entityType;
    System.Type noteFieldDac = (System.Type) null;
    for (; c != (System.Type) null && typeof (IBqlTable).IsAssignableFrom(c) && c != typeof (object); c = c.BaseType)
    {
      foreach (MemberInfo property in entityType.GetProperties())
      {
        if (property.IsDefined(typeof (PXNoteAttribute), true))
        {
          noteFieldDac = c;
          if (inhereted)
            return noteFieldDac;
        }
      }
    }
    return noteFieldDac;
  }

  public object GetEntityRow(string entityTypeFullName, Guid noteID)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(entityTypeFullName, nameof (entityTypeFullName), (string) null);
    return this.GetEntityRow(PXBuildManager.GetType(entityTypeFullName, false), new Guid?(noteID));
  }

  public virtual object GetEntityRow(System.Type entityType, Guid? noteID)
  {
    return this.GetEntityRow(entityType, noteID, true);
  }

  public virtual object GetEntityRow(System.Type entityType, Guid? noteID, bool isReadonly)
  {
    if (entityType == (System.Type) null)
      return (object) null;
    string key = $"_{entityType.FullName}_noteID_";
    string noteField = EntityHelper.GetNoteField(entityType);
    if (string.IsNullOrEmpty(noteField))
      return (object) null;
    System.Type noteFieldType = (System.Type) null;
    for (System.Type type = entityType; noteFieldType == (System.Type) null && type != typeof (object); type = type.BaseType)
      noteFieldType = type.GetNestedType(noteField);
    if (noteFieldType == (System.Type) null)
      return (object) null;
    PXCache cache = this.graph.Caches[entityType];
    object entityRow = cache.Cached.Cast<object>().FirstOrDefault<object>((Func<object, bool>) (_ =>
    {
      Guid? nullable1 = cache.GetValue(_, noteFieldType.Name) as Guid?;
      Guid? nullable2 = noteID;
      if (nullable1.HasValue != nullable2.HasValue)
        return false;
      return !nullable1.HasValue || nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault();
    }));
    if (entityRow != null)
      return entityRow;
    PXView pxView;
    if (!this.graph.Views.TryGetValue(key, out pxView))
    {
      System.Type type1 = BqlCommand.Compose(typeof (Equal<>), BqlCommand.Compose(typeof (Required<>), noteFieldType));
      System.Type type2 = BqlCommand.Compose(typeof (Where<,>), noteFieldType, type1);
      BqlCommand instance = BqlCommand.CreateInstance(typeof (Select<,>), entityType, type2);
      pxView = new PXView(this.graph, isReadonly, instance);
      this.graph.Views.Add(key, pxView);
    }
    using (new PXReadThroughArchivedScope())
      return pxView.SelectSingle((object) noteID);
  }

  public virtual object GetEntityRowByID(System.Type entityType, int? id)
  {
    if (entityType == (System.Type) null)
      return (object) null;
    string key = $"_{entityType.FullName}_entityID_";
    string idField = EntityHelper.GetIDField(entityType);
    if (string.IsNullOrEmpty(idField))
      return (object) null;
    System.Type type1 = (System.Type) null;
    for (System.Type type2 = entityType; type1 == (System.Type) null && type2 != typeof (object); type2 = type2.BaseType)
      type1 = type2.GetNestedType(idField);
    if (type1 == (System.Type) null)
      return (object) null;
    PXView pxView;
    if (!this.graph.Views.TryGetValue(key, out pxView))
    {
      System.Type type3 = BqlCommand.Compose(typeof (Equal<>), BqlCommand.Compose(typeof (Required<>), type1));
      System.Type type4 = BqlCommand.Compose(typeof (Where<,>), type1, type3);
      pxView = new PXView(this.graph, true, BqlCommand.CreateInstance(typeof (Select<,>), entityType, type4));
      this.graph.Views.Add(key, pxView);
    }
    using (new PXReadThroughArchivedScope())
      return pxView.SelectSingle((object) id);
  }

  public object[] GetEntityKey(System.Type entityType, object row)
  {
    PXCache cach = this.graph.Caches[entityType];
    row = EntityHelper.CorrectEntityType(cach, entityType, row);
    object[] entityKey = new object[cach.Keys.Count<string>()];
    for (int index = 0; index < cach.Keys.Count; ++index)
      entityKey[index] = cach.GetValue(row, cach.Keys[index]);
    return entityKey;
  }

  public object GetEntityRow(System.Type entityType, object[] keys)
  {
    if (keys == null)
      return (object) null;
    if (this.graph.Caches[entityType].Keys.Count != keys.Length)
      return (object) null;
    string viewName = this.GetViewName(entityType);
    PXView view = viewName != null ? this.graph.Views[viewName] : (PXView) null;
    using (new PXReadThroughArchivedScope())
      return view?.SelectSingle(keys);
  }

  public object GetSelectorRow(object row, string field, object key)
  {
    if (row == null)
      return (object) null;
    System.Type type = row.GetType();
    PXCache cache = this.graph.Caches[type];
    row = EntityHelper.CorrectEntityType(cache, type, row);
    return cache.GetAttributesReadonly(field).OfType<PXSelectorAttribute>().Select<PXSelectorAttribute, object>((Func<PXSelectorAttribute, object>) (attr =>
    {
      attr.CacheGlobal = false;
      return PXSelectorAttribute.GetItem(cache, attr, row, key, true);
    })).FirstOrDefault<object>();
  }

  public string GetViewName(System.Type entityType)
  {
    string key1 = $"_{entityType.FullName}_key_";
    PXCache cach = this.graph.Caches[entityType];
    if (!this.graph.Views.TryGetValue(key1, out PXView _))
    {
      System.Type type1 = (System.Type) null;
      for (int index = cach.Keys.Count - 1; index >= 0; --index)
      {
        string key2 = cach.Keys[index];
        System.Type type2 = (System.Type) null;
        foreach (System.Type bqlField in cach.BqlFields)
        {
          if (string.Compare(bqlField.Name, key2, StringComparison.OrdinalIgnoreCase) == 0)
          {
            type2 = bqlField;
            break;
          }
        }
        if (type2 != (System.Type) null)
        {
          System.Type type3;
          if (!(type1 != (System.Type) null))
            type3 = BqlCommand.Compose(typeof (Where<,>), type2, typeof (Equal<>), typeof (Required<>), type2);
          else
            type3 = BqlCommand.Compose(typeof (Where<,,>), type2, typeof (Equal<>), typeof (Required<>), type2, typeof (And<>), type1);
          type1 = type3;
        }
      }
      System.Type itemType = cach.GetItemType();
      PXView pxView;
      if (type1 != (System.Type) null)
        pxView = new PXView(this.graph, true, BqlCommand.CreateInstance(typeof (Select<,>), itemType, type1));
      else
        pxView = new PXView(this.graph, true, BqlCommand.CreateInstance(typeof (Select<>), itemType), (Delegate) (() => EntityHelper.SelectSingle(this.graph, itemType)));
      this.graph.Views.Add(key1, pxView);
    }
    return key1;
  }

  private static IEnumerable SelectSingle(PXGraph graph, System.Type itemType)
  {
    PXCache cache = graph.Caches[itemType];
    foreach (object obj in cache.Inserted)
      yield return obj;
    foreach (object obj in cache.Updated)
      yield return obj;
  }

  public object GetFieldExt(object row, string viewName, string fieldName)
  {
    PXFieldState fieldState = this.GetFieldState(row, viewName, fieldName);
    return fieldState != null ? fieldState.Value : this.graph.GetValueExt(viewName, row, fieldName);
  }

  public PXFieldState GetFieldState(object row, string viewName, string fieldName)
  {
    viewName = viewName ?? this.GetViewName(row.GetType());
    return this.graph.GetStateExt(viewName, row, fieldName) as PXFieldState;
  }

  public object GetField(object row, string viewName, string fieldName)
  {
    viewName = viewName ?? this.GetViewName(row.GetType());
    return this.graph.GetValue(viewName, row, fieldName);
  }

  public string GetFieldString(object row, string viewName, string fieldName)
  {
    return this.GetFieldString(row, viewName, fieldName, false);
  }

  public string GetFieldString(object row, System.Type entityType, string fieldName)
  {
    return this.GetFieldString(row, entityType, fieldName, false);
  }

  public virtual string GetFieldString(
    object row,
    System.Type entityType,
    string fieldName,
    bool preferDescription)
  {
    return this.GetFieldString(row, this.GetViewName(entityType), fieldName, preferDescription);
  }

  public string GetFieldString(
    object row,
    string viewName,
    string fieldName,
    bool preferDescription)
  {
    PXFieldState state = (PXFieldState) null;
    if (!string.IsNullOrEmpty(viewName))
    {
      if (preferDescription)
        state = this.graph.GetStateExt(viewName, row, fieldName + "_description") as PXFieldState;
      state = state ?? this.graph.GetStateExt(viewName, row, fieldName) as PXFieldState;
    }
    else if (row != null)
    {
      System.Type type = row.GetType();
      PXCache cach = this.graph.Caches[type];
      row = EntityHelper.CorrectEntityType(cach, type, row);
      if (preferDescription)
        state = cach.GetStateExt(row, fieldName + "_description") as PXFieldState;
      state = state ?? cach.GetStateExt(row, fieldName) as PXFieldState;
    }
    return EntityHelper.GetFieldString(state);
  }

  public static string GetFieldString(PXFieldState state)
  {
    if (state == null || state.Value == null)
      return string.Empty;
    if (state.DataType == typeof (bool))
      return EntityHelper.BoolToString((bool) state.Value);
    if (state.DataType == typeof (string))
      return !(state is PXStringState pxStringState) ? state.Value.ToString() : EntityHelper.FormatString(state.Value.ToString(), pxStringState.InputMask, pxStringState.AllowedValues, pxStringState.AllowedLabels);
    if (state.DataType == typeof (byte) || state.DataType == typeof (short) || state.DataType == typeof (int))
    {
      int int32 = Convert.ToInt32(state.Value);
      return state is PXIntState pxIntState ? EntityHelper.FormatIntegralValue(int32, pxIntState.AllowedValues, pxIntState.AllowedLabels) : EntityHelper.FormatIntegralValue(int32);
    }
    if (state.DataType == typeof (float))
      return EntityHelper.FormatFloatValue((float) state.Value, new int?(state.Precision));
    if (state.DataType == typeof (double))
      return EntityHelper.FormatDoubleValue((double) state.Value, new int?(state.Precision));
    if (state.DataType == typeof (Decimal))
      return EntityHelper.FormatDecimalValue((Decimal) state.Value, new int?(state.Precision));
    if (!(state.DataType == typeof (System.DateTime)))
      return state.Value.ToString();
    return !(state is PXDateState pxDateState) ? EntityHelper.FormatDateTimeValue(state.Value) : EntityHelper.FormatDateTimeValue(pxDateState.Value, pxDateState.DisplayMask);
  }

  private static string BoolToString(bool value) => !value ? "false" : "true";

  private static string FormatFloatValue(float value, int? precision = null)
  {
    return value.ToString("N" + precision.ToString());
  }

  private static string FormatDoubleValue(double value, int? precision = null)
  {
    return value.ToString("N" + precision.ToString());
  }

  private static string FormatDecimalValue(Decimal value, int? precision = null)
  {
    return value.ToString("N" + precision.ToString());
  }

  private static string FormatDateTimeValue(object value, string displayMask = "d")
  {
    return (!(value is System.DateTime dateTime) ? Convert.ToDateTime(value) : dateTime).ToString(displayMask ?? "d");
  }

  private static string FormatString(
    string value,
    string inputMask = null,
    string[] allowedValues = null,
    string[] allowedLabels = null)
  {
    string strA = value;
    if (allowedValues != null && allowedLabels != null)
    {
      int num = System.Math.Min(allowedLabels.Length, allowedValues.Length);
      for (int index = 0; index < num; ++index)
      {
        if (string.Compare(strA, allowedValues[index], StringComparison.OrdinalIgnoreCase) == 0)
        {
          strA = allowedLabels[index];
          break;
        }
      }
    }
    else if (!string.IsNullOrEmpty(inputMask))
      strA = Mask.Format(inputMask, strA);
    return strA?.TrimEnd();
  }

  private static string FormatIntegralValue(int value, int[] allowedValues = null, string[] allowedLabels = null)
  {
    if (allowedValues != null && allowedLabels != null)
    {
      int num = System.Math.Min(allowedLabels.Length, allowedValues.Length);
      for (int index = 0; index < num; ++index)
      {
        if (value == allowedValues[index])
          return allowedLabels[index];
      }
    }
    return value.ToString();
  }

  internal static string GetValueString(object value)
  {
    switch (value)
    {
      case null:
        return string.Empty;
      case bool flag:
        return EntityHelper.BoolToString(flag);
      case string str:
        return EntityHelper.FormatString(str);
      case byte _:
      case short _:
      case int _:
        return EntityHelper.FormatIntegralValue((int) value);
      case float num1:
        return EntityHelper.FormatFloatValue(num1);
      case double num2:
        int? precision1 = new int?();
        return EntityHelper.FormatDoubleValue(num2, precision1);
      case Decimal num3:
        int? precision2 = new int?();
        return EntityHelper.FormatDecimalValue(num3, precision2);
      case System.DateTime dateTime:
        return EntityHelper.FormatDateTimeValue((object) dateTime);
      default:
        return value.ToString();
    }
  }

  public IEnumerable<KeyValuePair<string, string>> GetFieldValuePairs(object row, System.Type entityType)
  {
    PXCache entityCache = this.graph.Caches[row.GetType()];
    row = EntityHelper.CorrectEntityType(entityCache, row.GetType(), row);
    foreach (string field in (List<string>) entityCache.Fields)
    {
      foreach (PXEventSubscriberAttribute attribute in entityCache.GetAttributes(row, field))
      {
        string fieldString;
        if (attribute is PXUIFieldAttribute pxuiFieldAttribute && pxuiFieldAttribute.Visibility == PXUIVisibility.SelectorVisible && !string.IsNullOrEmpty(fieldString = this.GetFieldString(row, entityType, field, true)))
          yield return new KeyValuePair<string, string>(pxuiFieldAttribute.DisplayName ?? field, fieldString);
      }
    }
  }

  public System.Type GetPrimaryGraphType(object row, bool checkRights)
  {
    return this.GetPrimaryGraphType(ref row, checkRights);
  }

  public System.Type GetPrimaryGraphType(ref object row, bool checkRights)
  {
    if (row == null)
      return (System.Type) null;
    System.Type type = row.GetType();
    return this.GetPrimaryGraphType(ref type, ref row, checkRights);
  }

  public System.Type GetPrimaryGraphType(System.Type cacheType, object row, bool checkRights)
  {
    return this.GetPrimaryGraphType(ref cacheType, ref row, checkRights);
  }

  public System.Type GetPrimaryGraphType(ref System.Type cacheType, ref object row, bool checkRights)
  {
    PXCache declaredCache = this.graph.Caches[cacheType];
    row = EntityHelper.CorrectEntityType(declaredCache, cacheType, row);
    System.Type graphType;
    PXPrimaryGraphAttribute.FindPrimaryGraph(declaredCache, checkRights, ref row, out graphType, out cacheType, out declaredCache);
    return graphType;
  }

  public PXGraph GetPrimaryGraph(object row, bool checkRights)
  {
    System.Type primaryGraphType = this.GetPrimaryGraphType(row, checkRights);
    return !(primaryGraphType != (System.Type) null) ? (PXGraph) null : PXGraph.CreateInstance(primaryGraphType);
  }

  public PXGraph GetPrimaryGraph(System.Type cacheType, object row, bool checkRights)
  {
    System.Type primaryGraphType = this.GetPrimaryGraphType(cacheType, row, checkRights);
    return !(primaryGraphType != (System.Type) null) ? (PXGraph) null : PXGraph.CreateInstance(primaryGraphType);
  }

  public Note SelectNote(Guid? noteID)
  {
    if (!noteID.HasValue)
      return (Note) null;
    return (Note) PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.SelectWindowed(this.graph, 0, 1, (object) noteID);
  }

  public BqlCommand GetViewCommand(string viewName)
  {
    return !this.graph.Views.ContainsKey(viewName) ? (BqlCommand) null : this.graph.Views[viewName].BqlSelect;
  }
}
