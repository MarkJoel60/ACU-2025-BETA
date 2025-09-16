// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PlugInSettingsListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class PlugInSettingsListAttribute : PXAggregateAttribute
{
  protected PlugInSettingsListAttribute.PXFieldValuesListInternal ListAttr
  {
    get => (PlugInSettingsListAttribute.PXFieldValuesListInternal) this._Attributes[0];
  }

  protected PXDBStringAttribute DBStringAttr => (PXDBStringAttribute) this._Attributes[1];

  public bool ExclusiveValues
  {
    get => this.ListAttr.ExclusiveValues;
    set => this.ListAttr.ExclusiveValues = value;
  }

  public bool IsKey
  {
    get => ((PXDBFieldAttribute) this.DBStringAttr).IsKey;
    set => ((PXDBFieldAttribute) this.DBStringAttr).IsKey = value;
  }

  /// <param name="length">The maximum length of a field value.</param>
  /// <param name="graphType">Graph that contains cache with the field.</param>
  /// <param name="fieldType">Field that need to be provided with value.</param>
  public PlugInSettingsListAttribute(int length, Type graphType, Type fieldType)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PlugInSettingsListAttribute.PXFieldValuesListInternal(graphType, fieldType));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDBStringAttribute(length)
    {
      InputMask = "",
      IsUnicode = true
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PlugInSettingsListAttribute.CAFormulaEditorWithCustomFieldsAttribute());
  }

  internal void SetList(PXCache cache, string[] values, string[] labels)
  {
    this.ListAttr.SetList(cache, values, labels);
  }

  protected class CAFormulaEditorWithCustomFieldsAttribute : PXFormulaEditorAttribute
  {
    public CAFormulaEditorWithCustomFieldsAttribute()
    {
      ((PXAggregateAttribute) this)._Attributes.Remove((PXEventSubscriberAttribute) ((IEnumerable) ((PXAggregateAttribute) this)._Attributes).OfType<PXDBStringAttribute>().First<PXDBStringAttribute>());
      ((PXEntityAttribute) this)._DBAttrIndex = -1;
      ((PXAggregateAttribute) this)._Attributes.Remove((PXEventSubscriberAttribute) ((IEnumerable) ((PXAggregateAttribute) this)._Attributes).OfType<PXStringAttribute>().First<PXStringAttribute>());
      ((PXEntityAttribute) this)._NonDBAttrIndex = -1;
      ((PXAggregateAttribute) this)._Attributes.Remove((PXEventSubscriberAttribute) ((IEnumerable) ((PXAggregateAttribute) this)._Attributes).OfType<PXUIFieldAttribute>().First<PXUIFieldAttribute>());
      ((PXEntityAttribute) this)._UIAttrIndex = -1;
    }

    protected virtual void FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
    {
      if (e.Row != null)
      {
        ACHPlugInParameter row = (ACHPlugInParameter) e.Row;
        if ((row != null ? (row.IsFormula.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
      }
      if (e.Row == null && e.ReturnState is PXStringState returnState)
      {
        returnState.AllowedValues = new string[1]
        {
          string.Empty
        };
        returnState.AllowedLabels = new string[1]
        {
          string.Empty
        };
      }
      base.FieldSelecting(cache, e);
    }
  }

  protected class PXFieldValuesListInternal : PXStringListAttribute, IPXFieldUpdatingSubscriber
  {
    private readonly PXGraph _Graph;
    private readonly Type _CacheItemType;
    private readonly string _CacheFieldName;
    private Dictionary<string, string> _RemittanceDetailsDesc;
    private Dictionary<string, string> _VendorDetailsDesc;
    private Dictionary<string, string> _RemittanceDetailsID;
    private Dictionary<string, string> _VendorDetailsID;

    /// <param name="graphType">Graph that contains cache with the field.</param>
    /// <param name="fieldType">Field that need to be provided with value.</param>
    public PXFieldValuesListInternal(Type graphType, Type fieldType)
    {
      if (fieldType == (Type) null)
        throw new PXArgumentException(nameof (fieldType), "The argument cannot be null.");
      if (!fieldType.IsNested || !typeof (IBqlField).IsAssignableFrom(fieldType))
        throw new PXArgumentException(nameof (fieldType), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
        {
          (object) fieldType
        });
      this._Graph = PXGraph.CreateInstance(graphType);
      this._CacheItemType = BqlCommand.GetItemType(fieldType);
      this._CacheFieldName = fieldType.Name;
    }

    internal void SetList(PXCache cache, string[] values, string[] labels)
    {
      cache.SetAltered(((PXEventSubscriberAttribute) this).FieldName, true);
      PXStringListAttribute.SetListInternal((IEnumerable<PXStringListAttribute>) EnumerableExtensions.AsSingleEnumerable<PlugInSettingsListAttribute.PXFieldValuesListInternal>(this), values, labels, cache);
    }

    public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (e.NewValue == null)
        return;
      ACHPlugInParameter row = (ACHPlugInParameter) e.Row;
      int? type1 = row.Type;
      int num = 0;
      if (!(type1.GetValueOrDefault() == num & type1.HasValue))
      {
        type1 = row.Type;
        if (type1.GetValueOrDefault() != 1)
        {
          e.NewValue = (object) e.NewValue.ToString();
          return;
        }
      }
      PXFieldUpdatingEventArgs updatingEventArgs = e;
      PXGraph graph = sender.Graph;
      type1 = row.Type;
      int type2 = type1.Value;
      string str = e.NewValue.ToString();
      string paymentDetailId = this.GetPaymentDetailID(graph, (SelectorType) type2, str);
      updatingEventArgs.NewValue = (object) paymentDetailId;
    }

    private string GetPaymentDetailID(PXGraph graph, SelectorType type, string value)
    {
      if (string.IsNullOrEmpty(value))
        return value;
      if (type != null)
      {
        if (type != 1)
          throw new NotImplementedException();
        if (this._VendorDetailsDesc == null)
        {
          this._VendorDetailsDesc = new Dictionary<string, string>();
          foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) (graph as PaymentMethodMaint).DetailsForVendor).Select(Array.Empty<object>()))
          {
            PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
            this._VendorDetailsDesc.Add(paymentMethodDetail.Descr, paymentMethodDetail.DetailID);
          }
        }
        string str;
        return !this._VendorDetailsDesc.TryGetValue(value, out str) ? value : str;
      }
      if (this._RemittanceDetailsDesc == null)
      {
        this._RemittanceDetailsDesc = new Dictionary<string, string>();
        foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) (graph as PaymentMethodMaint).DetailsForCashAccount).Select(Array.Empty<object>()))
        {
          PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
          this._RemittanceDetailsDesc.Add(paymentMethodDetail.Descr, paymentMethodDetail.DetailID);
        }
      }
      string str1;
      return !this._RemittanceDetailsDesc.TryGetValue(value, out str1) ? value : str1;
    }

    private string GetPaymentDetailDesc(PXGraph graph, SelectorType type, string value)
    {
      if (string.IsNullOrEmpty(value))
        return value;
      if (type != null)
      {
        if (type != 1)
          throw new NotImplementedException();
        if (this._VendorDetailsID == null)
        {
          this._VendorDetailsID = new Dictionary<string, string>();
          foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) (graph as PaymentMethodMaint).DetailsForVendor).Select(Array.Empty<object>()))
          {
            PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
            this._VendorDetailsID.Add(paymentMethodDetail.DetailID, paymentMethodDetail.Descr);
          }
        }
        string str;
        return !this._VendorDetailsID.TryGetValue(value, out str) ? value : str;
      }
      if (this._RemittanceDetailsID == null)
      {
        this._RemittanceDetailsID = new Dictionary<string, string>();
        foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) (graph as PaymentMethodMaint).DetailsForCashAccount).Select(Array.Empty<object>()))
        {
          PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
          this._RemittanceDetailsID.Add(paymentMethodDetail.DetailID, paymentMethodDetail.Descr);
        }
      }
      string str1;
      return !this._RemittanceDetailsID.TryGetValue(value, out str1) ? value : str1;
    }

    public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      ACHPlugInParameter row1 = (ACHPlugInParameter) e.Row;
      if (string.IsNullOrEmpty(row1?.ParameterID))
        return;
      bool? nullable1;
      if (row1 != null)
      {
        nullable1 = row1.IsGroupHeader;
        if (nullable1.GetValueOrDefault())
          goto label_6;
      }
      if (row1 != null)
      {
        // ISSUE: explicit non-virtual call
        nullable1 = __nonvirtual (row1.ReadOnly);
        if (nullable1.GetValueOrDefault())
          goto label_6;
      }
      (string, PXErrorLevel) errorWithLevel = PXUIFieldAttribute.GetErrorWithLevel<ACHPlugInParameter.value>(sender, e.Row);
      Dictionary<string, string> plugInSettings = (sender.Graph as PaymentMethodMaint).GetPlugInSettings();
      Dictionary<SelectorType?, string> plugInSelectorTypes = (sender.Graph as PaymentMethodMaint).GetPlugInSelectorTypes();
      string parameterId = row1?.ParameterID;
      string str1;
      ref string local1 = ref str1;
      int? type1;
      if (!plugInSettings.TryGetValue(parameterId, out local1) && row1 != null)
      {
        type1 = row1.Type;
        if (type1.HasValue)
        {
          Dictionary<SelectorType?, string> dictionary = plugInSelectorTypes;
          type1 = (int?) row1?.Type;
          SelectorType? key = type1.HasValue ? new SelectorType?((SelectorType) type1.GetValueOrDefault()) : new SelectorType?();
          ref string local2 = ref str1;
          dictionary.TryGetValue(key, out local2);
        }
      }
      if (string.IsNullOrEmpty(str1))
      {
        base.FieldSelecting(sender, e);
        if (!(e.ReturnState is PXStringState returnState) || returnState.AllowedValues.Length != 1 || returnState.AllowedValues[0] != null)
          return;
        returnState.AllowedValues = (string[]) null;
        returnState.AllowedLabels = (string[]) null;
        e.ReturnState = (object) returnState;
        return;
      }
      if (string.IsNullOrEmpty(row1?.ParameterID))
        return;
      Type cacheItemType = this._CacheItemType;
      object row2 = e.Row;
      Type type2 = typeof (ACHPlugInSettings);
      if (!sender.Graph.Views.Caches.Contains(typeof (ACHPlugInSettings)))
        sender.Graph.Views.Caches.Add(typeof (ACHPlugInSettings));
      if (!(sender.Graph.Caches[type2]?.GetStateExt((object) null, str1) is PXFieldState stateExt))
        return;
      stateExt.Enabled = true;
      stateExt.Visible = true;
      stateExt.DescriptionName = (string) null;
      stateExt.Value = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName);
      type1 = row1.Type;
      int num = 0;
      if (!(type1.GetValueOrDefault() == num & type1.HasValue))
      {
        type1 = row1.Type;
        if (type1.GetValueOrDefault() != 1)
          goto label_20;
      }
      PXFieldState pxFieldState = stateExt;
      PXGraph graph = sender.Graph;
      type1 = row1.Type;
      int type3 = type1.Value;
      string str2 = stateExt.Value?.ToString();
      string paymentDetailDesc = this.GetPaymentDetailDesc(graph, (SelectorType) type3, str2);
      pxFieldState.Value = (object) paymentDetailDesc;
label_20:
      if (stateExt.Value == null && stateExt.DataType == typeof (bool))
        sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName, (object) bool.FalseString);
      if (!string.IsNullOrEmpty(errorWithLevel.Item1))
      {
        stateExt.Error = errorWithLevel.Item1;
        stateExt.ErrorLevel = errorWithLevel.Item2;
      }
      else
      {
        stateExt.Error = (string) null;
        stateExt.ErrorLevel = (PXErrorLevel) 0;
      }
      ((CancelEventArgs) e).Cancel = true;
      e.ReturnState = (object) stateExt;
      return;
label_6:
      PXFieldSelectingEventArgs selectingEventArgs = e;
      object returnState1 = e.ReturnState;
      Type type4 = typeof (string);
      nullable1 = new bool?();
      bool? nullable2 = nullable1;
      nullable1 = new bool?();
      bool? nullable3 = nullable1;
      int? nullable4 = new int?();
      int? nullable5 = new int?();
      int? nullable6 = new int?();
      bool? nullable7 = new bool?(false);
      nullable1 = new bool?();
      bool? nullable8 = nullable1;
      nullable1 = new bool?();
      bool? nullable9 = nullable1;
      PXFieldState instance = PXFieldState.CreateInstance(returnState1, type4, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, "Value", (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
      selectingEventArgs.ReturnState = (object) instance;
    }
  }
}
