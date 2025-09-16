// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorWorkflowStageAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorWorkflowStageAttribute : PXSelectorAttribute
{
  public FSSelectorWorkflowStageAttribute(Type currentSrvOrdType)
    : base(BqlCommand.Compose(new Type[9]
    {
      typeof (Search2<,,,>),
      typeof (FSWFStage.wFStageID),
      typeof (InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdTypeID, Equal<FSWFStage.wFID>>>),
      typeof (Where<,>),
      typeof (FSSrvOrdType.srvOrdType),
      typeof (Equal<>),
      typeof (Current<>),
      currentSrvOrdType,
      typeof (OrderBy<Asc<FSWFStage.parentWFStageID, Asc<FSWFStage.sortOrder>>>)
    }))
  {
    this.SubstituteKey = typeof (FSWFStage.wFStageCD);
    this.DescriptionField = typeof (FSWFStage.descr);
  }
}
