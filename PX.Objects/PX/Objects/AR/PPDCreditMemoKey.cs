// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PPDCreditMemoKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.AR;

[Obsolete("PPDApplicationKeyis used instead of this class now")]
public class PPDCreditMemoKey
{
  private readonly FieldInfo[] _fields;
  public int? BranchID;
  public int? CustomerID;
  public int? CustomerLocationID;
  public string CuryID;
  public Decimal? CuryRate;
  public int? ARAccountID;
  public int? ARSubID;
  public string TaxZoneID;

  public PPDCreditMemoKey() => this._fields = this.GetType().GetFields();

  public override bool Equals(object obj)
  {
    return ((IEnumerable<FieldInfo>) this._fields).FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (field => !object.Equals(field.GetValue((object) this), field.GetValue(obj)))) == (FieldInfo) null;
  }

  public override int GetHashCode()
  {
    int hashCode = 17;
    EnumerableExtensions.ForEach<FieldInfo>((IEnumerable<FieldInfo>) this._fields, (Action<FieldInfo>) (field => hashCode = hashCode * 23 + field.GetValue((object) this).GetHashCode()));
    return hashCode;
  }
}
