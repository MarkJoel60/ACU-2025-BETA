// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXReverseRelationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

public class PXReverseRelationAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber
{
  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Row is CRRelation row ? (!row.TargetNoteID.HasValue ? 1 : 0) : 1) != 0)
      return;
    CRRelation crRelation = CRRelation.UK.Find(sender.Graph, row.RefNoteID, row.TargetNoteID, row.Role);
    if (crRelation == null)
      return;
    int? relationId1 = crRelation.RelationID;
    int? relationId2 = row.RelationID;
    if (relationId1.GetValueOrDefault() == relationId2.GetValueOrDefault() & relationId1.HasValue == relationId2.HasValue)
      return;
    sender.RaiseExceptionHandling<CRRelation.targetNoteID>((object) row, (object) row.TargetNoteID, (Exception) new PXSetPropertyException("The relation with the same settings already exists.", (PXErrorLevel) 5));
  }

  public void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is CRRelation row) || e.TranStatus != null)
      return;
    CRRelation original = sender.GetOriginal((object) row) as CRRelation;
    bool flag1 = PXReverseRelationAttribute.IsBidirectionalRole(original);
    bool flag2 = PXReverseRelationAttribute.IsBidirectionalRole(row);
    if (!flag1 && !flag2)
      return;
    switch (PXDBOperationExt.Command(e.Operation) - 1)
    {
      case 0:
        if (!flag1 || flag2)
        {
          if (original == null || !this.IsSignificantlyChanged(sender, row, original))
            break;
          CRRelation relation = PXReverseRelationAttribute.SearchForInversedRelation(sender.Graph, original);
          if (relation == null)
            break;
          int? relationId = relation.RelationID;
          sender.RestoreCopy((object) relation, (object) this.Invert(sender, row));
          relation.RelationID = relationId;
          this.UpdateRelationInDB(sender, relation);
          break;
        }
        goto case 2;
      case 1:
        if (!flag2)
          break;
        CRRelation relation1 = this.Invert(sender, row);
        this.InsertRelationInDB(sender, relation1);
        break;
      case 2:
        if (original == null)
          break;
        CRRelation relation2 = PXReverseRelationAttribute.SearchForInversedRelation(sender.Graph, original);
        if (relation2 == null)
          break;
        this.DeleteRelationInDB(sender, relation2);
        break;
    }
  }

  public virtual CRRelation Invert(PXCache sender, CRRelation relation)
  {
    if (relation == null)
      return (CRRelation) null;
    if (!(sender.CreateCopy((object) relation) is CRRelation copy))
      return (CRRelation) null;
    copy.RelationID = new int?();
    copy.RefNoteID = relation.TargetNoteID;
    copy.RefEntityType = relation.TargetType;
    copy.TargetNoteID = relation.RefNoteID;
    copy.TargetType = relation.RefEntityType;
    copy.Role = PXReverseRelationAttribute.GetOppositeRole(relation);
    object obj1;
    sender.RaiseFieldDefaulting<CRRelation.entityID>((object) copy, ref obj1);
    sender.SetValue<CRRelation.entityID>((object) copy, obj1);
    object obj2;
    sender.RaiseFieldDefaulting<CRRelation.contactID>((object) copy, ref obj2);
    sender.SetValue<CRRelation.contactID>((object) copy, obj2);
    return copy;
  }

  protected virtual void InsertRelationInDB(PXCache sender, CRRelation relation)
  {
    PreventRecursionCall.Execute((Action) (() =>
    {
      object obj = sender.Insert((object) relation);
      if (obj == null)
        return;
      sender.PersistInserted(obj);
    }), nameof (InsertRelationInDB), "C:\\build\\code_repo\\WebSites\\Pure\\PX.Objects\\CR\\Descriptor\\Attributes\\PXReverseRelationAttribute.cs", 132);
  }

  protected virtual void UpdateRelationInDB(PXCache sender, CRRelation relation)
  {
    PreventRecursionCall.Execute((Action) (() =>
    {
      object obj = sender.Update((object) relation);
      if (obj == null)
        return;
      sender.PersistUpdated(obj);
    }), nameof (UpdateRelationInDB), "C:\\build\\code_repo\\WebSites\\Pure\\PX.Objects\\CR\\Descriptor\\Attributes\\PXReverseRelationAttribute.cs", 145);
  }

  protected virtual void DeleteRelationInDB(PXCache sender, CRRelation relation)
  {
    PreventRecursionCall.Execute((Action) (() =>
    {
      object obj = sender.Delete((object) relation);
      if (obj == null)
        return;
      sender.PersistDeleted(obj);
    }), nameof (DeleteRelationInDB), "C:\\build\\code_repo\\WebSites\\Pure\\PX.Objects\\CR\\Descriptor\\Attributes\\PXReverseRelationAttribute.cs", 158);
  }

  public virtual bool IsSignificantlyChanged(PXCache sender, CRRelation row, CRRelation oldRow)
  {
    return row == null || oldRow == null || !sender.ObjectsEqual<CRRelation.refEntityType>((object) row, (object) oldRow) || !sender.ObjectsEqual<CRRelation.refNoteID>((object) row, (object) oldRow) || !sender.ObjectsEqual<CRRelation.targetType>((object) row, (object) oldRow) || !sender.ObjectsEqual<CRRelation.targetNoteID>((object) row, (object) oldRow);
  }

  public static bool IsBidirectionalRole(CRRelation relation)
  {
    string role = relation.Role;
    return role == "PR" || role == "CH" || role == "DE" || role == "SR";
  }

  public static string GetOppositeRole(CRRelation relation)
  {
    switch (relation.Role)
    {
      case "PR":
        return "CH";
      case "CH":
        return "PR";
      case "DE":
        return "SR";
      case "SR":
        return "DE";
      default:
        return relation.Role;
    }
  }

  public static CRRelation SearchForInversedRelation(PXGraph graph, CRRelation relation)
  {
    return CRRelation.UK.Find(graph, relation.TargetNoteID, relation.RefNoteID, PXReverseRelationAttribute.GetOppositeRole(relation));
  }
}
