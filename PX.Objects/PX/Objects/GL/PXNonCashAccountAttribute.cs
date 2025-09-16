// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PXNonCashAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.GL;

[PXRestrictor(typeof (Where<Account.curyID, IsNull, And<Account.isCashAccount, Equal<boolFalse>>>), "Cash account or denominated account cannot be specified here.", new Type[] {})]
public class PXNonCashAccountAttribute : AccountAttribute
{
}
