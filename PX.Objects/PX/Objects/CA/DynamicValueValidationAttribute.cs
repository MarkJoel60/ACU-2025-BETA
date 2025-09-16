// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.DynamicValueValidationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// This attribute allows to provide a dynamic validation rules for the field.<br />
/// The rule is defined as regexp and may be stored in some external field.<br />
/// In the costructor, one should provide a search method for this rule. <br />
/// </summary>
/// <example>
/// <code>
/// [DynamicValueValidation(typeof(Search&lt;PaymentMethodDetail.validRegexp,
/// 	Where&lt;PaymentMethodDetail.paymentMethodID, Equal&lt;Current&lt;VendorPaymentMethodDetail.paymentMethodID&gt;&gt;,
/// 	And&lt;PaymentMethodDetail.detailID, Equal&lt;Current&lt;VendorPaymentMethodDetail.detailID&gt;&gt;&gt;&gt;&gt;))]
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class DynamicValueValidationAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldVerifyingSubscriber
{
  protected Type _RegexpSearch;
  protected Type _SourceType;
  protected string _SourceField;
  protected BqlCommand _Select;
  protected int _Length;

  /// <summary>Ctor</summary>
  /// <param name="aRegexpSearch">Must be IBqlSearch returning a validation regexp.</param>
  public DynamicValueValidationAttribute(Type aRegexpSearch)
  {
    if (aRegexpSearch == (Type) null)
      throw new PXArgumentException("type", "The argument cannot be null.");
    this._RegexpSearch = typeof (IBqlSearch).IsAssignableFrom(aRegexpSearch) ? aRegexpSearch : throw new PXArgumentException("aSearchRegexp", "The ValidationField value is not valid.");
    this._Select = BqlCommand.CreateInstance(new Type[1]
    {
      aRegexpSearch
    });
    this._SourceType = BqlCommand.GetItemType(((IBqlSearch) this._Select).GetField());
    this._SourceField = ((IBqlSearch) this._Select).GetField().Name;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    string newValue = (string) e.NewValue;
    if (string.IsNullOrEmpty(newValue))
      return;
    string validationRegexp = this.FindValidationRegexp(sender, e.Row, out int? _);
    if (!this.ValidateValue(newValue, validationRegexp))
      throw new PXSetPropertyException("Provided value does not pass validation rules defined for this field.");
  }

  protected virtual void MultiSelectFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    int? controlType;
    if (!(e.NewValue is string) || this.FindValidationRegexp(sender, e.Row, out controlType) != null)
      return;
    if (controlType.GetValueOrDefault() == 6 && ((string) e.NewValue).TrimEnd().Length > this._Length)
      throw new PXSetPropertyException("The string value provided exceeds the field '{0}' length.", new object[1]
      {
        (object) $"[{this._FieldName}]"
      });
    DateTime result;
    if (controlType.GetValueOrDefault() != 5 || !sender.Graph.IsImport || sender.Graph.IsCopyPasteContext || !DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      return;
    e.NewValue = (object) result.ToString("M/d/yyyy");
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    foreach (PXDBStringAttribute pxdbStringAttribute in sender.GetAttributesReadonly(this._FieldName).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr is PXDBStringAttribute)).Cast<PXDBStringAttribute>())
      this._Length = pxdbStringAttribute.Length;
    if (this._Length <= 0)
      return;
    PXGraph.FieldUpdatingEvents fieldUpdating = sender.Graph.FieldUpdating;
    Type itemType = sender.GetItemType();
    string lower = this._FieldName.ToLower();
    DynamicValueValidationAttribute validationAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) validationAttribute, __vmethodptr(validationAttribute, MultiSelectFieldUpdating));
    fieldUpdating.AddHandler(itemType, lower, pxFieldUpdating);
  }

  protected virtual string FindValidationRegexp(PXCache sender, object row, out int? controlType)
  {
    controlType = new int?();
    if (this._Select == null)
      return (string) null;
    object obj = sender.Graph.TypedViews.GetView(this._Select, false).SelectSingleBound(new object[1]
    {
      row
    }, Array.Empty<object>());
    if (obj != null && obj is PXResult)
      obj = ((PXResult) obj)[this._SourceType];
    string validationRegexp = (string) sender.Graph.Caches[this._SourceType].GetValue(obj, this._SourceField == null ? this._FieldName : this._SourceField);
    if (!typeof (CSAttribute).IsAssignableFrom(this._SourceType) || obj == null)
      return validationRegexp;
    controlType = (int?) sender.Graph.Caches[this._SourceType].GetValue<CSAttribute.controlType>(obj);
    return validationRegexp;
  }

  protected virtual bool ValidateValue(string val, string regex)
  {
    return val == null || regex == null || new Regex(regex).IsMatch(val);
  }
}
