// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSearchableAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.BulkInsert.Provider;
using PX.Common;
using PX.Data.Search;
using PX.Data.Update;
using PX.Data.UserRecords;
using PX.DbServices.Model.DataSet;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.PXDataSet;
using Serilog;
using Serilog.Events;
using SerilogTimings.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class PXSearchableAttribute : PXEventSubscriberAttribute
{
  protected int category;
  protected System.Type[] fields;
  protected string titlePrefix;
  protected System.Type[] titleFields;
  protected PXView searchView;
  private readonly Dictionary<System.Type, PXSearchableAttribute.ListAttributeKind> _listAttributeKindByDacField = new Dictionary<System.Type, PXSearchableAttribute.ListAttributeKind>();
  private static object _forLock = new object();

  protected static Regex ComposedFormatArgsRegex { get; } = new Regex("(?<!(?<!\\{)\\{)\\{(?<index>\\d+)(,(?<alignment>\\d+))?(:(?<formatString>[^\\}]+))?\\}(?!\\}(?!\\}))", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

  /// <summary>
  /// The format of the first line in the search result or the Recently Viewed workspace.
  /// Numbers in curly braces reference to the fields listed in <see cref="P:PX.Data.PXSearchableAttribute.Line1Fields" />.
  /// </summary>
  /// <value>A format string.</value>
  /// <example>
  /// <code>
  /// Line1Format = "{0:d}{1}{2}",
  /// </code>
  /// </example>
  public string Line1Format { get; set; }

  /// <summary>
  /// The format of the second line in the search result or the Recently Viewed workspace.
  /// Numbers in curly braces reference to the fields listed in <see cref="P:PX.Data.PXSearchableAttribute.Line2Fields" />.
  /// </summary>
  /// <value>A format string.</value>
  /// <example>
  /// <code>
  /// Line2Format = "{0}",
  /// </code>
  /// </example>
  public string Line2Format { get; set; }

  /// <summary>
  /// The fields that are referenced from <see cref="P:PX.Data.PXSearchableAttribute.Line1Format" />.
  /// </summary>
  /// <value>An array of DAC field types.</value>
  /// <example>
  /// <code>
  /// Line1Fields = new Type[] { typeof(APInvoice.docDate), typeof(APInvoice.status), typeof(APInvoice.invoiceNbr) },
  /// </code>
  /// </example>
  public System.Type[] Line1Fields { get; set; }

  /// <summary>
  /// The fields that are referenced from <see cref="P:PX.Data.PXSearchableAttribute.Line2Format" />.
  /// </summary>
  /// <value>An array of DAC field types.</value>
  /// <example>
  /// <code>
  /// Line2Fields = new Type[] { typeof(APInvoice.docDesc) },
  /// </code>
  /// </example>
  public System.Type[] Line2Fields { get; set; }

  /// <summary>
  /// The list of the fields to be indexed that contain numbers with prefixes, such as <tt>CT00000040</tt>.
  /// </summary>
  /// <remarks>If a field contains a number with a prefix (such as <tt>CT00000040</tt>), searching for <tt>0040</tt>
  /// can pose a problem. All fields that are listed in <tt>NumberFields</tt> get special treatment
  /// and are indexed both with a prefix and without it (that is, as <tt>CT00000040</tt> and <tt>00000040</tt>).</remarks>
  /// <value>An array of DAC field types whose values can contain numbers with prefixes.</value>
  /// <example>
  /// <code>
  /// NumberFields = new Type[] { typeof(APInvoice.refNbr) },
  /// </code>
  /// </example>
  public System.Type[] NumberFields { get; set; }

  /// <summary>
  /// A <tt>Select</tt> BQL command that the system uses to check whether the current user has access rights to the record.
  /// If this command is specified, the record is shown to the user in one of the following cases:
  /// <list>
  /// <item><description>The record was created by the current user.</description></item>
  /// <item><description>The current user matches this BQL command.</description></item>
  /// </list>
  /// </summary>
  /// <value>A <tt>Select</tt> BQL command.</value>
  /// <example>
  /// <code>
  /// SelectDocumentUser = typeof(Select2&lt;Users,
  ///   InnerJoin&lt;EPEmployee, On&lt;Users.pKID, Equal&lt;EPEmployee.userID&gt;&gt;&gt;,
  ///   Where&lt;EPEmployee.bAccountID, Equal&lt;Current&lt;EPExpenseClaimDetails.employeeID&gt;&gt;&gt;&gt;)
  /// </code>
  /// </example>
  public System.Type SelectDocumentUser { get; set; }

  /// <summary>
  /// A constraint that defines whether the given DAC record is searchable.
  /// </summary>
  /// <value>A <tt>Where</tt> clause that defines the constraint.</value>
  /// <example>
  /// <para>The <tt>BAccount</tt> DAC can represent different types of records.
  /// The following example specifies that the system should search in only vendor records and the records that combine vendor and customer.</para>
  /// <code>
  /// WhereConstraint = typeof(Where&lt;Vendor.type.IsIn&lt;BAccountType.vendorType, BAccountType.combinedType&gt;&gt;)
  /// </code>
  /// </example>
  public System.Type WhereConstraint { get; set; }

  /// <summary>
  /// A property that you use to support row-level security and to prevent the display of search results
  /// to a user who does not have access to the corresponding DAC records.
  /// </summary>
  /// <value>A <tt>Join</tt> clause that joins the current DAC with the DAC that has a configured restriction group.</value>
  /// <remarks>In the property, you can specify a join with only one DAC through only one DAC field.</remarks>
  /// <example>
  /// <para>Suppose that the graph that manages <tt>ARInvoice</tt> records
  /// contains the following <see cref="T:PX.Data.Match`2" /> operator in the <tt>Document</tt> data view.</para>
  /// <code>Match&lt;Customer, Current&lt;AccessInfo.userName&gt;&gt;</code>
  /// <para>Therefore, the <tt>MatchWithJoin</tt> property of the <tt>PXSearchable</tt> attribute of the <tt>ARInvoice</tt> DAC
  /// must contain the following join to the <tt>Customer</tt> DAC.</para>
  /// <code>MatchWithJoin = typeof(InnerJoin&lt;Customer, On&lt;Customer.bAccountID, Equal&lt;ARInvoice.customerID&gt;&gt;&gt;),</code>
  /// </example>
  public System.Type MatchWithJoin { get; set; }

  /// <summary>
  /// A request that is used to define the relationship
  /// between the searchable fields and thus to make it possible to rebuild the search index
  /// and to use fields from other DACs in <see cref="P:PX.Data.PXSearchableAttribute.Line1Fields" /> and <see cref="P:PX.Data.PXSearchableAttribute.Line2Fields" />.
  /// </summary>
  /// <value>A <tt>Select</tt> BQL command that joins all additional DACs
  /// that are used during the search index rebuilding so that all searchable fields
  /// are retrieved by a single select request.</value>
  /// <remarks>We recommend that you use this property if the DAC fields in the search result include fields from another DACs.
  /// This approach prevents lazy loading of rows that uses the queries in the <see cref="T:PX.Data.PXSelectorAttribute" /> attribute.</remarks>
  /// <example>
  /// <code>
  /// SelectForFastIndexing = typeof(Select2&lt;GIDesign,
  ///   InnerJoin&lt;SiteMap,
  ///     On&lt;SiteMap.url, Equal&lt;Add&lt;GIUrl.giUrlID, ConvertToStr&lt;GIDesign.designID&gt;&gt;&gt;,
  ///     Or&lt;SiteMap.url, Equal&lt;Add&lt;GIUrl.giUrlName, GIDesign.name&gt;&gt;&gt;&gt;&gt;&gt;)
  /// </code>
  /// </example>
  public System.Type SelectForFastIndexing { get; set; }

  /// <summary>
  /// The information for a complex search result creation regarding retrieval of values for fields
  /// from other DACs that do not have selector attributes declared on them.
  /// </summary>
  private IForeignDacFieldRetrievalInfo[] ForeignDacFieldRetrievalInfos { get; }

  [InjectDependency]
  protected IRecordCachedContentBuilder UserRecordContentBuilder { get; set; }

  [InjectDependency]
  protected ILogger Logger { get; set; }

  private PXSearchableAttribute.UserRecordsUpdater RecordsUpdater { get; set; } = new PXSearchableAttribute.UserRecordsUpdater();

  [PXInternalUseOnly]
  public PXSearchableAttribute(
    int category,
    string titlePrefix,
    System.Type[] titleFields,
    System.Type[] fields,
    System.Type foreignDacFieldRetrievalInfos)
    : this(category, titlePrefix, titleFields, fields)
  {
    ExceptionExtensions.ThrowOnNull<System.Type>(foreignDacFieldRetrievalInfos, nameof (foreignDacFieldRetrievalInfos), (string) null);
    System.Type type = foreignDacFieldRetrievalInfos;
    if ((object) type == null)
      return;
    if (typeof (IForeignDacFieldRetrievalInfo).IsAssignableFrom(type))
    {
      this.ForeignDacFieldRetrievalInfos = new IForeignDacFieldRetrievalInfo[1]
      {
        ExceptionExtensions.CheckIfNull<IForeignDacFieldRetrievalInfo>((IForeignDacFieldRetrievalInfo) Activator.CreateInstance(type), "dacRetrievalInfo", (string) null)
      };
    }
    else
    {
      System.Type c = type;
      if (!typeof (TypeArrayOf<IForeignDacFieldRetrievalInfo>).IsAssignableFrom(c))
        return;
      this.ForeignDacFieldRetrievalInfos = TypeArrayOf<IForeignDacFieldRetrievalInfo>.CheckAndExtractInstances(c, (string) null);
    }
  }

  public PXSearchableAttribute(
    int category,
    string titlePrefix,
    System.Type[] titleFields,
    System.Type[] fields)
  {
    this.category = category;
    this.fields = fields;
    this.titleFields = titleFields;
    this.titlePrefix = titlePrefix;
  }

  /// <summary>
  /// Returns all searchable fields including dependent fields and key fields.
  /// </summary>
  /// <remarks>For example, since <tt>Contact.DisplayName</tt> depends on <tt>FirstName</tt>,
  /// <tt>LastName</tt>, and other fields, all these fields will also be returned.</remarks>
  /// <returns>The method returns all searchable fields.</returns>
  public ICollection<System.Type> GetSearchableFields(PXCache cache)
  {
    ExceptionExtensions.ThrowOnNull<PXCache>(cache, nameof (cache), (string) null);
    HashSet<System.Type> searchableFields = new HashSet<System.Type>();
    foreach (System.Type field in ((IEnumerable<System.Type>) this.titleFields).Union<System.Type>((IEnumerable<System.Type>) this.fields))
    {
      searchableFields.Add(field);
      foreach (System.Type type in PXDependsOnFieldsAttribute.GetDependsRecursive(cache, field.Name).Select<string, System.Type>(new Func<string, System.Type>(cache.GetBqlField)))
        searchableFields.Add(type);
      System.Type itemType = BqlCommand.GetItemType(field);
      foreach (System.Type bqlKey in cache.Graph.Caches[itemType].BqlKeys)
        searchableFields.Add(bqlKey);
    }
    if (this.WhereConstraint != (System.Type) null)
    {
      foreach (System.Type c in BqlCommand.Decompose(this.WhereConstraint))
      {
        if (typeof (IBqlField).IsAssignableFrom(c))
          searchableFields.Add(c);
      }
    }
    return (ICollection<System.Type>) searchableFields;
  }

  /// <summary>
  /// Gets all searchable fields from <see cref="T:PX.Data.PXSearchableAttribute" /> including fields in <see cref="P:PX.Data.PXSearchableAttribute.Line1Fields" />,
  /// <see cref="P:PX.Data.PXSearchableAttribute.Line2Fields" />, and <see cref="P:PX.Data.PXSearchableAttribute.NumberFields" />.
  /// </summary>
  /// <param name="cache">The cache.</param>
  /// <returns>The method returns all searchable fields.</returns>
  public ISet<System.Type> GetAllSearchableFields(PXCache cache)
  {
    ICollection<System.Type> searchableFields = this.GetSearchableFields(cache);
    if (!(searchableFields is HashSet<System.Type> typeSet))
      typeSet = searchableFields.ToHashSet<System.Type>();
    HashSet<System.Type> allSearchableFields = typeSet;
    IEnumerable<System.Type> types = (IEnumerable<System.Type>) this.Line1Fields ?? Enumerable.Empty<System.Type>();
    if (this.Line2Fields != null)
      types = types.Union<System.Type>((IEnumerable<System.Type>) this.Line2Fields);
    if (this.NumberFields != null)
      types = types.Union<System.Type>((IEnumerable<System.Type>) this.NumberFields);
    EnumerableExtensions.ForEach<System.Type>(types.Where<System.Type>((Func<System.Type, bool>) (field => !allSearchableFields.Contains(field))), (System.Action<System.Type>) (field => allSearchableFields.Add(field)));
    return (ISet<System.Type>) allSearchableFields;
  }

  /// <summary>
  /// Gets searchable fields for recently viewed records and favorite records.
  /// </summary>
  internal IReadOnlyCollection<System.Type> GetSearchableFieldsForUserRecords()
  {
    System.Type[] titleFields1 = this.titleFields;
    int length1 = titleFields1 != null ? titleFields1.Length : 0;
    System.Type[] fields = this.fields;
    int length2 = fields != null ? fields.Length : 0;
    int num1 = length1 + length2;
    System.Type[] line1Fields = this.Line1Fields;
    int length3 = line1Fields != null ? line1Fields.Length : 0;
    int num2 = num1 + length3;
    System.Type[] line2Fields = this.Line2Fields;
    int length4 = line2Fields != null ? line2Fields.Length : 0;
    List<System.Type> searchableFields = new List<System.Type>(num2 + length4);
    System.Type[] titleFields2 = this.titleFields;
    if ((titleFields2 != null ? (titleFields2.Length != 0 ? 1 : 0) : 0) != 0)
      searchableFields.AddRange((IEnumerable<System.Type>) this.titleFields);
    AddFields(this.fields);
    AddFields(this.Line1Fields);
    AddFields(this.Line2Fields);
    return (IReadOnlyCollection<System.Type>) searchableFields;

    void AddFields(System.Type[] fieldsArray)
    {
      if (fieldsArray == null || fieldsArray.Length == 0)
        return;
      foreach (System.Type fields in fieldsArray)
      {
        if (!searchableFields.Contains(fields))
          searchableFields.Add(fields);
      }
    }
  }

  private PXSearchableAttribute.ListAttributeKind GetListAttributeKindForField(
    PXCache cache,
    System.Type field)
  {
    lock (((ICollection) this._listAttributeKindByDacField).SyncRoot)
    {
      PXSearchableAttribute.ListAttributeKind attributeKindForField1;
      if (this._listAttributeKindByDacField.TryGetValue(field, out attributeKindForField1))
        return attributeKindForField1;
      PXSearchableAttribute.ListAttributeKind attributeKindForField2 = PXSearchableAttribute.ListAttributeKind.None;
      foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(field.Name))
      {
        if (attribute is PXStringListAttribute stringListAttribute)
        {
          attributeKindForField2 = stringListAttribute.IsLocalizable ? PXSearchableAttribute.ListAttributeKind.Localizable : PXSearchableAttribute.ListAttributeKind.NonLocalizable;
          break;
        }
        if (attribute is PXIntListAttribute intListAttribute)
        {
          attributeKindForField2 = intListAttribute.IsLocalizable ? PXSearchableAttribute.ListAttributeKind.Localizable : PXSearchableAttribute.ListAttributeKind.NonLocalizable;
          break;
        }
      }
      this._listAttributeKindByDacField.Add(field, attributeKindForField2);
      return attributeKindForField2;
    }
  }

  [PXInternalUseOnly]
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.RowPersisting += new PXRowPersisting(this.sender_RowPersisting);
    sender.RowPersisted += new PXRowPersisted(this.sender_RowPersisted);
  }

  [PXInternalUseOnly]
  public virtual bool IsSearchable(PXCache sender, object row)
  {
    if (this.WhereConstraint == (System.Type) null)
      return true;
    this.EnsureSearchView(sender);
    object[] objArray = this.searchView.PrepareParameters(new object[1]
    {
      row
    }, (object[]) null);
    return this.searchView.BqlSelect.Meet(sender, row, objArray);
  }

  protected virtual void EnsureSearchView(PXCache sender)
  {
    if (this.searchView != null)
      return;
    List<System.Type> typeList = new List<System.Type>();
    typeList.Add(typeof (Select<,>));
    typeList.Add(sender.GetItemType());
    typeList.AddRange((IEnumerable<System.Type>) BqlCommand.Decompose(this.WhereConstraint));
    BqlCommand instance = BqlCommand.CreateInstance(typeList.ToArray());
    this.searchView = new PXView(sender.Graph, true, instance);
  }

  private void sender_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    Guid guid = SequentialGuid.Generate();
    sender.SetValue(e.Row, this._FieldOrdinal, (object) guid);
  }

  private void sender_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    Guid? nullable = sender.GetValue(e.Row, this._FieldOrdinal) as Guid?;
    if (nullable.HasValue)
      this.RecordsUpdater.UpdateUserRecordsCachedContentOnRowPersisted(sender.Graph, e, nullable.Value, this.UserRecordContentBuilder, this.Logger);
    if (!this.IsSearchable(sender, e.Row))
      return;
    Dictionary<Guid, SearchIndex> dictionary = PXContext.GetSlot<Dictionary<Guid, SearchIndex>>("SearchIndexSlot");
    if (dictionary == null)
    {
      dictionary = new Dictionary<Guid, SearchIndex>();
      PXContext.SetSlot<Dictionary<Guid, SearchIndex>>("SearchIndexSlot", dictionary);
    }
    SearchIndex record = (SearchIndex) null;
    if (nullable.HasValue)
    {
      dictionary.TryGetValue(nullable.Value, out record);
      if (record == null || !string.Equals(record.EntityType, e.Row.GetType().FullName, StringComparison.OrdinalIgnoreCase))
      {
        Note note = (Note) PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, (object) nullable);
        record = this.BuildSearchIndex(sender, e.Row, (PXResult) null, note?.NoteText);
        dictionary[nullable.Value] = record;
      }
    }
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    if (!nullable.HasValue)
      throw new PXException("SearchIndex cannot be saved. NoteID is required for an entity to be searchable but it has not been supplied.");
    if (e.Operation == PXDBOperation.Delete)
    {
      PXDatabase.Delete(typeof (SearchIndex), new PXDataFieldRestrict(typeof (SearchIndex.noteID).Name, PXDbType.UniqueIdentifier, (object) record.NoteID));
    }
    else
    {
      if ((SearchIndex) PXSelectBase<SearchIndex, PXSelect<SearchIndex, Where<SearchIndex.noteID, Equal<Required<SearchIndex.noteID>>, And<SearchIndex.content, Equal<Required<SearchIndex.content>>, And<SearchIndex.category, Equal<Required<SearchIndex.category>>, And<SearchIndex.entityType, Equal<Required<SearchIndex.entityType>>>>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, (object) record.NoteID, (object) record.Content, (object) record.Category, (object) record.EntityType) != null || PXSearchableAttribute.Update(record))
        return;
      PXSearchableAttribute.Insert(record);
    }
  }

  [PXInternalUseOnly]
  public static bool Insert(SearchIndex record)
  {
    return PXDatabase.Insert(typeof (SearchIndex), new PXDataFieldAssign(typeof (SearchIndex.noteID).Name, PXDbType.UniqueIdentifier, (object) record.NoteID), new PXDataFieldAssign(typeof (SearchIndex.indexID).Name, PXDbType.UniqueIdentifier, (object) record.IndexID), new PXDataFieldAssign(typeof (SearchIndex.category).Name, PXDbType.Int, (object) record.Category), new PXDataFieldAssign(typeof (SearchIndex.entityType).Name, PXDbType.NVarChar, (object) record.EntityType), new PXDataFieldAssign(typeof (SearchIndex.content).Name, PXDbType.NText, (object) record.Content));
  }

  [PXInternalUseOnly]
  public static bool Update(SearchIndex record)
  {
    return PXDatabase.Update(typeof (SearchIndex), (PXDataFieldParam) new PXDataFieldRestrict(typeof (SearchIndex.noteID).Name, PXDbType.UniqueIdentifier, (object) record.NoteID), (PXDataFieldParam) new PXDataFieldAssign(typeof (SearchIndex.category).Name, PXDbType.Int, (object) record.Category), (PXDataFieldParam) new PXDataFieldAssign(typeof (SearchIndex.entityType).Name, PXDbType.NVarChar, (object) record.EntityType), (PXDataFieldParam) new PXDataFieldAssign(typeof (SearchIndex.content).Name, PXDbType.NText, (object) record.Content));
  }

  [PXInternalUseOnly]
  public static bool Delete(SearchIndex record)
  {
    return PXDatabase.Delete(typeof (SearchIndex), new PXDataFieldRestrict(typeof (SearchIndex.noteID).Name, PXDbType.UniqueIdentifier, (object) record.NoteID));
  }

  [PXInternalUseOnly]
  public static void BulkInsert(IEnumerable<SearchIndex> records)
  {
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    PxDataTable pxDataTable = new PxDataTable(dbServicesPoint.Schema.GetTable(typeof (SearchIndex).Name), (IEnumerable<object[]>) null);
    TransferTableTask transferTableTask = new TransferTableTask();
    transferTableTask.Source = (ITableAdapter) new PxDataTableAdapter(pxDataTable);
    transferTableTask.Destination = dbServicesPoint.GetTable(typeof (SearchIndex).Name, FileMode.Open);
    transferTableTask.AppendData = true;
    BatchTransferExecutorSync transferExecutorSync = new BatchTransferExecutorSync((DataTransferObserver) new SimpleDataTransferObserver(), (string) null);
    transferExecutorSync.Tasks.Enqueue(transferTableTask);
    byte[] numArray = new byte[0];
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    int currentCompany = PXInstanceHelper.CurrentCompany;
    foreach (SearchIndex record in records)
      ((PxDataRows) pxDataTable).AddRow(new object[7]
      {
        (object) currentCompany,
        (object) record.NoteID.Value,
        (object) record.IndexID.Value,
        (object) record.EntityType,
        (object) record.Category,
        (object) record.Content,
        (object) numArray
      });
    stopwatch.Restart();
    transferExecutorSync.StartSync();
  }

  [PXInternalUseOnly]
  public virtual SearchIndex BuildSearchIndex(
    PXCache sender,
    object row,
    PXResult res,
    string noteText)
  {
    SearchIndex searchIndex = new SearchIndex();
    searchIndex.IndexID = new Guid?(Guid.NewGuid());
    searchIndex.NoteID = (Guid?) sender.GetValue(row, typeof (Note.noteID).Name);
    searchIndex.Category = new int?(this.category);
    searchIndex.Content = $"{(SearchIndexProfiles.GetSlot().IsProfile(sender.GetItemType()) ? "Acumatica profile: " : "")}{this.BuildContent(sender, row, res)} {noteText}";
    searchIndex.EntityType = this.GetEntityTypeDeclaringSearchAttribute(sender, row).FullName ?? throw new InvalidOperationException("Could not get the type declaring the PXSearchableAttribute attribute for type " + row.GetType().FullName);
    return searchIndex;
  }

  private System.Type GetEntityTypeDeclaringSearchAttribute(PXCache sender, object row)
  {
    for (System.Type c = row.GetType(); typeof (IBqlTable).IsAssignableFrom(c); c = c.BaseType)
    {
      if (c.GetNestedType("noteID") != (System.Type) null && sender.GetAttributesOfType<PXSearchableAttribute>(row, "noteID").Any<PXSearchableAttribute>())
        return c;
    }
    return (System.Type) null;
  }

  [PXInternalUseOnly]
  public virtual PXSearchableAttribute.RecordInfo BuildRecordInfo(PXCache sender, object row)
  {
    List<System.Type> fieldTypes = new List<System.Type>();
    fieldTypes.AddRange((IEnumerable<System.Type>) this.titleFields);
    if (this.Line1Fields != null)
    {
      foreach (System.Type line1Field in this.Line1Fields)
      {
        if (!fieldTypes.Contains(line1Field))
          fieldTypes.Add(line1Field);
      }
    }
    if (this.Line2Fields != null)
    {
      foreach (System.Type line2Field in this.Line2Fields)
      {
        if (!fieldTypes.Contains(line2Field))
          fieldTypes.Add(line2Field);
      }
    }
    Dictionary<System.Type, object> values = this.ExtractValues(sender, row, (PXResult) null, (IEnumerable<System.Type>) fieldTypes);
    List<object> objectList = new List<object>();
    string title = string.Empty;
    if (this.titleFields != null && this.titleFields.Length != 0)
    {
      foreach (System.Type titleField in this.titleFields)
      {
        if (values.ContainsKey(titleField))
          objectList.Add(values[titleField]);
        else
          objectList.Add((object) string.Empty);
      }
    }
    if (this.titlePrefix != null)
    {
      title = string.Format(PXMessages.LocalizeNoPrefix(this.titlePrefix), objectList.ToArray());
      if (title.Trim().EndsWith("-"))
        title = title.Trim().TrimEnd('-');
    }
    List<object> argValues1 = new List<object>();
    List<string> displayNames1 = new List<string>();
    string empty1 = string.Empty;
    if (this.Line1Fields != null && this.Line1Fields.Length != 0)
    {
      for (int index = 0; index < this.Line1Fields.Length; ++index)
      {
        System.Type line1Field = this.Line1Fields[index];
        if (values.ContainsKey(line1Field) && values[line1Field] != null && !string.IsNullOrWhiteSpace(values[line1Field].ToString()))
        {
          string displayName = PXUIFieldAttribute.GetDisplayName(sender.Graph.Caches[BqlCommand.GetItemType(line1Field)], line1Field.Name);
          if (string.IsNullOrWhiteSpace(displayName))
            displayName = line1Field.Name;
          string str = this.OverrideDisplayName(line1Field, displayName);
          argValues1.Add(values[line1Field]);
          displayNames1.Add(str);
        }
        else
        {
          argValues1.Add((object) null);
          displayNames1.Add(string.Empty);
        }
      }
    }
    string line1 = this.BuildFormatedLine(this.Line1Format, argValues1, displayNames1);
    List<object> argValues2 = new List<object>();
    List<string> displayNames2 = new List<string>();
    string empty2 = string.Empty;
    if (this.Line2Fields != null && this.Line2Fields.Length != 0)
    {
      for (int index = 0; index < this.Line2Fields.Length; ++index)
      {
        System.Type line2Field = this.Line2Fields[index];
        if (values.ContainsKey(line2Field) && values[line2Field] != null && !string.IsNullOrWhiteSpace(values[line2Field].ToString()))
        {
          string displayName = PXUIFieldAttribute.GetDisplayName(sender.Graph.Caches[BqlCommand.GetItemType(line2Field)], line2Field.Name);
          if (string.IsNullOrWhiteSpace(displayName))
            displayName = line2Field.Name;
          string str = this.OverrideDisplayName(line2Field, displayName);
          argValues2.Add(values[line2Field]);
          displayNames2.Add(str);
        }
        else
        {
          argValues2.Add((object) null);
          displayNames2.Add(string.Empty);
        }
      }
    }
    string line2 = this.BuildFormatedLine(this.Line2Format, argValues2, displayNames2);
    return new PXSearchableAttribute.RecordInfo(title, line1, line2);
  }

  protected virtual string OverrideDisplayName(System.Type field, string displayName)
  {
    return displayName;
  }

  private string BuildFormatedLine(
    string compositeFormat,
    List<object> argValues,
    List<string> displayNames)
  {
    if (string.IsNullOrEmpty(compositeFormat))
      return string.Empty;
    MatchCollection matchCollection = PXSearchableAttribute.ComposedFormatArgsRegex.Matches(compositeFormat);
    StringBuilder stringBuilder = new StringBuilder(matchCollection.Count * 2 * 16 /*0x10*/ * displayNames.Count);
    object[] array1 = argValues.ToArray();
    object[] array2 = (object[]) displayNames.ToArray();
    for (int i = 0; i < matchCollection.Count && i < argValues.Count; ++i)
    {
      string str1 = string.Format(matchCollection[i].Value, array2);
      string str2 = string.Format(matchCollection[i].Value, array1);
      if (!string.IsNullOrWhiteSpace(str1) && !string.IsNullOrWhiteSpace(str2))
        stringBuilder.AppendFormat("{0}: {1} - ", (object) str1, (object) str2);
    }
    string str = stringBuilder.ToString();
    if (str.Length > 1)
      str = str.Substring(0, str.Length - 3);
    return str;
  }

  [PXInternalUseOnly]
  public virtual string BuildContent(PXCache sender, object row, PXResult res)
  {
    System.Type[] titleFields = this.titleFields;
    int length1 = titleFields != null ? titleFields.Length : 0;
    System.Type[] fields1 = this.fields;
    int length2 = fields1 != null ? fields1.Length : 0;
    List<System.Type> fieldTypesCollection = new List<System.Type>(length1 + length2);
    fieldTypesCollection.AddRange((IEnumerable<System.Type>) this.titleFields);
    System.Type[] fields2 = this.fields;
    if ((fields2 != null ? (fields2.Length != 0 ? 1 : 0) : 0) != 0)
    {
      foreach (System.Type field in this.fields)
      {
        if (!fieldTypesCollection.Contains(field))
          fieldTypesCollection.Add(field);
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    Dictionary<System.Type, object> values = this.ExtractValues(sender, row, res, (IEnumerable<System.Type>) fieldTypesCollection, true);
    List<string> stringList = new List<string>();
    List<object> objectList = new List<object>();
    if (this.titleFields != null && this.titleFields.Length != 0)
    {
      foreach (System.Type titleField in this.titleFields)
      {
        if (values.ContainsKey(titleField))
        {
          object obj = values[titleField];
          objectList.Add(obj);
          if (obj != null && this.NumberFields != null && ((IEnumerable<System.Type>) this.NumberFields).Contains<System.Type>(titleField))
          {
            string strValue = obj.ToString();
            string str = this.RemovePrefix(strValue);
            if (str.Length != strValue.Length)
              stringList.Add(str);
          }
        }
        else
          objectList.Add((object) string.Empty);
      }
    }
    if (this.titlePrefix != null)
      stringBuilder.Append(string.Format(this.titlePrefix, objectList.ToArray()));
    stringBuilder.Append(" ");
    foreach (string str in stringList)
      stringBuilder.AppendFormat("{0} ", (object) str);
    System.Type[] fields3 = this.fields;
    if ((fields3 != null ? (fields3.Length != 0 ? 1 : 0) : 0) != 0)
    {
      foreach (System.Type field in this.fields)
      {
        if (values.ContainsKey(field) && values[field] != null)
        {
          stringBuilder.Append(values[field].ToString());
          stringBuilder.Append(" ");
        }
      }
    }
    return stringBuilder.ToString();
  }

  private string RemovePrefix(string strValue)
  {
    if (string.IsNullOrEmpty(strValue))
      return string.Empty;
    int startIndex = 0;
    for (int index = 0; index < strValue.Length; ++index)
    {
      if (char.IsDigit(strValue[index]) && strValue[index] != '0')
      {
        startIndex = index;
        break;
      }
    }
    return strValue.Substring(startIndex);
  }

  protected virtual object GetFieldValue(
    PXCache sender,
    object row,
    System.Type field,
    bool disableLazyLoading)
  {
    return this.GetFieldValue(sender, row, field, disableLazyLoading, false);
  }

  private object GetFieldValue(
    PXCache sender,
    object row,
    System.Type field,
    bool disableLazyLoading,
    bool buildTranslations)
  {
    bool flag = sender.GetAttributes(field.Name).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr is PXDBLocalizableStringAttribute));
    PXSearchableAttribute.ListAttributeKind listAttributeKind = this.GetListAttributeKindForField(sender, field);
    if (!buildTranslations)
    {
      if (listAttributeKind == PXSearchableAttribute.ListAttributeKind.Localizable)
        listAttributeKind = PXSearchableAttribute.ListAttributeKind.NonLocalizable;
      flag = false;
    }
    if (disableLazyLoading && listAttributeKind == PXSearchableAttribute.ListAttributeKind.None && !flag)
      return sender.GetValue(row, field.Name);
    object stateExt1 = sender.GetStateExt(row, field.Name);
    if (!(stateExt1 is PXFieldState pxFieldState))
      return stateExt1;
    if (flag && sender.GetStateExt(row, field.Name + "Translations") is string[] stateExt2)
      return (object) string.Join(" ", stateExt2);
    object fieldValue = pxFieldState.Value;
    if (pxFieldState is PXIntState pxIntState && pxIntState.AllowedValues != null && pxIntState._NeutralLabels != null)
    {
      int num = System.Math.Min(pxIntState.AllowedValues.Length, System.Math.Min(pxIntState.AllowedLabels.Length, pxIntState._NeutralLabels.Length));
      for (int i = 0; i < num; ++i)
      {
        if (pxIntState.AllowedValues[i] == (int) fieldValue)
        {
          fieldValue = listAttributeKind == PXSearchableAttribute.ListAttributeKind.Localizable ? (object) this.GetAllTranslations(sender, pxIntState.Name, i, pxIntState._NeutralLabels, pxIntState.AllowedLabels) : (object) pxIntState.AllowedLabels[i];
          break;
        }
      }
    }
    else if (pxFieldState is PXStringState pxStringState1 && pxStringState1.AllowedValues != null && pxStringState1._NeutralLabels != null)
    {
      int num = System.Math.Min(pxStringState1.AllowedValues.Length, System.Math.Min(pxStringState1.AllowedLabels.Length, pxStringState1._NeutralLabels.Length));
      for (int i = 0; i < num; ++i)
      {
        if (pxStringState1.AllowedValues[i] == (string) fieldValue)
        {
          fieldValue = listAttributeKind == PXSearchableAttribute.ListAttributeKind.Localizable ? (object) this.GetAllTranslations(sender, pxStringState1.Name, i, pxStringState1._NeutralLabels, pxStringState1.AllowedLabels) : (object) pxStringState1.AllowedLabels[i];
          break;
        }
      }
    }
    if (pxFieldState is PXStringState pxStringState2 && pxStringState2.InputMask == "##-####")
    {
      string str = fieldValue.ToString();
      if (str.Length == 6)
        fieldValue = (object) $"{str.Substring(0, 2)}-{str.Substring(2, 4)}";
    }
    return fieldValue;
  }

  private string GetAllTranslations(
    PXCache sender,
    string field,
    int i,
    string[] neutral,
    string[] theonly)
  {
    PXLocale[] pxLocaleArray = PXContext.GetSlot<PXLocale[]>("SILocales");
    if (pxLocaleArray == null)
      PXContext.SetSlot<PXLocale[]>("SILocales", pxLocaleArray = PXLocalesProvider.GetLocales());
    if (pxLocaleArray.Length <= 1)
      return theonly[i];
    HashSet<string> values = new HashSet<string>();
    foreach (PXLocale pxLocale in pxLocaleArray)
    {
      if (!string.Equals(pxLocale.Name, Thread.CurrentThread.CurrentCulture.Name))
      {
        using (new PXCultureScope(new CultureInfo(pxLocale.Name)))
        {
          string[] allowedLabels = new string[neutral.Length];
          PXLocalizerRepository.ListLocalizer.Localize(field, sender, neutral, allowedLabels);
          if (!string.IsNullOrWhiteSpace(allowedLabels[i]))
            values.Add(allowedLabels[i]);
        }
      }
      else if (!string.IsNullOrWhiteSpace(theonly[i]))
        values.Add(theonly[i]);
    }
    return values.Count > 1 ? string.Join(" ", (IEnumerable<string>) values) : theonly[i];
  }

  public static PXSearchableAttribute GetSearchableAttribute(PXCache dacCache)
  {
    ExceptionExtensions.ThrowOnNull<PXCache>(dacCache, nameof (dacCache), (string) null);
    return !dacCache.Fields.Contains("NoteID") ? (PXSearchableAttribute) null : dacCache.GetAttributesReadonly("NoteID").OfType<PXSearchableAttribute>().FirstOrDefault<PXSearchableAttribute>();
  }

  [PXInternalUseOnly]
  public static List<System.Type> GetAllSearchableEntities(PXGraph graph)
  {
    return EnumerableExtensions.ToList<System.Type>(ServiceManager.TableList.Where<ServiceManager.TypeInfo>((Func<ServiceManager.TypeInfo, bool>) (table => table.Type.GetNestedType("noteID") != (System.Type) null && PXSearchableAttribute.GetSearchableAttribute(graph.Caches[table.Type]) != null)).Select<ServiceManager.TypeInfo, System.Type>((Func<ServiceManager.TypeInfo, System.Type>) (table => table.Type)), 30);
  }

  [PXInternalUseOnly]
  public virtual Dictionary<System.Type, object> ExtractValues(
    PXCache sender,
    object row,
    PXResult res,
    IEnumerable<System.Type> fieldTypes)
  {
    return this.ExtractValues(sender, row, res, fieldTypes, false);
  }

  private Dictionary<System.Type, object> ExtractValues(
    PXCache rowCache,
    object row,
    PXResult res,
    IEnumerable<System.Type> fieldTypesCollection,
    bool buildTranslations)
  {
    Dictionary<System.Type, object> extractedValuesByField = new Dictionary<System.Type, object>();
    Dictionary<System.Type, System.Type> dictionary = new Dictionary<System.Type, System.Type>();
    System.Type type1 = (System.Type) null;
    System.Type itemType1 = rowCache.GetItemType();
    IEnumerable<System.Type> types = fieldTypesCollection;
    System.Type[] titleFields = this.titleFields;
    int length1 = titleFields != null ? titleFields.Length : 0;
    System.Type[] fields = this.fields;
    int length2 = fields != null ? fields.Length : 0;
    int num = length1 + length2;
    List<System.Type> list = EnumerableExtensions.ToList<System.Type>(types, num);
    HashSet<System.Type> hashSet = list.ToHashSet<System.Type>();
    foreach (System.Type type2 in list)
    {
      System.Type itemType2 = BqlCommand.GetItemType(type2);
      if (!(itemType2 == (System.Type) null))
      {
        if (itemType1.IsAssignableFrom(itemType2) || itemType2.IsAssignableFrom(itemType1))
        {
          if (!extractedValuesByField.ContainsKey(type2))
          {
            object fieldValue = this.GetFieldValue(rowCache, row, type2, res != null, buildTranslations);
            extractedValuesByField.Add(type2, fieldValue);
          }
          type1 = type2;
        }
        else if (type1 != (System.Type) null && typeof (IBqlTable).IsAssignableFrom(itemType2))
        {
          object row1 = (object) null;
          if (res != null)
          {
            row1 = res[itemType2];
            if (row1 != null)
            {
              PXCache cach = rowCache.Graph.Caches[row1.GetType()];
              if (!extractedValuesByField.ContainsKey(type2))
              {
                object fieldValue = this.GetFieldValue(cach, row1, type2, false, buildTranslations);
                extractedValuesByField.Add(type2, fieldValue);
              }
            }
          }
          if (row1 == null)
          {
            System.Type type3;
            string str = dictionary.TryGetValue(itemType2, out type3) ? type3.Name : type1.Name;
            object row2 = (object) this.GetForeignDacFromSelectorAttribute(rowCache, row, str);
            if (row2 == null)
            {
              foreach (PXAggregateAttribute aggregateAttribute in rowCache.GetAttributesReadonly(str).OfType<PXAggregateAttribute>())
              {
                PXSelectorAttribute selectorAttribute = aggregateAttribute.GetAttribute<PXDimensionSelectorAttribute>()?.GetAttribute<PXSelectorAttribute>() ?? aggregateAttribute.GetAttribute<PXSelectorAttribute>();
                if (selectorAttribute != null)
                {
                  PXView view = rowCache.Graph.TypedViews.GetView(selectorAttribute.PrimarySelect, !selectorAttribute.DirtyRead);
                  object[] pars = new object[selectorAttribute.ParsCount + 1];
                  pars[pars.Length - 1] = rowCache.GetValue(row, selectorAttribute.FieldOrdinal);
                  object obj = rowCache._InvokeSelectorGetter(row, selectorAttribute.FieldName, view, pars, true);
                  if (obj == null)
                    obj = PXSelectorAttribute.SelectSingleBound(view, new object[2]
                    {
                      row,
                      (object) rowCache.Graph.Accessinfo
                    }, pars);
                  row2 = obj;
                }
              }
            }
            if (row2 is PXResult pxResult)
              row2 = pxResult[0];
            if (row2 != null)
            {
              if (!dictionary.ContainsKey(itemType2))
                dictionary.Add(itemType2, type1);
              PXCache cach = rowCache.Graph.Caches[row2.GetType()];
              if (!extractedValuesByField.ContainsKey(type2))
              {
                object fieldValue = this.GetFieldValue(cach, row2, type2, false, buildTranslations);
                extractedValuesByField.Add(type2, fieldValue);
              }
            }
            else if (this.ForeignDacFieldRetrievalInfos != null && this.ForeignDacFieldRetrievalInfos.Length != 0)
              this.TryAddValueFromForeignDAC(rowCache.Graph, itemType2, type2, extractedValuesByField, hashSet, rowCache, row);
          }
        }
      }
    }
    return extractedValuesByField;
  }

  private IBqlTable GetForeignDacFromSelectorAttribute(
    PXCache rowCache,
    object row,
    string selectorFieldName)
  {
    object foreignKeyValue = rowCache.GetValue(row, selectorFieldName);
    if (foreignKeyValue == null)
      return (IBqlTable) null;
    return rowCache.GetAttributesReadonly(selectorFieldName).OfType<PXSelectorAttribute>().FirstOrDefault<PXSelectorAttribute>()?.GetReferencedDacWithoutSelectorCacheUsage(rowCache, row, foreignKeyValue);
  }

  private bool TryAddValueFromForeignDAC(
    PXGraph graph,
    System.Type foreignDacType,
    System.Type foreignDacField,
    Dictionary<System.Type, object> extractedValuesByField,
    HashSet<System.Type> fieldTypesSet,
    PXCache rowCache,
    object row)
  {
    if (extractedValuesByField.ContainsKey(foreignDacField))
      return true;
    IForeignDacFieldRetrievalInfo dacFieldsRetrievalInfo = ((IEnumerable<IForeignDacFieldRetrievalInfo>) this.ForeignDacFieldRetrievalInfos).FirstOrDefault<IForeignDacFieldRetrievalInfo>((Func<IForeignDacFieldRetrievalInfo, bool>) (info => info.ForeignDac.IsAssignableFrom(foreignDacType)));
    if ((dacFieldsRetrievalInfo != null ? (!((IEnumerable<System.Type>) dacFieldsRetrievalInfo.ForeignDacFields).Contains<System.Type>(foreignDacField) ? 1 : 0) : 1) != 0)
      return false;
    BqlCommand instance = BqlCommand.CreateInstance(dacFieldsRetrievalInfo.Query);
    if (instance == null)
      return false;
    object row1 = new PXView(graph, true, instance).SelectSingle(this.PrepareParametersForForeignDacRetrieval(dacFieldsRetrievalInfo, rowCache, row));
    if (row1 == null)
      return false;
    IBqlTable data = PXResult.Unwrap(row1, foreignDacType);
    if (data == null)
      return false;
    PXCache cach = graph.Caches[foreignDacType];
    foreach (System.Type key in ((IEnumerable<System.Type>) dacFieldsRetrievalInfo.ForeignDacFields).Where<System.Type>((Func<System.Type, bool>) (field => fieldTypesSet.Contains(field))))
    {
      object obj = cach.GetValue((object) data, key.Name);
      extractedValuesByField[key] = obj;
    }
    return true;
  }

  private object[] PrepareParametersForForeignDacRetrieval(
    IForeignDacFieldRetrievalInfo dacFieldsRetrievalInfo,
    PXCache rowCache,
    object row)
  {
    if (dacFieldsRetrievalInfo.RequiredDacFields.Length == 0)
      return Array.Empty<object>();
    object[] objArray = new object[dacFieldsRetrievalInfo.RequiredDacFields.Length];
    for (int index = 0; index < dacFieldsRetrievalInfo.RequiredDacFields.Length; ++index)
    {
      System.Type requiredDacField = dacFieldsRetrievalInfo.RequiredDacFields[index];
      if (requiredDacField.DeclaringType == (System.Type) null || !this.BqlTable.IsAssignableFrom(requiredDacField.DeclaringType) && !requiredDacField.DeclaringType.IsAssignableFrom(this.BqlTable))
      {
        this.Logger.Error<string, string>("{RequiredDacField} is not a DAC field from the DAC {DacType} which declares the PXSearchable attribute", requiredDacField.FullName, this.BqlTable.FullName);
        throw new InvalidOperationException($"{requiredDacField.FullName} is not a DAC field from the DAC {this.BqlTable.FullName} which declares the PXSearchable attribute");
      }
      object obj = rowCache.GetValue(row, requiredDacField.Name);
      objArray[index] = obj;
    }
    return objArray;
  }

  private enum ListAttributeKind
  {
    None,
    NonLocalizable,
    Localizable,
  }

  /// <exclude />
  [DebuggerDisplay("{Title} / {Line1}; {Line2}")]
  public class RecordInfo
  {
    public string Title { get; private set; }

    public string Line1 { get; private set; }

    public string Line2 { get; private set; }

    public RecordInfo(string title, string line1, string line2)
    {
      this.Title = title;
      this.Line1 = line1;
      this.Line2 = line2;
    }
  }

  /// <summary>
  /// A user records updater class. Adds entries to the transaction scope for changed DACs on row persisted event to update user records cached content when transaction completes.
  /// This functionality is integrated with <see cref="T:PX.Data.PXSearchableAttribute" /> because there will be too much overhead to introduce a new attribute.
  /// The functionality of user records is related to the search since user records uses fields declared in <see cref="T:PX.Data.PXSearchableAttribute" /> constructor in DAC
  /// to build information displayed to the user.
  /// </summary>
  [PXInternalUseOnly]
  protected class UserRecordsUpdater
  {
    private const string UserRecordsSlotName = "UserRecordsSlot";

    /// <summary>
    /// Updates row's user records cached content on row persisted event. This functionality is integrated with <see cref="T:PX.Data.PXSearchableAttribute" /> because it will be too much overhead to avoid
    /// introduction of new attribute. The functionality is a bit related since user records uses <see cref="T:PX.Data.PXSearchableAttribute" /> to build information displayed to the user.
    /// </summary>
    /// <param name="screenGraph">The screen graph.</param>
    /// <param name="e">The row persisted event information.</param>
    /// <param name="noteID">Note ID.</param>
    /// <param name="contentBuilder">The cached content builder.</param>
    /// <param name="logger">The logger.</param>
    public void UpdateUserRecordsCachedContentOnRowPersisted(
      PXGraph screenGraph,
      PXRowPersistedEventArgs e,
      Guid noteID,
      IRecordCachedContentBuilder contentBuilder,
      ILogger logger)
    {
      PXDBOperation pxdbOperation = e.Operation.Command();
      if (e.TranStatus != PXTranStatus.Open || !(e.Row is IBqlTable row) || pxdbOperation == PXDBOperation.Insert)
        return;
      System.Type type = row.GetType();
      string cachedContent = e.Operation.Command() != PXDBOperation.Delete ? this.RebuildCachedContentOfUserRecords(screenGraph, noteID, row, type, contentBuilder, logger) : string.Empty;
      DacModificationType? nullable1;
      switch (pxdbOperation)
      {
        case PXDBOperation.Update:
          nullable1 = new DacModificationType?(DacModificationType.Update);
          break;
        case PXDBOperation.Delete:
          nullable1 = new DacModificationType?(DacModificationType.Delete);
          break;
        default:
          nullable1 = new DacModificationType?();
          break;
      }
      DacModificationType? nullable2 = nullable1;
      if (!nullable2.HasValue || PXTransactionScope.AddChangedDacEntryForUserRecordsModification(noteID, type, cachedContent, nullable2.Value))
        return;
      logger.Error<string, Guid>("Failed to add an entry for user records synchronization to the transaction scope for entity {EntityType} {NoteID}", type.FullName, noteID);
    }

    private string RebuildCachedContentOfUserRecords(
      PXGraph screenGraph,
      Guid noteID,
      IBqlTable entity,
      System.Type entityType,
      IRecordCachedContentBuilder contentBuilder,
      ILogger logger)
    {
      (Guid, System.Type) key = (noteID, entityType);
      Dictionary<(Guid, System.Type), string> dictionary = PXContext.GetSlot<Dictionary<(Guid, System.Type), string>>("UserRecordsSlot");
      if (dictionary == null)
      {
        dictionary = new Dictionary<(Guid, System.Type), string>();
        PXContext.SetSlot<Dictionary<(Guid, System.Type), string>>("UserRecordsSlot", dictionary);
      }
      string str;
      if (!dictionary.TryGetValue(key, out str))
      {
        Guid? rootUid = PXTransactionScope.RootUID;
        using (LoggerOperationExtensions.OperationAt(logger, (LogEventLevel) 0, new LogEventLevel?()).Time("Rebuild of the visited record index in DB Transaction {TransactionID} for entity {EntityType} {NoteID}", new object[3]
        {
          (object) rootUid,
          (object) entityType.FullName,
          (object) noteID
        }))
        {
          logger.Verbose<Guid?, string, Guid>("Starting rebuild of the visited record index in DB Transaction {TransactionID} for entity {EntityType} {NoteID}", rootUid, entityType.FullName, noteID);
          str = contentBuilder.BuildCachedContent(screenGraph, entity);
          dictionary[key] = str;
        }
      }
      return str;
    }
  }
}
