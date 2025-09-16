// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Common.Extensions;
using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>string</tt> type to the database field of <tt>char</tt>, <tt>varchar</tt>, <tt>nchar</tt>, or <tt>nvarchar</tt> type.</summary>
/// <remarks>
///   <para>The attribute is added to the value declaration of a DAC field. The field becomes bound to the database column with the same name.</para>
///   <para>It is possible to specify the maximum length and input validation mask for the string.</para>
///   <para>You can modify the <tt>Length</tt> and <tt>InputMask</tt> properties at run time by calling the static methods.</para>
/// </remarks>
/// <example>
///   <code title="Example2" lang="CS">
/// //The attribute below maps a string field to the database column (defines a bound field) and
/// //sets a limit for the value length to 50.
/// [PXDBString(50)]
/// public virtual string Fax { get; set; }</code>
/// <code title="Example" lang="CS">
/// //The attribute below defines a bound field taking as a value strings of any 8 characters.
/// //In the user interface, the input control will show the mask
/// //that splits the value into four groups separated by dots.
/// [PXDBString(8, InputMask = "CC.CC.CC.CC")]
/// public virtual string ReportID { get; set; }</code>
/// <code title="Example3" groupname="Example2" lang="CS">
/// //The attribute below defines a bound field that takes as a value
/// //a Unicode string of 5 uppercase characters that are strictly alphabetical letters.
/// [PXDBString(5, IsUnicode = true, InputMask = "&gt;LLLLL")]
/// public virtual string CuryID { get; set; }</code>
/// <code title="Example4" groupname="Example3" lang="CS">
/// //The example below shows a complex definition of a string key field
/// //represented in the user interface by a lookup control. In this example,
/// //the RefNbr field is mapped to the nvarchar(15) RefNbr column from the APRegister table.
/// [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
/// [PXDefault]
/// [PXUIField(DisplayName = "Reference Nbr.",
///            Visibility = PXUIVisibility.SelectorVisible,
///            TabOrder = 1)]
/// [PXSelector(typeof(
///     Search&lt;APRegister.refNbr,
///         Where&lt;APRegister.docType, Equal&lt;Optional&lt;APRegister.docType&gt;&gt;&gt;&gt;),
///     Filterable = true)]
/// public virtual string RefNbr { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBStringAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected int _Length = -1;
  protected string _InputMask;
  protected char? _PromptChar;
  protected bool? _IsUnicode;
  protected bool _IsFixed;
  private bool _padSpaced;
  protected PXDBStringAttribute.MaskMode _AutoMask;
  protected static Dictionary<string, string> _masks = new Dictionary<string, string>();

  public bool PadSpaced
  {
    get => this._padSpaced || this.IsFixed;
    set => this._padSpaced = value;
  }

  /// <summary>Gets or sets an indication that the string value will be trimmed
  /// if <tt>Length</tt> is set.</summary>
  public bool TrimStringToLength { get; set; }

  /// <summary>Gets the maximum length of the string value. If a string
  /// value exceeds the maximum length, it will be trimmed. If
  /// <tt>IsFixed</tt> is set to <tt>true</tt> and the string length is less
  /// then the maximum, it will be extended with spaces.</summary>
  /// <remarks>The default value is -1 (the string length is not limited). A
  /// different value can be set in the constructor.</remarks>
  public int Length => this._Length;

  /// <summary>Gets or sets the pattern that indicates the allowed
  /// characters in a field value. The user interface will not allow the
  /// user to enter other characters in the input control associated with
  /// the field.</summary>
  /// <remarks>
  /// <para>The default value for the key fields is
  /// '<tt>&gt;aaaaaa</tt>'.</para>
  /// <para><i>Control characters:</i></para>
  /// <list type="bullet">
  /// <item><description>'&gt;': the following chars to
  /// upper case</description></item>
  /// <item><description>'&lt;': the
  /// following chars to lower
  /// case</description></item>
  /// <item><description>'<tt>&amp;</tt>',
  /// '<tt>C</tt>': any character or a
  /// space</description></item>
  /// <item><description>'<tt>A</tt>',
  /// '<tt>a</tt>': a letter or
  /// digit</description></item>
  /// <item><description>'<tt>L</tt>',
  /// '<tt>?</tt>': a
  /// letter</description></item>
  /// <item><description>'<tt>#</tt>',
  /// '<tt>0</tt>', '<tt>9</tt>': a digit</description></item>
  /// </list>
  /// </remarks>
  /// <example>
  /// The attribute below defines a bound field taking as a value Unicode
  /// strings of 5 uppercase characters that are strictly alphabetical letters.
  /// <code>
  /// [PXDBString(5, IsUnicode = true, InputMask = "&gt;LLLLL")]
  /// public virtual string CuryID { get; set; }
  /// </code>
  /// </example>
  public string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  /// <exclude />
  public char? PromptChar
  {
    get => this._PromptChar;
    set => this._PromptChar = value;
  }

  /// <summary>Gets or sets an indication that the string consists of
  /// Unicode characters. This property should be set to <tt>true</tt> if
  /// the database column has a Unicode string type (<tt>nchar</tt> or
  /// <tt>nvarchar</tt>). The default value is <tt>false</tt>.</summary>
  /// <example>
  /// The example below shows a complex definition of a string key field
  /// represented in the user interface by a lookup control.
  /// In this example, the <tt>RefNbr</tt> field is mapped to the
  /// <tt>nvarchar(15)</tt> column from the <tt>APRegister</tt> table.
  /// <code>
  /// [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  /// [PXDefault]
  /// [PXUIField(DisplayName = "Reference Nbr.",
  ///            Visibility = PXUIVisibility.SelectorVisible,
  ///            TabOrder = 1)]
  /// [PXSelector(typeof(
  ///     Search&lt;APRegister.refNbr,
  ///         Where&lt;APRegister.docType, Equal&lt;Optional&lt;APRegister.docType&gt;&gt;&gt;&gt;),
  ///     Filterable = true)]
  /// public virtual string RefNbr { get; set; }
  /// </code>
  /// </example>
  public bool IsUnicode
  {
    get => this.IsUnicodeField();
    set => this._IsUnicode = new bool?(value);
  }

  /// <summary>Gets or sets an indication that the string has a fixed
  /// length. This property should be set to <tt>true</tt> if the database
  /// column has a fixed length type (<tt>char</tt> or <tt>nchar</tt>). The
  /// default value is <tt>false</tt>.</summary>
  public bool IsFixed
  {
    get => this._IsFixed;
    set => this._IsFixed = value;
  }

  /// <summary>Initializes a new instance of the attribute.</summary>
  public PXDBStringAttribute()
  {
  }

  /// <summary>Initializes a new instance with the given maximum length of a
  /// field value.</summary>
  /// <param name="length">The maximum length value assigned to the
  /// <tt>Length</tt> property.</param>
  /// <example>
  /// The attribute below maps a string field to the database column
  /// (defines a bound field) and sets a limit for the value length to 50.
  /// <code>
  /// [PXDBString(50)]
  /// public virtual string Fax { get; set; }</code>
  /// </example>
  public PXDBStringAttribute(int length) => this._Length = length;

  private static void setLength(PXDBStringAttribute attr, int length) => attr._Length = length;

  /// <summary>Sets the maximum length for the string field with the
  /// specified name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBString</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="length">The value that is assigned to the <tt>Length</tt>
  /// property.</param>
  public static void SetLength(PXCache cache, object data, string name, int length)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXDBStringAttribute)
        PXDBStringAttribute.setLength((PXDBStringAttribute) attribute, length);
    }
  }

  /// <summary>Sets the maximum length for the specified string
  /// field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBString</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="length">The value that is assigned to the <tt>Length</tt>
  /// property.</param>
  public static void SetLength<Field>(PXCache cache, object data, int length) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDBStringAttribute)
        PXDBStringAttribute.setLength((PXDBStringAttribute) attribute, length);
    }
  }

  /// <summary>Sets the maximum length for the string field with the
  /// specified name for all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBString</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="length">The value that is assigned to the <tt>Length</tt>
  /// property.</param>
  public static void SetLength(PXCache cache, string name, int length)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXDBStringAttribute)
        PXDBStringAttribute.setLength((PXDBStringAttribute) attribute, length);
    }
  }

  /// <summary>Sets the maximum length for the specified string field for
  /// all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBString</tt> type.</param>
  /// <param name="length">The value that is assigned to the <tt>Length</tt>
  /// property.</param>
  public static void SetLength<Field>(PXCache cache, int length) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXDBStringAttribute)
        PXDBStringAttribute.setLength((PXDBStringAttribute) attribute, length);
    }
  }

  /// <summary>Sets the input mask for the string field with the specified
  /// name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBString</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="name">The field name.</param>
  /// <param name="mask">The value that is assigned to the
  /// <tt>InputMask</tt> property.</param>
  public static void SetInputMask(PXCache cache, object data, string name, string mask)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXDBStringAttribute)
      {
        ((PXDBStringAttribute) attribute)._AutoMask = PXDBStringAttribute.MaskMode.Manual;
        ((PXDBStringAttribute) attribute)._InputMask = mask;
      }
    }
  }

  /// <summary>Sets the input mask for the specified string field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBString</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="mask">The value that is assigned to the
  /// <tt>InputMask</tt> property.</param>
  public static void SetInputMask<Field>(PXCache cache, object data, string mask) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDBStringAttribute)
      {
        ((PXDBStringAttribute) attribute)._AutoMask = PXDBStringAttribute.MaskMode.Manual;
        ((PXDBStringAttribute) attribute)._InputMask = mask;
      }
    }
  }

  /// <summary>Sets the input mask for the string field with the specified
  /// name for all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBString</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="mask">The value that is assigned to the
  /// <tt>InputMask</tt> property.</param>
  public static void SetInputMask(PXCache cache, string name, string mask)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXDBStringAttribute)
      {
        ((PXDBStringAttribute) attribute)._AutoMask = PXDBStringAttribute.MaskMode.Manual;
        ((PXDBStringAttribute) attribute)._InputMask = mask;
      }
    }
  }

  /// <summary>Sets the input mask for the specified string field for all
  /// data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBString</tt> type.</param>
  /// <param name="mask">The value that is assigned to the
  /// <tt>InputMask</tt> property.</param>
  public static void SetInputMask<Field>(PXCache cache, string mask) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXDBStringAttribute)
      {
        ((PXDBStringAttribute) attribute)._AutoMask = PXDBStringAttribute.MaskMode.Manual;
        ((PXDBStringAttribute) attribute)._InputMask = mask;
      }
    }
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || e.Cancel)
      return;
    if (!(e.NewValue is string newValue))
      newValue = Convert.ToString(e.NewValue);
    e.NewValue = (object) newValue;
    if (!this._IsFixed)
      e.NewValue = (object) ((string) e.NewValue).TrimEnd();
    if (this._Length < 0)
      return;
    int length = ((string) e.NewValue).Length;
    if (length > this._Length)
    {
      e.NewValue = (object) ((string) e.NewValue).Substring(0, this._Length);
    }
    else
    {
      if (!this._IsFixed || length >= this._Length)
        return;
      StringBuilder stringBuilder = new StringBuilder((string) e.NewValue, this._Length);
      for (int index = length; index < this._Length; ++index)
        stringBuilder.Append(' ');
      e.NewValue = (object) stringBuilder.ToString();
    }
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this.TrimStringToLength || e.NewValue == null || this._Length < 0)
      return;
    e.NewValue = (object) StringExtensions.Truncate(e.NewValue.ToString(), this._Length);
  }

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = this.DbType;
    if (this._Length < 0)
      return;
    e.DataLength = new int?(this._Length);
    if (!this.TrimStringToLength || e.DataValue == null)
      return;
    e.DataValue = (object) StringExtensions.Truncate(e.DataValue.ToString(), this._Length);
  }

  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    base.CommandPreparing(sender, e);
    this.SetPadSpacing(sender, e);
    if (this.IsKey && !this.IsFixed && (e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert && e.DataValue is string dataValue && dataValue.StartsWith(" ") && sender.Keys.variableLengthStrings.Contains(this._FieldName))
      throw new PXCommandPreparingException(this._FieldName, e.DataValue, "Key field cannot start with a leading space.");
  }

  private void SetPadSpacing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (e.SqlDialect.IsPadSpaceCompatible || !(e.Expr is Column expr))
      return;
    int num = this.PadSpaced || this.IsUnicode && this.Length > 0 && this.Length <= e.SqlDialect.DefaultPadSpaceLength || sender.GetAttributesReadonly(this.FieldName).OfType<PXDimensionAttribute>().Any<PXDimensionAttribute>() ? 1 : (sender.GetAttributesReadonly(this.FieldName).OfType<PXDBStringAttribute>().Any<PXDBStringAttribute>((System.Func<PXDBStringAttribute, bool>) (a => a.PadSpaced)) ? 1 : 0);
    expr.PadSpaced = num != 0;
  }

  protected PXDbType DbType
  {
    get
    {
      return !this._IsFixed ? (!this.IsUnicode ? PXDbType.VarChar : PXDbType.NVarChar) : (!this.IsUnicode ? PXDbType.Char : PXDbType.NChar);
    }
  }

  private bool IsUnicodeField()
  {
    if (this._IsUnicode.HasValue)
      return this._IsUnicode.Value;
    string name = this.BqlTable?.Name;
    if (name != null)
    {
      TableHeader tableStructure = PXDatabase.Provider.GetTableStructure(name);
      TableColumn tableColumn = tableStructure != null ? tableStructure.Columns.SingleOrDefault<TableColumn>((System.Func<TableColumn, bool>) (c => ((TableEntityBase) c).Name.OrdinalEquals(this.FieldName))) : (TableColumn) null;
      this._IsUnicode = tableColumn != null ? new bool?(EnumerableExtensions.IsIn<SqlDbType>(tableColumn.Type, SqlDbType.NChar, SqlDbType.NText, SqlDbType.NVarChar)) : new bool?();
    }
    return this._IsUnicode.GetValueOrDefault();
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      string s = e.Record.GetString(e.Position);
      if (s != null)
      {
        int count = this.IsFixed ? this.Length - s.Length : 0;
        if (count > 0)
          s += new string(' ', count);
      }
      if (sender.Graph.StringTable != null)
        s = sender.Graph.StringTable.Add(s);
      if (this._IsKey && !e.IsReadOnly)
      {
        sender.SetValue(e.Row, this._FieldOrdinal, (object) s);
        if ((s == null || sender.IsPresent(e.Row)) && sender.Graph.GetType() != typeof (PXGraph))
          e.Row = (object) null;
      }
      else
        sender.SetValue(e.Row, this._FieldOrdinal, (object) s);
    }
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    if (this._AutoMask == PXDBStringAttribute.MaskMode.Auto)
    {
      this._AutoMask = PXDBStringAttribute.MaskMode.Manual;
      if (sender.Keys.IndexOf(this._FieldName) != sender.Keys.Count - 1)
        this._InputMask = (string) null;
    }
    else if (this._AutoMask == PXDBStringAttribute.MaskMode.Foreign && !PXContext.GetSlot<bool>("selectorBypassInit"))
    {
      this._AutoMask = PXDBStringAttribute.MaskMode.Manual;
      if (!PXDBStringAttribute._masks.TryGetValue($"{this._BqlTable.Name}${this._FieldName}", out this._InputMask))
      {
        string str = (string) null;
        System.Type field = (System.Type) null;
        foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes((object) null, this._FieldName))
        {
          if (attribute is PXSelectorAttribute)
            field = ((PXSelectorAttribute) attribute).Field;
        }
        if (field != (System.Type) null)
        {
          foreach (PXEventSubscriberAttribute attribute in sender.Graph.Caches[BqlCommand.GetItemType(field)].GetAttributes((object) null, field.Name))
          {
            if (attribute is PXDBStringAttribute)
              str = ((PXDBStringAttribute) attribute).InputMask;
          }
        }
        this.InputMask = str;
        lock (((ICollection) PXDBStringAttribute._masks).SyncRoot)
          PXDBStringAttribute._masks[$"{this._BqlTable.Name}${this._FieldName}"] = str;
      }
    }
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(this._Length), this._IsUnicode, this._FieldName, new bool?(this._IsKey), new int?(), string.IsNullOrEmpty(this._InputMask) ? (string) null : this._InputMask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null, this._PromptChar);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this._InputMask == null)
    {
      if (this.IsKey)
      {
        StringBuilder stringBuilder = new StringBuilder(">");
        for (int index = 0; index < this._Length; ++index)
          stringBuilder.Append("a");
        this.InputMask = stringBuilder.ToString();
        this._AutoMask = PXDBStringAttribute.MaskMode.Auto;
      }
      else if (!PXDBStringAttribute._masks.TryGetValue($"{this._BqlTable.Name}${this._FieldName}", out this._InputMask))
        this._AutoMask = PXDBStringAttribute.MaskMode.Foreign;
    }
    if (!this.IsKey || this.IsFixed)
      return;
    sender.Keys.variableLengthStrings.Add(this._FieldName);
  }

  /// <exclude />
  protected enum MaskMode
  {
    Manual,
    Auto,
    Foreign,
  }
}
