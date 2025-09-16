// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRGramProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Objects.CR.Extensions.Cache;
using PX.Objects.CR.MassProcess;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

[PXInternalUseOnly]
public class CRGramProcessor
{
  private readonly PXGraph.GetDefaultDelegate _defaultDelegate;
  private long processedItems;
  private DateTime? track;
  protected readonly PXGraph graph;

  protected CRSetup Setup => this._defaultDelegate.Invoke() as CRSetup;

  public CRGramProcessor()
    : this(PXGraph.CreateInstance<PXGraph>())
  {
  }

  public CRGramProcessor(PXGraph graph)
  {
    this.graph = graph;
    this.processedItems = 0L;
    this.track = new DateTime?();
    if (graph.Defaults.TryGetValue(typeof (CRSetup), out this._defaultDelegate))
      return;
    PXSetupOptional<CRSetup> pxSetupOptional = new PXSetupOptional<CRSetup>(graph);
    this._defaultDelegate = graph.Defaults[typeof (CRSetup)];
  }

  public PX.Objects.CR.Contact GetContact(PXResult<PX.Objects.CR.Contact> entities)
  {
    return ((PXResult) entities)?.GetItem<PX.Objects.CR.Contact>();
  }

  public void CreateGrams(PXResult<PX.Objects.CR.Contact> entities)
  {
    this.PersistGrams(entities, true);
  }

  public IEnumerable<(bool IsBlocked, string BlockType)> CheckIsBlocked(
    PXResult<PX.Objects.CR.Contact> entities,
    IEnumerable<CRDuplicateResult> duplicates)
  {
    List<(CRGrams, CRValidationRules)> currentGramsResult = this.DoCreateGrams(entities).Where<(CRGrams, CRValidationRules)>((Func<(CRGrams, CRValidationRules), bool>) (result => result.Gram?.CreateOnEntry != "A")).ToList<(CRGrams, CRValidationRules)>();
    if (currentGramsResult.Count == 0)
      return (IEnumerable<(bool, string)>) null;
    List<CRDuplicateResult> list = duplicates.ToList<CRDuplicateResult>();
    return list.Count == 0 ? (IEnumerable<(bool, string)>) null : list.Select<CRDuplicateResult, (bool, string)>((Func<CRDuplicateResult, (bool, string)>) (duplicate =>
    {
      CRDuplicateRecord duplicateRecord = ((PXResult) duplicate).GetItem<CRDuplicateRecord>();
      bool flag = false;
      string str = "W";
      foreach ((CRGrams crGrams, CRValidationRules crValidationRules) in currentGramsResult.Where<(CRGrams, CRValidationRules)>((Func<(CRGrams, CRValidationRules), bool>) (_ => _.Gram?.ValidationType == duplicateRecord?.ValidationType)))
      {
        if (crGrams != null && crGrams.EntityName != null)
        {
          System.Type type = PXBuildManager.GetType(crGrams.EntityName, false);
          if (!(type == (System.Type) null))
          {
            object obj1 = (object) null;
            foreach (System.Type table in ((PXResult) duplicate).Tables)
            {
              if (type.IsAssignableFrom(table))
              {
                obj1 = ((PXResult) duplicate)[table];
                break;
              }
            }
            if (obj1 != null)
            {
              PXCache cach = this.graph.Caches[type];
              object input = cach.GetValue(obj1, crGrams.FieldName);
              if (!string.IsNullOrWhiteSpace(input?.ToString()))
              {
                object[] objArray = cach.GetFieldAttribute<PXDeduplicationSearchFieldAttribute>(crGrams.FieldName)?.ConvertValue(input, crValidationRules?.TransformationRule);
                if (objArray != null)
                {
                  foreach (object obj2 in objArray)
                  {
                    if (string.Equals(obj2.ToString(), crGrams.FieldValue, StringComparison.InvariantCultureIgnoreCase))
                    {
                      flag = true;
                      str = str == "W" ? crGrams.CreateOnEntry : str;
                    }
                  }
                }
              }
            }
          }
        }
      }
      return (flag, str);
    }));
  }

  public (bool IsGramsCreated, string NewDuplicateStatus, DateTime? GramValidationDate) PersistGrams(
    PXResult<PX.Objects.CR.Contact> entities,
    bool requireRecreate = false)
  {
    PX.Objects.CR.Contact contact = this.GetContact(entities);
    if (contact == null)
      return (false, (string) null, new DateTime?());
    try
    {
      if (!this.track.HasValue)
        this.track = new DateTime?(DateTime.Now);
      if (this.graph.Caches[((object) contact).GetType()].GetStatus((object) contact) == 3)
      {
        PXDatabase.Delete<CRGrams>(new PXDataFieldRestrict[1]
        {
          new PXDataFieldRestrict("EntityID", (PXDbType) 8, new int?(4), (object) contact.ContactID, (PXComp) 0)
        });
        return (false, contact.DuplicateStatus, contact.GrammValidationDateTime);
      }
      if (!requireRecreate && this.GramSourceUpdated(entities))
        return (false, contact.DuplicateStatus, contact.GrammValidationDateTime);
      PXDatabase.Delete<CRGrams>(new PXDataFieldRestrict[1]
      {
        new PXDataFieldRestrict("EntityID", (PXDbType) 8, new int?(4), (object) contact.ContactID, (PXComp) 0)
      });
      foreach ((CRGrams Gram, CRValidationRules _) in this.DoCreateGrams(entities))
      {
        if (Gram != null)
          PXDatabase.Insert<CRGrams>(new PXDataFieldAssign[6]
          {
            new PXDataFieldAssign("entityID", (PXDbType) 8, new int?(4), (object) contact.ContactID),
            new PXDataFieldAssign("entityName", (PXDbType) 12, new int?(40), (object) Gram.EntityName),
            new PXDataFieldAssign("fieldName", (PXDbType) 12, new int?(60), (object) Gram.FieldName),
            new PXDataFieldAssign("fieldValue", (PXDbType) 12, new int?(60), (object) Gram.FieldValue),
            new PXDataFieldAssign("score", (PXDbType) 5, new int?(8), (object) Gram.Score),
            new PXDataFieldAssign("validationType", (PXDbType) 12, new int?(2), (object) Gram.ValidationType)
          });
      }
      string str = "NV";
      DateTime? nullable = new DateTime?(PXTimeZoneInfo.Now);
      PXDatabase.Update<PX.Objects.CR.Contact>(new PXDataFieldParam[3]
      {
        (PXDataFieldParam) new PXDataFieldAssign("duplicateStatus", (object) str),
        (PXDataFieldParam) new PXDataFieldAssign("grammValidationDateTime", (object) PXTimeZoneInfo.ConvertTimeToUtc(nullable.Value, LocaleInfo.GetTimeZone())),
        (PXDataFieldParam) new PXDataFieldRestrict("contactID", (object) contact.ContactID)
      });
      ++this.processedItems;
      return (true, str, nullable);
    }
    finally
    {
      if (this.processedItems % 100L == 0L)
      {
        TimeSpan timeSpan = DateTime.Now - this.track.Value;
        this.track = new DateTime?(DateTime.Now);
      }
    }
  }

  public bool GramSourceUpdated(PXResult<PX.Objects.CR.Contact> entities)
  {
    PX.Objects.CR.Contact contact = this.GetContact(entities);
    if (contact == null || EnumerableExtensions.IsIn<PXEntryStatus>(this.graph.Caches[((object) contact).GetType()].GetStatus((object) contact), (PXEntryStatus) 2, (PXEntryStatus) 0))
      return false;
    bool flag = true;
    foreach (System.Type table in ((PXResult) entities).Tables)
    {
      PXCache cache = this.graph.Caches[table];
      object entity = ((PXResult) entities)[table];
      if (this.Definition.ValidationRules(contact.ContactType).Any<CRValidationRules>((Func<CRValidationRules, bool>) (rule => !string.Equals(cache.GetValue(entity, rule.MatchingField)?.ToString(), cache.GetValueOriginal(entity, rule.MatchingField)?.ToString(), StringComparison.InvariantCultureIgnoreCase))))
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  public bool IsRulesDefined => this.Definition.IsRulesDefined;

  public bool IsAnyBlockingRulesConfigured(string contactType)
  {
    return this.Definition.IsAnyBlockingRulesConfigured(contactType);
  }

  public virtual bool IsValidationOnEntryActive(string contactType)
  {
    return this.Definition.IsValidationOnEntryActive(contactType);
  }

  protected CRGramProcessor.ValidationDefinition Definition
  {
    get
    {
      return PXDatabase.GetSlot<CRGramProcessor.ValidationDefinition>("ValidationRules", new System.Type[2]
      {
        typeof (CRValidation),
        typeof (CRValidationRules)
      });
    }
  }

  protected (object entity, System.Type entityType) GetEntity(
    PXResult<PX.Objects.CR.Contact> entities,
    string sType)
  {
    int num = 0;
    foreach (System.Type table in ((PXResult) entities).Tables)
    {
      if (table.FullName.Equals(sType))
        return (((PXResult) entities)[num], table);
      ++num;
    }
    return ((object) null, (System.Type) null);
  }

  protected virtual (Decimal total, Decimal totalZero) GetTotals(
    PXResult<PX.Objects.CR.Contact> entities,
    string validationType)
  {
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    foreach (CRValidationRules crValidationRules in this.Definition.TypeRules[validationType])
    {
      (object entity, System.Type entityType) = this.GetEntity(entities, crValidationRules.MatchingEntity);
      Decimal? scoreWeight;
      if (entity != null)
      {
        if (this.graph.Caches[entityType].GetValue(entity, crValidationRules.MatchingField) == null)
        {
          Decimal num3 = num2;
          scoreWeight = crValidationRules.ScoreWeight;
          Decimal valueOrDefault = scoreWeight.GetValueOrDefault();
          num2 = num3 + valueOrDefault;
        }
        else
        {
          Decimal num4 = num1;
          scoreWeight = crValidationRules.ScoreWeight;
          Decimal valueOrDefault = scoreWeight.GetValueOrDefault();
          num1 = num4 + valueOrDefault;
        }
      }
    }
    bool? scoresNormalization = (bool?) this.Setup?.DuplicateScoresNormalization;
    return scoresNormalization.HasValue && !scoresNormalization.GetValueOrDefault() ? (num1, 0M) : (num1, num2);
  }

  protected virtual IEnumerable<(CRGrams Gram, CRValidationRules Rule)> DoCreateGrams(
    PXResult<PX.Objects.CR.Contact> entities)
  {
    PX.Objects.CR.Contact contact = ((PXResult) entities)?.GetItem<PX.Objects.CR.Contact>();
    if (contact != null)
    {
      string[] strArray = this.GetValidationTypes((object) contact);
      for (int index = 0; index < strArray.Length; ++index)
      {
        string str = strArray[index];
        if (this.Definition.TypeRules.ContainsKey(str))
        {
          foreach ((CRGrams, CRValidationRules) gram in this.CreateGramsForType(contact.ContactID, entities, str))
            yield return gram;
        }
      }
      strArray = (string[]) null;
    }
  }

  protected virtual IEnumerable<(CRGrams Gram, CRValidationRules Rule)> CreateGramsForType(
    int? mainEntityID,
    PXResult<PX.Objects.CR.Contact> entities,
    string validationType)
  {
    (Decimal total, Decimal totalZero) = this.GetTotals(entities, validationType);
    if (!(total == 0M))
    {
      foreach (CRValidationRules crValidationRules in this.Definition.TypeRules[validationType])
      {
        CRValidationRules rule = crValidationRules;
        (object entity, System.Type entityType) = this.GetEntity(entities, rule.MatchingEntity);
        if (entity != null)
        {
          PXCache cach = this.graph.Caches[entityType];
          string entityName = rule.MatchingEntity;
          string fieldName = rule.MatchingField;
          string transformRule = rule.TransformationRule;
          Decimal scoreWeight = rule.ScoreWeight.GetValueOrDefault();
          if (!(scoreWeight == 0M))
          {
            if (scoreWeight > 0M && totalZero > 0M)
              scoreWeight += totalZero * (scoreWeight / total);
            object input = cach.GetValue(entity, fieldName);
            if (!string.IsNullOrWhiteSpace(input?.ToString()))
            {
              object[] objArray1 = cach.GetFieldAttribute<PXDeduplicationSearchFieldAttribute>(fieldName)?.ConvertValue(input, rule.TransformationRule);
              if (objArray1 != null)
              {
                object[] objArray = objArray1;
                for (int index = 0; index < objArray.Length; ++index)
                {
                  string lower = objArray[index].ToString().ToLower();
                  switch (transformRule)
                  {
                    case "SW":
                      foreach ((CRGrams, CRValidationRules) splitWordGram in this.GetSplitWordGrams(mainEntityID, rule, entityName, lower, scoreWeight, fieldName))
                        yield return splitWordGram;
                      break;
                    case "DN":
                      foreach ((CRGrams, CRValidationRules) domainNameGram in this.GetDomainNameGrams(mainEntityID, rule, entityName, lower, scoreWeight, fieldName))
                        yield return domainNameGram;
                      break;
                    default:
                      yield return this.GetGrams(mainEntityID, rule, entityName, fieldName, lower, new Decimal?(Decimal.Round(scoreWeight, 4)));
                      break;
                  }
                }
                objArray = (object[]) null;
                entityName = (string) null;
                fieldName = (string) null;
                transformRule = (string) null;
                rule = (CRValidationRules) null;
              }
            }
          }
        }
      }
    }
  }

  protected virtual string[] GetValidationTypes(object document)
  {
    if (document is PX.Objects.CR.Contact contact)
      return this.Definition.ValidationTypes(contact.ContactType);
    throw new NotSupportedException();
  }

  protected virtual (CRGrams Gram, CRValidationRules Rule) GetGrams(
    int? mainEntityID,
    CRValidationRules rule,
    string entityName,
    string fieldName,
    string fieldValue,
    Decimal? score)
  {
    if (!mainEntityID.HasValue)
      throw new NotSupportedException();
    return (new CRGrams()
    {
      EntityID = mainEntityID,
      ValidationType = rule.ValidationType,
      EntityName = entityName,
      FieldName = fieldName,
      FieldValue = fieldValue,
      Score = score,
      CreateOnEntry = rule.CreateOnEntry
    }, rule);
  }

  protected virtual IEnumerable<(CRGrams Gram, CRValidationRules Rule)> GetSplitWordGrams(
    int? mainEntityID,
    CRValidationRules rule,
    string entityName,
    string stringValue,
    Decimal scoreWeight,
    string fieldName)
  {
    string[] words = stringValue.Split(this.Setup?.DuplicateCharsDelimiters?.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    string[] strArray = words;
    for (int index = 0; index < strArray.Length; ++index)
    {
      string fieldValue = strArray[index];
      Decimal num = Decimal.Round(scoreWeight / (Decimal) words.Length, 4);
      if (!(num <= 0M))
        yield return this.GetGrams(mainEntityID, rule, entityName, fieldName, fieldValue, new Decimal?(num));
    }
    strArray = (string[]) null;
  }

  protected virtual IEnumerable<(CRGrams Gram, CRValidationRules Rule)> GetDomainNameGrams(
    int? mainEntityID,
    CRValidationRules rule,
    string entityName,
    string stringValue,
    Decimal scoreWeight,
    string fieldName)
  {
    if (stringValue.Contains<char>('@'))
    {
      stringValue = StringExtensions.Segment(stringValue, '@', (ushort) 1);
    }
    else
    {
      try
      {
        stringValue = new UriBuilder(stringValue).Host;
        int length = stringValue.IndexOf('.');
        if ((length < 0 ? stringValue : stringValue.Substring(0, length)).Equals("www"))
          stringValue = stringValue.Substring(length + 1);
      }
      catch (UriFormatException ex)
      {
      }
    }
    yield return this.GetGrams(mainEntityID, rule, entityName, fieldName, stringValue, new Decimal?(Decimal.Round(scoreWeight, 4)));
  }

  protected class ValidationDefinition : IPrefetchable, IPXCompanyDependent
  {
    public List<CRValidationRules> Rules;
    public Dictionary<string, List<CRValidationRules>> TypeRules;
    private List<CRValidationRules> Leads;
    private List<CRValidationRules> Contacts;
    private List<CRValidationRules> Accounts;
    private List<CRValidation> Validations;

    public void Prefetch()
    {
      PXGraph instance = PXGraph.CreateInstance<PXGraph>();
      this.Rules = new List<CRValidationRules>();
      this.Leads = new List<CRValidationRules>();
      this.Contacts = new List<CRValidationRules>();
      this.Accounts = new List<CRValidationRules>();
      this.TypeRules = new Dictionary<string, List<CRValidationRules>>();
      foreach (PXResult<CRValidationRules> pxResult in PXSelectBase<CRValidationRules, PXSelect<CRValidationRules>.Config>.Select(instance, Array.Empty<object>()))
      {
        CRValidationRules crValidationRules = PXResult<CRValidationRules>.op_Implicit(pxResult);
        this.Rules.Add(crValidationRules);
        if (!this.TypeRules.ContainsKey(crValidationRules.ValidationType))
          this.TypeRules[crValidationRules.ValidationType] = new List<CRValidationRules>();
        this.TypeRules[crValidationRules.ValidationType].Add(crValidationRules);
        string validationType = crValidationRules.ValidationType;
        if (validationType != null && validationType.Length == 2)
        {
          switch (validationType[0])
          {
            case 'A':
              if (validationType == "AA")
              {
                this.Accounts.Add(crValidationRules);
                continue;
              }
              continue;
            case 'C':
              switch (validationType)
              {
                case "CL":
                  break;
                case "CC":
                  this.Contacts.Add(crValidationRules);
                  continue;
                case "CA":
                  this.Contacts.Add(crValidationRules);
                  this.Accounts.Add(crValidationRules);
                  continue;
                default:
                  continue;
              }
              break;
            case 'L':
              switch (validationType)
              {
                case "LL":
                  this.Leads.Add(crValidationRules);
                  continue;
                case "LC":
                  break;
                case "LA":
                  this.Leads.Add(crValidationRules);
                  this.Accounts.Add(crValidationRules);
                  continue;
                default:
                  continue;
              }
              break;
            default:
              continue;
          }
          this.Leads.Add(crValidationRules);
          this.Contacts.Add(crValidationRules);
        }
      }
      this.Validations = new List<CRValidation>();
      foreach (PXResult<CRValidation> pxResult in PXSelectBase<CRValidation, PXSelect<CRValidation>.Config>.Select(instance, Array.Empty<object>()))
        this.Validations.Add(PXResult<CRValidation>.op_Implicit(pxResult));
    }

    public List<CRValidationRules> ValidationRules(string contactType)
    {
      switch (contactType)
      {
        case "LD":
          return this.Leads;
        case "PN":
          return this.Contacts;
        case "AP":
          return this.Accounts;
        default:
          return new List<CRValidationRules>();
      }
    }

    public string[] ValidationTypes(string contactType)
    {
      switch (contactType)
      {
        case "LD":
          return new string[4]{ "LL", "LC", "LA", "CL" };
        case "PN":
          return new string[4]{ "CL", "CC", "CA", "LC" };
        case "AP":
          return new string[3]{ "LA", "CA", "AA" };
        default:
          return new string[0];
      }
    }

    public bool IsRulesDefined => this.Contacts.Count > 0 && this.Accounts.Count > 0;

    public bool IsAnyBlockingRulesConfigured(string contactType)
    {
      switch (contactType)
      {
        case "LD":
          if (this.TypeRules.ContainsKey("LL") && this.TypeRules["LL"].Any<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A")) || this.TypeRules.ContainsKey("LC") && this.TypeRules["LC"].Any<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A")))
            return true;
          return this.TypeRules.ContainsKey("LA") && this.TypeRules["LA"].Any<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A"));
        case "PN":
          if (this.TypeRules.ContainsKey("CL") && this.TypeRules["CL"].Any<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A")) || this.TypeRules.ContainsKey("CC") && this.TypeRules["CC"].Any<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A")))
            return true;
          return this.TypeRules.ContainsKey("CA") && this.TypeRules["CA"].Any<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A"));
        case "AP":
          return this.TypeRules.ContainsKey("AA") && this.TypeRules["AA"].Any<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A"));
        default:
          return false;
      }
    }

    public virtual bool IsValidationOnEntryActive(string contactType)
    {
      switch (contactType)
      {
        case "LD":
          return this.Validations.Where<CRValidation>((Func<CRValidation, bool>) (_ => EnumerableExtensions.IsIn<string>(_.Type, "LL", "LC", "LA"))).Any<CRValidation>((Func<CRValidation, bool>) (_ => _.ValidateOnEntry.GetValueOrDefault()));
        case "PN":
          return this.Validations.Where<CRValidation>((Func<CRValidation, bool>) (_ => EnumerableExtensions.IsIn<string>(_.Type, "CL", "CC", "CA"))).Any<CRValidation>((Func<CRValidation, bool>) (_ => _.ValidateOnEntry.GetValueOrDefault()));
        case "AP":
          return this.Validations.Where<CRValidation>((Func<CRValidation, bool>) (_ => _.Type == "AA")).Any<CRValidation>((Func<CRValidation, bool>) (_ => _.ValidateOnEntry.GetValueOrDefault()));
        default:
          return false;
      }
    }
  }
}
