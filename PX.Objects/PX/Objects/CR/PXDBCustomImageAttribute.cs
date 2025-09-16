// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXDBCustomImageAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;

#nullable disable
namespace PX.Objects.CR;

public class PXDBCustomImageAttribute : PXDBImageAttribute
{
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    ((PXDBStringAttribute) this).CommandPreparing(sender, e);
    if ((e.Operation & 3) == 2 || (e.Operation & 3) == 1 || (e.Operation & 3) == 3)
    {
      e.ExcludeFromInsertUpdate();
      e.Expr = (SQLExpression) null;
    }
    else
      e.Expr = SQLExpression.Null();
  }
}
