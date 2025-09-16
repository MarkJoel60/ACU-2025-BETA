// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.CommonAttributeColumnCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Microsoft.CSharp.RuntimeBinder;
using PX.Api;
using PX.Common;
using PX.CS;
using PX.Data;
using PX.Objects.CN.Common.Descriptor.Attributes;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GDPR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.CN.Common.Services;

public class CommonAttributeColumnCreator
{
  private readonly PXGraph graph;
  private readonly PXSelectBase<CSAnswers> answers;
  private readonly PXSelectBase<CSAttributeGroup> attributeGroups;
  private readonly EntityHelper entityHelper;
  private PXCache documentCache;
  private bool areControlsEnabled;

  public CommonAttributeColumnCreator(PXGraph graph, PXSelectBase<CSAttributeGroup> attributeGroups)
  {
    this.graph = graph;
    this.attributeGroups = attributeGroups;
    this.entityHelper = new EntityHelper(graph);
    this.answers = (PXSelectBase<CSAnswers>) new PXSelect<CSAnswers>(graph);
  }

  public void GenerateColumns(
    PXCache cache,
    string documentsView,
    string answerView,
    bool areColumnsEnabled = true)
  {
    this.areControlsEnabled = areColumnsEnabled;
    this.documentCache = cache;
    CommonAttributeColumnCreator.RemoveUnboundFields(cache);
    EnumerableExtensions.ForEach<CSAttributeGroup>((IEnumerable<CSAttributeGroup>) this.attributeGroups.Select(Array.Empty<object>()).FirstTableItems.OrderBy<CSAttributeGroup, short?>((Func<CSAttributeGroup, short?>) (x => x.SortOrder)), (Action<CSAttributeGroup>) (attributeGroup => this.ProcessAttributeGroup(cache, documentsView, attributeGroup)));
    this.AddRowEventHandlers(documentsView, answerView);
  }

  private void ProcessAttributeGroup(
    PXCache cache,
    string documentsView,
    CSAttributeGroup attributeGroup)
  {
    if (cache.Fields.Contains(attributeGroup.AttributeID.Trim()))
      return;
    this.AddFieldsAndFieldEventHandlers(cache, documentsView, attributeGroup);
  }

  private void AddFieldsAndFieldEventHandlers(
    PXCache cache,
    string documentsView,
    CSAttributeGroup attributeGroup)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CommonAttributeColumnCreator.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new CommonAttributeColumnCreator.\u003C\u003Ec__DisplayClass9_0()
    {
      \u003C\u003E4__this = this,
      attributeGroup = attributeGroup
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.attributeId = cDisplayClass90.attributeGroup.AttributeID.Trim();
    // ISSUE: reference to a compiler-generated field
    cache.Fields.Add(cDisplayClass90.attributeId);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.graph.FieldSelecting.AddHandler(documentsView, cDisplayClass90.attributeId, new PXFieldSelecting((object) cDisplayClass90, __methodptr(\u003CAddFieldsAndFieldEventHandlers\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.graph.FieldUpdating.AddHandler(documentsView, cDisplayClass90.attributeId, new PXFieldUpdating((object) cDisplayClass90, __methodptr(\u003CAddFieldsAndFieldEventHandlers\u003Eb__1)));
  }

  private void AddRowEventHandlers(string documentsView, string answerView)
  {
    // ISSUE: method pointer
    this.graph.RowPersisting.AddHandler(answerView, new PXRowPersisting((object) this, __methodptr(ValidateAnswer)));
    // ISSUE: method pointer
    this.graph.RowPersisting.AddHandler(documentsView, new PXRowPersisting((object) this, __methodptr(ValidateDocuments)));
    // ISSUE: method pointer
    this.graph.RowDeleted.AddHandler(documentsView, new PXRowDeleted((object) this, __methodptr(DeleteAnswers)));
    // ISSUE: method pointer
    this.graph.RowInserting.AddHandler(documentsView, new PXRowInserting((object) this, __methodptr(\u003CAddRowEventHandlers\u003Eb__10_0)));
    // ISSUE: method pointer
    this.graph.RowUpdating.AddHandler(documentsView, new PXRowUpdating((object) this, __methodptr(\u003CAddRowEventHandlers\u003Eb__10_1)));
  }

  private void CreateControl(
    PXFieldSelectingEventArgs arguments,
    CSAttributeGroup attributeGroup,
    string attributeId)
  {
    CSAttribute attribute1 = this.GetAttribute(attributeGroup.AttributeID);
    IEnumerable<CSAttributeDetail> attributeDetails = this.GetAttributeDetails(attributeGroup.AttributeID);
    KeyValueHelper.Attribute attribute2 = attribute1 == null ? (KeyValueHelper.Attribute) null : new KeyValueHelper.Attribute(new CSAttribute().PopulateFrom<CSAttribute, CSAttribute>(attribute1), attributeDetails.Select<CSAttributeDetail, CSAttributeDetail>((Func<CSAttributeDetail, CSAttributeDetail>) (o => new CSAttributeDetail().PopulateFrom<CSAttributeDetail, CSAttributeDetail>(o))));
    arguments.ReturnState = (object) KeyValueHelper.MakeFieldState(attribute2, "value", arguments.ReturnState, new int?(attributeGroup.Required.GetValueOrDefault() ? 1 : -1), (string) null, (string) null);
    this.ProcessReturnState(arguments, attributeId, attributeGroup);
  }

  private void ProcessReturnState(
    PXFieldSelectingEventArgs arguments,
    string attributeId,
    CSAttributeGroup attributeGroup)
  {
    if (!(arguments.ReturnState is PXFieldState returnState))
      return;
    if (arguments.Row != null)
      this.UpdateFieldStateForExistingDocument(attributeId, arguments.Row, returnState);
    returnState.SetFieldName(attributeId);
    returnState.Visible = attributeGroup.IsActive.GetValueOrDefault();
    returnState.Visibility = (PXUIVisibility) 3;
    returnState.DisplayName = attributeGroup.Description.Trim();
    returnState.Enabled = this.areControlsEnabled;
  }

  private void UpdateFieldStateForExistingDocument(
    string attributeId,
    object document,
    PXFieldState fieldState)
  {
    CSAnswers answer = this.GetAnswer(attributeId, document);
    if (answer == null)
      return;
    fieldState.Value = (object) answer.Value;
  }

  private void InsertOrUpdateAnswerValue(PXFieldUpdatingEventArgs arguments, CSAttributeGroup group)
  {
    if (arguments.Row == null)
      return;
    CSAnswers answer = this.GetAnswer(group.AttributeID, arguments.Row);
    if (answer != null)
      this.UpdateAnswer(arguments.NewValue, answer);
    else
      this.InsertAnswer(group, arguments.Row, arguments.NewValue.ToString());
  }

  private void ValidateAnswer(PXCache cache, PXRowPersistingEventArgs arguments)
  {
    if (!(arguments.Row is CSAnswers row) || arguments.Operation != 2 && arguments.Operation != 1 || !this.attributeGroups.Search<CSAttributeGroup.attributeID>((object) row.AttributeID, Array.Empty<object>()).FirstTableItems.Single<CSAttributeGroup>().Required.GetValueOrDefault() || !string.IsNullOrEmpty(row.Value))
      return;
    this.RaiseExceptionForEmptyAttributeValue(row);
  }

  private void RaiseExceptionForEmptyAttributeValue(CSAnswers attribute)
  {
    string str = $"Error: '{attribute.AttributeID}' cannot be empty.";
    object entityRow = this.entityHelper.GetEntityRow(this.documentCache.GetItemType(), attribute.RefNoteID);
    this.documentCache.RaiseExceptionHandling(((IEnumerable<PropertyInfo>) entityRow.GetType().GetProperties()).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (x => x.GetCustomAttributes(typeof (UiInformationFieldAttribute)).Any<System.Attribute>()))?.Name, entityRow, (object) true, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 5));
  }

  private void DeleteAnswers(PXCache cache, PXRowDeletedEventArgs arguments)
  {
    object row = arguments.Row;
    if (row == null)
      return;
    Guid? entityNoteId = this.entityHelper.GetEntityNoteID(row);
    if (!entityNoteId.HasValue)
      return;
    ((PXSelectBase) this.answers).Cache.DeleteAll(this.GetAnswersToDelete(row, entityNoteId).Select<CSAnswers, object>((Func<CSAnswers, object>) (x => this.CastAnswerToViewType(x))));
  }

  private IEnumerable<CSAnswers> GetAnswersToDelete(object document, Guid? noteId)
  {
    PXCache cach = this.graph.Caches[typeof (CSAnswers)];
    PXEntryStatus status = this.graph.Caches[document.GetType()].GetStatus(document);
    return status != 2 && status != 4 ? this.GetExistingAnswers(noteId) : CommonAttributeColumnCreator.GetInsertedAnswers(cach, noteId);
  }

  private void ValidateDocuments(PXCache cache, PXRowPersistingEventArgs arguments)
  {
    if (arguments.Operation != 2 && arguments.Operation != 1 || arguments.Row == null)
      return;
    List<string> stringList = new List<string>();
    this.ValidateDocumentAttributes(arguments.Row, stringList);
    if (stringList.Count > 0)
      throw new PXException("There are empty required attributes: {0}", new object[1]
      {
        (object) string.Join(", ", stringList.Select<string, string>((Func<string, string>) (s => $"'{s}'")))
      });
  }

  private void ValidateDocumentAttributes(object document, List<string> emptyRequired)
  {
    Guid? noteId = this.entityHelper.GetEntityNoteID(document);
    EnumerableExtensions.ForEach<CSAnswers>(EnumerableEx.Select<CSAnswers>(((PXSelectBase) this.answers).Cache.Cached).Where<CSAnswers>((Func<CSAnswers, bool>) (x =>
    {
      Guid? refNoteId = x.RefNoteID;
      Guid? nullable = noteId;
      if (refNoteId.HasValue != nullable.HasValue)
        return false;
      return !refNoteId.HasValue || refNoteId.GetValueOrDefault() == nullable.GetValueOrDefault();
    })), (Action<CSAnswers>) (attribute => this.ValidateDocumentAttribute(attribute, (ICollection<string>) emptyRequired, document)));
  }

  private void ValidateDocumentAttribute(
    CSAnswers attribute,
    ICollection<string> emptyRequired,
    object document)
  {
    if (!this.attributeGroups.Search<CSAttributeGroup.attributeID>((object) attribute.AttributeID, Array.Empty<object>()).FirstTableItems.Single<CSAttributeGroup>().Required.GetValueOrDefault() || !string.IsNullOrEmpty(attribute.Value))
      return;
    emptyRequired.Add(attribute.AttributeID);
    this.RaiseExceptionForDocumentAttribute(attribute, document);
  }

  private void RaiseExceptionForDocumentAttribute(CSAnswers attribute, object document)
  {
    string str = $"Error: '{attribute.AttributeID}' cannot be empty.";
    this.documentCache.RaiseExceptionHandling(((IEnumerable<PropertyInfo>) document.GetType().GetProperties()).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (x => x.GetCustomAttributes(typeof (UiInformationFieldAttribute)).Any<System.Attribute>()))?.Name, document, (object) true, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 5));
    PXUIFieldAttribute.SetError<CSAnswers.value>(((PXSelectBase) this.answers).Cache, (object) attribute, str);
  }

  private void InsertAnswers(object document)
  {
    if (document == null)
      return;
    EnumerableExtensions.ForEach<CSAttributeGroup>(this.attributeGroups.Select(Array.Empty<object>()).FirstTableItems, (Action<CSAttributeGroup>) (group => this.InsertAnswer(group, document)));
  }

  private void UpdateAnswer(object newValue, CSAnswers updatedAnswer)
  {
    updatedAnswer.Value = newValue?.ToString();
    if (((object) updatedAnswer).GetType() != ((PXSelectBase) this.answers).Cache.GetItemType())
    {
      object viewType = this.CastAnswerToViewType(updatedAnswer);
      // ISSUE: reference to a compiler-generated field
      if (CommonAttributeColumnCreator.\u003C\u003Eo__24.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CommonAttributeColumnCreator.\u003C\u003Eo__24.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Guid?, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "NoteId", typeof (CommonAttributeColumnCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = CommonAttributeColumnCreator.\u003C\u003Eo__24.\u003C\u003Ep__0.Target((CallSite) CommonAttributeColumnCreator.\u003C\u003Eo__24.\u003C\u003Ep__0, viewType, (Guid?) PXCacheEx.GetExtension<CSAnswersExt>((IBqlTable) updatedAnswer)?.NoteID);
      // ISSUE: reference to a compiler-generated field
      if (CommonAttributeColumnCreator.\u003C\u003Eo__24.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CommonAttributeColumnCreator.\u003C\u003Eo__24.\u003C\u003Ep__1 = CallSite<Action<CallSite, PXCache, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Update", (IEnumerable<Type>) null, typeof (CommonAttributeColumnCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CommonAttributeColumnCreator.\u003C\u003Eo__24.\u003C\u003Ep__1.Target((CallSite) CommonAttributeColumnCreator.\u003C\u003Eo__24.\u003C\u003Ep__1, ((PXSelectBase) this.answers).Cache, viewType);
    }
    ((PXSelectBase) this.answers).Cache.Update((object) updatedAnswer);
  }

  private void InsertAnswer(CSAttributeGroup group, object document, string value = null)
  {
    Guid? entityNoteId = this.entityHelper.GetEntityNoteID(document);
    if (this.GetAnswer(group.AttributeID, document) != null)
      return;
    object viewType = this.CastAnswerToViewType(CommonAttributeColumnCreator.CreateAnswer(group, value, entityNoteId));
    // ISSUE: reference to a compiler-generated field
    if (CommonAttributeColumnCreator.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CommonAttributeColumnCreator.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Action<CallSite, PXCache, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Insert", (IEnumerable<Type>) null, typeof (CommonAttributeColumnCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    CommonAttributeColumnCreator.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) CommonAttributeColumnCreator.\u003C\u003Eo__25.\u003C\u003Ep__0, ((PXSelectBase) this.answers).Cache, viewType);
  }

  private object CastAnswerToViewType(CSAnswers answer)
  {
    Type itemType = ((PXSelectBase) this.answers).Cache.GetItemType();
    return ((object) answer).Cast(itemType);
  }

  private CSAnswers GetAnswer(string attributeId, object document)
  {
    Guid? entityNoteId = this.entityHelper.GetEntityNoteID(document);
    return PXResultset<CSAnswers>.op_Implicit(this.answers.Search<CSAnswers.attributeID, CSAnswers.refNoteID>((object) attributeId, (object) entityNoteId, Array.Empty<object>()));
  }

  private IEnumerable<CSAttributeDetail> GetAttributeDetails(string attributeId)
  {
    return PXSelectBase<CSAttributeDetail, PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>, And<CSAttributeDetail.disabled, NotEqual<True>>>>.Config>.Select(this.graph, new object[1]
    {
      (object) attributeId
    }).FirstTableItems;
  }

  private CSAttribute GetAttribute(string attributeId)
  {
    return ((PXSelectBase<CSAttribute>) new PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Required<CSAttribute.attributeID>>>>(this.graph)).SelectSingle(new object[1]
    {
      (object) attributeId
    });
  }

  private static void RemoveUnboundFields(PXCache cache)
  {
    IEnumerable<string> customFieldsToRemove = CommonAttributeColumnCreator.GetCustomFieldsToRemove(cache);
    CommonAttributeColumnCreator.GetListFieldsToRemove(cache, customFieldsToRemove).ForEach((Action<string>) (x => cache.Fields.Remove(x)));
  }

  private static List<string> GetListFieldsToRemove(
    PXCache cache,
    IEnumerable<string> propertyNames)
  {
    return propertyNames.Aggregate<string, List<string>>(((IEnumerable<string>) cache.Fields).Where<string>((Func<string, bool>) (x => x.Contains("_"))).ToList<string>(), (Func<List<string>, string, List<string>>) ((current, propertyName) => EnumerableExtensions.Except<string>((IEnumerable<string>) current, propertyName + "_Description").ToList<string>()));
  }

  private static IEnumerable<string> GetCustomFieldsToRemove(PXCache cache)
  {
    return ((IEnumerable<PropertyInfo>) cache.GetItemType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (x => x.GetCustomAttributes(typeof (FieldDescriptionForDynamicColumnsAttribute)).Any<System.Attribute>())).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (x => x.Name));
  }

  private IEnumerable<CSAnswers> GetExistingAnswers(Guid? noteId)
  {
    return PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>>>.Config>.Select(this.graph, new object[1]
    {
      (object) noteId
    }).FirstTableItems;
  }

  private static CSAnswers CreateAnswer(CSAttributeGroup group, string value, Guid? noteId)
  {
    return new CSAnswers()
    {
      AttributeID = group.AttributeID,
      RefNoteID = noteId,
      Value = CommonAttributeColumnCreator.GetAnswerValue(group, value)
    };
  }

  private static string GetAnswerValue(CSAttributeGroup group, string value)
  {
    string str = group.ControlType.GetValueOrDefault() == 4 ? Convert.ToInt32(false).ToString() : group.DefaultValue;
    return value ?? str;
  }

  private static IEnumerable<CSAnswers> GetInsertedAnswers(PXCache cache, Guid? noteId)
  {
    return Enumerable.Cast<CSAnswers>(cache.Inserted).Where<CSAnswers>((Func<CSAnswers, bool>) (x =>
    {
      Guid? refNoteId = x.RefNoteID;
      Guid? nullable = noteId;
      if (refNoteId.HasValue != nullable.HasValue)
        return false;
      return !refNoteId.HasValue || refNoteId.GetValueOrDefault() == nullable.GetValueOrDefault();
    }));
  }
}
