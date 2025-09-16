// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AllocationPMTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
[PXHidden]
[Serializable]
public class AllocationPMTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected long? _TranID;

  [PXDBLongIdentity(IsKey = true)]
  public virtual long? TranID { get; set; }

  public abstract class tranID : BqlType<IBqlLong, long>.Field<
  #nullable disable
  AllocationPMTran.tranID>
  {
  }
}
