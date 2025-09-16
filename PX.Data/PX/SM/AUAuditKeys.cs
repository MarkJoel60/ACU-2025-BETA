// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditKeys
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
public class AUAuditKeys : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public 
  #nullable disable
  object Row;

  [PXInt(IsKey = true)]
  public int? Num { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string CombinedKey { get; set; }

  public abstract class num : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUAuditKeys.num>
  {
  }

  public abstract class combinedKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditKeys.combinedKey>
  {
  }
}
