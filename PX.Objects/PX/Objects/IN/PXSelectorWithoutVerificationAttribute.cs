// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXSelectorWithoutVerificationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class PXSelectorWithoutVerificationAttribute : PXSelectorAttribute
{
  public PXSelectorWithoutVerificationAttribute(Type type)
    : base(type)
  {
  }

  public PXSelectorWithoutVerificationAttribute(Type type, params Type[] fieldList)
    : base(type, fieldList)
  {
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }
}
