// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ActivityDetailsExt_Child_Actions`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable disable
namespace PX.Objects.CR.Extensions;

public abstract class ActivityDetailsExt_Child_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity, TPrimaryEntity_NoteID> : 
  ActivityDetailsExt_Actions<TActivityDetailsExt, TGraph, TPrimaryEntity, CRChildActivity, CRChildActivity.noteID>
  where TActivityDetailsExt : ActivityDetailsExt_Child<TGraph, TPrimaryEntity, TPrimaryEntity_NoteID>, IActivityDetailsExt
  where TGraph : PXGraph, new()
  where TPrimaryEntity : CRActivity, IBqlTable, INotable, new()
  where TPrimaryEntity_NoteID : IBqlField, IImplement<IBqlCastableTo<IBqlGuid>>
{
}
