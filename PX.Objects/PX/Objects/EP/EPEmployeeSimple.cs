// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEmployeeSimple
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Employee")]
[PXHidden]
[Serializable]
public sealed class EPEmployeeSimple : EPEmployee
{
  public new abstract class bAccountID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeSimple.bAccountID>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeSimple.defContactID>
  {
  }

  public new abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployeeSimple.userID>
  {
  }
}
