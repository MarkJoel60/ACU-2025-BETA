// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCheckUnique
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Ensures that a DAC field has distinct values in all data records in a given context.</summary>
/// <remarks>
///   <para>The attribute is placed on the declaration of a DAC field, and ensures that this field has a unique value within the current context.</para>
///   <para>The functionality of the attribute can be implemented through other ways. The use of the attribute for imposing constraint of a key field is obsolete. You
/// should use the <tt>IsKey</tt> property of the data type attribute for this purpose.</para>
/// </remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXDBString(30, IsKey = true)]
/// [PXUIField(DisplayName = "Mailing ID")]
/// [PXCheckUnique]
/// public override string NotificationCD { get; set; }</code>
/// </example>
public class PXCheckUnique : 
  PXEventSubscriberAttribute,
  IPXRowInsertingSubscriber,
  IPXRowUpdatingSubscriber,
  IPXRowPersistingSubscriber
{
  /// <summary>
  /// The additional <tt>Where</tt> clause that filters the data records
  /// that are selected to check uniqueness of the field value among them.
  /// </summary>
  public System.Type Where;
  protected string[] _UniqueFields;
  protected PXView _View;
  private const string DefaultErrorMessage = "An attempt was made to add a duplicate entry.";
  private string _errorMessage;
  private bool callInprocess;

  /// <summary>Initializes a new instance of the attribute.</summary>
  /// <param name="fields">Fields. The parameter is optional.</param>
  public PXCheckUnique(params System.Type[] fields)
  {
    this._UniqueFields = new string[fields.Length + 1];
    for (int index = 0; index < fields.Length; ++index)
      this._UniqueFields[index] = fields[index].Name;
  }

  /// <exclude />
  public bool IgnoreNulls { get; set; } = true;

  public bool UniqueKeyIsPartOfPrimaryKey { get; set; }

  public bool IgnoreDuplicatesOnCopyPaste { get; set; }

  public bool PersistingCheck { get; set; } = true;

  /// <summary>
  /// Gets of sets the value that indicates whether the field value
  /// is cleared when it duplicates a value in another data record.
  /// By default, the property equals <see langword="true" />.
  /// </summary>
  public bool ClearOnDuplicate { get; set; } = true;

  /// <summary>
  /// Gets or sets the value of custom error message.
  /// If message is not set, then default message will be shown.
  /// </summary>
  public virtual string ErrorMessage
  {
    get => this._errorMessage ?? "An attempt was made to add a duplicate entry.";
    set => this._errorMessage = value;
  }

  /// <summary>Allow to suppress additional duplicate validation before saving record to the database.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXCheckUnique</tt> type.</param>
  /// <typeparam name="Field">The field whose attribute is affected.</typeparam>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXCheckUnique</tt> type.</param>
  /// <param name="check">The value that is assigned to the
  /// <tt>PersisintCheck</tt> property.</param>
  public static void SetPersistingCheck<Field>(PXCache cache, bool check) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>((object) null))
    {
      if (attribute is PXCheckUnique)
        ((PXCheckUnique) attribute).PersistingCheck = check;
    }
  }

  /// <summary>Allow to suppress additional duplicate validation before saving record to the database.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXCheckUnique</tt> type.</param>
  /// <param name="name">The field name.</param>
  /// <param name="check">The value that is assigned to the
  /// <tt>PersisintCheck</tt> property.</param>
  public static void SetPersistingCheck(PXCache cache, string name, bool check)
  {
    foreach (PXCheckUnique pxCheckUnique in cache.GetAttributesOfType<PXCheckUnique>((object) null, name))
    {
      if (pxCheckUnique != null)
        pxCheckUnique.PersistingCheck = check;
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Graph.FieldDefaulting.AddHandler(sender.GetItemType(), this._FieldName, new PXFieldDefaulting(this.OnFieldDefaulting));
    this.CreateViewOnCacheAttached(sender);
  }

  /// <exclude />
  protected virtual void CreateViewOnCacheAttached(PXCache sender)
  {
    this._UniqueFields[this._UniqueFields.Length - 1] = this._FieldName;
    System.Type itemType = sender.GetItemType();
    System.Type type1 = this.Where;
    if ((object) type1 == null)
      type1 = typeof (PX.Data.Where<True, Equal<True>>);
    System.Type type2 = type1;
    for (int index = 0; index < this._UniqueFields.Length; ++index)
    {
      System.Type bqlField = sender.GetBqlField(this._UniqueFields[index]);
      System.Type type3;
      if (!this.IgnoreNulls)
        type3 = BqlCommand.Compose(typeof (Where2<,>), typeof (PX.Data.Where<,,>), bqlField, typeof (IsNull), typeof (And<,,>), typeof (Current<>), bqlField, typeof (IsNull), typeof (Or<,>), bqlField, typeof (Equal<>), typeof (Current<>), bqlField, typeof (And<>), type2);
      else
        type3 = BqlCommand.Compose(typeof (Where2<,>), typeof (PX.Data.Where<,>), bqlField, typeof (Equal<>), typeof (Current<>), bqlField, typeof (And<>), type2);
      type2 = type3;
    }
    System.Type type4 = BqlCommand.Compose(typeof (Select<,>), itemType, type2);
    this._View = new PXView(sender.Graph, false, BqlCommand.CreateInstance(type4));
  }

  /// <exclude />
  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (e.Row == null || !this.IgnoreNulls && ((IEnumerable<string>) this._UniqueFields).Any<string>((Func<string, bool>) (field => sender.GetValue(e.Row, field) == null)))
      return;
    e.Cancel = !this.ValidateDuplicates(sender, e.Row, (object) null);
  }

  /// <exclude />
  public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    this.ClearErrors(sender, e.NewRow);
    if (e.Row != null && e.NewRow != null && this.CheckUpdated(sender, e.Row, e.NewRow))
      e.Cancel = !this.ValidateDuplicates(sender, e.NewRow, e.Row);
    if (!this.ClearOnDuplicate || !PXCheckUnique.CheckEquals(sender.GetValue(e.Row, this._FieldName), sender.GetValue(e.NewRow, this._FieldName)) || !e.Cancel)
      return;
    this.ClearErrors(sender, e.NewRow);
    sender.SetValue(e.NewRow, this._FieldName, (object) null);
    e.Cancel = !this.ValidateDuplicates(sender, e.NewRow, e.Row);
  }

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null || e.Operation == PXDBOperation.Delete || !this.PersistingCheck)
      return;
    e.Cancel = !this.ValidateDuplicates(sender, e.Row, (object) null);
  }

  protected void ClearErrors(PXCache sender, object row)
  {
    foreach (string uniqueField in this._UniqueFields)
    {
      string error = PXUIFieldAttribute.GetError(sender, row, uniqueField);
      if (!string.IsNullOrEmpty(error) && this.CanClearError(error))
        PXUIFieldAttribute.SetError(sender, row, uniqueField, (string) null);
    }
  }

  protected virtual bool CanClearError(string errorText)
  {
    return PXMessages.Localize(this.ErrorMessage).EndsWith(errorText);
  }

  protected virtual void OnFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.callInprocess || e.Cancel)
      return;
    this.callInprocess = true;
    object newValue = (object) null;
    sender.RaiseFieldDefaulting(this._FieldName, e.Row, out newValue);
    if (newValue != null)
    {
      object copy = sender.CreateCopy(e.Row);
      sender.SetValue(copy, this._FieldName, newValue);
      e.NewValue = this.ValidateDuplicates(sender, copy, (object) null) || !this.ClearOnDuplicate ? newValue : (object) null;
      e.Cancel = true;
    }
    this.callInprocess = false;
  }

  protected bool CheckUpdated(PXCache sender, object row, object newRow)
  {
    foreach (string uniqueField in this._UniqueFields)
    {
      if (!PXCheckUnique.CheckEquals(sender.GetValue(row, uniqueField), sender.GetValue(newRow, uniqueField)))
        return true;
    }
    return false;
  }

  /// <summary>
  /// Checks whether the provided objects are equal, ignoring the case
  /// if the provided objects are strings.
  /// </summary>
  /// <param name="v1">The first object to compare.</param>
  /// <param name="v2">The second object to compare.</param>
  /// <returns></returns>
  public static bool CheckEquals(object v1, object v2)
  {
    return !(v1 is string) && !(v2 is string) ? object.Equals(v1, v2) : string.Compare((string) v1, (string) v2, true) == 0;
  }

  protected virtual bool CheckDefaults(PXCache sender, object row)
  {
    foreach (string uniqueField in this._UniqueFields)
    {
      bool flag = false;
      foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes(row, uniqueField))
      {
        if (attribute is PXDefaultAttribute && ((PXDefaultAttribute) attribute).PersistingCheck != PXPersistingCheck.Nothing)
        {
          flag = sender.GetValue(row, uniqueField) == null;
          break;
        }
      }
      if (flag)
        return false;
    }
    return true;
  }

  protected virtual bool ValidateDuplicates(PXCache sender, object row, object oldRow)
  {
    if (!this.IgnoreNulls || this.CheckDefaults(sender, row) && sender.GetValue(row, this._FieldOrdinal) != null)
    {
      PXView view = this._View;
      object[] currents = new object[1]{ row };
      object[] objArray = Array.Empty<object>();
      foreach (object obj in view.SelectMultiBound(currents, objArray))
      {
        object sibling = obj;
        Lazy<string> lazy = Lazy.By<string>((Func<string>) (() => this.PrepareMessage(sender, row, sibling)));
        if (!sender.ObjectsEqual(sibling, row) || sibling != row && this.UniqueKeyIsPartOfPrimaryKey && sender.GetStatus(row) != PXEntryStatus.Inserted)
        {
          foreach (string uniqueField in this._UniqueFields)
          {
            if (oldRow == null || !PXCheckUnique.CheckEquals(sender.GetValue(row, uniqueField), sender.GetValue(oldRow, uniqueField)))
            {
              PXFieldState valueExt = sender.GetValueExt(row, uniqueField) as PXFieldState;
              sender.RaiseExceptionHandling(uniqueField, row, valueExt != null ? valueExt.Value : sender.GetValue(row, uniqueField), (Exception) new PXSetPropertyException(lazy.Value));
            }
          }
          return this.IgnoreDuplicatesOnCopyPaste && sender.Graph.IsCopyPasteContext;
        }
      }
    }
    return true;
  }

  protected virtual string PrepareMessage(PXCache cache, object currentRow, object duplicateRow)
  {
    return this.ErrorMessage;
  }
}
