// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MassProcess.PXDeduplicationSearchEmailFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Mail;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Mail;

#nullable disable
namespace PX.Objects.CR.MassProcess;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class PXDeduplicationSearchEmailFieldAttribute : PXDeduplicationSearchFieldAttribute
{
  public override object[] ConvertValue(object input, string transformationRule)
  {
    if (transformationRule != "SE")
      return input.SingleToArray<object>();
    IEnumerable<MailAddress> addresses;
    try
    {
      addresses = (IEnumerable<MailAddress>) EmailParser.ParseAddresses(input as string);
    }
    catch
    {
      return base.ConvertValue(input, transformationRule);
    }
    List<object> objectList = new List<object>();
    foreach (MailAddress mailAddress in addresses)
      objectList.Add((object) mailAddress.Address);
    return objectList.ToArray();
  }

  public PXDeduplicationSearchEmailFieldAttribute()
    : base()
  {
  }
}
