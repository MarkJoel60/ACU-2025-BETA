// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>
/// This class allows you to restrict the set of fields on which
/// the <tt>SELECT</tt> operation will be performed to speed up this operation.
/// </summary>
/// <remarks>No <tt>RowSelecting</tt> event (for example, originated from attributes)
/// will be generated for the fields that are not specified in the <tt>PXFieldScope</tt> constructor.</remarks>
/// <example>In the following example, the <tt>SELECT</tt> operation inside the code block
/// will be performed on the <tt>SearchIndex.indexID</tt> and <tt>SearchIndex.entityType</tt>
/// fields only.
/// <code lang="CS">
/// using (new PXFieldScope(selectTopRank.View, new Type[] { typeof(SearchIndex.indexID), typeof(SearchIndex.entityType) }))
/// {
///     return base.ExecuteSelect(viewName, parameters, searches, sortcolumns,
///         descendings, filters, ref startRow, maximumRows, ref totalRows);
/// }
/// </code>
/// </example>
public class PXFieldScope : IDisposable
{
  private PXFieldScope _Previous;
  private readonly List<RestrictedField> _reallyAddedFields = new List<RestrictedField>();
  private readonly bool _previousMainTableRestricted;

  /// <summary>
  /// The set of fields on which the <tt>SELECT</tt> operations are performed.
  /// </summary>
  public RestrictedFieldsSet Fields { get; private set; }

  /// <summary>
  /// The <tt>PXView</tt> object that contains the fields on which
  /// the <tt>SELECT</tt> operations are performed.
  /// </summary>
  /// <remarks> The <tt>RestrictedFields</tt> property of this view contains the fields
  /// specified in the constructor of the <tt>PXFieldScope</tt> object. When the <tt>Dispose</tt>
  /// method is called, the <tt>RestrictedFields</tt> property of the view is cleared of these fields.</remarks>
  public PXView View { get; private set; }

  /// <summary>
  /// Creates a <tt>PXFieldScope</tt> object.
  /// </summary>
  /// <param name="view">The <tt>PXView</tt> object to store the fields
  /// on which the <tt>SELECT</tt> operations will be performed.</param>
  /// <param name="fieldsAndTables">The fields explicitly specified here and the fields
  /// from the tables specified here constitute the set of fields on which
  /// the <tt>SELECT</tt> operations will be performed.</param>
  /// <param name="collectDependencies">Specifies whether the fields that are dependent on
  /// the explicitly listed fields must be taken into account too.</param>
  public PXFieldScope(PXView view, IEnumerable<System.Type> fieldsAndTables, bool collectDependencies = true)
    : this(view, (IReadOnlyCollection<System.Type>) fieldsAndTables.ToArray<System.Type>(), collectDependencies)
  {
  }

  private PXFieldScope(
    PXView view,
    IReadOnlyCollection<System.Type> fieldsAndTables,
    bool collectDependencies)
    : this(view, PXFieldScope.ConvertFields(view, (IEnumerable<System.Type>) fieldsAndTables), collectDependencies)
  {
    this.View.MainTableCanBeRestricted = !fieldsAndTables.Contains<System.Type>(this.View.Cache.GetItemType());
  }

  /// <summary>
  /// Creates a <tt>PXFieldScope</tt> object.
  /// </summary>
  /// <param name="view">The <tt>PXView</tt> object to store the fields
  /// on which the <tt>SELECT</tt> operations will be performed.</param>
  /// <param name="fields">The fields on which to perform the <tt>SELECT</tt> operations.</param>
  /// <param name="collectDependencies">Specifies whether the fields that are dependent on
  /// the explicitly listed fields must be taken into account too.</param>
  public PXFieldScope(PXView view, IEnumerable<string> fields, bool collectDependencies = true)
    : this(view, PXFieldScope.ConvertFields(view, fields), collectDependencies)
  {
  }

  /// <summary>
  /// Creates a <tt>PXFieldScope</tt> object.
  /// </summary>
  /// <param name="view">The <tt>PXView</tt> object to store the fields
  /// on which the <tt>SELECT</tt> operations will be performed.</param>
  /// <param name="fields">The fields on which to perform the <tt>SELECT</tt> operations.</param>
  /// <param name="collectDependencies">Specifies whether the fields that are dependent on
  /// the explicitly listed fields must be taken into account too.</param>
  public PXFieldScope(PXView view, IEnumerable<RestrictedField> fields, bool collectDependencies = true)
  {
    this.View = view != null ? view : throw new ArgumentNullException(nameof (view));
    this._previousMainTableRestricted = this.View.MainTableCanBeRestricted;
    this.Fields = new RestrictedFieldsSet(fields);
    if (fields.Any<RestrictedField>())
      PXFieldScope.CollectKeys(this.Fields, this.View);
    if (collectDependencies)
      PXFieldScope.CollectDependecies(this.Fields, this.View);
    foreach (RestrictedField restrictedField in this.Fields.Where<RestrictedField>((Func<RestrictedField, bool>) (f => !this.View.RestrictedFields.Contains(f))))
      this._reallyAddedFields.Add(restrictedField);
    if (!this._reallyAddedFields.Any<RestrictedField>())
      return;
    this.View.RestrictedFields.AddRange((IEnumerable<RestrictedField>) this._reallyAddedFields);
    this._Previous = PXContext.GetSlot<PXFieldScope>();
    PXContext.SetSlot<PXFieldScope>(this);
  }

  /// <exclude />
  public PXFieldScope(PXView view, params System.Type[] fieldsAndTables)
    : this(view, (IReadOnlyCollection<System.Type>) fieldsAndTables, true)
  {
  }

  /// <summary>
  /// Returns <tt>true</tt> if a fields scope for the <tt>SELECT</tt> operation is set;
  /// otherwise, it returns <tt>false</tt>.
  /// </summary>
  public static bool IsScoped => PXContext.GetSlot<PXFieldScope>() != null;

  private static IEnumerable<RestrictedField> ConvertFields(
    PXView view,
    IEnumerable<System.Type> fieldsAndTables)
  {
    foreach (System.Type fieldsAndTable in fieldsAndTables)
    {
      if (typeof (IBqlTable).IsAssignableFrom(fieldsAndTable))
      {
        foreach (RestrictedField convertField in PXFieldScope.ConvertFields(view, (IEnumerable<System.Type>) view.Graph.Caches[fieldsAndTable].BqlFields))
          yield return convertField;
      }
      else
      {
        System.Type declaringType = fieldsAndTable.DeclaringType;
        System.Type table = declaringType;
        if (!typeof (IBqlTable).IsAssignableFrom(declaringType) && typeof (PXCacheExtension).IsAssignableFrom(declaringType))
        {
          System.Type firstExtensionParent = PXExtensionManager.GetFirstExtensionParent(declaringType);
          if (firstExtensionParent != (System.Type) null)
          {
            System.Type type = ((IEnumerable<System.Type>) firstExtensionParent.GetGenericArguments()).LastOrDefault<System.Type>();
            if ((object) type == null)
              type = table;
            table = type;
          }
        }
        yield return new RestrictedField(table, fieldsAndTable.Name);
      }
    }
  }

  private static IEnumerable<RestrictedField> ConvertFields(PXView view, IEnumerable<string> fields)
  {
    System.Type[] tables = view.GetItemTypes();
    foreach (string field in fields)
    {
      string str = field;
      System.Type table = tables[0];
      int length = str.IndexOf("__", StringComparison.OrdinalIgnoreCase);
      if (length >= 0)
      {
        string tableName = str.Substring(0, length);
        str = str.Substring(length + "__".Length, str.Length - length - "__".Length);
        table = ((IEnumerable<System.Type>) tables).FirstOrDefault<System.Type>((Func<System.Type, bool>) (t => string.Equals(t.Name, tableName, StringComparison.Ordinal)));
      }
      else
      {
        string[] strArray = str.Split('_', 3);
        if (strArray.Length == 3 && view.Cache.Fields.Contains(strArray[0]))
          str = strArray[0];
      }
      if (table != (System.Type) null)
        yield return new RestrictedField(table, str);
    }
  }

  /// <exclude />
  public void Dispose()
  {
    if (!this._reallyAddedFields.Any<RestrictedField>())
      return;
    foreach (RestrictedField reallyAddedField in this._reallyAddedFields)
      this.View.RestrictedFields.Remove(reallyAddedField);
    this.View.MainTableCanBeRestricted = this._previousMainTableRestricted;
    PXContext.SetSlot<PXFieldScope>(this._Previous);
  }

  private static void CollectDependecies(RestrictedFieldsSet restricted, PXView view)
  {
    System.Type itemType = view.GetItemType();
    foreach (System.Type key in restricted.Select<RestrictedField, System.Type>((Func<RestrictedField, System.Type>) (f => f.Table)).Distinct<System.Type>().ToArray<System.Type>())
      PXFieldScope.CollectDependencies(restricted, view.Graph.Caches[key], key == itemType);
  }

  private static void CollectDependencies(
    RestrictedFieldsSet restricted,
    PXCache cache,
    bool collectReportFields)
  {
    System.Type itemType = cache.GetItemType();
    if (collectReportFields)
    {
      foreach (string field in (List<string>) cache.Fields)
      {
        if (!field.Contains("_") && !(cache.GetBqlField(field) == (System.Type) null))
        {
          PropertyInfo property = cache.GetItemType().GetProperty(field);
          if (property != (PropertyInfo) null && property.IsDefined(typeof (IPXReportRequiredField), true))
            restricted.Add(new RestrictedField(itemType, field));
        }
      }
    }
    foreach (string field1 in (List<string>) cache.Fields)
    {
      if (!field1.Contains("_") && !(cache.GetBqlField(field1) == (System.Type) null))
      {
        RestrictedField restrictedField = new RestrictedField(itemType, field1);
        if (restricted.Contains(restrictedField))
        {
          foreach (string field2 in PXDependsOnFieldsAttribute.GetDependsRecursive(cache, field1))
          {
            RestrictedField field3 = new RestrictedField(itemType, field2);
            restricted.Add(field3);
          }
        }
      }
    }
  }

  private static void CollectKeys(RestrictedFieldsSet restricted, PXView view)
  {
    System.Type itemType = view.GetItemType();
    foreach (string key in (IEnumerable<string>) view.Graph.Caches[itemType].Keys)
      restricted.Add(new RestrictedField(itemType, key));
  }

  internal static void SetRestrictedFields(
    PXView view,
    IEnumerable<System.Type> fields,
    bool collectDependencies = true)
  {
    RestrictedFieldsSet restricted = new RestrictedFieldsSet(PXFieldScope.ConvertFields(view, fields));
    if (collectDependencies)
      PXFieldScope.CollectDependecies(restricted, view);
    view.RestrictedFields = restricted;
  }

  internal static void SetRestrictedFields(
    PXView view,
    IEnumerable<string> fields,
    bool collectDependencies = true)
  {
    RestrictedFieldsSet restricted = new RestrictedFieldsSet(PXFieldScope.ConvertFields(view, fields));
    if (collectDependencies)
      PXFieldScope.CollectDependecies(restricted, view);
    view.RestrictedFields = restricted;
  }
}
