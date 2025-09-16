// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Exceptions.FeatureIsDisabledException`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.Common.Exceptions;

public class FeatureIsDisabledException<TFeature> : PXException where TFeature : IBqlField
{
  public FeatureIsDisabledException()
    : base("The {0} feature is disabled.", new object[1]
    {
      (object) FeatureIsDisabledException<TFeature>.GetFeatureName(typeof (TFeature))
    })
  {
  }

  public FeatureIsDisabledException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  protected static string GetFeatureName(Type featureField)
  {
    Type itemType = BqlCommand.GetItemType(featureField);
    // ISSUE: explicit non-virtual call
    PropertyInfo propertyInfo = (object) itemType != null ? ((IEnumerable<PropertyInfo>) __nonvirtual (itemType.GetProperties())).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.Name.Equals(featureField.Name, StringComparison.OrdinalIgnoreCase))).FirstOrDefault<PropertyInfo>() : (PropertyInfo) null;
    return (((object) propertyInfo != null ? ((IEnumerable<object>) propertyInfo.GetCustomAttributes(typeof (FeatureAttribute), true)).FirstOrDefault<object>() : (object) null) is FeatureAttribute featureAttribute ? featureAttribute.DisplayName : (string) null) ?? propertyInfo?.Name ?? featureField.Name;
  }
}
