// Decompiled with JetBrains decompiler
// Type: PX.SM.PXScalableIconsListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

#nullable disable
namespace PX.SM;

public class PXScalableIconsListAttribute : PXImagesListAttribute
{
  private readonly string[] AvailableIcons;

  public PXScalableIconsListAttribute()
  {
    this.AvailableIcons = PXScalableIconsListAttribute.FillAvailableIcons();
    this.ExclusiveValues = false;
    this._AllowedImages = new string[this.AvailableIcons.Length];
    this._AllowedLabels = new string[this.AvailableIcons.Length];
    this._AllowedValues = new string[this.AvailableIcons.Length];
    this._NeutralAllowedLabels = this._AllowedLabels;
    for (int index = 0; index < this.AvailableIcons.Length; ++index)
    {
      this._AllowedValues[index] = this.AvailableIcons[index];
      this._AllowedLabels[index] = this.AvailableIcons[index].Replace('_', ' ');
      this._AllowedImages[index] = "fa@" + this.AvailableIcons[index];
    }
  }

  private static string[] FillAvailableIcons()
  {
    List<string> stringList = new List<string>();
    try
    {
      string[] strArray = File.ReadAllLines(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Content", "font-awesome.css"));
      string str1 = ".ac-";
      foreach (string str2 in strArray)
      {
        int num1 = str2.IndexOf(str1, StringComparison.Ordinal);
        if (num1 >= 0 && str2.Contains(":before"))
        {
          int num2 = str2.IndexOf(':');
          string str3 = str2.Substring(num1 + str1.Length, num2 - num1 - str1.Length);
          if (!stringList.Contains(str3))
            stringList.Add(str3);
        }
      }
    }
    catch
    {
    }
    stringList.Sort();
    return stringList.ToArray();
  }
}
