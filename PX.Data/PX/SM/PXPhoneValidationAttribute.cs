// Decompiled with JetBrains decompiler
// Type: PX.SM.PXPhoneValidationAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

/// <summary>Validates phone number field.</summary>
/// <example>
/// <code title="" description="" lang="neutral"></code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class PXPhoneValidationAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected string _mask;

  public PXPhoneValidationAttribute(string mask) => this._mask = mask;

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(), new int?(), this._mask, (string[]) null, (string[]) null, new bool?(), (string) null);
  }
}
