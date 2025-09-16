// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.ServiceManagerDacRegistry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Metadata;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Api.Soap.Screen;

internal class ServiceManagerDacRegistry : IDacRegistry
{
  public IEnumerable<Type> All => ServiceManager.AllTables;

  public IEnumerable<Type> Visible => ServiceManager.Tables;

  public IEnumerable<Type> Hidden => ServiceManager.HiddenTables;
}
