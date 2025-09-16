// Decompiled with JetBrains decompiler
// Type: PX.SM.PXIconsListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

#nullable disable
namespace PX.SM;

/// <exclude />
public class PXIconsListAttribute : PXImagesListAttribute
{
  private readonly string[] AvailableIcons;

  public PXIconsListAttribute()
    : this("main", "tree", "text")
  {
  }

  public PXIconsListAttribute(params string[] imageSets)
  {
    this.AvailableIcons = PXIconsListAttribute.FillAvailableIcons((IEnumerable<string>) imageSets);
    this.ExclusiveValues = false;
    this._AllowedImages = new string[this.AvailableIcons.Length];
    this._AllowedLabels = new string[this.AvailableIcons.Length];
    this._AllowedValues = new string[this.AvailableIcons.Length];
    this._NeutralAllowedLabels = this._AllowedLabels;
    for (int index = 0; index < this.AvailableIcons.Length; ++index)
      this._AllowedValues[index] = this._AllowedLabels[index] = this._AllowedImages[index] = this.AvailableIcons[index];
  }

  private static string[] FillAvailableIcons(IEnumerable<string> imageSets)
  {
    List<string> stringList = new List<string>();
    foreach (string imageSet in imageSets)
    {
      try
      {
        string applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;
        string[] array = ((IEnumerable<string>) File.ReadAllLines(Path.Combine(applicationPhysicalPath, "App_Themes", "Default", "00_Controls.css"))).Union<string>((IEnumerable<string>) File.ReadAllLines(Path.Combine(applicationPhysicalPath, "Content", "font-awesome.css"))).ToArray<string>();
        string str1 = $".{imageSet}-";
        foreach (string str2 in array)
        {
          int num1 = str2.IndexOf(str1, StringComparison.Ordinal);
          if (num1 >= 0 && !str2.Contains("icon") && !str2.Contains(",") && !str2.Contains(":hover") && !str2.Contains(":after"))
          {
            int num2 = str2.IndexOf('{');
            string str3 = (num2 > 0 ? str2.Substring(num1 + str1.Length, num2 - num1 - str1.Length) : str2.Substring(num1 + str1.Length)).Replace(":before", "");
            string str4 = $"{imageSet}@{str3.TrimEnd()}";
            if (!stringList.Contains(str4))
              stringList.Add(str4);
          }
        }
      }
      catch
      {
      }
    }
    stringList.Sort();
    return stringList.ToArray();
  }
}
