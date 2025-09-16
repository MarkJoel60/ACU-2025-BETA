// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ActivitySource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class ActivitySource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, INotable
{
  [PXGuid]
  [PXNote]
  public Guid? NoteID { get; set; }

  public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  ActivitySource.noteID>
  {
  }
}
