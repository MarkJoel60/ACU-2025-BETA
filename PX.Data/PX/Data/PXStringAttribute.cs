// Decompiled with JetBrains decompiler
// Type: PX.Data.PXStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>string</tt> type that is not
/// mapped to a database column.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</para>
/// <para>It is possible to specify the maximum length and input
/// validation mask for the string.</para>
/// <para>You can modify the <tt>Length</tt> and <tt>InputMask</tt>
/// properties at run time by calling the static methods.</para>
/// </remarks>
/// <example>
/// The attribute below defines an unbound field taking as a value Unicode
/// strings of 5 uppercase characters that are strictly aphabetical
/// letters.
/// <code>
/// [PXString(5, IsUnicode = true, InputMask = "&gt;LLLLL")]
/// public virtual String FinChargeCuryID { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXStringAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected int _Length = -1;
  protected string _InputMask;
  protected bool _IsUnicode = true;
  protected bool _IsFixed;
  protected bool _IsKey;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
  }

  /// <summary>Gets the maximum length of the string value. If a string
  /// value exceeds the maximum length, it will be trimmed. If
  /// <tt>IsFixed</tt> is set to <see langword="true" /> and the string length is less
  /// then the maximum, it will be extended with spaces. By default, the
  /// property is –1, which means that the string length is not
  /// limited.</summary>
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
  public string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  /// <summary>Gets or sets an indication that the string has a fixed
  /// length. This property should be set to <see langword="true" /> if the database
  /// column has a fixed length type (<tt>char</tt> or <tt>nchar</tt>). The
  /// default value is <see langword="false" />.</summary>
  public bool IsFixed
  {
    get => this._IsFixed;
    set => this._IsFixed = value;
  }

  /// <summary>Gets or sets an indication that the string consists of
  /// Unicode characters. This property should be set to <see langword="true" /> if
  /// the database column has a Unicode string type (<tt>nchar</tt> or
  /// <tt>nvarchar</tt>). The default value is <see langword="false" />.</summary>
  public bool IsUnicode
  {
    get => this._IsUnicode;
    set => this._IsUnicode = value;
  }

  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXStringAttribute()
  {
  }

  /// <summary>Initializes a new instance with the given maximum length of a
  /// field value.</summary>
  /// <param name="length">The maximum length value assigned to the
  /// <tt>Length</tt> property.</param>
  public PXStringAttribute(int length) => this._Length = length;

  private static void setLength(PXStringAttribute attr, int length) => attr._Length = length;

  /// <summary>Sets the maximum length for the string field with the
  /// specified name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXString</tt> type.</param>
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
      if (attribute is PXStringAttribute)
        PXStringAttribute.setLength((PXStringAttribute) attribute, length);
    }
  }

  /// <summary>Sets the maximum length for the specified string
  /// field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXString</tt> type.</param>
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
      if (attribute is PXStringAttribute)
        PXStringAttribute.setLength((PXStringAttribute) attribute, length);
    }
  }

  /// <summary>Sets the maximum length for the string field with the
  /// specified name for all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXString</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="length">The value that is assigned to the <tt>Length</tt>
  /// property.</param>
  public static void SetLength(PXCache cache, string name, int length)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXStringAttribute)
        PXStringAttribute.setLength((PXStringAttribute) attribute, length);
    }
  }

  /// <summary>Sets the maximum length for the specified string field for
  /// all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXString</tt> type.</param>
  /// <param name="length">The value that is assigned to the <tt>Length</tt>
  /// property.</param>
  public static void SetLength<Field>(PXCache cache, int length) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXStringAttribute)
        PXStringAttribute.setLength((PXStringAttribute) attribute, length);
    }
  }

  /// <summary>Sets the input mask for the string field with the specified
  /// name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXString</tt> type.</param>
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
      if (attribute is PXStringAttribute)
        ((PXStringAttribute) attribute)._InputMask = mask;
    }
  }

  /// <summary>Sets the input mask for the specified string field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXString</tt> type.</param>
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
      if (attribute is PXStringAttribute)
        ((PXStringAttribute) attribute)._InputMask = mask;
    }
  }

  /// <summary>Sets the input mask for the string field with the specified
  /// name for all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXString</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="mask">The value that is assigned to the
  /// <tt>InputMask</tt> property.</param>
  public static void SetInputMask(PXCache cache, string name, string mask)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXStringAttribute)
        ((PXStringAttribute) attribute)._InputMask = mask;
    }
  }

  /// <summary>Sets the input mask for the specified string field for all
  /// data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXString</tt> type.</param>
  /// <param name="mask">The value that is assigned to the
  /// <tt>InputMask</tt> property.</param>
  public static void SetInputMask<Field>(PXCache cache, string mask) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXStringAttribute)
        ((PXStringAttribute) attribute)._InputMask = mask;
    }
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || e.Cancel)
      return;
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

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(this._Length), new bool?(this._IsUnicode), this._FieldName, new bool?(this._IsKey), new int?(), this._InputMask, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  /// <exclude />
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
      return;
    if (e.Value != null)
    {
      e.BqlTable = this._BqlTable;
      if (e.Expr == null)
        e.Expr = (SQLExpression) new Column(this._FieldName, this._BqlTable);
      e.DataValue = e.Value;
      if (this._Length >= 0)
        e.DataLength = new int?(this._Length);
    }
    e.DataType = this._IsFixed ? (this._IsUnicode ? PXDbType.NChar : PXDbType.Char) : (this._IsUnicode ? PXDbType.NVarChar : PXDbType.VarChar);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (!this.IsKey)
      return;
    sender.Keys.Add(this._FieldName);
  }
}
