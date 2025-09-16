// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.CsAttributeValidatorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Collection;
using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class CsAttributeValidatorAttribute : PXEventSubscriberAttribute, IPXFieldVerifyingSubscriber
{
  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[(string) e.NewValue] == null)
      throw new PXSetPropertyException("'Attribute' cannot be found in the system.");
  }
}
