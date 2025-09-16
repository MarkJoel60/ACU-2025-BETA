// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWarnCancel`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXWarnCancel<TNode> : PXCancel<TNode> where TNode : class, IBqlTable, new()
{
  public PXWarnCancel(PXGraph graph, string name)
    : base(graph, name)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is PXButtonAttribute pxButtonAttribute)
        pxButtonAttribute.ConfirmationMessage = "Any unsaved changes will be discarded.";
    }
  }
}
