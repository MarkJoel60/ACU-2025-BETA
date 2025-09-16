// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRDuplicateValidationSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions.Cache;
using PX.Objects.CR.Extensions.CRDuplicateEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects.CR;

public class CRDuplicateValidationSetupMaint : PXGraph<
#nullable disable
CRDuplicateValidationSetupMaint>
{
  public FbqlSelect<SelectFromBase<CRSetup, TypeArrayOf<IFbqlJoin>.Empty>, CRSetup>.View Setup;
  public FbqlSelect<SelectFromBase<CRValidationTree, TypeArrayOf<IFbqlJoin>.Empty>, CRValidationTree>.View Nodes;
  [PXHidden]
  public FbqlSelect<SelectFromBase<CRValidationTree, TypeArrayOf<IFbqlJoin>.Empty>, CRValidationTree>.View NodesSelect;
  public FbqlSelect<SelectFromBase<CRValidation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CRValidation.iD, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRValidationTree.iD, IBqlInt>.FromCurrent>>, 
  #nullable disable
  CRValidation>.View CurrentNode;
  public FbqlSelect<SelectFromBase<CRValidationRules, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CRValidationRules.validationType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRValidation.type, IBqlString>.FromCurrent>>, 
  #nullable disable
  CRValidationRules>.View ValidationRules;
  public PXFilter<CRValidationRulesBuffer> Buffer;
  [PXHidden]
  public FbqlSelect<SelectFromBase<CRValidation, TypeArrayOf<IFbqlJoin>.Empty>, CRValidation>.View Validations;
  public PXSave<CRSetup> Save;
  public PXCancel<CRSetup> Cancel;
  public PXAction<CRValidationRules> Copy;
  public PXAction<CRValidationRules> Paste;
  protected const string DOUBLE_UNDERSCORE = "__";

  protected virtual IEnumerable nodes([PXDBInt] int? nodeID)
  {
    return !nodeID.HasValue ? (IEnumerable) ((PXSelectBase<CRValidationTree>) this.NodesSelect).Select(Array.Empty<object>()) : (IEnumerable) new CRValidation[0];
  }

  [PXButton(ImageKey = "Copy", Tooltip = "Copy")]
  [PXUIField(DisplayName = "Copy", Enabled = false)]
  public IEnumerable copy(PXAdapter adapter)
  {
    ((PXSelectBase) this.Buffer).Cache.Clear();
    foreach (CRValidationRules crValidationRules in GraphHelper.RowCast<CRValidationRules>((IEnumerable) ((PXSelectBase<CRValidationRules>) this.ValidationRules).Select(Array.Empty<object>())).Where<CRValidationRules>((Func<CRValidationRules, bool>) (r => !string.IsNullOrEmpty(r.MatchingEntity) && !string.IsNullOrEmpty(r.MatchingField))))
    {
      CRValidationRulesBuffer instance = ((PXSelectBase) this.Buffer).Cache.CreateInstance() as CRValidationRulesBuffer;
      instance.MatchingEntity = crValidationRules.MatchingEntity;
      instance.MatchingField = crValidationRules.MatchingField;
      instance.ScoreWeight = crValidationRules.ScoreWeight;
      instance.TransformationRule = crValidationRules.TransformationRule;
      instance.CreateOnEntry = crValidationRules.CreateOnEntry;
      ((PXSelectBase) this.Buffer).Cache.Insert((object) instance);
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "Paste", Tooltip = "Paste")]
  [PXUIField(DisplayName = "Paste", Enabled = false)]
  internal IEnumerable paste(PXAdapter adapter)
  {
    ((PXSelectBase) this.ValidationRules).Cache.Clear();
    List<string> matchingFields = GraphHelper.RowCast<CRValidationRulesBuffer>(((PXSelectBase) this.Buffer).Cache.Cached).Select<CRValidationRulesBuffer, string>((Func<CRValidationRulesBuffer, string>) (r => $"{r.MatchingEntity}__{r.MatchingField}")).ToList<string>();
    EnumerableExtensions.ForEach<CRValidationRules>(GraphHelper.RowCast<CRValidationRules>((IEnumerable) ((PXSelectBase<CRValidationRules>) this.ValidationRules).Select(Array.Empty<object>())).Where<CRValidationRules>((Func<CRValidationRules, bool>) (r => !matchingFields.Contains($"{r.MatchingEntity}__{r.MatchingField}"))), (Action<CRValidationRules>) (rule => ((PXSelectBase<CRValidationRules>) this.ValidationRules).Delete(rule)));
    foreach (CRValidationRulesBuffer validationRulesBuffer in ((PXSelectBase) this.Buffer).Cache.Cached)
    {
      CRValidationRulesBuffer ruleBuffer = validationRulesBuffer;
      if (!string.IsNullOrEmpty(ruleBuffer.MatchingField))
      {
        CRValidationRules crValidationRules1 = (CRValidationRules) null;
        CRValidationRules crValidationRules2 = ((PXSelectBase<CRValidationRules>) this.ValidationRules).Select(Array.Empty<object>()).FirstTableItems.FirstOrDefault<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.ValidationType == ((PXSelectBase<CRValidation>) this.CurrentNode).Current.Type && _.MatchingEntity == ruleBuffer.MatchingEntity && _.MatchingField == ruleBuffer.MatchingField));
        CRValidationRules crValidationRules3 = crValidationRules2 == null ? ((PXSelectBase) this.ValidationRules).Cache.CreateInstance() as CRValidationRules : ((PXSelectBase) this.ValidationRules).Cache.CreateCopy((object) crValidationRules2) as CRValidationRules;
        crValidationRules3.ValidationType = ((PXSelectBase<CRValidation>) this.CurrentNode).Current.Type;
        crValidationRules3.MatchingEntity = ruleBuffer.MatchingEntity;
        crValidationRules3.MatchingField = ruleBuffer.MatchingField;
        crValidationRules3.ScoreWeight = ruleBuffer.ScoreWeight;
        crValidationRules3.TransformationRule = ruleBuffer.TransformationRule;
        crValidationRules3.CreateOnEntry = ruleBuffer.CreateOnEntry;
        crValidationRules1 = (CRValidationRules) ((PXSelectBase) this.ValidationRules).Cache.Update((object) crValidationRules3);
      }
    }
    return adapter.Get();
  }

  public virtual void _(Events.RowSelected<CRValidation> e)
  {
    PXUIFieldAttribute.SetEnabled<CRValidation.validateOnEntry>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRValidation>>) e).Cache, (object) e.Row, !((PXSelectBase<CRValidationRules>) this.ValidationRules).Select(Array.Empty<object>()).FirstTableItems.Any<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A")) && ((IQueryable<PXResult<CRValidationRules>>) ((PXSelectBase<CRValidationRules>) this.ValidationRules).Select(Array.Empty<object>())).Any<PXResult<CRValidationRules>>());
  }

  public virtual void _(
    Events.FieldUpdated<CRValidation.validationThreshold> e)
  {
    EnumerableExtensions.ForEach<CRValidationRules>(((PXSelectBase<CRValidationRules>) this.ValidationRules).Select(Array.Empty<object>()).FirstTableItems.Where<CRValidationRules>((Func<CRValidationRules, bool>) (_ => _.CreateOnEntry != "A")), (Action<CRValidationRules>) (rule =>
    {
      rule.ScoreWeight = e.NewValue as Decimal?;
      ((PXSelectBase<CRValidationRules>) this.ValidationRules).Update(rule);
    }));
  }

  public virtual void _(Events.RowPersisting<CRValidation> e)
  {
    if (e.Row == null || e.Row.GramValidationDateTime.HasValue)
      return;
    e.Row.GramValidationDateTime = new DateTime?(PXTimeZoneInfo.Now);
  }

  public virtual void _(
    Events.FieldSelecting<CRValidationTree, CRValidation.description> e)
  {
    string type = e.Row?.Type;
    if (type == null || !(((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<CRValidationTree, CRValidation.description>>) e).Cache.GetStateExt<CRValidation.type>((object) e.Row) is PXStringState stateExt))
      return;
    Dictionary<string, string> valueLabelDic = stateExt.ValueLabelDic;
    string str;
    if (valueLabelDic == null || !valueLabelDic.TryGetValue(type, out str))
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<CRValidationTree, CRValidation.description>>) e).ReturnValue = (object) str;
  }

  protected virtual void _(
    Events.FieldUpdated<CRSetup.duplicateScoresNormalization> e)
  {
    if (!(e.NewValue is bool newValue) || newValue.Equals(((Events.FieldUpdatedBase<Events.FieldUpdated<CRSetup.duplicateScoresNormalization>, object, object>) e).OldValue))
      return;
    EnumerableExtensions.ForEach<CRValidation>(((PXSelectBase<CRValidation>) this.Validations).Select(Array.Empty<object>()).FirstTableItems, (Action<CRValidation>) (v =>
    {
      v.GramValidationDateTime = new DateTime?();
      ((PXSelectBase<CRValidation>) this.Validations).Update(v);
    }));
  }

  public virtual void _(
    Events.FieldSelecting<CRValidationRules, CRValidationRules.matchingFieldUI> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.ValidationType == "AA")
      this.CreateFieldStateForFieldName(e.Row.ValidationType, ((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<CRValidationRules, CRValidationRules.matchingFieldUI>>) e).Args, typeof (Contact), typeof (Address), typeof (PX.Objects.CR.Standalone.Location), typeof (BAccount));
    else
      this.CreateFieldStateForFieldName(e.Row.ValidationType, ((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<CRValidationRules, CRValidationRules.matchingFieldUI>>) e).Args, typeof (Contact), typeof (Address));
  }

  public virtual void _(Events.RowUpdated<CRValidationRules> e)
  {
    if (e.Row == null || e.OldRow == null)
      return;
    if (e.Row.CreateOnEntry != e.OldRow.CreateOnEntry)
      this.ProcessBlockTypeChange(e);
    if (!this.IsSignificantlyChanged(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRValidationRules>>) e).Cache, (object) e.Row, (object) e.OldRow))
      return;
    this.UpdateGramValidationDate(e.Row);
  }

  public virtual void _(Events.RowInserted<CRValidationRules> e)
  {
    if (e.Row == null)
      return;
    this.UpdateGramValidationDate(e.Row);
  }

  public virtual void _(Events.RowDeleted<CRValidationRules> e)
  {
    if (e.Row == null)
      return;
    this.UpdateGramValidationDate(e.Row);
    if (((IQueryable<PXResult<CRValidationRules>>) ((PXSelectBase<CRValidationRules>) this.ValidationRules).Select(Array.Empty<object>())).Any<PXResult<CRValidationRules>>())
      return;
    CRValidation copy = PXCache<CRValidation>.CreateCopy(((PXSelectBase<CRValidation>) this.CurrentNode).Current);
    copy.ValidateOnEntry = new bool?(false);
    ((PXSelectBase<CRValidation>) this.CurrentNode).Update(copy);
  }

  public virtual void _(Events.RowSelected<CRValidationRules> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<CRValidationRules.validationType>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRValidationRules>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<CRValidationRules.scoreWeight>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRValidationRules>>) e).Cache, (object) e.Row, e.Row.CreateOnEntry == "A");
    this.TransformationRulesSetList(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRValidationRules>>) e).Cache, e.Row);
  }

  public virtual void TransformationRulesSetList(PXCache cache, CRValidationRules row)
  {
    if (this.IsEmailField(row))
    {
      if (!(row.TransformationRule != "SW"))
        return;
      PXStringListAttribute.SetList<CRValidationRules.transformationRule>(cache, (object) row, new string[3]
      {
        "DN",
        "NO",
        "SE"
      }, new string[3]
      {
        "Domain Name",
        "None",
        "Split Email Addresses"
      });
    }
    else
      PXStringListAttribute.SetList<CRValidationRules.transformationRule>(cache, (object) row, new string[3]
      {
        "DN",
        "NO",
        "SW"
      }, new string[3]
      {
        "Domain Name",
        "None",
        "Split Words"
      });
  }

  protected virtual void _(
    Events.FieldVerifying<CRValidationRules, CRValidationRules.matchingFieldUI> e)
  {
    if (e.Row == null)
      return;
    string matchingFieldUI = ((Events.FieldVerifyingBase<Events.FieldVerifying<CRValidationRules, CRValidationRules.matchingFieldUI>, CRValidationRules, object>) e).NewValue?.ToString();
    if (matchingFieldUI == null)
      return;
    foreach (PXResult<CRValidationRules> pxResult in PXSelectBase<CRValidationRules, PXSelect<CRValidationRules, Where<CRValidationRules.validationType, Equal<Current<CRValidationRules.validationType>>, And<CRValidationRules.matchingEntity, Equal<Required<CRValidationRules.matchingEntity>>, And<CRValidationRules.matchingField, Equal<Required<CRValidationRules.matchingField>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) this.GetMatchingEntity(matchingFieldUI),
      (object) this.GetMatchingField(matchingFieldUI)
    }))
    {
      CRValidationRules crValidationRules = PXResult<CRValidationRules>.op_Implicit(pxResult);
      if (crValidationRules != null && crValidationRules.MatchingFieldUI == matchingFieldUI)
        throw new PXSetPropertyException("An attempt was made to add a duplicate entry.");
    }
  }

  public virtual void _(
    Events.FieldUpdated<CRValidationRules, CRValidationRules.transformationRule> e)
  {
    if (e.Row == null)
      return;
    string transformationRule = e.NewValue?.ToString();
    if (transformationRule == null)
      return;
    CRValidationRules row = e.Row;
    bool isEmailField = this.IsEmailField(row);
    this.CheckIsTransformationRuleValid(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRValidationRules, CRValidationRules.transformationRule>>) e).Cache, row, transformationRule, isEmailField);
    if (!(transformationRule == "SW"))
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRValidationRules, CRValidationRules.transformationRule>>) e).Cache.SetValueExt<CRValidationRules.createOnEntry>((object) row, (object) "A");
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRValidationRules, CRValidationRules.transformationRule>>) e).Cache.RaiseExceptionHandling<CRValidationRules.createOnEntry>((object) row, (object) row.CreateOnEntry, (Exception) new PXSetPropertyException("The Split Word transformation rule can only be used with the Allow option in the Create on Entry column.", (PXErrorLevel) 2));
  }

  public virtual void _(
    Events.FieldUpdated<CRValidationRules, CRValidationRules.createOnEntry> e)
  {
    if (e.Row == null)
      return;
    string str = e.NewValue?.ToString();
    if (str == null)
      return;
    CRValidationRules row = e.Row;
    if (!(str != "A"))
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRValidationRules, CRValidationRules.createOnEntry>>) e).Cache.SetValueExt<CRValidationRules.transformationRule>((object) row, (object) "NO");
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRValidationRules, CRValidationRules.createOnEntry>>) e).Cache.RaiseExceptionHandling<CRValidationRules.transformationRule>((object) row, (object) row.TransformationRule, (Exception) new PXSetPropertyException("The Split Word transformation rule can only be used with the Allow option in the Create on Entry column.", (PXErrorLevel) 2));
  }

  public virtual void _(
    Events.FieldUpdated<CRValidationRules, CRValidationRules.matchingFieldUI> e)
  {
    if (e.Row == null || e.NewValue?.ToString() == null)
      return;
    CRValidationRules row = e.Row;
    if (!(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRValidationRules, CRValidationRules.matchingFieldUI>>) e).Cache.GetStateExt<CRValidationRules.matchingFieldUI>((object) row) is PXStringState stateExt))
      return;
    int index = Array.IndexOf<string>(stateExt.AllowedValues, row.MatchingFieldUI);
    if (index >= 0)
    {
      string allowedValue = stateExt.AllowedValues[index];
      row.MatchingEntity = this.GetMatchingEntity(allowedValue);
      row.MatchingField = this.GetMatchingField(allowedValue);
    }
    string transformationRule = row.TransformationRule;
    bool isEmailField = this.IsEmailField(row);
    if (transformationRule.Equals("SE") && !isEmailField || transformationRule.Equals("SW") & isEmailField)
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRValidationRules, CRValidationRules.matchingFieldUI>>) e).Cache.SetValueExt<CRValidationRules.transformationRule>((object) row, (object) "NO");
    this.CheckIsTransformationRuleValid(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRValidationRules, CRValidationRules.matchingFieldUI>>) e).Cache, row, transformationRule, isEmailField);
  }

  protected void CheckIsTransformationRuleValid(
    PXCache rowCache,
    CRValidationRules row,
    string transformationRule,
    bool isEmailField)
  {
    if (!(transformationRule == "DN" & isEmailField))
      return;
    rowCache.RaiseExceptionHandling<CRValidationRules.transformationRule>((object) row, (object) row.TransformationRule, (Exception) new PXSetPropertyException("The selected transformation rule may cause inaccurate results of deduplication if most common email domains, such as gmail.com or outlook.com, are used in email addresses.", (PXErrorLevel) 2));
  }

  public virtual void ProcessBlockTypeChange(
    Events.RowUpdated<CRValidationRules> e,
    bool dummyToSuppressEmbeddingIntoEventsList = false)
  {
    if (((PXSelectBase<CRValidation>) this.CurrentNode).Current != null && e.Row.CreateOnEntry != "A")
    {
      e.Row.ScoreWeight = ((PXSelectBase<CRValidation>) this.CurrentNode).Current.ValidationThreshold ?? e.Row.ScoreWeight;
      CRValidation current = ((PXSelectBase<CRValidation>) this.CurrentNode).Current;
      current.ValidateOnEntry = new bool?(true);
      ((PXSelectBase<CRValidation>) this.CurrentNode).Update(current);
    }
    else
      ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRValidationRules>>) e).Cache.SetDefaultExt<CRValidationRules.scoreWeight>((object) e.Row);
  }

  public virtual void CreateFieldStateForFieldName(
    string validationType,
    PXFieldSelectingEventArgs e,
    params System.Type[] types)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    List<CRDuplicateValidationSetupMaint.FieldDTO> source = new List<CRDuplicateValidationSetupMaint.FieldDTO>();
    foreach (System.Type type in types)
    {
      string name = PXCacheNameAttribute.GetName(type);
      foreach (string str in ((PXGraph) this).Caches[type].GetFields_DeduplicationSearch(validationType))
      {
        string fieldName = str;
        PXFieldState stateExt = ((PXGraph) this).Caches[type].GetStateExt((object) null, fieldName) as PXFieldState;
        string fieldDisplayName = stateExt != null ? stateExt.DisplayName : fieldName;
        source.RemoveAll((Predicate<CRDuplicateValidationSetupMaint.FieldDTO>) (_ => _.FieldName == fieldName || _.FieldDisplayName == fieldDisplayName));
        source.Add(new CRDuplicateValidationSetupMaint.FieldDTO()
        {
          EntityName = type.FullName,
          EntityDisplayName = name,
          FieldName = fieldName,
          FieldDisplayName = fieldDisplayName
        });
      }
    }
    foreach (CRDuplicateValidationSetupMaint.FieldDTO fieldDto in (IEnumerable<CRDuplicateValidationSetupMaint.FieldDTO>) source.OrderBy<CRDuplicateValidationSetupMaint.FieldDTO, string>((Func<CRDuplicateValidationSetupMaint.FieldDTO, string>) (i => i.FieldDisplayName)))
    {
      stringList1.Add(fieldDto.FullyQualifiedFieldName);
      stringList2.Add(fieldDto.FieldDisplayName);
    }
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnValue, new int?(60), new bool?(), "MatchingFieldUI", new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), (string) null, (string[]) null);
  }

  public virtual bool IsSignificantlyChanged(PXCache sender, object row, object oldRow)
  {
    return row == null || oldRow == null || !sender.ObjectsEqual<CRValidationRules.matchingEntity>(row, oldRow) || !sender.ObjectsEqual<CRValidationRules.matchingField>(row, oldRow) || !sender.ObjectsEqual<CRValidationRules.matchingFieldUI>(row, oldRow) || !sender.ObjectsEqual<CRValidationRules.scoreWeight>(row, oldRow) || !sender.ObjectsEqual<CRValidationRules.transformationRule>(row, oldRow);
  }

  public virtual void UpdateGramValidationDate(CRValidationRules rules)
  {
    CRValidation copy = PXCache<CRValidation>.CreateCopy(((PXSelectBase<CRValidation>) this.CurrentNode).Current);
    copy.GramValidationDateTime = new DateTime?();
    ((PXSelectBase<CRValidation>) this.CurrentNode).Update(copy);
  }

  public virtual bool IsEmailField(CRValidationRules rule)
  {
    if (rule.MatchingEntity == null)
      return false;
    System.Type type = PXBuildManager.GetType(rule.MatchingEntity, false);
    return !(type == (System.Type) null) && ((PXGraph) this).Caches[type].GetFields_WithAttribute<PXDBEmailAttribute>().Any<string>((Func<string, bool>) (field => string.Equals(field, rule.MatchingField, StringComparison.OrdinalIgnoreCase)));
  }

  public virtual string GetMatchingEntity(string matchingFieldUI)
  {
    return matchingFieldUI.Substring(0, matchingFieldUI.IndexOf("__", StringComparison.InvariantCultureIgnoreCase));
  }

  public virtual string GetMatchingField(string matchingFieldUI)
  {
    return matchingFieldUI.Substring(matchingFieldUI.IndexOf("__", StringComparison.InvariantCultureIgnoreCase) + "__".Length);
  }

  public class FieldDTO
  {
    public string EntityName;
    public string EntityDisplayName;
    public string FieldName;
    public string FieldDisplayName;

    public string FullyQualifiedFieldName => $"{this.EntityName}__{this.FieldName}";
  }

  public class GramRecalculationExt : CRGramRecalculationExt<CRDuplicateValidationSetupMaint>
  {
    public static bool IsActive()
    {
      return CRGramRecalculationExt<CRDuplicateValidationSetupMaint>.IsFeatureActive();
    }
  }
}
