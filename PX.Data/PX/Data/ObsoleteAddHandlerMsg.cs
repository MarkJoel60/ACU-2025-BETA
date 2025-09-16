// Decompiled with JetBrains decompiler
// Type: PX.Data.ObsoleteAddHandlerMsg
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable enable
namespace PX.Data;

internal class ObsoleteAddHandlerMsg
{
  public const string AvoidManualSubscriptionOfAutoEventHandlers = "Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.";
}
