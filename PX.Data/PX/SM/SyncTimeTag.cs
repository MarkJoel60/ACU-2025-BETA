// Decompiled with JetBrains decompiler
// Type: PX.SM.SyncTimeTag
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class SyncTimeTag : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? NoteID { get; set; }

  [PXDBDateAndTime(InputMask = "", DisplayMask = "g", UseSmallDateTime = false, UseTimeZone = false)]
  public virtual System.DateTime? TimeTag { get; set; }

  public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  SyncTimeTag.noteID>
  {
  }

  public abstract class timeTag : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  SyncTimeTag.timeTag>
  {
  }
}
