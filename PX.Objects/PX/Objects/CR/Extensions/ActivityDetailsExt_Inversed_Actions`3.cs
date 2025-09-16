// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ActivityDetailsExt_Inversed_Actions`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR.Extensions;

public abstract class ActivityDetailsExt_Inversed_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity> : 
  ActivityDetailsExt_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity, PMCRActivity, PMCRActivity.noteID>
  where TActivityDetailsExt : ActivityDetailsExt_Inversed<TGraph, TPrimaryEntity>, IActivityDetailsExt
  where TGraph : PXGraph, new()
  where TPrimaryEntity : class, IBqlTable, INotable, new()
{
}
