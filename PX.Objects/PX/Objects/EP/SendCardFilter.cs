// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.SendCardFilter
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
public class SendCardFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Email")]
  [PXDefault]
  [PXDBEmail]
  public virtual 
  #nullable disable
  string Email { get; set; }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SendCardFilter.email>
  {
  }
}
