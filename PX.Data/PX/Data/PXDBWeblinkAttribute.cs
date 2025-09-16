// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBWeblinkAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class PXDBWeblinkAttribute : PXDBStringAttribute, IPXFieldVerifyingSubscriber
{
  private static readonly Regex regex = new Regex("(http://|https://)?(www\\.)?[\\w-]+(\\.[\\w-]+)+(/[\\w-]+)*(\\.(html|htm|cgi|php|aspx|asp|\\w+))?(\\?(\\w+\\=[\\w%+.]+){1}(&\\w+\\=[\\w%+.]+)*)?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

  public PXDBWeblinkAttribute()
    : base((int) byte.MaxValue)
  {
    this.IsUnicode = true;
    this.IsFixed = false;
  }

  private static bool isPossibleURL(string url)
  {
    if (string.IsNullOrEmpty(url))
      return false;
    if (url.IndexOf("://", StringComparison.Ordinal) < 0)
      url = "http://" + url;
    Uri result;
    return Uri.TryCreate(url, UriKind.Absolute, out result) && (string.IsNullOrEmpty(result.Scheme) || result.Scheme.StartsWith("http", StringComparison.OrdinalIgnoreCase));
  }

  public new virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!e.ExternalCall && !sender.Graph.IsMobile)
      return;
    string newValue = e.NewValue as string;
    if (string.IsNullOrEmpty(newValue))
      return;
    if (newValue.Length < 4 || newValue.Length > (int) byte.MaxValue)
      throw new PXSetPropertyException("The link URL does not have the correct format.");
    if (sender.Graph.IsImport || sender.Graph.IsExport)
    {
      if (PXDBWeblinkAttribute.isPossibleURL(newValue))
        return;
    }
    else
    {
      string input = newValue.Trim();
      string lower = input.ToLower();
      if (PXDBWeblinkAttribute.regex.IsMatch(input) && !lower.StartsWith("javascript:") && !lower.StartsWith("vbscript:"))
        return;
    }
    throw new PXSetPropertyException("The link URL does not have the correct format.");
  }
}
