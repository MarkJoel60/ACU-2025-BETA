// Decompiled with JetBrains decompiler
// Type: PX.Data.PXStringListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation;
using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using PX.SM;
using PX.Translation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>Sets a drop-down list as the input control for a DAC field.
/// In this control, a user selects from a fixed set of strings or
/// enters a value manually.</summary>
/// <remarks>
/// 	<para>The attribute configures a drop-down list that represents the
/// DAC field in the user interface. You should provide the list of
/// possible string values and the list of the corresponding labels in the
/// attribute constructor.</para>
/// 	<para>You can reconfigure the drop-down list at run time by calling the
/// static methods. You can set a different list of values or labels or
/// extend the list.</para>
/// </remarks>
/// <example><para>The attribute is added to the DAC field definition as follows.</para>
/// 	<code title="Example" lang="CS">
/// [PXStringList(
///     new[] { "N", "P", "I", "F" },
///     new[] { "New", "Prepared", "Processed", "Partially Processed" }
/// )]
/// [PXDefault("N")]
/// public virtual string Status { get; set; }</code>
/// 	<code title="Example2" description="The attribute below obtains the list of values from the provided string." groupname="Example" lang="CS">
/// [PXStringList("Dr.,Miss,Mr,Mrs,Prof.")]
/// public virtual string TitleOfCourtesy { get; set; }</code>
/// 	<code title="Example3" description="The attribute below obtains the lists of values and labels from the provided string. The user will select from Import and Export. While the field will be set to I or E." groupname="Example2" lang="CS">
/// [PXStringList("I;Import,E;Export")]
/// public virtual string TitleOfCourtesy { get; set; }</code>
/// 	<code title="Example4" description="The example below demonstrates an invocation of a PXStringListAttribute static method. The method called in the example will set the new lists of values and labels for all data records in the cache object that the Schedule.Cache variable references. The method will assign the lists to the PXStringList attribute instances attached to the ActionName field." groupname="Example3" lang="CS">
/// List&lt;string&gt; values = new List&lt;string&gt;();
/// List&lt;string&gt; labels = new List&lt;string&gt;();
/// ... // Fill the values and labels lists
/// // Specify as arrays of values and labels of the drop-down list
/// PXStringListAttribute.SetList&lt;AUSchedule.actionName&gt;(
///     Schedule.Cache, null, values.ToArray(), labels.ToArray());</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXBaseListAttribute))]
public class PXStringListAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXLocalizableList
{
  internal const char MultiSelectValueSeparator = ',';
  protected string[] _AllowedValues;
  protected string[] _AllowedLabels;
  protected string[] _DisabledValues;
  protected string[] _DisabledLabels;
  protected string[] _DeletedValues;
  protected string[] _DeletedLabels;
  protected string[] _NeutralAllowedLabels;
  protected bool _ExclusiveValues = true;
  protected string _DatabaseFieldName;
  protected int _ExplicitCnt;
  protected string _Locale;
  protected int _Length;
  private bool _disabledValuesRemoved;

  /// <summary>Gets or sets the value that indicates whether the values and
  /// labels used by the attribute are localizable.</summary>
  public virtual bool IsLocalizable { get; set; }

  /// <summary>
  /// Gets or sets the value that indicates whether the values and
  /// labels real localized
  /// </summary>
  public virtual bool IsLocalized { get; set; }

  public virtual bool SortByValues { get; set; }

  public virtual bool MultiSelect { get; set; }

  /// <summary>Gets or sets the value that indicates whether a user can
  /// input a value not present in the list of allowed values. If
  /// <see langword="true" />, it is prohibited. By default, the property is set to
  /// <see langword="true" />, which means that the user can select only from the
  /// values in the dropdown list.</summary>
  public bool ExclusiveValues
  {
    get => this._ExclusiveValues;
    set => this._ExclusiveValues = value;
  }

  /// <summary>Returns <tt>null</tt> on get. Sets the BQL field representing
  /// the field in BQL queries.</summary>
  public virtual System.Type BqlField
  {
    get => (System.Type) null;
    set
    {
      this._DatabaseFieldName = char.ToUpper(value.Name[0]).ToString() + value.Name.Substring(1);
      if (!value.IsNested)
        return;
      this.BqlTable = BqlCommand.GetItemType(value);
    }
  }

  /// <summary>Gets the dictionary of allowed value-label pairs.</summary>
  public Dictionary<string, string> ValueLabelDic
  {
    get
    {
      if (this._AllowedValues == null || this._AllowedLabels == null)
        return (Dictionary<string, string>) null;
      Dictionary<string, string> valueLabelDic = new Dictionary<string, string>(this._AllowedValues.Length);
      for (int index = 0; index < this._AllowedValues.Length; ++index)
      {
        if (this._AllowedValues[index] != null)
          valueLabelDic.Add(this._AllowedValues[index], this._AllowedLabels[index]);
      }
      return valueLabelDic;
    }
  }

  /// <summary>Retrieves the list of allowed values from cache.</summary>
  /// <param name="cache">The cache object to search for the attributes of <tt>PXStringList</tt> type.</param>
  public string[] GetAllowedValues(PXCache cache)
  {
    if (!this._disabledValuesRemoved)
    {
      this.RemoveDisabledValues(cache.GetType().GetGenericArguments()[0].FullName);
      this._disabledValuesRemoved = true;
    }
    return this._AllowedValues;
  }

  /// <exclude />
  protected PXStringListAttribute.DBStringProperties DBProperties { get; private set; }

  /// <summary>Initializes a new instance with empty lists of possible values
  /// and labels.</summary>
  public PXStringListAttribute()
  {
    this.DBProperties = new PXStringListAttribute.DBStringProperties();
    this.IsLocalizable = true;
    this._AllowedValues = new string[1];
    this._AllowedLabels = new string[1]{ "" };
    this.CreateNeutralLabels();
  }

  /// <summary>Initializes a new instance with the specified lists of
  /// possible values and corresponding labels. The two lists must be of the same
  /// length.</summary>
  /// <param name="allowedValues">The list of values assigned to the field
  /// when a user selects the corresponding labels.</param>
  /// <param name="allowedLabels">The list of labels displayed in the user
  /// interface when a user expands the control.</param>
  public PXStringListAttribute(string[] allowedValues, string[] allowedLabels)
  {
    this.DBProperties = new PXStringListAttribute.DBStringProperties();
    PXStringListAttribute.CheckValuesAndLabels(allowedValues, allowedLabels);
    this.IsLocalizable = true;
    this._AllowedValues = allowedValues;
    this._AllowedLabels = allowedLabels;
    this.CreateNeutralLabels();
  }

  public PXStringListAttribute(
    string[] allowedValues,
    string[] allowedLabels,
    string[] disabledValues = null,
    string[] disabledLabels = null,
    string[] deletedValues = null,
    string[] deletedLabels = null)
    : this(allowedValues, allowedLabels)
  {
    PXStringListAttribute.CheckValuesAndLabels(disabledValues, disabledLabels);
    PXStringListAttribute.CheckValuesAndLabels(deletedValues, deletedLabels);
    this._DisabledValues = disabledValues;
    this._DisabledLabels = disabledLabels;
    this._DeletedValues = deletedValues;
    this._DeletedLabels = deletedLabels;
  }

  /// <summary>Initializes a new instance with the list of possible values
  /// obtained from the provided string. The string should contain either
  /// values separated by a comma, or value-label pairs where the value and
  /// label are separated by a semicolon and different pairs are separated
  /// by a comma. In the first case, labels are set to value
  /// strings.</summary>
  /// <param name="list">The string that contains the list of values or value-
  /// label pairs.</param>
  /// <example>
  /// In the code pieces below, the attribute obtains the list of values from
  /// the provided string. In the second example, the user will select from <i>Import</i>
  /// and <i>Export</i>. While the field will be set to <i>I</i> or <i>E</i>.
  /// <code>
  /// [PXStringList("Dr.,Miss,Mr,Mrs,Prof.")]
  /// public virtual string TitleOfCourtesy { get; set; }
  /// </code>
  /// <code>
  /// [PXStringList("I;Import,E;Export")]
  /// public virtual string TitleOfCourtesy { get; set; }
  /// </code>
  /// </example>
  public PXStringListAttribute(string list)
  {
    this.DBProperties = new PXStringListAttribute.DBStringProperties();
    this.IsLocalizable = true;
    string[] strArray = list.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    this._AllowedValues = new string[strArray.Length];
    this._AllowedLabels = new string[strArray.Length];
    for (int index = 0; index < strArray.Length; ++index)
    {
      int length = strArray[index].IndexOf(';');
      if (length >= 0)
      {
        this._AllowedValues[index] = strArray[index].Substring(0, length);
        this._AllowedLabels[index] = length + 1 >= strArray[index].Length ? this._AllowedValues[index] : strArray[index].Substring(length + 1);
      }
      else
      {
        this._AllowedValues[index] = strArray[index];
        this._AllowedLabels[index] = strArray[index];
      }
    }
    this.CreateNeutralLabels();
  }

  /// <summary>Initializes a new instance with the specified list of
  /// tuples of possible values and corresponding labels. </summary>
  /// <param name="valuesToLabels">The list of tuples. In each tuple,
  /// the first item is a value assigned to the field when a user selects the corresponding label,
  /// the second item is a label displayed in the user interface when a user expands the control.</param>
  protected PXStringListAttribute(params Tuple<string, string>[] valuesToLabels)
    : this(((IEnumerable<Tuple<string, string>>) valuesToLabels).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) valuesToLabels).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>())
  {
  }

  public PXStringListAttribute(
    params (string Value, string Label)[] valuesToLabels)
    : this(((IEnumerable<(string, string)>) valuesToLabels).Select<(string, string), string>((Func<(string, string), string>) (pair => pair.Value)).ToArray<string>(), ((IEnumerable<(string, string)>) valuesToLabels).Select<(string, string), string>((Func<(string, string), string>) (pair => pair.Label)).ToArray<string>())
  {
  }

  [InjectDependencyOnTypeLevel]
  protected IWorkflowService WorkflowService { get; set; }

  internal static string[] SplitMultiSelectValues(string values) => values?.Split(',');

  /// <summary>Retrieves the localized label from the attribute instance that marks the specified field in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of <tt>PXStringList</tt> type.</param>
  /// <param name="row">The data record the method is applied to. If <tt>null</tt>, the method is applied to all data records kept in the cache object.</param>
  /// <typeparam name="TField">The field of the data record.</typeparam>
  public static string GetLocalizedLabel<TField>(PXCache cache, object row) where TField : IBqlField
  {
    return PXMessages.LocalizeNoPrefix(cache.GetAttributesReadonly<TField>(row).OfType<PXStringListAttribute>().Single<PXStringListAttribute>().ValueLabelDic[(string) cache.GetValue<TField>(row)]);
  }

  /// <summary>Retrieves the localized label for the specified value from the attribute instance that marks the specified field in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of <tt>PXStringList</tt> type.</param>
  /// <param name="row">The data record the method is applied to. If <tt>null</tt>, the method is applied to all data records kept in the cache object.</param>
  /// <typeparam name="TField">The field of the data record.</typeparam>
  public static string GetLocalizedLabel<TField>(PXCache cache, object row, string value) where TField : IBqlField
  {
    return PXMessages.LocalizeNoPrefix(cache.GetAttributesReadonly<TField>(row).OfType<PXStringListAttribute>().Single<PXStringListAttribute>().ValueLabelDic[value]);
  }

  /// <summary>Sets the value of the <see cref="P:PX.Data.PXStringListAttribute.IsLocalizable">IsLocalizable</see> property of the attribute instance that marks the specified field in a
  /// particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If <tt>null</tt>, the method is applied to all data records kept in the cache object.</param>
  /// <param name="isLocalizable">A Boolean value that indicates (if set to true) that the attribute labels are localizable.</param>
  /// <typeparam name="Field">The field of the data record.</typeparam>
  public static void SetLocalizable<Field>(PXCache cache, object data, bool isLocalizable) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXStringListAttribute stringListAttribute in cache.GetAttributes<Field>(data).OfType<PXStringListAttribute>())
      stringListAttribute.IsLocalizable = isLocalizable;
  }

  private static void CheckValuesAndLabels(string[] values, string[] labels, string paramName = "allowedLabels")
  {
    if (values == null && labels != null || values != null && labels == null || values != null && labels != null && values.Length != labels.Length)
      throw new PXArgumentException(paramName, "The length of the values array is not equal to the length of labels array.");
  }

  /// <summary>Assigns the possible values and labels from the specified
  /// attribute instance to the attribute instance that marks the specified field in a
  /// particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="listSource">The attribute instance from which the lists
  /// of possible values and labels are obtained.</param>
  /// <typeparam name="Field">The field of the data record.</typeparam>
  public static void SetList<Field>(PXCache cache, object data, PXStringListAttribute listSource) where Field : IBqlField
  {
    PXStringListAttribute.SetList<Field>(cache, data, listSource._AllowedValues, listSource._AllowedLabels);
  }

  /// <summary>Assigns the specified lists of possible values and labels
  /// to the attribute instance that marks the specified field in a
  /// particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="allowedValues">The new list of values.</param>
  /// <param name="allowedLabels">The new list of labels.</param>
  /// <typeparam name="Field">The field of the data record.</typeparam>
  /// <example>
  /// The code below shows how to modify the lists of values and labels
  /// of a drop-down control at run time.
  /// The method sets the new lists of values and labels for all data
  /// records in the cache object that the <tt>Schedule.Cache</tt> variable references. The
  /// method assigns the lists to the <tt>PXStringList</tt> attribute instances
  /// attached to the <tt>ActionName</tt> field.
  /// <code title="" description="" lang="CS">
  /// List&lt;string&gt; values = new List&lt;string&gt;();
  /// List&lt;string&gt; labels = new List&lt;string&gt;();
  /// // Fill the values and labels lists
  /// // Specify as arrays of values and labels of the drop-down list
  /// PXStringListAttribute.SetList&lt;AUSchedule.actionName&gt;(
  ///     Schedule.Cache, null, values.ToArray(), labels.ToArray());</code></example>
  public static void SetList<Field>(
    PXCache cache,
    object data,
    string[] allowedValues,
    string[] allowedLabels)
    where Field : IBqlField
  {
    PXStringListAttribute.CheckValuesAndLabels(allowedValues, allowedLabels);
    if (data == null)
      cache.SetAltered<Field>(true);
    PXStringListAttribute.SetListInternal(cache.GetAttributes<Field>(data).OfType<PXStringListAttribute>(), allowedValues, allowedLabels, cache);
  }

  /// <summary>Assigns the specified list of tuples of possible values and labels to the attribute
  /// instance that marks the field with the specified name in a particular
  /// data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="valuesToLabels">The new list of pairs. In this list,
  /// the first item is a value assigned to the field when a user selects the corresponding labels,
  /// the second item is a label displayed in the user interface when a user expands the control.</param>
  /// <typeparam name="Field">The field of the data record.</typeparam>
  public static void SetList<Field>(
    PXCache cache,
    object data,
    params Tuple<string, string>[] valuesToLabels)
    where Field : IBqlField
  {
    PXStringListAttribute.SetList<Field>(cache, data, ((IEnumerable<Tuple<string, string>>) valuesToLabels).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) valuesToLabels).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>());
  }

  public static void SetList<Field>(
    PXCache cache,
    object data,
    params (string Value, string Label)[] valuesToLabels)
    where Field : IBqlField
  {
    PXStringListAttribute.SetList<Field>(cache, data, ((IEnumerable<(string, string)>) valuesToLabels).Select<(string, string), string>((Func<(string, string), string>) (pair => pair.Value)).ToArray<string>(), ((IEnumerable<(string, string)>) valuesToLabels).Select<(string, string), string>((Func<(string, string), string>) (pair => pair.Label)).ToArray<string>());
  }

  /// <summary>Assigns the specified lists of possible values and labels to the attribute
  /// instance that marks the field with the specified name in a particular
  /// data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="field">The name of the field that is be marked with the
  /// attribute.</param>
  /// <param name="allowedValues">The new list of values.</param>
  /// <param name="allowedLabels">The new list of labels.</param>
  public static void SetList(
    PXCache cache,
    object data,
    string field,
    string[] allowedValues,
    string[] allowedLabels)
  {
    PXStringListAttribute.CheckValuesAndLabels(allowedValues, allowedLabels);
    if (data == null)
      cache.SetAltered(field, true);
    PXStringListAttribute.SetListInternal(cache.GetAttributes(data, field).OfType<PXStringListAttribute>(), allowedValues, allowedLabels, cache);
  }

  /// <summary>Assigns the possible values and labels from the specified
  /// attribute instance to the attribute instance that marks the field with the
  /// specified name in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="field">The name of the field that is marked with the
  /// attribute.</param>
  /// <param name="listSource">The attribute instance from which the lists
  /// of possible values and labels are obtained.</param>
  public static void SetList(
    PXCache cache,
    object data,
    string field,
    PXStringListAttribute listSource)
  {
    PXStringListAttribute.SetList(cache, data, field, listSource._AllowedValues, listSource._AllowedLabels);
  }

  /// <summary>Assigns the specified list of tuples of possible values and labels to the attribute
  /// instance that marks the field with the specified name in a particular
  /// data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="field">The name of the field that is marked with the
  /// attribute.</param>
  /// <param name="valuesToLabels">The new list of pairs. In each pair,
  /// the first item is a value assigned to the field when a user selects the corresponding labels,
  /// the second item is a label displayed in the user interface when a user expands the control.</param>
  public static void SetList(
    PXCache cache,
    object data,
    string field,
    params Tuple<string, string>[] valuesToLabels)
  {
    PXStringListAttribute.SetList(cache, data, field, ((IEnumerable<Tuple<string, string>>) valuesToLabels).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) valuesToLabels).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>());
  }

  public static void SetList(
    PXCache cache,
    object data,
    string field,
    params (string Value, string Label)[] valuesToLabels)
  {
    PXStringListAttribute.SetList(cache, data, field, ((IEnumerable<(string, string)>) valuesToLabels).Select<(string, string), string>((Func<(string, string), string>) (pair => pair.Value)).ToArray<string>(), ((IEnumerable<(string, string)>) valuesToLabels).Select<(string, string), string>((Func<(string, string), string>) (pair => pair.Label)).ToArray<string>());
  }

  protected static void SetListInternal(
    IEnumerable<PXStringListAttribute> attributes,
    string[] allowedValues,
    string[] allowedLabels,
    PXCache cache)
  {
    foreach (PXStringListAttribute attribute in attributes)
    {
      attribute._AllowedValues = (string[]) allowedValues?.Clone();
      attribute._AllowedLabels = (string[]) allowedLabels?.Clone();
      attribute._NeutralAllowedLabels = (string[]) null;
      attribute._disabledValuesRemoved = false;
      attribute.CreateNeutralLabels();
      attribute._disabledValuesRemoved = false;
      if (attribute._AllowedLabels != null)
        attribute.TryLocalize(cache);
    }
  }

  public static void AppendList<Field>(
    PXCache cache,
    object data,
    params (string Value, string Label)[] valuesToLabels)
    where Field : IBqlField
  {
    PXStringListAttribute.AppendList<Field>(cache, data, ((IEnumerable<(string, string)>) valuesToLabels).Select<(string, string), string>((Func<(string, string), string>) (pair => pair.Value)).ToArray<string>(), ((IEnumerable<(string, string)>) valuesToLabels).Select<(string, string), string>((Func<(string, string), string>) (pair => pair.Label)).ToArray<string>());
  }

  /// <summary>In the
  /// attribute instance that marks the specified field in a particular data
  /// record, extends the lists of possible values and labels with the specified lists of possible values and labels.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="allowedValues">The list of values that is appended to the
  /// existing list of values.</param>
  /// <param name="allowedLabels">The list of labels that is appended to the
  /// existing list of labels.</param>
  /// <typeparam name="Field">The field of the data record.</typeparam>
  public static void AppendList<Field>(
    PXCache cache,
    object data,
    string[] allowedValues,
    string[] allowedLabels)
    where Field : IBqlField
  {
    PXStringListAttribute.CheckValuesAndLabels(allowedValues, allowedLabels);
    if (data == null)
      cache.SetAltered<Field>(true);
    PXStringListAttribute.AppendListInternal(cache.GetAttributes<Field>(data).OfType<PXStringListAttribute>(), allowedValues, allowedLabels, cache);
  }

  /// <summary>In the
  /// attribute instance that marks the field with the specified name in a
  /// particular data record, extends the lists of possible values and labels with the specified lists of possible values and labels.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXStringList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="allowedValues">The list of values that is appended to the
  /// existing list of values.</param>
  /// <param name="allowedLabels">The list of labels that is appended to the
  /// existing list of labels.</param>
  public static void AppendList(
    PXCache cache,
    object data,
    string field,
    string[] allowedValues,
    string[] allowedLabels)
  {
    PXStringListAttribute.CheckValuesAndLabels(allowedValues, allowedLabels);
    if (data == null)
      cache.SetAltered(field, true);
    PXStringListAttribute.AppendListInternal(cache.GetAttributes(data, field).OfType<PXStringListAttribute>(), allowedValues, allowedLabels, cache);
  }

  private static void AppendListInternal(
    IEnumerable<PXStringListAttribute> attributes,
    string[] allowedValues,
    string[] allowedLabels,
    PXCache cache)
  {
    foreach (PXStringListAttribute attribute in attributes)
    {
      if (allowedValues == null)
      {
        attribute._AllowedValues = (string[]) null;
        attribute._AllowedLabels = (string[]) null;
        attribute._NeutralAllowedLabels = (string[]) null;
      }
      else
      {
        if (attribute._AllowedValues == null)
        {
          attribute._AllowedValues = allowedValues;
          attribute._AllowedLabels = allowedLabels;
          attribute._NeutralAllowedLabels = (string[]) null;
          attribute.CreateNeutralLabels();
        }
        else
        {
          int length = attribute._AllowedValues.Length;
          Array.Resize<string>(ref attribute._AllowedValues, attribute._AllowedValues.Length + allowedValues.Length);
          Array.Copy((Array) allowedValues, 0, (Array) attribute._AllowedValues, length, allowedValues.Length);
          Array.Resize<string>(ref attribute._AllowedLabels, attribute._AllowedValues.Length);
          Array.Copy((Array) allowedLabels, 0, (Array) attribute._AllowedLabels, length, allowedLabels.Length);
          Array.Resize<string>(ref attribute._NeutralAllowedLabels, attribute._AllowedValues.Length);
          Array.Copy((Array) allowedLabels, 0, (Array) attribute._NeutralAllowedLabels, length, allowedLabels.Length);
        }
        attribute.TryLocalize(cache);
      }
    }
  }

  internal static void SetExclusiveValues<Field>(PXCache cache, object data, bool exclusiveValues) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    PXStringListAttribute.SetExclusiveValuesInternal(cache.GetAttributes<Field>(data).OfType<PXStringListAttribute>(), exclusiveValues, cache);
  }

  internal static void SetExclusiveValues(
    PXCache cache,
    object data,
    string field,
    bool exclusiveValues)
  {
    if (data == null)
      cache.SetAltered(field, true);
    PXStringListAttribute.SetExclusiveValuesInternal(cache.GetAttributes(data, field).OfType<PXStringListAttribute>(), exclusiveValues, cache);
  }

  private static void SetExclusiveValuesInternal(
    IEnumerable<PXStringListAttribute> attributes,
    bool exclusiveValues,
    PXCache cache)
  {
    foreach (PXStringListAttribute attribute in attributes)
      attribute.ExclusiveValues = exclusiveValues;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    string name = Thread.CurrentThread.CurrentCulture.Name;
    if (this._Locale != null && !string.Equals(this._Locale, name))
    {
      this._Locale = name;
      this.TryLocalize(sender);
    }
    if (!this._disabledValuesRemoved)
    {
      this.RemoveDisabledValues(sender.GetItemType().FullName);
      this._disabledValuesRemoved = true;
    }
    bool deleted = false;
    bool disabled = false;
    object returnValue = e.ReturnValue;
    string[] strArray1;
    if (returnValue == null)
      strArray1 = (string[]) null;
    else
      strArray1 = returnValue.ToString().Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
    string[] strArray2 = strArray1;
    Dictionary<string, string> extraItems = new Dictionary<string, string>();
    if (strArray2 != null)
    {
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (this._AllowedValues == null || !((IEnumerable<string>) this._AllowedValues).Contains<string>(strArray2[index]))
        {
          if (this._DeletedValues != null && ((IEnumerable<string>) this._DeletedValues).Contains<string>(strArray2[index]))
            deleted = true;
          else if (this._DisabledValues != null && ((IEnumerable<string>) this._DisabledValues).Contains<string>(strArray2[index]))
            disabled = true;
        }
      }
    }
    if (this._DeletedValues != null)
    {
      for (int index = 0; index < this._DeletedValues.Length; ++index)
      {
        string[] allowedValues = this._AllowedValues;
        if (!(allowedValues != null ? new bool?(((IEnumerable<string>) allowedValues).Contains<string>(this._DeletedValues[index])) : new bool?()).Value)
          extraItems[this._DeletedValues[index]] = this._DeletedLabels[index];
      }
    }
    if (this._DisabledValues != null)
    {
      for (int index = 0; index < this._DisabledValues.Length; ++index)
      {
        string[] allowedValues = this._AllowedValues;
        if (!(allowedValues != null ? new bool?(((IEnumerable<string>) allowedValues).Contains<string>(this._DisabledValues[index])) : new bool?()).Value)
          extraItems[this._DisabledValues[index]] = this._DisabledLabels[index];
      }
    }
    e.ReturnState = (object) this.CreateFieldStringState(e, extraItems, deleted, disabled);
  }

  private PXStringState CreateFieldStringState(
    PXFieldSelectingEventArgs e,
    Dictionary<string, string> extraItems,
    bool deleted,
    bool disabled)
  {
    string[] allowedValues = this._AllowedValues;
    PXStringState instance = (PXStringState) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(), new int?(-1), (string) null, allowedValues, this._AllowedLabels, new bool?(this._ExclusiveValues), (string) null, this._NeutralAllowedLabels);
    instance.MultiSelect = this.MultiSelect;
    if (deleted | disabled && e.Row != null)
    {
      string str = "The record has been deleted.";
      instance.Error = str;
      instance.ErrorLevel = PXErrorLevel.Warning;
    }
    if (extraItems.Any<KeyValuePair<string, string>>())
      instance.ExtraItems = extraItems;
    if (!this.DBProperties.IsSet)
      this.DBProperties.Fill(this._BqlTable, this._DatabaseFieldName);
    if (this.DBProperties.IsSet)
    {
      bool? nullable = this.DBProperties.Nullable;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        instance.EmptyPossible = true;
    }
    return instance;
  }

  private SQLExpression GetConditionForValues(string[] allowedValues, SQLExpression fieldExpr)
  {
    SQLSwitch conditionForValues = new SQLSwitch();
    for (int index = 0; index < allowedValues.Length; ++index)
    {
      string allowedValue = allowedValues[index];
      SQLExpression when = allowedValue == null ? fieldExpr?.IsNull() : SQLExpressionExt.EQ(fieldExpr, (SQLExpression) new SQLConst((object) allowedValue));
      SQLConst then = new SQLConst((object) this._AllowedLabels[index]);
      if (this.IsLocalized)
        then.SetDBType(PXDbType.NVarChar);
      conditionForValues.Case(when, (SQLExpression) then);
    }
    return (SQLExpression) conditionForValues;
  }

  private bool AllowedValuesAreFilled(string[] allowedValues)
  {
    return allowedValues.Length != 0 && !((IEnumerable<string>) allowedValues).All<string>((Func<string, bool>) (val => val == null));
  }

  /// <exclude />
  public virtual void OrderByCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.External) != PXDBOperation.External || e.Value != null || this._AllowedValues.Length == 0)
      return;
    string[] allowedValues = this._AllowedValues;
    try
    {
      this._AllowedValues = new string[0];
      PXCommandPreparingEventArgs.FieldDescription description;
      sender.RaiseCommandPreparing(this._FieldName, e.Row, e.Value, e.Operation, e.Table, out description);
      if (description == null || description.Expr == null)
        return;
      SQLExpression fieldExpr = description.Expr;
      if (sender.BqlSelect != null && fieldExpr is Column column && !column.Name.Equals(this._FieldName, StringComparison.OrdinalIgnoreCase))
      {
        fieldExpr = column.Duplicate();
        ((Column) fieldExpr).Name = this._FieldName;
      }
      e.Cancel = true;
      e.Expr = this.AllowedValuesAreFilled(allowedValues) ? this.GetConditionForValues(allowedValues, fieldExpr) : fieldExpr;
      e.BqlTable = description.BqlTable;
    }
    finally
    {
      this._AllowedValues = allowedValues;
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this._DatabaseFieldName == null)
      this._DatabaseFieldName = this._FieldName;
    if (this._AllowedLabels != null)
    {
      string[] allowedLabels = this._AllowedLabels;
      this._AllowedLabels = new string[allowedLabels.Length];
      Array.Copy((Array) allowedLabels, 0, (Array) this._AllowedLabels, 0, allowedLabels.Length);
    }
    IWorkflowService workflowService = this.WorkflowService;
    if ((workflowService != null ? (workflowService.ApplyComboBoxValues(sender.Graph, sender.GetItemType().FullName, this._DatabaseFieldName, ref this._AllowedValues, ref this._AllowedLabels) ? 1 : 0) : 0) != 0)
      this._ExplicitCnt = 0;
    this.AppendNeutral();
    this._Locale = Thread.CurrentThread.CurrentCulture.Name;
    this.TryLocalize(sender);
    if (!this.SortByValues)
      sender.CommandPreparingEvents[this._FieldName.ToLower()] += new PXCommandPreparing(this.OrderByCommandPreparing);
    if (!this.MultiSelect)
      return;
    foreach (PXDBStringAttribute pxdbStringAttribute in sender.GetAttributesReadonly(this._FieldName).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr is PXDBStringAttribute)).Cast<PXDBStringAttribute>())
      this._Length = pxdbStringAttribute.Length;
    if (this._Length <= 0)
      return;
    sender.FieldUpdatingEvents[this._FieldName.ToLower()] += new PXFieldUpdating(this.MultiSelectFieldUpdating);
  }

  protected void MultiSelectFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string && ((string) e.NewValue).TrimEnd().Length > this._Length)
      throw new PXSetPropertyException("The string value provided exceeds the field '{0}' length.", new object[1]
      {
        (object) $"[{this._FieldName}]"
      });
  }

  internal void RemoveDisabledValues(string cacheName)
  {
    if (this._AllowedLabels == null || this._AllowedValues == null || !(this._BqlTable != (System.Type) null) || this._DatabaseFieldName == null)
      return;
    IEnumerable<Tuple<string, string>> source = ((IEnumerable<string>) this._AllowedValues).Zip<string, string, Tuple<string, string>>((IEnumerable<string>) this._AllowedLabels, (Func<string, string, Tuple<string, string>>) ((v, l) => new Tuple<string, string>(v, l))).Where<Tuple<string, string>>((Func<Tuple<string, string>, bool>) (t => !PXAccess.IsStringListValueDisabled(cacheName, this._DatabaseFieldName, t.Item1)));
    this._AllowedValues = source.Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (p => p.Item1)).ToArray<string>();
    this._AllowedLabels = source.Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (p => p.Item2)).ToArray<string>();
  }

  private void AppendNeutral()
  {
    if (this._AllowedLabels == null || this._NeutralAllowedLabels != null && this._NeutralAllowedLabels.Length == this._AllowedLabels.Length && EnumerableExtensions.GetHashCodeOfSequence((IEnumerable) this._NeutralAllowedLabels) == EnumerableExtensions.GetHashCodeOfSequence((IEnumerable) this._AllowedLabels))
      return;
    this._NeutralAllowedLabels = new string[this._AllowedLabels.Length];
    this._AllowedLabels.CopyTo((Array) this._NeutralAllowedLabels, 0);
  }

  protected virtual void TryLocalize(PXCache sender)
  {
    if (!this.IsLocalizable)
      return;
    if (ResourceCollectingManager.IsStringCollecting)
      PXPageRipper.RipList(this.FieldName, sender, this._NeutralAllowedLabels, CollectResourceSettings.Resource);
    else if (!PXInvariantCultureScope.IsSet())
    {
      PXDictionaryManager slot = PXContext.GetSlot<PXDictionaryManager>();
      this._AllowedLabels = sender.Graph.Prototype.Memoize<string[]>((Func<string[]>) (() =>
      {
        PXLocalizerRepository.ListLocalizer.Localize(this.FieldName, sender, this._NeutralAllowedLabels, this._AllowedLabels);
        return this._AllowedLabels;
      }), (object) sender.GetItemType(), (object) this.FieldName, (object) Thread.CurrentThread.CurrentCulture.Name, (object) EnumerableExtensions.GetHashCodeOfSequence((IEnumerable) this._NeutralAllowedLabels), (object) (slot == null ? 0 : slot.GetHashCode()));
    }
    this.IsLocalized = EnumerableExtensions.GetHashCodeOfSequence((IEnumerable) this._NeutralAllowedLabels) != EnumerableExtensions.GetHashCodeOfSequence((IEnumerable) this._AllowedLabels);
  }

  protected virtual void RipDynamicLabels(string[] dynamicAllowedLabels, PXCache sender)
  {
    if (!this.IsLocalizable || !ResourceCollectingManager.IsStringCollecting)
      return;
    PXPageRipper.RipList(this.FieldName, sender, dynamicAllowedLabels, CollectResourceSettings.Resource);
  }

  private void CreateNeutralLabels()
  {
    if (this._AllowedLabels == null || this._AllowedLabels.Length == 0 || this._NeutralAllowedLabels != null)
      return;
    this._NeutralAllowedLabels = new string[this._AllowedLabels.Length];
    this._AllowedLabels.CopyTo((Array) this._NeutralAllowedLabels, 0);
  }

  /// <summary>Pairs the value with its label.</summary>
  /// <exclude />
  /// <param name="value">The value.</param>
  /// <param name="label">The label.</param>
  protected static Tuple<string, string> Pair(string value, string label)
  {
    return Tuple.Create<string, string>(value, label);
  }

  protected class DBStringProperties
  {
    public bool? _nullable;
    private ReaderWriterLock _sync = new ReaderWriterLock();

    public bool? Nullable
    {
      get
      {
        PXReaderWriterScope readerWriterScope;
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          return this._nullable;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }

    public void Fill(System.Type table, string field)
    {
      PXReaderWriterScope readerWriterScope;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
        if (this._nullable.HasValue)
          return;
        ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
        if (this._nullable.HasValue)
          return;
        if (table == (System.Type) null)
        {
          this._nullable = new bool?(true);
        }
        else
        {
          try
          {
            TableHeader tableStructure = PXDatabase.Provider.GetTableStructure(table.Name);
            if (tableStructure != null)
            {
              TableColumn tableColumn = tableStructure.Columns.FirstOrDefault<TableColumn>((Func<TableColumn, bool>) (c => string.Equals(((TableEntityBase) c).Name, field, StringComparison.OrdinalIgnoreCase)));
              if (tableColumn != null)
                this._nullable = new bool?(tableColumn.IsNullable);
              else
                this._nullable = new bool?(true);
            }
            else
              this._nullable = new bool?(true);
          }
          catch
          {
          }
        }
      }
      finally
      {
        readerWriterScope.Dispose();
      }
    }

    public bool IsSet
    {
      get
      {
        PXReaderWriterScope readerWriterScope;
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          return this._nullable.HasValue;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }
  }
}
