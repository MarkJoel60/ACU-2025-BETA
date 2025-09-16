// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditValues
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
public class AUAuditValues : AuditHistory, IBqlTable, IBqlTableSystemDataStorage
{
  public 
  #nullable disable
  object Row;

  [PXString]
  [PXUIField(DisplayName = "User Name", Visibility = PXUIVisibility.Visible)]
  public virtual string UserName { get; set; }

  [PXDBLong(IsKey = true)]
  [PXUIField(DisplayName = "Change ID", Visible = false)]
  public override long? ChangeID { get; set; }

  public abstract class userName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditValues.userName>
  {
  }

  public new abstract class changeID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AUAuditValues.changeID>
  {
  }
}
