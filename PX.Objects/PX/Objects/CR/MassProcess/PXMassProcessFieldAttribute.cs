// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MassProcess.PXMassProcessFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.MassProcess;

public abstract class PXMassProcessFieldAttribute : PXEventSubscriberAttribute
{
  private System.Type _searchCommand;

  public System.Type SearchCommand
  {
    get => this._searchCommand;
    set
    {
      if (value != (System.Type) null && !typeof (BqlCommand).IsAssignableFrom(value))
        throw new ArgumentException($"Type '{MainTools.GetLongName(value)}' must inherite '{MainTools.GetLongName(typeof (BqlCommand))}' type.", nameof (value));
      this._searchCommand = !(value != (System.Type) null) || typeof (IBqlSearch).IsAssignableFrom(value) ? value : throw new ArgumentException($"Type '{MainTools.GetLongName(value)}' must implement interface '{MainTools.GetLongName(typeof (IBqlSearch))}'.", nameof (value));
    }
  }
}
