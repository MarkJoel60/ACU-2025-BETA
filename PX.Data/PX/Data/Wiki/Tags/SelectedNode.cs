// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.SelectedNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Wiki.Tags;

[PXHidden]
public class SelectedNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXGuid]
  [PXUIField(Visible = false)]
  public virtual Guid? TagID { get; set; }

  public abstract class tagID : BqlType<IBqlGuid, Guid>.Field<SelectedNode.tagID>
  {
  }
}
