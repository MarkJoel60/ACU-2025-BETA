// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.DAC.BaseCache
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CN.Common.DAC;

[Serializable]
public class BaseCache : PXBqlTable, INotable
{
  [PXDBTimestamp]
  public virtual byte[] Tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedById { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenId { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedById { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenId { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }
}
