// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaAutoNumberAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.PM;

public class ProformaAutoNumberAttribute : AutoNumberAttribute
{
  public ProformaAutoNumberAttribute()
    : base(typeof (PMSetup.proformaNumbering), typeof (AccessInfo.businessDate))
  {
  }

  public bool Disable { get; set; }

  public static void DisableAutonumbiring(PXCache cache)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly<PMProforma.refNbr>())
    {
      if (subscriberAttribute is ProformaAutoNumberAttribute)
      {
        ((ProformaAutoNumberAttribute) subscriberAttribute).Disable = true;
        ((AutoNumberAttribute) subscriberAttribute).UserNumbering = new bool?(true);
      }
    }
  }

  protected override string GetNewNumberSymbol(string numberingID)
  {
    return this.Disable ? this.NullString : base.GetNewNumberSymbol(numberingID);
  }
}
