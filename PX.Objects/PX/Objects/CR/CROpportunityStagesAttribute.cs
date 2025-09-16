// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityStagesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

public class CROpportunityStagesAttribute : 
  PXStringListAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowSelectedSubscriber
{
  private readonly System.Type _oppClassID;
  private readonly System.Type _stageChangedDate;

  public virtual bool IsLocalizable => true;

  public bool OnlyActiveStages { get; set; }

  public CROpportunityStagesAttribute(System.Type oppClassID, System.Type stageChangedDate = null)
  {
    if (oppClassID == (System.Type) null)
      throw new ArgumentNullException(nameof (oppClassID));
    if (!typeof (IBqlField).IsAssignableFrom(oppClassID))
      throw new ArgumentException(MainTools.GetLongName(typeof (IBqlField)) + " expected.", nameof (oppClassID));
    if (stageChangedDate != (System.Type) null && !typeof (IBqlField).IsAssignableFrom(stageChangedDate))
      throw new ArgumentException(MainTools.GetLongName(typeof (IBqlField)) + " expected.", nameof (stageChangedDate));
    this._oppClassID = oppClassID;
    this._stageChangedDate = stageChangedDate;
    base.TryLocalize((PXCache) null);
  }

  public virtual void FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(cache, e);
    if (e.Row == null)
      return;
    string classID;
    string stageID;
    List<CROpportunityStagesAttribute.CROppClassStage> source = !this.OnlyActiveStages || !this.TryGetClassAndStage(cache, e.Row, out classID, out stageID) ? this.GetClassStages(string.Empty) : this.GetClassStages(classID).Where<CROpportunityStagesAttribute.CROppClassStage>((Func<CROpportunityStagesAttribute.CROppClassStage, bool>) (s => s.IsActive.GetValueOrDefault() || s.StageID == stageID)).ToList<CROpportunityStagesAttribute.CROppClassStage>();
    string[] array1 = source.Select<CROpportunityStagesAttribute.CROppClassStage, string>((Func<CROpportunityStagesAttribute.CROppClassStage, string>) (x => x.StageID)).ToArray<string>();
    string[] array2 = source.Select<CROpportunityStagesAttribute.CROppClassStage, string>((Func<CROpportunityStagesAttribute.CROppClassStage, string>) (x => Messages.GetLocal(x.Name))).ToArray<string>();
    e.ReturnState = (object) (PXStringState) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(), new int?(-1), (string) null, array1, array2, new bool?(true), (string) null, this._NeutralAllowedLabels);
  }

  public virtual void FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    string classID;
    if (e.Row == null || !this.TryGetClassAndStage(cache, e.Row, out classID, out string _))
      return;
    CROpportunityStagesAttribute.CROppClassStage crOppClassStage = this.GetClassStages(classID).FirstOrDefault<CROpportunityStagesAttribute.CROppClassStage>((Func<CROpportunityStagesAttribute.CROppClassStage, bool>) (s => s.IsActive.GetValueOrDefault()));
    e.NewValue = (object) crOppClassStage?.StageID;
  }

  public virtual void RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    string classID1;
    string stageID1;
    string classID2;
    string stageID2;
    if (e.Row == null || !this.TryGetClassAndStage(cache, e.Row, out classID1, out stageID1) || !this.TryGetClassAndStage(cache, e.OldRow, out classID2, out stageID2))
      return;
    if (classID1 == null && stageID1 != null)
      stageID1 = (string) null;
    if (classID1 != classID2 && stageID1 == null)
    {
      object obj;
      cache.RaiseFieldDefaulting(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      cache.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, obj);
      stageID1 = cache.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) as string;
    }
    if (!(stageID1 != stageID2) || !(this._stageChangedDate != (System.Type) null))
      return;
    string field = cache.GetField(this._stageChangedDate);
    if ((cache.GetValue(e.Row, field) as DateTime?).HasValue)
    {
      cache.SetValue(e.Row, field, (object) PXTimeZoneInfo.Now);
    }
    else
    {
      object obj;
      cache.RaiseFieldDefaulting(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      if (!(stageID1 != (string) obj))
        return;
      cache.SetValue(e.Row, field, (object) PXTimeZoneInfo.Now);
    }
  }

  public virtual void RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    string classID;
    string stageID;
    if (e.Row == null || !this.TryGetClassAndStage(cache, e.Row, out classID, out stageID))
      return;
    CROpportunityStagesAttribute.CROppClassStage crOppClassStage = this.GetClassStages(classID).SingleOrDefault<CROpportunityStagesAttribute.CROppClassStage>((Func<CROpportunityStagesAttribute.CROppClassStage, bool>) (s => s.StageID == stageID));
    if (classID != null && crOppClassStage != null)
    {
      bool? isActive = crOppClassStage.IsActive;
      bool flag = false;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      {
        cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) stageID, (Exception) new PXSetPropertyException("This stage is not active for the selected opportunity class.", (PXErrorLevel) 4));
        return;
      }
    }
    if (((PXFieldState) cache.GetStateExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName)).ErrorLevel != 4)
      return;
    cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) null, (Exception) null);
  }

  protected bool TryGetClassAndStage(
    PXCache cache,
    object row,
    out string classID,
    out string stageID)
  {
    string field = cache.GetField(this._oppClassID);
    if (cache.Fields.Contains(field) && cache.Fields.Contains(((PXEventSubscriberAttribute) this)._FieldName))
    {
      classID = cache.GetValue(row, field) as string;
      stageID = cache.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldName) as string;
      return true;
    }
    classID = (string) null;
    stageID = (string) null;
    return false;
  }

  protected virtual void TryLocalize(PXCache sender)
  {
    List<CROpportunityStagesAttribute.CROppClassStage> classStages;
    try
    {
      classStages = this.GetClassStages(string.Empty);
    }
    catch (PXInvalidOperationException ex)
    {
      PXTrace.WriteError((Exception) ex);
      return;
    }
    this._AllowedValues = classStages.Select<CROpportunityStagesAttribute.CROppClassStage, string>((Func<CROpportunityStagesAttribute.CROppClassStage, string>) (x => x.StageID)).ToArray<string>();
    this._AllowedLabels = classStages.Select<CROpportunityStagesAttribute.CROppClassStage, string>((Func<CROpportunityStagesAttribute.CROppClassStage, string>) (x => x.Name)).ToArray<string>();
    this._NeutralAllowedLabels = classStages.Select<CROpportunityStagesAttribute.CROppClassStage, string>((Func<CROpportunityStagesAttribute.CROppClassStage, string>) (x => x.NeutralName)).ToArray<string>();
  }

  protected virtual List<CROpportunityStagesAttribute.CROppClassStage> GetClassStages(string classID)
  {
    CROpportunityStagesAttribute.Definition definitions = this.Definitions;
    if (definitions == null)
      throw new PXInvalidOperationException("The opportunity stages slot was not initialized.");
    List<CROpportunityStagesAttribute.CROppClassStage> crOppClassStageList;
    return !definitions.ClassStages.TryGetValue(classID ?? string.Empty, out crOppClassStageList) ? definitions.ClassStages[string.Empty] : crOppClassStageList;
  }

  private CROpportunityStagesAttribute.Definition Definitions
  {
    get
    {
      CROpportunityStagesAttribute.Definition slot = PXContext.GetSlot<CROpportunityStagesAttribute.Definition>();
      if (slot != null)
        return slot;
      return PXContext.SetSlot<CROpportunityStagesAttribute.Definition>(PXDatabase.GetLocalizableSlot<CROpportunityStagesAttribute.Definition>(typeof (CROpportunityStagesAttribute.Definition).FullName, new System.Type[2]
      {
        typeof (CROpportunityProbability),
        typeof (CROpportunityClassProbability)
      }));
    }
  }

  protected class CROppClassStage
  {
    public string StageID { get; set; }

    public string Name { get; set; }

    public string NeutralName { get; set; }

    public int? SortOrder { get; set; }

    public int? Probability { get; set; }

    public bool? IsActive { get; set; }
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public readonly Dictionary<string, List<CROpportunityStagesAttribute.CROppClassStage>> ClassStages = new Dictionary<string, List<CROpportunityStagesAttribute.CROppClassStage>>();

    public void Prefetch()
    {
      this.ClassStages.Clear();
      using (new PXConnectionScope())
      {
        List<CROpportunityStagesAttribute.CROppClassStage> list = PXDatabase.SelectMulti<CROpportunityProbability>(new PXDataField[5]
        {
          (PXDataField) new PXDataField<CROpportunityProbability.stageCode>(),
          (PXDataField) new PXDataField<CROpportunityProbability.name>(),
          PXDBLocalizableStringAttribute.GetValueSelect("CROpportunityProbability", "Name", false),
          (PXDataField) new PXDataField<CROpportunityProbability.sortOrder>(),
          (PXDataField) new PXDataField<CROpportunityProbability.probability>()
        }).Select<PXDataRecord, CROpportunityStagesAttribute.CROppClassStage>((Func<PXDataRecord, CROpportunityStagesAttribute.CROppClassStage>) (x => new CROpportunityStagesAttribute.CROppClassStage()
        {
          StageID = x.GetString(0),
          NeutralName = x.GetString(1),
          Name = x.GetString(2),
          SortOrder = x.GetInt32(3),
          Probability = x.GetInt32(4),
          IsActive = new bool?(false)
        })).OrderBy<CROpportunityStagesAttribute.CROppClassStage, int?>((Func<CROpportunityStagesAttribute.CROppClassStage, int?>) (x => x.SortOrder)).ThenBy<CROpportunityStagesAttribute.CROppClassStage, int?>((Func<CROpportunityStagesAttribute.CROppClassStage, int?>) (x => x.Probability)).ThenBy<CROpportunityStagesAttribute.CROppClassStage, string>((Func<CROpportunityStagesAttribute.CROppClassStage, string>) (x => x.StageID)).ToList<CROpportunityStagesAttribute.CROppClassStage>();
        this.ClassStages.Add(string.Empty, list);
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<CROpportunityClassProbability>(new PXDataField[2]
        {
          (PXDataField) new PXDataField<CROpportunityClassProbability.classID>(),
          (PXDataField) new PXDataField<CROpportunityClassProbability.stageID>()
        }))
        {
          string key = pxDataRecord.GetString(0);
          string stageID = pxDataRecord.GetString(1);
          if (!this.ClassStages.ContainsKey(key))
            this.ClassStages.Add(key, CROpportunityStagesAttribute.Definition.CloneStagesList(list));
          CROpportunityStagesAttribute.CROppClassStage crOppClassStage = this.ClassStages[key].FirstOrDefault<CROpportunityStagesAttribute.CROppClassStage>((Func<CROpportunityStagesAttribute.CROppClassStage, bool>) (s => s.StageID == stageID));
          if (crOppClassStage != null)
            crOppClassStage.IsActive = new bool?(true);
        }
      }
    }

    private static List<CROpportunityStagesAttribute.CROppClassStage> CloneStagesList(
      List<CROpportunityStagesAttribute.CROppClassStage> stages)
    {
      return stages.Select<CROpportunityStagesAttribute.CROppClassStage, CROpportunityStagesAttribute.CROppClassStage>((Func<CROpportunityStagesAttribute.CROppClassStage, CROpportunityStagesAttribute.CROppClassStage>) (s => new CROpportunityStagesAttribute.CROppClassStage()
      {
        StageID = s.StageID,
        Name = s.Name,
        NeutralName = s.NeutralName,
        SortOrder = s.SortOrder,
        Probability = s.Probability,
        IsActive = s.IsActive
      })).ToList<CROpportunityStagesAttribute.CROppClassStage>();
    }
  }
}
