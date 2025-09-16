// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNoteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Api.Soap.Screen;
using PX.Common;
using PX.Data.EP;
using PX.Data.SQLTree;
using PX.Data.Wiki.Tags;
using PX.DbServices.QueryObjectModel;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;

#nullable enable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXNoteAttribute : 
  PXDBGuidAttribute,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber,
  IPXRowDeletedSubscriber,
  IPXReportRequiredField
{
  internal const 
  #nullable disable
  string _NoteIDField = "NoteID";
  internal const string _NoteTextField = "NoteText";
  internal string _NoteTextFieldDisplayName = "Note Text";
  public const string NotePopupTextField = "NotePopupText";
  internal const string _NoteFilesField = "NoteFiles";
  internal const string _NoteImagesField = "NoteImages";
  internal const string _NoteActivityField = "NoteActivity";
  protected const string _NoteImagesViewPrefix = "$NoteImages$";
  internal const string _NoteTextExistsField = "NoteTextExists";
  public const string NotePopupTextExistsField = "NotePopupTextExists";
  internal const string _NoteFilesCountField = "NoteFilesCount";
  internal const string _NoteActivitiesCountField = "NoteActivitiesCount";
  private static readonly ISet<string> _noteFields = (ISet<string>) new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    "NoteID",
    "NoteText",
    "NotePopupText",
    "NoteFiles",
    "NoteImages",
    "NoteActivity",
    "NoteTextExists",
    "NotePopupTextExists",
    "NoteFilesCount",
    "NoteActivitiesCount"
  };
  private PXView _noteNoteID;
  private PXView _noteDocNoteID;
  private PXView _noteFileID;
  protected bool _PassThrough;
  protected bool _ForceRetain;
  protected System.Type _ParentNoteID;
  protected System.Type[] extraSearchResultColumns;
  protected System.Type[] foreignRelations;
  protected bool _TextRequired;
  protected bool _PopupTextRequired;
  protected bool _FilesRequired;
  protected bool _ActivityRequired;
  private string _declaringType;
  private static readonly Dictionary<string, MethodInfo> _castDic = new Dictionary<string, MethodInfo>();
  private static readonly object _syncObj = new object();
  internal const string fileListView = "fileListView";
  private readonly Func<PXCache, IBqlTable, string> GetOriginalCountsItem = (Func<PXCache, IBqlTable, string>) ((c, t) => c._GetOriginalCounts((object) t).Item1);
  private readonly Func<PXCache, IBqlTable, string> GetPopupOriginalCountsItem = (Func<PXCache, IBqlTable, string>) ((c, t) => c._GetOriginalCounts((object) t).Item4);

  /// <summary>Initializes a new instance of the attribute that will be used
  /// to attach notes to data record but won't save values of the fields in
  /// a note.</summary>
  public PXNoteAttribute()
    : base(true)
  {
  }

  /// <summary>
  /// Initializes a new instance of the attribute that is used to attach notes to a data record.
  /// </summary>
  /// <remarks> This constructor is the same as the parameterless one. The <paramref name="searches" /> parameter is ignored.</remarks>
  /// <param name="searches">This parameter is ignored.</param>
  [Obsolete("The constructor is obsolete. Use the parameterless constructor instead. The PXNoteAttribute constructor is exactly the same as the parameterless one. It does not provide any additional functionality and does not save values of provided fields in the note. The constructor will be removed in a future version of Acumatica ERP.")]
  public PXNoteAttribute(params System.Type[] searches)
    : base(true)
  {
  }

  [InjectDependencyOnTypeLevel]
  protected IActivityService ActivityService { get; set; }

  /// <exclude />
  public virtual System.Type ParentNoteID
  {
    get => this._ParentNoteID;
    set => this._ParentNoteID = value;
  }

  /// <summary>Gets or sets the value that indicates whether activity items
  /// can be associated with the DAC where the <tt>PXNote</tt> attribute is
  /// used. If the property equals <tt>true</tt>, the DAC will appear in the
  /// list of types in the lookup that selects the related data record for
  /// an activity. If the property equals <tt>false</tt>, activity
  /// attributes cannot be associated with data records of the DAC. By
  /// default the property equals <tt>false</tt>.</summary>
  public bool ShowInReferenceSelector { get; set; }

  /// <summary>Gets or sets the value that indicates whether the calculation
  /// of activities will be suppressed for object
  /// </summary>
  public bool SuppressActivitiesCount { get; set; }

  /// <summary>Gets or sets the value that indicates whether the calculation
  /// of activities will be calculated by parent link
  /// </summary>
  public bool ActivitiesCountByParent { get; set; }

  /// <summary>Gets or sets the list of fields that will be displayed in a
  /// separate column when rendering search results.</summary>
  public System.Type[] ExtraSearchResultColumns
  {
    get => this.extraSearchResultColumns;
    set => this.extraSearchResultColumns = value;
  }

  /// <summary>Gets or sets the list of fields that connect the current
  /// table with foreign tables. The fields from the foreign tables can be
  /// specified along with current table fields in the <tt>Searches</tt>
  /// list.</summary>
  public System.Type[] ForeignRelations
  {
    get => this.foreignRelations;
    set => this.foreignRelations = value;
  }

  /// <exclude />
  public bool ForceFileCorrection { get; set; }

  public virtual bool PopupTextEnabled { get; set; }

  [PXInternalUseOnly]
  public virtual bool DoNotUseAsRecordID { get; set; }

  private static IEnumerable<PXDataRecord> SelectNote(PXGraph graph, Guid id)
  {
    return PXDatabase.Provider.Select(new Select<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>().GetQuery(graph, 1L), (IEnumerable<PXDataValue>) new PXDataValue[1]
    {
      new PXDataValue((object) id)
    });
  }

  protected Note GetNote(PXGraph graph, Guid? id)
  {
    if (!id.HasValue)
      return (Note) null;
    return (Note) this.GetView(graph).SelectSingle((object) id.Value);
  }

  protected PXView GetView(PXGraph graph)
  {
    return this._noteNoteID ?? (this._noteNoteID = new PXView(graph, false, (BqlCommand) new Select<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>()));
  }

  protected int GetActivityCount(PXGraph graph, Guid? id)
  {
    if (!id.HasValue)
      return 0;
    PXDataFieldValue pxDataFieldValue = (PXDataFieldValue) null;
    if (this.ActivitiesCountByParent)
    {
      EntityHelper entityHelper = new EntityHelper(graph);
      object entityRow = entityHelper.GetEntityRow(id);
      if (entityRow == null)
        return 0;
      string idField = EntityHelper.GetIDField(graph.Caches[entityRow.GetType()]);
      if (!string.IsNullOrWhiteSpace(idField))
        pxDataFieldValue = new PXDataFieldValue((SQLExpression) new Column("BAccountID", "CRActivity"), PXDbType.Int, new int?(4), (object) (int) entityHelper.GetField(entityRow, (string) null, idField));
    }
    if (pxDataFieldValue == null)
      pxDataFieldValue = new PXDataFieldValue((SQLExpression) new Column("RefNoteID", "CRActivity"), PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) id.Value);
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<CRActivity>(new PXDataField(SQLExpression.Count()), (PXDataField) pxDataFieldValue, (PXDataField) new PXDataFieldValue((SQLExpression) new Column("UIStatus", "CRActivity"), PXDbType.Char, new int?(2), (object) "CL", PXComp.NE), (PXDataField) new PXDataFieldValue((SQLExpression) new Column("UIStatus", "CRActivity"), PXDbType.Char, new int?(2), (object) "RL", PXComp.NE)))
      return pxDataRecord != null ? pxDataRecord.GetInt32(0).GetValueOrDefault() : 0;
  }

  protected PXView GetDocView(PXGraph graph)
  {
    if (this._noteDocNoteID == null)
      this._noteDocNoteID = new PXView(graph, false, (BqlCommand) new Select<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>());
    return this._noteDocNoteID;
  }

  protected PXView GetFileByID(PXGraph graph)
  {
    if (this._noteFileID == null)
      this._noteFileID = new PXView(graph, false, new Select2<UploadFile, InnerJoin<UploadFileRevisionNoData, On<UploadFileRevisionNoData.fileID, Equal<UploadFile.fileID>, And<UploadFileRevisionNoData.fileRevisionID, Equal<UploadFile.lastRevisionID>>>>>().WhereAnd(typeof (Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>>)));
    return this._noteFileID;
  }

  /// <summary>Returns the identifier of the note attached to the provided
  /// object and inserts a new note into the cache if the note does not
  /// exist.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="name">The name of the field that stores note identifier.
  /// If <tt>null</tt>, the method will search attributes on all fields and
  /// use the first <tt>PXNote</tt> attribute it finds.</param>
  public static Guid? GetNoteID(PXCache cache, object data, string name)
  {
    data = PXNoteAttribute.CastRow(cache, data);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXNoteAttribute)
        return new Guid?(((PXNoteAttribute) attribute).GetNoteID(cache, data));
    }
    return new Guid?();
  }

  /// <summary>Returns the identifier of the note attached to the provided
  /// object if note exists/
  /// </summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="name">The name of the field that stores note identifier.
  /// If <tt>null</tt>, the method will search attributes on all fields and
  /// use the first <tt>PXNote</tt> attribute it finds.</param>
  public static Guid? GetNoteIDIfExists(PXCache cache, object data, string name)
  {
    data = PXNoteAttribute.CastRow(cache, data);
    using (IEnumerator<PXNoteAttribute> enumerator = cache.GetAttributes(data, name).OfType<PXNoteAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
        return enumerator.Current.GetNoteIDIfExist(cache, data);
    }
    return new Guid?();
  }

  /// <summary>Returns the identifier of the note attached to the provided
  /// object if note exists/
  /// </summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static Guid? GetNoteIDIfExists(PXCache cache, object data)
  {
    data = PXNoteAttribute.CastRow(cache, data);
    using (IEnumerator<PXNoteAttribute> enumerator = cache.GetAttributesReadonly((string) null).OfType<PXNoteAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
        return enumerator.Current.GetNoteIDIfExist(cache, data);
    }
    return new Guid?();
  }

  /// <summary>
  /// Checks if note id record actually exists in the database
  /// </summary>
  private static bool NoteExists(PXCache cache, Guid? id)
  {
    return id.HasValue && PXNoteAttribute.NoteExists(cache.GetAttributesReadonly((string) null).OfType<PXNoteAttribute>().FirstOrDefault<PXNoteAttribute>(), cache.Graph, id.Value);
  }

  private static bool NoteExists(PXNoteAttribute noteAttribute, PXGraph graph, Guid id)
  {
    return noteAttribute != null ? noteAttribute.GetNote(graph, new Guid?(id)) != null : PXNoteAttribute.SelectNote(graph, id).Any<PXDataRecord>();
  }

  /// <summary>Returns the identifier of the note attached to the provided
  /// object and inserts a new note into the cache if the note does not
  /// exist. The field that stores note identifier is specified in the type
  /// parameter.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static Guid? GetNoteID<Field>(PXCache cache, object data) where Field : IBqlField
  {
    data = PXNoteAttribute.CastRow(cache, data);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXNoteAttribute)
        return new Guid?(((PXNoteAttribute) attribute).GetNoteID(cache, data));
    }
    return new Guid?();
  }

  internal static bool ImportEnsureNewNoteID(PXCache cache, object data, string externalKey)
  {
    return cache.GetAttributes(data, "NoteID").OfType<PXNoteAttribute>().Any<PXNoteAttribute>((Func<PXNoteAttribute, bool>) (attr => attr.EnsureNoteID(cache, data, externalKey) != Guid.Empty));
  }

  /// <summary>Returns the identifier of the note attached to the provided
  /// object or <tt>null</tt> if the note does not exist.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="name">The name of the field that stores note identifier.
  /// If <tt>null</tt>, the method will search attributes on all fields and
  /// use the first <tt>PXNote</tt> attribute it finds.</param>
  public static Guid? GetNoteIDReadonly(PXCache cache, object data, string name)
  {
    data = PXNoteAttribute.CastRow(cache, data);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXNoteAttribute pxNoteAttribute)
        return (Guid?) cache.GetValue(data, pxNoteAttribute._FieldOrdinal);
    }
    return new Guid?();
  }

  private static object CastRow(PXCache cache, object data)
  {
    System.Type itemType = cache.GetItemType();
    if (data is PXResult pxResult)
      data = PXNoteAttribute.DynamicalyChangeType(pxResult[itemType], itemType);
    data = PXNoteAttribute.DynamicalyChangeType(data, itemType);
    return data;
  }

  private static object DynamicalyChangeType(object obj, System.Type type)
  {
    if (type == (System.Type) null)
      throw new ArgumentNullException(nameof (type));
    if (obj == null)
      return (object) null;
    System.Type type1 = obj.GetType();
    if (type.IsAssignableFrom(type1))
      return obj;
    MethodInfo methodInfo = (MethodInfo) null;
    lock (PXNoteAttribute._syncObj)
    {
      string key = $"{type1.Name}->{type.Name}";
      if (!PXNoteAttribute._castDic.TryGetValue(key, out methodInfo))
      {
        methodInfo = PXNoteAttribute.GetMethod(type1, "op_Implicit", type, BindingFlags.Static | BindingFlags.Public);
        if (methodInfo == (MethodInfo) null)
          methodInfo = PXNoteAttribute.GetMethod(type1, "op_Explicit", type, BindingFlags.Static | BindingFlags.Public);
        PXNoteAttribute._castDic.Add(key, methodInfo);
      }
    }
    return !(methodInfo == (MethodInfo) null) ? methodInfo.Invoke((object) null, new object[1]
    {
      obj
    }) : throw new InvalidCastException($"Invalid cast: {MainTools.GetLongName(type1)} to {MainTools.GetLongName(type)}");
  }

  private static MethodInfo GetMethod(
    System.Type toSearch,
    string methodName,
    System.Type returnType,
    BindingFlags bindingFlags)
  {
    return Array.Find<MethodInfo>(toSearch.GetMethods(bindingFlags), (Predicate<MethodInfo>) (inf => inf.Name == methodName && inf.ReturnType == returnType));
  }

  /// <summary>Returns the identifier of the note attached to the provided
  /// object or <tt>null</tt> if the note does not exist. The field that
  /// stores note identifier is specified in the type parameter.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static Guid? GetNoteIDReadonly<Field>(PXCache cache, object data) where Field : IBqlField
  {
    data = PXNoteAttribute.CastRow(cache, data);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXNoteAttribute pxNoteAttribute)
        return (Guid?) cache.GetValue(data, pxNoteAttribute._FieldOrdinal);
    }
    return new Guid?();
  }

  /// <summary>Sets the DAC type of the data record to which the note is
  /// attached. The full name of the DAC is saved in the database in the
  /// note record. This information is used, for example, to determine the
  /// webpage to open to show full details of the data record associated
  /// with a note.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="noteFieldName">The name of the field that stores note
  /// identifier.</param>
  /// <param name="newEntityType">New DAC type to associate with the
  /// note.</param>
  public static void UpdateEntityType(
    PXCache cache,
    object data,
    string noteFieldName,
    System.Type newEntityType)
  {
    data = PXNoteAttribute.CastRow(cache, data);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, noteFieldName))
    {
      if (attribute is PXNoteAttribute)
      {
        Note note = (Note) PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.SelectWindowed(cache.Graph, 0, 1, (object) ((PXNoteAttribute) attribute).GetNoteID(cache, data));
        if (note != null)
        {
          note.EntityType = ((PXNoteAttribute) attribute)._declaringType;
          cache.Graph.Caches[typeof (Note)].Update((object) note);
        }
      }
    }
  }

  /// <exclude />
  public static long? GetParentNoteID<Field>(PXCache cache, object data) where Field : IBqlField
  {
    data = PXNoteAttribute.CastRow(cache, data);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXNoteAttribute)
        return ((PXNoteAttribute) attribute).GetParentNoteID(cache, data);
    }
    return new long?();
  }

  /// <exclude />
  public static void ForcePassThrow<Field>(PXCache cache) where Field : IBqlField
  {
    PXNoteAttribute.ForcePassThrow(cache, typeof (Field).Name);
  }

  /// <exclude />
  public static void ForcePassThrow(PXCache cache, string name)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(name))
    {
      if (subscriberAttribute is PXNoteAttribute)
      {
        ((PXNoteAttribute) subscriberAttribute)._PassThrough = true;
        break;
      }
    }
  }

  /// <exclude />
  public static void ForceRetain<Field>(PXCache cache) where Field : IBqlField
  {
    PXNoteAttribute.ForceRetain(cache, typeof (Field).Name);
  }

  /// <exclude />
  public static void ForceRetain(PXCache cache, string name)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(name))
    {
      if (subscriberAttribute is PXNoteAttribute)
      {
        ((PXNoteAttribute) subscriberAttribute)._ForceRetain = true;
        break;
      }
    }
  }

  /// <summary>Returns the identifier of the note attached to the provided
  /// object and inserts a new note into the database if the note does not
  /// exist.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static Guid? GetNoteIDNow(PXCache cache, object data)
  {
    data = PXNoteAttribute.CastRow(cache, data);
    Guid? id = new Guid?();
    using (IEnumerator<PXNoteAttribute> enumerator = cache.GetAttributesReadonly((string) null).OfType<PXNoteAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXNoteAttribute current = enumerator.Current;
        id = (Guid?) cache.GetValue(data, current.FieldName);
        if (!id.HasValue)
        {
          id = new Guid?(PXNoteAttribute.GenerateId());
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            PXNoteAttribute.InsertNoteRecord(cache, id.Value, string.Empty);
            cache.SetValue(data, current.FieldName, (object) id);
            current.updateTableWithId(cache, data, id);
            transactionScope.Complete();
            cache.Graph.TimeStamp = PXDatabase.SelectTimeStamp();
          }
        }
        else
          PXNoteAttribute.InsertNoteIDIfNotExists(cache, id.Value, current);
      }
    }
    return id;
  }

  internal static void InsertNoteIDIfNotExists(PXCache cache, Guid id, PXNoteAttribute noteattr = null)
  {
    if (PXNoteAttribute.NoteExists(noteattr, cache.Graph, id))
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PXNoteAttribute.InsertNoteRecord(cache, id, string.Empty);
      transactionScope.Complete();
      cache.Graph.TimeStamp = PXDatabase.SelectTimeStamp();
    }
  }

  internal static Guid GenerateId() => SequentialGuid.Generate();

  private void updateTableWithId(PXCache sender, object data, Guid? id)
  {
    List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign(this._DatabaseFieldName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) id, sender.ValueToString(this._FieldName, (object) id)));
    foreach (string field in (List<string>) sender.Fields)
    {
      PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
      try
      {
        sender.RaiseCommandPreparing(field, data, sender.GetValue(data, field), PXDBOperation.Update, (System.Type) null, out description);
      }
      catch (PXDBTimestampAttribute.PXTimeStampEmptyException ex)
      {
      }
      if (description != null && description.IsRestriction && description.DataType != PXDbType.Timestamp && (sender.BqlSelect == null || description.Expr is Column expr && (expr.Table() as SimpleTable).Name == this._BqlTable.Name))
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
    }
    if (pxDataFieldParamList.Count <= 1 && sender.Keys.Count != 0)
      return;
    PXDatabase.Update(this.BqlTable, pxDataFieldParamList.ToArray());
  }

  /// <summary>
  /// Sets the flags to behavior control of extended virtual fields selecting
  /// </summary>
  /// <param name="cache">The cache object to search for the attributes of <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If <tt>null</tt>, the method is applied to all data records in the cache object.</param>
  /// <param name="fieldName">The field name.</param>
  /// <param name="isTextRequired">Flag of selecting 'NoteText' fields</param>
  /// <param name="isFilesRequired">Flag of selecting 'NoteDoc' fields</param>
  /// <param name="isActivitiesRequired">Flag of selecting 'NoteActivity' fields</param>
  public static void SetTextFilesActivitiesRequired(
    PXCache cache,
    object data,
    string fieldName,
    bool isTextRequired = true,
    bool isFilesRequired = true,
    bool isActivitiesRequired = false)
  {
    if (data == null)
      cache.SetAltered(fieldName, true);
    foreach (PXNoteAttribute pxNoteAttribute in cache.GetAttributes(data, fieldName).OfType<PXNoteAttribute>())
    {
      pxNoteAttribute._TextRequired = isTextRequired;
      pxNoteAttribute._PopupTextRequired = isTextRequired;
      pxNoteAttribute._FilesRequired = isFilesRequired;
      pxNoteAttribute._ActivityRequired = isActivitiesRequired;
    }
  }

  /// <summary>
  /// Sets the flags to behavior control of extended virtual fields selecting
  /// </summary>
  /// <param name="cache">The cache object to search for the attributes of <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If <tt>null</tt>, the method is applied to all data records in the cache object.</param>
  /// <param name="isTextRequired">Flag of selecting 'NoteText' fields</param>
  /// <param name="isFilesRequired">Flag of selecting 'NoteDoc' fields</param>
  /// <param name="isActivitiesRequired">Flag of selecting 'NoteActivity' fields</param>
  public static void SetTextFilesActivitiesRequired<Field>(
    PXCache cache,
    object data,
    bool isTextRequired = true,
    bool isFilesRequired = true,
    bool isActivitiesRequired = false)
    where Field : IBqlField
  {
    PXNoteAttribute.SetTextFilesActivitiesRequired(cache, data, typeof (Field).Name, isTextRequired, isFilesRequired, isActivitiesRequired);
  }

  /// <summary>Sets the list of identifiers of files that are shown in the
  /// <b>Files</b> pop-up window.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="fileIDs">The indetifiers of files to display.</param>
  public static void SetFileNotes(PXCache cache, object data, params Guid[] fileIDs)
  {
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(cache, data);
    if ((fileNotes == null || fileNotes.Length == 0) && (fileIDs == null || fileIDs.Length == 0))
      return;
    cache.SetValueExt(data, "NoteFiles", (object) fileIDs);
  }

  /// <summary>Returns the list of identifiers of files that are shown in
  /// the <b>Files</b> pop-up window.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static Guid[] GetFileNotes(PXCache sender, object data)
  {
    using (IEnumerator<PXNoteAttribute> enumerator = sender.GetAttributesReadonly((string) null).OfType<PXNoteAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXNoteAttribute current = enumerator.Current;
        Guid? nullable = (Guid?) sender.GetValue(data, current._FieldName);
        return current.GetDocView(sender.Graph).SelectMulti((object) nullable).Cast<NoteDoc>().Where<NoteDoc>((Func<NoteDoc, bool>) (doc => doc.FileID.HasValue)).Select<NoteDoc, Guid>((Func<NoteDoc, Guid>) (doc => doc.FileID.Value)).ToArray<Guid>();
      }
    }
    return (Guid[]) null;
  }

  /// <summary>Returns the text comment of the note attached to the provided
  /// object.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static string GetNote(PXCache sender, object data)
  {
    string str = (string) PXFieldState.UnwrapValue(sender.GetValueExt(data, "NoteText"));
    return !string.IsNullOrEmpty(str) ? str : (string) null;
  }

  public static string GetPopupNote(PXCache cache, object data)
  {
    return PXFieldState.UnwrapValue(cache.GetValueExt(data, "NotePopupText")) as string;
  }

  /// <summary>Sets the text of the note attached to the provided data
  /// record.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXNote</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="note">The text to place in the note.</param>
  public static void SetNote(PXCache sender, object data, string note)
  {
    if (!(PXNoteAttribute.GetNote(sender, data) != note))
      return;
    sender.SetValueExt(data, "NoteText", (object) note);
  }

  /// <exclude />
  public static void CopyNoteAndFiles(
    PXCache src_cache,
    object src_row,
    PXCache dst_cache,
    object dst_row,
    bool? copyNotes,
    bool? copyFiles)
  {
    object noteIdIfExists = (object) PXNoteAttribute.GetNoteIDIfExists(src_cache, src_row);
    bool? nullable1 = copyNotes;
    bool flag1 = true;
    if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue && noteIdIfExists != null)
      PXNoteAttribute.SetNote(dst_cache, dst_row, PXNoteAttribute.GetNote(src_cache, src_row));
    bool? nullable2 = copyFiles;
    bool flag2 = true;
    if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue) || noteIdIfExists == null)
      return;
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(src_cache, src_row);
    if (fileNotes == null || !((IEnumerable<Guid>) fileNotes).Any<Guid>())
      return;
    PXNoteAttribute.SetFileNotes(dst_cache, dst_row, fileNotes);
  }

  /// <exclude />
  public static void CopyNoteAndFiles(
    PXCache src_cache,
    object src_row,
    PXCache dst_cache,
    object dst_row,
    PXNoteAttribute.IPXCopySettings settings = null)
  {
    PXCache src_cache1 = src_cache;
    object src_row1 = src_row;
    PXCache dst_cache1 = dst_cache;
    object dst_row1 = dst_row;
    bool? nullable;
    int num1;
    if (settings != null)
    {
      nullable = settings.CopyNotes;
      bool flag = true;
      num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    bool? copyNotes = new bool?(num1 != 0);
    int num2;
    if (settings != null)
    {
      nullable = settings.CopyFiles;
      bool flag = true;
      num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    bool? copyFiles = new bool?(num2 != 0);
    PXNoteAttribute.CopyNoteAndFiles(src_cache1, src_row1, dst_cache1, dst_row1, copyNotes, copyFiles);
  }

  [PXInternalUseOnly]
  public static bool TryGetNoteEntityType(PXCache cache, Guid? noteID, out System.Type entityType)
  {
    if (cache != null)
    {
      PXNoteAttribute pxNoteAttribute = cache.GetAttributesReadonly("NoteID").OfType<PXNoteAttribute>().FirstOrDefault<PXNoteAttribute>();
      if (pxNoteAttribute != null)
      {
        string entityType1 = pxNoteAttribute.GetEntityType(cache, noteID);
        if (!string.IsNullOrEmpty(entityType1))
        {
          entityType = PXBuildManager.GetType(entityType1, false);
          return entityType != (System.Type) null;
        }
      }
    }
    entityType = (System.Type) null;
    return false;
  }

  [PXInternalUseOnly]
  public static bool TryGetNoteGraphType(PXCache cache, out System.Type graphType)
  {
    if (cache != null)
    {
      PXNoteAttribute pxNoteAttribute = cache.GetAttributesReadonly("NoteID").OfType<PXNoteAttribute>().FirstOrDefault<PXNoteAttribute>();
      if (pxNoteAttribute != null)
      {
        string graphType1 = pxNoteAttribute.GetGraphType(cache.Graph);
        if (!string.IsNullOrEmpty(graphType1))
        {
          graphType = PXBuildManager.GetType(graphType1, false);
          return graphType != (System.Type) null;
        }
      }
    }
    graphType = (System.Type) null;
    return false;
  }

  [Obsolete("Use either the virtual version of this method or the TryGetNoteGraphType method instead.")]
  protected static string GetGraphType(PXCache cache)
  {
    System.Type graphType;
    return PXNoteAttribute.TryGetNoteGraphType(cache, out graphType) ? graphType.FullName : CustomizedTypeManager.GetTypeNotCustomized(cache.Graph).FullName;
  }

  protected virtual string GetGraphType(PXGraph graph)
  {
    return CustomizedTypeManager.GetTypeNotCustomized(graph).FullName;
  }

  protected virtual string GetEntityType(PXCache cache, Guid? noteId) => this._declaringType;

  protected virtual bool IsVirtualTable(System.Type table) => PXDatabase.IsVirtualTable(table);

  protected Guid? GetNoteIDIfExist(PXCache sender, object row)
  {
    return (Guid?) sender.GetValue(row, this._FieldOrdinal);
  }

  protected Guid GetNoteID(PXCache sender, object row)
  {
    return this.EnsureNoteID(sender, row, (string) null);
  }

  protected Guid EnsureNoteID(PXCache sender, object row, string externalKey)
  {
    Guid? noteId = (Guid?) sender.GetValue(row, this._FieldOrdinal);
    PXView view = this.GetView(sender.Graph);
    if (noteId.HasValue)
    {
      if (view.SelectSingle((object) noteId.Value) != null)
        return noteId.Value;
    }
    Note note1 = new Note()
    {
      NoteID = noteId,
      NoteText = string.Empty,
      EntityType = this.GetEntityType(sender, noteId),
      GraphType = this.GetGraphType(sender.Graph),
      ExternalKey = externalKey
    };
    Note note2 = (Note) view.Cache.Insert((object) note1);
    if (!noteId.HasValue)
    {
      noteId = note2.NoteID;
      sender.SetValue(row, this._FieldOrdinal, (object) noteId);
    }
    if (sender.Locate(row) != null)
      sender.Graph.EnsureRowPersistence(row);
    sender.IsDirty = true;
    return noteId.Value;
  }

  protected long? GetParentNoteID(PXCache sender, object row)
  {
    if (this._ParentNoteID == (System.Type) null)
      return new long?();
    PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(this._ParentNoteID)];
    return cach.Current == null ? new long?() : (long?) cach.GetValue(cach.Current, this._ParentNoteID.Name);
  }

  /// <exclude />
  public virtual void noteTextFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    Func<Note, string> getText = (Func<Note, string>) (n => n.NoteText);
    this.NoteTextGenericFieldSelecting(sender, e, this.GetOriginalCountsItem, new Action<PXCache, object, string>(this.SetNoteTextExists), getText, "NoteText");
  }

  public virtual void notePopupTextFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!this.PopupTextEnabled)
      return;
    Func<Note, string> getText = (Func<Note, string>) (n => n.NotePopupText);
    this.NoteTextGenericFieldSelecting(sender, e, this.GetPopupOriginalCountsItem, new Action<PXCache, object, string>(this.SetPopupNoteTextExists), getText, "NotePopupText");
  }

  private void NoteTextGenericFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    Func<PXCache, IBqlTable, string> getOriginalCountsItem,
    Action<PXCache, object, string> setNoteTextExists,
    Func<Note, string> getText,
    string fieldName)
  {
    if (e.Row == null && this.IsVirtualTable(sender.BqlTable))
    {
      e.ReturnValue = (object) null;
      e.ReturnState = (object) null;
      e.Cancel = true;
    }
    else
    {
      Guid? id = new Guid?();
      if (e.Row != null)
        id = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      if (id.HasValue)
      {
        string str = getOriginalCountsItem(sender, e.Row as IBqlTable);
        if (str == null)
        {
          Note note = this.GetNote(sender.Graph, id);
          if (note != null)
            e.ReturnValue = (object) PXNoteAttribute.NormalizeNoteText(getText(note) ?? string.Empty);
        }
        else
          e.ReturnValue = (object) str;
        setNoteTextExists(sender, e.Row, (string) e.ReturnValue);
      }
      if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
        return;
      e.ReturnState = (object) PXNoteState.CreateInstance(e.ReturnState, fieldName, this._FieldName, "NoteText", "NoteFiles", "NoteActivity", "NoteTextExists", "NoteFilesCount", this._NoteTextFieldDisplayName);
    }
  }

  /// <exclude />
  public static string NormalizeNoteText(string noteText)
  {
    if (noteText == null)
      return (string) null;
    string[] strArray = noteText.Split(PXDatabase.Provider.SqlDialect.WildcardFieldSeparatorChar);
    return strArray.Length == 0 ? string.Empty : strArray[0];
  }

  /// <exclude />
  public virtual void noteTextFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    Action<PXCache, Guid, string> updateNoteRecord = (Action<PXCache, Guid, string>) ((c, g, s) => this.UpdateNote(c, g, s));
    Action<Note, string> assignText = (Action<Note, string>) ((n, s) => n.NoteText = s);
    System.Action<bool> textRequired = (System.Action<bool>) (r => this._TextRequired = r);
    this.NoteTextGenericFieldUpdating(sender, e, new Action<PXCache, object, string>(this.SetNoteTextExists), updateNoteRecord, assignText, "NoteText", textRequired);
  }

  public virtual void notePopupTextFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!this.PopupTextEnabled)
      return;
    Action<PXCache, Guid, string> updateNoteRecord = (Action<PXCache, Guid, string>) ((c, g, s) => this.UpdateNote(c, g, popupText: s));
    Action<Note, string> assignText = (Action<Note, string>) ((n, s) => n.NotePopupText = s);
    System.Action<bool> textRequired = (System.Action<bool>) (r => this._PopupTextRequired = r);
    this.NoteTextGenericFieldUpdating(sender, e, new Action<PXCache, object, string>(this.SetPopupNoteTextExists), updateNoteRecord, assignText, "NoteText", textRequired);
    if ((string.IsNullOrEmpty(e.NewValue as string) ? 0 : (this.NeedToAssignDefaultNoteTextValue(sender, e.Row) ? 1 : 0)) == 0)
      return;
    sender.SetValueExt(e.Row, "NoteText", (object) string.Empty);
  }

  private static string GetCurrentScreenID(PXCache cache)
  {
    string currentScreenId = PXContext.GetScreenID()?.Replace(".", "");
    if (currentScreenId != null)
      return currentScreenId;
    if (!(PXSiteMap.Provider is PXDatabaseSiteMapProvider provider))
      return (string) null;
    return provider.FindSiteMapNodeByGraphType(cache.Graph.GetType().FullName)?.ScreenID;
  }

  private static bool RowIsUnchanged(PXCache cache, object row)
  {
    PXEntryStatus status = cache.GetStatus(row);
    return status == PXEntryStatus.Notchanged || status == PXEntryStatus.Held;
  }

  private bool NeedToAssignDefaultNoteTextValue(PXCache cache, object row)
  {
    Guid? nullable = cache.GetValue(row, this._FieldOrdinal) as Guid?;
    if (!nullable.HasValue)
      return false;
    PXView view = this.GetView(cache.Graph);
    return view.SelectSingle((object) nullable) is Note note && view.Cache.GetStatus((object) note) == PXEntryStatus.Inserted && note.NoteText == null;
  }

  private void NoteTextGenericFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    Action<PXCache, object, string> setNoteTextExists,
    Action<PXCache, Guid, string> updateNoteRecord,
    Action<Note, string> assignText,
    string noteTextField,
    System.Action<bool> textRequired)
  {
    if (e.Row != null)
    {
      string newValue = (string) e.NewValue;
      Guid? id = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      setNoteTextExists(sender, e.Row, newValue ?? string.Empty);
      if (this._PassThrough || !sender.AllowUpdate && PXNoteAttribute.RowIsUnchanged(sender, e.Row))
      {
        if (string.IsNullOrWhiteSpace(newValue))
        {
          if (!id.HasValue)
            return;
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            updateNoteRecord(sender, id.Value, string.Empty);
            transactionScope.Complete();
          }
        }
        else
        {
          bool flag = !id.HasValue;
          if (flag)
            id = new Guid?(PXNoteAttribute.GenerateId());
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            updateNoteRecord(sender, id.Value, newValue);
            if (flag)
            {
              sender.SetValue(e.Row, this._FieldOrdinal, (object) id);
              this.updateTableWithId(sender, e.Row, id);
            }
            transactionScope.Complete();
          }
        }
      }
      else
      {
        bool flag = id.HasValue;
        if (flag)
        {
          if (this.GetView(sender.Graph).SelectSingle((object) id) is Note note1)
          {
            assignText(note1, newValue ?? string.Empty);
            note1.EntityType = this.GetEntityType(sender, note1.NoteID);
            note1.GraphType = this.GetGraphType(sender.Graph);
            this.GetView(sender.Graph).Cache.Update((object) note1);
          }
          else
            flag = false;
        }
        if (!flag && !string.IsNullOrEmpty(newValue))
        {
          Note note2 = new Note();
          bool hasValue = id.HasValue;
          if (hasValue)
            note2.NoteID = id;
          assignText(note2, newValue);
          note2.EntityType = this.GetEntityType(sender, note2.NoteID);
          note2.GraphType = this.GetGraphType(sender.Graph);
          if (this.GetView(sender.Graph).Cache.Insert((object) note2) is Note note3)
          {
            id = note3.NoteID;
            setNoteTextExists(sender, e.Row, note3.NoteText);
            if (!hasValue)
              sender.SetValue(e.Row, this._FieldOrdinal, (object) id);
          }
        }
        if (PXNoteAttribute.RowIsUnchanged(sender, e.Row))
          sender.SetStatus(e.Row, PXEntryStatus.Modified);
        sender.IsDirty = true;
      }
    }
    else
    {
      textRequired(true);
      PXCache cach = sender.Graph.Caches[this._BqlTable];
      if (cach == sender)
        return;
      object newValue = (object) null;
      cach.RaiseFieldUpdating(noteTextField, (object) null, ref newValue);
    }
  }

  /// <exclude />
  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this._ForceRetain || !(sender.GetValue(e.Row, this._FieldOrdinal) is Guid guid1))
      return;
    Guid id = guid1;
    Note note1 = new Note() { NoteID = new Guid?(id) };
    PXCache cach1 = sender.Graph.Caches[typeof (Note)];
    PXCache cach2 = sender.Graph.Caches[typeof (NoteDoc)];
    switch (sender.GetStatus(e.Row))
    {
      case PXEntryStatus.Inserted:
      case PXEntryStatus.InsertedDeleted:
        Note note2 = (Note) cach1.Locate((object) note1);
        if (note2 != null)
          cach1.Remove((object) note2);
        using (IEnumerator<NoteDoc> enumerator = cach2.Cached.Cast<NoteDoc>().Where<NoteDoc>((Func<NoteDoc, bool>) (_ =>
        {
          Guid? noteId = _.NoteID;
          Guid guid2 = id;
          if (!noteId.HasValue)
            return false;
          return !noteId.HasValue || noteId.GetValueOrDefault() == guid2;
        })).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            NoteDoc current = enumerator.Current;
            cach2.Remove((object) current);
          }
          break;
        }
      default:
        cach1.Delete((object) note1);
        using (List<object>.Enumerator enumerator = this.GetDocView(sender.Graph).SelectMulti((object) id).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            NoteDoc current = (NoteDoc) enumerator.Current;
            cach2.Delete((object) current);
          }
          break;
        }
    }
  }

  /// <exclude />
  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    e.NewValue = (object) PXNoteAttribute.GenerateId();
  }

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(sender.GetValue(e.Row, this._FieldOrdinal) is Guid guid))
      return;
    Guid id = guid;
    string screenID = (string) null;
    PXCache cache = sender.Graph.Caches[typeof (Note)];
    PXCache dcache = sender.Graph.Caches[typeof (NoteDoc)];
    if (PXTransactionScope.IsScoped)
    {
      DoSaveNotes();
    }
    else
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        DoSaveNotes();
        transactionScope.Complete();
      }
    }
    if (this._ForceRetain || e.Operation.Command() != PXDBOperation.Delete)
      return;
    this.ExecuteDeleteOnRowPersisting(sender, e, cache, dcache, id);

    void DoSaveNotes()
    {
      foreach (Note row in cache.Inserted)
      {
        Guid? noteId = row.NoteID;
        Guid id = id;
        if ((noteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() != id ? 1 : 0) : 0) : 1) == 0)
          cache.PersistInserted((object) row);
      }
      foreach (Note row in cache.Updated)
      {
        Guid? noteId = row.NoteID;
        Guid id = id;
        if ((noteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == id ? 1 : 0) : 1) : 0) != 0)
        {
          cache.PersistUpdated((object) row);
          break;
        }
      }
      foreach (NoteDoc row in dcache.Inserted)
      {
        Guid? nullable = row.NoteID;
        Guid id = id;
        if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == id ? 1 : 0) : 1) : 0) != 0)
        {
          dcache.PersistInserted((object) row);
          nullable = row.FileID;
          if (nullable.HasValue)
          {
            if (screenID == null)
              screenID = PXNoteAttribute.GetCurrentScreenID(sender);
            if (screenID != null)
            {
              nullable = row.FileID;
              Guid fileID = nullable.Value;
              nullable = new Guid?();
              Guid? pageID = nullable;
              string screenId = screenID;
              UploadFileMaintenance.SetAccessSource(fileID, pageID, screenId);
            }
          }
        }
      }
    }
  }

  protected void ExecuteDeleteOnRowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e,
    PXCache cache,
    PXCache dcache,
    Guid id)
  {
    if (!(e.Row is SYMapping))
    {
      PXGraph graph = new PXGraph();
      foreach (UploadFile firstTableItem in PXSelectBase<UploadFile, PXSelectReadonly2<UploadFile, InnerJoin<NoteDoc, On<NoteDoc.fileID, Equal<UploadFile.fileID>>>, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select(sender.Graph, (object) id).FirstTableItems)
      {
        if (((IEnumerable<NoteDoc>) PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.fileID, Equal<Required<NoteDoc.fileID>>>>.Config>.Select(graph, (object) firstTableItem.FileID).FirstTableItems.ToArray<NoteDoc>()).All<NoteDoc>((Func<NoteDoc, bool>) (fileNote =>
        {
          Guid? noteId = fileNote.NoteID;
          Guid guid = id;
          if (!noteId.HasValue)
            return false;
          return !noteId.HasValue || noteId.GetValueOrDefault() == guid;
        })))
        {
          PXDatabase.Delete<NoteDoc>((PXDataFieldRestrict) new PXDataFieldRestrict<NoteDoc.fileID>(PXDbType.UniqueIdentifier, (object) firstTableItem.FileID), (PXDataFieldRestrict) new PXDataFieldRestrict<NoteDoc.noteID>(PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) id, PXComp.NE));
          PXDatabase.Delete<UploadFileRevision>((PXDataFieldRestrict) new PXDataFieldRestrict<UploadFileRevision.fileID>(PXDbType.UniqueIdentifier, (object) firstTableItem.FileID));
          PXDatabase.Delete<UploadFile>((PXDataFieldRestrict) new PXDataFieldRestrict<UploadFile.fileID>(PXDbType.UniqueIdentifier, (object) firstTableItem.FileID));
          PXDatabase.Delete<UploadFileTag>((PXDataFieldRestrict) new PXDataFieldRestrict<UploadFileTag.fileID>(PXDbType.UniqueIdentifier, (object) firstTableItem.FileID));
        }
      }
    }
    foreach (Note row in cache.Deleted)
    {
      Guid? noteId = row.NoteID;
      Guid guid = id;
      if ((noteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == guid ? 1 : 0) : 1) : 0) != 0)
        cache.PersistDeleted((object) row);
    }
    foreach (NoteDoc row in dcache.Deleted)
    {
      Guid? noteId = row.NoteID;
      Guid guid = id;
      if ((noteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == guid ? 1 : 0) : 1) : 0) != 0)
        dcache.PersistDeleted((object) row);
    }
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    PXCache cach1 = sender.Graph.Caches[typeof (Note)];
    PXCache cach2 = sender.Graph.Caches[typeof (NoteDoc)];
    Guid? nullable1 = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
    if (!nullable1.HasValue)
    {
      if (e.TranStatus != PXTranStatus.Completed)
        return;
      foreach (Note note in cach1.Deleted)
        cach1.SetStatus((object) note, PXEntryStatus.Notchanged);
      cach1.IsDirty = false;
      cach1.Persisted(false);
      foreach (NoteDoc noteDoc in cach2.Deleted)
        cach2.SetStatus((object) noteDoc, PXEntryStatus.Notchanged);
      cach2.IsDirty = false;
    }
    else if (e.TranStatus != PXTranStatus.Open)
    {
      if (e.TranStatus == PXTranStatus.Aborted)
        return;
      if (nullable1.HasValue && nullable1.Value != Guid.Empty)
      {
        foreach (Note note in cach1.Inserted)
        {
          Guid? nullable2 = nullable1;
          Guid? noteId = note.NoteID;
          if ((nullable2.HasValue == noteId.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == noteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          {
            cach1.SetStatus((object) note, PXEntryStatus.Notchanged);
            break;
          }
        }
      }
      foreach (Note note in cach1.Updated)
      {
        Guid? nullable3 = nullable1;
        Guid? noteId = note.NoteID;
        if ((nullable3.HasValue == noteId.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() == noteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          cach1.SetStatus((object) note, PXEntryStatus.Notchanged);
          break;
        }
      }
      foreach (Note note in cach1.Deleted)
        cach1.SetStatus((object) note, PXEntryStatus.Notchanged);
      cach1.IsDirty = false;
      cach1.Persisted(false);
      foreach (NoteDoc noteDoc in cach2.Inserted)
        cach2.SetStatus((object) noteDoc, PXEntryStatus.Notchanged);
      foreach (NoteDoc noteDoc in cach2.Deleted)
        cach2.SetStatus((object) noteDoc, PXEntryStatus.Notchanged);
      cach2.IsDirty = false;
    }
    else
    {
      if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert)
        return;
      string str1 = (string) null;
      if (this.ForceFileCorrection)
        return;
      foreach (NoteDoc noteDoc in cach2.Inserted)
      {
        Guid? nullable4 = noteDoc.FileID;
        if (nullable4.HasValue)
        {
          nullable4 = noteDoc.NoteID;
          Guid? nullable5 = nullable1;
          if ((nullable4.HasValue == nullable5.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          {
            if (str1 == null)
            {
              string screenId = PXContext.GetScreenID();
              PXSiteMapNode screenIdUnsecure;
              if (string.IsNullOrEmpty(screenId) || (screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId.Replace(".", ""))) == null || string.IsNullOrEmpty(screenIdUnsecure.Title))
                break;
              StringBuilder stringBuilder = new StringBuilder(screenIdUnsecure.Title);
              stringBuilder.Append(" (");
              for (int index = 0; index < sender.Keys.Count; ++index)
              {
                if (index > 0)
                  stringBuilder.Append(", ");
                stringBuilder.Append(sender.GetValue(e.Row, sender.Keys[index]));
              }
              stringBuilder.Append(")");
              str1 = stringBuilder.ToString();
            }
            string str2 = (string) null;
            using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<UploadFile>(new PXDataField(typeof (UploadFile.name).Name), (PXDataField) new PXDataFieldValue(typeof (UploadFile.fileID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) noteDoc.FileID)))
            {
              if (pxDataRecord != null)
                str2 = pxDataRecord.GetString(0);
            }
            if (str2 != null)
            {
              Guid? nullable6 = new Guid?();
              int num = str2.IndexOf(")\\");
              if (num > 0)
              {
                using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<NoteDoc>(new PXDataField(typeof (NoteDoc.noteID).Name), (PXDataField) new PXDataFieldValue(typeof (NoteDoc.fileID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) noteDoc.FileID), (PXDataField) new PXDataFieldValue(typeof (NoteDoc.noteID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) nullable1, PXComp.NE)))
                {
                  if (pxDataRecord != null)
                    nullable6 = pxDataRecord.GetGuid(0);
                }
              }
              if (!nullable6.HasValue)
                PXDatabase.Update<UploadFile>((PXDataFieldParam) new PXDataFieldAssign(typeof (UploadFile.name).Name, PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) (num <= str2.IndexOf(" (") ? $"{str1}\\{str2}" : str1 + str2.Substring(num + 1))), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UploadFile.fileID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) noteDoc.FileID), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
            }
          }
        }
      }
    }
  }

  /// <exclude />
  public virtual void noteFilesFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null && this.IsVirtualTable(sender.BqlTable))
    {
      e.ReturnValue = (object) null;
      e.ReturnState = (object) null;
      e.Cancel = true;
    }
    else
    {
      Guid? noteId = new Guid?();
      if (e.Row != null)
        noteId = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      if (noteId.HasValue)
      {
        UploadFileMaintenance.OverrideAccessRights(PXContext.Session, noteId);
        PXContext.SetSlot<string>($"{typeof (Note).FullName}+{noteId}", noteId.ToString());
        int? nullable = sender._GetOriginalCounts((object) (e.Row as IBqlTable)).Item2;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          noteId = new Guid?();
        if (noteId.HasValue)
        {
          List<string> stringList = new List<string>();
          PXView docView = this.GetDocView(sender.Graph);
          object[] objArray = new object[1]
          {
            (object) noteId
          };
          foreach (NoteDoc noteDoc in docView.SelectMulti(objArray))
          {
            Guid? fileId = noteDoc.FileID;
            if (fileId.HasValue)
            {
              fileId = noteDoc.FileID;
              Guid guid = fileId.Value;
              PXResult<UploadFile, UploadFileRevisionNoData> pxResult = (PXResult<UploadFile, UploadFileRevisionNoData>) this.GetFileByID(sender.Graph).SelectSingle((object) guid);
              UploadFile uploadFile = (UploadFile) pxResult;
              UploadFileRevision uploadFileRevision = (UploadFileRevision) (UploadFileRevisionNoData) pxResult;
              if (uploadFile != null && uploadFileRevision != null && !string.IsNullOrEmpty(uploadFile.Name))
              {
                int? size = uploadFileRevision.Size;
                string empty;
                if (!size.HasValue)
                {
                  empty = string.Empty;
                }
                else
                {
                  size = uploadFileRevision.Size;
                  empty = size.Value.ToString();
                }
                string str1 = empty;
                string str2 = uploadFileRevision.Comment != null ? TextUtils.EscapeString(uploadFileRevision.Comment, '@', '$') : string.Empty;
                stringList.Add($"{guid.ToString()}${uploadFile.Name}${str2}${str1}${uploadFileRevision.CreatedDateTime.Value.ToFileTimeUtc().ToString()}");
              }
            }
          }
          if (stringList.Count > 0)
            e.ReturnValue = (object) stringList.ToArray();
          this.SetFilesExists(sender, e.Row, new int?(stringList.Count));
        }
      }
      if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
        return;
      e.ReturnState = (object) PXNoteState.CreateInstance(e.ReturnState, "NoteText", this._FieldName, "NoteText", "NoteFiles", "NoteActivity", "NoteTextExists", "NoteFilesCount");
    }
  }

  /// <exclude />
  public static string UnescapeComment(string comment)
  {
    return TextUtils.UnEscapeString(comment, '@', '$');
  }

  /// <exclude />
  public virtual void noteImagesFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    Guid? nullable1 = new Guid?();
    if (e.Row != null)
      nullable1 = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
    if (nullable1.HasValue)
    {
      int? nullable2 = sender._GetOriginalCounts((object) (e.Row as IBqlTable)).Item2;
      int num = 0;
      if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
        nullable1 = new Guid?();
      if (nullable1.HasValue)
      {
        List<Guid> guidList1 = new List<Guid>();
        List<object> objectList = this.GetDocView(sender.Graph).SelectMulti((object) nullable1);
        if (objectList.Count > 0)
        {
          HashSet<string> stringSet = new HashSet<string>(((IEnumerable<string>) SitePolicy.AllowedImageTypesExt).Select<string, string>((Func<string, string>) (ext => ext.TrimStart('.'))), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          foreach (NoteDoc noteDoc in objectList)
          {
            Guid? fileId = noteDoc.FileID;
            if (fileId.HasValue)
            {
              PXView fileById = this.GetFileByID(sender.Graph);
              object[] objArray = new object[1];
              fileId = noteDoc.FileID;
              objArray[0] = (object) fileId.Value;
              if (fileById.SelectSingle(objArray) is UploadFile uploadFile && !string.IsNullOrEmpty(uploadFile.Name) && (stringSet.Count == 0 || stringSet.Contains(uploadFile.Extansion)))
              {
                List<Guid> guidList2 = guidList1;
                fileId = uploadFile.FileID;
                Guid guid = fileId.Value;
                guidList2.Add(guid);
              }
            }
          }
        }
        if (guidList1.Count > 0)
          e.ReturnValue = (object) guidList1.ToArray();
      }
    }
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXNoteState.CreateInstance(e.ReturnState, "NoteText", this._FieldName, "NoteText", "NoteFiles", "NoteActivity", "NoteTextExists", "NoteFilesCount");
  }

  /// <exclude />
  public virtual void noteFilesFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
    {
      this._FilesRequired = true;
      PXCache cach = sender.Graph.Caches[this._BqlTable];
      if (cach == sender)
        return;
      object newValue = (object) null;
      cach.RaiseFieldUpdating("NoteFiles", (object) null, ref newValue);
    }
    else
    {
      if (!(e.NewValue is Guid[] newValue))
        return;
      Guid? id = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      sender._ResetOriginalCounts((object) (IBqlTable) e.Row, false, true, false);
      if (this._PassThrough || !sender.AllowUpdate && PXNoteAttribute.RowIsUnchanged(sender, e.Row))
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          if (!id.HasValue)
          {
            id = new Guid?(PXNoteAttribute.GenerateId());
            this.InsertNote(sender, id.Value, string.Empty);
            sender.SetValue(e.Row, this._FieldOrdinal, (object) id);
            this.updateTableWithId(sender, e.Row, id);
          }
          this.UpdateNote(sender, id.Value);
          sender.Graph.Caches[typeof (Note)].ClearQueryCache();
          foreach (Guid fileID in newValue)
          {
            PXDatabase.Delete(typeof (NoteDoc), new PXDataFieldRestrict(typeof (NoteDoc.noteID).Name, PXDbType.UniqueIdentifier, (object) id), new PXDataFieldRestrict(typeof (NoteDoc.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID));
            try
            {
              PXDatabase.Insert(typeof (NoteDoc), new PXDataFieldAssign(typeof (NoteDoc.noteID).Name, PXDbType.UniqueIdentifier, (object) id), new PXDataFieldAssign(typeof (NoteDoc.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID), PXDataFieldAssign.OperationSwitchAllowed);
            }
            catch (PXDbOperationSwitchRequiredException ex)
            {
              PXDatabase.Update<NoteDoc>((PXDataFieldParam) new PXDataFieldAssign(typeof (NoteDoc.noteID).Name, PXDbType.UniqueIdentifier, (object) id), (PXDataFieldParam) new PXDataFieldAssign(typeof (NoteDoc.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID), (PXDataFieldParam) new PXDataFieldRestrict(typeof (NoteDoc.noteID).Name, PXDbType.UniqueIdentifier, (object) id), (PXDataFieldParam) new PXDataFieldRestrict(typeof (NoteDoc.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID));
            }
            string screenId = PXContext.GetScreenID();
            if (!string.IsNullOrEmpty(screenId))
              UploadFileMaintenance.SetAccessSource(fileID, new Guid?(), screenId.Replace(".", ""));
          }
          if (newValue.Length != 0 && !PXTimeTagAttribute.SyncScope.IsScoped())
          {
            sender.GetAttributes(e.Row, (string) null).FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBTimestampAttribute));
            foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes(e.Row, (string) null))
            {
              if (attribute is PXTimeTagAttribute)
              {
                Guid? noteId = PXNoteAttribute.GetNoteID(sender, e.Row, (string) null);
                if (noteId.HasValue)
                  PXDatabase.Update<SyncTimeTag>(new List<PXDataFieldParam>()
                  {
                    (PXDataFieldParam) new PXDataFieldAssign<SyncTimeTag.timeTag>(PXDbType.DateTime, (object) System.DateTime.UtcNow),
                    (PXDataFieldParam) new PXDataFieldRestrict<SyncTimeTag.noteID>(PXDbType.UniqueIdentifier, (object) noteId)
                  }.ToArray());
              }
            }
            sender.Graph.Caches[typeof (NoteDoc)].ClearQueryCache();
          }
          transactionScope.Complete();
        }
      }
      else
      {
        if (!id.HasValue && sender.Graph.Caches[typeof (Note)].Insert() is Note note)
        {
          note.NoteText = string.Empty;
          note.EntityType = this.GetEntityType(sender, note.NoteID);
          note.GraphType = this.GetGraphType(sender.Graph);
          id = note.NoteID;
          sender.SetValue(e.Row, this._FieldOrdinal, (object) id);
        }
        if (!id.HasValue)
          return;
        PXCache cach = sender.Graph.Caches[typeof (NoteDoc)];
        PXResultset<NoteDoc> pxResultset = PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select(sender.Graph, (object) id);
        Dictionary<Guid, NoteDoc> dictionary = new Dictionary<Guid, NoteDoc>(pxResultset.Count);
        bool flag = false;
        foreach (PXResult<NoteDoc> pxResult in pxResultset)
        {
          NoteDoc noteDoc = (NoteDoc) pxResult;
          dictionary[noteDoc.FileID.Value] = noteDoc;
        }
        foreach (Guid key in newValue)
        {
          if (!dictionary.ContainsKey(key))
          {
            cach.Insert((object) new NoteDoc()
            {
              NoteID = id,
              FileID = new Guid?(key)
            });
            flag = true;
          }
        }
        if (!flag)
          return;
        sender.MarkUpdated(e.Row);
        sender.IsDirty = true;
      }
    }
  }

  /// <exclude />
  public virtual void noteActivityFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    Guid? refNoteID = new Guid?();
    if (e.Row != null)
      refNoteID = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
    e.ReturnValue = (object) null;
    if (refNoteID.HasValue)
    {
      Guid? nullable = refNoteID;
      Guid empty = Guid.Empty;
      if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) != 0)
      {
        int count = this.ActivityService.GetCount((object) refNoteID);
        e.ReturnValue = (object) count;
        this.SetActivitiesFound(sender, e.Row, new int?(count));
      }
    }
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXNoteState.CreateInstance(e.ReturnState, "NoteText", this._FieldName, "NoteText", "NoteFiles", "NoteActivity", "NoteTextExists", "NoteFilesCount");
  }

  private static void InvalidateCaches(PXGraph graph)
  {
    graph.Caches[typeof (Note)].ClearQueryCache();
  }

  protected virtual void InsertNote(PXCache sender, Guid id, string text = "")
  {
    PXNoteAttribute.InsertNoteRecord(sender.Graph, this.GetGraphType(sender.Graph), this.GetEntityType(sender, new Guid?(id)), id, text);
  }

  /// <exclude />
  public static void InsertNoteRecord(PXCache sender, Guid id, string text = "")
  {
    PXNoteAttribute pxNoteAttribute = sender.GetAttributesReadonly((string) null).OfType<PXNoteAttribute>().FirstOrDefault<PXNoteAttribute>();
    if (pxNoteAttribute != null)
      pxNoteAttribute.InsertNote(sender, id, text);
    else
      PXNoteAttribute.InsertNoteRecord(sender.Graph, PXNoteAttribute.GetGraphType(sender), sender.GetItemType().FullName, id, text);
  }

  /// <exclude />
  private static void InsertNoteRecord(
    PXGraph graph,
    string graphType,
    string entityType,
    Guid id,
    string text = "")
  {
    try
    {
      PXDatabase.Insert(typeof (Note), (PXDataFieldAssign) new PXDataFieldAssign<Note.noteText>(PXDbType.NVarChar, new int?(0), (object) text), (PXDataFieldAssign) new PXDataFieldAssign<Note.entityType>(PXDbType.VarChar, (object) entityType), (PXDataFieldAssign) new PXDataFieldAssign<Note.graphType>(PXDbType.VarChar, (object) graphType), (PXDataFieldAssign) new PXDataFieldAssign<Note.noteID>(PXDbType.UniqueIdentifier, (object) id), PXDataFieldAssign.OperationSwitchAllowed);
    }
    catch (PXVisibiltyUpdateRequiredException ex)
    {
      PXDatabase.Update<Note>((PXDataFieldParam) new PXDataFieldAssign<Note.noteText>(PXDbType.NVarChar, new int?(0), (object) text), (PXDataFieldParam) new PXDataFieldAssign<Note.entityType>(PXDbType.VarChar, (object) entityType), (PXDataFieldParam) new PXDataFieldAssign<Note.graphType>(PXDbType.VarChar, (object) graphType), (PXDataFieldParam) new PXDataFieldRestrict<Note.noteID>(PXDbType.UniqueIdentifier, (object) id));
    }
    PXNoteAttribute.InvalidateCaches(graph);
  }

  protected virtual void UpdateNote(PXCache sender, Guid id, string text = null, string popupText = null)
  {
    PXNoteAttribute.UpdateNoteRecord(sender.Graph, this.GetGraphType(sender.Graph), this.GetEntityType(sender, new Guid?(id)), id, text, popupText);
  }

  [PXInternalUseOnly]
  public static void UpdateNoteRecord(PXCache sender, Guid id, string text = "", string popupText = null)
  {
    PXNoteAttribute pxNoteAttribute = sender.GetAttributesReadonly((string) null).OfType<PXNoteAttribute>().FirstOrDefault<PXNoteAttribute>();
    if (pxNoteAttribute != null)
      pxNoteAttribute.UpdateNote(sender, id, text);
    else
      PXNoteAttribute.UpdateNoteRecord(sender.Graph, PXNoteAttribute.GetGraphType(sender), sender.GetItemType().FullName, id, text, popupText);
  }

  /// <exclude />
  private static void UpdateNoteRecord(
    PXGraph graph,
    string graphType,
    string entityType,
    Guid id,
    string text = null,
    string popupText = null)
  {
    Note note = new Note() { NoteID = new Guid?(id) };
    if (graph.Caches[typeof (Note)].GetStatus((object) note) == PXEntryStatus.Inserted)
      return;
    bool flag = false;
    try
    {
      List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>()
      {
        (PXDataFieldParam) new PXDataFieldAssign<Note.entityType>(PXDbType.VarChar, (object) entityType),
        (PXDataFieldParam) new PXDataFieldAssign<Note.graphType>(PXDbType.VarChar, (object) graphType),
        (PXDataFieldParam) new PXDataFieldRestrict<Note.noteID>(PXDbType.UniqueIdentifier, (object) id),
        (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
      };
      if (text != null)
        pxDataFieldParamList.Insert(0, (PXDataFieldParam) new PXDataFieldAssign<Note.noteText>(PXDbType.NVarChar, (object) text));
      if (popupText != null)
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<Note.notePopupText>(PXDbType.VarChar, (object) popupText));
      if (!PXDatabase.Update(typeof (Note), pxDataFieldParamList.ToArray()))
        flag = true;
    }
    catch (PXInsertSharedRecordRequiredException ex)
    {
      flag = true;
    }
    if (flag)
      PXDatabase.Insert<Note>((PXDataFieldAssign) new PXDataFieldAssign<Note.noteText>(PXDbType.NVarChar, new int?(0), (object) (text ?? "")), (PXDataFieldAssign) new PXDataFieldAssign<Note.entityType>(PXDbType.VarChar, (object) entityType), (PXDataFieldAssign) new PXDataFieldAssign<Note.graphType>(PXDbType.VarChar, (object) graphType), (PXDataFieldAssign) new PXDataFieldAssign<Note.noteID>(PXDbType.UniqueIdentifier, (object) id), (PXDataFieldAssign) new PXDataFieldAssign<Note.notePopupText>(PXDbType.NVarChar, new int?(0), (object) (popupText ?? "")));
    PXNoteAttribute.InvalidateCaches(graph);
  }

  /// <exclude />
  public virtual void noteActivityFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.Row != null)
    {
      Guid? id = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      if (this._PassThrough || !sender.AllowUpdate && PXNoteAttribute.RowIsUnchanged(sender, e.Row))
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          if (!id.HasValue)
          {
            id = new Guid?(PXNoteAttribute.GenerateId());
            this.InsertNote(sender, id.Value, string.Empty);
            sender.SetValue(e.Row, this._FieldOrdinal, (object) id);
            this.updateTableWithId(sender, e.Row, id);
          }
          this.UpdateNote(sender, id.Value);
          transactionScope.Complete();
        }
      }
      else
      {
        if (!id.HasValue && sender.Graph.Caches[typeof (Note)].Insert() is Note note)
        {
          note.NoteText = string.Empty;
          note.EntityType = this.GetEntityType(sender, note.NoteID);
          note.GraphType = this.GetGraphType(sender.Graph);
          id = note.NoteID;
          sender.SetValue(e.Row, this._FieldOrdinal, (object) id);
        }
        if (!id.HasValue)
          return;
        sender.MarkUpdated(e.Row);
        sender.IsDirty = true;
      }
    }
    else
    {
      this._ActivityRequired = true;
      PXCache cach = sender.Graph.Caches[this._BqlTable];
      if (cach == sender)
        return;
      object newValue = (object) null;
      cach.RaiseFieldUpdating("NoteFiles", (object) null, ref newValue);
    }
  }

  /// <summary>Gets or set the field whose value will be displayed as value
  /// in the lookup that selects the related data record for an
  /// activity.</summary>
  public System.Type DescriptionField { get; set; }

  /// <summary>Gets or sets the BQL expression that selects the data records
  /// to be displayed in the pop-up window of the lookup that selects the
  /// related data record for an activity. As the BQL expression, you can
  /// specify a <tt>Search&lt;&gt;</tt> command or just a field. This field,
  /// or the main field of the <tt>Search&lt;&gt;</tt> command, will be the
  /// value that identifies a data record in the activity item.</summary>
  public System.Type Selector { get; set; }

  /// <summary>Gets or set the list of columns that will be displayed in the
  /// pop-up window of the lookup that selects the related data record for
  /// an activity.</summary>
  public System.Type[] FieldList { get; set; }

  public static IEnumerable<System.Type> PXNoteTypes
  {
    get
    {
      System.Type type;
      PropertyInfo[] propertyInfoArray;
      int index1;
      foreach (System.Type table in ServiceManager.Tables)
      {
        type = table;
        propertyInfoArray = type.GetProperties();
        for (index1 = 0; index1 < propertyInfoArray.Length; ++index1)
        {
          PropertyInfo propertyInfo = propertyInfoArray[index1];
          if (propertyInfo.DeclaringType == type && propertyInfo.IsDefined(typeof (PXNoteAttribute), false))
            yield return type;
        }
        propertyInfoArray = (PropertyInfo[]) null;
        type = (System.Type) null;
      }
      System.Type[] typeArray = ((IEnumerable<System.Type>) PXCache.FindExtensionTypes()).Where<System.Type>((Func<System.Type, bool>) (_ => PXCache.IsActiveExtension(_))).ToArray<System.Type>();
      for (index1 = 0; index1 < typeArray.Length; ++index1)
      {
        type = typeArray[index1];
        propertyInfoArray = type.GetProperties();
        for (int index2 = 0; index2 < propertyInfoArray.Length; ++index2)
        {
          PropertyInfo propertyInfo = propertyInfoArray[index2];
          if (propertyInfo.DeclaringType == type && propertyInfo.IsDefined(typeof (PXNoteAttribute), false))
            yield return type;
        }
        propertyInfoArray = (PropertyInfo[]) null;
        type = (System.Type) null;
      }
      typeArray = (System.Type[]) null;
    }
  }

  protected virtual void noteTextCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    this.NoteTextGenericCommandPreparing(sender, e, this._TextRequired, "NoteText");
  }

  protected virtual void notePopupTextCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if (!this.PopupTextEnabled)
      return;
    this.NoteTextGenericCommandPreparing(sender, e, this._PopupTextRequired, "NotePopupText");
  }

  private void NoteTextGenericCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e,
    bool textRequired,
    string noteTextField)
  {
    if (this.GetValueForSubquery(sender, e, textRequired, false) != null)
    {
      Query q = new Query();
      SimpleTable t = new SimpleTable("Note");
      q.Field((SQLExpression) new Column(noteTextField, (Table) t)).From((Table) t).Where(SQLExpressionExt.EQ(new Column("NoteId", (Table) t), (SQLExpression) this.GetExpressionForSubquery(sender, e, this._TextRequired, false)));
      e.Expr = new SubQuery(q).Embrace();
    }
    else
      e.Expr = SQLExpression.Null();
  }

  protected virtual void noteActivitiesCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((this.SuppressActivitiesCount ? 0 : (PXDatabase.Provider.SchemaCache.GetTableHeader("CRActivity") != null ? 1 : 0)) != 0)
    {
      if (this.GetValueForSubquery(sender, e, this._ActivityRequired, true) != null)
      {
        SQLExpression expressionForSubquery = (SQLExpression) this.GetExpressionForSubquery(sender, e, this._ActivityRequired, true);
        Query q = new Query();
        SimpleTable t = new SimpleTable("CRActivity");
        q.Field(SQLExpression.Count()).From((Table) t).Where((this.ActivitiesCountByParent ? SQLExpressionExt.EQ(new Column("BAccountID", (Table) t), expressionForSubquery) : SQLExpressionExt.EQ(new Column("RefNoteID", (Table) t), expressionForSubquery)).And(new Column("UIStatus", (Table) t).NE((object) "CL").And(new Column("UIStatus", (Table) t).NE((object) "RL"))));
        e.Expr = (SQLExpression) new SubQuery(q);
      }
      else
        e.Expr = SQLExpression.Null();
    }
    else
      e.Expr = SQLExpression.Null();
  }

  private YaqlScalar GetValueForSubquery(
    PXCache sender,
    PXCommandPreparingEventArgs e,
    bool fieldRequired,
    bool forCRActivity)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Select & fieldRequired)
    {
      string str = (string) null;
      if ((e.Operation & PXDBOperation.Option) == PXDBOperation.External)
        str = sender.GetItemType().Name;
      if (!this._BqlTable.IsAssignableFrom(sender.BqlTable))
      {
        if (sender.Graph.Caches[this._BqlTable].BqlSelect != null && ((e.Operation & PXDBOperation.Option) == PXDBOperation.External || (e.Operation & PXDBOperation.Option) == PXDBOperation.Select && e.Value == null))
        {
          e.Cancel = true;
          e.DataType = PXDbType.NVarChar;
          e.DataValue = e.Value;
          e.BqlTable = this._BqlTable;
          return (YaqlScalar) Yaql.column(this._DatabaseFieldName, str ?? this._BqlTable.Name);
        }
        PXCommandPreparingEventArgs.FieldDescription description;
        e.Cancel = !sender.Graph.Caches[this._BqlTable].RaiseCommandPreparing(this._DatabaseFieldName, e.Row, e.Value, e.Operation, e.Table, out description);
        if (description != null)
        {
          e.DataType = description.DataType;
          e.DataValue = description.DataValue;
          e.BqlTable = this._BqlTable;
          return Yaql.raw(description.Expr.SQLQuery(e.SqlDialect.GetConnection()).ToString());
        }
      }
      else if ((e.Operation & PXDBOperation.Option) == PXDBOperation.External || (e.Operation & PXDBOperation.Option) == PXDBOperation.Select && e.Value == null)
      {
        e.Cancel = true;
        e.DataType = PXDbType.NVarChar;
        e.DataValue = e.Value;
        e.BqlTable = this._BqlTable;
        return forCRActivity && this.ActivitiesCountByParent ? (YaqlScalar) Yaql.column(EntityHelper.GetIDField(sender), str ?? (e.Table != (System.Type) null ? e.Table.Name : this._BqlTable.Name)) : (YaqlScalar) Yaql.column(this._DatabaseFieldName, str ?? (e.Table != (System.Type) null ? e.Table.Name : this._BqlTable.Name));
      }
    }
    return (YaqlScalar) null;
  }

  private Column GetExpressionForSubquery(
    PXCache sender,
    PXCommandPreparingEventArgs e,
    bool fieldRequired,
    bool forCRActivity)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Select & fieldRequired)
    {
      System.Type type = (System.Type) null;
      if ((e.Operation & PXDBOperation.Option) == PXDBOperation.External)
        type = sender.GetItemType();
      if (!this._BqlTable.IsAssignableFrom(sender.BqlTable))
      {
        if (sender.Graph.Caches[this._BqlTable].BqlSelect != null && ((e.Operation & PXDBOperation.Option) == PXDBOperation.External || (e.Operation & PXDBOperation.Option) == PXDBOperation.Select && e.Value == null))
        {
          e.Cancel = true;
          e.DataType = PXDbType.NVarChar;
          e.DataValue = e.Value;
          e.BqlTable = this._BqlTable;
          string databaseFieldName = this._DatabaseFieldName;
          System.Type dac = type;
          if ((object) dac == null)
            dac = this._BqlTable;
          SimpleTable t = new SimpleTable(dac);
          return new Column(databaseFieldName, (Table) t);
        }
        PXCommandPreparingEventArgs.FieldDescription description;
        e.Cancel = !sender.Graph.Caches[this._BqlTable].RaiseCommandPreparing(this._DatabaseFieldName, e.Row, e.Value, e.Operation, e.Table, out description);
        if (description != null)
        {
          e.DataType = description.DataType;
          e.DataValue = description.DataValue;
          e.BqlTable = this._BqlTable;
          return new Column((description.Expr as Column).Name, (Table) new SimpleTable(this._BqlTable));
        }
      }
      else if ((e.Operation & PXDBOperation.Option) == PXDBOperation.External || (e.Operation & PXDBOperation.Option) == PXDBOperation.Select && e.Value == null)
      {
        e.Cancel = true;
        e.DataType = PXDbType.NVarChar;
        e.DataValue = e.Value;
        e.BqlTable = this._BqlTable;
        if (forCRActivity && this.ActivitiesCountByParent)
        {
          string idField = EntityHelper.GetIDField(sender);
          System.Type dac = type;
          if ((object) dac == null)
            dac = e.Table ?? this._BqlTable;
          SimpleTable t = new SimpleTable(dac);
          return new Column(idField, (Table) t);
        }
        string databaseFieldName = this._DatabaseFieldName;
        System.Type dac1 = type;
        if ((object) dac1 == null)
          dac1 = e.Table ?? this._BqlTable;
        SimpleTable t1 = new SimpleTable(dac1);
        return new Column(databaseFieldName, (Table) t1);
      }
    }
    return (Column) null;
  }

  protected virtual void noteFilesCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (this.GetValueForSubquery(sender, e, this._FilesRequired, false) != null)
    {
      Query q = new Query();
      SimpleTable t = new SimpleTable("NoteDoc");
      q.Field(SQLExpression.Count()).From((Table) t).Where(SQLExpressionExt.EQ(new Column("NoteId", (Table) t), (SQLExpression) this.GetExpressionForSubquery(sender, e, this._FilesRequired, false)));
      e.Expr = new SubQuery(q).Embrace();
    }
    else
      e.Expr = SQLExpression.Null();
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    base.CommandPreparing(sender, e);
    if ((e.Operation & PXDBOperation.Option) != PXDBOperation.Internal || e.Expr == null)
      return;
    e.BqlTable = this._BqlTable;
    e.Expr = (SQLExpression) new NoteIdExpression(e.Expr, this.PopupTextEnabled);
  }

  /// <exclude />
  public void SetFilesExists(PXCache sender, object row, int? count)
  {
    if (!(row is IBqlTable))
      return;
    sender._SetOriginalCounts((object) (IBqlTable) row, (string) null, count, new int?(), (string) null);
  }

  /// <exclude />
  public void SetActivitiesFound(PXCache sender, object row, int? count)
  {
    if (!(row is IBqlTable))
      return;
    sender._SetOriginalCounts((object) (IBqlTable) row, (string) null, new int?(), count, (string) null);
  }

  /// <exclude />
  public void SetNoteTextExists(PXCache sender, object row, string text)
  {
    if (!(row is IBqlTable))
      return;
    sender._SetOriginalCounts((object) (IBqlTable) row, text, new int?(), new int?(), (string) null);
  }

  /// <exclude />
  public void SetPopupNoteTextExists(PXCache sender, object row, string text)
  {
    if (!(row is IBqlTable))
      return;
    sender._SetOriginalCounts((object) (IBqlTable) row, (string) null, new int?(), new int?(), text);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    base.RowSelecting(sender, e);
    if (e.Row != null)
    {
      Guid? nullable = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      string text1 = e.Record.GetString(e.Position);
      ++e.Position;
      int? int32_1 = e.Record.GetInt32(e.Position);
      ++e.Position;
      int? int32_2 = e.Record.GetInt32(e.Position);
      ++e.Position;
      string text2 = (string) null;
      if (this.PopupTextEnabled)
      {
        text2 = e.Record.GetString(e.Position);
        ++e.Position;
      }
      if (!nullable.HasValue || text1 == null && !int32_1.HasValue && (!this.PopupTextEnabled || text2 == null))
        return;
      if (!string.IsNullOrEmpty(text1))
      {
        string[] strArray = text1.Split(sender.Graph.SqlDialect.WildcardFieldSeparatorChar);
        if (strArray.Length != 0)
          text1 = strArray[0];
      }
      else if (text1 == null && (int32_1.HasValue || text2 != null))
        text1 = "";
      if (!string.IsNullOrEmpty(text2))
      {
        string[] strArray = text2.Split(sender.Graph.SqlDialect.WildcardFieldSeparatorChar);
        if (strArray.Length != 0)
          text2 = strArray[0];
      }
      this.SetNoteTextExists(sender, e.Row, text1);
      this.SetFilesExists(sender, e.Row, int32_1);
      this.SetActivitiesFound(sender, e.Row, int32_2);
      if (!this.PopupTextEnabled || text2 == null)
        return;
      this.SetPopupNoteTextExists(sender, e.Row, text2);
    }
    else
      e.Position += this.PopupTextEnabled ? 4 : 3;
  }

  /// <exclude />
  public static void AttachFile(PXCache sender, object data, FileInfo fileInfo)
  {
    string empty = string.Empty;
    string screenId = PXContext.GetScreenID();
    PXSiteMapNode mapNodeByScreenId;
    if (string.IsNullOrEmpty(screenId) || (mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId.Replace(".", ""))) == null || string.IsNullOrEmpty(mapNodeByScreenId.Title))
      return;
    string str = mapNodeByScreenId.Title + "\\" + string.Join<object>(" ", EnumerableExtensions.WhereNotNull<object>(sender.Keys.Select<string, object>((Func<string, object>) (key => sender.GetValue(data, key)))));
    if (string.IsNullOrEmpty(str))
      return;
    PXDatabase.Update<UploadFile>((PXDataFieldParam) new PXDataFieldAssign<UploadFile.name>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) $"{str}\\{fileInfo.Name}"), (PXDataFieldParam) new PXDataFieldRestrict<UploadFile.fileID>(PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) fileInfo.UID.Value));
    sender.SetValueExt(data, "NoteFiles", (object) new Guid[1]
    {
      fileInfo.UID.Value
    });
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    sender._NoteIDOrdinal = new int?(this._FieldOrdinal);
    sender._NoteIDName = this._FieldName;
    base.CacheAttached(sender);
    sender.Graph.Views.Caches.Remove(typeof (Note));
    if (!sender.Graph.Views.RestorableCaches.Contains(typeof (Note)))
      sender.Graph.Views.RestorableCaches.Add(typeof (Note));
    int num1 = sender.Fields.IndexOf(this._FieldName);
    PropertyInfo property1 = sender.GetItemType().GetProperty(this._FieldName);
    if (property1 != (PropertyInfo) null)
    {
      this._declaringType = property1.DeclaringType.FullName;
    }
    else
    {
      foreach (System.Type extensionType in sender.GetExtensionTypes())
      {
        PropertyInfo property2 = extensionType.GetProperty(this._FieldName);
        if (property2 != (PropertyInfo) null)
        {
          System.Type declaringType = property2.DeclaringType;
          if (declaringType.BaseType != (System.Type) null && declaringType.BaseType.IsGenericType)
          {
            this._declaringType = ((IEnumerable<System.Type>) declaringType.BaseType.GetGenericArguments()).Last<System.Type>().FullName;
            break;
          }
          break;
        }
      }
    }
    int num2;
    sender.Fields.Insert(num2 = num1 + 1, "NoteText");
    string lower1 = "NoteText".ToLower();
    sender.FieldSelectingEvents[lower1] += new PXFieldSelecting(this.noteTextFieldSelecting);
    sender.FieldUpdatingEvents[lower1] += new PXFieldUpdating(this.noteTextFieldUpdating);
    sender.CommandPreparingEvents[lower1] += new PXCommandPreparing(this.noteTextCommandPreparing);
    int num3;
    sender.Fields.Insert(num3 = num2 + 1, "NoteFiles");
    string lower2 = "NoteFiles".ToLower();
    sender.FieldSelectingEvents[lower2] += new PXFieldSelecting(this.noteFilesFieldSelecting);
    sender.FieldUpdatingEvents[lower2] += new PXFieldUpdating(this.noteFilesFieldUpdating);
    sender.CommandPreparingEvents[lower2] += new PXCommandPreparing(this.noteFilesCommandPreparing);
    int num4;
    sender.Fields.Insert(num4 = num3 + 1, "NoteImages");
    sender.FieldSelectingEvents["NoteImages".ToLower()] += new PXFieldSelecting(this.noteImagesFieldSelecting);
    int num5;
    sender.Fields.Insert(num5 = num4 + 1, "NoteActivity");
    string lower3 = "NoteActivity".ToLower();
    sender.FieldSelectingEvents[lower3] += new PXFieldSelecting(this.noteActivityFieldSelecting);
    sender.FieldUpdatingEvents[lower3] += new PXFieldUpdating(this.noteActivityFieldUpdating);
    sender.CommandPreparingEvents[lower3] += new PXCommandPreparing(this.noteActivitiesCommandPreparing);
    int num6;
    sender.Fields.Insert(num6 = num5 + 1, "NoteTextExists");
    sender.FieldSelectingEvents["NoteTextExists".ToLower()] += new PXFieldSelecting(this.noteTextExistsFieldSelecting);
    int num7;
    sender.Fields.Insert(num7 = num6 + 1, "NoteFilesCount");
    sender.FieldSelectingEvents["NoteFilesCount".ToLower()] += new PXFieldSelecting(this.noteFilesCountFieldSelecting);
    int num8;
    sender.Fields.Insert(num8 = num7 + 1, "NoteActivitiesCount");
    sender.FieldSelectingEvents["NoteActivitiesCount".ToLower()] += new PXFieldSelecting(this.noteActivitiesCountFieldSelecting);
    if (this.PopupTextEnabled)
    {
      int num9;
      sender.Fields.Insert(num9 = num8 + 1, "NotePopupText");
      string lower4 = "NotePopupText".ToLower();
      sender.FieldSelectingEvents[lower4] += new PXFieldSelecting(this.notePopupTextFieldSelecting);
      sender.FieldUpdatingEvents[lower4] += new PXFieldUpdating(this.notePopupTextFieldUpdating);
      sender.CommandPreparingEvents[lower4] += new PXCommandPreparing(this.notePopupTextCommandPreparing);
      int num10;
      sender.Fields.Insert(num10 = num9 + 1, "NotePopupTextExists");
      sender.FieldSelectingEvents["NotePopupTextExists".ToLower()] += new PXFieldSelecting(this.notePopupTextExistsFieldSelecting);
    }
    this._NoteTextFieldDisplayName = PXMessages.LocalizeNoPrefix("Note Text");
    Select<UploadFile> imagesSelect = new Select<UploadFile>();
    PXView pxView1 = new PXView(sender.Graph, true, (BqlCommand) new Select<UploadFile>(), (Delegate) (() =>
    {
      List<UploadFile> uploadFileList = new List<UploadFile>();
      if (sender.GetStateExt(sender.Current, "NoteImages") is PXNoteState stateExt2 && stateExt2.Value is Guid[] array2)
      {
        PXView pxView2 = new PXView(sender.Graph, true, imagesSelect.WhereNew(InHelper.Create(typeof (UploadFile.fileID), array2.Length)));
        object[] objArray = Array.ConvertAll<Guid, object>(array2, (Converter<Guid, object>) (input => (object) input));
        foreach (UploadFile uploadFile1 in pxView2.SelectMulti(objArray))
        {
          int num11 = string.IsNullOrEmpty(uploadFile1.Name) ? -1 : uploadFile1.Name.LastIndexOf('\\');
          UploadFile uploadFile2 = uploadFile1;
          if (num11 > -1 && num11 < uploadFile1.Name.Length - 1)
          {
            uploadFile2 = (UploadFile) pxView2.Cache.CreateCopy((object) uploadFile1);
            uploadFile2.Name = uploadFile1.Name.Substring(num11 + 1);
          }
          uploadFileList.Add(uploadFile2);
        }
      }
      return (IEnumerable) uploadFileList;
    }));
    string key = "$NoteImages$" + sender.GetItemType().Name;
    sender.Graph.Views.Add(key, pxView1);
    Select2<UploadFile, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>, InnerJoin<UploadFileRevisionNoData, On<UploadFile.fileID, Equal<UploadFileRevisionNoData.fileID>, And<UploadFileRevisionNoData.fileRevisionID, Equal<UploadFile.lastRevisionID>>>>>, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>> select = new Select2<UploadFile, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>, InnerJoin<UploadFileRevisionNoData, On<UploadFile.fileID, Equal<UploadFileRevisionNoData.fileID>, And<UploadFileRevisionNoData.fileRevisionID, Equal<UploadFile.lastRevisionID>>>>>, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>();
    PXView pxView3 = new PXView(sender.Graph, true, (BqlCommand) select);
    sender.Graph.Views.Add("fileListView", pxView3);
  }

  public static void ResetFileListCache(PXCache sender)
  {
    sender.Graph.Views["fileListView"].Cache.ClearQueryCache();
    new PXView(sender.Graph, false, (BqlCommand) new Select<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>()).Cache.ClearQueryCache();
    if (sender.Current == null)
      return;
    sender._ResetOriginalCounts(sender.Current, false, true, false);
  }

  private void noteActivitiesCountFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null && this.IsVirtualTable(sender.BqlTable))
    {
      e.ReturnValue = (object) null;
      e.ReturnState = (object) null;
      e.Cancel = true;
    }
    else
    {
      Guid? id = new Guid?();
      if (e.Row != null)
        id = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      if (!id.HasValue)
        return;
      int? nullable = sender._GetOriginalCounts((object) (e.Row as IBqlTable)).Item3;
      if (!nullable.HasValue && PXDatabase.Provider.SchemaCache.GetTableHeader("CRActivity") != null)
      {
        PXCache sender1 = sender;
        object row = e.Row;
        nullable = new int?(this.GetActivityCount(sender.Graph, id));
        int? count = nullable;
        this.SetActivitiesFound(sender1, row, count);
      }
      e.ReturnValue = (object) nullable.GetValueOrDefault();
    }
  }

  private void noteFilesCountFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null && this.IsVirtualTable(sender.BqlTable))
    {
      e.ReturnValue = (object) null;
      e.ReturnState = (object) null;
      e.Cancel = true;
    }
    else
    {
      Guid? noteID = new Guid?();
      if (e.Row != null)
        noteID = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      if (!noteID.HasValue)
        return;
      int? nullable = sender._GetOriginalCounts((object) (e.Row as IBqlTable)).Item2;
      if (!nullable.HasValue)
      {
        PXView docView = this.GetDocView(sender.Graph);
        int num1;
        if (sender.GetStatus(e.Row) != PXEntryStatus.Inserted || !(e.Row.GetType() != sender.Graph.PrimaryItemType))
          num1 = docView.SelectMulti((object) noteID).Count<object>();
        else
          num1 = PXNoteAttribute.FiltrateCachedButNotDeletedNoteDoc(docView, noteID).Count<NoteDoc>();
        int num2 = num1;
        this.SetFilesExists(sender, e.Row, new int?(num2));
        nullable = sender._GetOriginalCounts((object) (e.Row as IBqlTable)).Item2;
      }
      e.ReturnValue = (object) nullable.GetValueOrDefault();
    }
  }

  private static IEnumerable<NoteDoc> FiltrateCachedButNotDeletedNoteDoc(
    PXView noteDocView,
    Guid? noteID)
  {
    foreach (NoteDoc noteDoc in noteDocView.Cache.Cached.RowCast<NoteDoc>())
    {
      switch (noteDocView.Cache.GetStatus((object) noteDoc))
      {
        case PXEntryStatus.Deleted:
        case PXEntryStatus.InsertedDeleted:
          continue;
        default:
          Guid? noteId = noteDoc.NoteID;
          Guid? nullable = noteID;
          if ((noteId.HasValue == nullable.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          {
            yield return noteDoc;
            continue;
          }
          continue;
      }
    }
  }

  private void noteTextExistsFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    Func<Note, string> getNoteText = (Func<Note, string>) (n => n.NoteText);
    this.NoteTextExistsGenericFieldSelecting(sender, e, this.GetOriginalCountsItem, getNoteText, new Action<PXCache, object, string>(this.SetNoteTextExists));
  }

  private void notePopupTextExistsFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!this.PopupTextEnabled)
      return;
    Func<Note, string> getNoteText = (Func<Note, string>) (n => n.NotePopupText);
    this.NoteTextExistsGenericFieldSelecting(sender, e, this.GetPopupOriginalCountsItem, getNoteText, new Action<PXCache, object, string>(this.SetPopupNoteTextExists));
  }

  private void NoteTextExistsGenericFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    Func<PXCache, IBqlTable, string> getOriginalCountsItem,
    Func<Note, string> getNoteText,
    Action<PXCache, object, string> setNoteTextExists)
  {
    if (e.Row == null && this.IsVirtualTable(sender.BqlTable))
    {
      e.ReturnValue = (object) null;
      e.ReturnState = (object) null;
      e.Cancel = true;
    }
    else
    {
      Guid? id = new Guid?();
      if (e.Row != null)
        id = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      if (!id.HasValue)
        return;
      string str = getOriginalCountsItem(sender, e.Row as IBqlTable);
      if (str == null)
      {
        Note note = sender.Graph.Caches<Note>().Locate(new Note()
        {
          NoteID = id
        });
        if (note == null && sender.GetStatus(e.Row) != PXEntryStatus.Inserted)
          note = this.GetNote(sender.Graph, id);
        string[] strArray = ((note != null ? getNoteText(note) : string.Empty) ?? string.Empty).Split(sender.Graph.SqlDialect.WildcardFieldSeparatorChar);
        setNoteTextExists(sender, e.Row, strArray.Length != 0 ? strArray[0] ?? string.Empty : string.Empty);
        str = getOriginalCountsItem(sender, e.Row as IBqlTable);
      }
      e.ReturnValue = (object) !string.IsNullOrEmpty(str);
    }
  }

  internal static bool IsNoteRelatedField(string field)
  {
    return !string.IsNullOrEmpty(field) && PXNoteAttribute._noteFields.Contains(field);
  }

  /// <exclude />
  public interface IPXCopySettings
  {
    bool? CopyNotes { get; }

    bool? CopyFiles { get; }
  }
}
