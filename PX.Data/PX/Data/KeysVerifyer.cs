// Decompiled with JetBrains decompiler
// Type: PX.Data.KeysVerifyer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
internal sealed class KeysVerifyer
{
  private HashSet<string> keys = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public void Check(System.Type table)
  {
    if (this.keys.Count > 0)
      throw new PXException("An attempt of an update of the whole table {0} was detected because of missed key values.", new object[1]
      {
        (object) table.Name
      });
  }

  public KeysVerifyer(PXCache sender)
  {
    for (int index = 0; index < sender.Keys.Count; ++index)
      this.keys.Add(sender.Keys[index]);
  }

  public void ExcludeField(string name) => this.keys.Remove(name);
}
