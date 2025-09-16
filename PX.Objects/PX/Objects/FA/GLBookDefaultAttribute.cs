// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.GLBookDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

public class GLBookDefaultAttribute : PXDefaultAttribute
{
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    base.FieldDefaulting(sender, e);
    FABook faBook = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.updateGL, Equal<True>>>.Config>.SelectSingleBound(sender.Graph, new object[0], Array.Empty<object>()));
    if (faBook == null)
      return;
    e.NewValue = (object) faBook.BookCode;
  }
}
