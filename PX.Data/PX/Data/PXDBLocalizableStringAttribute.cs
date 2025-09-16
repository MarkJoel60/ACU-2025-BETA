// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLocalizableStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Newtonsoft.Json;
using PX.Common;
using PX.Common.Extensions;
using PX.Data.Localization;
using PX.Data.Maintenance;
using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points.DbmsBase;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>Allows you to configure a DAC field to have values in multiple languages.</summary>
/// <remarks>The <tt>PXDBLocalizableString</tt> attribute works similarly to the <tt>PXDBString</tt> attribute, but unlike the  <tt>PXDBString</tt> attribute,
/// the <tt>PXDBLocalizableString</tt> attribute can be used instead of the <tt>PXDBText</tt> and PXString attributes.</remarks>
/// <example>
/// <code title="Example" lang="CS">
/// //The general declaration example
/// public abstract class noteID : PX.Data.BQL.BqlGuid.Field&lt;noteID&gt; { }
/// 
/// [PXNote]
/// [PXDBLocalizableString(60, IsUnicode = true)]
/// public virtual Guid? NoteID { get; set; }</code>
/// <code title="Example2" lang="CS">
/// //If you need to configure a field that has the PXString attribute
/// //that is used in conjunction with the PXDBCalced attribute,
/// //replace the PXString attribute with the PXDBLocalizableString attribute
/// //and set the value of the NonDB parameter to true, as shown in the following example.
/// PXDBLocalizableString(255, IsUnicode = true, NonDB = true,
///    BqlField = typeof(PaymentMethod.descr))]
/// [PXDBCalced(typeof(Switch&lt;Case&lt;Where&lt;PaymentMethod.descr, IsNotNull&gt;,
///    PaymentMethod.descr&gt;, CustomerPaymentMethod.descr&gt;), typeof(string))]</code>
/// <code title="Example3" lang="CS">
/// //If you want to obtain the value of a multi-language field in the current locale,
/// //use the PXDatabase.SelectSingle() or PXDatabase.SelectMulti() method,
/// //and pass to it the return value of the PXDBLocalizableStringAttribute.GetValueSelect() static method
/// //instead of passing a new PXDataField object to it.
/// foreach (PXDataRecord record in PXDatabase.SelectMulti&lt;Numbering&gt;(
///    newPXDataField&lt;Numbering.numberingID&gt;(),
///    PXDBLocalizableStringAttribute.GetValueSelect("Numbering",
///       "NewSymbol", false),
///    newPXDataField&lt;Numbering.userNumbering&gt;()))
/// {
///    ...
/// }</code>
/// <code title="Example4" lang="CS">
/// //If you want to obtain the value of a multi-language field in a specific language,
/// //use the PXDBLocalizableStringAttribute.GetTranslation() method.
/// //Pass to the method as input parameters a DAC cache, a DAC instance, a field name,
/// //and the ISO code of the language.
/// //The following code shows an example of use of the PXDBLocalizableStringAttribute.GetTranslation() method.
/// tran.TranDesc =
///    PXDBLocalizableStringAttribute.GetTranslation(
///       Caches[typeof(InventoryItem)], item, typeof(InventoryItem.descr).Name,
///       customer.Current?.LanguageName);</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBLocalizableStringAttribute : 
  PXDBStringAttribute,
  IPXRowPersistedSubscriber,
  IPXRowPersistingSubscriber
{
  internal const string TranslationsPostfix = "Translations";
  protected int _PositionInTranslations;
  protected PXDBLocalizableStringAttribute.Definition _Definition;
  protected Dictionary<string, int> _Indexes;
  protected Dictionary<string, int> _FieldIndexes;

  /// <exclude />
  public PXDBLocalizableStringAttribute(int length)
    : base(length)
  {
  }

  /// <exclude />
  public PXDBLocalizableStringAttribute()
  {
  }

  public bool NonDB { get; set; }

  public bool IsProjection { get; set; }

  public bool IsSecondLevelProjection { get; set; }

  public bool ShowTranslations { set; get; } = true;

  [InjectDependencyOnTypeLevel]
  private ILocalizableFieldService LocalizableFieldService { get; set; }

  public bool MultiLingual
  {
    get
    {
      ILocalizableFieldService localizableFieldService = this.LocalizableFieldService;
      return localizableFieldService != null && localizableFieldService.IsFieldEnabled(this.BqlTable.Name, this.FieldName);
    }
  }

  internal static void Clear()
  {
    PXContext.ClearSlot<PXDBLocalizableStringAttribute.Definition>();
    PXDatabase.ResetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeof (Locale));
  }

  public static bool IsEnabled
  {
    get
    {
      PXDBLocalizableStringAttribute.Definition slot = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
      if (slot == null)
      {
        System.Type[] typeArray = new System.Type[1]
        {
          typeof (Locale)
        };
        PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(slot = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeArray));
      }
      return slot != null && !string.IsNullOrEmpty(slot.DefaultLocale);
    }
  }

  public static string DefaultLocale
  {
    get
    {
      PXDBLocalizableStringAttribute.Definition slot = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
      if (slot == null)
      {
        System.Type[] typeArray = new System.Type[1]
        {
          typeof (Locale)
        };
        PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(slot = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeArray));
      }
      return slot == null || string.IsNullOrEmpty(slot.DefaultLocale) ? "" : slot.DefaultLocale;
    }
  }

  public static bool HasMultipleLocales
  {
    get
    {
      PXDBLocalizableStringAttribute.Definition slot = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
      if (slot == null)
      {
        System.Type[] typeArray = new System.Type[1]
        {
          typeof (Locale)
        };
        PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(slot = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeArray));
      }
      return slot != null && slot.HasMultipleLocales;
    }
  }

  public static List<string> EnabledLocales
  {
    get
    {
      PXDBLocalizableStringAttribute.Definition slot = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
      if (slot == null)
      {
        System.Type[] typeArray = new System.Type[1]
        {
          typeof (Locale)
        };
        PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(slot = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeArray));
      }
      return slot == null ? new List<string>() : slot.DefaultPlusAlternative;
    }
  }

  public static void SetShowTranslations(
    PXCache cache,
    object data,
    string name,
    bool showTranslations)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXDBLocalizableStringAttribute localizableStringAttribute in cache.GetAttributesOfType<PXDBLocalizableStringAttribute>(data, name))
    {
      if (localizableStringAttribute != null)
        localizableStringAttribute.ShowTranslations = showTranslations;
    }
  }

  public static void SetShowTranslations<Field>(PXCache cache, object data, bool showTranslations) where Field : IBqlField
  {
    PXDBLocalizableStringAttribute.SetShowTranslations(cache, data, typeof (Field).Name, showTranslations);
  }

  /// <exclude />
  public static void DefaultTranslationsFromMessage(
    PXCache sender,
    object row,
    string fieldName,
    string message)
  {
    PXDBLocalizableStringAttribute.Definition slot = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
    if (slot == null)
    {
      System.Type[] typeArray = new System.Type[1]
      {
        typeof (Locale)
      };
      PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(slot = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeArray));
    }
    if (slot == null || !(sender.GetValueExt((object) null, fieldName + "Translations") is string[] valueExt))
      return;
    string[] strArray = new string[valueExt.Length];
    for (int index = 0; index < strArray.Length; ++index)
    {
      List<string> stringList;
      if (slot.LocalesByLanguage.TryGetValue(valueExt[index], out stringList))
      {
        foreach (string localeName in stringList)
        {
          using (new PXLocaleScope(localeName))
          {
            strArray[index] = PXMessages.LocalizeNoPrefix(message);
            if (strArray[index] != null)
            {
              if (!string.Equals(strArray[index], message))
                break;
            }
            if (valueExt[index] != slot.DefaultLocale)
              strArray[index] = (string) null;
          }
        }
      }
    }
    sender.SetValueExt(row, fieldName + "Translations", (object) strArray);
  }

  /// <summary>
  /// If <paramref name="language" /> is defined, returns the translation of the specified field for the specified record;
  /// otherwise, returns the current value of the field.
  /// </summary>
  /// <param name="cache">The cache object that is used to search for the attributes of
  /// the <see cref="T:PX.Data.PXDBLocalizableStringAttribute" /> type.</param>
  /// <param name="data">The record from which the translation is retrieved.</param>
  /// <param name="fieldName">The name of the field for which translation is requested.
  /// The field must have <see cref="T:PX.Data.PXDBLocalizableStringAttribute" />.</param>
  /// <param name="language">A predefined System.Globalization.CultureInfo name, System.Globalization.CultureInfo.Name
  /// of an existing System.Globalization.CultureInfo, or Windows-only culture name.
  /// The name is not case-sensitive.</param>
  public static string GetTranslation(
    PXCache cache,
    object data,
    string fieldName,
    string language)
  {
    PXDBLocalizableStringAttribute localizableStringAttribute = cache.GetAttributes(fieldName).OfType<PXDBLocalizableStringAttribute>().FirstOrDefault<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (l => l.MultiLingual));
    if (localizableStringAttribute != null && !string.IsNullOrWhiteSpace(language))
    {
      language = new CultureInfo(language).TwoLetterISOLanguageName;
      string[] strArray1 = (string[]) null;
      object valueExt1 = cache.GetValueExt(data, fieldName + "Translations");
      if (!(valueExt1 is string[] strArray2))
      {
        if (valueExt1 is string str)
          return localizableStringAttribute.GetTranslationFromPackedValue(str, language);
      }
      else
        strArray1 = strArray2;
      int index;
      if (cache.GetValueExt((object) null, fieldName + "Translations") is string[] valueExt2 && strArray1 != null && (index = Array.IndexOf<string>(valueExt2, language)) >= 0 && index < strArray1.Length && !string.IsNullOrEmpty(strArray1[index]))
        return strArray1[index];
    }
    return cache.GetValue(data, fieldName) as string;
  }

  /// <summary>
  /// If <paramref name="language" /> is defined, returns the translation of the
  /// field of the specified type for the specified record;
  /// otherwise, returns the current value of the field.
  /// </summary>
  /// <typeparam name="TField">The type of the field for which translation is requested.
  /// The field must have <see cref="T:PX.Data.PXDBLocalizableStringAttribute" />.</typeparam>
  /// <param name="cache">The cache object that is used to search for the attributes of
  /// the <see cref="T:PX.Data.PXDBLocalizableStringAttribute" /> type.</param>
  /// <param name="data">The record from which the translation is retrieved.</param>
  /// <param name="language">A predefined System.Globalization.CultureInfo name, System.Globalization.CultureInfo.Name
  /// of an existing System.Globalization.CultureInfo, or Windows-only culture name.
  /// The name is not case-sensitive.</param>
  public static string GetTranslation<TField>(PXCache cache, object data, string language) where TField : IBqlField
  {
    return PXDBLocalizableStringAttribute.GetTranslation(cache, data, typeof (TField).Name, language);
  }

  /// <summary>
  /// Returns the translation of the field of the specified type for the specified record for current locale.
  /// </summary>
  /// <typeparam name="TField">The type of the field for which translation is requested.
  /// The field must have <see cref="T:PX.Data.PXDBLocalizableStringAttribute" />.</typeparam>
  /// <param name="cache">The cache object that is used to search for the attributes of
  /// the <see cref="T:PX.Data.PXDBLocalizableStringAttribute" /> type.</param>
  /// <param name="data">The record from which the translation is retrieved.</param>
  public static string GetTranslation<TField>(PXCache cache, object data) where TField : IBqlField
  {
    return (string) PXFieldState.UnwrapValue(cache.GetValueExt<TField>(data));
  }

  /// <summary>
  /// Copies all translations from the specified field of the source record
  /// to the specified field of the destination record.
  /// </summary>
  /// <typeparam name="TSourceField">The type of the source field.
  /// The field must have <see cref="T:PX.Data.PXDBLocalizableStringAttribute" />.</typeparam>
  /// <typeparam name="TDestinationField">The type of the destination field.
  /// Translations are copied to this field.
  /// The field must have <see cref="T:PX.Data.PXDBLocalizableStringAttribute" />.</typeparam>
  /// <param name="sourceCache">The cache object that is used to search for the attributes of
  /// the <see cref="T:PX.Data.PXDBLocalizableStringAttribute" /> type
  /// on the <typeparamref name="TSourceField" /> field.</param>
  /// <param name="sourceData">The record from which translations are retrieved.</param>
  /// <param name="destinationCache">The cache object that is used to search for the attributes of
  /// the <see cref="T:PX.Data.PXDBLocalizableStringAttribute" /> type
  /// on the <typeparamref name="TDestinationField" /> field.</param>
  /// <param name="destinationData">The record to which translations are copied.</param>
  public static void CopyTranslations<TSourceField, TDestinationField>(
    PXCache sourceCache,
    object sourceData,
    PXCache destinationCache,
    object destinationData)
    where TSourceField : IBqlField
    where TDestinationField : IBqlField
  {
    if (!PXDBLocalizableStringAttribute.IsEnabled)
      return;
    string[] translations = new string[0];
    using (IEnumerator<PXDBLocalizableStringAttribute> enumerator = sourceCache.GetAttributes<TSourceField>(sourceData).OfType<PXDBLocalizableStringAttribute>().Where<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (a => a.MultiLingual)).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        translations = enumerator.Current.GetTranslations(sourceCache, sourceData);
        if (translations != null)
          translations = (string[]) translations.Clone();
      }
    }
    foreach (PXDBLocalizableStringAttribute localizableStringAttribute in destinationCache.GetAttributes<TDestinationField>(destinationData).OfType<PXDBLocalizableStringAttribute>().Where<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (a => a.MultiLingual)))
      localizableStringAttribute.SetTranslations(destinationCache, destinationData, translations);
  }

  /// <summary>
  /// Copies all translations from the specified field of the source record
  /// to the specified field of the destination record.
  /// </summary>
  /// <typeparam name="TSourceField">The type of the source field.
  /// The field must have <see cref="T:PX.Data.PXDBLocalizableStringAttribute" />.</typeparam>
  /// <typeparam name="TDestinationField">The type of the destination field.
  /// Translations are copied to this field.
  /// The field must have <see cref="T:PX.Data.PXDBLocalizableStringAttribute" />.</typeparam>
  /// <param name="graph">PXGraph which contains source and destination caches.</param>
  /// <param name="sourceData">The record from which translations are retrieved.</param>
  /// <param name="destinationData">The record to which translations are copied.</param>
  public static void CopyTranslations<TSourceField, TDestinationField>(
    PXGraph graph,
    object sourceData,
    object destinationData)
    where TSourceField : IBqlField
    where TDestinationField : IBqlField
  {
    PXCache cach1 = graph.Caches[BqlCommand.GetItemType<TSourceField>()];
    PXCache cach2 = graph.Caches[BqlCommand.GetItemType<TDestinationField>()];
    object sourceData1 = sourceData;
    PXCache destinationCache = cach2;
    object destinationData1 = destinationData;
    PXDBLocalizableStringAttribute.CopyTranslations<TSourceField, TDestinationField>(cach1, sourceData1, destinationCache, destinationData1);
  }

  internal static int[] _GetSlotIndexes(PXCache sender)
  {
    List<int> intList = new List<int>();
    if (PXDBLocalizableStringAttribute.HasMultipleLocales)
    {
      foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributesReadonly((string) null))
      {
        if (subscriberAttribute is PXDBLocalizableStringAttribute)
          intList.Add(((PXDBLocalizableStringAttribute) subscriberAttribute)._PositionInTranslations);
      }
    }
    return intList.ToArray();
  }

  /// <summary>
  /// Fills in translations for the specified field of the specified record.
  /// Translations are collected from <paramref name="message" /> .
  /// </summary>
  /// <typeparam name="TField">The type of the destination field.
  /// Translations are filled in for this field.
  /// The field must have <see cref="T:PX.Data.PXDBLocalizableStringAttribute" />.</typeparam>
  /// <param name="cache">The cache object that is used to search for the attributes of
  /// the <see cref="T:PX.Data.PXDBLocalizableStringAttribute" /> type.</param>
  /// <param name="data">The record for which translations are filled in.</param>
  /// <param name="message">The message from which translations are collected.
  /// The message should be translated in <see cref="T:PX.SM.TranslationMaint" />. </param>
  public static void SetTranslationsFromMessage<TField>(PXCache cache, object data, string message) where TField : IBqlField
  {
    PXDBLocalizableStringAttribute.SetTranslationsFromMessageFormatNLA<TField>(cache, data, message);
  }

  public static void SetTranslationsFromMessageFormatNLA<TField>(
    PXCache cache,
    object data,
    string message,
    params string[] args)
    where TField : IBqlField
  {
    PXDBLocalizableStringAttribute.Definition slot = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
    if (slot == null)
    {
      System.Type[] typeArray = new System.Type[1]
      {
        typeof (Locale)
      };
      PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(slot = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeArray));
    }
    if (slot == null || !(cache.GetValueExt((object) null, typeof (TField).Name + "Translations") is string[] valueExt))
      return;
    string[] translations = new string[valueExt.Length];
    for (int index = 0; index < valueExt.Length; ++index)
    {
      List<string> stringList;
      if (slot.LocalesByLanguage.TryGetValue(valueExt[index], out stringList))
      {
        foreach (string localeName in stringList)
        {
          using (new PXLocaleScope(localeName))
          {
            string format = PXLocalizer.Localize(message);
            try
            {
              translations[index] = string.Format(format, (object[]) args);
            }
            catch (FormatException ex)
            {
              PXTrace.WriteError($"Following message could not be translated:{message}. As it expects more than {args.Length.ToString()} arguments.");
              throw;
            }
          }
        }
      }
    }
    foreach (PXDBLocalizableStringAttribute localizableStringAttribute in cache.GetAttributes<TField>(data).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attribute => attribute is PXDBLocalizableStringAttribute)))
      localizableStringAttribute.SetTranslations(cache, data, translations);
  }

  internal static void EnsureTranslations(HashSet<string> tables)
  {
    PXDBLocalizableStringAttribute.EnsureTranslations((Func<string, bool>) (t => tables.Contains(t)));
  }

  /// <exclude />
  public static void EnsureTranslations(Func<string, bool> tableMeet)
  {
    PXDBLocalizableStringAttribute.Definition def = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
    if (def == null)
    {
      System.Type[] typeArray = new System.Type[1]
      {
        typeof (Locale)
      };
      PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(def = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeArray));
    }
    if (def == null)
      return;
    if (string.IsNullOrEmpty(def.DefaultLocale))
      return;
    try
    {
      ILocalizableFieldService localizableFieldService = ServiceLocator.IsLocationProviderSet ? ServiceLocator.Current.GetInstance<ILocalizableFieldService>() : (ILocalizableFieldService) null;
      KeyValueExtHelper.CopyKeyValueExtensions<PXDBLocalizableStringAttribute>((Func<string, string>) (fieldName => fieldName + def.DefaultLocale.ToUpper()), (Func<TypeCode, Attribute, string, string, string>) ((typeCode, attribute, tableName, fieldName) =>
      {
        string str = (string) null;
        if (typeCode == TypeCode.String && !((PXDBLocalizableStringAttribute) attribute).NonDB && !((PXDBLocalizableStringAttribute) attribute).IsProjection && tableMeet(tableName))
        {
          ILocalizableFieldService localizableFieldService1 = localizableFieldService;
          if ((localizableFieldService1 != null ? (localizableFieldService1.IsFieldEnabled(tableName, fieldName) ? 1 : 0) : 0) != 0)
          {
            str = "ValueString";
            if (attribute is PXDBLocalizableStringAttribute && (((PXDBStringAttribute) attribute).Length <= 0 || ((PXDBStringAttribute) attribute).Length > 256 /*0x0100*/))
              str = "ValueText";
          }
        }
        return str;
      }), false);
    }
    catch
    {
    }
  }

  /// <exclude />
  public static PXDataField GetValueSelect(string tableName, string fieldName, bool isLong)
  {
    PXDBLocalizableStringAttribute.Definition def = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
    if (def == null)
    {
      System.Type[] typeArray = new System.Type[1]
      {
        typeof (Locale)
      };
      PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(def = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeArray));
    }
    if (def == null || string.IsNullOrEmpty(def.DefaultLocale))
      return new PXDataField(fieldName);
    ISqlDialect sqlDialect = PXDatabase.Provider.SqlDialect;
    PXDatabaseProviderBase provider = PXDatabase.Provider as PXDatabaseProviderBase;
    string currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    string tableName1 = tableName;
    string kvExtTableName = sqlDialect.GetKvExtTableName(tableName1);
    Column l = new Column("FieldName", kvExtTableName);
    SQLSwitch exp1 = new SQLSwitch();
    List<string> list = def.DefaultPlusAlternative.Where<string>((Func<string, bool>) (_ =>
    {
      if (string.Equals(_, def.DefaultLocale, StringComparison.OrdinalIgnoreCase))
        return false;
      return currentLanguage == null || !string.Equals(_, currentLanguage, StringComparison.OrdinalIgnoreCase);
    })).ToList<string>();
    list.Insert(0, def.DefaultLocale);
    if (!string.IsNullOrEmpty(currentLanguage))
      list.Insert(0, currentLanguage);
    for (int index = 0; index < list.Count; ++index)
    {
      string upper = list[index].ToUpper();
      exp1.Case(l.EQ((object) (fieldName + upper)), (SQLExpression) new SQLConst((object) index));
    }
    SQLExpression r1 = (SQLExpression) null;
    bool flag = false;
    if (provider != null)
    {
      companySetting settings;
      int companyId = provider.getCompanyID(tableName, out settings);
      int[] selectables;
      if (settings != null && settings.Flag != companySetting.companyFlag.Separate && provider.tryGetSelectableCompanies(companyId, out selectables))
      {
        SQLExpression exp2 = (SQLExpression) new SQLConst((object) companyId);
        foreach (int c in selectables)
          exp2 = exp2.Seq((object) c);
        r1 = new Column("CompanyID", kvExtTableName).In(exp2);
        flag = true;
      }
      else
        r1 = provider.GetRestrictionExpression(kvExtTableName, kvExtTableName, true);
    }
    Query q = new Query();
    SQLExpression exp3 = (SQLExpression) null;
    for (int index = 0; index < list.Count; ++index)
    {
      SQLConst r2 = new SQLConst((object) (fieldName + list[index].ToUpper()));
      exp3 = index == 0 ? (SQLExpression) r2 : exp3.Seq((SQLExpression) r2);
    }
    q.Field((SQLExpression) new Column(isLong ? "ValueText" : "ValueString", kvExtTableName)).From((Table) new SimpleTable(kvExtTableName)).Where(SQLExpressionExt.EQ(new Column("RecordID", kvExtTableName), new Column("NoteID", tableName).And(new Column("FieldName", kvExtTableName).In(exp3))).And(r1)).OrderAsc((SQLExpression) exp1);
    if (flag)
      q.OrderDesc((SQLExpression) new Column("CompanyID", kvExtTableName));
    q.Limit(1);
    return new PXDataField((SQLExpression) new SubQuery(q));
  }

  internal static bool IsPackedValue(string value)
  {
    return value.StartsWith("[{", StringComparison.Ordinal) && value.EndsWith("}]", StringComparison.Ordinal);
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    using (this.NonDB ? new RestoreScope().RestoreTo<SQLExpression>((System.Action<SQLExpression>) (_ => e.Expr = _), e.Expr).RestoreTo<System.Type>((System.Action<System.Type>) (_ => e.BqlTable = _), e.BqlTable) : (RestoreScope) null)
    {
      if (this._Definition != null)
        this.CommandPreparingLocalizable(sender, e);
      else
        base.CommandPreparing(sender, e);
    }
  }

  private void CommandPreparingLocalizable(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Select)
    {
      base.CommandPreparing(sender, e);
      PXCommandPreparingEventArgs.FieldDescription fieldDescription = e.GetFieldDescription();
      if (fieldDescription == null)
        return;
      if ((e.Operation & PXDBOperation.Option) == PXDBOperation.External)
      {
        string currentLanguage = this.GetCurrentLanguage(sender.Graph.IsContractBasedAPI);
        List<string> list = this._Definition.DefaultPlusAlternative.Where<string>((Func<string, bool>) (_ =>
        {
          if (string.Equals(_, this._Definition.DefaultLocale, StringComparison.OrdinalIgnoreCase))
            return false;
          return currentLanguage == null || !string.Equals(_, currentLanguage, StringComparison.OrdinalIgnoreCase);
        })).ToList<string>();
        list.Insert(0, this._Definition.DefaultLocale);
        if (!string.IsNullOrEmpty(currentLanguage) && this._Indexes.ContainsKey(currentLanguage))
          list.Insert(0, currentLanguage);
        System.Type bqlTable = fieldDescription.BqlTable;
        if (this.IsSecondLevelProjection)
        {
          PXDBLocalizableStringAttribute localizableStringAttribute = sender.Graph.Caches[fieldDescription.BqlTable].GetAttributes(this._DatabaseFieldName).OfType<PXDBLocalizableStringAttribute>().FirstOrDefault<PXDBLocalizableStringAttribute>();
          if (localizableStringAttribute != null)
            bqlTable = localizableStringAttribute.BqlTable;
        }
        SimpleTable t1 = new SimpleTable(e.SqlDialect.GetKvExtTableName(bqlTable.Name));
        Column l = new Column("FieldName", (Table) t1);
        SQLExpression exp1 = SQLExpression.None();
        SQLSwitch exp2 = new SQLSwitch();
        for (int index = 0; index < list.Count; ++index)
        {
          string upper = list[index].ToUpper();
          exp2.Case(l.EQ((object) ((this.IsProjection ? this._DatabaseFieldName : this._FieldName) + upper)), (SQLExpression) new SQLConst((object) index));
        }
        Column field = this._Length <= 0 || this._Length > 256 /*0x0100*/ ? new Column("ValueText", (Table) t1) : new Column("ValueString", (Table) t1);
        for (int index = 0; index < list.Count; ++index)
          exp1 = exp1.Seq((object) ((this.IsProjection ? this._DatabaseFieldName : this._FieldName) + list[index].ToUpper()));
        bool flag = false;
        if (e.Table.IsDefined(typeof (PXProjectionAttribute), true))
        {
          System.Type[] source = e.Table.GetCustomAttributes(true).OfType<PXProjectionAttribute>().First<PXProjectionAttribute>().GetTableCommand()?.GetTables();
          if (source == null)
            source = new System.Type[1]{ bqlTable };
          if (((IEnumerable<System.Type>) source).Any<System.Type>((Func<System.Type, bool>) (t => bqlTable.IsAssignableFrom(t))))
            flag = true;
        }
        else if (bqlTable.IsAssignableFrom(e.Table))
          flag = true;
        System.Type type = flag ? e.Table : bqlTable;
        string noteIdName = sender.Graph.Caches[type]._NoteIDName;
        Query q = new Query();
        q.Field((SQLExpression) field).From((Table) t1).Where(SQLExpressionExt.EQ(new Column("RecordID", (Table) t1), (SQLExpression) new Column(noteIdName, type)).And(l.In(exp1))).OrderAsc((SQLExpression) exp2);
        e.Expr = (SQLExpression) new SubQuery(q);
      }
      else if (fieldDescription.BqlTable.IsAssignableFrom(this.BqlTable) && !this.IsSecondLevelProjection)
      {
        PXNoteAttribute pxNoteAttribute = sender.GetAttributesOfType<PXNoteAttribute>(e.Row, sender._NoteIDName).FirstOrDefault<PXNoteAttribute>();
        System.Type table = fieldDescription.BqlTable.IsAssignableFrom(pxNoteAttribute?.BqlTable) ? e.Table : (System.Type) null;
        Query attributesJoined = BqlCommand.GetNoteAttributesJoined((System.Type) null, fieldDescription.BqlTable, table, e.Operation);
        e.Expr = (SQLExpression) new SubQuery(attributesJoined);
      }
      e.BqlTable = sender.BqlTable;
    }
    else
    {
      string[] slot = sender.GetSlot<string[]>(e.Row, this._PositionInTranslations);
      string str;
      if (slot != null && (str = slot[this._Indexes[this._Definition.DefaultLocale]]) != null)
        e.Value = (object) str;
      base.CommandPreparing(sender, e);
    }
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!this.NonDB)
      base.RowSelecting(sender, e);
    if (this._Definition == null)
      return;
    string firstColumn = sender.GetValue(e.Row, this._FieldOrdinal) as string;
    string[] attributes;
    if (!sender.Graph.SqlDialect.tryExtractAttributes(firstColumn, (IDictionary<string, int>) this._FieldIndexes, out attributes))
      return;
    string str = (string) null;
    sender.SetSlot<string[]>(e.Row, this._PositionInTranslations, attributes, true);
    string currentLanguage = this.GetCurrentLanguage(sender.Graph.IsContractBasedAPI);
    if (!string.IsNullOrEmpty(currentLanguage) && this._Indexes.ContainsKey(currentLanguage))
      str = attributes[this._Indexes[currentLanguage]];
    if (string.IsNullOrEmpty(str))
      str = attributes[this._Indexes[this._Definition.DefaultLocale]];
    if (string.IsNullOrEmpty(str))
    {
      foreach (KeyValuePair<string, int> index in this._Indexes)
      {
        if ((str = attributes[index.Value]) != null)
          break;
      }
    }
    sender.SetValue(e.Row, this._FieldOrdinal, (object) str);
  }

  internal bool TryTranslateValue(
    PXGraph graph,
    string nonLocalizedString,
    out string localizedString)
  {
    ExceptionExtensions.ThrowOnNull<PXGraph>(graph, nameof (graph), (string) null);
    localizedString = (string) null;
    string[] attributes;
    if (string.IsNullOrWhiteSpace(nonLocalizedString) || this._Definition == null || !graph.SqlDialect.tryExtractAttributes(nonLocalizedString, (IDictionary<string, int>) this._FieldIndexes, out attributes))
      return false;
    string currentLanguage = this.GetCurrentLanguage(graph.IsContractBasedAPI);
    int index1;
    if (!string.IsNullOrEmpty(currentLanguage) && this._Indexes.TryGetValue(currentLanguage, out index1))
      localizedString = attributes[index1];
    if (!string.IsNullOrEmpty(localizedString))
      return true;
    int index2 = this._Indexes[this._Definition.DefaultLocale];
    localizedString = attributes[index2];
    if (!string.IsNullOrEmpty(localizedString))
      return true;
    foreach (int index3 in this._Indexes.Values)
    {
      localizedString = attributes[index3];
      if (!string.IsNullOrEmpty(localizedString))
        return true;
    }
    return false;
  }

  /// <exclude />
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._Definition != null && this.ShowTranslations)
    {
      string currentLanguage = this.GetCurrentLanguage(sender.Graph.IsContractBasedAPI);
      if (e.Row == null)
      {
        base.FieldSelecting(sender, e);
        if (!(e.ReturnState is PXStringState))
          return;
        ((PXStringState) e.ReturnState).Language = currentLanguage;
      }
      else
      {
        string[] translations;
        if ((translations = this.GetTranslations(sender, e.Row)) != null)
        {
          string str = translations[this._Indexes[currentLanguage]];
          string a = (string) null;
          if (str != null)
          {
            a = currentLanguage;
            if (sender.Graph.IsCopyPasteContext && !sender.Graph.IsMobile && ((IEnumerable<string>) translations).Count<string>((Func<string, bool>) (_ => !string.IsNullOrEmpty(_))) > 1)
              str = this.PackTranslations(translations);
          }
          else if ((str = translations[this._Indexes[this._Definition.DefaultLocale]]) != null)
          {
            a = this._Definition.DefaultLocale;
            if (sender.Graph.IsCopyPasteContext && !sender.Graph.IsMobile)
              str = this.PackTranslations(translations);
          }
          else
          {
            foreach (KeyValuePair<string, int> index in this._Indexes)
            {
              if ((str = translations[index.Value]) != null)
              {
                a = index.Key;
                break;
              }
            }
            if (sender.Graph.IsCopyPasteContext && !sender.Graph.IsMobile && str != null)
              str = this.PackTranslations(translations);
          }
          if (a != null)
          {
            e.ReturnValue = (object) str;
            if (!string.Equals(a, currentLanguage, StringComparison.OrdinalIgnoreCase))
              e.IsAltered = true;
          }
          base.FieldSelecting(sender, e);
          if (!(e.ReturnState is PXStringState))
            return;
          ((PXStringState) e.ReturnState).Language = a ?? currentLanguage;
        }
        else
        {
          base.FieldSelecting(sender, e);
          if (!(e.ReturnState is PXStringState))
            return;
          ((PXStringState) e.ReturnState).Language = currentLanguage;
        }
      }
    }
    else
      base.FieldSelecting(sender, e);
  }

  protected virtual string PackTranslations(string[] translations)
  {
    if (((IEnumerable<string>) translations).All<string>((Func<string, bool>) (x => string.IsNullOrWhiteSpace(x))))
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder("[");
    foreach (KeyValuePair<string, int> index in this._Indexes)
    {
      if (!string.IsNullOrEmpty(translations[index.Value]))
      {
        if (stringBuilder.Length > 1)
          stringBuilder.Append(',');
        stringBuilder.Append('{');
        stringBuilder.Append(index.Key);
        stringBuilder.Append(':');
        stringBuilder.Append(translations[index.Value]);
        stringBuilder.Append('}');
      }
    }
    stringBuilder.Append(']');
    return stringBuilder.ToString();
  }

  internal virtual string GetTranslationFromPackedValue(string value, string currentLanguage = null)
  {
    if (this._Definition == null || !this.ShowTranslations)
      return value;
    if (currentLanguage == null)
      currentLanguage = this.GetCurrentLanguage(false);
    if (string.IsNullOrEmpty(currentLanguage) || !this._Indexes.ContainsKey(currentLanguage))
      currentLanguage = this._Definition.DefaultLocale;
    return this.UnpackTranslations(value)[this._Indexes[currentLanguage]];
  }

  protected virtual string[] UnpackTranslations(string value)
  {
    string[] strArray = new string[this._Indexes.Count];
    value = value.Replace("[{", "").Replace("}]", "");
    string str1 = value;
    string[] separator = new string[1]{ "},{" };
    foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      int length = str2.IndexOf(':');
      int index;
      if (length > 1 && length + 1 < str2.Length - 1 && this._Indexes.TryGetValue(str2.Substring(0, length), out index))
        strArray[index] = str2.Substring(length + 1);
    }
    return strArray;
  }

  protected virtual string[] GetTranslations(PXCache sender, object row)
  {
    string[] alternatives = sender.GetSlot<string[]>(row, this._PositionInTranslations);
    if (alternatives == null)
    {
      if (sender.GetStatus(row) != PXEntryStatus.Inserted)
      {
        if (OnDemandCommand.GetKeyValues(sender, row, this._BqlTable, this._FieldIndexes, out alternatives))
          sender.SetSlot<string[]>(row, this._PositionInTranslations, alternatives, true);
      }
      else
      {
        string str = sender.GetValue(row, this._FieldOrdinal) as string;
        if (!string.IsNullOrEmpty(str))
        {
          string currentLanguage = this.GetCurrentLanguage(sender.Graph.IsContractBasedAPI);
          alternatives = new string[this._FieldIndexes.Count];
          alternatives[this._Indexes[currentLanguage]] = str;
          this.SetTranslations(sender, row, alternatives);
        }
      }
    }
    return alternatives;
  }

  protected virtual void SetTranslations(PXCache sender, object row, string[] translations)
  {
    sender.SetSlot<string[]>(row, this._PositionInTranslations, translations);
  }

  private static bool TryDeserializeTranslations(
    string text,
    out PXDBLocalizableStringAttribute.Translations translations)
  {
    try
    {
      translations = JsonConvert.DeserializeObject<PXDBLocalizableStringAttribute.Translations>(text);
      return translations != null;
    }
    catch (Exception ex)
    {
      translations = (PXDBLocalizableStringAttribute.Translations) null;
      return false;
    }
  }

  internal PXDBLocalizableStringAttribute.Translations GetTranslationsWithLanguage(
    PXCache cache,
    object row)
  {
    if (this._Definition != null)
    {
      string[] translations = this.GetTranslations(cache, row);
      if (translations != null)
      {
        KeyValuePair<string, string>[] items = new KeyValuePair<string, string>[this._Definition.DefaultPlusAlternative.Count];
        string currentLanguage = this.GetCurrentLanguage(cache.Graph.IsContractBasedAPI);
        foreach (KeyValuePair<string, int> index in this._Indexes)
          items[this.AdjustPosition(index.Value, index.Key, currentLanguage)] = new KeyValuePair<string, string>(index.Key, translations[index.Value]);
        return new PXDBLocalizableStringAttribute.Translations((IEnumerable<KeyValuePair<string, string>>) items);
      }
    }
    return new PXDBLocalizableStringAttribute.Translations();
  }

  private string GetCurrentLanguage(bool forceDefault)
  {
    string key = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    if (((string.IsNullOrEmpty(key) ? 1 : (!this._Indexes.ContainsKey(key) ? 1 : 0)) | (forceDefault ? 1 : 0)) != 0)
      key = this._Definition.DefaultLocale;
    return key;
  }

  protected int AdjustPosition(int position, string language, string currentLanguage)
  {
    if (string.Equals(language, currentLanguage, StringComparison.OrdinalIgnoreCase))
      return 0;
    if (string.Equals(language, this._Definition.DefaultLocale, StringComparison.OrdinalIgnoreCase))
      return 1;
    if (string.Equals(currentLanguage, this._Definition.DefaultLocale, StringComparison.OrdinalIgnoreCase))
      return position < this._Indexes[currentLanguage] ? position + 1 : position;
    if (position < this._Indexes[currentLanguage] && position < this._Indexes[this._Definition.DefaultLocale])
      return position + 2;
    return position > this._Indexes[currentLanguage] && position > this._Indexes[this._Definition.DefaultLocale] ? position : position + 1;
  }

  protected virtual void Translations_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._Definition == null)
      return;
    if (sender.Graph.IsExport && !sender.Graph.IsMobile && !sender.Graph.IsCopyPasteContext)
    {
      string[] translations = this.GetTranslations(sender, e.Row);
      if (translations == null)
        return;
      e.ReturnValue = (object) this.PackTranslations(translations);
    }
    else
    {
      string currentLanguage = this.GetCurrentLanguage(sender.Graph.IsContractBasedAPI);
      string[] strArray = new string[this._Definition.DefaultPlusAlternative.Count];
      if (e.Row == null)
      {
        foreach (KeyValuePair<string, int> index in this._Indexes)
          strArray[this.AdjustPosition(index.Value, index.Key, currentLanguage)] = index.Key;
      }
      else
      {
        string[] translations = this.GetTranslations(sender, e.Row);
        if (translations != null)
        {
          foreach (KeyValuePair<string, int> index in this._Indexes)
            strArray[this.AdjustPosition(index.Value, index.Key, currentLanguage)] = translations[index.Value];
        }
      }
      e.ReturnValue = (object) strArray;
    }
  }

  protected virtual void Translations_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (this._Definition == null || !(e.NewValue is string[]) || e.Row == null)
      return;
    string[] newValue = (string[]) e.NewValue;
    string[] slot = this.GetTranslations(sender, e.Row) ?? new string[this._Definition.DefaultPlusAlternative.Count];
    string currentLanguage = this.GetCurrentLanguage(sender.Graph.IsContractBasedAPI);
    bool flag = false;
    foreach (KeyValuePair<string, int> index in this._Indexes)
    {
      string b = newValue[this.AdjustPosition(index.Value, index.Key, currentLanguage)];
      if (string.IsNullOrEmpty(b))
        b = (string) null;
      flag |= !string.Equals(slot[index.Value], b, StringComparison.InvariantCultureIgnoreCase);
      slot[index.Value] = b;
    }
    if (!flag)
      return;
    sender.SetSlot<string[]>(e.Row, this._PositionInTranslations, slot);
    int status = (int) sender.GetStatus(e.Row);
    sender.SetValue(e.Row, this._FieldOrdinal, (object) ((IEnumerable<string>) newValue).FirstOrDefault<string>((Func<string, bool>) (_ => _ != null)));
    if (status == 2)
      sender.SetStatus(e.Row, PXEntryStatus.Inserted);
    if (sender.GetStatus(e.Row) != PXEntryStatus.Notchanged)
      return;
    sender.SetStatus(e.Row, PXEntryStatus.Modified);
    sender.IsDirty = true;
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Row == null || this._Definition == null || e.TranStatus != PXTranStatus.Open || (this.IsProjection || this.IsSecondLevelProjection) && sender.BqlTable != (System.Type) null && this._BqlTable != (System.Type) null && sender.BqlTable.Name != this._BqlTable.Name)
      return;
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    HashSet<string> stringSet = (HashSet<string>) null;
    TableHeader table = dbServicesPoint.Schema.GetTable(sender.BqlTable.Name);
    if (table != null)
      stringSet = EnumerableExtensions.ToHashSetAcu<string>(table.GetPrimaryKey().Columns.Select<TableIndexOnColumn, string>((Func<TableIndexOnColumn, string>) (c => ((TableEntityBase) c).Name)), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
    foreach (string str in sender.Fields.Where<string>((Func<string, bool>) (fieldName => !sender.GetAttributesReadonly((object) null, fieldName).OfType<PXDBTimestampAttribute>().Any<PXDBTimestampAttribute>())))
    {
      object obj = sender.GetValue(e.Row, str);
      PXCommandPreparingEventArgs.FieldDescription description;
      sender.RaiseCommandPreparing(str, e.Row, obj, PXDBOperation.Update, (System.Type) null, out description);
      if (description != null && description.Expr != null && (description.IsRestriction || stringSet != null && stringSet.Contains(str)))
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
    }
    string noteIdName = sender._NoteIDName;
    object obj1 = sender.GetValue(e.Row, noteIdName);
    object obj2 = obj1;
    if (obj1 == null)
    {
      obj1 = (object) SequentialGuid.Generate();
      sender.SetValue(e.Row, noteIdName, obj1);
    }
    PXCommandPreparingEventArgs.FieldDescription description1;
    sender.RaiseCommandPreparing(noteIdName, e.Row, obj1, PXDBOperation.Update, (System.Type) null, out description1);
    if (description1 != null && description1.Expr != null)
    {
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue)
      {
        OldValue = obj2,
        IsChanged = (obj2 == null)
      });
      PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign("RecordID", description1.DataType, description1.DataLength, description1.DataValue);
      pxDataFieldAssign.Storage = StorageBehavior.KeyValueKey;
      pxDataFieldAssign.OldValue = obj1;
      pxDataFieldAssign.IsChanged = false;
      pxDataFieldParamList.Add((PXDataFieldParam) pxDataFieldAssign);
    }
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Delete)
      return;
    string[] translations = this.GetTranslations(sender, e.Row);
    string[] slot = sender.GetSlot<string[]>(e.Row, this._PositionInTranslations, true);
    if (translations == null)
      return;
    this.GetCurrentLanguage(sender.Graph.IsContractBasedAPI);
    foreach (KeyValuePair<string, int> fieldIndex in this._FieldIndexes)
    {
      string strA = translations[fieldIndex.Value];
      string strB1 = slot?[fieldIndex.Value];
      PXDataFieldAssign pxDataFieldAssign1 = new PXDataFieldAssign(fieldIndex.Key, PXDbType.NVarChar, new int?(256 /*0x0100*/), (object) strA);
      if (this._Length <= 0 || this._Length > 256 /*0x0100*/)
        pxDataFieldAssign1.Storage = StorageBehavior.KeyValueText;
      else
        pxDataFieldAssign1.Storage = StorageBehavior.KeyValueString;
      pxDataFieldAssign1.OldValue = (object) strB1;
      pxDataFieldAssign1.IsChanged = !PXLocalesProvider.CollationComparer.Equals(strA, strB1);
      pxDataFieldParamList.Add((PXDataFieldParam) pxDataFieldAssign1);
      if (string.Equals(this._Definition.DefaultLocale, fieldIndex.Key.Substring(fieldIndex.Key.Length - 2), StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(strA))
      {
        PXCommandPreparingEventArgs e1 = new PXCommandPreparingEventArgs(e.Row, (object) strA, PXDBOperation.Update, this._BqlTable, sender.Graph.SqlDialect);
        base.CommandPreparing(sender, e1);
        PXCommandPreparingEventArgs.FieldDescription fieldDescription = e1.GetFieldDescription();
        if (fieldDescription != null && fieldDescription.Expr != null && fieldDescription.DataValue != null)
        {
          PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign((Column) fieldDescription.Expr, fieldDescription.DataType, fieldDescription.DataLength, fieldDescription.DataValue);
          object strB2 = sender.GetValue(e.Row, this._FieldName);
          pxDataFieldAssign2.OldValue = strB2;
          pxDataFieldAssign2.IsChanged = !PXLocalesProvider.CollationComparer.Equals(strA, strB2 as string);
          pxDataFieldParamList.Add((PXDataFieldParam) pxDataFieldAssign2);
        }
      }
    }
    PXTransactionScope.RemoveTranslationField(sender.BqlTable.Name, this._FieldName);
    PXTransactionScope.SetSkipMainTableEvent();
    sender.Graph.ProviderUpdate(this._BqlTable, pxDataFieldParamList.ToArray());
  }

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this._Definition == null || e.Row == null)
      return;
    IStructuralEquatable translations = (IStructuralEquatable) this.GetTranslations(sender, e.Row);
    IStructuralEquatable slot = (IStructuralEquatable) sender.GetSlot<string[]>(e.Row, this._PositionInTranslations, true);
    if (translations == null || translations.Equals((object) slot, (IEqualityComparer) StringComparer.Ordinal))
      return;
    PXTransactionScope.AddTranslationField(sender.BqlTable.Name, this._FieldName);
  }

  /// <exclude />
  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (this._Definition != null && e.Row != null && e.NewValue != sender.GetValue(e.Row, this._FieldOrdinal))
    {
      if (e.NewValue is string && PXDBLocalizableStringAttribute.IsPackedValue((string) e.NewValue))
      {
        string[] strArray = this.UnpackTranslations((string) e.NewValue);
        sender.SetSlot<string[]>(e.Row, this._PositionInTranslations, strArray);
        sender.SetValue(e.Row, this._FieldOrdinal, (object) ((IEnumerable<string>) strArray).FirstOrDefault<string>((Func<string, bool>) (_ => _ != null)));
        if (sender.GetStatus(e.Row) == PXEntryStatus.Notchanged)
        {
          sender.SetStatus(e.Row, PXEntryStatus.Modified);
          sender.IsDirty = true;
        }
        e.NewValue = sender.GetValue(e.Row, this._FieldOrdinal);
      }
      else
      {
        PXDBLocalizableStringAttribute.Translations translations;
        if (e.NewValue is string && PXDBLocalizableStringAttribute.TryDeserializeTranslations((string) e.NewValue, out translations))
        {
          HashSet<string> hashSet = LocalesHelper.GetActiveLocales().Select<string, string>((Func<string, string>) (locale => StringExtensions.FirstSegment(locale, '-'))).Distinct<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          string[] slot = sender.GetSlot<string[]>(e.Row, this._PositionInTranslations);
          foreach (KeyValuePair<string, string> keyValuePair in (Dictionary<string, string>) translations)
          {
            if (this._Indexes.ContainsKey(keyValuePair.Key) && hashSet.Contains(keyValuePair.Key))
              slot[this._Indexes[keyValuePair.Key]] = keyValuePair.Value;
          }
          sender.SetSlot<string[]>(e.Row, this._PositionInTranslations, slot);
          sender.SetValue(e.Row, this._FieldOrdinal, (object) ((IEnumerable<string>) slot).FirstOrDefault<string>((Func<string, bool>) (_ => _ != null)));
          if (sender.GetStatus(e.Row) == PXEntryStatus.Notchanged)
          {
            sender.SetStatus(e.Row, PXEntryStatus.Modified);
            sender.IsDirty = true;
          }
          e.NewValue = sender.GetValue(e.Row, this._FieldOrdinal);
        }
        else
        {
          base.FieldUpdating(sender, e);
          string currentLanguage = this.GetCurrentLanguage(sender.Graph.IsContractBasedAPI);
          if (string.IsNullOrEmpty(currentLanguage) || !this._Indexes.ContainsKey(currentLanguage))
            return;
          string[] slot = this.GetTranslations(sender, e.Row) ?? new string[this._Definition.DefaultPlusAlternative.Count];
          slot[this._Indexes[currentLanguage]] = e.NewValue as string;
          sender.SetSlot<string[]>(e.Row, this._PositionInTranslations, slot);
        }
      }
    }
    else
      base.FieldUpdating(sender, e);
  }

  /// <exclude />
  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (!this.NonDB || !(typeof (ISubscriber) == typeof (IPXRowSelectingSubscriber)))
      return;
    subscribers.Remove(this as ISubscriber);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    ILocalizableFieldService localizableFieldService = this.LocalizableFieldService;
    if ((localizableFieldService != null ? (!localizableFieldService.IsFieldEnabled(this.BqlTable.Name, this.FieldName) ? 1 : 0) : 1) != 0)
    {
      this.ShowTranslations = false;
    }
    else
    {
      this._Definition = PXContext.GetSlot<PXDBLocalizableStringAttribute.Definition>();
      if (this._Definition == null)
        PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(this._Definition = PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeof (Locale)));
      sender.Graph._RecordCachedSlot(this.GetType(), (object) this._Definition, (Func<object>) (() => (object) PXContext.SetSlot<PXDBLocalizableStringAttribute.Definition>(PXDatabase.GetSlot<PXDBLocalizableStringAttribute.Definition>("Definition", typeof (Locale)))));
      if (this._Definition != null && string.IsNullOrEmpty(this._Definition.DefaultLocale))
        this._Definition = (PXDBLocalizableStringAttribute.Definition) null;
      this.DefinitionProcessing(sender);
    }
  }

  protected void DefinitionProcessing(PXCache sender)
  {
    if (this._Definition == null)
      return;
    this._Indexes = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._FieldIndexes = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    for (int index = 0; index < this._Definition.DefaultPlusAlternative.Count; ++index)
    {
      this._Indexes[this._Definition.DefaultPlusAlternative[index]] = index;
      this._FieldIndexes[(this.IsProjection ? this._DatabaseFieldName : this._FieldName) + this._Definition.DefaultPlusAlternative[index].ToUpper()] = index;
    }
    if (this.NonDB)
      sender.RowSelecting += new PXRowSelecting(((PXDBFieldAttribute) this).RowSelecting);
    string str = this._FieldName + "Translations";
    if (!sender.Fields.Contains(str))
    {
      sender.Fields.Add(str);
      string lower = str.ToLower();
      sender.FieldSelectingEvents[lower] += new PXFieldSelecting(this.Translations_FieldSelecting);
      sender.FieldUpdatingEvents[lower] += new PXFieldUpdating(this.Translations_FieldUpdating);
    }
    int cnt = this._Definition.DefaultPlusAlternative.Count;
    this._PositionInTranslations = sender.SetupSlot<string[]>((Func<string[]>) (() => new string[cnt]), (Func<string[], string[], string[]>) ((item, copy) =>
    {
      for (int index = 0; index < cnt && index < item.Length && index < copy.Length; ++index)
        item[index] = copy[index];
      return item;
    }), (Func<string[], string[]>) (item => (string[]) item.Clone()));
  }

  /// <exclude />
  protected internal class Definition : IPrefetchable, IPXCompanyDependent
  {
    public string DefaultLocale;
    public List<string> DefaultPlusAlternative;
    public bool HasMultipleLocales;
    public Dictionary<string, List<string>> LocalesByLanguage = new Dictionary<string, List<string>>();

    public void Prefetch()
    {
      this.DefaultPlusAlternative = new List<string>();
      int num = 0;
      HashSet<string> stringSet = new HashSet<string>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Locale>((PXDataField) new PXDataField<Locale.localeName>(), (PXDataField) new PXDataField<Locale.isDefault>(), (PXDataField) new PXDataField<Locale.isAlternative>(), (PXDataField) new PXDataFieldValue<Locale.isActive>(PXDbType.Bit, new int?(1), (object) true), (PXDataField) new PXDataFieldOrder<Locale.number>()))
      {
        string name = pxDataRecord.GetString(0);
        if (!string.IsNullOrEmpty(name))
        {
          string str = name;
          ++num;
          string letterIsoLanguageName = new CultureInfo(name).TwoLetterISOLanguageName;
          bool? boolean = pxDataRecord.GetBoolean(1);
          bool flag1 = true;
          if (boolean.GetValueOrDefault() == flag1 & boolean.HasValue)
          {
            this.DefaultLocale = letterIsoLanguageName;
          }
          else
          {
            boolean = pxDataRecord.GetBoolean(2);
            bool flag2 = true;
            if (!(boolean.GetValueOrDefault() == flag2 & boolean.HasValue))
              continue;
          }
          if (stringSet.Add(letterIsoLanguageName))
            this.DefaultPlusAlternative.Add(letterIsoLanguageName);
          List<string> stringList;
          if (!this.LocalesByLanguage.TryGetValue(letterIsoLanguageName, out stringList))
            this.LocalesByLanguage[letterIsoLanguageName] = stringList = new List<string>();
          stringList.Add(str);
        }
      }
      this.HasMultipleLocales = num > 1;
    }
  }

  internal class Translations : Dictionary<string, string>
  {
    public Translations()
      : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
    }

    public Translations(IEnumerable<KeyValuePair<string, string>> items)
      : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      EnumerableExtensions.AddRange<string, string>((IDictionary<string, string>) this, items);
    }

    public bool ValuesEqual(
      PXDBLocalizableStringAttribute.Translations translations)
    {
      if (this == translations)
        return true;
      if (translations == null || translations.Count == 0 || this.Count != translations.Count)
        return false;
      foreach (KeyValuePair<string, string> keyValuePair in (Dictionary<string, string>) this)
      {
        string b;
        if (!translations.TryGetValue(keyValuePair.Key, out b) || !string.Equals(keyValuePair.Value, b, StringComparison.Ordinal))
          return false;
      }
      return true;
    }

    public Translations(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
