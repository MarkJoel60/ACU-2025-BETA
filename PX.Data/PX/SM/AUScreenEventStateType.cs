// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenEventStateType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUScreenEventStateType
{
  public const 
  #nullable disable
  string StartState = "S";
  public const string EndState = "E";
  public const string EventSubscriberCriterias = "C";

  public class startState : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenEventStateType.startState>
  {
    public startState()
      : base("S")
    {
    }
  }

  public class endState : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenEventStateType.endState>
  {
    public endState()
      : base("E")
    {
    }
  }

  public class eventSubscriberCriterias : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUScreenEventStateType.eventSubscriberCriterias>
  {
    public eventSubscriberCriterias()
      : base("C")
    {
    }
  }
}
