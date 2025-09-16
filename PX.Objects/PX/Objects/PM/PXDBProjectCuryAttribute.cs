// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PXDBProjectCuryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM.Extensions;

#nullable disable
namespace PX.Objects.PM;

public class PXDBProjectCuryAttribute : PXDBDecimalAttribute
{
  public PXDBProjectCuryAttribute()
    : base(typeof (Search2<Currency.decimalPlaces, InnerJoin<PMProject, On<PMProject.curyID, Equal<Currency.curyID>>>, Where<PMProject.contractID, Equal<Current<PMTran.projectID>>>>))
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    base.CacheAttached(sender);
  }
}
