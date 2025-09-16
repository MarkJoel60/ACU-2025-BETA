// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRAttributeSourceList`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CR;

public class CRAttributeSourceList<TReference, TSourceField> : CRAttributeList<TReference>
  where TReference : class, IBqlTable, new()
  where TSourceField : IBqlField
{
  private object _AttributeSource;

  public CRAttributeSourceList(PXGraph graph)
    : base(graph)
  {
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldUpdated.AddHandler<TSourceField>(new PXFieldUpdated((object) this, __methodptr(ReferenceSourceFieldUpdated)));
  }

  protected object AttributeSource
  {
    get
    {
      PXCache<TReference> pxCache = GraphHelper.Caches<TReference>(((PXSelectBase) this)._Graph);
      string noteField = EntityHelper.GetNoteField(typeof (TReference));
      if (this._AttributeSource != null)
      {
        Guid? noteId = this.GetNoteId(this._AttributeSource);
        Guid? nullable = (Guid?) ((PXCache) pxCache).GetValue(((PXCache) pxCache).Current, noteField);
        if ((noteId.HasValue == nullable.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
          goto label_3;
      }
      this._AttributeSource = PXSelectorAttribute.Select<TSourceField>((PXCache) pxCache, ((PXCache) pxCache).Current);
label_3:
      return this._AttributeSource;
    }
  }

  protected override string GetDefaultAnswerValue(CRAttribute.AttributeExt attr)
  {
    if (this.AttributeSource == null)
      return base.GetDefaultAnswerValue(attr);
    Guid? noteId = this.GetNoteId(this.AttributeSource);
    foreach (PXResult<CSAnswers> pxResult in PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) noteId
    }))
    {
      CSAnswers csAnswers = PXResult<CSAnswers>.op_Implicit(pxResult);
      if (csAnswers.AttributeID == attr.ID && !string.IsNullOrEmpty(csAnswers.Value))
        return csAnswers.Value;
    }
    return base.GetDefaultAnswerValue(attr);
  }

  protected void ReferenceSourceFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.CopyAttributes(e.Row, this.AttributeSource);
  }
}
