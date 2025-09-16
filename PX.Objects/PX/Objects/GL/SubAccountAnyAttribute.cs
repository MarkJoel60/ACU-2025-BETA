// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SubAccountAnyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Version of SubAccountAttribute wich allow inactive Subaccounts as well
/// </summary>
[PXRestrictor(typeof (Where<True, Equal<True>>), "Subaccount {0} is inactive.", new Type[] {}, ReplaceInherited = true)]
public class SubAccountAnyAttribute : SubAccountAttribute
{
  public SubAccountAnyAttribute()
  {
  }

  public SubAccountAnyAttribute(Type accountType)
    : base(accountType)
  {
  }
}
