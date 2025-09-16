// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPActivityComplite
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class EPActivityComplite : PMTimeActivity
{
  public new abstract class approvalStatus : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    EPActivityComplite.approvalStatus>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPActivityComplite.noteID>
  {
  }
}
