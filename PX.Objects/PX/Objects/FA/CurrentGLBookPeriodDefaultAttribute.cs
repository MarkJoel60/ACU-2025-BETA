// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.CurrentGLBookPeriodDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.FA;

public class CurrentGLBookPeriodDefaultAttribute : PXDefaultAttribute
{
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    base.FieldDefaulting(sender, e);
    FABookPeriod faBookPeriod = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelectJoin<FABookPeriod, LeftJoin<FABook, On<FABookPeriod.bookID, Equal<FABook.bookID>>>, Where<FABookPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>, And<FABookPeriod.endDate, Greater<Current<AccessInfo.businessDate>>, And<FABook.updateGL, Equal<True>, And<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>>>.Config>.SelectSingleBound(sender.Graph, new object[0], Array.Empty<object>()));
    if (faBookPeriod == null)
      return;
    e.NewValue = (object) FinPeriodIDFormattingAttribute.FormatForDisplay(faBookPeriod.FinPeriodID);
  }
}
