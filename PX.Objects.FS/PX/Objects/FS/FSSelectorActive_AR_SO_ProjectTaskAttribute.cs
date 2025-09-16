// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorActive_AR_SO_ProjectTaskAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorActive_AR_SO_ProjectTaskAttribute : PXSelectorAttribute
{
  public FSSelectorActive_AR_SO_ProjectTaskAttribute(Type whereType)
    : this(whereType, typeof (On<FSSrvOrdType.srvOrdType, Equal<Current<FSSrvOrdType.srvOrdType>>>))
  {
  }

  public FSSelectorActive_AR_SO_ProjectTaskAttribute(Type whereType, Type srvOrdType)
    : base(BqlCommand.Compose(new Type[8]
    {
      typeof (Search2<,,>),
      typeof (PMTask.taskID),
      typeof (InnerJoin<,>),
      typeof (FSSrvOrdType),
      srvOrdType,
      typeof (Where2<,>),
      whereType,
      typeof (And<PMTask.isCancelled, Equal<False>, And<PMTask.isCompleted, Equal<False>, And2<Where<FSSrvOrdType.enableINPosting, Equal<False>, Or<PMTask.visibleInIN, Equal<True>>>, And<Where2<Where<FSSrvOrdType.postTo, Equal<FSPostTo.None>, Or<FSSrvOrdType.postTo, Equal<FSPostTo.Projects>>>, Or<Where2<Where<FSSrvOrdType.postTo, Equal<FSPostTo.Accounts_Receivable_Module>, And<Where<PMTask.visibleInAR, Equal<True>>>>, Or<Where2<Where<FSSrvOrdType.postTo, Equal<FSPostTo.Sales_Order_Module>, Or<FSSrvOrdType.postTo, Equal<FSPostTo.Sales_Order_Invoice>>>, And<Where<PMTask.visibleInSO, Equal<True>>>>>>>>>>>>)
    }), new Type[2]
    {
      typeof (PMTask.taskCD),
      typeof (PMTask.description)
    })
  {
    this.SubstituteKey = typeof (PMTask.taskCD);
    this.DescriptionField = typeof (PMTask.description);
    this.DirtyRead = true;
  }
}
