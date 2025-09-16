// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CountryStateSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common.Exceptions;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.CR;

public abstract class CountryStateSelectorAttribute(System.Type search) : PXSelectorAttribute(search)
{
  protected static CountryStateSelectorAttribute.Definition slot
  {
    get
    {
      return PXDatabase.GetSlot<CountryStateSelectorAttribute.Definition>("CountiesListDefinition", new System.Type[2]
      {
        typeof (PX.Objects.CS.Country),
        typeof (PX.Objects.CS.State)
      });
    }
  }

  protected abstract string Type { get; }

  /// <summary>
  /// Validates state for specified country and if validation failed, throws PXException.
  /// Returns state id if state was found by the description or the regex, or <paramref name="state" /> value.
  /// </summary>
  /// <param name="state"></param>
  /// <param name="countryID"></param>
  /// <returns>StateID</returns>
  /// <exception cref="T:PX.Objects.Common.Exceptions.LocalizationPreparedException">Thrown if validation failed.</exception>
  protected string ValidateStateByCountry(string state, string countryID)
  {
    CountryStateSelectorAttribute.Definition.Item obj;
    if (Str.IsNullOrEmpty(state) || Str.IsNullOrEmpty(countryID) || !CountryStateSelectorAttribute.slot.Countries.TryGetValue(countryID, ref obj) || !(obj is CountryStateSelectorAttribute.Definition.CountryItem countryItem) || countryItem.StateValidation == "X")
      return state;
    if (EnumerableExtensions.IsIn<string>(countryItem.StateValidation, "I", "N", "R"))
    {
      if (countryItem.States.TryGetValue(state, ref obj))
        return obj.ID;
      if (EnumerableExtensions.IsIn<string>(countryItem.StateValidation, "N", "R"))
      {
        if (countryItem.StatesDescription.TryGetValue(state, ref obj))
          return obj.ID;
        if (countryItem.StateValidation == "R")
        {
          (string, Regex)[] array = ((IEnumerable<CountryStateSelectorAttribute.Definition.Item>) countryItem.StatesRegEx).Where<CountryStateSelectorAttribute.Definition.Item>((Func<CountryStateSelectorAttribute.Definition.Item, bool>) (i => i.Regex != null)).Select<CountryStateSelectorAttribute.Definition.Item, (string, Regex)>((Func<CountryStateSelectorAttribute.Definition.Item, (string, Regex)>) (i => (i.ID, new Regex(i.Regex, RegexOptions.IgnoreCase | RegexOptions.Multiline)))).Where<(string, Regex)>((Func<(string, Regex), bool>) (i => i.regex.IsMatch(state))).ToArray<(string, Regex)>();
          if (array.Length > 1)
            throw new LocalizationPreparedException("The value matches more than one country by regexp. {0} matched: {1}", new object[2]
            {
              (object) this.Type,
              (object) string.Join(", ", ((IEnumerable<(string, Regex)>) array).Select<(string, Regex), string>((Func<(string, Regex), string>) (i => i.id)))
            });
          if (array.Length == 1)
            return array[0].Item1;
        }
      }
    }
    throw new LocalizationPreparedException("The '{0}' state cannot be found in the system.", new object[1]
    {
      (object) state
    });
  }

  [Obsolete("Use ValidateStateByCountry instead for validation, or Definition.Item.List.TryGetValue to find.")]
  protected bool Find(
    CountryStateSelectorAttribute.Definition.Item.List items,
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!items.Contains((string) e.NewValue))
      return false;
    e.NewValue = (object) items[(string) e.NewValue].ID;
    return true;
  }

  [Obsolete("Use ValidateStateByCountry instead for validation, or Definition.Item.List.TryGetValue to find.")]
  protected bool FindRegex(
    CountryStateSelectorAttribute.Definition.Item.List items,
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    string str1 = (string) null;
    string str2 = e.NewValue as string;
    int num = 0;
    foreach (CountryStateSelectorAttribute.Definition.Item obj in (KList<string, CountryStateSelectorAttribute.Definition.Item>) items)
    {
      if (obj.Regex != null)
      {
        try
        {
          if (new Regex(obj.Regex, RegexOptions.IgnoreCase | RegexOptions.Multiline).IsMatch((string) e.NewValue))
          {
            ++num;
            if (str1 == null)
              str2 = obj.ID;
            str1 = str1 + (str1 == null ? string.Empty : ", ") + obj.ID;
          }
        }
        catch
        {
        }
      }
    }
    if (num > 1)
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("The value matches more than one country by regexp. {0} matched: {1}", new object[2]
      {
        (object) this.Type,
        (object) str1
      }));
    e.NewValue = (object) str2;
    return num > 0;
  }

  protected class Definition : IPrefetchable, IPXCompanyDependent
  {
    public readonly CountryStateSelectorAttribute.Definition.Item.List Countries = new CountryStateSelectorAttribute.Definition.Item.List();
    public readonly CountryStateSelectorAttribute.Definition.Item.List CountriesDescription = new CountryStateSelectorAttribute.Definition.Item.List(true);
    public readonly CountryStateSelectorAttribute.Definition.Item.List CountriesRegex = new CountryStateSelectorAttribute.Definition.Item.List();
    private List<CountryStateSelectorAttribute.Definition.CountyStateLocale> locales = new List<CountryStateSelectorAttribute.Definition.CountyStateLocale>();

    public void Prefetch()
    {
      this.GetLocales();
      this.GetCountryList();
      this.GetStateList();
    }

    /// <summary>Getting list of countries with needed conditions.</summary>
    private void GetCountryList()
    {
      ISqlDialect sqlDialect = PXDatabase.Provider.SqlDialect;
      List<PXDataField> pxDataFieldList = new List<PXDataField>();
      pxDataFieldList.Add(new PXDataField("CountryID"));
      pxDataFieldList.Add(new PXDataField("CountryRegexp"));
      pxDataFieldList.Add(new PXDataField("CountryValidationMethod"));
      pxDataFieldList.Add(new PXDataField("StateValidationMethod"));
      pxDataFieldList.Add(new PXDataField("Description"));
      foreach (CountryStateSelectorAttribute.Definition.CountyStateLocale locale in this.locales)
      {
        SubQuery subQuery = new SubQuery(new Query().Field((SQLExpression) new Column("ValueString", "CountryKvExt", (PXDbType) 100)).From((Table) new SimpleTable("CountryKvExt", (string) null)).Where(SQLExpressionExt.EQ((SQLExpression) new Column("RecordID", "CountryKvExt", (PXDbType) 100), ((SQLExpression) new Column("NoteID", "Country", (PXDbType) 100)).And(SQLExpressionExt.EQ((SQLExpression) new Column("FieldName", "CountryKvExt", (PXDbType) 100), (SQLExpression) new SQLConst((object) locale.Country))))).Limit(1));
        pxDataFieldList.Add(new PXDataField((SQLExpression) subQuery));
      }
      int length = pxDataFieldList.ToArray().Length;
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(typeof (PX.Objects.CS.Country), pxDataFieldList.ToArray()))
      {
        for (int index = 4; index < length; ++index)
        {
          string description = pxDataRecord.GetString(index);
          if (description != null)
          {
            CountryStateSelectorAttribute.Definition.CountryItem countryItem = new CountryStateSelectorAttribute.Definition.CountryItem(pxDataRecord.GetString(0), description, pxDataRecord.GetString(1), pxDataRecord.GetString(2), pxDataRecord.GetString(3));
            this.Countries.Add((CountryStateSelectorAttribute.Definition.Item) countryItem);
            if (countryItem.Validation != "I")
              this.CountriesDescription.Add((CountryStateSelectorAttribute.Definition.Item) countryItem);
            if (countryItem.Validation == "R" && countryItem.Regex != null)
              this.CountriesRegex.Add((CountryStateSelectorAttribute.Definition.Item) countryItem);
          }
        }
      }
    }

    /// <summary>Getting list of states with needed conditions.</summary>
    private void GetStateList()
    {
      ISqlDialect sqlDialect = PXDatabase.Provider.SqlDialect;
      List<PXDataField> pxDataFieldList = new List<PXDataField>();
      pxDataFieldList.Add(new PXDataField("StateID"));
      pxDataFieldList.Add(new PXDataField("StateRegexp"));
      pxDataFieldList.Add(new PXDataField("CountryID"));
      pxDataFieldList.Add(new PXDataField("Name"));
      foreach (CountryStateSelectorAttribute.Definition.CountyStateLocale locale in this.locales)
      {
        SubQuery subQuery = new SubQuery(new Query().Field((SQLExpression) new Column("ValueString", "StateKvExt", (PXDbType) 100)).From((Table) new SimpleTable("StateKvExt", (string) null)).Where(SQLExpressionExt.EQ((SQLExpression) new Column("RecordID", "StateKvExt", (PXDbType) 100), ((SQLExpression) new Column("NoteID", "State", (PXDbType) 100)).And(SQLExpressionExt.EQ((SQLExpression) new Column("FieldName", "StateKvExt", (PXDbType) 100), (SQLExpression) new SQLConst((object) locale.State))))).Limit(1));
        pxDataFieldList.Add(new PXDataField((SQLExpression) subQuery));
      }
      int length = pxDataFieldList.ToArray().Length;
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(typeof (PX.Objects.CS.State), pxDataFieldList.ToArray()))
      {
        for (int index = 3; index < length; ++index)
        {
          string description = pxDataRecord.GetString(index);
          if (description != null)
            ((CountryStateSelectorAttribute.Definition.CountryItem) this.Countries[pxDataRecord.GetString(2)]).AddState(new CountryStateSelectorAttribute.Definition.Item(pxDataRecord.GetString(0), description, pxDataRecord.GetString(1)));
        }
      }
    }

    /// <summary>Getting all posible locations.</summary>
    private void GetLocales()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Locale>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<Locale.localeName>()
      }))
        this.locales.Add(new CountryStateSelectorAttribute.Definition.CountyStateLocale()
        {
          Country = "Description" + pxDataRecord.GetString(0).Substring(3, 2),
          State = "Name" + pxDataRecord.GetString(0).Substring(3, 2)
        });
    }

    public class Item
    {
      public readonly string ID;
      public readonly string Description;
      public readonly string Regex;

      public Item(string id, string description, string regex)
      {
        this.ID = id;
        this.Description = description;
        this.Regex = regex;
      }

      public class List : KList<string, CountryStateSelectorAttribute.Definition.Item>
      {
        private readonly bool KeyDescription;

        public List(bool keyDescription = false)
          : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase, 4, true)
        {
          this.KeyDescription = keyDescription;
        }

        protected virtual string GetKeyForItem(CountryStateSelectorAttribute.Definition.Item item)
        {
          return !this.KeyDescription ? item.ID : item.Description;
        }
      }
    }

    public class CountryItem : CountryStateSelectorAttribute.Definition.Item
    {
      public readonly string Validation;
      public readonly string StateValidation;
      public readonly CountryStateSelectorAttribute.Definition.Item.List States;
      public readonly CountryStateSelectorAttribute.Definition.Item.List StatesDescription;
      public readonly CountryStateSelectorAttribute.Definition.Item.List StatesRegEx;

      public CountryItem(
        string id,
        string description,
        string regex,
        string validation,
        string stateValidation)
        : base(id, description, regex)
      {
        this.Validation = validation;
        this.StateValidation = stateValidation;
        this.States = new CountryStateSelectorAttribute.Definition.Item.List();
        this.StatesDescription = new CountryStateSelectorAttribute.Definition.Item.List(true);
        this.StatesRegEx = new CountryStateSelectorAttribute.Definition.Item.List(true);
      }

      public void AddState(
        CountryStateSelectorAttribute.Definition.Item state)
      {
        if (this.StateValidation == "X")
          return;
        this.States.Add(state);
        if (this.StateValidation != "I")
          this.StatesDescription.Add(state);
        if (!(this.StateValidation == "R") || state.Regex == null)
          return;
        this.StatesRegEx.Add(state);
      }
    }

    public class CountyStateLocale
    {
      public string Country { get; set; }

      public string State { get; set; }
    }
  }
}
