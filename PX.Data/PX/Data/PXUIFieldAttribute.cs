// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIFieldAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.DacDescriptorGeneration;
using PX.SM;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data;

/// <summary>Configures the properties of the input control that corresponds to a DAC field in the user interface,
/// or of the button that corresponds to an action. The attribute is mandatory
/// for all DAC fields that are displayed in the user interface.</summary>
/// <remarks>
///   <para>You can use the static methods (such as <tt>SetEnabeled</tt>, <tt>SetRequired</tt>)
///   to set the properties of the attribute at run time. The <tt>PXUIFieldAttribute</tt> static methods
///   can be called either in the graph constructor or in the <tt>RowSelected</tt> event handlers.</para>
///   <para>If you want to modify the <see cref="P:PX.Data.PXUIFieldAttribute.Visible" />, <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" />, or <see cref="P:PX.Data.PXUIFieldAttribute.Required" />
///   properties for all rows in a grid, you use the <tt>RowSelected</tt> event handler of the primary view DAC.
///   If you wan to set the <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property of a field in a particular row in the grid,
///   you use the <tt>RowSelected</tt> event handler of the DAC that includes this field.</para>
///   <para>If the grid column layout is configured at run time,
///   you set the <tt>data</tt> parameter of the corresponding method to <see langword="null" />.
///   This indicates that the property should be set for all data records shown in the grid.
///   If a specific data record is passed to the method rather than <see langword="null" />,
///   the method invocation has no effect.</para>
///   <para>If you want to change the <see cref="P:PX.Data.PXUIFieldAttribute.Visible" /> or <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property
///   of <tt>PXUIFieldAttribute</tt> for a button at run time, you use the corresponding
///   static methods of <see cref="T:PX.Data.PXAction" />.
///   You usually use these methods in the <tt>RowSelected</tt> event handler of the primary view DAC.</para>
/// </remarks>
/// <example>
/// <para>The code below shows configuration of the input control for a DAC field.</para>
/// <code lang="CS">
/// [PXDBDecimal(2)]
/// [PXUIField(DisplayName = "Documents Total",
///            Visibility = PXUIVisibility.SelectorVisible,
///            Enabled = false)]
/// public virtual decimal? CuryDocsTotal { get; set; }
/// </code>
/// </example>
/// <example>
/// <para>The example below shows how to change the layout configuration at run time.</para>
/// <para>In the <tt>SetEnabeled</tt> method, the first parameter is set to the cache variable.
/// This is a <see cref="T:PX.Data.PXCache" /> object that keeps  <tt>APInvoice</tt> data records.</para>
/// <para>The second parameter is set to an <tt>APInvoice</tt> data record that is obtained from <tt>e.Row</tt>.
/// The <tt>SetVisible</tt> method is called for an <tt>APTran</tt> DAC field.
/// Therefore, a different cache object should be passed to the method.
/// The appropriate cache is specified through the <tt>Cache</tt> property of the <tt>Transactions</tt> view.</para>
/// <code lang="CS">
/// protected virtual void APInvoice_RowSelected(PXCache cache,
///                                              PXRowSelectedEventArgs)
/// {
///     APInvoice doc = e.Row as APInvoice;
/// 
///     // Disable the field input control
///     PXUIFieldAttribute.SetEnabled&lt;APInvoice.taxZoneID&gt;(
///         cache, doc, false);
/// 
///     // Showing or hiding a 'required' mark beside a field input control
///     PXUIFieldAttribute.SetRequired&lt;APInvoice.dueDate&gt;(
///         cache, (doc.DocType != APDocType.DebitAdj));
/// 
///     // Making a field visible.
///     // The data parameter is set to null to set the property for all
///     // APTran data records. The cache object is obtained through the
///     // Transactions data view
///     PXUIFieldAttribute.SetVisible&lt;APTran.projectID&gt;(
///         Transactions.Cache, null, true);
/// }
/// 
/// // Definition of the Transactions data view in the same graph
/// public PXSelect&lt;APTran,
///     Where&lt;APTran.tranType, Equal&lt;Current&lt;APInvoice.docType&gt;&gt;,
///         And&lt;APTran.refNbr, Equal&lt;Current&lt;APInvoice.refNbr&gt;&gt;&gt;&gt;&gt;
///     Transactions;
/// </code>
/// </example>
/// <example>
/// <para>The following example shows how to use the attribute to configure actions.</para>
/// <code lang="CS">
/// // The action declaration
/// public PXAction&lt;APDocumentFilter&gt; viewDocument;
/// // The action method declaration
/// [PXUIField(DisplayName = "View Document",
///            MapEnableRights = PXCacheRights.Select,
///            MapViewRights = PXCacheRights.Select)]
/// [PXButton]
/// public virtual IEnumerable ViewDocument(PXAdapter adapter)
/// {
///     ...
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
[PXAttributeFamily(typeof (PXUIFieldAttribute))]
public class PXUIFieldAttribute : 
  PXEventSubscriberAttribute,
  IPXInterfaceField,
  IPXExceptionHandlingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected string _ErrorText;
  protected object _ErrorValue;
  protected PXErrorLevel _ErrorLevel;
  protected bool _Enabled = true;
  protected bool _Visible = true;
  protected bool _ReadOnly;
  protected string _DisplayName;
  protected bool _Filterable = true;
  protected PXUIVisibility _Visibility = PXUIVisibility.Visible;
  protected int _TabOrder = -1;
  protected bool _ViewRights = true;
  protected bool _EnableRights = true;
  protected PXCacheRights _MapViewRights = PXCacheRights.Select;
  protected PXCacheRights _MapEnableRights = PXCacheRights.Update;
  protected PXErrorHandling _ErrorHandling;
  protected bool? _Required;
  protected object _RestoredValue;
  protected string _NeutralDisplayName;
  protected string _FieldClass;
  protected System.Type _MapErrorTo;
  private static readonly Regex _FieldPlaceholderPattern = new Regex("\\[\\w\\S*\\]", RegexOptions.Compiled);
  private PXUIFieldAttribute.FieldSourceType _FieldSourceType;

  /// <summary>The field to which the system should map
  /// the error related to the field with the attribute.</summary>
  /// <value>If the value of the property is set to a field type,
  /// the error is mapped to the specified field and the field with the attribute.
  /// If the value is <see langword="null" />, the error is mapped to only the field with the attribute.
  /// By default, the value is <see langword="null" />.
  /// </value>
  public System.Type MapErrorTo
  {
    get => this._MapErrorTo;
    set => this._MapErrorTo = value;
  }

  /// <summary>The value that indicates whether the field is
  /// shown or hidden depending on the features enabled or disabled.</summary>
  /// <value>By default, the property is set to the segmented field name.</value>
  public virtual string FieldClass
  {
    get => this._FieldClass;
    set => this._FieldClass = value;
  }

  /// <summary>The value that indicates whether the asterisk
  /// symbol is shown for the UI element that corresponds to the field.</summary>
  /// <remarks>This property does not check that the field value is specified and
  /// does not add any other restrictions. Use the
  /// <see cref="T:PX.Data.PXDefaultAttribute">PXDefault</see> attribute to check that the value is specified.</remarks>
  /// <value>The default value is <see langword="false" />.</value>
  public virtual bool Required
  {
    get
    {
      bool? required = this._Required;
      bool flag = true;
      return required.GetValueOrDefault() == flag & required.HasValue;
    }
    set => this._Required = new bool?(value);
  }

  /// <summary>The <see cref="T:PX.Data.PXErrorHandling" /> value that
  /// specifies the way the attribute treats an error related to the field.
  /// The error is either indicated only when the field is visible, always
  /// indicated, or never indicated.</summary>
  /// <value>The default value is <see cref="F:PX.Data.PXErrorHandling.WhenVisible" />.
  /// </value>
  public virtual PXErrorHandling ErrorHandling
  {
    get => this._ErrorHandling;
    set => this._ErrorHandling = value;
  }

  [PXInternalUseOnly]
  public PXErrorLevel ErrorLevel => this._ErrorLevel;

  protected internal virtual bool ViewRights
  {
    get => this._ViewRights;
    set
    {
      this._ViewRights = value;
      if (value)
        return;
      this._Visible = false;
    }
  }

  protected internal virtual bool EnableRights
  {
    get => this._EnableRights;
    set
    {
      this._EnableRights = value;
      if (value)
        return;
      this._Enabled = false;
    }
  }

  /// <summary>The <see cref="T:PX.Data.PXCacheRights" /> value that
  /// specifies a user's access rights to the cache that allows the user to see the button in the
  /// user interface. The property is used when the <tt>PXUIField</tt> attribute
  /// configures an action button.</summary>
  public virtual PXCacheRights MapViewRights
  {
    get => this._MapViewRights;
    set => this._MapViewRights = value;
  }

  /// <summary>The <see cref="T:PX.Data.PXCacheRights" /> value that specifies
  /// a user's access rights to the cache that allows the user to click the button in the user
  /// interface. The property is used when the <tt>PXUIField</tt> configures
  /// an action button.</summary>
  public virtual PXCacheRights MapEnableRights
  {
    get => this._MapEnableRights;
    set => this._MapEnableRights = value;
  }

  public PXUIFieldAttribute()
  {
    if (this._FieldSourceType != null)
      return;
    this._FieldSourceType = new PXUIFieldAttribute.FieldSourceType();
  }

  string IPXInterfaceField.ErrorText
  {
    get => this._ErrorText;
    set => this._ErrorText = value;
  }

  object IPXInterfaceField.ErrorValue
  {
    get => this._ErrorValue;
    set => this._ErrorValue = value;
  }

  PXErrorLevel IPXInterfaceField.ErrorLevel
  {
    get => this._ErrorLevel;
    set => this._ErrorLevel = value;
  }

  void IPXInterfaceField.ForceEnabled() => this._Enabled = true;

  bool IPXInterfaceField.ViewRights => this._ViewRights;

  /// <summary>The value that indicates whether the value in the field input
  /// control is available for selection and editing. If the value is <see langword="false" />,
  /// a user cannot edit and select the field value in the control. Compare to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property.</summary>
  /// <value>The default value is <see langword="true" />.</value>
  public virtual bool Enabled
  {
    get => this._Enabled;
    set
    {
      if (!this._EnableRights && value)
        return;
      this._Enabled = value;
    }
  }

  /// <summary>A value that indicates whether the field control or grid
  /// column is visible in the user interface.</summary>
  /// <value>The default value is <see langword="true" />.</value>
  /// <remarks>To completely remove a grid column from the user interface,
  /// along with <see cref="P:PX.Data.PXUIFieldAttribute.Visible" /> set to <see langword="false" />,
  /// you need to set <see cref="P:PX.Data.PXUIFieldAttribute.Visibility" /> to <see cref="F:PX.Data.PXUIVisibility.Invisible" />.
  /// If <see cref="P:PX.Data.PXUIFieldAttribute.Visible" /> is <see langword="false" /> for a grid column
  /// and <see cref="P:PX.Data.PXUIFieldAttribute.Visibility" /> has its default value, which is <see cref="F:PX.Data.PXUIVisibility.Visible" />,
  /// the column becomes hidden, and a user still can add the column to the table
  /// by using the Column Configuration dialog box.</remarks>
  public virtual bool Visible
  {
    get => this._Visible;
    set
    {
      if (!this._ViewRights && value)
        return;
      this._Visible = value;
    }
  }

  public virtual bool IsReadOnly
  {
    get => this._ReadOnly;
    set => this._ReadOnly = value;
  }

  /// <summary>The field name displayed in the user interface.
  /// This name is rendered as the input control label on a form or as the
  /// grid column header.</summary>
  /// <value>The default value is the field name.</value>
  public virtual string DisplayName
  {
    get => this._DisplayName == null ? (this._DisplayName = this._FieldName) : this._DisplayName;
    set => this._DisplayName = value;
  }

  /// <summary>The value that indicates whether the labels used
  /// by the attribute are localizable.</summary>
  public virtual bool IsLocalizable { get; set; } = true;

  /// <summary>The <see cref="T:PX.Data.PXUIVisibility" /> value that
  /// specifies where the input control for the field appears in the user interface.
  /// You can specify whether the input control appears in a form, grid, or
  /// lookup table of a selector control, or never appears in the user interface.</summary>
  /// <value>The default value is <see cref="F:PX.Data.PXUIVisibility.Visible" />.</value>
  public virtual PXUIVisibility Visibility
  {
    get => this._Visibility;
    set => this._Visibility = value;
  }

  /// <summary>The order in which the field input control gets
  /// the focus when the user moves it by pressing the TAB key.</summary>
  public virtual int TabOrder
  {
    get => this._TabOrder == -1 ? this._FieldOrdinal : this._TabOrder;
    set => this._TabOrder = value;
  }

  internal string NeutralDisplayName
  {
    get
    {
      if (this._NeutralDisplayName == null)
        this._NeutralDisplayName = this._DisplayName ?? this._FieldName;
      return this._NeutralDisplayName;
    }
  }

  /// <exclude />
  public void ChangeNeutralDisplayName(string newValue) => this._NeutralDisplayName = newValue;

  /// <summary>Finds all fields with non-empty error strings and returns a
  /// dictionary with field names as the keys and error messages as the
  /// values.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record whose fields are checked for error
  /// strings. If the value is <see langword="null" />, the method takes into account all data
  /// records in the cache object.</param>
  public static Dictionary<string, string> GetErrors(
    PXCache cache,
    object data,
    params PXErrorLevel[] errorLevels)
  {
    Dictionary<string, string> errors = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (IPXInterfaceField pxInterfaceField in cache.GetAttributes(data, (string) null).OfType<IPXInterfaceField>().Union<IPXInterfaceField>((IEnumerable<IPXInterfaceField>) (cache._KeyValueAttributeUIFields ?? new IPXInterfaceField[0])))
    {
      string errorText = pxInterfaceField.ErrorText;
      if (!string.IsNullOrEmpty(errorText) && (errorLevels == null || errorLevels.Length == 0 || ((IEnumerable<PXErrorLevel>) errorLevels).Contains<PXErrorLevel>(pxInterfaceField.ErrorLevel)))
        errors[((PXEventSubscriberAttribute) pxInterfaceField).FieldName] = errorText;
    }
    return errors;
  }

  public static void SetError(
    PXCache cache,
    object data,
    string name,
    string error,
    string errorvalue,
    bool resetErrorValue,
    PXErrorLevel level)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (IPXInterfaceField attr in cache.GetAttributes(data, name).OfType<IPXInterfaceField>())
    {
      attr.ErrorText = PXMessages.LocalizeNoPrefix(error);
      attr.ErrorLevel = level;
      attr.ErrorValue = !resetErrorValue || errorvalue != null ? (object) errorvalue : (object) (string) null;
      PXUIFieldAttribute.CollectErrorAttribute(cache, attr);
    }
  }

  private static void CollectErrorAttribute(PXCache cache, IPXInterfaceField attr)
  {
    if (string.IsNullOrEmpty(attr.ErrorText) && attr.ErrorValue == null)
      return;
    cache.AttributesWithError.Add(attr);
  }

  /// <summary>Sets the error string to display as a tooltip for the field
  /// with the specified name.
  /// The value of the field will be cleared in the UI.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="error">The string that is set as the error message
  /// string.</param>
  public static void SetError(PXCache cache, object data, string name, string error)
  {
    PXUIFieldAttribute.SetError(cache, data, name, error, (string) null, false, error == null ? PXErrorLevel.Undefined : PXErrorLevel.Error);
  }

  /// <summary>Sets the error string to display as a tooltip for the
  /// specified field.
  /// The value of the field will be cleared in the UI.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="error">The error string displayed as a tooltip on the
  /// field input control.</param>
  public static void SetError<Field>(PXCache cache, object data, string error) where Field : IBqlField
  {
    PXUIFieldAttribute.SetError(cache, data, typeof (Field).Name, error);
  }

  /// <summary>Sets the error string to display as a tooltip and the error
  /// value to display in the input control for the field with the specified
  /// name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="error">The error string displayed as a tooltip on the
  /// field input control.</param>
  /// <param name="errorvalue">The string displayed in the field input
  /// control. The value is not assigned to the field.</param>
  public static void SetError(
    PXCache cache,
    object data,
    string name,
    string error,
    string errorvalue)
  {
    PXUIFieldAttribute.SetError(cache, data, name, error, errorvalue, true, error == null ? PXErrorLevel.Undefined : PXErrorLevel.Error);
  }

  /// <summary>Sets the error string to display as a tooltip and the error
  /// value to display in the input control for the specified field. The
  /// error level is set to <tt>PXErrorLevel.Error</tt>.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="error">The error string displayed as a tooltip on the
  /// field input control.</param>
  /// <param name="errorvalue">The string displayed in the field input
  /// control. The value is not assigned to the field.</param>
  public static void SetError<Field>(PXCache cache, object data, string error, string errorvalue) where Field : IBqlField
  {
    PXUIFieldAttribute.SetError(cache, data, typeof (Field).Name, error, errorvalue);
  }

  internal static void CopyErrorInfo(IPXInterfaceField source, IPXInterfaceField destination)
  {
    destination.ErrorText = source.ErrorText;
    destination.ErrorLevel = source.ErrorLevel;
    destination.ErrorValue = source.ErrorValue;
  }

  /// <summary>Sets the error string to display as a tooltip for the field
  /// with the specified name. The error level is set to <see cref="F:PX.Data.PXErrorLevel.Warning" />.
  /// </summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="error">The error string displayed as a tooltip on the
  /// field input control.</param>
  public static void SetWarning(PXCache cache, object data, string name, string error)
  {
    PXUIFieldAttribute.SetError(cache, data, name, error, (string) null, true, PXErrorLevel.Warning);
  }

  /// <summary>Sets the error string to display as a tooltip for the
  /// specified field. The error level is set to
  /// <see cref="F:PX.Data.PXErrorLevel.Warning" />.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="error">The error string displayed as a tooltip on the
  /// field input control.</param>
  public static void SetWarning<Field>(PXCache cache, object data, string error) where Field : IBqlField
  {
    PXUIFieldAttribute.SetWarning(cache, data, typeof (Field).Name, error);
  }

  /// <summary>Returns the error string displayed for the specified
  /// field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  public static string GetError<Field>(PXCache cache, object data) where Field : IBqlField
  {
    return PXUIFieldAttribute.GetError(cache, data, typeof (Field).Name);
  }

  /// <summary>Returns the error string displayed for the field with the
  /// specified name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  public static string GetError(PXCache cache, object data, string name)
  {
    return cache.GetAttributes(data, name).OfType<IPXInterfaceField>().FirstOrDefault<IPXInterfaceField>().With<IPXInterfaceField, string>((Func<IPXInterfaceField, string>) (a => a.ErrorText));
  }

  /// <summary>Returns the error string (if the error level is <see cref="F:PX.Data.PXErrorLevel.Error" />
  /// or <see cref="F:PX.Data.PXErrorLevel.RowError" />) displayed for the specified
  /// field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  public static string GetErrorOnly<Field>(PXCache cache, object data) where Field : IBqlField
  {
    return PXUIFieldAttribute.GetErrorOnly(cache, data, typeof (Field).Name);
  }

  /// <summary>Returns the error string (if the error level is <see cref="F:PX.Data.PXErrorLevel.Error" />
  /// or <see cref="F:PX.Data.PXErrorLevel.RowError" />) displayed for the field with the
  /// specified name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  public static string GetErrorOnly(PXCache cache, object data, string name)
  {
    return cache.GetAttributes(data, name).OfType<IPXInterfaceField>().Where<IPXInterfaceField>((Func<IPXInterfaceField, bool>) (a => a.ErrorLevel == PXErrorLevel.Error || a.ErrorLevel == PXErrorLevel.RowError)).FirstOrDefault<IPXInterfaceField>().With<IPXInterfaceField, string>((Func<IPXInterfaceField, string>) (a => a.ErrorText));
  }

  /// <summary>Returns the warning string displayed for the specified
  /// field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  public static string GetWarning<Field>(PXCache cache, object data) where Field : IBqlField
  {
    return PXUIFieldAttribute.GetWarning(cache, data, typeof (Field).Name);
  }

  /// <summary>Returns the warning string displayed for the field with the
  /// specified name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  public static string GetWarning(PXCache cache, object data, string name)
  {
    return cache.GetAttributes(data, name).OfType<IPXInterfaceField>().Where<IPXInterfaceField>((Func<IPXInterfaceField, bool>) (a => a.ErrorLevel == PXErrorLevel.Warning || a.ErrorLevel == PXErrorLevel.RowWarning)).FirstOrDefault<IPXInterfaceField>().With<IPXInterfaceField, string>((Func<IPXInterfaceField, string>) (a => a.ErrorText));
  }

  public static (string errorMessage, PXErrorLevel errorLevel) GetErrorWithLevel<Field>(
    PXCache cache,
    object data)
    where Field : IBqlField
  {
    return PXUIFieldAttribute.GetErrorWithLevel(cache, data, typeof (Field).Name);
  }

  public static (string errorMessage, PXErrorLevel errorLevel) GetErrorWithLevel(
    PXCache cache,
    object data,
    string name)
  {
    return cache.GetAttributes(data, name).OfType<IPXInterfaceField>().FirstOrDefault<IPXInterfaceField>().With<IPXInterfaceField, (string, PXErrorLevel)>((Func<IPXInterfaceField, (string, PXErrorLevel)>) (a => (a.ErrorText, a.ErrorLevel)));
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property for the field with the
  /// specified name.
  /// If <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> is set to <see langword="false" />, the method also clears
  /// the required mark of the field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="isEnabled">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property.</param>
  public static void SetEnabled(PXCache cache, object data, string name, bool isEnabled)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, name))
    {
      if (pxuiFieldAttribute != null)
        pxuiFieldAttribute.Enabled = isEnabled;
    }
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property for all fields in the
  /// specific data record or all data records.
  /// If <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> is set to <see langword="false" />, the method also clears
  /// the required mark of the field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isEnabled">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property.</param>
  public static void SetEnabled(PXCache cache, object data, bool isEnabled)
  {
    bool measuringUpdatability = cache._MeasuringUpdatability;
    try
    {
      cache._MeasuringUpdatability = false;
      foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, (string) null))
      {
        if (pxuiFieldAttribute != null)
        {
          pxuiFieldAttribute.Enabled = isEnabled;
          if (data == null)
            cache.SetAltered(pxuiFieldAttribute.FieldName, true);
        }
      }
    }
    finally
    {
      cache._MeasuringUpdatability = measuringUpdatability;
    }
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property for the specified field.
  /// If <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> is set to <see langword="false" />, the method also clears
  /// the required mark of the field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isEnabled">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property.</param>
  public static void SetEnabled<Field>(PXCache cache, object data, bool isEnabled) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, typeof (Field).Name))
    {
      if (pxuiFieldAttribute != null)
        pxuiFieldAttribute.Enabled = isEnabled;
    }
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property to <see langword="true" />
  /// for the field with the specified name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  public static void SetEnabled(PXCache cache, object data, string name)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, name))
    {
      if (pxuiFieldAttribute != null)
        pxuiFieldAttribute.Enabled = true;
    }
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property to <see langword="true" />
  /// for the specified field of the specific data record in
  /// the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  public static void SetEnabled<Field>(PXCache cache, object data) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, typeof (Field).Name))
    {
      if (pxuiFieldAttribute != null)
        pxuiFieldAttribute.Enabled = true;
    }
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Visible" /> property to
  /// <see langword="true" /> for the specified field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  public static void SetVisible<Field>(PXCache cache, object data) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, typeof (Field).Name))
    {
      if (pxuiFieldAttribute != null)
        pxuiFieldAttribute.Visible = true;
    }
  }

  /// <summary>Shows or hides the input control for the specified field in
  /// the user interface by setting the <see cref="P:PX.Data.PXUIFieldAttribute.Visible" /> property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isVisible">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Visible" /> property.</param>
  public static void SetVisible<Field>(PXCache cache, object data, bool isVisible) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, typeof (Field).Name))
    {
      if (pxuiFieldAttribute != null)
        pxuiFieldAttribute.Visible = isVisible;
    }
  }

  /// <summary>Shows or hides the input control for the field with the
  /// specified name in the user interface by setting the <see cref="P:PX.Data.PXUIFieldAttribute.Visible" />
  /// property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="isVisible">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property.</param>
  public static void SetVisible(PXCache cache, object data, string name, bool isVisible)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, name))
    {
      if (pxuiFieldAttribute != null)
        pxuiFieldAttribute.Visible = isVisible;
    }
  }

  /// <summary>Makes the input control for the field with the specified name
  /// visible in the user interface by setting the <see cref="P:PX.Data.PXUIFieldAttribute.Visible" /> property
  /// to <see langword="true" />.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  public static void SetVisible(PXCache cache, object data, string name)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributesOfType<PXUIFieldAttribute>(data, name))
    {
      if (pxuiFieldAttribute != null)
        pxuiFieldAttribute.Visible = true;
    }
  }

  /// <summary>Makes the input control for the specified field read-only by
  /// setting the <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property to <see langword="true" />.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  public static void SetReadOnly<Field>(PXCache cache, object data) where Field : IBqlField
  {
    PXUIFieldAttribute.SetReadOnly<Field>(cache, data, true);
  }

  /// <summary>Makes the input control for the field with the specified name
  /// read-only by setting the <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property to
  /// <see langword="true" />.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  public static void SetReadOnly(PXCache cache, object data, string name)
  {
    PXUIFieldAttribute.SetReadOnly(cache, data, name, true);
  }

  /// <summary>Makes the input controls for all fields read-only by setting
  /// the <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property to <see langword="true" />.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  public static void SetReadOnly(PXCache cache, object data)
  {
    PXUIFieldAttribute.SetReadOnly(cache, data, true);
  }

  /// <summary>Makes the input controls for all field read-only or not read-
  /// only by setting the <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isReadOnly">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property.</param>
  public static void SetReadOnly(PXCache cache, object data, bool isReadOnly)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, (string) null))
    {
      if (attribute is PXUIFieldAttribute pxuiFieldAttribute)
      {
        pxuiFieldAttribute.IsReadOnly = isReadOnly;
        if (data == null)
          cache.SetAltered(attribute.FieldName, true);
      }
    }
  }

  /// <summary>Makes the input control for the specified field read-only or
  /// not read-only by setting the <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isReadOnly">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property.</param>
  public static void SetReadOnly<Field>(PXCache cache, object data, bool isReadOnly) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXUIFieldAttribute)
        ((PXUIFieldAttribute) attribute)._ReadOnly = isReadOnly;
    }
  }

  /// <summary>Makes the input control for the field with the specified name
  /// read- only or not-read-only by setting the <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" />
  /// property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="isReadOnly">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.IsReadOnly" /> property.</param>
  public static void SetReadOnly(PXCache cache, object data, string name, bool isReadOnly)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXUIFieldAttribute)
        ((PXUIFieldAttribute) attribute)._ReadOnly = isReadOnly;
    }
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property for the field with the
  /// specified name by setting the <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property.
  /// If <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> is set to <see langword="false" />, the method also clears
  /// the required mark of the field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="isEnabled">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property.</param>
  public static void SetEnabled(PXCache cache, string name, bool isEnabled)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXUIFieldAttribute)
      {
        if (name == null)
          cache.SetAltered(attribute.FieldName, true);
        ((PXUIFieldAttribute) attribute).Enabled = isEnabled;
      }
    }
  }

  /// <summary>Shows or hides the input control for the field with the
  /// specified name in the user interface for all data record by setting
  /// the <see cref="P:PX.Data.PXUIFieldAttribute.Visible" /> property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="isVisible">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Enabled" /> property.</param>
  public static void SetVisible(PXCache cache, string name, bool isVisible)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXUIFieldAttribute)
        ((PXUIFieldAttribute) attribute).Visible = isVisible;
    }
  }

  /// <summary>Sets the visibility status of the input control for the
  /// specified field by setting the <see cref="P:PX.Data.PXUIFieldAttribute.Visibility" /> property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="visibility">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Visibility" /> property.</param>
  public static void SetVisibility<Field>(PXCache cache, object data, PXUIVisibility visibility) where Field : IBqlField
  {
    if (visibility == PXUIVisibility.Undefined)
      return;
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXUIFieldAttribute)
        ((PXUIFieldAttribute) attribute)._Visibility = visibility;
    }
  }

  /// <summary>Sets the visibility status of the input control for the field
  /// with the specified name by setting the <see cref="P:PX.Data.PXUIFieldAttribute.Visibility" />
  /// property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="data">The data record the method is applied to.
  /// If the value is <see langword="null" />, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="visibility">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Visibility" /> property.</param>
  public static void SetVisibility(
    PXCache cache,
    object data,
    string name,
    PXUIVisibility visibility)
  {
    if (visibility == PXUIVisibility.Undefined)
      return;
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXUIFieldAttribute)
        ((PXUIFieldAttribute) attribute)._Visibility = visibility;
    }
  }

  /// <summary>Sets the visibility status of the input control for the field
  /// with the specified name by setting the <see cref="P:PX.Data.PXUIFieldAttribute.Visibility" />
  /// property.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="visibility">The value that is assigned to the
  /// <see cref="P:PX.Data.PXUIFieldAttribute.Visibility" /> property.</param>
  public static void SetVisibility(PXCache cache, string name, PXUIVisibility visibility)
  {
    if (visibility == PXUIVisibility.Undefined)
      return;
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXUIFieldAttribute)
        ((PXUIFieldAttribute) attribute)._Visibility = visibility;
    }
  }

  /// <summary>Returns the value of the <see cref="P:PX.Data.PXUIFieldAttribute.DisplayName" /> property for
  /// the specified field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  public static string GetDisplayName<Field>(PXCache cache) where Field : IBqlField
  {
    return PXUIFieldAttribute.GetDisplayName(cache, typeof (Field).Name);
  }

  /// <summary>Returns the value of the <see cref="P:PX.Data.PXUIFieldAttribute.DisplayName" /> property for
  /// the field with the specified name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="name">The field name.</param>
  public static string GetDisplayName(PXCache cache, string fieldName)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(fieldName))
    {
      if (subscriberAttribute is PXUIFieldAttribute)
        return ((PXUIFieldAttribute) subscriberAttribute).DisplayName;
    }
    return fieldName;
  }

  /// <exclude />
  public static string GetNeutralDisplayName(PXCache cache, string fieldName)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(fieldName))
    {
      if (subscriberAttribute is PXUIFieldAttribute)
        return ((PXUIFieldAttribute) subscriberAttribute).NeutralDisplayName;
    }
    return fieldName;
  }

  /// <exclude />
  public static string SetNeutralDisplayName(
    PXCache cache,
    string fieldName,
    string neutralDisplaydName)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(fieldName))
    {
      if (subscriberAttribute is PXUIFieldAttribute)
      {
        ((PXUIFieldAttribute) subscriberAttribute).ChangeNeutralDisplayName(neutralDisplaydName);
        break;
      }
    }
    return fieldName;
  }

  /// <summary>Sets the display name of the specified field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="displayName">The new display name.</param>
  public static void SetDisplayName<Field>(PXCache cache, string displayName) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXUIFieldAttribute)
      {
        string str = PXMessages.Localize(displayName, out string _);
        ((PXUIFieldAttribute) attribute).DisplayName = str;
        ((PXUIFieldAttribute) attribute).ChangeNeutralDisplayName(displayName);
        break;
      }
    }
  }

  /// <summary>Sets the display name of the field with the specified
  /// name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="fieldName">The field name.</param>
  /// <param name="displayName">The new display name.</param>
  public static void SetDisplayName(PXCache cache, string fieldName, string displayName)
  {
    cache.SetAltered(fieldName, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(fieldName))
    {
      if (attribute is PXUIFieldAttribute)
      {
        string str = PXMessages.Localize(displayName, out string _);
        ((PXUIFieldAttribute) attribute).DisplayName = str;
        ((PXUIFieldAttribute) attribute).ChangeNeutralDisplayName(displayName);
        ((PXUIFieldAttribute) attribute).IsLocalizable = true;
        break;
      }
    }
  }

  /// <summary>Sets the localized display name of the field with the specified
  /// name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="fieldName">The field name.</param>
  /// <param name="displayName">The new display name.</param>
  public static void SetDisplayNameLocalized(PXCache cache, string fieldName, string displayName)
  {
    cache.SetAltered(fieldName, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(fieldName))
    {
      if (attribute is PXUIFieldAttribute)
      {
        string str = PXMessages.Localize(displayName, out string _);
        ((PXUIFieldAttribute) attribute).DisplayName = str;
        ((PXUIFieldAttribute) attribute).IsLocalizable = false;
        break;
      }
    }
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Required" /> property for the specified field
  /// for all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="required">The value assigned to the <see cref="P:PX.Data.PXUIFieldAttribute.Required" />
  /// property.</param>
  public static void SetRequired<Field>(PXCache cache, bool required) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    EnumerableExtensions.ForEach<PXUIFieldAttribute>(cache.GetAttributes<Field>().OfType<PXUIFieldAttribute>(), (System.Action<PXUIFieldAttribute>) (attr => attr._Required = new bool?(required)));
  }

  /// <summary>Sets the <see cref="P:PX.Data.PXUIFieldAttribute.Required" /> property for the field with the
  /// specified name for all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXUIField</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="required">The value assigned to the <see cref="P:PX.Data.PXUIFieldAttribute.Required" />
  /// property.</param>
  public static void SetRequired(PXCache cache, string name, bool required)
  {
    cache.SetAltered(name, true);
    foreach (PXUIFieldAttribute pxuiFieldAttribute in cache.GetAttributes(name).OfType<PXUIFieldAttribute>())
      pxuiFieldAttribute._Required = new bool?(required);
  }

  /// <exclude />
  public virtual void ExceptionHandling(PXCache sender, PXExceptionHandlingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item || e.Row == null)
      return;
    if (e.Exception != null)
    {
      if (this._RestoredValue != null && e.NewValue == this._RestoredValue)
      {
        e.Cancel = true;
        return;
      }
      if (!string.IsNullOrEmpty(this._ErrorText) && this._ErrorLevel != PXErrorLevel.Warning && this._ErrorLevel != PXErrorLevel.RowWarning && this._ErrorLevel != PXErrorLevel.Undefined)
      {
        if (e.Exception is PXSetPropertyKeepPreviousException)
        {
          e.Cancel = true;
          return;
        }
        if (e.Exception is PXRowPersistingException)
          return;
      }
      e.Cancel = e.Exception is PXSetPropertyException && (this._ErrorHandling == PXErrorHandling.Always || this.Visible && this._ErrorHandling == PXErrorHandling.WhenVisible);
      this.ApplyErrorTextTemplates(e);
    }
    else
    {
      this._ErrorValue = (object) null;
      this._ErrorText = (string) null;
      this._ErrorLevel = PXErrorLevel.Undefined;
      e.Cancel = true;
    }
    PXUIFieldAttribute.CollectErrorAttribute(sender, (IPXInterfaceField) this);
    if (!(this.MapErrorTo != (System.Type) null))
      return;
    foreach (PXUIFieldAttribute attr in sender.GetAttributes(e.Row, this.MapErrorTo.Name).OfType<PXUIFieldAttribute>())
    {
      attr._ErrorText = this._ErrorText;
      attr._ErrorLevel = this._ErrorLevel;
      attr._ErrorValue = sender.GetValue(e.Row, this.MapErrorTo.Name);
      PXUIFieldAttribute.CollectErrorAttribute(sender, (IPXInterfaceField) attr);
    }
  }

  private void ApplyErrorTextTemplates(PXExceptionHandlingEventArgs eventArgs)
  {
    if (eventArgs.Exception is PXException exception1)
    {
      this._ErrorText = exception1.MessageNoPrefix;
      if (eventArgs.Exception is PXSetPropertyException exception)
      {
        this._ErrorLevel = exception.ErrorLevel;
        this._ErrorValue = exception.ErrorValue ?? eventArgs.NewValue;
      }
      else
      {
        this._ErrorValue = eventArgs.NewValue;
        this._ErrorLevel = PXErrorLevel.Error;
      }
    }
    else
    {
      this._ErrorValue = eventArgs.NewValue;
      this._ErrorText = eventArgs.Exception.Message;
      this._ErrorLevel = PXErrorLevel.Error;
    }
    string str = this._DisplayName ?? this._FieldName;
    string errorText1 = this._ErrorText;
    this._ErrorText = PXUIFieldAttribute.FormatFieldName(this._ErrorText, this._FieldName, str);
    string errorText2 = this._ErrorText;
    if ((object) errorText1 == (object) errorText2 || !(eventArgs.Exception is PXOverridableException exception2))
      return;
    exception2.SetMessage(this._ErrorText);
  }

  /// <summary>
  /// Replaces placeholders in the source string with the provided value.
  /// </summary>
  /// <param name="sourceText">Source string with placeholders. Valid placeholders are field name surrounded with square brackets ([myField]) or zero in curly brackets ({0})</param>
  /// <param name="fieldName">Field name</param>
  /// <param name="value">Value placeholders to be replaced with</param>
  /// <returns></returns>
  internal static string FormatFieldName(string sourceText, string fieldName, string value)
  {
    return sourceText.Replace("{0}", value).Replace($"[{fieldName}]", value, StringComparison.OrdinalIgnoreCase);
  }

  internal static IEnumerable<string> FindFieldNames(string text)
  {
    foreach (Capture match in PXUIFieldAttribute._FieldPlaceholderPattern.Matches(text))
      yield return match.Value.Trim('[', ']');
  }

  /// <exclude />
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Update) && !string.IsNullOrEmpty(this._ErrorText) && this._ErrorLevel != PXErrorLevel.Warning && this._ErrorLevel != PXErrorLevel.RowWarning && this._ErrorLevel != PXErrorLevel.RowInfo)
    {
      string local;
      switch (e.Operation)
      {
        case PXDBOperation.Insert:
          local = ErrorMessages.GetLocal("Inserting ");
          break;
        case PXDBOperation.Delete:
          local = ErrorMessages.GetLocal("Deleting ");
          break;
        default:
          local = ErrorMessages.GetLocal("Updating ");
          break;
      }
      Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(sender, e.Row);
      DacDescriptor? emptyDacDescriptor = sender.GetNonEmptyDacDescriptor(e.Row);
      string itemName = PXUIFieldAttribute.GetItemName(sender);
      Dictionary<string, string> innerExceptions = errors;
      System.Type type = sender.Graph.GetType();
      object row = e.Row;
      object[] objArray = new object[2]
      {
        (object) local,
        (object) itemName
      };
      throw new PXOuterException(emptyDacDescriptor, innerExceptions, type, row, "{0} '{1}' record raised at least one error. Please review the errors.", objArray);
    }
  }

  /// <summary>Returns the user-friendly name of the specified cache object.
  /// The name is set using the <see cref="T:PX.Data.PXCacheNameAttribute">PXCacheName</see> attribute.</summary>
  /// <param name="cache">The cache object the method is applied to.</param>
  public static string GetItemName(PXCache sender)
  {
    System.Type itemType = sender.GetItemType();
    object[] customAttributes = itemType.GetCustomAttributes(typeof (PXCacheNameAttribute), false);
    if (customAttributes != null)
    {
      foreach (PXNameAttribute pxNameAttribute in customAttributes)
      {
        string name = pxNameAttribute.GetName();
        if (!string.IsNullOrEmpty(name))
          return name;
      }
    }
    return itemType.Name;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXFieldState returnState1 = e.ReturnState as PXFieldState;
    if (!this._ViewRights)
    {
      if (!e.ExternalCall)
        e.ReturnValue = (object) null;
      else if (returnState1 != null)
        returnState1.Visibility = PXUIVisibility.HiddenByAccessRights;
    }
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    if (!string.IsNullOrEmpty(this._ErrorText) && this._ErrorLevel == PXErrorLevel.Error)
      e.ReturnValue = PXFieldState.UnwrapValue(this._ErrorValue);
    PXUIVisibility pxuiVisibility = returnState1 == null || returnState1.Visibility != PXUIVisibility.HiddenByAccessRights ? (this._ViewRights ? this._Visibility : PXUIVisibility.Invisible) : PXUIVisibility.HiddenByAccessRights;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState2 = e.ReturnState;
    bool? isKey = new bool?(sender.Keys.Contains(this._FieldName));
    bool? nullable = new bool?();
    int num;
    if (this._Required.HasValue)
    {
      bool? required = this._Required;
      bool flag = true;
      num = required.GetValueOrDefault() == flag & required.HasValue ? 3 : -3;
    }
    else
      num = e.Row != null || this._Enabled ? 0 : -3;
    int? required1 = new int?(num);
    int? precision = new int?();
    int? length = new int?();
    string fieldName = this._FieldName;
    string displayName = this._DisplayName;
    string errorText = this._ErrorText;
    int errorLevel = (int) this._ErrorLevel;
    bool? enabled = new bool?(this._Enabled);
    bool? visible = new bool?(this.Visible);
    bool? readOnly = new bool?(this._ReadOnly);
    int visibility = (int) pxuiVisibility;
    PXFieldState instance = PXFieldState.CreateInstance(returnState2, (System.Type) null, isKey, nullable, required1, precision, length, fieldName: fieldName, displayName: displayName, error: errorText, errorLevel: (PXErrorLevel) errorLevel, enabled: enabled, visible: visible, readOnly: readOnly, visibility: (PXUIVisibility) visibility);
    selectingEventArgs.ReturnState = (object) instance;
  }

  /// <exclude />
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this._EnableRights || !e.ExternalCall)
      return;
    object valuePending = sender.GetValuePending(e.Row, this._FieldName);
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    if (e.NewValue != valuePending || valuePending == obj || obj == null && sender.Keys.Contains(this._FieldName))
      return;
    if (this._ViewRights)
      throw new PXSetPropertyException("You don't have enough rights on '{0}'.", new object[1]
      {
        (object) $"[{this._FieldName}]"
      });
    this._RestoredValue = obj;
    e.NewValue = this._RestoredValue;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    PXAccess.Secure(sender, (PXEventSubscriberAttribute) this);
    this.TryLocalize(sender);
  }

  /// <exclude />
  public UIFieldSourceType GetFieldSourceType(PXCache fieldCache)
  {
    UIFieldSourceType fieldSourceType = UIFieldSourceType.Undefined;
    if (WebConfig.EnablePageOpenOptimizations && this._FieldSourceType.IsSet)
      return this._FieldSourceType.SourceType;
    if (fieldCache.Graph != null && this.FieldOrdinal == -1)
    {
      fieldSourceType = this._FieldName.Contains<char>('@') ? UIFieldSourceType.AutomationButtonName : UIFieldSourceType.ActionName;
    }
    else
    {
      System.Type itemType = fieldCache.GetItemType();
      if (itemType.GetProperty(this._FieldName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public) != (PropertyInfo) null)
        fieldSourceType = UIFieldSourceType.DacFieldName;
      else if (PXPageRipper.GetExtentionWithProperty(fieldCache.GetExtensionTypes(), fieldCache.GetItemType(), this.FieldName) != (System.Type) null)
        fieldSourceType = UIFieldSourceType.CacheExtensionFieldName;
      else if (itemType.BaseType != (System.Type) null && itemType.BaseType.GetProperty(this._FieldName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public) != (PropertyInfo) null)
        fieldSourceType = UIFieldSourceType.ParentDacFieldName;
    }
    if (WebConfig.EnablePageOpenOptimizations && !this._FieldSourceType.IsSet)
    {
      this._FieldSourceType.SourceType = fieldSourceType;
      this._FieldSourceType.IsSet = true;
    }
    return fieldSourceType;
  }

  protected void TryLocalize(PXCache sender)
  {
    if (!this.IsLocalizable)
      return;
    if (ResourceCollectingManager.IsStringCollecting)
      PXPageRipper.RipUIField(this, sender, this.GetFieldSourceType(sender), CollectResourceSettings.Resource);
    else
      PXLocalizerRepository.UIFieldLocalizer.Localize(this, this._BqlTable.FullName, sender, this.GetFieldSourceType(sender));
  }

  private class FieldSourceType
  {
    public bool IsSet { get; set; }

    public UIFieldSourceType SourceType { get; set; }
  }
}
