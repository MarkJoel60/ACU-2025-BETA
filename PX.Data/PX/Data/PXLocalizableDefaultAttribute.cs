// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLocalizableDefaultAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Localization;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Allows you to have a multi-language field with a default value in a specific language.</summary>
/// <remarks>Use this attribute instead of the <tt>PXDefault</tt> attribute and specify in its second parameter either a BQL field or a BQL select that provides language
/// selection.</remarks>
/// <example><para>For example, in Acumatica ERP, the SOLine line description defaulted to the appropriate InventoryItem description based on the language that is set for a customer. The TransactionDesr field of the SOLine DAC has the PXLocalizableDefault attribute with a second parameter that specifies the language as follows: typeof(Customer.languageName). </para>
/// 	<code title="Example" lang="CS">
/// PXLocalizableDefault(typeof(Search&lt;InventoryItem.descr,
///    Where&lt;InventoryItem.inventoryID,
///    Equal&lt;Current&lt;SOLine.inventoryID&gt;&gt;&gt;&gt;),
///    typeof(Customer.languageName),
///    PersistingCheck = PXPersistingCheck.Nothing)]</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXDefaultAttribute))]
public class PXLocalizableDefaultAttribute : PXDefaultAttribute
{
  protected System.Type _LanguageSourceType;
  protected string _LanguageSourceField;
  protected BqlCommand _LanguageSelect;

  [InjectDependency]
  private ILocalizableFieldService LocalizableFieldService { get; set; }

  /// <exclude />
  public PXLocalizableDefaultAttribute(System.Type sourceType, System.Type languageSourceType)
    : base(sourceType)
  {
    if (languageSourceType == (System.Type) null)
      throw new PXArgumentException("languageType", "The argument cannot be null.");
    if (typeof (IBqlSearch).IsAssignableFrom(languageSourceType))
    {
      this._LanguageSelect = BqlCommand.CreateInstance(languageSourceType);
      this._LanguageSourceType = BqlCommand.GetItemType(((IBqlSearch) this._LanguageSelect).GetField());
      this._LanguageSourceField = ((IBqlSearch) this._LanguageSelect).GetField().Name;
    }
    else if (typeof (IBqlSelect).IsAssignableFrom(languageSourceType))
    {
      this._LanguageSelect = BqlCommand.CreateInstance(languageSourceType);
      this._LanguageSourceType = this._LanguageSelect.GetTables()[0];
    }
    else if (languageSourceType.IsNested && typeof (IBqlField).IsAssignableFrom(languageSourceType))
    {
      this._LanguageSourceType = BqlCommand.GetItemType(languageSourceType);
      this._LanguageSourceField = languageSourceType.Name;
    }
    else
      this._LanguageSourceType = typeof (IBqlTable).IsAssignableFrom(languageSourceType) ? languageSourceType : throw new PXArgumentException("languageType", "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) sourceType
      });
  }

  /// <exclude />
  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    string language = (string) null;
    if (e.NewValue == null && this.LocalizableFieldService.IsFieldEnabled(this.BqlTable.Name, this.FieldName) && (this._LanguageSelect == null || this._SearchOnDefault || e.Row != null))
    {
      if (this._LanguageSelect != null)
      {
        object data = sender.Graph.TypedViews.GetView(this._LanguageSelect, true).SelectSingleBound(new object[1]
        {
          e.Row
        });
        if (data is PXResult)
          data = ((PXResult) data)[this._LanguageSourceType];
        if (data != null)
          language = sender.Graph.Caches[this._LanguageSourceType].GetValue(data, this._LanguageSourceField ?? this._FieldName) as string;
      }
      else if (this._LanguageSourceType != (System.Type) null)
      {
        PXCache cach = sender.Graph.Caches[this._LanguageSourceType];
        if (cach.Current != null)
          language = cach.GetValue(cach.Current, this._LanguageSourceField == null ? this._FieldName : this._LanguageSourceField) as string;
      }
    }
    if (string.IsNullOrWhiteSpace(language))
    {
      base.FieldDefaulting(sender, e);
    }
    else
    {
      string fieldName = this._SourceField == null ? this._FieldName : this._SourceField;
      if (this._Select != null)
      {
        Tuple<object, PXCache> tuple = PXDefaultAttribute.SelectRow(sender, (PXDefaultAttribute) this, e.Row);
        if (tuple != null && tuple.Item1 != null)
        {
          e.NewValue = (object) PXDBLocalizableStringAttribute.GetTranslation(tuple.Item2, tuple.Item1, fieldName, language);
          if (e.NewValue != null)
          {
            e.Cancel = true;
            return;
          }
        }
      }
      else if (this._SourceType != (System.Type) null)
      {
        PXCache cach = sender.Graph.Caches[this._SourceType];
        if (cach.Current != null)
        {
          e.NewValue = (object) PXDBLocalizableStringAttribute.GetTranslation(cach, cach.Current, fieldName, language);
          if (e.NewValue != null)
          {
            e.Cancel = true;
            return;
          }
        }
      }
      else if (this._Formula != null)
      {
        bool? result = new bool?();
        object obj = (object) null;
        BqlFormula.Verify(sender, e.Row, this._Formula, ref result, ref obj);
        e.NewValue = obj;
      }
      if (this._Constant == null)
        return;
      e.NewValue = this._Constant;
    }
  }
}
