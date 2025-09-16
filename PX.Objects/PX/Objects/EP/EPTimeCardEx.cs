// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimeCardEx
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public class EPTimeCardEx : EPTimeCard
{
  public new abstract class timeCardCD : BqlType<IBqlString, string>.Field<
  #nullable disable
  EPTimeCardEx.timeCardCD>
  {
  }

  public new abstract class origTimeCardCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCardEx.origTimeCardCD>
  {
  }
}
