// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.CustomComparer`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

public class CustomComparer<TDAC> : IEqualityComparer<TDAC> where TDAC : IBqlTable
{
  private Func<object, int> _getHashCodeDelegate;
  private Func<object, object, bool> _equalsDelegate;

  public CustomComparer(
    Func<object, int> getHashCodeDelegate,
    Func<object, object, bool> equalsDelegate)
  {
    if (getHashCodeDelegate == null)
      throw new ArgumentNullException(nameof (getHashCodeDelegate));
    if (equalsDelegate == null)
      throw new ArgumentNullException(nameof (equalsDelegate));
    this._getHashCodeDelegate = getHashCodeDelegate;
    this._equalsDelegate = equalsDelegate;
  }

  public bool Equals(TDAC x, TDAC y) => this._equalsDelegate((object) x, (object) y);

  public int GetHashCode(TDAC obj) => this._getHashCodeDelegate((object) obj);
}
