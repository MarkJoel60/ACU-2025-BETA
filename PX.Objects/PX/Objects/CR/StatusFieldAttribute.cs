// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.StatusFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.Cache;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public class StatusFieldAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected readonly System.Type _entityTypeField;

  protected string _entityTypeFieldName { get; set; }

  public StatusFieldAttribute(System.Type entityTypeField)
  {
    this._entityTypeField = entityTypeField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._entityTypeFieldName = sender.GetField(this._entityTypeField);
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null || !(sender.GetValue(e.Row, this._entityTypeFieldName) is string typeName))
      return;
    (string FieldName, CRRelationDetail Attribute) fieldWithAttribute = sender.Graph.Caches[this.GetRelatedEntityType(typeName)].GetField_WithAttribute<CRRelationDetail>();
    if (fieldWithAttribute.FieldName == null || fieldWithAttribute.Attribute.StatusFieldName == null)
      return;
    object stateExt = sender.Graph.Caches[this.GetRelatedEntityType(typeName)].GetStateExt((object) null, fieldWithAttribute.Attribute.StatusFieldName);
    e.ReturnState = stateExt;
    if (e.ReturnState is PXFieldState returnState)
      returnState.Enabled = false;
    e.ReturnValue = sender.GetValue(e.Row, this._FieldName);
  }

  protected System.Type GetRelatedEntityType(string typeName)
  {
    return PXBuildManager.GetType(typeName, false);
  }
}
