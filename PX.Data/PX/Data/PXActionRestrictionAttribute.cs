// Decompiled with JetBrains decompiler
// Type: PX.Data.PXActionRestrictionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
public class PXActionRestrictionAttribute : PXEventSubscriberAttribute
{
  public virtual IBqlCreator Where { get; protected set; }

  public virtual string Message { get; protected set; }

  public PXActionRestrictionAttribute(System.Type where, string message = null)
  {
    if (where == (System.Type) null)
      throw new PXArgumentException(nameof (where), "The argument cannot be null.");
    this.Where = typeof (IBqlWhere).IsAssignableFrom(where) ? PXFormulaAttribute.InitFormula(BqlCommand.MakeGenericType(typeof (Switch<,>), typeof (Case<,>), where, typeof (True), typeof (False))) : throw new PXArgumentException(nameof (where), "An invalid argument has been specified.");
    this.Message = message;
  }

  public virtual void Verify(PXAdapter adapter)
  {
    foreach (object obj1 in adapter.Get())
    {
      bool? result = new bool?(false);
      object obj2 = (object) null;
      BqlFormula.Verify(adapter.View.Cache, obj1, this.Where, ref result, ref obj2);
      bool? nullable = obj2 as bool?;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        throw string.IsNullOrEmpty(this.Message) ? (PXException) new PXActionDisabledException(adapter.Menu) : new PXException(this.Message);
    }
  }
}
