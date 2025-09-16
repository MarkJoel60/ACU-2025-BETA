// Decompiled with JetBrains decompiler
// Type: PX.Data.Selector`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Searches for the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> attribute
/// on the key field and calculates the provided expression for the data record
/// currently referenced by the attribute.
/// </summary>
/// <typeparam name="KeyField">The key field to which the PXSelector attribute
/// should be attached.</typeparam>
/// <typeparam name="ForeignOperand">The expression that is calculated for the
/// data record currently referenced by PXSelector.</typeparam>
/// <remarks>You can use fluent BQL <see cref="T:PX.Data.BQL.BqlOperand`2.FromSelectorOf`1" /> instead.</remarks>
/// <example>
/// <code>
/// [PXFormula(typeof(
///     Selector&lt;APPaymentChargeTran.entryTypeID,
///              Selector&lt;CAEntryType.accountID, Account.accountCD&gt;&gt;))]
/// public virtual int? AccountID { get; set; }
/// </code>
/// </example>
public sealed class Selector<KeyField, ForeignOperand> : IBqlCreator, IBqlVerifier, IBqlOperand
  where KeyField : IBqlOperand
  where ForeignOperand : IBqlOperand
{
  private IBqlParameter _operand;
  private IBqlCreator _foreignOperand;

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (typeof (IBqlField).IsAssignableFrom(typeof (KeyField)))
    {
      if (info.Fields != null)
      {
        System.Type type = typeof (KeyField);
        info.Fields.Add(type);
        List<PXEventSubscriberAttribute> attributesReadonly = graph?.Caches[type.DeclaringType]?.GetAttributesReadonly(type.Name);
        if (attributesReadonly == null)
          return true;
        foreach (PXEventSubscriberAttribute subscriberAttribute in attributesReadonly)
        {
          BqlCommand select = subscriberAttribute is PXSelectorAttribute selectorAttribute ? selectorAttribute.GetSelect() : (BqlCommand) null;
          if (select != null)
          {
            System.Type[] referencedFields = select.GetReferencedFields(false);
            info.Fields.AddRange((IEnumerable<System.Type>) referencedFields);
          }
        }
      }
      return true;
    }
    if (this._operand == null)
      this._operand = this._operand.createOperand<KeyField>() as IBqlParameter;
    if (this._operand == null)
      throw new PXArgumentException(nameof (KeyField), "'{0}' either has to be a class field or has to expose the IBqlParameter interface.");
    return this._operand.AppendExpression(ref exp, graph, info, selection);
  }

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    System.Type type = typeof (KeyField);
    value = (object) null;
    object data;
    System.Type itemType;
    if (typeof (IBqlParameter).IsAssignableFrom(type))
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<KeyField>() as IBqlParameter;
      System.Type field = this._operand != null ? this._operand.GetReferencedType() : throw new PXArgumentException(nameof (KeyField), "'{0}' either has to be a class field or has to expose the IBqlParameter interface.");
      if (!(cache.GetItemType() == BqlCommand.GetItemType(field)) && !BqlCommand.GetItemType(field).IsAssignableFrom(cache.GetItemType()))
      {
        this._operand.Verify(cache, item, pars, ref result, ref value);
        cache = cache.Graph.Caches[BqlCommand.GetItemType(field)];
        item = cache.Current;
        data = PXSelectorAttribute.SelectSingle(cache, item, field.Name, value);
      }
      else
        data = PXSelectorAttribute.SelectSingle(cache, BqlFormula.ItemContainer.Unwrap(item), field.Name);
      itemType = PXSelectorAttribute.GetItemType(cache, field.Name);
    }
    else
    {
      if (!typeof (IBqlField).IsAssignableFrom(type))
        throw new PXArgumentException(nameof (KeyField), "'{0}' either has to be a class field or has to expose the IBqlParameter interface.");
      if (!(cache.GetItemType() == BqlCommand.GetItemType(type)) && !BqlCommand.GetItemType(type).IsAssignableFrom(cache.GetItemType()))
        return;
      if (item is BqlFormula.ItemContainer itemContainer)
        itemContainer.InvolvedFields.Add(type);
      itemType = PXSelectorAttribute.GetItemType(cache, type.Name);
      data = (!typeof (IBqlField).IsAssignableFrom(typeof (ForeignOperand)) ? 1 : (itemType == BqlCommand.GetItemType(typeof (ForeignOperand)) ? 1 : (BqlCommand.GetItemType(typeof (ForeignOperand)).IsAssignableFrom(itemType) ? 1 : 0))) != 0 ? PXSelectorAttribute.Select(cache, BqlFormula.ItemContainer.Unwrap(item), type.Name) : PXSelectorAttribute.SelectSingle(cache, BqlFormula.ItemContainer.Unwrap(item), type.Name);
    }
    if (data == null)
      return;
    PXCache cach1 = cache.Graph.Caches[itemType];
    if (typeof (IBqlField).IsAssignableFrom(typeof (ForeignOperand)))
    {
      if (data is PXResult pxResult)
      {
        for (int i = 0; i < pxResult.TableCount; ++i)
        {
          PXCache cach2 = cache.Graph.Caches[pxResult.GetItemType(i)];
          if (cach2.GetItemType() == BqlCommand.GetItemType(typeof (ForeignOperand)) || BqlCommand.GetItemType(typeof (ForeignOperand)).IsAssignableFrom(cach2.GetItemType()))
          {
            value = cach2.GetValue(pxResult[i], typeof (ForeignOperand).Name);
            return;
          }
        }
        value = (object) null;
      }
      else if (!(cach1.GetItemType() == BqlCommand.GetItemType(typeof (ForeignOperand))) && !BqlCommand.GetItemType(typeof (ForeignOperand)).IsAssignableFrom(cach1.GetItemType()))
        value = (object) null;
      else
        value = cach1.GetValue(data, typeof (ForeignOperand).Name);
    }
    else
    {
      if (this._foreignOperand == null)
        this._foreignOperand = (object) Activator.CreateInstance<ForeignOperand>() as IBqlCreator;
      if (this._foreignOperand == null)
        throw new PXArgumentException(nameof (ForeignOperand), "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
      this._foreignOperand.Verify(cach1, data is PXResult ? ((PXResult) data)[0] : data, pars, ref result, ref value);
    }
  }
}
