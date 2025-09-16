// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEntityNameAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXEntityNameAttribute : PXStringListAttribute
{
  private readonly System.Type refNoteID;
  private EntityHelper helper;

  public PXEntityNameAttribute(System.Type refNoteID) => this.refNoteID = refNoteID;

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.helper = new EntityHelper(sender.Graph);
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[this.refNoteID.DeclaringType];
    Guid? noteID = (Guid?) cach.GetValue(cach == sender ? e.Row : cach.Current, this.refNoteID.Name);
    e.ReturnValue = (object) this.helper.GetFriendlyEntityName(noteID);
  }
}
