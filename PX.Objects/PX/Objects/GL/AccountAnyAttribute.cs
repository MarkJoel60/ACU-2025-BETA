// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountAnyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Represents Account Field
/// The Selector will return all accounts.
/// This attribute also tracks currency (which is supplied as curyField parameter) for the Account and
/// raises an error in case Denominated GL Account currency is different from transaction currency.
/// </summary>
[PXRestrictor(typeof (Where<True, Equal<True>>), "Account is inactive.", new Type[] {}, ReplaceInherited = true)]
public class AccountAnyAttribute : AccountAttribute
{
  public AccountAnyAttribute()
    : base((Type) null)
  {
    this.Filterable = true;
  }

  public AccountAnyAttribute(Type branchID, Type SearchType)
    : base(branchID, SearchType)
  {
    this.Filterable = true;
  }

  public override void Verify(PXCache sender, Account item, object row)
  {
  }
}
