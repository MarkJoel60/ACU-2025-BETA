// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public class CROpportunityClassMaint : PXGraph<CROpportunityClassMaint, CROpportunityClass>
{
  [PXViewName("Opportunity Class")]
  public PXSelect<CROpportunityClass> OpportunityClass;
  [PXHidden]
  public PXSelect<CROpportunityClass, Where<CROpportunityClass.cROpportunityClassID, Equal<Current<CROpportunityClass.cROpportunityClassID>>>> OpportunityClassProperties;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<CROpportunityClass, CROpportunity> Mapping;
  [PXHidden]
  public PXSelect<CRSetup> Setup;
  [PXViewName("Opportunity Class Stages")]
  public PXSelectOrderBy<CROpportunityProbability, OrderBy<Asc<CROpportunityProbability.sortOrder, Asc<CROpportunityProbability.probability, Asc<CROpportunityProbability.stageCode>>>>> OpportunityProbabilities;
  [PXHidden]
  public PXSelect<CROpportunityClassProbability> OpportunityClassActiveProbabilities;

  protected virtual IEnumerable opportunityProbabilities()
  {
    CROpportunityClassMaint opportunityClassMaint = this;
    List<string> activeProbabilities = new List<string>();
    foreach (PXResult<CROpportunityClassProbability> pxResult in ((PXSelectBase<CROpportunityClassProbability>) opportunityClassMaint.OpportunityClassActiveProbabilities).Select(Array.Empty<object>()))
    {
      CROpportunityClassProbability classProbability = PXResult<CROpportunityClassProbability>.op_Implicit(pxResult);
      if (classProbability.ClassID == ((PXSelectBase<CROpportunityClass>) opportunityClassMaint.OpportunityClass).Current.CROpportunityClassID)
        activeProbabilities.Add(classProbability.StageID);
    }
    foreach (PXResult<CROpportunityProbability> pxResult in PXSelectBase<CROpportunityProbability, PXSelectOrderBy<CROpportunityProbability, OrderBy<Asc<CROpportunityProbability.sortOrder, Asc<CROpportunityProbability.probability, Asc<CROpportunityProbability.stageCode>>>>>.Config>.Select((PXGraph) opportunityClassMaint, Array.Empty<object>()))
    {
      CROpportunityProbability opportunityProbability = PXResult<CROpportunityProbability>.op_Implicit(pxResult);
      opportunityProbability.IsActive = new bool?(activeProbabilities.Contains(opportunityProbability.StageCode));
      yield return (object) new PXResult<CROpportunityProbability>(opportunityProbability);
    }
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Attribute")]
  protected virtual void _(
    Events.CacheAttached<CSAttributeGroup.attributeID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Opportunity Stage")]
  protected virtual void _(
    Events.CacheAttached<CROpportunityProbability.stageCode> e)
  {
  }

  protected virtual void CROpportunityClass_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is CROpportunityClass row))
      return;
    CRSetup crSetup = PXResultset<CRSetup>.op_Implicit(((PXSelectBase<CRSetup>) this.Setup).Select(Array.Empty<object>()));
    if (crSetup == null || !(crSetup.DefaultOpportunityClassID == row.CROpportunityClassID))
      return;
    crSetup.DefaultOpportunityClassID = (string) null;
    ((PXSelectBase<CRSetup>) this.Setup).Update(crSetup);
  }

  protected virtual void _(
    Events.FieldVerifying<CROpportunityClass, CROpportunityClass.defaultOwner> e)
  {
    CROpportunityClass row = e.Row;
    if (row == null || ((Events.FieldVerifyingBase<Events.FieldVerifying<CROpportunityClass, CROpportunityClass.defaultOwner>, CROpportunityClass, object>) e).NewValue != null)
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<CROpportunityClass, CROpportunityClass.defaultOwner>, CROpportunityClass, object>) e).NewValue = (object) (row.DefaultOwner ?? "N");
  }

  protected virtual void _(
    Events.FieldUpdated<CROpportunityClass, CROpportunityClass.defaultOwner> e)
  {
    CROpportunityClass row = e.Row;
    if (row == null || e.NewValue == ((Events.FieldUpdatedBase<Events.FieldUpdated<CROpportunityClass, CROpportunityClass.defaultOwner>, CROpportunityClass, object>) e).OldValue)
      return;
    row.DefaultAssignmentMapID = new int?();
  }

  protected virtual void CROpportunityClass_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CROpportunityClass row))
      return;
    ((PXAction) this.Delete).SetEnabled(this.CanDelete(row));
  }

  protected virtual void CROpportunityClass_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is CROpportunityClass row && !this.CanDelete(row))
      throw new PXException("This record is referenced and cannot be deleted.");
  }

  protected virtual void CROpportunityClass_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (PXResultset<CROpportunityProbability>.op_Implicit(((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Select(Array.Empty<object>())) == null)
      this.InitOpportunityProbabilities();
    if (((PXSelectBase<CROpportunityClass>) this.OpportunityClass).Select(Array.Empty<object>()).Count != 1)
      return;
    this.ActivateAllOpportunityClassProbabilities();
  }

  protected virtual void CROpportunityClass_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (((PXSelectBase<CROpportunityClass>) this.OpportunityClass).Current == null || e.Operation != 2 && e.Operation != 1)
      return;
    CROpportunityClass current = ((PXSelectBase<CROpportunityClass>) this.OpportunityClass).Current;
    foreach (PXResult<CROpportunityClassProbability> pxResult in ((PXSelectBase<CROpportunityClassProbability>) this.OpportunityClassActiveProbabilities).Select(Array.Empty<object>()))
    {
      if (PXResult<CROpportunityClassProbability>.op_Implicit(pxResult).ClassID == current.CROpportunityClassID)
        return;
    }
    ((PXSelectBase) this.OpportunityClass).Cache.RaiseExceptionHandling<CROpportunityClass.cROpportunityClassID>((object) current, (object) current.CROpportunityClassID, (Exception) new PXSetPropertyException("Activate at least one stage for this opportunity class.", (PXErrorLevel) 4));
  }

  private bool CanDelete(CROpportunityClass row)
  {
    if (row != null)
    {
      if (PXResultset<CROpportunity>.op_Implicit(PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.classID, Equal<Required<CROpportunity.classID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.CROpportunityClassID
      })) != null)
        return false;
    }
    return true;
  }

  public virtual void CROpportunityProbability_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CROpportunityProbability row))
      return;
    bool flag = sender.GetStatus(e.Row) == 2 || sender.GetStatus(e.Row) == 4;
    PXUIFieldAttribute.SetEnabled<CROpportunityProbability.stageCode>(sender, (object) row, flag);
  }

  public virtual void CROpportunityProbability_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!e.ExternalCall || !(e.Row is CROpportunityProbability row))
      return;
    if (PXResultset<PX.Objects.CR.Standalone.CROpportunity>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.CROpportunity, PXSelect<PX.Objects.CR.Standalone.CROpportunity, Where<PX.Objects.CR.Standalone.CROpportunity.stageID, Equal<Required<CROpportunityProbability.stageCode>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.StageCode
    })) != null)
      throw new PXException("The '{0}' stage cannot be deleted because it is specified for at least one opportunity.", new object[1]
      {
        (object) row.Name
      });
    List<string> values = new List<string>();
    foreach (PXResult<CROpportunityClassProbability> pxResult in PXSelectBase<CROpportunityClassProbability, PXSelect<CROpportunityClassProbability, Where<CROpportunityClassProbability.stageID, Equal<Required<CROpportunityProbability.stageCode>>, And<CROpportunityClassProbability.classID, NotEqual<Current<CROpportunityClass.cROpportunityClassID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.StageCode
    }))
    {
      CROpportunityClassProbability classProbability = PXResult<CROpportunityClassProbability>.op_Implicit(pxResult);
      values.Add(classProbability.ClassID);
    }
    if (values.Count > 0)
      throw new PXException("This stage cannot be deleted because it is active for the following opportunity class(es): {0}.", new object[1]
      {
        (object) string.Join(", ", (IEnumerable<string>) values)
      });
    if (((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Ask("Warning", "This stage will be deleted from all opportunity classes. Are you sure you want to proceed?", (MessageButtons) 4, (MessageIcon) 3) == 6)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void CROpportunityProbability_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.UpdateOpportunityClassProbability(sender, (CROpportunityProbability) e.Row);
  }

  public virtual void CROpportunityProbability_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.UpdateOpportunityClassProbability(sender, (CROpportunityProbability) e.Row);
    CROpportunityProbability oldRow = e.OldRow as CROpportunityProbability;
    if (!(e.Row is CROpportunityProbability row) || oldRow == null)
      return;
    if (!(row.Name != oldRow.Name))
    {
      int? probability = row.Probability;
      int? nullable = oldRow.Probability;
      if (probability.GetValueOrDefault() == nullable.GetValueOrDefault() & probability.HasValue == nullable.HasValue)
      {
        nullable = row.SortOrder;
        int? sortOrder = oldRow.SortOrder;
        if (nullable.GetValueOrDefault() == sortOrder.GetValueOrDefault() & nullable.HasValue == sortOrder.HasValue)
          return;
      }
    }
    ((PXSelectBase) this.OpportunityProbabilities).Cache.RaiseExceptionHandling<CROpportunityProbability.stageCode>((object) row, (object) row.StageCode, (Exception) new PXSetPropertyException("All opportunity classes share the same list of stages. Modifying a stage in this list will affect all opportunity classes.", (PXErrorLevel) 3));
  }

  public virtual void CROpportunityProbability_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is CROpportunityProbability row) || ((PXSelectBase<CROpportunityClass>) this.OpportunityClass).Current == null)
      return;
    foreach (PXResult<CROpportunityClassProbability> pxResult in PXSelectBase<CROpportunityClassProbability, PXSelect<CROpportunityClassProbability, Where<CROpportunityClassProbability.stageID, Equal<Required<CROpportunityProbability.stageCode>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.StageCode
    }))
      ((PXSelectBase<CROpportunityClassProbability>) this.OpportunityClassActiveProbabilities).Delete(PXResult<CROpportunityClassProbability>.op_Implicit(pxResult));
  }

  public virtual void CROpportunityProbability_Probability_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CROpportunityProbability row = e.Row as CROpportunityProbability;
    if (row.SortOrder.HasValue)
      return;
    row.SortOrder = row.Probability;
  }

  private void UpdateOpportunityClassProbability(
    PXCache sender,
    CROpportunityProbability probability)
  {
    if (((PXSelectBase<CROpportunityClass>) this.OpportunityClass).Current == null)
      return;
    CROpportunityClassProbability classProbability1 = PXResultset<CROpportunityClassProbability>.op_Implicit(PXSelectBase<CROpportunityClassProbability, PXSelect<CROpportunityClassProbability, Where<CROpportunityClassProbability.classID, Equal<Current<CROpportunityClass.cROpportunityClassID>>, And<CROpportunityClassProbability.stageID, Equal<Required<CROpportunityProbability.stageCode>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) probability.StageCode
    }));
    if (probability.IsActive.GetValueOrDefault() && classProbability1 == null)
    {
      CROpportunityClassProbability classProbability2 = ((PXSelectBase<CROpportunityClassProbability>) this.OpportunityClassActiveProbabilities).Insert();
      classProbability2.ClassID = ((PXSelectBase<CROpportunityClass>) this.OpportunityClass).Current.CROpportunityClassID;
      classProbability2.StageID = probability.StageCode;
      ((PXSelectBase<CROpportunityClassProbability>) this.OpportunityClassActiveProbabilities).Update(classProbability2);
    }
    else
    {
      bool? isActive = probability.IsActive;
      bool flag = false;
      if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue) || classProbability1 == null)
        return;
      ((PXSelectBase<CROpportunityClassProbability>) this.OpportunityClassActiveProbabilities).Delete(classProbability1);
    }
  }

  private void InitOpportunityProbabilities()
  {
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.OpportunityProbabilities).Cache
    }))
    {
      ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Insert(new CROpportunityProbability()
      {
        StageCode = "L",
        Name = "Prospect",
        Probability = new int?(0),
        SortOrder = new int?(0)
      });
      ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Insert(new CROpportunityProbability()
      {
        StageCode = "N",
        Name = "Nurture",
        Probability = new int?(5),
        SortOrder = new int?(5)
      });
      ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Insert(new CROpportunityProbability()
      {
        StageCode = "P",
        Name = "Qualification",
        Probability = new int?(10),
        SortOrder = new int?(10)
      });
      ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Insert(new CROpportunityProbability()
      {
        StageCode = "Q",
        Name = "Development",
        Probability = new int?(20),
        SortOrder = new int?(20)
      });
      ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Insert(new CROpportunityProbability()
      {
        StageCode = "V",
        Name = "Solution",
        Probability = new int?(40),
        SortOrder = new int?(40)
      });
      ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Insert(new CROpportunityProbability()
      {
        StageCode = "A",
        Name = "Proof",
        Probability = new int?(60),
        SortOrder = new int?(60)
      });
      ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Insert(new CROpportunityProbability()
      {
        StageCode = "R",
        Name = "Negotiation",
        Probability = new int?(80 /*0x50*/),
        SortOrder = new int?(80 /*0x50*/)
      });
      ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Insert(new CROpportunityProbability()
      {
        StageCode = "W",
        Name = "Won",
        Probability = new int?(100),
        SortOrder = new int?(100)
      });
    }
  }

  private void ActivateAllOpportunityClassProbabilities()
  {
    if (((PXSelectBase<CROpportunityClass>) this.OpportunityClass).Current == null)
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.OpportunityClassActiveProbabilities).Cache
    }))
    {
      string opportunityClassId = ((PXSelectBase<CROpportunityClass>) this.OpportunityClass).Current.CROpportunityClassID;
      foreach (PXResult<CROpportunityProbability> pxResult in ((PXSelectBase<CROpportunityProbability>) this.OpportunityProbabilities).Select(Array.Empty<object>()))
      {
        CROpportunityProbability opportunityProbability = PXResult<CROpportunityProbability>.op_Implicit(pxResult);
        ((PXSelectBase<CROpportunityClassProbability>) this.OpportunityClassActiveProbabilities).Insert(new CROpportunityClassProbability()
        {
          ClassID = opportunityClassId,
          StageID = opportunityProbability.StageCode
        });
      }
    }
  }
}
