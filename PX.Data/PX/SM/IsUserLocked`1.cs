// Decompiled with JetBrains decompiler
// Type: PX.SM.IsUserLocked`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class IsUserLocked<Date> : BqlFormulaEvaluator<Date> where Date : IBqlOperand
{
  public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
  {
    System.DateTime? par = (System.DateTime?) pars[typeof (Date)];
    return (object) (bool) (!par.HasValue ? 0 : (PXTimeZoneInfo.ConvertTimeToUtc(par.Value, LocaleInfo.GetTimeZone()) > SitePolicy.AccountLockoutTime ? 1 : 0));
  }
}
