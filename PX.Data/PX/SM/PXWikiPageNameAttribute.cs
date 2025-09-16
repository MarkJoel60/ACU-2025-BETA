// Decompiled with JetBrains decompiler
// Type: PX.SM.PXWikiPageNameAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

public sealed class PXWikiPageNameAttribute : PXEventSubscriberAttribute, IPXRowSelectedSubscriber
{
  private readonly System.Type _pageBqlField;

  public PXWikiPageNameAttribute(System.Type pageBqlField)
  {
    this._pageBqlField = !(pageBqlField == (System.Type) null) ? pageBqlField : throw new ArgumentNullException("wikiBqlField");
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    object obj = (object) null;
    PXResultset<SimpleWikiPage> pxResultset = PXSelectBase<SimpleWikiPage, PXSelect<SimpleWikiPage, Where<SimpleWikiPage.pageID, Equal<Required<SimpleWikiPage.pageID>>, And<Where<SimpleWikiPage.name, Like<GenTemplateLeftLike>, Or<SimpleWikiPage.name, Like<TemplateLeftLike>, Or<SimpleWikiPage.name, Like<ContainerTemplateLeftLike>>>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, sender.GetValue(e.Row, sender.GetField(this._pageBqlField)));
    if (pxResultset != null && pxResultset.Count > 0)
      obj = (object) ((SimpleWikiPage) pxResultset[0]).Name;
    sender.SetValue(e.Row, this._FieldOrdinal, obj);
  }
}
