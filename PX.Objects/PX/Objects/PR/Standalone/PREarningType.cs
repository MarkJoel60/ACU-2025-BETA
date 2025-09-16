// Decompiled with JetBrains decompiler
// Type: PX.Objects.PR.Standalone.PREarningType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PR.Standalone;

[PXCacheName("Payroll Earning Type")]
[Serializable]
public class PREarningType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual 
  #nullable disable
  string TypeCD { get; set; }

  public abstract class typeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PREarningType.typeCD>
  {
  }
}
