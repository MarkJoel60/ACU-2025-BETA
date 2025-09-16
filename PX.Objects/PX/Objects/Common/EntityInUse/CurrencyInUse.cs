// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.EntityInUse.CurrencyInUse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.EntityInUse;

[EntityInUseDBSlotOn]
[PXHidden]
[Serializable]
public class CurrencyInUse : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(5, IsKey = true, IsUnicode = true)]
  public virtual string CuryID { get; set; }

  public abstract class curyID : IBqlField, IBqlOperand
  {
  }
}
