// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.ServiceManagerGraphRegistry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Api.Soap.Screen;

internal class ServiceManagerGraphRegistry : IGraphRegistry
{
  public IEnumerable<System.Type> All => ServiceManager.AllGraphsTypesNotCustomized;

  public IEnumerable<System.Type> Visible
  {
    get => this.All.Where<System.Type>((Func<System.Type, bool>) (t => !this.IsHidden(t)));
  }

  public IEnumerable<System.Type> Hidden
  {
    get => this.All.Where<System.Type>(new Func<System.Type, bool>(this.IsHidden));
  }

  private bool IsHidden(System.Type graphType)
  {
    return Attribute.IsDefined((MemberInfo) graphType, typeof (PXHiddenAttribute), true);
  }
}
