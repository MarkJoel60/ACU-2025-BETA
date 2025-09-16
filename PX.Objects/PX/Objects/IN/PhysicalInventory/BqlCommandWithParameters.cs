// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PhysicalInventory.BqlCommandWithParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.PhysicalInventory;

public class BqlCommandWithParameters
{
  public BqlCommand Command { get; set; }

  public List<object> JoinParameters { get; set; }

  public List<object> WhereParameters { get; set; }

  public BqlCommandWithParameters()
    : this((BqlCommand) null)
  {
  }

  public BqlCommandWithParameters(BqlCommand command)
  {
    this.Command = command;
    this.JoinParameters = new List<object>();
    this.WhereParameters = new List<object>();
  }

  public object[] GetParameters()
  {
    return this.JoinParameters.Union<object>((IEnumerable<object>) this.WhereParameters).ToArray<object>();
  }
}
