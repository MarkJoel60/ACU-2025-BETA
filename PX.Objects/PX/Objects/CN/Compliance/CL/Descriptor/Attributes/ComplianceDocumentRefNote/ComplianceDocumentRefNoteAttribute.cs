// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentRefNoteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote;

public class ComplianceDocumentRefNoteAttribute : 
  PXDBGuidAttribute,
  IPXRowPersistedSubscriber,
  IPXRowDeletedSubscriber
{
  private const string ClDisplayNameField = "ClDisplayName";
  private readonly Type itemType;
  private readonly ComplianceDocumentEntityHelper complianceDocumentEntityHelper;
  private readonly RefNoteRedirectHelper redirectHelper;
  private EntityHelper entityHelper;
  private string descriptionFieldName;

  public ComplianceDocumentRefNoteAttribute(Type itemType)
    : base(false)
  {
    this.itemType = itemType;
    this.complianceDocumentEntityHelper = new ComplianceDocumentEntityHelper(itemType);
    this.redirectHelper = new RefNoteRedirectHelper();
  }

  public virtual void CacheAttached(PXCache cache)
  {
    this.entityHelper = new EntityHelper(cache.Graph);
    this.descriptionFieldName = ((PXEventSubscriberAttribute) this)._FieldName + "_Description";
    ((PXDBFieldAttribute) this).CacheAttached(cache);
    PXGraph graph = cache.Graph;
    string key = "_GuidSelector_" + MainTools.GetLongName(this.itemType);
    if (!((Dictionary<string, PXView>) graph.Views).ContainsKey(key))
    {
      PXView view = this.complianceDocumentEntityHelper.CreateView(graph);
      graph.Views.Add(key, view);
    }
    cache.Fields.Add(this.descriptionFieldName);
    // ISSUE: method pointer
    cache.Graph.FieldSelecting.AddHandler(cache.GetItemType(), this.descriptionFieldName, new PXFieldSelecting((object) this, __methodptr(DescriptionFieldSelecting)));
    this.CreateRedirectAction(cache);
  }

  public virtual void FieldSelecting(PXCache cache, PXFieldSelectingEventArgs args)
  {
    string viewName = "_GuidSelector_" + MainTools.GetLongName(this.itemType);
    string[] fieldList = this.entityHelper.GetFieldList(this.itemType);
    string[] array = ((IEnumerable<string>) fieldList).Select<string, string>(new Func<string, string>(this.GetFieldDisplayName)).ToArray<string>();
    PXFieldState fieldState = this.GetFieldState(args, viewName, fieldList, array);
    fieldState.ValueField = "ClDisplayName";
    fieldState.DescriptionName = "ClDisplayName";
    fieldState.SelectorMode = (PXSelectorMode) 6;
    args.ReturnState = (object) fieldState;
    args.ReturnValue = (object) this.GetDescription(cache, args.Row);
  }

  public static void SetComplianceDocumentReference<TField>(
    PXCache cache,
    ComplianceDocument doc,
    string docType,
    string refNumber,
    Guid? noteId)
    where TField : IBqlField
  {
    cache.GetAttributesReadonly<TField>((object) doc).OfType<ComplianceDocumentRefNoteAttribute>().First<ComplianceDocumentRefNoteAttribute>().SetComplianceDocumentReference(cache, doc, docType, refNumber, noteId);
  }

  public virtual void SetComplianceDocumentReference(
    PXCache cache,
    ComplianceDocument doc,
    string docType,
    string refNumber,
    Guid? noteId)
  {
    Guid? referenceId = (Guid?) cache.GetValue((object) doc, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    ComplianceDocumentReference documentReference = this.InsertComplianceDocumentReference(cache.Graph, docType, refNumber, noteId);
    cache.SetValue((object) doc, ((PXEventSubscriberAttribute) this)._FieldName, (object) documentReference.ComplianceDocumentReferenceId);
    this.TryDeleteReference(cache, referenceId);
  }

  public virtual void FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs args)
  {
    if (args.NewValue != null)
    {
      Guid? noteId = this.complianceDocumentEntityHelper.GetNoteId(cache.Graph, (string) args.NewValue);
      if (noteId.HasValue)
      {
        Guid? referenceId = (Guid?) cache.GetValue(args.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
        ComplianceDocumentReference documentReference = this.InsertComplianceDocumentReference(cache, args, noteId);
        args.NewValue = (object) documentReference.ComplianceDocumentReferenceId;
        this.TryDeleteReference(cache, referenceId);
      }
      else
      {
        ComplianceDocument row = args.Row as ComplianceDocument;
        args.NewValue = cache.GetValue((object) row, ((PXEventSubscriberAttribute) this)._FieldName);
        ((CancelEventArgs) args).Cancel = true;
      }
    }
    else
      this.DeleteExistingReference(cache, (ComplianceDocument) args.Row);
  }

  private PXButtonDelegate GetRedirectionDelegate(PXCache cache)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    return new PXButtonDelegate((object) new ComplianceDocumentRefNoteAttribute.\u003C\u003Ec__DisplayClass12_0()
    {
      cache = cache,
      \u003C\u003E4__this = this
    }, __methodptr(\u003CGetRedirectionDelegate\u003Eb__0));
  }

  private PXFieldState GetFieldState(
    PXFieldSelectingEventArgs args,
    string viewName,
    string[] fieldList,
    string[] headerList)
  {
    return PXFieldState.CreateInstance(args.ReturnState, (Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, ((PXEventSubscriberAttribute) this)._FieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, viewName, fieldList, headerList);
  }

  private void DescriptionFieldSelecting(PXCache cache, PXFieldSelectingEventArgs args)
  {
    string description = this.GetDescription(cache, args.Row);
    args.ReturnState = (object) PXFieldState.CreateInstance((object) description, typeof (string), new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, this.descriptionFieldName, (string) null, this.descriptionFieldName, (string) null, (PXErrorLevel) 0, new bool?(false), new bool?(!string.IsNullOrEmpty(description)), new bool?(), (PXUIVisibility) 1, (string) null, (string[]) null, (string[]) null);
  }

  private string GetDescription(PXCache cache, object row)
  {
    Guid? referenceId = (Guid?) cache.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (referenceId.HasValue && referenceId.Value != Guid.Empty)
    {
      ComplianceDocumentReference documentReference = ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(cache.Graph, referenceId);
      if (documentReference != null)
        return this.GetFieldValue(documentReference);
    }
    return (string) null;
  }

  private string GetFieldValue(ComplianceDocumentReference reference)
  {
    return string.Join(", ", ComplianceReferenceTypeHelper.GetValueByKey(this.itemType, reference.Type), reference.ReferenceNumber);
  }

  private void CreateRedirectAction(PXCache cache)
  {
    PXButtonDelegate redirectionDelegate = this.GetRedirectionDelegate(cache);
    string str = $"{cache.GetItemType().Name}${((PXEventSubscriberAttribute) this)._FieldName}$Link";
    cache.Graph.Actions[str] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(ComplianceDocumentRefNoteAttribute.GetDacOfPrimaryView(cache)), (object) cache.Graph, (object) str, (object) redirectionDelegate, (object) ComplianceDocumentRefNoteAttribute.GetEventSubscriberAttributes());
    cache.Graph.Actions[str].SetVisible(false);
  }

  private static Type GetDacOfPrimaryView(PXCache cache)
  {
    return !((Dictionary<string, PXView>) cache.Graph.Views).ContainsKey(cache.Graph.PrimaryView) ? cache.BqlTable : cache.Graph.Views[cache.Graph.PrimaryView].GetItemType();
  }

  private static PXEventSubscriberAttribute[] GetEventSubscriberAttributes()
  {
    return new PXEventSubscriberAttribute[1]
    {
      (PXEventSubscriberAttribute) new PXUIFieldAttribute()
      {
        MapEnableRights = (PXCacheRights) 1
      }
    };
  }

  private string GetFieldDisplayName(string column)
  {
    PropertyInfo property = this.itemType.GetProperty(column);
    PXUIFieldAttribute pxuiFieldAttribute = (object) property != null ? (PXUIFieldAttribute) ((IEnumerable<object>) property.GetCustomAttributes(typeof (PXUIFieldAttribute), false)).FirstOrDefault<object>() : (PXUIFieldAttribute) (object) null;
    return pxuiFieldAttribute == null ? column : pxuiFieldAttribute.DisplayName;
  }

  private ComplianceDocumentReference InsertComplianceDocumentReference(
    PXCache cache,
    PXFieldUpdatingEventArgs args,
    Guid? noteId)
  {
    string[] strArray = ((string) args.NewValue).Split(',');
    string str = strArray[0].Trim();
    string refNumber = strArray[1].Trim();
    return this.InsertComplianceDocumentReference(cache.Graph, ComplianceReferenceTypeHelper.GetKeyByValue(this.itemType, str), refNumber, noteId);
  }

  private void TryDeleteReference(PXCache cache, Guid? referenceId)
  {
    if (!referenceId.HasValue || !(referenceId.Value != Guid.Empty))
      return;
    ComplianceDocumentReference documentReference = ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(cache.Graph, referenceId);
    if (documentReference == null)
      return;
    cache.Graph.Caches[typeof (ComplianceDocumentReference)].Delete((object) documentReference);
  }

  private void DeleteExistingReference(PXCache cache, ComplianceDocument doc)
  {
    Guid? referenceId = (Guid?) cache.GetValue((object) doc, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    this.TryDeleteReference(cache, referenceId);
  }

  private ComplianceDocumentReference InsertComplianceDocumentReference(
    PXGraph graph,
    string docType,
    string refNumber,
    Guid? noteId)
  {
    ComplianceDocumentReference documentReference = new ComplianceDocumentReference()
    {
      ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid()),
      Type = docType,
      ReferenceNumber = refNumber,
      RefNoteId = noteId
    };
    return (ComplianceDocumentReference) graph.Caches[typeof (ComplianceDocumentReference)].Insert((object) documentReference);
  }

  public void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    PXCache cach = cache.Graph.Caches[typeof (ComplianceDocumentReference)];
    if (e.TranStatus == null)
    {
      if (!(e.Row is ComplianceDocument))
        return;
      Guid? referenceId = (Guid?) cache.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      if (!referenceId.HasValue || !(referenceId.Value != Guid.Empty))
        return;
      ComplianceDocumentReference documentReference1 = ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(cache.Graph, referenceId);
      if (documentReference1 != null)
      {
        ComplianceDocumentReference documentReference2 = (ComplianceDocumentReference) cach.Locate((object) documentReference1);
        if (documentReference2 == null)
          return;
        if (cach.GetStatus((object) documentReference2) == 2)
        {
          cach.PersistInserted((object) documentReference2);
        }
        else
        {
          if (cach.GetStatus((object) documentReference2) != 1)
            return;
          cach.PersistUpdated((object) documentReference2);
        }
      }
      else
      {
        ComplianceDocumentReference documentReference3 = (ComplianceDocumentReference) cach.Locate((object) new ComplianceDocumentReference()
        {
          ComplianceDocumentReferenceId = referenceId
        });
        if (documentReference3 == null || cach.GetStatus((object) documentReference3) != 3)
          return;
        cach.PersistDeleted((object) documentReference3);
      }
    }
    else
    {
      if (e.TranStatus != 2 && e.TranStatus != 1)
        return;
      cach.Persisted(e.TranStatus == 2);
    }
  }

  public void RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    this.DeleteExistingReference(cache, (ComplianceDocument) e.Row);
  }
}
