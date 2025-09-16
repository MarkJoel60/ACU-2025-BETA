// Decompiled with JetBrains decompiler
// Type: PX.SM.Standalone.EMailAccount
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM.Standalone;

[Serializable]
public class EMailAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? EmailAccountID { get; set; }

  [PXRSACryptString]
  public virtual string Password { get; set; }

  public abstract class emailAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class password : IBqlField, IBqlOperand
  {
  }
}
