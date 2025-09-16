// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIVerifyAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>This attribute enables verifying a field at runtime.</summary>
/// <remarks>A field is verified when one of the following events occur:
/// <tt>RowInserted</tt>, <tt>RowPersisting</tt>, <tt>FieldVerifying</tt>. You can also specify that the
/// verification of a field is performed when the <tt>RowSelected</tt> event occurs;
/// you can do it by assigning <tt>true</tt> to the <tt>OnSelectingVerify</tt>
/// parameter of the constructor.</remarks>
/// <seealso cref="T:PX.Data.PXUIEnabledAttribute" />
/// <example>
/// <code lang="CS">
/// [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof(POReceiptLine.receiptNbr))]
/// [PXUIField(DisplayName = "Receipt Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
/// // ...
/// [PXUIVerify(typeof(Where&lt;receiptCuryID, Equal&lt;Current&lt;APInvoice.curyID&gt;&gt;&gt;),  // IBqlWhere used
///     PXErrorLevel.RowWarning,
///     Messages.APDocumentCurrencyDiffersFromSourceDocument,
///     true /* also validate on RowSelected */)]
/// public virtual String ReceiptNbr { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
public class PXUIVerifyAttribute : 
  PXBaseConditionAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowInsertedSubscriber,
  IPXRowPersistingSubscriber
{
  protected PXErrorLevel _ErrorLevel;
  protected string _Message;
  protected System.Type[] _args;
  [Obsolete("This field has been deprecated and will be removed in Acumatica 7.0")]
  protected bool _OnSelectingVerify;
  [Obsolete("This field has been deprecated and will be removed in Acumatica 7.0")]
  protected bool _CheckOnVerifying;
  [Obsolete("This field has been deprecated and will be removed in Acumatica 7.0")]
  protected bool _CheckOnInserted;

  public TriggerPoints VerificationPoints { get; protected set; }

  private void SetVerificationPoint(TriggerPoints triggerPoint, bool value)
  {
    if (value)
      this.VerificationPoints |= triggerPoint;
    else
      this.VerificationPoints &= ~triggerPoint;
  }

  public bool CheckOnVerify
  {
    get => this.VerificationPoints.HasFlag((Enum) TriggerPoints.FieldVerifying);
    set => this.SetVerificationPoint(TriggerPoints.FieldVerifying, value);
  }

  public bool CheckOnInserted
  {
    get => this.VerificationPoints.HasFlag((Enum) TriggerPoints.RowInserted);
    set => this.SetVerificationPoint(TriggerPoints.RowInserted, value);
  }

  public bool CheckOnRowSelected
  {
    get => this.VerificationPoints.HasFlag((Enum) TriggerPoints.RowSelected);
    set => this.SetVerificationPoint(TriggerPoints.RowSelected, value);
  }

  public bool CheckOnRowPersisting
  {
    get => this.VerificationPoints.HasFlag((Enum) TriggerPoints.RowPersisting);
    set => this.SetVerificationPoint(TriggerPoints.RowPersisting, value);
  }

  /// <summary>
  /// Gets or sets a value indicating whether BQL fields specified by
  /// <see cref="F:PX.Data.PXUIVerifyAttribute._args" /> should be converted to field names, not field values,
  /// when composing the error message.
  /// </summary>
  public bool MessageArgumentsAreFieldNames { get; set; }

  public PXUIVerifyAttribute(
    System.Type conditionType,
    PXErrorLevel errorLevel,
    string message,
    bool OnSelectingVerify,
    params System.Type[] args)
    : this(conditionType, errorLevel, message, args)
  {
    this.CheckOnRowSelected = OnSelectingVerify;
  }

  public PXUIVerifyAttribute(
    System.Type conditionType,
    TriggerPoints verificationPoints,
    PXErrorLevel errorLevel,
    string message,
    params System.Type[] args)
    : this(conditionType, errorLevel, message, args)
  {
    this.VerificationPoints = verificationPoints;
  }

  public PXUIVerifyAttribute(
    System.Type conditionType,
    PXErrorLevel errorLevel,
    string message,
    params System.Type[] args)
    : base(conditionType)
  {
    this._ErrorLevel = errorLevel;
    this._Message = message;
    this._args = args;
    this.VerificationPoints = TriggerPoints.RowInserted | TriggerPoints.RowPersisting | TriggerPoints.FieldVerifying;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this._Formula == null)
      return;
    List<System.Type> typeList = new List<System.Type>();
    SQLExpression sqlExpression = SQLExpression.None();
    IBqlCreator formula = this._Formula;
    ref SQLExpression local = ref sqlExpression;
    PXGraph graph = sender.Graph;
    BqlCommandInfo info = new BqlCommandInfo(false);
    info.Fields = typeList;
    info.BuildExpression = false;
    BqlCommand.Selection selection = new BqlCommand.Selection();
    formula.AppendExpression(ref local, graph, info, selection);
    foreach (System.Type field in typeList)
    {
      if (field.IsNested && (BqlCommand.GetItemType(field) == sender.GetItemType() || sender.GetItemType().IsSubclassOf(BqlCommand.GetItemType(field))) && !field.Name.Equals(this._FieldName, StringComparison.OrdinalIgnoreCase))
        sender.FieldUpdatedEvents[field.Name.ToLower()] += (PXFieldUpdated) ((cache, e) => this.dependentFieldUpdated(cache, e.Row));
    }
    if (!this.CheckOnRowSelected)
      return;
    sender.RowSelected += (PXRowSelected) ((cache, e) =>
    {
      PXSetPropertyException error = PXUIVerifyAttribute.VerifyingAndGetError(this, sender, e.Row);
      object newValue = (object) null;
      if (error != null && error.ErrorLevel == PXErrorLevel.Error)
      {
        newValue = sender.GetValueExt(e.Row, this._FieldName);
        if (newValue is PXFieldState)
          newValue = ((PXFieldState) newValue).Value;
      }
      cache.RaiseExceptionHandling(this.FieldName, e.Row, newValue, (Exception) error);
    });
  }

  protected virtual void dependentFieldUpdated(PXCache sender, object row)
  {
    object valueExt = sender.GetValueExt(row, this._FieldName);
    if (valueExt is PXFieldState)
      valueExt = ((PXFieldState) valueExt).Value;
    try
    {
      if (this.CheckOnVerify)
        this.Verifying(sender, row);
      sender.RaiseExceptionHandling(this._FieldName, row, valueExt, (Exception) null);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling(this._FieldName, row, valueExt, (Exception) ex);
    }
  }

  private void Verifying(PXCache sender, object row)
  {
    PXSetPropertyException error = PXUIVerifyAttribute.VerifyingAndGetError(this, sender, row);
    if (error != null)
      throw error;
  }

  private static PXSetPropertyException VerifyingAndGetError(
    PXUIVerifyAttribute attr,
    PXCache sender,
    object row)
  {
    if (row == null)
      return (PXSetPropertyException) null;
    if (attr == null)
      throw new PXException("The attribute '{0}' cannot be found.", new object[1]
      {
        (object) typeof (PXUIVerifyAttribute).Name
      });
    if (!(attr.Formula != (System.Type) null) || PXBaseConditionAttribute.GetConditionResult(sender, row, attr.Formula))
      return (PXSetPropertyException) null;
    List<object> objectList = new List<object>();
    foreach (System.Type type in attr._args)
    {
      if (typeof (IConstant).IsAssignableFrom(type))
      {
        IConstant instance = Activator.CreateInstance(type) as IConstant;
        objectList.Add(instance?.Value);
      }
      else if (attr.MessageArgumentsAreFieldNames)
        objectList.Add((object) PXUIFieldAttribute.GetDisplayName(sender, type.Name));
      else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Current<>))
      {
        System.Type genericArgument = type.GetGenericArguments()[0];
        PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(genericArgument)];
        objectList.Add(cach.GetValue(cach.Current, genericArgument.Name));
      }
      else
      {
        object valueExt = sender.GetValueExt(row, type.Name);
        if (valueExt is PXFieldState)
          objectList.Add(((PXFieldState) valueExt).Value);
        else
          objectList.Add(valueExt);
      }
    }
    return new PXSetPropertyException(attr._Message, attr._ErrorLevel, objectList.ToArray());
  }

  public static PXSetPropertyException VerifyingAndGetError<Field>(PXCache sender, object row) where Field : IBqlField
  {
    PXSetPropertyException error1 = (PXSetPropertyException) null;
    foreach (PXSetPropertyException error2 in sender.GetAttributes<Field>(row).OfType<PXUIVerifyAttribute>().Select<PXUIVerifyAttribute, PXSetPropertyException>((Func<PXUIVerifyAttribute, PXSetPropertyException>) (attr => PXUIVerifyAttribute.VerifyingAndGetError(attr, sender, row))).Where<PXSetPropertyException>((Func<PXSetPropertyException, bool>) (currentError => currentError != null)))
    {
      switch (error2.ErrorLevel)
      {
        case PXErrorLevel.RowInfo:
          if (error1 == null || error1.ErrorLevel != PXErrorLevel.Warning)
          {
            error1 = error2;
            continue;
          }
          continue;
        case PXErrorLevel.Warning:
          if (error1 == null || error1.ErrorLevel != PXErrorLevel.RowWarning)
          {
            error1 = error2;
            continue;
          }
          continue;
        case PXErrorLevel.RowWarning:
        case PXErrorLevel.Error:
          if (error1 == null || error1.ErrorLevel != PXErrorLevel.Error)
          {
            error1 = error2;
            continue;
          }
          continue;
        case PXErrorLevel.RowError:
          return error2;
        default:
          error1 = error2;
          continue;
      }
    }
    return error1;
  }

  void IPXFieldVerifyingSubscriber.FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this.CheckOnVerify)
      return;
    object copy = sender.CreateCopy(e.Row);
    sender.SetValue(copy, this._FieldName, e.NewValue);
    try
    {
      this.Verifying(sender, copy);
      sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) null);
    }
    catch (PXSetPropertyException ex)
    {
      object valueExt = sender.GetValueExt(copy, this._FieldName);
      if (valueExt is PXFieldState)
        valueExt = ((PXFieldState) valueExt).Value;
      sender.RaiseExceptionHandling(this._FieldName, e.Row, valueExt, (Exception) ex);
    }
  }

  void IPXRowInsertedSubscriber.RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!this.CheckOnInserted)
      return;
    try
    {
      this.Verifying(sender, e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      object newValue = sender.GetValue(e.Row, this._FieldName);
      sender.RaiseExceptionHandling(this._FieldName, e.Row, newValue, (Exception) ex);
    }
  }

  void IPXRowPersistingSubscriber.RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!this.CheckOnRowPersisting)
      return;
    try
    {
      this.Verifying(sender, e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      object valueExt = sender.GetValueExt(e.Row, this._FieldName);
      if (valueExt is PXFieldState)
        valueExt = ((PXFieldState) valueExt).Value;
      sender.RaiseExceptionHandling(this._FieldName, e.Row, valueExt, (Exception) ex);
    }
  }
}
